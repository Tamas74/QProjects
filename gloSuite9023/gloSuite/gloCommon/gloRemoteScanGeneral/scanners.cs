﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.0.30319.1.
// 
namespace gloRemoteScanGeneral {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class Scanners {
        
        private ScannersInstalledScanners[] itemsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("InstalledScanners")]
        public ScannersInstalledScanners[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="")]
    public partial class ScannersInstalledScanners {
        
        private string installedScannersIDField;
        
        private string defaultField;
        
        private ScannersInstalledScannersScanner[] scannerField;
        
        /// <remarks/>
        public string InstalledScannersID {
            get {
                return this.installedScannersIDField;
            }
            set {
                this.installedScannersIDField = value;
            }
        }
        
        /// <remarks/>
        public string Default {
            get {
                return this.defaultField;
            }
            set {
                this.defaultField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Scanner")]
        public ScannersInstalledScannersScanner[] Scanner {
            get {
                return this.scannerField;
            }
            set {
                this.scannerField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="")]
    public partial class ScannersInstalledScannersScanner {
        
        private string scannerIDField;
        
        private string nameField;
        
        private string isDefaultField;
        
        private string modeField;
        
        private string scanResolutionField;
        
        private string scanBrightnessField;
        
        private string scanContrastField;
        
        private string scanScanSideField;
        
        private string scanSupportedSizeField;
        
        private ScannersInstalledScannersScannerScanMode[] scanModeField;
        
        private ScannersInstalledScannersScannerResolution[] resolutionField;
        
        private ScannersInstalledScannersScannerBrightness[] brightnessField;
        
        private ScannersInstalledScannersScannerContrast[] contrastField;
        
        private ScannersInstalledScannersScannerScanSide[] scanSideField;
        
        private ScannersInstalledScannersScannerSupportedSize[] supportedSizeField;
        
        /// <remarks/>
        public string ScannerID {
            get {
                return this.scannerIDField;
            }
            set {
                this.scannerIDField = value;
            }
        }
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public string IsDefault {
            get {
                return this.isDefaultField;
            }
            set {
                this.isDefaultField = value;
            }
        }
        
        /// <remarks/>
        public string Mode {
            get {
                return this.modeField;
            }
            set {
                this.modeField = value;
            }
        }
        
        /// <remarks/>
        public string ScanResolution {
            get {
                return this.scanResolutionField;
            }
            set {
                this.scanResolutionField = value;
            }
        }
        
        /// <remarks/>
        public string ScanBrightness {
            get {
                return this.scanBrightnessField;
            }
            set {
                this.scanBrightnessField = value;
            }
        }
        
        /// <remarks/>
        public string ScanContrast {
            get {
                return this.scanContrastField;
            }
            set {
                this.scanContrastField = value;
            }
        }
        
        /// <remarks/>
        public string ScanScanSide {
            get {
                return this.scanScanSideField;
            }
            set {
                this.scanScanSideField = value;
            }
        }
        
        /// <remarks/>
        public string ScanSupportedSize {
            get {
                return this.scanSupportedSizeField;
            }
            set {
                this.scanSupportedSizeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ScanMode")]
        public ScannersInstalledScannersScannerScanMode[] ScanMode {
            get {
                return this.scanModeField;
            }
            set {
                this.scanModeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Resolution")]
        public ScannersInstalledScannersScannerResolution[] Resolution {
            get {
                return this.resolutionField;
            }
            set {
                this.resolutionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Brightness")]
        public ScannersInstalledScannersScannerBrightness[] Brightness {
            get {
                return this.brightnessField;
            }
            set {
                this.brightnessField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Contrast")]
        public ScannersInstalledScannersScannerContrast[] Contrast {
            get {
                return this.contrastField;
            }
            set {
                this.contrastField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ScanSide")]
        public ScannersInstalledScannersScannerScanSide[] ScanSide {
            get {
                return this.scanSideField;
            }
            set {
                this.scanSideField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SupportedSize")]
        public ScannersInstalledScannersScannerSupportedSize[] SupportedSize {
            get {
                return this.supportedSizeField;
            }
            set {
                this.supportedSizeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="")]
    public partial class ScannersInstalledScannersScannerScanMode {
        
        private string scanModeIDField;
        
        private string nameField;
        
        private string depthField;
        
        private ScannersInstalledScannersScannerScanModeScanDepth[] scanDepthField;
        
        /// <remarks/>
        public string ScanModeID {
            get {
                return this.scanModeIDField;
            }
            set {
                this.scanModeIDField = value;
            }
        }
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public string Depth {
            get {
                return this.depthField;
            }
            set {
                this.depthField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ScanDepth")]
        public ScannersInstalledScannersScannerScanModeScanDepth[] ScanDepth {
            get {
                return this.scanDepthField;
            }
            set {
                this.scanDepthField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="")]
    public partial class ScannersInstalledScannersScannerScanModeScanDepth {
        
        private string scanDepthIdField;
        
        private string nameField;
        
        /// <remarks/>
        public string ScanDepthId {
            get {
                return this.scanDepthIdField;
            }
            set {
                this.scanDepthIdField = value;
            }
        }
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="")]
    public partial class ScannersInstalledScannersScannerResolution {
        
        private string resolutionIDField;
        
        private string nameField;
        
        /// <remarks/>
        public string ResolutionID {
            get {
                return this.resolutionIDField;
            }
            set {
                this.resolutionIDField = value;
            }
        }
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="")]
    public partial class ScannersInstalledScannersScannerBrightness {
        
        private string brightnessIDField;
        
        private string nameField;
        
        /// <remarks/>
        public string BrightnessID {
            get {
                return this.brightnessIDField;
            }
            set {
                this.brightnessIDField = value;
            }
        }
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="")]
    public partial class ScannersInstalledScannersScannerContrast {
        
        private string contrastIDField;
        
        private string nameField;
        
        /// <remarks/>
        public string ContrastID {
            get {
                return this.contrastIDField;
            }
            set {
                this.contrastIDField = value;
            }
        }
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="")]
    public partial class ScannersInstalledScannersScannerScanSide {
        
        private string scanSideIDField;
        
        private string nameField;
        
        /// <remarks/>
        public string ScanSideID {
            get {
                return this.scanSideIDField;
            }
            set {
                this.scanSideIDField = value;
            }
        }
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="")]
    public partial class ScannersInstalledScannersScannerSupportedSize {
        
        private string supportedSizeIDField;
        
        private string nameField;
        
        private string lengthField;
        
        private string leftField;
        
        private string topField;
        
        private string widthField;
        
        /// <remarks/>
        public string SupportedSizeID {
            get {
                return this.supportedSizeIDField;
            }
            set {
                this.supportedSizeIDField = value;
            }
        }
        
        /// <remarks/>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public string Length {
            get {
                return this.lengthField;
            }
            set {
                this.lengthField = value;
            }
        }
        
        /// <remarks/>
        public string Left {
            get {
                return this.leftField;
            }
            set {
                this.leftField = value;
            }
        }
        
        /// <remarks/>
        public string Top {
            get {
                return this.topField;
            }
            set {
                this.topField = value;
            }
        }
        
        /// <remarks/>
        public string Width {
            get {
                return this.widthField;
            }
            set {
                this.widthField = value;
            }
        }
    }
}
