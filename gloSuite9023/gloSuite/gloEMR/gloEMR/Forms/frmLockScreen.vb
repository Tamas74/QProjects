Imports System.Data.SqlClient
Imports System.Runtime.InteropServices

'Change For Resolving case no GLO2010-0007101
Public Class frmLockScreen
    Inherits frmImmoveableForm
    ' Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        'Change For Resolving case no GLO2010-0007101
        Me.Moveable = False

    End Sub

#Region " TO Check the Multiple instances Of Form "

    '' TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents pnltxtUserName As System.Windows.Forms.Panel
    Friend WithEvents pnltxtPassword As System.Windows.Forms.Panel
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    '' Private Shared _mu As New Mutex
    Private Shared frm As frmLockScreen

    ''Form overrides dispose to clean up the component list.
    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        ' Check to see if Dispose has already been called.
        If Not (Me.blnDisposed) Then
            ' If disposing equals true, dispose all managed
            ' and unmanaged resources.
            If (disposing) Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                ' Dispose managed resources.
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
                'frm = Nothing
            End If
            ' Release unmanaged resources. If disposing is false,
            ' only the following code is executed.

            ' Note that this is not thread safe.
            ' Another thread could start disposing the object
            ' after the managed resources are disposed,
            ' but before the disposed flag is set to true.
            ' If thread safety is necessary, it must be
            ' implemented by the client.
        End If
        frm = Nothing
        Me.blnDisposed = True

    End Sub

    Public Overloads Sub Dispose()
        Dispose(True)
        ' Take yourself off of the finalization queue
        ' to prevent finalization code for this object
        ' from executing a second time.
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Public Shared Function GetInstance()
        '_mu.WaitOne()
        Try
            If frm Is Nothing Then
                frm = New frmLockScreen()
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.LockScreen, gloAuditTrail.ActivityType.Initialize, "Lock Screen New Insatance Created", gloAuditTrail.ActivityOutCome.Success)
                'UpdateLog("Lock Screen New Insatanc Created")
            Else
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.LockScreen, gloAuditTrail.ActivityType.Initialize, "Lock Screen Existing Insatance Found", gloAuditTrail.ActivityOutCome.Success)
                'UpdateLog("Lock Screen Existing Insatanc Found")
            End If
        Finally
            '_mu.ReleaseMutex()
        End Try
        Return frm
    End Function

