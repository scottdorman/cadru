//------------------------------------------------------------------------------
// <copyright file="WriteThisAssemblyCodeFile.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2020 Scott Dorman.
// </copyright>
//
// <license>
//    Licensed under the Microsoft Public License (Ms-PL) (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//    http://opensource.org/licenses/Ms-PL.html
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </license>
//------------------------------------------------------------------------------

using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

using Cadru.Build.Tasks.Internal;
using Cadru.Build.Tasks.Resources;

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Cadru.Build.Tasks
{
    /// <summary>
    /// Generates a temporary code file with the specified generated code
    /// fragment. Does not delete the file.
    /// </summary>
    /// <comment>Currently only supports writing .NET attributes.</comment>
    public class WriteThisAssemblyCodeFile : Task
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="WriteThisAssemblyCodeFile"/> task.
        /// </summary>
        public WriteThisAssemblyCodeFile() : base(Strings.ResourceManager)
        {
        }

        /// <summary>
        /// Language of code to generate. Language name can be any language for
        /// which a CodeDom provider is available. For example, "C#",
        /// "VisualBasic". Emitted file will have the default extension for that language.
        /// </summary>
        [Required]
        public string Language { get; set; }

        /// <summary>
        /// Description of attributes to write. Item include is the full type
        /// name of the attribute. For example,
        /// "System.AssemblyVersionAttribute". Each piece of metadata is the
        /// name-value pair of a parameter, which must be of type System.String.
        /// Some attributes only allow positional constructor arguments, or the
        /// user may just prefer them. To set those, use metadata names like
        /// "_Parameter1", "_Parameter2" etc. If a parameter index is skipped,
        /// it's an error.
        /// </summary>
        [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "<Pending>")]
        public ITaskItem[] AssemblyAttributes { get; set; }

        /// <summary>
        /// Destination folder for the generated code. Typically the
        /// intermediate folder.
        /// </summary>
        public ITaskItem OutputDirectory { get; set; }

        /// <summary>
        /// The path to the file that was generated. If this is set, and a file
        /// name, the destination folder will be prepended. If this is set, and
        /// is rooted, the destination folder will be ignored. If this is not
        /// set, the destination folder will be used, an arbitrary file name
        /// will be used, and the default extension for the language selected.
        /// </summary>
        [Output]
        public ITaskItem OutputFile { get; set; }

        /// <summary>
        /// Main entry point.
        /// </summary>
        public override bool Execute()
        {
            if (String.IsNullOrEmpty(this.Language))
            {
                this.Log.LogErrorWithCodeFromResources("General.InvalidValue", nameof(this.Language), "WriteThisAssemblyCodeFile");
                return false;
            }

            if (this.OutputFile == null && this.OutputDirectory == null)
            {
                this.Log.LogErrorWithCodeFromResources("WriteCodeFragment.MustSpecifyLocation");
                return false;
            }

            var code = this.GenerateCode(out var extension);

            if (this.Log.HasLoggedErrors)
            {
                return false;
            }

            if (code.Length == 0)
            {
                this.Log.LogMessageFromResources(MessageImportance.Low, "WriteCodeFragment.NoWorkToDo");
                this.OutputFile = null;
                return true;
            }

            try
            {
                if (this.OutputFile != null && this.OutputDirectory != null && !Path.IsPathRooted(this.OutputFile.ItemSpec))
                {
                    this.OutputFile = new TaskItem(Path.Combine(this.OutputDirectory.ItemSpec, this.OutputFile.ItemSpec));
                }

                this.OutputFile = this.OutputFile ?? new TaskItem(FileUtilities.GetTemporaryFile(this.OutputDirectory.ItemSpec, extension));

                File.WriteAllText(this.OutputFile.ItemSpec, code); // Overwrites file if it already exists (and can be overwritten)
            }
            catch (Exception ex) when (ExceptionHandling.IsIoRelatedException(ex))
            {
                this.Log.LogErrorWithCodeFromResources("WriteCodeFragment.CouldNotWriteOutput", (this.OutputFile == null) ? String.Empty : this.OutputFile.ItemSpec, ex.Message);
                return false;
            }

            this.Log.LogMessageFromResources(MessageImportance.Low, "WriteCodeFragment.GeneratedFile", this.OutputFile.ItemSpec);

            return !this.Log.HasLoggedErrors;
        }

        private static CodeMemberField CreateConstant(string name, object value, CodeTypeReference codeTypeReference)
        {
            var member = new CodeMemberField(codeTypeReference, name);
            member.Attributes = (member.Attributes & ~MemberAttributes.AccessMask & ~MemberAttributes.ScopeMask) | MemberAttributes.Public | MemberAttributes.Const;
            member.InitExpression = new CodePrimitiveExpression(value);
            return member;
        }

        /// <summary>
        /// Generates the code into a string. If it fails, logs an error and
        /// returns null. If no meaningful code is generated, returns empty
        /// string. Returns the default language extension as an out parameter.
        /// </summary>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.IO.StringWriter.#ctor(System.Text.StringBuilder)", Justification = "Reads fine to me")]
        private string GenerateCode(out string extension)
        {
            var excludeAttributes = new List<string>
            {
                "InternalsVisibleTo"
            };

            extension = null;
            var haveGeneratedContent = false;

            CodeDomProvider provider;

            try
            {
                provider = CodeDomProvider.CreateProvider(this.Language);
            }
            catch (SystemException e) when
#if FEATURE_SYSTEM_CONFIGURATION
            (e is ConfigurationException || e is SecurityException)
#else
            (e.GetType().Name == "ConfigurationErrorsException") //TODO: catch specific exception type once it is public https://github.com/dotnet/corefx/issues/40456
#endif
            {
                this.Log.LogErrorWithCodeFromResources("WriteCodeFragment.CouldNotCreateProvider", this.Language, e.Message);
                return null;
            }

            extension = provider.FileExtension;

            var unit = new CodeCompileUnit();

            var globalNamespace = new CodeNamespace();
            unit.Namespaces.Add(globalNamespace);

            // Declare authorship. Unfortunately CodeDOM puts this comment after
            // the attributes.
            var comment = String.Format(Strings.WriteCodeFragment_Comment, this.GetType().Name);
            globalNamespace.Comments.Add(new CodeCommentStatement(comment));

            if (this.AssemblyAttributes == null)
            {
                return String.Empty;
            }

            // For convenience, bring in the namespaces, where many assembly
            // attributes lie
            globalNamespace.Imports.Add(new CodeNamespaceImport("System"));
            var codeTypeMembers = new CodeTypeMemberCollection();

            var rawFieldData = new Dictionary<string, List<KeyValuePair<string, object>>>();

            foreach (var attributeItem in this.AssemblyAttributes)
            {
                // All of the possible assembly attributes take a single
                // constructor argument with the exception of AssemblyMetadata.
                // That means, they all have only one entry in the metadata
                // collection except AssemblyMetadata attributes, which will
                // have two.
                var customMetadata = attributeItem.CloneCustomMetadata();

                foreach (DictionaryEntry entry in customMetadata)
                {
                    var name = (string)entry.Key;
                    var value = (string)entry.Value;
                    if (name.StartsWith("_Parameter", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!rawFieldData.TryGetValue(attributeItem.ItemSpec, out var data))
                        {
                            data = new List<KeyValuePair<string, object>>();
                            rawFieldData.Add(attributeItem.ItemSpec, data);
                        }

                        data.Add(new KeyValuePair<string, object>(name, value));
                    }
                }
            }

            foreach (var entry in rawFieldData)
            {
                var name = entry.Key.Substring(entry.Key.LastIndexOf('.') + 1).Replace("Attribute", "");

                if (excludeAttributes.Contains(name))
                {
                    continue;
                }

                if (entry.Value.Count == 1)
                {
                    var expressionValue = entry.Value.First().Value;
                    codeTypeMembers.Add(CreateConstant(name, expressionValue, new CodeTypeReference(typeof(string))));
                }
                else
                {
                    for (var i = 0; i < entry.Value.Count; i += 2)
                    {
                        name = (string)entry.Value.ElementAt(i).Value;
                        var expressionValue = entry.Value.ElementAt(i + 1).Value;
                        codeTypeMembers.Add(CreateConstant(name, expressionValue, new CodeTypeReference(typeof(string))));
                    }
                }

                haveGeneratedContent = true;
            }

            var thisAssembly = new CodeTypeDeclaration("ThisAssembly")
            {
                IsStruct = true
            };

            thisAssembly.Members.AddRange(codeTypeMembers);
            globalNamespace.Types.Add(thisAssembly);

            var generatedCode = new StringBuilder();
            using (var writer = new StringWriter(generatedCode, CultureInfo.CurrentCulture))
            {
                generatedCode.AppendLine(new CodeWarningPragmaDirective(WarningPragmaMode.Disable, new string[] { "CS1591" }).ToPragmaString(this.Language));
                provider.GenerateCodeFromCompileUnit(unit, writer, new CodeGeneratorOptions());
                generatedCode.AppendLine(new CodeWarningPragmaDirective(WarningPragmaMode.Restore, new string[] { "CS1591" }).ToPragmaString(this.Language));
            }

            var code = generatedCode.ToString();

            // If we just generated infrastructure, don't bother returning
            // anything as there's no point writing the file
            return haveGeneratedContent ? code : String.Empty;
        }
    }
}