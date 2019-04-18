
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System;
namespace gloGlobal.Schemas.PDR.Acknowledgement
{

    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "confirmation")]
    public partial class AcknowledgementRequest : IDisposable
    {
        private List<program> programsField;

        private string transactionIDField;

        
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("program", typeof(program), Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<program> programs
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

        public AcknowledgementRequest() { this.programs = new List<program>(); }


        public void Dispose()
        {
            if (this.programs != null)
            {
                foreach (program p in this.programs)
                { p.Dispose(); }
                this.programs.Clear();
                this.programs = null;
            }
            this.transactionID = string.Empty;
        }
    }

    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class program : IDisposable
    {
        public program() { this.id = ""; }
        public program(string ID) { this.id = ID; }

        private string idField;

        
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

        public void Dispose()
        {
            this.id = string.Empty;
        }
    }


    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false,ElementName = "response")]
    public partial class printConfirmationResponse { }
}