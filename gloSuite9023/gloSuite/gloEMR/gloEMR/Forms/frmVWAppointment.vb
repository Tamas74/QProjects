Imports ComponentGo.Calendars
Imports Microsoft.Win32
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports gloSettings


Public Class frmVWAppointment
    Inherits System.Windows.Forms.Form


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        initEvents()
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
    Friend WithEvents ToolBar1 As System.Windows.Forms.ToolBar
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pnlLeftTop As System.Windows.Forms.Panel
    Friend WithEvents MonthCalendar1 As System.Windows.Forms.MonthCalendar
    Friend WithEvents lblDoctors As System.Windows.Forms.Label
    Friend WithEvents trvProviders As System.Windows.Forms.TreeView
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents pnlLeftTop3 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnlLeftTop2 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlDoctor As System.Windows.Forms.Panel
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuNewAppointment As System.Windows.Forms.MenuItem
    Friend WithEvents mnuModifyAppointment As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDeleteAppointment As System.Windows.Forms.MenuItem
    Friend WithEvents sep1 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPatientVisit As System.Windows.Forms.MenuItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lblPatientName As System.Windows.Forms.Label
    Friend WithEvents lblChiefComplaints As System.Windows.Forms.Label
    Friend WithEvents tlbbtnShowHideCalendar As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnSep1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnNewAppointment As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnModifyAppointment As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnDeleteAppointment As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnSep2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnClose As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnPatientVisit As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnSep3 As System.Windows.Forms.ToolBarButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblChiefComplaintsLabel As System.Windows.Forms.Label
    Friend WithEvents lblPatientNameLabel As System.Windows.Forms.Label
    Friend WithEvents picBusyTime As System.Windows.Forms.PictureBox
    Friend WithEvents picClinicWorkingTime As System.Windows.Forms.PictureBox
    Friend WithEvents picClinicNonWorkingTime As System.Windows.Forms.PictureBox
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents mnuDeleteAppointmentGroup As System.Windows.Forms.MenuItem
    Friend WithEvents tblbtnSep4 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnDeleteAppointmentGroup As System.Windows.Forms.ToolBarButton
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents tlbbtnPrintPreview As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnPrint As System.Windows.Forms.ToolBarButton
    Friend WithEvents tlbbtnSep4 As System.Windows.Forms.ToolBarButton
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPrintPreview As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPrint As System.Windows.Forms.MenuItem
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton5 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton8 As System.Windows.Forms.ToolStripButton
    Friend WithEvents AppointmentCalendar As ComponentGo.Calendars.ComboCalendar
    Friend WithEvents pnlCustomizeApp As System.Windows.Forms.Panel
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpicTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpicFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents ToolStripButton6 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents DefaultPreviewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CustomizeDateToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents ToolStripButton7 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents tlsDefaultPrint As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlsCustomizePrint As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWAppointment))
        Me.ToolBar1 = New System.Windows.Forms.ToolBar
        Me.tlbbtnShowHideCalendar = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnSep1 = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnNewAppointment = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnModifyAppointment = New System.Windows.Forms.ToolBarButton
        Me.tblbtnSep4 = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnDeleteAppointment = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnDeleteAppointmentGroup = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnSep2 = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnPatientVisit = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnSep3 = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnPrintPreview = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnPrint = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnSep4 = New System.Windows.Forms.ToolBarButton
        Me.tlbbtnClose = New System.Windows.Forms.ToolBarButton
        Me.pnlLeft = New System.Windows.Forms.Panel
        Me.pnlLeftTop3 = New System.Windows.Forms.Panel
        Me.lblChiefComplaints = New System.Windows.Forms.Label
        Me.lblChiefComplaintsLabel = New System.Windows.Forms.Label
        Me.lblPatientName = New System.Windows.Forms.Label
        Me.lblPatientNameLabel = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.pnlLeftTop2 = New System.Windows.Forms.Panel
        Me.Label6 = New System.Windows.Forms.Label
        Me.picBusyTime = New System.Windows.Forms.PictureBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.picClinicWorkingTime = New System.Windows.Forms.PictureBox
        Me.picClinicNonWorkingTime = New System.Windows.Forms.PictureBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Splitter2 = New System.Windows.Forms.Splitter
        Me.pnlDoctor = New System.Windows.Forms.Panel
        Me.trvProviders = New System.Windows.Forms.TreeView
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblDoctors = New System.Windows.Forms.Label
        Me.pnlLeftTop = New System.Windows.Forms.Panel
        Me.MonthCalendar1 = New System.Windows.Forms.MonthCalendar
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.mnuNewAppointment = New System.Windows.Forms.MenuItem
        Me.mnuModifyAppointment = New System.Windows.Forms.MenuItem
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.mnuDeleteAppointment = New System.Windows.Forms.MenuItem
        Me.mnuDeleteAppointmentGroup = New System.Windows.Forms.MenuItem
        Me.sep1 = New System.Windows.Forms.MenuItem
        Me.mnuPatientVisit = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.mnuPrintPreview = New System.Windows.Forms.MenuItem
        Me.mnuPrint = New System.Windows.Forms.MenuItem
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripSplitButton
        Me.DefaultPreviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CustomizeDateToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripButton7 = New System.Windows.Forms.ToolStripSplitButton
        Me.tlsDefaultPrint = New System.Windows.Forms.ToolStripMenuItem
        Me.tlsCustomizePrint = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripButton8 = New System.Windows.Forms.ToolStripButton
        Me.AppointmentCalendar = New ComponentGo.Calendars.ComboCalendar
        Me.pnlCustomizeApp = New System.Windows.Forms.Panel
        Me.btnOk = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.dtpicTo = New System.Windows.Forms.DateTimePicker
        Me.dtpicFrom = New System.Windows.Forms.DateTimePicker
        Me.lblTo = New System.Windows.Forms.Label
        Me.lblFrom = New System.Windows.Forms.Label
        Me.pnlLeft.SuspendLayout()
        Me.pnlLeftTop3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlLeftTop2.SuspendLayout()
        CType(Me.picBusyTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picClinicWorkingTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picClinicNonWorkingTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.pnlDoctor.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlLeftTop.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlCustomizeApp.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolBar1
        '
        Me.ToolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tlbbtnShowHideCalendar, Me.tlbbtnSep1, Me.tlbbtnNewAppointment, Me.tlbbtnModifyAppointment, Me.tblbtnSep4, Me.tlbbtnDeleteAppointment, Me.tlbbtnDeleteAppointmentGroup, Me.tlbbtnSep2, Me.tlbbtnPatientVisit, Me.tlbbtnSep3, Me.tlbbtnPrintPreview, Me.tlbbtnPrint, Me.tlbbtnSep4, Me.tlbbtnClose})
        Me.ToolBar1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.Location = New System.Drawing.Point(0, 137)
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(271, 29)
        Me.ToolBar1.TabIndex = 0
        Me.ToolBar1.Visible = False
        '
        'tlbbtnShowHideCalendar
        '
        Me.tlbbtnShowHideCalendar.ImageIndex = 0
        Me.tlbbtnShowHideCalendar.Name = "tlbbtnShowHideCalendar"
        Me.tlbbtnShowHideCalendar.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton
        Me.tlbbtnShowHideCalendar.Tag = "ShowHide"
        Me.tlbbtnShowHideCalendar.ToolTipText = "Show / Hide Calendar"
        '
        'tlbbtnSep1
        '
        Me.tlbbtnSep1.Name = "tlbbtnSep1"
        Me.tlbbtnSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tlbbtnNewAppointment
        '
        Me.tlbbtnNewAppointment.ImageIndex = 1
        Me.tlbbtnNewAppointment.Name = "tlbbtnNewAppointment"
        Me.tlbbtnNewAppointment.Tag = "NewAppointment"
        Me.tlbbtnNewAppointment.ToolTipText = "New Appointment"
        '
        'tlbbtnModifyAppointment
        '
        Me.tlbbtnModifyAppointment.Enabled = False
        Me.tlbbtnModifyAppointment.ImageIndex = 2
        Me.tlbbtnModifyAppointment.Name = "tlbbtnModifyAppointment"
        Me.tlbbtnModifyAppointment.Tag = "ModifyAppointment"
        Me.tlbbtnModifyAppointment.ToolTipText = "Modify Appointment"
        '
        'tblbtnSep4
        '
        Me.tblbtnSep4.Name = "tblbtnSep4"
        Me.tblbtnSep4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tlbbtnDeleteAppointment
        '
        Me.tlbbtnDeleteAppointment.Enabled = False
        Me.tlbbtnDeleteAppointment.ImageIndex = 3
        Me.tlbbtnDeleteAppointment.Name = "tlbbtnDeleteAppointment"
        Me.tlbbtnDeleteAppointment.Tag = "DeleteAppointment"
        Me.tlbbtnDeleteAppointment.ToolTipText = "Delete Appointment"
        '
        'tlbbtnDeleteAppointmentGroup
        '
        Me.tlbbtnDeleteAppointmentGroup.Enabled = False
        Me.tlbbtnDeleteAppointmentGroup.ImageIndex = 4
        Me.tlbbtnDeleteAppointmentGroup.Name = "tlbbtnDeleteAppointmentGroup"
        Me.tlbbtnDeleteAppointmentGroup.Tag = "DeleteAppointmentGroup"
        Me.tlbbtnDeleteAppointmentGroup.ToolTipText = "Delete Appointment Group"
        '
        'tlbbtnSep2
        '
        Me.tlbbtnSep2.Name = "tlbbtnSep2"
        Me.tlbbtnSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tlbbtnPatientVisit
        '
        Me.tlbbtnPatientVisit.Enabled = False
        Me.tlbbtnPatientVisit.ImageIndex = 5
        Me.tlbbtnPatientVisit.Name = "tlbbtnPatientVisit"
        Me.tlbbtnPatientVisit.Tag = "PatientVisit"
        Me.tlbbtnPatientVisit.ToolTipText = "Patient Visit"
        Me.tlbbtnPatientVisit.Visible = False
        '
        'tlbbtnSep3
        '
        Me.tlbbtnSep3.Name = "tlbbtnSep3"
        Me.tlbbtnSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        Me.tlbbtnSep3.Visible = False
        '
        'tlbbtnPrintPreview
        '
        Me.tlbbtnPrintPreview.ImageIndex = 13
        Me.tlbbtnPrintPreview.Name = "tlbbtnPrintPreview"
        Me.tlbbtnPrintPreview.Tag = "PrintPreview"
        Me.tlbbtnPrintPreview.ToolTipText = "Print Preview"
        '
        'tlbbtnPrint
        '
        Me.tlbbtnPrint.ImageIndex = 14
        Me.tlbbtnPrint.Name = "tlbbtnPrint"
        Me.tlbbtnPrint.Tag = "Print"
        Me.tlbbtnPrint.ToolTipText = "Print"
        '
        'tlbbtnSep4
        '
        Me.tlbbtnSep4.Name = "tlbbtnSep4"
        Me.tlbbtnSep4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tlbbtnClose
        '
        Me.tlbbtnClose.ImageIndex = 6
        Me.tlbbtnClose.Name = "tlbbtnClose"
        Me.tlbbtnClose.Tag = "Close"
        Me.tlbbtnClose.ToolTipText = "Close Window"
        '
        'pnlLeft
        '
        Me.pnlLeft.BackgroundImage = CType(resources.GetObject("pnlLeft.BackgroundImage"), System.Drawing.Image)
        Me.pnlLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeft.Controls.Add(Me.pnlLeftTop3)
        Me.pnlLeft.Controls.Add(Me.Panel4)
        Me.pnlLeft.Controls.Add(Me.pnlLeftTop2)
        Me.pnlLeft.Controls.Add(Me.Panel3)
        Me.pnlLeft.Controls.Add(Me.Splitter2)
        Me.pnlLeft.Controls.Add(Me.pnlDoctor)
        Me.pnlLeft.Controls.Add(Me.pnlLeftTop)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 58)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(222, 652)
        Me.pnlLeft.TabIndex = 1
        '
        'pnlLeftTop3
        '
        Me.pnlLeftTop3.BackColor = System.Drawing.Color.Transparent
        Me.pnlLeftTop3.BackgroundImage = CType(resources.GetObject("pnlLeftTop3.BackgroundImage"), System.Drawing.Image)
        Me.pnlLeftTop3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeftTop3.Controls.Add(Me.lblChiefComplaints)
        Me.pnlLeftTop3.Controls.Add(Me.lblChiefComplaintsLabel)
        Me.pnlLeftTop3.Controls.Add(Me.lblPatientName)
        Me.pnlLeftTop3.Controls.Add(Me.lblPatientNameLabel)
        Me.pnlLeftTop3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftTop3.Location = New System.Drawing.Point(0, 440)
        Me.pnlLeftTop3.Name = "pnlLeftTop3"
        Me.pnlLeftTop3.Size = New System.Drawing.Size(222, 212)
        Me.pnlLeftTop3.TabIndex = 5
        '
        'lblChiefComplaints
        '
        Me.lblChiefComplaints.BackColor = System.Drawing.Color.Transparent
        Me.lblChiefComplaints.Location = New System.Drawing.Point(20, 76)
        Me.lblChiefComplaints.Name = "lblChiefComplaints"
        Me.lblChiefComplaints.Size = New System.Drawing.Size(166, 115)
        Me.lblChiefComplaints.TabIndex = 8
        Me.lblChiefComplaints.Visible = False
        '
        'lblChiefComplaintsLabel
        '
        Me.lblChiefComplaintsLabel.AutoSize = True
        Me.lblChiefComplaintsLabel.BackColor = System.Drawing.Color.Transparent
        Me.lblChiefComplaintsLabel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChiefComplaintsLabel.Location = New System.Drawing.Point(4, 48)
        Me.lblChiefComplaintsLabel.Name = "lblChiefComplaintsLabel"
        Me.lblChiefComplaintsLabel.Size = New System.Drawing.Size(103, 15)
        Me.lblChiefComplaintsLabel.TabIndex = 7
        Me.lblChiefComplaintsLabel.Text = "Chief Complaints"
        Me.lblChiefComplaintsLabel.Visible = False
        '
        'lblPatientName
        '
        Me.lblPatientName.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientName.Location = New System.Drawing.Point(28, 26)
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Size = New System.Drawing.Size(154, 16)
        Me.lblPatientName.TabIndex = 6
        Me.lblPatientName.Visible = False
        '
        'lblPatientNameLabel
        '
        Me.lblPatientNameLabel.AutoSize = True
        Me.lblPatientNameLabel.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientNameLabel.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientNameLabel.Location = New System.Drawing.Point(6, 8)
        Me.lblPatientNameLabel.Name = "lblPatientNameLabel"
        Me.lblPatientNameLabel.Size = New System.Drawing.Size(83, 15)
        Me.lblPatientNameLabel.TabIndex = 5
        Me.lblPatientNameLabel.Text = "Patient Name"
        Me.lblPatientNameLabel.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImage = CType(resources.GetObject("Panel4.BackgroundImage"), System.Drawing.Image)
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Label4)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 419)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(222, 21)
        Me.Panel4.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(220, 19)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Appointment Details"
        '
        'pnlLeftTop2
        '
        Me.pnlLeftTop2.BackColor = System.Drawing.Color.Transparent
        Me.pnlLeftTop2.BackgroundImage = CType(resources.GetObject("pnlLeftTop2.BackgroundImage"), System.Drawing.Image)
        Me.pnlLeftTop2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeftTop2.Controls.Add(Me.Label6)
        Me.pnlLeftTop2.Controls.Add(Me.picBusyTime)
        Me.pnlLeftTop2.Controls.Add(Me.Label3)
        Me.pnlLeftTop2.Controls.Add(Me.Label2)
        Me.pnlLeftTop2.Controls.Add(Me.picClinicWorkingTime)
        Me.pnlLeftTop2.Controls.Add(Me.picClinicNonWorkingTime)
        Me.pnlLeftTop2.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLeftTop2.Location = New System.Drawing.Point(0, 350)
        Me.pnlLeftTop2.Name = "pnlLeftTop2"
        Me.pnlLeftTop2.Size = New System.Drawing.Size(222, 69)
        Me.pnlLeftTop2.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(25, 44)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(166, 14)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Doctor Busy/Holiday Time"
        '
        'picBusyTime
        '
        Me.picBusyTime.BackColor = System.Drawing.Color.Red
        Me.picBusyTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picBusyTime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picBusyTime.Location = New System.Drawing.Point(6, 46)
        Me.picBusyTime.Name = "picBusyTime"
        Me.picBusyTime.Size = New System.Drawing.Size(13, 10)
        Me.picBusyTime.TabIndex = 8
        Me.picBusyTime.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(25, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(128, 14)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Clinic Working Time"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(25, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(157, 14)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Clinic Non Working Time"
        '
        'picClinicWorkingTime
        '
        Me.picClinicWorkingTime.BackColor = System.Drawing.Color.Cornsilk
        Me.picClinicWorkingTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picClinicWorkingTime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picClinicWorkingTime.Location = New System.Drawing.Point(6, 10)
        Me.picClinicWorkingTime.Name = "picClinicWorkingTime"
        Me.picClinicWorkingTime.Size = New System.Drawing.Size(13, 10)
        Me.picClinicWorkingTime.TabIndex = 5
        Me.picClinicWorkingTime.TabStop = False
        '
        'picClinicNonWorkingTime
        '
        Me.picClinicNonWorkingTime.BackColor = System.Drawing.Color.Yellow
        Me.picClinicNonWorkingTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picClinicNonWorkingTime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picClinicNonWorkingTime.Location = New System.Drawing.Point(6, 28)
        Me.picClinicNonWorkingTime.Name = "picClinicNonWorkingTime"
        Me.picClinicNonWorkingTime.Size = New System.Drawing.Size(13, 10)
        Me.picClinicNonWorkingTime.TabIndex = 4
        Me.picClinicNonWorkingTime.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 331)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(222, 19)
        Me.Panel3.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(220, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Color Coding"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(0, 326)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(222, 5)
        Me.Splitter2.TabIndex = 2
        Me.Splitter2.TabStop = False
        '
        'pnlDoctor
        '
        Me.pnlDoctor.Controls.Add(Me.trvProviders)
        Me.pnlDoctor.Controls.Add(Me.Panel2)
        Me.pnlDoctor.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDoctor.Location = New System.Drawing.Point(0, 166)
        Me.pnlDoctor.Name = "pnlDoctor"
        Me.pnlDoctor.Size = New System.Drawing.Size(222, 160)
        Me.pnlDoctor.TabIndex = 1
        '
        'trvProviders
        '
        Me.trvProviders.BackColor = System.Drawing.Color.GhostWhite
        Me.trvProviders.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvProviders.CheckBoxes = True
        Me.trvProviders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvProviders.ForeColor = System.Drawing.Color.Black
        Me.trvProviders.Location = New System.Drawing.Point(0, 21)
        Me.trvProviders.Name = "trvProviders"
        Me.trvProviders.Size = New System.Drawing.Size(222, 139)
        Me.trvProviders.TabIndex = 3
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.lblDoctors)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(222, 21)
        Me.Panel2.TabIndex = 2
        '
        'lblDoctors
        '
        Me.lblDoctors.BackColor = System.Drawing.Color.Transparent
        Me.lblDoctors.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDoctors.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDoctors.ForeColor = System.Drawing.Color.Black
        Me.lblDoctors.Location = New System.Drawing.Point(0, 0)
        Me.lblDoctors.Name = "lblDoctors"
        Me.lblDoctors.Size = New System.Drawing.Size(220, 19)
        Me.lblDoctors.TabIndex = 2
        Me.lblDoctors.Text = "Doctors"
        Me.lblDoctors.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlLeftTop
        '
        Me.pnlLeftTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeftTop.Controls.Add(Me.ToolBar1)
        Me.pnlLeftTop.Controls.Add(Me.MonthCalendar1)
        Me.pnlLeftTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLeftTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeftTop.Name = "pnlLeftTop"
        Me.pnlLeftTop.Size = New System.Drawing.Size(222, 166)
        Me.pnlLeftTop.TabIndex = 0
        '
        'MonthCalendar1
        '
        Me.MonthCalendar1.Dock = System.Windows.Forms.DockStyle.Top
        Me.MonthCalendar1.ForeColor = System.Drawing.Color.Maroon
        Me.MonthCalendar1.Location = New System.Drawing.Point(0, 0)
        Me.MonthCalendar1.MaxSelectionCount = 1
        Me.MonthCalendar1.Name = "MonthCalendar1"
        Me.MonthCalendar1.TabIndex = 1
        Me.MonthCalendar1.TitleBackColor = System.Drawing.Color.Orange
        Me.MonthCalendar1.TitleForeColor = System.Drawing.Color.Brown
        Me.MonthCalendar1.TrailingForeColor = System.Drawing.Color.Tomato
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(112, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(222, 58)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(5, 652)
        Me.Splitter1.TabIndex = 2
        Me.Splitter1.TabStop = False
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuNewAppointment, Me.mnuModifyAppointment, Me.MenuItem1, Me.mnuDeleteAppointment, Me.mnuDeleteAppointmentGroup, Me.sep1, Me.mnuPatientVisit, Me.MenuItem2, Me.mnuPrintPreview, Me.mnuPrint})
        '
        'mnuNewAppointment
        '
        Me.mnuNewAppointment.Index = 0
        Me.mnuNewAppointment.Text = "New Appointment"
        '
        'mnuModifyAppointment
        '
        Me.mnuModifyAppointment.Index = 1
        Me.mnuModifyAppointment.Text = "Modify Appointment"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 2
        Me.MenuItem1.Text = "-"
        '
        'mnuDeleteAppointment
        '
        Me.mnuDeleteAppointment.Index = 3
        Me.mnuDeleteAppointment.Shortcut = System.Windows.Forms.Shortcut.Del
        Me.mnuDeleteAppointment.Text = "Delete Appointment"
        '
        'mnuDeleteAppointmentGroup
        '
        Me.mnuDeleteAppointmentGroup.Enabled = False
        Me.mnuDeleteAppointmentGroup.Index = 4
        Me.mnuDeleteAppointmentGroup.Text = "Delete Appointment Group"
        '
        'sep1
        '
        Me.sep1.Index = 5
        Me.sep1.Text = "-"
        Me.sep1.Visible = False
        '
        'mnuPatientVisit
        '
        Me.mnuPatientVisit.Index = 6
        Me.mnuPatientVisit.Text = "Patient Visit"
        Me.mnuPatientVisit.Visible = False
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 7
        Me.MenuItem2.Text = "-"
        '
        'mnuPrintPreview
        '
        Me.mnuPrintPreview.Index = 8
        Me.mnuPrintPreview.Text = "Print Preview"
        '
        'mnuPrint
        '
        Me.mnuPrint.Index = 9
        Me.mnuPrint.Text = "Print"
        '
        'Timer1
        '
        Me.Timer1.Interval = 500
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Document = Me.PrintDocument1
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'PrintDocument1
        '
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.ToolStrip1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(698, 58)
        Me.Panel1.TabIndex = 4
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStrip1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripSeparator1, Me.ToolStripButton2, Me.ToolStripButton3, Me.ToolStripSeparator2, Me.ToolStripButton4, Me.ToolStripButton5, Me.ToolStripSeparator3, Me.ToolStripButton6, Me.ToolStripButton7, Me.ToolStripButton8})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(696, 53)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(46, 50)
        Me.ToolStripButton1.Tag = "ShowHide"
        Me.ToolStripButton1.Text = "Show"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton1.ToolTipText = "Show or Hide Calendar"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 53)
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(39, 50)
        Me.ToolStripButton2.Tag = "NewAppointment"
        Me.ToolStripButton2.Text = "New"
        Me.ToolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton2.ToolTipText = "New Appointment"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(51, 50)
        Me.ToolStripButton3.Tag = "ModifyAppointment"
        Me.ToolStripButton3.Text = "Modify"
        Me.ToolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton3.ToolTipText = "Modify Appointment"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 53)
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(52, 50)
        Me.ToolStripButton4.Tag = "DeleteAppointment"
        Me.ToolStripButton4.Text = "Delete"
        Me.ToolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton4.ToolTipText = "Delete Appointment"
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.Image = CType(resources.GetObject("ToolStripButton5.Image"), System.Drawing.Image)
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(53, 50)
        Me.ToolStripButton5.Tag = "DeleteAppointmentGroup"
        Me.ToolStripButton5.Text = "Del All "
        Me.ToolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton5.ToolTipText = "Delete All Appointments"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 53)
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripButton6.BackgroundImage = CType(resources.GetObject("ToolStripButton6.BackgroundImage"), System.Drawing.Image)
        Me.ToolStripButton6.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DefaultPreviewToolStripMenuItem, Me.CustomizeDateToolStripMenuItem1})
        Me.ToolStripButton6.Image = CType(resources.GetObject("ToolStripButton6.Image"), System.Drawing.Image)
        Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(106, 50)
        Me.ToolStripButton6.Tag = ""
        Me.ToolStripButton6.Text = "Print Preview"
        Me.ToolStripButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton6.ToolTipText = "Print Preview"
        '
        'DefaultPreviewToolStripMenuItem
        '
        Me.DefaultPreviewToolStripMenuItem.AccessibleRole = System.Windows.Forms.AccessibleRole.None
        Me.DefaultPreviewToolStripMenuItem.Name = "DefaultPreviewToolStripMenuItem"
        Me.DefaultPreviewToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.DefaultPreviewToolStripMenuItem.Tag = "Default Preview"
        Me.DefaultPreviewToolStripMenuItem.Text = "Default Preview"
        '
        'CustomizeDateToolStripMenuItem1
        '
        Me.CustomizeDateToolStripMenuItem1.Name = "CustomizeDateToolStripMenuItem1"
        Me.CustomizeDateToolStripMenuItem1.Size = New System.Drawing.Size(206, 22)
        Me.CustomizeDateToolStripMenuItem1.Tag = "Customize Preview"
        Me.CustomizeDateToolStripMenuItem1.Text = "Customize Preview"
        '
        'ToolStripButton7
        '
        Me.ToolStripButton7.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripButton7.BackgroundImage = CType(resources.GetObject("ToolStripButton7.BackgroundImage"), System.Drawing.Image)
        Me.ToolStripButton7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStripButton7.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsDefaultPrint, Me.tlsCustomizePrint})
        Me.ToolStripButton7.Image = CType(resources.GetObject("ToolStripButton7.Image"), System.Drawing.Image)
        Me.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton7.Name = "ToolStripButton7"
        Me.ToolStripButton7.Size = New System.Drawing.Size(52, 50)
        Me.ToolStripButton7.Tag = "Print"
        Me.ToolStripButton7.Text = "Print"
        Me.ToolStripButton7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton7.ToolTipText = "Print"
        '
        'tlsDefaultPrint
        '
        Me.tlsDefaultPrint.Name = "tlsDefaultPrint"
        Me.tlsDefaultPrint.Size = New System.Drawing.Size(185, 22)
        Me.tlsDefaultPrint.Text = "Default Print"
        '
        'tlsCustomizePrint
        '
        Me.tlsCustomizePrint.Name = "tlsCustomizePrint"
        Me.tlsCustomizePrint.Size = New System.Drawing.Size(185, 22)
        Me.tlsCustomizePrint.Text = "Customize Print"
        '
        'ToolStripButton8
        '
        Me.ToolStripButton8.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripButton8.BackgroundImage = CType(resources.GetObject("ToolStripButton8.BackgroundImage"), System.Drawing.Image)
        Me.ToolStripButton8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStripButton8.Image = CType(resources.GetObject("ToolStripButton8.Image"), System.Drawing.Image)
        Me.ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton8.Name = "ToolStripButton8"
        Me.ToolStripButton8.Size = New System.Drawing.Size(46, 50)
        Me.ToolStripButton8.Tag = "Close"
        Me.ToolStripButton8.Text = "Close"
        Me.ToolStripButton8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton8.ToolTipText = "Close"
        '
        'AppointmentCalendar
        '
        Me.AppointmentCalendar.AmPmMode = ComponentGo.Calendars.AmPmMode.AmPm
        Me.AppointmentCalendar.BackColor = System.Drawing.Color.WhiteSmoke
        Me.AppointmentCalendar.ContextMenu = Me.ContextMenu1
        '
        '
        '
        Me.AppointmentCalendar.DailyCalendar.AllDayVisible = False
        Me.AppointmentCalendar.DailyCalendar.AllowDateChange = False
        Me.AppointmentCalendar.DailyCalendar.BackColor = System.Drawing.Color.LawnGreen
        Me.AppointmentCalendar.DailyCalendar.DaysLabelColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.AppointmentCalendar.DailyCalendar.DaysLabelColorSecond = System.Drawing.Color.Orange
        Me.AppointmentCalendar.DailyCalendar.DaysWorkspaceColor = System.Drawing.Color.LightSteelBlue
        Me.AppointmentCalendar.DailyCalendar.DaysWorkspaceColorSecond = System.Drawing.Color.DodgerBlue
        Me.AppointmentCalendar.DailyCalendar.ForeColor = System.Drawing.Color.Black
        Me.AppointmentCalendar.DailyCalendar.FreeHourColor = System.Drawing.Color.Yellow
        Me.AppointmentCalendar.DailyCalendar.HoursLabelColor = System.Drawing.Color.Orange
        Me.AppointmentCalendar.DailyCalendar.HoursLabelColorSecond = System.Drawing.Color.Linen
        Me.AppointmentCalendar.DailyCalendar.LineColor = System.Drawing.Color.Maroon
        Me.AppointmentCalendar.DailyCalendar.ResourcesLabelColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.AppointmentCalendar.DailyCalendar.ResourcesLabelColorSecond = System.Drawing.Color.OldLace
        Me.AppointmentCalendar.DailyCalendar.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.AppointmentCalendar.DailyCalendar.SelectedAppointmentColor = System.Drawing.Color.SkyBlue
        Me.AppointmentCalendar.DailyCalendar.SelectedColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.AppointmentCalendar.DailyCalendar.SelectedDayColor = System.Drawing.Color.Tomato
        Me.AppointmentCalendar.DailyCalendar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.AppointmentCalendar.DailyCalendar.TodayMarkerColor = System.Drawing.Color.Blue
        Me.AppointmentCalendar.DailyCalendar.VisibleColumns = 5
        Me.AppointmentCalendar.DailyCalendar.WorkHourColor = System.Drawing.Color.Cornsilk
        Me.AppointmentCalendar.DailyCalendar.WorkHourColorSecond = System.Drawing.Color.PeachPuff
        Me.AppointmentCalendar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AppointmentCalendar.EnableEditInPlace = False
        Me.AppointmentCalendar.Location = New System.Drawing.Point(227, 128)
        '
        '
        '
        Me.AppointmentCalendar.MonthlyCalendar.AllowWeekChange = False
        Me.AppointmentCalendar.MonthlyCalendar.AlternativeMonthColor = System.Drawing.Color.GhostWhite
        Me.AppointmentCalendar.MonthlyCalendar.DaysLabelColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.AppointmentCalendar.MonthlyCalendar.DaysLabelColorSecond = System.Drawing.Color.Orange
        Me.AppointmentCalendar.MonthlyCalendar.FreeHourColor = System.Drawing.Color.Yellow
        Me.AppointmentCalendar.MonthlyCalendar.LineColor = System.Drawing.Color.Maroon
        Me.AppointmentCalendar.MonthlyCalendar.ResourcesLabelColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.AppointmentCalendar.MonthlyCalendar.ResourcesLabelColorSecond = System.Drawing.Color.OldLace
        Me.AppointmentCalendar.MonthlyCalendar.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.AppointmentCalendar.MonthlyCalendar.SelectedAppointmentColor = System.Drawing.Color.SkyBlue
        Me.AppointmentCalendar.MonthlyCalendar.SelectedAppointmentTextColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.AppointmentCalendar.MonthlyCalendar.SelectedColor = System.Drawing.Color.Tomato
        Me.AppointmentCalendar.MonthlyCalendar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.AppointmentCalendar.MonthlyCalendar.TodayMarkerColor = System.Drawing.Color.Blue
        Me.AppointmentCalendar.MonthlyCalendar.WorkHourColor = System.Drawing.Color.Cornsilk
        Me.AppointmentCalendar.MonthlyCalendar.WorkHourColorSecond = System.Drawing.Color.PeachPuff
        Me.AppointmentCalendar.Name = "AppointmentCalendar"
        Me.AppointmentCalendar.Size = New System.Drawing.Size(471, 582)
        Me.AppointmentCalendar.TabIndex = 3
        Me.AppointmentCalendar.ToolbarColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.AppointmentCalendar.ToolbarColorSecond = System.Drawing.Color.DeepSkyBlue
        Me.AppointmentCalendar.Views = CType(((ComponentGo.Calendars.CalendarView.Daily Or ComponentGo.Calendars.CalendarView.Weekly) _
                    Or ComponentGo.Calendars.CalendarView.Monthly), ComponentGo.Calendars.CalendarView)
        Me.AppointmentCalendar.VisibleDays = 5
        '
        '
        '
        Me.AppointmentCalendar.WeeklyCalendar.AllowWeekChange = False
        Me.AppointmentCalendar.WeeklyCalendar.BackColor = System.Drawing.Color.LawnGreen
        Me.AppointmentCalendar.WeeklyCalendar.DaysLabelColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.AppointmentCalendar.WeeklyCalendar.DaysLabelColorSecond = System.Drawing.Color.Orange
        Me.AppointmentCalendar.WeeklyCalendar.FreeHourColor = System.Drawing.Color.Yellow
        Me.AppointmentCalendar.WeeklyCalendar.LineColor = System.Drawing.Color.Maroon
        Me.AppointmentCalendar.WeeklyCalendar.ResourcesLabelColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.AppointmentCalendar.WeeklyCalendar.ResourcesLabelColorSecond = System.Drawing.Color.OldLace
        Me.AppointmentCalendar.WeeklyCalendar.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.AppointmentCalendar.WeeklyCalendar.SelectedAppointmentColor = System.Drawing.Color.SkyBlue
        Me.AppointmentCalendar.WeeklyCalendar.SelectedAppointmentTextColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.AppointmentCalendar.WeeklyCalendar.SelectedColor = System.Drawing.Color.Tomato
        Me.AppointmentCalendar.WeeklyCalendar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.AppointmentCalendar.WeeklyCalendar.TodayMarkerColor = System.Drawing.Color.Blue
        Me.AppointmentCalendar.WeeklyCalendar.WorkHourColor = System.Drawing.Color.Cornsilk
        Me.AppointmentCalendar.WeeklyCalendar.WorkHourColorSecond = System.Drawing.Color.PeachPuff
        '
        'pnlCustomizeApp
        '
        Me.pnlCustomizeApp.BackColor = System.Drawing.Color.Transparent
        Me.pnlCustomizeApp.BackgroundImage = CType(resources.GetObject("pnlCustomizeApp.BackgroundImage"), System.Drawing.Image)
        Me.pnlCustomizeApp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlCustomizeApp.Controls.Add(Me.btnOk)
        Me.pnlCustomizeApp.Controls.Add(Me.btnCancel)
        Me.pnlCustomizeApp.Controls.Add(Me.GroupBox4)
        Me.pnlCustomizeApp.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCustomizeApp.Location = New System.Drawing.Point(227, 58)
        Me.pnlCustomizeApp.Name = "pnlCustomizeApp"
        Me.pnlCustomizeApp.Size = New System.Drawing.Size(471, 70)
        Me.pnlCustomizeApp.TabIndex = 6
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOk.Location = New System.Drawing.Point(221, 45)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(63, 24)
        Me.btnOk.TabIndex = 5
        Me.btnOk.Text = "&OK"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.Transparent
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(290, 45)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(63, 24)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox4.Controls.Add(Me.dtpicTo)
        Me.GroupBox4.Controls.Add(Me.dtpicFrom)
        Me.GroupBox4.Controls.Add(Me.lblTo)
        Me.GroupBox4.Controls.Add(Me.lblFrom)
        Me.GroupBox4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.Color.Black
        Me.GroupBox4.Location = New System.Drawing.Point(0, 1)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(353, 42)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Date"
        '
        'dtpicTo
        '
        Me.dtpicTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpicTo.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpicTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpicTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpicTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpicTo.CustomFormat = "MM/dd/yyyy"
        Me.dtpicTo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpicTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpicTo.Location = New System.Drawing.Point(235, 17)
        Me.dtpicTo.Name = "dtpicTo"
        Me.dtpicTo.Size = New System.Drawing.Size(107, 22)
        Me.dtpicTo.TabIndex = 6
        '
        'dtpicFrom
        '
        Me.dtpicFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpicFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpicFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpicFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpicFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpicFrom.CustomFormat = "MM/dd/yyyy"
        Me.dtpicFrom.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpicFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpicFrom.Location = New System.Drawing.Point(50, 16)
        Me.dtpicFrom.Name = "dtpicFrom"
        Me.dtpicFrom.Size = New System.Drawing.Size(107, 22)
        Me.dtpicFrom.TabIndex = 5
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.BackColor = System.Drawing.Color.Transparent
        Me.lblTo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.Location = New System.Drawing.Point(207, 21)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(22, 14)
        Me.lblTo.TabIndex = 4
        Me.lblTo.Text = "To"
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblFrom.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrom.Location = New System.Drawing.Point(6, 21)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(38, 14)
        Me.lblFrom.TabIndex = 0
        Me.lblFrom.Text = "From"
        '
        'frmVWAppointment
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(7, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(698, 710)
        Me.Controls.Add(Me.AppointmentCalendar)
        Me.Controls.Add(Me.pnlCustomizeApp)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVWAppointment"
        Me.Text = "View Appointment"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlLeftTop3.ResumeLayout(False)
        Me.pnlLeftTop3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.pnlLeftTop2.ResumeLayout(False)
        Me.pnlLeftTop2.PerformLayout()
        CType(Me.picBusyTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picClinicWorkingTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picClinicNonWorkingTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.pnlDoctor.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlLeftTop.ResumeLayout(False)
        Me.pnlLeftTop.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlCustomizeApp.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public clProviders As New Collection
    Dim trvSearchNode As TreeNode
    Dim nPageNo As Byte
    '''''''Added by Anil on 20071204    
    Dim nProvider As Int64
    Dim AppFromDate As Date
    Dim AppToDate As Date
    Dim blnPrint As Boolean = True
    '''''''

    Private Sub Fill_Providers()

        With trvProviders
            .Nodes.Clear()
            Dim trvGroups As TreeNode
            trvGroups = New TreeNode
            With trvGroups
                .Text = "Doctor"
                .ImageIndex = 7
                .SelectedImageIndex = 7
                .ForeColor = Color.Black
            End With
            .Nodes.Add(trvGroups)
            trvGroups = Nothing
            Dim clProviders As New Collection
            Dim objProviders As New clsProvider
            clProviders = objProviders.Fill_Providers
            objProviders = Nothing

            Dim nCount As Integer
            For nCount = 1 To clProviders.Count
                trvGroups = New TreeNode
                With trvGroups
                    .Text = clProviders.Item(nCount)
                    .ImageIndex = 7
                    .SelectedImageIndex = 7
                    .ForeColor = Color.Black
                End With
                .Nodes(0).Nodes.Add(trvGroups)
            Next
            .ExpandAll()

            ''20080923 gstrDoctorName
            If Trim(gstrLoginProviderName) = "All" Then
                .Nodes(0).Checked = True
            Else
                SearchNode(trvProviders, gstrLoginProviderName)
                trvSearchNode.Checked = True
            End If
        End With

    End Sub

    Private Sub frmVWAppointment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            AppointmentCalendar.DailyCalendar.WorkHourBegin = New TimeSpan(gClinicStartTime.Hour, gClinicStartTime.Minute, gClinicStartTime.Second)
            AppointmentCalendar.DailyCalendar.WorkHourEnd = New TimeSpan(gClinicEndTime.Hour, gClinicEndTime.Minute, gClinicEndTime.Second)
            'Set Calendar Resolution
            If gnAppointmentInterval <= 1 Then
                AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.Minute
            ElseIf gnAppointmentInterval > 1 And gnAppointmentInterval <= 5 Then
                AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.FiveMinutes
            ElseIf gnAppointmentInterval = 6 Then
                AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.SixMinutes
            ElseIf gnAppointmentInterval > 6 And gnAppointmentInterval <= 10 Then
                AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.TenMinutes
            ElseIf gnAppointmentInterval > 10 And gnAppointmentInterval <= 15 Then
                AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.Quart
            ElseIf gnAppointmentInterval > 16 And gnAppointmentInterval <= 20 Then
                AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.TwentyMinutes
            ElseIf gnAppointmentInterval > 20 And gnAppointmentInterval <= 30 Then
                AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.Half
            ElseIf gnAppointmentInterval > 30 And gnAppointmentInterval <= 60 Then
                AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.One
            ElseIf gnAppointmentInterval > 60 And gnAppointmentInterval <= 120 Then
                AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.Two
            ElseIf gnAppointmentInterval > 120 And gnAppointmentInterval <= 180 Then
                AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.Three
            ElseIf gnAppointmentInterval > 180 And gnAppointmentInterval <= 240 Then
                AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.Four
            ElseIf gnAppointmentInterval > 240 And gnAppointmentInterval <= 360 Then
                AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.Six
            ElseIf gnAppointmentInterval > 360 And gnAppointmentInterval <= 720 Then
                AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.Twelve
            Else
                AppointmentCalendar.DailyCalendar.HoursResolution = HoursResolutions.Half
            End If
            AppointmentCalendar.DailyCalendar.FirstHour = New TimeSpan(gClinicStartTime.Hour, gClinicStartTime.Minute, gClinicStartTime.Second)
            Call Fill_Providers()
            MonthCalendar1.SelectionStart = System.DateTime.Now.Date
            Call Fill_Appointments()
            Call setAppointmentColor()

            '''''''Code is added by Anil on 20071204
            pnlCustomizeApp.Visible = False
            If AppointmentCalendar.CurrentView = CalendarView.Daily Then
                AppFromDate = Now.Date
                AppToDate = Now.Date
            ElseIf AppointmentCalendar.CurrentView = CalendarView.Weekly Then
                AppFromDate = AppointmentCalendar.FirstDateTime
                AppToDate = AppFromDate.AddDays(5)
            ElseIf AppointmentCalendar.CurrentView = CalendarView.Monthly Then
                AppFromDate = AppointmentCalendar.FirstDateTime
                AppToDate = AppFromDate.AddDays(30)
            End If
            '''''''''''''''''''''''''''''
            'sarika 3rd july 07
            pnlLeft.Visible = False
            '-------------------

            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub SearchNode(ByVal Trv As TreeView, ByVal strText As String)
        Dim trvNde As TreeNode
        For Each trvNde In Trv.Nodes
            SearchNode(trvNde, strText)
        Next
    End Sub
    Private Sub SearchNode(ByVal rootNode As TreeNode, ByVal strText As String)
        For Each childNode As TreeNode In rootNode.Nodes
            If Trim(childNode.Text) = Trim(strText) Then
                trvSearchNode = childNode
                Exit Sub
            End If
            SearchNode(childNode, strText)
        Next
    End Sub
    Private Sub MonthCalendar1_DateChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles MonthCalendar1.DateChanged
        Try
            Me.Cursor = Cursors.WaitCursor
            AppointmentCalendar.FirstDateTime = MonthCalendar1.SelectionStart
            GetCheckedNodes()
            If clProviders.Count >= 1 Then
                Call Fill_Appointments()
            End If
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub trvProviders_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvProviders.AfterCheck
        Try
            Me.Cursor = Cursors.WaitCursor
            CheckUncheckChildNodes(e.Node, e.Node.Checked)
            GetCheckedNodes()
            If clProviders.Count >= 1 Then
                lblDoctors.Text = "Doctors"
                Timer1.Enabled = False
                AppointmentCalendar.Enabled = True
                Call Fill_Appointments()
            Else
                lblDoctors.Text = "Doctors - No Doctor Selected"
                Timer1.Enabled = True
                AppointmentCalendar.Enabled = False
                Me.Cursor = Cursors.Default
                'MessageBox.Show("Please select at least one provider", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub CheckUncheckChildNodes(ByVal rootNode As TreeNode, ByVal blnCheck As Boolean)
        For Each childNode As TreeNode In rootNode.Nodes
            childNode.Checked = blnCheck
            CheckUncheckChildNodes(childNode, blnCheck)
        Next
    End Sub
    Private Sub AppointmentCalendar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AppointmentCalendar.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            If AppointmentCalendar.Appointments.SelectedCount <= 0 Then
                tlbbtnModifyAppointment.Enabled = False
                tlbbtnDeleteAppointment.Enabled = False
                tlbbtnDeleteAppointmentGroup.Enabled = False
                tlbbtnPatientVisit.Enabled = False
                mnuModifyAppointment.Enabled = False
                mnuDeleteAppointment.Enabled = False
                mnuDeleteAppointmentGroup.Enabled = False
                mnuPatientVisit.Enabled = False

                lblPatientNameLabel.Visible = False
                lblChiefComplaintsLabel.Visible = False
                lblPatientName.Visible = False
                lblChiefComplaints.Visible = False
                lblPatientName.Text = ""
                lblChiefComplaints.Text = ""
            Else
                Dim strID As String
                Dim objApp As Appointment
                Dim objAppointment As New clsAppointments
                objApp = AppointmentCalendar.Appointments.FirstSelected


                strID = Trim(objApp.Tag)
                If strID <> "" Then
                    'Appointment is selected
                    lblPatientNameLabel.Visible = True
                    lblChiefComplaintsLabel.Visible = True
                    lblPatientName.Visible = True
                    lblChiefComplaints.Visible = True

                    tlbbtnModifyAppointment.Enabled = True
                    tlbbtnDeleteAppointment.Enabled = True

                    objAppointment.SearchAppointment(objApp.Tag)
                    lblPatientName.Text = objAppointment.PatientFirstName & " " & objAppointment.PatientLastName
                    lblChiefComplaints.Text = objAppointment.Complaints

                    ' '' To Set the Selected Patient on Main Menu []
                    If Me.MdiParent.Name = MainMenu.Name Then
                        CType(Me.MdiParent, MainMenu).ShowDefaultPatientDetails(objAppointment.PatientID)
                    End If
                    ' '' 

                    ' ''Check Appointment is set as Future Group Appointment or not
                    If objAppointment.AppointmentGroupID <> 0 Then
                        mnuDeleteAppointmentGroup.Enabled = True
                        tlbbtnDeleteAppointmentGroup.Enabled = True
                    Else
                        mnuDeleteAppointmentGroup.Enabled = False
                        tlbbtnDeleteAppointmentGroup.Enabled = False
                    End If

                    'Check Appointment is set as Pull Charts Appointment or not
                    If objAppointment.IsPullChartsAppointment = False Then
                        'This Appointment is not set by PULL CHARTS so Patient Visits can be registered
                        tlbbtnPatientVisit.Enabled = True
                        mnuPatientVisit.Enabled = True
                    Else
                        'This Appointment is set by PULL CHARTS so Patient Visits can not be registered
                        tlbbtnPatientVisit.Enabled = False
                        mnuPatientVisit.Enabled = False
                    End If

                Else
                    'No Appointment is selected
                    mnuDeleteAppointmentGroup.Enabled = False
                    tlbbtnDeleteAppointmentGroup.Enabled = False
                    tlbbtnModifyAppointment.Enabled = False
                    tlbbtnDeleteAppointment.Enabled = False
                    tlbbtnPatientVisit.Enabled = False
                    lblPatientNameLabel.Visible = False
                    lblChiefComplaintsLabel.Visible = False
                    lblPatientName.Visible = False
                    lblChiefComplaints.Visible = False
                    lblPatientName.Text = ""
                    lblChiefComplaints.Text = ""
                End If
                objAppointment = Nothing
                objApp = Nothing
            End If
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub
    Private Sub Fill_Appointments()
        AppointmentCalendar.Appointments.Clear()
        AppointmentCalendar.Resources.Clear()
        GetCheckedNodes()
        If clProviders.Count > 0 Then
            AppointmentCalendar.DailyCalendar.VisibleColumns = clProviders.Count
        End If

        Dim nCount As Int16
        For nCount = 1 To clProviders.Count
            AppointmentCalendar.Resources.Add(New Resource(clProviders.Item(nCount)))
            Dim dsData As New DataSet
            Dim objAppointment As New clsAppointments
            dsData = objAppointment.Fill_Appointments(AppointmentCalendar.FirstDateTime.Date, AppointmentCalendar.FirstDateTime.AddDays(7 * AppointmentCalendar.MonthlyCalendar.VisibleWeeks()).Date, clProviders.Item(nCount))
            'dsData = objAppointment.Fill_Appointments(MonthCalendar1.SelectionStart.Date, MonthCalendar1.SelectionStart.Date.AddDays(7 * AppointmentCalendar.MonthlyCalendar.VisibleWeeks()).Date, clProviders.Item(nCount))
            Dim strMessage As String
            Dim ap As Appointment
            Dim drRow As DataRow
            Dim nCount1 As Int16
            For nCount1 = 0 To dsData.Tables(0).Rows.Count - 1
                drRow = dsData.Tables(0).Rows(nCount1)
                ap = New Appointment
                With ap
                    .Tag = drRow.Item(0)
                    .ResourceIndex = nCount - 1
                    .AllDay = False
                    .DateBegin = CType(drRow.Item(1), Date)
                    '.DateEnd = CType(drRow.Item(1), Date).AddMinutes(gnAppointmentInterval)
                    .DateEnd = CType(drRow.Item(1), Date).AddMinutes(dsData.Tables(0).Rows(nCount1).Item(5))
                    .Text = drRow.Item(2) & "-" & drRow.Item(3)

                    'Check Appointment is Missing or not
                    If CType(drRow.Item(1), Date) < System.DateTime.Now.Date Then
                        'Retrieve Appointment Details
                        objAppointment.SearchAppointment(drRow.Item(0))
                        'Check Appointment is Pull Charts Appointment or not
                        If objAppointment.IsPullChartsAppointment = True Then
                            'Appointment is PULL CHARTS Appointment
                            .BackColor = Color.FromArgb(nPullChartsAppointmentsColor)
                        Else
                            'Appointment is not PULL CHARTS Appointment
                            'Appointment Date is less than today's date
                            'Check Appointment is Missing or not.. i.e. No visits are registered against this appointment
                            If objAppointment.IsMissingAppointment(drRow.Item(0)) = True Then
                                .BackColor = Color.FromArgb(nMissingAppointmentsColor)
                            Else
                                'Visits is already registered against this appointment
                                If IsDBNull(drRow(7)) = False Then
                                    If Trim(drRow(7)) <> "" Then
                                        .BackColor = Color.FromArgb(drRow(7))
                                    End If
                                End If
                            End If

                        End If
                    Else
                        'Appointment Date is greater than today's date
                        If IsDBNull(drRow(7)) = False Then
                            If Trim(drRow(7)) <> "" Then
                                .BackColor = Color.FromArgb(drRow(7))
                            End If
                        End If
                    End If

                End With
                AppointmentCalendar.Appointments.Add(ap)
                'ap.EnsureVisible()
            Next
            objAppointment = Nothing
            'Add Doctor Busy/Holiday Schedule
            Dim dtHolidaySchedule As New DataTable
            Dim objHoliday As New clsDoctorHolidaySchedule
            'dtHolidaySchedule = objHoliday.RetrieveSchedule(clProviders.Item(nCount), AppointmentCalendar.FirstDateTime.Date, AppointmentCalendar.FirstDateTime.AddDays(7 * AppointmentCalendar.MonthlyCalendar.VisibleWeeks()).Date)
            dtHolidaySchedule = objHoliday.RetrieveSchedule(clProviders.Item(nCount), clsDoctorHolidaySchedule.enmScheduleCriteria.All)
            objHoliday = Nothing
            For nCount1 = 0 To dtHolidaySchedule.Rows.Count - 1
                drRow = dtHolidaySchedule.Rows(nCount1)
                ap = New Appointment
                With ap
                    .BackColor = Color.FromArgb(nBusyTimeColor)
                    .Tag = ""
                    .ResourceIndex = nCount - 1
                    .AllDay = False
                    .DateBegin = drRow.Item(1)
                    .DateEnd = drRow.Item(2)
                    .Text = drRow.Item(3)
                End With
                AppointmentCalendar.Appointments.Add(ap)
            Next
        Next

    End Sub
    Private Function GetCheckedNodes() As Collection
        Dim nCount As Integer
        For nCount = clProviders.Count To 1 Step -1
            clProviders.Remove(nCount)
        Next
        Dim trvnde As TreeNode
        For Each trvnde In trvProviders.Nodes
            GetCheckedNodes(trvnde)
        Next
        Return clProviders
    End Function
    Private Sub GetCheckedNodes(ByVal rootNode As TreeNode)
        If rootNode.Checked Then
            If Trim(rootNode.Text) <> Trim(trvProviders.Nodes(0).Text) Then
                clProviders.Add(rootNode.Text)
            End If
        End If
        For Each childNode As TreeNode In rootNode.Nodes
            GetCheckedNodes(childNode)
        Next
    End Sub
    Private Sub mnuNewAppointment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewAppointment.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Call NewAppointment()
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub AppointmentCalendar_Default(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AppointmentCalendar.Default
        Try
            Me.Cursor = Cursors.WaitCursor
            If AppointmentCalendar.Appointments.SelectedCount = 0 Then
                mnuNewAppointment_Click(sender, e)
            Else
                Dim strID As String
                Dim objApp As Appointment
                objApp = AppointmentCalendar.Appointments.Selected(0)
                strID = Trim(objApp.Tag)
                objApp = Nothing
                If strID = "" Then
                    'Check Appointment Module Level
                    If gnAppointmentModuleLevel <> 0 Then
                        Me.Cursor = Cursors.Default
                        MessageBox.Show("You can not add any appointment.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        mnuNewAppointment_Click(sender, e)
                    End If
                Else
                    Me.Cursor = Cursors.Default
                    mnuModifyAppointment_Click(sender, e)
                End If
            End If
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub mnuModifyAppointment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuModifyAppointment.Click
        Try
            Call ModifyAppointment()
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            If Trim(lblDoctors.Text) = "" Then
                lblDoctors.Text = "Doctors - No Doctor Selected"
            Else
                lblDoctors.Text = ""
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AppointmentCalendar_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles AppointmentCalendar.DragDrop
        Try
            'Check Visit is registered or not

            Dim strDoctorName As String
            Dim objAppointment As New clsAppointments
            If AppointmentCalendar.Appointments.SelectedCount > 0 Then
                Dim objApp As Appointment
                objApp = AppointmentCalendar.Appointments.Selected(0)
                'Check Visit is registered against this appointment or not
                If objAppointment.IsVisitRegistered(objApp.Tag) = True Then
                    MessageBox.Show("Visit is already registered against this appointment." & vbCrLf & "So you can not modify this appointment", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Call Fill_Appointments()
                    objAppointment = Nothing
                    Exit Sub
                End If

                'Check with Clinic Time
                Dim nAppointmentTime As Long
                Dim nStartTime As Long
                Dim nEndTime As Long

                nStartTime = New TimeSpan(gClinicStartTime.Hour, gClinicStartTime.Minute, gClinicStartTime.Second).Ticks
                nEndTime = New TimeSpan(gClinicEndTime.Hour, gClinicEndTime.Minute, gClinicEndTime.Second).Ticks
                nAppointmentTime = New TimeSpan(objApp.DateBegin.Hour, objApp.DateBegin.Minute, objApp.DateBegin.Second).Ticks

                'Check Appointment Module Level
                If gnAppointmentModuleLevel <> 0 Then
                    'Check Clinic Timing
                    If nAppointmentTime < nStartTime Or nAppointmentTime > nEndTime Then
                        MessageBox.Show("Clinic Timing is " & Format(gClinicStartTime, "Medium Time") & " To " & Format(gClinicEndTime, "Medium Time") & "." & vbCrLf & "You can not add appointment at " & objApp.DateBegin, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Call Fill_Appointments()
                        Exit Sub
                    End If
                    'Check Doctor's Availability
                    Dim objDoctor As New clsDoctorHolidaySchedule
                    If objDoctor.IsDoctorAvailable(Trim(AppointmentCalendar.Resources(objApp.ResourceIndex).Text), objApp.DateBegin) = False Then
                        MessageBox.Show(Trim(AppointmentCalendar.Resources(objApp.ResourceIndex).Text) & " is not available at this time", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        objApp = Nothing
                        objDoctor = Nothing
                        Call Fill_Appointments()
                        Exit Sub
                    End If
                    objDoctor = Nothing
                End If



                Dim strMessage As String
                objAppointment.SearchAppointment(objApp.Tag)
                strDoctorName = objAppointment.Provider

                If Trim(strDoctorName) <> Trim(AppointmentCalendar.Resources(objApp.ResourceIndex).Text) Then
                    If objAppointment.AppointmentDate = objApp.DateBegin Then
                        strMessage = "Are you sure you want to change Doctor?"
                    Else
                        strMessage = "Are you sure you want to change Doctor as well as appointment time?"
                    End If
                    strDoctorName = ""
                    If MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                        'Check Appointment Module Level
                        If gnAppointmentModuleLevel <> 0 Then
                            'Check the Appointment is available or not
                            If objAppointment.CheckAppointmentAvailable(objApp.DateBegin, objApp.DateEnd, Trim(AppointmentCalendar.Resources(objApp.ResourceIndex).Text), objApp.Tag) = False Then
                                MessageBox.Show("You can not set the Appointment at " & objApp.DateBegin, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Call Fill_Appointments()
                                objAppointment = Nothing
                                Exit Sub
                            End If
                        End If
                        objAppointment.UpdateAppointmentDateTime(objApp.Tag, objApp.DateBegin, Trim(AppointmentCalendar.Resources(objApp.ResourceIndex).Text))
                        objAppointment = Nothing
                    End If
                    objApp = Nothing
                Else
                    If objAppointment.AppointmentDate <> objApp.DateBegin Then
                        strMessage = "Are you sure you want to change appointment time?"
                        If MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
                            'Check Appointment Module Level
                            If gnAppointmentModuleLevel <> 0 Then
                                'Check the Appointment is available or not
                                If objAppointment.CheckAppointmentAvailable(objApp.DateBegin, objApp.DateEnd, Trim(AppointmentCalendar.Resources(objApp.ResourceIndex).Text), objApp.Tag) = False Then
                                    MessageBox.Show("You can not set the Appointment at " & objApp.DateBegin, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Call Fill_Appointments()
                                    objAppointment = Nothing
                                    Exit Sub
                                End If
                            End If
                            objAppointment.UpdateAppointmentDateTime(objApp.Tag, objApp.DateBegin, Trim(AppointmentCalendar.Resources(objApp.ResourceIndex).Text))
                            objAppointment = Nothing
                        End If
                        objApp = Nothing
                    End If
                End If
                'Code commented by supriya on 04/07/2007
                'If MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                '    'Check Appointment Module Level
                '    If gnAppointmentModuleLevel <> 0 Then
                '        'Check the Appointment is available or not
                '        If objAppointment.CheckAppointmentAvailable(objApp.DateBegin, objApp.DateEnd, Trim(AppointmentCalendar.Resources(objApp.ResourceIndex).Text), objApp.Tag) = False Then
                '            MessageBox.Show("You can not set the Appointment at " & objApp.DateBegin, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            Call Fill_Appointments()
                '            objAppointment = Nothing
                '            Exit Sub
                '        End If
                '    End If
                '    objAppointment.UpdateAppointmentDateTime(objApp.Tag, objApp.DateBegin, Trim(AppointmentCalendar.Resources(objApp.ResourceIndex).Text))
                '    objAppointment = Nothing
                'End If
                'objApp = Nothing
                'Code commented by supriya on 04/07/2007
            End If
            Call Fill_Appointments()
            objAppointment = Nothing
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub AppointmentCalendar_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles AppointmentCalendar.DragEnter
        'Try
        '    Dim objApp As Appointment
        '    objApp = AppointmentCalendar.Appointments.Selected(0)
        '    strDoctorName = AppointmentCalendar.Resources(objApp.ResourceIndex).Text
        '    objApp = Nothing
        'Catch objErr As Exception
        '    MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub AppointmentCalendar_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles AppointmentCalendar.MouseDown
        Try
            If e.Button = MouseButtons.Right Then
                GetCheckedNodes()
                If clProviders.Count <= 0 Then
                    MessageBox.Show("Please select minimum one Provider", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                If AppointmentCalendar.Appointments.SelectedCount <= 0 Then
                    mnuModifyAppointment.Enabled = False
                    mnuDeleteAppointment.Enabled = False

                    'mnuPatientVisit.Enabled = False
                Else
                    mnuModifyAppointment.Enabled = True
                    mnuDeleteAppointment.Enabled = True
                    'mnuDeleteAppointmentGroup.Enabled = True
                    'mnuPatientVisit.Enabled = True
                End If
            End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub




    Private Sub initEvents()
        RemoveHandler AppointmentCalendar.CurrentCalendar.Click, AddressOf AppointmentCalendar_Click
        RemoveHandler AppointmentCalendar.CurrentCalendar.MouseDown, AddressOf AppointmentCalendar_MouseDown
        RemoveHandler AppointmentCalendar.CurrentCalendar.DragDrop, AddressOf AppointmentCalendar_DragDrop

        AddHandler AppointmentCalendar.CurrentCalendar.Click, AddressOf AppointmentCalendar_Click
        AddHandler AppointmentCalendar.CurrentCalendar.MouseDown, AddressOf AppointmentCalendar_MouseDown
        AddHandler AppointmentCalendar.CurrentCalendar.DragDrop, AddressOf AppointmentCalendar_DragDrop
    End Sub

    Private Sub mnuPatientVisit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPatientVisit.Click
        Try
            PatientVisit()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PatientVisit()
        Try
            If AppointmentCalendar.Appointments.SelectedCount = 0 Then
                MessageBox.Show("Please select the appointment", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Dim frmPatientVisit As New frmVisit
            frmPatientVisit.Fill_Provider()
            Dim objApp As Appointment
            objApp = AppointmentCalendar.Appointments.Selected(0)
            frmPatientVisit.dtpDate.Value = objApp.DateBegin
            frmPatientVisit.dtpTime.Value = objApp.DateBegin
            frmPatientVisit.cmbProvider.Text = AppointmentCalendar.Resources.Item(objApp.ResourceIndex).Text()
            Dim objAppointment As New clsAppointments
            objAppointment.SearchAppointment(objApp.Tag)
            frmPatientVisit.lblVisitID.Tag = objApp.Tag 'Appointment ID
            frmPatientVisit.lblVisitID.Text = 0 'Visit ID 0 for new visit
            frmPatientVisit.txtPatientCode.Tag = objAppointment.PatientID
            frmPatientVisit.txtPatientCode.Text = objAppointment.PatientCode
            frmPatientVisit.txtFirstName.Text = objAppointment.PatientFirstName
            frmPatientVisit.txtLastName.Text = objAppointment.PatientLastName
            objAppointment = Nothing
            frmPatientVisit.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub NewAppointment()
        Try
            If AppointmentCalendar.CurrentView = CalendarView.Daily Then
                'Check New Appointment time
                Dim nAppointmentTime As Long
                Dim nStartTime As Long
                Dim nEndTime As Long

                nStartTime = New TimeSpan(gClinicStartTime.Hour, gClinicStartTime.Minute, gClinicStartTime.Second).Ticks
                nEndTime = New TimeSpan(gClinicEndTime.Hour, gClinicEndTime.Minute, gClinicEndTime.Second).Ticks
                nAppointmentTime = New TimeSpan(AppointmentCalendar.SelectedDateBegin.Hour, AppointmentCalendar.SelectedDateBegin.Minute, AppointmentCalendar.SelectedDateBegin.Second).Ticks

                'Check Appointment Module Level
                If gnAppointmentModuleLevel <> 0 Then
                    'Appointment Module Level other than 0.
                    'Validate Clinic Working Time with Appointment Time
                    If nAppointmentTime < nStartTime Or nAppointmentTime > nEndTime Then
                        MessageBox.Show("Clinic Timing is " & Format(gClinicStartTime, "Medium Time") & " To " & Format(gClinicEndTime, "Medium Time") & "." & vbCrLf & "You can not add appointment at " & AppointmentCalendar.SelectedDateBegin, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If

            End If
            Dim frm As New frmAppointment
            frm.Fill_AppointmentSchedulerTypes()
            frm.Fill_Provider()
            frm.lblAppointmentID.Text = 0
            frm.txtFirstName.Text = gstrPatientFirstName
            frm.txtLastName.Text = gstrPatientLastName
            frm.txtPatientCode.Text = gstrPatientCode
            frm.txtPatientCode.Tag = gnPatientID
            'Remark

            If IsDate(AppointmentCalendar.SelectedDateBegin.Date) = True Then
                If Trim(CStr(Format(AppointmentCalendar.SelectedDateBegin.Date, "MM/dd/yyyy"))) = "01/01/0001" Then
                    frm.dtpDate.Value = MonthCalendar1.SelectionStart.Date
                Else
                    frm.dtpDate.Value = AppointmentCalendar.SelectedDateBegin.Date
                End If
            Else
                frm.dtpDate.Value = MonthCalendar1.SelectionStart.Date
            End If


            If IsDate(AppointmentCalendar.SelectedDateBegin.Date) = True Then
                If Trim(CStr(Format(AppointmentCalendar.SelectedDateBegin.Date, "MM/dd/yyyy"))) = "01/01/0001" Then
                    frm.dtpTime.Value = System.DateTime.Now
                Else
                    frm.dtpTime.Value = AppointmentCalendar.SelectedDateBegin
                End If
            Else
                frm.dtpTime.Value = System.DateTime.Now
            End If
            If gnAppointmentInterval > 0 Then
                frm.numAppointmentDuration.Value = gnAppointmentInterval
            End If
            frm.grpFutureAppointments.Enabled = False
            ''''''''''******This IF statement is added by Anil on 09/10/2007, Because for BlankDB the application was giving error
            If AppointmentCalendar.SelectedResource.MinValue > 0 Then
                frm.cmbProvider.Text = AppointmentCalendar.Resources.Item(AppointmentCalendar.SelectedResource).Text()
            End If
            Me.Cursor = Cursors.Default
            frm.blnAppointmentTypeChange = True
            If frm.ShowDialog() = DialogResult.OK Then
                Call Fill_Appointments()
                'DB Dim frmmdi As MainMenu
                'DB frmmdi = CType(Me.MdiParent, MainMenu)
                'DB frmmdi.Fill_Appointments()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ModifyAppointment()
        Try
            If AppointmentCalendar.Appointments.SelectedCount = 0 Then
                MessageBox.Show("Please select the appointment", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If



            Me.Cursor = Cursors.WaitCursor
            Dim objApp As Appointment
            objApp = AppointmentCalendar.Appointments.Selected(0)

            Dim objAppointment As New clsAppointments
            'Check Visit is registered against this appointment or not
            'If visit is registered the Appointment can not be modify
            If objAppointment.IsVisitRegistered(objApp.Tag) = True Then
                MessageBox.Show("Visit is already registered against this appointment." & vbCrLf & "So you can not modify this appointment", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                objAppointment = Nothing
                Exit Sub
            End If


            Dim frm As New frmAppointment
            frm.Fill_AppointmentSchedulerTypes()
            frm.Fill_Provider()
            frm.lblAppointmentID.Text = objApp.Tag
            frm.dtpDate.Value = objApp.DateBegin
            frm.dtpTime.Value = objApp.DateBegin
            frm.cmbProvider.Text = AppointmentCalendar.Resources.Item(objApp.ResourceIndex).Text()

            objAppointment.SearchAppointment(objApp.Tag)
            frm.txtPatientCode.Tag = objAppointment.PatientID()
            frm.txtPatientCode.Text = objAppointment.PatientCode
            frm.txtFirstName.Text = objAppointment.PatientFirstName
            frm.txtLastName.Text = objAppointment.PatientLastName
            frm.txtChiefComplaints.Text = objAppointment.Complaints
            If frm.cmbAppointmentTypes.FindString(Trim(objAppointment.AppointmentSchedulerType)) >= 0 Then
                frm.cmbAppointmentTypes.SelectedIndex = frm.cmbAppointmentTypes.FindString(Trim(objAppointment.AppointmentSchedulerType))
            End If
            Select Case objAppointment.AppointmentType
                Case "Phone"
                    frm.optPhone.Checked = True
                Case "Referral"
                    frm.optReferral.Checked = True
                Case "Email"
                    frm.optEmail.Checked = True
                Case "In Person"
                    frm.optInPerson.Checked = True
            End Select
            frm.numAppointmentDuration.Value = objAppointment.AppointmentInterval
            objAppointment = Nothing
            Me.Cursor = Cursors.Default
            frm.grpFutureAppointments.Enabled = False
            frm.cmbAppointmentTypes.Enabled = False
            frm.chkCustomizeAppointment.Enabled = False
            frm.blnAppointmentTypeChange = True
            If frm.ShowDialog() = DialogResult.OK Then
                Call Fill_Appointments()
                'DB Dim frmmdi As MainMenu
                'DB frmmdi = CType(Me.MdiParent, MainMenu)
                'DB frmmdi.Fill_Appointments()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub
    Private Sub DeleteAppointment()
        Try
            If AppointmentCalendar.Appointments.SelectedCount = 0 Then
                MessageBox.Show("Please select the appointment", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim objApp As Appointment
            objApp = AppointmentCalendar.Appointments.Selected(0)

            Dim objAppointment As New clsAppointments
            'Check Visit is registered against this appointment or not
            'If visit is registered the Appointment can not be modify
            If objAppointment.IsVisitRegistered(objApp.Tag) = True Then
                MessageBox.Show("Visit is already registered against this appointment." & vbCrLf & "So you can not delete this appointment", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                objAppointment = Nothing
                Exit Sub
            End If


            If MessageBox.Show("Are you sure you want to delete the selected appointment?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                objAppointment.DeleteAppointment(objApp.Tag)
                objAppointment = Nothing
                Call Fill_Appointments()
                'DB  Dim frmmdi As MainMenu
                'DB  frmmdi = CType(Me.MdiParent, MainMenu)
                'DB  frmmdi.Fill_Appointments()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuDeleteAppointment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteAppointment.Click
        Try
            DeleteAppointment()
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
    '    Try
    '        Select Case Trim(e.Button.Tag)
    '            Case "ShowHide"
    '                pnlLeft.Visible = Not pnlLeft.Visible
    '            Case "NewAppointment"
    '                Call NewAppointment()
    '            Case "ModifyAppointment"
    '                Call ModifyAppointment()
    '            Case "DeleteAppointment"
    '                Call DeleteAppointment()
    '            Case "DeleteAppointmentGroup"
    '                Call DeleteAppointmentGroup()
    '            Case "PatientVisit"
    '                Call PatientVisit()
    '            Case "PrintPreview"
    '                Call PrintPreview()
    '            Case "Print"
    '                Call PrintAppointments()
    '            Case "Close"
    '                Me.Close()
    '        End Select
    '    Catch objErr As Exception
    '        Me.Cursor = Cursors.Default
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub AppointmentCalendar_CurrentViewChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles AppointmentCalendar.CurrentViewChanged
        Try
            '''''''Code is added by Anil on 20071204

            If AppointmentCalendar.CurrentView = CalendarView.Daily Then
                AppFromDate = Now.Date
                AppToDate = Now.Date
            ElseIf AppointmentCalendar.CurrentView = CalendarView.Weekly Then
                AppFromDate = AppointmentCalendar.FirstDateTime
                AppToDate = AppFromDate.AddDays(5)
            ElseIf AppointmentCalendar.CurrentView = CalendarView.Monthly Then
                AppFromDate = AppointmentCalendar.FirstDateTime
                AppToDate = AppFromDate.AddDays(30)
            End If
            '''''''''''''''''''''''''''''
            Me.Cursor = Cursors.WaitCursor
            initEvents()
            AppointmentCalendar.FirstDateTime = MonthCalendar1.SelectionStart
            Call Fill_Appointments()
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub setAppointmentColor()
        Try
            picClinicWorkingTime.BackColor = Color.FromArgb(nWorkingTimeColor)
            picClinicNonWorkingTime.BackColor = Color.FromArgb(nNonWorkingTimeColor)
            picBusyTime.BackColor = Color.FromArgb(nBusyTimeColor)
            AppointmentCalendar.DailyCalendar.FreeHourColor = picClinicNonWorkingTime.BackColor
            AppointmentCalendar.DailyCalendar.WorkHourColor = picClinicWorkingTime.BackColor
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub picClinicWorkingTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picClinicWorkingTime.Click
        Try
            With ColorDialog1
                .AllowFullOpen = True
                .ShowHelp = False
                .Color = picClinicWorkingTime.BackColor
                If .ShowDialog() = DialogResult.OK Then
                    picClinicWorkingTime.BackColor = .Color
                    AppointmentCalendar.DailyCalendar.WorkHourColor = picClinicWorkingTime.BackColor
                    nWorkingTimeColor = picClinicWorkingTime.BackColor.ToArgb
                    'Dim regKey As RegistryKey
                    'If IsNothing(Registry.LocalMachine.OpenSubKey("Software\gloEMR")) = True Then
                    '    regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", True)
                    '    regKey.CreateSubKey("gloEMR")
                    '    regKey.Close()
                    'End If
                    'regKey = Registry.LocalMachine.OpenSubKey("Software\gloEMR", True)
                    'regKey.SetValue("WorkingTime", nWorkingTimeColor)
                    'regKey.Close()
                    'regKey = Nothing
                    If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) = False Then
                        gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoft, True)
                        gloRegistrySetting.CreateSubKey(gloRegistrySetting.gstrEMR)
                        gloRegistrySetting.CloseRegistryKey()

                    End If

                    gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
                    gloRegistrySetting.SetRegistryValue("WorkingTime", nWorkingTimeColor)
                    gloRegistrySetting.CloseRegistryKey()

                End If
            End With
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub picClinicNonWorkingTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picClinicNonWorkingTime.Click
        Try
            With ColorDialog1
                .AllowFullOpen = True
                .ShowHelp = False
                .Color = picClinicNonWorkingTime.BackColor
                If .ShowDialog() = DialogResult.OK Then
                    picClinicNonWorkingTime.BackColor = .Color
                    AppointmentCalendar.DailyCalendar.FreeHourColor = picClinicNonWorkingTime.BackColor
                    nNonWorkingTimeColor = picClinicNonWorkingTime.BackColor.ToArgb
                    'Dim regKey As RegistryKey
                    'If IsNothing(Registry.LocalMachine.OpenSubKey("Software\gloEMR")) = True Then
                    '    regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", True)
                    '    regKey.CreateSubKey("gloEMR")
                    '    regKey.Close()
                    'End If
                    'regKey = Registry.LocalMachine.OpenSubKey("Software\gloEMR", True)
                    'regKey.SetValue("WorkingTime", nNonWorkingTimeColor)
                    'regKey.Close()
                    'regKey = Nothing
                    If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) = False Then
                        gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoft, True)
                        gloRegistrySetting.CreateSubKey(gloRegistrySetting.gstrEMR)
                        gloRegistrySetting.CloseRegistryKey()

                    End If
                    gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
                    gloRegistrySetting.SetRegistryValue("WorkingTime", nWorkingTimeColor)
                    gloRegistrySetting.CloseRegistryKey()

                End If
            End With
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub picBusyTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picBusyTime.Click
        Try
            With ColorDialog1
                .AllowFullOpen = True
                .ShowHelp = False
                .Color = picBusyTime.BackColor
                If .ShowDialog() = DialogResult.OK Then
                    picBusyTime.BackColor = .Color
                    nBusyTimeColor = picBusyTime.BackColor.ToArgb
                    'Dim regKey As RegistryKey
                    'If IsNothing(Registry.LocalMachine.OpenSubKey("Software\gloEMR")) = True Then
                    '    regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE", True)
                    '    regKey.CreateSubKey("gloEMR")
                    '    regKey.Close()
                    'End If
                    'regKey = Registry.LocalMachine.OpenSubKey("Software\gloEMR", True)
                    'regKey.SetValue("BusyTimeColor", nBusyTimeColor)
                    'regKey.Close()
                    'regKey = Nothing
                    If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) = False Then
                        gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoft, True)
                        gloRegistrySetting.CreateSubKey(gloRegistrySetting.gstrEMR)
                        gloRegistrySetting.CloseRegistryKey()

                    End If
                    gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
                    gloRegistrySetting.SetRegistryValue("BusyTimeColor", nBusyTimeColor)
                    gloRegistrySetting.CloseRegistryKey()
                End If
            End With
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Delete Selected Appointment Group
    Private Sub DeleteAppointmentGroup()
        Try
            If AppointmentCalendar.Appointments.SelectedCount = 0 Then
                MessageBox.Show("Please select the appointment", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            Dim objApp As Appointment
            objApp = AppointmentCalendar.Appointments.Selected(0)

            Dim objAppointment As New clsAppointments
            'Check Visit is registered against this appointment or not
            'If visit is registered the Appointment can not be modify
            If objAppointment.IsVisitRegistered(objApp.Tag) = True Then
                MessageBox.Show("Visit is already registered against this appointment." & vbCrLf & "So you can not delete this appointment", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                objAppointment = Nothing
                Exit Sub
            End If


            If MessageBox.Show("Are you sure you want to delete the selected appointment group?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                objAppointment.DeleteAppointmentGroup(objApp.Tag)
                objAppointment = Nothing
                Call Fill_Appointments()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    'Delete Appointment Group
    Private Sub mnuDeleteAppointmentGroup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteAppointmentGroup.Click
        Try
            DeleteAppointmentGroup()
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Function IsMissingAppointment(ByVal nAppointmentID As Integer) As Boolean
    '    Try
    '        Dim blnMissingAppointment As Boolean

    '        Return blnMissingAppointment
    '    Catch objErr As Exception
    '        Return False
    '    End Try
    'End Function

    Private Sub PrintPreview()
        'PrintDocument1.DefaultPageSettings.Landscape = True
        'PrintPreviewDialog1.WindowState = FormWindowState.Maximized
        'PrintPreviewDialog1.ShowDialog()

        ''---------------------------Added by Anil on 20071205
        Try
            blnPrint = True
            GetCheckedNodes()
            Dim frm As New frmViewMainPatientDemographics
            frm.FormName = "Appointments"
            frm.Text = "Appointments"
            frm.mycaller = Me
            frm.AppFromDate = AppFromDate
            frm.AppToDate = AppToDate
            frm.PrintAppointmentReport(blnPrint)
            frm.Show()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ''-------------------------------------------

    End Sub
    ''--------------------------This Function is Added by Anil on 20071205
    Public Function GetProviderID(ByVal strProvider As String) As Long
        Try
            Dim objProvider As New clsProvider
            Dim ds As New DataSet
            Dim ProviderID As Long = 0
            ds = objProvider.ScanProvider(strProvider)
            If IsNothing(ds) = False Then
                ProviderID = ds.Tables(0).Rows(0)(0)
            End If
            Return ProviderID
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
    ''----------------------------------------------------------------------

    Private Sub PrintAppointments()
        ''' PrintDocument1.Print()
        '''''''--------------------------''Code to add report for Appointment
        '''''----------------------------'''Code is added by Anil on 20071204
        Try
            blnPrint = False
            AppointmentReports()
            
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ''''''---------------------------------------------------------------------------------------------
    End Sub
    Public Function AppointmentReports()
        Try
            GetCheckedNodes()
            Dim frm As New frmViewMainPatientDemographics
            frm.FormName = "Appointments"
            frm.Text = "Appointments"
            frm.mycaller = Me
            frm.AppFromDate = AppFromDate
            frm.AppToDate = AppToDate
            frm.PrintAppointmentReport(blnPrint)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        'Dim font As Font
        'font = New Font("Arial", 14, FontStyle.Bold)
        'e.Graphics.DrawString("Appointment Report", font, SystemBrushes.WindowText, e.PageBounds.Width / 2 - 50, 20)
        'Select Case AppointmentCalendar.CurrentView
        '    Case CalendarView.Daily
        '        e.Graphics.DrawString("Date " & AppointmentCalendar.DailyCalendar.FirstDate, New Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, e.PageBounds.Width / 2 - 30, 60)
        '        e.HasMorePages = True
        '        Dim Height As Int16
        '        Select Case nPageNo
        '            Case 0
        '                AppointmentCalendar.DailyCalendar.FirstHour = New TimeSpan(0, 0, 0)
        '                Height = AppointmentCalendar.DailyCalendar.GetHourRectangle(New TimeSpan(8, 0, 0), False, True).Top
        '                e.Graphics.DrawString("Page No. 1", New Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, e.PageBounds.Width / 2 - 25, Height + 150)
        '            Case 1
        '                AppointmentCalendar.DailyCalendar.FirstHour = New TimeSpan(8, 0, 0)
        '                Height = AppointmentCalendar.DailyCalendar.GetHourRectangle(New TimeSpan(16, 0, 0), False, True).Top
        '                e.Graphics.DrawString("Page No. 2", New Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, e.PageBounds.Width / 2 - 25, Height + 150)
        '            Case 2
        '                AppointmentCalendar.DailyCalendar.FirstHour = New TimeSpan(16, 0, 0)
        '                Height = AppointmentCalendar.DailyCalendar.GetHourRectangle(New TimeSpan(24, 0, 0), False, True).Top
        '                e.Graphics.DrawString("Page No. 3", New Font("Arial", 12, FontStyle.Regular), SystemBrushes.WindowText, e.PageBounds.Width / 2 - 25, Height + 150)
        '                e.HasMorePages = False
        '        End Select
        '        nPageNo += 1
        '        AppointmentCalendar.DailyCalendar.Print(e.Graphics, New Rectangle(e.PageBounds.X + 10, 100, e.PageBounds.Width - 20, Height))
        '    Case CalendarView.Weekly
        '        Dim X, Y, Height, Width As Integer
        '        X = e.PageBounds.X + 10
        '        Y = 70
        '        Height = e.PageBounds.Height - 50
        '        Width = e.PageBounds.Width - 20
        '        e.Graphics.DrawRectangle(SystemPens.ControlText, X - 2, Y - 2, Width - 4, Height + 4)
        '        AppointmentCalendar.WeeklyCalendar.Print(e.Graphics, New Rectangle(X, Y, Width, Height))
        '    Case CalendarView.Monthly
        '        Dim X, Y, Height, Width As Integer
        '        X = e.PageBounds.X + 10
        '        Y = 70
        '        Height = e.PageBounds.Height - 50
        '        Width = e.PageBounds.Width - 20
        '        e.Graphics.DrawRectangle(SystemPens.ControlText, X - 2, Y - 2, Width - 4, Height + 4)
        '        AppointmentCalendar.MonthlyCalendar.Print(e.Graphics, New Rectangle(X, Y, Width, Height))
        'End Select

    End Sub
    Private Sub mnuPrintPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintPreview.Click
        Try
            nPageNo = 0
            Call PrintPreview()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub mnuPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrint.Click
        Try
            nPageNo = 0
            Call PrintAppointments()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub PrintDocument1_BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.BeginPrint
        'If AppointmentCalendar.CurrentView = CalendarView.Daily Then
        '    AppointmentCalendar.Dock = DockStyle.None
        '    'AppointmentCalendar.DailyCalendar.Height = 1000
        '    nPageNo = 0
        'End If
    End Sub
    Private Sub PrintDocument1_EndPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.EndPrint
        'If AppointmentCalendar.CurrentView = CalendarView.Daily Then
        '    AppointmentCalendar.Dock = DockStyle.Fill
        '    nPageNo = 0
        'End If
    End Sub




    'Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked
    '    Try
    '        Select Case Trim(e.Button.Tag)
    '            Case "ShowHide"
    '                pnlLeft.Visible = Not pnlLeft.Visible
    '            Case "NewAppointment"
    '                Call NewAppointment()
    '            Case "ModifyAppointment"
    '                Call ModifyAppointment()
    '            Case "DeleteAppointment"
    '                Call DeleteAppointment()
    '            Case "DeleteAppointmentGroup"
    '                Call DeleteAppointmentGroup()
    '            Case "PatientVisit"
    '                Call PatientVisit()
    '            Case "PrintPreview"
    '                Call PrintPreview()
    '            Case "Print"
    '                Call PrintAppointments()
    '            Case "Close"
    '                Me.Close()
    '        End Select
    '    Catch objErr As Exception
    '        Me.Cursor = Cursors.Default
    '        MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub ToolStrip1_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "ShowHide"
                    'pnlLeft.Visible = Not pnlLeft.Visible
                    'sarika 3rd July 07
                    If e.ClickedItem.Text = "Show" Then
                        e.ClickedItem.Text = "Hide"
                        pnlLeft.Visible = True
                    Else
                        e.ClickedItem.Text = "Show"
                        pnlLeft.Visible = False
                    End If
                    '---------------------------

                Case "NewAppointment"
                    Call NewAppointment()
                Case "ModifyAppointment"
                    Call ModifyAppointment()
                Case "DeleteAppointment"
                    Call DeleteAppointment()
                Case "DeleteAppointmentGroup"
                    Call DeleteAppointmentGroup()
                Case "PatientVisit"
                    Call PatientVisit()
                    ''---------------------------Modified by Anil on 20071205
                Case "Default Preview"
                    Call PrintPreview()
                Case "Customize Preview"
                    Call CustomizeDateToolStripMenuItem1_Click(sender, e)
                Case "Default Print"
                    Call PrintAppointments()
                Case "Customize Print"
                    Call tlsCustomizePrint_Click(sender, e)
                    ''--------------------------------------
                Case "Close"
                    Me.Close()
            End Select
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''---------------------------Added by Anil on 20071205
    Private Sub CustomizeDateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DefaultPreviewToolStripMenuItem.Click
        '''''''Code is added by Anil on 20071204
        Try
            pnlCustomizeApp.Visible = False
            If AppointmentCalendar.CurrentView = CalendarView.Daily Then
                AppFromDate = Now.Date
                AppToDate = Now.Date
            ElseIf AppointmentCalendar.CurrentView = CalendarView.Weekly Then
                AppFromDate = AppointmentCalendar.FirstDateTime
                AppToDate = AppFromDate.AddDays(5)
            ElseIf AppointmentCalendar.CurrentView = CalendarView.Monthly Then
                AppFromDate = AppointmentCalendar.FirstDateTime
                AppToDate = AppFromDate.AddDays(30)
            End If
            '''''''''''''''''''''''''''''
            PrintPreview()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''---------------------------Added by Anil on 20071205
    Private Sub CustomizeDateToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomizeDateToolStripMenuItem1.Click
        Try
            blnPrint = True
            pnlCustomizeApp.Visible = True
            dtpicFrom.Value = Now.Date
            dtpicTo.Value = Now.Date
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Try
            AppFromDate = dtpicFrom.Value
            AppToDate = dtpicTo.Value
            pnlCustomizeApp.Visible = False
            If blnPrint = False Then
                PrintAppointments()
            Else
                PrintPreview()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            pnlCustomizeApp.Visible = False
            blnPrint = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlsDefaultPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsDefaultPrint.Click

        Try
            blnPrint = False
            pnlCustomizeApp.Visible = False
            If AppointmentCalendar.CurrentView = CalendarView.Daily Then
                AppFromDate = Now.Date
                AppToDate = Now.Date
            ElseIf AppointmentCalendar.CurrentView = CalendarView.Weekly Then
                AppFromDate = AppointmentCalendar.FirstDateTime
                AppToDate = AppFromDate.AddDays(5)
            ElseIf AppointmentCalendar.CurrentView = CalendarView.Monthly Then
                AppFromDate = AppointmentCalendar.FirstDateTime
                AppToDate = AppFromDate.AddDays(30)
            End If
            PrintAppointments()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlsCustomizePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsCustomizePrint.Click
        Try
            blnPrint = False
            pnlCustomizeApp.Visible = True
            dtpicFrom.Value = Now.Date
            dtpicTo.Value = Now.Date
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '''''''''-----------------------------------------------up to here

End Class
