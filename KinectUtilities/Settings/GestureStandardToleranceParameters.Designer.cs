﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.586
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KinectUtilities.Settings {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class GestureStandardToleranceParameters : global::System.Configuration.ApplicationSettingsBase {
        
        private static GestureStandardToleranceParameters defaultInstance = ((GestureStandardToleranceParameters)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new GestureStandardToleranceParameters())));
        
        public static GestureStandardToleranceParameters Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.1")]
        public double JointAngleTolerance {
            get {
                return ((double)(this["JointAngleTolerance"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("6")]
        public double FramesPerSecondCapture {
            get {
                return ((double)(this["FramesPerSecondCapture"]));
            }
        }
    }
}