Imports C1.Win.C1FlexGrid
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMRGeneralLibrary.gloEMRPrescription
Imports System.Data.SqlClient

Public Class frmUnLockExams
    Inherits System.Windows.Forms.Form



    Dim COL_Select As Integer = 0
    Dim COL_ExamID As Integer = 1
    Dim COL_PatientCode As Integer = 2
    Dim COL_PatientName As Integer = 3
    Dim COL_ExamName As Integer = 4
    Dim COL_UserName As Integer = 5
    Dim COL_MachineName As Integer = 6
    Dim COL_dtDOS As Integer = 7
    Dim COL_IsFinished As Integer = 8
    Dim COL_PatientID As Integer = 9
    Dim COL_VisitID As Integer = 10
    Dim COL_VisitDate As Integer = 11
    Dim COL_PrescriptionDateTime As Integer = 12
    Dim COL_VitalID As Integer = 13
    Dim COL_VitalDateTime As Integer = 14
    Dim COL_LabOrderID As Integer = 15
    Dim COL_OrderDateTime As Integer = 16
    Dim COL_OrderName As Integer = 17
    Dim COL_MessageID As Integer = 18
    Dim COL_MessageDateTime As Integer = 19
    Dim COL_MessageName As Integer = 20
    Dim COL_LetterID As Integer = 21
    Dim COL_LetterDateTime As Integer = 22
    Dim COL_LetterName As Integer = 23
    Dim COL_PTProtocolID As Integer = 24
    Dim COL_PTProtocolDateTime As Integer = 25
    Dim COL_PTProtocolName As Integer = 26
    Dim COL_ConsentID As Integer = 27
    Dim COL_ConsentDateTime As Integer = 28
    Dim COL_ConsentName As Integer = 29
    Dim COL_FlowSheetRecordID As Integer = 30
    Dim COL_FlowSheetName As Integer = 31
    Dim COL_TaskID As Integer = 32
    Dim COL_TaskDate As Integer = 33
    Dim COL_Subject As Integer = 34
    Dim COL_ImmunizationID As Integer = 35
    Dim COL_DocumentID As Integer = 36
    Dim COL_DocumetnName As Integer = 37
    Dim COL_DisclosureID As Integer = 38
    Dim COL_DisclosureDateTime As Integer = 39
    Dim COL_DisclosureName As Integer = 40
    ''Bug : 00000828: Record locking. New node added to unlock Nurse Notes.
    Dim COL_NurseNotesID As Integer = 41
    Dim COL_NurseNotesDateTime As Integer = 42
    Dim COL_NurseNotesName As Integer = 43

    Dim COL_WorkersCompFormID As Integer = 44
    Dim COL_WorkersCompFormDateOfInjury As Integer = 45
    Dim COL_WorkersCompFormType As Integer = 46
    Dim COL_WorkersCompFormClaimNo As Integer = 47
    Dim COL_WorkersCompFormDOS As Integer = 48
    Dim COL_WorkersCompFormCreatedBy As Integer = 49
    Dim COL_WorkersCompFormCreatedDate As Integer = 50
    Dim COL_sDeviceId As Integer = 51
    Dim COL_dtImplantDate As Integer = 52
    Dim COL_BrandName As Integer = 53

    '' Total No Of Columns
    Dim Col_Count As Integer = 54
    Dim _PatientID As Long
    Friend WithEvents pnltrvformlist As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents C1grdExams As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_UnLockExam As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnUnlockExam As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnSelectAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents trvFormList As System.Windows.Forms.TreeView
    Private WithEvents _RxBusinessLayer As RxBusinesslayer
    Private WithEvents ClsImplantDeviceDBLayer As ClsImplantDeviceDBLayer

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
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try

            Dim dtpContextMenu As ContextMenu() = {cmnuSelectClear}
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpContextMenu)
                gloGlobal.cEventHelper.DisposeContextMenu(dtpContextMenu)
            Catch ex As Exception

            End Try


        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlLeftMain As System.Windows.Forms.Panel
    Friend WithEvents pnlLeftBottom As System.Windows.Forms.Panel
    Friend WithEvents pnlGrid As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmnuSelectClear As System.Windows.Forms.ContextMenu
    Friend WithEvents cmnuSelectAll As System.Windows.Forms.MenuItem
    Friend WithEvents cmnuClearAll As System.Windows.Forms.MenuItem
    Friend WithEvents imgTreeView As System.Windows.Forms.ImageList
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents pnlDSO As System.Windows.Forms.Panel
    Friend WithEvents wdExam As AxDSOFramer.AxFramerControl
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblExamName As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmnuRefresh As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUnLockExams))
        Me.imgTreeView = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlLeftMain = New System.Windows.Forms.Panel()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.C1grdExams = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlDSO = New System.Windows.Forms.Panel()
        Me.wdExam = New AxDSOFramer.AxFramerControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblExamName = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.pnltrvformlist = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.trvFormList = New System.Windows.Forms.TreeView()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.pnlLeftBottom = New System.Windows.Forms.Panel()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.cmnuSelectClear = New System.Windows.Forms.ContextMenu()
        Me.cmnuSelectAll = New System.Windows.Forms.MenuItem()
        Me.cmnuClearAll = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.cmnuRefresh = New System.Windows.Forms.MenuItem()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_UnLockExam = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnSelectAll = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnUnlockExam = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlMain.SuspendLayout()
        Me.pnlLeftMain.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.C1grdExams, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlDSO.SuspendLayout()
        CType(Me.wdExam, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.pnltrvformlist.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.pnlLeftBottom.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_UnLockExam.SuspendLayout()
        Me.SuspendLayout()
        '
        'imgTreeView
        '
        Me.imgTreeView.ImageStream = CType(resources.GetObject("imgTreeView.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTreeView.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeView.Images.SetKeyName(0, "Bullet06.ico")
        Me.imgTreeView.Images.SetKeyName(1, "Small Arrow.ico")
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlLeftMain)
        Me.pnlMain.Controls.Add(Me.Panel8)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 54)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(824, 536)
        Me.pnlMain.TabIndex = 4
        '
        'pnlLeftMain
        '
        Me.pnlLeftMain.Controls.Add(Me.pnlGrid)
        Me.pnlLeftMain.Controls.Add(Me.Splitter1)
        Me.pnlLeftMain.Controls.Add(Me.pnltrvformlist)
        Me.pnlLeftMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeftMain.Name = "pnlLeftMain"
        Me.pnlLeftMain.Size = New System.Drawing.Size(824, 510)
        Me.pnlLeftMain.TabIndex = 3
        '
        'pnlGrid
        '
        Me.pnlGrid.Controls.Add(Me.Panel5)
        Me.pnlGrid.Controls.Add(Me.Panel7)
        Me.pnlGrid.Controls.Add(Me.pnlDSO)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.Location = New System.Drawing.Point(228, 0)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Size = New System.Drawing.Size(596, 510)
        Me.pnlGrid.TabIndex = 2
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Label11)
        Me.Panel5.Controls.Add(Me.Label12)
        Me.Panel5.Controls.Add(Me.Label13)
        Me.Panel5.Controls.Add(Me.Label14)
        Me.Panel5.Controls.Add(Me.C1grdExams)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 30)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel5.Size = New System.Drawing.Size(596, 480)
        Me.Panel5.TabIndex = 9
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(1, 476)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(591, 1)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "label2"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(0, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 476)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(592, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 476)
        Me.Label13.TabIndex = 10
        Me.Label13.Text = "label3"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(593, 1)
        Me.Label14.TabIndex = 9
        Me.Label14.Text = "label1"
        '
        'C1grdExams
        '
        Me.C1grdExams.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1grdExams.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1grdExams.BackColor = System.Drawing.Color.White
        Me.C1grdExams.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1grdExams.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1grdExams.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1grdExams.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1grdExams.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1grdExams.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1grdExams.Location = New System.Drawing.Point(0, 0)
        Me.C1grdExams.Name = "C1grdExams"
        Me.C1grdExams.Rows.Count = 1
        Me.C1grdExams.Rows.DefaultSize = 19
        Me.C1grdExams.Rows.Fixed = 0
        Me.C1grdExams.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1grdExams.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1grdExams.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1grdExams.Size = New System.Drawing.Size(593, 477)
        Me.C1grdExams.StyleInfo = resources.GetString("C1grdExams.StyleInfo")
        Me.C1grdExams.TabIndex = 8
        Me.C1grdExams.Tree.NodeImageCollapsed = CType(resources.GetObject("C1grdExams.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1grdExams.Tree.NodeImageExpanded = CType(resources.GetObject("C1grdExams.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Panel1)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel7.Size = New System.Drawing.Size(596, 30)
        Me.Panel7.TabIndex = 11
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(593, 24)
        Me.Panel1.TabIndex = 4
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(1, 23)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(591, 1)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "label2"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 23)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(592, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 23)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "label3"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(593, 1)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "label1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(17, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "   Locked Exams"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlDSO
        '
        Me.pnlDSO.Controls.Add(Me.wdExam)
        Me.pnlDSO.Controls.Add(Me.Panel2)
        Me.pnlDSO.Controls.Add(Me.Label15)
        Me.pnlDSO.Controls.Add(Me.Label16)
        Me.pnlDSO.Controls.Add(Me.Label17)
        Me.pnlDSO.Controls.Add(Me.Label18)
        Me.pnlDSO.Location = New System.Drawing.Point(58, 69)
        Me.pnlDSO.Name = "pnlDSO"
        Me.pnlDSO.Size = New System.Drawing.Size(208, 152)
        Me.pnlDSO.TabIndex = 7
        Me.pnlDSO.Visible = False
        '
        'wdExam
        '
        Me.wdExam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wdExam.Enabled = True
        Me.wdExam.Location = New System.Drawing.Point(1, 25)
        Me.wdExam.Name = "wdExam"
        Me.wdExam.OcxState = CType(resources.GetObject("wdExam.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdExam.Size = New System.Drawing.Size(206, 126)
        Me.wdExam.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Controls.Add(Me.lblExamName)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(1, 1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(206, 24)
        Me.Panel2.TabIndex = 13
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(0, 23)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(206, 1)
        Me.Label19.TabIndex = 19
        Me.Label19.Text = "label1"
        '
        'lblExamName
        '
        Me.lblExamName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblExamName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExamName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblExamName.Location = New System.Drawing.Point(0, 0)
        Me.lblExamName.Name = "lblExamName"
        Me.lblExamName.Size = New System.Drawing.Size(206, 24)
        Me.lblExamName.TabIndex = 13
        Me.lblExamName.Text = "Exam Name"
        Me.lblExamName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(1, 151)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(206, 1)
        Me.Label15.TabIndex = 21
        Me.Label15.Text = "label2"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(0, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 151)
        Me.Label16.TabIndex = 20
        Me.Label16.Text = "label4"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(207, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 151)
        Me.Label17.TabIndex = 19
        Me.Label17.Text = "label3"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(0, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(208, 1)
        Me.Label18.TabIndex = 18
        Me.Label18.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(225, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 510)
        Me.Splitter1.TabIndex = 4
        Me.Splitter1.TabStop = False
        '
        'pnltrvformlist
        '
        Me.pnltrvformlist.Controls.Add(Me.Panel4)
        Me.pnltrvformlist.Controls.Add(Me.Panel6)
        Me.pnltrvformlist.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnltrvformlist.Location = New System.Drawing.Point(0, 0)
        Me.pnltrvformlist.Name = "pnltrvformlist"
        Me.pnltrvformlist.Size = New System.Drawing.Size(225, 510)
        Me.pnltrvformlist.TabIndex = 3
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.trvFormList)
        Me.Panel4.Controls.Add(Me.Label25)
        Me.Panel4.Controls.Add(Me.Label24)
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Controls.Add(Me.Label4)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 30)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel4.Size = New System.Drawing.Size(225, 480)
        Me.Panel4.TabIndex = 6
        '
        'trvFormList
        '
        Me.trvFormList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvFormList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvFormList.ForeColor = System.Drawing.Color.Black
        Me.trvFormList.ImageIndex = 0
        Me.trvFormList.ImageList = Me.imgTreeView
        Me.trvFormList.Indent = 20
        Me.trvFormList.ItemHeight = 20
        Me.trvFormList.Location = New System.Drawing.Point(7, 4)
        Me.trvFormList.Name = "trvFormList"
        Me.trvFormList.SelectedImageIndex = 0
        Me.trvFormList.ShowLines = False
        Me.trvFormList.ShowNodeToolTips = True
        Me.trvFormList.Size = New System.Drawing.Size(217, 472)
        Me.trvFormList.TabIndex = 0
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.White
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(7, 1)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(217, 3)
        Me.Label25.TabIndex = 10
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.White
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(4, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(3, 475)
        Me.Label24.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(4, 476)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(220, 1)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 476)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "label4"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(224, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 476)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "label3"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(222, 1)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "label1"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel3)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.Panel6.Size = New System.Drawing.Size(225, 30)
        Me.Panel6.TabIndex = 10
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel3.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel3.Controls.Add(Me.lbl_RightBrd)
        Me.Panel3.Controls.Add(Me.lbl_TopBrd)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(3, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(222, 24)
        Me.Panel3.TabIndex = 5
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 23)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(220, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(221, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(222, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(222, 24)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "   Modules"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.pnlLeftBottom)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel8.Location = New System.Drawing.Point(0, 510)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel8.Size = New System.Drawing.Size(824, 26)
        Me.Panel8.TabIndex = 4
        Me.Panel8.Visible = False
        '
        'pnlLeftBottom
        '
        Me.pnlLeftBottom.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlLeftBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeftBottom.Controls.Add(Me.lblProgress)
        Me.pnlLeftBottom.Controls.Add(Me.ProgressBar1)
        Me.pnlLeftBottom.Controls.Add(Me.Label20)
        Me.pnlLeftBottom.Controls.Add(Me.Label21)
        Me.pnlLeftBottom.Controls.Add(Me.Label22)
        Me.pnlLeftBottom.Controls.Add(Me.Label23)
        Me.pnlLeftBottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftBottom.Location = New System.Drawing.Point(3, 0)
        Me.pnlLeftBottom.Name = "pnlLeftBottom"
        Me.pnlLeftBottom.Size = New System.Drawing.Size(818, 23)
        Me.pnlLeftBottom.TabIndex = 2
        '
        'lblProgress
        '
        Me.lblProgress.AutoSize = True
        Me.lblProgress.BackColor = System.Drawing.Color.Transparent
        Me.lblProgress.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblProgress.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblProgress.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgress.Location = New System.Drawing.Point(496, 1)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblProgress.Size = New System.Drawing.Size(80, 20)
        Me.lblProgress.TabIndex = 7
        Me.lblProgress.Text = "Printing ...."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblProgress.Visible = False
        '
        'ProgressBar1
        '
        Me.ProgressBar1.BackColor = System.Drawing.Color.White
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Right
        Me.ProgressBar1.ForeColor = System.Drawing.Color.LimeGreen
        Me.ProgressBar1.Location = New System.Drawing.Point(576, 1)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(241, 21)
        Me.ProgressBar1.TabIndex = 6
        Me.ProgressBar1.Visible = False
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label20.Location = New System.Drawing.Point(1, 22)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(816, 1)
        Me.Label20.TabIndex = 11
        Me.Label20.Text = "label2"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(0, 1)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(1, 22)
        Me.Label21.TabIndex = 10
        Me.Label21.Text = "label4"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(817, 1)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 22)
        Me.Label22.TabIndex = 9
        Me.Label22.Text = "label3"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(818, 1)
        Me.Label23.TabIndex = 8
        Me.Label23.Text = "label1"
        '
        'cmnuSelectClear
        '
        Me.cmnuSelectClear.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.cmnuSelectAll, Me.cmnuClearAll, Me.MenuItem2, Me.cmnuRefresh})
        '
        'cmnuSelectAll
        '
        Me.cmnuSelectAll.Index = 0
        Me.cmnuSelectAll.Text = "&Select All"
        '
        'cmnuClearAll
        '
        Me.cmnuClearAll.Index = 1
        Me.cmnuClearAll.Text = "Cl&ear All"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 2
        Me.MenuItem2.Text = "-"
        '
        'cmnuRefresh
        '
        Me.cmnuRefresh.Index = 3
        Me.cmnuRefresh.Text = "&Refresh"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlToolStrip.Controls.Add(Me.ts_UnLockExam)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlToolStrip.ForeColor = System.Drawing.Color.Black
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(824, 54)
        Me.pnlToolStrip.TabIndex = 13
        '
        'ts_UnLockExam
        '
        Me.ts_UnLockExam.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ts_UnLockExam.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_UnLockExam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_UnLockExam.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_UnLockExam.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_UnLockExam.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSelectAll, Me.ts_btnUnlockExam, Me.ts_btnClose})
        Me.ts_UnLockExam.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ts_UnLockExam.Location = New System.Drawing.Point(0, 0)
        Me.ts_UnLockExam.Name = "ts_UnLockExam"
        Me.ts_UnLockExam.Size = New System.Drawing.Size(824, 53)
        Me.ts_UnLockExam.TabIndex = 1
        Me.ts_UnLockExam.Text = "ToolStrip1"
        '
        'ts_btnSelectAll
        '
        Me.ts_btnSelectAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSelectAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnSelectAll.Image = CType(resources.GetObject("ts_btnSelectAll.Image"), System.Drawing.Image)
        Me.ts_btnSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSelectAll.Name = "ts_btnSelectAll"
        Me.ts_btnSelectAll.Size = New System.Drawing.Size(67, 50)
        Me.ts_btnSelectAll.Tag = "SelectAll"
        Me.ts_btnSelectAll.Text = "&Select All"
        Me.ts_btnSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnUnlockExam
        '
        Me.ts_btnUnlockExam.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnUnlockExam.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnUnlockExam.Image = CType(resources.GetObject("ts_btnUnlockExam.Image"), System.Drawing.Image)
        Me.ts_btnUnlockExam.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnUnlockExam.Name = "ts_btnUnlockExam"
        Me.ts_btnUnlockExam.Size = New System.Drawing.Size(51, 50)
        Me.ts_btnUnlockExam.Tag = "UnlockExam"
        Me.ts_btnUnlockExam.Text = "&Unlock"
        Me.ts_btnUnlockExam.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmUnLockExams
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(824, 590)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmUnLockExams"
        Me.Text = "Unlock Records"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlLeftMain.ResumeLayout(False)
        Me.pnlGrid.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        CType(Me.C1grdExams, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlDSO.ResumeLayout(False)
        CType(Me.wdExam, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.pnltrvformlist.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.pnlLeftBottom.ResumeLayout(False)
        Me.pnlLeftBottom.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_UnLockExam.ResumeLayout(False)
        Me.ts_UnLockExam.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmUnLockExams_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(C1grdExams)

        Try
            Fill_FormList()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.View, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Fill_FormList()
        trvFormList.Nodes.Add("Patient Exam")
        trvFormList.Nodes.Add("Patient Demographics")
        trvFormList.Nodes.Add("Patient ROS")
        trvFormList.Nodes.Add("Patient History")
        ''Bug : 00000828: Record locking. Prescription and Medication have same screen, so removing Medication and Changing name of Prescription to Rx-Meds.
        'trvFormList.Nodes.Add("Medication")
        'trvFormList.Nodes.Add("Prescription")
        trvFormList.Nodes.Add("Rx-Meds")
        trvFormList.Nodes.Add("Patient Vitals")
        ''Replace Radiology wiyh orders
        ''trvFormList.Nodes.Add("Radiology")
        trvFormList.Nodes.Add("Orders")
        trvFormList.Nodes.Add("Labs")
        trvFormList.Nodes.Add("Messages")
        trvFormList.Nodes.Add("Letters")
        trvFormList.Nodes.Add("PT Protocol")
        trvFormList.Nodes.Add("Patient Consent")
        trvFormList.Nodes.Add("Flowsheet")
        ''Bug : 00000828: Record locking. Locking not implemented for Tasks,Immunization and DMS. So removing it from unlock records tool.
        'trvFormList.Nodes.Add("Task")
        'trvFormList.Nodes.Add("Immunization")
        'trvFormList.Nodes.Add("DMS")
        trvFormList.Nodes.Add("Problem List")
        trvFormList.Nodes.Add("Disclosure Management")
        ''Bug : 00000828: Record locking. New node added to unlock Nurse Notes.
        trvFormList.Nodes.Add("Nurse Notes")
        trvFormList.Nodes.Add("Workers Comp Form")
        trvFormList.Nodes.Add("OB Plan")
        trvFormList.Nodes.Add("Implant Devices")
        trvFormList.SelectedNode = trvFormList.Nodes(0)
    End Sub

    Public Sub Fill_LockedExams()
        'If IsNothing(trvCriteria.SelectedNode) = True Then Exit Sub
        'If trvCriteria.SelectedNode Is trvCriteria.Nodes(0) Then Exit Sub
        '  Dim dvExam As New DataView
        Dim objExams As New clsPatientExams
        Dim dtExams As DataTable
        dtExams = objExams.Fill_LockedExams()


        If IsNothing(dtExams) = False Then
            '' Exams Of Selected Patient
            SetGridStyle(dtExams)
        End If
        'SelectClearAll(False)
        objExams.Dispose()
        objExams = Nothing

    End Sub

    Private Sub SetGridStyle(ByVal dt As DataTable)
        With C1grdExams
            .Visible = False
            .Rows.Fixed = 1
            .Cols.Fixed = 0
            '  If dt.Rows.Count > 0 Then
            .DataSource = dt.DefaultView
            '    End If
            .Cols.Count = Col_Count

            'Select ,nExamID, PatientCode, Name, ExamName, dtDOS, IsFinished, 
            'UserName, MachineName

            .AllowEditing = True
            .Width = .Width - 20

            .Cols(COL_Select).Width = .Width * 0.08
            .Cols(COL_Select).AllowEditing = True
            .Cols(COL_Select).DataType = System.Type.GetType("System.Boolean")
            .Cols(COL_Select).Name = "Select"
            .SetData(0, COL_Select, "Select")
            .Cols(COL_Select).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_ExamID).Width = .Width * 0
            .Cols(COL_ExamID).AllowEditing = False

            .Cols(COL_PatientCode).Width = .Width * 0.1
            .SetData(0, COL_PatientCode, "Patient Code")
            .Cols(COL_PatientCode).AllowEditing = False

            .Cols(COL_PatientName).Width = .Width * 0.15
            .SetData(0, COL_PatientName, "Patient Name")
            .Cols(COL_PatientName).AllowEditing = False

            .Cols(COL_ExamName).Width = .Width * 0.2
            .SetData(0, COL_ExamName, "Exam Name")
            .Cols(COL_ExamName).AllowEditing = False

            '' dtDOS, IsFinished, UserName, MachineName
            .Cols(COL_dtDOS).Width = .Width * 0.15
            .SetData(0, COL_dtDOS, "DOS")
            .Cols(COL_dtDOS).AllowEditing = False

            .Cols(COL_IsFinished).Width = .Width * 0.12
            .SetData(0, COL_IsFinished, "Finished")
            .Cols(COL_IsFinished).AllowEditing = False

            .Cols(COL_UserName).Width = .Width * 0.1
            .SetData(0, COL_UserName, "User Name")
            .Cols(COL_UserName).DataType = System.Type.GetType("System.string")
            .Cols(COL_UserName).AllowEditing = False

            .Cols(COL_MachineName).Width = .Width * 0.1
            .SetData(0, COL_MachineName, "Machine")
            .Cols(COL_MachineName).DataType = System.Type.GetType("System.string")
            .Cols(COL_MachineName).AllowEditing = False
            .Cols(COL_PatientID).Width = .Width * 0
            .Cols(COL_VisitID).Width = .Width * 0
            .Cols(COL_VisitDate).Width = .Width * 0
            .Cols(COL_PrescriptionDateTime).Width = .Width * 0
            .Cols(COL_VitalID).Width = .Width * 0
            .Cols(COL_VitalDateTime).Width = .Width * 0
            .Cols(COL_LabOrderID).Width = .Width * 0
            .Cols(COL_OrderDateTime).Width = .Width * 0
            .Cols(COL_OrderName).Width = .Width * 0
            .Cols(COL_MessageID).Width = .Width * 0
            .Cols(COL_MessageDateTime).Width = .Width * 0
            .Cols(COL_MessageName).Width = .Width * 0
            .Cols(COL_LetterID).Width = .Width * 0
            .Cols(COL_LetterDateTime).Width = .Width * 0
            .Cols(COL_LetterName).Width = .Width * 0
            .Cols(COL_PTProtocolID).Width = .Width * 0
            .Cols(COL_PTProtocolDateTime).Width = .Width * 0
            .Cols(COL_PTProtocolName).Width = .Width * 0
            .Cols(COL_ConsentID).Width = .Width * 0
            .Cols(COL_ConsentDateTime).Width = .Width * 0
            .Cols(COL_ConsentName).Width = .Width * 0
            .Cols(COL_FlowSheetRecordID).Width = .Width * 0
            .Cols(COL_FlowSheetName).Width = .Width * 0
            .Cols(COL_TaskID).Width = .Width * 0
            .Cols(COL_TaskDate).Width = .Width * 0
            .Cols(COL_Subject).Width = .Width * 0
            .Cols(COL_ImmunizationID).Width = .Width * 0
            .Cols(COL_DocumentID).Width = .Width * 0
            .Cols(COL_DocumetnName).Width = .Width * 0

            .Cols(COL_DisclosureID).Width = .Width * 0
            .Cols(COL_DisclosureName).Width = .Width * 0
            .Cols(COL_DisclosureDateTime).Width = .Width * 0

            .Cols(COL_NurseNotesID).Width = .Width * 0
            .Cols(COL_NurseNotesName).Width = .Width * 0
            .Cols(COL_NurseNotesDateTime).Width = .Width * 0

            .Cols(COL_WorkersCompFormID).Width = .Width * 0
            .Cols(COL_WorkersCompFormDateOfInjury).Width = .Width * 0
            .Cols(COL_WorkersCompFormType).Width = .Width * 0
            .Cols(COL_WorkersCompFormClaimNo).Width = .Width * 0
            .Cols(COL_WorkersCompFormDOS).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedBy).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedDate).Width = .Width * 0

            .Visible = True
            .Refresh()
        End With

    End Sub

    Private Sub SetGridStyleAllPatient(ByVal dt As DataTable)
        With C1grdExams
            .Visible = False
            '  If dt.Rows.Count > 0 Then
            .DataSource = dt.DefaultView
            '    End If
            .Cols.Count = 12

            .AllowEditing = True
            .Width = .Width - 20

            '' 0-Select,1-ExamID,2-PatientName ,3-ExamName,4-DOS,5-VisitID,6-PatientID,7-PatientCode,8-DOB,9-ProviderID,10-ProviderName, 11-bIsFinished 

            .Cols(0).Width = .Width * 0.08
            .Cols(0).AllowEditing = True
            .Cols(0).DataType = System.Type.GetType("System.Boolean")
            .Cols(0).Name = "Select"
            .SetData(0, 0, "Select")
            .Cols(0).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(1).Width = .Width * 0
            '.Cols(0).Name = "ExamID"
            .Cols(1).AllowEditing = False

            .Cols(2).Width = .Width * 0.3
            .SetData(0, 2, "Patient Name")
            .Cols(2).AllowEditing = False

            .Cols(3).Width = .Width * 0.35
            .SetData(0, 3, "Exam Name")
            .Cols(3).AllowEditing = False

            .Cols(4).Width = .Width * 0.15
            .SetData(0, 4, "DOS")
            .Cols(4).AllowEditing = False

            .Cols(5).Width = 0  '' VisitID
            .Cols(6).Width = 0  '' PatientID
            .Cols(7).Width = 0  '' Patient Code
            .Cols(8).Width = 0  '' DOB
            .Cols(9).Width = 0  '' ProviderID

            .Cols(10).Width = .Width * 0.3
            .SetData(0, 10, "Provider Name")
            .Cols(10).AllowEditing = False

            .Cols(11).Width = .Width * 0.1
            .SetData(0, 11, "Finished")
            .Cols(11).DataType = System.Type.GetType("System.string")
            .Cols(11).AllowEditing = False

            .Visible = True
            .Refresh()
        End With

    End Sub

    Private Sub btnSelect_Deselect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnSelectAll.Click
        Try
            If ts_btnSelectAll.Text = "&Select All" Then
                ts_btnSelectAll.Text = "Cl&ear All"
                ts_btnSelectAll.Image = Global.gloEMR.My.Resources.Clear_All1
                ts_btnSelectAll.ImageAlign = ContentAlignment.MiddleCenter
                SelectClearAll(True)
            Else
                ts_btnSelectAll.Text = "&Select All"
                ts_btnSelectAll.Image = Global.gloEMR.My.Resources.Select_All1
                ts_btnSelectAll.ImageAlign = ContentAlignment.MiddleCenter
                SelectClearAll(False)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SelectClearAll(Optional ByVal blnSelect As Boolean = True)
        With C1grdExams
            For i As Integer = 1 To .Rows.Count - 1
                .Rows(i)(0) = blnSelect
            Next
        End With

        If blnSelect = True Then
            ts_btnSelectAll.Text = "Cl&ear All"
        Else
            ts_btnSelectAll.Text = "&Select All"
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Try
            If Me.Cursor Is Cursors.Default Then
                'If pnlDSO.Visible = True Then
                '    Priview_Close()
                'Else
                Me.Close()
                'End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Close, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1grdExams_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If C1grdExams.Rows.Count > 1 Then
                'Try
                '    If (IsNothing(C1grdExams.ContextMenu) = False) Then
                '        C1grdExams.ContextMenu.Dispose()
                '        C1grdExams.ContextMenu = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                C1grdExams.ContextMenu = cmnuSelectClear
                cmnuSelectClear.MenuItems(0).Visible = True
                cmnuSelectClear.MenuItems(1).Visible = True
                cmnuSelectClear.MenuItems(2).Visible = True
                cmnuSelectClear.MenuItems(3).Visible = True
            Else
                'Try
                '    If (IsNothing(C1grdExams.ContextMenu) = False) Then
                '        C1grdExams.ContextMenu.Dispose()
                '        C1grdExams.ContextMenu = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                C1grdExams.ContextMenu = cmnuSelectClear
                cmnuSelectClear.MenuItems(0).Visible = False
                cmnuSelectClear.MenuItems(1).Visible = False
                cmnuSelectClear.MenuItems(2).Visible = False
                cmnuSelectClear.MenuItems(3).Visible = True
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmnuClearAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuClearAll.Click
        Try
            SelectClearAll(False)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub cmnuSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmnuSelectAll.Click
        Try
            SelectClearAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnUnlockExam.Click
        Try
            If IsNothing(trvFormList.SelectedNode) = True Then
                Exit Sub
            End If

            Select Case trvFormList.SelectedNode.Text
                Case "Patient Exam"

                    Dim ExamIDs As New Collection
                    Me.Cursor = Cursors.WaitCursor

                    '''' For Selected Patient
                    ''''' 0-Select, 1-ExamID,2-DOS, 3-ExamName, 4-VisitID, 5-DOB, 6-ProviderID, 7-ProviderName , 8-bIsFinished

                    '' For All Patients
                    '' 0-Select,1-ExamID,2-PatientName ,3-ExamName,4-DOS,5-VisitID,6-PatientID,7-PatientCode,8-DOB,9-ProviderID,10-ProviderName, 11-bIsFinished 

                    '' 
                    With C1grdExams
                        '' Add Selected Exams in one Collection
                        For i As Integer = 1 To .Rows.Count - 1
                            If .Rows(i)(0) = True Then
                                '' Add Selected ExamIDs & MachineName to Collection
                                ExamIDs.Add(.Rows(i)(COL_ExamID) & "|" & .Rows(i)(COL_MachineName))
                            End If
                        Next
                    End With

                    With ExamIDs
                        If .Count >= 1 Then
                            Dim oclsExam As New clsPatientExams
                            '' Unlock Exams One by One
                            For i As Integer = 1 To .Count
                                Dim objIDs() As String
                                objIDs = CStr(ExamIDs(i)).Split("|")
                                oclsExam.UnLock_Exam(objIDs(0), objIDs(1))
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Unlock Exam.", 0, objIDs(0), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            Next
                            oclsExam.Dispose()
                            oclsExam = Nothing
                            Call Fill_LockedExams()
                        End If
                    End With

                Case "Patient Demographics"
                    '20090824
                    'commented by Mayuri
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.PatientRegistration, C1grdExams.GetData(i, COL_PatientID), 0, Now)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Patient Demographics", C1grdExams.GetData(i, COL_PatientID), C1grdExams.GetData(i, COL_PatientID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next

                Case "Patient ROS"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.PatientROS, C1grdExams.GetData(i, COL_PatientID), C1grdExams.GetData(i, COL_VisitID), C1grdExams.GetData(i, COL_VisitDate))
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Patient ROS", C1grdExams.GetData(i, COL_PatientID), C1grdExams.GetData(i, COL_VisitID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next

                Case "Patient History"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.PatientHistory, C1grdExams.GetData(i, COL_PatientID), C1grdExams.GetData(i, COL_VisitID), C1grdExams.GetData(i, COL_VisitDate))
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Patient History", C1grdExams.GetData(i, COL_PatientID), C1grdExams.GetData(i, COL_VisitID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next

                Case "Medication"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.Medication, C1grdExams.GetData(i, COL_PatientID), C1grdExams.GetData(i, COL_VisitID), C1grdExams.GetData(i, COL_VisitDate))
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Medication, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Patient Medication", C1grdExams.GetData(i, COL_PatientID), C1grdExams.GetData(i, COL_VisitID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next
                Case "Rx-Meds"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.Prescription, C1grdExams.GetData(i, COL_PatientID), C1grdExams.GetData(i, COL_VisitID), C1grdExams.GetData(i, COL_PrescriptionDateTime))
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Patient Rx-Meds", C1grdExams.GetData(i, COL_PatientID), C1grdExams.GetData(i, COL_VisitID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next
                Case "Patient Vitals"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            ''Bug : 00000828: Record locking. Record is not getting unlockoed.
                            UnLock_Transaction(TrnType.PatientVitals, C1grdExams.GetData(i, COL_VitalID), 0, C1grdExams.GetData(i, COL_VitalDateTime))
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Vitals, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Patient Vitals", 0, C1grdExams.GetData(i, COL_VitalID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next
                Case "Orders"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.Radiology, C1grdExams.GetData(i, COL_PatientID), C1grdExams.GetData(i, COL_VisitID), C1grdExams.GetData(i, COL_OrderDateTime))
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Order Templates [Orders]", C1grdExams.GetData(i, COL_PatientID), C1grdExams.GetData(i, COL_VisitID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next
                Case "Labs"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.Labs, C1grdExams.GetData(i, COL_LabOrderID), 0, C1grdExams.GetData(i, COL_OrderDateTime))
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Patient Labs", 0, C1grdExams.GetData(i, COL_LabOrderID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next
                Case "Messages"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.Messages, C1grdExams.GetData(i, COL_MessageID), 0, C1grdExams.GetData(i, COL_MessageDateTime))
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Messages, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Patient Messages", 0, C1grdExams.GetData(i, COL_MessageID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next
                Case "Letters"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.Letters, C1grdExams.GetData(i, COL_LetterID), 0, C1grdExams.GetData(i, COL_LetterDateTime))
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Letters, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Patient Letters", 0, C1grdExams.GetData(i, COL_LetterID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next
                Case "PT Protocol"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.PTProtocol, C1grdExams.GetData(i, COL_PTProtocolID), 0, C1grdExams.GetData(i, COL_PTProtocolDateTime))
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "PT Protocol", 0, C1grdExams.GetData(i, COL_PTProtocolID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next
                Case "Patient Consent"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.PatientConsent, C1grdExams.GetData(i, COL_ConsentID), 0, C1grdExams.GetData(i, COL_ConsentDateTime))
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.PatientConsent, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Patient Consent", 0, C1grdExams.GetData(i, COL_ConsentID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next
                Case "Flowsheet"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.Flowsheet, C1grdExams.GetData(i, COL_FlowSheetRecordID), 0, Now)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Flowsheet", 0, C1grdExams.GetData(i, COL_FlowSheetRecordID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)

                        End If
                    Next

                Case "Task"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.Task, C1grdExams.GetData(i, COL_TaskID), 0, C1grdExams.GetData(i, COL_TaskDate))
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Task", 0, C1grdExams.GetData(i, COL_TaskID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next
                Case "Immunization"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.Immunization, C1grdExams.GetData(i, COL_ImmunizationID), 0, Now)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Immunization", 0, C1grdExams.GetData(i, COL_ImmunizationID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next
                Case "DMS"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.DMS, C1grdExams.GetData(i, COL_DocumentID), 0, Now)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "DMS", 0, C1grdExams.GetData(i, COL_DocumentID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next
                Case "Problem List"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.ProblemList, C1grdExams.GetData(i, COL_PatientID), 0, Now)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Problem List", C1grdExams.GetData(i, COL_PatientID), 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next
                Case "Disclosure Management"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.DisclosureManagement, C1grdExams.GetData(i, COL_DisclosureID), 0, C1grdExams.GetData(i, COL_DisclosureDateTime))
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Disclosure Management", 0, C1grdExams.GetData(i, COL_DisclosureID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next
                    ''Bug : 00000828: Record locking. New node added to unlock Nurse Notes.
                Case "Nurse Notes"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.NurseNotes, C1grdExams.GetData(i, COL_NurseNotesID), 0, C1grdExams.GetData(i, COL_NurseNotesDateTime))
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Nurse Notes", 0, C1grdExams.GetData(i, COL_NurseNotesID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next

                Case "Workers Comp Form"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.WorkersCompForm, C1grdExams.GetData(i, COL_WorkersCompFormID), 0, C1grdExams.GetData(i, COL_WorkersCompFormCreatedDate))
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Workers Comp Form", 0, C1grdExams.GetData(i, COL_WorkersCompFormID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next

                Case "OB Plan"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.PatientOBPlan, C1grdExams.GetData(i, COL_PatientID), C1grdExams.GetData(i, COL_VisitID), C1grdExams.GetData(i, COL_VisitDate))
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.NYWCForms, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "OB Plan", C1grdExams.GetData(i, COL_PatientID), C1grdExams.GetData(i, COL_VisitID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next
                Case "Implant Devices"
                    For i As Integer = C1grdExams.Rows.Count - 1 To i = 1 Step -1
                        If C1grdExams.GetCellCheck(i, COL_Select) = CheckEnum.Checked Then
                            UnLock_Transaction(TrnType.ImplantDevices, C1grdExams.GetData(i, COL_PatientID), C1grdExams.GetData(i, COL_VisitID), System.DateTime.Now)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.UnlockRecord, gloAuditTrail.ActivityType.Unlock, "Implant Devices", C1grdExams.GetData(i, COL_PatientID), C1grdExams.GetData(i, COL_VisitID), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            C1grdExams.Rows.Remove(i)
                        End If
                    Next
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmnuRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmnuRefresh.Click
        Try
            Call Fill_LockedExams()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub trvFormList_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvFormList.AfterSelect
        Try
            '''' If not click on treeview node
            If IsNothing(trvFormList.SelectedNode) = True Then
                Exit Sub
            End If

            ''''chandge the button name as per selection of treeview node
            If trvFormList.SelectedNode.Text <> "" Then
                Label1.Text = "Unlock " & trvFormList.SelectedNode.Text
                'btnUnlock.Text = "Unlock " & trvFormList.SelectedNode.Text
                'btnSelect_Deselect.Text = "&Select All"
            End If
            'C1grdExams.Clear()
            C1grdExams.DataSource = Nothing
            Select Case trvFormList.SelectedNode.Text
                Case "Patient Exam"
                    Call Fill_LockedExams()
                Case "Patient Demographics"
                    Call PatientRegistration()
                    ' btnUnlock.Text = "Unlock Patient Demographics"
                Case "Patient ROS"
                    Call PatientROS()
                Case "Patient History"
                    Call PatientHistory()
                Case "Medication"
                    Call Medication()
                Case "Rx-Meds"
                    Call Prescription()
                Case "Patient Vitals"
                    Call PatientVitals()
                    'Case "Radiology"
                Case "Orders"
                    Call Radiology()
                Case "Labs"
                    Call Labs()
                Case "Messages"
                    Call Messages()
                Case "Letters"
                    Call Letters()
                Case "PT Protocol"
                    Call PTProtocol()
                Case "Patient Consent"
                    Call PatientConsent()

                Case "Flowsheet"
                    Call Flowsheet()
                Case "Task"
                    Call Task()
                Case "Immunization"
                    Call Immunization()
                Case "DMS"
                    Call DMS()
                Case "Problem List"
                    Call ProblemList()
                Case "Disclosure Management"
                    Call DisclosureManagement()
                    ''Bug : 00000828: Record locking. New node added to unlock Nurse Notes.
                Case "Nurse Notes"
                    Call NurseNotes()
                Case "Workers Comp Form"
                    WorkersCompForm()
                Case "OB Plan"
                    Call PatientOBPlan()
                Case "Implant Devices"
                    Call ImplantDevices()
            End Select

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub PatientRegistration()
        Call setGrid_PaitentReg()
        Dim objPatientReg As New ClsPatientRegistrationDBLayer
        Dim dt As DataTable = objPatientReg.Fill_LockPatientRegistration(gstrClientMachineName, TrnType.PatientRegistration)

        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(i + 1, COL_UserName, dt.Rows(i)("sUserName"))
                    .SetData(i + 1, COL_PatientID, dt.Rows(i)("nPatientID"))
                End With
            Next
            dt.Dispose()
            dt = Nothing
        End If
        objPatientReg.Dispose()
        objPatientReg = Nothing
    End Sub

    Public Sub setGrid_PaitentReg()
        With C1grdExams
            .Visible = True

            .Cols.Count = Col_Count

            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .AllowEditing = True
            .Width = .Width - 20



            .Cols(COL_Select).Width = .Width * 0.08
            .Cols(COL_Select).AllowEditing = True
            .Cols(COL_Select).DataType = System.Type.GetType("System.Boolean")
            .Cols(COL_Select).Name = "Select"
            .SetData(0, COL_Select, "Select")
            .Cols(COL_Select).TextAlignFixed = TextAlignEnum.CenterCenter
            .Cols(COL_ExamID).Width = 0
            '''''''''

            .Cols(COL_PatientCode).Width = .Width * 0.1
            .Cols(COL_PatientCode).AllowEditing = False
            .Cols(COL_PatientCode).Name = "Patient Code"
            .SetData(0, COL_PatientCode, "Patient Code")
            .Cols(COL_PatientCode).TextAlign = TextAlignEnum.LeftCenter
            '''''''''
            '''''''''
            .Cols(COL_PatientName).Width = .Width * 0.15
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_PatientName).Name = "Patient Name"
            .SetData(0, COL_PatientName, "Patient Name")
            .Cols(COL_PatientName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''
            .Cols(COL_ExamName).Width = 0

            ''''''''''
            .Cols(COL_UserName).Width = .Width * 0.15
            .Cols(COL_UserName).AllowEditing = False
            .Cols(COL_UserName).Name = "User Name"
            .SetData(0, COL_UserName, "User Name")
            .Cols(COL_UserName).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''
            .Cols(COL_MachineName).Width = 0
            .Cols(COL_dtDOS).Width = 0
            .Cols(COL_IsFinished).Width = 0

            ''''''''''''''
            .Cols(COL_PatientID).Width = .Width * 0
            .Cols(COL_PatientID).AllowEditing = False
            .Cols(COL_PatientID).Name = "PatientID"
            .SetData(0, COL_PatientID, "PatientID")
            .Cols(COL_PatientID).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''''''

            .Cols(COL_VisitID).Width = 0
            .Cols(COL_VisitDate).Width = 0
            .Cols(COL_PrescriptionDateTime).Width = 0
            .Cols(COL_VitalID).Width = 0
            .Cols(COL_VitalDateTime).Width = 0
            .Cols(COL_LabOrderID).Width = 0
            .Cols(COL_OrderDateTime).Width = 0
            .Cols(COL_OrderName).Width = 0
            .Cols(COL_MessageID).Width = 0
            .Cols(COL_MessageDateTime).Width = 0
            .Cols(COL_MessageName).Width = 0
            .Cols(COL_LetterID).Width = 0
            .Cols(COL_LetterDateTime).Width = 0
            .Cols(COL_LetterName).Width = 0
            .Cols(COL_PTProtocolID).Width = 0
            .Cols(COL_PTProtocolDateTime).Width = 0
            .Cols(COL_PTProtocolName).Width = 0
            .Cols(COL_ConsentID).Width = 0
            .Cols(COL_ConsentDateTime).Width = 0
            .Cols(COL_ConsentName).Width = 0
            .Cols(COL_FlowSheetRecordID).Width = 0
            .Cols(COL_FlowSheetName).Width = 0
            .Cols(COL_TaskID).Width = 0
            .Cols(COL_TaskDate).Width = 0
            .Cols(COL_Subject).Width = 0
            .Cols(COL_ImmunizationID).Width = 0
            .Cols(COL_DocumentID).Width = 0
            .Cols(COL_DocumetnName).Width = 0

            .Cols(COL_DisclosureID).Width = .Width * 0
            .Cols(COL_DisclosureName).Width = .Width * 0
            .Cols(COL_DisclosureDateTime).Width = .Width * 0

            .Cols(COL_NurseNotesID).Width = .Width * 0
            .Cols(COL_NurseNotesName).Width = .Width * 0
            .Cols(COL_NurseNotesDateTime).Width = .Width * 0

            .Cols(COL_WorkersCompFormID).Width = .Width * 0
            .Cols(COL_WorkersCompFormDateOfInjury).Width = .Width * 0
            .Cols(COL_WorkersCompFormType).Width = .Width * 0
            .Cols(COL_WorkersCompFormClaimNo).Width = .Width * 0
            .Cols(COL_WorkersCompFormDOS).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedBy).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedDate).Width = .Width * 0
        End With
    End Sub

    Public Sub PatientROS()
        Call SetGridPatientROS()
        Dim objPatientROS As New clsPatientROS
        Dim dt As DataTable = objPatientROS.Fill_LockPatientROS(gstrClientMachineName, TrnType.PatientROS)

        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(i + 1, COL_VisitDate, dt.Rows(i)("dtVisitDate"))
                    .SetData(i + 1, COL_UserName, dt.Rows(i)("sUserName"))
                    .SetData(i + 1, COL_PatientID, dt.Rows(i)("nPatientID"))
                    .SetData(i + 1, COL_VisitID, dt.Rows(i)("nVisitID"))
                End With
            Next
            dt.Dispose()
            dt = Nothing
        End If
        objPatientROS.Dispose()
        objPatientROS = Nothing
    End Sub

    Public Sub SetGridPatientROS()
        With C1grdExams

            '.Visible = True
            .Cols.Count = Col_Count

            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .AllowEditing = True
            .Width = .Width - 20



            ''''''''''''''''
            .Cols(COL_Select).Width = .Width * 0.08
            .Cols(COL_Select).AllowEditing = True
            .Cols(COL_Select).DataType = System.Type.GetType("System.Boolean")
            .Cols(COL_Select).Name = "Select"
            .SetData(0, COL_Select, "Select")
            .Cols(COL_Select).TextAlignFixed = TextAlignEnum.CenterCenter
            '''''''''''

            .Cols(COL_ExamID).Width = 0
            '''''''''

            .Cols(COL_PatientCode).Width = .Width * 0.1
            .Cols(COL_PatientCode).AllowEditing = False
            .Cols(COL_PatientCode).Name = "Patient Code"
            .SetData(0, COL_PatientCode, "Patient Code")
            .Cols(COL_PatientCode).TextAlign = TextAlignEnum.LeftCenter
            '''''''''
            '''''''''
            .Cols(COL_PatientName).Width = .Width * 0.15
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_PatientName).Name = "Patient Name"
            .SetData(0, COL_PatientName, "Patient Name")
            .Cols(COL_PatientName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''
            .Cols(COL_ExamName).Width = 0

            ''''''''''
            .Cols(COL_UserName).Width = .Width * 0.15
            .Cols(COL_UserName).AllowEditing = False
            .Cols(COL_UserName).Name = "User Name"
            .SetData(0, COL_UserName, "User Name")
            .Cols(COL_UserName).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''
            .Cols(COL_MachineName).Width = 0
            .Cols(COL_dtDOS).Width = 0
            .Cols(COL_IsFinished).Width = 0

            ''''''''''''''
            .Cols(COL_PatientID).Width = .Width * 0
            .Cols(COL_PatientID).AllowEditing = False
            .Cols(COL_PatientID).Name = "PatientID"
            .SetData(0, COL_PatientID, "PatientID")
            .Cols(COL_PatientID).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''''''

            ''''''''''''''''
            .Cols(COL_VisitID).Width = .Width * 0
            .Cols(COL_VisitID).AllowEditing = False
            .Cols(COL_VisitID).Name = "VisitID"
            .SetData(0, COL_VisitID, "VisitID")
            .Cols(COL_VisitID).TextAlign = TextAlignEnum.LeftCenter

            ''''''''''''

            ''''''''''''''
            .Cols(COL_VisitDate).Width = .Width * 0.2
            .Cols(COL_VisitDate).AllowEditing = True
            .Cols(COL_VisitDate).Name = "Visit Date"
            .SetData(0, COL_VisitDate, "Visit Date")
            .Cols(COL_VisitDate).TextAlign = TextAlignEnum.CenterCenter

            ''''''''''''''



            .Cols(COL_PrescriptionDateTime).Width = 0
            .Cols(COL_VitalID).Width = 0
            .Cols(COL_VitalDateTime).Width = 0
            .Cols(COL_LabOrderID).Width = 0
            .Cols(COL_OrderDateTime).Width = 0
            .Cols(COL_OrderName).Width = 0
            .Cols(COL_MessageID).Width = 0
            .Cols(COL_MessageDateTime).Width = 0
            .Cols(COL_MessageName).Width = 0
            .Cols(COL_LetterID).Width = 0
            .Cols(COL_LetterDateTime).Width = 0
            .Cols(COL_LetterName).Width = 0
            .Cols(COL_PTProtocolID).Width = 0
            .Cols(COL_PTProtocolDateTime).Width = 0
            .Cols(COL_PTProtocolName).Width = 0
            .Cols(COL_ConsentID).Width = 0
            .Cols(COL_ConsentDateTime).Width = 0
            .Cols(COL_ConsentName).Width = 0
            .Cols(COL_FlowSheetRecordID).Width = 0
            .Cols(COL_FlowSheetName).Width = 0
            .Cols(COL_TaskID).Width = 0
            .Cols(COL_TaskDate).Width = 0
            .Cols(COL_Subject).Width = 0
            .Cols(COL_ImmunizationID).Width = 0
            .Cols(COL_DocumentID).Width = 0
            .Cols(COL_DocumetnName).Width = 0

            .Cols(COL_DisclosureID).Width = .Width * 0
            .Cols(COL_DisclosureName).Width = .Width * 0
            .Cols(COL_DisclosureDateTime).Width = .Width * 0

            .Cols(COL_NurseNotesID).Width = .Width * 0
            .Cols(COL_NurseNotesName).Width = .Width * 0
            .Cols(COL_NurseNotesDateTime).Width = .Width * 0

            .Cols(COL_WorkersCompFormID).Width = .Width * 0
            .Cols(COL_WorkersCompFormDateOfInjury).Width = .Width * 0
            .Cols(COL_WorkersCompFormType).Width = .Width * 0
            .Cols(COL_WorkersCompFormClaimNo).Width = .Width * 0
            .Cols(COL_WorkersCompFormDOS).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedBy).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedDate).Width = .Width * 0
        End With
    End Sub

    Public Sub PatientOBPlan()

        Dim objOBPlan As New clsPatientOBPlan
        Dim _dt As DataTable = Nothing
        Try
            Call SetGridPatientROS()

            _dt = objOBPlan.Fill_LockPatientOBPlan(gstrClientMachineName, TrnType.PatientOBPlan)
            If Not IsNothing(_dt) Then
                C1grdExams.Rows.Count = 1
                For i As Integer = 0 To _dt.Rows.Count - 1
                    With C1grdExams
                        .Rows.Add()
                        .SetData(i + 1, COL_PatientCode, _dt.Rows(i)("sPatientCode"))
                        .SetData(i + 1, COL_PatientName, _dt.Rows(i)("PatientName"))
                        .SetData(i + 1, COL_VisitDate, _dt.Rows(i)("dtVisitDate"))
                        .SetData(i + 1, COL_UserName, _dt.Rows(i)("sUserName"))
                        .SetData(i + 1, COL_PatientID, _dt.Rows(i)("nPatientID"))
                        .SetData(i + 1, COL_VisitID, _dt.Rows(i)("nVisitID"))
                    End With

                Next
            End If
        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ex = Nothing
        Finally

            If Not IsNothing(_dt) Then
                _dt.Dispose() : _dt = Nothing
            End If

            If Not IsNothing(objOBPlan) Then
                objOBPlan.Dispose() : objOBPlan = Nothing
            End If

        End Try


    End Sub

    Public Sub PatientHistory()
        Call SetGridPatientROS()
        Dim objHistory As New clsPatientHistory
        Dim dt As DataTable = objHistory.Fill_LockPatientHistory(gstrClientMachineName, TrnType.PatientHistory)

        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(i + 1, COL_VisitDate, dt.Rows(i)("dtVisitDate"))
                    .SetData(i + 1, COL_UserName, dt.Rows(i)("sUserName"))
                    .SetData(i + 1, COL_PatientID, dt.Rows(i)("nPatientID"))
                    .SetData(i + 1, COL_VisitID, dt.Rows(i)("nVisitID"))
                End With

            Next
            dt.Dispose()
            dt = Nothing
        End If
        objHistory.Dispose()
        objHistory = Nothing
    End Sub

    Public Sub Medication()
        Call SetGridPatientROS()
        Dim objMedication As New clsMedicationDBLayer
        Dim dt As DataTable = objMedication.Fill_LockMedication(gstrClientMachineName, TrnType.Medication)

        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(i + 1, COL_VisitDate, dt.Rows(i)("dtVisitDate"))
                    .SetData(i + 1, COL_UserName, dt.Rows(i)("sUserName1"))
                    .SetData(i + 1, COL_PatientID, dt.Rows(i)("nPatientID"))
                    .SetData(i + 1, COL_VisitID, dt.Rows(i)("nVisitID"))
                End With
            Next
            dt.Dispose()
            dt = Nothing

        End If
        ' objMedication.Dispose()
        objMedication = Nothing
    End Sub

    Public Sub ImplantDevices()
        Call setGridImplantDevices()
        '_RxBusinessLayer = New RxBusinesslayer(_PatientID)
        ClsImplantDeviceDBLayer = New ClsImplantDeviceDBLayer()
        Dim dt As DataTable = ClsImplantDeviceDBLayer.Fill_LockImplantDevices(gstrClientMachineName, TrnType.ImplantDevices)

        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_VisitID, dt.Rows(i)("nDevicelist_Id"))
                    .SetData(i + 1, COL_PatientID, dt.Rows(i)("nPatientID"))
                    .SetData(i + 1, COL_sDeviceId, dt.Rows(i)("sDeviceId"))
                    .SetData(i + 1, COL_dtImplantDate, dt.Rows(i)("dtImplant_Date"))
                    .SetData(i + 1, COL_BrandName, dt.Rows(i)("sBrandName"))
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                End With
            Next
            dt.Dispose()
            dt = Nothing
        End If
        ClsImplantDeviceDBLayer.Dispose()
        ClsImplantDeviceDBLayer = Nothing
    End Sub

    

    Public Sub Prescription()
        Call setGridPrescription()
        _RxBusinessLayer = New RxBusinesslayer(_PatientID)
        Dim dt As DataTable = _RxBusinessLayer.Fill_LockPrescription(gstrClientMachineName, TrnType.Prescription)

        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(i + 1, COL_UserName, dt.Rows(i)("sUserName1"))
                    .SetData(i + 1, COL_PatientID, dt.Rows(i)("nPatientID"))
                    .SetData(i + 1, COL_VisitID, dt.Rows(i)("nVisitID"))
                    .SetData(i + 1, COL_PrescriptionDateTime, dt.Rows(i)("dtPrescriptionDate"))
                End With

            Next
            dt.Dispose()
            dt = Nothing
        End If
        _RxBusinessLayer.Dispose()
        _RxBusinessLayer = Nothing
    End Sub

    Public Sub setGridImplantDevices()
        With C1grdExams
            .Visible = True

            .Cols.Count = Col_Count

            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .AllowEditing = True
            .Width = .Width - 20

            .Cols(COL_Select).Width = .Width * 0.08
            .Cols(COL_Select).AllowEditing = True
            .Cols(COL_Select).DataType = System.Type.GetType("System.Boolean")
            .Cols(COL_Select).Name = "Select"
            .SetData(0, COL_Select, "Select")
            .Cols(COL_Select).TextAlignFixed = TextAlignEnum.CenterCenter


            .Cols(COL_PatientID).Width = .Width * 0
            .Cols(COL_PatientID).AllowEditing = False
            .Cols(COL_PatientID).Name = "PatientID"
            .SetData(0, COL_PatientID, "PatientID")
            .Cols(COL_PatientID).TextAlign = TextAlignEnum.LeftCenter

            .Cols(COL_sDeviceId).Width = .Width * 0.2
            .Cols(COL_sDeviceId).AllowEditing = False
            .Cols(COL_sDeviceId).Name = "DeviceId"
            .SetData(0, COL_sDeviceId, "DeviceId")
            .Cols(COL_sDeviceId).TextAlign = TextAlignEnum.LeftCenter

            .Cols(COL_dtImplantDate).Width = .Width * 0.1
            .Cols(COL_dtImplantDate).AllowEditing = False
            .Cols(COL_dtImplantDate).Name = "Implant Date"
            .SetData(0, COL_dtImplantDate, "Implant Date")
            .Cols(COL_dtImplantDate).TextAlign = TextAlignEnum.LeftCenter

            .Cols(COL_BrandName).Width = .Width * 0.1
            .Cols(COL_BrandName).AllowEditing = False
            .Cols(COL_BrandName).Name = "Brand Name"
            .SetData(0, COL_BrandName, "Brand Name")
            .Cols(COL_BrandName).TextAlign = TextAlignEnum.LeftCenter

            .Cols(COL_PatientCode).Width = .Width * 0.1
            .Cols(COL_PatientCode).AllowEditing = False
            .Cols(COL_PatientCode).Name = "Patient Code"
            .SetData(0, COL_PatientCode, "Patient Code")
            .Cols(COL_PatientCode).TextAlign = TextAlignEnum.LeftCenter
            '''''''''
            '''''''''
            .Cols(COL_PatientName).Width = .Width * 0.15
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_PatientName).Name = "Patient Name"
            .SetData(0, COL_PatientName, "Patient Name")
            .Cols(COL_PatientName).TextAlign = TextAlignEnum.LeftCenter


            '''''''''''
            .Cols(COL_ExamName).Width = 0
            .Cols(COL_MachineName).Width = 0
            .Cols(COL_UserName).Width = 0
            .Cols(COL_dtDOS).Width = 0
            .Cols(COL_IsFinished).Width = 0
            .Cols(COL_PrescriptionDateTime).Width = 0
            .Cols(COL_VisitDate).Width = .Width * 0
            .Cols(COL_VitalID).Width = 0
            .Cols(COL_VitalDateTime).Width = 0
            .Cols(COL_LabOrderID).Width = 0
            .Cols(COL_OrderDateTime).Width = 0
            .Cols(COL_OrderName).Width = 0
            .Cols(COL_MessageID).Width = 0
            .Cols(COL_MessageDateTime).Width = 0
            .Cols(COL_MessageName).Width = 0
            .Cols(COL_LetterID).Width = 0
            .Cols(COL_LetterDateTime).Width = 0
            .Cols(COL_LetterName).Width = 0
            .Cols(COL_PTProtocolID).Width = 0
            .Cols(COL_PTProtocolDateTime).Width = 0
            .Cols(COL_PTProtocolName).Width = 0
            .Cols(COL_ConsentID).Width = 0
            .Cols(COL_ConsentDateTime).Width = 0
            .Cols(COL_ConsentName).Width = 0
            .Cols(COL_FlowSheetRecordID).Width = 0
            .Cols(COL_FlowSheetName).Width = 0
            .Cols(COL_TaskID).Width = 0
            .Cols(COL_TaskDate).Width = 0
            .Cols(COL_Subject).Width = 0
            .Cols(COL_ImmunizationID).Width = 0
            .Cols(COL_DocumentID).Width = 0
            .Cols(COL_DocumetnName).Width = 0

            .Cols(COL_DisclosureID).Width = .Width * 0
            .Cols(COL_DisclosureName).Width = .Width * 0
            .Cols(COL_DisclosureDateTime).Width = .Width * 0

            .Cols(COL_NurseNotesID).Width = .Width * 0
            .Cols(COL_NurseNotesName).Width = .Width * 0
            .Cols(COL_NurseNotesDateTime).Width = .Width * 0

            .Cols(COL_WorkersCompFormID).Width = .Width * 0
            .Cols(COL_WorkersCompFormDateOfInjury).Width = .Width * 0
            .Cols(COL_WorkersCompFormType).Width = .Width * 0
            .Cols(COL_WorkersCompFormClaimNo).Width = .Width * 0
            .Cols(COL_WorkersCompFormDOS).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedBy).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedDate).Width = .Width * 0
        End With
    End Sub

    Public Sub setGridPrescription()
        With C1grdExams
            .Visible = True

            .Cols.Count = Col_Count

            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .AllowEditing = True
            .Width = .Width - 20



            .Cols(COL_Select).Width = .Width * 0.08
            .Cols(COL_Select).AllowEditing = True
            .Cols(COL_Select).DataType = System.Type.GetType("System.Boolean")
            .Cols(COL_Select).Name = "Select"
            .SetData(0, COL_Select, "Select")
            .Cols(COL_Select).TextAlignFixed = TextAlignEnum.CenterCenter
            .Cols(COL_ExamID).Width = 0
            '''''''''

            .Cols(COL_PatientCode).Width = .Width * 0.1
            .Cols(COL_PatientCode).AllowEditing = False
            .Cols(COL_PatientCode).Name = "Patient Code"
            .SetData(0, COL_PatientCode, "Patient Code")
            .Cols(COL_PatientCode).TextAlign = TextAlignEnum.LeftCenter
            '''''''''
            '''''''''
            .Cols(COL_PatientName).Width = .Width * 0.15
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_PatientName).Name = "Patient Name"
            .SetData(0, COL_PatientName, "Patient Name")
            .Cols(COL_PatientName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''
            .Cols(COL_ExamName).Width = 0

            ''''''''''
            .Cols(COL_UserName).Width = .Width * 0.15
            .Cols(COL_UserName).AllowEditing = False
            .Cols(COL_UserName).Name = "User Name"
            .SetData(0, COL_UserName, "User Name")
            .Cols(COL_UserName).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''
            .Cols(COL_MachineName).Width = 0
            .Cols(COL_dtDOS).Width = 0
            .Cols(COL_IsFinished).Width = 0

            ''''''''''''''
            .Cols(COL_PatientID).Width = .Width * 0
            .Cols(COL_PatientID).AllowEditing = False
            .Cols(COL_PatientID).Name = "PatientID"
            .SetData(0, COL_PatientID, "PatientID")
            .Cols(COL_PatientID).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''''''

            ''''''''''''''''
            .Cols(COL_VisitID).Width = .Width * 0
            .Cols(COL_VisitID).AllowEditing = False
            .Cols(COL_VisitID).Name = "VisitID"
            .SetData(0, COL_VisitID, "VisitID")
            .Cols(COL_VisitID).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''''
            .Cols(COL_VisitDate).Width = .Width * 0

            ''''''''''''''
            .Cols(COL_PrescriptionDateTime).Width = .Width * 0.2
            .Cols(COL_PrescriptionDateTime).AllowEditing = False
            .Cols(COL_PrescriptionDateTime).Name = "DateTime"
            .SetData(0, COL_PrescriptionDateTime, "DateTime")
            .Cols(COL_PrescriptionDateTime).TextAlign = TextAlignEnum.LeftCenter
            .Cols(COL_PrescriptionDateTime).Width = 0
            '''''''''''''


            .Cols(COL_VitalID).Width = 0
            .Cols(COL_VitalDateTime).Width = 0
            .Cols(COL_LabOrderID).Width = 0
            .Cols(COL_OrderDateTime).Width = 0
            .Cols(COL_OrderName).Width = 0
            .Cols(COL_MessageID).Width = 0
            .Cols(COL_MessageDateTime).Width = 0
            .Cols(COL_MessageName).Width = 0
            .Cols(COL_LetterID).Width = 0
            .Cols(COL_LetterDateTime).Width = 0
            .Cols(COL_LetterName).Width = 0
            .Cols(COL_PTProtocolID).Width = 0
            .Cols(COL_PTProtocolDateTime).Width = 0
            .Cols(COL_PTProtocolName).Width = 0
            .Cols(COL_ConsentID).Width = 0
            .Cols(COL_ConsentDateTime).Width = 0
            .Cols(COL_ConsentName).Width = 0
            .Cols(COL_FlowSheetRecordID).Width = 0
            .Cols(COL_FlowSheetName).Width = 0
            .Cols(COL_TaskID).Width = 0
            .Cols(COL_TaskDate).Width = 0
            .Cols(COL_Subject).Width = 0
            .Cols(COL_ImmunizationID).Width = 0
            .Cols(COL_DocumentID).Width = 0
            .Cols(COL_DocumetnName).Width = 0

            .Cols(COL_DisclosureID).Width = .Width * 0
            .Cols(COL_DisclosureName).Width = .Width * 0
            .Cols(COL_DisclosureDateTime).Width = .Width * 0

            .Cols(COL_NurseNotesID).Width = .Width * 0
            .Cols(COL_NurseNotesName).Width = .Width * 0
            .Cols(COL_NurseNotesDateTime).Width = .Width * 0

            .Cols(COL_WorkersCompFormID).Width = .Width * 0
            .Cols(COL_WorkersCompFormDateOfInjury).Width = .Width * 0
            .Cols(COL_WorkersCompFormType).Width = .Width * 0
            .Cols(COL_WorkersCompFormClaimNo).Width = .Width * 0
            .Cols(COL_WorkersCompFormDOS).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedBy).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedDate).Width = .Width * 0
        End With
    End Sub

    Public Sub PatientVitals()
        Call setGridVitals()
        Dim objVitals As New clsPatientVitals
        Dim dt As DataTable = objVitals.Fill_LockPatientVitals(gstrClientMachineName, TrnType.PatientVitals)

        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1

                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_VitalID, dt.Rows(i)("nVitalID"))
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(i + 1, COL_UserName, dt.Rows(i)("sUserName"))
                    .SetData(i + 1, COL_VitalDateTime, dt.Rows(i)("dtVitalDate"))
                End With
            Next
            dt.Dispose()
            dt = Nothing
        End If
        objVitals.Dispose()
        objVitals = Nothing
    End Sub

    Public Sub setGridVitals()
        With C1grdExams
            .Visible = True

            .Cols.Count = Col_Count

            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .AllowEditing = True
            .Width = .Width - 20

            .Cols(COL_Select).Width = .Width * 0.08
            .Cols(COL_Select).AllowEditing = True
            .Cols(COL_Select).DataType = System.Type.GetType("System.Boolean")
            .Cols(COL_Select).Name = "Select"
            .SetData(0, COL_Select, "Select")
            .Cols(COL_Select).TextAlignFixed = TextAlignEnum.CenterCenter
            .Cols(COL_ExamID).Width = 0

            '''''''''
            .Cols(COL_PatientCode).Width = .Width * 0.1
            .Cols(COL_PatientCode).AllowEditing = False
            .Cols(COL_PatientCode).Name = "Patient Code"
            .SetData(0, COL_PatientCode, "Patient Code")
            .Cols(COL_PatientCode).TextAlign = TextAlignEnum.LeftCenter
            '''''''''

            '''''''''
            .Cols(COL_PatientName).Width = .Width * 0.15
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_PatientName).Name = "Patient Name"
            .SetData(0, COL_PatientName, "Patient Name")
            .Cols(COL_PatientName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''

            .Cols(COL_ExamName).Width = 0


            ''''''''''
            .Cols(COL_UserName).Width = .Width * 0.15
            .Cols(COL_UserName).AllowEditing = False
            .Cols(COL_UserName).Name = "User Name"
            .SetData(0, COL_UserName, "User Name")
            .Cols(COL_UserName).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''

            .Cols(COL_MachineName).Width = 0
            .Cols(COL_dtDOS).Width = 0
            .Cols(COL_IsFinished).Width = 0
            .Cols(COL_PatientID).Width = 0
            .Cols(COL_VisitID).Width = 0
            .Cols(COL_VisitDate).Width = 0
            .Cols(COL_PrescriptionDateTime).Width = 0

            ''''''''''' 
            .Cols(COL_VitalID).Width = .Width * 0.15
            .Cols(COL_VitalID).AllowEditing = False
            .Cols(COL_VitalID).Name = "VitalID"
            .SetData(0, COL_VitalID, "VitalID")
            .Cols(COL_VitalID).TextAlign = TextAlignEnum.CenterCenter
            ''''''''''   

            ''''''''
            .Cols(COL_VitalDateTime).Width = .Width * 0.2
            .Cols(COL_VitalDateTime).AllowEditing = False
            .Cols(COL_VitalDateTime).Name = "Vital DateTime"
            .SetData(0, COL_VitalDateTime, "Vital DateTime")
            .Cols(COL_VitalDateTime).TextAlign = TextAlignEnum.CenterCenter
            .Cols(COL_VitalDateTime).Width = 0
            ''''''''

            .Cols(COL_LabOrderID).Width = 0
            .Cols(COL_OrderDateTime).Width = 0
            .Cols(COL_OrderName).Width = 0
            .Cols(COL_MessageID).Width = 0
            .Cols(COL_MessageDateTime).Width = 0
            .Cols(COL_MessageName).Width = 0
            .Cols(COL_LetterID).Width = 0
            .Cols(COL_LetterDateTime).Width = 0
            .Cols(COL_LetterName).Width = 0
            .Cols(COL_PTProtocolID).Width = 0
            .Cols(COL_PTProtocolDateTime).Width = 0
            .Cols(COL_PTProtocolName).Width = 0
            .Cols(COL_ConsentID).Width = 0
            .Cols(COL_ConsentDateTime).Width = 0
            .Cols(COL_ConsentName).Width = 0
            .Cols(COL_FlowSheetRecordID).Width = 0
            .Cols(COL_FlowSheetName).Width = 0
            .Cols(COL_TaskID).Width = 0
            .Cols(COL_TaskDate).Width = 0
            .Cols(COL_Subject).Width = 0
            .Cols(COL_ImmunizationID).Width = 0
            .Cols(COL_DocumentID).Width = 0
            .Cols(COL_DocumetnName).Width = 0

            .Cols(COL_DisclosureID).Width = .Width * 0
            .Cols(COL_DisclosureName).Width = .Width * 0
            .Cols(COL_DisclosureDateTime).Width = .Width * 0

            .Cols(COL_NurseNotesID).Width = .Width * 0
            .Cols(COL_NurseNotesName).Width = .Width * 0
            .Cols(COL_NurseNotesDateTime).Width = .Width * 0

            .Cols(COL_WorkersCompFormID).Width = .Width * 0
            .Cols(COL_WorkersCompFormDateOfInjury).Width = .Width * 0
            .Cols(COL_WorkersCompFormType).Width = .Width * 0
            .Cols(COL_WorkersCompFormClaimNo).Width = .Width * 0
            .Cols(COL_WorkersCompFormDOS).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedBy).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedDate).Width = .Width * 0
        End With
    End Sub

    Public Sub Radiology()
        Call setGridRadiology()
        Dim objRadiology As New clsRadiology
        Dim dt As DataTable = objRadiology.Fill_LockRadiology(gstrClientMachineName, TrnType.Radiology)

        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_VisitID, dt.Rows(i)("lm_Visit_ID"))
                    .SetData(i + 1, COL_PatientID, dt.Rows(i)("lm_Patient_ID"))
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(i + 1, COL_UserName, dt.Rows(i)("lm_sUserName"))
                    .SetData(i + 1, COL_OrderDateTime, dt.Rows(i)("lm_OrderDate"))
                End With
            Next
            dt.Dispose()
            dt = Nothing
        End If
        objRadiology.Dispose()
        objRadiology = Nothing
    End Sub

    Public Sub setGridRadiology()
        With C1grdExams
            .Visible = True

            .Cols.Count = Col_Count

            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .AllowEditing = True
            .Width = .Width - 20

            .Cols(COL_Select).Width = .Width * 0.08
            .Cols(COL_Select).AllowEditing = True
            .Cols(COL_Select).DataType = System.Type.GetType("System.Boolean")
            .Cols(COL_Select).Name = "Select"
            .SetData(0, COL_Select, "Select")
            .Cols(COL_Select).TextAlignFixed = TextAlignEnum.CenterCenter
            .Cols(COL_ExamID).Width = 0

            '''''''''
            .Cols(COL_PatientCode).Width = .Width * 0.1
            .Cols(COL_PatientCode).AllowEditing = False
            .Cols(COL_PatientCode).Name = "Patient Code"
            .SetData(0, COL_PatientCode, "Patient Code")
            .Cols(COL_PatientCode).TextAlign = TextAlignEnum.LeftCenter
            '''''''''

            '''''''''
            .Cols(COL_PatientName).Width = .Width * 0.15
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_PatientName).Name = "Patient Name"
            .SetData(0, COL_PatientName, "Patient Name")
            .Cols(COL_PatientName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''

            .Cols(COL_ExamName).Width = 0


            ''''''''''
            .Cols(COL_UserName).Width = .Width * 0.15
            .Cols(COL_UserName).AllowEditing = False
            .Cols(COL_UserName).Name = "User Name"
            .SetData(0, COL_UserName, "User Name")
            .Cols(COL_UserName).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''

            .Cols(COL_MachineName).Width = 0
            .Cols(COL_dtDOS).Width = 0
            .Cols(COL_IsFinished).Width = 0

            ''''''''''''''
            .Cols(COL_PatientID).Width = .Width * 0
            .Cols(COL_PatientID).AllowEditing = False
            .Cols(COL_PatientID).Name = "PatientID"
            .SetData(0, COL_PatientID, "PatientID")
            .Cols(COL_PatientID).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''''''

            ''''''''''''''''
            .Cols(COL_VisitID).Width = .Width * 0
            .Cols(COL_VisitID).AllowEditing = False
            .Cols(COL_VisitID).Name = "VisitID"
            .SetData(0, COL_VisitID, "VisitID")
            .Cols(COL_VisitID).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''''

            .Cols(COL_VisitDate).Width = 0
            .Cols(COL_PrescriptionDateTime).Width = 0
            .Cols(COL_VitalID).Width = 0
            .Cols(COL_VitalDateTime).Width = 0

            .Cols(COL_LabOrderID).Width = 0

            '''''''''''
            .Cols(COL_OrderDateTime).Width = .Width * 0.2
            .Cols(COL_OrderDateTime).AllowEditing = False
            .Cols(COL_OrderDateTime).Name = "Order DateTime"
            .SetData(0, COL_OrderDateTime, "Order DateTime")
            .Cols(COL_OrderDateTime).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''

            .Cols(COL_OrderName).Width = 0
            .Cols(COL_MessageID).Width = 0
            .Cols(COL_MessageDateTime).Width = 0
            .Cols(COL_MessageName).Width = 0
            .Cols(COL_LetterID).Width = 0
            .Cols(COL_LetterDateTime).Width = 0
            .Cols(COL_LetterName).Width = 0
            .Cols(COL_PTProtocolID).Width = 0
            .Cols(COL_PTProtocolDateTime).Width = 0
            .Cols(COL_PTProtocolName).Width = 0
            .Cols(COL_ConsentID).Width = 0
            .Cols(COL_ConsentDateTime).Width = 0
            .Cols(COL_ConsentName).Width = 0
            .Cols(COL_FlowSheetRecordID).Width = 0
            .Cols(COL_FlowSheetName).Width = 0
            .Cols(COL_TaskID).Width = 0
            .Cols(COL_TaskDate).Width = 0
            .Cols(COL_Subject).Width = 0
            .Cols(COL_ImmunizationID).Width = 0
            .Cols(COL_DocumentID).Width = 0
            .Cols(COL_DocumetnName).Width = 0

            .Cols(COL_DisclosureID).Width = .Width * 0
            .Cols(COL_DisclosureName).Width = .Width * 0
            .Cols(COL_DisclosureDateTime).Width = .Width * 0

            .Cols(COL_NurseNotesID).Width = .Width * 0
            .Cols(COL_NurseNotesName).Width = .Width * 0
            .Cols(COL_NurseNotesDateTime).Width = .Width * 0

            .Cols(COL_WorkersCompFormID).Width = .Width * 0
            .Cols(COL_WorkersCompFormDateOfInjury).Width = .Width * 0
            .Cols(COL_WorkersCompFormType).Width = .Width * 0
            .Cols(COL_WorkersCompFormClaimNo).Width = .Width * 0
            .Cols(COL_WorkersCompFormDOS).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedBy).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedDate).Width = .Width * 0
        End With
    End Sub

    Public Sub Labs()
        Call setGridLab()
        Dim objLabs As New clsLabs
        Dim dt As DataTable = objLabs.Fill_LockLab(gstrClientMachineName, TrnType.Labs)

        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_LabOrderID, dt.Rows(i)("labom_OrderID"))
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(i + 1, COL_UserName, dt.Rows(i)("labom_UserName"))
                    .SetData(i + 1, COL_OrderDateTime, dt.Rows(i)("labom_TransactionDate"))
                    .SetData(i + 1, COL_OrderName, dt.Rows(i)("OrderName"))
                End With
            Next
            dt.Dispose()
            dt = Nothing
        End If
        objLabs.Dispose()
        objLabs = Nothing
    End Sub

    Public Sub setGridLab()
        With C1grdExams
            .Visible = True

            .Cols.Count = Col_Count

            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .AllowEditing = True
            .Width = .Width - 20

            .Cols(COL_Select).Width = .Width * 0.08
            .Cols(COL_Select).AllowEditing = True
            .Cols(COL_Select).DataType = System.Type.GetType("System.Boolean")
            .Cols(COL_Select).Name = "Select"
            .SetData(0, COL_Select, "Select")
            .Cols(COL_Select).TextAlignFixed = TextAlignEnum.CenterCenter
            .Cols(COL_ExamID).Width = 0

            '''''''''
            .Cols(COL_PatientCode).Width = .Width * 0.1
            .Cols(COL_PatientCode).AllowEditing = False
            .Cols(COL_PatientCode).Name = "Patient Code"
            .SetData(0, COL_PatientCode, "Patient Code")
            .Cols(COL_PatientCode).TextAlign = TextAlignEnum.LeftCenter
            '''''''''

            '''''''''
            .Cols(COL_PatientName).Width = .Width * 0.15
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_PatientName).Name = "Patient Name"
            .SetData(0, COL_PatientName, "Patient Name")
            .Cols(COL_PatientName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''

            .Cols(COL_ExamName).Width = 0


            ''''''''''
            .Cols(COL_UserName).Width = .Width * 0.1
            .Cols(COL_UserName).AllowEditing = False
            .Cols(COL_UserName).Name = "User Name"
            .SetData(0, COL_UserName, "User Name")
            .Cols(COL_UserName).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''

            .Cols(COL_MachineName).Width = 0
            .Cols(COL_dtDOS).Width = 0
            .Cols(COL_IsFinished).Width = 0
            .Cols(COL_PatientID).Width = 0
            .Cols(COL_VisitID).Width = 0
            .Cols(COL_VisitDate).Width = 0
            .Cols(COL_PrescriptionDateTime).Width = 0
            .Cols(COL_VitalID).Width = 0
            .Cols(COL_VitalDateTime).Width = 0

            ''''''''''''''''
            .Cols(COL_LabOrderID).Width = .Width * 0
            .Cols(COL_LabOrderID).AllowEditing = False
            .Cols(COL_LabOrderID).Name = "LabOrderId"
            .SetData(0, COL_LabOrderID, "LabOrderId")
            .Cols(COL_LabOrderID).TextAlign = TextAlignEnum.LeftCenter

            '''''''''''
            .Cols(COL_OrderDateTime).Width = .Width * 0.15
            .Cols(COL_OrderDateTime).AllowEditing = False
            .Cols(COL_OrderDateTime).Name = "Order DateTime"
            .SetData(0, COL_OrderDateTime, "Order DateTime")
            .Cols(COL_OrderDateTime).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''

            '''''''''''
            .Cols(COL_OrderName).Width = .Width * 0.15
            .Cols(COL_OrderName).AllowEditing = False
            .Cols(COL_OrderName).Name = "Order Number"
            .SetData(0, COL_OrderName, "Order Number")
            .Cols(COL_OrderName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''

            .Cols(COL_MessageID).Width = 0
            .Cols(COL_MessageDateTime).Width = 0
            .Cols(COL_MessageName).Width = 0
            .Cols(COL_LetterID).Width = 0
            .Cols(COL_LetterDateTime).Width = 0
            .Cols(COL_LetterName).Width = 0
            .Cols(COL_PTProtocolID).Width = 0
            .Cols(COL_PTProtocolDateTime).Width = 0
            .Cols(COL_PTProtocolName).Width = 0
            .Cols(COL_ConsentID).Width = 0
            .Cols(COL_ConsentDateTime).Width = 0
            .Cols(COL_ConsentName).Width = 0
            .Cols(COL_FlowSheetRecordID).Width = 0
            .Cols(COL_FlowSheetName).Width = 0
            .Cols(COL_TaskID).Width = 0
            .Cols(COL_TaskDate).Width = 0
            .Cols(COL_Subject).Width = 0
            .Cols(COL_ImmunizationID).Width = 0
            .Cols(COL_DocumentID).Width = 0
            .Cols(COL_DocumetnName).Width = 0

            .Cols(COL_DisclosureID).Width = .Width * 0
            .Cols(COL_DisclosureName).Width = .Width * 0
            .Cols(COL_DisclosureDateTime).Width = .Width * 0

            .Cols(COL_NurseNotesID).Width = .Width * 0
            .Cols(COL_NurseNotesName).Width = .Width * 0
            .Cols(COL_NurseNotesDateTime).Width = .Width * 0

            .Cols(COL_WorkersCompFormID).Width = .Width * 0
            .Cols(COL_WorkersCompFormDateOfInjury).Width = .Width * 0
            .Cols(COL_WorkersCompFormType).Width = .Width * 0
            .Cols(COL_WorkersCompFormClaimNo).Width = .Width * 0
            .Cols(COL_WorkersCompFormDOS).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedBy).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedDate).Width = .Width * 0
        End With
    End Sub

    Public Sub Messages()
        Call setGridMessages()
        Dim objMessages As New clsMessage
        Dim dt As DataTable = objMessages.Fill_LockMessages(gstrClientMachineName, TrnType.Messages)


        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_MessageID, dt.Rows(i)("nMessageID"))
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(i + 1, COL_UserName, dt.Rows(i)("sUserName"))
                    .SetData(i + 1, COL_MessageDateTime, dt.Rows(i)("dtMsgDate"))
                    .SetData(i + 1, COL_MessageName, dt.Rows(i)("MessageName"))
                End With

            Next
            dt.Dispose()
            dt = Nothing
        End If
        objMessages.Dispose()
        objMessages = Nothing
    End Sub

    Public Sub setGridMessages()
        With C1grdExams
            .Visible = True

            .Cols.Count = Col_Count

            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .AllowEditing = True
            .Width = .Width - 20

            .Cols(COL_Select).Width = .Width * 0.08
            .Cols(COL_Select).AllowEditing = True
            .Cols(COL_Select).DataType = System.Type.GetType("System.Boolean")
            .Cols(COL_Select).Name = "Select"
            .SetData(0, COL_Select, "Select")
            .Cols(COL_Select).TextAlignFixed = TextAlignEnum.CenterCenter
            .Cols(COL_ExamID).Width = 0

            '''''''''
            .Cols(COL_PatientCode).Width = .Width * 0.1
            .Cols(COL_PatientCode).AllowEditing = False
            .Cols(COL_PatientCode).Name = "Patient Code"
            .SetData(0, COL_PatientCode, "Patient Code")
            .Cols(COL_PatientCode).TextAlign = TextAlignEnum.LeftCenter
            '''''''''

            '''''''''
            .Cols(COL_PatientName).Width = .Width * 0.15
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_PatientName).Name = "Patient Name"
            .SetData(0, COL_PatientName, "Patient Name")
            .Cols(COL_PatientName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''

            .Cols(COL_ExamName).Width = 0


            ''''''''''
            .Cols(COL_UserName).Width = .Width * 0.1
            .Cols(COL_UserName).AllowEditing = False
            .Cols(COL_UserName).Name = "User Name"
            .SetData(0, COL_UserName, "User Name")
            .Cols(COL_UserName).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''

            .Cols(COL_MachineName).Width = 0
            .Cols(COL_dtDOS).Width = 0
            .Cols(COL_IsFinished).Width = 0
            .Cols(COL_PatientID).Width = 0
            .Cols(COL_VisitID).Width = 0
            .Cols(COL_VisitDate).Width = 0
            .Cols(COL_PrescriptionDateTime).Width = 0
            .Cols(COL_VitalID).Width = 0
            .Cols(COL_VitalDateTime).Width = 0
            .Cols(COL_LabOrderID).Width = 0
            .Cols(COL_OrderDateTime).Width = 0
            .Cols(COL_OrderName).Width = 0
            ''''''''''''
            .Cols(COL_MessageID).Width = .Width * 0
            .Cols(COL_MessageID).AllowEditing = False
            .Cols(COL_MessageID).Name = "MesssageID"
            .SetData(0, COL_MessageID, "MesssageID")
            .Cols(COL_MessageID).TextAlign = TextAlignEnum.LeftCenter

            '''''''''''''''
            ''''''''''''
            .Cols(COL_MessageDateTime).Width = .Width * 0.15
            .Cols(COL_MessageDateTime).AllowEditing = False
            .Cols(COL_MessageDateTime).Name = "Messsage DateTime"
            .SetData(0, COL_MessageDateTime, "Messsage DateTime")
            .Cols(COL_MessageDateTime).TextAlign = TextAlignEnum.LeftCenter

            ''''''''''''
            ''''''''''''
            .Cols(COL_MessageName).Width = .Width * 0.2
            .Cols(COL_MessageName).AllowEditing = False
            .Cols(COL_MessageName).Name = "Messsage Name"
            .SetData(0, COL_MessageName, "Messsage Name")
            .Cols(COL_MessageName).TextAlign = TextAlignEnum.LeftCenter

            ''''''''''''



            .Cols(COL_LetterID).Width = 0
            .Cols(COL_LetterDateTime).Width = 0
            .Cols(COL_LetterName).Width = 0
            .Cols(COL_PTProtocolID).Width = 0
            .Cols(COL_PTProtocolDateTime).Width = 0
            .Cols(COL_PTProtocolName).Width = 0
            .Cols(COL_ConsentID).Width = 0
            .Cols(COL_ConsentDateTime).Width = 0
            .Cols(COL_ConsentName).Width = 0
            .Cols(COL_FlowSheetRecordID).Width = 0
            .Cols(COL_FlowSheetName).Width = 0
            .Cols(COL_TaskID).Width = 0
            .Cols(COL_TaskDate).Width = 0
            .Cols(COL_Subject).Width = 0
            .Cols(COL_ImmunizationID).Width = 0
            .Cols(COL_DocumentID).Width = 0
            .Cols(COL_DocumetnName).Width = 0

            .Cols(COL_DisclosureID).Width = .Width * 0
            .Cols(COL_DisclosureName).Width = .Width * 0
            .Cols(COL_DisclosureDateTime).Width = .Width * 0

            .Cols(COL_NurseNotesID).Width = .Width * 0
            .Cols(COL_NurseNotesName).Width = .Width * 0
            .Cols(COL_NurseNotesDateTime).Width = .Width * 0

            .Cols(COL_WorkersCompFormID).Width = .Width * 0
            .Cols(COL_WorkersCompFormDateOfInjury).Width = .Width * 0
            .Cols(COL_WorkersCompFormType).Width = .Width * 0
            .Cols(COL_WorkersCompFormClaimNo).Width = .Width * 0
            .Cols(COL_WorkersCompFormDOS).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedBy).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedDate).Width = .Width * 0
        End With
    End Sub

    Public Sub Letters()
        Call setGridLetters()
        Dim objLetter As New clsPatientLetters
        Dim dt As DataTable
        dt = objLetter.Fill_LockPatientLetter(gstrClientMachineName, TrnType.Letters)

        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_LetterID, dt.Rows(i)("nLetterID"))
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(i + 1, COL_UserName, dt.Rows(i)("sUserName"))
                    .SetData(i + 1, COL_LetterDateTime, dt.Rows(i)("dtLetterdate"))
                    .SetData(i + 1, COL_LetterName, dt.Rows(i)("LetterName"))
                End With

            Next
            dt.Dispose()
            dt = Nothing
        End If
        objLetter.Dispose()
        objLetter = Nothing
    End Sub

    Public Sub setGridLetters()
        With C1grdExams
            .Visible = True

            .Cols.Count = Col_Count

            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .AllowEditing = True
            .Width = .Width - 20

            .Cols(COL_Select).Width = .Width * 0.08
            .Cols(COL_Select).AllowEditing = True
            .Cols(COL_Select).DataType = System.Type.GetType("System.Boolean")
            .Cols(COL_Select).Name = "Select"
            .SetData(0, COL_Select, "Select")
            .Cols(COL_Select).TextAlignFixed = TextAlignEnum.CenterCenter
            .Cols(COL_ExamID).Width = 0

            '''''''''
            .Cols(COL_PatientCode).Width = .Width * 0.1
            .Cols(COL_PatientCode).AllowEditing = False
            .Cols(COL_PatientCode).Name = "Patient Code"
            .SetData(0, COL_PatientCode, "Patient Code")
            .Cols(COL_PatientCode).TextAlign = TextAlignEnum.LeftCenter
            '''''''''

            '''''''''
            .Cols(COL_PatientName).Width = .Width * 0.15
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_PatientName).Name = "Patient Name"
            .SetData(0, COL_PatientName, "Patient Name")
            .Cols(COL_PatientName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''

            .Cols(COL_ExamName).Width = 0


            ''''''''''
            .Cols(COL_UserName).Width = .Width * 0.1
            .Cols(COL_UserName).AllowEditing = False
            .Cols(COL_UserName).Name = "User Name"
            .SetData(0, COL_UserName, "User Name")
            .Cols(COL_UserName).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''

            .Cols(COL_MachineName).Width = 0
            .Cols(COL_dtDOS).Width = 0
            .Cols(COL_IsFinished).Width = 0
            .Cols(COL_PatientID).Width = 0
            .Cols(COL_VisitID).Width = 0
            .Cols(COL_VisitDate).Width = 0
            .Cols(COL_PrescriptionDateTime).Width = 0
            .Cols(COL_VitalID).Width = 0
            .Cols(COL_VitalDateTime).Width = 0
            .Cols(COL_LabOrderID).Width = 0
            .Cols(COL_OrderDateTime).Width = 0
            .Cols(COL_OrderName).Width = 0
            .Cols(COL_MessageID).Width = .Width * 0
            .Cols(COL_MessageDateTime).Width = .Width * 0
            .Cols(COL_MessageName).Width = .Width * 0

            '''''''''''           
            .Cols(COL_LetterID).Width = .Width * 0
            .Cols(COL_LetterID).AllowEditing = False
            .Cols(COL_LetterID).Name = "LetterID"
            .SetData(0, COL_LetterID, "LetterID")
            .Cols(COL_LetterID).TextAlign = TextAlignEnum.LeftCenter

            '''''''''''''''''

            ''''''''
            .Cols(COL_LetterDateTime).Width = .Width * 0.2
            .Cols(COL_LetterDateTime).AllowEditing = False
            .Cols(COL_LetterDateTime).Name = "Letter DateTime"
            .SetData(0, COL_LetterDateTime, "Letter DateTime")
            .Cols(COL_LetterDateTime).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''

            '''''''
            .Cols(COL_LetterName).Width = .Width * 0.2
            .Cols(COL_LetterName).AllowEditing = False
            .Cols(COL_LetterName).Name = "Letter Name"
            .SetData(0, COL_LetterName, "Letter Name")
            .Cols(COL_LetterName).TextAlign = TextAlignEnum.LeftCenter
            ''''''''


            .Cols(COL_PTProtocolID).Width = 0
            .Cols(COL_PTProtocolDateTime).Width = 0
            .Cols(COL_PTProtocolName).Width = 0
            .Cols(COL_ConsentID).Width = 0
            .Cols(COL_ConsentDateTime).Width = 0
            .Cols(COL_ConsentName).Width = 0
            .Cols(COL_FlowSheetRecordID).Width = 0
            .Cols(COL_FlowSheetName).Width = 0
            .Cols(COL_TaskID).Width = 0
            .Cols(COL_TaskDate).Width = 0
            .Cols(COL_Subject).Width = 0
            .Cols(COL_ImmunizationID).Width = 0
            .Cols(COL_DocumentID).Width = 0
            .Cols(COL_DocumetnName).Width = 0

            .Cols(COL_DisclosureID).Width = .Width * 0
            .Cols(COL_DisclosureName).Width = .Width * 0
            .Cols(COL_DisclosureDateTime).Width = .Width * 0

            .Cols(COL_NurseNotesID).Width = .Width * 0
            .Cols(COL_NurseNotesName).Width = .Width * 0
            .Cols(COL_NurseNotesDateTime).Width = .Width * 0

            .Cols(COL_WorkersCompFormID).Width = .Width * 0
        End With
    End Sub

    Public Sub PTProtocol()
        Call setGridPTProtocol()
        Dim objPTProtocol As New clsPTProtocols
        Dim dt As DataTable = objPTProtocol.Fill_LockPTProtocol(gstrClientMachineName, TrnType.PTProtocol)

        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_PTProtocolID, dt.Rows(i)("nProtocolID"))
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(i + 1, COL_UserName, dt.Rows(i)("sUserName"))
                    .SetData(i + 1, COL_PTProtocolDateTime, dt.Rows(i)("dtProtocoldate"))
                    .SetData(i + 1, COL_PTProtocolName, dt.Rows(i)("PTProtocolName"))
                End With

            Next
            dt.Dispose()
            dt = Nothing
        End If
        objPTProtocol.Dispose()
        objPTProtocol = Nothing
    End Sub

    Public Sub setGridPTProtocol()
        With C1grdExams
            .Visible = True

            .Cols.Count = Col_Count

            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .AllowEditing = True
            .Width = .Width - 20

            .Cols(COL_Select).Width = .Width * 0.08
            .Cols(COL_Select).AllowEditing = True
            .Cols(COL_Select).DataType = System.Type.GetType("System.Boolean")
            .Cols(COL_Select).Name = "Select"
            .SetData(0, COL_Select, "Select")
            .Cols(COL_Select).TextAlignFixed = TextAlignEnum.CenterCenter
            '''''''''''''

            .Cols(COL_ExamID).Width = 0


            '''''''''
            .Cols(COL_PatientCode).Width = .Width * 0.1
            .Cols(COL_PatientCode).AllowEditing = False
            .Cols(COL_PatientCode).Name = "Patient Code"
            .SetData(0, COL_PatientCode, "Patient Code")
            .Cols(COL_PatientCode).TextAlign = TextAlignEnum.LeftCenter
            '''''''''

            '''''''''
            .Cols(COL_PatientName).Width = .Width * 0.15
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_PatientName).Name = "Patient Name"
            .SetData(0, COL_PatientName, "Patient Name")
            .Cols(COL_PatientName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''

            .Cols(COL_ExamName).Width = 0


            ''''''''''
            .Cols(COL_UserName).Width = .Width * 0.15
            .Cols(COL_UserName).AllowEditing = False
            .Cols(COL_UserName).Name = "User Name"
            .SetData(0, COL_UserName, "User Name")
            .Cols(COL_UserName).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''

            .Cols(COL_MachineName).Width = 0
            .Cols(COL_dtDOS).Width = 0
            .Cols(COL_IsFinished).Width = 0
            .Cols(COL_PatientID).Width = 0
            .Cols(COL_VisitID).Width = 0
            .Cols(COL_VisitDate).Width = 0
            .Cols(COL_PrescriptionDateTime).Width = 0
            .Cols(COL_VitalID).Width = 0
            .Cols(COL_VitalDateTime).Width = 0
            .Cols(COL_LabOrderID).Width = 0
            .Cols(COL_OrderDateTime).Width = 0
            .Cols(COL_OrderName).Width = 0
            .Cols(COL_MessageID).Width = .Width * 0
            .Cols(COL_MessageDateTime).Width = .Width * 0
            .Cols(COL_MessageName).Width = .Width * 0
            .Cols(COL_LetterID).Width = .Width * 0
            .Cols(COL_LetterDateTime).Width = .Width * 0
            .Cols(COL_LetterName).Width = .Width * 0

            ''''''''''''
            .Cols(COL_PTProtocolID).Width = .Width * 0
            .Cols(COL_PTProtocolID).AllowEditing = False
            .Cols(COL_PTProtocolID).Name = "PTProtocolID"
            .SetData(0, COL_PTProtocolID, "PTProtocolID")
            .Cols(COL_PTProtocolID).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''''

            ''''''''''''
            .Cols(COL_PTProtocolDateTime).Width = .Width * 0.2
            .Cols(COL_PTProtocolDateTime).AllowEditing = False
            .Cols(COL_PTProtocolDateTime).Name = "PTProtocol DateTime"
            .SetData(0, COL_PTProtocolDateTime, "PTProtocol DateTime")
            .Cols(COL_PTProtocolDateTime).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''''

            ''''''''''
            .Cols(COL_PTProtocolName).Width = .Width * 0.2
            .Cols(COL_PTProtocolName).AllowEditing = False
            .Cols(COL_PTProtocolName).Name = "PTProtocolName"
            .SetData(0, COL_PTProtocolName, "PTProtocol Name")
            .Cols(COL_PTProtocolName).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''
            .Cols(COL_ConsentID).Width = 0
            .Cols(COL_ConsentDateTime).Width = 0
            .Cols(COL_ConsentName).Width = 0
            .Cols(COL_FlowSheetRecordID).Width = 0
            .Cols(COL_FlowSheetName).Width = 0
            .Cols(COL_TaskID).Width = 0
            .Cols(COL_TaskDate).Width = 0
            .Cols(COL_Subject).Width = 0
            .Cols(COL_ImmunizationID).Width = 0
            .Cols(COL_DocumentID).Width = 0
            .Cols(COL_DocumetnName).Width = 0

            .Cols(COL_DisclosureID).Width = .Width * 0
            .Cols(COL_DisclosureName).Width = .Width * 0
            .Cols(COL_DisclosureDateTime).Width = .Width * 0

            .Cols(COL_NurseNotesID).Width = .Width * 0
            .Cols(COL_NurseNotesName).Width = .Width * 0
            .Cols(COL_NurseNotesDateTime).Width = .Width * 0

            .Cols(COL_WorkersCompFormID).Width = .Width * 0
            .Cols(COL_WorkersCompFormDateOfInjury).Width = .Width * 0
            .Cols(COL_WorkersCompFormType).Width = .Width * 0
            .Cols(COL_WorkersCompFormClaimNo).Width = .Width * 0
            .Cols(COL_WorkersCompFormDOS).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedBy).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedDate).Width = .Width * 0
        End With
    End Sub

    Public Sub PatientConsent()
        Call setGridConsent()
        Dim objConsent As New clsPatientConsent
        Dim dt As DataTable = objConsent.Fill_LockPatientConsent(gstrClientMachineName, TrnType.PatientConsent)

        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_ConsentID, dt.Rows(i)("nConsentId"))
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(i + 1, COL_UserName, dt.Rows(i)("sUserName"))
                    .SetData(i + 1, COL_ConsentDateTime, dt.Rows(i)("dtConsentdate"))
                    .SetData(i + 1, COL_ConsentName, dt.Rows(i)("ConsentName"))
                End With

            Next
            dt.Dispose()
            dt = Nothing
        End If
        objConsent.Dispose()
        objConsent = Nothing
    End Sub

    Public Sub DisclosureManagement()
        Call setGridDisclosure()
        Dim objConsent As New clsDisclosureMgmt
        Dim dt As DataTable      ''Bug : 00000828: Record locking. Record not displayed in unlock record list.
        dt = objConsent.Fill_LockPatientDisclosure(gstrClientMachineName, TrnType.DisclosureManagement)

        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_DisclosureID, dt.Rows(i)("nDisclosureId"))
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(i + 1, COL_UserName, dt.Rows(i)("sUserName"))
                    .SetData(i + 1, COL_DisclosureDateTime, dt.Rows(i)("dtDisclosuredate"))
                    .SetData(i + 1, COL_DisclosureName, dt.Rows(i)("DisclosureName"))
                End With

            Next
            dt.Dispose()
            dt = Nothing
        End If
        objConsent.Dispose()
        objConsent = Nothing
    End Sub

    ''Bug : 00000828: Record locking. New Node Added to unlock Nurse Notes.
    Public Sub NurseNotes()
        Call setGridNurseNotes()
        Dim objNurseNotes As New clsNurseNotes
        Dim dt As DataTable = objNurseNotes.Fill_LockNurseNotes(gstrClientMachineName, TrnType.NurseNotes)

        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_NurseNotesID, dt.Rows(i)("nNurseNotesId"))
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(i + 1, COL_UserName, dt.Rows(i)("sUserName"))
                    .SetData(i + 1, COL_NurseNotesDateTime, dt.Rows(i)("dtNotesdate"))
                    .SetData(i + 1, COL_NurseNotesName, dt.Rows(i)("NurseNotesName"))
                End With

            Next
            dt.Dispose()
            dt = Nothing
        End If
        objNurseNotes.Dispose()
        objNurseNotes = Nothing
    End Sub

    Public Sub setGridConsent()
        With C1grdExams
            .Visible = True

            .Cols.Count = Col_Count

            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .AllowEditing = True
            .Width = .Width - 20

            .Cols(COL_Select).Width = .Width * 0.08
            .Cols(COL_Select).AllowEditing = True
            .Cols(COL_Select).DataType = System.Type.GetType("System.Boolean")
            .Cols(COL_Select).Name = "Select"
            .SetData(0, COL_Select, "Select")
            .Cols(COL_Select).TextAlignFixed = TextAlignEnum.CenterCenter
            .Cols(COL_ExamID).Width = 0

            '''''''''
            .Cols(COL_PatientCode).Width = .Width * 0.1
            .Cols(COL_PatientCode).AllowEditing = False
            .Cols(COL_PatientCode).Name = "Patient Code"
            .SetData(0, COL_PatientCode, "Patient Code")
            .Cols(COL_PatientCode).TextAlign = TextAlignEnum.LeftCenter
            '''''''''

            '''''''''
            .Cols(COL_PatientName).Width = .Width * 0.15
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_PatientName).Name = "Patient Name"
            .SetData(0, COL_PatientName, "Patient Name")
            .Cols(COL_PatientName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''

            .Cols(COL_ExamName).Width = 0


            ''''''''''
            .Cols(COL_UserName).Width = .Width * 0.15
            .Cols(COL_UserName).AllowEditing = False
            .Cols(COL_UserName).Name = "User Name"
            .SetData(0, COL_UserName, "User Name")
            .Cols(COL_UserName).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''

            .Cols(COL_MachineName).Width = 0
            .Cols(COL_dtDOS).Width = 0
            .Cols(COL_IsFinished).Width = 0
            .Cols(COL_PatientID).Width = 0
            .Cols(COL_VisitID).Width = 0
            .Cols(COL_VisitDate).Width = 0
            .Cols(COL_PrescriptionDateTime).Width = 0
            .Cols(COL_VitalID).Width = 0
            .Cols(COL_VitalDateTime).Width = 0
            .Cols(COL_LabOrderID).Width = 0
            .Cols(COL_OrderDateTime).Width = 0
            .Cols(COL_OrderName).Width = 0
            .Cols(COL_MessageID).Width = .Width * 0
            .Cols(COL_MessageDateTime).Width = .Width * 0
            .Cols(COL_MessageName).Width = .Width * 0
            .Cols(COL_LetterID).Width = .Width * 0
            .Cols(COL_LetterDateTime).Width = .Width * 0
            .Cols(COL_LetterName).Width = .Width * 0
            .Cols(COL_PTProtocolID).Width = .Width * 0
            .Cols(COL_PTProtocolDateTime).Width = .Width * 0
            .Cols(COL_PTProtocolName).Width = .Width * 0

            ''''''''''''''
            .Cols(COL_ConsentID).Width = .Width * 0
            .Cols(COL_ConsentID).AllowEditing = False
            .Cols(COL_ConsentID).Name = "ConsentID"
            .SetData(0, COL_ConsentID, "ConsentID")
            .Cols(COL_ConsentID).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''''''''''''''''''

            ''''''''''''''''
            .Cols(COL_ConsentDateTime).Width = .Width * 0.2
            .Cols(COL_ConsentDateTime).AllowEditing = False
            .Cols(COL_ConsentDateTime).Name = "Consent DateTime"
            .SetData(0, COL_ConsentDateTime, "Consent DateTime")
            .Cols(COL_ConsentDateTime).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''''''

            ''''''''''''
            .Cols(COL_ConsentName).Width = .Width * 0.2
            .Cols(COL_ConsentName).AllowEditing = False
            .Cols(COL_ConsentName).Name = "Consent Name"
            .SetData(0, COL_ConsentName, "Consent Name")
            .Cols(COL_ConsentName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''

            .Cols(COL_FlowSheetRecordID).Width = 0
            .Cols(COL_FlowSheetName).Width = 0
            .Cols(COL_TaskID).Width = 0
            .Cols(COL_TaskDate).Width = 0
            .Cols(COL_Subject).Width = 0
            .Cols(COL_ImmunizationID).Width = 0
            .Cols(COL_DocumentID).Width = 0
            .Cols(COL_DocumetnName).Width = 0

            .Cols(COL_DisclosureID).Width = .Width * 0
            .Cols(COL_DisclosureName).Width = .Width * 0
            .Cols(COL_DisclosureDateTime).Width = .Width * 0

            .Cols(COL_NurseNotesID).Width = .Width * 0
            .Cols(COL_NurseNotesName).Width = .Width * 0
            .Cols(COL_NurseNotesDateTime).Width = .Width * 0

            .Cols(COL_WorkersCompFormID).Width = .Width * 0
            .Cols(COL_WorkersCompFormDateOfInjury).Width = .Width * 0
            .Cols(COL_WorkersCompFormType).Width = .Width * 0
            .Cols(COL_WorkersCompFormClaimNo).Width = .Width * 0
            .Cols(COL_WorkersCompFormDOS).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedBy).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedDate).Width = .Width * 0
        End With
    End Sub

    Public Sub setGridDisclosure()
        With C1grdExams
            .Visible = True

            .Cols.Count = Col_Count

            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .AllowEditing = True
            .Width = .Width - 20

            .Cols(COL_Select).Width = .Width * 0.08
            .Cols(COL_Select).AllowEditing = True
            .Cols(COL_Select).DataType = System.Type.GetType("System.Boolean")
            .Cols(COL_Select).Name = "Select"
            .SetData(0, COL_Select, "Select")
            .Cols(COL_Select).TextAlignFixed = TextAlignEnum.CenterCenter
            .Cols(COL_ExamID).Width = 0

            '''''''''
            .Cols(COL_PatientCode).Width = .Width * 0.1
            .Cols(COL_PatientCode).AllowEditing = False
            .Cols(COL_PatientCode).Name = "Patient Code"
            .SetData(0, COL_PatientCode, "Patient Code")
            .Cols(COL_PatientCode).TextAlign = TextAlignEnum.LeftCenter
            '''''''''

            '''''''''
            .Cols(COL_PatientName).Width = .Width * 0.15
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_PatientName).Name = "Patient Name"
            .SetData(0, COL_PatientName, "Patient Name")
            .Cols(COL_PatientName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''

            .Cols(COL_ExamName).Width = 0


            ''''''''''
            .Cols(COL_UserName).Width = .Width * 0.15
            .Cols(COL_UserName).AllowEditing = False
            .Cols(COL_UserName).Name = "User Name"
            .SetData(0, COL_UserName, "User Name")
            .Cols(COL_UserName).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''

            .Cols(COL_MachineName).Width = 0
            .Cols(COL_dtDOS).Width = 0
            .Cols(COL_IsFinished).Width = 0
            .Cols(COL_PatientID).Width = 0
            .Cols(COL_VisitID).Width = 0
            .Cols(COL_VisitDate).Width = 0
            .Cols(COL_PrescriptionDateTime).Width = 0
            .Cols(COL_VitalID).Width = 0
            .Cols(COL_VitalDateTime).Width = 0
            .Cols(COL_LabOrderID).Width = 0
            .Cols(COL_OrderDateTime).Width = 0
            .Cols(COL_OrderName).Width = 0
            .Cols(COL_MessageID).Width = .Width * 0
            .Cols(COL_MessageDateTime).Width = .Width * 0
            .Cols(COL_MessageName).Width = .Width * 0
            .Cols(COL_LetterID).Width = .Width * 0
            .Cols(COL_LetterDateTime).Width = .Width * 0
            .Cols(COL_LetterName).Width = .Width * 0
            .Cols(COL_PTProtocolID).Width = .Width * 0
            .Cols(COL_PTProtocolDateTime).Width = .Width * 0
            .Cols(COL_PTProtocolName).Width = .Width * 0

            ''''''''''''''
            .Cols(COL_DisclosureID).Width = .Width * 0
            .Cols(COL_DisclosureID).AllowEditing = False
            .Cols(COL_DisclosureID).Name = "DisclosureID"
            .SetData(0, COL_DisclosureID, "DisclosureID")
            .Cols(COL_DisclosureID).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''''''''''''''''''

            ''''''''''''''''
            .Cols(COL_DisclosureDateTime).Width = .Width * 0.2
            .Cols(COL_DisclosureDateTime).AllowEditing = False
            .Cols(COL_DisclosureDateTime).Name = "Disclosure DateTime"
            .SetData(0, COL_DisclosureDateTime, "Disclosure DateTime")
            .Cols(COL_DisclosureDateTime).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''''''

            ''''''''''''
            .Cols(COL_DisclosureName).Width = .Width * 0.2
            .Cols(COL_DisclosureName).AllowEditing = False
            .Cols(COL_DisclosureName).Name = "Disclosure Name"
            .SetData(0, COL_DisclosureName, "Disclosure Name")
            .Cols(COL_DisclosureName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''

            .Cols(COL_FlowSheetRecordID).Width = 0
            .Cols(COL_FlowSheetName).Width = 0
            .Cols(COL_TaskID).Width = 0
            .Cols(COL_TaskDate).Width = 0
            .Cols(COL_Subject).Width = 0
            .Cols(COL_ImmunizationID).Width = 0
            .Cols(COL_DocumentID).Width = 0
            .Cols(COL_DocumetnName).Width = 0

            .Cols(COL_NurseNotesID).Width = .Width * 0
            .Cols(COL_NurseNotesName).Width = .Width * 0
            .Cols(COL_NurseNotesDateTime).Width = .Width * 0

            .Cols(COL_WorkersCompFormID).Width = .Width * 0
            .Cols(COL_WorkersCompFormDateOfInjury).Width = .Width * 0
            .Cols(COL_WorkersCompFormType).Width = .Width * 0
            .Cols(COL_WorkersCompFormClaimNo).Width = .Width * 0
            .Cols(COL_WorkersCompFormDOS).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedBy).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedDate).Width = .Width * 0
        End With
    End Sub

    ''Bug : 00000828: Record locking. New Node Added to unlock Nurse Notes.
    Public Sub setGridNurseNotes()
        With C1grdExams
            .Visible = True

            .Cols.Count = Col_Count

            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .AllowEditing = True
            .Width = .Width - 20

            .Cols(COL_Select).Width = .Width * 0.08
            .Cols(COL_Select).AllowEditing = True
            .Cols(COL_Select).DataType = System.Type.GetType("System.Boolean")
            .Cols(COL_Select).Name = "Select"
            .SetData(0, COL_Select, "Select")
            .Cols(COL_Select).TextAlignFixed = TextAlignEnum.CenterCenter
            .Cols(COL_ExamID).Width = 0

            '''''''''
            .Cols(COL_PatientCode).Width = .Width * 0.1
            .Cols(COL_PatientCode).AllowEditing = False
            .Cols(COL_PatientCode).Name = "Patient Code"
            .SetData(0, COL_PatientCode, "Patient Code")
            .Cols(COL_PatientCode).TextAlign = TextAlignEnum.LeftCenter
            '''''''''

            '''''''''
            .Cols(COL_PatientName).Width = .Width * 0.15
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_PatientName).Name = "Patient Name"
            .SetData(0, COL_PatientName, "Patient Name")
            .Cols(COL_PatientName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''

            .Cols(COL_ExamName).Width = 0


            ''''''''''
            .Cols(COL_UserName).Width = .Width * 0.15
            .Cols(COL_UserName).AllowEditing = False
            .Cols(COL_UserName).Name = "User Name"
            .SetData(0, COL_UserName, "User Name")
            .Cols(COL_UserName).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''

            .Cols(COL_MachineName).Width = 0
            .Cols(COL_dtDOS).Width = 0
            .Cols(COL_IsFinished).Width = 0
            .Cols(COL_PatientID).Width = 0
            .Cols(COL_VisitID).Width = 0
            .Cols(COL_VisitDate).Width = 0
            .Cols(COL_PrescriptionDateTime).Width = 0
            .Cols(COL_VitalID).Width = 0
            .Cols(COL_VitalDateTime).Width = 0
            .Cols(COL_LabOrderID).Width = 0
            .Cols(COL_OrderDateTime).Width = 0
            .Cols(COL_OrderName).Width = 0
            .Cols(COL_MessageID).Width = .Width * 0
            .Cols(COL_MessageDateTime).Width = .Width * 0
            .Cols(COL_MessageName).Width = .Width * 0
            .Cols(COL_LetterID).Width = .Width * 0
            .Cols(COL_LetterDateTime).Width = .Width * 0
            .Cols(COL_LetterName).Width = .Width * 0
            .Cols(COL_PTProtocolID).Width = .Width * 0
            .Cols(COL_PTProtocolDateTime).Width = .Width * 0
            .Cols(COL_PTProtocolName).Width = .Width * 0
            .Cols(COL_DisclosureID).Width = .Width * 0
            .Cols(COL_DisclosureDateTime).Width = .Width * 0
            .Cols(COL_DisclosureName).Width = .Width * 0
            ''''''''''''''
            .Cols(COL_NurseNotesID).Width = .Width * 0
            .Cols(COL_NurseNotesID).AllowEditing = False
            .Cols(COL_NurseNotesID).Name = "NurseNotesID"
            .SetData(0, COL_NurseNotesID, "NurseNotesID")
            .Cols(COL_NurseNotesID).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''''''''''''''''''

            ''''''''''''''''
            .Cols(COL_NurseNotesDateTime).Width = .Width * 0.2
            .Cols(COL_NurseNotesDateTime).AllowEditing = False
            .Cols(COL_NurseNotesDateTime).Name = "NurseNotes DateTime"
            .SetData(0, COL_NurseNotesDateTime, "NurseNotes DateTime")
            .Cols(COL_NurseNotesDateTime).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''''''

            ''''''''''''
            .Cols(COL_NurseNotesName).Width = .Width * 0.2
            .Cols(COL_NurseNotesName).AllowEditing = False
            .Cols(COL_NurseNotesName).Name = "NurseNotes Name"
            .SetData(0, COL_NurseNotesName, "NurseNotes Name")
            .Cols(COL_NurseNotesName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''

            .Cols(COL_FlowSheetRecordID).Width = 0
            .Cols(COL_FlowSheetName).Width = 0
            .Cols(COL_TaskID).Width = 0
            .Cols(COL_TaskDate).Width = 0
            .Cols(COL_Subject).Width = 0
            .Cols(COL_ImmunizationID).Width = 0
            .Cols(COL_DocumentID).Width = 0
            .Cols(COL_DocumetnName).Width = 0

            .Cols(COL_DisclosureID).Width = .Width * 0
            .Cols(COL_DisclosureName).Width = .Width * 0
            .Cols(COL_DisclosureDateTime).Width = .Width * 0

            .Cols(COL_WorkersCompFormID).Width = .Width * 0
            .Cols(COL_WorkersCompFormDateOfInjury).Width = .Width * 0
            .Cols(COL_WorkersCompFormType).Width = .Width * 0
            .Cols(COL_WorkersCompFormClaimNo).Width = .Width * 0
            .Cols(COL_WorkersCompFormDOS).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedBy).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedDate).Width = .Width * 0
        End With
    End Sub

    Public Sub Flowsheet()
        Call setGridFlowSheet()
        Dim objFlowSheet As New clsFlowSheet
        Dim dt As DataTable

        dt = objFlowSheet.Fill_LockFlowSheet(gstrClientMachineName, TrnType.Flowsheet)

        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_FlowSheetRecordID, dt.Rows(i)("nFlowSheetRecordID"))
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(i + 1, COL_UserName, dt.Rows(i)("sUserName"))
                    .SetData(i + 1, COL_FlowSheetName, dt.Rows(i)("sFlowSheetName"))
                End With

            Next
            dt.Dispose()
            dt = Nothing
        End If
        objFlowSheet.Dispose()
        objFlowSheet = Nothing
    End Sub

    Public Sub setGridFlowSheet()
        With C1grdExams
            .Visible = True

            .Cols.Count = Col_Count

            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .AllowEditing = True
            .Width = .Width - 20

            .Cols(COL_Select).Width = .Width * 0.08
            .Cols(COL_Select).AllowEditing = True
            .Cols(COL_Select).DataType = System.Type.GetType("System.Boolean")
            .Cols(COL_Select).Name = "Select"
            .SetData(0, COL_Select, "Select")
            .Cols(COL_Select).TextAlignFixed = TextAlignEnum.CenterCenter
            .Cols(COL_ExamID).Width = 0

            '''''''''
            .Cols(COL_PatientCode).Width = .Width * 0.1
            .Cols(COL_PatientCode).AllowEditing = False
            .Cols(COL_PatientCode).Name = "Patient Code"
            .SetData(0, COL_PatientCode, "Patient Code")
            .Cols(COL_PatientCode).TextAlign = TextAlignEnum.LeftCenter
            '''''''''

            '''''''''
            .Cols(COL_PatientName).Width = .Width * 0.15
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_PatientName).Name = "Patient Name"
            .SetData(0, COL_PatientName, "Patient Name")
            .Cols(COL_PatientName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''

            .Cols(COL_ExamName).Width = 0


            ''''''''''
            .Cols(COL_UserName).Width = .Width * 0.15
            .Cols(COL_UserName).AllowEditing = False
            .Cols(COL_UserName).Name = "User Name"
            .SetData(0, COL_UserName, "User Name")
            .Cols(COL_UserName).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''

            .Cols(COL_MachineName).Width = 0
            .Cols(COL_dtDOS).Width = 0
            .Cols(COL_IsFinished).Width = 0
            .Cols(COL_PatientID).Width = 0
            .Cols(COL_VisitID).Width = 0
            .Cols(COL_VisitDate).Width = 0
            .Cols(COL_PrescriptionDateTime).Width = 0
            .Cols(COL_VitalID).Width = 0
            .Cols(COL_VitalDateTime).Width = 0
            .Cols(COL_LabOrderID).Width = 0
            .Cols(COL_OrderDateTime).Width = 0
            .Cols(COL_OrderName).Width = 0
            .Cols(COL_MessageID).Width = .Width * 0
            .Cols(COL_MessageDateTime).Width = .Width * 0
            .Cols(COL_MessageName).Width = .Width * 0
            .Cols(COL_LetterID).Width = .Width * 0
            .Cols(COL_LetterDateTime).Width = .Width * 0
            .Cols(COL_LetterName).Width = .Width * 0
            .Cols(COL_PTProtocolID).Width = .Width * 0
            .Cols(COL_PTProtocolDateTime).Width = .Width * 0
            .Cols(COL_PTProtocolName).Width = .Width * 0
            .Cols(COL_ConsentID).Width = .Width * 0
            .Cols(COL_ConsentDateTime).Width = .Width * 0
            .Cols(COL_ConsentName).Width = .Width * 0

            ''''''''''''''''
            .Cols(COL_FlowSheetRecordID).Width = .Width * 0
            .Cols(COL_FlowSheetRecordID).AllowEditing = False
            .Cols(COL_FlowSheetRecordID).Name = "FlowSheet ID"
            .SetData(0, COL_FlowSheetRecordID, "FlowSheet ID")
            .Cols(COL_FlowSheetRecordID).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''''''''''''''''''''
            ''''''''''''
            ''Bug : 00000828: Record locking. Form Level locking so Name not displayed
            .Cols(COL_FlowSheetName).Width = .Width * 0
            .Cols(COL_FlowSheetName).AllowEditing = False
            .Cols(COL_FlowSheetName).Name = "FlowSheet Name"
            .SetData(0, COL_FlowSheetName, "FlowSheet Name")
            .Cols(COL_FlowSheetName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''''

            .Cols(COL_TaskID).Width = 0
            .Cols(COL_TaskDate).Width = 0
            .Cols(COL_Subject).Width = 0
            .Cols(COL_ImmunizationID).Width = 0
            .Cols(COL_DocumentID).Width = 0
            .Cols(COL_DocumetnName).Width = 0

            .Cols(COL_DisclosureID).Width = .Width * 0
            .Cols(COL_DisclosureName).Width = .Width * 0
            .Cols(COL_DisclosureDateTime).Width = .Width * 0

            .Cols(COL_NurseNotesID).Width = .Width * 0
            .Cols(COL_NurseNotesName).Width = .Width * 0
            .Cols(COL_NurseNotesDateTime).Width = .Width * 0

            .Cols(COL_WorkersCompFormID).Width = .Width * 0
            .Cols(COL_WorkersCompFormDateOfInjury).Width = .Width * 0
            .Cols(COL_WorkersCompFormType).Width = .Width * 0
            .Cols(COL_WorkersCompFormClaimNo).Width = .Width * 0
            .Cols(COL_WorkersCompFormDOS).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedBy).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedDate).Width = .Width * 0
        End With
    End Sub

    Public Sub Task()
        Call setGridTask()
        Dim objTask As New ClsTasksDBLayer
        Dim dt As DataTable
        dt = objTask.Fill_LockTask(gstrClientMachineName, TrnType.Task)

        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_TaskID, dt.Rows(i)("nTaskId"))
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(i + 1, COL_UserName, dt.Rows(i)("sUserName"))
                    .SetData(i + 1, COL_TaskDate, dt.Rows(i)("dtTaskDate"))
                    .SetData(i + 1, COL_Subject, dt.Rows(i)("sSubject"))
                End With

            Next
            dt.Dispose()
            dt = Nothing
        End If
        objTask.Dispose()
        objTask = Nothing
    End Sub

    Public Sub setGridTask()
        With C1grdExams
            .Visible = True

            .Cols.Count = Col_Count

            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .AllowEditing = True
            .Width = .Width - 20

            .Cols(COL_Select).Width = .Width * 0.08
            .Cols(COL_Select).AllowEditing = True
            .Cols(COL_Select).DataType = System.Type.GetType("System.Boolean")
            .Cols(COL_Select).Name = "Select"
            .SetData(0, COL_Select, "Select")
            .Cols(COL_Select).TextAlignFixed = TextAlignEnum.CenterCenter
            .Cols(COL_ExamID).Width = 0

            '''''''''
            .Cols(COL_PatientCode).Width = .Width * 0.1
            .Cols(COL_PatientCode).AllowEditing = False
            .Cols(COL_PatientCode).Name = "Patient Code"
            .SetData(0, COL_PatientCode, "Patient Code")
            .Cols(COL_PatientCode).TextAlign = TextAlignEnum.LeftCenter
            '''''''''

            '''''''''
            .Cols(COL_PatientName).Width = .Width * 0.15
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_PatientName).Name = "Patient Name"
            .SetData(0, COL_PatientName, "Patient Name")
            .Cols(COL_PatientName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''

            .Cols(COL_ExamName).Width = 0


            ''''''''''
            .Cols(COL_UserName).Width = .Width * 0.08
            .Cols(COL_UserName).AllowEditing = False
            .Cols(COL_UserName).Name = "User Name"
            .SetData(0, COL_UserName, "User Name")
            .Cols(COL_UserName).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''

            .Cols(COL_MachineName).Width = 0
            .Cols(COL_dtDOS).Width = 0
            .Cols(COL_IsFinished).Width = 0
            .Cols(COL_PatientID).Width = 0
            .Cols(COL_VisitID).Width = 0
            .Cols(COL_VisitDate).Width = 0
            .Cols(COL_PrescriptionDateTime).Width = 0
            .Cols(COL_VitalID).Width = 0
            .Cols(COL_VitalDateTime).Width = 0
            .Cols(COL_LabOrderID).Width = 0
            .Cols(COL_OrderDateTime).Width = 0
            .Cols(COL_OrderName).Width = 0
            .Cols(COL_MessageID).Width = .Width * 0
            .Cols(COL_MessageDateTime).Width = .Width * 0
            .Cols(COL_MessageName).Width = .Width * 0
            .Cols(COL_LetterID).Width = .Width * 0
            .Cols(COL_LetterDateTime).Width = .Width * 0
            .Cols(COL_LetterName).Width = .Width * 0
            .Cols(COL_PTProtocolID).Width = .Width * 0
            .Cols(COL_PTProtocolDateTime).Width = .Width * 0
            .Cols(COL_PTProtocolName).Width = .Width * 0
            .Cols(COL_ConsentID).Width = .Width * 0
            .Cols(COL_ConsentDateTime).Width = .Width * 0
            .Cols(COL_ConsentName).Width = .Width * 0
            .Cols(COL_FlowSheetRecordID).Width = .Width * 0
            .Cols(COL_FlowSheetName).Width = .Width * 0

            ''''''''''''''''
            .Cols(COL_TaskID).Width = .Width * 0
            .Cols(COL_TaskID).AllowEditing = False
            .Cols(COL_TaskID).Name = "TaskID"
            .SetData(0, COL_TaskID, "TaskID")
            .Cols(COL_TaskID).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''''

            '''''''''''''
            .Cols(COL_TaskDate).Width = .Width * 0.2
            .Cols(COL_TaskDate).AllowEditing = False
            .Cols(COL_TaskDate).Name = "Task Date"
            .SetData(0, COL_TaskDate, "Task Date")
            .Cols(COL_TaskDate).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''''''''''''

            '''''''''''''''''''''
            .Cols(COL_Subject).Width = .Width * 0.15
            .Cols(COL_Subject).AllowEditing = False
            .Cols(COL_Subject).Name = "Subject"
            .SetData(0, COL_Subject, "Subject")
            .Cols(COL_Subject).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''''''''

            .Cols(COL_ImmunizationID).Width = 0
            .Cols(COL_DocumentID).Width = 0
            .Cols(COL_DocumetnName).Width = 0

            .Cols(COL_DisclosureID).Width = .Width * 0
            .Cols(COL_DisclosureName).Width = .Width * 0
            .Cols(COL_DisclosureDateTime).Width = .Width * 0

            .Cols(COL_NurseNotesID).Width = .Width * 0
            .Cols(COL_NurseNotesName).Width = .Width * 0
            .Cols(COL_NurseNotesDateTime).Width = .Width * 0

            .Cols(COL_WorkersCompFormID).Width = .Width * 0
            .Cols(COL_WorkersCompFormDateOfInjury).Width = .Width * 0
            .Cols(COL_WorkersCompFormType).Width = .Width * 0
            .Cols(COL_WorkersCompFormClaimNo).Width = .Width * 0
            .Cols(COL_WorkersCompFormDOS).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedBy).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedDate).Width = .Width * 0
        End With
    End Sub

    Public Sub Immunization()
        Call setGridImmunization()

        Dim objImmunization As New gloStream.Immunization.Transaction

        Dim dt As DataTable
        dt = objImmunization.Fill_LockImmunization(gstrClientMachineName, TrnType.Immunization)
        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_ImmunizationID, dt.Rows(i)("im_trn_mst_Id"))
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(i + 1, COL_UserName, dt.Rows(i)("im_trn_mst_UserName"))
                End With

            Next
            dt.Dispose()
            dt = Nothing
        End If
        ' objImmunization.Dispose()
        objImmunization = Nothing

    End Sub

    Public Sub setGridImmunization()
        With C1grdExams
            .Visible = True

            .Cols.Count = Col_Count

            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .AllowEditing = True
            .Width = .Width - 20

            .Cols(COL_Select).Width = .Width * 0.08
            .Cols(COL_Select).AllowEditing = True
            .Cols(COL_Select).DataType = System.Type.GetType("System.Boolean")
            .Cols(COL_Select).Name = "Select"
            .SetData(0, COL_Select, "Select")
            .Cols(COL_Select).TextAlignFixed = TextAlignEnum.CenterCenter
            .Cols(COL_ExamID).Width = 0

            '''''''''
            .Cols(COL_PatientCode).Width = .Width * 0.1
            .Cols(COL_PatientCode).AllowEditing = False
            .Cols(COL_PatientCode).Name = "Patient Code"
            .SetData(0, COL_PatientCode, "Patient Code")
            .Cols(COL_PatientCode).TextAlign = TextAlignEnum.LeftCenter
            '''''''''

            '''''''''
            .Cols(COL_PatientName).Width = .Width * 0.15
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_PatientName).Name = "Patient Name"
            .SetData(0, COL_PatientName, "Patient Name")
            .Cols(COL_PatientName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''

            .Cols(COL_ExamName).Width = 0


            ''''''''''
            .Cols(COL_UserName).Width = .Width * 0.15
            .Cols(COL_UserName).AllowEditing = False
            .Cols(COL_UserName).Name = "User Name"
            .SetData(0, COL_UserName, "User Name")
            .Cols(COL_UserName).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''

            .Cols(COL_MachineName).Width = 0
            .Cols(COL_dtDOS).Width = 0
            .Cols(COL_IsFinished).Width = 0
            .Cols(COL_PatientID).Width = 0
            .Cols(COL_VisitID).Width = 0
            .Cols(COL_VisitDate).Width = 0
            .Cols(COL_PrescriptionDateTime).Width = 0
            .Cols(COL_VitalID).Width = 0
            .Cols(COL_VitalDateTime).Width = 0
            .Cols(COL_LabOrderID).Width = 0
            .Cols(COL_OrderDateTime).Width = 0
            .Cols(COL_OrderName).Width = 0
            .Cols(COL_MessageID).Width = 0
            .Cols(COL_MessageDateTime).Width = 0
            .Cols(COL_MessageName).Width = 0
            .Cols(COL_LetterID).Width = 0
            .Cols(COL_LetterDateTime).Width = 0
            .Cols(COL_LetterName).Width = 0
            .Cols(COL_PTProtocolID).Width = 0
            .Cols(COL_PTProtocolDateTime).Width = 0
            .Cols(COL_PTProtocolName).Width = 0
            .Cols(COL_ConsentID).Width = 0
            .Cols(COL_ConsentDateTime).Width = 0
            .Cols(COL_ConsentName).Width = 0
            .Cols(COL_FlowSheetRecordID).Width = 0
            .Cols(COL_FlowSheetName).Width = 0
            .Cols(COL_TaskDate).Width = 0
            .Cols(COL_Subject).Width = 0

            '''''''''''''''''
            .Cols(COL_ImmunizationID).Width = .Width * 0
            .Cols(COL_ImmunizationID).AllowEditing = False
            .Cols(COL_ImmunizationID).Name = "ImmunizationID"
            .SetData(0, COL_ImmunizationID, "ImmunizationID")
            .Cols(COL_ImmunizationID).TextAlign = TextAlignEnum.LeftCenter

            ''''''''''''''
            .Cols(COL_DocumentID).Width = 0
            .Cols(COL_DocumetnName).Width = 0

            .Cols(COL_DisclosureID).Width = .Width * 0
            .Cols(COL_DisclosureName).Width = .Width * 0
            .Cols(COL_DisclosureDateTime).Width = .Width * 0

            .Cols(COL_NurseNotesID).Width = .Width * 0
            .Cols(COL_NurseNotesName).Width = .Width * 0
            .Cols(COL_NurseNotesDateTime).Width = .Width * 0

            .Cols(COL_WorkersCompFormID).Width = .Width * 0
            .Cols(COL_WorkersCompFormDateOfInjury).Width = .Width * 0
            .Cols(COL_WorkersCompFormType).Width = .Width * 0
            .Cols(COL_WorkersCompFormClaimNo).Width = .Width * 0
            .Cols(COL_WorkersCompFormDOS).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedBy).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedDate).Width = .Width * 0
        End With
    End Sub

    Public Sub DMS()
        Call setGridDMS()
        Dim objDMS As New clsDMSCategory
        Dim dt As DataTable = objDMS.Fill_LockDMS(gnClientMachineID, TrnType.DMS)

        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_DocumentID, dt.Rows(i)("DocumentID"))
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(i + 1, COL_UserName, dt.Rows(i)("sUserName"))
                    .SetData(i + 1, COL_DocumetnName, dt.Rows(i)("DocumentName"))
                End With

            Next
            dt.Dispose()
            dt = Nothing
        End If
        ' objDMS.Dispose()
        objDMS = Nothing
    End Sub

    Public Sub setGridDMS()
        With C1grdExams
            .Visible = True

            .Cols.Count = Col_Count

            .Cols.Fixed = 0
            .Rows.Fixed = 1
            .AllowEditing = True
            .Width = .Width - 20

            .Cols(COL_Select).Width = .Width * 0.08
            .Cols(COL_Select).AllowEditing = True
            .Cols(COL_Select).DataType = System.Type.GetType("System.Boolean")
            .Cols(COL_Select).Name = "Select"
            .SetData(0, COL_Select, "Select")
            .Cols(COL_Select).TextAlignFixed = TextAlignEnum.CenterCenter
            .Cols(COL_ExamID).Width = 0

            '''''''''
            .Cols(COL_PatientCode).Width = .Width * 0.1
            .Cols(COL_PatientCode).AllowEditing = False
            .Cols(COL_PatientCode).Name = "Patient Code"
            .SetData(0, COL_PatientCode, "Patient Code")
            .Cols(COL_PatientCode).TextAlign = TextAlignEnum.LeftCenter
            '''''''''

            '''''''''
            .Cols(COL_PatientName).Width = .Width * 0.15
            .Cols(COL_PatientName).AllowEditing = False
            .Cols(COL_PatientName).Name = "Patient Name"
            .SetData(0, COL_PatientName, "Patient Name")
            .Cols(COL_PatientName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''

            .Cols(COL_ExamName).Width = 0


            ''''''''''
            .Cols(COL_UserName).Width = .Width * 0.15
            .Cols(COL_UserName).AllowEditing = False
            .Cols(COL_UserName).Name = "User Name"
            .SetData(0, COL_UserName, "User Name")
            .Cols(COL_UserName).TextAlign = TextAlignEnum.LeftCenter
            ''''''''''

            .Cols(COL_MachineName).Width = 0
            .Cols(COL_dtDOS).Width = 0
            .Cols(COL_IsFinished).Width = 0
            .Cols(COL_PatientID).Width = 0
            .Cols(COL_VisitID).Width = 0
            .Cols(COL_VisitDate).Width = 0
            .Cols(COL_PrescriptionDateTime).Width = 0
            .Cols(COL_VitalID).Width = 0
            .Cols(COL_VitalDateTime).Width = 0
            .Cols(COL_LabOrderID).Width = 0
            .Cols(COL_OrderDateTime).Width = 0
            .Cols(COL_OrderName).Width = 0
            .Cols(COL_MessageID).Width = .Width * 0
            .Cols(COL_MessageDateTime).Width = .Width * 0
            .Cols(COL_MessageName).Width = .Width * 0
            .Cols(COL_LetterID).Width = .Width * 0
            .Cols(COL_LetterDateTime).Width = .Width * 0
            .Cols(COL_LetterName).Width = .Width * 0
            .Cols(COL_PTProtocolID).Width = .Width * 0
            .Cols(COL_PTProtocolDateTime).Width = .Width * 0
            .Cols(COL_PTProtocolName).Width = .Width * 0
            .Cols(COL_ConsentID).Width = .Width * 0
            .Cols(COL_ConsentDateTime).Width = .Width * 0
            .Cols(COL_ConsentName).Width = .Width * 0
            .Cols(COL_FlowSheetRecordID).Width = .Width * 0
            .Cols(COL_FlowSheetName).Width = .Width * 0
            .Cols(COL_TaskDate).Width = .Width * 0
            .Cols(COL_Subject).Width = .Width * 0
            .Cols(COL_ImmunizationID).Width = .Width * 0

            ''''''''''''''
            .Cols(COL_DocumentID).Width = 0
            .Cols(COL_DocumentID).AllowEditing = False
            .Cols(COL_DocumentID).Name = "DocumentID"
            .SetData(0, COL_DocumentID, "DocumentID")
            .Cols(COL_DocumentID).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''''''''''''''
            ''''''''''''''''''
            .Cols(COL_DocumetnName).Width = 0.2
            .Cols(COL_DocumetnName).AllowEditing = False
            .Cols(COL_DocumetnName).Name = "Document Name"
            .SetData(0, COL_DocumetnName, "Document Name")
            .Cols(COL_DocumetnName).TextAlign = TextAlignEnum.LeftCenter
            '''''''''''''

            .Cols(COL_DisclosureID).Width = .Width * 0
            .Cols(COL_DisclosureName).Width = .Width * 0
            .Cols(COL_DisclosureDateTime).Width = .Width * 0

            .Cols(COL_NurseNotesID).Width = .Width * 0
            .Cols(COL_NurseNotesName).Width = .Width * 0
            .Cols(COL_NurseNotesDateTime).Width = .Width * 0

            .Cols(COL_WorkersCompFormID).Width = .Width * 0
            .Cols(COL_WorkersCompFormDateOfInjury).Width = .Width * 0
            .Cols(COL_WorkersCompFormType).Width = .Width * 0
            .Cols(COL_WorkersCompFormClaimNo).Width = .Width * 0
            .Cols(COL_WorkersCompFormDOS).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedBy).Width = .Width * 0
            .Cols(COL_WorkersCompFormCreatedDate).Width = .Width * 0
        End With
    End Sub

    Public Sub ProblemList()
        Call setGrid_PaitentReg()
        Dim objProblemlList As New clsPatientProblemList
        Dim dt As DataTable = objProblemlList.Fill_LockProblemList(gstrClientMachineName, TrnType.ProblemList)

        If Not IsNothing(dt) = True Then
            C1grdExams.Rows.Count = 1
            For i As Integer = 0 To dt.Rows.Count - 1
                With C1grdExams
                    .Rows.Add()
                    .SetData(i + 1, COL_PatientCode, dt.Rows(i)("sPatientCode"))
                    .SetData(i + 1, COL_PatientName, dt.Rows(i)("PatientName"))
                    .SetData(i + 1, COL_UserName, dt.Rows(i)("sUserName"))
                    .SetData(i + 1, COL_PatientID, dt.Rows(i)("nPatientID"))
                End With

            Next
            dt.Dispose()
            dt = Nothing
        End If
        objProblemlList.Dispose()
        objProblemlList = Nothing
    End Sub

    Private Sub WorkersCompForm()
        Dim dtData As DataTable = Nothing

        Try
            setGrid_WorkersCompForm()

            dtData = GetUnlockRecords(gnClientMachineID, gstrClientMachineName, TrnType.WorkersCompForm)

            If Not IsNothing(dtData) Then
                C1grdExams.Rows.Count = 1

                For i As Integer = 0 To dtData.Rows.Count - 1
                    With C1grdExams
                        .Rows.Add()
                        .SetData(i + 1, COL_WorkersCompFormID, dtData.Rows(i)("FormId"))
                        .SetData(i + 1, COL_WorkersCompFormDateOfInjury, dtData.Rows(i)("nInjuryDate"))
                        .SetData(i + 1, COL_WorkersCompFormType, dtData.Rows(i)("FormType"))
                        .SetData(i + 1, COL_WorkersCompFormClaimNo, dtData.Rows(i)("ClaimNo"))
                        .SetData(i + 1, COL_WorkersCompFormDOS, dtData.Rows(i)("DOS"))
                        .SetData(i + 1, COL_WorkersCompFormCreatedBy, dtData.Rows(i)("CreatedBy"))
                        .SetData(i + 1, COL_WorkersCompFormCreatedDate, dtData.Rows(i)("CreatedOn"))
                        .SetData(i + 1, COL_PatientName, dtData.Rows(i)("PatientName"))
                        .SetData(i + 1, COL_UserName, dtData.Rows(i)("sUserName"))
                        .SetData(i + 1, COL_MachineName, dtData.Rows(i)("sMachineName"))
                    End With
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(dtData) Then
                dtData.Dispose()
                dtData = Nothing
            End If
        End Try
    End Sub

    Private Sub setGrid_WorkersCompForm()
        Try
            With C1grdExams
                .Visible = True

                .Cols.Count = Col_Count

                .Cols.Fixed = 0
                .Rows.Fixed = 1
                .AllowEditing = True
                .Width = .Width - 20

                .Cols(COL_Select).Width = .Width * 0.05
                .Cols(COL_Select).AllowEditing = True
                .Cols(COL_Select).DataType = System.Type.GetType("System.Boolean")
                .Cols(COL_Select).Name = "Select"
                .SetData(0, COL_Select, "Select")
                .Cols(COL_Select).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_ExamID).Width = 0

                .Cols(COL_PatientCode).Width = .Width * 0

                .Cols(COL_PatientName).Width = .Width * 0.15
                .Cols(COL_PatientName).AllowEditing = False
                .Cols(COL_PatientName).Name = "Patient Name"
                .SetData(0, COL_PatientName, "Patient Name")
                .Cols(COL_PatientName).TextAlign = TextAlignEnum.LeftCenter

                .Cols(COL_ExamName).Width = 0

                .Cols(COL_UserName).Width = .Width * 0.1
                .Cols(COL_UserName).AllowEditing = False
                .Cols(COL_UserName).Name = "User Name"
                .SetData(0, COL_UserName, "User Name")
                .Cols(COL_UserName).TextAlign = TextAlignEnum.LeftCenter

                .Cols(COL_MachineName).Width = .Width * 0.1
                .Cols(COL_MachineName).AllowEditing = False
                .Cols(COL_MachineName).Name = "Machine Name"
                .SetData(0, COL_MachineName, "Machine Name")
                .Cols(COL_MachineName).TextAlign = TextAlignEnum.LeftCenter

                .Cols(COL_dtDOS).Width = 0
                .Cols(COL_IsFinished).Width = 0
                .Cols(COL_PatientID).Width = 0
                .Cols(COL_VisitID).Width = 0
                .Cols(COL_VisitDate).Width = 0
                .Cols(COL_PrescriptionDateTime).Width = 0
                .Cols(COL_VitalID).Width = 0
                .Cols(COL_VitalDateTime).Width = 0
                .Cols(COL_LabOrderID).Width = 0
                .Cols(COL_OrderDateTime).Width = 0
                .Cols(COL_OrderName).Width = 0
                .Cols(COL_MessageID).Width = .Width * 0
                .Cols(COL_MessageDateTime).Width = .Width * 0
                .Cols(COL_MessageName).Width = .Width * 0
                .Cols(COL_LetterID).Width = .Width * 0
                .Cols(COL_LetterDateTime).Width = .Width * 0
                .Cols(COL_LetterName).Width = .Width * 0
                .Cols(COL_PTProtocolID).Width = .Width * 0
                .Cols(COL_PTProtocolDateTime).Width = .Width * 0
                .Cols(COL_PTProtocolName).Width = .Width * 0
                .Cols(COL_ConsentID).Width = .Width * 0
                .Cols(COL_ConsentDateTime).Width = .Width * 0
                .Cols(COL_ConsentName).Width = .Width * 0
                .Cols(COL_FlowSheetRecordID).Width = .Width * 0
                .Cols(COL_FlowSheetName).Width = .Width * 0
                .Cols(COL_TaskDate).Width = .Width * 0
                .Cols(COL_Subject).Width = .Width * 0
                .Cols(COL_ImmunizationID).Width = .Width * 0
                .Cols(COL_DocumentID).Width = 0
                .Cols(COL_DocumetnName).Width = 0
                .Cols(COL_DisclosureID).Width = .Width * 0
                .Cols(COL_DisclosureName).Width = .Width * 0
                .Cols(COL_DisclosureDateTime).Width = .Width * 0
                .Cols(COL_NurseNotesID).Width = .Width * 0
                .Cols(COL_NurseNotesName).Width = .Width * 0
                .Cols(COL_NurseNotesDateTime).Width = .Width * 0

                .Cols(COL_WorkersCompFormID).Width = 0
                .Cols(COL_WorkersCompFormID).AllowEditing = False
                .Cols(COL_WorkersCompFormID).Name = "Form ID"
                .SetData(0, COL_WorkersCompFormID, "Form ID")
                .Cols(COL_WorkersCompFormID).TextAlign = TextAlignEnum.LeftCenter

                .Cols(COL_WorkersCompFormDateOfInjury).Width = .Width * 0.1
                .Cols(COL_WorkersCompFormDateOfInjury).AllowEditing = False
                .Cols(COL_WorkersCompFormDateOfInjury).Name = "Date Of Injury"
                .SetData(0, COL_WorkersCompFormDateOfInjury, "Date Of Injury")
                .Cols(COL_WorkersCompFormDateOfInjury).TextAlign = TextAlignEnum.LeftCenter

                .Cols(COL_WorkersCompFormType).Width = .Width * 0.1
                .Cols(COL_WorkersCompFormType).AllowEditing = False
                .Cols(COL_WorkersCompFormType).Name = "Form Type"
                .SetData(0, COL_WorkersCompFormType, "Form Type")
                .Cols(COL_WorkersCompFormType).TextAlign = TextAlignEnum.LeftCenter

                .Cols(COL_WorkersCompFormClaimNo).Width = .Width * 0.1
                .Cols(COL_WorkersCompFormClaimNo).AllowEditing = False
                .Cols(COL_WorkersCompFormClaimNo).Name = "Claim #"
                .SetData(0, COL_WorkersCompFormClaimNo, "Claim #")
                .Cols(COL_WorkersCompFormClaimNo).TextAlign = TextAlignEnum.LeftCenter

                .Cols(COL_WorkersCompFormDOS).Width = .Width * 0.1
                .Cols(COL_WorkersCompFormDOS).AllowEditing = False
                .Cols(COL_WorkersCompFormDOS).Name = "DOS"
                .SetData(0, COL_WorkersCompFormDOS, "DOS")
                .Cols(COL_WorkersCompFormDOS).TextAlign = TextAlignEnum.LeftCenter

                .Cols(COL_WorkersCompFormCreatedBy).Width = .Width * 0.1
                .Cols(COL_WorkersCompFormCreatedBy).AllowEditing = False
                .Cols(COL_WorkersCompFormCreatedBy).Name = "Created By"
                .SetData(0, COL_WorkersCompFormCreatedBy, "Created By")
                .Cols(COL_WorkersCompFormCreatedBy).TextAlign = TextAlignEnum.LeftCenter

                .Cols(COL_WorkersCompFormCreatedDate).Width = .Width * 0.1
                .Cols(COL_WorkersCompFormCreatedDate).AllowEditing = False
                .Cols(COL_WorkersCompFormCreatedDate).Name = "Created Date"
                .SetData(0, COL_WorkersCompFormCreatedDate, "Created Date")
                .Cols(COL_WorkersCompFormCreatedDate).TextAlign = TextAlignEnum.LeftCenter
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function GetUnlockRecords(ByVal MachinID As String, ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Dim oResultTable As DataTable = Nothing

        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@sMachinName"
            oParamater.Value = MachinName
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nTrnType"
            oParamater.Value = TransactionType
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nMachinID"
            oParamater.Value = MachinID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oResultTable = oDB.GetDataTable("gsp_Select_UnLock_Record")

            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oParamater = Nothing

            If Not IsNothing(oResultTable) Then
                oResultTable.Dispose()
                oResultTable = Nothing
            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Private Sub C1grdExams_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1grdExams.Click

    End Sub

    Private Sub C1grdExams_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1grdExams.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class
