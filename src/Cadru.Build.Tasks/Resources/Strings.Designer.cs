﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cadru.Build.Tasks.Resources
{
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings
    {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings()
        {
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Cadru.Build.Tasks.Resources.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to MSB3098: &quot;{1}&quot; task received an invalid value for the &quot;{0}&quot; parameter..
        /// </summary>
        internal static string General_InvalidValue
        {
            get
            {
                return ResourceManager.GetString("General.InvalidValue", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to MSB5003: Failed to create a temporary file. Temporary files folder is full or its path is incorrect. {0}.
        /// </summary>
        internal static string Shared_FailedCreatingTempFile
        {
            get
            {
                return ResourceManager.GetString("Shared.FailedCreatingTempFile", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Parameter &quot;{0}&quot; cannot be null..
        /// </summary>
        internal static string Shared_ParameterCannotBeNull
        {
            get
            {
                return ResourceManager.GetString("Shared.ParameterCannotBeNull", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Parameter &quot;{0}&quot; cannot have zero length..
        /// </summary>
        internal static string Shared_ParameterCannotHaveZeroLength
        {
            get
            {
                return ResourceManager.GetString("Shared.ParameterCannotHaveZeroLength", resourceCulture);
            }
        }

        internal static string WriteCodeFragment_AutoGeneratedComment
        {
            get
            {
                return ResourceManager.GetString("WriteCodeFragment.AutoGeneratedComment", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Generated by the {0} class..
        /// </summary>
        internal static string WriteCodeFragment_Comment
        {
            get
            {
                return ResourceManager.GetString("WriteCodeFragment.Comment", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to MSB3712: Code for the language &quot;{0}&quot; could not be generated. {1}.
        /// </summary>
        internal static string WriteCodeFragment_CouldNotCreateProvider
        {
            get
            {
                return ResourceManager.GetString("WriteCodeFragment.CouldNotCreateProvider", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to MSB3713: The file &quot;{0}&quot; could not be created. {1}.
        /// </summary>
        internal static string WriteCodeFragment_CouldNotWriteOutput
        {
            get
            {
                return ResourceManager.GetString("WriteCodeFragment.CouldNotWriteOutput", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Emitted specified code into &quot;{0}&quot;..
        /// </summary>
        internal static string WriteCodeFragment_GeneratedFile
        {
            get
            {
                return ResourceManager.GetString("WriteCodeFragment.GeneratedFile", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to MSB3711: At least one of OutputFile or OutputDirectory must be provided..
        /// </summary>
        internal static string WriteCodeFragment_MustSpecifyLocation
        {
            get
            {
                return ResourceManager.GetString("WriteCodeFragment.MustSpecifyLocation", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to No output file was written because no code was specified to create..
        /// </summary>
        internal static string WriteCodeFragment_NoWorkToDo
        {
            get
            {
                return ResourceManager.GetString("WriteCodeFragment.NoWorkToDo", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to MSB3714: The parameter &quot;{0}&quot; was supplied, but not all previously numbered parameters..
        /// </summary>
        internal static string WriteCodeFragment_SkippedNumberedParameter
        {
            get
            {
                return ResourceManager.GetString("WriteCodeFragment.SkippedNumberedParameter", resourceCulture);
            }
        }
    }
}
