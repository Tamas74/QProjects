Public Class myList
    Private itemindex As Int64
    Private _Rowno As Int32
    Private itemDescription As String = String.Empty
    Private itemType As Boolean
    Private itemId As System.Int64
    Private itemVisitDate As Date
    Private _IsFinished As Boolean
    Private _SendTask As Boolean  ''swaraj 20100612 - to store bSendTask value in SmartConfig Table
    Private _HistoryCategory As String = String.Empty
    Private _HistoryItem As String = String.Empty
    Private _Group As String = String.Empty
    Private _MedicalConditionID As Long 'variable added by sagar to store the medical condition id in the property procedure
    Private _Reaction As String = String.Empty
    Private _ColorCode As Long
    ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 140110
    Private _ResolvedDate As Date
    Private _RComment As String = String.Empty
    ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 140110
    'Private _strItemname As String = ""

    ''Variables added in History table sDrugName/sDosage/sNDCCode/nddid
    Private _sHxDrugName As String
    Private _sHxDrugDosage As String = String.Empty
    Private _sHxNDCCode As String = String.Empty
    Private _nHxddid As Long
    Private _Status As String
    Private _DOEOAllergy As String = String.Empty


    Private _ConceptId As String = String.Empty
    Private _DescId As String = String.Empty
    Private _SnowMadeID As String = String.Empty
    Private _Description As String = String.Empty
    Private _ICD9 As String = String.Empty   'Added by kanchan on 20100526
    Private _RxNorm As String = String.Empty   'Added by kanchan on 20100828

    Private _Code As String
    Private _ParameterName As String
    ''Private _Operater As gloStream.gloCMS.Supporting.[Operator]
    Private _Value As String = String.Empty
    Private _DMSID As String = String.Empty

    'sarika 20090211 DICOM
    Private _DICOMID As String = ""
    '--

    Private Result As Object

    'added by sarika 13th nov 07 -- for 1 fax to multiple recipients
    Private _ContactID As Long = 0
    Private _ContactPersonName As String = ""
    Private _ContactPersonFaxNo As String = ""
    '-------------------------------------
    Private _DisclosureAssociationID As Int64
    Private _DisclosureType As String

    'For De-Normalization
    Private itemDrugName As String = ""
    Private itemDosage As String = ""
    Private itemDrugForm As String = ""

    Private itemRoute As String = String.Empty
    Private itemFrequency As String = String.Empty
    Private itemNDCCode As String = String.Empty
    Private itemIsNarcotic As Int16
    Private itemDuration As String = String.Empty
    Private itemnDDid As Int64
    Private itemDrugQtyQualifier As String = String.Empty
    'For De-Normalization

    'sarika referral letter 20081125
    Private _ReferralLetterName As String = ""


    '
    Private _sNPI As String = ""
    Private _sEmail As String = ""
    Private _nFlagType As Integer = 0


    Public Enum ControlType
        None = 0
        CheckBox = 1
        Text = 2
    End Enum


    Public Enum CategoryType
        None = 0
        General = 1
        Hitory = 2
        Physical_Examination = 3
        Medical_Decision_Making = 4
        HPI = 5
        Management_option = 6
        Labs = 7
        X_Ray_Radiology = 8
        Other_Diagonsis_Tests = 9
        ROS = 10
        DB_History = 11
    End Enum


    '--
    Private _ControlType As ControlType
    Private _CategoryType As CategoryType
    Private _AssociatedItem As String = String.Empty
    Private _AssociatedCategory As String = String.Empty
    Private _AssociatedProperty As String = String.Empty

    Private _MyCollection As New Collection

    Private _OrderComment As enumOrderComment
    Private _ICD9Count As Integer
    Private _CPTCount As Integer
    Private _ModCount As Integer
    'Shubhangi

    Public Enum enumExamControlType
        None = 0
        GeneralMultiSystem = 1
        Cardiovascular = 2
        EarsNoseThroat = 3
        Eye = 4
        Genitourinary = 5
        HemaLymphImmuno = 6
        Musculoskeletal = 7
        Neurological = 8
        Psychiatric = 9
        Respiratory = 10
        Skin = 11
        Pre97Guidelines = 12
    End Enum

    Private _ExamControlType As enumExamControlType

    Private _nStatus As Integer
    Private _Immediacy As Integer
    Private _Provider As String
    Private _Location As String
    Private _LastModified As DateTime
    Private _Comments As String
    Private _ExamID As String



    Public Enum enumOrderComment '' SUDHIR 20090420 '' FOR SAVING ORDER TEST WITH TEMPLATE. ''
        None = 0
        Assigned = 1 '' Template is Assigned for Test as comment
        UnAssigned = 2 '' Template is Not Assigned for Test as comment
    End Enum

    Sub New(ByVal intindex As Int64, ByVal strDescription As String)
        itemindex = intindex
        itemDescription = strDescription
    End Sub
    ''Sandip Darade 20090401
    ''to add name of item 
    Sub New(ByVal strDescription As String, ByVal strItemName As String, ByVal intindex As Int64)
        itemindex = intindex
        itemDescription = strDescription
        _ParameterName = strItemName
    End Sub

    'For De-Normalization
    Sub New(ByVal intindex As Int64, ByVal strDescription As String, ByVal strDrugName As String, ByVal strDosage As String, ByVal strDrugForm As String, ByVal strRoute As String, ByVal strFrequency As String, ByVal strNDCCode As String, ByVal nIsNarcotic As Int16, ByVal strDuration As String, ByVal nDDid As Int64, ByVal strDrugQtyQualifier As String)
        itemindex = intindex
        itemDescription = strDescription
        'For De-Normalization
        itemDrugName = strDrugName
        itemDosage = strDosage
        itemDrugForm = strDrugForm
        itemRoute = strRoute
        itemFrequency = strFrequency
        itemNDCCode = strNDCCode
        itemIsNarcotic = nIsNarcotic
        itemDuration = strDuration
        itemnDDid = nDDid
        itemDrugQtyQualifier = strDrugQtyQualifier
        'For De-Normalization
    End Sub
    'For De-Normalization

    'By mahesh for Messages

    Sub New(ByVal UserID As Int64, ByVal UserName As String, ByVal ProviderID As Int64)
        itemindex = UserID
        itemDescription = UserName
        itemId = ProviderID
    End Sub

    Sub New(ByVal intindex As Int64, ByVal strDescription As String, ByVal blnType As Boolean, ByVal intID As System.Int64)
        itemindex = intindex
        itemDescription = strDescription
        itemType = blnType
        itemId = intID
    End Sub

    'code added by sarika on 13th nov 07 --- for one fax to multiple recipients
    Sub New(ByVal ContactId As Long, ByVal strContactPersonName As String, ByVal strContactFaxNo As String)
        _ContactID = ContactId
        _ContactPersonName = strContactPersonName
        _ContactPersonFaxNo = strContactFaxNo
    End Sub
    '------------

    Sub New()

    End Sub

    'For De-Normalization

    'Code Start-Added by kanchan on 20100828 for RxNorm
    Public Property RxNormID() As String
        Get
            Return _RxNorm
        End Get
        Set(ByVal Value As String)
            _RxNorm = Value
        End Set
    End Property
    'Code End-Added by kanchan on 20100828 for RxNorm

    'Code Start-Added by kanchan on 20100526 for ICD9
    Public Property ICD9() As String
        Get
            Return _ICD9
        End Get
        Set(ByVal Value As String)
            _ICD9 = Value
        End Set
    End Property
    'Code End-Added by kanchan on 20100526 for ICD9
    Public Property DrugName() As String
        Get
            Return itemDrugName
        End Get
        Set(ByVal Value As String)
            itemDrugName = Value
        End Set
    End Property
    Public Property Dosage() As String
        Get
            Return itemDosage
        End Get
        Set(ByVal Value As String)
            itemDosage = Value
        End Set
    End Property
    Public Property DrugForm() As String
        Get
            Return itemDrugForm
        End Get
        Set(ByVal Value As String)
            itemDrugForm = Value
        End Set
    End Property

    Public Property Route() As String
        Get
            Return itemRoute
        End Get
        Set(ByVal Value As String)
            itemRoute = Value
        End Set
    End Property

    Public Property Frequency() As String
        Get
            Return itemFrequency
        End Get
        Set(ByVal Value As String)
            itemFrequency = Value
        End Set
    End Property

    Public Property NDCCode() As String
        Get
            Return itemNDCCode
        End Get
        Set(ByVal Value As String)
            itemNDCCode = Value
        End Set
    End Property

    Public Property IsNarcotic() As Int16
        Get
            Return itemIsNarcotic
        End Get
        Set(ByVal Value As Int16)
            itemIsNarcotic = Value
        End Set
    End Property

    Public Property Duration() As String
        Get
            Return itemDuration
        End Get
        Set(ByVal Value As String)
            itemDuration = Value
        End Set
    End Property

    Public Property DDid() As Int64
        Get
            Return itemnDDid
        End Get
        Set(ByVal Value As Int64)
            itemnDDid = Value
        End Set
    End Property

    Public Property DrugQtyQualifier() As String
        Get
            Return itemDrugQtyQualifier
        End Get
        Set(ByVal Value As String)
            itemDrugQtyQualifier = Value
        End Set
    End Property
    'For De-Normalization

    Public Property ParameterName() As String
        Get
            Return _ParameterName
        End Get
        Set(ByVal Value As String)
            _ParameterName = Value
        End Set
    End Property

    ''Public Property Operater() As gloStream.gloCMS.Supporting.[Operator]
    ''    Get
    ''        Return _Operater
    ''    End Get
    ''    Set(ByVal Value As gloStream.gloCMS.Supporting.[Operator])
    ''        _Operater = Value
    ''    End Set
    ''End Property

    Public Property Value() As String
        Get
            Return _Value
        End Get
        Set(ByVal Value As String)
            _Value = Value
        End Set
    End Property

    Public Property TemplateResult() As Object
        Get
            Return Result
        End Get
        Set(ByVal Value As Object)
            Result = Value
        End Set
    End Property

    Public Property Index() As Int64
        Get
            Return itemindex
        End Get
        Set(ByVal Value As Int64)
            itemindex = Value
        End Set
    End Property

    Public Property RowNo() As Int32
        Get
            Return _Rowno
        End Get
        Set(ByVal Value As Int32)
            _Rowno = Value
        End Set
    End Property


    'code added by sagar on 4 august 2007 to access the medical condition id in the history table
    Public Property MedicalConditionID() As Long
        Get
            Return _MedicalConditionID
        End Get

        Set(ByVal Value As Long)
            _MedicalConditionID = Value
        End Set
    End Property


    'for denormalization of History table
    Public Property HxDrugName() As String
        Get
            Return _sHxDrugName
        End Get
        Set(ByVal Value As String)
            _sHxDrugName = Value
        End Set
    End Property

    Public Property HxDrugDosage() As String
        Get
            Return _sHxDrugDosage
        End Get
        Set(ByVal Value As String)
            _sHxDrugDosage = Value
        End Set
    End Property

    Public Property HxNDCCode() As String
        Get
            Return _sHxNDCCode
        End Get
        Set(ByVal Value As String)
            _sHxNDCCode = Value
        End Set
    End Property

    Public Property HxDDID() As Long
        Get
            Return _nHxddid
        End Get
        Set(ByVal Value As Long)
            _nHxddid = Value
        End Set
    End Property

    Public Property DOEOAllergy() As String
        Get
            Return _DOEOAllergy
        End Get
        Set(ByVal Value As String)
            _DOEOAllergy = Value
        End Set
    End Property

    Public Property Status() As String
        Get
            Return _Status
        End Get
        Set(ByVal Value As String)
            _Status = Value
        End Set
    End Property
   

    ' added by chetan 
    Public Property ConceptId() As String
        Get
            Return _ConceptId
        End Get
        Set(ByVal Value As String)
            _ConceptId = Value
        End Set

    End Property

    Public Property DescId() As String
        Get
            Return _DescId
        End Get
        Set(ByVal Value As String)
            _DescId = Value
        End Set
    End Property

    Public Property SnowMadeID() As String
        Get
            Return _SnowMadeID
        End Get
        Set(ByVal Value As String)
            _SnowMadeID = Value
        End Set
    End Property

    Public Property SnoDescription() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property
    'for denormalization of History table

    Public Property ID() As Long
        Get
            Return itemId
        End Get
        Set(ByVal Value As Long)
            itemId = Value
        End Set
    End Property
    Public Property Type() As Boolean
        Get
            Return itemType
        End Get
        Set(ByVal Value As Boolean)
            itemType = Value
        End Set
    End Property

    Public Property Description() As String
        Get
            Return itemDescription
        End Get
        Set(ByVal Value As String)
            itemDescription = Value
        End Set
    End Property

    Public Property Code() As String
        Get
            Return _Code
        End Get
        Set(ByVal Value As String)
            _Code = Value
        End Set
    End Property

    Public Property HistoryCategory() As String
        Get
            Return _HistoryCategory
        End Get
        Set(ByVal Value As String)
            _HistoryCategory = Value
        End Set
    End Property

    Public Property HistoryItem() As String
        Get
            Return _HistoryItem
        End Get
        Set(ByVal Value As String)
            _HistoryItem = Value
        End Set
    End Property

    Public Property Group() As String
        Get
            Return _Group
        End Get
        Set(ByVal Value As String)
            _Group = Value
        End Set
    End Property

    '' Added By Mahesh 
    '' Used in Orders 
    Public Property VisitDate() As Date
        Get
            Return itemVisitDate
        End Get
        Set(ByVal Value As Date)
            itemVisitDate = Value
        End Set
    End Property

    '' Added By Mahesh 
    '' Used in Orders to SetGet Order is Finished /NotFinised 

    ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 140110
    Public Property ResolvedDate() As Date
        Get
            Return _ResolvedDate
        End Get
        Set(ByVal Value As Date)
            _ResolvedDate = Value
        End Set
    End Property

    Public Property RComment() As String
        Get
            Return _RComment
        End Get
        Set(ByVal Value As String)
            _RComment = Value
        End Set
    End Property

    ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 140110

    Public Property IsFinished() As Boolean
        Get
            Return _IsFinished
        End Get
        Set(ByVal Value As Boolean)
            _IsFinished = Value
        End Set
    End Property
    '' swaraj 20100612 - To store bSendTask value in SmartConfig table
    Public Property SendTask() As Boolean
        Get
            Return _SendTask
        End Get
        Set(ByVal Value As Boolean)
            _SendTask = Value
        End Set
    End Property

    Public Property nStatus() As Integer
        Get
            Return _nStatus
        End Get
        Set(ByVal value As Integer)
            _nStatus = value
        End Set
    End Property

    Public Property Immediacy() As Integer
        Get
            Return _Immediacy
        End Get
        Set(ByVal value As Integer)
            _Immediacy = value
        End Set
    End Property

    Public Property Provider() As String
        Get
            Return _Provider
        End Get
        Set(ByVal value As String)
            _Provider = value
        End Set
    End Property

    Public Property Location() As String
        Get
            Return _Location
        End Get
        Set(ByVal value As String)
            _Location = value
        End Set
    End Property

    Public Property LastModified() As DateTime
        Get
            Return _LastModified
        End Get
        Set(ByVal value As DateTime)
            _LastModified = value
        End Set
    End Property


    Public Property ExamID() As String
        Get
            Return _ExamID
        End Get
        Set(ByVal value As String)
            _ExamID = value
        End Set
    End Property

    Public Property Comments() As String
        Get
            Return _Comments
        End Get
        Set(ByVal value As String)
            _Comments = value
        End Set
    End Property
    ''Added by Pramod
