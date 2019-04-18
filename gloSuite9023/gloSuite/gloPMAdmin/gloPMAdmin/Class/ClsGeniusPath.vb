''Sandip Darade 20090830
''Added  class for  Genious Path setting
Public Class ClsGeniusPath
    Private _GeniusPath As String = ""
    Private _GeniusCode As String = ""
    Private _GeniusDescription As String = ""
    'path
    'Code
    'Description
    Public Property GeniusPath() As String
        Get
            Return _GeniusPath
        End Get
        Set(ByVal Value As String)
            _GeniusPath = Value
        End Set
    End Property
    Public Property GeniusCode() As String
        Get
            Return _GeniusCode
        End Get
        Set(ByVal Value As String)
            _GeniusCode = Value
        End Set
    End Property
    Public Property GeniusDescription() As String
        Get
            Return _GeniusDescription
        End Get
        Set(ByVal Value As String)
            _GeniusDescription = Value
        End Set
    End Property

End Class
