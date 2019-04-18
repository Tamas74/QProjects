﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.17929
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System.Xml.Serialization
Namespace SS_Resp_WSEPCSDrugCheckServiceMultiple
    '
    'This source code was auto-generated by xsd, Version=4.0.30319.17929.
    '

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0"), _
     System.Xml.Serialization.XmlRootAttribute([Namespace]:="urn:drfirst.com:epcsapi:v1_0", IsNullable:=False)> _
    Partial Public Class EpcsResponse

        Private epcsResponseHeaderField As EpcsResponseEpcsResponseHeader

        Private epcsRequestField As EpcsResponseEpcsRequest

        Private epcsResponseBodyField As EpcsResponseEpcsResponseBody

        '''<remarks/>
        Public Property EpcsResponseHeader() As EpcsResponseEpcsResponseHeader
            Get
                Return Me.epcsResponseHeaderField
            End Get
            Set(value As EpcsResponseEpcsResponseHeader)
                Me.epcsResponseHeaderField = Value
            End Set
        End Property

        '''<remarks/>
        Public Property EpcsRequest() As EpcsResponseEpcsRequest
            Get
                Return Me.epcsRequestField
            End Get
            Set(value As EpcsResponseEpcsRequest)
                Me.epcsRequestField = Value
            End Set
        End Property

        '''<remarks/>
        Public Property EpcsResponseBody() As EpcsResponseEpcsResponseBody
            Get
                Return Me.epcsResponseBodyField
            End Get
            Set(value As EpcsResponseEpcsResponseBody)
                Me.epcsResponseBodyField = Value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsResponseEpcsResponseHeader

        Private serverNameField As String

        Private epcsApiVersionField As String

        Private serverVersionField As EpcsResponseEpcsResponseHeaderServerVersion

        Private dateField As String

        '''<remarks/>
        Public Property ServerName() As String
            Get
                Return Me.serverNameField
            End Get
            Set(value As String)
                Me.serverNameField = Value
            End Set
        End Property

        '''<remarks/>
        Public Property EpcsApiVersion() As String
            Get
                Return Me.epcsApiVersionField
            End Get
            Set(value As String)
                Me.epcsApiVersionField = value
            End Set
        End Property

        '''<remarks/>
        Public Property ServerVersion() As EpcsResponseEpcsResponseHeaderServerVersion
            Get
                Return Me.serverVersionField
            End Get
            Set(value As EpcsResponseEpcsResponseHeaderServerVersion)
                Me.serverVersionField = Value
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
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsResponseEpcsResponseHeaderServerVersion

        Private appNameField As String

        Private applicationVersionField As String

        '''<remarks/>
        Public Property AppName() As String
            Get
                Return Me.appNameField
            End Get
            Set(value As String)
                Me.appNameField = Value
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
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsResponseEpcsRequest

        Private epcsRequestHeaderField As EpcsResponseEpcsRequestEpcsRequestHeader

        Private epcsRequestBodyField As EpcsResponseEpcsRequestEpcsRequestBody

        '''<remarks/>
        Public Property EpcsRequestHeader() As EpcsResponseEpcsRequestEpcsRequestHeader
            Get
                Return Me.epcsRequestHeaderField
            End Get
            Set(value As EpcsResponseEpcsRequestEpcsRequestHeader)
                Me.epcsRequestHeaderField = Value
            End Set
        End Property

        '''<remarks/>
        Public Property EpcsRequestBody() As EpcsResponseEpcsRequestEpcsRequestBody
            Get
                Return Me.epcsRequestBodyField
            End Get
            Set(value As EpcsResponseEpcsRequestEpcsRequestBody)
                Me.epcsRequestBodyField = Value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsResponseEpcsRequestEpcsRequestHeader

        Private vendorNameField As String

        Private vendorLabelField As String

        Private vendorNodeNameField As String

        Private vendorNodeLabelField As String

        Private appVersionField As EpcsResponseEpcsRequestEpcsRequestHeaderAppVersion

        Private sourceOrganizationIdField As String

        Private dateField As Date

        Private epcsApiVersionField As String

        '''<remarks/>
        Public Property VendorName() As String
            Get
                Return Me.vendorNameField
            End Get
            Set(value As String)
                Me.vendorNameField = Value
            End Set
        End Property

        '''<remarks/>
        Public Property VendorLabel() As String
            Get
                Return Me.vendorLabelField
            End Get
            Set(value As String)
                Me.vendorLabelField = Value
            End Set
        End Property

        '''<remarks/>
        Public Property VendorNodeName() As String
            Get
                Return Me.vendorNodeNameField
            End Get
            Set(value As String)
                Me.vendorNodeNameField = Value
            End Set
        End Property

        '''<remarks/>
        Public Property VendorNodeLabel() As String
            Get
                Return Me.vendorNodeLabelField
            End Get
            Set(value As String)
                Me.vendorNodeLabelField = Value
            End Set
        End Property

        '''<remarks/>
        Public Property AppVersion() As EpcsResponseEpcsRequestEpcsRequestHeaderAppVersion
            Get
                Return Me.appVersionField
            End Get
            Set(value As EpcsResponseEpcsRequestEpcsRequestHeaderAppVersion)
                Me.appVersionField = Value
            End Set
        End Property

        '''<remarks/>
        Public Property SourceOrganizationId() As String
            Get
                Return Me.sourceOrganizationIdField
            End Get
            Set(value As String)
                Me.sourceOrganizationIdField = Value
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
        Public Property EpcsApiVersion() As String
            Get
                Return Me.epcsApiVersionField
            End Get
            Set(value As String)
                Me.epcsApiVersionField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsResponseEpcsRequestEpcsRequestHeaderAppVersion

        Private appNameField As String

        Private applicationVersionField As String

        '''<remarks/>
        Public Property AppName() As String
            Get
                Return Me.appNameField
            End Get
            Set(value As String)
                Me.appNameField = Value
            End Set
        End Property

        '''<remarks/>
        Public Property ApplicationVersion() As String
            Get
                Return Me.applicationVersionField
            End Get
            Set(value As String)
                Me.applicationVersionField = Value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsResponseEpcsRequestEpcsRequestBody

        Private wsGetEPCSDrugCheckRequestField As EpcsResponseEpcsRequestEpcsRequestBodyWsGetEPCSDrugCheckRequest

        '''<remarks/>
        Public Property WsGetEPCSDrugCheckRequest() As EpcsResponseEpcsRequestEpcsRequestBodyWsGetEPCSDrugCheckRequest
            Get
                Return Me.wsGetEPCSDrugCheckRequestField
            End Get
            Set(value As EpcsResponseEpcsRequestEpcsRequestBodyWsGetEPCSDrugCheckRequest)
                Me.wsGetEPCSDrugCheckRequestField = Value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsResponseEpcsRequestEpcsRequestBodyWsGetEPCSDrugCheckRequest

        Private ePCSDrugCheckRequestListTypeField() As EpcsResponseEpcsRequestEpcsRequestBodyWsGetEPCSDrugCheckRequestEPCSDrugPermissionStatusType

        '''<remarks/>
        <System.Xml.Serialization.XmlArrayItemAttribute("EPCSDrugPermissionStatusType", IsNullable:=False)> _
        Public Property EPCSDrugCheckRequestListType() As EpcsResponseEpcsRequestEpcsRequestBodyWsGetEPCSDrugCheckRequestEPCSDrugPermissionStatusType()
            Get
                Return Me.ePCSDrugCheckRequestListTypeField
            End Get
            Set(value As EpcsResponseEpcsRequestEpcsRequestBodyWsGetEPCSDrugCheckRequestEPCSDrugPermissionStatusType())
                Me.ePCSDrugCheckRequestListTypeField = Value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsResponseEpcsRequestEpcsRequestBodyWsGetEPCSDrugCheckRequestEPCSDrugPermissionStatusType

        Private ncpdpIdField As String

        Private drugNameField As String

        Private ndcIdField As String

        Private prescriberStateCodeField As String

        Private errorListField As String

        '''<remarks/>
        Public Property NcpdpId() As String
            Get
                Return Me.ncpdpIdField
            End Get
            Set(value As String)
                Me.ncpdpIdField = value
            End Set
        End Property

        '''<remarks/>
        Public Property DrugName() As String
            Get
                Return Me.drugNameField
            End Get
            Set(value As String)
                Me.drugNameField = Value
            End Set
        End Property

        '''<remarks/>
        Public Property NdcId() As String
            Get
                Return Me.ndcIdField
            End Get
            Set(value As String)
                Me.ndcIdField = value
            End Set
        End Property

        '''<remarks/>
        Public Property PrescriberStateCode() As String
            Get
                Return Me.prescriberStateCodeField
            End Get
            Set(value As String)
                Me.prescriberStateCodeField = Value
            End Set
        End Property

        '''<remarks/>
        Public Property ErrorList() As String
            Get
                Return Me.errorListField
            End Get
            Set(value As String)
                Me.errorListField = Value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsResponseEpcsResponseBody

        Private wsGetEPCSDrugCheckResponseField As EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponse

        '''<remarks/>
        Public Property WsGetEPCSDrugCheckResponse() As EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponse
            Get
                Return Me.wsGetEPCSDrugCheckResponseField
            End Get
            Set(value As EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponse)
                Me.wsGetEPCSDrugCheckResponseField = Value
            End Set
        End Property
        Private errorResponseField As EpcsResponseEpcsResponseBodyErrorResponse

        '''<remarks/>
        Public Property ErrorResponse() As EpcsResponseEpcsResponseBodyErrorResponse
            Get
                Return Me.errorResponseField
            End Get
            Set(value As EpcsResponseEpcsResponseBodyErrorResponse)
                Me.errorResponseField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponse

        Private ePCSDrugCheckResponseListTypeField() As EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponseEPCSDrugPermissionStatusType

        '''<remarks/>
        <System.Xml.Serialization.XmlArrayItemAttribute("EPCSDrugPermissionStatusType", IsNullable:=False)> _
        Public Property EPCSDrugCheckResponseListType() As EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponseEPCSDrugPermissionStatusType()
            Get
                Return Me.ePCSDrugCheckResponseListTypeField
            End Get
            Set(value As EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponseEPCSDrugPermissionStatusType())
                Me.ePCSDrugCheckResponseListTypeField = Value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponseEPCSDrugPermissionStatusType

        Private ncpdpIdField As String

        Private drugNameField As String

        Private ndcIdField As String

        Private prescriberStateCodeField As String

        Private statusField As String

        Private scheduleField As String

        Private dEAScheduleField As String

        ''For Nadean Check
        Private PharmacyNotePrefixField As String = ""

        Private PharmacyNoteMinLengthField As String = ""

        Private PharmacyNoteDescriptionField As String = ""


        Private errorListField As EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponseEPCSDrugPermissionStatusTypeErrorList

        '''<remarks/>
        Public Property NcpdpId() As String
            Get
                Return Me.ncpdpIdField
            End Get
            Set(value As String)
                Me.ncpdpIdField = value
            End Set
        End Property

        '''<remarks/>
        Public Property DrugName() As String
            Get
                Return Me.drugNameField
            End Get
            Set(value As String)
                Me.drugNameField = Value
            End Set
        End Property

        '''<remarks/>
        Public Property NdcId() As String
            Get
                Return Me.ndcIdField
            End Get
            Set(value As String)
                Me.ndcIdField = value
            End Set
        End Property

        '''<remarks/>
        Public Property PrescriberStateCode() As String
            Get
                Return Me.prescriberStateCodeField
            End Get
            Set(value As String)
                Me.prescriberStateCodeField = Value
            End Set
        End Property

        '''<remarks/>
        Public Property Status() As String
            Get
                Return Me.statusField
            End Get
            Set(value As String)
                Me.statusField = Value
            End Set
        End Property

        '''<remarks/>
        Public Property Schedule() As String
            Get
                Return Me.scheduleField
            End Get
            Set(value As String)
                Me.scheduleField = Value
            End Set
        End Property

        '''<remarks/>
        Public Property DEASchedule() As String
            Get
                Return Me.dEAScheduleField
            End Get
            Set(value As String)
                Me.dEAScheduleField = Value
            End Set
        End Property
        Public Property PharmacyNotePrefix() As String
            Get
                Return Me.PharmacyNotePrefixField
            End Get
            Set(value As String)
                Me.PharmacyNotePrefixField = value
            End Set
        End Property
        '''<remarks/>
        Public Property PharmacyNoteMinLength() As String
            Get
                Return Me.PharmacyNoteMinLengthField
            End Get
            Set(value As String)
                Me.PharmacyNoteMinLengthField = value
            End Set
        End Property
        '''<remarks/>
        Public Property PharmacyNoteDescription() As String
            Get
                Return Me.PharmacyNoteDescriptionField
            End Get
            Set(value As String)
                Me.PharmacyNoteDescriptionField = value
            End Set
        End Property

        '''<remarks/>
        Public Property ErrorList() As EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponseEPCSDrugPermissionStatusTypeErrorList
            Get
                Return Me.errorListField
            End Get
            Set(value As EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponseEPCSDrugPermissionStatusTypeErrorList)
                Me.errorListField = Value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponseEPCSDrugPermissionStatusTypeErrorList

        Private errorField As EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponseEPCSDrugPermissionStatusTypeErrorListError

        Private textField() As String

        '''<remarks/>
        Public Property [Error]() As EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponseEPCSDrugPermissionStatusTypeErrorListError
            Get
                Return Me.errorField
            End Get
            Set(value As EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponseEPCSDrugPermissionStatusTypeErrorListError)
                Me.errorField = Value
            End Set
        End Property

        '''<remarks/>
        <System.Xml.Serialization.XmlTextAttribute()> _
        Public Property Text() As String()
            Get
                Return Me.textField
            End Get
            Set(value As String())
                Me.textField = Value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsResponseEpcsResponseBodyWsGetEPCSDrugCheckResponseEPCSDrugPermissionStatusTypeErrorListError

        Private errorCodeField As String

        Private errorTextField As String

        '''<remarks/>
        Public Property ErrorCode() As String
            Get
                Return Me.errorCodeField
            End Get
            Set(value As String)
                Me.errorCodeField = value
            End Set
        End Property

        '''<remarks/>
        Public Property ErrorText() As String
            Get
                Return Me.errorTextField
            End Get
            Set(value As String)
                Me.errorTextField = Value
            End Set
        End Property
    End Class
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsResponseEpcsResponseBodyErrorResponse

        Private errorResponseListField As EpcsResponseEpcsResponseBodyErrorResponseErrorResponseList

        '''<remarks/>
        Public Property ErrorResponseList() As EpcsResponseEpcsResponseBodyErrorResponseErrorResponseList
            Get
                Return Me.errorResponseListField
            End Get
            Set(value As EpcsResponseEpcsResponseBodyErrorResponseErrorResponseList)
                Me.errorResponseListField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsResponseEpcsResponseBodyErrorResponseErrorResponseList

        Private errorField As EpcsResponseEpcsResponseBodyErrorResponseErrorResponseListError

        '''<remarks/>
        Public Property [Error]() As EpcsResponseEpcsResponseBodyErrorResponseErrorResponseListError
            Get
                Return Me.errorField
            End Get
            Set(value As EpcsResponseEpcsResponseBodyErrorResponseErrorResponseListError)
                Me.errorField = value
            End Set
        End Property
    End Class

    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1"), _
     System.SerializableAttribute(), _
     System.Diagnostics.DebuggerStepThroughAttribute(), _
     System.ComponentModel.DesignerCategoryAttribute("code"), _
     System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True, [Namespace]:="urn:drfirst.com:epcsapi:v1_0")> _
    Partial Public Class EpcsResponseEpcsResponseBodyErrorResponseErrorResponseListError

        Private errorCodeField As String

        Private errorTextField As String

        '''<remarks/>
        Public Property ErrorCode() As String
            Get
                Return Me.errorCodeField
            End Get
            Set(value As String)
                Me.errorCodeField = value
            End Set
        End Property

        '''<remarks/>
        Public Property ErrorText() As String
            Get
                Return Me.errorTextField
            End Get
            Set(value As String)
                Me.errorTextField = value
            End Set
        End Property
    End Class
End Namespace
