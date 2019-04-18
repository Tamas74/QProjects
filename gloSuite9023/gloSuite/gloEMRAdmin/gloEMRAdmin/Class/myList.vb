Public Class myList
    Private itemindex As Int64
    Private itemDescription As String
    Private itemType As Boolean
    Private itemId As System.Int64
    Private itemVisitDate As Date
    Private _IsFinished As Boolean
    Private _HistoryCategory As String
    Private _HistoryItem As String

    Private _Code As String
    Private _ParameterName As String
    'Private _Operater As gloStream.gloCMS.Supporting.[Operator]
    Private _Value As String

    Private Result As Object

    ''sudhir 
    Private _gloEMR_Provider As String
    Private _gloPM_Provider As String
    Private _ExternalID As String

    'Shubahngi
    Private _ExamControlType As enumExamControlType
    Private _AssociatedCategory As String = String.Empty
    'End


    Sub New(ByVal intindex As Int64, ByVal strDescription As String)
        itemindex = intindex
        itemDescription = strDescription

    End Sub
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
    Sub New()

    End Sub
    'Shubhangi 20100105
    'Procedures related to EM Exam type
    Public Property AssociatedCategory() As String
        Get
            Return _AssociatedCategory
        End Get
        Set(ByVal value As String)
            _AssociatedCategory = value
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
    'End

    Public Property ParameterName() As String
        Get
            Return _ParameterName
        End Get
        Set(ByVal Value As String)
            _ParameterName = Value
        End Set
    End Property

    'Public Property Operater() As gloStream.gloCMS.Supporting.[Operator]
    '    Get
    '        Return _Operater
    '    End Get
    '    Set(ByVal Value As gloStream.gloCMS.Supporting.[Operator])
    '        _Operater = Value
    '    End Set
    'End Property

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
    Public Property ID() As Int64
        Get
            Return itemId
        End Get
        Set(ByVal Value As Int64)
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
    Public Property IsFinished() As Boolean
        Get
            Return _IsFinished
        End Get
        Set(ByVal Value As Boolean)
            _IsFinished = Value
        End Set
    End Property

    Public Overrides Function Tostring() As String
        Return itemDescription
    End Function

    ''sudhir
    Public Property gloEMR_Provider() As String
        Get
            Return _gloEMR_Provider
        End Get
        Set(ByVal value As String)
            _gloEMR_Provider = value
        End Set
    End Property

    Public Property gloPM_Provider() As String
        Get
            Return _gloPM_Provider
        End Get
        Set(ByVal value As String)
            _gloPM_Provider = value
        End Set
    End Property

    Public Property ExternalID() As String
        Get
            Return _ExternalID
        End Get
        Set(ByVal value As String)
            _ExternalID = value
        End Set
    End Property
End Class
