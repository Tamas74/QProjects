Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.DirectoryServices

Namespace gloCommunity
	Public Partial Class frmEmailConfig
		Inherits Form
        Private _MessageBoxCaption As String = gstrMessageBoxCaption
		Public Sub New()
			InitializeComponent()
		End Sub

        '      'Private Sub frmEmailConfig_Load(sender As Object, e As EventArgs)

        '      'End Sub

        'Private Sub btnConfig_Click(sender As Object, e As EventArgs)
        '	If String.IsNullOrEmpty(txtEmail.Text.Trim()) Then
        '		MessageBox.Show("Please enter the E-mail", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '		txtEmail.Focus()
        '		Return
        '	End If
        '	If CheckEmail(txtEmail.Text) = False Then
        '		MessageBox.Show("Please enter valid E-mail address", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '		txtEmail.Focus()
        '		Return
        '	End If
        '	Dim result As SearchResult = Nothing
        '	Try
        '		'Set user email code
        '              result = clsGetADUser.GetADuser()
        '		If result IsNot Nothing AndAlso result.Properties.Count > 0 Then
        '			Dim entryToUpdate As DirectoryEntry = result.GetDirectoryEntry()
        '			entryToUpdate.Properties("mail").Value = txtEmail.Text
        '			entryToUpdate.CommitChanges()
        '		End If
        '	Catch ex As Exception

        '		Me.Cursor = Cursors.[Default]
        '	Finally
        '		If result IsNot Nothing Then
        '			result = Nothing
        '		End If
        '		Me.Close()
        '	End Try

        'End Sub

        'Private Sub frmEmailConfig_FormClosing(sender As Object, e As FormClosingEventArgs)

        'End Sub

		Private Function CheckEmail(EmailAddress As String) As Boolean
			Dim strPattern As String = "^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"

			If System.Text.RegularExpressions.Regex.IsMatch(EmailAddress, strPattern) Then

				Return True
			End If
			Return False
		End Function


        Private Sub btnConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfig.Click
            If String.IsNullOrEmpty(txtEmail.Text.Trim()) Then
                MessageBox.Show("Please enter the E-mail", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtEmail.Focus()
                Return
            End If
            If CheckEmail(txtEmail.Text) = False Then
                MessageBox.Show("Please enter valid E-mail address", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtEmail.Focus()
                Return
            End If
            Dim result As SearchResult = Nothing
            Try
                'Set user email code
                result = clsGetADUser.GetADuser()
                If result IsNot Nothing AndAlso result.Properties.Count > 0 Then
                    Dim entryToUpdate As DirectoryEntry = result.GetDirectoryEntry()
                    entryToUpdate.Properties("mail").Value = txtEmail.Text
                    entryToUpdate.CommitChanges()
                    Me.Close()
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                End If
            Catch ex As Exception

                Me.Cursor = Cursors.Default
                MessageBox.Show("Windows Login User Does Not have Rights to Add E-mail Address in Active Directory.\nPlease Contact System Administrator.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Finally
                If result IsNot Nothing Then
                    result = Nothing
                End If
                
            End Try
        End Sub
    End Class
End Namespace
