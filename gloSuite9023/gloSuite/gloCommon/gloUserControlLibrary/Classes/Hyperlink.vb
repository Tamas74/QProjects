Public Class Hyperlink

    Dim _text As String = String.Empty
    Dim _link As String = String.Empty
    Dim _visited As Boolean = False

    Public Sub New(ByVal text As String, ByVal link As String)
        _text = text
        _link = link
        _visited = False
    End Sub

    Public Sub New(ByVal link As String)
        _text = link
        _link = link
        _visited = False
    End Sub

    Public ReadOnly Property Visited() As Boolean
        Get
            Return _visited
        End Get
    End Property

    Public Function Activate() As Boolean
        Activate = False
        Try
            _visited = True
            gloGlobal.gloLabGenral.OpenLinkInBrowser(_link)
        Catch ex As Exception
            ex = Nothing
            Activate = False
        End Try
    End Function

    Public ReadOnly Property GetLink() As String
        Get
            Return _link
        End Get
    End Property

    Public ReadOnly Property GetText() As String
        Get
            Return _text
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return _text
    End Function

End Class
