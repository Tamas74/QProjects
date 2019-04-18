Public Class clsBrowseForFolder
    Inherits System.Windows.Forms.Design.FolderNameEditor

    Public Function BrowseDialog(ByVal sTitle As String) As String
        Dim bDialog As New FolderBrowser
        Dim thisPath As String = Nothing
        With bDialog
            .Description = sTitle
            .ShowDialog(System.Windows.Forms.Form.ActiveForm)
            thisPath = .DirectoryPath()
            .Dispose()
        End With
        bDialog = Nothing
        Return thisPath
    End Function
End Class