#End Region


    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLockScreen))
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.pnltxtPassword = New System.Windows.Forms.Panel()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.pnltxtUserName = New System.Windows.Forms.Panel()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnltxtPassword.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnltxtUserName.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtPassword
        '
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPassword.Location = New System.Drawing.Point(29, 6)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(233, 14)
        Me.txtPassword.TabIndex = 2
        '
        'txtUserName
        '
        Me.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtUserName.Location = New System.Drawing.Point(29, 6)
        Me.txtUserName.Margin = New System.Windows.Forms.Padding(0)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(233, 14)
        Me.txtUserName.TabIndex = 1
        '
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnOK.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(101, Byte), Integer), CType(CType(194, Byte), Integer))
        Me.btnOK.FlatAppearance.BorderSize = 0
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.ForeColor = System.Drawing.Color.White
        Me.btnOK.Location = New System.Drawing.Point(66, 256)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(310, 28)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "&OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.PictureBox4)
        Me.Panel1.Controls.Add(Me.btnOK)
        Me.Panel1.Controls.Add(Me.pnltxtPassword)
        Me.Panel1.Controls.Add(Me.pnltxtUserName)
        Me.Panel1.Location = New System.Drawing.Point(231, 180)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(453, 342)
        Me.Panel1.TabIndex = 16
        '
        'PictureBox4
        '
        Me.PictureBox4.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox4.BackgroundImage = CType(resources.GetObject("PictureBox4.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox4.Location = New System.Drawing.Point(177, 63)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(89, 89)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox4.TabIndex = 19
        Me.PictureBox4.TabStop = False
        '
        'pnltxtPassword
        '
        Me.pnltxtPassword.BackColor = System.Drawing.Color.White
        Me.pnltxtPassword.Controls.Add(Me.PictureBox2)
        Me.pnltxtPassword.Controls.Add(Me.txtPassword)
        Me.pnltxtPassword.Location = New System.Drawing.Point(66, 221)
        Me.pnltxtPassword.Name = "pnltxtPassword"
        Me.pnltxtPassword.Size = New System.Drawing.Size(310, 26)
        Me.pnltxtPassword.TabIndex = 18
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
        'pnltxtUserName
        '
        Me.pnltxtUserName.BackColor = System.Drawing.Color.White
        Me.pnltxtUserName.Controls.Add(Me.txtUserName)
        Me.pnltxtUserName.Controls.Add(Me.PictureBox3)
        Me.pnltxtUserName.Location = New System.Drawing.Point(66, 186)
        Me.pnltxtUserName.Name = "pnltxtUserName"
        Me.pnltxtUserName.Size = New System.Drawing.Size(310, 26)
        Me.pnltxtUserName.TabIndex = 17
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
        'frmLockScreen
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(915, 702)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLockScreen"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "gloEMR"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnltxtPassword.ResumeLayout(False)
        Me.pnltxtPassword.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnltxtUserName.ResumeLayout(False)
        Me.pnltxtUserName.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private x As Short
    Private Declare Function disable Lib "user32" Alias "SystemParametersInfoA" (ByVal a As Integer, ByVal b As Integer, ByRef c As Boolean, ByVal d As Integer) As Integer
    Private blnClose As Boolean = False
    Private _PreviousLoginName As String
    Private blnIsAdministrator As Boolean
    Private AccessFlag As Boolean

    Public NoofAttempts As Integer = 0

    <DllImport("user32.dll")> _
    Private Shared Function LockWindowUpdate(hWnd As IntPtr) As Boolean
    End Function

    Private Sub frmLockScreen_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        '**************Added by madan on 20100513********************************************************
        '********To identify lock screen is activated****************************************************
        gloEmdeonInterface.Classes.clsEmdeonGeneral.IsLockScreenActivated = False
        '*********End Madan changes**********************************************************************
        gloGlobal.clsFocusToLockScreen.blnSetfocustoUserName = False
        Try
            ' Application.DoEvents()
            Me.Dispose()
        Catch exdispose As Exception

        End Try

    End Sub

    Private Sub frmLockScreen_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If blnClose = False Then
                e.Cancel = True
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''Problem 00000363
    ''Added code to resolve problem of flickring.
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            If (gloSettings.gloRegistrySetting.IsServerOS) Then
                ' Activate double buffering at the form level.  All child controls will be double buffered as well.
                cp.ExStyle = cp.ExStyle Or &H2000000
                ' WS_EX_COMPOSITED
            End If
            Return cp
        End Get
    End Property

    'Private Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer, _
    'ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer, _
    'ByVal dwExtraInfo As Integer)

    Private Sub frmLockScreen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            txtUserName.Focus()
            LockWindowUpdate(Me.Handle)
            Me.SuspendLayout()
            Dim obj As gloUIControlLibrary.WPFUserControl.gloEMRLockScreen = New gloUIControlLibrary.WPFUserControl.gloEMRLockScreen()

            Me.ResumeLayout()

            '********To identify lock screen is activated****************************************************
            gloEmdeonInterface.Classes.clsEmdeonGeneral.IsLockScreenActivated = True

            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.LockScreen, gloAuditTrail.ActivityType.Load, "Lock Screen Load Started", gloAuditTrail.ActivityOutCome.Success)

            x = disable(97, True, False, 0)
            _PreviousLoginName = gstrLoginName

            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.LockScreen, gloAuditTrail.ActivityType.Load, "Lock Screen Load End", gloAuditTrail.ActivityOutCome.Success)

            'Try
            '    '07-Mar-2017 Aniket: Fire the mouse click event once so that the focus is on the lock screen
            '    mouse_event(&H2, 0, 0, 0, 0)
            '    mouse_event(&H4, 0, 0, 0, 0)
            'Catch ex As Exception

            'End Try


        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.LockScreen, gloAuditTrail.ActivityType.Load, "LockScreen Load Exception: " & objErr.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Function CheckLogin() As Boolean

        AccessFlag = False

        'Check Users wants to unlock the screen by Nick Name or not
        If Trim(txtUserName.Text) = "" Then
            MessageBox.Show("User Name must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtPassword.Clear()
            txtUserName.Focus()
            AccessFlag = True
            Return False
        End If
        If Trim(txtPassword.Text) = "" Then
            txtPassword.Clear()
            MessageBox.Show("Password must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtPassword.Focus()
            AccessFlag = True
            Return False
        End If
        'End If

        Dim objLogin As New clsLogin
        If objLogin.IsClientAccess(gstrClientMachineName) = False Then
            MessageBox.Show("This machine does not have rights to access gloEMR system.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objLogin.Dispose()
            objLogin = Nothing
            End
        End If

        Dim objEncryption As New clsencryption
        Dim strPassword As String

        strPassword = objEncryption.EncryptToBase64String(txtPassword.Text, constEncryptDecryptKey)


        objEncryption = Nothing


        Dim blnResetPwdFlag As Boolean = False


        If objLogin.IsValidLogin(txtUserName.Text, strPassword) = False Then

            blnResetPwdFlag = IsPasswordResetted()
            If blnResetPwdFlag = True Then
                MessageBox.Show("Your Password has been reset by the administrator. Please contact the administrator to get the resetted password.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtPassword.Text = ""
                txtPassword.Focus()
                objLogin.Dispose()
                objLogin = Nothing

                Return False
            End If
            MessageBox.Show(gstrUnauthLoginBanner, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.LockScreen, gloAuditTrail.ActivityType.Login, "Invalid Login", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            txtPassword.Clear()
            txtUserName.Focus()
            objLogin.Dispose()
            objLogin = Nothing

            Return False
        End If
        If objLogin.IsAccessPermission(txtUserName.Text) = False Then
            MessageBox.Show("Access Denied." & vbCrLf & "Please contact the administrator.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtUserName.Focus()
            objLogin.Dispose()
            objLogin = Nothing
            AccessFlag = True
            Return False
        End If
        'Check UnLockScreen user is Admin or not
        gblnIsAdmin = objLogin.IsLoginUserAdmin(Trim(txtUserName.Text), True)



        'Added Code for Audit LOG Enhancement
        Dim _isLogin As Boolean = True
        gintLoginSessionID = gloAuditTrail.gloAuditTrail.UpdateRemoteLoginDetails(gstrLoginName, True, gstrClientMachineName, gloAuditTrail.SoftwareComponent.gloEMR.ToString(), gnClinicID, _isLogin)
        objLogin.UpdateLoginStatus(Trim(txtUserName.Text), True, gstrClientMachineName, gintLoginSessionID)
        _isLogin = Nothing


        Dim oDB As New gloStream.gloDataBase.gloDataBase
        oDB.Connect(GetConnectionString)
        gstrLoginName = oDB.ExecuteQueryScaler("select sLoginName from User_MST where sLoginName = '" & txtUserName.Text.Trim.Replace("'", "''") & "'")
        oDB.Disconnect()

        If gstrLoginName = "" Then
            gstrLoginName = Trim(txtUserName.Text)
            Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
            appSettings("UserName") = gstrLoginName
        End If
        gstrLoginPassword = Trim(txtPassword.Text)
        oDB.Dispose()
        oDB = Nothing


        '' Add Login Provider ID to Application config File
        gstrLoginProviderName = ""
        gstrLoginProviderName = objLogin.DefaultLoginProvider(gstrLoginName, gnLoginProviderID)
        gnLoginProviderID = objLogin.GetLoginProviderID(gstrLoginName).ToString()

        objLogin.Dispose()
        objLogin = Nothing

        gstrLoginTime = CType(Format(Date.Now, "Medium Time"), String)

        'Set gloPM global variables [Used in common module] 
        gloGlobal.gloPMGlobal.LoginProviderID = gnLoginProviderID
        gloGlobal.gloPMGlobal.UserID = gnLoginID
        gloGlobal.gloPMGlobal.UserName = Convert.ToString(gstrLoginName)
        gloGlobal.gloPMGlobal.IsAccountsOn = GetPatientAccountFeatureSetting()


        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.LockScreen, gloAuditTrail.ActivityType.Login, "User Unlock the screen", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

        Return True

    End Function

    Private Function IsPasswordResetted() As Boolean
        Dim blnResetPwdFlag As Boolean = False
        Dim _strSQL As String = ""
        Dim oDataReader As SqlDataReader
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection

        Try
            conn.ConnectionString = GetConnectionString()

            _strSQL = "select IsPasswordReset ,sLoginName, nUserID from User_MST where sLoginName = '" & gstrLoginName.Replace("'", "''") & "'"
            cmd = New SqlClient.SqlCommand(_strSQL, conn)
            conn.Open()
            '  blnResetPwdFlag = cmd.ExecuteScalar

            oDataReader = cmd.ExecuteReader

            If Not oDataReader Is Nothing Then
                If oDataReader.HasRows = True Then
                    While oDataReader.Read
                        'the value can be NULL besides 0 and 1 , so chk for null value
                        If Not IsDBNull(oDataReader.Item("IsPasswordReset")) Then
                            'if not nulll then set the value of flag 
                            blnResetPwdFlag = oDataReader.Item("IsPasswordReset")
                        Else
                            'if the value is null then set the flag to false
                            blnResetPwdFlag = False
                        End If
                        '''' 
                        gstrLoginName = oDataReader.Item("sLoginName")
                        ' '' Added By Mahesh To Store the LoginUserID
                        gnLoginID = oDataReader.Item("nUserID")
                    End While
                End If
                oDataReader.Close()
            End If

            Return blnResetPwdFlag

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Dispose()

            End If

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(GetConnectionString)

        Try
            Dim oDataReader As SqlDataReader
            Dim blnResetPwdFlag As Boolean = False
            Dim blnIsAccessDenied As Boolean = False
            Dim _strSQL As String = ""


            ''Modified by Ravi on 29/01/2008 For Case insentiv ecomaraision
            Select Case String.Compare(gstrLoginName, Trim(txtUserName.Text), True)
                Case -1, 1
                    gstrLoginName = Trim(txtUserName.Text)
                    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
                    appSettings("UserName") = gstrLoginName

                    NoofAttempts = 0
                Case Else

            End Select

            conn.Open()
            _strSQL = "select nAccessDenied, sLoginName ,nUserID from User_MST where sLoginName = '" & gstrLoginName.Replace("'", "''") & "'"
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
                        gstrLoginName = CStr(oDataReader.Item("sLoginName")).Trim
                        ' '' Added By Mahesh To Store the LoginUserID
                        gnLoginID = oDataReader.Item("nUserID")
                    End While
                End If
                oDataReader.Close()
            End If

            If blnIsAccessDenied = False Then
                If CheckLogin() = True Then
                    ''''''''<<<<<<><><><><><>>>>>>
                    ''''' If Previous User & Current User are Different then
                    'If _PreviousLoginName <> gstrLoginName Then
                    'Dim objclsExam As New clsPatientExams
                    '    '''' Locked Exams for Previous are Updated By New User (Current User)
                    'objclsExam.Update_UnLock_Exam(gstrLoginName, _PreviousLoginName)
                    '    objclsExam = Nothing
                    'End If
                    ''''''''<<<<<<><><><><><>>>>>>
                    '' Validate Provider License
                    Dim smessage As String = ""
                    smessage = MyBase.ValidateLogin(gnLoginProviderID, GetConnectionString())
                    If Trim(smessage) <> "" Then
                        If MessageBox.Show(smessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                            System.Environment.[Exit](0)
                        End If
                    End If
                    smessage = ""
                    '' Validate Provider License

                    blnResetPwdFlag = IsPasswordResetted()

                    'sarika 11th feb 08
                    If blnResetPwdFlag = True Then
                        Dim frm As New frmChangePassword(gstrLoginName)   ''txtUserName.Text.Trim
                        frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                        frm.Dispose()
                        frm = Nothing
                        txtUserName.Text = ""
                        txtPassword.Text = ""
                        Exit Sub
                        '  MsgBox("Your password has been reset")
                    End If
                    '----------------------------------


                    ' ''To get the Co-Signature right is enabled or not

                    'GetCoSign(gstrLoginName)
                    GetSecurityUser(gstrLoginName)
                    Dim a As Short
                    a = disable(97, False, False, 0)
                    blnClose = True
                    Me.Close()
                Else
                    If AccessFlag = False Then
                        NoofAttempts = NoofAttempts + 1
                        If NoofAttempts >= gintNoOfAttempts Then

                            _strSQL = "select nAdministrator from User_MST where sLoginName = '" & txtUserName.Text.Trim.Replace("'", "''") & "'"
                            If (IsNothing(cmd) = False) Then
                                cmd.Parameters.Clear()
                                cmd.Dispose()
                                cmd = Nothing

                            End If

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
                                '   Me.Hide()
                                ' Dim frmNoAccess As New frmRestrictAccess
                                'sarika Remove Restrict Access form
                                If SetAccessDeniedFlag() = True Then


                                    MsgBox("Access denied, Please contact your administrator.", MsgBoxStyle.Information)
                                    If (IsNothing(cmd) = False) Then
                                        cmd.Parameters.Clear()
                                        cmd.Dispose()
                                        cmd = Nothing

                                    End If
                                    If (IsNothing(conn) = False) Then
                                        conn.Close()
                                        conn.Dispose()
                                        conn = Nothing

                                    End If
                                    End
                                End If

                                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.LockScreen, gloAuditTrail.ActivityType.Login, "Access denied to user", gloAuditTrail.ActivityOutCome.Success)
                                ''Added Rahul P on 20101011
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.LockScreen, gloAuditTrail.ActivityType.Login, "Access denied to user", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                ''
                                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.NodeAuthenticationFailure, "Access denied to user", gstrLoginName, gstrClientMachineName, gnPatientID)
                                'sarika Remove Restrict Access form
                                '   frmNoAccess.ShowDialog(Me)
                                'sarika Remove Restrict Access form

                                End
                            End If
                        End If

                        txtPassword.Text = ""
                        'txtNickName.Text = ""
                    End If
                End If
            Else
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.LockScreen, gloAuditTrail.ActivityType.Login, "Access denied to user", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.LockScreen, gloAuditTrail.ActivityType.Login, "Access denied to user", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.NodeAuthenticationFailure, "Access denied to user", gstrLoginName, gstrClientMachineName, gnPatientID)
                MsgBox("Access denied, Please contact your administrator.", MsgBoxStyle.Information)

                '  Exit Sub
                If (IsNothing(cmd) = False) Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing

                End If
                If (IsNothing(conn) = False) Then
                    conn.Close()
                    conn.Dispose()
                    conn = Nothing

                End If

                End
            End If

        Catch objErr As Exception
            '' MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            MessageBox.Show("Unable to connect to the server.  Please try later.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (IsNothing(cmd) = False) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

            End If
            If (IsNothing(conn) = False) Then
                conn.Close()
                conn.Dispose()
                conn = Nothing

            End If
        End Try
    End Sub

    Public Function SetAccessDeniedFlag() As Boolean


        Dim conn As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim _strSQL As String = ""

        Try
            conn.Open()


            If txtUserName.Text.Trim <> "" Then
                _strSQL = "update User_MST set nAccessDenied = 1 where sLoginName ='" & txtUserName.Text.Trim.Replace("'", "''") & "'"
            Else
                MsgBox("You must enter the Nickname or the Username.", MsgBoxStyle.Information)
            End If

            cmd = New SqlCommand(_strSQL, conn)

            cmd.ExecuteNonQuery()

            Return True

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    Private Function GetPatientAccountFeatureSetting() As Boolean

        Dim result As Object = 0
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Try
            oDB.Connect(False)

            Dim _strSqlQuery As String = "SELECT ISNULL(sSettingValue,'') AS sSettingsValue FROM Settings_Replication where sSettingName='Patient Account Feature'"

            result = oDB.ExecuteScalar_Query(_strSqlQuery)
        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
            End If
        End Try
        If result.ToString().Trim().Length = 0 Then
            result = 0
        End If
        Return Convert.ToBoolean(result)
    End Function

    Private Sub frmLockScreen_Shown(sender As System.Object, e As System.EventArgs) Handles MyBase.Shown
        LockWindowUpdate(IntPtr.Zero)
        gloGlobal.clsFocusToLockScreen.blnSetfocustoUserName = True ''''added for setting focus to lockscreen username control if lock screen is open incident  CAS-05612-H7T4V3
    End Sub

    Private Sub txtUserName_TextChanged(sender As Object, e As System.EventArgs) Handles txtUserName.TextChanged
        gloGlobal.clsFocusToLockScreen.blnSetfocustoUserName = False '' setting focus to  false
    End Sub

    Private Sub txtUserName_Leave(sender As System.Object, e As System.EventArgs) Handles txtUserName.Leave
        gloGlobal.clsFocusToLockScreen.blnSetfocustoUserName = False '' setting focus to  false 
    End Sub
End Class
