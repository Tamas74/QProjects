Imports System.Text.RegularExpressions
Public Class frmLabURLdocument

    Public Event AddURLEvent(URlDisplayName As String, URLName As String, Rowno As Integer)
    Public _blnOpenforModify As Boolean = False
    Private _UrlDisplayName As String = ""
    Private _UrlName As String = ""
    Private _RowNo As Integer = -1
    Public Property UrlDisplayName As String
        Get
            Return _UrlDisplayName
        End Get
        Set(value As String)
            _UrlDisplayName = value
        End Set
    End Property

    Public Property UrlName As String
        Get
            Return _UrlName
        End Get
        Set(value As String)
            _UrlName = value
        End Set
    End Property
    Public Property RowNo As Integer
        Get
            Return _RowNo
        End Get
        Set(value As Integer)
            _RowNo = value
        End Set
    End Property
    Private Sub frmLabURLdocument_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If _blnOpenforModify = True Then ''If form is open is Modify mode
            txtURLDisplayName.Text = UrlDisplayName
            txtURLName.Text = UrlName
            txtURLDisplayName.Focus()
        Else
            txtURLDisplayName.Text = DateTime.Now.ToString("MM dd yyyy hh mm ss tt")
            txtURLName.TabIndex = 0
        End If
    End Sub

    Private Sub ts_btnClose_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Sub ts_btnSave_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnSave.Click
        Try
            If Trim(txtURLDisplayName.Text) = "" Then
                MessageBox.Show("URL display name is required.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtURLDisplayName.Focus()
                Exit Sub
            End If

            If Trim(txtURLName.Text) = "" Then
                MessageBox.Show("URL is required.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                 txtURLName.Focus()
                    Exit Sub
            End If
            'If (Not ValidateURL()) Then
            'If MessageBox.Show("URL is invalid do you want to continue?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            '    txtURLName.Focus()
            '    Exit Sub
            'End If
            'End If
                If ((_blnOpenforModify = True) And (UrlDisplayName.Trim() = txtURLDisplayName.Text.Trim()) And (UrlName.Trim() = txtURLName.Text.Trim())) Then
                    Me.Close()
                    Exit Sub
                End If
                RaiseEvent AddURLEvent(txtURLDisplayName.Text, txtURLName.Text, RowNo)

                txtURLDisplayName.Focus()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'Private Function ValidateURL() As Boolean
    '    Dim b As Boolean = Regex.IsMatch(txtURLName.Text, "(https?:\/\/(?:www\.|(?!www))[^\s\.]+\.[^\s]{2,}|www\.[^\s]+\.[^\s]{2,})")
    '    Return b
    'End Function

    Private Sub txtURLName_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtURLName.KeyPress
       
    End Sub
End Class