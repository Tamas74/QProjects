Public Class ClsFaxException
    Inherits ApplicationException

    Public Sub New(ByVal strmessage As String)
        MyBase.New(strmessage)
    End Sub
    Public Sub New()
        MyBase.New()
    End Sub
End Class
