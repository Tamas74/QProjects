Public Class mytable
    Private itemDescription As String
    Private itemCode As String
    Private m_Modcode As String
    Private m_unit As Int64
    Private _nICDRevision As Int16
    Sub New()
        MyBase.new()
    End Sub
    Sub New(ByVal strDescription As String, ByVal strcode As String, ByVal nICDRevision As Int16)
        MyBase.new()
        itemCode = strcode
        itemDescription = strDescription
        _nICDRevision = nICDRevision
    End Sub
    Sub New(ByVal strcode As String, ByVal strModCode As String, ByVal strDescription As String, ByVal intunit As Int64)
        MyBase.new()
        itemCode = strcode
        itemDescription = strDescription
        m_unit = intunit
        m_Modcode = strModCode
    End Sub
    Sub New(ByVal strcode As String, ByVal intunit As Int64)
        MyBase.new()
        itemCode = strcode
        m_unit = intunit
    End Sub
    Sub New(ByVal strcode As String, ByVal strDescription As String, ByVal strModCode As String)
        MyBase.new()
        itemCode = strcode
        itemDescription = strDescription
        m_Modcode = strModCode
    End Sub
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
            Return itemCode
        End Get
        Set(ByVal Value As String)
            itemCode = Value
        End Set
    End Property
    Public Property ModCode() As String
        Get
            Return m_Modcode
        End Get
        Set(ByVal Value As String)
            m_Modcode = Value
        End Set
    End Property
    Public Property Unit() As Int64
        Get
            Return m_unit
        End Get
        Set(ByVal Value As Int64)
            m_unit = Value
        End Set
    End Property
    Public Overrides Function Tostring() As String
        Return itemDescription
    End Function
End Class
