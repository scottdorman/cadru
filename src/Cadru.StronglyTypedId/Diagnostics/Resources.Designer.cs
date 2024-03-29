﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cadru.StronglyTypedId.Diagnostics {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Cadru.StronglyTypedId.Diagnostics.Resources", typeof(Resources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to STI14.
        /// </summary>
        internal static string InvalidBackingTypeDiagnostic_Id {
            get {
                return ResourceManager.GetString("InvalidBackingTypeDiagnostic_Id", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The BackingType value provided is not valid and no globally configured default was found..
        /// </summary>
        internal static string InvalidBackingTypeDiagnostic_Message {
            get {
                return ResourceManager.GetString("InvalidBackingTypeDiagnostic_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid backing type.
        /// </summary>
        internal static string InvalidBackingTypeDiagnostic_Title {
            get {
                return ResourceManager.GetString("InvalidBackingTypeDiagnostic_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ST11.
        /// </summary>
        internal static string InvalidConfigurationDiagnostic_Id {
            get {
                return ResourceManager.GetString("InvalidConfigurationDiagnostic_Id", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        internal static string InvalidConfigurationDiagnostic_Message {
            get {
                return ResourceManager.GetString("InvalidConfigurationDiagnostic_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .
        /// </summary>
        internal static string InvalidConfigurationDiagnostic_Title {
            get {
                return ResourceManager.GetString("InvalidConfigurationDiagnostic_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ST15.
        /// </summary>
        internal static string InvalidConverterDiagnostic_Id {
            get {
                return ResourceManager.GetString("InvalidConverterDiagnostic_Id", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The StronglyTypedIdConverter value provided is not valid and no globally configured default was found..
        /// </summary>
        internal static string InvalidConverterDiagnostic_Message {
            get {
                return ResourceManager.GetString("InvalidConverterDiagnostic_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid converters.
        /// </summary>
        internal static string InvalidConverterDiagnostic_Title {
            get {
                return ResourceManager.GetString("InvalidConverterDiagnostic_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to STI2.
        /// </summary>
        internal static string NotPartialDiagnostic_Id {
            get {
                return ResourceManager.GetString("NotPartialDiagnostic_Id", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The target of the StronglyTypedId attribute must be declared as partial..
        /// </summary>
        internal static string NotPartialDiagnostic_Message {
            get {
                return ResourceManager.GetString("NotPartialDiagnostic_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Must be partial.
        /// </summary>
        internal static string NotPartialDiagnostic_Title {
            get {
                return ResourceManager.GetString("NotPartialDiagnostic_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to STI3.
        /// </summary>
        internal static string NotRecordDiagnostic_Id {
            get {
                return ResourceManager.GetString("NotRecordDiagnostic_Id", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The target of the StronglyTypedId attribute must be declared as a record or a record struct..
        /// </summary>
        internal static string NotRecordDiagnostic_Message {
            get {
                return ResourceManager.GetString("NotRecordDiagnostic_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Must be record or record struct.
        /// </summary>
        internal static string NotRecordDiagnostic_Title {
            get {
                return ResourceManager.GetString("NotRecordDiagnostic_Title", resourceCulture);
            }
        }
    }
}
