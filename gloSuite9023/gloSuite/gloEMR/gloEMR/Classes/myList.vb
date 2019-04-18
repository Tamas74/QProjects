Public Class myList
    '  Inherits CollectionBase
    Implements IDisposable
    ' Private oListModifier As myList
    Private _DischargeDate As Date
    Private _DMTemplateName As String = ""
    Private _DMTemplate As Object = Nothing
    Private itemindex As Int64
    Private itemDescription As String = String.Empty
    Private itemType As Boolean
    Private itemId As System.Int64
    Private itemVisitDate As Date
    Private _IsFinished As Boolean
    Private _HistoryCategory As String = String.Empty
    Private _ROSSource As String = String.Empty
    Private _ROSDateEntered As DateTime = Nothing
    Private _nROSPatientFormID As System.Int64 = 0
    Private _HistoryItem As String = String.Empty
    Private _Group As String = String.Empty
    Private _MedicalConditionID As Long 'variable added by sagar to store the medical condition id in the property procedure
    Private _Reaction As String = String.Empty
    Private _ColorCode As Long
    Private _DOEOAllergy As String = String.Empty
    Private _ConceptId As String = String.Empty
    Private _DescId As String = String.Empty
    Private _SnowMadeID As String = String.Empty
    Private _Description As String = String.Empty
    Private _nICDRevision As Int16 = 9
    Private _IsSnomedOneToOne As Boolean = False
    Private _ICD9 As String = String.Empty
    Private _RxNorm As String = String.Empty
    Private _IsModified As Boolean = False
    Private _RComment As String = String.Empty
    Private _ReferralLetter As Object
    Private _CPT As String = String.Empty
    Private _sHistoryType As String = String.Empty
    Private _OnsetDate As String = String.Empty
    Private _nRowOrder As Int64 = 0
    Private _sHxDrugName As String = String.Empty
    Private _sHxDrugDosage As String = String.Empty
    Private _sHxNDCCode As String = String.Empty

    Private _Code As String = String.Empty
    Private _ParameterName As String = String.Empty
    Private _Value As String = String.Empty
    Private _DMSID As String = String.Empty
    Private _DICOMID As String = ""
    Private Result As Object


    Private _ContactID As Long = 0
    Private _ContactPersonName As String = ""
    Private _ContactPersonFaxNo As String = ""

    Private _DisclosureAssociationID As Int64
    Private _DisclosureType As String = String.Empty


    Private itemDrugName As String = ""
    Private itemDosage As String = ""
    Private itemDrugForm As String = ""

    Private itemRoute As String = String.Empty
    Private itemFrequency As String = String.Empty
    Private itemNDCCode As String = String.Empty
    Private itemIsNarcotic As Int16
    Private itemDuration As String = String.Empty

    Private itemDrugQtyQualifier As String = String.Empty
    Private _ItemChecked As Boolean = False
    Private _sUserID As String = String.Empty


    Private _ReferralLetterName As String = ""



    Private _ControlType As ControlType
    Private _CategoryType As CategoryType
    Private _AssociatedItem As String = String.Empty
    Private _AssociatedCategory As String = String.Empty
    Private _AssociatedProperty As String = String.Empty

    Private _MyCollection As New Collection
    Private bAssigned As Boolean = True

    Private _OrderComment As enumOrderComment
    Private _ICD9Count As Integer
    Private _CPTCount As Integer
    Private _ModCount As Integer
    Private _mpid As Int32 = 0
    Private _ExamControlType As enumExamControlType

    Private _ResolvedDate As Date
    Private _Status As String = String.Empty
    Private _Comments As String = String.Empty '' chetan assign it to empty on 28-oct-2010 
    Private _Immediacy As Integer
    Private _Provider As String = String.Empty
    Private _Location As String = String.Empty
    Private _LastModified As DateTime
    Private _ExamID As String = String.Empty
    Private _TextComment As String = String.Empty
    Private _LOINC_Code As String = String.Empty
    Private _loincDesc As String = String.Empty
    Private _CqmCode As String = String.Empty
    Private _CqmDesc As String = String.Empty
    Private _nDeviceListID As Long
    Private _sProcStatus As String = String.Empty
    Private _sAllergySeverity As String = String.Empty ''Bug #110255:pulling history to back date Issue Resolved 
    Private _sAllergyIntelorenceCode As String = String.Empty ''
    Private _OrderID As Long = 0
    Private _sConcernStatus As String = String.Empty
    Private _sProblemType As String = String.Empty
    Private _SendTask As Boolean
    Property _formID As Long

    Private _isEncounterDiagnosis As Boolean
    Public Property IsEncounterDiagnosis() As Boolean
        Get
            Return _isEncounterDiagnosis
        End Get
        Set(ByVal value As Boolean)
            _isEncounterDiagnosis = value
        End Set
    End Property

    Public Property sConcernStatus() As String
        Get
            Return _sConcernStatus
        End Get
        Set(ByVal value As String)
            _sConcernStatus = value
        End Set
    End Property
    Public Property sCDAProblemType() As String ''(2015 Certification)
        Get
            Return _sProblemType
        End Get
        Set(ByVal value As String)
            _sProblemType = value
        End Set
    End Property

    Public Enum enumOrderComment '' SUDHIR 20090420 '' FOR SAVING ORDER TEST WITH TEMPLATE. ''
        None = 0
        Assigned = 1 '' Template is Assigned for Test as comment
        UnAssigned = 2 '' Template is Not Assigned for Test as comment
    End Enum

    Sub New(ByVal intindex As Int64, ByVal strDescription As String)
        itemindex = intindex
        itemDescription = strDescription
    End Sub

    Sub New(ByVal strDescription As String, ByVal strItemName As String, ByVal intindex As Int64, Optional ByVal blnchecked As Boolean = False)
        itemindex = intindex
        itemDescription = strDescription
        _ParameterName = strItemName
        '''''''''''''' For Smart Diagnosis/Treatment/Order Changes by Ujwala as on 20101011
        ItemChecked = blnchecked
        '''''''''''''' For Smart Diagnosis/Treatment/Order Changes by Ujwala as on 20101011
    End Sub
    Sub New(ByVal intindex As Int64, ByVal strDescription As String, ByVal strDrugName As String, ByVal strNDCCode As String, ByVal _mpid As Int32, Optional ByVal blnchecked As Boolean = False)
        itemindex = intindex
        itemDescription = strDescription
        'For De-Normalization
        itemNDCCode = strNDCCode
        itemDrugName = strDrugName
        mpid = _mpid
        ItemChecked = blnchecked
    End Sub

    Sub New(ByVal UserID As Int64, ByVal UserName As String, ByVal ProviderID As Int64)
        itemindex = UserID
        itemDescription = UserName
        itemId = ProviderID
    End Sub

    Sub New(ByVal intindex As Int64, ByVal strDescription As String, ByVal blnType As Boolean, ByVal intID As System.Int64, Optional ByVal StrComment As String = "", Optional ByVal IcdRevision As Int16 = 0)
        itemindex = intindex
        itemDescription = strDescription
        itemType = blnType
        itemId = intID
        '' chetan added on 13 nov 2010
        Comments = StrComment
        nICDRevision = IcdRevision
    End Sub

    Sub New(ByVal ContactId As Long, ByVal strContactPersonName As String, ByVal strContactFaxNo As String)
        _ContactID = ContactId
        _ContactPersonName = strContactPersonName
        _ContactPersonFaxNo = strContactFaxNo
    End Sub

    Sub New()

    End Sub

    Public Property DrugName() As String
        Get
            Return itemDrugName
        End Get
        Set(ByVal Value As String)
            itemDrugName = Value
        End Set
    End Property

    Public Property mpid() As Int32
        Get
            Return _mpid
        End Get
        Set(ByVal Value As Int32)
            _mpid = Value
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

    Public Property DrugQtyQualifier() As String
        Get
            Return itemDrugQtyQualifier
        End Get
        Set(ByVal Value As String)
            itemDrugQtyQualifier = Value
        End Set
    End Property

    Public Property ParameterName() As String
        Get
            Return _ParameterName
        End Get
        Set(ByVal Value As String)
            _ParameterName = Value
        End Set
    End Property

    Public Property UserID() As String
        Get
            Return _sUserID
        End Get
        Set(ByVal value As String)
            _sUserID = value
        End Set
    End Property

    Public Property ItemChecked() As Boolean
        Get
            Return _ItemChecked
        End Get
        Set(ByVal value As Boolean)
            _ItemChecked = value
        End Set
    End Property

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

    Public Property ReferralLetter() As Object
        Get
            Return _ReferralLetter
        End Get
        Set(ByVal Value As Object)
            _ReferralLetter = Value
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

    Public Property MedicalConditionID() As Long
        Get
            Return _MedicalConditionID
        End Get

        Set(ByVal Value As Long)
            _MedicalConditionID = Value
        End Set
    End Property

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

    Public Property DOEOAllergy() As String
        Get
            Return _DOEOAllergy
        End Get
        Set(ByVal Value As String)
            _DOEOAllergy = Value
        End Set
    End Property

    Public Property SendTask() As Boolean
        Get
            Return _SendTask
        End Get
        Set(ByVal Value As Boolean)
            _SendTask = Value
        End Set
    End Property

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
    Public Property nICDRevision() As Int16
        Get
            Return _nICDRevision
        End Get
        Set(ByVal value As Int16)
            _nICDRevision = value
        End Set
    End Property
    Public Property IsSnomedOneToOne() As Boolean
        Get
            Return _IsSnomedOneToOne
        End Get
        Set(ByVal value As Boolean)
            _IsSnomedOneToOne = value
        End Set
    End Property
    Public Property RxNormID() As String
        Get
            Return _RxNorm
        End Get
        Set(ByVal Value As String)
            _RxNorm = Value
        End Set
    End Property

    Public Property IsModified() As String
        Get
            Return _IsModified
        End Get
        Set(ByVal Value As String)
            _IsModified = Value
        End Set
    End Property

    Public Property ICD9() As String
        Get
            Return _ICD9
        End Get
        Set(ByVal Value As String)
            _ICD9 = Value
        End Set
    End Property

    Public Property CPT() As String
        Get
            Return _CPT
        End Get
        Set(ByVal Value As String)
            _CPT = Value
        End Set
    End Property

    Public Property nRowOrder() As Int64
        Get
            Return _nRowOrder
        End Get
        Set(ByVal Value As Int64)
            _nRowOrder = Value
        End Set
    End Property

    Public Property sHistoryType() As String
        Get
            Return _sHistoryType
        End Get
        Set(ByVal Value As String)
            _sHistoryType = Value
        End Set
    End Property

    Public Property OnsetDate() As String
        Get
            Return _OnsetDate
        End Get
        Set(ByVal Value As String)
            _OnsetDate = Value
        End Set
    End Property

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
    Public Property ROSSource() As String
        Get
            Return _ROSSource
        End Get
        Set(ByVal Value As String)
            _ROSSource = Value
        End Set
    End Property

    Public Property ROSDateEntered() As DateTime
        Get
            Return _ROSDateEntered
        End Get
        Set(ByVal Value As DateTime)
            _ROSDateEntered = Value
        End Set
    End Property

    Public Property ROSPatientFormID() As Int64
        Get
            Return _nROSPatientFormID
        End Get
        Set(ByVal Value As Int64)
            _nROSPatientFormID = Value
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

    Public Property VisitDate() As Date
        Get
            Return itemVisitDate
        End Get
        Set(ByVal Value As Date)
            itemVisitDate = Value
        End Set
    End Property

    Public Property DischargeDate() As Date
        Get
            Return _DischargeDate
        End Get
        Set(ByVal value As Date)
            _DischargeDate = value
        End Set
    End Property

    Public Property IsFinished() As Boolean
        Get
            Return _IsFinished
        End Get
        Set(ByVal Value As Boolean)
            _IsFinished = Value
        End Set
    End Property

    Public Property DMSID() As String
        Get
            Return _DMSID
        End Get
        Set(ByVal value As String)
            _DMSID = value
        End Set
    End Property

    Public Property DICOMID() As String
        Get
            Return _DICOMID
        End Get
        Set(ByVal value As String)
            _DICOMID = value
        End Set
    End Property

    Public Property ContactID() As Long
        Get
            Return _ContactID
        End Get
        Set(ByVal value As Long)
            _ContactID = value
        End Set
    End Property

    Public Property OrderID() As Long
        Get
            Return _OrderID
        End Get
        Set(ByVal value As Long)
            _OrderID = value
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

    Public Property FormID() As Long
        Get
            Return _formID
        End Get
        Set(ByVal value As Long)
            _formID = value
        End Set
    End Property

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

    Public Property ReferralLetterName() As String
        Get
            Return _ReferralLetterName
        End Get
        Set(ByVal value As String)
            _ReferralLetterName = value
        End Set
    End Property

    Public Property ControlType() As [Enum]
        Get
            Return _ControlType
        End Get
        Set(ByVal value As [Enum])
            _ControlType = value
        End Set
    End Property

    Public Property CategoryType() As [Enum]
        Get
            Return _CategoryType
        End Get
        Set(ByVal value As [Enum])
            _CategoryType = value
        End Set
    End Property

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
            If (bAssigned) Then
                _MyCollection.Clear()
                bAssigned = False
            End If
            _MyCollection = value
        End Set
    End Property

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

    Public Property ExamControlType() As [Enum]
        Get
            Return _ExamControlType
        End Get
        Set(ByVal value As [Enum])
            _ExamControlType = value
        End Set
    End Property

    Public Property ResolvedDate() As Date
        Get
            Return _ResolvedDate
        End Get
        Set(ByVal Value As Date)
            _ResolvedDate = Value
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

    Public Property Comments() As String
        Get
            Return _Comments
        End Get
        Set(ByVal value As String)
            _Comments = value
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

    Public Property RComment() As String
        Get
            Return _RComment
        End Get
        Set(ByVal value As String)
            _RComment = value
        End Set
    End Property

    Public Property LoincCode() As String
        Get
            Return _LOINC_Code
        End Get
        Set(ByVal value As String)
            _LOINC_Code = value
        End Set
    End Property
    Public Property LoincDesc() As String
        Get
            Return _loincDesc
        End Get
        Set(ByVal value As String)
            _loincDesc = value
        End Set
    End Property

    Public Property TextComment() As String
        Get
            Return _TextComment
        End Get
        Set(ByVal value As String)
            _TextComment = value
        End Set
    End Property

    Private _Units As Double = 1
    Public Property Units() As Double
        Get
            Return _Units
        End Get
        Set(ByVal value As Double)
            _Units = value
        End Set
    End Property

    Private _TimedTherapy As String = String.Empty
    Public Property TimedTherapy() As String
        Get
            Return _TimedTherapy
        End Get
        Set(ByVal value As String)
            _TimedTherapy = value
        End Set
    End Property

    Private _UnTimedTherapy As String = String.Empty
    Public Property UnTimedTherapy() As String
        Get
            Return _UnTimedTherapy
        End Get
        Set(ByVal value As String)
            _UnTimedTherapy = value
        End Set
    End Property

    Private _ModCode As String
    Public Property ModCode() As String
        Get
            Return _ModID
        End Get
        Set(ByVal value As String)
            _ModCode = value
        End Set
    End Property

    Private _ModDesc As String
    Public Property ModDesc() As String
        Get
            Return _ModDesc
        End Get
        Set(ByVal value As String)
            _ModDesc = value
        End Set
    End Property
    Private _ModID As Long
    Public Property ModID() As Long
        Get
            Return _ModID
        End Get
        Set(ByVal value As Long)
            _ModID = value
        End Set
    End Property
    Private _ReasonConceptID As String = String.Empty
    Public Property ReasonConceptID() As String
        Get
            Return _ReasonConceptID
        End Get
        Set(ByVal Value As String)
            _ReasonConceptID = Value
        End Set
    End Property
    Private _ReasonConceptDesc As String = String.Empty
    Public Property ReasonConceptDesc() As String
        Get
            Return _ReasonConceptDesc
        End Get
        Set(ByVal value As String)
            _ReasonConceptDesc = value
        End Set
    End Property


    Public Property CqmCode() As String
        Get
            Return _CqmCode
        End Get
        Set(ByVal value As String)
            _CqmCode = value
        End Set
    End Property
    Public Property CqmDesc() As String
        Get
            Return _CqmDesc
        End Get
        Set(ByVal value As String)
            _CqmDesc = value
        End Set
    End Property
    
    Public Property nDeviceListID() As Long
        Get
            Return _nDeviceListID
        End Get
        Set(ByVal value As Long)
            _nDeviceListID = value
        End Set
    End Property
    Public Property sProcStatus() As String
        Get
            Return _sProcStatus
        End Get
        Set(ByVal value As String)
            _sProcStatus = value
        End Set
    End Property
    Public Property sAllergyIntelorenceCode() As String
        Get
            Return _sAllergyIntelorenceCode
        End Get
        Set(ByVal value As String)
            _sAllergyIntelorenceCode = value
        End Set
    End Property
    Public Property sAllergySeverity() As String
        Get
            Return _sAllergySeverity
        End Get
        Set(ByVal value As String)
            _sAllergySeverity = value
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
    Private _ContactExternalCode As String = ""
    Private _ContactTemplateName As String
    Private _ContactFlag As Int16
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
    Public Property ContactFlag() As Int16
        Get
            Return _ContactFlag
        End Get
        Set(ByVal value As Int16)
            _ContactFlag = value
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


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If (bAssigned) Then
                    _MyCollection.Clear()
                    bAssigned = False
                End If
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class


