Public Class myTreeNode
    'Inherits TreeNode
    'Private mykey As Long
    'Private m_NodeName As String
    'Private Result As Object
    'Private _OrderTime As Date
    'Private _IsFinished As Boolean
    'Private m_IsNarcotics As Int16
    'Private m_ddID As Int64

    'Sub New()
    '    MyBase.New("")
    '    mykey = 0
    'End Sub
    ''Sub New(ByVal strname As String, ByVal key As Int64)
    ''    MyBase.New(strname)
    ''    mykey = key
    ''End Sub
    'Sub New(ByVal strname As String, ByVal key As Long)
    '    MyBase.New(strname)
    '    mykey = key
    '    m_NodeName = strname
    'End Sub
    'Sub New(ByVal strname As String, ByVal key As Long, ByVal dtPrescriptiondate As DateTime)
    '    MyBase.New(strname)
    '    MyBase.Tag = dtPrescriptiondate
    '    mykey = key
    'End Sub
    'Sub New(ByVal strname As String, ByVal key As Long, ByVal Drugname As String)
    '    MyBase.New(strname)
    '    MyBase.Tag = Drugname
    '    mykey = key
    'End Sub
    'Sub New(ByVal strname As String, ByVal key As Long, ByVal Drugname As String, ByVal DDID As Int64)
    '    MyBase.New(strname)
    '    MyBase.Tag = Drugname
    '    mykey = key
    '    m_ddID = DDID
    'End Sub
    'Sub New(ByVal strname As String, ByVal key As Long, ByVal Drugname As String, ByVal strname1 As String)
    '    MyBase.New(strname)
    '    MyBase.Tag = Drugname
    '    m_NodeName = strname1
    '    mykey = key
    'End Sub
    'Sub New(ByVal strname As String, ByVal key As Long, ByVal ID As Long)
    '    MyBase.New(strname)
    '    MyBase.Tag = ID
    '    mykey = key
    'End Sub
    'Public Property Key() As Long
    '    Get
    '        Return mykey
    '    End Get
    '    Set(ByVal Value As Long)
    '        mykey = Value
    '    End Set
    'End Property
    'Public Property Name() As String
    '    Get
    '        Return MyBase.Text
    '    End Get
    '    Set(ByVal Value As String)
    '        MyBase.Text = Name
    '    End Set

    'End Property
    'Public Property NodeName() As String
    '    Get
    '        Return m_NodeName
    '    End Get
    '    Set(ByVal Value As String)
    '        m_NodeName = Value
    '    End Set
    'End Property

    ' '' By Mahesh 
    ' '' for OrderDate
    'Public Property OrderTime() As Date
    '    Get
    '        Return _OrderTime
    '    End Get
    '    Set(ByVal Value As Date)
    '        _OrderTime = Value
    '    End Set
    'End Property

    ' '' By Mahesh 
    ' '' for Order Status (Finished / Not-Finished)
    'Public Property IsFinished() As Boolean
    '    Get
    '        Return _IsFinished
    '    End Get
    '    Set(ByVal Value As Boolean)
    '        _IsFinished = Value
    '    End Set
    'End Property
    'Public Property TemplateResult() As Object
    '    Get
    '        Return Result
    '    End Get
    '    Set(ByVal Value As Object)
    '        Result = Value
    '    End Set
    'End Property
    'Public Property IsNarcotics() As Int16
    '    Get
    '        Return m_IsNarcotics
    '    End Get
    '    Set(ByVal Value As Int16)
    '        m_IsNarcotics = Value
    '    End Set
    'End Property
    'Public Property DDID() As Int64
    '    Get
    '        Return m_ddID
    '    End Get
    '    Set(ByVal Value As Int64)
    '        m_ddID = Value
    '    End Set
    'End Property

    Inherits TreeNode
    Private mykey As Long
    Private m_NodeName As String
    Private Result As Object
    Private _OrderTime As Date
    Private _IsFinished As Boolean
    Private m_IsNarcotics As Int16

    'To get the Formulary status of the drug for the selected drug
    Private m_FormularyStatus As String

    'To get the RxType of the drug for the selected drug
    Private m_DrugRxType As String

    Private m_ddID As Int64
    Private m_SIGId As Long ''used for drug provider association
    '' SUDHIR 20090513 ''
    Private _ID As Int64
    Private _Code As String
    Private _sIndicator As String 'GLO2010-0005444'sIndicator

    '' GLO2011-0010684
    '' Variable used to hold the ROS Comments
    Private _sComment As String

    Private _Description As String
    Private _Unit As String
    Private _ConceptID As String
    Private _CPT As String
    Private _HistoryType As String
    Private _ICD9 As String
    Private _ICDRevision As String

    ''For De-Normalization  -20090127
    'Private m_DrugName As String = ""
    'Private m_Dosage As String = ""
    'Private m_DrugForm As String = ""
    ''For De-Normalization

    '// drugProvider form - Suraj 20090127
    Private m_DrugForm As String = ""
    Private m_Route As String = ""
    Private m_Frequency As String = ""
    Private m_Duration As String = ""
    Private m_NDCCode As String = ""
    Private m_DrugQtyQualifier As String = ""
    Private _IsSystem As String = ""
    '//

    ''sarika Fax from Referrals 20081121
    'Private m_FaxReferralName As String = ""
    ''---

    ''sarika Referral Letter 20081125
    'Private m_Referralletter As String = ""
    ''---------


    ''sarika DM Denormalization
    'Private m_TemplateName As String = ""
    'Private m_Template As Object = Nothing


    ''ICD9, CPT 
    'Private m_ICD9CPTCode As String = ""
    'Private m_ICD9CPTName As String = ""

    ''---sarika DM Denormalization-------------------
    'Private m_arrRefferalDetails As ArrayList = Nothing

    'Added by Ashish on 8-29-2013 for Education Association
    Private bIsSnomedCode As Boolean = False
    Private sSnomedID As String = ""

    '3-Sep-13 Ashish: Added bIsICD, bIsMedication, bIsDrugs, bIsLabs
    Private bIsICD9 As Boolean = False
    Private bIsMedication As Boolean = False
    Private bIsDrugs As Boolean = False
    Private bIsLabs As Boolean = False
    Private sTemplateName As String = String.Empty

    Sub New()
        MyBase.New("")
        mykey = 0
        m_SIGId = 0 '''''used for drugProvider association
    End Sub

    'Sub New(ByVal strname As String, ByVal key As Int64)
    '    MyBase.New(strname)
    '    mykey = key
    'End Sub
    Sub New(ByVal strname As String, ByVal key As Long)
        MyBase.New(strname)
        mykey = key
        m_NodeName = strname
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

    ''used for DrugProviderassociation, 
    Sub New(ByVal strname As String, ByVal key As Long, ByVal Drugname As String, ByVal NDCCODE As String, ByVal SIGID As Long)
        MyBase.New(strname)
        MyBase.Tag = Drugname
        mykey = key
        m_NDCCode = NDCCODE
        m_SIGId = SIGID
    End Sub

    Sub New(ByVal strname As String, ByVal key As Long, ByVal Drugname As String, ByVal DDID As Int64)
        MyBase.New(strname)
        MyBase.Tag = Drugname
        mykey = key
        m_ddID = DDID
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

    ''for drugproviderasscoiation
    Public Property SIGID() As Long
        Get
            Return m_SIGId
        End Get
        Set(ByVal Value As Long)
            m_SIGId = Value
        End Set
    End Property
    Public Property Key() As Long
        Get
            Return mykey
        End Get
        Set(ByVal Value As Long)
            mykey = Value
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

    'For de-Normalization
    Public Property NodeName() As String
        Get
            Return m_NodeName
        End Get
        Set(ByVal Value As String)
            m_NodeName = Value
        End Set
    End Property

    '' By Mahesh 
    '' for OrderDate
    Public Property OrderTime() As Date
        Get
            Return _OrderTime
        End Get
        Set(ByVal Value As Date)
            _OrderTime = Value
        End Set
    End Property

    '' By Mahesh 
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

    Public Property IsNarcotics() As Int16
        Get
            Return m_IsNarcotics
        End Get
        Set(ByVal Value As Int16)
            m_IsNarcotics = Value
        End Set
    End Property

    'Formulary Status
    Public Property FormularyStatus() As String
        Get
            Return m_FormularyStatus
        End Get
        Set(ByVal Value As String)
            m_FormularyStatus = Value
        End Set
    End Property

    'Drug RxType
    Public Property DrugRxType() As String
        Get
            Return m_DrugRxType
        End Get
        Set(ByVal Value As String)
            m_DrugRxType = Value
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

    Public Property ID() As Int64
        Get
            Return _ID
        End Get
        Set(ByVal value As Int64)
            _ID = value
        End Set
    End Property
    Public Property ConceptID() As String
        Get
            Return _ConceptID
        End Get
        Set(ByVal value As String)
            _ConceptID = value
        End Set
    End Property
    Public Property ICDRevision() As String
        Get
            Return _ICDRevision
        End Get
        Set(ByVal value As String)
            _ICDRevision = value
        End Set
    End Property
    Public Property ICD9() As String
        Get
            Return _ICD9
        End Get
        Set(ByVal value As String)
            _ICD9 = value
        End Set
    End Property
    Public Property CPT() As String
        Get
            Return _CPT
        End Get
        Set(ByVal value As String)
            _CPT = value
        End Set
    End Property
    Public Property HistoryType() As String
        Get
            Return _HistoryType
        End Get
        Set(ByVal value As String)
            _HistoryType = value
        End Set
    End Property
    Public Property Indicator() As String  'GLO2010-0005444:'sIndicator
        Get
            Return _sIndicator
        End Get
        Set(ByVal value As String)
            _sIndicator = value
        End Set
    End Property

    '' GLO2011-0010684
    '' Property used to hold the ROS Comments
    Public Property Comments() As String
        Get
            Return _sComment
        End Get
        Set(ByVal value As String)
            _sComment = value
        End Set
    End Property

    Public Property Code() As String
        Get
            Return _Code
        End Get
        Set(ByVal value As String)
            _Code = value
        End Set
    End Property

    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property

    Public Property Unit() As String
        Get
            Return _Unit
        End Get
        Set(ByVal value As String)
            _Unit = value
        End Set
    End Property

    'For De-Normalization
    '// drugProvider form - Suraj 20090127
    Public Property DrugForm() As String
        Get
            Return m_DrugForm
        End Get
        Set(ByVal Value As String)
            m_DrugForm = Value
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

    Public Property DrugQtyQualifier() As String
        Get
            Return m_DrugQtyQualifier
        End Get
        Set(ByVal value As String)
            m_DrugQtyQualifier = value
        End Set
    End Property

    Public Property IsSystemCategory() As String
        Get
            Return _IsSystem
        End Get
        Set(ByVal value As String)
            _IsSystem = value
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

    Public _arrRefferalDetails As ArrayList = Nothing

    Public Property arrRefferalDetails() As ArrayList
        Get
            Return Me._arrRefferalDetails
        End Get
        Set(ByVal value As ArrayList)
            Me._arrRefferalDetails = value
        End Set
    End Property

End Class
