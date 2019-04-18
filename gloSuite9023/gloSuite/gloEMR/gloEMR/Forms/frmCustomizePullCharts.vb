Imports System.Data.SqlClient



Public Class frmCustomizePullCharts
    'Inherits System.Windows.Forms.Form
    Inherits gloAUSLibrary.MasterForm
    Dim _PatientID As Int64
#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _PatientID = PatientID
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtpTime, dtpDate}
            Dim cntControls() As System.Windows.Forms.Control = {dtpTime, dtpDate}

            If Not (components Is Nothing) Then
                components.Dispose()
            End If

            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try


            If (IsNothing(dtpControls) = False) Then
                If dtpControls.Length > 0 Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                End If
            End If


            If (IsNothing(cntControls) = False) Then
                If cntControls.Length > 0 Then
                    gloGlobal.cEventHelper.DisposeAllControls(cntControls)
                End If
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblPatientName As System.Windows.Forms.Label
    Friend WithEvents lblPatientCode As System.Windows.Forms.Label
    Friend WithEvents lblDoctorName As System.Windows.Forms.Label
    Friend WithEvents dtpTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents pnl_tls_Command As System.Windows.Forms.Panel
    Private WithEvents tls As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents btn_tls_Ok As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_Cancel As System.Windows.Forms.ToolStripButton
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents cmbApp_Location As System.Windows.Forms.ComboBox
    Friend WithEvents lblApp_Location As System.Windows.Forms.Label
    Friend WithEvents numApp_DateTime_Duration As System.Windows.Forms.NumericUpDown
    Private WithEvents lblDurationUnit As System.Windows.Forms.Label
    Friend WithEvents lblApp_DateTime_Duration As System.Windows.Forms.Label
    Friend WithEvents cmbApp_AppointmentType As System.Windows.Forms.ComboBox
    Friend WithEvents lblApp_AppointmentType As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCustomizePullCharts))
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.cmbApp_AppointmentType = New System.Windows.Forms.ComboBox()
        Me.lblApp_AppointmentType = New System.Windows.Forms.Label()
        Me.numApp_DateTime_Duration = New System.Windows.Forms.NumericUpDown()
        Me.cmbApp_Location = New System.Windows.Forms.ComboBox()
        Me.lblDurationUnit = New System.Windows.Forms.Label()
        Me.lblApp_Location = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblApp_DateTime_Duration = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtpTime = New System.Windows.Forms.DateTimePicker()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblDoctorName = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblPatientName = New System.Windows.Forms.Label()
        Me.lblPatientCode = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.pnl_tls_Command = New System.Windows.Forms.Panel()
        Me.tls = New gloGlobal.gloToolStripIgnoreFocus()
        Me.btn_tls_Ok = New System.Windows.Forms.ToolStripButton()
        Me.btn_tls_Cancel = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnlTop.SuspendLayout()
        CType(Me.numApp_DateTime_Duration, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMain.SuspendLayout()
        Me.pnl_tls_Command.SuspendLayout()
        Me.tls.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.Transparent
        Me.pnlTop.Controls.Add(Me.cmbApp_AppointmentType)
        Me.pnlTop.Controls.Add(Me.lblApp_AppointmentType)
        Me.pnlTop.Controls.Add(Me.numApp_DateTime_Duration)
        Me.pnlTop.Controls.Add(Me.cmbApp_Location)
        Me.pnlTop.Controls.Add(Me.lblDurationUnit)
        Me.pnlTop.Controls.Add(Me.lblApp_Location)
        Me.pnlTop.Controls.Add(Me.Label9)
        Me.pnlTop.Controls.Add(Me.Label8)
        Me.pnlTop.Controls.Add(Me.Label7)
        Me.pnlTop.Controls.Add(Me.lblApp_DateTime_Duration)
        Me.pnlTop.Controls.Add(Me.Label6)
        Me.pnlTop.Controls.Add(Me.dtpTime)
        Me.pnlTop.Controls.Add(Me.dtpDate)
        Me.pnlTop.Controls.Add(Me.Label5)
        Me.pnlTop.Controls.Add(Me.Label3)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTop.Location = New System.Drawing.Point(3, 90)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlTop.Size = New System.Drawing.Size(381, 122)
        Me.pnlTop.TabIndex = 0
        '
        'cmbApp_AppointmentType
        '
        Me.cmbApp_AppointmentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbApp_AppointmentType.ForeColor = System.Drawing.Color.Black
        Me.cmbApp_AppointmentType.FormattingEnabled = True
        Me.cmbApp_AppointmentType.Location = New System.Drawing.Point(101, 92)
        Me.cmbApp_AppointmentType.Margin = New System.Windows.Forms.Padding(2)
        Me.cmbApp_AppointmentType.Name = "cmbApp_AppointmentType"
        Me.cmbApp_AppointmentType.Size = New System.Drawing.Size(145, 22)
        Me.cmbApp_AppointmentType.TabIndex = 93
        '
        'lblApp_AppointmentType
        '
        Me.lblApp_AppointmentType.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblApp_AppointmentType.AutoEllipsis = True
        Me.lblApp_AppointmentType.AutoSize = True
        Me.lblApp_AppointmentType.BackColor = System.Drawing.Color.Transparent
        Me.lblApp_AppointmentType.Location = New System.Drawing.Point(56, 96)
        Me.lblApp_AppointmentType.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblApp_AppointmentType.Name = "lblApp_AppointmentType"
        Me.lblApp_AppointmentType.Size = New System.Drawing.Size(43, 14)
        Me.lblApp_AppointmentType.TabIndex = 94
        Me.lblApp_AppointmentType.Text = "Type :"
        Me.lblApp_AppointmentType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'numApp_DateTime_Duration
        '
        Me.numApp_DateTime_Duration.ForeColor = System.Drawing.Color.Black
        Me.numApp_DateTime_Duration.Location = New System.Drawing.Point(261, 39)
        Me.numApp_DateTime_Duration.Margin = New System.Windows.Forms.Padding(2)
        Me.numApp_DateTime_Duration.Maximum = New Decimal(New Integer() {1440, 0, 0, 0})
        Me.numApp_DateTime_Duration.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numApp_DateTime_Duration.Name = "numApp_DateTime_Duration"
        Me.numApp_DateTime_Duration.Size = New System.Drawing.Size(42, 22)
        Me.numApp_DateTime_Duration.TabIndex = 89
        Me.numApp_DateTime_Duration.Value = New Decimal(New Integer() {15, 0, 0, 0})
        '
        'cmbApp_Location
        '
        Me.cmbApp_Location.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbApp_Location.ForeColor = System.Drawing.Color.Black
        Me.cmbApp_Location.FormattingEnabled = True
        Me.cmbApp_Location.Location = New System.Drawing.Point(101, 66)
        Me.cmbApp_Location.Margin = New System.Windows.Forms.Padding(2)
        Me.cmbApp_Location.Name = "cmbApp_Location"
        Me.cmbApp_Location.Size = New System.Drawing.Size(246, 22)
        Me.cmbApp_Location.TabIndex = 91
        '
        'lblDurationUnit
        '
        Me.lblDurationUnit.AutoSize = True
        Me.lblDurationUnit.Location = New System.Drawing.Point(306, 43)
        Me.lblDurationUnit.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblDurationUnit.Name = "lblDurationUnit"
        Me.lblDurationUnit.Size = New System.Drawing.Size(41, 14)
        Me.lblDurationUnit.TabIndex = 92
        Me.lblDurationUnit.Text = "(mins)"
        '
        'lblApp_Location
        '
        Me.lblApp_Location.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblApp_Location.AutoEllipsis = True
        Me.lblApp_Location.AutoSize = True
        Me.lblApp_Location.BackColor = System.Drawing.Color.Transparent
        Me.lblApp_Location.Location = New System.Drawing.Point(38, 70)
        Me.lblApp_Location.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblApp_Location.Name = "lblApp_Location"
        Me.lblApp_Location.Size = New System.Drawing.Size(61, 14)
        Me.lblApp_Location.TabIndex = 90
        Me.lblApp_Location.Text = "Location :"
        Me.lblApp_Location.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Location = New System.Drawing.Point(1, 121)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(379, 1)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "label1"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Location = New System.Drawing.Point(1, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(379, 1)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "label1"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Location = New System.Drawing.Point(380, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 119)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "label4"
        '
        'lblApp_DateTime_Duration
        '
        Me.lblApp_DateTime_Duration.AutoSize = True
        Me.lblApp_DateTime_Duration.BackColor = System.Drawing.Color.Transparent
        Me.lblApp_DateTime_Duration.Location = New System.Drawing.Point(198, 43)
        Me.lblApp_DateTime_Duration.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblApp_DateTime_Duration.Name = "lblApp_DateTime_Duration"
        Me.lblApp_DateTime_Duration.Size = New System.Drawing.Size(61, 14)
        Me.lblApp_DateTime_Duration.TabIndex = 88
        Me.lblApp_DateTime_Duration.Text = "Duration :"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Location = New System.Drawing.Point(0, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 119)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "label4"
        '
        'dtpTime
        '
        Me.dtpTime.CustomFormat = "hh:mm tt"
        Me.dtpTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTime.Location = New System.Drawing.Point(101, 39)
        Me.dtpTime.Name = "dtpTime"
        Me.dtpTime.ShowUpDown = True
        Me.dtpTime.Size = New System.Drawing.Size(93, 22)
        Me.dtpTime.TabIndex = 35
        Me.dtpTime.Value = New Date(2005, 8, 30, 0, 0, 0, 0)
        '
        'dtpDate
        '
        Me.dtpDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(101, 12)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(93, 22)
        Me.dtpDate.TabIndex = 34
        Me.dtpDate.Value = New Date(2005, 8, 30, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(57, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 14)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Time :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(58, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 14)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "Date :"
        '
        'lblDoctorName
        '
        Me.lblDoctorName.AutoSize = True
        Me.lblDoctorName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDoctorName.Location = New System.Drawing.Point(106, 10)
        Me.lblDoctorName.Name = "lblDoctorName"
        Me.lblDoctorName.Size = New System.Drawing.Size(0, 14)
        Me.lblDoctorName.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label4.Location = New System.Drawing.Point(5, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 14)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Provider Name :"
        '
        'lblPatientName
        '
        Me.lblPatientName.AutoSize = True
        Me.lblPatientName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientName.Location = New System.Drawing.Point(106, 64)
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Size = New System.Drawing.Size(0, 14)
        Me.lblPatientName.TabIndex = 3
        '
        'lblPatientCode
        '
        Me.lblPatientCode.AutoSize = True
        Me.lblPatientCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientCode.Location = New System.Drawing.Point(106, 37)
        Me.lblPatientCode.Name = "lblPatientCode"
        Me.lblPatientCode.Size = New System.Drawing.Size(0, 14)
        Me.lblPatientCode.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Patient Name :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Patient Code :"
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlMain.Controls.Add(Me.lbl_pnlRight)
        Me.pnlMain.Controls.Add(Me.lbl_pnlTop)
        Me.pnlMain.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.lblPatientCode)
        Me.pnlMain.Controls.Add(Me.lblPatientName)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.lblDoctorName)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMain.Location = New System.Drawing.Point(3, 3)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(381, 87)
        Me.pnlMain.TabIndex = 2
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(1, 86)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(379, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(380, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 86)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(1, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(380, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 87)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'pnl_tls_Command
        '
        Me.pnl_tls_Command.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tls_Command.Controls.Add(Me.tls)
        Me.pnl_tls_Command.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tls_Command.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tls_Command.Name = "pnl_tls_Command"
        Me.pnl_tls_Command.Size = New System.Drawing.Size(387, 53)
        Me.pnl_tls_Command.TabIndex = 3
        '
        'tls
        '
        Me.tls.BackColor = System.Drawing.Color.Transparent
        Me.tls.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_Ok, Me.btn_tls_Cancel})
        Me.tls.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls.Location = New System.Drawing.Point(0, 0)
        Me.tls.Name = "tls"
        Me.tls.Size = New System.Drawing.Size(387, 53)
        Me.tls.TabIndex = 0
        Me.tls.Text = "toolStrip1"
        '
        'btn_tls_Ok
        '
        Me.btn_tls_Ok.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Ok.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_tls_Ok.Image = CType(resources.GetObject("btn_tls_Ok.Image"), System.Drawing.Image)
        Me.btn_tls_Ok.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Ok.Name = "btn_tls_Ok"
        Me.btn_tls_Ok.Size = New System.Drawing.Size(66, 50)
        Me.btn_tls_Ok.Text = "&Save&&Cls"
        Me.btn_tls_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_Ok.ToolTipText = "Save and Close"
        '
        'btn_tls_Cancel
        '
        Me.btn_tls_Cancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Cancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_tls_Cancel.Image = CType(resources.GetObject("btn_tls_Cancel.Image"), System.Drawing.Image)
        Me.btn_tls_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Cancel.Name = "btn_tls_Cancel"
        Me.btn_tls_Cancel.Size = New System.Drawing.Size(43, 50)
        Me.btn_tls_Cancel.Text = "&Close"
        Me.btn_tls_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.pnlTop)
        Me.Panel1.Controls.Add(Me.pnlMain)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 53)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(387, 215)
        Me.Panel1.TabIndex = 6
        '
        'frmCustomizePullCharts
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(387, 268)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnl_tls_Command)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCustomizePullCharts"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pull Chart"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.numApp_DateTime_Duration, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.pnl_tls_Command.ResumeLayout(False)
        Me.pnl_tls_Command.PerformLayout()
        Me.tls.ResumeLayout(False)
        Me.tls.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private _TypeofAppointment As String
    Private _ProviderID As Long
    Private _dtClinicStartTime As DateTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 09:00 AM")
    Private _dtClinicEndTime As DateTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " 06:00 PM")
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Dim isBaddebtPatient As Boolean = False

    Public Property TypeofAppointment() As String
        Get
            Return _TypeofAppointment
        End Get
        Set(ByVal value As String)
            _TypeofAppointment = value
        End Set
    End Property

    Public Property ProviderID() As Int64
        Get
            Return _ProviderID
        End Get
        Set(ByVal value As Int64)
            _ProviderID = value
        End Set
    End Property

    Private Sub frmCustomizePullCharts_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objSettings As New clsSettings

        Try

            GetClinicTiming()
            Fill_LocationCombo()
            FillAppointmenttypes()
            GetAppointmentTypeFromAdmin()

            'set datetimepicker's date to selected date and time to the current time
            dtpTime.Value = Convert.ToDateTime(dtpDate.Value.Date.ToShortDateString() + " " + DateTime.Now.ToShortTimeString())
            dtpTime.Format = DateTimePickerFormat.Custom
            dtpTime.CustomFormat = "hh:mm tt"

            numApp_DateTime_Duration.Value = objSettings.GetSettingValue("Pull Chart Interval")

            CheckAppointmentDetails()

            Dim _ToolStrip As New List(Of Object)
            _ToolStrip.Add(Me.btn_tls_Ok)
            MyBase.FormControls = Nothing
            MyBase.FormControls = _ToolStrip.ToArray()
            MyBase.SetChildFormControls()

            _ToolStrip = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(objSettings) = False Then
                objSettings.Dispose()
                objSettings = Nothing
            End If
        End Try
    End Sub

    Private Sub GetAppointmentTypeFromAdmin()

        Dim oSet As New gloSettings.GeneralSettings(GetConnectionString())
        Dim _objSettingValue As Object = Nothing
        Dim _clinicID As Int64 = 1
        If appSettings("ClinicID") IsNot Nothing Then
            If appSettings("ClinicID") <> "" Then
                _clinicID = Convert.ToInt64(appSettings("ClinicID"))
            Else
                _clinicID = 1
            End If
        Else
            _clinicID = 1
        End If

        If _TypeofAppointment = "Today" Then
            dtpDate.Value = System.DateTime.Now.Date
            dtpDate.Enabled = False
        ElseIf _TypeofAppointment = "Tomorrow" Then
            dtpDate.Value = System.DateTime.Now.Date.AddDays(1)
            dtpDate.Enabled = False
        ElseIf _TypeofAppointment = "Customise" Then
            dtpDate.Value = System.DateTime.Now.Date
        End If


        If _TypeofAppointment = "Today" Or _TypeofAppointment = "Customise" Then
            oSet.GetSetting("Default AppointmentType for Same Day", 0, _clinicID, _objSettingValue)
            If (_objSettingValue = "" Or _objSettingValue = "0") Then
                oSet.GetSetting("Default AppointmentType for future", 0, _clinicID, _objSettingValue)
                If (_objSettingValue <> "") Then
                    cmbApp_AppointmentType.SelectedValue = _objSettingValue
                End If
            Else
                cmbApp_AppointmentType.SelectedValue = _objSettingValue
            End If
        Else
            oSet.GetSetting("Default AppointmentType for future", 0, _clinicID, _objSettingValue)
            If (_objSettingValue <> "") Then
                cmbApp_AppointmentType.SelectedValue = _objSettingValue
            End If
        End If

        If oSet IsNot Nothing Then
            oSet.Dispose()
            oSet = Nothing
        End If

        _objSettingValue = Nothing

    End Sub

    'code start by nilesh on date 20101227 for case GLO2010-0005425
    Private Function CheckAppointmentDetails() As Boolean
        Dim sMySQL As String
        Dim oDBLayer As gloDatabaseLayer.DBLayer = Nothing
        Dim iMaxTime As Int16
        Dim iMaxAppointmentsInSlot As Int16
        Try
            oDBLayer = New gloDatabaseLayer.DBLayer(GetConnectionString())
            'sMySQL = "select isnull(Max(dtEndTime),0) from AS_Appointment_MST where dtEndDate=" & gloDateMaster.gloDate.DateAsNumber(dtpDate.Value) & " and nASBaseID='" & ProviderID.ToString & "'"
            sMySQL = "SELECT ISNULL(MAX(AS_Appointment_MST.dtEndTime),0) FROM AS_Appointment_MST INNER JOIN AS_Appointment_DTL ON AS_Appointment_MST.nMSTAppointmentID=AS_Appointment_DTL.nMSTAppointmentID WHERE AS_Appointment_MST.dtEndDate=" & gloDateMaster.gloDate.DateAsNumber(dtpDate.Value) & " AND AS_Appointment_MST.nASBaseID='" & ProviderID.ToString & "' AND AS_Appointment_DTL.nUsedStatus NOT IN (5,6,7)"
            oDBLayer.Connect(False)
            iMaxTime = oDBLayer.ExecuteScalar_Query(sMySQL)

            If iMaxTime <> vbNullString AndAlso iMaxTime > 0 Then
                Dim objSettings As New clsSettings
                Dim dtSetting As DataTable = objSettings.GetSetting("MaxAppointmentsInSlot")

                If dtSetting.Rows.Count > 0 Then
                    iMaxAppointmentsInSlot = dtSetting.Rows(0).Item("sSettingsValue").ToString
                End If
                objSettings.Dispose()
                objSettings = Nothing
                dtSetting.Dispose()
                dtSetting = Nothing
                'sMySQL = "select count(*) from AS_Appointment_MST where dtStartDate=" & gloDateMaster.gloDate.DateAsNumber(dtpDate.Value) & " and dtStartTime between " & dtpTime.Value.Hour.ToString & "00" & " and " & dtpTime.Value.AddHours(1).Hour.ToString & "00" & " and nASBaseID='" & ProviderID.ToString & "'"
                sMySQL = "SELECT COUNT(AS_Appointment_MST.nMSTAppointmentID) FROM AS_Appointment_MST INNER JOIN AS_Appointment_DTL ON AS_Appointment_MST.nMSTAppointmentID=AS_Appointment_DTL.nMSTAppointmentID WHERE AS_Appointment_MST.dtStartDate=" & gloDateMaster.gloDate.DateAsNumber(dtpDate.Value) & " AND AS_Appointment_MST.dtStartTime BETWEEN " & dtpTime.Value.Hour.ToString & "00" & " AND " & dtpTime.Value.AddHours(1).Hour.ToString & "00" & " AND AS_Appointment_MST.nASBaseID='" & ProviderID.ToString & "' AND AS_Appointment_DTL.nUsedStatus NOT IN (5,6,7)"
                Dim iTotalAppointmentsInHour As Int16 = oDBLayer.ExecuteScalar_Query(sMySQL)

                If iTotalAppointmentsInHour >= iMaxAppointmentsInSlot Then
                    dtpTime.Value = gloDateMaster.gloTime.TimeAsDateTime(Date.Now, iMaxTime)
                    dtpTime.Value = dtpTime.Value.AddHours(1)
                    dtpTime.Value = dtpTime.Value.AddMinutes(-(dtpTime.Value.Minute))
                    'dtpTime.Value = dtpTime.Value.AddMinutes(numApp_DateTime_Duration.Value)
                Else
                    dtpTime.Value = gloDateMaster.gloTime.TimeAsDateTime(Date.Now, iMaxTime)
                    'dtpTime.Value = dtpTime.Value.AddMinutes(numApp_DateTime_Duration.Value)
                End If

                dtpTime.Format = DateTimePickerFormat.Custom
                dtpTime.CustomFormat = "hh:mm tt"
            End If
            
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
                oDBLayer = Nothing
            End If
        End Try
        Return Nothing
    End Function
    'code end by nilesh on date 20101227 for case GLO2010-0005425

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tls_Cancel.Click
        Me.Close()
    End Sub
    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_tls_Ok.Click
        Try
            Dim _IsPullChartsOnToday As Boolean = False
            'Dim arrPatientAppointment As New ArrayList
            'arrPatientAppointment.Add(0)
            'arrPatientAppointment.Add(gnPatientID)
            ''Retrieve Provider ID
            'Dim objProvider As New clsProvider
            'arrPatientAppointment.Add(objProvider.RetrieveProviderID(lblDoctorName.Text))
            'objProvider = Nothing


            ''Check Appointment Module Level
            'If gnAppointmentModuleLevel <> 0 Then
            '    arrPatientAppointment.Add(MonthCalendar1.SelectionStart.Date & " " & Format(gClinicStartTime.AddHours(-1), "Medium Time"))
            'Else
            '    Dim objAppointments As New clsAppointments
            '    Dim dtPullChartsDate As DateTime
            '    dtPullChartsDate = objAppointments.GetLastPullChartsDate(MonthCalendar1.SelectionStart.Date)
            '    If dtPullChartsDate = MonthCalendar1.SelectionStart.Date Then
            '        dtPullChartsDate = dtPullChartsDate.Date & " " & Format(gClinicStartTime, "Medium Time")
            '    Else
            '        dtPullChartsDate = dtPullChartsDate.AddMinutes(gnPULLCHARTSInterval)
            '    End If
            '    arrPatientAppointment.Add(dtPullChartsDate)
            'End If

            'arrPatientAppointment.Add("")
            'arrPatientAppointment.Add("Phone")
            'arrPatientAppointment.Add(gnAppointmentInterval)
            'arrPatientAppointment.Add("None")

            ''PULL CHARTS Appointment
            'arrPatientAppointment.Add(1)

            ''Appointment Group ID
            'arrPatientAppointment.Add(0)

            ''PULL CHARTS Appointment Color
            'arrPatientAppointment.Add(nPullChartsAppointmentsColor)

            'Dim objAppointment As New clsAppointments
            'objAppointment.AddData(arrPatientAppointment)
            'objAppointment = Nothing
            'Me.Close()

            'Dim arrPatientAppointment As New ArrayList
            'arrPatientAppointment.Add(0)
            'arrPatientAppointment.Add(gnPatientID)
            ''Retrieve Provider ID
            'Dim objProvider As New clsProvider
            'arrPatientAppointment.Add(objProvider.RetrieveProviderID(lblDoctorName.Text))
            'objProvider = Nothing


            ''Check Appointment Module Level
            'If gnAppointmentModuleLevel <> 0 Then
            '    arrPatientAppointment.Add(dtpDate.Value.Date & " " & Format(dtpTime.Value, "Medium Time"))
            'Else
            '    '' Commented BY Maheshg  20071105
            '    'Dim objAppointments As New clsAppointments
            '    'Dim dtPullChartsDate As DateTime
            '    'dtPullChartsDate = objAppointments.GetLastPullChartsDate(dtpDate.Value.Date)
            '    'If dtPullChartsDate = dtpDate.Value.Date Then
            '    '    dtPullChartsDate = dtPullChartsDate.Date & " " & Format(dtpTime.Value, "Medium Time")
            '    'Else
            '    '    dtPullChartsDate = dtPullChartsDate.AddMinutes(gnPULLCHARTSInterval)
            '    'End If
            '    'arrPatientAppointment.Add(dtPullChartsDate)
            '    '' 

            '    ''Added By Mahesh 20071105
            '    Dim dtPullChartsDate As DateTime
            '    dtPullChartsDate = dtpDate.Value.Date & " " & Format(dtpTime.Value, "Medium Time")
            '    arrPatientAppointment.Add(dtPullChartsDate)
            'End If

            'arrPatientAppointment.Add("")
            'arrPatientAppointment.Add("Phone")
            'arrPatientAppointment.Add(gnAppointmentInterval)
            'arrPatientAppointment.Add("None")

            ''PULL CHARTS Appointment
            'arrPatientAppointment.Add(1)

            ''Appointment Group ID
            'arrPatientAppointment.Add(0)

            ''PULL CHARTS Appointment Color
            'arrPatientAppointment.Add(nPullChartsAppointmentsColor)

            'Dim objAppointment As New clsAppointments
            'objAppointment.AddData(arrPatientAppointment)
            'objAppointment = Nothing

            ''Sandip Darade 24 Feb 09 
            ''Set up appointment

            ''Sandip Darade 20090708
            ''location a mandatory field 

            Me.Focus()

            If isBaddebtPatient Then
                Dim dResult As DialogResult = MessageBox.Show("Patient is in BAD DEBT status, are you sure you want to schedule a new appointment ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                If dResult = DialogResult.No Then
                    Me.Close()
                    Exit Sub
                End If
            End If


            If (cmbApp_Location.Text = "") Then
                MessageBox.Show("Please select a location.   ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbApp_Location.Focus()
                'Code added by shubhangi 20110311 to resolved issue: 8912
            ElseIf (dtpDate.Value > Convert.ToDateTime("12/31/2100")) Then
                MessageBox.Show("Check the date it should not be greater than 12/31/2100.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                Exit Sub
            Else

                ''Code Added by Mayuri:20091214
                ''To check whether patient is already scheduled for selected day

                Dim _StartDate As Int64
                Dim objProvider As New clsProvider
                objProvider.Dispose()
                objProvider = Nothing
                '_PatientID = gnPatientID
                _StartDate = gloDateMaster.gloDate.DateAsNumber(dtpDate.Value)

                If IsMaximumAppointmentRegisterd(dtpDate.Value, dtpTime.Value, ProviderID) = False Then

                Else
                    Dim dgResult As DialogResult = DialogResult.None
                    dgResult = MessageBox.Show("All appointments defined in the template are filled for this time. Do you want to create an additional appointment?  ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If dgResult = DialogResult.No Then
                        Exit Sub
                    End If
                End If


                ''_IsPullChartsOnToday = IsPullChartsOnToday(_PatientID, _StartDate)
                'Dim ogloAppointment As New gloAppointmentScheduling.gloAppointment(GetConnectionString())
                'ogloAppointment.IsAppointmentOnToday(_PatientID, gnClinicID, , 0)
                'If (_IsPullChartsOnToday = True) Then
                '    Dim dresult As DialogResult = MessageBox.Show("This patient is already scheduled for selected day. Do you want to register anyway? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                '    If dresult = DialogResult.No Then
                '        Exit Sub
                '    End If


                'End If
                ''End code Added by Mayuri:20091214




                Dim objCon As New SqlConnection
                Dim objCmd As New SqlCommand
               
                Dim dtDOB As DateTime = Nothing
                objCon.ConnectionString = GetConnectionString()
                objCmd.CommandType = CommandType.Text
                objCmd.CommandText = "Select dtDOB from Patient Where npatientID ='" & _PatientID & "'"
                objCmd.Connection = objCon

                objCon.Open()
                dtDOB = objCmd.ExecuteScalar

                objCon.Close()
                objCon.Dispose()
                objCon = Nothing
                If objCmd IsNot Nothing Then
                    objCmd.Parameters.Clear()
                    objCmd.Dispose()
                    objCmd = Nothing
                End If
                If IsNothing(dtDOB) = False Then
                    If dtpDate.Value.CompareTo(CDate(dtDOB)) < 0 Then
                        MessageBox.Show(" Appointment Date must greater than Patient's 'Date of Birth'.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        dtpDate.Focus()
                       
                        Exit Sub
                    End If
                End If
                
                SaveAppointment()
                'Me.Close()


                End If


        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub
    

#Region "set up appointment"

    Private Sub Fill_LocationCombo()
        Dim dtlocation As DataTable = Get_Locations()

        If (dtlocation IsNot Nothing AndAlso dtlocation.Rows.Count > 0) Then
            cmbApp_Location.DataSource = dtlocation
            cmbApp_Location.DisplayMember = "Location"
            cmbApp_Location.ValueMember = "LocationID"
            cmbApp_Location.Refresh()

            Dim _DefaultLocationID As Int64
            _DefaultLocationID = GetDefaultLocation()
            If _DefaultLocationID > 0 Then
                cmbApp_Location.SelectedValue = _DefaultLocationID
            End If
        End If
    End Sub

    Private Sub FillAppointmenttypes()
        'Appointment Types
        Dim oApptCommon As New gloAppointmentScheduling.gloAppointmnetScheduleCommon(GetConnectionString())
        Dim oListItems As gloGeneralItem.gloItems
        '        oListItems = New gloGeneralItem.gloItems()
        oListItems = oApptCommon.GetAppointmentTypes()
        Dim oTableAppTypes As New DataTable()

        oTableAppTypes.Columns.Add("ID")
        oTableAppTypes.Columns.Add("DispName")

        Dim dr As DataRow
        dr = oTableAppTypes.NewRow()
        dr(0) = 0
        dr(1) = ""
        oTableAppTypes.Rows.Add(dr)

        For _Counter As Int16 = 0 To oListItems.Count - 1
            Dim oRow As DataRow
            oRow = oTableAppTypes.NewRow()
            oRow(0) = oListItems(_Counter).ID
            oRow(1) = oListItems(_Counter).Description
            oTableAppTypes.Rows.Add(oRow)
        Next
        cmbApp_AppointmentType.DataSource = oTableAppTypes
        cmbApp_AppointmentType.DisplayMember = "DispName"
        cmbApp_AppointmentType.ValueMember = "ID"
        oListItems.Dispose()
        oListItems = Nothing
        oApptCommon.Dispose()
        oApptCommon = Nothing
    End Sub


    Public Function Get_Locations() As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing

        Try
            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = " SELECT ISNULL(sLocation,'') AS Location,ISNULL(nLocationID,0) AS LocationID FROM  AB_Location WHERE nClinicID = " & gnClinicID & " AND ISNULL(bIsBlocked,'false') = 'false'"
            objCmd.Connection = objCon

            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = objCmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()

            Return dt

        Catch ex As SqlException
            Throw
        Catch ex As Exception
            Throw
        Finally

            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If

            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If

            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If

        End Try

    End Function

    Public Function GetDefaultLocation() As Int64
        Dim oValue As Object
        Dim oDB As gloDatabaseLayer.DBLayer = Nothing
        Try
            oDB = New gloDatabaseLayer.DBLayer(GetConnectionString())
            oDB.Connect(False)
            oValue = oDB.ExecuteScalar("GetDefaultLocationID")
            oDB.Disconnect()
            Return Convert.ToInt64(oValue)
        Catch ex As Exception
            Throw
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If

            oValue = Nothing
        End Try
    End Function

    ''Code Added by Mayuri:20091214-To check whether selected patient is schedule for selected day
    Public Function IsPullChartsOnToday(ByVal _PatientID As Int64, ByVal _StartDate As Int64) As Boolean
        Dim _result As Boolean = False
        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing
        
        Try

            objCon = New SqlConnection(GetConnectionString())
            objCon.Open()
            Dim _sqlString As String
            '_sqlString = "select count(*) from AS_Appointment_MST where nPatientID= " + _PatientID.ToString() + ""
            _sqlString = "select count(*) from AS_Appointment_MST where nPatientID= " + _PatientID.ToString() + "and dtStartDate=" + _StartDate.ToString() + ""
            objCmd = New SqlCommand(_sqlString, objCon)
            Dim value As Object = objCmd.ExecuteScalar()

            If value IsNot Nothing AndAlso Convert.ToString(value) <> "" Then
                If Convert.ToInt32(value) > 0 Then
                    _result = True
                Else
                    _result = False
                End If

            End If
            Return _result
        Catch ex As Exception
            Return Nothing
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If

        End Try

    End Function
    ''
    Private Sub GetClinicTiming()
        Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString())
        Dim value As Object = Nothing
        Try
            ogloSettings.GetSetting("ClinicStartTime", value)
            If value IsNot Nothing AndAlso Convert.ToString(value).Trim() <> "" Then
                _dtClinicStartTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() & " " & value.ToString())
                value = Nothing
            End If

            ogloSettings.GetSetting("ClinicEndTime", value)
            If value IsNot Nothing AndAlso Convert.ToString(value).Trim() <> "" Then
                _dtClinicEndTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() & " " & value.ToString())
                value = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        Finally
            If ogloSettings IsNot Nothing Then
                ogloSettings.Dispose()
            End If
            value = Nothing
        End Try
    End Sub

    ''
    ''End code added by Mayuri:20091214

    ''Add appointment info
    Private Sub SaveAppointment()

        Dim ts As New TimeSpan(0, Convert.ToDecimal(numApp_DateTime_Duration.Value), 0)

      
        Try
            Dim tmpProviderID As String = ""
            If Convert.ToString(gnLoginProviderID) <> "0" Then
                tmpProviderID = ""
            Else
                tmpProviderID = ProviderID
            End If

            If MyBase.SetChildFormModules("SaveAndFinishExam", "Save and Finish Exam", tmpProviderID) = True Then
                Exit Sub
            End If
            Dim objProvider As New clsProvider
            objProvider.Dispose()
            objProvider = Nothing
            Dim oMasterAppointment As New gloAppointmentScheduling.MasterAppointment()


            oMasterAppointment.ASBaseDescription = lblDoctorName.Text
            oMasterAppointment.ASBaseCode = lblDoctorName.Text
            oMasterAppointment.ASBaseID = ProviderID
            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'oMasterAppointment.PatientID = gnPatientID
            oMasterAppointment.PatientID = _PatientID
            'end modification
            oMasterAppointment.LocationID = Convert.ToInt64(cmbApp_Location.SelectedValue)
            oMasterAppointment.LocationName = Convert.ToString(cmbApp_Location.Text)
            oMasterAppointment.StartDate = dtpDate.Value

            'changes for to create multi-day appointment
            'Added total duration in Start time to calculate end date
            Dim dtendDate As DateTime
            Dim dDuration As Decimal
            dDuration = Convert.ToDouble(numApp_DateTime_Duration.Value)
            dtendDate = Convert.ToDateTime(dtpDate.Value.ToShortDateString() + " " + dtpTime.Value.ToShortTimeString())


            oMasterAppointment.EndDate = dtendDate.AddMinutes(Convert.ToDouble(numApp_DateTime_Duration.Value))
            oMasterAppointment.StartTime = dtpTime.Value
            'oMasterAppointment.EndTime = dtpTime.Value.AddMinutes(ts.Minutes)
            'RESOLVED ISSUE 10310
            oMasterAppointment.EndTime = dtpTime.Value.AddMinutes(Convert.ToDouble(numApp_DateTime_Duration.Value))
            oMasterAppointment.Duration = numApp_DateTime_Duration.Value
            oMasterAppointment.ASBaseFlag = gloAppointmentScheduling.ASBaseType.Provider
            oMasterAppointment.UsedStatus = gloAppointmentScheduling.ASUsedStatus.Registred
            ''lines added  by Sandip Darade  20100423
            ''Add appointmenttype
            oMasterAppointment.AppointmentTypeID = Convert.ToInt64(cmbApp_AppointmentType.SelectedValue)
            oMasterAppointment.AppointmentTypeCode = cmbApp_AppointmentType.Text
            oMasterAppointment.AppointmentTypeDesc = cmbApp_AppointmentType.Text
            ''For Single appointment 
            oMasterAppointment.Criteria.SingleCriteria.StartDate = gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.StartDate.ToString("MM/dd/yyyy"))
            oMasterAppointment.Criteria.SingleCriteria.EndDate = gloDateMaster.gloDate.DateAsNumber(oMasterAppointment.EndDate.ToString("MM/dd/yyyy"))
            oMasterAppointment.Criteria.SingleCriteria.StartTime = gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.StartTime.ToString("hh:mm tt"))
            oMasterAppointment.Criteria.SingleCriteria.EndTime = gloDateMaster.gloTime.TimeAsNumber(oMasterAppointment.EndTime.ToString("hh:mm tt"))
            oMasterAppointment.Criteria.SingleCriteria.Duration = oMasterAppointment.Duration


            '' Added by Pranit on 16 sep 2011 to pass Empty ResourceIDs to MasterAppointment (object) 
            oMasterAppointment.ResourceIDS = New System.Text.StringBuilder("")
            '' End by Pranit on 16 sep 2011


            Dim dgClinicTime As DialogResult
            If ((Convert.ToDateTime(oMasterAppointment.StartTime.ToShortTimeString()) < Convert.ToDateTime(_dtClinicStartTime.ToShortTimeString())) Or (Convert.ToDateTime(oMasterAppointment.StartTime.ToShortTimeString()) > Convert.ToDateTime(_dtClinicEndTime.ToShortTimeString()))) Then

                dgClinicTime = MessageBox.Show("Appointment is outside clinic time.  Do you want to continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If dgClinicTime = Windows.Forms.DialogResult.No Then
                    oMasterAppointment.Dispose()
                    oMasterAppointment = Nothing
                    Exit Sub
                End If
            ElseIf ((Convert.ToDateTime(oMasterAppointment.EndTime.ToShortTimeString()) < Convert.ToDateTime(_dtClinicStartTime.ToShortTimeString())) Or (Convert.ToDateTime(oMasterAppointment.EndTime.ToShortTimeString()) > Convert.ToDateTime(_dtClinicEndTime.ToShortTimeString()))) Then
                dgClinicTime = MessageBox.Show("Appointment is outside clinic time.  Do you want to continue?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If dgClinicTime = Windows.Forms.DialogResult.No Then
                    oMasterAppointment.Dispose()
                    oMasterAppointment = Nothing
                    Exit Sub
                End If

            End If
            Dim ogloAppointment As New gloAppointmentScheduling.gloAppointment(GetConnectionString())
            Dim appointmetncnt As Int64
            appointmetncnt = ogloAppointment.IsAppointmentOnToday(_PatientID, gnClinicID, oMasterAppointment.StartDate, 0)
            If (appointmetncnt >= 1) Then
                Dim dresult As DialogResult = MessageBox.Show("This patient is already scheduled for selected day. Do you want to register anyway? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If dresult = DialogResult.No Then
                    ogloAppointment.Dispose()
                    ogloAppointment = Nothing
                    oMasterAppointment.Dispose()
                    oMasterAppointment = Nothing
                    Exit Sub
                End If
            End If

            'SHUBHANGI 20110323 TO RESOLVED 8895

            If (ogloAppointment.IsAppointmentRegisterd(oMasterAppointment.StartTime, oMasterAppointment.EndTime, oMasterAppointment.ASBaseID, 0) = True) Then
                Dim dgResult As New DialogResult
                dgResult = MessageBox.Show("Warning –  " + lblDoctorName.Text + "  has appointment conflicts during this time. Continue with this new appointment?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If dgResult = Windows.Forms.DialogResult.No Then
                    ogloAppointment.Dispose()
                    ogloAppointment = Nothing
                    oMasterAppointment.Dispose()
                    oMasterAppointment = Nothing
                    Exit Sub
                End If
            End If
            Dim dt As DataTable = Nothing
            Dim appbook As New gloAppointmentBook.Books.AppointmentType(GetConnectionString())
            'SHUBHANGI 20110314 TO RESOLVED 8807
            dt = appbook.GetAppointmentType(Convert.ToInt64(cmbApp_AppointmentType.SelectedValue))
            If (dt.Rows.Count >= 1) Then
                oMasterAppointment.ColorCode = dt.Rows(0)("sColorCode")
            End If
            If Not dt Is Nothing Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not appbook Is Nothing Then
                appbook.Dispose()
                appbook = Nothing

            End If
            oMasterAppointment.ClinicID = gnClinicID
            oMasterAppointment.PATransaction = New gloAppointmentScheduling.PriorAuthorizationTransaction()


            Dim _UserName As String

            If appSettings("UserName") IsNot Nothing Then
                If appSettings("UserName") <> "" Then
                    _UserName = Convert.ToString(appSettings("UserName")).Trim()
                Else
                    _UserName = ""
                End If
            Else
                _UserName = ""
            End If



            Dim oClsgloUserRights As gloUserRights.ClsgloUserRights = Nothing

            oClsgloUserRights = New gloUserRights.ClsgloUserRights(GetConnectionString())
            oClsgloUserRights.CheckForUserRights("admin")


            Dim dtable As DataTable = Nothing

            dtable = ResourseName(oMasterAppointment.ASBaseID, oMasterAppointment.StartTime, oMasterAppointment.EndTime, oMasterAppointment.StartDate, oMasterAppointment.LocationName)

            If (dtable.Rows.Count > 0) Then
                If (oClsgloUserRights.OverrideProviderBlockSchedule = False) Then
                    MessageBox.Show("Schedule is blocked for provider. Could not create appointment.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    dtable.Dispose()
                    dtable = Nothing
                    oClsgloUserRights.Dispose()
                    oClsgloUserRights = Nothing
                    ogloAppointment.Dispose()
                    ogloAppointment = Nothing
                    oMasterAppointment.Dispose()
                    oMasterAppointment = Nothing
                    Exit Sub
                Else
                    If (MessageBox.Show("Schedule for " + dtable.Rows(0)("sASBaseDesc") + " is blocked from " + dtable.Rows(0)("dtStarttime") & " to " & dtable.Rows(0)("dtEndtime") + "." + Environment.NewLine + "Save this " + oMasterAppointment.StartTime.ToShortTimeString() + " - " + oMasterAppointment.EndTime.ToShortTimeString() + " appointment? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) Then
                    Else
                        dtable.Dispose()
                        dtable = Nothing
                        oClsgloUserRights.Dispose()
                        oClsgloUserRights = Nothing
                        ogloAppointment.Dispose()
                        ogloAppointment = Nothing
                        oMasterAppointment.Dispose()
                        oMasterAppointment = Nothing
                        Exit Sub
                    End If
                End If
            End If


            ''Added by Abhijeet on 20100925 set the enum value for new appointment  message queue entry
            gloAppointmentScheduling.gloHL7._AppointmentHL7Flag = gloAppointmentScheduling.HL7AppointmentFlag.Add
            ''End of changes by Abhijeet on 20100925 set the enum value for new appointment  message queue entry
            Dim nMasterAppointmentID As Int64 = 0
            nMasterAppointmentID = ogloAppointment.Add(oMasterAppointment, gloAppointmentScheduling.AppointmentScheduleFlag.None, 0, 0, 0)
            If (nMasterAppointmentID > 0) Then
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Appointment, gloAuditTrail.ActivityCategory.PullCharts, gloAuditTrail.ActivityType.Add, "Appointment added", gloAuditTrail.ActivityOutCome.Success)
                ''Added on 20100422-To fix case no:#0003868
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Appointment, gloAuditTrail.ActivityCategory.PullCharts, gloAuditTrail.ActivityType.Add, "Appointment added", oMasterAppointment.PatientID, nMasterAppointmentID, oMasterAppointment.ASBaseID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            End If
            dtable.Dispose()
            dtable = Nothing
            oClsgloUserRights.Dispose()
            oClsgloUserRights = Nothing
            ogloAppointment.Dispose()
            ogloAppointment = Nothing
            oMasterAppointment.Dispose()
            oMasterAppointment = Nothing
            Me.Close()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Appointment, gloAuditTrail.ActivityCategory.PullCharts, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)

            'ADDED BY SHUBHANGI 20110314
        Finally
           
        End Try
    End Sub


    Public Function ResourseName(ByVal ProviderID As Int64, ByVal StartTime As DateTime, ByVal EndTime As DateTime, ByVal AppoinmentDate As DateTime, ByVal Location As String) As DataTable
        Dim dt As DataTable = Nothing

        Dim ts As New TimeSpan()
        ts = EndTime - StartTime
        Dim duration As Decimal
        duration = ts.TotalMinutes



        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
        Dim DatabaseConnectionString As String = Convert.ToString(appSettings("DataBaseConnectionString"))
        Dim _clinicID As Int64 = 1
        If appSettings("ClinicID") IsNot Nothing Then
            If appSettings("ClinicID") <> "" Then
                _clinicID = Convert.ToInt64(appSettings("ClinicID"))
            Else
                _clinicID = 1
            End If
        Else
            _clinicID = 1
        End If

        Try
            oDB.Connect(False)


            Dim dateTime As New DateTime()
            Dim splitDate As String = AppoinmentDate.ToShortDateString() + " " + StartTime.ToShortTimeString()
            dateTime = Convert.ToDateTime(splitDate)

            Dim newDateTime As New DateTime()
            newDateTime = dateTime.AddMinutes(duration)

            oDBParameters.Add("@ProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@dtStartDate", gloDateMaster.gloDate.DateAsNumber(AppoinmentDate.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@dtEndDate", gloDateMaster.gloDate.DateAsNumber(newDateTime.ToShortDateString()), ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@dtStartTime", gloDateMaster.gloTime.TimeAsNumber(StartTime.ToShortTimeString()), ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@dtEndTime", gloDateMaster.gloTime.TimeAsNumber(EndTime.ToShortTimeString()), ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@ClinicId", _clinicID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@Flag", 2, ParameterDirection.Input, SqlDbType.Int)
            oDBParameters.Add("@LocationName", Location, ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Connect(False)
            oDB.Retrive("AS_BlockedSlots", oDBParameters, dt)
            oDB.Disconnect()

            oDBParameters.Dispose()
            oDBParameters = Nothing

            oDB.Dispose()
            oDB = Nothing


            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return Nothing
        Finally
            'dt.Dispose()
            'dt = Nothing
        End Try
    End Function



    ''set  datetimepicker's  date to selected date and time to the current time
    Private Sub dtpDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDate.ValueChanged

        dtpTime.Value = Convert.ToDateTime(dtpDate.Value.Date.ToShortDateString() + " " + DateTime.Now.ToShortTimeString())

    End Sub
    Private Function IsMaximumAppointmentRegisterd(ByVal dtAppDate As DateTime, ByVal dtAppTime As DateTime, ByVal ASBaseID As Int64) As Boolean
        Dim _result As Boolean = False
        Dim ogloAppointment As New gloAppointmentScheduling.gloAppointment(GetConnectionString())
        Try
            _result = ogloAppointment.IsMaximumAppointmentRegisterd(dtAppDate, dtAppTime, ASBaseID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
        ogloAppointment.Dispose()
        Return _result
    End Function


#End Region

    Private Sub frmCustomizePullCharts_Shown(sender As System.Object, e As System.EventArgs) Handles MyBase.Shown
        Dim oSecurity As New gloSecurity.gloSecurity(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        If oSecurity.isBadDebtPatient(_PatientID, True) Then
            Dim dResult As DialogResult = MessageBox.Show("Patient is in BAD DEBT status, are you sure you want to schedule a new appointment ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
            If dResult = DialogResult.Yes Then
                isBaddebtPatient = True
            ElseIf dResult = DialogResult.No Then
                Me.Close()
            End If
        End If
    End Sub
End Class
