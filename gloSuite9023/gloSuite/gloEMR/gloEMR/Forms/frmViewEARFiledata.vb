Public Class frmViewEARFiledata

    Dim _EARTempFileName As String = ""

    Private Sub tlbbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlbbtnClose.Click
        Me.Close()
    End Sub

    Public Sub New(ByVal EARTempFileName As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _EARTempFileName = EARTempFileName
    End Sub

    Private Sub frmViewEARFiledata_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If _EARTempFileName <> "" Then
                rchtxtbxEARFiledata.Text = My.Computer.FileSystem.ReadAllText(_EARTempFileName)
                My.Computer.FileSystem.DeleteFile(_EARTempFileName)
            Else
                MessageBox.Show("Invalid EAR request file information", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class