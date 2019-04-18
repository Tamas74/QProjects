Imports System.Data.SqlClient

Public Class frmLockScreen
    Inherits System.Windows.Forms.Form

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
    Private WithEvents pnlLogin As System.Windows.Forms.Panel
    Friend WithEvents pnltxtPassword As System.Windows.Forms.Panel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Private WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents pnltxtUserName As System.Windows.Forms.Panel
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Private WithEvents txtUserName As System.Windows.Forms.TextBox
    Private WithEvents btnOK As System.Windows.Forms.Button
    Private WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents PictureBox4 As System.Windows.Forms.PictureBox

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLockScreen))
        Me.pnlLogin = New System.Windows.Forms.Panel()
        Me.pnltxtPassword = New System.Windows.Forms.Panel()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.pnltxtUserName = New System.Windows.Forms.Panel()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.pnlLogin.SuspendLayout()
        Me.pnltxtPassword.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnltxtUserName.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlLogin
        '
        Me.pnlLogin.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.pnlLogin.BackColor = System.Drawing.Color.Transparent
        Me.pnlLogin.BackgroundImage = CType(resources.GetObject("pnlLogin.BackgroundImage"), System.Drawing.Image)
        Me.pnlLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLogin.Controls.Add(Me.pnltxtPassword)
        Me.pnlLogin.Controls.Add(Me.pnltxtUserName)
        Me.pnlLogin.Controls.Add(Me.btnOK)
        Me.pnlLogin.Controls.Add(Me.Panel2)
        Me.pnlLogin.Location = New System.Drawing.Point(199, 157)
        Me.pnlLogin.Name = "pnlLogin"
        Me.pnlLogin.Size = New System.Drawing.Size(453, 342)
        Me.pnlLogin.TabIndex = 13
        '
        'pnltxtPassword
        '
        Me.pnltxtPassword.BackColor = System.Drawing.Color.White
        Me.pnltxtPassword.Controls.Add(Me.PictureBox2)
        Me.pnltxtPassword.Controls.Add(Me.txtPassword)
        Me.pnltxtPassword.Location = New System.Drawing.Point(67, 216)
        Me.pnltxtPassword.Name = "pnltxtPassword"
        Me.pnltxtPassword.Size = New System.Drawing.Size(310, 26)
        Me.pnltxtPassword.TabIndex = 20
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(24, 26)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 16
        Me.PictureBox2.TabStop = False
        '
        'txtPassword
        '
        Me.txtPassword.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPassword.Location = New System.Drawing.Point(30, 7)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(255, 14)
        Me.txtPassword.TabIndex = 1
        '
        'pnltxtUserName
        '
        Me.pnltxtUserName.BackColor = System.Drawing.Color.White
        Me.pnltxtUserName.Controls.Add(Me.PictureBox3)
        Me.pnltxtUserName.Controls.Add(Me.txtUserName)
        Me.pnltxtUserName.Location = New System.Drawing.Point(67, 181)
        Me.pnltxtUserName.Name = "pnltxtUserName"
        Me.pnltxtUserName.Size = New System.Drawing.Size(310, 26)
        Me.pnltxtUserName.TabIndex = 19
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox3.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(24, 26)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 17
        Me.PictureBox3.TabStop = False
        '
        'txtUserName
        '
        Me.txtUserName.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtUserName.Location = New System.Drawing.Point(30, 6)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(255, 14)
        Me.txtUserName.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.btnOK.FlatAppearance.BorderSize = 0
        Me.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.ForeColor = System.Drawing.Color.White
        Me.btnOK.Location = New System.Drawing.Point(67, 253)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(310, 28)
        Me.btnOK.TabIndex = 6
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.PictureBox4)
        Me.Panel2.Location = New System.Drawing.Point(178, 58)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(89, 89)
        Me.Panel2.TabIndex = 8
        '
        'PictureBox4
        '
        Me.PictureBox4.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox4.BackgroundImage = CType(resources.GetObject("PictureBox4.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox4.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(89, 89)
        Me.PictureBox4.TabIndex = 0
        Me.PictureBox4.TabStop = False
        '
        'frmLockScreen
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(851, 656)
        Me.Controls.Add(Me.pnlLogin)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLockScreen"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlLogin.ResumeLayout(False)
        Me.pnltxtPassword.ResumeLayout(False)
        Me.pnltxtPassword.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnltxtUserName.ResumeLayout(False)
        Me.pnltxtUserName.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Dim x As Short
    Private Declare Function disable Lib "user32" Alias "SystemParametersInfoA" (ByVal a As Integer, ByVal b As Integer, ByRef c As Boolean, ByVal d As Integer) As Integer
    Dim blnClose As Boolean = False
    Dim NoofAttempts As Integer = 0
    Dim AccessFlag As Boolean

    Private Sub frmLockScreen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            txtUserName.Focus()
            LockWindowUpdate(Me.Handle)
            Me.SuspendLayout()
            Dim obj As gloUIControlLibrary.WPFUserControl.gloPMAdminLockScreen = New gloUIControlLibrary.WPFUserControl.gloPMAdminLockScreen()
            ' ElementHost1.Child = obj
            Me.ResumeLayout()

            'pnlLoginInfo.Visible = False
            'x = disable(97, True, False, 0)
            'Dim pt As New Point(Me.Width / 2 - 165, Me.Height / 2 - 65)
            'pnlLoginInfo.Location = pt
            'pnlLoginInfo.Visible = True


            ' NoofAttempts = 0
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub frmLockScreen_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            If blnClose = False Then
                e.Cancel = True
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Function CheckLogin() As Boolean
        'Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
        'Dim cmd As SqlCommand
        'Dim oDataReader As SqlDataReader
        'Dim blnResetPwdFlag As Boolean = False
        'Dim blnIsAccessDenied As Boolean = False
        'Dim _strSQL As String = ""
        AccessFlag = False

        If Trim(txtUserName.Text) = "" Then
            MessageBox.Show("User Name must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtUserName.Focus()
            AccessFlag = True
            Return False
        End If
        If Trim(txtPassword.Text) = "" Then
            MessageBox.Show("Password must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtPassword.Focus()
            AccessFlag = True
            Return False
        End If


        'sarika 19th july 08
        'Bug 859

        '***************
        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim oDataReader As SqlDataReader
        Dim blnResetPwdFlag As Boolean = False
        Dim _strSQL As String = ""
        '*********************
        _strSQL = "select nAdministrator from User_Mst where sLoginName ='" & txtUserName.Text.Trim.Replace("'", "''") & "'"
        conn.Open()
        cmd = New SqlCommand(_strSQL, conn)
        oDataReader = cmd.ExecuteReader

        If Not oDataReader Is Nothing Then
            If oDataReader.HasRows = True Then
                While oDataReader.Read
                    If Not IsDBNull(oDataReader.Item("nAdministrator")) Then
                        'if not nulll then set the value of flag 
                        gblnAdmin = oDataReader.Item("nAdministrator")
                    Else
                        'if the value is null then set the flag to false
                        gblnAdmin = False
                    End If
                End While
                'the value can be NULL besides 0 and 1 , so chk for null value
            End If
            oDataReader.Close()
        End If
        conn.Close()


        If gblnAdmin = False Then



            '//the user is not an administrator so, he should not be allowed to login to the Admin system 
            '//though he enters valid credentials as only administrator users are allowed to login to the admin module

            MessageBox.Show("Access denied. User " & txtUserName.Text & " have no rights to access gloPM admin application.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Function

            '----------------------
        End If



        Dim objLogin As New clsLogin
        If gblnAdmin = False Then
            If objLogin.IsClientAccess(gstrClientMachineName) = False Then
                MessageBox.Show("This machine does not have rights to access gloPM system.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                objLogin = Nothing
                End
            End If
            'Check User has rights to access the gloEMR Admin or not
            If objLogin.IsAccessPermission(txtUserName.Text, True) = False Then
                MessageBox.Show("Access Denied." & vbCrLf & txtUserName.Text & " user has no rights to access gloPM Admin application.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtUserName.Focus()
                objLogin = Nothing
                AccessFlag = True
                Return False
            End If
        End If

        Dim objEncryption As New clsEncryption
        Dim strPassword As String
        strPassword = objEncryption.EncryptToBase64String(txtPassword.Text, constEncryptDecryptKey)
        objEncryption = Nothing
        If objLogin.IsValidLogin(txtUserName.Text, strPassword) = False Then
            MessageBox.Show("Invalid User Name/Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtUserName.Focus()
            objLogin = Nothing
            Return False
        End If
        If objLogin.IsAccessPermission(txtUserName.Text) = False Then
            MessageBox.Show("Access Denied." & vbCrLf & txtUserName.Text & " Please contact the administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtUserName.Focus()
            objLogin = Nothing
            AccessFlag = True
            Return False
        End If
        objLogin = Nothing

        Return True
    End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim oDataReader As SqlDataReader
        Dim blnResetPwdFlag As Boolean = False
        Dim blnIsAccessDenied As Boolean = False
        Dim _strSQL As String = ""

        Dim blnIsAdministrator As Boolean

        Dim objAudit As New clsAudit
        Try

            conn.Open()
            _strSQL = "select nAccessDenied from User_MST where sLoginName = '" & gstrLoginName.Replace("'", "''") & "'"
            cmd = New SqlClient.SqlCommand(_strSQL, conn)
            oDataReader = cmd.ExecuteReader


            If Not oDataReader Is Nothing Then
                If oDataReader.HasRows = True Then
                    While oDataReader.Read
                        'the value can be NULL besides 0 and 1 , so chk for null value
                        If Not IsDBNull(oDataReader.Item("nAccessDenied")) Then
                            'if not nulll then set the value of flag 
                            blnIsAccessDenied = oDataReader.Item("nAccessDenied")
                        Else
                            'if the value is null then set the flag to false
                            blnIsAccessDenied = False
                        End If
                    End While
                End If
                oDataReader.Close()
            End If


            If blnIsAccessDenied = False Then
                'sarika 14th feb --------
                If gstrLoginName <> txtUserName.Text Then
                    gstrLoginName = txtUserName.Text
                    NoofAttempts = 0
                End If
                '-------------------------

                If CheckLogin() = True Then
                    ''''''<<<<<<><><><><><>>>>>>
                    '''' If Previous User & Current User are Different then
                    'If _PreviousLoginName <> gstrLoginName Then
                    'Dim objclsExam As New clsPatientExams
                    '    ''' Locked Exams for Previous are Updated By New User (Current User)
                    'objclsExam.Update_UnLock_Exam(gstrLoginName, _PreviousLoginName)
                    '    objclsExam = Nothing
                    'End If
                    ''''''<<<<<<><><><><><>>>>>>

                    'sarika  21 feb
                    'Dim objAudit As New clsAudit
                    objAudit.CreateLog(clsAudit.enmActivityType.Login, gstrLoginName & " user has logged in again.", gstrLoginName, gstrClientMachineName)
                    objAudit = Nothing
                    '-------------


                    'sarika 21st july 08 
                    '--- Bug 859
                    gstrLoginTime = CType(Format(Date.Now, "Medium Time"), String)
                    gstrLoginName = txtUserName.Text.Trim()
                    '---------------
                    Dim a As Short
                    a = disable(97, False, False, 0)
                    blnClose = True
                    Me.Close()
                Else
                    'sarika  20th april 2007
                    'Dim objAudit As New clsAudit
                    objAudit.CreateLog(clsAudit.enmActivityType.NodeAuthenticationFailure, "Login Attempt failed on Lockscreen due to incorrect credentials.", gstrLoginName, gstrClientMachineName, , , clsAudit.enmOutcome.Failure)

                    '-------------
                    If AccessFlag = False Then
                        NoofAttempts = NoofAttempts + 1
                        If NoofAttempts >= gintNoOfAttempts Then

                            _strSQL = "select nAdministrator from User_MST where sLoginName = '" & txtUserName.Text.Trim.Replace("'", "''") & "'"
                            cmd = New SqlClient.SqlCommand(_strSQL, conn)
                            oDataReader = cmd.ExecuteReader

                            If Not oDataReader Is Nothing Then
                                If oDataReader.HasRows = True Then
                                    While oDataReader.Read
                                        'the value can be NULL besides 0 and 1 , so chk for null value
                                        If Not IsDBNull(oDataReader.Item("nAdministrator")) Then
                                            'if not nulll then set the value of flag 
                                            blnIsAdministrator = oDataReader.Item("nAdministrator")
                                        Else
                                            'if the value is null then set the flag to false
                                            blnIsAdministrator = False
                                        End If
                                    End While
                                End If
                                oDataReader.Close()
                            End If
                            'set the Access Denied flag for this user in the user_mgt table to True .
                            If blnIsAdministrator = False Then
                                'sarika Remove Restrict Access form
                                ' Me.Hide()
                                '   Dim frmNoAccess As New frmRestrictAccess
                                'sarika Remove Restrict Access form
                                If SetAccessDeniedFlag() = True Then
                                    objAudit.CreateLog(clsAudit.enmActivityType.SecurityAdmin, "User's access to gloPM admin has been denied since, he could not enter correct credentials and exceeded the number of LockOut attempts on LockScreen. ", gstrLoginName, gstrClientMachineName, , , clsAudit.enmOutcome.Failure)
                                    MessageBox.Show("Your access to  gloPM system is denied . Please Contact the administrator.", "gloPM Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                                'sarika Remove Restrict Access form
                                ' frmNoAccess.ShowDialog()
                                'sarika Remove Restrict Access form
                                End
                            End If
                        End If

                        txtPassword.Text = ""

                    End If
                End If
            Else

                MessageBox.Show("Your access is denied . Please contact the administrator", "gloPM admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
                objAudit.CreateLog(clsAudit.enmActivityType.SecurityAdmin, "User with no access to gloPM admin attempted to login.", gstrLoginName, gstrClientMachineName, , , clsAudit.enmOutcome.Failure)
                '  Exit Sub
                End
            End If
            objAudit = Nothing
            conn.Close()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function SetAccessDeniedFlag() As Boolean

        'sets the AccessDenied flag to true for the user
        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        '  Dim oDataReader As SqlDataReader
        Dim _strSQL As String = ""

        Try
            conn.Open()


            If txtUserName.Text.Trim <> "" Then
                _strSQL = "update User_MST set nAccessDenied = 1 where sLoginName ='" & txtUserName.Text.Trim.Replace("'", "''") & "'"
            Else
                MessageBox.Show("You must enter the  Username.", "gloPM Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If


            cmd = New SqlCommand(_strSQL, conn)

            cmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            ' MsgBox(ex.Message)
            ' MessageBox.Show("Error validating ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            conn.Close()

        End Try
    End Function

    Private Sub frmLockScreen_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        LockWindowUpdate(IntPtr.Zero)
    End Sub


    Private Shared Function LockWindowUpdate(hWnd As IntPtr) As Boolean
    End Function


End Class
