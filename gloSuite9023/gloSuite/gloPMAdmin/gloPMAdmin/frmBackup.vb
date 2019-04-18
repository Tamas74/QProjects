'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************
Imports SQLDMO

Public Class frmBackup
    Inherits System.Windows.Forms.Form
    

    Dim WithEvents objBackup As New Backup

    Dim _sSQLServerName As String
    Dim _sSQLDataBaseName As String


    Public Property SQLServerName() As String
        Get
            Return _sSQLServerName
        End Get
        Set(ByVal Value As String)
            _sSQLServerName = Value
        End Set
    End Property

    Public Property SQLDataBaseName() As String
        Get
            Return _sSQLDataBaseName
        End Get
        Set(ByVal Value As String)
            _sSQLDataBaseName = Value
        End Set
    End Property

    Public blnModify As Boolean
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents grpDaily As System.Windows.Forms.GroupBox
    Friend WithEvents chkEveryDay As System.Windows.Forms.CheckBox
    Friend WithEvents lblMonth As System.Windows.Forms.Label
    Friend WithEvents numMonth As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkSun As System.Windows.Forms.CheckBox
    Friend WithEvents chkSat As System.Windows.Forms.CheckBox
    Friend WithEvents chkFri As System.Windows.Forms.CheckBox
    Friend WithEvents chkThu As System.Windows.Forms.CheckBox
    Friend WithEvents chkWed As System.Windows.Forms.CheckBox
    Friend WithEvents chkTue As System.Windows.Forms.CheckBox
    Friend WithEvents chkMon As System.Windows.Forms.CheckBox
    Friend WithEvents lblJobOccursValue As System.Windows.Forms.Label
    Friend WithEvents numFrequency As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblJobOccursHead As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtScheduleName As System.Windows.Forms.TextBox
    Friend WithEvents dtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents tmScheduleTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents optNoEndDate As System.Windows.Forms.RadioButton
    Friend WithEvents optEndDate As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents optMonthly As System.Windows.Forms.RadioButton
    Friend WithEvents optWeekly As System.Windows.Forms.RadioButton
    Friend WithEvents optDaily As System.Windows.Forms.RadioButton
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOK As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Public strJobID As String
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
    Friend WithEvents cmbOverwrite As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents txtLocation As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlBackupType As System.Windows.Forms.Panel
    Friend WithEvents optDifferential As System.Windows.Forms.RadioButton
    Friend WithEvents optComplete As System.Windows.Forms.RadioButton
    Friend WithEvents pnlBackupTo As System.Windows.Forms.Panel
    Friend WithEvents optDisk As System.Windows.Forms.RadioButton
    Friend WithEvents optTape As System.Windows.Forms.RadioButton
    Friend WithEvents pnlSchedule As System.Windows.Forms.Panel
    Friend WithEvents optSchedule As System.Windows.Forms.RadioButton
    Friend WithEvents optNow As System.Windows.Forms.RadioButton
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBackup))
        Me.pnlSchedule = New System.Windows.Forms.Panel
        Me.optSchedule = New System.Windows.Forms.RadioButton
        Me.optNow = New System.Windows.Forms.RadioButton
        Me.pnlBackupTo = New System.Windows.Forms.Panel
        Me.optDisk = New System.Windows.Forms.RadioButton
        Me.optTape = New System.Windows.Forms.RadioButton
        Me.pnlBackupType = New System.Windows.Forms.Panel
        Me.optDifferential = New System.Windows.Forms.RadioButton
        Me.optComplete = New System.Windows.Forms.RadioButton
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.txtLocation = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.cmbOverwrite = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.lblStatus = New System.Windows.Forms.Label
        Me.grpDaily = New System.Windows.Forms.GroupBox
        Me.chkEveryDay = New System.Windows.Forms.CheckBox
        Me.lblMonth = New System.Windows.Forms.Label
        Me.numMonth = New System.Windows.Forms.NumericUpDown
        Me.chkSun = New System.Windows.Forms.CheckBox
        Me.chkSat = New System.Windows.Forms.CheckBox
        Me.chkFri = New System.Windows.Forms.CheckBox
        Me.chkThu = New System.Windows.Forms.CheckBox
        Me.chkWed = New System.Windows.Forms.CheckBox
        Me.chkTue = New System.Windows.Forms.CheckBox
        Me.chkMon = New System.Windows.Forms.CheckBox
        Me.lblJobOccursValue = New System.Windows.Forms.Label
        Me.numFrequency = New System.Windows.Forms.NumericUpDown
        Me.lblJobOccursHead = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtScheduleName = New System.Windows.Forms.TextBox
        Me.dtEndDate = New System.Windows.Forms.DateTimePicker
        Me.dtStartDate = New System.Windows.Forms.DateTimePicker
        Me.tmScheduleTime = New System.Windows.Forms.DateTimePicker
        Me.optNoEndDate = New System.Windows.Forms.RadioButton
        Me.optEndDate = New System.Windows.Forms.RadioButton
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.optMonthly = New System.Windows.Forms.RadioButton
        Me.optWeekly = New System.Windows.Forms.RadioButton
        Me.optDaily = New System.Windows.Forms.RadioButton
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel
        Me.tstrip = New System.Windows.Forms.ToolStrip
        Me.btnOK = New System.Windows.Forms.ToolStripButton
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.pnlSchedule.SuspendLayout()
        Me.pnlBackupTo.SuspendLayout()
        Me.pnlBackupType.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.grpDaily.SuspendLayout()
        CType(Me.numMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numFrequency, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSchedule
        '
        Me.pnlSchedule.Controls.Add(Me.optSchedule)
        Me.pnlSchedule.Controls.Add(Me.optNow)
        Me.pnlSchedule.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSchedule.Location = New System.Drawing.Point(120, 131)
        Me.pnlSchedule.Name = "pnlSchedule"
        Me.pnlSchedule.Size = New System.Drawing.Size(260, 19)
        Me.pnlSchedule.TabIndex = 16
        '
        'optSchedule
        '
        Me.optSchedule.Dock = System.Windows.Forms.DockStyle.Right
        Me.optSchedule.Location = New System.Drawing.Point(145, 0)
        Me.optSchedule.Name = "optSchedule"
        Me.optSchedule.Size = New System.Drawing.Size(115, 19)
        Me.optSchedule.TabIndex = 15
        Me.optSchedule.Text = "Schedule"
        '
        'optNow
        '
        Me.optNow.Checked = True
        Me.optNow.Dock = System.Windows.Forms.DockStyle.Left
        Me.optNow.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optNow.Location = New System.Drawing.Point(0, 0)
        Me.optNow.Name = "optNow"
        Me.optNow.Size = New System.Drawing.Size(68, 19)
        Me.optNow.TabIndex = 14
        Me.optNow.TabStop = True
        Me.optNow.Text = "Now"
        '
        'pnlBackupTo
        '
        Me.pnlBackupTo.Controls.Add(Me.optDisk)
        Me.pnlBackupTo.Controls.Add(Me.optTape)
        Me.pnlBackupTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlBackupTo.Location = New System.Drawing.Point(120, 71)
        Me.pnlBackupTo.Name = "pnlBackupTo"
        Me.pnlBackupTo.Size = New System.Drawing.Size(205, 19)
        Me.pnlBackupTo.TabIndex = 15
        '
        'optDisk
        '
        Me.optDisk.Checked = True
        Me.optDisk.Dock = System.Windows.Forms.DockStyle.Left
        Me.optDisk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDisk.Location = New System.Drawing.Point(0, 0)
        Me.optDisk.Name = "optDisk"
        Me.optDisk.Size = New System.Drawing.Size(70, 19)
        Me.optDisk.TabIndex = 9
        Me.optDisk.TabStop = True
        Me.optDisk.Text = "Disk"
        '
        'optTape
        '
        Me.optTape.Dock = System.Windows.Forms.DockStyle.Right
        Me.optTape.Location = New System.Drawing.Point(144, 0)
        Me.optTape.Name = "optTape"
        Me.optTape.Size = New System.Drawing.Size(61, 19)
        Me.optTape.TabIndex = 10
        Me.optTape.Text = "Tape"
        '
        'pnlBackupType
        '
        Me.pnlBackupType.Controls.Add(Me.optDifferential)
        Me.pnlBackupType.Controls.Add(Me.optComplete)
        Me.pnlBackupType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlBackupType.Location = New System.Drawing.Point(123, 13)
        Me.pnlBackupType.Name = "pnlBackupType"
        Me.pnlBackupType.Size = New System.Drawing.Size(239, 19)
        Me.pnlBackupType.TabIndex = 14
        '
        'optDifferential
        '
        Me.optDifferential.Dock = System.Windows.Forms.DockStyle.Right
        Me.optDifferential.Location = New System.Drawing.Point(141, 0)
        Me.optDifferential.Name = "optDifferential"
        Me.optDifferential.Size = New System.Drawing.Size(98, 19)
        Me.optDifferential.TabIndex = 4
        Me.optDifferential.Text = "Differential"
        '
        'optComplete
        '
        Me.optComplete.Checked = True
        Me.optComplete.Dock = System.Windows.Forms.DockStyle.Left
        Me.optComplete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optComplete.Location = New System.Drawing.Point(0, 0)
        Me.optComplete.Name = "optComplete"
        Me.optComplete.Size = New System.Drawing.Size(119, 19)
        Me.optComplete.TabIndex = 3
        Me.optComplete.TabStop = True
        Me.optComplete.Text = "Complete"
        '
        'btnBrowse
        '
        Me.btnBrowse.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Button
        Me.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Image = CType(resources.GetObject("btnBrowse.Image"), System.Drawing.Image)
        Me.btnBrowse.Location = New System.Drawing.Point(368, 100)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowse.TabIndex = 11
        '
        'txtLocation
        '
        Me.txtLocation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.Location = New System.Drawing.Point(120, 100)
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(243, 22)
        Me.txtLocation.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 103)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 14)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Backup &Location :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(44, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 14)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "&Backup To :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(48, 41)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 14)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "&Overwrite :"
        '
        'cmbOverwrite
        '
        Me.cmbOverwrite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOverwrite.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOverwrite.Location = New System.Drawing.Point(120, 37)
        Me.cmbOverwrite.Name = "cmbOverwrite"
        Me.cmbOverwrite.Size = New System.Drawing.Size(176, 22)
        Me.cmbOverwrite.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(31, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Backup &Type :"
        '
        'TabPage1
        '
        Me.TabPage1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(316, 144)
        Me.TabPage1.TabIndex = 1
        Me.TabPage1.Text = "Schedule Information"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.pnlSchedule)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.pnlBackupTo)
        Me.Panel1.Controls.Add(Me.cmbOverwrite)
        Me.Panel1.Controls.Add(Me.pnlBackupType)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.btnBrowse)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txtLocation)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 56)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(505, 161)
        Me.Panel1.TabIndex = 4
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(4, 157)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(497, 1)
        Me.Label12.TabIndex = 20
        Me.Label12.Text = "label2"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(3, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 154)
        Me.Label13.TabIndex = 19
        Me.Label13.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(501, 4)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 154)
        Me.Label14.TabIndex = 18
        Me.Label14.Text = "label3"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(3, 3)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(499, 1)
        Me.Label15.TabIndex = 17
        Me.Label15.Text = "label1"
        '
        'lblStatus
        '
        Me.lblStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(135, 4)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(157, 20)
        Me.lblStatus.TabIndex = 3
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblStatus.Visible = False
        '
        'grpDaily
        '
        Me.grpDaily.Controls.Add(Me.chkEveryDay)
        Me.grpDaily.Controls.Add(Me.lblMonth)
        Me.grpDaily.Controls.Add(Me.numMonth)
        Me.grpDaily.Controls.Add(Me.chkSun)
        Me.grpDaily.Controls.Add(Me.chkSat)
        Me.grpDaily.Controls.Add(Me.chkFri)
        Me.grpDaily.Controls.Add(Me.chkThu)
        Me.grpDaily.Controls.Add(Me.chkWed)
        Me.grpDaily.Controls.Add(Me.chkTue)
        Me.grpDaily.Controls.Add(Me.chkMon)
        Me.grpDaily.Controls.Add(Me.lblJobOccursValue)
        Me.grpDaily.Controls.Add(Me.numFrequency)
        Me.grpDaily.Controls.Add(Me.lblJobOccursHead)
        Me.grpDaily.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpDaily.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grpDaily.Location = New System.Drawing.Point(120, 39)
        Me.grpDaily.Name = "grpDaily"
        Me.grpDaily.Size = New System.Drawing.Size(362, 90)
        Me.grpDaily.TabIndex = 16
        Me.grpDaily.TabStop = False
        Me.grpDaily.Text = "Daily"
        '
        'chkEveryDay
        '
        Me.chkEveryDay.AutoSize = True
        Me.chkEveryDay.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEveryDay.Location = New System.Drawing.Point(14, 46)
        Me.chkEveryDay.Name = "chkEveryDay"
        Me.chkEveryDay.Size = New System.Drawing.Size(85, 18)
        Me.chkEveryDay.TabIndex = 12
        Me.chkEveryDay.Text = "EveryDay"
        Me.chkEveryDay.Visible = False
        '
        'lblMonth
        '
        Me.lblMonth.AutoSize = True
        Me.lblMonth.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonth.Location = New System.Drawing.Point(252, 21)
        Me.lblMonth.Name = "lblMonth"
        Me.lblMonth.Size = New System.Drawing.Size(47, 14)
        Me.lblMonth.TabIndex = 11
        Me.lblMonth.Text = "month"
        Me.lblMonth.Visible = False
        '
        'numMonth
        '
        Me.numMonth.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numMonth.Location = New System.Drawing.Point(202, 17)
        Me.numMonth.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.numMonth.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numMonth.Name = "numMonth"
        Me.numMonth.Size = New System.Drawing.Size(44, 22)
        Me.numMonth.TabIndex = 10
        Me.numMonth.Value = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numMonth.Visible = False
        '
        'chkSun
        '
        Me.chkSun.AutoSize = True
        Me.chkSun.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSun.Location = New System.Drawing.Point(230, 66)
        Me.chkSun.Name = "chkSun"
        Me.chkSun.Size = New System.Drawing.Size(50, 18)
        Me.chkSun.TabIndex = 9
        Me.chkSun.Text = "Sun"
        Me.chkSun.Visible = False
        '
        'chkSat
        '
        Me.chkSat.AutoSize = True
        Me.chkSat.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSat.Location = New System.Drawing.Point(171, 66)
        Me.chkSat.Name = "chkSat"
        Me.chkSat.Size = New System.Drawing.Size(47, 18)
        Me.chkSat.TabIndex = 8
        Me.chkSat.Text = "Sat"
        Me.chkSat.Visible = False
        '
        'chkFri
        '
        Me.chkFri.AutoSize = True
        Me.chkFri.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFri.Location = New System.Drawing.Point(109, 66)
        Me.chkFri.Name = "chkFri"
        Me.chkFri.Size = New System.Drawing.Size(41, 18)
        Me.chkFri.TabIndex = 7
        Me.chkFri.Text = "Fri"
        Me.chkFri.Visible = False
        '
        'chkThu
        '
        Me.chkThu.AutoSize = True
        Me.chkThu.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkThu.Location = New System.Drawing.Point(295, 46)
        Me.chkThu.Name = "chkThu"
        Me.chkThu.Size = New System.Drawing.Size(49, 18)
        Me.chkThu.TabIndex = 6
        Me.chkThu.Text = "Thu"
        Me.chkThu.Visible = False
        '
        'chkWed
        '
        Me.chkWed.AutoSize = True
        Me.chkWed.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWed.Location = New System.Drawing.Point(230, 46)
        Me.chkWed.Name = "chkWed"
        Me.chkWed.Size = New System.Drawing.Size(55, 18)
        Me.chkWed.TabIndex = 5
        Me.chkWed.Text = "Wed"
        Me.chkWed.Visible = False
        '
        'chkTue
        '
        Me.chkTue.AutoSize = True
        Me.chkTue.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTue.Location = New System.Drawing.Point(171, 46)
        Me.chkTue.Name = "chkTue"
        Me.chkTue.Size = New System.Drawing.Size(49, 18)
        Me.chkTue.TabIndex = 4
        Me.chkTue.Text = "Tue"
        Me.chkTue.Visible = False
        '
        'chkMon
        '
        Me.chkMon.AutoSize = True
        Me.chkMon.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMon.Location = New System.Drawing.Point(109, 46)
        Me.chkMon.Name = "chkMon"
        Me.chkMon.Size = New System.Drawing.Size(52, 18)
        Me.chkMon.TabIndex = 3
        Me.chkMon.Text = "Mon"
        Me.chkMon.Visible = False
        '
        'lblJobOccursValue
        '
        Me.lblJobOccursValue.AutoSize = True
        Me.lblJobOccursValue.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJobOccursValue.Location = New System.Drawing.Point(127, 21)
        Me.lblJobOccursValue.Name = "lblJobOccursValue"
        Me.lblJobOccursValue.Size = New System.Drawing.Size(47, 14)
        Me.lblJobOccursValue.TabIndex = 2
        Me.lblJobOccursValue.Text = "day(s)"
        '
        'numFrequency
        '
        Me.numFrequency.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numFrequency.Location = New System.Drawing.Point(66, 17)
        Me.numFrequency.Maximum = New Decimal(New Integer() {366, 0, 0, 0})
        Me.numFrequency.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numFrequency.Name = "numFrequency"
        Me.numFrequency.Size = New System.Drawing.Size(58, 22)
        Me.numFrequency.TabIndex = 1
        Me.numFrequency.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblJobOccursHead
        '
        Me.lblJobOccursHead.AutoSize = True
        Me.lblJobOccursHead.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJobOccursHead.Location = New System.Drawing.Point(12, 21)
        Me.lblJobOccursHead.Name = "lblJobOccursHead"
        Me.lblJobOccursHead.Size = New System.Drawing.Size(51, 14)
        Me.lblJobOccursHead.TabIndex = 0
        Me.lblJobOccursHead.Text = "Every :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(19, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(107, 14)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "&Schedule Name :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(38, 142)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 14)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Start Date :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(14, 171)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(96, 14)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Schedule Time :"
        '
        'txtScheduleName
        '
        Me.txtScheduleName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScheduleName.Location = New System.Drawing.Point(130, 9)
        Me.txtScheduleName.Name = "txtScheduleName"
        Me.txtScheduleName.Size = New System.Drawing.Size(352, 22)
        Me.txtScheduleName.TabIndex = 1
        '
        'dtEndDate
        '
        Me.dtEndDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtEndDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtEndDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtEndDate.Location = New System.Drawing.Point(338, 138)
        Me.dtEndDate.Name = "dtEndDate"
        Me.dtEndDate.Size = New System.Drawing.Size(107, 22)
        Me.dtEndDate.TabIndex = 5
        '
        'dtStartDate
        '
        Me.dtStartDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtStartDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtStartDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtStartDate.Location = New System.Drawing.Point(111, 138)
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.Size = New System.Drawing.Size(112, 22)
        Me.dtStartDate.TabIndex = 3
        '
        'tmScheduleTime
        '
        Me.tmScheduleTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tmScheduleTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.tmScheduleTime.Location = New System.Drawing.Point(111, 167)
        Me.tmScheduleTime.Name = "tmScheduleTime"
        Me.tmScheduleTime.ShowUpDown = True
        Me.tmScheduleTime.Size = New System.Drawing.Size(112, 22)
        Me.tmScheduleTime.TabIndex = 12
        '
        'optNoEndDate
        '
        Me.optNoEndDate.AutoSize = True
        Me.optNoEndDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optNoEndDate.Location = New System.Drawing.Point(253, 167)
        Me.optNoEndDate.Name = "optNoEndDate"
        Me.optNoEndDate.Size = New System.Drawing.Size(95, 18)
        Me.optNoEndDate.TabIndex = 13
        Me.optNoEndDate.Text = "No End Date"
        '
        'optEndDate
        '
        Me.optEndDate.AutoSize = True
        Me.optEndDate.Checked = True
        Me.optEndDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optEndDate.Location = New System.Drawing.Point(253, 140)
        Me.optEndDate.Name = "optEndDate"
        Me.optEndDate.Size = New System.Drawing.Size(84, 18)
        Me.optEndDate.TabIndex = 14
        Me.optEndDate.TabStop = True
        Me.optEndDate.Text = "End Date :"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.optMonthly)
        Me.GroupBox3.Controls.Add(Me.optWeekly)
        Me.GroupBox3.Controls.Add(Me.optDaily)
        Me.GroupBox3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox3.Location = New System.Drawing.Point(12, 39)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(102, 90)
        Me.GroupBox3.TabIndex = 15
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Job Occurs"
        '
        'optMonthly
        '
        Me.optMonthly.AutoSize = True
        Me.optMonthly.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optMonthly.Location = New System.Drawing.Point(17, 62)
        Me.optMonthly.Name = "optMonthly"
        Me.optMonthly.Size = New System.Drawing.Size(68, 18)
        Me.optMonthly.TabIndex = 2
        Me.optMonthly.Text = "Monthly"
        '
        'optWeekly
        '
        Me.optWeekly.AutoSize = True
        Me.optWeekly.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optWeekly.Location = New System.Drawing.Point(17, 42)
        Me.optWeekly.Name = "optWeekly"
        Me.optWeekly.Size = New System.Drawing.Size(65, 18)
        Me.optWeekly.TabIndex = 1
        Me.optWeekly.Text = "Weekly"
        '
        'optDaily
        '
        Me.optDaily.AutoSize = True
        Me.optDaily.Checked = True
        Me.optDaily.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDaily.Location = New System.Drawing.Point(17, 22)
        Me.optDaily.Name = "optDaily"
        Me.optDaily.Size = New System.Drawing.Size(54, 18)
        Me.optDaily.TabIndex = 0
        Me.optDaily.TabStop = True
        Me.optDaily.Text = "Daily"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.BackColor = System.Drawing.Color.White
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Right
        Me.ProgressBar1.ForeColor = System.Drawing.Color.LimeGreen
        Me.ProgressBar1.Location = New System.Drawing.Point(292, 4)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(209, 20)
        Me.ProgressBar1.TabIndex = 2
        Me.ProgressBar1.Visible = False
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
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(505, 56)
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
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOK, Me.btnCancel})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(505, 53)
        Me.tstrip.TabIndex = 0
        Me.tstrip.Text = "ToolStrip1"
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(40, 50)
        Me.btnOK.Text = "&Save"
        Me.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.btnCancel.Text = "&Close"
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCancel.ToolTipText = "Close"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.GroupBox3)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.optEndDate)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.grpDaily)
        Me.Panel2.Controls.Add(Me.optNoEndDate)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.tmScheduleTime)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.dtStartDate)
        Me.Panel2.Controls.Add(Me.txtScheduleName)
        Me.Panel2.Controls.Add(Me.dtEndDate)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 242)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Panel2.Size = New System.Drawing.Size(505, 198)
        Me.Panel2.TabIndex = 20
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 197)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(497, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 197)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(501, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 197)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "label3"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(3, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(499, 1)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = "label1"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Panel3)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 217)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel4.Size = New System.Drawing.Size(505, 25)
        Me.Panel4.TabIndex = 21
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.Label20)
        Me.Panel3.Controls.Add(Me.Label16)
        Me.Panel3.Controls.Add(Me.Label17)
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.Label19)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Location = New System.Drawing.Point(3, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(499, 22)
        Me.Panel3.TabIndex = 19
        '
        'Label20
        '
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.White
        Me.Label20.Location = New System.Drawing.Point(1, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(497, 20)
        Me.Label20.TabIndex = 9
        Me.Label20.Text = "  Schedule Information"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(1, 21)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(497, 1)
        Me.Label16.TabIndex = 8
        Me.Label16.Text = "label2"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(0, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 21)
        Me.Label17.TabIndex = 7
        Me.Label17.Text = "label4"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(498, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 21)
        Me.Label18.TabIndex = 6
        Me.Label18.Text = "label3"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(0, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(499, 1)
        Me.Label19.TabIndex = 5
        Me.Label19.Text = "label1"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel5.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Button
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.lblStatus)
        Me.Panel5.Controls.Add(Me.ProgressBar1)
        Me.Panel5.Controls.Add(Me.Label21)
        Me.Panel5.Controls.Add(Me.Label22)
        Me.Panel5.Controls.Add(Me.Label23)
        Me.Panel5.Controls.Add(Me.Label24)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel5.Location = New System.Drawing.Point(0, 440)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel5.Size = New System.Drawing.Size(505, 28)
        Me.Panel5.TabIndex = 22
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label21.Location = New System.Drawing.Point(4, 24)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(497, 1)
        Me.Label21.TabIndex = 8
        Me.Label21.Text = "label2"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(3, 4)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 21)
        Me.Label22.TabIndex = 7
        Me.Label22.Text = "label4"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(501, 4)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 21)
        Me.Label23.TabIndex = 6
        Me.Label23.Text = "label3"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(3, 3)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(499, 1)
        Me.Label24.TabIndex = 5
        Me.Label24.Text = "label1"
        '
        'frmBackup
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(505, 468)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBackup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Database Backup"
        Me.pnlSchedule.ResumeLayout(False)
        Me.pnlBackupTo.ResumeLayout(False)
        Me.pnlBackupType.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grpDaily.ResumeLayout(False)
        Me.grpDaily.PerformLayout()
        CType(Me.numMonth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numFrequency, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tstrip.Click
        Try
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub frmBackup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    Try
    '        Me.Cursor = Cursors.WaitCursor
    '        With dtStartDate
    '            .Format = DateTimePickerFormat.Custom
    '            .CustomFormat = DTFORMAT
    '            '.Value = Date.Now
    '        End With
    '        With dtEndDate
    '            .Format = DateTimePickerFormat.Custom
    '            .CustomFormat = DTFORMAT
    '            '.Value = Date.Now
    '        End With
    '        dtEndDate.Checked = True
    '        Me.Cursor = Cursors.Default
    '    Catch objErr As Exception
    '        Me.Cursor = Cursors.Default
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            With FolderBrowserDialog1
                .Description = "Select Backup Folder"
                .ShowNewFolderButton = True
            End With
            If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
                txtLocation.Text = FolderBrowserDialog1.SelectedPath
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim _LogText As String = ""
        Dim objAudit As New clsAudit

        Try
            If Trim(txtLocation.Text) = "" Then
                MessageBox.Show("Please browse Backup Location", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtLocation.Focus()
                Exit Sub
            End If
            If Dir(txtLocation.Text, FileAttribute.Directory) = "" Then
                MessageBox.Show("Please browse valid Backup Location", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtLocation.Focus()
                Exit Sub
            End If

            If optSchedule.Checked = True AndAlso Trim(txtScheduleName.Text) = "" Then
                MessageBox.Show("Please enter Schedule Name", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtScheduleName.Focus()
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            'Initialise
            _sSQLServerName = gstrSQLServerName
            _sSQLDataBaseName = gstrDatabaseName
            lblStatus.Visible = True
            Dim objSQLServer As New SQLServer2
            'or Windows Authentication
            objSQLServer.LoginSecure = True
            objSQLServer.Connect(gstrSQLServerName)


            lblStatus.Text = "Successfully Connected to SQL Server"
            ProgressBar1.Visible = True
            Dim objSQLDatabase As Database2
            objSQLDatabase = objSQLServer.Databases.Item(_sSQLDataBaseName)
            If optNow.Checked = True Then
                'Check for Complete or Differencial Backup
                If optComplete.Checked = True Then
                    objBackup.Action = SQLDMO_BACKUP_TYPE.SQLDMOBackup_Database
                Else
                    objBackup.Action = SQLDMO_BACKUP_TYPE.SQLDMOBackup_Differential
                End If
                'Check for Append or Overwrite Backup
                If Trim(cmbOverwrite.SelectedItem) = "Append to media" Then
                    objBackup.Initialize = False
                Else
                    objBackup.Initialize = True
                End If

                objBackup.Database = _sSQLDataBaseName
                objBackup.BackupSetName = "gloEMR Backup"
                objBackup.BackupSetDescription = "gloEMR Full Backup on " & Date.Now
                objBackup.Files = txtLocation.Text & "\gloEMR.bak"
                objBackup.SQLBackup(objSQLServer)

                'sarika  22nd feb


                Dim _Backuptype As String = ""
                Dim _Backupto As String = ""

                If optComplete.Checked = True Then
                    _Backuptype = "Complete"
                Else
                    _Backuptype = "Differencial"
                End If
                If optDisk.Checked = True Then
                    _Backupto = "Disk"
                Else
                    _Backupto = "Tape"
                End If
                _LogText = "Database Backup is in the location & " & txtLocation.Text.Trim & " . The Backup type is " & _Backuptype & " and Overwrite method is " & cmbOverwrite.Text & " and the Backup is taken on " & _Backupto

                '                _LogText = _LogText & vbCrLf & "Result is : " & txtLog.Text
                If Len(_LogText) > 1000 Then
                    _LogText = Mid(_LogText, 1, 1000)
                End If
                objAudit.CreateLog(clsAudit.enmActivityType.Modify, gstrLoginName & " user has taken a Database Backup." & vbCrLf & _LogText, gstrLoginName, gstrClientMachineName, 0, True)

                '-------------
            Else
                'Create Job Schedule
                Dim nCount As Int16
                If blnModify = True Then
                    For nCount = 1 To objSQLServer.JobServer.Jobs.Count
                        If Trim(objSQLServer.JobServer.Jobs.Item(nCount).JobID) = Trim(strJobID) Then
                            objSQLServer.JobServer.Jobs.Remove(nCount)
                            Exit For
                        End If
                    Next
                Else
                    For nCount = 1 To objSQLServer.JobServer.Jobs.Count
                        If Trim(objSQLServer.JobServer.Jobs.Item(nCount).Name) = Trim(txtScheduleName.Text) Then
                            Me.Cursor = Cursors.Default
                            MessageBox.Show("This schedule already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            txtScheduleName.Focus()
                            objSQLServer.Close()
                            Exit Sub
                        End If
                    Next
                End If
                Dim objJobSchedule As New JobSchedule
                Dim objJob As New Job
                objJob.Name = txtScheduleName.Text
                Dim strStartYear, strStartMonth, strStartDay As String
                Dim strEndYear, strEndMonth, strEndDay As String
                Dim strScheduleTime As String


                objJobSchedule.Name = txtScheduleName.Text

                strStartYear = dtStartDate.Value.Year
                strStartMonth = dtStartDate.Value.Month
                If strStartMonth.Length = 1 Then strStartMonth = "0" & strStartMonth
                strStartDay = dtStartDate.Value.Day
                If strStartDay.Length = 1 Then strStartDay = "0" & strStartDay

                strEndYear = dtEndDate.Value.Year
                strEndMonth = dtEndDate.Value.Month
                If strEndMonth.Length = 1 Then strEndMonth = "0" & strEndMonth
                strEndDay = dtEndDate.Value.Day
                If strEndDay.Length = 1 Then strEndDay = "0" & strEndDay

                strScheduleTime = Format(tmScheduleTime.Value, "HHmmss")


                objJobSchedule.Schedule.ActiveStartDate = strStartYear & strStartMonth & strStartDay
                objJobSchedule.Schedule.ActiveStartTimeOfDay = strScheduleTime

                If optNoEndDate.Checked = True Then
                    objJobSchedule.Schedule.ActiveEndDate = 99991231
                ElseIf optEndDate.Checked = True Then
                    objJobSchedule.Schedule.ActiveEndDate = strEndYear & strEndMonth & strEndDay
                End If

                If optDaily.Checked = True Then
                    objJobSchedule.Schedule.FrequencyType = SQLDMO_FREQUENCY_TYPE.SQLDMOFreq_Daily
                    objJobSchedule.Schedule.FrequencyInterval = numFrequency.Value
                ElseIf optWeekly.Checked = True Then
                    objJobSchedule.Schedule.FrequencyType = SQLDMO_FREQUENCY_TYPE.SQLDMOFreq_Weekly
                    objJobSchedule.Schedule.FrequencyRecurrenceFactor = numFrequency.Value

                    If chkEveryDay.Checked = False Then
                        If chkMon.Checked = True Then
                            objJobSchedule.Schedule.FrequencyInterval = objJobSchedule.Schedule.FrequencyInterval Or SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Monday
                        End If
                        If chkTue.Checked = True Then
                            objJobSchedule.Schedule.FrequencyInterval = objJobSchedule.Schedule.FrequencyInterval Or SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Tuesday
                        End If
                        If chkWed.Checked = True Then
                            objJobSchedule.Schedule.FrequencyInterval = objJobSchedule.Schedule.FrequencyInterval Or SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Wednesday
                        End If
                        If chkThu.Checked = True Then
                            objJobSchedule.Schedule.FrequencyInterval = objJobSchedule.Schedule.FrequencyInterval Or SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Thursday
                        End If
                        If chkFri.Checked = True Then
                            objJobSchedule.Schedule.FrequencyInterval = objJobSchedule.Schedule.FrequencyInterval Or SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Friday
                        End If
                        If chkSat.Checked = True Then
                            objJobSchedule.Schedule.FrequencyInterval = objJobSchedule.Schedule.FrequencyInterval Or SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Saturday
                        End If
                        If chkSun.Checked = True Then
                            objJobSchedule.Schedule.FrequencyInterval = objJobSchedule.Schedule.FrequencyInterval Or SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Sunday
                        End If
                    Else
                        objJobSchedule.Schedule.FrequencyInterval = SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_EveryDay
                    End If
                ElseIf optMonthly.Checked = True Then
                    objJobSchedule.Schedule.FrequencyType = SQLDMO_FREQUENCY_TYPE.SQLDMOFreq_Monthly
                    objJobSchedule.Schedule.FrequencyRecurrenceFactor = numMonth.Value
                    objJobSchedule.Schedule.FrequencyInterval = numFrequency.Value
                End If
                objJobSchedule.Enabled = True
                'Create Commend for Job Step
                Dim strCommand As String
                strCommand = "Backup Database " & _sSQLDataBaseName & " TO "
                If optDisk.Checked = True Then
                    strCommand = strCommand & " DISK=" & txtLocation.Text & "\gloEMR.bak"
                Else
                    strCommand = strCommand & " TAPE "
                End If
                strCommand = strCommand & " WITH "
                If Trim(cmbOverwrite.Text) = "Append to media" Then
                    strCommand = strCommand & " NOINIT, "
                Else
                    strCommand = strCommand & " INIT, "
                End If
                If optDifferential.Checked = True Then
                    strCommand = strCommand & " DIFFERENTIAL , "
                End If
                strCommand = strCommand & "NOUNLOAD,NAME='" & txtScheduleName.Text & "',NOSKIP,STATS = 10,NOFORMAT"
                Dim objJobServer As JobServer
                objJobServer = objSQLServer.JobServer
                objJobServer.Jobs.Add(objJob)
                objJob.BeginAlter()
                objJob.JobSchedules.Add(objJobSchedule)
                objJob.ApplyToTargetServer("(local)")
                Dim objJobStep As New JobStep
                With objJobStep
                    .SubSystem = "TSQL"
                    .StepID = 1
                    .Name = _sSQLDataBaseName & " Database Backup"
                    .Command = strCommand
                End With
                objJobStep.OnSuccessAction = SQLDMO_JOBSTEPACTION_TYPE.SQLDMOJobStepAction_QuitWithSuccess
                objJobStep.OnFailAction = SQLDMO_JOBSTEPACTION_TYPE.SQLDMOJobStepAction_QuitWithFailure
                objJob.JobSteps.Add(objJobStep)
                objJob.StartStepID = 1
                objJob.DoAlter()
                Dim objBackupSchedule As New clsBackupSchedule
                If optComplete.Checked = True Then
                    objBackupSchedule.BackupType = clsBackupSchedule.enmBackupType.Complete
                Else
                    objBackupSchedule.BackupType = clsBackupSchedule.enmBackupType.Differencial
                End If
                If UCase(Trim(cmbOverwrite.SelectedItem)) = UCase("Append to media") Then
                    objBackupSchedule.Overwrite = clsBackupSchedule.enmBackupOverwrite.AppendToMedia
                Else
                    objBackupSchedule.Overwrite = clsBackupSchedule.enmBackupOverwrite.OverwriteExistingMedia
                End If
                If optDisk.Checked = True Then
                    objBackupSchedule.BackupTo = clsBackupSchedule.enmBackupTo.Disk
                Else
                    objBackupSchedule.BackupTo = clsBackupSchedule.enmBackupTo.Tape
                End If
                objBackupSchedule.BackupLocation = Trim(txtLocation.Text)
                objBackupSchedule.UserID = 1
                If blnModify = True Then
                    objBackupSchedule.AddUpdateBackupSchedule(objJob.JobID, txtScheduleName.Tag)

                    'sarika  22nd feb

                    Dim _Backuptype As String = ""
                    Dim _Backupto As String = ""

                    If optComplete.Checked = True Then
                        _Backuptype = "Complete"
                    Else
                        _Backuptype = "Differencial"
                    End If
                    If optDisk.Checked = True Then
                        _Backupto = "Disk"
                    Else
                        _Backupto = "Tape"
                    End If
                    _LogText = "Database Backup is in the location & " & txtLocation.Text.Trim & " . The Backup type is " & _Backuptype & " and Overwrite method is " & cmbOverwrite.Text & " and the Backup is taken on " & _Backupto

                    '                _LogText = _LogText & vbCrLf & "Result is : " & txtLog.Text
                    If Len(_LogText) > 1000 Then
                        _LogText = Mid(_LogText, 1, 1000)
                    End If

                    '   Dim objAudit As New clsAudit
                    objAudit.CreateLog(clsAudit.enmActivityType.Modify, gstrLoginName & " user has modified the Backup schedule : " & txtScheduleName.Text & _LogText, gstrLoginName, gstrClientMachineName, 0, True)
                    objAudit = Nothing
                    '-------------

                Else
                    objBackupSchedule.AddUpdateBackupSchedule(objJob.JobID)

                    'sarika  22nd feb
                    'Dim objAudit As New clsAudit
                    objAudit.CreateLog(clsAudit.enmActivityType.Add, gstrLoginName & " user has added a new BackUp schedule named " & txtScheduleName.Text, gstrLoginName, gstrClientMachineName, 0, True)
                    'objAudit = Nothing
                    '-------------

                End If
                objBackupSchedule = Nothing
            End If

            objSQLServer.Close()
            lblStatus.Text = "Successfully Disconnected from SQL Server"
            objSQLServer = Nothing
            Me.DialogResult = DialogResult.OK
            Me.Cursor = Cursors.Default
            Me.Close()
        Catch objErr As Exception
            Me.DialogResult = DialogResult.None
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            objAudit.CreateLog(clsAudit.enmActivityType.Add, gstrLoginName & " user failed to add a new BackUp schedule named " & txtScheduleName.Text, gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Failure)
        Finally
            objAudit = Nothing
        End Try

    End Sub

    Private Sub optNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optNow.Click

        If optNow.Checked = True Then
            optNow.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optNow.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If

        Try
            Panel2.Enabled = False
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Fill_OverwriteOptions()
        With cmbOverwrite
            .Items.Clear()
            .Items.Add("Append to media")
            .Items.Add("Overwrite existing media")
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub objBackup_PercentComplete(ByVal Message As String, ByVal Percent As Integer) Handles objBackup.PercentComplete
        Try
            System.Windows.Forms.Application.DoEvents()
            ProgressBar1.Value = Percent
            lblStatus.Text = "Backup " & Percent & "%"
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub optEndDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optEndDate.Click

        If optEndDate.Checked = True Then
            optEndDate.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optEndDate.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If


        Try
            dtEndDate.Enabled = True
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub optNoEndDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optNoEndDate.Click

        If optNoEndDate.Checked = True Then
            optNoEndDate.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optNoEndDate.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If


        Try
            dtEndDate.Enabled = False
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub optDaily_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optDaily.Click
        If optDaily.Checked = True Then
            optDaily.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optDaily.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If


        Try
            grpDaily.Text = "Daily"
            chkEveryDay.Visible = False
            ShowHideWeekCheckBox(False)
            lblJobOccursHead.Text = "Every"
            lblJobOccursValue.Text = "day(s)"
            numFrequency.Minimum = 1
            numFrequency.Maximum = 366
            numFrequency.Value = 1
            numMonth.Visible = False
            lblMonth.Visible = False
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub optWeekly_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optWeekly.Click
        If optWeekly.Checked = True Then
            optWeekly.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optWeekly.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If


        Try
            grpDaily.Text = "Weekly"
            chkEveryDay.Visible = True
            If chkEveryDay.CheckState = CheckState.Checked Then
                ShowHideWeekCheckBox(False)
            Else
                ShowHideWeekCheckBox(True)
            End If

            lblJobOccursHead.Text = "Every"
            lblJobOccursValue.Text = "week(s) on"
            numFrequency.Minimum = 1
            numFrequency.Maximum = 52
            numFrequency.Value = 1
            numMonth.Visible = False
            lblMonth.Visible = False
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub optMonthly_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optMonthly.Click

        If optMonthly.Checked = True Then
            optMonthly.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optMonthly.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If



        Try
            grpDaily.Text = "Monthly"
            chkEveryDay.Visible = False
            ShowHideWeekCheckBox(False)
            lblJobOccursHead.Text = "Day"
            lblJobOccursValue.Text = "of every"
            numFrequency.Minimum = 1
            numFrequency.Maximum = 31
            numFrequency.Value = 1
            numMonth.Visible = True
            lblMonth.Visible = True
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub optSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSchedule.Click

        If optSchedule.Checked = True Then
            optSchedule.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optSchedule.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If


        Try
            Panel2.Enabled = True
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ShowHideWeekCheckBox(ByVal blnShow As Boolean)
        chkMon.Visible = blnShow
        chkTue.Visible = blnShow
        chkWed.Visible = blnShow
        chkThu.Visible = blnShow
        chkFri.Visible = blnShow
        chkSat.Visible = blnShow
        chkSun.Visible = blnShow
    End Sub

    Private Sub chkEveryDay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEveryDay.Click
        Try
            If chkEveryDay.CheckState = CheckState.Checked Then
                Call ShowHideWeekCheckBox(False)
            Else
                Call ShowHideWeekCheckBox(True)
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Fill_BackupDetails(ByVal strJobID As String)
        Dim objSQLServer As New SQLServer2
        'or Windows Authentication
        objSQLServer.LoginSecure = True
        objSQLServer.Connect(gstrSQLServerName)
        Dim objJob As Job = Nothing
        Dim nCount As Int16
        For nCount = 1 To objSQLServer.JobServer.Jobs.Count
            If Trim(objSQLServer.JobServer.Jobs.Item(nCount).JobID) = Trim(strJobID) Then
                objJob = objSQLServer.JobServer.Jobs.Item(nCount)
                Exit For
            End If
        Next
        optSchedule.Checked = True
        Dim objSender As Object = Nothing
        Dim obje As EventArgs = Nothing
        optSchedule_Click(objSender, obje)
        txtScheduleName.Text = objJob.Name
        If objJob.JobSchedules.Count >= 1 Then
            Dim objSchedule As JobSchedule
            objSchedule = objJob.JobSchedules.Item(1)
            If objSchedule.Schedule.ActiveStartDate <> 0 And CStr(objSchedule.Schedule.ActiveStartDate).Length = 8 Then
                dtStartDate.Value = RetrieveDate(objSchedule.Schedule.ActiveStartDate)
                tmScheduleTime.Value = RetrieveTime(objSchedule.Schedule.ActiveStartTimeOfDay)
            End If
            If objSchedule.Schedule.ActiveEndDate = 99991231 Then
                optNoEndDate.Checked = True
                optNoEndDate_Click(objSender, obje)
            Else
                If objSchedule.Schedule.ActiveEndDate <> 0 And CStr(objSchedule.Schedule.ActiveEndDate).Length = 8 Then
                    dtEndDate.Value = RetrieveDate(objSchedule.Schedule.ActiveEndDate)
                End If
            End If
            Select Case objSchedule.Schedule.FrequencyType
                Case SQLDMO_FREQUENCY_TYPE.SQLDMOFreq_Daily
                    optDaily.Checked = True
                    Call optDaily_Click(objSender, obje)
                    numFrequency.Value = objSchedule.Schedule.FrequencyInterval
                Case SQLDMO_FREQUENCY_TYPE.SQLDMOFreq_Weekly
                    optWeekly.Checked = True
                    Call optWeekly_Click(objSender, obje)
                    numFrequency.Value = objSchedule.Schedule.FrequencyRecurrenceFactor
                    If objSchedule.Schedule.FrequencyInterval And SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Monday Then
                        chkMon.Checked = True
                    End If
                    If objSchedule.Schedule.FrequencyInterval And SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Tuesday Then
                        chkTue.Checked = True
                    End If
                    If objSchedule.Schedule.FrequencyInterval And SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Wednesday Then
                        chkWed.Checked = True
                    End If
                    If objSchedule.Schedule.FrequencyInterval And SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Thursday Then
                        chkThu.Checked = True
                    End If
                    If objSchedule.Schedule.FrequencyInterval And SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Friday Then
                        chkFri.Checked = True
                    End If
                    If objSchedule.Schedule.FrequencyInterval And SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Saturday Then
                        chkSat.Checked = True
                    End If
                    If objSchedule.Schedule.FrequencyInterval And SQLDMO_WEEKDAY_TYPE.SQLDMOWeek_Sunday Then
                        chkSun.Checked = True
                    End If

                Case SQLDMO_FREQUENCY_TYPE.SQLDMOFreq_Monthly
                    optMonthly.Checked = True
                    Call optMonthly_Click(objSender, obje)
                    numMonth.Value = objSchedule.Schedule.FrequencyRecurrenceFactor
                    numFrequency.Value = objSchedule.Schedule.FrequencyInterval
            End Select
        End If
        objSQLServer.Close()
        objSQLServer = Nothing
        Dim objBackupSchedule As New clsBackupSchedule
        objBackupSchedule.RetrieveBackupSchedule(strJobID)
        If objBackupSchedule.BackupType = clsBackupSchedule.enmBackupType.Complete Then
            optComplete.Checked = True
        Else
            optDifferential.Checked = True
        End If
        If objBackupSchedule.BackupTo = clsBackupSchedule.enmBackupTo.Disk Then
            optDisk.Checked = True
        Else
            optTape.Checked = True
        End If
        If objBackupSchedule.Overwrite = clsBackupSchedule.enmBackupOverwrite.AppendToMedia Then
            cmbOverwrite.Text = "Append to media"
        Else
            cmbOverwrite.Text = "Overwrite existing media"
        End If
        txtLocation.Text = objBackupSchedule.BackupLocation
        objBackupSchedule = Nothing
    End Sub

    Private Function RetrieveDate(ByVal nDate As Integer) As Date
        If nDate <> 0 And CStr(nDate).Length = 8 Then
            Return CType(Mid(CStr(nDate), 5, 2) & "/" & Mid(CStr(nDate), 7, 2) & "/" & Mid(CStr(nDate), 1, 4), Date)
        End If
    End Function

    Private Function RetrieveTime(ByVal nTime As Integer) As DateTime
        If nTime <> 0 And CStr(nTime).Length = 6 Then
            Return CType(Date.Now.Date & " " & Mid(CStr(nTime), 1, 2) & ":" & Mid(CStr(nTime), 3, 2) & ":" & Mid(CStr(nTime), 5, 2), DateTime)
        End If
    End Function


    Private Sub optComplete_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optComplete.CheckedChanged
        If optComplete.Checked = True Then
            optComplete.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optComplete.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optDifferential_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDifferential.CheckedChanged
        If optDifferential.Checked = True Then
            optDifferential.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optDifferential.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optDisk_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optDisk.CheckedChanged
        If optDisk.Checked = True Then
            optDisk.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optDisk.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub optTape_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTape.CheckedChanged
        If optTape.Checked = True Then
            optTape.Font = New Font("Tahoma", 9, FontStyle.Bold)
        Else
            optTape.Font = New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub
End Class

