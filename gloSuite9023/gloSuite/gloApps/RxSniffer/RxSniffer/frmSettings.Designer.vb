<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettings))
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chkGenrateRxEligLog = New System.Windows.Forms.CheckBox()
        Me.chkBoxEnableEligibility = New System.Windows.Forms.CheckBox()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.rbLogDelete = New System.Windows.Forms.RadioButton()
        Me.rbArchive = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.MaxLogSize = New System.Windows.Forms.NumericUpDown()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnLogFilePath = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtLogFilePath = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.numInterval = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chckLog = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkPDMPAutoDownload = New System.Windows.Forms.CheckBox()
        Me.chkEnableSMDownload = New System.Windows.Forms.CheckBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtSecureMsgSrv = New System.Windows.Forms.TextBox()
        Me.chkPharmacyType = New System.Windows.Forms.CheckBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TxtErxWebService = New System.Windows.Forms.TextBox()
        Me.PnlPharmacyDownload = New System.Windows.Forms.Panel()
        Me.chckFullPrescriberDownload = New System.Windows.Forms.CheckBox()
        Me.cmbFullDay = New System.Windows.Forms.ComboBox()
        Me.chckFullDownload = New System.Windows.Forms.CheckBox()
        Me.cmbPartialTime = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbFullTime = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkEnbPharmacy = New System.Windows.Forms.CheckBox()
        Me.rbProduction = New System.Windows.Forms.RadioButton()
        Me.rbStagging = New System.Windows.Forms.RadioButton()
        Me.gbDatabase = New System.Windows.Forms.GroupBox()
        Me.txtServer = New System.Windows.Forms.TextBox()
        Me.blbServer = New System.Windows.Forms.Label()
        Me.txtDatabase = New System.Windows.Forms.TextBox()
        Me.bllDatabase = New System.Windows.Forms.Label()
        Me.txtUser = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.lblUser = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tlsp_Settings = New System.Windows.Forms.ToolStrip()
        Me.btnUpdate = New System.Windows.Forms.ToolStripButton()
        Me.btnClose = New System.Windows.Forms.ToolStripButton()
        Me.DirectorySearcher1 = New System.DirectoryServices.DirectorySearcher()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.ChkAutoResponseforPendingApportunity = New System.Windows.Forms.CheckBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.btnEpcsLogFilePath = New System.Windows.Forms.Button()
        Me.cmdEpcsFullTime = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtEpcsAuditlogPassword = New System.Windows.Forms.TextBox()
        Me.txtEpcsAuditlogUsername = New System.Windows.Forms.TextBox()
        Me.txtEpcsLogFilePath = New System.Windows.Forms.TextBox()
        Me.chkEnableEpcsLog = New System.Windows.Forms.CheckBox()
        Me.txtPDMPServiceURL = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.pnlMain.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        CType(Me.MaxLogSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.numInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.PnlPharmacyDownload.SuspendLayout()
        Me.gbDatabase.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.tlsp_Settings.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMain.Controls.Add(Me.GroupBox2)
        Me.pnlMain.Controls.Add(Me.GroupBox10)
        Me.pnlMain.Controls.Add(Me.GroupBox3)
        Me.pnlMain.Controls.Add(Me.Label10)
        Me.pnlMain.Controls.Add(Me.Label9)
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Controls.Add(Me.Label7)
        Me.pnlMain.Controls.Add(Me.GroupBox1)
        Me.pnlMain.Controls.Add(Me.gbDatabase)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.Location = New System.Drawing.Point(3, 3)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(525, 637)
        Me.pnlMain.TabIndex = 8
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkGenrateRxEligLog)
        Me.GroupBox2.Controls.Add(Me.chkBoxEnableEligibility)
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(10, 574)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(507, 42)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        '
        'chkGenrateRxEligLog
        '
        Me.chkGenrateRxEligLog.AutoSize = True
        Me.chkGenrateRxEligLog.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGenrateRxEligLog.Location = New System.Drawing.Point(167, 17)
        Me.chkGenrateRxEligLog.Name = "chkGenrateRxEligLog"
        Me.chkGenrateRxEligLog.Size = New System.Drawing.Size(180, 18)
        Me.chkGenrateRxEligLog.TabIndex = 2
        Me.chkGenrateRxEligLog.Text = "Generate Auto Eligibility Log"
        Me.chkGenrateRxEligLog.UseVisualStyleBackColor = True
        '
        'chkBoxEnableEligibility
        '
        Me.chkBoxEnableEligibility.AutoSize = True
        Me.chkBoxEnableEligibility.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBoxEnableEligibility.Location = New System.Drawing.Point(12, 18)
        Me.chkBoxEnableEligibility.Name = "chkBoxEnableEligibility"
        Me.chkBoxEnableEligibility.Size = New System.Drawing.Size(141, 18)
        Me.chkBoxEnableEligibility.TabIndex = 1
        Me.chkBoxEnableEligibility.Text = "Enable Auto Eligibility"
        Me.chkBoxEnableEligibility.UseVisualStyleBackColor = True
        '
        'GroupBox10
        '
        Me.GroupBox10.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox10.Controls.Add(Me.rbLogDelete)
        Me.GroupBox10.Controls.Add(Me.rbArchive)
        Me.GroupBox10.Controls.Add(Me.Label6)
        Me.GroupBox10.Controls.Add(Me.MaxLogSize)
        Me.GroupBox10.Controls.Add(Me.Label11)
        Me.GroupBox10.Controls.Add(Me.btnLogFilePath)
        Me.GroupBox10.Controls.Add(Me.Label5)
        Me.GroupBox10.Controls.Add(Me.txtLogFilePath)
        Me.GroupBox10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox10.Location = New System.Drawing.Point(10, 431)
        Me.GroupBox10.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.GroupBox10.Size = New System.Drawing.Size(507, 109)
        Me.GroupBox10.TabIndex = 3
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Log Maintenance Settings"
        '
        'rbLogDelete
        '
        Me.rbLogDelete.AutoSize = True
        Me.rbLogDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbLogDelete.Location = New System.Drawing.Point(259, 25)
        Me.rbLogDelete.Name = "rbLogDelete"
        Me.rbLogDelete.Size = New System.Drawing.Size(85, 18)
        Me.rbLogDelete.TabIndex = 2
        Me.rbLogDelete.Text = "Log Delete"
        Me.rbLogDelete.UseVisualStyleBackColor = True
        '
        'rbArchive
        '
        Me.rbArchive.AutoSize = True
        Me.rbArchive.Checked = True
        Me.rbArchive.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbArchive.Location = New System.Drawing.Point(147, 25)
        Me.rbArchive.Name = "rbArchive"
        Me.rbArchive.Size = New System.Drawing.Size(89, 18)
        Me.rbArchive.TabIndex = 1
        Me.rbArchive.TabStop = True
        Me.rbArchive.Text = "Log Archive"
        Me.rbArchive.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(198, 53)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(23, 14)
        Me.Label6.TabIndex = 44
        Me.Label6.Text = "MB"
        '
        'MaxLogSize
        '
        Me.MaxLogSize.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaxLogSize.ForeColor = System.Drawing.Color.Black
        Me.MaxLogSize.Location = New System.Drawing.Point(147, 49)
        Me.MaxLogSize.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.MaxLogSize.Name = "MaxLogSize"
        Me.MaxLogSize.Size = New System.Drawing.Size(48, 22)
        Me.MaxLogSize.TabIndex = 3
        Me.MaxLogSize.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(21, 53)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(123, 14)
        Me.Label11.TabIndex = 41
        Me.Label11.Text = "Max size for Log File :"
        '
        'btnLogFilePath
        '
        Me.btnLogFilePath.BackgroundImage = CType(resources.GetObject("btnLogFilePath.BackgroundImage"), System.Drawing.Image)
        Me.btnLogFilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLogFilePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogFilePath.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogFilePath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnLogFilePath.Image = CType(resources.GetObject("btnLogFilePath.Image"), System.Drawing.Image)
        Me.btnLogFilePath.Location = New System.Drawing.Point(471, 80)
        Me.btnLogFilePath.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnLogFilePath.Name = "btnLogFilePath"
        Me.btnLogFilePath.Size = New System.Drawing.Size(21, 21)
        Me.btnLogFilePath.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(15, 84)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(129, 14)
        Me.Label5.TabIndex = 36
        Me.Label5.Text = "Archive Log Location :"
        '
        'txtLogFilePath
        '
        Me.txtLogFilePath.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLogFilePath.Location = New System.Drawing.Point(147, 79)
        Me.txtLogFilePath.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtLogFilePath.Name = "txtLogFilePath"
        Me.txtLogFilePath.Size = New System.Drawing.Size(321, 22)
        Me.txtLogFilePath.TabIndex = 4
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.numInterval)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.chckLog)
        Me.GroupBox3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox3.Location = New System.Drawing.Point(10, 529)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(507, 42)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        '
        'numInterval
        '
        Me.numInterval.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numInterval.Location = New System.Drawing.Point(150, 14)
        Me.numInterval.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
        Me.numInterval.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numInterval.Name = "numInterval"
        Me.numInterval.Size = New System.Drawing.Size(59, 22)
        Me.numInterval.TabIndex = 1
        Me.numInterval.Value = New Decimal(New Integer() {300, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(212, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 14)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "(seconds)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(48, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 14)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Service Interval :"
        '
        'chckLog
        '
        Me.chckLog.AutoSize = True
        Me.chckLog.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckLog.Location = New System.Drawing.Point(279, 17)
        Me.chckLog.Name = "chckLog"
        Me.chckLog.Size = New System.Drawing.Size(100, 18)
        Me.chckLog.TabIndex = 2
        Me.chckLog.Text = "Truncate Log"
        Me.chckLog.UseVisualStyleBackColor = True
        Me.chckLog.Visible = False
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(4, 633)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(517, 1)
        Me.Label10.TabIndex = 27
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(4, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(517, 1)
        Me.Label9.TabIndex = 26
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(521, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 631)
        Me.Label8.TabIndex = 25
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 631)
        Me.Label7.TabIndex = 24
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.txtPDMPServiceURL)
        Me.GroupBox1.Controls.Add(Me.chkPDMPAutoDownload)
        Me.GroupBox1.Controls.Add(Me.chkEnableSMDownload)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtSecureMsgSrv)
        Me.GroupBox1.Controls.Add(Me.chkPharmacyType)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.TxtErxWebService)
        Me.GroupBox1.Controls.Add(Me.PnlPharmacyDownload)
        Me.GroupBox1.Controls.Add(Me.chkEnbPharmacy)
        Me.GroupBox1.Controls.Add(Me.rbProduction)
        Me.GroupBox1.Controls.Add(Me.rbStagging)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(10, 87)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(507, 338)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Directory Service Settings"
        '
        'chkPDMPAutoDownload
        '
        Me.chkPDMPAutoDownload.AutoSize = True
        Me.chkPDMPAutoDownload.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPDMPAutoDownload.Location = New System.Drawing.Point(159, 201)
        Me.chkPDMPAutoDownload.Name = "chkPDMPAutoDownload"
        Me.chkPDMPAutoDownload.Size = New System.Drawing.Size(326, 18)
        Me.chkPDMPAutoDownload.TabIndex = 42
        Me.chkPDMPAutoDownload.Text = "Enable prescription drug monitoring program download"
        Me.chkPDMPAutoDownload.UseVisualStyleBackColor = True
        '
        'chkEnableSMDownload
        '
        Me.chkEnableSMDownload.AutoSize = True
        Me.chkEnableSMDownload.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEnableSMDownload.Location = New System.Drawing.Point(159, 153)
        Me.chkEnableSMDownload.Name = "chkEnableSMDownload"
        Me.chkEnableSMDownload.Size = New System.Drawing.Size(212, 18)
        Me.chkEnableSMDownload.TabIndex = 5
        Me.chkEnableSMDownload.Text = "Enable Secure Message Download"
        Me.chkEnableSMDownload.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(10, 78)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(146, 14)
        Me.Label12.TabIndex = 41
        Me.Label12.Text = "Secure Message Service :"
        '
        'txtSecureMsgSrv
        '
        Me.txtSecureMsgSrv.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSecureMsgSrv.Location = New System.Drawing.Point(159, 73)
        Me.txtSecureMsgSrv.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtSecureMsgSrv.Name = "txtSecureMsgSrv"
        Me.txtSecureMsgSrv.Size = New System.Drawing.Size(310, 22)
        Me.txtSecureMsgSrv.TabIndex = 4
        '
        'chkPharmacyType
        '
        Me.chkPharmacyType.AutoSize = True
        Me.chkPharmacyType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPharmacyType.Location = New System.Drawing.Point(159, 129)
        Me.chkPharmacyType.Name = "chkPharmacyType"
        Me.chkPharmacyType.Size = New System.Drawing.Size(155, 18)
        Me.chkPharmacyType.TabIndex = 6
        Me.chkPharmacyType.Text = "4.5 Directory Download"
        Me.chkPharmacyType.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(48, 47)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(108, 14)
        Me.Label13.TabIndex = 38
        Me.Label13.Text = "eRx Web Service :"
        '
        'TxtErxWebService
        '
        Me.TxtErxWebService.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtErxWebService.Location = New System.Drawing.Point(159, 45)
        Me.TxtErxWebService.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.TxtErxWebService.Name = "TxtErxWebService"
        Me.TxtErxWebService.Size = New System.Drawing.Size(310, 22)
        Me.TxtErxWebService.TabIndex = 3
        '
        'PnlPharmacyDownload
        '
        Me.PnlPharmacyDownload.Controls.Add(Me.chckFullPrescriberDownload)
        Me.PnlPharmacyDownload.Controls.Add(Me.cmbFullDay)
        Me.PnlPharmacyDownload.Controls.Add(Me.chckFullDownload)
        Me.PnlPharmacyDownload.Controls.Add(Me.cmbPartialTime)
        Me.PnlPharmacyDownload.Controls.Add(Me.Label4)
        Me.PnlPharmacyDownload.Controls.Add(Me.cmbFullTime)
        Me.PnlPharmacyDownload.Controls.Add(Me.Label3)
        Me.PnlPharmacyDownload.Location = New System.Drawing.Point(38, 222)
        Me.PnlPharmacyDownload.Name = "PnlPharmacyDownload"
        Me.PnlPharmacyDownload.Size = New System.Drawing.Size(448, 110)
        Me.PnlPharmacyDownload.TabIndex = 8
        '
        'chckFullPrescriberDownload
        '
        Me.chckFullPrescriberDownload.AutoSize = True
        Me.chckFullPrescriberDownload.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckFullPrescriberDownload.Location = New System.Drawing.Point(121, 29)
        Me.chckFullPrescriberDownload.Name = "chckFullPrescriberDownload"
        Me.chckFullPrescriberDownload.Size = New System.Drawing.Size(189, 18)
        Me.chckFullPrescriberDownload.TabIndex = 2
        Me.chckFullPrescriberDownload.Text = "Start Full Prescriber Download"
        Me.chckFullPrescriberDownload.UseVisualStyleBackColor = True
        '
        'cmbFullDay
        '
        Me.cmbFullDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFullDay.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFullDay.FormattingEnabled = True
        Me.cmbFullDay.ItemHeight = 14
        Me.cmbFullDay.Location = New System.Drawing.Point(121, 52)
        Me.cmbFullDay.Name = "cmbFullDay"
        Me.cmbFullDay.Size = New System.Drawing.Size(139, 22)
        Me.cmbFullDay.TabIndex = 3
        '
        'chckFullDownload
        '
        Me.chckFullDownload.AutoSize = True
        Me.chckFullDownload.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chckFullDownload.Location = New System.Drawing.Point(121, 5)
        Me.chckFullDownload.Name = "chckFullDownload"
        Me.chckFullDownload.Size = New System.Drawing.Size(188, 18)
        Me.chckFullDownload.TabIndex = 1
        Me.chckFullDownload.Text = "Start Full Pharmacy Download"
        Me.chckFullDownload.UseVisualStyleBackColor = True
        '
        'cmbPartialTime
        '
        Me.cmbPartialTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPartialTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPartialTime.FormattingEnabled = True
        Me.cmbPartialTime.ItemHeight = 14
        Me.cmbPartialTime.Location = New System.Drawing.Point(121, 80)
        Me.cmbPartialTime.Name = "cmbPartialTime"
        Me.cmbPartialTime.Size = New System.Drawing.Size(139, 22)
        Me.cmbPartialTime.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 84)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(109, 14)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Nightly download :"
        '
        'cmbFullTime
        '
        Me.cmbFullTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFullTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFullTime.FormattingEnabled = True
        Me.cmbFullTime.ItemHeight = 14
        Me.cmbFullTime.Location = New System.Drawing.Point(264, 52)
        Me.cmbFullTime.Name = "cmbFullTime"
        Me.cmbFullTime.Size = New System.Drawing.Size(100, 22)
        Me.cmbFullTime.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(26, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 14)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Full download :"
        '
        'chkEnbPharmacy
        '
        Me.chkEnbPharmacy.AutoSize = True
        Me.chkEnbPharmacy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEnbPharmacy.Location = New System.Drawing.Point(159, 177)
        Me.chkEnbPharmacy.Name = "chkEnbPharmacy"
        Me.chkEnbPharmacy.Size = New System.Drawing.Size(173, 18)
        Me.chkEnbPharmacy.TabIndex = 7
        Me.chkEnbPharmacy.Text = "Enable Directory Download"
        Me.chkEnbPharmacy.UseVisualStyleBackColor = True
        '
        'rbProduction
        '
        Me.rbProduction.AutoSize = True
        Me.rbProduction.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbProduction.Location = New System.Drawing.Point(306, 21)
        Me.rbProduction.Name = "rbProduction"
        Me.rbProduction.Size = New System.Drawing.Size(181, 18)
        Me.rbProduction.TabIndex = 2
        Me.rbProduction.TabStop = True
        Me.rbProduction.Text = "Surescript Production Server"
        Me.rbProduction.UseVisualStyleBackColor = True
        '
        'rbStagging
        '
        Me.rbStagging.AutoSize = True
        Me.rbStagging.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbStagging.Location = New System.Drawing.Point(39, 21)
        Me.rbStagging.Name = "rbStagging"
        Me.rbStagging.Size = New System.Drawing.Size(163, 18)
        Me.rbStagging.TabIndex = 1
        Me.rbStagging.TabStop = True
        Me.rbStagging.Text = "Surescript Staging Server"
        Me.rbStagging.UseVisualStyleBackColor = True
        '
        'gbDatabase
        '
        Me.gbDatabase.Controls.Add(Me.txtServer)
        Me.gbDatabase.Controls.Add(Me.blbServer)
        Me.gbDatabase.Controls.Add(Me.txtDatabase)
        Me.gbDatabase.Controls.Add(Me.bllDatabase)
        Me.gbDatabase.Controls.Add(Me.txtUser)
        Me.gbDatabase.Controls.Add(Me.lblPassword)
        Me.gbDatabase.Controls.Add(Me.lblUser)
        Me.gbDatabase.Controls.Add(Me.txtPassword)
        Me.gbDatabase.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbDatabase.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gbDatabase.Location = New System.Drawing.Point(10, 6)
        Me.gbDatabase.Name = "gbDatabase"
        Me.gbDatabase.Size = New System.Drawing.Size(507, 78)
        Me.gbDatabase.TabIndex = 1
        Me.gbDatabase.TabStop = False
        Me.gbDatabase.Text = "Database Settings"
        '
        'txtServer
        '
        Me.txtServer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServer.Location = New System.Drawing.Point(140, 19)
        Me.txtServer.Name = "txtServer"
        Me.txtServer.Size = New System.Drawing.Size(139, 22)
        Me.txtServer.TabIndex = 1
        '
        'blbServer
        '
        Me.blbServer.AutoSize = True
        Me.blbServer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.blbServer.Location = New System.Drawing.Point(62, 23)
        Me.blbServer.Name = "blbServer"
        Me.blbServer.Size = New System.Drawing.Size(76, 14)
        Me.blbServer.TabIndex = 11
        Me.blbServer.Text = "SQL Server :"
        '
        'txtDatabase
        '
        Me.txtDatabase.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDatabase.Location = New System.Drawing.Point(353, 19)
        Me.txtDatabase.Name = "txtDatabase"
        Me.txtDatabase.Size = New System.Drawing.Size(139, 22)
        Me.txtDatabase.TabIndex = 2
        '
        'bllDatabase
        '
        Me.bllDatabase.AutoSize = True
        Me.bllDatabase.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bllDatabase.Location = New System.Drawing.Point(287, 23)
        Me.bllDatabase.Name = "bllDatabase"
        Me.bllDatabase.Size = New System.Drawing.Size(65, 14)
        Me.bllDatabase.TabIndex = 7
        Me.bllDatabase.Text = "Database :"
        '
        'txtUser
        '
        Me.txtUser.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUser.Location = New System.Drawing.Point(140, 48)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(139, 22)
        Me.txtUser.TabIndex = 3
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPassword.Location = New System.Drawing.Point(286, 52)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(66, 14)
        Me.lblPassword.TabIndex = 4
        Me.lblPassword.Text = "Password :"
        '
        'lblUser
        '
        Me.lblUser.AutoSize = True
        Me.lblUser.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUser.Location = New System.Drawing.Point(73, 52)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(65, 14)
        Me.lblUser.TabIndex = 3
        Me.lblUser.Text = "SQL User :"
        '
        'txtPassword
        '
        Me.txtPassword.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(353, 48)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(139, 22)
        Me.txtPassword.TabIndex = 4
        '
        'pnlBottom
        '
        Me.pnlBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlBottom.Controls.Add(Me.tlsp_Settings)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlBottom.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlBottom.Location = New System.Drawing.Point(0, 0)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(539, 54)
        Me.pnlBottom.TabIndex = 18
        '
        'tlsp_Settings
        '
        Me.tlsp_Settings.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_Settings.BackgroundImage = CType(resources.GetObject("tlsp_Settings.BackgroundImage"), System.Drawing.Image)
        Me.tlsp_Settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_Settings.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_Settings.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_Settings.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnUpdate, Me.btnClose})
        Me.tlsp_Settings.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_Settings.Location = New System.Drawing.Point(0, 0)
        Me.tlsp_Settings.Name = "tlsp_Settings"
        Me.tlsp_Settings.Size = New System.Drawing.Size(539, 53)
        Me.tlsp_Settings.TabIndex = 1
        Me.tlsp_Settings.Text = "toolStrip1"
        '
        'btnUpdate
        '
        Me.btnUpdate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnUpdate.Image = CType(resources.GetObject("btnUpdate.Image"), System.Drawing.Image)
        Me.btnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(66, 50)
        Me.btnUpdate.Tag = "OK"
        Me.btnUpdate.Text = "&Save&&Cls"
        Me.btnUpdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnUpdate.ToolTipText = "Save and Close"
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(43, 50)
        Me.btnClose.Tag = "Cancel"
        Me.btnClose.Text = "&Close"
        Me.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'DirectorySearcher1
        '
        Me.DirectorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01")
        Me.DirectorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01")
        Me.DirectorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01")
        '
        'ChkAutoResponseforPendingApportunity
        '
        Me.ChkAutoResponseforPendingApportunity.AutoSize = True
        Me.ChkAutoResponseforPendingApportunity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAutoResponseforPendingApportunity.Location = New System.Drawing.Point(12, 18)
        Me.ChkAutoResponseforPendingApportunity.Name = "ChkAutoResponseforPendingApportunity"
        Me.ChkAutoResponseforPendingApportunity.Size = New System.Drawing.Size(284, 18)
        Me.ChkAutoResponseforPendingApportunity.TabIndex = 1
        Me.ChkAutoResponseforPendingApportunity.Text = "Enable Auto Send for Unattended Opportunity"
        Me.ChkAutoResponseforPendingApportunity.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.ChkAutoResponseforPendingApportunity)
        Me.GroupBox4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox4.Location = New System.Drawing.Point(20, 697)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(507, 42)
        Me.GroupBox4.TabIndex = 19
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 54)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(539, 670)
        Me.TabControl1.TabIndex = 45
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.pnlMain)
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(531, 643)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.Label18)
        Me.TabPage2.Controls.Add(Me.Label19)
        Me.TabPage2.Controls.Add(Me.Label20)
        Me.TabPage2.Controls.Add(Me.Label21)
        Me.TabPage2.Controls.Add(Me.GroupBox5)
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(531, 677)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "EPCS"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label18.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(4, 673)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(523, 1)
        Me.Label18.TabIndex = 31
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label19.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(4, 3)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(523, 1)
        Me.Label19.TabIndex = 30
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label20.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(527, 3)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 671)
        Me.Label20.TabIndex = 29
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label21.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(3, 3)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(1, 671)
        Me.Label21.TabIndex = 28
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Label23)
        Me.GroupBox5.Controls.Add(Me.Label22)
        Me.GroupBox5.Controls.Add(Me.btnEpcsLogFilePath)
        Me.GroupBox5.Controls.Add(Me.cmdEpcsFullTime)
        Me.GroupBox5.Controls.Add(Me.Label15)
        Me.GroupBox5.Controls.Add(Me.Label17)
        Me.GroupBox5.Controls.Add(Me.Label16)
        Me.GroupBox5.Controls.Add(Me.Label14)
        Me.GroupBox5.Controls.Add(Me.txtEpcsAuditlogPassword)
        Me.GroupBox5.Controls.Add(Me.txtEpcsAuditlogUsername)
        Me.GroupBox5.Controls.Add(Me.txtEpcsLogFilePath)
        Me.GroupBox5.Controls.Add(Me.chkEnableEpcsLog)
        Me.GroupBox5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox5.Location = New System.Drawing.Point(10, 6)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(507, 219)
        Me.GroupBox5.TabIndex = 2
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "EPCS Audit Log Settings"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label23.Location = New System.Drawing.Point(11, 181)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(471, 28)
        Me.Label23.TabIndex = 44
        Me.Label23.Text = "Note: Please enter valid username and password which has full permissions " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     " & _
    "    to this network location."
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(309, 91)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(188, 14)
        Me.Label22.TabIndex = 43
        Me.Label22.Text = "(e.g. domainname\username)"
        '
        'btnEpcsLogFilePath
        '
        Me.btnEpcsLogFilePath.BackgroundImage = CType(resources.GetObject("btnEpcsLogFilePath.BackgroundImage"), System.Drawing.Image)
        Me.btnEpcsLogFilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEpcsLogFilePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEpcsLogFilePath.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEpcsLogFilePath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnEpcsLogFilePath.Image = CType(resources.GetObject("btnEpcsLogFilePath.Image"), System.Drawing.Image)
        Me.btnEpcsLogFilePath.Location = New System.Drawing.Point(422, 147)
        Me.btnEpcsLogFilePath.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnEpcsLogFilePath.Name = "btnEpcsLogFilePath"
        Me.btnEpcsLogFilePath.Size = New System.Drawing.Size(21, 21)
        Me.btnEpcsLogFilePath.TabIndex = 5
        '
        'cmdEpcsFullTime
        '
        Me.cmdEpcsFullTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmdEpcsFullTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEpcsFullTime.FormattingEnabled = True
        Me.cmdEpcsFullTime.ItemHeight = 14
        Me.cmdEpcsFullTime.Location = New System.Drawing.Point(122, 57)
        Me.cmdEpcsFullTime.Name = "cmdEpcsFullTime"
        Me.cmdEpcsFullTime.Size = New System.Drawing.Size(100, 22)
        Me.cmdEpcsFullTime.TabIndex = 1
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(32, 60)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(87, 14)
        Me.Label15.TabIndex = 41
        Me.Label15.Text = "Start Interval :"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(53, 120)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(66, 14)
        Me.Label17.TabIndex = 39
        Me.Label17.Text = "Password :"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(49, 91)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(69, 14)
        Me.Label16.TabIndex = 39
        Me.Label16.Text = "Username :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(57, 150)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(61, 14)
        Me.Label14.TabIndex = 39
        Me.Label14.Text = "Location :"
        '
        'txtEpcsAuditlogPassword
        '
        Me.txtEpcsAuditlogPassword.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEpcsAuditlogPassword.Location = New System.Drawing.Point(122, 117)
        Me.txtEpcsAuditlogPassword.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtEpcsAuditlogPassword.Name = "txtEpcsAuditlogPassword"
        Me.txtEpcsAuditlogPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtEpcsAuditlogPassword.Size = New System.Drawing.Size(180, 22)
        Me.txtEpcsAuditlogPassword.TabIndex = 3
        '
        'txtEpcsAuditlogUsername
        '
        Me.txtEpcsAuditlogUsername.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEpcsAuditlogUsername.Location = New System.Drawing.Point(122, 88)
        Me.txtEpcsAuditlogUsername.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtEpcsAuditlogUsername.Name = "txtEpcsAuditlogUsername"
        Me.txtEpcsAuditlogUsername.Size = New System.Drawing.Size(180, 22)
        Me.txtEpcsAuditlogUsername.TabIndex = 2
        '
        'txtEpcsLogFilePath
        '
        Me.txtEpcsLogFilePath.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEpcsLogFilePath.Location = New System.Drawing.Point(122, 147)
        Me.txtEpcsLogFilePath.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtEpcsLogFilePath.Name = "txtEpcsLogFilePath"
        Me.txtEpcsLogFilePath.Size = New System.Drawing.Size(291, 22)
        Me.txtEpcsLogFilePath.TabIndex = 4
        '
        'chkEnableEpcsLog
        '
        Me.chkEnableEpcsLog.AutoSize = True
        Me.chkEnableEpcsLog.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEnableEpcsLog.Location = New System.Drawing.Point(7, 33)
        Me.chkEnableEpcsLog.Name = "chkEnableEpcsLog"
        Me.chkEnableEpcsLog.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkEnableEpcsLog.Size = New System.Drawing.Size(131, 18)
        Me.chkEnableEpcsLog.TabIndex = 0
        Me.chkEnableEpcsLog.Text = ": Enable Audit Log "
        Me.chkEnableEpcsLog.UseVisualStyleBackColor = True
        '
        'txtPDMPServiceURL
        '
        Me.txtPDMPServiceURL.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPDMPServiceURL.Location = New System.Drawing.Point(159, 101)
        Me.txtPDMPServiceURL.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtPDMPServiceURL.Name = "txtPDMPServiceURL"
        Me.txtPDMPServiceURL.Size = New System.Drawing.Size(310, 22)
        Me.txtPDMPServiceURL.TabIndex = 43
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(63, 104)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(93, 14)
        Me.Label24.TabIndex = 44
        Me.Label24.Text = "PDMP  Service :"
        '
        'frmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(539, 724)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettings"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "RxSniffer - Current Settings"
        Me.pnlMain.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox10.PerformLayout()
        CType(Me.MaxLogSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.numInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.PnlPharmacyDownload.ResumeLayout(False)
        Me.PnlPharmacyDownload.PerformLayout()
        Me.gbDatabase.ResumeLayout(False)
        Me.gbDatabase.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tlsp_Settings.ResumeLayout(False)
        Me.tlsp_Settings.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents numInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents lblUser As System.Windows.Forms.Label
    Friend WithEvents chckFullDownload As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDatabase As System.Windows.Forms.TextBox
    Friend WithEvents bllDatabase As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtServer As System.Windows.Forms.TextBox
    Friend WithEvents blbServer As System.Windows.Forms.Label
    Friend WithEvents gbDatabase As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbFullTime As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFullDay As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPartialTime As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chckLog As System.Windows.Forms.CheckBox
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents rbProduction As System.Windows.Forms.RadioButton
    Friend WithEvents rbStagging As System.Windows.Forms.RadioButton
    Friend WithEvents chkEnbPharmacy As System.Windows.Forms.CheckBox
    Private WithEvents tlsp_Settings As System.Windows.Forms.ToolStrip
    Private WithEvents btnUpdate As System.Windows.Forms.ToolStripButton
    Private WithEvents btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents btnLogFilePath As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtLogFilePath As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents MaxLogSize As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents rbLogDelete As System.Windows.Forms.RadioButton
    Friend WithEvents rbArchive As System.Windows.Forms.RadioButton
    Friend WithEvents PnlPharmacyDownload As System.Windows.Forms.Panel
    Friend WithEvents DirectorySearcher1 As System.DirectoryServices.DirectorySearcher
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkGenrateRxEligLog As System.Windows.Forms.CheckBox
    Friend WithEvents chkBoxEnableEligibility As System.Windows.Forms.CheckBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TxtErxWebService As System.Windows.Forms.TextBox
    Friend WithEvents chkPharmacyType As System.Windows.Forms.CheckBox
    Friend WithEvents chckFullPrescriberDownload As System.Windows.Forms.CheckBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtSecureMsgSrv As System.Windows.Forms.TextBox
    Friend WithEvents chkEnableSMDownload As System.Windows.Forms.CheckBox
    Friend WithEvents ChkAutoResponseforPendingApportunity As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdEpcsFullTime As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents chkEnableEpcsLog As System.Windows.Forms.CheckBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtEpcsAuditlogPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtEpcsAuditlogUsername As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents btnEpcsLogFilePath As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtEpcsLogFilePath As System.Windows.Forms.TextBox
    Friend WithEvents chkPDMPAutoDownload As System.Windows.Forms.CheckBox
    Friend WithEvents txtPDMPServiceURL As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label


End Class
