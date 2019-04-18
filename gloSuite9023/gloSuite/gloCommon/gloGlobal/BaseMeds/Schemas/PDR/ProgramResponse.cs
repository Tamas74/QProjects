using System.Xml.Serialization;
using System.Collections.Generic;
using System;

namespace gloGlobal.Schemas.PDR
{
    public partial class ProgramResponse : IDisposable
    {
        public List<Program> Programs {get;set;}
        public string TransactionID { get; set; }
        public string RxNumber { get; set; }

        public void Dispose()
        {
            if (this.Programs != null)
            {
                foreach (Program p in this.Programs) { p.Dispose(); }
                this.Programs.Clear();
                this.Programs = null;
            }

            this.TransactionID = string.Empty;
            this.RxNumber = string.Empty;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false, ElementName="response")]
    public partial class ProgramResponseBase : IDisposable
    {

        private List<Program> programsField;

        private string transactionIDField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("program", IsNullable = false)]
        public List<Program> programs
        {
            get
            {
                return this.programsField;
            }
            set
            {
                this.programsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string transactionID
        {
            get
            {
                return this.transactionIDField;
            }
            set
            {
                this.transactionIDField = value;
            }
        }

        public void Dispose()
        {
            if (this.programs != null)
            {
                foreach (Program p in this.programs) { p.Dispose(); }
                this.programs.Clear();
                this.programs = null;
            }

            this.transactionID = string.Empty;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class Program : IDisposable
    {
        private string sProgramFilePath = "";
        private string nameField = "";

        private bool paidField = false;

        private string imageField = "";

        private string paymentNotesField = "";

        private string binField = "";

        private bool binFieldSpecified = false;

        private string pcnField = "";

        private string groupField = "";

        private string cardholderIDField = "";

        private string idField = "";

        private string typeField = "";

        [XmlIgnore]
        public string programFilePath
        {
            get { return this.sProgramFilePath; }
            set { this.sProgramFilePath = value; }
        }

        /// <remarks/>
        public string name
        {
            get { return this.nameField; }
            set { this.nameField = value; }
        }

        /// <remarks/>
        public bool paid
        {
            get { return this.paidField; }
            set { this.paidField = value; }
        }

        /// <remarks/>
        public string image
        {
            get { return this.imageField; }
            set { this.imageField = value; }
        }

        /// <remarks/>
        public string paymentNotes
        {
            get { return this.paymentNotesField; }
            set { this.paymentNotesField = value; }
        }

        /// <remarks/>
        public string bin
        {
            get { return this.binField; }
            set { this.binField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool binSpecified
        {
            get
            {
                return this.binFieldSpecified;
            }
            set
            {
                this.binFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string pcn
        {
            get
            {
                return this.pcnField;
            }
            set
            {
                this.pcnField = value;
            }
        }

        /// <remarks/>
        public string group
        {
            get
            {
                return this.groupField;
            }
            set
            {
                this.groupField = value;
            }
        }

        /// <remarks/>
        public string cardholderID
        {
            get
            {
                return this.cardholderIDField;
            }
            set
            {
                this.cardholderIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        public void Dispose()
        {
            this.bin = string.Empty;
            this.binSpecified = false;
            this.cardholderID = string.Empty;
            this.group = string.Empty;
            this.id = string.Empty;
            this.image = string.Empty;
            this.name = string.Empty;
            this.paymentNotes = string.Empty;
            this.pcn = string.Empty;
            this.type = string.Empty;
        }
    }
}


