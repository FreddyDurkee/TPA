﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TUIApplication.Properties {
    using System;
    
    
    /// <summary>
    ///   Klasa zasobu wymagająca zdefiniowania typu do wyszukiwania zlokalizowanych ciągów itd.
    /// </summary>
    // Ta klasa została automatycznie wygenerowana za pomocą klasy StronglyTypedResourceBuilder
    // przez narzędzie, takie jak ResGen lub Visual Studio.
    // Aby dodać lub usunąć składową, edytuj plik ResX, a następnie ponownie uruchom narzędzie ResGen
    // z opcją /str lub ponownie utwórz projekt VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        /// Zwraca buforowane wystąpienie ResourceManager używane przez tę klasę.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TUIApplication.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Przesłania właściwość CurrentUICulture bieżącego wątku dla wszystkich
        ///   przypadków przeszukiwania zasobów za pomocą tej klasy zasobów wymagającej zdefiniowania typu.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Give me the path, please: .
        /// </summary>
        public static string askDLLPath {
            get {
                return ResourceManager.GetString("askDLLPath", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Goodbye. :(.
        /// </summary>
        public static string goodbye {
            get {
                return ResourceManager.GetString("goodbye", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Incorrect input!.
        /// </summary>
        public static string incorrectInput {
            get {
                return ResourceManager.GetString("incorrectInput", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Serialized!.
        /// </summary>
        public static string serializeMsg {
            get {
                return ResourceManager.GetString("serializeMsg", resourceCulture);
            }
        }
        
        /// <summary>
        /// Wyszukuje zlokalizowany ciąg podobny do ciągu Welcome my friend!\nWanna read some dll?.
        /// </summary>
        public static string welcomeMsg {
            get {
                return ResourceManager.GetString("welcomeMsg", resourceCulture);
            }
        }
    }
}
