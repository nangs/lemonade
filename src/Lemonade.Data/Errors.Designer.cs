﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lemonade.Core {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Errors {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Errors() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Lemonade.Core.Errors", typeof(Errors).Assembly);
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
        ///   Looks up a localized string similar to Could not create the specified application.
        /// </summary>
        internal static string FailedToCreatedApplication {
            get {
                return ResourceManager.GetString("FailedToCreatedApplication", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not create the specified feature.
        /// </summary>
        internal static string FailedToCreateFeature {
            get {
                return ResourceManager.GetString("FailedToCreateFeature", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not delete the specified application.
        /// </summary>
        internal static string FailedToDeleteApplication {
            get {
                return ResourceManager.GetString("FailedToDeleteApplication", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not delete the specified feature.
        /// </summary>
        internal static string FailedToDeleteFeature {
            get {
                return ResourceManager.GetString("FailedToDeleteFeature", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not update the specified application.
        /// </summary>
        internal static string FailedToUpdateApplication {
            get {
                return ResourceManager.GetString("FailedToUpdateApplication", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not update the specified feature.
        /// </summary>
        internal static string FailedToUpdateFeature {
            get {
                return ResourceManager.GetString("FailedToUpdateFeature", resourceCulture);
            }
        }
    }
}
