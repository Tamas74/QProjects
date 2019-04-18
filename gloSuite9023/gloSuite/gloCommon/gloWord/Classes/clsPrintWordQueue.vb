Public Class clsPrintWordQueue
    Public Property FilePath() As String
        Get
            Return m_FilePath
        End Get
        Set(value As String)
            m_FilePath = Value
        End Set
    End Property
    Private m_FilePath As String
    Public Property UserID() As Int64
        Get
            Return m_UserID
        End Get
        Set(value As Int64)
            m_UserID = Value
        End Set
    End Property
    Private m_UserID As Int64

End Class
