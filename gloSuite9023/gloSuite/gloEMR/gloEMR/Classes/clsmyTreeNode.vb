Public Class myTreeNode
    Inherits TreeNode
    Implements IDisposable

    Private mykey As Long
    Private m_NodeName As String
    Private Result As Object
    Private _OrderTime As Date
    Private _IsFinished As Boolean
    Private m_IsNarcotics As Int16
    Private m_ddID As Int64
    Private _ReferralLetter As Object

    Private m_DrugName As String = ""
    Private m_Dosage As String = ""
    Private m_OnsetDate As String = ""


    Private m_DateResolved As String = ""
    Private m_DrugForm As String = ""
    Private m_HistoryType As String = ""

    '29-Aug-13 Ashish: Addition of nAgeMin, nAgeMax,
    'bIsSnomed and sGender columns for frmEducationAssociation
    Private n_AgeMinimum As Integer = 0
    Private n_AgeMaximum As Integer = 0
    Private sGender As String = ""
    Private bIsSnomedCode As Boolean = False
    Private _SmartDxKey As Long
    '30-Aug-13 Ashish: Added bIsProvider and bIsPatient for
    'judging the context

    Private bIsProviderReferenceMaterial As Boolean = False
    Private bIsPatientEducationMaterial As Boolean = False
    Private sSnomedID As String = ""

    '3-Sep-13 Ashish: Added bIsICD, bIsMedication, bIsDrugs, bIsLabs
    Private bIsICD9 As Boolean = False
    Private bIsMedication As Boolean = False
    Private bIsDrugs As Boolean = False
    Private bIsLabs As Boolean = False
    Private sTemplateName As String = String.Empty

    '24-Sep-13 Ashish: Added dLabResultValueOne, dLabResultValueTwo
    'nOperator and nIsProviderAdvancedMaterial

    Private dLabResultValueOne As Decimal = 0.0
    Private dLabResultValueTwo As Decimal = 0.0
    Private nOperator As Integer = 0
    Private nIsProviderAdvancedMaterial As Integer = 0

    '29-Mar-13 Aniket: Addition of source column on the History screen
    Private m_HistorySource As String = ""
    Private _nICDRevision As Integer = 0 ''added for ICD10 implementation
    Private m_CPT As String = ""
    Private m_ModifierKey As String = ""
    Private m_ModifierCode As String = ""
    Private m_UnitsKey As String = ""
    Private m_nHistoryID As Int64
    Private m_nRowOrder As Int64

    Private m_Route As String = ""
    Private m_Frequency As String = ""
    Private m_Duration As String = ""
    Private m_NDCCode As String = ""
    Private m_ConceptCode As String = ""
    Private m_DrugQtyQualifier As String = ""
    
    Private m_FaxReferralName As String = ""
    Private m_Referralletter As String = ""
    Private m_TemplateName As String = ""
    Private m_Template As Object = Nothing


    Private m_ICD9CPTCode As String = ""
    Private m_ICD9CPTName As String = ""

    Private m_arrRefferalDetails As ArrayList = Nothing
    Private _faxTo As String = ""
    Private _faxName As String = ""
    Private _faxCoverPage As String = ""
    Private m_ReasonConceptID As String = ""
    Private m_ReasonConceptDesc As String = ""
    Private _LoincCode As String = ""
    Private _LoincCodeDesc As String = ""
    Private _CQMCode As String = ""
    Private _CqmDesc As String = ""
    Private _nDeviceListID As Long
    Private _sProcStatus As String
    Private m_ResolvedEndDate As String = "" ''Code Added For 2015 certification
    Private _AllergyIntelorenceCode As String ''Code Added For 2015 certification
    Private _sAllergySeverity As String ''Code Added For 2015 certification
    Private _sCVXCode As String = ""
    Private _sCVXDesc As String = ""



    Sub New()
        MyBase.New("")
        mykey = 0
    End Sub

    Sub New(ByVal strname As String, ByVal key As Long)
        MyBase.New(strname)
        mykey = key
        m_NodeName = strname
    End Sub

    Sub New(ByVal strname As String, ByVal key As Long, ByVal ICDRev As Integer)
        MyBase.New(strname)
        mykey = key
        m_NodeName = strname
        _nICDRevision = ICDRev
    End Sub

    Sub New(ByVal strname As String, ByVal key As Long, ByVal dtPrescriptiondate As DateTime)
        MyBase.New(strname)
        MyBase.Tag = dtPrescriptiondate
        mykey = key
    End Sub

    Sub New(ByVal strname As String, ByVal key As Long, ByVal Drugname As String)
        MyBase.New(strname)
        MyBase.Tag = Drugname
        mykey = key
    End Sub


    Sub New(ByVal strname As String, ByVal key As Long, ByVal Drugname As String, ByVal strname1 As String)
        MyBase.New(strname)
        MyBase.Tag = Drugname
        m_NodeName = strname1
        mykey = key
    End Sub

    Sub New(ByVal strname As String, ByVal key As Long, ByVal ID As Long)
        MyBase.New(strname)
        MyBase.Tag = ID
        mykey = key
    End Sub

    Sub New(ByVal Drugname As String, ByVal key As Long, ByVal NDCCode As String, ByVal _mpid As Int32)
        MyBase.New(Drugname)
        MyBase.Tag = Drugname
        mykey = key
        m_DrugName = Drugname
        m_NDCCode = NDCCode
        mpid = _mpid
    End Sub

    Sub New(ByVal strname As String, ByVal key As Long, ByVal Drugname As String, ByVal Dosage As String, ByVal DrugForm As String, ByVal Route As String, ByVal Frequency As String, ByVal NDCCode As String, ByVal IsNarcotics As Int16, ByVal Duration As String, ByVal mpid As Int32, ByVal DrugQtyQualifier As String)
        MyBase.New(strname)
        MyBase.Tag = Drugname
        mykey = key
        m_DrugName = Drugname
        m_Dosage = Dosage
        m_DrugForm = DrugForm
        'Denormalization
        m_Route = Route
        m_Frequency = Frequency
        m_NDCCode = NDCCode
        m_IsNarcotics = IsNarcotics
        m_Duration = Duration
        mpid = _mpid
        m_DrugQtyQualifier = DrugQtyQualifier
        'Denormalization
    End Sub

    Public Property Key() As Long
        Get
            Return mykey
        End Get
        Set(ByVal Value As Long)
            mykey = Value
        End Set
    End Property
    Public Property SmartDxKey() As Long
        Get
            Return _SmartDxKey
        End Get
        Set(ByVal Value As Long)
            _SmartDxKey = Value
        End Set
    End Property
    Public Property Name() As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal Value As String)
            MyBase.Text = Name
        End Set

    End Property

    Public Property FaxTo() As String
        Get
            Return _faxTo
        End Get
        Set(ByVal Value As String)
            _faxTo = Value
        End Set
    End Property

    Public Property FaxName() As String
        Get
            Return _faxName
        End Get
        Set(ByVal Value As String)
            _faxName = Value
        End Set
    End Property

    Public Property FaxCoverPage() As String
        Get
            Return _faxCoverPage
        End Get
        Set(ByVal Value As String)
            _faxCoverPage = Value
        End Set

    End Property

    Public Property OrderTime() As Date
        Get
            Return _OrderTime
        End Get
        Set(ByVal Value As Date)
            _OrderTime = Value
        End Set
    End Property


    '' for Order Status (Finished / Not-Finished)
    Public Property IsFinished() As Boolean
        Get
            Return _IsFinished
        End Get
        Set(ByVal Value As Boolean)
            _IsFinished = Value
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

    Public Property IsNarcotics() As Int16
        Get
            Return m_IsNarcotics
        End Get
        Set(ByVal Value As Int16)
            m_IsNarcotics = Value
        End Set
    End Property

    Public Property DDID() As Int64
        Get
            Return m_ddID
        End Get
        Set(ByVal Value As Int64)
            m_ddID = Value
        End Set
    End Property
    Public Property mpid() As Integer

    Public Property FaxReferralName() As String
        Get
            Return m_FaxReferralName
        End Get
        Set(ByVal Value As String)
            m_FaxReferralName = Value
        End Set
    End Property

    Public Property FaxReferralLetter() As String
        Get
            Return m_Referralletter
        End Get
        Set(ByVal Value As String)
            m_Referralletter = Value
        End Set
    End Property

    Public Property DrugName() As String
        Get
            Return m_DrugName
        End Get
        Set(ByVal Value As String)
            m_DrugName = Value
        End Set

    End Property

    Public Property DrugForm() As String
        Get
            Return m_DrugForm
        End Get
        Set(ByVal Value As String)
            m_DrugForm = Value
        End Set

    End Property

    Public Property HistoryType() As String
        Get
            Return m_HistoryType
        End Get
        Set(ByVal Value As String)
            m_HistoryType = Value
        End Set

    End Property

    '29-Mar-13 Aniket: Addition of source column on the History screen
    Public Property HistorySource() As String
        Get
            Return m_HistorySource
        End Get
        Set(ByVal Value As String)
            m_HistorySource = Value
        End Set

    End Property
    'added for ICD10 implementation
    Public Property nICDRevision() As Integer
        Get
            Return _nICDRevision
        End Get
        Set(ByVal Value As Integer)
            _nICDRevision = Value
        End Set

    End Property


    Public Property CPT() As String
        Get
            Return m_CPT
        End Get
        Set(ByVal Value As String)
            m_CPT = Value
        End Set

    End Property
    Public Property ModifierKey() As String
        Get
            Return m_ModifierKey
        End Get
        Set(ByVal Value As String)
            m_ModifierKey = Value
        End Set

    End Property
    Public Property ModifierCode() As String
        Get
            Return m_ModifierCode
        End Get
        Set(ByVal Value As String)
            m_ModifierCode = Value
        End Set

    End Property
    Public Property UnitsKey() As String
        Get
            Return m_UnitsKey
        End Get
        Set(ByVal Value As String)
            m_UnitsKey = Value
        End Set

    End Property
    Public Property Dosage() As String
        Get
            Return m_Dosage
        End Get
        Set(ByVal Value As String)
            m_Dosage = Value
        End Set
    End Property

    Public Property nHistoryID() As Int64
        Get
            Return m_nHistoryID
        End Get
        Set(ByVal Value As Int64)
            m_nHistoryID = Value
        End Set
    End Property

    Public Property nRowOrder() As Int64
        Get
            Return m_nRowOrder
        End Get
        Set(ByVal Value As Int64)
            m_nRowOrder = Value
        End Set
    End Property

    Public Property OnsetDate() As String
        Get
            Return m_OnsetDate
        End Get
        Set(ByVal Value As String)
            m_OnsetDate = Value
        End Set
    End Property
    Public Property ResolvedEndDate() As String ''Code Added For 2015 certification
        Get
            Return m_ResolvedEndDate
        End Get
        Set(ByVal Value As String)
            m_ResolvedEndDate = Value
        End Set
    End Property

    Public Property DateResolved() As String
        Get
            Return m_DateResolved
        End Get
        Set(ByVal Value As String)
            m_DateResolved = Value
        End Set
    End Property

    Public Property NodeName() As String
        Get
            Return m_NodeName
        End Get
        Set(ByVal Value As String)
            m_NodeName = Value
        End Set
    End Property
 
    Public Property Route() As String
        Get
            Return m_Route
        End Get
        Set(ByVal value As String)
            m_Route = value
        End Set
    End Property

    Public Property Frequency() As String
        Get
            Return m_Frequency
        End Get
        Set(ByVal value As String)
            m_Frequency = value
        End Set
    End Property

    Public Property Duration() As String
        Get
            Return m_Duration
        End Get
        Set(ByVal value As String)
            m_Duration = value
        End Set
    End Property

    Public Property NDCCode() As String
        Get
            Return m_NDCCode
        End Get
        Set(ByVal value As String)
            m_NDCCode = value
        End Set
    End Property

    Public Property ConceptCode() As String
        Get
            Return m_ConceptCode
        End Get
        Set(ByVal value As String)
            m_ConceptCode = value
        End Set
    End Property

    Public Property DrugQtyQualifier() As String
        Get
            Return m_DrugQtyQualifier
        End Get
        Set(ByVal value As String)
            m_DrugQtyQualifier = value
        End Set
    End Property

    Public Property DMTemplate() As Object
        Get
            Return m_Template
        End Get
        Set(ByVal value As Object)
            m_Template = value
        End Set
    End Property

    Public Property DMTemplateName() As String
        Get
            Return m_TemplateName
        End Get
        Set(ByVal value As String)
            m_TemplateName = value
        End Set
    End Property

    Public Property arrRefferalDetails() As ArrayList
        Get
            Return m_arrRefferalDetails
        End Get
        Set(ByVal value As ArrayList)
            m_arrRefferalDetails = value
        End Set
    End Property

    Public Property MinimumAge() As Integer
        Get
            Return Me.n_AgeMinimum
        End Get
        Set(ByVal value As Integer)
            Me.n_AgeMinimum = value
        End Set
    End Property

    Public Property MaximumAge() As Integer
        Get
            Return Me.n_AgeMaximum
        End Get
        Set(ByVal value As Integer)
            Me.n_AgeMaximum = value
        End Set
    End Property

    Public Property Gender() As String
        Get
            Return Me.sGender
        End Get
        Set(ByVal value As String)
            Me.sGender = value
        End Set
    End Property

    Public Property IsSnomedCode() As Boolean
        Get
            Return Me.bIsSnomedCode
        End Get
        Set(ByVal value As Boolean)
            Me.bIsSnomedCode = value
        End Set
    End Property

    Public Property IsPatientEducationMaterial() As Boolean
        Get
            Return Me.bIsPatientEducationMaterial
        End Get
        Set(ByVal value As Boolean)
            Me.bIsPatientEducationMaterial = value
        End Set
    End Property

    Public Property IsProviderReferenceMaterial() As Boolean
        Get
            Return Me.bIsProviderReferenceMaterial
        End Get
        Set(ByVal value As Boolean)
            Me.bIsProviderReferenceMaterial = value
        End Set
    End Property

    Public Property SnomedID() As String
        Get
            Return Me.sSnomedID
        End Get
        Set(ByVal value As String)
            Me.sSnomedID = value
        End Set
    End Property

    Public Property IsICD9() As Boolean
        Get
            Return Me.bIsICD9
        End Get
        Set(ByVal value As Boolean)
            Me.bIsICD9 = value
        End Set
    End Property

    Public Property IsMedication() As Boolean
        Get
            Return Me.bIsMedication
        End Get
        Set(ByVal value As Boolean)
            Me.bIsMedication = value
        End Set
    End Property

    Public Property IsLabs() As Boolean
        Get
            Return Me.bIsLabs
        End Get
        Set(ByVal value As Boolean)
            Me.bIsLabs = value
        End Set
    End Property

    Public Property TemplateName() As String
        Get
            Return Me.sTemplateName
        End Get
        Set(ByVal value As String)
            Me.sTemplateName = value
        End Set
    End Property

    Public Property LabResultValueOne() As Decimal
        Get
            Return Me.dLabResultValueOne
        End Get
        Set(value As Decimal)
            Me.dLabResultValueOne = value
        End Set
    End Property

    Public Property LabResultValueTwo() As Decimal
        Get
            Return Me.dLabResultValueTwo
        End Get
        Set(value As Decimal)
            Me.dLabResultValueTwo = value
        End Set
    End Property

    Public Property LabResultOperator() As Integer
        Get
            Return Me.nOperator
        End Get
        Set(value As Integer)
            Me.nOperator = value
        End Set
    End Property

    Public Property IsProviderAdvanceMaterial() As Integer
        Get
            Return Me.nIsProviderAdvancedMaterial
        End Get
        Set(value As Integer)
            Me.nIsProviderAdvancedMaterial = value
        End Set
    End Property
    Public Property ReasonConceptCode() As String
        Get
            Return m_ReasonConceptID
        End Get
        Set(ByVal value As String)
            m_ReasonConceptID = value
        End Set
    End Property
    Public Property ReasonConceptDesc() As String
        Get
            Return m_ReasonConceptDesc
        End Get
        Set(ByVal value As String)
            m_ReasonConceptDesc = value
        End Set
    End Property

    Public Property LoincCode() As String
        Get
            Return _LoincCode
        End Get
        Set(ByVal value As String)
            _LoincCode = value
        End Set
    End Property
    Public Property LoincDescr() As String
        Get
            Return _LoincCodeDesc
        End Get
        Set(ByVal value As String)
            _LoincCodeDesc = value
        End Set
    End Property
    Public Property CQMCode() As String
        Get
            Return _CQMCode
        End Get
        Set(ByVal value As String)
            _CQMCode = value
        End Set
    End Property
    Public Property CQMDesc() As String
        Get
            Return _CqmDesc
        End Get
        Set(ByVal value As String)
            _CqmDesc = value
        End Set
    End Property
    
    Public Property nDeviceListID As Long
        Get
            Return _nDeviceListID
        End Get
        Set(value As Long)
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
    Public Property AllergyIntelorenceCode() As String ''Code Added For 2015 certification
        Get
            Return _AllergyIntelorenceCode
        End Get
        Set(ByVal value As String)
            _AllergyIntelorenceCode = value
        End Set
    End Property
    Public Property sAllergySeverity() As String ''Code Added For 2015 certification
        Get
            Return _sAllergySeverity
        End Get
        Set(ByVal value As String)
            _sAllergySeverity = value
        End Set
    End Property
    Public Property CVXCode As String
        Get
            Return _sCVXCode
        End Get
        Set(value As String)
            _sCVXCode = value
        End Set
    End Property
    Public Property CVXDesc As String
        Get
            Return _sCVXDesc
        End Get
        Set(value As String)
            _sCVXDesc = value
        End Set
    End Property


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).

                'Free String Variables
                'm_NodeName = Nothing
                'm_DrugName = Nothing
                'm_Dosage = Nothing
                'm_OnsetDate = Nothing
                'm_DateResolved = Nothing
                'm_DrugForm = Nothing
                'm_HistoryType = Nothing
                'm_HistorySource = Nothing
                'm_CPT = Nothing
                'm_Route = Nothing
                'm_Frequency = Nothing
                'm_Duration = Nothing
                'm_NDCCode = Nothing
                'm_ConceptCode = Nothing
                'm_DrugQtyQualifier = Nothing
                'm_FaxReferralName = Nothing
                'm_Referralletter = Nothing
                'm_TemplateName = Nothing
                'm_ICD9CPTCode = Nothing
                'm_ICD9CPTName = Nothing
                '_faxTo = Nothing
                '_faxName = Nothing
                '_faxCoverPage = Nothing

                ''Free Object Variables
                'Result = Nothing
                '_ReferralLetter = Nothing
                'm_Template = Nothing

                ''Free other variables
                'm_arrRefferalDetails = Nothing
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
