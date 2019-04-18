Imports Microsoft.Win32
Imports gloSettings
Public Class frmStartupSettings
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
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbSQLServer As System.Windows.Forms.ComboBox
    Private WithEvents pnl_tlspTOP As System.Windows.Forms.Panel
    Private WithEvents tlsp_StartupSettings As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnOk As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents grbAuthentication As System.Windows.Forms.GroupBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents rbSQL As System.Windows.Forms.RadioButton
    Friend WithEvents rbWindows As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbDatabase As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStartupSettings))
        Me.cmbDatabase = New System.Windows.Forms.ComboBox
        Me.cmbSQLServer = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnl_tlspTOP = New System.Windows.Forms.Panel
        Me.tlsp_StartupSettings = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnOk = New System.Windows.Forms.ToolStripButton
        Me.ts_btnCancel = New System.Windows.Forms.ToolStripButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.grbAuthentication = New System.Windows.Forms.GroupBox
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.rbSQL = New System.Windows.Forms.RadioButton
        Me.rbWindows = New System.Windows.Forms.RadioButton
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.pnl_tlspTOP.SuspendLayout()
        Me.tlsp_StartupSettings.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.grbAuthentication.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbDatabase
        '
        Me.cmbDatabase.ForeColor = System.Drawing.Color.Black
        Me.cmbDatabase.Location = New System.Drawing.Point(131, 163)
        Me.cmbDatabase.Name = "cmbDatabase"
        Me.cmbDatabase.Size = New System.Drawing.Size(192, 22)
        Me.cmbDatabase.TabIndex = 4
        '
        'cmbSQLServer
        '
        Me.cmbSQLServer.ForeColor = System.Drawing.Color.Black
        Me.cmbSQLServer.Location = New System.Drawing.Point(131, 17)
        Me.cmbSQLServer.Name = "cmbSQLServer"
        Me.cmbSQLServer.Size = New System.Drawing.Size(192, 22)
        Me.cmbSQLServer.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(28, 167)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Database Name :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(17, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "SQL Server Name :"
        '
        'pnl_tlspTOP
        '
        Me.pnl_tlspTOP.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tlspTOP.Controls.Add(Me.tlsp_StartupSettings)
        Me.pnl_tlspTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlspTOP.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.pnl_tlspTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlspTOP.Name = "pnl_tlspTOP"
        Me.pnl_tlspTOP.Size = New System.Drawing.Size(345, 54)
        Me.pnl_tlspTOP.TabIndex = 4
        '
        'tlsp_StartupSettings
        '
        Me.tlsp_StartupSettings.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_StartupSettings.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_StartupSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_StartupSettings.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_StartupSettings.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_StartupSettings.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOk, Me.ts_btnCancel})
        Me.tlsp_StartupSettings.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_StartupSettings.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_StartupSettings.Name = "tlsp_StartupSettings"
        Me.tlsp_StartupSettings.Size = New System.Drawing.Size(345, 53)
        Me.tlsp_StartupSettings.TabIndex = 0
        Me.tlsp_StartupSettings.Text = "toolStrip1"
        '
        'ts_btnOk
        '
        Me.ts_btnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOk.Image = CType(resources.GetObject("ts_btnOk.Image"), System.Drawing.Image)
        Me.ts_btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOk.Name = "ts_btnOk"
        Me.ts_btnOk.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnOk.Tag = "OK"
        Me.ts_btnOk.Text = "&Save&&Cls"
        Me.ts_btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnOk.ToolTipText = "Save and Close"
        '
        'ts_btnCancel
        '
        Me.ts_btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnCancel.Image = CType(resources.GetObject("ts_btnCancel.Image"), System.Drawing.Image)
        Me.ts_btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCancel.Name = "ts_btnCancel"
        Me.ts_btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnCancel.Tag = "Cancel"
        Me.ts_btnCancel.Text = "&Close"
        Me.ts_btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.grbAuthentication)
        Me.Panel1.Controls.Add(Me.rbSQL)
        Me.Panel1.Controls.Add(Me.rbWindows)
        Me.Panel1.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel1.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel1.Controls.Add(Me.cmbDatabase)
        Me.Panel1.Controls.Add(Me.lbl_RightBrd)
        Me.Panel1.Controls.Add(Me.cmbSQLServer)
        Me.Panel1.Controls.Add(Me.lbl_TopBrd)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(345, 202)
        Me.Panel1.TabIndex = 0
        '
        'grbAuthentication
        '
        Me.grbAuthentication.Controls.Add(Me.txtPassword)
        Me.grbAuthentication.Controls.Add(Me.txtUserName)
        Me.grbAuthentication.Controls.Add(Me.Label5)
        Me.grbAuthentication.Controls.Add(Me.Label4)
        Me.grbAuthentication.Location = New System.Drawing.Point(12, 73)
        Me.grbAuthentication.Name = "grbAuthentication"
        Me.grbAuthentication.Size = New System.Drawing.Size(321, 82)
        Me.grbAuthentication.TabIndex = 3
        Me.grbAuthentication.TabStop = False
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(119, 50)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(192, 22)
        Me.txtPassword.TabIndex = 1
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(119, 17)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(192, 22)
        Me.txtUserName.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(50, 54)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 14)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Password :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(42, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 14)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "User Name :"
        '
        'rbSQL
        '
        Me.rbSQL.AutoSize = True
        Me.rbSQL.Location = New System.Drawing.Point(230, 49)
        Me.rbSQL.Name = "rbSQL"
        Me.rbSQL.Size = New System.Drawing.Size(47, 18)
        Me.rbSQL.TabIndex = 2
        Me.rbSQL.TabStop = True
        Me.rbSQL.Text = "SQL"
        Me.rbSQL.UseVisualStyleBackColor = True
        '
        'rbWindows
        '
        Me.rbWindows.AutoSize = True
        Me.rbWindows.Location = New System.Drawing.Point(134, 49)
        Me.rbWindows.Name = "rbWindows"
        Me.rbWindows.Size = New System.Drawing.Size(75, 18)
        Me.rbWindows.TabIndex = 1
        Me.rbWindows.TabStop = True
        Me.rbWindows.Text = "Windows"
        Me.rbWindows.UseVisualStyleBackColor = True
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 198)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(337, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 195)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(341, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 195)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(339, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(32, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 14)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Authentication :"
        '
        'frmStartupSettings
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(345, 256)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnl_tlspTOP)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmStartupSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "gloEMR Startup Settings"
        Me.pnl_tlspTOP.ResumeLayout(False)
        Me.pnl_tlspTOP.PerformLayout()
        Me.tlsp_StartupSettings.ResumeLayout(False)
        Me.tlsp_StartupSettings.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grbAuthentication.ResumeLayout(False)
        Me.grbAuthentication.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim blnButtonClick As Boolean = False
    Private Sub frmStartupSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Call Fill_SQLServers()
            cmbSQLServer.Text = gstrSQLServerName
            cmbDatabase.Text = gstrDatabaseName

            If gblnSQLAuthentication Then
                rbSQL.Checked = True
            Else
                rbWindows.Checked = True
            End If

            txtUserName.Text = gstrSQLUserEMR
            txtPassword.Text = gstrSQLPasswordEMR

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'Private Sub Fill_SQLServers()
    '    With cmbSQLServer
    '        .Items.Clear()
    '        Dim clSQLServers As New Collection
    '        Dim objSettings As New clsStartUpSettings
    '        clSQLServers = objSettings.Fill_RegisteredSQLServers
    '        Dim nCount As Int16
    '        For nCount = 1 To clSQLServers.Count
    '            .Items.Add(clSQLServers.Item(nCount))
    '        Next

    '    End With
    'End Sub
    'Private Sub cmbSQLServer_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSQLServer.Leave
    '    If Trim(cmbSQLServer.Text) = "" Then
    '        MessageBox.Show("Please select SQL Server Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        cmbSQLServer.Focus()
    '        Exit Sub
    '    End If
    '    Dim clDatabases As New Collection
    '    Dim objSettings As New clsStartUpSettings
    '    clDatabases = objSettings.Fill_SQLDatabases(Trim(cmbSQLServer.Text))
    '    Dim nCount As Int16
    '    With cmbDatabase
    '        .Items.Clear()
    '        For nCount = 1 To clDatabases.Count
    '            .Items.Add(clDatabases.Item(nCount))
    '        Next

    '    End With

    'End Sub

    Private Sub OKStartupSettings()
        Try
            If Trim(cmbSQLServer.Text) = "" Then
                MsgBox("Please select SQL Server Name") ', gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbSQLServer.Focus()
                Exit Sub
            End If
            If Trim(cmbDatabase.Text) = "" Then
                MessageBox.Show("Please select SQL Database Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbDatabase.Focus()
                Exit Sub
            End If
            If rbSQL.Checked Then
                If txtUserName.Text.Trim = "" Then
                    MessageBox.Show("Please enter SQL User Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtUserName.Focus()
                    Exit Sub
                End If
                If txtPassword.Text = "" Then
                    MessageBox.Show("Please enter SQL Password", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtPassword.Focus()
                    Exit Sub
                End If
            End If
            '  Dim objSettings As New clsStartUpSettings
            Dim _Error As String = ""
            If clsStartUpSettings.IsConnect(Trim(cmbSQLServer.Text), Trim(cmbDatabase.Text), rbSQL.Checked, txtUserName.Text.Trim, txtPassword.Text, _Error) = False Then
                'MessageBox.Show("Please select valid SQL Server Name/Database", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                MessageBox.Show(_Error, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbSQLServer.Focus()
                '  objSettings = Nothing
                Exit Sub
            End If
            '   objSettings = Nothing

            'Dim regKey As RegistryKey
            'If IsNothing(Registry.LocalMachine.OpenSubKey("Software\gloEMR")) = True Then
            '    regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", True)
            '    regKey.CreateSubKey("gloEMR")
            '    regKey.Close()
            'End If
            If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) = False Then
                gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoft, True)
                gloRegistrySetting.CreateSubKey(gloRegistrySetting.gstrEMR)
                gloRegistrySetting.CloseRegistryKey()
            End If

            gstrSQLServerName = Trim(cmbSQLServer.Text)
            gstrDatabaseName = Trim(cmbDatabase.Text)
            gblnSQLAuthentication = rbSQL.Checked
            gstrSQLUserEMR = txtUserName.Text.Trim
            gstrSQLPasswordEMR = txtPassword.Text

            'regKey = Registry.LocalMachine.OpenSubKey("Software\gloEMR", True)
            'regKey.SetValue("SQLServer", gstrSQLServerName)
            'regKey.SetValue("Database", gstrDatabaseName)
            'regKey.SetValue("IsSQLAuthentication", gblnSQLAuthentication)
            'regKey.SetValue("SQLUserEMR", gstrSQLUserEMR)
            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
            gloRegistrySetting.SetRegistryValue("SQLServer", gstrSQLServerName)
            gloRegistrySetting.SetRegistryValue("Database", gstrDatabaseName)
            gloRegistrySetting.SetRegistryValue("IsSQLAuthentication", gblnSQLAuthentication)
            gloRegistrySetting.SetRegistryValue("SQLUserEMR", gstrSQLUserEMR)

            If gstrSQLPasswordEMR <> "" Then
                Dim oEncryption As New clsencryption
                'regKey.SetValue("SQLPasswordEMR", oEncryption.EncryptToBase64String(gstrSQLPasswordEMR, constEncryptDecryptKey))
                gloRegistrySetting.SetRegistryValue("SQLPasswordEMR", oEncryption.EncryptToBase64String(gstrSQLPasswordEMR, constEncryptDecryptKey))
                oEncryption = Nothing
            End If

            'regKey.Close()
            gloRegistrySetting.CloseRegistryKey()
            Me.DialogResult = DialogResult.OK
            blnButtonClick = True
            Me.Close()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frmStartupSettings_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            If blnButtonClick = False Then
                'Changed the message as per Bug #69957: 00000716: gloEMR Startup Message and also changed 'Yes'/'No' logic.
                If MessageBox.Show("You can not log in to gloEMR Sytem without configuring startup settings." & vbCrLf & "Are you sure you want to exit?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                    e.Cancel = True
                Else
                    End
                End If
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlsp_StartupSettings_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_StartupSettings.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "OK"
                    OKStartupSettings()
                Case "Cancel"
                    Me.Close()

            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rbSQL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbSQL.CheckedChanged
        If rbSQL.Checked Then
            rbSQL.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbSQL.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbWindows_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbWindows.CheckedChanged
        If rbWindows.Checked Then
            rbWindows.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            grbAuthentication.Enabled = False
        Else
            rbWindows.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
            grbAuthentication.Enabled = True
        End If
    End Sub
End Class
