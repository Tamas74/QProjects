Imports gloTaskMail

Public Class frmTasks
    Inherits System.Windows.Forms.Form


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal LogID As Long, Optional ByVal TaskType As ClsTasksDBLayer.TaskType = ClsTasksDBLayer.TaskType.Task, Optional ByVal blnOpenFromExam As Boolean = False, Optional ByVal blnRecordLock As Boolean = False)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        LoginId = LogID
        _TaskType = TaskType
        _blnOpenFromExam = blnOpenFromExam

        'Add any initialization after the InitializeComponent() call
        intFlag = 0
    End Sub
    Public Sub New(ByVal ID As Long, ByVal intID As Long, Optional ByVal TaskType As ClsTasksDBLayer.TaskType = ClsTasksDBLayer.TaskType.Task, Optional ByVal blnRecordLock As Boolean = False)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        LoginId = ID
        TaskId = intID
        intFlag = 0
        _TaskType = TaskType
        'Add any initialization after the InitializeComponent() call

    End Sub
    Public Sub New(ByVal ID As Long, ByVal intID As System.Int64, ByVal flag As Int64, Optional ByVal TaskType As ClsTasksDBLayer.TaskType = ClsTasksDBLayer.TaskType.Task, Optional ByVal blnRecordLock As Boolean = False)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        LoginId = ID
        TaskId = intID
        intFlag = flag
        _TaskType = TaskType
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
    Friend WithEvents pnlFrom As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFrom As System.Windows.Forms.TextBox
    Friend WithEvents btnClearTo As System.Windows.Forms.Button
    Friend WithEvents btnTo As System.Windows.Forms.Button
    Friend WithEvents pnlSubject As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents dtpDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTaskDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbPriority As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtSubject As System.Windows.Forms.TextBox
    Friend WithEvents cmbTo As System.Windows.Forms.ComboBox
    Friend WithEvents pnlNotes As System.Windows.Forms.Panel
    Friend WithEvents GrpNotes As System.Windows.Forms.GroupBox
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents btnClearPatient As System.Windows.Forms.Button
    Friend WithEvents btnPatient As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPatient As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_Task As System.Windows.Forms.ToolStrip
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnShowDocs As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnShowLabs As System.Windows.Forms.ToolStripButton
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents ts_btnShowOrders As System.Windows.Forms.ToolStripButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTasks))
        Me.pnlFrom = New System.Windows.Forms.Panel
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtPatient = New System.Windows.Forms.TextBox
        Me.btnClearPatient = New System.Windows.Forms.Button
        Me.btnPatient = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.cmbTo = New System.Windows.Forms.ComboBox
        Me.btnClearTo = New System.Windows.Forms.Button
        Me.btnTo = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtFrom = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlSubject = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.txtSubject = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.cmbPriority = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.dtpDueDate = New System.Windows.Forms.DateTimePicker
        Me.dtpTaskDate = New System.Windows.Forms.DateTimePicker
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbStatus = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.pnlNotes = New System.Windows.Forms.Panel
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.GrpNotes = New System.Windows.Forms.GroupBox
        Me.txtNotes = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.ts_Task = New System.Windows.Forms.ToolStrip
        Me.ts_btnShowDocs = New System.Windows.Forms.ToolStripButton
        Me.ts_btnShowLabs = New System.Windows.Forms.ToolStripButton
        Me.ts_btnShowOrders = New System.Windows.Forms.ToolStripButton
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlFrom.SuspendLayout()
        Me.pnlSubject.SuspendLayout()
        Me.pnlNotes.SuspendLayout()
        Me.GrpNotes.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_Task.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlFrom
        '
        Me.pnlFrom.BackColor = System.Drawing.Color.Transparent
        Me.pnlFrom.Controls.Add(Me.Label9)
        Me.pnlFrom.Controls.Add(Me.Label10)
        Me.pnlFrom.Controls.Add(Me.Label11)
        Me.pnlFrom.Controls.Add(Me.Label12)
        Me.pnlFrom.Controls.Add(Me.txtPatient)
        Me.pnlFrom.Controls.Add(Me.btnClearPatient)
        Me.pnlFrom.Controls.Add(Me.btnPatient)
        Me.pnlFrom.Controls.Add(Me.Label8)
        Me.pnlFrom.Controls.Add(Me.cmbTo)
        Me.pnlFrom.Controls.Add(Me.btnClearTo)
        Me.pnlFrom.Controls.Add(Me.btnTo)
        Me.pnlFrom.Controls.Add(Me.Label2)
        Me.pnlFrom.Controls.Add(Me.txtFrom)
        Me.pnlFrom.Controls.Add(Me.Label1)
        Me.pnlFrom.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFrom.Location = New System.Drawing.Point(0, 112)
        Me.pnlFrom.Name = "pnlFrom"
        Me.pnlFrom.Padding = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.pnlFrom.Size = New System.Drawing.Size(594, 98)
        Me.pnlFrom.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(4, 94)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(586, 1)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "label2"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 2)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 93)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "label4"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(590, 2)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 93)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "label3"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(3, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(588, 1)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "label1"
        '
        'txtPatient
        '
        Me.txtPatient.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtPatient.ForeColor = System.Drawing.Color.Black
        Me.txtPatient.Location = New System.Drawing.Point(104, 68)
        Me.txtPatient.Name = "txtPatient"
        Me.txtPatient.ReadOnly = True
        Me.txtPatient.Size = New System.Drawing.Size(398, 22)
        Me.txtPatient.TabIndex = 8
        '
        'btnClearPatient
        '
        Me.btnClearPatient.BackgroundImage = CType(resources.GetObject("btnClearPatient.BackgroundImage"), System.Drawing.Image)
        Me.btnClearPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearPatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearPatient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearPatient.Image = CType(resources.GetObject("btnClearPatient.Image"), System.Drawing.Image)
        Me.btnClearPatient.Location = New System.Drawing.Point(538, 65)
        Me.btnClearPatient.Name = "btnClearPatient"
        Me.btnClearPatient.Size = New System.Drawing.Size(24, 24)
        Me.btnClearPatient.TabIndex = 7
        '
        'btnPatient
        '
        Me.btnPatient.BackgroundImage = CType(resources.GetObject("btnPatient.BackgroundImage"), System.Drawing.Image)
        Me.btnPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPatient.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPatient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPatient.Image = CType(resources.GetObject("btnPatient.Image"), System.Drawing.Image)
        Me.btnPatient.Location = New System.Drawing.Point(508, 65)
        Me.btnPatient.Name = "btnPatient"
        Me.btnPatient.Size = New System.Drawing.Size(24, 24)
        Me.btnPatient.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(23, 72)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 14)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "For Patient :"
        '
        'cmbTo
        '
        Me.cmbTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTo.ForeColor = System.Drawing.Color.Black
        Me.cmbTo.Location = New System.Drawing.Point(104, 37)
        Me.cmbTo.Name = "cmbTo"
        Me.cmbTo.Size = New System.Drawing.Size(398, 22)
        Me.cmbTo.TabIndex = 1
        '
        'btnClearTo
        '
        Me.btnClearTo.BackgroundImage = CType(resources.GetObject("btnClearTo.BackgroundImage"), System.Drawing.Image)
        Me.btnClearTo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearTo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearTo.Image = CType(resources.GetObject("btnClearTo.Image"), System.Drawing.Image)
        Me.btnClearTo.Location = New System.Drawing.Point(538, 35)
        Me.btnClearTo.Name = "btnClearTo"
        Me.btnClearTo.Size = New System.Drawing.Size(24, 24)
        Me.btnClearTo.TabIndex = 3
        '
        'btnTo
        '
        Me.btnTo.BackgroundImage = CType(resources.GetObject("btnTo.BackgroundImage"), System.Drawing.Image)
        Me.btnTo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTo.Image = CType(resources.GetObject("btnTo.Image"), System.Drawing.Image)
        Me.btnTo.Location = New System.Drawing.Point(508, 35)
        Me.btnTo.Name = "btnTo"
        Me.btnTo.Size = New System.Drawing.Size(24, 24)
        Me.btnTo.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = " Assigned To :"
        '
        'txtFrom
        '
        Me.txtFrom.ForeColor = System.Drawing.Color.Black
        Me.txtFrom.Location = New System.Drawing.Point(104, 8)
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(398, 22)
        Me.txtFrom.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Assigned By :"
        '
        'pnlSubject
        '
        Me.pnlSubject.BackColor = System.Drawing.Color.Transparent
        Me.pnlSubject.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlSubject.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlSubject.Controls.Add(Me.lbl_RightBrd)
        Me.pnlSubject.Controls.Add(Me.lbl_TopBrd)
        Me.pnlSubject.Controls.Add(Me.txtSubject)
        Me.pnlSubject.Controls.Add(Me.Label7)
        Me.pnlSubject.Controls.Add(Me.cmbPriority)
        Me.pnlSubject.Controls.Add(Me.Label6)
        Me.pnlSubject.Controls.Add(Me.dtpDueDate)
        Me.pnlSubject.Controls.Add(Me.dtpTaskDate)
        Me.pnlSubject.Controls.Add(Me.Label4)
        Me.pnlSubject.Controls.Add(Me.Label3)
        Me.pnlSubject.Controls.Add(Me.cmbStatus)
        Me.pnlSubject.Controls.Add(Me.Label5)
        Me.pnlSubject.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSubject.Location = New System.Drawing.Point(0, 0)
        Me.pnlSubject.Name = "pnlSubject"
        Me.pnlSubject.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlSubject.Size = New System.Drawing.Size(594, 112)
        Me.pnlSubject.TabIndex = 1
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 108)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(586, 1)
        Me.lbl_BottomBrd.TabIndex = 20
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 4)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 105)
        Me.lbl_LeftBrd.TabIndex = 19
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(590, 4)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 105)
        Me.lbl_RightBrd.TabIndex = 18
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(588, 1)
        Me.lbl_TopBrd.TabIndex = 17
        Me.lbl_TopBrd.Text = "label1"
        '
        'txtSubject
        '
        Me.txtSubject.ForeColor = System.Drawing.Color.Black
        Me.txtSubject.Location = New System.Drawing.Point(104, 72)
        Me.txtSubject.MaxLength = 100
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Size = New System.Drawing.Size(458, 22)
        Me.txtSubject.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(45, 79)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 14)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Subject :"
        '
        'cmbPriority
        '
        Me.cmbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPriority.ForeColor = System.Drawing.Color.Black
        Me.cmbPriority.Location = New System.Drawing.Point(396, 40)
        Me.cmbPriority.Name = "cmbPriority"
        Me.cmbPriority.Size = New System.Drawing.Size(166, 22)
        Me.cmbPriority.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(345, 47)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 14)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Priority :"
        '
        'dtpDueDate
        '
        Me.dtpDueDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpDueDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpDueDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpDueDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpDueDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpDueDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDueDate.Location = New System.Drawing.Point(104, 41)
        Me.dtpDueDate.Name = "dtpDueDate"
        Me.dtpDueDate.Size = New System.Drawing.Size(196, 22)
        Me.dtpDueDate.TabIndex = 2
        '
        'dtpTaskDate
        '
        Me.dtpTaskDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpTaskDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpTaskDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpTaskDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpTaskDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpTaskDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpTaskDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTaskDate.Location = New System.Drawing.Point(104, 8)
        Me.dtpTaskDate.Name = "dtpTaskDate"
        Me.dtpTaskDate.Size = New System.Drawing.Size(196, 22)
        Me.dtpTaskDate.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(35, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 14)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Due Date :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(32, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 14)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Task Date :"
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.ForeColor = System.Drawing.Color.Black
        Me.cmbStatus.Location = New System.Drawing.Point(396, 8)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(166, 22)
        Me.cmbStatus.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(347, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 14)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Status :"
        '
        'pnlNotes
        '
        Me.pnlNotes.BackColor = System.Drawing.Color.Transparent
        Me.pnlNotes.Controls.Add(Me.Label13)
        Me.pnlNotes.Controls.Add(Me.Label14)
        Me.pnlNotes.Controls.Add(Me.Label15)
        Me.pnlNotes.Controls.Add(Me.Label16)
        Me.pnlNotes.Controls.Add(Me.GrpNotes)
        Me.pnlNotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlNotes.Location = New System.Drawing.Point(0, 210)
        Me.pnlNotes.Name = "pnlNotes"
        Me.pnlNotes.Padding = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.pnlNotes.Size = New System.Drawing.Size(594, 194)
        Me.pnlNotes.TabIndex = 2
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(4, 190)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(586, 1)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "label2"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 2)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 189)
        Me.Label14.TabIndex = 7
        Me.Label14.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(590, 2)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 189)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "label3"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(3, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(588, 1)
        Me.Label16.TabIndex = 5
        Me.Label16.Text = "label1"
        '
        'GrpNotes
        '
        Me.GrpNotes.BackColor = System.Drawing.Color.Transparent
        Me.GrpNotes.Controls.Add(Me.txtNotes)
        Me.GrpNotes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrpNotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpNotes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GrpNotes.Location = New System.Drawing.Point(3, 1)
        Me.GrpNotes.Name = "GrpNotes"
        Me.GrpNotes.Size = New System.Drawing.Size(588, 190)
        Me.GrpNotes.TabIndex = 0
        Me.GrpNotes.TabStop = False
        Me.GrpNotes.Text = "Notes"
        '
        'txtNotes
        '
        Me.txtNotes.BackColor = System.Drawing.Color.White
        Me.txtNotes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNotes.ForeColor = System.Drawing.Color.Black
        Me.txtNotes.Location = New System.Drawing.Point(8, 21)
        Me.txtNotes.MaxLength = 255
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(576, 163)
        Me.txtNotes.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.pnlNotes)
        Me.Panel1.Controls.Add(Me.pnlFrom)
        Me.Panel1.Controls.Add(Me.pnlSubject)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 53)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(594, 404)
        Me.Panel1.TabIndex = 4
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlToolStrip.Controls.Add(Me.ts_Task)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlToolStrip.ForeColor = System.Drawing.Color.Black
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(594, 53)
        Me.pnlToolStrip.TabIndex = 12
        '
        'ts_Task
        '
        Me.ts_Task.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ts_Task.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_Task.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_Task.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_Task.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_Task.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_Task.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnShowDocs, Me.ts_btnShowLabs, Me.ts_btnShowOrders, Me.ts_btnSave, Me.ts_btnClose})
        Me.ts_Task.Location = New System.Drawing.Point(0, 0)
        Me.ts_Task.Name = "ts_Task"
        Me.ts_Task.Size = New System.Drawing.Size(594, 53)
        Me.ts_Task.TabIndex = 1
        Me.ts_Task.Text = "ToolStrip1"
        '
        'ts_btnShowDocs
        '
        Me.ts_btnShowDocs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnShowDocs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnShowDocs.Image = CType(resources.GetObject("ts_btnShowDocs.Image"), System.Drawing.Image)
        Me.ts_btnShowDocs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnShowDocs.Name = "ts_btnShowDocs"
        Me.ts_btnShowDocs.Size = New System.Drawing.Size(119, 50)
        Me.ts_btnShowDocs.Tag = "Show Document"
        Me.ts_btnShowDocs.Text = "Show &Documents"
        Me.ts_btnShowDocs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnShowDocs.ToolTipText = "Show Documents"
        Me.ts_btnShowDocs.Visible = False
        '
        'ts_btnShowLabs
        '
        Me.ts_btnShowLabs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnShowLabs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnShowLabs.Image = CType(resources.GetObject("ts_btnShowLabs.Image"), System.Drawing.Image)
        Me.ts_btnShowLabs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnShowLabs.Name = "ts_btnShowLabs"
        Me.ts_btnShowLabs.Size = New System.Drawing.Size(78, 50)
        Me.ts_btnShowLabs.Tag = "Show Lab"
        Me.ts_btnShowLabs.Text = "Show &Labs"
        Me.ts_btnShowLabs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnShowLabs.Visible = False
        '
        'ts_btnShowOrders
        '
        Me.ts_btnShowOrders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnShowOrders.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnShowOrders.Image = CType(resources.GetObject("ts_btnShowOrders.Image"), System.Drawing.Image)
        Me.ts_btnShowOrders.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnShowOrders.Name = "ts_btnShowOrders"
        Me.ts_btnShowOrders.Size = New System.Drawing.Size(90, 50)
        Me.ts_btnShowOrders.Tag = "Show Orders"
        Me.ts_btnShowOrders.Text = "Show &Orders"
        Me.ts_btnShowOrders.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnShowOrders.Visible = False
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnSave.Image = CType(resources.GetObject("ts_btnSave.Image"), System.Drawing.Image)
        Me.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSave.Name = "ts_btnSave"
        Me.ts_btnSave.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnSave.Tag = "Save"
        Me.ts_btnSave.Text = "&Save&&Cls"
        Me.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnSave.ToolTipText = "Save and Close"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmTasks
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(594, 457)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTasks"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Task Details"
        Me.pnlFrom.ResumeLayout(False)
        Me.pnlFrom.PerformLayout()
        Me.pnlSubject.ResumeLayout(False)
        Me.pnlSubject.PerformLayout()
        Me.pnlNotes.ResumeLayout(False)
        Me.GrpNotes.ResumeLayout(False)
        Me.GrpNotes.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_Task.ResumeLayout(False)
        Me.ts_Task.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private ObjTasksDBLayer As ClsTasksDBLayer
    Private TaskId As Long
    Private strLoginName As String
    Private intFlag As Int16  'Identifies if form loaded from view form or mdi
    Private LoginId As Long

    '' 20070102
    Private _TaskType As ClsTasksDBLayer.TaskType
    Private _TaskDate As Date
    Private _LabNo As String

    Private _PatientID As Long
    Private _VisitID As Long

    '' For Record Level Lock 
    Private _blnRecordLock As Boolean

    '''''
    'Private WithEvents dgCustomGrid As CustomDataGrid
    Private WithEvents dgcustomGrid As CustomTask
    Dim ReferralCount As Integer
    Private btnStatus As Integer
    '' btnStatus=1 For Users 
    '' btnStatus=2 For Patients

    '' By Mahesh, 20070322
    '' To Open Task from Patient Exam
    Private _blnOpenFromExam As Boolean
    Private _ExamId As Int64
    Private _dtDOS As DateTime
    ''

    'sarika
    Private _ReAssignFlag As Boolean

    'Pramod 20070509
    Private Col_Check As Integer = 5
    Private Col_UserID As Integer = 0
    Private Col_LoginName As Integer = 1
    Private Col_Column1 As Integer = 2
    Private Col_Column2 As Integer = 3
    Private Col_ProviderID As Integer = 4

    Private Col_Count As Integer = 6

    'pramod 20070509
    Private Col_nPatientID As Integer = 0
    Private Col_PatientCode As Integer = 1
    Private Col_Column12 As Integer = 2
    Private Col_Column22 As Integer = 3

    Private Col_count2 As Integer = 4

    'sarika 1st oct 07
    Dim sColumnName As String = ""
    '------------------
    Dim DocumentID As Long
    Dim Year As String
    Public Property ReAssignFlag() As Boolean
        Get
            Return _ReAssignFlag
        End Get
        Set(ByVal Value As Boolean)
            _ReAssignFlag = Value
        End Set
    End Property

    Public Property dtDOS() As DateTime
        Get
            Return _dtDOS
        End Get
        Set(ByVal Value As DateTime)
            _dtDOS = Value
        End Set
    End Property
    Public Property ExamID() As Int64
        Get
            Return _ExamId
        End Get
        Set(ByVal Value As Int64)
            _ExamId = Value
        End Set
    End Property

    Private Sub FillControls()
        cmbStatus.Items.Add("Not Started")
        cmbStatus.Items.Add("In Progress")
        cmbStatus.Items.Add("Completed")
        cmbStatus.Items.Add("Waiting on someone else")
        cmbStatus.Items.Add("Deferred")
        cmbStatus.SelectedIndex = -1
        cmbStatus.Text = "Not Started"

        cmbPriority.Items.Add("Low")
        cmbPriority.Items.Add("Normal")
        cmbPriority.Items.Add("High")
        cmbPriority.SelectedIndex = -1
        cmbPriority.Text = "Low"

        If intFlag <> 0 Then   'Opened from MDI form
            dtpDueDate.Enabled = False
            dtpTaskDate.Enabled = False
            cmbPriority.Enabled = False
            'btnClearTo.Enabled = False
            'btnTo.Enabled = False

            txtSubject.Enabled = False
            txtPatient.Enabled = False
            btnPatient.Enabled = False
            btnClearPatient.Enabled = False
            txtFrom.Enabled = False
            '''''''This change is made by Anil on 06/10/2007
            '''''''This is done to highlight the controls after making them disable.
            txtSubject.BackColor = Color.White
            txtPatient.BackColor = Color.White
            txtFrom.BackColor = Color.White
        End If
    End Sub

    Private Sub frmTasks_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            '' <><><> Unlock the Record <><><>
            '' Mahesh - 20070723
            If _blnRecordLock = False Then
                '' if the Locked by by the Current User & on Current Machine only
                UnLock_Transaction(TrnType.Task, TaskId, 0, Now)
            End If
            '' <><><> Unlock the Record <><><>
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmTasks_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            ObjTasksDBLayer = New ClsTasksDBLayer

            txtPatient.Tag = 0
            FillControls()
            If TaskId <> 0 Then
                'Opened in Edit mode
                Try

                    Dim arrlist As ArrayList
                    txtPatient.Tag = 0
                    arrlist = ObjTasksDBLayer.FetchTasksForUpdate(TaskId)
                    If Not IsNothing(arrlist) Then
                        SetData(arrlist)
                    End If

                    '''' 20070102
                    '' If Task is of type Order radiology
                    If _TaskType = ClsTasksDBLayer.TaskType.OrderRadiology Then
                        If arrlist.Count > 0 Then
                            '' Make 'Show Order' Button Visible
                            'btnShowOrders.Visible = True
                            'btnShowLabResult.Visible = False
                            'btnShowDoc.Visible = False

                            ts_btnShowOrders.Visible = True
                            ts_btnShowLabs.Visible = False
                            ts_btnShowDocs.Visible = False
                            ''
                            _TaskDate = CType(arrlist.Item(1), System.DateTime)
                            _PatientID = CType(arrlist.Item(8), System.String)
                            _VisitID = GenerateVisitID(_TaskDate)



                            btnPatient.Enabled = False
                            btnClearPatient.Enabled = False
                        End If
                    End If
                    ''''

                    '' If Task is of type Lab Order
                    If _TaskType = ClsTasksDBLayer.TaskType.LabOrder Then
                        If arrlist.Count > 0 Then
                            '' Make 'Show Order' Button Visible
                            'btnShowOrders.Visible = False
                            'btnShowLabResult.Visible = True
                            'btnShowDoc.Visible = False
                            ''
                            ts_btnShowOrders.Visible = False
                            ts_btnShowLabs.Visible = True
                            ts_btnShowDocs.Visible = False
                            ''

                            _TaskDate = CType(arrlist.Item(1), System.DateTime)
                            _PatientID = CType(arrlist.Item(8), System.String)
                            _VisitID = GenerateVisitID(_TaskDate)
                            If IsNothing(arrlist.Item(11)) = False Then
                                _LabNo = CType(arrlist.Item(11), System.String)
                            Else
                                _LabNo = ""
                            End If
                            btnPatient.Enabled = False
                            btnClearPatient.Enabled = False

                        End If
                    End If

                    If _TaskType = ClsTasksDBLayer.TaskType.DMS Then
                        If arrlist.Count > 0 Then
                            '' Make 'Show Order' Button Visible
                            'btnShowOrders.Visible = False
                            'btnShowLabResult.Visible = False
                            'btnShowDoc.Visible = True

                            ts_btnShowOrders.Visible = False
                            ts_btnShowLabs.Visible = False
                            ts_btnShowDocs.Visible = True
                            ''
                            _TaskDate = CType(arrlist.Item(1), System.DateTime)
                            _PatientID = CType(arrlist.Item(8), System.String)
                            _VisitID = GenerateVisitID(_TaskDate)
                            btnPatient.Enabled = False
                            btnClearPatient.Enabled = False
                        End If
                    End If

                    arrlist = ObjTasksDBLayer.FetchTasksDetailsForUpdate(TaskId)  'Fetch Referrals against Patient
                    If Not IsNothing(arrlist) Then
                        SetTaskDetails(arrlist)
                    End If
                Catch ex As SqlClient.SqlException
                    MessageBox.Show(ex.Message, "Task Details", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.Close()
                End Try
            Else
                '''' Opened in Add Mode
                txtFrom.Tag = LoginId
                txtFrom.Text = gstrLoginName

                '''' By Mahesh, 20070322
                If _blnOpenFromExam = True Then
                    'Dim _strSQL As String
                    'Dim strTaskID As String = ""
                    'Dim oDB As New gloStream.gloDataBase.gloDataBase
                    '_strSQL = " Select DISTINCT nTaskId FROM Tasks_Mst WHERE (nPatientID =  " & gnPatientID & ") " _
                    '    & " AND convert(varchar, dtTaskDate,101)='" & Format(_dtDOS, "MM/dd/yyyy") & "'"

                    'oDB.Connect(GetConnectionString)

                    'strTaskID = oDB.ExecuteQueryScaler(_strSQL)
                    'If strTaskID <> "" Then
                    '    TaskId = CLng(strTaskID)
                    'Else
                    '    TaskId = 0
                    'End If

                    'oDB.Disconnect()
                    'oDB = Nothing
                    TaskId = 0
                    If TaskId > 0 Then
                        'Opened in Edit mode
                        Try
                            Dim arrlist As ArrayList
                            txtPatient.Tag = 0
                            arrlist = ObjTasksDBLayer.FetchTasksForUpdate(TaskId)
                            If Not IsNothing(arrlist) Then
                                SetData(arrlist)
                            End If

                            '''' 20070102
                            '' If Task is of type Order
                            If _TaskType = ClsTasksDBLayer.TaskType.OrderRadiology Then
                                If arrlist.Count > 0 Then
                                    '' Make 'Show Order' Button Visible
                                    'btnShowOrders.Visible = True
                                    'btnShowLabResult.Visible = False

                                    ts_btnShowOrders.Visible = True
                                    ts_btnShowLabs.Visible = False

                                    _TaskDate = CType(arrlist.Item(1), System.DateTime)
                                    _PatientID = CType(arrlist.Item(8), System.String)
                                    _VisitID = GenerateVisitID(_TaskDate)
                                    btnPatient.Enabled = False
                                    btnClearPatient.Enabled = False
                                End If
                            End If
                            ''''

                            If _TaskType = ClsTasksDBLayer.TaskType.LabOrder Then
                                If arrlist.Count > 0 Then
                                    'btnShowOrders.Visible = False
                                    'btnShowLabResult.Visible = True

                                    ts_btnShowOrders.Visible = False
                                    ts_btnShowLabs.Visible = True

                                    _TaskDate = CType(arrlist.Item(1), System.DateTime)
                                    _PatientID = CType(arrlist.Item(8), System.String)
                                    _VisitID = GenerateVisitID(_TaskDate)
                                    btnPatient.Enabled = False
                                    btnClearPatient.Enabled = False
                                End If
                            End If

                            arrlist = ObjTasksDBLayer.FetchTasksDetailsForUpdate(TaskId)  'Fetch Referrals against Patient
                            If Not IsNothing(arrlist) Then
                                SetTaskDetails(arrlist)
                            End If

                        Catch ex As SqlClient.SqlException
                            MessageBox.Show(ex.Message, "Task Details", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Me.Close()
                        End Try
                    Else
                        dtpDueDate.Text = dtDOS
                        dtpTaskDate.Value = dtDOS
                    End If
                    txtPatient.Text = gstrPatientFirstName & " " & gstrPatientLastName
                    txtPatient.Tag = gnPatientID
                End If
            End If
            'Pramod 05122007
            '            gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Patient Tasks Opened", gstrLoginName, gstrClientMachineName, txtPatient.Tag)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "Patient Tasks Opened", gloAuditTrail.ActivityOutCome.Success)
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "Patient Tasks Opened", gloAuditTrail.ActivityOutCome.Success)


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        Finally
            If _blnRecordLock = True Then
                'btnOK.Enabled = False
            End If
        End Try
    End Sub
    Private Sub SetTaskDetails(ByRef arrlist As ArrayList)
        Dim i As Integer
        If Not IsNothing(arrlist) Then
            For i = 0 To arrlist.Count - 1
                cmbTo.Items.Add(arrlist.Item(i))
                If i = 0 Then
                    Dim objmylist As myList
                    objmylist = (CType(arrlist.Item(0), myList))
                    cmbTo.Text = objmylist.Description
                End If
            Next
        End If
    End Sub

    ''''Commented by Pramod on 04102008 for implement old Tasks Starts

    'Private Sub SetData(ByVal arrlist As ArrayList)
    '    'select nFromId,dtTaskDate,
    '    '	sSubject,dtDuedate,sPriority,sStatus,sNotes from
    '    '	Tasks_Mst where nTaskID=@nTaskID
    '    If arrlist.Count > 0 Then
    '        txtFrom.Tag = CType(arrlist.Item(0), Long)

    '        txtSubject.Text = CType(arrlist.Item(2), System.String)
    '        If ReAssignFlag = True Then
    '            dtpDueDate.Value = Now
    '            dtpTaskDate.Value = Now
    '        Else
    '            dtpDueDate.Text = CType(arrlist.Item(3), System.DateTime)
    '            dtpTaskDate.Value = CType(arrlist.Item(1), System.DateTime)
    '        End If

    '        cmbPriority.Text = CType(arrlist.Item(4), System.String)
    '        cmbStatus.Text = CType(arrlist.Item(5), System.String)
    '        txtNotes.Text = CType(arrlist.Item(6), System.String)
    '        txtFrom.Text = CType(arrlist.Item(7), System.String)
    '        txtPatient.Tag = CType(arrlist.Item(8), System.String)
    '        txtPatient.Text = CType(arrlist.Item(9), System.String)
    '        If _TaskType = ClsTasksDBLayer.TaskType.DMS Then
    '            DocumentID = CType(arrlist.Item(11), Long)
    '        End If
    '        If _TaskType = ClsTasksDBLayer.TaskType.Exam Then
    '            _ExamId = CType(arrlist.Item(11), Int64)
    '        End If

    '    End If
    'End Sub
    ''''Commented by Pramod on 04102008 for implement old Tasks End
    Private Sub SetData(ByVal arrlist As ArrayList)
        'select nFromId,dtTaskDate,
        '	sSubject,dtDuedate,sPriority,sStatus,sNotes from
        '	Tasks_Mst where nTaskID=@nTaskID
        If arrlist.Count > 0 Then
            txtFrom.Tag = CType(arrlist.Item(0), Long)

            txtSubject.Text = CType(arrlist.Item(2), System.String)
            If ReAssignFlag = True Then
                dtpDueDate.Value = Now
                dtpTaskDate.Value = Now
            Else
                dtpDueDate.Text = CType(arrlist.Item(3), System.DateTime)
                dtpTaskDate.Value = CType(arrlist.Item(1), System.DateTime)
            End If

            cmbPriority.Text = CType(arrlist.Item(4), System.String)
            cmbStatus.Text = CType(arrlist.Item(5), System.String)
            txtNotes.Text = CType(arrlist.Item(6), System.String)
            txtFrom.Text = CType(arrlist.Item(7), System.String)
            txtPatient.Tag = CType(arrlist.Item(8), System.String)
            txtPatient.Text = CType(arrlist.Item(9), System.String)

            'Changes made for view Document directly from Task Form
            'Below Commmented Code is previous Code
            'Changes By - Sagar Ghodke 20080719

            'If _TaskType = ClsTasksDBLayer.TaskType.DMS Then
            '    DocumentID = CType(Split(arrlist.Item(11), ",").GetValue(1), Long)
            'End If

            If _TaskType = ClsTasksDBLayer.TaskType.DMS Then

                Dim DocInfo() As String
                DocInfo = Split(arrlist.Item(11), ",")

                If (DocInfo.Length = 2) Then
                    Year = CType(DocInfo.GetValue(0), String)
                    DocumentID = CType(DocInfo.GetValue(1), Long)
                Else
                    DocumentID = 0
                    Year = ""
                End If
            End If

            'End Changes 20080719

            If _TaskType = ClsTasksDBLayer.TaskType.Exam Then
                _ExamId = CType(arrlist.Item(11), Int64)
            End If

        End If
    End Sub
    Private Sub btnOK_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim ToolTip1 = New System.Windows.Forms.ToolTip
        'ToolTip1.SetToolTip(Me.btnOK, "Save Task Details")
    End Sub

    Private Sub btnClose_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim ToolTip1 = New System.Windows.Forms.ToolTip
        'ToolTip1.SetToolTip(Me.btnClose, "Close Task Details")
    End Sub

    Private Sub btnTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTo.Click

        RemoveControl()
        btnStatus = 1
        LoadGrid()

        'When users custom control is visible them hide the search textbox and label
        dgcustomGrid.Label1.Visible = False
        dgcustomGrid.txtsearch.Visible = False
    End Sub
    Private Sub LoadGrid()
        Try
            AddControl()
            If Not IsNothing(dgcustomGrid) Then
                dgcustomGrid.Top = pnlSubject.Top
                dgcustomGrid.Left = pnlSubject.Left
                dgcustomGrid.Height = pnlSubject.Height + pnlNotes.Height '+ pnlOK.Height
                dgcustomGrid.Visible = True
                dgcustomGrid.Width = pnlSubject.Width

                dgcustomGrid.BringToFront()
                BindGrid()
                dgcustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Task Details", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            Dim dt As DataTable

            ReferralCount = 0
            Dim col As New DataColumn

            If btnStatus = 1 Then
                dt = ObjTasksDBLayer.FillControls(btnStatus)
                '''' Add One Column to the Datatable For CheckBOX
                col.ColumnName = "Select"
                col.DataType = System.Type.GetType("System.Boolean")
                col.DefaultValue = CBool("False")
                dt.Columns.Add(col)

            ElseIf btnStatus = 2 Then
                'Fill datatable with Patient Record
                dt = ObjTasksDBLayer.FillControls(btnStatus)
            End If


            If Not IsNothing(dt) Then
                If btnStatus = 1 Then
                    '' For DataBinding Users
                    dgcustomGrid.datasource(ObjTasksDBLayer.DsDataview)
                    ' Sort data view on Login Name
                    ObjTasksDBLayer.SortDataview(ObjTasksDBLayer.DsDataview.Table.Columns(1).ColumnName)
                ElseIf btnStatus = 2 Then
                    '''' For Databinding Patient
                    dgcustomGrid.datasource(ObjTasksDBLayer.DsDataview)
                    ' Sort data view on First Name
                    'sarika 1st oct 07
                    'changed the sortdataview from column 2 to column3 as column3 is Last name
                    ObjTasksDBLayer.SortDataview(ObjTasksDBLayer.DsDataview.Table.Columns(3).ColumnName)
                    dgcustomGrid.Label1.Text = "Last Name"
                    '-----------------------
                End If

            End If
            ReferralCount = dt.Rows.Count
            HideColumns()
            'End With


        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Task Details", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Task Details", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub HideColumns()

        ' Dim ts As New clsDataGridTableStyle(ObjTasksDBLayer.DsDataview.Table.TableName)
        'Dim ts As DataGridTableStyle = New DataGridTableStyle
        'ts.MappingName = ObjTasksDBLayer.DsDataview.Table.TableName
        'ts.ReadOnly = True
        ' ts.RowHeadersVisible = True
        'ts.AlternatingBackColor = System.Drawing.Color.Gainsboro
        'ts.HeaderBackColor = System.Drawing.Color.WhiteSmoke
        'ts.HeaderFont = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

        ' ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {IDCol, TemplateGalleryNameCol, CategoryCol, ProviderCol, DescriptionCol})

        Dim _TotalWidth As Single = dgcustomGrid.C1Task.Width - 5
        If btnStatus = 1 Then
            ' '' Show User Info
            With dgcustomGrid.C1Task
                .Cols.Fixed = 0
                .Rows.Fixed = 1
                .Cols.Count = 6
                .AllowEditing = True

                .SetData(0, Col_Check, "Select")
                '.Cols(Col_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(Col_Check).Width = _TotalWidth * 0.09
                .Cols(Col_Check).AllowEditing = True


                .SetData(0, Col_UserID, "UserID")
                '.Cols(Col_UserID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(Col_UserID).Width = _TotalWidth * 0
                .Cols(Col_UserID).AllowEditing = False

                .SetData(0, Col_LoginName, "Login Name")
                '.Cols(Col_LoginName).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(Col_LoginName).Width = _TotalWidth * 0.44
                .Cols(Col_LoginName).AllowEditing = False

                .SetData(0, Col_Column1, "Name")
                '.Cols(Col_Column1).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(Col_Column1).Width = _TotalWidth * 0.47
                .Cols(Col_Column1).AllowEditing = False

                .Cols(Col_ProviderID).Width = 0
                .Cols(Col_Column2).Width = 0

                'Move the last column select to first column
                .Cols.Move(.Cols.Count - 1, 0)
            End With


            'Dim dgID As New DataGridTextBoxColumn

            'With dgID
            '    .MappingName = ObjTasksDBLayer.DsDataview.Table.Columns(0).ColumnName
            '    .Alignment = HorizontalAlignment.Center
            '    .Width = 0x
            '    .NullText = ""
            '    .ReadOnly = True
            'End With

            'Dim dgCol1 As New DataGridTextBoxColumn

            'With dgCol1
            '    .MappingName = ObjTasksDBLayer.DsDataview.Table.Columns(1).ColumnName
            '    .HeaderText = "Login Name"
            '    .Width = dgcustomGrid.Gridwidth / 2.3
            '    .NullText = ""
            '    .ReadOnly = True
            'End With

            'Dim dgCol2 As New DataGridTextBoxColumn
            'With dgCol2
            '    .MappingName = ObjTasksDBLayer.DsDataview.Table.Columns(2).ColumnName
            '    .HeaderText = "User Name"
            '    .Width = dgcustomGrid.Gridwidth / 2.3
            '    .NullText = ""
            '    .ReadOnly = True
            'End With
            ''ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {dgID, dgCol1, dgCol2})
        ElseIf btnStatus = 2 Then
            '''' Show Patient Info
            With dgcustomGrid.C1Task
                .Cols.Fixed = 0
                .Cols.Count = 4

                .SetData(0, Col_nPatientID, "nPatientID")
                .Cols(Col_nPatientID).Width = 0

                .SetData(0, Col_PatientCode, "Patient Code")
                .Cols(Col_PatientCode).Width = _TotalWidth * 0.19
                '.Cols(Col_PatientCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .SetData(0, Col_Column12, "First Name")
                .Cols(Col_Column12).Width = _TotalWidth * 0.39
                '.Cols(Col_Column12).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

                .SetData(0, Col_Column22, "Last Name")
                .Cols(Col_Column22).Width = _TotalWidth * 0.39
                '.Cols(Col_Column22).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            End With

            'Dim dgID As New DataGridTextBoxColumn
            'With dgID
            '    .MappingName = ObjTasksDBLayer.DsDataview.Table.Columns(0).ColumnName
            '    .Alignment = HorizontalAlignment.Center
            '    .Width = 0
            '    .NullText = ""
            '    .ReadOnly = True
            'End With

            'Dim dgCol1 As New DataGridTextBoxColumn
            'With dgCol1
            '    .MappingName = ObjTasksDBLayer.DsDataview.Table.Columns(1).ColumnName
            '    .HeaderText = "Patient Code"
            '    .Width = dgcustomGrid.Gridwidth / 5
            '    .NullText = ""
            '    .ReadOnly = True
            'End With

            'Dim dgCol2 As New DataGridTextBoxColumn
            'With dgCol2
            '    .MappingName = ObjTasksDBLayer.DsDataview.Table.Columns(2).ColumnName
            '    .HeaderText = "First Name"
            '    .Width = dgcustomGrid.Gridwidth / 3
            '    .NullText = ""
            '    .ReadOnly = True
            'End With

            'Dim dgCol3 As New DataGridTextBoxColumn
            'With dgCol3
            '    .MappingName = ObjTasksDBLayer.DsDataview.Table.Columns(3).ColumnName
            '    .HeaderText = "Last  Name"
            '    .Width = dgcustomGrid.Gridwidth / 3
            '    .NullText = ""
            '    .ReadOnly = True
            'End With
            '(for while)ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {dgID, dgCol1, dgCol2, dgCol3})
        End If

        'dgContactsList.TableStyles.Clear()
        '  dgcustomGrid.SetTableStyleCol(ts)
    End Sub
    Private Sub AddControl()
        'If Not IsNothing(dgCustomGrid) Then
        '    RemoveControl()
        'End If
        dgcustomGrid = New CustomTask
        Me.Controls.Add(dgcustomGrid)
        'If intStatus = 1 Or intStatus = 2 Or intStatus = 4 Then
        '    dgCustomGrid.Parent = Me.TabPage1
        'Else
        '    dgCustomGrid.Parent = Me.TabPage3
        'End If

        ''''dgcustomGrid.Visible = True
        'dgCustomGrid.Dock = DockStyle.Fill
        'dgCustomGrid.Dock = DockStyle.Bottom

        ''''dgcustomGrid.BringToFront()
        dgcustomGrid.SetVisible = False
    End Sub
    Private Sub RemoveControl()
        If Not IsNothing(dgcustomGrid) Then
            Me.Controls.Remove(dgcustomGrid)
            dgcustomGrid.Visible = False
            dgcustomGrid = Nothing
        End If
    End Sub
    Private Sub SetGridValues(Optional ByVal dblstatus As System.Int16 = 0)
        Try
            If btnStatus = 1 Then

                If dblstatus = 0 Then
                    Dim i As Integer
                    Dim count As Integer

                    'For i = 0 To ReferralCount - 1
                    '    If dgcustomGrid.GetSelect(i) > 0 Then

                    '        'If cmbTo.FindStringExact(CType(dgCustomGrid.GetItem(i, 2), System.String)) < 0 Then
                    '        If FindDuplicateTo(CType(dgcustomGrid.GetItem(i, 0), Long)) Then
                    '            cmbTo.Items.Add(New myList(CType(dgcustomGrid.GetItem(i, 0), Long), CType(dgcustomGrid.GetItem(i, 1), System.String)))
                    '            cmbTo.Text = CType(dgcustomGrid.GetItem(i, 1), System.String)
                    '        End If
                    '    End If
                    'Next

                    'Pramod 05122007
                    ''Bind the checked user in the combo box

                    For i = 1 To dgcustomGrid.C1Task.Rows.Count - 1
                        If dgcustomGrid.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                            If FindDuplicateTo(CType(dgcustomGrid.C1Task.GetData(i, 1), Long)) Then
                                cmbTo.Items.Add(New myList(CType(dgcustomGrid.C1Task.GetData(i, 1), Long), CType(dgcustomGrid.C1Task.GetData(i, 2), System.String)))
                                cmbTo.Text = CType(dgcustomGrid.GetItem(i, 2), System.String)
                            End If
                        End If
                    Next

                    'ElseIf dblstatus = 1 Then
                    '    If FindDuplicateTo(CType(dgcustomGrid.GetItem(dgcustomGrid.GetCurrentrowIndex, 0), Long)) Then
                    '        'If cmbTo.FindStringExact(CType(dgCustomGrid.GetItem(dgCustomGrid.GetCurrentrowIndex, 2), System.String)) < 0 Then
                    '        cmbTo.Items.Add(New myList(CType(dgcustomGrid.GetItem(dgcustomGrid.GetCurrentrowIndex, 0), Long), CType(dgcustomGrid.GetItem(dgcustomGrid.GetCurrentrowIndex, 1), System.String)))
                    '        cmbTo.Text = CType(dgcustomGrid.GetItem(dgcustomGrid.GetCurrentrowIndex, 1), System.String)
                    '    End If
                End If
            ElseIf btnStatus = 2 Then

                ''''<><><><><> Check Patient Status <><><><><><>''''
                '''' 20060918 -Mahesh 
                Dim PatID As Long = 0
                '''''''********** The following IF statement is added by Anil on 06/10/2007 
                '''''''********** This is added because the application was giving error : "Index was out of range" while adding blank Patient name in Add Task form.
                If Not dgcustomGrid.GetCurrentrowIndex >= 0 Then
                    MessageBox.Show("Patient's Name Required", "Add Tasks", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                Else
                    PatID = CType(dgcustomGrid.GetItem(dgcustomGrid.GetCurrentrowIndex, 0), Long)
                    '''''<><><><><> Check Patient Status <><><><><><>''''
                    ''''' 20070125 -Mahesh 
                    If CheckPatientStatus(PatID) = False Then
                        Exit Sub
                    End If
                    '''''<><><><><> Check Patient Status <><><><><><>''''

                    txtPatient.Text = CType(dgcustomGrid.GetItem(dgcustomGrid.GetCurrentrowIndex, 2), System.String) & " " & CType(dgcustomGrid.GetItem(dgcustomGrid.GetCurrentrowIndex, 3), System.String)
                    txtPatient.Tag = CType(dgcustomGrid.GetItem(dgcustomGrid.GetCurrentrowIndex, 0), Long)
                    btnPatient.Focus()
                End If
            End If
            RemoveControl()
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Task Details", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RemoveControl()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Task Details", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RemoveControl()
        Finally
            'RemoveControl()
        End Try
    End Sub
    Private Function FindDuplicateTo(ByVal Id As Long) As Boolean
        Dim i As Integer
        For i = 0 To cmbTo.Items.Count - 1
            Dim objmylist As myList
            objmylist = (CType(cmbTo.Items.Item(i), myList))
            If Id = objmylist.Index Then
                Return False
                Exit Function
            End If
        Next
        Return True
    End Function


    Private Sub dgcustomGrid_DragLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgcustomGrid.DragLeave

    End Sub

    Private Sub dgcustomGrid_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgcustomGrid.MouseUp

    End Sub

    Private Sub dgCustomGrid_SearchChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgcustomGrid.SearchChanged
        Try

            Dim mystr As String

            'sarika 1st oct 07
            Dim ind As Integer
            ind = dgcustomGrid.colIndex
            '---------------------

            mystr = Replace(Trim(dgcustomGrid.SearchText), "'", "''")
            'sarika 1st oct 07
            If ind > 0 Then
                ObjTasksDBLayer.SetRowFilter(ind, mystr)
            Else

                ObjTasksDBLayer.SetRowFilter(mystr)
            End If




            '-----------------------
            'Dim dt As DataTable
            ReferralCount = ObjTasksDBLayer.DsDataview.Count
            'If Not IsNothing(dt) Then
            '    ReferralCount = dt.Rows.Count
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Task Details", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'Pramod 05122007
        'Patient has search then  set the column width for flexgrid
        Dim _TotalWidth As Single = dgcustomGrid.C1Task.Width - 5
        If btnStatus = 2 Then

            dgcustomGrid.C1Task.Cols.Count = 4
            dgcustomGrid.C1Task.Cols(Col_nPatientID).Width = 0
            dgcustomGrid.C1Task.Cols(Col_PatientCode).Width = _TotalWidth * 0.19
            dgcustomGrid.C1Task.Cols(Col_Column12).Width = _TotalWidth * 0.39
            dgcustomGrid.C1Task.Cols(Col_Column22).Width = _TotalWidth * 0.39

        End If


    End Sub

    Private Sub dgCustomGrid_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgcustomGrid.CloseClick
        RemoveControl()
    End Sub

    Private Sub dgCustomGrid_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgcustomGrid.OKClick
        SetGridValues(0)
    End Sub

    Private Sub dgCustomGrid_Dblclick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgcustomGrid.Dblclick
        SetGridValues(1)
    End Sub

    Private Sub btnClearTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearTo.Click
        'If cmbTo.SelectedText <> "" Then
        If MessageBox.Show("Do you want to clear selected user?", "Task Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
            'cmbTo.Items.Clear()
            Try
                cmbTo.Items.Remove(CType(cmbTo.SelectedItem, myList))
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Patient Messages", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

        End If
        'End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnSave.Click
        Try
            If Trim(txtFrom.Text) = "" Then
                MessageBox.Show("Assigned By Required", "Task Details", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtFrom.Focus()
                Exit Sub
            ElseIf cmbTo.Items.Count < 1 Then
                MessageBox.Show("Select User to whom you want to assign task", "Task Details", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbTo.Focus()
                Exit Sub
            ElseIf cmbStatus.Text = "" Then
                MessageBox.Show("Status Required", "Task Details", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbStatus.Focus()
                Exit Sub
            ElseIf cmbPriority.Text = "" Then
                MessageBox.Show("Priority Required", "Task Details", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                cmbPriority.Focus()
                Exit Sub
            ElseIf Trim(txtSubject.Text) = "" Then
                MessageBox.Show("Enter Subject information", "Task Details", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtSubject.Focus()
                Exit Sub
            ElseIf Trim(txtPatient.Text) = "" Then
                MessageBox.Show("Enter Patient information", "Task Details", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtPatient.Focus()
                Exit Sub
            End If

            '''''<><><><><> Check Patient Status <><><><><><>''''
            ''''' 20071011 -Mahesh 
            If CheckPatientStatus(txtPatient.Tag) = False Then
                Exit Sub
            End If
            '''''<><><><><> Check Patient Status <><><><><><>''''


            Dim arrlist As New ArrayList
            GetData(arrlist)

            Dim ArrTo(cmbTo.Items.Count - 1) As Long
            Dim mlist As myList
            Dim i As Integer
            'If chkLstInsurance.CheckedItems.Count <> 0 Then

            '    For i = 0 To chkLstInsurance.CheckedItems.Count - 1
            '        mlist = chkLstInsurance.CheckedItems(i)
            '        ArrInsurance(i) = mlist.Index
            '    Next
            'End If
            If cmbTo.Items.Count <> 0 Then
                For i = 0 To cmbTo.Items.Count - 1
                    mlist = cmbTo.Items.Item(i)
                    ArrTo(i) = mlist.Index
                Next
            End If
            If TaskId = 0 Then
                ObjTasksDBLayer.AddData(arrlist, ArrTo, _TaskType, ReAssignFlag)
            Else
                If ReAssignFlag = True Then
                    ObjTasksDBLayer.AddData(arrlist, ArrTo, _TaskType, ReAssignFlag)
                Else
                    If arrlist.Count > 1 Then
                        If arrlist.Item(1) <> 0 Then 'FromID
                            ObjTasksDBLayer.UpdateData(arrlist, ArrTo, _TaskType)
                        End If
                    End If
                End If
            End If

            If _TaskType = ClsTasksDBLayer.TaskType.LabOrder Then
                Dim objgloEMRLab As New gloEMRGeneralLibrary.gloEMRLab.gloEMRLabOrder

                objgloEMRLab.UpdateLabOrderUsers(ArrTo, _TaskDate)
            End If

            ''--- Add Task New 
            ''''Commented by Pramod for old Task on 04102008
            ''Call AddTasks("Exams", txtNotes.Text.Trim, dtpTaskDate.Value, dtpDueDate.Value, TaskType.Exam)
            ''---

            'If intFlag <> 0 Then
            '    Dim frm As MainMenu
            '    frm = Me.MdiParent
            '    frm.ShowTasks()
            'End If
            Me.Close()
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Task Details added.", gloAuditTrail.ActivityOutCome.Success)
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Task Details added.", gloAuditTrail.ActivityOutCome.Success)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Task Details", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub GetData(ByVal Arrlist As ArrayList)
        '       @nTaskId	numeric(18,0),
        '@nFromId	numeric(18,0),
        '@nToId		numeric(18,0),
        '@dtTaskDate	numeric(18,0),
        '@sSubject	varchar(100),
        '@dtDuedate	datetime,
        '@sPriority	varchar(50),
        '@sStatus	varchar(50),
        '@sNotes	
        Arrlist.Add(TaskId)
        If TaskId <> 0 Then  'Opened in update mode
            If ReAssignFlag = True Then
                Arrlist.Add(LoginId)
            Else
                Arrlist.Add(txtFrom.Tag)
            End If

        Else                 'Opened in Add mode 
            Arrlist.Add(LoginId)
        End If
        Arrlist.Add(dtpTaskDate.Value)
        Arrlist.Add(txtSubject.Text)
        Arrlist.Add(dtpDueDate.Value)
        Arrlist.Add(cmbPriority.Text)
        Arrlist.Add(cmbStatus.Text)
        Arrlist.Add(txtNotes.Text)
        Arrlist.Add(txtPatient.Tag)
        Arrlist.Add(DocumentID)
        Arrlist.Add(_ExamId)
    End Sub
    Private Sub btnTo_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTo.MouseHover
        Dim ToolTip1 = New System.Windows.Forms.ToolTip
        ToolTip1.SetToolTip(Me.btnTo, "Select User")
    End Sub

    Private Sub btnClearTo_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearTo.MouseHover
        Dim ToolTip1 = New System.Windows.Forms.ToolTip
        ToolTip1.SetToolTip(Me.btnClearTo, "Clear Users")
    End Sub

    Private Sub dgCustomGrid_MouseUpClick(ByVal sender As Object, ByVal e As Object) Handles dgcustomGrid.MouseUpClick
        If dgcustomGrid.GetCurrentrowIndex >= 0 Then
            'Dim i As Integer
            'dgcustomGrid.flex.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
            'dgcustomGrid.flex.Select(dgcustomGrid.flex.Row, True)
            'dgcustomGrid.flex.RowSel
            dgcustomGrid.GetSelect(dgcustomGrid.GetCurrentrowIndex)
        End If
    End Sub

    Private Sub txtFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFrom.KeyPress
        e.Handled = True
    End Sub

    Private Sub txtSubject_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSubject.KeyPress
        If intFlag <> 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtNotes_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNotes.KeyPress
        'If intFlag <> 0 Then
        '    e.Handled = True
        'End If
    End Sub

    Private Sub btnPatient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatient.Click
        RemoveControl()
        btnStatus = 2
        LoadGrid()
        'Pramod 05122007
        'Patient record is viwed the visible the textsearch box and label
        dgcustomGrid.txtsearch.Visible = True
        dgcustomGrid.Label1.Visible = True
    End Sub

    Private Sub btnClearPatient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearPatient.Click
        If Trim(txtPatient.Text) <> "" And txtPatient.Tag <> 0 Then
            If MessageBox.Show("Do you want to clear selected Patient?", "Tasks", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                txtPatient.Text = ""
                txtPatient.Tag = 0
            End If
        End If
    End Sub


    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnShowOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnShowOrders.Click
        Try
            'Dim frm As New frm_LM_ViewOrders2(_VisitID, _TaskDate, txtPatient.Tag)
            Dim dt As Date = _TaskDate
            Dim frm As New frm_LM_Orders(_VisitID, Now, , , True)
            With frm
                '.TaskID = TaskId
                '.TaskNotes = txtNotes.Text.Trim
                .IsopenfrmTask = True
                .ShowInTaskbar = False
                .WindowState = FormWindowState.Maximized
                .MdiParent = Me.Parent ' CType(Me.Owner, MainMenu)
                MainMenu.ShowHideMainMenu(False, False)
                .ShowDialog()
                .BringToFront()
                Me.Close()

            End With
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "LM orders viewed .", gloAuditTrail.ActivityOutCome.Success)
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "LM orders viewed .", gloAuditTrail.ActivityOutCome.Success)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Task Details", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnShowLabResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnShowLabs.Click
        Try

            'code commented by sarika 2nd nov 07
            'Dim oLM_LabResults As New cls_LM_LabResult
            'Dim dt As New DataTable
            'dt = oLM_LabResults.GetOrdersLabsResult(txtPatient.Tag, _TaskDate)  '' 3376 ,  391070797559562401,
            ' '' nFlowSheetID, sFlowSheetName, lm_test_Name, lm_OrderDate,sPatientCode
            'oLM_LabResults = Nothing

            'If IsNothing(dt) = False Then
            '    If dt.Rows.Count > 0 Then
            '        '''''''''''''''''''''''''
            '        '' To Fill PAtients Information 
            '        ''''' Change the Patient Details
            '        gstrPatientCode = dt.Rows(0)("sPatientCode") 'PatientCode
            '        Try
            '            CType(Me.Owner, MainMenu).txtSearchPatient.Text = ""
            '            CType(Me.Owner, MainMenu).dgPatient.ResetSelectedRows()
            '            CType(Me.Owner, MainMenu).ShowDefaultPatientDetails()
            '            CType(Me.Owner, MainMenu).dgPatientDetails.DataSource = Nothing
            '        Catch ex As Exception
            '        End Try
            '        ''''''''''''''''''''''
            '        Dim frm As New frm_LM_LabResult(dt, _TaskDate, True)
            '        With frm

            '            .ShowInTaskbar = False
            '            .WindowState = FormWindowState.Maximized
            '            .MdiParent = CType(Me.Owner, MainMenu)
            '            .Show()
            '            Me.Close()
            '        End With
            '    End If
            'End If
            '----------------

            'code added by sarika 2nd nov 07
            Dim LabOrderID As Long

            If (_LabNo = "") Then
                LabOrderID = GetLabOrderID(_TaskDate, _VisitID, _PatientID)
            Else
                LabOrderID = _LabNo
            End If




            'If LabOrderID = 0 Then
            '    MessageBox.Show("Lab Oder for Transaction date " & _TaskDate.ToString & " is no more available", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Sub
            'End If
            Dim frmLabOrder As New frmLab_RequestOrder
            With frmLabOrder
                .LabOrderParameter.OrderID = LabOrderID
                .LabOrderParameter.OrderNumberID = 0
                .LabOrderParameter.OrderNumberPrefix = "ORD"
                .LabOrderParameter.PatientID = _PatientID
                .LabOrderParameter.VisitID = _VisitID
                .LabOrderParameter.TransactionDate = _TaskDate
                .WindowState = FormWindowState.Maximized
                .MdiParent = CType(Me.Owner, MainMenu)
                .blnOpenFromTask = True
                MainMenu.ShowHideMainMenu(False, False)
                .Show()
                Me.Close()
            End With
            '-----------------------------------------------
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Lab results viewed.", gloAuditTrail.ActivityOutCome.Success)
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, "Lab results viewed.", gloAuditTrail.ActivityOutCome.Success)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    'sarika 2nd nov 07
    Private Function GetLabOrderID(ByVal TDate As DateTime, ByVal VisitID As Long, ByVal PatientID As Long) As Long
        Dim constr As String
        constr = GetConnectionString()
        Dim conn As New SqlClient.SqlConnection(constr)
        Dim cmd As SqlClient.SqlCommand
        Dim _strSQL As String = ""
        Dim LabOrderID As Long

        Try
            conn.Open()

            _strSQL = "select isnull(labom_OrderID,0) as labom_OrderID from Lab_Order_MST where labom_TransactionDate ='" & TDate & "' and labom_PatientID = " & PatientID & " and labom_VisitID = " & VisitID & ""

            cmd = New SqlClient.SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = _strSQL

            LabOrderID = cmd.ExecuteScalar()

            Return LabOrderID

        Catch ex As Exception
            MessageBox.Show("", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            conn.Close()
        End Try
    End Function
    '-----

    Private Sub frmTasks_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Try
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "Task closed.", gloAuditTrail.ActivityOutCome.Success)
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, "Task closed.", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub txtSubject_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSubject.MouseHover

    End Sub

    Private Sub btnPatient_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPatient.MouseHover
        Dim ToolTip1 = New System.Windows.Forms.ToolTip
        ToolTip1.SetToolTip(Me.btnPatient, "Select Patient")
    End Sub

    Private Sub btnClearPatient_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearPatient.MouseHover
        Dim ToolTip1 = New System.Windows.Forms.ToolTip
        ToolTip1.SetToolTip(Me.btnClearPatient, "Clear Patient")
    End Sub

    Private Sub btnShowDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnShowDocs.Click
        'ObjTasksDBLayer = New ClsTasksDBLayer
        'Dim _DocumentFileName As String = ObjTasksDBLayer.FetchDocumentName(DocumentID) ''c1CategorisedDocuments.GetData(c1CategorisedDocuments.RowSel, COL_CAT_FILENAME)

        'Dim oViewDocument As New frmDMS_ViewDocument

        'oViewDocument.pnlDocument.Visible = False
        'oViewDocument._DMSPatientID = _PatientID
        'oViewDocument._DMSDocumentFileName = _DocumentFileName
        ''pnlLeft.Visible = False
        'oViewDocument.ShowInTaskbar = False
        ''oViewDocument.MdiParent = Me
        'oViewDocument.WindowState = FormWindowState.Maximized
        'oViewDocument.ShowDialog()
        'oViewDocument = Nothing
    End Sub


    Private Sub AddTasks(ByVal Subject As String, ByVal Notes As String, ByVal TaskDate As DateTime, ByVal DueDate As DateTime, ByVal TaskType As gloTaskMail.TaskType, Optional ByVal FaxTiffFileName As String = "")

        Dim arrlist As New ArrayList
        '' fill Value to arraylist 
        '' Like Taskid ,Date, etc Task Related Values
        ''Call GetData(arrlist)

        Dim ArrTasks(cmbTo.Items.Count - 1) As Int64
        Dim mlist As myList
        Dim i As Integer

        For i = 0 To cmbTo.Items.Count - 1
            mlist = cmbTo.Items.Item(i)
            ArrTasks(i) = mlist.Index
        Next
        'End If

        '' ''ObjTasksDBLayer.AddData(arrlist, ArrTasks)
        ''If ArrTasks.Length > 0 Then
        ''    ObjTasksDBLayer.UpdateData(arrlist, ArrTasks, ClsTasksDBLayer.TaskType.OrderRadiology)
        ''End If

        Dim ogloTask As New gloTaskMail.gloTask(GetConnectionString)
        Dim oTask As New Task()
        Dim oTaskProgress As New gloTaskMail.TaskProgress
        For i = 0 To ArrTasks.Length - 1

            Dim oTaskAssign As New TaskAssign

            oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Hold
            oTaskAssign.AssignFromID = gnLoginID
            oTaskAssign.AssignFromName = gstrLoginName
            oTaskAssign.AssignToID = ArrTasks(i)
            oTaskAssign.AssignToName = ""
            oTask.Assignment.Add(oTaskAssign)

        Next

        oTaskProgress.ClinicID = gnClinicID
        oTaskProgress.Complete = 0
        oTaskProgress.DateTime = TaskDate
        oTaskProgress.Description = Notes
        oTaskProgress.StatusID = 1 '' Not Started
        oTaskProgress.TaskID = 0

        '' 
        oTask.TaskType = TaskType
        oTask.PatientID = gnPatientID
        oTask.Subject = Subject
        oTask.ClinicID = gnClinicID
        oTask.DateCreated = gloDateMaster.gloDate.DateAsNumber(TaskDate)
        oTask.StartDate = gloDateMaster.gloDate.DateAsNumber(TaskDate)
        oTask.DueDate = gloDateMaster.gloDate.DateAsNumber(DueDate)
        oTask.FaxTiffFileName = FaxTiffFileName
        oTask.IsPrivate = False
        oTask.MachineName = gstrClientMachineName
        oTask.Progress = oTaskProgress

        ogloTask.Add(oTask)
    End Sub

End Class
