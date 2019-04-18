Public Class DrugInteractionControlException
    Inherits ApplicationException
    Private m_message As String
    Public Property ErrMessage() As String
        Get
            Return m_message
        End Get
        Set(ByVal Value As String)
            m_message = Value
        End Set
    End Property
End Class
