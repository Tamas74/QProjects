'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************
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
    Friend WithEvents cmbDatabase As System.Windows.Forms.ComboBox
    Friend WithEvents txtDomainName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtServerName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOK As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents grbAuthentication As System.Windows.Forms.GroupBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents rbSQL As System.Windows.Forms.RadioButton
    Friend WithEvents rbWindows As System.Windows.Forms.RadioButton
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbArchiveDatabase As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStartupSettings))
        Me.cmbArchiveDatabase = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtServerName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDomainName = New System.Windows.Forms.TextBox()
        Me.cmbDatabase = New System.Windows.Forms.ComboBox()
        Me.cmbSQLServer = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel()
        Me.tstrip = New System.Windows.Forms.ToolStrip()
        Me.btnOK = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.grbAuthentication = New System.Windows.Forms.GroupBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.rbSQL = New System.Windows.Forms.RadioButton()
        Me.rbWindows = New System.Windows.Forms.RadioButton()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.grbAuthentication.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbArchiveDatabase
        '
        Me.cmbArchiveDatabase.ForeColor = System.Drawing.Color.Black
        Me.cmbArchiveDatabase.Location = New System.Drawing.Point(164, 195)
        Me.cmbArchiveDatabase.Name = "cmbArchiveDatabase"
        Me.cmbArchiveDatabase.Size = New System.Drawing.Size(201, 22)
        Me.cmbArchiveDatabase.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(19, 199)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(144, 14)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "&Archive Database Name :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(78, 267)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 14)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Ser&ver Name :"
        '
        'txtServerName
        '
        Me.txtServerName.ForeColor = System.Drawing.Color.Black
        Me.txtServerName.Location = New System.Drawing.Point(164, 263)
        Me.txtServerName.Name = "txtServerName"
        Me.txtServerName.Size = New System.Drawing.Size(201, 22)
        Me.txtServerName.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(108, 233)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 14)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Do&main :"
        '
        'txtDomainName
        '
        Me.txtDomainName.ForeColor = System.Drawing.Color.Black
        Me.txtDomainName.Location = New System.Drawing.Point(164, 229)
        Me.txtDomainName.Name = "txtDomainName"
        Me.txtDomainName.Size = New System.Drawing.Size(201, 22)
        Me.txtDomainName.TabIndex = 6
        '
        'cmbDatabase
        '
        Me.cmbDatabase.ForeColor = System.Drawing.Color.Black
        Me.cmbDatabase.Location = New System.Drawing.Point(164, 162)
        Me.cmbDatabase.Name = "cmbDatabase"
        Me.cmbDatabase.Size = New System.Drawing.Size(201, 22)
        Me.cmbDatabase.TabIndex = 4
        '
        'cmbSQLServer
        '
        Me.cmbSQLServer.ForeColor = System.Drawing.Color.Black
        Me.cmbSQLServer.Location = New System.Drawing.Point(164, 13)
        Me.cmbSQLServer.Name = "cmbSQLServer"
        Me.cmbSQLServer.Size = New System.Drawing.Size(201, 22)
        Me.cmbSQLServer.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(63, 165)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "&Database Name :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(52, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "&SQL Server Name :"
        '
        'pnl_tlsp_Top
        '
        Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp_Top.Controls.Add(Me.tstrip)
        Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(401, 56)
        Me.pnl_tlsp_Top.TabIndex = 18
        '
        'tstrip
        '
        Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tstrip.BackgroundImage = CType(resources.GetObject("tstrip.BackgroundImage"), System.Drawing.Image)
        Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOK})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(401, 53)
        Me.tstrip.TabIndex = 0
        Me.tstrip.Text = "ToolStrip1"
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(66, 50)
        Me.btnOK.Text = "&Save&&Cls"
        Me.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOK.ToolTipText = "Save and Close"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.grbAuthentication)
        Me.Panel1.Controls.Add(Me.rbSQL)
        Me.Panel1.Controls.Add(Me.rbWindows)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.cmbArchiveDatabase)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtDomainName)
        Me.Panel1.Controls.Add(Me.cmbDatabase)
        Me.Panel1.Controls.Add(Me.cmbSQLServer)
        Me.Panel1.Controls.Add(Me.txtServerName)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 56)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(401, 302)
        Me.Panel1.TabIndex = 19
        '
        'grbAuthentication
        '
        Me.grbAuthentication.Controls.Add(Me.txtPassword)
        Me.grbAuthentication.Controls.Add(Me.txtUserName)
        Me.grbAuthentication.Controls.Add(Me.Label10)
        Me.grbAuthentication.Controls.Add(Me.Label11)
        Me.grbAuthentication.Location = New System.Drawing.Point(47, 67)
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
        Me.txtPassword.TabIndex = 2
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(119, 17)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(192, 22)
        Me.txtUserName.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(50, 54)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(66, 14)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "Password :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(42, 21)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(74, 14)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "User Name :"
        '
        'rbSQL
        '
        Me.rbSQL.AutoSize = True
        Me.rbSQL.Location = New System.Drawing.Point(265, 45)
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
        Me.rbWindows.Location = New System.Drawing.Point(169, 45)
        Me.rbWindows.Name = "rbWindows"
        Me.rbWindows.Size = New System.Drawing.Size(75, 18)
        Me.rbWindows.TabIndex = 1
        Me.rbWindows.TabStop = True
        Me.rbWindows.Text = "Windows"
        Me.rbWindows.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(67, 47)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(96, 14)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "Authentication :"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(4, 298)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(393, 1)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "label2"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 295)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(397, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 295)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(395, 1)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "label1"
        '
        'frmStartupSettings
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(401, 358)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmStartupSettings"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "gloPM Startup Settings"
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grbAuthentication.ResumeLayout(False)
        Me.grbAuthentication.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim blnButtonClick As Boolean = False
    Public blnOpenFromMainForm As Boolean = False
    Private Sub frmStartupSettings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ''  Call Fill_SQLServers()
            cmbSQLServer.Text = gstrSQLServerName
            cmbDatabase.Text = gstrDatabaseName
            cmbArchiveDatabase.Text = gstrArchiveDatabaseName
            txtDomainName.Text = gstrDomainName
            txtServerName.Text = gstrWindowsServerName

            If gblnSQLAuthentication Then
                rbSQL.Checked = True
            Else
                rbWindows.Checked = True
            End If

            txtUserName.Text = gstrSQLUserEMR
            txtPassword.Text = gstrSQLPasswordEMR
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, "gloPM Admin", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Fill_SQLServers()
        With cmbSQLServer
            .Items.Clear()
            Dim clSQLServers As New Collection
            Dim objSettings As New clsStartUpSettings
            clSQLServers = objSettings.Fill_RegisteredSQLServers
            Dim nCount As Int16
            For nCount = 1 To clSQLServers.Count
                .Items.Add(clSQLServers.Item(nCount))
            Next

        End With
    End Sub


    Private Sub cmbSQLServer_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSQLServer.Leave
        Try
            If Trim(cmbSQLServer.Text) = "" Then
                MessageBox.Show("Please select SQL Server Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbSQLServer.Focus()
                Exit Sub
            End If
            'Me.Cursor = Cursors.WaitCursor
            'Dim clDatabases As New Collection
            'Dim objSettings As New clsStartUpSettings
            'clDatabases = objSettings.Fill_SQLDatabases(Trim(cmbSQLServer.Text))
            'Dim nCount As Int16
            'With cmbDatabase
            '    .Items.Clear()
            '    cmbArchiveDatabase.Items.Clear()
            '    For nCount = 1 To clDatabases.Count
            '        .Items.Add(clDatabases.Item(nCount))
            '        cmbArchiveDatabase.Items.Add(clDatabases.Item(nCount))
            '    Next
            'End With
            'Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, "gloPM Admin", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            If Trim(cmbSQLServer.Text) = "" Then
                ' MsgBox("Please select SQL Server Name") ', gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                MessageBox.Show("Please select SQL Server Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
            If Trim(txtDomainName.Text) = "" Then
                MessageBox.Show("Please enter Domain Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDomainName.Focus()
                Exit Sub
            End If
            If Trim(txtServerName.Text) = "" Then
                MessageBox.Show("Please enter Windows Domain server Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtServerName.Focus()
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim objSettings As New clsStartUpSettings
            'Checking SQL Server Name and Database Name
            Dim _Error As String = ""
            If objSettings.IsConnect(Trim(cmbSQLServer.Text), Trim(cmbDatabase.Text), rbSQL.Checked, txtUserName.Text.Trim, txtPassword.Text, _Error) = False Then
                'MessageBox.Show("Please select valid SQL Server Name/Database", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                MessageBox.Show(_Error, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbSQLServer.Focus()
                objSettings = Nothing
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            'Checking SQL Server Name and Archive Database Name
            _Error = ""
            If objSettings.IsConnect(Trim(cmbSQLServer.Text), Trim(cmbArchiveDatabase.Text), rbSQL.Checked, txtUserName.Text.Trim, txtPassword.Text, _Error) = False Then
                'MessageBox.Show("Please select valid SQL Server Name/ Archived Database", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                MessageBox.Show(_Error, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbArchiveDatabase.Focus()
                objSettings = Nothing
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            objSettings = Nothing

            gstrAdminFor = "gloPM"
            Try
                Dim filepath As String = Application.StartupPath + "\admin.ini"
                Dim sr As New IO.StreamReader(filepath)
                Dim str As String = sr.ReadLine()

                If Not IsNothing(str) And str <> "" Then
                    gstrAdminFor = str
                End If
            Catch ex As Exception
                gstrAdminFor = "gloPM"
            End Try

            Dim regKey As RegistryKey

            If (gstrAdminFor = "gloEMR") Then
                gstrSQLServerName = Trim(cmbSQLServer.Text)
                gstrDatabaseName = Trim(cmbDatabase.Text)
                gstrArchiveDatabaseName = Trim(cmbArchiveDatabase.Text)
                gstrDomainName = Trim(txtDomainName.Text)
                gstrWindowsServerName = Trim(txtServerName.Text)
                gblnSQLAuthentication = rbSQL.Checked
                gstrSQLUserEMR = txtUserName.Text.Trim
                gstrSQLPasswordEMR = txtPassword.Text


                'Bug #39752: 00000312 : EMR Settings - Hosting Item : Reading and Wrinting a Registry from HKEY_CURRENT_USER
                If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) = False Then
                    gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoft, True)
                    gloRegistrySetting.CreateSubKey(gloRegistrySetting.gstrEMR)
                    gloRegistrySetting.CloseRegistryKey()
                End If

                gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
                gloRegistrySetting.SetRegistryValue("SQLServer", gstrSQLServerName)
                gloRegistrySetting.SetRegistryValue("Database", gstrDatabaseName)
                gloRegistrySetting.SetRegistryValue("ArchiveDatabase", gstrArchiveDatabaseName)
                gloRegistrySetting.SetRegistryValue("Domain", gstrDomainName)
                gloRegistrySetting.SetRegistryValue("WindowsServer", gstrWindowsServerName)
                gloRegistrySetting.SetRegistryValue("IsSQLAuthentication", gblnSQLAuthentication)
                gloRegistrySetting.SetRegistryValue("SQLUserEMR", gstrSQLUserEMR)
                If gstrSQLPasswordEMR <> "" Then
                    Dim oEncryption As New clsEncryption
                    gloRegistrySetting.SetRegistryValue("SQLPasswordEMR", oEncryption.EncryptToBase64String(gstrSQLPasswordEMR, constEncryptDecryptKey_Services))
                    oEncryption = Nothing
                End If
                ''Sandip Darade 
                ''Set values for global variables and registry for gloPM Admin
            Else ''gloPM Admin
                gstrSQLServerName = Trim(cmbSQLServer.Text)
                gstrDatabaseName = Trim(cmbDatabase.Text)
                gstrArchiveDatabaseName = Trim(cmbArchiveDatabase.Text)
                gstrDomainName = Trim(txtDomainName.Text)
                gstrWindowsServerName = Trim(txtServerName.Text)
                gblnSQLAuthentication = rbSQL.Checked
                If (gblnSQLAuthentication = True) Then
                    gblnWindowsAuthentication = False
                Else
                    gblnWindowsAuthentication = True
                End If
                gstrSQLUser = txtUserName.Text.Trim
                'gstrSQLUserEMR is used everywhere in the project.
                ' but that will not set at the time of startupSettings save.
                gstrSQLUserEMR = gstrSQLUser
                gstrSQLPassword = txtPassword.Text
                gstrSQLPasswordEMR = txtPassword.Text

                'Bug #39752: 00000312 : EMR Settings - Hosting Item : Reading and Wrinting a Registry from HKEY_CURRENT_USER
                If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM) = False Then
                    gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoft, True)
                    gloRegistrySetting.CreateSubKey(gloRegistrySetting.gstrSoftPM)
                    gloRegistrySetting.CloseRegistryKey()
                End If

                gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftPM, True)
                gloRegistrySetting.SetRegistryValue("SQLServer", gstrSQLServerName)
                gloRegistrySetting.SetRegistryValue("Database", gstrDatabaseName)
                gloRegistrySetting.SetRegistryValue("ISWINAUTHENTICATION", gblnWindowsAuthentication)
                gloRegistrySetting.SetRegistryValue("SQLUSERNAME", gstrSQLUser)
                If gstrSQLPassword <> "" Then
                    Dim oEncryption As New clsEncryption
                    gloRegistrySetting.SetRegistryValue("SQLPASSWORD", oEncryption.EncryptToBase64String(gstrSQLPassword, constEncryptDecryptKey_Services))
                    oEncryption = Nothing
                End If
            End If
            gloRegistrySetting.CloseRegistryKey()

            'btnOK.DialogResult = DialogResult.OK
            blnButtonClick = True

            'sarika  27th apr 2007
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Other, "User has set the Database settings.", gstrLoginName, gstrClientMachineName)
            objAudit = Nothing
            '-------------

            Me.Cursor = Cursors.Default
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'sarika 27th feb 2007
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Other, "Error occured while setting the Database settings.", gstrLoginName, gstrClientMachineName, , , clsAudit.enmOutcome.Failure)
            objAudit = Nothing
            '-------------
        End Try

    End Sub

    Private Sub frmStartupSettings_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            If blnButtonClick = False And blnOpenFromMainForm = False Then
                'Changed the message as per Bug #69957: 00000716: gloPM Startup Message and also changed 'Yes'/'No' logic.
                If MessageBox.Show("You can not log in to gloPM System without configuring startup settings." & vbCrLf & "Are you sure you want to exit?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                    e.Cancel = True
                Else
                    End
                End If
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, "gloPM Admin", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub cmbSQLServer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSQLServer.SelectedIndexChanged

    End Sub

    Private Sub tstrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tstrip.ItemClicked

    End Sub

    Private Sub rbSQL_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbSQL.CheckedChanged
        If rbSQL.Checked Then
            rbSQL.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbSQL.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbWindows_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbWindows.CheckedChanged
        If rbWindows.Checked Then
            rbWindows.Font = New Font("Tahoma", 9, FontStyle.Bold)
            grbAuthentication.Enabled = False
        Else
            rbWindows.Font = New Font("Tahoma", 9, FontStyle.Regular)
            grbAuthentication.Enabled = True
        End If
    End Sub

End Class
