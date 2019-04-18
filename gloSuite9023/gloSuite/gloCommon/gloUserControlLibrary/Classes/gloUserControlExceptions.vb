Public Class gloUserControlExceptions
    Inherits ApplicationException

    Private _errMessage As String = ""
    Private _errCode As String = ""

    Public Property ErrorMessage() As String
        Get
            Return _errMessage
        End Get
        Set(ByVal value As String)
            _errMessage = value
        End Set
    End Property

    Public Property ErrorCode() As String
        Get
            Return _errCode
        End Get
        Set(ByVal value As String)
            _errCode = value
        End Set
    End Property



End Class
Public Class ClsUserControlGeneral
    Public Shared Connectionstring As String
End Class
