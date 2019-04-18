Imports C1.Win.C1FlexGrid
Imports gloEMRGeneralLibrary.gloEMRDatabase

Public Class frmFinishExam
    Inherits System.Windows.Forms.Form


#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientID As Long)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _patientID = PatientID
    End Sub

    'Form overrides dispose to clean up the component list.
    'Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    '    If disposing Then
    '        If Not (components Is Nothing) Then
    '            components.Dispose()
    '        End If
    '    End If
    '    MyBase.Dispose(disposing)
    'End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    ' System.Windows.Forms.DataGrid
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlLeftMain As System.Windows.Forms.Panel
    Friend WithEvents pnlLeftTopTop As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlLeftTop As System.Windows.Forms.Panel
    Friend WithEvents trvCriteria As System.Windows.Forms.TreeView
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents imgTreeView As System.Windows.Forms.ImageList
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_UnfinishedExams As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnOpenExam As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnlUnfinishedExamcontrol As System.Windows.Forms.Panel
    Friend WithEvents cmnu_Diagnosis As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents C1Exams As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents btnShowExams As System.Windows.Forms.ToolStripButton
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents cmbTemplateSpecility As System.Windows.Forms.ComboBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlTopMain As System.Windows.Forms.Panel
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents ts_btnPrint As System.Windows.Forms.ToolStripButton
    Private WithEvents Label8 As System.Windows.Forms.Label

    Private Property ProvideName As String

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFinishExam))
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.pnlLeftTop = New System.Windows.Forms.Panel()
        Me.pnlUnfinishedExamcontrol = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.trvCriteria = New System.Windows.Forms.TreeView()
        Me.imgTreeView = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlLeftMain = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.C1Exams = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pnlTopMain = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmbProvider = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cmbTemplateSpecility = New System.Windows.Forms.ComboBox()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.pnlLeftTopTop = New System.Windows.Forms.Panel()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_UnfinishedExams = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnOpenExam = New System.Windows.Forms.ToolStripButton()
        Me.btnShowExams = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.cmnu_Diagnosis = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlLeft.SuspendLayout()
        Me.pnlLeftTop.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlLeftMain.SuspendLayout()
        CType(Me.C1Exams, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTopMain.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlLeftTopTop.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_UnfinishedExams.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlLeft
        '
        Me.pnlLeft.Controls.Add(Me.pnlLeftTop)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 54)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(192, 438)
        Me.pnlLeft.TabIndex = 2
        '
        'pnlLeftTop
        '
        Me.pnlLeftTop.Controls.Add(Me.pnlUnfinishedExamcontrol)
        Me.pnlLeftTop.Controls.Add(Me.Label8)
        Me.pnlLeftTop.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlLeftTop.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlLeftTop.Controls.Add(Me.lbl_pnlRight)
        Me.pnlLeftTop.Controls.Add(Me.lbl_pnlTop)
        Me.pnlLeftTop.Controls.Add(Me.trvCriteria)
        Me.pnlLeftTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeftTop.Name = "pnlLeftTop"
        Me.pnlLeftTop.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.pnlLeftTop.Size = New System.Drawing.Size(192, 438)
        Me.pnlLeftTop.TabIndex = 0
        '
        'pnlUnfinishedExamcontrol
        '
        Me.pnlUnfinishedExamcontrol.BackColor = System.Drawing.Color.White
        Me.pnlUnfinishedExamcontrol.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlUnfinishedExamcontrol.Location = New System.Drawing.Point(4, 9)
        Me.pnlUnfinishedExamcontrol.Name = "pnlUnfinishedExamcontrol"
        Me.pnlUnfinishedExamcontrol.Size = New System.Drawing.Size(187, 425)
        Me.pnlUnfinishedExamcontrol.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.White
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Location = New System.Drawing.Point(4, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(187, 5)
        Me.Label8.TabIndex = 10
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 434)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(187, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 431)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(191, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 431)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(189, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'trvCriteria
        '
        Me.trvCriteria.BackColor = System.Drawing.Color.White
        Me.trvCriteria.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCriteria.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvCriteria.ForeColor = System.Drawing.Color.Black
        Me.trvCriteria.HideSelection = False
        Me.trvCriteria.ImageIndex = 0
        Me.trvCriteria.ImageList = Me.imgTreeView
        Me.trvCriteria.Indent = 20
        Me.trvCriteria.ItemHeight = 20
        Me.trvCriteria.Location = New System.Drawing.Point(3, 3)
        Me.trvCriteria.Name = "trvCriteria"
        Me.trvCriteria.SelectedImageIndex = 0
        Me.trvCriteria.Size = New System.Drawing.Size(189, 432)
        Me.trvCriteria.TabIndex = 0
        '
        'imgTreeView
        '
        Me.imgTreeView.ImageStream = CType(resources.GetObject("imgTreeView.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTreeView.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeView.Images.SetKeyName(0, "Small Arrow.ico")
        Me.imgTreeView.Images.SetKeyName(1, "Bullet06.ico")
        Me.imgTreeView.Images.SetKeyName(2, "Olders.ico")
        Me.imgTreeView.Images.SetKeyName(3, "Yesterdays.ico")
        Me.imgTreeView.Images.SetKeyName(4, "Last Week.ico")
        Me.imgTreeView.Images.SetKeyName(5, "LastMonth.ico")
        Me.imgTreeView.Images.SetKeyName(6, "Current.ico")
        Me.imgTreeView.Images.SetKeyName(7, "Unfinished Exam.ico")
        Me.imgTreeView.Images.SetKeyName(8, "DX01.ico")
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlLeftMain)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.pnlTopMain)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(195, 54)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlMain.Size = New System.Drawing.Size(1089, 438)
        Me.pnlMain.TabIndex = 0
        '
        'pnlLeftMain
        '
        Me.pnlLeftMain.Controls.Add(Me.Label9)
        Me.pnlLeftMain.Controls.Add(Me.C1Exams)
        Me.pnlLeftMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftMain.Location = New System.Drawing.Point(1, 29)
        Me.pnlLeftMain.Name = "pnlLeftMain"
        Me.pnlLeftMain.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlLeftMain.Size = New System.Drawing.Size(1084, 405)
        Me.pnlLeftMain.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Location = New System.Drawing.Point(0, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1084, 1)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "label1"
        '
        'C1Exams
        '
        Me.C1Exams.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1Exams.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.C1Exams.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Exams.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Exams.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1Exams.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Exams.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Exams.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Exams.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Exams.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Exams.Location = New System.Drawing.Point(0, 3)
        Me.C1Exams.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1Exams.Name = "C1Exams"
        Me.C1Exams.Rows.Count = 1
        Me.C1Exams.Rows.DefaultSize = 19
        Me.C1Exams.Rows.Fixed = 0
        Me.C1Exams.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Exams.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Exams.ShowCellLabels = True
        Me.C1Exams.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1Exams.Size = New System.Drawing.Size(1084, 402)
        Me.C1Exams.StyleInfo = resources.GetString("C1Exams.StyleInfo")
        Me.C1Exams.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(1, 434)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1084, 1)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "label2"
        '
        'pnlTopMain
        '
        Me.pnlTopMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopMain.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopMain.Controls.Add(Me.Panel1)
        Me.pnlTopMain.Controls.Add(Me.Panel2)
        Me.pnlTopMain.Controls.Add(Me.pnlLeftTopTop)
        Me.pnlTopMain.Controls.Add(Me.Label7)
        Me.pnlTopMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTopMain.Location = New System.Drawing.Point(1, 4)
        Me.pnlTopMain.Name = "pnlTopMain"
        Me.pnlTopMain.Size = New System.Drawing.Size(1084, 25)
        Me.pnlTopMain.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.cmbProvider)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(692, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(366, 24)
        Me.Panel1.TabIndex = 5
        '
        'cmbProvider
        '
        Me.cmbProvider.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProvider.FormattingEnabled = True
        Me.cmbProvider.Location = New System.Drawing.Point(108, 0)
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.Size = New System.Drawing.Size(232, 22)
        Me.cmbProvider.TabIndex = 0
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(340, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(26, 24)
        Me.Label11.TabIndex = 165
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(108, 24)
        Me.Label10.TabIndex = 164
        Me.Label10.Text = "Provider : "
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.cmbTemplateSpecility)
        Me.Panel2.Controls.Add(Me.Label67)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(309, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(383, 24)
        Me.Panel2.TabIndex = 4
        '
        'cmbTemplateSpecility
        '
        Me.cmbTemplateSpecility.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbTemplateSpecility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplateSpecility.ForeColor = System.Drawing.Color.Black
        Me.cmbTemplateSpecility.Location = New System.Drawing.Point(93, 0)
        Me.cmbTemplateSpecility.Name = "cmbTemplateSpecility"
        Me.cmbTemplateSpecility.Size = New System.Drawing.Size(275, 22)
        Me.cmbTemplateSpecility.TabIndex = 0
        '
        'Label67
        '
        Me.Label67.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label67.AutoSize = True
        Me.Label67.BackColor = System.Drawing.Color.Transparent
        Me.Label67.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label67.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.Location = New System.Drawing.Point(17, 5)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(70, 14)
        Me.Label67.TabIndex = 163
        Me.Label67.Text = "Specialty :"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlLeftTopTop
        '
        Me.pnlLeftTopTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeftTopTop.Controls.Add(Me.dtTo)
        Me.pnlLeftTopTop.Controls.Add(Me.Label1)
        Me.pnlLeftTopTop.Controls.Add(Me.dtFrom)
        Me.pnlLeftTopTop.Controls.Add(Me.Label2)
        Me.pnlLeftTopTop.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeftTopTop.Enabled = False
        Me.pnlLeftTopTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeftTopTop.Name = "pnlLeftTopTop"
        Me.pnlLeftTopTop.Size = New System.Drawing.Size(309, 24)
        Me.pnlLeftTopTop.TabIndex = 2
        '
        'dtTo
        '
        Me.dtTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtTo.CustomFormat = "MM/dd/yyyy"
        Me.dtTo.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtTo.Location = New System.Drawing.Point(200, 0)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(98, 22)
        Me.dtTo.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(157, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 24)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "    To "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtFrom
        '
        Me.dtFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtFrom.CustomFormat = "MM/dd/yyyy"
        Me.dtFrom.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFrom.Location = New System.Drawing.Point(59, 0)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(98, 22)
        Me.dtFrom.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 24)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "From "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Location = New System.Drawing.Point(0, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1084, 1)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "label1"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Location = New System.Drawing.Point(1, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1084, 1)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "label1"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Location = New System.Drawing.Point(1085, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 432)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Location = New System.Drawing.Point(0, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 432)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "label4"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(192, 54)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 438)
        Me.Splitter1.TabIndex = 2
        Me.Splitter1.TabStop = False
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_UnfinishedExams)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1284, 54)
        Me.pnlToolStrip.TabIndex = 3
        '
        'ts_UnfinishedExams
        '
        Me.ts_UnfinishedExams.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ts_UnfinishedExams.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_UnfinishedExams.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_UnfinishedExams.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_UnfinishedExams.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_UnfinishedExams.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_UnfinishedExams.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOpenExam, Me.btnShowExams, Me.ts_btnPrint, Me.ts_btnClose})
        Me.ts_UnfinishedExams.Location = New System.Drawing.Point(0, 0)
        Me.ts_UnfinishedExams.Name = "ts_UnfinishedExams"
        Me.ts_UnfinishedExams.Size = New System.Drawing.Size(1284, 53)
        Me.ts_UnfinishedExams.TabIndex = 0
        Me.ts_UnfinishedExams.Text = "ToolStrip1"
        '
        'ts_btnOpenExam
        '
        Me.ts_btnOpenExam.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOpenExam.Image = CType(resources.GetObject("ts_btnOpenExam.Image"), System.Drawing.Image)
        Me.ts_btnOpenExam.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOpenExam.Name = "ts_btnOpenExam"
        Me.ts_btnOpenExam.Size = New System.Drawing.Size(85, 50)
        Me.ts_btnOpenExam.Tag = "OpenExam"
        Me.ts_btnOpenExam.Text = "&Open Exams"
        Me.ts_btnOpenExam.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnOpenExam.ToolTipText = "Open Exams"
        '
        'btnShowExams
        '
        Me.btnShowExams.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowExams.Image = CType(resources.GetObject("btnShowExams.Image"), System.Drawing.Image)
        Me.btnShowExams.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnShowExams.Name = "btnShowExams"
        Me.btnShowExams.Size = New System.Drawing.Size(88, 50)
        Me.btnShowExams.Tag = "ShowExam"
        Me.btnShowExams.Text = "&Show Exams"
        Me.btnShowExams.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnShowExams.Visible = False
        '
        'ts_btnPrint
        '
        Me.ts_btnPrint.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnPrint.Image = CType(resources.GetObject("ts_btnPrint.Image"), System.Drawing.Image)
        Me.ts_btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnPrint.Name = "ts_btnPrint"
        Me.ts_btnPrint.Size = New System.Drawing.Size(41, 50)
        Me.ts_btnPrint.Tag = "Print"
        Me.ts_btnPrint.Text = "&Print"
        Me.ts_btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'cmnu_Diagnosis
        '
        Me.cmnu_Diagnosis.Name = "cmnu_Diagnosis"
        Me.cmnu_Diagnosis.Size = New System.Drawing.Size(61, 4)
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmFinishExam
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1284, 492)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmFinishExam"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Unfinished Exams"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlLeftTop.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlLeftMain.ResumeLayout(False)
        CType(Me.C1Exams, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTopMain.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlLeftTopTop.ResumeLayout(False)
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_UnfinishedExams.ResumeLayout(False)
        Me.ts_UnfinishedExams.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    '' SUDHIR 20090701 ''
#Region " TO Check the Multiple instances Of Form "

    '' TO Keep track that the Form's Instance is Disposed or not
    Private blnDisposed As Boolean
    Private Shared frm As frmFinishExam

    ''Form overrides dispose to clean up the component list.
    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)

        If Not (Me.blnDisposed) Then

            If (disposing) Then
                Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtTo, dtFrom}
                Dim cntControls() As System.Windows.Forms.Control = {dtTo, dtFrom}
                Dim CmppControls() As System.Windows.Forms.ContextMenuStrip = {cmnu_Diagnosis}

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




                If (IsNothing(CmppControls) = False) Then
                    If CmppControls.Length > 0 Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(CmppControls)
                    End If
                End If



                If (IsNothing(CmppControls) = False) Then
                    If CmppControls.Length > 0 Then
                        gloGlobal.cEventHelper.DisposeContextMenuStrip(CmppControls)
                    End If
                End If



            End If

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

    Public Shared Function GetInstance(ByVal PatientID As Long) As frmFinishExam
        '_mu.WaitOne()
        Try
            If frm Is Nothing Then
                frm = New frmFinishExam(PatientID)
            End If
        Finally
            '_mu.ReleaseMutex()
        End Try
        Return frm
    End Function

