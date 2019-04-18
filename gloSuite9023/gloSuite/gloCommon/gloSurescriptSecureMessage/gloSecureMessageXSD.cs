

using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel;


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class gloSecureMail : INotifyPropertyChanged

{

    private string sAttachmentID = "";

    private string sDocumentName = "";

    private long nSecureMessageInboxIDField;
    
    private string rowNoField = "";

    private bool bIsReadField;
    
    private string nNoOfAttachmentsField = "";
    
    private string fromField = "";
    
    private string sToField = "";
    
    private string subjectField = "";
    
    private string receivedField = "";
    
    private string sMessageBodyField = "";
    
    private string statusCodeField = "";
    
    private string statusDescriptionField = "";
    
    private string nUseCaseField = "";
    
    private string eMailNameField = "";

    private string sDelegatedUser = "";    
    
    private string dtSendReceiveDateTimeField = "";
    
    private string sCDAConfidentialityField = "";

    //No ofRAttachments
    private int _noOfRAttachements = 0;
   
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public int noOfRAttachements
    {
        get { return _noOfRAttachements; }
        set { _noOfRAttachements = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string AttachmentID
    {
        get
        {
            return this.sAttachmentID;
        }
        set
        {
            this.sAttachmentID = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DocumentName
    {
        get
        {
            return this.sDocumentName;
        }
        set
        {
            this.sDocumentName = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public long nSecureMessageInboxID {
        get {
            return this.nSecureMessageInboxIDField;
        }
        set {
            this.nSecureMessageInboxIDField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string RowNo {
        get {
            return this.rowNoField;
        }
        set {
            this.rowNoField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public bool bIsRead {
        get {
            return this.bIsReadField;
        }
        set {
            this.bIsReadField = value;

            this.RaisePropertyChangedEvent("bIsRead");
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string nNoOfAttachments {
        get {
            return this.nNoOfAttachmentsField;
        }
        set {
            this.nNoOfAttachmentsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string From {
        get {
            return this.fromField;
        }
        set {
            this.fromField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string sTo {
        get {
            return this.sToField;
        }
        set {
            this.sToField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Subject {
        get {
            return this.subjectField;
        }
        set {
            this.subjectField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Received {
        get {
            return this.receivedField;
        }
        set {
            this.receivedField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string sMessageBody {
        get {
            return this.sMessageBodyField;
        }
        set {
            this.sMessageBodyField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string StatusCode {
        get {
            return this.statusCodeField;
        }
        set {
            this.statusCodeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string StatusDescription {
        get {
            return this.statusDescriptionField;
        }
        set {
            this.statusDescriptionField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string nUseCase {
        get {
            return this.nUseCaseField;
        }
        set {
            this.nUseCaseField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string EMailName {
        get {
            return this.eMailNameField;
        }
        set {
            this.eMailNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string DelegatedUser
    {
        get
        {
            return this.sDelegatedUser;
        }
        set
        {
            this.sDelegatedUser = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string dtSendReceiveDateTime {
        get {
            return this.dtSendReceiveDateTimeField;
        }
        set {
            this.dtSendReceiveDateTimeField = value;
        }
    }

    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string sCDAConfidentiality
    {
        get
        {
            return this.sCDAConfidentialityField;
        }
        set
        {
            this.sCDAConfidentialityField = value;
        }
    }

    private void RaisePropertyChangedEvent(string PropertyName)
    {
        if (this.PropertyChanged != null)
        { this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName)); }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
