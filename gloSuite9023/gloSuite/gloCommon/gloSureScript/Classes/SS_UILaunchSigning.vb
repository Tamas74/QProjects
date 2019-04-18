﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.18444
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System.Xml.Serialization
Namespace UILaunchSigning

    '
    'This source code was auto-generated by xsd, Version=4.0.30319.1.
    '

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0"), _
     System.Xml.Serialization.XmlRootAttribute([Namespace]:="urn:drfirst.com:epcsapi:v1_0", IsNullable:=False)> _
    Partial Public Class EpcsRequest

        Private epcsRequestHeaderField As EpcsRequestEpcsRequestHeader

        Private epcsRequestBodyField As EpcsRequestEpcsRequestBody

        '''<remarks/>
        Public Property EpcsRequestHeader() As EpcsRequestEpcsRequestHeader
            Get
                Return Me.epcsRequestHeaderField
            End Get
            Set(value As EpcsRequestEpcsRequestHeader)
                Me.epcsRequestHeaderField = value
            End Set
        End Property

        '''<remarks/>
        Public Property EpcsRequestBody() As EpcsRequestEpcsRequestBody
            Get
                Return Me.epcsRequestBodyField
            End Get
            Set(value As EpcsRequestEpcsRequestBody)
                Me.epcsRequestBodyField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsRequestEpcsRequestHeader

        Private vendorNameField As String

        Private vendorLabelField As String

        Private vendorNodeNameField As String

        Private vendorNodeLabelField As String

        Private appVersionField As EpcsRequestEpcsRequestHeaderAppVersion

        Private sourceOrganizationIdField As String
        Private EPCSApiVersionField As String

        Private dateField As String

        Private privateLabelField As EpcsRequestEpcsRequestHeaderPrivateLabel
        '''<remarks/>
        Public Property VendorName() As String
            Get
                Return Me.vendorNameField
            End Get
            Set(value As String)
                Me.vendorNameField = value
            End Set
        End Property

        '''<remarks/>
        Public Property VendorLabel() As String
            Get
                Return Me.vendorLabelField
            End Get
            Set(value As String)
                Me.vendorLabelField = value
            End Set
        End Property

        '''<remarks/>
        Public Property VendorNodeName() As String
            Get
                Return Me.vendorNodeNameField
            End Get
            Set(value As String)
                Me.vendorNodeNameField = value
            End Set
        End Property

        '''<remarks/>
        Public Property VendorNodeLabel() As String
            Get
                Return Me.vendorNodeLabelField
            End Get
            Set(value As String)
                Me.vendorNodeLabelField = value
            End Set
        End Property

        '''<remarks/>
        Public Property AppVersion() As EpcsRequestEpcsRequestHeaderAppVersion
            Get
                Return Me.appVersionField
            End Get
            Set(value As EpcsRequestEpcsRequestHeaderAppVersion)
                Me.appVersionField = value
            End Set
        End Property

        '''<remarks/>
        Public Property SourceOrganizationId() As String
            Get
                Return Me.sourceOrganizationIdField
            End Get
            Set(value As String)
                Me.sourceOrganizationIdField = value
            End Set
        End Property

        '''<remarks/>
        Public Property [Date]() As String
            Get
                Return Me.dateField
            End Get
            Set(value As String)
                Me.dateField = value
            End Set
        End Property

        '''<remarks/>
        Public Property EPCSApiVersion() As String
            Get
                Return Me.EPCSApiVersionField
            End Get
            Set(value As String)
                Me.EPCSApiVersionField = value
            End Set
        End Property
        Public Property PrivateLabel() As EpcsRequestEpcsRequestHeaderPrivateLabel
            Get
                Return Me.privateLabelField
            End Get
            Set(value As EpcsRequestEpcsRequestHeaderPrivateLabel)
                Me.privateLabelField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsRequestEpcsRequestHeaderAppVersion

        Private appNameField As String

        Private applicationVersionField As String

        Private textField() As String

        '''<remarks/>
        Public Property AppName() As String
            Get
                Return Me.appNameField
            End Get
            Set(value As String)
                Me.appNameField = value
            End Set
        End Property

        '''<remarks/>
        Public Property ApplicationVersion() As String
            Get
                Return Me.applicationVersionField
            End Get
            Set(value As String)
                Me.applicationVersionField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlTextAttribute()> _
        Public Property Text() As String()
            Get
                Return Me.textField
            End Get
            Set(value As String())
                Me.textField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsRequestEpcsRequestBody

        Private uiLaunchSigningRequestField As EpcsRequestEpcsRequestBodyUiLaunchSigningRequest

        '''<remarks/>
        Public Property UiLaunchSigningRequest() As EpcsRequestEpcsRequestBodyUiLaunchSigningRequest
            Get
                Return Me.uiLaunchSigningRequestField
            End Get
            Set(value As EpcsRequestEpcsRequestBodyUiLaunchSigningRequest)
                Me.uiLaunchSigningRequestField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsRequestEpcsRequestBodyUiLaunchSigningRequest

        Private sourceTransactionReferenceNumberField As String

        Private prescriberNpiField As String

        Private postBackUrlField As String

        Private closeWindowOnExitField As String

        Private patientPrescriptionDataListField As EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataList

        Private routerLabelField As String

        Private routePrescriptionViaEpcsField As String

        Private compressResponseWithGzipField As String

        '''<remarks/>
        Public Property SourceTransactionReferenceNumber() As String
            Get
                Return Me.sourceTransactionReferenceNumberField
            End Get
            Set(value As String)
                Me.sourceTransactionReferenceNumberField = value
            End Set
        End Property

        '''<remarks/>
        Public Property PrescriberNpi() As String
            Get
                Return Me.prescriberNpiField
            End Get
            Set(value As String)
                Me.prescriberNpiField = value
            End Set
        End Property

        '''<remarks/>
        Public Property PostBackUrl() As String
            Get
                Return Me.postBackUrlField
            End Get
            Set(value As String)
                Me.postBackUrlField = value
            End Set
        End Property

        '''<remarks/>
        Public Property CloseWindowOnExit() As String
            Get
                Return Me.closeWindowOnExitField
            End Get
            Set(value As String)
                Me.closeWindowOnExitField = value
            End Set
        End Property

        '''<remarks/>
        Public Property PatientPrescriptionDataList() As EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataList
            Get
                Return Me.patientPrescriptionDataListField
            End Get
            Set(value As EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataList)
                Me.patientPrescriptionDataListField = value
            End Set
        End Property

        '''<remarks/>
        Public Property RouterLabel() As String
            Get
                Return Me.routerLabelField
            End Get
            Set(value As String)
                Me.routerLabelField = value
            End Set
        End Property

        '''<remarks/>
        Public Property RoutePrescriptionViaEpcs() As String
            Get
                Return Me.routePrescriptionViaEpcsField
            End Get
            Set(value As String)
                Me.routePrescriptionViaEpcsField = value
            End Set
        End Property

        '''<remarks/>
        Public Property CompressResponseWithGzip() As String
            Get
                Return Me.compressResponseWithGzipField
            End Get
            Set(value As String)
                Me.compressResponseWithGzipField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataList

        Private patientPrescriptionDataField As EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataListPatientPrescriptionData

        '''<remarks/>
        Public Property PatientPrescriptionData() As EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataListPatientPrescriptionData
            Get
                Return Me.patientPrescriptionDataField
            End Get
            Set(value As EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataListPatientPrescriptionData)
                Me.patientPrescriptionDataField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataListPatientPrescriptionData

        Private patientField As EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataListPatientPrescriptionDataPatient

        Private prescriptionListField() As EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataListPatientPrescriptionDataPrescription

        '''<remarks/>
        Public Property Patient() As EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataListPatientPrescriptionDataPatient
            Get
                Return Me.patientField
            End Get
            Set(value As EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataListPatientPrescriptionDataPatient)
                Me.patientField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlArrayItemAttribute("Prescription", IsNullable:=False)> _
        Public Property PrescriptionList() As EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataListPatientPrescriptionDataPrescription()
            Get
                Return Me.prescriptionListField
            End Get
            Set(value As EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataListPatientPrescriptionDataPrescription())
                Me.prescriptionListField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataListPatientPrescriptionDataPatient

        Private externalPatientIdField As String

        Private firstnameField As String

        Private lastnameField As String

        Private middlenameField As String

        Private suffixField As String

        Private prefixField As String

        Private genderField As String

        Private dateofbirthField As String

        Private zipcodeField As String

        '''<remarks/>
        Public Property ExternalPatientId() As String
            Get
                Return Me.externalPatientIdField
            End Get
            Set(value As String)
                Me.externalPatientIdField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Firstname() As String
            Get
                Return Me.firstnameField
            End Get
            Set(value As String)
                Me.firstnameField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Lastname() As String
            Get
                Return Me.lastnameField
            End Get
            Set(value As String)
                Me.lastnameField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Middlename() As String
            Get
                Return Me.middlenameField
            End Get
            Set(value As String)
                Me.middlenameField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Suffix() As String
            Get
                Return Me.suffixField
            End Get
            Set(value As String)
                Me.suffixField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Prefix() As String
            Get
                Return Me.prefixField
            End Get
            Set(value As String)
                Me.prefixField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Gender() As String
            Get
                Return Me.genderField
            End Get
            Set(value As String)
                Me.genderField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Dateofbirth() As String
            Get
                Return Me.dateofbirthField
            End Get
            Set(value As String)
                Me.dateofbirthField = value
            End Set
        End Property

        Property Addressline1 As String

        Property Addressline2 As String

        Property City As String

        Property State As String

        '''<remarks/>
        Public Property Zipcode() As String
            Get
                Return Me.zipcodeField
            End Get
            Set(value As String)
                Me.zipcodeField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataListPatientPrescriptionDataPrescription

        Private sourcePrescriptionIdField As Long

        'Private sSMessageField As String
        Private sSMessageField As Xml.XmlDocument

        '''<remarks/>
        Public Property SourcePrescriptionId() As Long
            Get
                Return Me.sourcePrescriptionIdField
            End Get
            Set(value As Long)
                Me.sourcePrescriptionIdField = value
            End Set
        End Property

        '''<remarks/>
        Public Property SSMessage() As Xml.XmlDocument
            Get
                Return Me.sSMessageField
            End Get
            Set(value As Xml.XmlDocument)
                Me.sSMessageField = value
            End Set
        End Property
        'Public Property SSMessage() As String
        '    Get
        '        Return Me.sSMessageField
        '    End Get
        '    Set(value As String)
        '        Me.sSMessageField = value
        '    End Set
        'End Property
    End Class
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
    Partial Public Class EpcsRequestEpcsRequestHeaderPrivateLabel

        Private applicationNameField As String

        Private faqUrlField As String

        Private helpUrlField As String

        Private logoUrlField As String

        Private contactInfoField As EpcsRequestEpcsRequestHeaderPrivateLabelContactInfo

        '''<remarks/>
        Public Property ApplicationName() As String
            Get
                Return Me.applicationNameField
            End Get
            Set(value As String)
                Me.applicationNameField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(DataType:="anyURI")> _
        Public Property FaqUrl() As String
            Get
                Return Me.faqUrlField
            End Get
            Set(value As String)
                Me.faqUrlField = value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(DataType:="anyURI")> _
        Public Property HelpUrl() As String
            Get
                Return Me.helpUrlField
            End Get
            Set(value As String)
                Me.helpUrlField = value
            End Set
        End Property

        '''<remarks/>
        Public Property LogoUrl() As String
            Get
                Return Me.logoUrlField
            End Get
            Set(value As String)
                Me.logoUrlField = value
            End Set
        End Property

        '''<remarks/>
        Public Property ContactInfo() As EpcsRequestEpcsRequestHeaderPrivateLabelContactInfo
            Get
                Return Me.contactInfoField
            End Get
            Set(value As EpcsRequestEpcsRequestHeaderPrivateLabelContactInfo)
                Me.contactInfoField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
    Partial Public Class EpcsRequestEpcsRequestHeaderPrivateLabelContactInfo

        Private companyNameField As String

        Private contactNameField As String

        Private addressLine1Field As String

        Private cityField As String

        Private stateField As String

        Private countryField As String

        Private zipField As String

        Private phoneField As String

        Private faxField As String

        Private emailField As String

        Private availabilityField As String

        '''<remarks/>
        Public Property CompanyName() As String
            Get
                Return Me.companyNameField
            End Get
            Set(value As String)
                Me.companyNameField = value
            End Set
        End Property

        '''<remarks/>
        Public Property ContactName() As String
            Get
                Return Me.contactNameField
            End Get
            Set(value As String)
                Me.contactNameField = value
            End Set
        End Property

        '''<remarks/>
        Public Property AddressLine1() As String
            Get
                Return Me.addressLine1Field
            End Get
            Set(value As String)
                Me.addressLine1Field = value
            End Set
        End Property

        '''<remarks/>
        Public Property City() As String
            Get
                Return Me.cityField
            End Get
            Set(value As String)
                Me.cityField = value
            End Set
        End Property

        '''<remarks/>
        Public Property State() As String
            Get
                Return Me.stateField
            End Get
            Set(value As String)
                Me.stateField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Country() As String
            Get
                Return Me.countryField
            End Get
            Set(value As String)
                Me.countryField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Zip() As String
            Get
                Return Me.zipField
            End Get
            Set(value As String)
                Me.zipField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Phone() As String
            Get
                Return Me.phoneField
            End Get
            Set(value As String)
                Me.phoneField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Fax() As String
            Get
                Return Me.faxField
            End Get
            Set(value As String)
                Me.faxField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Email() As String
            Get
                Return Me.emailField
            End Get
            Set(value As String)
                Me.emailField = value
            End Set
        End Property

        '''<remarks/>
        Public Property Availability() As String
            Get
                Return Me.availabilityField
            End Get
            Set(value As String)
                Me.availabilityField = value
            End Set
        End Property
    End Class


End Namespace