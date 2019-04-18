Public Class gloSurescriptDBException
    Inherits System.ApplicationException


    Private m_source As String
    Private m_ErrorCode As String

#Region "Public Constructors"
    Public Sub New(ByVal Message As String, ByVal source As String, ByVal errorcode As String)
        MyBase.New(Message)
    End Sub
    Public Sub New(ByVal Message As String)
        MyBase.New(Message)
    End Sub
#End Region

#Region "Public Property Procedures"
    Public Overrides Property Source() As String
        Get
            Return m_source
        End Get
        Set(ByVal Value As String)
            m_source = Value
        End Set
    End Property
    Public Property ErrorCode() As String
        Get
            Return m_source
        End Get
        Set(ByVal Value As String)
            m_source = Value
        End Set
    End Property

#End Region

End Class

