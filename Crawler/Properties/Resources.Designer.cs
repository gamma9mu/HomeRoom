﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17379
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HomeRoom.Properties {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("HomeRoom.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to 79468C520824589EBDAAFEBF29C53AFD6D0CF891.
        /// </summary>
        internal static string AppId {
            get {
                return ResourceManager.GetString("AppId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://schemas.microsoft.com/LiveSearch/2008/04/XML/multimedia.
        /// </summary>
        internal static string imageSchema {
            get {
                return ResourceManager.GetString("imageSchema", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://api.bing.net/xml.aspx?Appid={0}&amp;sources={1}&amp;query={2}&amp;{1}.offset={3}&amp;{1}.count={4}.
        /// </summary>
        internal static string SearchUrl {
            get {
                return ResourceManager.GetString("SearchUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://schemas.microsoft.com/LiveSearch/2008/04/XML/web.
        /// </summary>
        internal static string webSchema {
            get {
                return ResourceManager.GetString("webSchema", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://opensearch.org/searchsuggest2.
        /// </summary>
        internal static string WikipediaResponseNs {
            get {
                return ResourceManager.GetString("WikipediaResponseNs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to http://en.wikipedia.org/w/api.php?format=xml&amp;action=opensearch&amp;search={0}&amp;limit={1}.
        /// </summary>
        internal static string WikipediaUrl {
            get {
                return ResourceManager.GetString("WikipediaUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to HomeRoom (http://www.cs.siu.edu/cs498/group2/).
        /// </summary>
        internal static string WikipediaUserAgent {
            get {
                return ResourceManager.GetString("WikipediaUserAgent", resourceCulture);
            }
        }
    }
}
