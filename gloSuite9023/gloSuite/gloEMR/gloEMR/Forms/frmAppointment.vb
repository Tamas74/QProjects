Imports ComponentGo.Calendars
Imports System.Data.SqlClient
Imports gloUserControlLibrary
Public Class frmAppointment
    Inherits System.Windows.Forms.Form
    Dim objclsAppointment As New clsAppointments
    'Dim dt As New DataTable
    ''Private WithEvents dgCustomGrid As CustomGrid.CustomDataGrid

    Dim dtAppointment As New DataTable
    Dim clmnAppointmentDate As New DataColumn("AppointmentDate")
    Dim clmnAppointmentTime As New DataColumn("AppointmentTime")
    Dim clmnIsAvailable As New DataColumn("IsAvailable")
    Dim clmnDoctor As New DataColumn("Doctor")

    Public Shared strAppointmentDate As String
    Public Shared strAppointmentTime As String

    Public blnAppointmentTypeChange As Boolean = False

    Dim strFutureAppointmentProviderName As String
    Dim strFutureAppointmentDate As String
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Dim strFutureAppointmentTime As String
    Dim dtPatient As New DataTable
    'Private WithEvents objPatientControl As gloUC_CustomSearchInC1Flexgrid
    Private WithEvents objPatientControl As gloPatientDataGrid
    Private ReferralCount As Int64
    Private rowindex As Integer
    Private Me_Width As Integer
    Dim Phone_AS As String
    Dim DOB_AS As Date
    Dim ISDOB_AS As Boolean
    Private WithEvents pnl_tls As System.Windows.Forms.Panel
    Private WithEvents tlsNewAppointment As System.Windows.Forms.ToolStrip
    Private WithEvents btn_tls_Save As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_Close As System.Windows.Forms.ToolStripButton
    Dim dvPatient As DataView

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
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optReferral As System.Windows.Forms.RadioButton
    Friend WithEvents optEmail As System.Windows.Forms.RadioButton
    Friend WithEvents optPhone As System.Windows.Forms.RadioButton
    Friend WithEvents optInPerson As System.Windows.Forms.RadioButton
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlSearchPatient As System.Windows.Forms.Panel
    'System.Windows.Forms.DataGrid
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSearchPatient As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblAppointmentID As System.Windows.Forms.Label
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents txtPatientCode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents numAppointmentDuration As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtpTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtChiefComplaints As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnShowFutureAppointments As System.Windows.Forms.Button
    Friend WithEvents dgFutureAppointments As clsDataGrid ' System.Windows.Forms.DataGrid
    Friend WithEvents cmbAppointmentTypes As System.Windows.Forms.ComboBox
    Friend WithEvents grpFutureAppointments As System.Windows.Forms.GroupBox
    Friend WithEvents lblAppointmentUpTo As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblAppointmentInterval As System.Windows.Forms.Label
    Friend WithEvents btnDeleteAppointment As System.Windows.Forms.Button
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuModifyAppointment As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDeleteAppointment As System.Windows.Forms.MenuItem
    Friend WithEvents btnModifyAppointment As System.Windows.Forms.Button
    Friend WithEvents btnSchedule As System.Windows.Forms.Button
    Friend WithEvents btnCheckAvailability As System.Windows.Forms.Button
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents grpCustomizeAppointments As System.Windows.Forms.GroupBox
    Friend WithEvents MonthCalendar1 As System.Windows.Forms.MonthCalendar
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lstCustomizeAppointments As System.Windows.Forms.ListBox
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents pnlMiddle As System.Windows.Forms.Panel
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents pnlCustomizeMain As System.Windows.Forms.Panel
    Friend WithEvents AppointmentCelendar As ComponentGo.Calendars.DailyCalendar
    Friend WithEvents btnDeleteCustomizeAppointment As System.Windows.Forms.Button
    Friend WithEvents chkCustomizeAppointment As System.Windows.Forms.CheckBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnDeleteAll As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAppointment))
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.lblMessage = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.chkCustomizeAppointment = New System.Windows.Forms.CheckBox
        Me.btnCheckAvailability = New System.Windows.Forms.Button
        Me.btnSchedule = New System.Windows.Forms.Button
        Me.cmbAppointmentTypes = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.numAppointmentDuration = New System.Windows.Forms.NumericUpDown
        Me.Label9 = New System.Windows.Forms.Label
        Me.dtpTime = New System.Windows.Forms.DateTimePicker
        Me.dtpDate = New System.Windows.Forms.DateTimePicker
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtChiefComplaints = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.cmbProvider = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnSearchPatient = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblAppointmentID = New System.Windows.Forms.Label
        Me.txtLastName = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtFirstName = New System.Windows.Forms.TextBox
        Me.txtPatientCode = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.optReferral = New System.Windows.Forms.RadioButton
        Me.optEmail = New System.Windows.Forms.RadioButton
        Me.optPhone = New System.Windows.Forms.RadioButton
        Me.optInPerson = New System.Windows.Forms.RadioButton
        Me.grpCustomizeAppointments = New System.Windows.Forms.GroupBox
        Me.pnlCustomizeMain = New System.Windows.Forms.Panel
        Me.AppointmentCelendar = New ComponentGo.Calendars.DailyCalendar
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.Label14 = New System.Windows.Forms.Label
        Me.pnlLeft = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.lstCustomizeAppointments = New System.Windows.Forms.ListBox
        Me.pnlMiddle = New System.Windows.Forms.Panel
        Me.btnDeleteAll = New System.Windows.Forms.Button
        Me.btnDeleteCustomizeAppointment = New System.Windows.Forms.Button
        Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar
        Me.grpFutureAppointments = New System.Windows.Forms.GroupBox
        Me.dgFutureAppointments = New gloEMR.clsDataGrid
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.mnuModifyAppointment = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.mnuDeleteAppointment = New System.Windows.Forms.MenuItem
        Me.btnModifyAppointment = New System.Windows.Forms.Button
        Me.btnDeleteAppointment = New System.Windows.Forms.Button
        Me.lblAppointmentInterval = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.lblAppointmentUpTo = New System.Windows.Forms.Label
        Me.btnShowFutureAppointments = New System.Windows.Forms.Button
        Me.pnlSearchPatient = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label15 = New System.Windows.Forms.Label
        Me.pnl_tls = New System.Windows.Forms.Panel
        Me.tlsNewAppointment = New System.Windows.Forms.ToolStrip
        Me.btn_tls_Save = New System.Windows.Forms.ToolStripButton
        Me.btn_tls_Close = New System.Windows.Forms.ToolStripButton
        Me.Panel2.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.numAppointmentDuration, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpCustomizeAppointments.SuspendLayout()
        Me.pnlCustomizeMain.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlMiddle.SuspendLayout()
        Me.grpFutureAppointments.SuspendLayout()
        CType(Me.dgFutureAppointments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnl_tls.SuspendLayout()
        Me.tlsNewAppointment.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.pnlMain)
        Me.Panel2.Controls.Add(Me.pnlSearchPatient)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 73)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(989, 437)
        Me.Panel2.TabIndex = 1
        '
        'pnlMain
        '
        Me.pnlMain.BackgroundImage = CType(resources.GetObject("pnlMain.BackgroundImage"), System.Drawing.Image)
        Me.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMain.Controls.Add(Me.lblMessage)
        Me.pnlMain.Controls.Add(Me.GroupBox4)
        Me.pnlMain.Controls.Add(Me.GroupBox3)
        Me.pnlMain.Controls.Add(Me.GroupBox2)
        Me.pnlMain.Controls.Add(Me.GroupBox1)
        Me.pnlMain.Controls.Add(Me.grpCustomizeAppointments)
        Me.pnlMain.Controls.Add(Me.grpFutureAppointments)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(989, 437)
        Me.pnlMain.TabIndex = 3
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblMessage.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.Color.Red
        Me.lblMessage.Location = New System.Drawing.Point(4, 422)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(978, 20)
        Me.lblMessage.TabIndex = 30
        Me.lblMessage.Visible = False
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.Controls.Add(Me.chkCustomizeAppointment)
        Me.GroupBox4.Controls.Add(Me.btnCheckAvailability)
        Me.GroupBox4.Controls.Add(Me.btnSchedule)
        Me.GroupBox4.Controls.Add(Me.cmbAppointmentTypes)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Controls.Add(Me.numAppointmentDuration)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.dtpTime)
        Me.GroupBox4.Controls.Add(Me.dtpDate)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.txtChiefComplaints)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.ForeColor = System.Drawing.Color.Black
        Me.GroupBox4.Location = New System.Drawing.Point(2, 187)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(528, 183)
        Me.GroupBox4.TabIndex = 28
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Appointment Details"
        '
        'chkCustomizeAppointment
        '
        Me.chkCustomizeAppointment.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCustomizeAppointment.Location = New System.Drawing.Point(375, 82)
        Me.chkCustomizeAppointment.Name = "chkCustomizeAppointment"
        Me.chkCustomizeAppointment.Size = New System.Drawing.Size(162, 20)
        Me.chkCustomizeAppointment.TabIndex = 39
        Me.chkCustomizeAppointment.Text = "Customized Appointments"
        '
        'btnCheckAvailability
        '
        Me.btnCheckAvailability.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCheckAvailability.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnCheckAvailability.BackgroundImage = CType(resources.GetObject("btnCheckAvailability.BackgroundImage"), System.Drawing.Image)
        Me.btnCheckAvailability.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCheckAvailability.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnCheckAvailability.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnCheckAvailability.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCheckAvailability.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCheckAvailability.Location = New System.Drawing.Point(289, 48)
        Me.btnCheckAvailability.Name = "btnCheckAvailability"
        Me.btnCheckAvailability.Size = New System.Drawing.Size(145, 25)
        Me.btnCheckAvailability.TabIndex = 38
        Me.btnCheckAvailability.Text = "Check Availability"
        Me.btnCheckAvailability.UseVisualStyleBackColor = False
        Me.btnCheckAvailability.Visible = False
        '
        'btnSchedule
        '
        Me.btnSchedule.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSchedule.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnSchedule.BackgroundImage = CType(resources.GetObject("btnSchedule.BackgroundImage"), System.Drawing.Image)
        Me.btnSchedule.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSchedule.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnSchedule.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSchedule.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSchedule.Location = New System.Drawing.Point(391, 16)
        Me.btnSchedule.Name = "btnSchedule"
        Me.btnSchedule.Size = New System.Drawing.Size(77, 25)
        Me.btnSchedule.TabIndex = 37
        Me.btnSchedule.Text = "Schedule"
        Me.btnSchedule.UseVisualStyleBackColor = False
        Me.btnSchedule.Visible = False
        '
        'cmbAppointmentTypes
        '
        Me.cmbAppointmentTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAppointmentTypes.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAppointmentTypes.Location = New System.Drawing.Point(127, 81)
        Me.cmbAppointmentTypes.Name = "cmbAppointmentTypes"
        Me.cmbAppointmentTypes.Size = New System.Drawing.Size(238, 22)
        Me.cmbAppointmentTypes.TabIndex = 36
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(4, 85)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(121, 14)
        Me.Label11.TabIndex = 35
        Me.Label11.Text = "Appointment Type"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(193, 57)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(82, 14)
        Me.Label10.TabIndex = 34
        Me.Label10.Text = "(in minutes)"
        '
        'numAppointmentDuration
        '
        Me.numAppointmentDuration.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numAppointmentDuration.Increment = New Decimal(New Integer() {5, 0, 0, 0})
        Me.numAppointmentDuration.Location = New System.Drawing.Point(127, 53)
        Me.numAppointmentDuration.Maximum = New Decimal(New Integer() {1440, 0, 0, 0})
        Me.numAppointmentDuration.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numAppointmentDuration.Name = "numAppointmentDuration"
        Me.numAppointmentDuration.Size = New System.Drawing.Size(58, 22)
        Me.numAppointmentDuration.TabIndex = 33
        Me.numAppointmentDuration.Value = New Decimal(New Integer() {15, 0, 0, 0})
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(64, 57)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 14)
        Me.Label9.TabIndex = 32
        Me.Label9.Text = "Duration"
        '
        'dtpTime
        '
        Me.dtpTime.CustomFormat = "hh:mm tt"
        Me.dtpTime.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTime.Location = New System.Drawing.Point(289, 21)
        Me.dtpTime.Name = "dtpTime"
        Me.dtpTime.ShowUpDown = True
        Me.dtpTime.Size = New System.Drawing.Size(90, 22)
        Me.dtpTime.TabIndex = 31
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
        Me.dtpDate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(127, 21)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(110, 22)
        Me.dtpDate.TabIndex = 30
        Me.dtpDate.Value = New Date(2005, 8, 30, 0, 0, 0, 0)
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 107)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(108, 14)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "Reason For Visit"
        '
        'txtChiefComplaints
        '
        Me.txtChiefComplaints.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChiefComplaints.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChiefComplaints.Location = New System.Drawing.Point(127, 107)
        Me.txtChiefComplaints.Multiline = True
        Me.txtChiefComplaints.Name = "txtChiefComplaints"
        Me.txtChiefComplaints.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtChiefComplaints.Size = New System.Drawing.Size(396, 68)
        Me.txtChiefComplaints.TabIndex = 28
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(249, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(36, 14)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "Time"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(88, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 14)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "Date"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.cmbProvider)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.ForeColor = System.Drawing.Color.Black
        Me.GroupBox3.Location = New System.Drawing.Point(2, 131)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(528, 46)
        Me.GroupBox3.TabIndex = 27
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Provider Details"
        '
        'cmbProvider
        '
        Me.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProvider.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProvider.Location = New System.Drawing.Point(129, 18)
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.Size = New System.Drawing.Size(393, 22)
        Me.cmbProvider.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(31, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 14)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Provider"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.btnSearchPatient)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.lblAppointmentID)
        Me.GroupBox2.Controls.Add(Me.txtLastName)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.txtFirstName)
        Me.GroupBox2.Controls.Add(Me.txtPatientCode)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(2, -1)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(528, 121)
        Me.GroupBox2.TabIndex = 26
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Patient Details"
        '
        'btnSearchPatient
        '
        Me.btnSearchPatient.AutoSize = True
        Me.btnSearchPatient.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnSearchPatient.BackgroundImage = CType(resources.GetObject("btnSearchPatient.BackgroundImage"), System.Drawing.Image)
        Me.btnSearchPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSearchPatient.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnSearchPatient.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnSearchPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchPatient.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchPatient.Location = New System.Drawing.Point(295, 28)
        Me.btnSearchPatient.Name = "btnSearchPatient"
        Me.btnSearchPatient.Size = New System.Drawing.Size(117, 26)
        Me.btnSearchPatient.TabIndex = 31
        Me.btnSearchPatient.Text = "Change Patient"
        Me.btnSearchPatient.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(12, 68)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(92, 14)
        Me.Label7.TabIndex = 30
        Me.Label7.Text = "Patient Name"
        '
        'lblAppointmentID
        '
        Me.lblAppointmentID.AutoSize = True
        Me.lblAppointmentID.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppointmentID.Location = New System.Drawing.Point(370, 33)
        Me.lblAppointmentID.Name = "lblAppointmentID"
        Me.lblAppointmentID.Size = New System.Drawing.Size(109, 14)
        Me.lblAppointmentID.TabIndex = 29
        Me.lblAppointmentID.Text = " Appointment ID"
        Me.lblAppointmentID.Visible = False
        '
        'txtLastName
        '
        Me.txtLastName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLastName.Enabled = False
        Me.txtLastName.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastName.Location = New System.Drawing.Point(291, 65)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(149, 21)
        Me.txtLastName.TabIndex = 28
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(325, 91)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(84, 14)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "(Last Name)"
        '
        'txtFirstName
        '
        Me.txtFirstName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFirstName.Enabled = False
        Me.txtFirstName.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFirstName.Location = New System.Drawing.Point(129, 65)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(160, 21)
        Me.txtFirstName.TabIndex = 26
        '
        'txtPatientCode
        '
        Me.txtPatientCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPatientCode.Enabled = False
        Me.txtPatientCode.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPatientCode.Location = New System.Drawing.Point(130, 29)
        Me.txtPatientCode.Name = "txtPatientCode"
        Me.txtPatientCode.Size = New System.Drawing.Size(160, 21)
        Me.txtPatientCode.TabIndex = 25
        Me.txtPatientCode.Tag = "1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(161, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 14)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "(First Name)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 14)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Patient Code"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.optReferral)
        Me.GroupBox1.Controls.Add(Me.optEmail)
        Me.GroupBox1.Controls.Add(Me.optPhone)
        Me.GroupBox1.Controls.Add(Me.optInPerson)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(2, 376)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(528, 44)
        Me.GroupBox1.TabIndex = 19
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Appointment Type"
        '
        'optReferral
        '
        Me.optReferral.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.optReferral.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optReferral.Location = New System.Drawing.Point(211, 20)
        Me.optReferral.Name = "optReferral"
        Me.optReferral.Size = New System.Drawing.Size(88, 16)
        Me.optReferral.TabIndex = 4
        Me.optReferral.Text = "Referral"
        '
        'optEmail
        '
        Me.optEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.optEmail.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optEmail.Location = New System.Drawing.Point(309, 20)
        Me.optEmail.Name = "optEmail"
        Me.optEmail.Size = New System.Drawing.Size(88, 16)
        Me.optEmail.TabIndex = 2
        Me.optEmail.Text = "Email"
        '
        'optPhone
        '
        Me.optPhone.Checked = True
        Me.optPhone.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.optPhone.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPhone.Location = New System.Drawing.Point(113, 20)
        Me.optPhone.Name = "optPhone"
        Me.optPhone.Size = New System.Drawing.Size(88, 16)
        Me.optPhone.TabIndex = 1
        Me.optPhone.TabStop = True
        Me.optPhone.Text = "Phone"
        '
        'optInPerson
        '
        Me.optInPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.optInPerson.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optInPerson.Location = New System.Drawing.Point(403, 20)
        Me.optInPerson.Name = "optInPerson"
        Me.optInPerson.Size = New System.Drawing.Size(119, 16)
        Me.optInPerson.TabIndex = 0
        Me.optInPerson.Text = "In Person"
        '
        'grpCustomizeAppointments
        '
        Me.grpCustomizeAppointments.BackgroundImage = CType(resources.GetObject("grpCustomizeAppointments.BackgroundImage"), System.Drawing.Image)
        Me.grpCustomizeAppointments.Controls.Add(Me.pnlCustomizeMain)
        Me.grpCustomizeAppointments.Controls.Add(Me.pnlBottom)
        Me.grpCustomizeAppointments.Controls.Add(Me.pnlLeft)
        Me.grpCustomizeAppointments.ForeColor = System.Drawing.Color.Black
        Me.grpCustomizeAppointments.Location = New System.Drawing.Point(536, 1)
        Me.grpCustomizeAppointments.Name = "grpCustomizeAppointments"
        Me.grpCustomizeAppointments.Size = New System.Drawing.Size(452, 420)
        Me.grpCustomizeAppointments.TabIndex = 2
        Me.grpCustomizeAppointments.TabStop = False
        Me.grpCustomizeAppointments.Text = "Customize Appointments"
        Me.grpCustomizeAppointments.Visible = False
        '
        'pnlCustomizeMain
        '
        Me.pnlCustomizeMain.Controls.Add(Me.AppointmentCelendar)
        Me.pnlCustomizeMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCustomizeMain.Location = New System.Drawing.Point(192, 18)
        Me.pnlCustomizeMain.Name = "pnlCustomizeMain"
        Me.pnlCustomizeMain.Size = New System.Drawing.Size(257, 375)
        Me.pnlCustomizeMain.TabIndex = 3
        '
        'AppointmentCelendar
        '
        Me.AppointmentCelendar.AllDayVisible = False
        Me.AppointmentCelendar.AllowDateChange = False
        Me.AppointmentCelendar.AllowOverlap = False
        Me.AppointmentCelendar.BackColor = System.Drawing.Color.GhostWhite
        Me.AppointmentCelendar.BackgroundImage = CType(resources.GetObject("AppointmentCelendar.BackgroundImage"), System.Drawing.Image)
        Me.AppointmentCelendar.DaysLabelColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.AppointmentCelendar.DaysLabelColorSecond = System.Drawing.Color.Orange
        Me.AppointmentCelendar.DaysWorkspaceColor = System.Drawing.Color.LightSteelBlue
        Me.AppointmentCelendar.DaysWorkspaceColorSecond = System.Drawing.Color.DodgerBlue
        Me.AppointmentCelendar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AppointmentCelendar.EnableEditInPlace = False
        Me.AppointmentCelendar.FreeHourColor = System.Drawing.Color.Yellow
        Me.AppointmentCelendar.HoursLabelColor = System.Drawing.Color.Orange
        Me.AppointmentCelendar.HoursLabelColorSecond = System.Drawing.Color.Linen
        Me.AppointmentCelendar.LineColor = System.Drawing.Color.Maroon
        Me.AppointmentCelendar.Location = New System.Drawing.Point(0, 0)
        Me.AppointmentCelendar.Name = "AppointmentCelendar"
        Me.AppointmentCelendar.ResourcesLabelColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.AppointmentCelendar.ResourcesLabelColorSecond = System.Drawing.Color.OldLace
        Me.AppointmentCelendar.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.AppointmentCelendar.SelectedAppointmentColor = System.Drawing.Color.SkyBlue
        Me.AppointmentCelendar.SelectedColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.AppointmentCelendar.SelectedDayColor = System.Drawing.Color.Tomato
        Me.AppointmentCelendar.Size = New System.Drawing.Size(257, 375)
        Me.AppointmentCelendar.TabIndex = 0
        Me.AppointmentCelendar.TodayMarkerColor = System.Drawing.Color.Blue
        Me.AppointmentCelendar.WorkHourColor = System.Drawing.Color.Cornsilk
        Me.AppointmentCelendar.WorkHourColorSecond = System.Drawing.Color.PeachPuff
        '
        'pnlBottom
        '
        Me.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlBottom.Controls.Add(Me.Label14)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(192, 393)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(257, 24)
        Me.pnlBottom.TabIndex = 2
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Linen
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label14.ForeColor = System.Drawing.Color.Maroon
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(255, 22)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "Double Click on Calendar to add appointment"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlLeft
        '
        Me.pnlLeft.BackgroundImage = CType(resources.GetObject("pnlLeft.BackgroundImage"), System.Drawing.Image)
        Me.pnlLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlLeft.Controls.Add(Me.Panel3)
        Me.pnlLeft.Controls.Add(Me.pnlMiddle)
        Me.pnlLeft.Controls.Add(Me.MonthCalendar1)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(3, 18)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(189, 399)
        Me.pnlLeft.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.Controls.Add(Me.lstCustomizeAppointments)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 190)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(189, 207)
        Me.Panel3.TabIndex = 5
        '
        'lstCustomizeAppointments
        '
        Me.lstCustomizeAppointments.BackColor = System.Drawing.Color.GhostWhite
        Me.lstCustomizeAppointments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstCustomizeAppointments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstCustomizeAppointments.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCustomizeAppointments.ItemHeight = 14
        Me.lstCustomizeAppointments.Location = New System.Drawing.Point(0, 0)
        Me.lstCustomizeAppointments.Name = "lstCustomizeAppointments"
        Me.lstCustomizeAppointments.Size = New System.Drawing.Size(189, 198)
        Me.lstCustomizeAppointments.TabIndex = 0
        '
        'pnlMiddle
        '
        Me.pnlMiddle.BackgroundImage = CType(resources.GetObject("pnlMiddle.BackgroundImage"), System.Drawing.Image)
        Me.pnlMiddle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMiddle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMiddle.Controls.Add(Me.btnDeleteAll)
        Me.pnlMiddle.Controls.Add(Me.btnDeleteCustomizeAppointment)
        Me.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMiddle.Location = New System.Drawing.Point(0, 165)
        Me.pnlMiddle.Name = "pnlMiddle"
        Me.pnlMiddle.Size = New System.Drawing.Size(187, 25)
        Me.pnlMiddle.TabIndex = 4
        '
        'btnDeleteAll
        '
        Me.btnDeleteAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDeleteAll.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnDeleteAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnDeleteAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnDeleteAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeleteAll.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeleteAll.Location = New System.Drawing.Point(0, 0)
        Me.btnDeleteAll.Name = "btnDeleteAll"
        Me.btnDeleteAll.Size = New System.Drawing.Size(99, 23)
        Me.btnDeleteAll.TabIndex = 1
        Me.btnDeleteAll.Text = "Delete All"
        '
        'btnDeleteCustomizeAppointment
        '
        Me.btnDeleteCustomizeAppointment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDeleteCustomizeAppointment.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDeleteCustomizeAppointment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnDeleteCustomizeAppointment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnDeleteCustomizeAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeleteCustomizeAppointment.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeleteCustomizeAppointment.Location = New System.Drawing.Point(119, 0)
        Me.btnDeleteCustomizeAppointment.Name = "btnDeleteCustomizeAppointment"
        Me.btnDeleteCustomizeAppointment.Size = New System.Drawing.Size(66, 23)
        Me.btnDeleteCustomizeAppointment.TabIndex = 0
        Me.btnDeleteCustomizeAppointment.Text = "Delete"
        '
        'MonthCalendar1
        '
        Me.MonthCalendar1.BackColor = System.Drawing.Color.White
        Me.MonthCalendar1.Dock = System.Windows.Forms.DockStyle.Top
        Me.MonthCalendar1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MonthCalendar1.ForeColor = System.Drawing.Color.Maroon
        Me.MonthCalendar1.Location = New System.Drawing.Point(0, 0)
        Me.MonthCalendar1.MaxSelectionCount = 1
        Me.MonthCalendar1.Name = "MonthCalendar1"
        Me.MonthCalendar1.TabIndex = 0
        Me.MonthCalendar1.TitleBackColor = System.Drawing.Color.Orange
        Me.MonthCalendar1.TitleForeColor = System.Drawing.Color.Maroon
        Me.MonthCalendar1.TrailingForeColor = System.Drawing.Color.Tomato
        '
        'grpFutureAppointments
        '
        Me.grpFutureAppointments.BackColor = System.Drawing.Color.White
        Me.grpFutureAppointments.BackgroundImage = CType(resources.GetObject("grpFutureAppointments.BackgroundImage"), System.Drawing.Image)
        Me.grpFutureAppointments.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.grpFutureAppointments.Controls.Add(Me.dgFutureAppointments)
        Me.grpFutureAppointments.Controls.Add(Me.btnModifyAppointment)
        Me.grpFutureAppointments.Controls.Add(Me.btnDeleteAppointment)
        Me.grpFutureAppointments.Controls.Add(Me.lblAppointmentInterval)
        Me.grpFutureAppointments.Controls.Add(Me.Label13)
        Me.grpFutureAppointments.Controls.Add(Me.Label12)
        Me.grpFutureAppointments.Controls.Add(Me.lblAppointmentUpTo)
        Me.grpFutureAppointments.Controls.Add(Me.btnShowFutureAppointments)
        Me.grpFutureAppointments.ForeColor = System.Drawing.Color.Black
        Me.grpFutureAppointments.Location = New System.Drawing.Point(536, 0)
        Me.grpFutureAppointments.Name = "grpFutureAppointments"
        Me.grpFutureAppointments.Size = New System.Drawing.Size(452, 419)
        Me.grpFutureAppointments.TabIndex = 29
        Me.grpFutureAppointments.TabStop = False
        Me.grpFutureAppointments.Text = "Future Appointment Details"
        '
        'dgFutureAppointments
        '
        Me.dgFutureAppointments.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.dgFutureAppointments.BackColor = System.Drawing.Color.GhostWhite
        Me.dgFutureAppointments.BackgroundColor = System.Drawing.Color.GhostWhite
        Me.dgFutureAppointments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dgFutureAppointments.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgFutureAppointments.CaptionFont = New System.Drawing.Font("Verdana", 9.0!)
        Me.dgFutureAppointments.CaptionForeColor = System.Drawing.Color.White
        Me.dgFutureAppointments.CaptionVisible = False
        Me.dgFutureAppointments.ContextMenu = Me.ContextMenu1
        Me.dgFutureAppointments.DataMember = ""
        Me.dgFutureAppointments.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgFutureAppointments.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgFutureAppointments.ForeColor = System.Drawing.Color.Black
        Me.dgFutureAppointments.FullRowSelect = True
        Me.dgFutureAppointments.GridLineColor = System.Drawing.Color.Black
        Me.dgFutureAppointments.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgFutureAppointments.HeaderFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgFutureAppointments.HeaderForeColor = System.Drawing.Color.White
        Me.dgFutureAppointments.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgFutureAppointments.Location = New System.Drawing.Point(3, 100)
        Me.dgFutureAppointments.Name = "dgFutureAppointments"
        Me.dgFutureAppointments.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.dgFutureAppointments.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgFutureAppointments.ReadOnly = True
        Me.dgFutureAppointments.RowHeadersVisible = False
        Me.dgFutureAppointments.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgFutureAppointments.SelectionForeColor = System.Drawing.Color.Black
        Me.dgFutureAppointments.Size = New System.Drawing.Size(446, 316)
        Me.dgFutureAppointments.TabIndex = 1
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuModifyAppointment, Me.MenuItem2, Me.mnuDeleteAppointment})
        '
        'mnuModifyAppointment
        '
        Me.mnuModifyAppointment.Index = 0
        Me.mnuModifyAppointment.Text = "Modify Appointment"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.Text = "-"
        '
        'mnuDeleteAppointment
        '
        Me.mnuDeleteAppointment.Index = 2
        Me.mnuDeleteAppointment.Text = "Remove Appointment"
        '
        'btnModifyAppointment
        '
        Me.btnModifyAppointment.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnModifyAppointment.BackgroundImage = CType(resources.GetObject("btnModifyAppointment.BackgroundImage"), System.Drawing.Image)
        Me.btnModifyAppointment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnModifyAppointment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnModifyAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnModifyAppointment.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnModifyAppointment.Location = New System.Drawing.Point(186, 72)
        Me.btnModifyAppointment.Name = "btnModifyAppointment"
        Me.btnModifyAppointment.Size = New System.Drawing.Size(126, 25)
        Me.btnModifyAppointment.TabIndex = 7
        Me.btnModifyAppointment.Text = "Modify Appointment"
        Me.btnModifyAppointment.UseVisualStyleBackColor = False
        '
        'btnDeleteAppointment
        '
        Me.btnDeleteAppointment.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnDeleteAppointment.BackgroundImage = CType(resources.GetObject("btnDeleteAppointment.BackgroundImage"), System.Drawing.Image)
        Me.btnDeleteAppointment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnDeleteAppointment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnDeleteAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeleteAppointment.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeleteAppointment.Location = New System.Drawing.Point(315, 72)
        Me.btnDeleteAppointment.Name = "btnDeleteAppointment"
        Me.btnDeleteAppointment.Size = New System.Drawing.Size(131, 25)
        Me.btnDeleteAppointment.TabIndex = 6
        Me.btnDeleteAppointment.Text = "Remove Appointment"
        Me.btnDeleteAppointment.UseVisualStyleBackColor = False
        '
        'lblAppointmentInterval
        '
        Me.lblAppointmentInterval.BackColor = System.Drawing.Color.Transparent
        Me.lblAppointmentInterval.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppointmentInterval.Location = New System.Drawing.Point(158, 47)
        Me.lblAppointmentInterval.Name = "lblAppointmentInterval"
        Me.lblAppointmentInterval.Size = New System.Drawing.Size(281, 18)
        Me.lblAppointmentInterval.TabIndex = 5
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(5, 48)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(149, 14)
        Me.Label13.TabIndex = 4
        Me.Label13.Text = "Appointment Interval :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(12, 20)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(136, 14)
        Me.Label12.TabIndex = 3
        Me.Label12.Text = "Appointment Up To :"
        '
        'lblAppointmentUpTo
        '
        Me.lblAppointmentUpTo.BackColor = System.Drawing.Color.Transparent
        Me.lblAppointmentUpTo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppointmentUpTo.Location = New System.Drawing.Point(158, 20)
        Me.lblAppointmentUpTo.Name = "lblAppointmentUpTo"
        Me.lblAppointmentUpTo.Size = New System.Drawing.Size(281, 18)
        Me.lblAppointmentUpTo.TabIndex = 2
        '
        'btnShowFutureAppointments
        '
        Me.btnShowFutureAppointments.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnShowFutureAppointments.BackgroundImage = CType(resources.GetObject("btnShowFutureAppointments.BackgroundImage"), System.Drawing.Image)
        Me.btnShowFutureAppointments.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnShowFutureAppointments.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnShowFutureAppointments.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShowFutureAppointments.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowFutureAppointments.Location = New System.Drawing.Point(8, 71)
        Me.btnShowFutureAppointments.Name = "btnShowFutureAppointments"
        Me.btnShowFutureAppointments.Size = New System.Drawing.Size(77, 25)
        Me.btnShowFutureAppointments.TabIndex = 0
        Me.btnShowFutureAppointments.Text = "Refresh"
        Me.btnShowFutureAppointments.UseVisualStyleBackColor = False
        '
        'pnlSearchPatient
        '
        Me.pnlSearchPatient.BackgroundImage = CType(resources.GetObject("pnlSearchPatient.BackgroundImage"), System.Drawing.Image)
        Me.pnlSearchPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSearchPatient.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSearchPatient.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearchPatient.Name = "pnlSearchPatient"
        Me.pnlSearchPatient.Size = New System.Drawing.Size(989, 437)
        Me.pnlSearchPatient.TabIndex = 4
        Me.pnlSearchPatient.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 52)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(989, 21)
        Me.Panel1.TabIndex = 3
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label15.Location = New System.Drawing.Point(0, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(99, 14)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "Select Patient"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnl_tls
        '
        Me.pnl_tls.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnl_tls.Controls.Add(Me.tlsNewAppointment)
        Me.pnl_tls.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tls.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tls.Name = "pnl_tls"
        Me.pnl_tls.Size = New System.Drawing.Size(989, 52)
        Me.pnl_tls.TabIndex = 4
        '
        'tlsNewAppointment
        '
        Me.tlsNewAppointment.BackColor = System.Drawing.Color.Transparent
        Me.tlsNewAppointment.BackgroundImage = CType(resources.GetObject("tlsNewAppointment.BackgroundImage"), System.Drawing.Image)
        Me.tlsNewAppointment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsNewAppointment.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsNewAppointment.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_Save, Me.btn_tls_Close})
        Me.tlsNewAppointment.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsNewAppointment.Location = New System.Drawing.Point(0, 0)
        Me.tlsNewAppointment.Name = "tlsNewAppointment"
        Me.tlsNewAppointment.Size = New System.Drawing.Size(989, 52)
        Me.tlsNewAppointment.TabIndex = 0
        Me.tlsNewAppointment.Text = "toolStrip1"
        '
        'btn_tls_Save
        '
        Me.btn_tls_Save.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Save.Image = CType(resources.GetObject("btn_tls_Save.Image"), System.Drawing.Image)
        Me.btn_tls_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Save.Name = "btn_tls_Save"
        Me.btn_tls_Save.Size = New System.Drawing.Size(70, 49)
        Me.btn_tls_Save.Tag = "Save"
        Me.btn_tls_Save.Text = "&Save && Cls"
        Me.btn_tls_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_Save.ToolTipText = "Save and Close"
        '
        'btn_tls_Close
        '
        Me.btn_tls_Close.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Close.Image = CType(resources.GetObject("btn_tls_Close.Image"), System.Drawing.Image)
        Me.btn_tls_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Close.Name = "btn_tls_Close"
        Me.btn_tls_Close.Size = New System.Drawing.Size(47, 49)
        Me.btn_tls_Close.Tag = "Close"
        Me.btn_tls_Close.Text = "  &Close"
        Me.btn_tls_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_Close.ToolTipText = "Close"
        '
        'frmAppointment
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(989, 510)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnl_tls)
        Me.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAppointment"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "New Appointment"
        Me.Panel2.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.numAppointmentDuration, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.grpCustomizeAppointments.ResumeLayout(False)
        Me.pnlCustomizeMain.ResumeLayout(False)
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlLeft.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlMiddle.ResumeLayout(False)
        Me.grpFutureAppointments.ResumeLayout(False)
        Me.grpFutureAppointments.PerformLayout()
        CType(Me.dgFutureAppointments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnl_tls.ResumeLayout(False)
        Me.pnl_tls.PerformLayout()
        Me.tlsNewAppointment.ResumeLayout(False)
        Me.tlsNewAppointment.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    'User Clicks on Cancel button
    Private Sub CloseNewAppointment()

        Try
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        Catch objErr As Exception
            Me.DialogResult = DialogResult.Cancel
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'User clicks on OK button
    Private Sub SaveNewAppointment()

        'Save code goes here
        '''''<><><><><> Check Patient Status <><><><><><>''''
        ''''' 20070125 -Mahesh 
        If CheckPatientStatus(gnPatientID) = False Then
            Exit Sub
        End If
        '''''<><><><><> Check Patient Status <><><><><><>''''

        'Check Appointment Duration Interval is less than 0 or not
        If numAppointmentDuration.Value <= 0 Then
            MessageBox.Show("Appointment interval can not be less than or equal to 0", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            numAppointmentDuration.Focus()
            Exit Sub
        End If
        'Local Variable
        Dim Arrlist As New ArrayList
        Try
            'If Trim(txtPatientCode.Text) = "" Then
            'MsgBox("Patientcode Required")
            'txtPatientCode.Focus()
            'Exit Sub
            'End If

            'Check cLINIC TIME
            Dim nAppointmentTime As Long
            Dim nStartTime As Long
            Dim nEndTime As Long

            'Save Reason of Visit
            objclsAppointment.ReasonofVisit = txtChiefComplaints.Text

            'Retrieve Clinic Start Time
            nStartTime = New TimeSpan(gClinicStartTime.Hour, gClinicStartTime.Minute, gClinicStartTime.Second).Ticks

            'Retrieve Clinic End Time
            nEndTime = New TimeSpan(gClinicEndTime.Hour, gClinicEndTime.Minute, gClinicEndTime.Second).Ticks

            'Retrieve Appointment Time
            nAppointmentTime = New TimeSpan(dtpTime.Value.Hour, dtpTime.Value.Minute, dtpTime.Value.Second).Ticks

            'Check Appointment Module Level
            If gnAppointmentModuleLevel <> 0 Then

                'Check Clinic Working Time with Appointment Time
                If nAppointmentTime < nStartTime Or nAppointmentTime >= nEndTime Then
                    MessageBox.Show("Clinic Timing is " & Format(gClinicStartTime, "Medium Time") & " To " & Format(gClinicEndTime, "Medium Time") & "." & vbCrLf & "You can not add appointment at " & dtpTime.Value, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                'Check Appointment can be set before closing clinic or not
                Dim dtAppointmentEndTime As DateTime

                dtAppointmentEndTime = CType(dtpDate.Value.Date & " " & Format(dtpTime.Value, "Medium Time"), DateTime).AddMinutes(numAppointmentDuration.Value)
                nAppointmentTime = New TimeSpan(dtAppointmentEndTime.Hour, dtAppointmentEndTime.Minute, dtAppointmentEndTime.Second).Ticks
                If nAppointmentTime > nEndTime Then
                    MessageBox.Show("Clinic Timing is " & Format(gClinicStartTime, "Medium Time") & " To " & Format(gClinicEndTime, "Medium Time") & "." & vbCrLf & "So appointment is not available at " & dtpTime.Value & " for " & numAppointmentDuration.Value & " minutes", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                'Change as per Yaw

                'Check Appointment Time is Free or not
                If objclsAppointment.CheckAppointmentAvailable(dtpDate.Value.Date & " " & Format(dtpTime.Value, "Medium Time"), numAppointmentDuration.Value, cmbProvider.Text, lblAppointmentID.Text) = False Then
                    MessageBox.Show("Appointment is not available at " & dtpDate.Value.Date & " " & Format(dtpTime.Value, "Medium Time") & " for " & cmbProvider.Text, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                'Check Doctor is Vailable or not
                Dim objDoctor As New clsDoctorHolidaySchedule
                If objDoctor.IsDoctorAvailable(Trim(cmbProvider.Text), dtpDate.Value.Date & " " & Format(dtpTime.Value, "Medium Time")) = False Then
                    MessageBox.Show(cmbProvider.Text & " is not available at this time", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    objDoctor = Nothing
                    Exit Sub
                End If
                objDoctor = Nothing

            End If

            'Retrieve Max Group ID
            Dim nMaxAppointmentGroupID As Integer
            nMaxAppointmentGroupID = objclsAppointment.MaxAppointmentGroupID() + 1
            If lblAppointmentID.Text = 0 And CType(dgFutureAppointments.DataSource, DataTable).Rows.Count >= 1 Then
                'Set the Appointment Group ID
                SetData(Arrlist, -1, nMaxAppointmentGroupID)
            Else
                'Set Appointment
                SetData(Arrlist)
            End If

            'Add Appointment
            objclsAppointment.AddData(Arrlist)
            Dim nCount As Integer
            'Check Appointment is Customize Appointments or not
            If chkCustomizeAppointment.Checked = True Then
                For nCount = 0 To lstCustomizeAppointments.Items.Count - 1
                    Arrlist.Clear()
                    SetData(Arrlist, nCount, 0, True)
                    objclsAppointment.AddData(Arrlist)
                Next
            Else
                'Appointments is not customize appointments
                If lblAppointmentID.Text = 0 And CType(dgFutureAppointments.DataSource, DataTable).Rows.Count >= 1 Then
                    For nCount = 0 To CType(dgFutureAppointments.DataSource, DataTable).Rows.Count - 1
                        If CType(dgFutureAppointments.DataSource, DataTable).Rows(nCount).Item(2) = "Yes" Then
                            Arrlist.Clear()
                            SetData(Arrlist, nCount, nMaxAppointmentGroupID)
                            objclsAppointment.AddData(Arrlist)
                        End If
                    Next
                End If
            End If
            Me.DialogResult = DialogResult.OK
            'Close the form
            Me.Close()
        Catch ex As SqlClient.SqlException
            Me.DialogResult = DialogResult.Cancel
            MessageBox.Show(ex.Message, "Appointment Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            Me.DialogResult = DialogResult.Cancel
            MessageBox.Show(ex.Message, "Appointment Registration", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Loading the application
    Private Sub frmAppointment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Open Connection
            'Fill_Provider()
            ''dtpDate.Value = frmViewAppointments.dtAppointmentDate
            ''dtpTime.Value = frmViewAppointments.dtAppointmentTime
            'dtpDate.Focus()
            'dtpTime.Value = Now

            'txtPatientCode.Tag = gnPatientID
            'txtPatientCode.Text = gstrPatientCode
            'txtFirstName.Text = gstrPatientFirstName
            'txtLastName.Text = gstrPatientLastName

            'Add columns in data table
            dtAppointment.Columns.Add(clmnAppointmentDate)
            dtAppointment.Columns.Add(clmnAppointmentTime)
            dtAppointment.Columns.Add(clmnIsAvailable)
            dtAppointment.Columns.Add(clmnDoctor)


            dgFutureAppointments.DataSource = dtAppointment

            'Formatting Data Grid
            Dim grdTableStyle As New clsDataGridTableStyle(dtAppointment.TableName)


            Dim grdColStyleAppointmentDate As New DataGridTextBoxColumn
            With grdColStyleAppointmentDate
                .HeaderText = "Date"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAppointment.Columns(0).ColumnName
                .NullText = ""
                .Width = 0.25 * dgFutureAppointments.Width
            End With

            Dim grdColStyleAppointmentTime As New DataGridTextBoxColumn
            With grdColStyleAppointmentTime
                .HeaderText = "Time"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAppointment.Columns(1).ColumnName
                .NullText = ""
                .Width = 0.25 * dgFutureAppointments.Width
            End With


            Dim grdColStyleIsAvailable As New DataGridTextBoxColumn
            With grdColStyleIsAvailable
                .HeaderText = "Is Available"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAppointment.Columns(2).ColumnName
                .NullText = ""
                .Width = 0.2 * dgFutureAppointments.Width
            End With

            Dim grdColStyleDoctor As New DataGridTextBoxColumn
            With grdColStyleDoctor
                .HeaderText = "Doctor"
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtAppointment.Columns(3).ColumnName
                .NullText = ""
                .Width = 0.25 * dgFutureAppointments.Width
            End With

            grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleAppointmentDate, grdColStyleAppointmentTime, grdColStyleIsAvailable, grdColStyleDoctor})
            dgFutureAppointments.TableStyles.Clear()
            dgFutureAppointments.TableStyles.Add(grdTableStyle)

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Width = 550
            Me_Width = Me.Width
        End Try

    End Sub

    'Fill Providers
    Public Sub Fill_Provider()
        Dim dt As DataTable = objclsAppointment.GetAllProvider()
        cmbProvider.DataSource = dt
        cmbProvider.ValueMember = dt.Columns(0).ColumnName
        cmbProvider.DisplayMember = dt.Columns(1).ColumnName
        '& " " & objclsTemplateGallery.GetAllProvider.Table.Columns(2).ColumnName & " " & objclsTemplateGallery.GetAllProvider.Table.Columns(3).ColumnName
        'cmbProvider.SelectedIndex = -1
    End Sub

    'Settings Appointments
    Private Sub SetData(ByRef Arrlist As ArrayList, Optional ByVal nRowNo As Int16 = -1, Optional ByVal nAppointmentGroupID As Integer = 0, Optional ByVal blnCustomizeAppointment As Boolean = False)
        Arrlist.Add(lblAppointmentID.Text)
        Arrlist.Add(txtPatientCode.Tag)

        'Arrlist.Add(Format(dtpDate.Value, "D"))
        If nRowNo <> -1 Then
            'Check Appointments are Future Appointments or Customize appointments
            If blnCustomizeAppointment = False Then
                'Appointments are Future Appointments
                'Find Provider ID
                Dim objProvider As New clsProvider
                Dim nProviderID As Integer
                If Trim(CType(dgFutureAppointments.DataSource, DataTable).Rows(nRowNo).Item(3)) <> "" Then
                    nProviderID = objProvider.RetrieveProviderID(CType(dgFutureAppointments.DataSource, DataTable).Rows(nRowNo).Item(3))
                End If
                Arrlist.Add(nProviderID)
                Arrlist.Add(CType(CType(dgFutureAppointments.DataSource, DataTable).Rows(nRowNo).Item(0), Date) & " " & Format(CType(dgFutureAppointments.DataSource, DataTable).Rows(nRowNo).Item(1), "Medium Time"))
            Else
                'Appointments are Customize Appointments
                Arrlist.Add(cmbProvider.SelectedValue)
                Arrlist.Add(lstCustomizeAppointments.Items(nRowNo))
            End If
        Else
            Arrlist.Add(cmbProvider.SelectedValue)
            Arrlist.Add(dtpDate.Value.Date & " " & Format(dtpTime.Value, "Medium Time"))
        End If

        'Arrlist.Add(Format(dtpTime.Value, "Medium Time"))
        Arrlist.Add(txtChiefComplaints.Text)
        If optPhone.Checked = True Then
            Arrlist.Add(optPhone.Text)
        ElseIf optReferral.Checked = True Then
            Arrlist.Add(optReferral.Text)
        ElseIf optEmail.Checked = True Then
            Arrlist.Add(optEmail.Text)
        ElseIf optInPerson.Checked = True Then
            Arrlist.Add(optInPerson.Text)
        End If
        Arrlist.Add(numAppointmentDuration.Value)
        Arrlist.Add(cmbAppointmentTypes.Text)

        'Set this appointments is not from PULL Charts
        Arrlist.Add(0)
        'Appointement Group id
        Arrlist.Add(nAppointmentGroupID)

        'Appointment Color
        Arrlist.Add("")

        'Arrlist.Add(Trim(txtReferringPhysicain.Text))
    End Sub

    'Search the Patient
    Private Sub btnSearchPatient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPatient.Click
        Dim objPatient As New clsPatient
        Try
            pnlMain.Visible = False
            pnl_tls.Visible = False
            If Not IsNothing(objPatientControl) Then
                Me.Controls.Remove(objPatientControl)
                objPatientControl.Visible = False
                objPatientControl = Nothing
                rowindex = 0
            End If
            'dtPatient = objPatient.Fill_Patients(  "", clsPatient.enmPatientSearchCriteria.PatientCode)
            objPatientControl = New gloPatientDataGrid("", "") ' False)
            ReferralCount = dtPatient.Rows.Count - 1

            'If CType(dgFutureAppointments.DataSource, DataTable).Rows.Count > 1 Then
            '    objPatientControl.ResizeColumnWidth(1000)
            'Else
            '    objPatientControl.ResizeColumnWidth(550)
            'End If
            If Me.Width = 550 Then Me.Width = 1000
            Me.pnlSearchPatient.Controls.Add(objPatientControl)
            objPatientControl.Dock = DockStyle.Fill
            objPatientControl.Visible = True
            objPatientControl.BringToFront()
            pnlSearchPatient.Visible = True


        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    'Code commented by supriya datagrid replaced with C1flexgrid usercontrol

    'Clicking on OK button
    'Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    '    Try
    '        If dgData.CurrentRowIndex >= 0 Then
    '            '''''<><><><><> Check Patient Status <><><><><><>''''
    '            ''''' 20070125 -Mahesh 
    '            If CheckPatientStatus(gnPatientID) = False Then
    '                Exit Sub
    '            End If
    '            '''''<><><><><> Check Patient Status <><><><><><>''''

    '            pnlMain.Visible = True
    '            pnlMainCommands.Visible = True
    '            pnlSearchPatient.Visible = False
    '            txtPatientCode.Tag = dgData.Item(dgData.CurrentRowIndex, 0)
    '            txtPatientCode.Text = dgData.Item(dgData.CurrentRowIndex, 1)
    '            txtFirstName.Text = dgData.Item(dgData.CurrentRowIndex, 2)
    '            txtLastName.Text = dgData.Item(dgData.CurrentRowIndex, 3)
    '        Else
    '            MessageBox.Show("Please select Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        End If
    '    Catch objErr As Exception
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub
    'Code commented by supriya datagrid replaced with C1flexgrid usercontrol

    'User clicked on Cancel button
    'Code commented by supriya datagrid replaced with C1flexgrid usercontrol

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Try
    '        pnlMain.Visible = True
    '        pnlMainCommands.Visible = True
    '        pnlSearchPatient.Visible = False
    '    Catch objErr As Exception
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub
    'Code commented by supriya datagrid replaced with C1flexgrid usercontrol

    'To search Patient
    'Code commented by supriya datagrid replaced with C1flexgrid usercontrol
    'Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
    '    Try
    '        'Check character is Enter Key
    '        If e.KeyChar = ChrW(Keys.Enter) Then
    '            'Check Text Box is blank or not
    '            If Trim(txtSearch.Text) <> "" Then
    '                Dim dtPatient As DataTable
    '                Dim objPatient As New clsPatient
    '                If optPatientCode.Checked = True Then
    '                    'Search on Patient Code
    '                    dtPatient = objPatient.Fill_Patients(Trim(txtSearch.Text), clsPatient.enmPatientSearchCriteria.PatientCode)
    '                ElseIf optFirstName.Checked = True Then
    '                    'Search on Patient First Name
    '                    dtPatient = objPatient.Fill_Patients(Trim(txtSearch.Text), clsPatient.enmPatientSearchCriteria.PatientFirstName)
    '                Else
    '                    'Search on Patient Last Name
    '                    dtPatient = objPatient.Fill_Patients(Trim(txtSearch.Text), clsPatient.enmPatientSearchCriteria.PatientLastName)
    '                End If
    '                objPatient = Nothing
    '                'Bind to Grid
    '                dgData.DataSource = dtPatient

    '                'Formatting the Data Grid
    '                Dim grdTableStyle As New clsDataGridTableStyle(dtPatient.TableName)

    '                Dim grdColStylePatientID As New DataGridTextBoxColumn
    '                With grdColStylePatientID
    '                    .HeaderText = "Patient ID"
    '                    .Alignment = HorizontalAlignment.Left
    '                    .MappingName = dtPatient.Columns(0).ColumnName
    '                    .NullText = ""
    '                    .Width = 0
    '                End With

    '                Dim grdColStylePatientCode As New DataGridTextBoxColumn
    '                With grdColStylePatientCode
    '                    .HeaderText = "Patient ID"
    '                    .Alignment = HorizontalAlignment.Left
    '                    .MappingName = dtPatient.Columns(1).ColumnName
    '                    .NullText = ""
    '                    .Width = 0.33 * dgData.Width
    '                End With


    '                Dim grdColStylePatientFirstName As New DataGridTextBoxColumn
    '                With grdColStylePatientFirstName
    '                    .HeaderText = "First Name"
    '                    .Alignment = HorizontalAlignment.Left
    '                    .MappingName = dtPatient.Columns(2).ColumnName
    '                    .NullText = ""
    '                    .Width = 0.33 * dgData.Width
    '                End With

    '                Dim grdColStylePatientLastName As New DataGridTextBoxColumn
    '                With grdColStylePatientLastName
    '                    .HeaderText = "Last Name"
    '                    .Alignment = HorizontalAlignment.Left
    '                    .MappingName = dtPatient.Columns(4).ColumnName
    '                    .NullText = ""
    '                    .Width = 0.33 * dgData.Width - 5
    '                End With


    '                grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStylePatientID, grdColStylePatientCode, grdColStylePatientFirstName, grdColStylePatientLastName})
    '                dgData.TableStyles.Clear()
    '                dgData.TableStyles.Add(grdTableStyle)

    '            End If
    '        End If
    '    Catch objErr As Exception
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub
    'Code commented by supriya datagrid replaced with C1flexgrid usercontrol


    'Code commented by supriya datagrid replaced with C1flexgrid usercontrol
    'Select the Patient
    'Private Sub dgData_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgData.DoubleClick
    '    Try
    '        If dgData.CurrentRowIndex >= 0 Then
    '            '''''<><><><><> Check Patient Status <><><><><><>''''
    '            ''''' 20070125 -Mahesh 
    '            If CheckPatientStatus(gnPatientID) = False Then
    '                Exit Sub
    '            End If
    '            '''''<><><><><> Check Patient Status <><><><><><>''''

    '            'Display the Patient details
    '            pnlMain.Visible = True
    '            pnlMainCommands.Visible = True
    '            pnlSearchPatient.Visible = False
    '            txtPatientCode.Tag = dgData.Item(dgData.CurrentRowIndex, 0)
    '            txtPatientCode.Text = dgData.Item(dgData.CurrentRowIndex, 1)
    '            txtFirstName.Text = dgData.Item(dgData.CurrentRowIndex, 2)
    '            txtLastName.Text = dgData.Item(dgData.CurrentRowIndex, 3)
    '        Else
    '            MessageBox.Show("Please select Patient", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        End If
    '    Catch objErr As Exception
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub
    'Code commented by supriya datagrid replaced with C1flexgrid usercontrol

    'Fill Appointment Scheduler Types
    Public Sub Fill_AppointmentSchedulerTypes()
        Try
            With cmbAppointmentTypes
                .Items.Clear()
                .Items.Add("None")
                Dim clAppointmentTypes As New Collection
                Dim objAppointmentType As New clsAppointmentScheduler
                clAppointmentTypes = objAppointmentType.FillAppointmentSchedulerTypes()
                objAppointmentType = Nothing
                Dim nCount As Integer
                For nCount = 1 To clAppointmentTypes.Count
                    .Items.Add(clAppointmentTypes.Item(nCount))
                Next
                ''''''''******This IF statement is added by Anil on 09/10/2007 , Because for blankDB it was giving error
                If clAppointmentTypes.Count > 0 Then
                    .SelectedIndex = 0
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Fill_AppointmentSchedulerTypes", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Fill the apppointments in Grid as per selected Appointment Schedular type
    Private Sub cmbAppointmentTypes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAppointmentTypes.SelectedIndexChanged
        Try
            If Trim(cmbAppointmentTypes.SelectedItem) <> "None" Then
                ''Anil 20071221, to disable the checkbox for Appointments other than "None".
                chkCustomizeAppointment.Enabled = False
            Else
                chkCustomizeAppointment.Enabled = True
                ''
            End If
            If blnAppointmentTypeChange = True Then
                If cmbAppointmentTypes.SelectedIndex = 0 Then
                    numAppointmentDuration.Value = gnAppointmentInterval
                    grpFutureAppointments.Enabled = False
                    dtAppointment.Rows.Clear()
                    'CType(dgFutureAppointments.DataSource, DataTable).Rows.Clear()
                    lblAppointmentUpTo.Text = ""
                    lblAppointmentInterval.Text = ""
                    lblMessage.Visible = False
                Else
                    'grpFutureAppointments.Show()
                    grpFutureAppointments.Enabled = True
                    Dim objAppointment As New clsAppointmentScheduler
                    objAppointment.FillAppointmentSchedulerTypesDetails(cmbAppointmentTypes.Text)
                    numAppointmentDuration.Value = objAppointment.AppointmentDuration
                    Select Case objAppointment.AppointmentUpToDurationType
                        Case clsAppointmentScheduler.enmAppointmentUpToDurationType.Days
                            lblAppointmentUpTo.Text = objAppointment.AppointmentUpToDuration & " Days"
                        Case clsAppointmentScheduler.enmAppointmentUpToDurationType.Weeks
                            lblAppointmentUpTo.Text = objAppointment.AppointmentUpToDuration & " Weeks"
                        Case clsAppointmentScheduler.enmAppointmentUpToDurationType.Months
                            lblAppointmentUpTo.Text = objAppointment.AppointmentUpToDuration & " Months"
                    End Select

                    Select Case objAppointment.AppointmentIntervalType
                        Case clsAppointmentScheduler.enmAppointmentIntervalType.Daily
                            lblAppointmentInterval.Text = "Daily - After Every " & objAppointment.AppointmentInterval & " Days"
                        Case clsAppointmentScheduler.enmAppointmentIntervalType.Weekly
                            Dim strInterval As String
                            strInterval = "Weekly - On Every "
                            If objAppointment.AppointmentInterval Mod 2 = 0 Then
                                strInterval = strInterval & " Monday,"
                            End If
                            If objAppointment.AppointmentInterval Mod 3 = 0 Then
                                strInterval = strInterval & " Tuesday,"
                            End If
                            If objAppointment.AppointmentInterval Mod 5 = 0 Then
                                strInterval = strInterval & " Wednesday,"
                            End If
                            If objAppointment.AppointmentInterval Mod 7 = 0 Then
                                strInterval = strInterval & " Thursday,"
                            End If
                            If objAppointment.AppointmentInterval Mod 11 = 0 Then
                                strInterval = strInterval & " Friday,"
                            End If
                            If objAppointment.AppointmentInterval Mod 13 = 0 Then
                                strInterval = strInterval & " Saturday,"
                            End If
                            If objAppointment.AppointmentInterval Mod 17 = 0 Then
                                strInterval = strInterval & " Sunday,"
                            End If
                            strInterval = Mid(strInterval, 1, Len(strInterval) - 1)
                            lblAppointmentInterval.Text = strInterval

                        Case clsAppointmentScheduler.enmAppointmentIntervalType.Monthly
                            lblAppointmentInterval.Text = "Monthly - After Every " & objAppointment.AppointmentInterval & " Months"
                    End Select
                    objAppointment = Nothing
                    Call Fill_FutureAppointments()
                End If
            End If

            If Not dgFutureAppointments.DataSource Is Nothing Then
                If CType(dgFutureAppointments.DataSource, DataTable).Rows.Count >= 1 Then
                    If Not Me.Width = 1000 Then Me.Width = 1000
                Else
                    If Not Me.Width = 550 Then Me.Width = 550
                End If
            Else
                If Not Me.Width = 1000 Then Me.Width = 1000
            End If


        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'Fill Future Appointments
    Private Sub Fill_FutureAppointments()
        If Trim(cmbAppointmentTypes.SelectedItem) <> "None" Then
            ''Anil 20071221, to disable the checkbox for Appointments other than "None".
            chkCustomizeAppointment.Enabled = False
            ''
            If gnAppointmentModuleLevel = 0 Then
                'Fill Future Appointments level 0
                Call Fill_FutureAppointments_WithLevel0()
            Else
                'Fill Future Appointments for level 1
                Call Fill_FutureAppointments_WithLevel1()
            End If
        Else
            chkCustomizeAppointment.Enabled = True
        End If
    End Sub
    'Fill_Future Appointments With Level 1
    Private Sub Fill_FutureAppointments_WithLevel1()
        'Clear the table
        dtAppointment.Rows.Clear()

        'Check Clinic Timings
        If numAppointmentDuration.Value <= 0 Then
            MessageBox.Show("Appointment interval can not be less than or equal to 0", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            numAppointmentDuration.Focus()
            Exit Sub
        End If
        Dim nAppointmentTime As Long
        Dim nStartTime As Long
        Dim nEndTime As Long

        'Retrieve Clinic Starting Time
        nStartTime = New TimeSpan(gClinicStartTime.Hour, gClinicStartTime.Minute, gClinicStartTime.Second).Ticks
        'Retrieve Clinic Closing Time
        nEndTime = New TimeSpan(gClinicEndTime.Hour, gClinicEndTime.Minute, gClinicEndTime.Second).Ticks
        'Retrieve Appointment Time
        nAppointmentTime = New TimeSpan(dtpTime.Value.Hour, dtpTime.Value.Minute, dtpTime.Value.Second).Ticks

        'Check Appointment Module Level
        If nAppointmentTime < nStartTime Or nAppointmentTime >= nEndTime Then
            lblMessage.Visible = True
            lblMessage.Text = "Clinic Timing is " & Format(gClinicStartTime, "Medium Time") & " To " & Format(gClinicEndTime, "Medium Time") & ". So appointment can not be set at " & dtpTime.Value
            Exit Sub
        End If
        'Check Appointment can be set before closing clinic or not
        Dim dtAppointmentEndTime As DateTime
        dtAppointmentEndTime = CType(dtpDate.Value.Date & " " & Format(dtpTime.Value, "Medium Time"), DateTime).AddMinutes(numAppointmentDuration.Value)
        nAppointmentTime = New TimeSpan(dtAppointmentEndTime.Hour, dtAppointmentEndTime.Minute, dtAppointmentEndTime.Second).Ticks
        If nAppointmentTime > nEndTime Then
            lblMessage.Visible = True
            lblMessage.Text = "Clinic Timing is " & Format(gClinicStartTime, "Medium Time") & " To " & Format(gClinicEndTime, "Medium Time") & ". So appointment can not be set at " & dtpTime.Value & " for " & numAppointmentDuration.Value & " minutes"
            Exit Sub
        End If







        lblMessage.Visible = False

        Dim dtFromDate As DateTime
        Dim dtAppointmentDate As DateTime
        Dim dtToDate As DateTime

        dtFromDate = dtpDate.Value
        dtAppointmentDate = dtFromDate


        Dim objDoctor As New clsDoctorHolidaySchedule
        Dim objAppointment As New clsAppointmentScheduler
        'Retrieve Appointments
        objAppointment.FillAppointmentSchedulerTypesDetails(cmbAppointmentTypes.Text)
        Select Case objAppointment.AppointmentUpToDurationType
            Case clsAppointmentScheduler.enmAppointmentUpToDurationType.Days
                dtToDate = dtFromDate.AddDays(objAppointment.AppointmentUpToDuration)
            Case clsAppointmentScheduler.enmAppointmentUpToDurationType.Weeks
                dtToDate = dtFromDate.AddDays(objAppointment.AppointmentUpToDuration * 7)
            Case clsAppointmentScheduler.enmAppointmentUpToDurationType.Months
                dtToDate = dtFromDate.AddMonths(objAppointment.AppointmentUpToDuration)
        End Select
        '' 20071116 Mahesh
        dtToDate = dtToDate.AddDays(-1)
        ''
        Dim drRow As DataRow
        Select Case objAppointment.AppointmentIntervalType
            Case clsAppointmentScheduler.enmAppointmentIntervalType.Daily
                While 1
                    dtAppointmentDate = dtAppointmentDate.AddDays(objAppointment.AppointmentInterval)
                    If dtAppointmentDate > dtToDate Then
                        Exit While
                    End If
                    drRow = dtAppointment.NewRow
                    drRow(0) = Format(dtAppointmentDate, "Short Date") ' & " - " & WeekdayName(Weekday(dtAppointmentDate))
                    drRow(1) = Format(dtpTime.Value, "Medium Time")
                    If objDoctor.IsDoctorAvailable(cmbProvider.Text, Format(dtAppointmentDate, "Short Date") & " " & Format(dtpTime.Value, "Medium Time")) = False Then
                        'Default Doctor is not available.
                        'So check for another doctors
                        strFutureAppointmentProviderName = ""
                        Call SetFutureAppointment(Format(dtAppointmentDate, "Short Date") & " " & Format(dtpTime.Value, "Medium Time"), Trim(cmbProvider.Text))
                        If Trim(strFutureAppointmentProviderName) <> "" Then
                            drRow(2) = "Yes"
                            drRow(3) = strFutureAppointmentProviderName
                            If Trim(strFutureAppointmentTime) <> "" Then
                                drRow(1) = strFutureAppointmentTime
                            End If
                        Else
                            drRow(2) = "No"
                            drRow(3) = ""
                        End If
                    Else
                        'Doctor is available.
                        'Now check Appointment is available or not
                        If objclsAppointment.CheckAppointmentAvailable(dtAppointmentDate.Date & " " & Format(dtpTime.Value, "Medium Time"), numAppointmentDuration.Value, cmbProvider.Text) = False Then
                            'Appointment is not available. So check for other doctors
                            strFutureAppointmentProviderName = ""
                            Call SetFutureAppointment(Format(dtAppointmentDate, "Short Date") & " " & Format(dtpTime.Value, "Medium Time"), Trim(cmbProvider.Text))
                            If Trim(strFutureAppointmentProviderName) <> "" Then
                                drRow(2) = "Yes"
                                drRow(3) = strFutureAppointmentProviderName
                                If Trim(strFutureAppointmentTime) <> "" Then
                                    drRow(1) = strFutureAppointmentTime
                                End If

                            Else
                                drRow(2) = "No"
                                drRow(3) = ""
                            End If
                        Else
                            drRow(2) = "Yes"
                            drRow(3) = cmbProvider.Text
                        End If
                    End If
                    dtAppointment.Rows.Add(drRow)
                End While
            Case clsAppointmentScheduler.enmAppointmentIntervalType.Weekly
                Dim nWeekDay As Byte
                While 1
                    dtAppointmentDate = dtAppointmentDate.AddDays(1)
                    If dtAppointmentDate > dtToDate Then
                        Exit While
                    End If
                    'Retrieve Week Day
                    Select Case Weekday(dtAppointmentDate)
                        Case 1
                            nWeekDay = 17
                        Case 2
                            nWeekDay = 2
                        Case 3
                            nWeekDay = 3
                        Case 4
                            nWeekDay = 5
                        Case 5
                            nWeekDay = 7
                        Case 6
                            nWeekDay = 11
                        Case 7
                            nWeekDay = 13
                    End Select

                    If objAppointment.AppointmentInterval Mod nWeekDay = 0 Then
                        drRow = dtAppointment.NewRow
                        drRow(0) = Format(dtAppointmentDate, "Short Date") '& " - " & WeekdayName(Weekday(dtAppointmentDate))
                        drRow(1) = Format(dtpTime.Value, "Medium Time")
                        If objDoctor.IsDoctorAvailable(cmbProvider.Text, Format(dtAppointmentDate, "Short Date") & " " & Format(dtpTime.Value, "Medium Time")) = False Then
                            'Default Doctor is not available.
                            'Check for other doctors
                            strFutureAppointmentProviderName = ""
                            Call SetFutureAppointment(Format(dtAppointmentDate, "Short Date") & " " & Format(dtpTime.Value, "Medium Time"), Trim(cmbProvider.Text))
                            If Trim(strFutureAppointmentProviderName) <> "" Then
                                drRow(2) = "Yes"
                                drRow(3) = strFutureAppointmentProviderName
                                If Trim(strFutureAppointmentTime) <> "" Then
                                    drRow(1) = strFutureAppointmentTime
                                End If
                            Else
                                drRow(2) = "No"
                                drRow(3) = ""
                            End If
                        Else
                            'Doctor is available.
                            'Now check Appointment is available or not
                            If objclsAppointment.CheckAppointmentAvailable(dtAppointmentDate.Date & " " & Format(dtpTime.Value, "Medium Time"), numAppointmentDuration.Value, cmbProvider.Text) = False Then
                                'Appointment is not available
                                'Check for other doctors
                                strFutureAppointmentProviderName = ""
                                Call SetFutureAppointment(Format(dtAppointmentDate, "Short Date") & " " & Format(dtpTime.Value, "Medium Time"), Trim(cmbProvider.Text))
                                If Trim(strFutureAppointmentProviderName) <> "" Then
                                    drRow(2) = "Yes"
                                    drRow(3) = strFutureAppointmentProviderName
                                    If Trim(strFutureAppointmentTime) <> "" Then
                                        drRow(1) = strFutureAppointmentTime
                                    End If
                                Else
                                    drRow(2) = "No"
                                    drRow(3) = ""
                                End If
                            Else
                                drRow(2) = "Yes"
                                drRow(3) = cmbProvider.Text
                            End If
                        End If
                        dtAppointment.Rows.Add(drRow)
                    End If
                End While
                'Appointment is Monthly
            Case clsAppointmentScheduler.enmAppointmentIntervalType.Monthly
                While 1
                    dtAppointmentDate = dtAppointmentDate.AddMonths(objAppointment.AppointmentInterval)
                    If dtAppointmentDate > dtToDate Then
                        Exit While
                    End If
                    drRow = dtAppointment.NewRow
                    drRow(0) = Format(dtAppointmentDate, "Short Date") '& " - " & WeekdayName(Weekday(dtAppointmentDate))
                    drRow(1) = Format(dtpTime.Value, "Medium Time")
                    'Check Provider is available or not
                    If objDoctor.IsDoctorAvailable(cmbProvider.Text, Format(dtAppointmentDate, "Short Date") & " " & Format(dtpTime.Value, "Medium Time")) = False Then
                        'Doctor is not available
                        'Check for other doctors
                        strFutureAppointmentProviderName = ""
                        Call SetFutureAppointment(Format(dtAppointmentDate, "Short Date") & " " & Format(dtpTime.Value, "Medium Time"), Trim(cmbProvider.Text))
                        If Trim(strFutureAppointmentProviderName) <> "" Then
                            drRow(2) = "Yes"
                            drRow(3) = strFutureAppointmentProviderName
                            If Trim(strFutureAppointmentTime) <> "" Then
                                drRow(1) = strFutureAppointmentTime
                            End If
                        Else
                            drRow(2) = "No"
                            drRow(3) = ""
                        End If
                    Else
                        'Doctor is available.
                        'Now check Appointment is available or not
                        If objclsAppointment.CheckAppointmentAvailable(dtAppointmentDate.Date & " " & Format(dtpTime.Value, "Medium Time"), numAppointmentDuration.Value, cmbProvider.Text) = False Then
                            'Appointment is not available
                            'Check for other doctors
                            strFutureAppointmentProviderName = ""
                            Call SetFutureAppointment(Format(dtAppointmentDate, "Short Date") & " " & Format(dtpTime.Value, "Medium Time"), Trim(cmbProvider.Text))
                            If Trim(strFutureAppointmentProviderName) <> "" Then
                                drRow(2) = "Yes"
                                drRow(3) = strFutureAppointmentProviderName
                                If Trim(strFutureAppointmentTime) <> "" Then
                                    drRow(1) = strFutureAppointmentTime
                                End If
                            Else
                                drRow(2) = "No"
                                drRow(3) = ""
                            End If
                        Else
                            drRow(2) = "Yes"
                            drRow(3) = cmbProvider.Text
                        End If
                    End If
                    dtAppointment.Rows.Add(drRow)

                End While
        End Select
        objDoctor = Nothing
        objAppointment = Nothing
    End Sub

    'Fill Future Appointments With Level 0
    Private Sub Fill_FutureAppointments_WithLevel0()
        'Clear the data table
        dtAppointment.Rows.Clear()

        'Check Clinic Timings
        If numAppointmentDuration.Value <= 0 Then
            MessageBox.Show("Appointment interval can not be less than or equal to 0", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            numAppointmentDuration.Focus()
            Exit Sub
        End If
        Dim nAppointmentTime As Long
        Dim nStartTime As Long
        Dim nEndTime As Long

        nStartTime = New TimeSpan(gClinicStartTime.Hour, gClinicStartTime.Minute, gClinicStartTime.Second).Ticks
        nEndTime = New TimeSpan(gClinicEndTime.Hour, gClinicEndTime.Minute, gClinicEndTime.Second).Ticks
        nAppointmentTime = New TimeSpan(dtpTime.Value.Hour, dtpTime.Value.Minute, dtpTime.Value.Second).Ticks

        lblMessage.Visible = False

        Dim dtFromDate As DateTime
        Dim dtAppointmentDate As DateTime
        Dim dtToDate As DateTime

        dtFromDate = dtpDate.Value
        dtAppointmentDate = dtFromDate

        'Dim objDoctor As New clsDoctorHolidaySchedule
        Dim objAppointment As New clsAppointmentScheduler
        objAppointment.FillAppointmentSchedulerTypesDetails(cmbAppointmentTypes.Text)
        Select Case objAppointment.AppointmentUpToDurationType
            Case clsAppointmentScheduler.enmAppointmentUpToDurationType.Days
                dtToDate = dtFromDate.AddDays(objAppointment.AppointmentUpToDuration)
            Case clsAppointmentScheduler.enmAppointmentUpToDurationType.Weeks
                dtToDate = dtFromDate.AddDays(objAppointment.AppointmentUpToDuration * 7)
            Case clsAppointmentScheduler.enmAppointmentUpToDurationType.Months
                dtToDate = dtFromDate.AddMonths(objAppointment.AppointmentUpToDuration)
        End Select
        '' 20071116 Mahesh
        dtToDate = dtToDate.AddDays(-1)
        ''
        Dim drRow As DataRow
        Select Case objAppointment.AppointmentIntervalType
            Case clsAppointmentScheduler.enmAppointmentIntervalType.Daily
                While 1
                    dtAppointmentDate = dtAppointmentDate.AddDays(objAppointment.AppointmentInterval)
                    If dtAppointmentDate > dtToDate Then
                        Exit While
                    End If
                    drRow = dtAppointment.NewRow
                    drRow(0) = Format(dtAppointmentDate, "Short Date") ' & " - " & WeekdayName(Weekday(dtAppointmentDate))
                    drRow(1) = Format(dtpTime.Value, "Medium Time")
                    drRow(2) = "Yes"
                    drRow(3) = cmbProvider.Text
                    dtAppointment.Rows.Add(drRow)
                End While
            Case clsAppointmentScheduler.enmAppointmentIntervalType.Weekly
                Dim nWeekDay As Byte
                While 1
                    dtAppointmentDate = dtAppointmentDate.AddDays(1)
                    If dtAppointmentDate > dtToDate Then
                        Exit While
                    End If

                    Select Case Weekday(dtAppointmentDate)
                        Case 1
                            nWeekDay = 17
                        Case 2
                            nWeekDay = 2
                        Case 3
                            nWeekDay = 3
                        Case 4
                            nWeekDay = 5
                        Case 5
                            nWeekDay = 7
                        Case 6
                            nWeekDay = 11
                        Case 7
                            nWeekDay = 13
                    End Select

                    If objAppointment.AppointmentInterval Mod nWeekDay = 0 Then
                        drRow = dtAppointment.NewRow
                        drRow(0) = Format(dtAppointmentDate, "Short Date") '& " - " & WeekdayName(Weekday(dtAppointmentDate))
                        drRow(1) = Format(dtpTime.Value, "Medium Time")
                        drRow(2) = "Yes"
                        drRow(3) = cmbProvider.Text
                        dtAppointment.Rows.Add(drRow)
                    End If
                End While
            Case clsAppointmentScheduler.enmAppointmentIntervalType.Monthly
                While 1
                    dtAppointmentDate = dtAppointmentDate.AddMonths(objAppointment.AppointmentInterval)
                    If dtAppointmentDate > dtToDate Then
                        Exit While
                    End If
                    drRow = dtAppointment.NewRow
                    drRow(0) = Format(dtAppointmentDate, "Short Date") '& " - " & WeekdayName(Weekday(dtAppointmentDate))
                    drRow(1) = Format(dtpTime.Value, "Medium Time")
                    drRow(2) = "Yes"
                    drRow(3) = cmbProvider.Text
                    dtAppointment.Rows.Add(drRow)
                End While
        End Select
        objAppointment = Nothing
    End Sub

    'While filling future appointments in grid, If the appointment is not available for default provider then appointment will be set to another provider
    Private Sub SetFutureAppointment(ByVal dtAppointmentDateTime As DateTime, ByVal strProviderName As String)
        strFutureAppointmentProviderName = ""
        strFutureAppointmentDate = ""
        strFutureAppointmentTime = ""
        Dim strOtherProvider As String
        Dim nCount As Int16
        For nCount = 0 To cmbProvider.Items.Count - 1
            strOtherProvider = cmbProvider.Items(nCount).item(1)
            If LCase(strOtherProvider) <> LCase(strProviderName) Then
                'Check Appointment Time is Free or not
                If objclsAppointment.CheckAppointmentAvailable(dtAppointmentDateTime, numAppointmentDuration.Value, strOtherProvider) = True Then
                    'Check Doctor is Available or not
                    Dim objDoctor As New clsDoctorHolidaySchedule
                    If objDoctor.IsDoctorAvailable(strOtherProvider, dtAppointmentDateTime) = True Then
                        strFutureAppointmentProviderName = strOtherProvider
                        objDoctor = Nothing
                        Exit Sub
                    End If
                    objDoctor = Nothing
                End If
            End If
        Next
        'Appointment is not available for any doctor
        'Check other time slot in that day for default doctor
        Dim nAppointmentTime As Long
        Dim nEndTime As Long
        Dim dtTempAppointmentTime As DateTime
        nEndTime = New TimeSpan(gClinicEndTime.Hour, gClinicEndTime.Minute, gClinicEndTime.Second).Ticks
        dtTempAppointmentTime = dtAppointmentDateTime

        While (1)
            dtTempAppointmentTime = dtTempAppointmentTime.AddMinutes(5)
            nAppointmentTime = New TimeSpan(dtTempAppointmentTime.Hour, dtTempAppointmentTime.Minute, dtTempAppointmentTime.Second).Ticks
            If nAppointmentTime >= nEndTime Then
                Exit While
            End If
            'Check Appointment Time is Free or not
            If objclsAppointment.CheckAppointmentAvailable(dtTempAppointmentTime, numAppointmentDuration.Value, strProviderName) = True Then
                'Check Doctor is Available or not
                Dim objDoctor As New clsDoctorHolidaySchedule
                If objDoctor.IsDoctorAvailable(strProviderName, dtTempAppointmentTime) = True Then
                    strFutureAppointmentProviderName = strProviderName
                    strFutureAppointmentDate = Format(dtTempAppointmentTime.Date, "Short Date")
                    strFutureAppointmentTime = Format(dtTempAppointmentTime, "Medium Time")
                    objDoctor = Nothing
                    Exit Sub
                End If
                objDoctor = Nothing
            End If
        End While
        'Appointment is not available in the whole day for default doctor
        'Check other time slot in that day for other doctor
        dtTempAppointmentTime = dtAppointmentDateTime
        For nCount = 0 To cmbProvider.Items.Count - 1
            strOtherProvider = cmbProvider.Items(nCount).item(1)
            If LCase(strOtherProvider) <> LCase(strProviderName) Then
                While (1)
                    dtTempAppointmentTime = dtTempAppointmentTime.AddMinutes(5)
                    nAppointmentTime = New TimeSpan(dtTempAppointmentTime.Hour, dtTempAppointmentTime.Minute, dtTempAppointmentTime.Second).Ticks
                    If nAppointmentTime >= nEndTime Then
                        Exit While
                    End If
                    'Check Appointment Time is Free or not
                    If objclsAppointment.CheckAppointmentAvailable(dtTempAppointmentTime, numAppointmentDuration.Value, strOtherProvider) = True Then
                        'Check Doctor is Available or not
                        Dim objDoctor As New clsDoctorHolidaySchedule
                        If objDoctor.IsDoctorAvailable(strOtherProvider, dtTempAppointmentTime) = True Then
                            strFutureAppointmentProviderName = strOtherProvider
                            strFutureAppointmentDate = Format(dtTempAppointmentTime.Date, "Short Date")
                            strFutureAppointmentTime = Format(dtTempAppointmentTime, "Medium Time")
                            objDoctor = Nothing
                            Exit Sub
                        End If
                        objDoctor = Nothing
                    End If
                End While
            End If
        Next
        strFutureAppointmentProviderName = ""
        strFutureAppointmentDate = ""
        strFutureAppointmentTime = ""
    End Sub

    'Show Future Appointments
    Private Sub btnShowFutureAppointments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowFutureAppointments.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Call Fill_FutureAppointments()
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    'Delete the Appointment
    Private Sub btnDeleteAppointment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAppointment.Click
        Try
            If dgFutureAppointments.CurrentRowIndex >= 0 Then
                Dim drRow As DataRow
                drRow = dtAppointment.Rows(dgFutureAppointments.CurrentRowIndex)
                If MessageBox.Show("Are you sure, you want to delete " & drRow(0) & " appointment?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    dtAppointment.Rows(dgFutureAppointments.CurrentRowIndex).Delete()
                End If
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'Modify the Appointment
    Private Sub mnuModifyAppointment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuModifyAppointment.Click
        Try
            btnModifyAppointment_Click(sender, e)
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    'Show Appointment Schedule
    Private Sub ShowAppointmentSchedule(ByVal dtAppointmentDateTime As DateTime)

        Try
            strAppointmentDate = ""
            strAppointmentTime = ""

            'Retrieve Appointment details
            Dim frm As New frmModifyAppointment
            frm.lblDoctorName.Text = cmbProvider.Text
            frm.tmAppointmentTime.Value = dtAppointmentDateTime
            frm.AppointmentCalendar.SelectedDateBegin = dtAppointmentDateTime
            frm.AppointmentCalendar.DailyCalendar.WorkHourBegin = New TimeSpan(gClinicStartTime.Hour, gClinicStartTime.Minute, gClinicStartTime.Second)
            frm.AppointmentCalendar.DailyCalendar.WorkHourEnd = New TimeSpan(gClinicEndTime.Hour, gClinicEndTime.Minute, gClinicEndTime.Second)
            'Set Calendar Resolution
            If gnAppointmentInterval <= 1 Then
                frm.AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.Minute
            ElseIf gnAppointmentInterval > 1 And gnAppointmentInterval <= 5 Then
                frm.AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.FiveMinutes
            ElseIf gnAppointmentInterval = 6 Then
                frm.AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.SixMinutes
            ElseIf gnAppointmentInterval > 6 And gnAppointmentInterval <= 10 Then
                frm.AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.TenMinutes
            ElseIf gnAppointmentInterval > 10 And gnAppointmentInterval <= 15 Then
                frm.AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.Quart
            ElseIf gnAppointmentInterval > 16 And gnAppointmentInterval <= 20 Then
                frm.AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.TwentyMinutes
            ElseIf gnAppointmentInterval > 20 And gnAppointmentInterval <= 30 Then
                frm.AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.Half
            ElseIf gnAppointmentInterval > 30 And gnAppointmentInterval <= 60 Then
                frm.AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.One
            ElseIf gnAppointmentInterval > 60 And gnAppointmentInterval <= 120 Then
                frm.AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.Two
            ElseIf gnAppointmentInterval > 120 And gnAppointmentInterval <= 180 Then
                frm.AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.Three
            ElseIf gnAppointmentInterval > 180 And gnAppointmentInterval <= 240 Then
                frm.AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.Four
            ElseIf gnAppointmentInterval > 240 And gnAppointmentInterval <= 360 Then
                frm.AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.Six
            ElseIf gnAppointmentInterval > 360 And gnAppointmentInterval <= 720 Then
                frm.AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.Twelve
            Else
                frm.AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.Half
            End If
            frm.AppointmentCalendar.DailyCalendar.FirstHour = New TimeSpan(gClinicStartTime.Hour, gClinicStartTime.Minute, gClinicStartTime.Second)
            frm.AppointmentCalendar.DailyCalendar.FreeHourColor = Color.FromArgb(nNonWorkingTimeColor)
            frm.AppointmentCalendar.DailyCalendar.WorkHourColor = Color.FromArgb(nWorkingTimeColor)
            frm.AppointmentCalendar.Appointments.Clear()
            frm.AppointmentCalendar.Resources.Clear()
            frm.AppointmentCalendar.DailyCalendar.VisibleColumns = 1
            frm.AppointmentCalendar.DailyCalendar.Resources.Add(New Resource(cmbProvider.Text))
            frm.AppointmentCalendar.DailyCalendar.FirstDate = dtAppointmentDateTime.Date



            'Check Appointment Module Level
            If gnAppointmentModuleLevel <> 0 Then
                Dim dsData As New DataSet
                Dim objAppointment As New clsAppointments
                dsData = objAppointment.Fill_Appointments(dtAppointmentDateTime.Date, cmbProvider.Text)
                'dsData = objAppointment.Fill_Appointments(MonthCalendar1.SelectionStart.Date, MonthCalendar1.SelectionStart.Date.AddDays(7 * AppointmentCalendar.MonthlyCalendar.VisibleWeeks()).Date, clProviders.Item(nCount))
                objAppointment = Nothing
                Dim strMessage As String
                Dim ap As Appointment
                Dim drRow1 As DataRow
                Dim nCount1 As Int16
                For nCount1 = 0 To dsData.Tables(0).Rows.Count - 1
                    drRow1 = dsData.Tables(0).Rows(nCount1)
                    ap = New Appointment
                    With ap
                        .Tag = drRow1.Item(0)
                        .ResourceIndex = 0
                        .AllDay = False
                        .DateBegin = CType(drRow1.Item(1), Date)
                        '.DateEnd = CType(drRow.Item(1), Date).AddMinutes(gnAppointmentInterval)
                        .DateEnd = CType(drRow1.Item(1), Date).AddMinutes(dsData.Tables(0).Rows(nCount1).Item(5))
                        .Text = drRow1.Item(2) & "-" & drRow1.Item(3)
                    End With
                    frm.AppointmentCalendar.Appointments.Add(ap)
                    'ap.EnsureVisible()
                Next
                'Add Dcotor Busy/Holiday Schedule
                Dim dtHolidaySchedule As New DataTable
                Dim objHoliday As New clsDoctorHolidaySchedule
                'dtHolidaySchedule = objHoliday.RetrieveSchedule(cmbProvider.Text, dtAppointmentDateTime.Date, dtAppointmentDateTime.Date.AddMonths(1))
                dtHolidaySchedule = objHoliday.RetrieveSchedule(cmbProvider.Text, clsDoctorHolidaySchedule.enmScheduleCriteria.All)
                objHoliday = Nothing
                For nCount1 = 0 To dtHolidaySchedule.Rows.Count - 1
                    drRow1 = dtHolidaySchedule.Rows(nCount1)
                    ap = New Appointment
                    With ap
                        .BackColor = Color.FromArgb(nBusyTimeColor)
                        .Tag = ""
                        .ResourceIndex = 0
                        .AllDay = False
                        .DateBegin = drRow1.Item(1)
                        .DateEnd = drRow1.Item(2)
                        .Text = drRow1.Item(3)
                    End With
                    frm.AppointmentCalendar.Appointments.Add(ap)
                Next

            End If
            frm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    'Modify the appointment
    Private Sub btnModifyAppointment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModifyAppointment.Click
        Try
            If dgFutureAppointments.CurrentRowIndex >= 0 Then
                Dim drRow As DataRow
                drRow = dtAppointment.Rows(dgFutureAppointments.CurrentRowIndex)

                Call ShowAppointmentSchedule(CType(drRow(0) & " " & Format(drRow(1), "Medium Time"), DateTime))
                If Trim(strAppointmentDate) <> "" Then
                    drRow(0) = strAppointmentDate
                End If
                If Trim(strAppointmentTime) <> "" Then
                    drRow(1) = strAppointmentTime
                End If
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'Delete the appointment
    Private Sub mnuDeleteAppointment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteAppointment.Click
        Try
            btnDeleteAppointment_Click(sender, e)
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'User change the date in Calendar Control
    Private Sub dtpDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDate.ValueChanged
        Try
            'Check view is customize or not
            If chkCustomizeAppointment.Checked = True Then
                MonthCalendar1.SetDate(dtpDate.Value)
            Else
                If blnAppointmentTypeChange = True And cmbAppointmentTypes.SelectedIndex <> 0 And cmbAppointmentTypes.Enabled = True Then
                    Me.Cursor = Cursors.WaitCursor
                    Call Fill_FutureAppointments()
                    Me.Cursor = Cursors.Default
                End If
            End If

        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'User change the Appointment time
    Private Sub dtpTime_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpTime.ValueChanged
        Try
            If blnAppointmentTypeChange = True And cmbAppointmentTypes.SelectedIndex <> 0 And cmbAppointmentTypes.Enabled = True Then
                Me.Cursor = Cursors.WaitCursor
                'Fill Future Appointments
                Call Fill_FutureAppointments()
                Me.Cursor = Cursors.Default
            End If
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'User clicked for schedule
    Private Sub btnSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSchedule.Click
        Try
            strAppointmentDate = ""
            strAppointmentTime = ""
            'Show Appointment Schedule
            Call ShowAppointmentSchedule(CType(dtpDate.Value.Date & " " & Format(dtpTime.Value, "Medium Time"), DateTime))
            If Trim(strAppointmentDate) <> "" Then
                dtpDate.Value = CType(strAppointmentDate, DateTime)
                If Trim(strAppointmentTime) <> "" Then
                    dtpTime.Value = CType(strAppointmentDate & " " & strAppointmentTime, DateTime)
                End If
            End If

        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Check for Availability
    Private Sub btnCheckAvailability_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckAvailability.Click
        Try
            'Check Appointment interval is less than 0 or not
            If numAppointmentDuration.Value <= 0 Then
                MessageBox.Show("Appointment interval can not be less than or equal to 0", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                numAppointmentDuration.Focus()
                Exit Sub
            End If
            Dim nAppointmentTime As Long
            Dim nStartTime As Long
            Dim nEndTime As Long

            'Retrieve Clinic Start Time
            nStartTime = New TimeSpan(gClinicStartTime.Hour, gClinicStartTime.Minute, gClinicStartTime.Second).Ticks
            'Retrieve Clinic Closing Time
            nEndTime = New TimeSpan(gClinicEndTime.Hour, gClinicEndTime.Minute, gClinicEndTime.Second).Ticks
            'Retrieve Appointment Time
            nAppointmentTime = New TimeSpan(dtpTime.Value.Hour, dtpTime.Value.Minute, dtpTime.Value.Second).Ticks
            'Check Appointment Module Level
            If gnAppointmentModuleLevel <> 0 Then
                If nAppointmentTime < nStartTime Or nAppointmentTime >= nEndTime Then
                    MessageBox.Show("Clinic Timing is " & Format(gClinicStartTime, "Medium Time") & " To " & Format(gClinicEndTime, "Medium Time") & "." & vbCrLf & "So appointment is not available " & dtpTime.Value, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                'Check Appointment can be set before closing clinic or not
                Dim dtAppointmentEndTime As DateTime
                dtAppointmentEndTime = CType(dtpDate.Value.Date & " " & Format(dtpTime.Value, "Medium Time"), DateTime).AddMinutes(numAppointmentDuration.Value)
                nAppointmentTime = New TimeSpan(dtAppointmentEndTime.Hour, dtAppointmentEndTime.Minute, dtAppointmentEndTime.Second).Ticks
                If nAppointmentTime > nEndTime Then
                    MessageBox.Show("Clinic Timing is " & Format(gClinicStartTime, "Medium Time") & " To " & Format(gClinicEndTime, "Medium Time") & "." & vbCrLf & "So appointment is not available at " & dtpTime.Value & " for " & numAppointmentDuration.Value & " minutes", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                'Check Appointment Time is Free or not
                If objclsAppointment.CheckAppointmentAvailable(dtpDate.Value.Date & " " & Format(dtpTime.Value, "Medium Time"), numAppointmentDuration.Value, cmbProvider.Text, lblAppointmentID.Text) = False Then
                    MessageBox.Show("Appointment is not available at " & dtpDate.Value.Date & " " & Format(dtpTime.Value, "Medium Time") & " for " & cmbProvider.Text, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                'Check Doctor is Available or not
                Dim objDoctor As New clsDoctorHolidaySchedule
                If objDoctor.IsDoctorAvailable(Trim(cmbProvider.Text), dtpDate.Value.Date & " " & Format(dtpTime.Value, "Medium Time")) = False Then
                    MessageBox.Show(cmbProvider.Text & " is not available at this time", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    objDoctor = Nothing
                    Exit Sub
                End If
                objDoctor = Nothing
                MessageBox.Show("Appointment is available at " & dtpDate.Value.Date & " " & Format(dtpTime.Value, "Medium Time"), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'Change the Provider
    Private Sub cmbProvider_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProvider.SelectedIndexChanged
        Try
            If blnAppointmentTypeChange = True And cmbAppointmentTypes.SelectedIndex <> 0 Then
                Me.Cursor = Cursors.WaitCursor
                'Fill Future Appointments
                Call Fill_FutureAppointments()
                Me.Cursor = Cursors.Default
            End If
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'User changed the appointment duration
    Private Sub numAppointmentDuration_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles numAppointmentDuration.ValueChanged
        Try
            If blnAppointmentTypeChange = True And cmbAppointmentTypes.SelectedIndex <> 0 And cmbAppointmentTypes.Enabled = True Then
                Me.Cursor = Cursors.WaitCursor
                'Fill Future Appointments
                Call Fill_FutureAppointments()
                Me.Cursor = Cursors.Default
            End If
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Timer events
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        On Error Resume Next
        lblMessage.Visible = Not lblMessage.Visible
    End Sub

    'User change the date in Calendar control
    Private Sub MonthCalendar1_DateChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles MonthCalendar1.DateChanged
        Try
            AppointmentCelendar.FirstDate = MonthCalendar1.SelectionStart
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'User selected the customize appointment
    Private Sub chkCustomizeAppointment_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCustomizeAppointment.CheckedChanged
        Try
            grpFutureAppointments.Visible = Not chkCustomizeAppointment.Checked
            '''''The code is added by Anil on 16/10/2007
            '''''the code is added to resize the form window on CustomizeAppointment check and uncheck
            If chkCustomizeAppointment.Checked = True Then
                grpCustomizeAppointments.Visible = chkCustomizeAppointment.Checked
                Me.Width = Me_Width + grpCustomizeAppointments.Width
                If grpFutureAppointments.Visible = True And grpFutureAppointments.Enabled = True And IsNothing(dgFutureAppointments.DataSource) = False And CType(dgFutureAppointments.DataSource, DataTable).Rows.Count >= 1 Then
                    Me.Width = Me_Width + grpFutureAppointments.Width
                End If
            Else
                grpCustomizeAppointments.Visible = chkCustomizeAppointment.Checked
                Me.Width = Me.Width - grpCustomizeAppointments.Width
                If grpFutureAppointments.Visible = True And grpFutureAppointments.Enabled = True And IsNothing(dgFutureAppointments.DataSource) = False And CType(dgFutureAppointments.DataSource, DataTable).Rows.Count >= 1 Then
                    Me.Width = Me_Width + grpFutureAppointments.Width
                End If
            End If

            cmbAppointmentTypes.Enabled = Not chkCustomizeAppointment.Checked
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AppointmentCelendar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AppointmentCelendar.Click

    End Sub

    'User double clicked on Appointment control
    Private Sub AppointmentCelendar_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles AppointmentCelendar.DoubleClick
        Try
            If lstCustomizeAppointments.Items.Contains(AppointmentCelendar.SelectedDateBegin) = False Then
                lstCustomizeAppointments.Items.Add(AppointmentCelendar.SelectedDateBegin)
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Deleting Customize Appointment
    Private Sub btnDeleteCustomizeAppointment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteCustomizeAppointment.Click
        Try
            If IsNothing(lstCustomizeAppointments.SelectedItem) = False Then
                lstCustomizeAppointments.Items.Remove(lstCustomizeAppointments.SelectedItem)
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Delete all appointments
    Private Sub btnDeleteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAll.Click
        Try
            lstCustomizeAppointments.Items.Clear()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub objPatientControl_OK_Click() Handles objPatientControl.OK_Click
        Try
            SetPatientData()

            If Not dgFutureAppointments.DataSource Is Nothing Then
                If CType(dgFutureAppointments.DataSource, DataTable).Rows.Count > 1 Then
                    If Not Me.Width = 1000 Then Me.Width = 1000
                Else
                    If Not Me.Width = 550 Then Me.Width = 550
                End If
            Else
                If Not Me.Width = 1000 Then Me.Width = 1000
            End If

            pnlMain.Visible = True
            pnl_tls.Visible = True
            pnlMain.BringToFront()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub objPatientControl_Cancel_Click() Handles objPatientControl.Cancel_Click
        'If Not IsNothing(ObjcustomGrid) Then
        '    Me.Controls.Remove(ObjcustomGrid)
        '    ObjcustomGrid.Visible = False
        '    ObjcustomGrid = Nothing
        '    rowindex = 0
        'End If
        Try
            If Not IsNothing(objPatientControl) Then
                Me.pnlSearchPatient.Controls.Remove(objPatientControl)
                objPatientControl.Visible = False
                objPatientControl = Nothing
                rowindex = 0
            End If

            If Not dgFutureAppointments.DataSource Is Nothing Then
                If CType(dgFutureAppointments.DataSource, DataTable).Rows.Count > 1 Then
                    If Not Me.Width = 1000 Then Me.Width = 1000
                Else
                    If Not Me.Width = 550 Then Me.Width = 550
                End If
            Else
                If Not Me.Width = 1000 Then Me.Width = 1000
            End If

            pnlSearchPatient.Visible = False
            pnlMain.Visible = True
            pnl_tls.Visible = True
            pnlMain.BringToFront()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub SetPatientData()
        'If dtPatient.Rows.Count > 0 Then
        '    If rowindex <= ReferralCount Then
        If Not IsNothing(objPatientControl) Then
            txtFirstName.Tag = gnPatientID
            gnPatientID = objPatientControl.PatientID

            If CheckPatientStatus(objPatientControl.PatientID) = False Then
                gnPatientID = txtFirstName.Tag
                Exit Sub
            End If

            ' gnPatientID = txtFirstName.Tag
            txtFirstName.Tag = 0
            txtPatientCode.Tag = objPatientControl.PatientID   '' objPatientControl._UCflex.GetData(objPatientControl._UCflex.Row, 0) '' PatientID
            txtPatientCode.Text = objPatientControl.PatientCode  '' UCflex.GetData(objPatientControl._UCflex.Row, 1) '' Patient Code
            txtFirstName.Text = objPatientControl.FirstName   '' UCflex.GetData(objPatientControl._UCflex.Row, 2) '' Patient First Name
            txtLastName.Text = objPatientControl.LastName   '' _UCflex.GetData(objPatientControl._UCflex.Row, 3) '' Patient Last Name
        End If

        '    End If
        'End If
        If Not IsNothing(objPatientControl) Then
            Me.pnlSearchPatient.Controls.Remove(objPatientControl)
            objPatientControl.Visible = False
            objPatientControl = Nothing
            rowindex = 0
        End If
    End Sub

    '''''<><><><><> Check Patient Status <><><><><><>''''

    'Display the Patient details


    Private Sub objPatientControl_PicAdv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles objPatientControl.PicAdv_Click
        Try
            Dim RowFilter As String = ""
            Dim DVMain As DataView

            Dim frm As New frmAdvancedSearch
            With frm
                .Phone = Phone_AS
                .ISDOB = ISDOB_AS
                .DOB = DOB_AS

                Select Case Trim(objPatientControl.lblSearchCriteria.Text)
                    Case "Patient ID"
                        .Code = objPatientControl.txtSearchPatient.Text.Trim
                    Case "First Name"
                        .FName = objPatientControl.txtSearchPatient.Text.Trim
                    Case "Last Name"
                        .LName = objPatientControl.txtSearchPatient.Text.Trim
                    Case "SSN No"
                        .SSN = objPatientControl.txtSearchPatient.Text.Trim
                End Select
                ''''
                .ShowInTaskbar = False
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog()
                '' Set Values For Update
                Phone_AS = .Phone
                ISDOB_AS = .ISDOB
                DOB_AS = .DOB

                '''''''''
                'If .ISDOB = True Then
                '    ISDOB = True
                '    DOB = .DOB()
                'End If
                'Phone = .Phone

                dvPatient = CType(objPatientControl.dgPatient.DataSource, DataView)

                If .Code <> "" Then ''PatientCode
                    RowFilter = GetRowFilter(RowFilter)
                    If .Code.StartsWith("%") = True Or .Code.StartsWith("*") = True Then
                        RowFilter = RowFilter & dvPatient.Table.Columns("PatientCode").ColumnName & " Like '%" & .Code & "%'"
                    Else
                        RowFilter = RowFilter & dvPatient.Table.Columns("PatientCode").ColumnName & " Like '" & .Code & "%'"
                    End If
                End If


                If .FName <> "" Then ''PatientFirstName
                    RowFilter = GetRowFilter(RowFilter)
                    If .FName.IndexOf(",") >= 1 Then
                        Dim strFirstName As String
                        Dim strLastName As String
                        strFirstName = Mid(.FName, 1, .FName.IndexOf(","))
                        strLastName = Mid(.FName, .FName.IndexOf(",") + 2)
                        RowFilter = RowFilter & dvPatient.Table.Columns("PatientFirstName").ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns("PatientLastName").ColumnName & " Like '" & strLastName & "%'"
                    Else
                        If .FName.StartsWith("%") = True Or .FName.StartsWith("*") = True Then
                            RowFilter = RowFilter & dvPatient.Table.Columns("PatientFirstName").ColumnName & " Like '%" & .FName & "%'"
                        Else
                            RowFilter = RowFilter & dvPatient.Table.Columns("PatientFirstName").ColumnName & " Like '" & .FName & "%'"
                        End If
                    End If
                End If

                If .LName <> "" Then ''PatientLastName
                    RowFilter = GetRowFilter(RowFilter)
                    If .LName.IndexOf(",") >= 1 Then
                        Dim strFirstName As String
                        Dim strLastName As String
                        strLastName = Mid(.LName, 1, .LName.IndexOf(","))
                        strFirstName = Mid(.LName, .LName.IndexOf(",") + 2)
                        RowFilter = RowFilter & dvPatient.Table.Columns("PatientFirstName").ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns("PatientLastName").ColumnName & " Like '" & strLastName & "%'"
                    Else
                        If .LName.StartsWith("%") = True Or .LName.StartsWith("*") = True Then
                            RowFilter = RowFilter & dvPatient.Table.Columns("PatientLastName").ColumnName & " Like '%" & .LName & "%'"
                        Else
                            RowFilter = RowFilter & dvPatient.Table.Columns("PatientLastName").ColumnName & " Like '" & .LName & "%'"
                        End If
                    End If
                End If

                If .SSN <> "" And IsNumeric(.SSN) = True Then ''SSNNo  ''PatientDOB '' Phone
                    RowFilter = GetRowFilter(RowFilter)
                    RowFilter = RowFilter & dvPatient.Table.Columns("SSNNo").ColumnName & "=" & .SSN
                    'Else
                    '    RowFilter = GetRowFilter(RowFilter)
                    '    RowFilter = RowFilter & dvPatient.Table.Columns("SSNNo").ColumnName & " Like '%'"
                End If

                If .Phone <> "" Then ''PatientDOB '' Phone
                    RowFilter = GetRowFilter(RowFilter)
                    If .Phone.StartsWith("%") = True Or .Phone.StartsWith("*") = True Then
                        RowFilter = RowFilter & dvPatient.Table.Columns("Phone").ColumnName & " Like '%" & .Phone & "%'"
                    Else
                        RowFilter = RowFilter & dvPatient.Table.Columns("Phone").ColumnName & " Like '" & .Phone & "%'"
                    End If
                End If


                If .ISDOB = True And IsDate(.DOB) = True Then
                    RowFilter = GetRowFilter(RowFilter)
                    RowFilter = RowFilter & dvPatient.Table.Columns("PatientDOB").ColumnName & "= '" & .DOB & "'"
                End If


                '''' For Search on Gardian's Info
                '''' 20070128
                If .IsGuardianinfo = True Then
                    ''sMother_fName,  sMother_lName, sMother_Phone, sMother_Mobile, sFather_fName, sFather_lName, sFather_Phone, sFather_Mobile
                    ''''
                    If .MotherFirstName <> "" Then  ''MotherFirstName
                        RowFilter = GetRowFilter(RowFilter)
                        If .MotherFirstName.IndexOf(",") >= 1 Then
                            Dim strFirstName As String
                            Dim strLastName As String
                            strLastName = Mid(.MotherFirstName, 1, .MotherFirstName.IndexOf(","))
                            strFirstName = Mid(.MotherFirstName, .MotherFirstName.IndexOf(",") + 2)
                            RowFilter = RowFilter & dvPatient.Table.Columns("sMother_fName").ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns("sMother_lName").ColumnName & " Like '" & strLastName & "%'"
                        Else
                            If .MotherFirstName.StartsWith("%") = True Or .MotherFirstName.StartsWith("*") = True Then
                                RowFilter = RowFilter & dvPatient.Table.Columns("sMother_fName").ColumnName & " Like '%" & .MotherFirstName & "%'"
                            Else
                                RowFilter = RowFilter & dvPatient.Table.Columns("sMother_fName").ColumnName & " Like '" & .MotherFirstName & "%'"
                            End If
                        End If
                    End If
                    ''''
                    If .MotherLastName <> "" Then   ''MotherLastName
                        RowFilter = GetRowFilter(RowFilter)
                        If .MotherLastName.IndexOf(",") >= 1 Then
                            Dim strFirstName As String
                            Dim strLastName As String
                            strLastName = Mid(.MotherLastName, 1, .MotherLastName.IndexOf(","))
                            strFirstName = Mid(.MotherLastName, .MotherLastName.IndexOf(",") + 2)
                            RowFilter = RowFilter & dvPatient.Table.Columns("sMother_fName").ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns("sMother_lName").ColumnName & " Like '" & strLastName & "%'"
                        Else
                            If .MotherLastName.StartsWith("%") = True Or .MotherLastName.StartsWith("*") = True Then
                                RowFilter = RowFilter & dvPatient.Table.Columns("sMother_lName").ColumnName & " Like '%" & .MotherLastName & "%'"
                            Else
                                RowFilter = RowFilter & dvPatient.Table.Columns("sMother_lName").ColumnName & " Like '" & .MotherLastName & "%'"
                            End If
                        End If
                    End If

                    If .MotherCellNo <> "" Then  ''MotherCellNo '' sMother_Mobile
                        RowFilter = GetRowFilter(RowFilter)
                        If .MotherCellNo.StartsWith("%") = True Or .MotherCellNo.StartsWith("*") = True Then
                            RowFilter = RowFilter & dvPatient.Table.Columns("sMother_Mobile").ColumnName & " Like '%" & .MotherCellNo & "%'"
                        Else
                            RowFilter = RowFilter & dvPatient.Table.Columns("sMother_Mobile").ColumnName & " Like '" & .MotherCellNo & "%'"
                        End If
                    End If

                    If .MotherPhoneNo <> "" Then  ''MotherPhoneNo '' sMother_Phone
                        RowFilter = GetRowFilter(RowFilter)
                        If .MotherPhoneNo.StartsWith("%") = True Or .MotherPhoneNo.StartsWith("*") = True Then
                            RowFilter = RowFilter & dvPatient.Table.Columns("sMother_Phone").ColumnName & " Like '%" & .MotherPhoneNo & "%'"
                        Else
                            RowFilter = RowFilter & dvPatient.Table.Columns("sMother_Phone").ColumnName & " Like '" & .MotherPhoneNo & "%'"
                        End If
                    End If
                    '''''

                    '''''
                    If .FatherFirstName <> "" Then   '' FatherFirstName , sFather_fName
                        RowFilter = GetRowFilter(RowFilter)
                        If .FatherFirstName.IndexOf(",") >= 1 Then
                            Dim strFirstName As String
                            Dim strLastName As String
                            strLastName = Mid(.FatherFirstName, 1, .FatherFirstName.IndexOf(","))
                            strFirstName = Mid(.FatherFirstName, .FatherFirstName.IndexOf(",") + 2)
                            RowFilter = RowFilter & dvPatient.Table.Columns("sFather_fName").ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns("sFather_lName").ColumnName & " Like '" & strLastName & "%'"
                        Else
                            If .FatherFirstName.StartsWith("%") = True Or .FatherFirstName.StartsWith("*") = True Then
                                RowFilter = RowFilter & dvPatient.Table.Columns("sFather_fName").ColumnName & " Like '%" & .FatherFirstName & "%'"
                            Else
                                RowFilter = RowFilter & dvPatient.Table.Columns("sFather_fName").ColumnName & " Like '" & .FatherFirstName & "%'"
                            End If
                        End If
                    End If
                    ''''
                    If .FatherLastName <> "" Then    ''FatherLastName, sFather_lName
                        RowFilter = GetRowFilter(RowFilter)
                        If .FatherLastName.IndexOf(",") >= 1 Then
                            Dim strFirstName As String
                            Dim strLastName As String
                            strLastName = Mid(.FatherLastName, 1, .FatherLastName.IndexOf(","))
                            strFirstName = Mid(.FatherLastName, .FatherLastName.IndexOf(",") + 2)
                            RowFilter = RowFilter & dvPatient.Table.Columns("sFather_fName").ColumnName & " Like '" & strFirstName & "%' and " & dvPatient.Table.Columns("sFather_lName").ColumnName & " Like '" & strLastName & "%'"
                        Else
                            If .FatherLastName.StartsWith("%") = True Or .FatherLastName.StartsWith("*") = True Then
                                RowFilter = RowFilter & dvPatient.Table.Columns("sFather_lName").ColumnName & " Like '%" & .FatherLastName & "%'"
                            Else
                                RowFilter = RowFilter & dvPatient.Table.Columns("sFather_lName").ColumnName & " Like '" & .FatherLastName & "%'"
                            End If
                        End If
                    End If

                    If .FatherCellNo <> "" Then   ''FatherCellNo ''  sFather_Mobile
                        RowFilter = GetRowFilter(RowFilter)
                        If .FatherCellNo.StartsWith("%") = True Or .FatherCellNo.StartsWith("*") = True Then
                            RowFilter = RowFilter & dvPatient.Table.Columns("sFather_Mobile").ColumnName & " Like '%" & .FatherCellNo & "%'"
                        Else
                            RowFilter = RowFilter & dvPatient.Table.Columns("sFather_Mobile").ColumnName & " Like '" & .FatherCellNo & "%'"
                        End If
                    End If

                    If .FatherPhoneNo <> "" Then   ''FatherPhoneNo '' sFather_Phone
                        RowFilter = GetRowFilter(RowFilter)
                        If .FatherPhoneNo.StartsWith("%") = True Or .FatherPhoneNo.StartsWith("*") = True Then
                            RowFilter = RowFilter & dvPatient.Table.Columns("sFather_Phone").ColumnName & " Like '%" & .FatherPhoneNo & "%'"
                        Else
                            RowFilter = RowFilter & dvPatient.Table.Columns("sFather_Phone").ColumnName & " Like '" & .FatherPhoneNo & "%'"
                        End If
                    End If

                End If

            End With

            Me.Cursor = Cursors.WaitCursor
            'Dim dvPatient As DataView

            'dgPatient.DataSource = dvPatient

            If RowFilter <> "" Then
                dvPatient.RowFilter = RowFilter
            End If



            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Function GetRowFilter(ByVal RowFilter As String) As String
        If RowFilter = "" Then
            Return RowFilter
        Else
            Return RowFilter & " AND "
        End If
    End Function




    Private Sub tlsNewAppointment_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsNewAppointment.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Save"
                SaveNewAppointment()

            Case "Close"
                CloseNewAppointment()

        End Select
    End Sub
End Class
