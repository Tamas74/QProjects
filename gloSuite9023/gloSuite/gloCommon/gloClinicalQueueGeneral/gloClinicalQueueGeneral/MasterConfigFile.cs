﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.0.30319.1.
// 
namespace gloClinicalQueueGeneral {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class MasterConfigFile {
        
        private MasterConfigFileMasterConfig[] itemsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MasterConfig", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public MasterConfigFileMasterConfig[] Items {
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class MasterConfigFileMasterConfig {
        
        private long masterConfigIdField;
        
        private string installedPrintersFileField;
        
        private string currentDefaultPrinterField;
        
        private MasterConfigFileMasterConfigModulePrinters[] modulePrintersField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long MasterConfigId {
            get {
                return this.masterConfigIdField;
            }
            set {
                this.masterConfigIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string InstalledPrintersFile {
            get {
                return this.installedPrintersFileField;
            }
            set {
                this.installedPrintersFileField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CurrentDefaultPrinter {
            get {
                return this.currentDefaultPrinterField;
            }
            set {
                this.currentDefaultPrinterField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ModulePrinters", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public MasterConfigFileMasterConfigModulePrinters[] ModulePrinters {
            get {
                return this.modulePrintersField;
            }
            set {
                this.modulePrintersField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class MasterConfigFileMasterConfigModulePrinters {
        
        private long modulePrintersIdField;
        
        private string moduleNameField;
        
        private string printerNameField;
        
        private string printerTypeField;
        
        private string settingFileField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long ModulePrintersId {
            get {
                return this.modulePrintersIdField;
            }
            set {
                this.modulePrintersIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ModuleName {
            get {
                return this.moduleNameField;
            }
            set {
                this.moduleNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PrinterName {
            get {
                return this.printerNameField;
            }
            set {
                this.printerNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PrinterType {
            get {
                return this.printerTypeField;
            }
            set {
                this.printerTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SettingFile {
            get {
                return this.settingFileField;
            }
            set {
                this.settingFileField = value;
            }
        }
    }
}
