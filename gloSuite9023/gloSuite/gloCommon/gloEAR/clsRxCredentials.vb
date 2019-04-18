Public Class clsRxCredentials
    Implements IDisposable

    Dim oService As New eRx.eRxMessage

    Private _SenderID As String = ""
    Private _SenderParticipantPassword As String = ""

    Public Property SenderID() As String
        Get
            Return _SenderID
        End Get
        Set(ByVal value As String)
            _SenderID = value
        End Set
    End Property



    Public Property SenderParticipantPassword() As String
        Get
            Return _SenderParticipantPassword
        End Get
        Set(ByVal value As String)
            _SenderParticipantPassword = value
        End Set
    End Property


    Public Sub New()
        Dim _key As String = oService.Login("sarika@ophit.net", "spX12ss@!!21nasik")
        Dim strArr(1) As String

        strArr = oService.GetProvideRoot(_key)
        Dim objEnc As New clsRxencryption


        _SenderID = Convert.ToString(objEnc.DecryptFromBase64String(strArr(0).ToString(), "25487956"))
        _SenderParticipantPassword = Convert.ToString(objEnc.DecryptFromBase64String(strArr(1).ToString(), "25487956"))


    End Sub



    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
