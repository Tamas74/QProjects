<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCCDASchedule
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCCDASchedule))
        Me.fbExportLocation = New System.Windows.Forms.FolderBrowserDialog()
        Me.pnl_Base = New System.Windows.Forms.Panel()
        Me.rbServiceConfigured = New System.Windows.Forms.RadioButton()
        Me.rbSystemConfigured = New System.Windows.Forms.RadioButton()
        Me.grpMain = New System.Windows.Forms.GroupBox()
        Me.grprdb = New System.Windows.Forms.GroupBox()
        Me.dt_rdb_tm = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dtp_rdb_encto = New System.Windows.Forms.DateTimePicker()
        Me.dtp_rdb_encfr = New System.Windows.Forms.DateTimePicker()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.num_rdb_dom = New System.Windows.Forms.NumericUpDown()
        Me.grpRecurring = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.GBox_GeneralInfo = New System.Windows.Forms.GroupBox()
        Me.dtRecursGenerateOnTime = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtRecursGenerateOnDate = New System.Windows.Forms.DateTimePicker()
        Me.dtRecursStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.numRecursDays = New System.Windows.Forms.NumericUpDown()
        Me.grpOneTime = New System.Windows.Forms.GroupBox()
        Me.lblEndDate = New System.Windows.Forms.Label()
        Me.dtOneTimeStartDate = New System.Windows.Forms.DateTimePicker()
        Me.grpOneTimeGenerateOn = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtOneTimeGenerateOnTime = New System.Windows.Forms.DateTimePicker()
        Me.dtOneTimeGenerateOnDate = New System.Windows.Forms.DateTimePicker()
        Me.lblStartDate = New System.Windows.Forms.Label()
        Me.dtOneTimeEndDate = New System.Windows.Forms.DateTimePicker()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtExportLocation = New System.Windows.Forms.TextBox()
        Me.cmbScheduleType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.tblMedication = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblSaveCls = New System.Windows.Forms.ToolStripButton()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.pnl_Base.SuspendLayout()
        Me.grpMain.SuspendLayout()
        Me.grprdb.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.num_rdb_dom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpRecurring.SuspendLayout()
        Me.GBox_GeneralInfo.SuspendLayout()
        CType(Me.numRecursDays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpOneTime.SuspendLayout()
        Me.grpOneTimeGenerateOn.SuspendLayout()
        Me.tblMedication.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.rbServiceConfigured)
        Me.pnl_Base.Controls.Add(Me.rbSystemConfigured)
        Me.pnl_Base.Controls.Add(Me.grpMain)
        Me.pnl_Base.Controls.Add(Me.lbl_BottomBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_LeftBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_RightBrd)
        Me.pnl_Base.Controls.Add(Me.lbl_TopBrd)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 53)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl_Base.Size = New System.Drawing.Size(671, 515)
        Me.pnl_Base.TabIndex = 96
        '
        'rbServiceConfigured
        '
        Me.rbServiceConfigured.AutoSize = True
        Me.rbServiceConfigured.Location = New System.Drawing.Point(308, 13)
        Me.rbServiceConfigured.Name = "rbServiceConfigured"
        Me.rbServiceConfigured.Size = New System.Drawing.Size(127, 18)
        Me.rbServiceConfigured.TabIndex = 19
        Me.rbServiceConfigured.TabStop = True
        Me.rbServiceConfigured.Text = "Service Configured"
        Me.rbServiceConfigured.UseVisualStyleBackColor = True
        '
        'rbSystemConfigured
        '
        Me.rbSystemConfigured.AutoSize = True
        Me.rbSystemConfigured.Location = New System.Drawing.Point(164, 13)
        Me.rbSystemConfigured.Name = "rbSystemConfigured"
        Me.rbSystemConfigured.Size = New System.Drawing.Size(128, 18)
        Me.rbSystemConfigured.TabIndex = 18
        Me.rbSystemConfigured.TabStop = True
        Me.rbSystemConfigured.Text = "System Configured"
        Me.rbSystemConfigured.UseVisualStyleBackColor = True
        '
        'grpMain
        '
        Me.grpMain.Controls.Add(Me.grprdb)
        Me.grpMain.Controls.Add(Me.grpRecurring)
        Me.grpMain.Controls.Add(Me.grpOneTime)
        Me.grpMain.Controls.Add(Me.btnBrowse)
        Me.grpMain.Controls.Add(Me.Label8)
        Me.grpMain.Controls.Add(Me.txtExportLocation)
        Me.grpMain.Controls.Add(Me.cmbScheduleType)
        Me.grpMain.Controls.Add(Me.Label1)
        Me.grpMain.Location = New System.Drawing.Point(12, 31)
        Me.grpMain.Name = "grpMain"
        Me.grpMain.Size = New System.Drawing.Size(647, 471)
        Me.grpMain.TabIndex = 14
        Me.grpMain.TabStop = False
        '
        'grprdb
        '
        Me.grprdb.Controls.Add(Me.dt_rdb_tm)
        Me.grprdb.Controls.Add(Me.Label11)
        Me.grprdb.Controls.Add(Me.GroupBox2)
        Me.grprdb.Controls.Add(Me.Label14)
        Me.grprdb.Controls.Add(Me.num_rdb_dom)
        Me.grprdb.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grprdb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grprdb.Location = New System.Drawing.Point(10, 340)
        Me.grprdb.Name = "grprdb"
        Me.grprdb.Size = New System.Drawing.Size(625, 115)
        Me.grprdb.TabIndex = 23
        Me.grprdb.TabStop = False
        Me.grprdb.Text = " Recurring Date"
        '
        'dt_rdb_tm
        '
        Me.dt_rdb_tm.CustomFormat = "hh:mm tt"
        Me.dt_rdb_tm.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dt_rdb_tm.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dt_rdb_tm.Location = New System.Drawing.Point(486, 59)
        Me.dt_rdb_tm.Name = "dt_rdb_tm"
        Me.dt_rdb_tm.ShowUpDown = True
        Me.dt_rdb_tm.Size = New System.Drawing.Size(104, 22)
        Me.dt_rdb_tm.TabIndex = 13
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(440, 64)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(42, 14)
        Me.Label11.TabIndex = 14
        Me.Label11.Text = "Time :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.dtp_rdb_encto)
        Me.GroupBox2.Controls.Add(Me.dtp_rdb_encfr)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(10, 20)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(313, 81)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(30, 52)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(96, 14)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "Encounters To :"
        '
        'dtp_rdb_encto
        '
        Me.dtp_rdb_encto.CustomFormat = "MM/dd/yyyy hh:mm tt"
        Me.dtp_rdb_encto.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_rdb_encto.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_rdb_encto.Location = New System.Drawing.Point(131, 48)
        Me.dtp_rdb_encto.Name = "dtp_rdb_encto"
        Me.dtp_rdb_encto.Size = New System.Drawing.Size(163, 22)
        Me.dtp_rdb_encto.TabIndex = 16
        '
        'dtp_rdb_encfr
        '
        Me.dtp_rdb_encfr.CustomFormat = "MM/dd/yyyy hh:mm tt"
        Me.dtp_rdb_encfr.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtp_rdb_encfr.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtp_rdb_encfr.Location = New System.Drawing.Point(131, 20)
        Me.dtp_rdb_encfr.Name = "dtp_rdb_encfr"
        Me.dtp_rdb_encfr.Size = New System.Drawing.Size(163, 22)
        Me.dtp_rdb_encfr.TabIndex = 13
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(19, 24)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(108, 14)
        Me.Label15.TabIndex = 15
        Me.Label15.Text = "Encounters From :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(330, 35)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(153, 14)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "Export Day of the month :"
        '
        'num_rdb_dom
        '
        Me.num_rdb_dom.Location = New System.Drawing.Point(485, 31)
        Me.num_rdb_dom.Maximum = New Decimal(New Integer() {31, 0, 0, 0})
        Me.num_rdb_dom.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.num_rdb_dom.Name = "num_rdb_dom"
        Me.num_rdb_dom.Size = New System.Drawing.Size(59, 22)
        Me.num_rdb_dom.TabIndex = 1
        Me.num_rdb_dom.Value = New Decimal(New Integer() {31, 0, 0, 0})
        '
        'grpRecurring
        '
        Me.grpRecurring.Controls.Add(Me.Label9)
        Me.grpRecurring.Controls.Add(Me.GBox_GeneralInfo)
        Me.grpRecurring.Controls.Add(Me.dtRecursStartDate)
        Me.grpRecurring.Controls.Add(Me.Label6)
        Me.grpRecurring.Controls.Add(Me.Label5)
        Me.grpRecurring.Controls.Add(Me.numRecursDays)
        Me.grpRecurring.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpRecurring.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grpRecurring.Location = New System.Drawing.Point(10, 216)
        Me.grpRecurring.Name = "grpRecurring"
        Me.grpRecurring.Size = New System.Drawing.Size(625, 115)
        Me.grpRecurring.TabIndex = 21
        Me.grpRecurring.TabStop = False
        Me.grpRecurring.Text = " Recurring"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(374, 44)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(108, 14)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Encounters From :"
        '
        'GBox_GeneralInfo
        '
        Me.GBox_GeneralInfo.Controls.Add(Me.dtRecursGenerateOnTime)
        Me.GBox_GeneralInfo.Controls.Add(Me.Label2)
        Me.GBox_GeneralInfo.Controls.Add(Me.Label7)
        Me.GBox_GeneralInfo.Controls.Add(Me.dtRecursGenerateOnDate)
        Me.GBox_GeneralInfo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBox_GeneralInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GBox_GeneralInfo.Location = New System.Drawing.Point(10, 23)
        Me.GBox_GeneralInfo.Name = "GBox_GeneralInfo"
        Me.GBox_GeneralInfo.Size = New System.Drawing.Size(313, 81)
        Me.GBox_GeneralInfo.TabIndex = 5
        Me.GBox_GeneralInfo.TabStop = False
        Me.GBox_GeneralInfo.Text = "Start On"
        '
        'dtRecursGenerateOnTime
        '
        Me.dtRecursGenerateOnTime.CustomFormat = "hh:mm tt"
        Me.dtRecursGenerateOnTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtRecursGenerateOnTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtRecursGenerateOnTime.Location = New System.Drawing.Point(130, 49)
        Me.dtRecursGenerateOnTime.Name = "dtRecursGenerateOnTime"
        Me.dtRecursGenerateOnTime.ShowUpDown = True
        Me.dtRecursGenerateOnTime.Size = New System.Drawing.Size(163, 22)
        Me.dtRecursGenerateOnTime.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(84, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 14)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Time :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(85, 26)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 14)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Date :"
        '
        'dtRecursGenerateOnDate
        '
        Me.dtRecursGenerateOnDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtRecursGenerateOnDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtRecursGenerateOnDate.Location = New System.Drawing.Point(131, 21)
        Me.dtRecursGenerateOnDate.Name = "dtRecursGenerateOnDate"
        Me.dtRecursGenerateOnDate.Size = New System.Drawing.Size(163, 22)
        Me.dtRecursGenerateOnDate.TabIndex = 9
        '
        'dtRecursStartDate
        '
        Me.dtRecursStartDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtRecursStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtRecursStartDate.Location = New System.Drawing.Point(486, 41)
        Me.dtRecursStartDate.Name = "dtRecursStartDate"
        Me.dtRecursStartDate.Size = New System.Drawing.Size(106, 22)
        Me.dtRecursStartDate.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(549, 76)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 14)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "day(s)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(378, 76)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 14)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Data for the last :"
        '
        'numRecursDays
        '
        Me.numRecursDays.Location = New System.Drawing.Point(485, 72)
        Me.numRecursDays.Maximum = New Decimal(New Integer() {31, 0, 0, 0})
        Me.numRecursDays.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numRecursDays.Name = "numRecursDays"
        Me.numRecursDays.Size = New System.Drawing.Size(59, 22)
        Me.numRecursDays.TabIndex = 1
        Me.numRecursDays.Value = New Decimal(New Integer() {31, 0, 0, 0})
        '
        'grpOneTime
        '
        Me.grpOneTime.Controls.Add(Me.lblEndDate)
        Me.grpOneTime.Controls.Add(Me.dtOneTimeStartDate)
        Me.grpOneTime.Controls.Add(Me.grpOneTimeGenerateOn)
        Me.grpOneTime.Controls.Add(Me.lblStartDate)
        Me.grpOneTime.Controls.Add(Me.dtOneTimeEndDate)
        Me.grpOneTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpOneTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grpOneTime.Location = New System.Drawing.Point(10, 92)
        Me.grpOneTime.Name = "grpOneTime"
        Me.grpOneTime.Size = New System.Drawing.Size(625, 115)
        Me.grpOneTime.TabIndex = 22
        Me.grpOneTime.TabStop = False
        Me.grpOneTime.Text = "One-time occurrence"
        '
        'lblEndDate
        '
        Me.lblEndDate.AutoSize = True
        Me.lblEndDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEndDate.Location = New System.Drawing.Point(40, 69)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(96, 14)
        Me.lblEndDate.TabIndex = 5
        Me.lblEndDate.Text = "Encounters To :"
        '
        'dtOneTimeStartDate
        '
        Me.dtOneTimeStartDate.CustomFormat = "MM/dd/yyyy hh:mm tt"
        Me.dtOneTimeStartDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtOneTimeStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtOneTimeStartDate.Location = New System.Drawing.Point(142, 37)
        Me.dtOneTimeStartDate.Name = "dtOneTimeStartDate"
        Me.dtOneTimeStartDate.Size = New System.Drawing.Size(165, 22)
        Me.dtOneTimeStartDate.TabIndex = 0
        '
        'grpOneTimeGenerateOn
        '
        Me.grpOneTimeGenerateOn.Controls.Add(Me.Label4)
        Me.grpOneTimeGenerateOn.Controls.Add(Me.Label3)
        Me.grpOneTimeGenerateOn.Controls.Add(Me.dtOneTimeGenerateOnTime)
        Me.grpOneTimeGenerateOn.Controls.Add(Me.dtOneTimeGenerateOnDate)
        Me.grpOneTimeGenerateOn.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpOneTimeGenerateOn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grpOneTimeGenerateOn.Location = New System.Drawing.Point(389, 16)
        Me.grpOneTimeGenerateOn.Name = "grpOneTimeGenerateOn"
        Me.grpOneTimeGenerateOn.Size = New System.Drawing.Size(219, 80)
        Me.grpOneTimeGenerateOn.TabIndex = 9
        Me.grpOneTimeGenerateOn.TabStop = False
        Me.grpOneTimeGenerateOn.Text = "Generate On"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(52, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 14)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Time :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(54, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 14)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Date :"
        '
        'dtOneTimeGenerateOnTime
        '
        Me.dtOneTimeGenerateOnTime.CustomFormat = "hh:mm tt"
        Me.dtOneTimeGenerateOnTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtOneTimeGenerateOnTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtOneTimeGenerateOnTime.Location = New System.Drawing.Point(96, 48)
        Me.dtOneTimeGenerateOnTime.Name = "dtOneTimeGenerateOnTime"
        Me.dtOneTimeGenerateOnTime.ShowUpDown = True
        Me.dtOneTimeGenerateOnTime.Size = New System.Drawing.Size(106, 22)
        Me.dtOneTimeGenerateOnTime.TabIndex = 10
        '
        'dtOneTimeGenerateOnDate
        '
        Me.dtOneTimeGenerateOnDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtOneTimeGenerateOnDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtOneTimeGenerateOnDate.Location = New System.Drawing.Point(96, 20)
        Me.dtOneTimeGenerateOnDate.Name = "dtOneTimeGenerateOnDate"
        Me.dtOneTimeGenerateOnDate.Size = New System.Drawing.Size(106, 22)
        Me.dtOneTimeGenerateOnDate.TabIndex = 9
        '
        'lblStartDate
        '
        Me.lblStartDate.AutoSize = True
        Me.lblStartDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStartDate.Location = New System.Drawing.Point(28, 41)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(108, 14)
        Me.lblStartDate.TabIndex = 4
        Me.lblStartDate.Text = "Encounters From :"
        '
        'dtOneTimeEndDate
        '
        Me.dtOneTimeEndDate.CustomFormat = "MM/dd/yyyy hh:mm tt"
        Me.dtOneTimeEndDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtOneTimeEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtOneTimeEndDate.Location = New System.Drawing.Point(141, 65)
        Me.dtOneTimeEndDate.Name = "dtOneTimeEndDate"
        Me.dtOneTimeEndDate.Size = New System.Drawing.Size(165, 22)
        Me.dtOneTimeEndDate.TabIndex = 1
        '
        'btnBrowse
        '
        Me.btnBrowse.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Image = CType(resources.GetObject("btnBrowse.Image"), System.Drawing.Image)
        Me.btnBrowse.Location = New System.Drawing.Point(608, 55)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(24, 24)
        Me.btnBrowse.TabIndex = 20
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(45, 61)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(101, 14)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Export Location :"
        '
        'txtExportLocation
        '
        Me.txtExportLocation.Location = New System.Drawing.Point(150, 57)
        Me.txtExportLocation.Name = "txtExportLocation"
        Me.txtExportLocation.Size = New System.Drawing.Size(452, 22)
        Me.txtExportLocation.TabIndex = 18
        '
        'cmbScheduleType
        '
        Me.cmbScheduleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbScheduleType.FormattingEnabled = True
        Me.cmbScheduleType.Items.AddRange(New Object() {"One Time", "Recurring", "Recurring Date"})
        Me.cmbScheduleType.Location = New System.Drawing.Point(150, 24)
        Me.cmbScheduleType.Name = "cmbScheduleType"
        Me.cmbScheduleType.Size = New System.Drawing.Size(452, 22)
        Me.cmbScheduleType.TabIndex = 16
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(49, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 14)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Schedule Type :"
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 511)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(663, 1)
        Me.lbl_BottomBrd.TabIndex = 4
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 508)
        Me.lbl_LeftBrd.TabIndex = 3
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(667, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 508)
        Me.lbl_RightBrd.TabIndex = 2
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(665, 1)
        Me.lbl_TopBrd.TabIndex = 0
        Me.lbl_TopBrd.Text = "label1"
        '
        'tblMedication
        '
        Me.tblMedication.BackColor = System.Drawing.Color.Transparent
        Me.tblMedication.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblMedication.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblMedication.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblMedication.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblSaveCls, Me.tblClose})
        Me.tblMedication.Location = New System.Drawing.Point(0, 0)
        Me.tblMedication.Name = "tblMedication"
        Me.tblMedication.Size = New System.Drawing.Size(671, 53)
        Me.tblMedication.TabIndex = 1
        Me.tblMedication.Text = "ToolStrip1"
        '
        'tblSaveCls
        '
        Me.tblSaveCls.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSaveCls.Image = CType(resources.GetObject("tblSaveCls.Image"), System.Drawing.Image)
        Me.tblSaveCls.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblSaveCls.Name = "tblSaveCls"
        Me.tblSaveCls.Size = New System.Drawing.Size(66, 50)
        Me.tblSaveCls.Text = "&Save&&Cls"
        Me.tblSaveCls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblSaveCls.ToolTipText = "Save and Close"
        '
        'tblClose
        '
        Me.tblClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblClose.Image = CType(resources.GetObject("tblClose.Image"), System.Drawing.Image)
        Me.tblClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblClose.Name = "tblClose"
        Me.tblClose.Size = New System.Drawing.Size(43, 50)
        Me.tblClose.Text = "&Close"
        Me.tblClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblClose.ToolTipText = "Close"
        '
        'frmCCDASchedule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(671, 568)
        Me.Controls.Add(Me.pnl_Base)
        Me.Controls.Add(Me.tblMedication)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCCDASchedule"
        Me.Text = "CCDA Schedule"
        Me.pnl_Base.ResumeLayout(False)
        Me.pnl_Base.PerformLayout()
        Me.grpMain.ResumeLayout(False)
        Me.grpMain.PerformLayout()
        Me.grprdb.ResumeLayout(False)
        Me.grprdb.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.num_rdb_dom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpRecurring.ResumeLayout(False)
        Me.grpRecurring.PerformLayout()
        Me.GBox_GeneralInfo.ResumeLayout(False)
        Me.GBox_GeneralInfo.PerformLayout()
        CType(Me.numRecursDays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpOneTime.ResumeLayout(False)
        Me.grpOneTime.PerformLayout()
        Me.grpOneTimeGenerateOn.ResumeLayout(False)
        Me.grpOneTimeGenerateOn.PerformLayout()
        Me.tblMedication.ResumeLayout(False)
        Me.tblMedication.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tblMedication As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblSaveCls As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents fbExportLocation As System.Windows.Forms.FolderBrowserDialog
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents rbServiceConfigured As System.Windows.Forms.RadioButton
    Friend WithEvents rbSystemConfigured As System.Windows.Forms.RadioButton
    Friend WithEvents grpMain As System.Windows.Forms.GroupBox
    Friend WithEvents grpRecurring As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GBox_GeneralInfo As System.Windows.Forms.GroupBox
    Friend WithEvents dtRecursGenerateOnTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtRecursGenerateOnDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtRecursStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents numRecursDays As System.Windows.Forms.NumericUpDown
    Friend WithEvents grpOneTime As System.Windows.Forms.GroupBox
    Friend WithEvents lblEndDate As System.Windows.Forms.Label
    Friend WithEvents dtOneTimeStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents grpOneTimeGenerateOn As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtOneTimeGenerateOnTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtOneTimeGenerateOnDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblStartDate As System.Windows.Forms.Label
    Friend WithEvents dtOneTimeEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtExportLocation As System.Windows.Forms.TextBox
    Friend WithEvents cmbScheduleType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grprdb As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dtp_rdb_encfr As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents num_rdb_dom As System.Windows.Forms.NumericUpDown
    Friend WithEvents dt_rdb_tm As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents dtp_rdb_encto As System.Windows.Forms.DateTimePicker
End Class