#Region "Commented By Pramod for DMSV2"
    '' Used to store DMSID in Radiology Flexgrid
    'Public Property DMSID() As Long
    '    Get
    '        Return _DMSID
    '    End Get
    '    Set(ByVal value As Long)
    '        _DMSID = value
    '    End Set
    'End Property
#End Region
    Public Property DMSID() As String
        Get
            Return _DMSID
        End Get
        Set(ByVal value As String)
            _DMSID = value
        End Set
    End Property

    'sarika 20090211 DICOM
    Public Property DICOMID() As String
        Get
            Return _DICOMID
        End Get
        Set(ByVal value As String)
            _DICOMID = value
        End Set
    End Property
    '-----

    'added by sarika 13th nov 07 --- for one fax to multiple recipients
    Public Property ContactID() As Long
        Get
            Return _ContactID
        End Get
        Set(ByVal value As Long)
            _ContactID = value
        End Set
    End Property

    Public Property ContactPersonName() As String
        Get
            Return _ContactPersonName
        End Get
        Set(ByVal value As String)
            _ContactPersonName = value
        End Set
    End Property

    Public Property ContactPersonFaxNo() As String
        Get
            Return _ContactPersonFaxNo
        End Get
        Set(ByVal value As String)
            _ContactPersonFaxNo = value
        End Set
    End Property


    '-----------------


    Public Overrides Function Tostring() As String
        Return itemDescription
    End Function

    Public Property Reaction() As String
        Get
            Return _Reaction
        End Get
        Set(ByVal value As String)
            _Reaction = value
        End Set
    End Property

    Public Property ColorCode() As Long
        Get
            Return _ColorCode
        End Get
        Set(ByVal value As Long)
            _ColorCode = value
        End Set
    End Property
    Public Property DisclosureAssociationID() As Int64
        Get
            Return _DisclosureAssociationID
        End Get
        Set(ByVal value As Int64)
            _DisclosureAssociationID = value
        End Set
    End Property
    Public Property DisclosureType() As String
        Get
            Return _DisclosureType
        End Get
        Set(ByVal value As String)
            _DisclosureType = value
        End Set
    End Property

    'sarika Referral Letter 20081125
    Public Property ReferralLetterName() As String
        Get
            Return _ReferralLetterName
        End Get
        Set(ByVal value As String)
            _ReferralLetterName = value
        End Set
    End Property
    '---

    'Public Property ControlType() As [Enum]
    '    Get
    '        Return _ControlType
    '    End Get
    '    Set(ByVal value As [Enum])
    '        _ControlType = value
    '    End Set
    'End Property

    'Public Property CategoryType() As [Enum]
    '    Get
    '        Return _CategoryType
    '    End Get
    '    Set(ByVal value As [Enum])
    '        _CategoryType = value
    '    End Set
    'End Property
    Public Property AssociatedItem() As String
        Get
            Return _AssociatedItem
        End Get
        Set(ByVal value As String)
            _AssociatedItem = value
        End Set
    End Property
    Public Property AssociatedCategory() As String
        Get
            Return _AssociatedCategory
        End Get
        Set(ByVal value As String)
            _AssociatedCategory = value
        End Set
    End Property

    Public Property AssociatedProperty() As String
        Get
            Return _AssociatedProperty
        End Get
        Set(ByVal value As String)
            _AssociatedProperty = value
        End Set
    End Property

    Public Property MyCollection() As Collection
        Get
            Return _MyCollection
        End Get
        Set(ByVal value As Collection)
            _MyCollection = value
        End Set
    End Property

    'sarika DM Denormalization 20090331
    Private _DMTemplateName As String = ""
    'Private _DMTemplate As Object = Nothing
    Private _DMTemplate As Object = Nothing

    Public Property DMTemplateName() As String
        Get
            Return _DMTemplateName
        End Get
        Set(ByVal value As String)
            _DMTemplateName = value
        End Set
    End Property

    Public Property DMTemplate() As Object
        Get

            Return _DMTemplate
        End Get
        Set(ByVal value As Object)
            _DMTemplate = value
        End Set
    End Property

    Public Property OrderComment() As enumOrderComment
        Get
            Return _OrderComment
        End Get
        Set(ByVal value As enumOrderComment)
            _OrderComment = value
        End Set
    End Property

    Public Property ICD9Count() As Integer
        Get
            Return _ICD9Count
        End Get
        Set(ByVal value As Integer)
            _ICD9Count = value
        End Set
    End Property
    Public Property CPTCount() As Integer
        Get
            Return _CPTCount
        End Get
        Set(ByVal value As Integer)
            _CPTCount = value
        End Set
    End Property
    Public Property ModCount() As Integer
        Get
            Return _ModCount
        End Get
        Set(ByVal value As Integer)
            _ModCount = value
        End Set
    End Property
    'Shubhnagi
    Public Property ExamControlType() As [Enum]
        Get
            Return _ExamControlType
        End Get
        Set(ByVal value As [Enum])
            _ExamControlType = value
        End Set
    End Property
    'End

    Public Property FlagType() As Integer
        Get
            Return _nFlagType
        End Get
        Set(ByVal value As Integer)
            _nFlagType = value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _sEmail
        End Get
        Set(ByVal value As String)
            _sEmail = value
        End Set
    End Property

    Public Property NPI() As String
        Get
            Return _sNPI
        End Get
        Set(ByVal value As String)
            _sNPI = value
        End Set
    End Property
  