#End Region
    '' END SUDHIR ''
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String
    Dim _CustomFromDate As Date
    Dim _CustomToDate As Date

    'sarika Date Mgt 20081111
    Dim oUnFinishedExams As gloUserControlLibrary.DateManagement
    Dim dtUnfinishedExams As DataTable

    Dim selectedCategory As String = ""
    Public blnExamOpened As Boolean = False
    Public SelectedNode As gloUserControlLibrary.myTreeNode
    'Shubhangi 20100119
    Dim _GridWidth As Integer
    Dim _patientID As Long
    Dim dt_ProviderCombo As DataTable = Nothing
    Dim ProviderName As String
    Dim ProviderID As Int64


    Private Sub frmFinishExam_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        ''it is writen for always to be in the maximize state
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub frmFinishExam_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        frmPatientExam.blnOpenFromUnfinishedExam = False
        Try
            'Application.DoEvents()
            Me.Dispose()
        Catch exdispose As Exception

        End Try
    End Sub
    '---
    Private Sub frmFinishExam_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        gloC1FlexStyle.Style(C1Exams) ''Ojeswini''

        Try
            Me.Cursor = Cursors.WaitCursor
            dtFrom.Visible = True
            dtTo.Visible = True

            Call Get_PatientDetails()

            pnlLeftTopTop.Visible = True

            Fill_TemplateSpecility()

            FillProviderCombo()
            Call Fill_UnFinishedExams()

            btnShowExams.Visible = False
            ''
            _CustomFromDate = Now.Date
            _CustomToDate = Now.Date


            Me.Cursor = Cursors.Default

            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.View, "Unfinished Exams Report Opened", gloAuditTrail.ActivityOutCome.Success)
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnShowExams_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowExams.Click
        Try
            _CustomFromDate = dtFrom.Value
            _CustomToDate = dtTo.Value
            selectedCategory = oUnFinishedExams.mySelectedNode.Text
            ToFromDateValidation() '' dhruv - date validation
            If Not IsNothing(oUnFinishedExams) Then
                Call Fill_UnFinishedExamsNew()
            Else
                Call Fill_UnFinishedExams()

            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.View, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Close()
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub OpenExam()

        If C1Exams.Rows.Count > 0 Then
            Dim _RowSel As Integer = C1Exams.RowSel

            If _RowSel > 0 Then
                strPatientCode = C1Exams.GetData(_RowSel, "PatientCode").ToString
                _patientID = C1Exams.GetData(_RowSel, "PatientID").ToString
                Try
                    ''''''''''''' Added by Ujwala Atre - to implement 'lock status' functionality as on 11152010
                    If MainMenu.IsAccess(False, _patientID, , True) = False Then
                        Exit Sub
                    End If

                    '******Shweta 20090828 *********'
                    'To check exeception related to word
                    If CheckWordForException() = False Then
                        Exit Sub
                    End If
                    If (gblnPastExam = False) Then
                        MessageBox.Show("User has no right to view past exam.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If


                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                End Try

                ''Dhruv -> Validating the Date for the to -> From
                Dim _Answer As Boolean = ToFromDateValidation()
                If _Answer = True Then
                    Return
                End If
                ''End Dhruv

                'Dim objSender As Object
                'Dim objE As EventArgs
                Dim nPastExamID As Long
                Dim nVisitID As Long
                Dim dtDOS As DateTime
                Dim strExamName As String
                Dim blnFinished As Boolean
                Me.Cursor = Cursors.WaitCursor
                'Added By Shweta 20091127
                Dim strTemplateName As String
                'End 20091127

                ''Dim em As System.Windows.Forms.MouseEventArgs
                nPastExamID = Convert.ToInt64(C1Exams.GetData(_RowSel, "ExamID"))
                nVisitID = Convert.ToInt64(C1Exams.GetData(_RowSel, "VisitID"))
                dtDOS = Convert.ToDateTime(C1Exams.GetData(_RowSel, "DOS"))
                strExamName = C1Exams.GetData(_RowSel, "ExamName")
                blnFinished = False
                'Added By Shweta 20091127
                strTemplateName = C1Exams.GetData(_RowSel, "TemplateName").ToString()
                'End 20091127
                '''''''''<<<<<<><><><><><>>>>>>
                ''''' 2060922 - Mahesh
                'Dim mydt As New mytable
                'Dim clsobjPatExam As New clsPatientExams
                'mydt = clsobjPatExam.Scan_n_Lock_Exam(nPastExamID)
                'clsobjPatExam = Nothing
                'If IsNothing(mydt) = False Then
                '    If mydt.Unit = 1 Then
                '        MessageBox.Show("This Exam is already opened by " & mydt.Code & " on " & mydt.Description & ". You cannot open this Exam, now.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End If
                '''''''''<<<<<<><><><><><>>>>>>

                'Me.Hide()

                '' To check the Provider Associated for the Exam is same as provider Selected
                If Not blnFinished Then
                    Dim objExam As New clsPatientExams
                    objExam.SetProviderExam(nPastExamID)
                    objExam.Dispose()
                    objExam = Nothing
                End If

                Dim frm As New frmPatientExam(Convert.ToInt64(C1Exams.GetData(_RowSel, "PatientID")), nVisitID)
                With frm
                    .Hide()
                    .blnModify = True
                    .Text = "Past Exams"
                    ''    .lblPatientName.Text = dgData.Item(dgData.CurrentRowIndex, 1)
                    .PatientID = Convert.ToInt64(C1Exams.GetData(_RowSel, "PatientID"))
                    ''  .lblPatinetCode.Text = dgData.Item(dgData.CurrentRowIndex, 6)

                    'If Trim(dgData.Item(dgData.CurrentRowIndex, 7)) <> "" Then
                    '    .lblPatientDOB.Text = Format(dgData.Item(dgData.CurrentRowIndex, 7), "MM/dd/yyyy")
                    '    Dim nMonths As Int16
                    '    nMonths = DateDiff(DateInterval.Month, CType(.lblPatientDOB.Text, Date), Date.Now.Date)
                    '    .lblPatientAge.Text = nMonths \ 12 & " Yrs " & nMonths Mod 12 & " Months"  ' DateDiff(DateInterval.Month, CType(gstrPatientDOB, Date), Date.Now.Date) & " Yrs"
                    '    'ctype(gstrPatientDOB,Date)
                    'Else
                    '    .lblPatientDOB.Text = ""
                    '    .lblPatientAge.Text = ""
                    'End If
                    .pnlPastExam.Visible = False
                    .pnlPastExamView.Visible = True
                    ' .chkShowPreview.Visible = True
                    'sarika 20081120 -- exam is opened only if user wants to view it
                    'Changed by Shweta 20091127
                    ' 'If (.OpenPastExam(nPastExamID, nVisitID, dtDOS, strExamName, blnFinished)) = True Then
                    If (.OpenPastExam(nPastExamID, nVisitID, dtDOS, strExamName, blnFinished, strTemplateName)) = True Then
                        'End 20091127
                        '---
                        .Hide()
                        '.OpenPastExam(nPastExamID, nVisitID, dtDOS, strExamName, blnFinished) 
                        ''COMMENT BY SUDHIR 20090131 '' AS IT IS EXECUTED IN IF STATEMENT ABOVE. NO NEED TO CALL AGAIN.

                        .IsPastExam = True
                        .MdiParent = Me.ParentForm
                        CType(Me.ParentForm, MainMenu).ShowHideMainMenu(False, False)
                        CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False
                        frmPatientExam.blnOpenFromUnfinishedExam = True
                        .Show()
                        If .ExamViewMode Then
                            .ViewExam(nPastExamID)
                        Else
                            .OpenPastExamContents(nPastExamID, blnFinished)
                        End If
                        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Open, "Past Exam Opened", .PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
                        ''Added Rahul P on 20101011
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Open, "Past Exam Opened", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        ''
                        '.dgExams.CurrentRowIndex = dgData.CurrentRowIndex
                        '.dgExams_DoubleClick(sender, e)
                    Else

                        frm.Dispose()
                        frm = Nothing
                    End If
                End With
                Me.Cursor = Cursors.Default
            End If

        End If

    End Sub

    Private Sub Fill_Criterias()
        With trvCriteria
            .Nodes.Clear()
            Dim RootNode As New TreeNode
            Dim nde As TreeNode

            RootNode.Text = "Unfinished Exams"
            RootNode.ImageIndex = 7
            RootNode.SelectedImageIndex = 7
            .Nodes.Add(RootNode)

            nde = New TreeNode
            nde.Text = "Today"
            nde.ImageIndex = 2 '1
            nde.SelectedImageIndex = 2 '1
            RootNode.Nodes.Add(nde)

            nde = New TreeNode
            nde.Text = "Yesterday"
            nde.ImageIndex = 3 '1
            nde.SelectedImageIndex = 3 '1
            RootNode.Nodes.Add(nde)

            nde = New TreeNode
            nde.Text = "Last Week"
            nde.ImageIndex = 4 '1
            nde.SelectedImageIndex = 4 '1
            RootNode.Nodes.Add(nde)

            nde = New TreeNode
            nde.Text = "Last Month"
            nde.ImageIndex = 5 '1
            nde.SelectedImageIndex = 5 '1
            RootNode.Nodes.Add(nde)
            nde = New TreeNode

            nde.Text = "Customize"
            nde.ImageIndex = 6 '1
            nde.SelectedImageIndex = 6 '1
            RootNode.Nodes.Add(nde)

            '.Nodes(0).Nodes.Add("Today")
            '.Nodes(0).Nodes.Add("Yesterday")
            '.Nodes(0).Nodes.Add("Last Week")
            '.Nodes(0).Nodes.Add("Last Month")
            '.Nodes(0).Nodes.Add("Customize")
            .SelectedNode = .Nodes(0).Nodes(0)
            .ExpandAll()
        End With
    End Sub

    Private Sub trvCriteria_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvCriteria.AfterSelect
        ''''Added by Anil on 20071211
        If Trim(trvCriteria.SelectedNode.Text) = "Customize" Then
            dtFrom.Value = _CustomFromDate
            dtTo.Value = _CustomToDate
        End If
        ''''''''
        pnlLeftTopTop.Enabled = False
        Fill_Exams()
    End Sub

    Private Sub trvCriteria_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvCriteria.MouseDown
        Try
            Dim trvNode As TreeNode
            trvNode = trvCriteria.GetNodeAt(e.X, e.Y)
            If IsNothing(trvNode) = False Then
                pnlLeftTopTop.Enabled = False
                trvCriteria.SelectedNode = trvNode
                Call Fill_Exams()
            End If
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to show exams due to " & objErr.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub dgData_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'Try
        '    Dim ptPoint As Point = New Point(e.X, e.Y)
        '    Dim htInfo As DataGrid.HitTestInfo = dgData.HitTest(ptPoint)
        '    If htInfo.Type = DataGrid.HitTestType.Cell Then
        '        OpenExam()
        '    Else
        '        Exit Sub
        '    End If

        'Catch objErr As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    MessageBox.Show("Unable to open exams due to " & objErr.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        'End Try
    End Sub

    ''Sandip Darade 20090613
    ''View  diagnosis for the exam
    Private Sub dgData_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'Try

        '    Dim ptPoint As Point = New Point(e.X, e.Y)
        '    Dim htInfo As DataGrid.HitTestInfo = dgData.HitTest(ptPoint)
        '    cmnu_Diagnosis.Items.Clear()
        '    dgData.ContextMenuStrip = cmnu_Diagnosis
        '    dgData.Select(htInfo.Row) '' select the clicked row 

        '    If htInfo.Type = DataGrid.HitTestType.Cell Then

        '        If (e.Button = Windows.Forms.MouseButtons.Right) Then

        '            If dgData.CurrentRowIndex >= 0 Then

        '                If Not IsNothing(CType(dgData.Item(dgData.CurrentRowIndex, 0), Int64)) Then  ''Exam ID

        '                    Dim oMenuItem_ViewDiagnosis As New ToolStripMenuItem
        '                    oMenuItem_ViewDiagnosis.Text = "View Diagnosis"
        '                    oMenuItem_ViewDiagnosis.ForeColor = Color.FromArgb(31, 73, 125)
        '                    oMenuItem_ViewDiagnosis.Image = imgTreeView.Images(8)
        '                    cmnu_Diagnosis.Items.Add(oMenuItem_ViewDiagnosis)
        '                    AddHandler oMenuItem_ViewDiagnosis.Click, AddressOf oMenuItem_ViewDiagnosisclick

        '                End If
        '            End If
        '        End If
        '    Else
        '        Exit Sub
        '    End If
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        'End Try

    End Sub

    ''Sandip Darade 20090613
    ''View  diagnosis for the exam
    Private Sub oMenuItem_ViewDiagnosisclick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            '' SUDHIR 20090605 ''
            If C1Exams.RowSel > 0 Then
                Dim _PatientID As Int64 = CType(C1Exams.GetData(C1Exams.RowSel, "PatientID"), Int64)
                Dim _ExamID As Int64 = CType(C1Exams.GetData(C1Exams.RowSel, "ExamID"), Int64)
                Dim _VisitID As Int64 = CType(C1Exams.GetData(C1Exams.RowSel, "VisitID"), Int64)
                Dim _DOS As DateTime = CType(C1Exams.GetData(C1Exams.RowSel, "DOS"), DateTime)
                Dim _ExamName As String = CType(C1Exams.GetData(C1Exams.RowSel, "ExamName"), String)

                ShowDiagnosis(_PatientID, _ExamID, _VisitID, _DOS, _ExamName)
            End If

            '' END SUDHIR ''

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgData_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'Try
        '    Dim ptPoint As Point = New Point(e.X, e.Y)
        '    Dim htInfo As DataGrid.HitTestInfo = dgData.HitTest(ptPoint)
        '    If htInfo.Type = DataGrid.HitTestType.Cell Then
        '        dgData.Select(htInfo.Row)
        '    Else
        '        Exit Sub
        '    End If
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    MessageBox.Show(ex.Message, "Patient Consent", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try

    End Sub


    Private Sub frmFinishExam_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Try
            If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                Dim frm As MainMenu
                frm = Me.MdiParent
                If frm.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOn Or frm.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicSleeping Then
                    frm.DgnMicBtn1.MicState = DNSTools.DgnMicStateConstants.dgnmicOff
                End If
                frm = Nothing
            End If

            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Other, "Unfinished Exams Report Closed", gstrLoginName, gstrClientMachineName, gnPatientID)
            'objAudit = Nothing
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.Close, "Unfinished Exams Report Closed", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    Private Sub ts_UnfinishedExams_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_UnfinishedExams.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "OpenExam"
                Call OpenExam()
            Case "Close"
                Me.Close()
        End Select
    End Sub


#Region "Date Management Methods"

    'sarika Date Mgt 20081006
    Public Function Fill_UnFinishedExams(Optional ByVal isCalledFromExam As Boolean = False)


        ' pnlUnfinishedExamcontrol.Controls.Remove(oUnFinishedExams)
        If Not IsNothing(oUnFinishedExams) Then
            pnlUnfinishedExamcontrol.Controls.Remove(oUnFinishedExams)
            Try
                RemoveHandler oUnFinishedExams.trvCategoryDoublclick, AddressOf oUnFinishedExams_Doubleclick
                ''AddHandler oUnFinishedExams.trvcatagoryAfterselect, AddressOf oUnfishedExams_AfterSelect
                RemoveHandler oUnFinishedExams.trvSCatagaoryMouseNodeClick, AddressOf oUnfishedExams_MouseNodeClick
            Catch ex As Exception

            End Try
            oUnFinishedExams.Dispose()
            oUnFinishedExams = Nothing
        End If
        ' Dim i As Integer
        'Fill_TreeView_Structure(trvUnfinishedExams)

        Dim obj As New clsPatientExams


        If (isCalledFromExam) Then
            cmbProvider.SelectedValue = gnLoginProviderID
            ProviderID = gnLoginProviderID
            dtUnfinishedExams = obj.GetLoginProvUnfinishedExams(ProviderID, Nothing, Nothing)
        End If
        If IsNothing(dtUnfinishedExams) = False Then
            If (dtUnfinishedExams.Rows.Count = 0) Then
                dtUnfinishedExams = obj.GetLoginProvUnfinishedExams(ProviderID, Nothing, Nothing)
            End If
        Else

            dtUnfinishedExams = obj.GetLoginProvUnfinishedExams(ProviderID, Nothing, Nothing)
        End If

        '---
        If Not IsNothing(dtUnfinishedExams) Then


            Dim dv As New DataView(dtUnfinishedExams)

            dtUnfinishedExams = dv.ToTable()

            oUnFinishedExams = New gloUserControlLibrary.DateManagement(dtUnfinishedExams, gloUserControlLibrary.DateManagement.Categorization.UnFinishedExams1, selectedCategory)
            oUnFinishedExams.Dock = DockStyle.Fill
            oUnFinishedExams.BringToFront()
            AddHandler oUnFinishedExams.trvCategoryDoublclick, AddressOf oUnFinishedExams_Doubleclick
            ''AddHandler oUnFinishedExams.trvcatagoryAfterselect, AddressOf oUnfishedExams_AfterSelect
            AddHandler oUnFinishedExams.trvSCatagaoryMouseNodeClick, AddressOf oUnfishedExams_MouseNodeClick
            '   AddHandler oUnFinishedExams.trvCategoryMouseDown, AddressOf oUnfishedExams_MouseDown

            pnlUnfinishedExamcontrol.Controls.Add(oUnFinishedExams)


            ''If oUnFinishedExams.trvCateogory.Nodes.Contains(CType(SelectedNode, TreeNode)) = True Then
            If IsNothing(SelectedNode) = False Then
                oUnFinishedExams.mySelectedNode = SelectedNode
            Else
                If (oUnFinishedExams.trvCateogory.Nodes.Count > 0) Then
                    oUnFinishedExams.trvCateogory.SelectedNode = oUnFinishedExams.trvCateogory.Nodes(0)
                    oUnFinishedExams.mySelectedNode = oUnFinishedExams.trvCateogory.Nodes(0)
                End If
            End If

            ''End If

            Call Fill_Exams(oUnFinishedExams.mySelectedNode.Text)
            'dtUnfinishedExams.Dispose()
            'dtUnfinishedExams = Nothing
        Else
            'C1Exams.Clear()
            C1Exams.DataSource = Nothing
            C1Exams.Rows.Count = 1
        End If

        obj.Dispose()
        Return Nothing
    End Function

    Private Sub oUnFinishedExams_Doubleclick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            '' Check If the Category Node is Not Selected
            If IsNothing(oUnFinishedExams.mySelectedNode) Then
                Exit Sub
            End If

            If IsNothing(oUnFinishedExams.mySelectedNode.Parent) = False Then

                '' Check If it is Root Node
                If IsNothing(oUnFinishedExams.mySelectedNode.Parent) = False Then
                    Dim Node As New gloUserControlLibrary.myTreeNode
                    Node = oUnFinishedExams.mySelectedNode
                    Dim str() As String
                    str = Split(Node.Tag.ToString.Trim, "-", 2)

                    Call ShowPastExam(Node.Key, Node.PatientId, Node.TemplateResult, str(0), If(str.Length > 1, str(1), ""), False, Node.NodeName)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oUnfishedExams_MouseNodeClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs)
        Try
            ' Dim i As Integer
            'For i = 0 To oUnFinishedExams.trvCateogory.Nodes.Count - 1
            '    oUnFinishedExams.trvCateogory.Nodes(i).BackColor = Color.White
            '    oUnFinishedExams.trvCateogory.Nodes(i).ForeColor = Color.Black
            'Next

            Dim trvNode As gloUserControlLibrary.myTreeNode
            trvNode = CType(oUnFinishedExams.mySelectedNode, gloUserControlLibrary.myTreeNode)
            If IsNothing(trvNode) = False Then
                oUnFinishedExams.mySelectedNode = trvNode
            End If

            If IsNothing(oUnFinishedExams.mySelectedNode) = True Then
                Exit Sub
            End If

            SelectedNode = oUnFinishedExams.mySelectedNode
            selectedCategory = oUnFinishedExams.mySelectedNode.Text

            '' Check If the Category Node is Not Selected
            If trvNode.Text = "Customize" Then
                pnlLeftTopTop.Enabled = True
                btnShowExams.Visible = True
                If Not IsNothing(C1Exams.DataSource) Then
                    'C1Exams.Clear()
                    C1Exams.DataSource = Nothing
                    C1Exams.Rows.Count = 1
                End If
                dtFrom.Value = _CustomFromDate
                dtTo.Value = _CustomToDate

                Fill_UnFinishedExamsNew()
            Else

                If IsNothing(oUnFinishedExams.mySelectedNode.Parent) = False Then
                    strPatientCode = CType(oUnFinishedExams.mySelectedNode, gloUserControlLibrary.myTreeNode).NodeName
                    _patientID = CType(oUnFinishedExams.mySelectedNode, gloUserControlLibrary.myTreeNode).PatientId
                Else
                    Fill_UnFinishedExamsNew()
                    pnlLeftTopTop.Enabled = False
                    btnShowExams.Visible = False
                    dtFrom.Value = _CustomFromDate
                    dtTo.Value = _CustomToDate
                End If
            End If

            oUnFinishedExams.trvCateogory.SelectedNode = oUnFinishedExams.mySelectedNode
            'cmbProvider.SelectedValue = gnPatientProviderID
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub ShowPastExam(ByVal ExamID As Long, ByVal PatientId As Int64, ByVal VisitID As Long, ByVal DOS As String, ByVal ExamName As String, ByVal blnFinished As Boolean, Optional ByVal PatientCode As String = "")
        Try

            _patientID = PatientId

            If PatientCode <> "" Then
                strPatientCode = PatientCode
            Else
                strPatientCode = GetPatientCodefromID(PatientId)
            End If


            If Trim(strPatientFirstName) <> "" Then

                ''''''''''''' Added by Ujwala Atre - to implement 'lock status' functionality as on 11152010
                If MainMenu.IsAccess(False, _patientID) = False Then
                    Exit Sub
                End If
                ''''''''''''' Added by Ujwala Atre - to implement 'lock status' functionality as on 11152010

                If Not blnFinished Then
                    Dim objExam As New clsPatientExams
                    objExam.SetProviderExam(ExamID)
                    objExam.Dispose()
                    objExam = Nothing
                End If

                Me.Cursor = Cursors.WaitCursor

                Dim frm As New frmPatientExam(PatientId, VisitID)

                With frm
                    .Hide()
                    .blnModify = True
                    .Text = "Past Exams"
                    Dim sender As Object = Nothing
                    Dim e As System.EventArgs = Nothing
                    .cmdPastExam_Click(sender, e)
                    '.PatientID = gnPatientID
                    .PatientID = PatientId
                    ' .chkShowPreview.Visible = True
                    .pnlPastExam.Visible = True
                    If (.OpenPastExam(ExamID, VisitID, Convert.ToDateTime(DOS), ExamName.Trim, blnFinished) = True) Then

                        '''' Hide Tool Bar Mahesh 20070424
                        '    Me.pnlMenu.Visible = False
                        '''' User Want to Open Exam
                        Me.pnlLeft.Visible = False
                        'Me.pnlRights.Visible = False
                        Me.Splitter1.Visible = False
                        '  .MdiParent = Me
                        .IsPastExam = True
                        .Show()
                        If .ExamViewMode Then
                            .ViewExam(ExamID)
                        Else
                            .OpenPastExamContents(ExamID, blnFinished)
                        End If

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, "Exam Opened", .PatientID, ExamID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    Else

                        frm.Dispose()
                        frm = Nothing
                    End If
                End With
                Me.Cursor = Cursors.Default
            Else
                Me.Cursor = Cursors.Default
                MessageBox.Show("Select the patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Function GetPatientCodefromID(ByVal nPatientID As Long) As String
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        ' Dim strSQL As String
        Dim PatCode As String
        'strSQL = "SELECT nPatientID FROM Patient WHERE  sPatientCode = '" & PatientCode & "'"
        oDB.Connect(GetConnectionString)
        PatCode = oDB.ExecuteQueryScaler("SELECT  sPatientCode FROM Patient where nPatientID = " & nPatientID)

        oDB.Disconnect()
        oDB = Nothing

        Return PatCode
        '
    End Function





    Public Sub Fill_Exams(Optional ByVal strCategory As String = "")

        Try

            Dim dtExamDetal As New DataTable
            Dim dtStart As DateTime
            Dim dtEnd As DateTime
            Dim nPastExamID As Long = 0
            If C1Exams.Row > 1 Then
                nPastExamID = Convert.ToInt64(C1Exams.GetData(C1Exams.Row, "ExamID"))
            End If

            If IsNothing(oUnFinishedExams) Then
                Exit Sub
            Else
                If IsNothing(oUnFinishedExams.mySelectedNode.Text) = True Then Exit Sub
            End If
            ''
            oUnFinishedExams.trvCateogory.SelectedNode = oUnFinishedExams.mySelectedNode
            Dim dtExams As New DataTable
            Dim objExams As New clsPatientExams
            ' Dim _DateRange() As DateTime
            'C1Exams.Clear()
            C1Exams.DataSource = Nothing
            C1Exams.Rows.Count = 1

            If oUnFinishedExams.mySelectedNode.Text = "Customize" Then
                pnlLeftTopTop.Visible = True

                dtExams = objExams.Fill_Exams(_CustomFromDate, _CustomToDate.AddDays(1), blnFinished:=False, ProviderID:=ProviderID, speciality:=IIf(cmbTemplateSpecility.Text = "", "All", Replace(cmbTemplateSpecility.Text, "'", "''")))

                pnlLeftTopTop.Enabled = True
                Label2.Text = "From "
                Label1.Text = "To "
                Label1.Visible = True
                dtTo.Visible = True
                btnShowExams.Visible = True
                If Not IsNothing(dtExams) Then
                    If dtExams.Rows.Count > 0 Then
                        Dim dv As New DataView(dtExams)

                        dtExams = dv.ToTable()
                        If Not IsNothing(objExams) Then
                            objExams.Dispose()
                            objExams = Nothing
                        End If
                        C1Exams.DataSource = dtExams
                    Else
                        C1Exams.DataSource = dtExams
                        C1Exams.Rows.Count = 1
                    End If
                    'Bug #49541: gloEMR - Unfinished Exam - Application does not display column names. 
                    DesignCustomizeGrid()
                Else
                    'C1Exams.Clear()
                    C1Exams.DataSource = Nothing
                    C1Exams.Rows.Count = 1
                End If

                'C1Exams.EndUpdate()
            Else
                'category other than customize
                pnlLeftTopTop.Visible = False

                If Not IsNothing(dtUnfinishedExams) Then
                    Dim dv As New DataView(dtUnfinishedExams)
                    dv.RowFilter = " Category = '" & oUnFinishedExams.mySelectedNode.Text & "'"


                    If dv.ToTable.Rows.Count > 0 Then

                        dtEnd = dv.ToTable.Rows(0)(0)
                        dtStart = dv.ToTable.Rows(dv.ToTable.Rows.Count - 1)(0)
                    End If
                    'dtExamDetal = objExams.GetLoginProvUnfinishedExams(ProviderID, dtStart, dtEnd, IIf(cmbTemplateSpecility.Text <> "All", Replace(cmbTemplateSpecility.Text, "'", "''"), ""))
                    dtExamDetal = objExams.GetLoginProvUnfinishedExams(ProviderID, dtStart, dtEnd, IIf(cmbTemplateSpecility.Text = "", "All", Replace(cmbTemplateSpecility.Text, "'", "''")))



                    If Not IsNothing(dtExamDetal) Then
                        Dim dvnew As New DataView(dtExamDetal)

                        C1Exams.DataSource = dvnew.ToTable
                        DesignGrid()
                    End If

                End If
            End If
            If Not IsNothing(objExams) Then  ''if condition added for Bug #69021: 
                objExams.Dispose()
                objExams = Nothing
            End If

            If nPastExamID > 0 Then
                If Not IsNothing(C1Exams.Cols("ExamID")) Then
                    Dim rowIndex As Int64
                    If C1Exams.Cols("ExamID").Index > 0 Then
                        rowIndex = C1Exams.FindRow(nPastExamID, 1, C1Exams.Cols("ExamID").Index, False, True, False)
                        If rowIndex > 0 Then
                            C1Exams.Select(rowIndex, 0, True)
                        End If


                    End If

                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub


    'sarika 20081206
    Public Function Fill_UnFinishedExamsNew()
        ' Dim i As Integer

        Dim obj As New clsPatientExams

        If IsNothing(dtUnfinishedExams) = False Then
            If (dtUnfinishedExams.Rows.Count = 0) Then
                dtUnfinishedExams = obj.GetAllUnfinishedExams(ProviderID)
            End If
        Else
            'dtUnfinishedExams = New DataTable
            dtUnfinishedExams = obj.GetAllUnfinishedExams(ProviderID)
        End If
        If Not IsNothing(dtUnfinishedExams) Then

            Dim dv As New DataView(dtUnfinishedExams)
            'dv.Sort = "DOS DESC "
            dtUnfinishedExams = dv.ToTable()

            If IsNothing(SelectedNode) = False Then
                oUnFinishedExams.mySelectedNode = SelectedNode
            Else
                oUnFinishedExams.trvCateogory.SelectedNode = oUnFinishedExams.trvCateogory.Nodes(0)
                oUnFinishedExams.mySelectedNode = oUnFinishedExams.trvCateogory.Nodes(0)
            End If

            Call Fill_Exams(oUnFinishedExams.mySelectedNode.Text)
        Else
            'C1Exams.Clear()
            C1Exams.DataSource = Nothing
            C1Exams.Rows.Count = 1
        End If
        obj.Dispose()
        Return Nothing
    End Function
    '-----------------


#End Region

    Private Sub btnShowExams_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnShowExams.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnShowExams.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnShowExams_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnShowExams.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btnShowExams.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub DesignGrid()
        Try
            C1Exams.AllowEditing = False
            C1Exams.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            C1Exams.AllowSorting = AllowSortingEnum.SingleColumn
            C1Exams.Rows.Fixed = 1
            C1Exams.ExtendLastCol = True
            If Not IsNothing(C1Exams.Cols("ExamID")) Then

                C1Exams.Cols("ExamID").Caption = "ExamID"
                C1Exams.Cols("ExamID").Visible = False

                C1Exams.Cols("PatientID").Caption = "PatientID"
                C1Exams.Cols("PatientID").Visible = False

                C1Exams.Cols("PatientCode").Caption = "Patient Code"
                C1Exams.Cols("PatientCode").Visible = False

                C1Exams.Cols("ExamName").Caption = "Exam Name"
                'C1Exams.Cols("ExamName").Width = C1Exams.Width * 0.3
                C1Exams.Cols("ExamName").Width = _GridWidth * 0.3
                C1Exams.Cols("ExamName").Move(1)

                'Added By Shweta 20091127
                'To Show the Template Name
                C1Exams.Cols("TemplateName").Caption = "Template Name"
                'C1Exams.Cols("TemplateName").Width = C1Exams.Width * 0.3
                C1Exams.Cols("TemplateName").Width = _GridWidth * 0.3
                C1Exams.Cols("TemplateName").Move(2)
                'End 20091127

                C1Exams.Cols("DOS").Caption = "DOS"
                'C1Exams.Cols("DOS").Width = (C1Exams.Width * 0.2) - 20
                C1Exams.Cols("DOS").Width = (_GridWidth * 0.12) - 40
                'Changed By Shweta 20091127
                'To Show the Template Name
                C1Exams.Cols("DOS").Move(3)
                'End 20091127

                C1Exams.Cols("PatientName").Caption = "Patient Name"
                'C1Exams.Cols("PatientName").Width = C1Exams.Width * 0.2
                C1Exams.Cols("PatientName").Width = _GridWidth * 0.2
                C1Exams.Cols("PatientName").Move(0)
                C1Exams.Cols("Specialty").Width = _GridWidth * 0.1
                C1Exams.Cols("Specialty").Caption = "Specialty"
                C1Exams.Cols("Flag").Caption = "Flag"
                C1Exams.Cols("Flag").Visible = False

                C1Exams.Cols("VisitID").Caption = "VisitID"
                C1Exams.Cols("VisitID").Visible = False

                C1Exams.Cols("PatientDOB").Caption = "DOB"
                C1Exams.Cols("PatientDOB").Visible = False

                C1Exams.Cols("Category").Caption = "Category"
                C1Exams.Cols("Category").Visible = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DesignCustomizeGrid()
        Try
            C1Exams.AllowEditing = False
            C1Exams.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
            C1Exams.AllowSorting = AllowSortingEnum.SingleColumn
            C1Exams.Rows.Fixed = 1
            C1Exams.ExtendLastCol = True
            C1Exams.Cols("ExamID").Caption = "ExamID"
            C1Exams.Cols("ExamID").Visible = False


            C1Exams.Cols("PatientName").Caption = "Patient Name"
            'C1Exams.Cols("PatientName").Width = C1Exams.Width * 0.4
            C1Exams.Cols("PatientName").Width = _GridWidth * 0.2
            C1Exams.Cols("PatientName").Move(0)

            C1Exams.Cols("ExamName").Caption = "Exam Name"
            'C1Exams.Cols("ExamName").Width = C1Exams.Width * 0.4
            C1Exams.Cols("ExamName").Width = _GridWidth * 0.3
            C1Exams.Cols("ExamName").Move(1)

            'Added By Shweta 20091127
            'To Show the Template Name
            C1Exams.Cols("TemplateName").Caption = "Template Name"
            'C1Exams.Cols("TemplateName").Width = C1Exams.Width * 0.4
            C1Exams.Cols("TemplateName").Width = _GridWidth * 0.3
            C1Exams.Cols("TemplateName").Move(2)
            'End 20091127

            C1Exams.Cols("DOS").Caption = "DOS"
            'C1Exams.Cols("DOS").Width = (C1Exams.Width * 0.2) - 20
            C1Exams.Cols("DOS").Width = (_GridWidth * 0.1) - 40
            C1Exams.Cols("DOS").Move(3)

            C1Exams.Cols("VisitID").Caption = "VisitID"
            C1Exams.Cols("VisitID").Visible = False

            C1Exams.Cols("PatientID").Caption = "PatientID"
            C1Exams.Cols("PatientID").Visible = False

            C1Exams.Cols("PatientCode").Caption = "Patient Code"
            C1Exams.Cols("PatientCode").Visible = False

            C1Exams.Cols("DOB").Caption = "DOB"
            C1Exams.Cols("DOB").Visible = False

            'Bug #49541: gloEMR - Unfinsished Exam - Application does not display column names.
            C1Exams.Cols("Specialty").Width = _GridWidth * 0.1
            C1Exams.Cols("Specialty").Caption = "Specialty"
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1Exams_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Exams.MouseDoubleClick
        Try
            ''Added by Mayuri:20100405-Issue ID:# 6357
            ''It should Re-order the data but should not Open unfinished exam.
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As HitTestInfo = C1Exams.HitTest(ptPoint)

            If htInfo.Type = HitTestTypeEnum.Cell Then
                blnExamOpened = True
                OpenExam()
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Close, "Past exam opened", _patientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub C1Exams_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Exams.MouseDown
        Try
            'Try
            '    If (IsNothing(C1Exams.ContextMenuStrip) = False) Then
            '        C1Exams.ContextMenuStrip.Dispose()
            '        C1Exams.ContextMenuStrip = Nothing
            '    End If
            'Catch ex As Exception

            'End Try
            C1Exams.ContextMenuStrip = Nothing
            Dim nRow As Integer = -1

            If e.Button = Windows.Forms.MouseButtons.Right Then
                C1Exams.Row = C1Exams.HitTest(e.X, e.Y).Row
                nRow = C1Exams.Row

                If nRow > 0 Then
                    cmnu_Diagnosis.Items.Clear()
                    Dim oMenuItem_ViewDiagnosis As New ToolStripMenuItem
                    oMenuItem_ViewDiagnosis.Text = "View Diagnosis"
                    oMenuItem_ViewDiagnosis.ForeColor = Color.FromArgb(31, 73, 125)
                    oMenuItem_ViewDiagnosis.Image = imgTreeView.Images(8)
                    cmnu_Diagnosis.Items.Add(oMenuItem_ViewDiagnosis)
                    AddHandler oMenuItem_ViewDiagnosis.Click, AddressOf oMenuItem_ViewDiagnosisclick
                    'Try
                    '    If (IsNothing(C1Exams.ContextMenuStrip) = False) Then
                    '        C1Exams.ContextMenuStrip.Dispose()
                    '        C1Exams.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    C1Exams.ContextMenuStrip = cmnu_Diagnosis
                Else
                    'Try
                    '    If (IsNothing(C1Exams.ContextMenuStrip) = False) Then
                    '        C1Exams.ContextMenuStrip.Dispose()
                    '        C1Exams.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    C1Exams.ContextMenuStrip = Nothing
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmFinishExam_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        _GridWidth = C1Exams.Width
        DesignGrid()
    End Sub

    Private Sub C1Exams_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Exams.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
    Private Function Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing
        Dim oPatient As New gloPatient.gloPatient(GetConnectionString)
        Try
            dtPatient = New DataTable
            dtPatient = GetPatientInfo(_patientID)
            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    strPatientCode = Convert.ToString(dtPatient.Rows(0)("sPatientCode"))
                    strPatientFirstName = Convert.ToString(dtPatient.Rows(0)("sFirstName"))
                    strPatientLastName = Convert.ToString(dtPatient.Rows(0)("sLastName"))
                    strPatientDOB = Convert.ToString(dtPatient.Rows(0)("dtDOB"))
                    strPatientAge = GetAge(Convert.ToDateTime(dtPatient.Rows(0)("dtDOB")))
                    strPatientGender = Convert.ToString(dtPatient.Rows(0)("sGender"))
                    strPatientMaritalStatus = Convert.ToString(dtPatient.Rows(0)("sMaritalStatus"))
                End If
            End If

        Catch ex As Exception

        Finally
            If IsNothing(dtPatient) = False Then
                dtPatient.Dispose()
                dtPatient = Nothing
            End If
            If IsNothing(oPatient) = False Then
                oPatient.Dispose()
                oPatient = Nothing
            End If

        End Try
        Return Nothing
    End Function

#Region "6786 : Dhruv -> For the From to Validation"
    Private Sub dtFrom_CloseUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtFrom.CloseUp
        ToFromDateValidation()
    End Sub


    Private Sub dtTo_CloseUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtTo.CloseUp
        ToFromDateValidation()
    End Sub
    Private Function ToFromDateValidation() As Boolean
        If (Convert.ToDateTime(dtFrom.Text) > Convert.ToDateTime(dtTo.Text)) Then
            MessageBox.Show("Invalid date criteria", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtFrom.Focus()
            Return True
        End If
        Return Nothing
    End Function
    Private Sub Fill_TemplateSpecility()
        Dim pExam As New clsPatientExams()
        Dim dt As DataTable = pExam.GetAllTemplateSpecility()
        Dim str As String = dt.DefaultView.Sort()
        Dim r As DataRow
        r = dt.NewRow
        r.Item("sDescription") = "All"
        r.Item("nCategoryId") = 0
        dt.Rows.InsertAt(r, 0)

        Try
            cmbTemplateSpecility.DataSource = dt.DefaultView
            cmbTemplateSpecility.ValueMember = dt.Columns(0).ColumnName
            cmbTemplateSpecility.DisplayMember = dt.Columns(1).ColumnName
            cmbTemplateSpecility.SelectedIndex = 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If IsNothing(dt) = False Then
                dt = Nothing
            End If

            If IsNothing(pExam) = False Then
                pExam.Dispose()
                pExam = Nothing
            End If
        End Try
    End Sub
    Public Function GetAllTemplateSpecility() As DataTable

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing
        Dim oResultTable As New DataTable

        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@CategoryType"
            oParamater.Value = "Template Specialty"
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            oResultTable = oDB.GetDataTable("gsp_FillCategory_MST")
            If Not oResultTable Is Nothing Then
                oResultTable.Rows.Add(0, "<All>")
                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If IsNothing(oParamater) = False Then
                oParamater = Nothing
            End If
            If IsNothing(oResultTable) = False Then
                oResultTable.Dispose()
                oResultTable = Nothing
            End If
        End Try
    End Function
#End Region



    Private Sub cmbTemplateSpecility_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTemplateSpecility.SelectionChangeCommitted
        Fill_Exams()
    End Sub

    Public Sub FillProviderCombo()
        Dim dt As DataTable = Nothing   ''Slr new not needed 

        If IsNothing(dt_ProviderCombo) Then
            Dim oDB As New DataBaseLayer

            Dim strSelect As String = "select nProviderID,isnull(sFirstName,'') + ' ' + CASE ISNULL(sMiddleName,'') WHEN  '' THEN '' When sMiddleName then  " _
                                     & "sMiddleName +  ' ' END +  isnull(sLastName,'') as Name from Provider_MST"
            dt = oDB.GetDataTable_Query(strSelect)
            dt_ProviderCombo = dt
            oDB.Dispose()
            oDB = Nothing
            strSelect = Nothing
        Else
            dt = dt_ProviderCombo
        End If

        If Not IsNothing(dt) Then
            Dim dr As DataRow() = dt.Select("nProviderID=0")
            If (dr.Length = 0) Then
                Dim r As DataRow
                r = dt.NewRow
                r.Item("Name") = "All"
                r.Item("nProviderID") = 0
                dt.Rows.InsertAt(r, 0)

                r = Nothing
            End If
        End If

        'Dim strProviderName As String = ""
        cmbProvider.DataSource = dt
        cmbProvider.DisplayMember = dt.Columns("Name").ToString()
        cmbProvider.ValueMember = dt.Columns("nProviderID").ToString()
        cmbProvider.SelectedValue = gnLoginProviderID
        ProviderID = gnLoginProviderID



    End Sub
    Private Sub cmbProvider_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProvider.SelectionChangeCommitted
        ProviderID = cmbProvider.SelectedValue
        'If Not (oUnFinishedExams.trvCateogory.SelectedNode.Text) = "Customize" Then
        Fill_Exams(ProviderID)
        'End If
    End Sub
    Private Sub ts_btnPrint_Click(sender As Object, e As System.EventArgs) Handles ts_btnPrint.Click
        If C1Exams.Rows.Count > 1 Then
            Print_SSRS("rpt_UnfinishedExam")
        End If
    End Sub
    Public Sub Print_SSRS(ByVal _ReportName As String)
        Try
            Dim _parameterName As String = ""
            Dim _ParameterValue As String = ""
            Dim dtStart As DateTime
            Dim dtEnd As DateTime

            If Not IsNothing(dtUnfinishedExams) Then
                Dim dv As New DataView(dtUnfinishedExams)
                dv.RowFilter = " Category = '" & oUnFinishedExams.mySelectedNode.Text & "'"
                If dv.ToTable.Rows.Count > 0 Then

                    dtEnd = dv.ToTable.Rows(0)(0)
                    dtStart = dv.ToTable.Rows(dv.ToTable.Rows.Count - 1)(0)
                End If
            End If
            If _ReportName.Contains("rpt_UnfinishedExam") Then
                _parameterName = "ProviderID,dtStartDOS,dtENDDOS,sProviderName,Specility,IsCustomized"
                If Not (oUnFinishedExams.trvCateogory.SelectedNode.Text) = "Customize" Then
                    _ParameterValue = ProviderID.ToString() & "," & dtStart & "," & dtEnd & "," & cmbProvider.GetItemText(cmbProvider.SelectedItem) & "," & IIf(cmbTemplateSpecility.Text = "", "All", Replace(cmbTemplateSpecility.Text, "'", "''")) & "," & "Other"
                Else
                    _ParameterValue = ProviderID.ToString() & "," & dtFrom.Value & "," & dtTo.Value & "," & cmbProvider.GetItemText(cmbProvider.SelectedItem) & "," & IIf(cmbTemplateSpecility.Text = "", "All", Replace(cmbTemplateSpecility.Text, "'", "''")) & "," & "Customize"
                End If
            End If
            PrintSSRSReport(_ReportName, _parameterName, _ParameterValue, True, True)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
    End Sub

    Public blnFlagIsPrintSuccessFull As Boolean = True
    Private Sub PrintSSRSReport(ByVal ReportName As String, ByVal ParameterName As String, ByVal ParameterValue As String, ByVal blnPrint As Boolean, ByVal _ShowPrintDialog As Boolean)
        Dim clsPrntRpt As gloSSRSApplication.clsPrintReport

        clsPrntRpt = New gloSSRSApplication.clsPrintReport(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR, Nothing)
        clsPrntRpt.PrintReport(ReportName, ParameterName, ParameterValue, _ShowPrintDialog, "")

        blnFlagIsPrintSuccessFull = clsPrntRpt.IsPrintSuccess

        If Not clsPrntRpt Is Nothing Then
            clsPrntRpt.Dispose()
            clsPrntRpt = Nothing
        End If
      
    End Sub
End Class
