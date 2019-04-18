Namespace gloEMRDatabase
    Public Class gloDBException
        Inherits ApplicationException
        Private _ErrMessage As String
        Private _ErrCode As String
        Public Property ErrMessage() As String
            Get
                Return _ErrMessage
            End Get
            Set(ByVal Value As String)
                _ErrMessage = Value
            End Set
        End Property

        Public Property ErrCode() As String
            Get
                Return _ErrCode
            End Get
            Set(ByVal Value As String)
                _ErrCode = Value
            End Set
        End Property
    End Class
End Namespace