#Region "Contact Variable"
    Private _ContactName As String
    Private _ContactFirstName As String
    Private _ContactMiddleName As String
    Private _ContactLastName As String
    Private _ContactGender As String
    Private _ContactDegree As String
    Private _ContactAddressLine1 As String
    Private _ContactAddressLine2 As String
    Private _ContactCity As String
    Private _ContactState As String
    Private _ContactZip As String
    Private _ContactPhone As String
    Private _ContactFax As String
    Private _ContactMobile As String
    Private _ContactExternalCode As String
    Private _ContactTemplateName As String
    Public Property ContactName() As String
        Get
            Return _ContactName
        End Get
        Set(ByVal value As String)
            _ContactName = value
        End Set
    End Property

    Public Property ContactFirstName() As String
        Get
            Return _ContactFirstName
        End Get
        Set(ByVal value As String)
            _ContactFirstName = value
        End Set
    End Property

    Public Property ContactMiddleName() As String
        Get
            Return _ContactMiddleName
        End Get
        Set(ByVal value As String)
            _ContactMiddleName = value
        End Set
    End Property

    Public Property ContactLastName() As String
        Get
            Return _ContactLastName
        End Get
        Set(ByVal value As String)
            _ContactLastName = value
        End Set
    End Property

    Public Property ContactGender() As String
        Get
            Return _ContactGender
        End Get
        Set(ByVal value As String)
            _ContactGender = value
        End Set
    End Property

    Public Property ContactDegree() As String
        Get
            Return _ContactDegree
        End Get
        Set(ByVal value As String)
            _ContactDegree = value
        End Set
    End Property

    Public Property ContactAddressLine1() As String
        Get
            Return _ContactAddressLine1
        End Get
        Set(ByVal value As String)
            _ContactAddressLine1 = value
        End Set
    End Property

    Public Property ContactAddressLine2() As String
        Get
            Return _ContactAddressLine2
        End Get
        Set(ByVal value As String)
            _ContactAddressLine2 = value
        End Set
    End Property

    Public Property ContactCity() As String
        Get
            Return _ContactCity
        End Get
        Set(ByVal value As String)
            _ContactCity = value
        End Set
    End Property

    Public Property ContactState() As String
        Get
            Return _ContactState
        End Get
        Set(ByVal value As String)
            _ContactState = value
        End Set
    End Property

    Public Property ContactZip() As String
        Get
            Return _ContactZip
        End Get
        Set(ByVal value As String)
            _ContactZip = value
        End Set
    End Property

    Public Property ContactPhone() As String
        Get
            Return _ContactPhone
        End Get
        Set(ByVal value As String)
            _ContactPhone = value
        End Set
    End Property

    Public Property ContactFax() As String
        Get
            Return _ContactFax
        End Get
        Set(ByVal value As String)
            _ContactFax = value
        End Set
    End Property

    Public Property ContactMobile() As String
        Get
            Return _ContactMobile
        End Get
        Set(ByVal value As String)
            _ContactMobile = value
        End Set
    End Property

    Public Property ContactExternalCode() As String
        Get
            Return _ContactExternalCode
        End Get
        Set(ByVal value As String)
            _ContactExternalCode = value
        End Set
    End Property

    Public Property ContactTemplateName() As String
        Get
            Return _ContactTemplateName
        End Get
        Set(ByVal value As String)
            _ContactTemplateName = value
        End Set
    End Property
#End Region

    '---
End Class
