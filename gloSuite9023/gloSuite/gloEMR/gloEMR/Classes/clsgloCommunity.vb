Imports gloCommunity.Classes
Public Class clsgloCommunity
    Public Function CheckADUserEmail() As Boolean
        Dim _blnConfigured As Boolean = True
        Dim CHKADResult As Integer = clsGetADUser.CheckADuser()
        If CHKADResult = 0 Then               ''        If clsGetADUser.CheckADuser() = False Then
            Dim _Result As Integer = Convert.ToInt32(MessageBox.Show("User E-mail Id is not configured to Active Directory. Do you want to configure?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3))
            If _Result = Convert.ToInt32(DialogResult.Yes) Then
                Dim ofrmEmail As New gloCommunity.frmEmailConfig()
                Dim _frmResult As Integer = Convert.ToInt32(ofrmEmail.ShowDialog(ofrmEmail.Parent))
                If _frmResult = Convert.ToInt32(DialogResult.Cancel) Then
                    _blnConfigured = False
                End If
                ofrmEmail.Dispose()
                ofrmEmail = Nothing
            Else
                _blnConfigured = False
            End If
        ElseIf CHKADResult = 2 Then
            MessageBox.Show("Windows Login User Does Not have Rights to Add E-mail Address in Active Directory." + vbCrLf + "Please Contact System Administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            _blnConfigured = False
        End If
        Return _blnConfigured
    End Function
End Class
