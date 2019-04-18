'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************

Imports System.Diagnostics
Imports System.IO
Public Class frmLogin
    Inherits System.Windows.Forms.Form

    Dim nNoOfLoginAttempt As Byte
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents picLoginBackground As System.Windows.Forms.PictureBox
    Friend WithEvents pnlDate As System.Windows.Forms.Panel
    Friend WithEvents lblDate As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmLogin))
        Me.picLoginBackground = New System.Windows.Forms.PictureBox
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.lblVersion = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnLogin = New System.Windows.Forms.Button
        Me.pnlDate = New System.Windows.Forms.Panel
        Me.lblDate = New System.Windows.Forms.Label
        Me.pnlDate.SuspendLayout()
        Me.SuspendLayout()
        '
        'picLoginBackground
        '
        Me.picLoginBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picLoginBackground.Image = CType(resources.GetObject("picLoginBackground.Image"), System.Drawing.Image)
        Me.picLoginBackground.Location = New System.Drawing.Point(0, 0)
        Me.picLoginBackground.Name = "picLoginBackground"
        Me.picLoginBackground.Size = New System.Drawing.Size(374, 218)
        Me.picLoginBackground.TabIndex = 0
        Me.picLoginBackground.TabStop = False
        '
        'txtUserName
        '
        Me.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserName.Location = New System.Drawing.Point(103, 98)
        Me.txtUserName.MaxLength = 99
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(127, 21)
        Me.txtUserName.TabIndex = 0
        Me.txtUserName.Text = ""
        '
        'txtPassword
        '
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassword.Location = New System.Drawing.Point(103, 138)
        Me.txtPassword.MaxLength = 99
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(127, 21)
        Me.txtPassword.TabIndex = 1
        Me.txtPassword.Text = ""
        '
        'lblVersion
        '
        Me.lblVersion.BackColor = System.Drawing.Color.FromArgb(CType(51, Byte), CType(167, Byte), CType(248, Byte))
        Me.lblVersion.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(108, 78)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(94, 16)
        Me.lblVersion.TabIndex = 4
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(242, 176)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(64, 24)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "&Cancel"
        '
        'btnLogin
        '
        Me.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogin.Location = New System.Drawing.Point(166, 176)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(64, 24)
        Me.btnLogin.TabIndex = 2
        Me.btnLogin.Text = "&Login"
        '
        'pnlDate
        '
        Me.pnlDate.BackColor = System.Drawing.Color.FromArgb(CType(47, Byte), CType(164, Byte), CType(246, Byte))
        Me.pnlDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDate.Controls.Add(Me.lblDate)
        Me.pnlDate.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlDate.Location = New System.Drawing.Point(0, 218)
        Me.pnlDate.Name = "pnlDate"
        Me.pnlDate.Size = New System.Drawing.Size(374, 32)
        Me.pnlDate.TabIndex = 5
        '
        'lblDate
        '
        Me.lblDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(70, 4)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(298, 20)
        Me.lblDate.TabIndex = 0
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmLogin
        '
        Me.AcceptButton = Me.btnLogin
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(374, 250)
        Me.Controls.Add(Me.pnlDate)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUserName)
        Me.Controls.Add(Me.picLoginBackground)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLogin"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "gloEMR Admin Login"
        Me.TopMost = True
        Me.pnlDate.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Retrieve the Computer Name and store it in global variable
            gstrClientMachineName = System.Windows.Forms.SystemInformation.ComputerName()
            'Display gloEMR Version No
            lblVersion.Text = "Version: " & RetrieveVersion()
            Call ShowDateStamp()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Retrieve gloEMR Version No
    Private Function RetrieveVersion() As String
        RetrieveVersion = "1.0.0"
    End Function



    'Procedure to validate the Login
    'Check User Name & Password is entered or not
    'Check User Name and Passwords are valid or not
    'Return True if User Name & Passwords are valid
    Private Function CheckLogin() As Boolean
        'Check User Name is entered or not
        If Trim(txtUserName.Text) = "" Then
            'User Name is not entered
            MessageBox.Show("User Name must be enter", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtUserName.Focus()
            Return False
        End If
        'Check Password is entered or not
        If Trim(txtPassword.Text) = "" Then
            'Password is not entered
            MessageBox.Show("Password must be enter", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtPassword.Focus()
            Return False
        End If
        'User Name and password are enetered

        Dim objLogin As New clsLogin
        'Check this Client machine has rights to access the gloEMR or not
        If objLogin.IsClientAccess(gstrClientMachineName) = False Then
            'Client machine does not have rights to access the gloEMR
            MessageBox.Show("This machine does not have rights to access gloEMR system.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objLogin = Nothing
            End
        End If

        'Encrypt the Password
        Dim objEncryption As New clsEncryption
        Dim strPassword As String
        strPassword = objEncryption.EncryptToBase64String(txtPassword.Text, constEncryptDecryptKey)
        objEncryption = Nothing

        'Check User Name and Passwords are valid or not
        If objLogin.IsValidLogin(txtUserName.Text, strPassword) = False Then
            'User Name or Password is not valid
            MessageBox.Show("Invalid User Name/Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtUserName.Focus()
            objLogin = Nothing
            Return False
        End If

        'Check the User is blocked or not
        If objLogin.IsAccessPermission(txtUserName.Text) = False Then
            'User is blocked
            MessageBox.Show("Access Denied." & vbCrLf & txtUserName.Text & " user has been blocked.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtUserName.Focus()
            objLogin = Nothing
            Return False
        End If
        'Check User has rights to access the gloEMR Admin or not
        If objLogin.IsAccessPermission(txtUserName.Text, True) = False Then
            MessageBox.Show("Access Denied." & vbCrLf & txtUserName.Text & " user has not rights to access gloEMR Admin application.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtUserName.Focus()
            objLogin = Nothing
            Return False
        End If
        objLogin = Nothing
        'User is valid and has rights to access the gloEMR Admin
        Return True
    End Function


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            End
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Try
            If CheckLogin() = True Then
                gstrLoginName = txtUserName.Text
                gstrLoginPassword = txtPassword.Text

                'sarika  21 feb
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.Login, gstrLoginName & " user has logged in.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing

                '-------------
                Dim frmgloEMRMain As New frmgloEMRAdmin
                Me.Hide()
                frmgloEMRMain.ShowDialog()
            Else
                nNoOfLoginAttempt = nNoOfLoginAttempt + 1
                If nNoOfLoginAttempt >= 3 Then
                    '     Me.Hide()
                    MessageBox.Show("Access Denied, Please contact the administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End
                    '   Dim frmNoAccess As New frmRestrictAccess
                    '  frmNoAccess.ShowDialog()
                End If
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

  
    Private Sub txtPassword_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPassword.GotFocus
        Try
            Dim state As New clsKeyBoardState(CType(Keys.CapsLock, Integer))
            If state.KeyState = True Then
                Dim tlbCaps As New ToolTip
                tlbCaps.SetToolTip(txtPassword, "Caps Lock On")
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ShowDateStamp()
        Try
            Dim aModuleName As String = Diagnostics.Process.GetCurrentProcess.MainModule.ModuleName
            lblDate.Text = "Last Modified Date " & File.GetLastWriteTime(Application.StartupPath & "\" & aModuleName)
        Catch ex As Exception

        End Try
        
    End Sub
End Class
