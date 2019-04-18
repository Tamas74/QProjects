Imports System.Windows.Forms
Public Class myTreeNode
    Inherits TreeNode
    Implements IDisposable
    Private mykey As Long
    Private m_NodeName As String
    Private Result As Object
    Private _OrderTime As Date
    Private _IsFinished As Boolean
    Private m_IsNarcotics As Int16
    Private m_ppid As Int64

    'For De-Normalization  -20090127
    Private m_DrugName As String = ""
    Private m_Dosage As String = ""
    Private m_DrugForm As String = ""
    'For De-Normalization

    '// drugProvider form - Suraj 20090127
    Private m_Route As String = ""
    Private m_Frequency As String = ""
    Private m_Duration As String = ""
    Private m_NDCCode As String = ""
    Private m_DrugQtyQualifier As String = ""
    '//

    'sarika Fax from Referrals 20081121
    Private m_FaxReferralName As String = ""
    '---

    'sarika Referral Letter 20081125
    Private m_Referralletter As String = ""
    '---------


    'sarika DM Denormalization
    Private m_TemplateName As String = ""
    Private m_Template As Object = Nothing


    'ICD9, CPT 
    Private m_ICD9CPTCode As String = ""
    Private m_ICD9CPTName As String = ""

    '---sarika DM Denormalization-------------------
    Private m_arrRefferalDetails As ArrayList = Nothing
    Private _faxTo As String = ""
    Private _faxName As String = ""
    Private _faxCoverPage As String = ""


    Sub New()
        MyBase.New("")
        mykey = 0
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
    Sub New(ByVal strname As String, ByVal key As Long, ByVal Drugname As String, ByVal ppid As Int64)
        MyBase.New(strname)
        MyBase.Tag = Drugname
        mykey = key
        m_ppid = ppid
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
    'For De-Normalization -20090127
    Sub New(ByVal strname As String, ByVal key As Long, ByVal Drugname As String, ByVal Dosage As String, ByVal DrugForm As String, ByVal Route As String, ByVal Frequency As String, ByVal NDCCode As String, ByVal IsNarcotics As Int16, ByVal Duration As String, ByVal ppid As Int64, ByVal DrugQtyQualifier As String)
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
        m_ppid = ppid
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
    Public Property Name() As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal Value As String)
            MyBase.Text = Name
        End Set

    End Property
    'Public Property NodeName() As String
    '    Get
    '        Return m_NodeName
    '    End Get
    '    Set(ByVal Value As String)
    '        m_NodeName = Value
    '    End Set
    'End Property

    '' By Mahesh 
    '' for OrderDate

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
    'Public Property DDID() As Int64
    '    Get
    '        Return m_ppid
    '    End Get
    '    Set(ByVal Value As Int64)
    '        m_ppid = Value
    '    End Set
    'End Property

    'sarika Fax from Referrals 20081121

    Public Property FaxReferralName() As String
        Get
            Return m_FaxReferralName
        End Get
        Set(ByVal Value As String)
            m_FaxReferralName = Value
        End Set
    End Property

    '---

    'sarika Referral Letter 20081125

    Public Property FaxReferralLetter() As String
        Get
            Return m_Referralletter
        End Get
        Set(ByVal Value As String)
            m_Referralletter = Value
        End Set
    End Property

    '---
    'For de-Normalization  - 20090127
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
    Public Property Dosage() As String
        Get
            Return m_Dosage
        End Get
        Set(ByVal Value As String)
            m_Dosage = Value
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
    'For De-Normalization
    '// drugProvider form - Suraj 20090127
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

    'sarika DM Denormalization
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
    '---

    '//
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
