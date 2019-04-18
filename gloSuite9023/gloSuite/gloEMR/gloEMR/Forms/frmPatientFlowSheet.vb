Imports System.IO
Imports C1.Win.C1FlexGrid
Imports System.Drawing.Color
Imports System.Drawing.Printing
Imports gloUserControlLibrary
Imports Word = Microsoft.Office.Interop.Word

Public Class frmPatientFlowSheet
    'Inherits System.Windows.Forms.Form
    Inherits frmBaseForm

    Implements IPatientContext

    Dim objFlowSheet As New clsFlowSheet

    Private _PatientID As Long
    Private _FlowSheetID As Long
    Private _FlowSheetName As String

    Private blnModify As Boolean
    Private storedPageSettings As PageSettings
    Private PrintPreviewDialog1 As New PrintPreviewDialog
    Private PrintDialog1 As New PrintDialog
    Private GridPrinter As DataGridPrinter
    Private dtgrid As DataTable

    Dim dtStructure As DataTable '' This stores Structure of FlowSheet.. Used to Fetch Structure & same dt Used while saving flowsheet.

    Private Arrlist As ArrayList
    Dim strFlowSheetName As String
    Private _FlowSheetRecordID As Long
    Dim intFlowSheetID As Long
    Dim _blnRecordLock As Boolean = False
    Dim dt As DataTable
    Dim cnt As Int64 = 0
    Dim ht As Int16 = 25
    Dim cntwidth As Int16 = 0
    Dim blnSave As Boolean = True
    Private dtFlowSheet As DataTable '' USED TO STORE FLOWSHEET LOCALY WHILE NAVIGATING TO OTHER FLOWSHEETS ''
    Private _VisitID As Int64 = 0
    Private _FlowSheetModified As Boolean = False
    Private _IsLoading As Boolean = False
    ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
    Public Shared Array_Flow_List As ArrayList = Nothing
    ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
    Public Delegate Sub SaveFlowSheet()             'Added by kanchan on 20100619 for Flowsheet
    Public Event EvnSaveFlowSheet As SaveFlowSheet  'Added by kanchan on 20100619 for Flowsheet
    Private strPatientCode As String
    Private strPatientFirstName As String
    Private strPatientMiddleName As String
    Private strPatientLastName As String
    Private strPatientDOB As String
    Private strPatientAge As String
    Private strPatientGender As String
    Private strPatientMaritalStatus As String
    'For Holding Deleted FlowSheet record row Id
    Public dtFlowSheetDeletedRowId As DataTable = Nothing
    Private FormLevelLockID As Long = -1 '' To the set record lock
    Dim _blnFormLock As Boolean = False


    Dim sFlwShtFilePath As String = ""
    Dim CopiedbyCCQPrint As Boolean = False
    Dim blnPrint As Boolean = False '''''if print or print preview button is click, if print button then True if Printpreview then false
#Region " Windows Controls "
    'Friend WithEvents tlsFlowSheet As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsFlowSheet As gloToolStrip.gloToolStrip
    Friend WithEvents tls_btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_btnPreview As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_btnSetting As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_btnFinish As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tls_btnShow As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlFlowSheetHistory As System.Windows.Forms.Panel
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlFlowSheetHistoryBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlFlowSheetHistoryLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlFlowSheetHistoryRightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlFlowSheetHistoryTopBrd As System.Windows.Forms.Label
    Friend WithEvents pnl_Grid As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlGridBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlGridLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlGridRightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlGridTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlcomboBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlcomboLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlcomboRightBrd As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents btnClearSearchFlowsheetHistory As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlcomboTopBrd As System.Windows.Forms.Label
#End Region

#Region " Windows Form Designer generated code "
    'constructors commented as not in use 
    'Public Sub New()
    '    MyBase.New()

    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub

    'Public Sub New(ByVal PatientID As Long, ByVal FlowSheetID As Long)
    '    MyBase.New()
    '    'm_VisitID = VisitID
    '    _PatientID = PatientID
    '    _FlowSheetID = FlowSheetID

    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub

    Public Sub New(ByVal patientID As Long, ByVal flowSheetName As String)
        MyBase.New()
        _PatientID = patientID
        _FlowSheetName = flowSheetName
        InitializeComponent()
    End Sub

    Public Sub New(ByVal patientID As Long)
        '''''Constructor created by Ujwala on 20101013 for Smart diagnosis changes 
        MyBase.New()
        _PatientID = patientID
        InitializeComponent()
        '''''Constructor created by Ujwala on 20101013 for Smart diagnosis changes 
    End Sub
    'Public Sub New()
    '    '''''Constructor created by Ujwala on 20101013 for Smart diagnosis changes 
    '    MyBase.New()

    '    InitializeComponent()
    '    '''''Constructor created by Ujwala on 20101013 for Smart diagnosis changes 
    'End Sub

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
            
            Dim dtpControls As ContextMenu() = {cntFlowSheetHistory}
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                gloGlobal.cEventHelper.DisposeContextMenu(dtpControls)
            Catch ex As Exception

            End Try
            Try
                If (IsNothing(PrintPreviewDialog1) = False) Then
                    PrintPreviewDialog1.Dispose()
                    PrintPreviewDialog1 = Nothing
                End If
            Catch ex As Exception

            End Try

            Try
                If (IsNothing(PrintDialog1) = False) Then
                    PrintDialog1.Dispose()
                    PrintDialog1 = Nothing
                End If
            Catch ex As Exception

            End Try
            Try
                If (IsNothing(gloUC_PatientStrip1) = False) Then
                    gloUC_PatientStrip1.Dispose()
                    gloUC_PatientStrip1 = Nothing
                End If
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
    Friend WithEvents ToolBarButton1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents cntFlowSheetHistory As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuDeleteFlowSheet As System.Windows.Forms.MenuItem
    Friend WithEvents ToolBarButton2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents pnlPrevFlowSheet As System.Windows.Forms.Panel
    Friend WithEvents trFlowsheetHistory As System.Windows.Forms.TreeView
    Friend WithEvents txtSearchFlowsheetHistory As System.Windows.Forms.TextBox
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents pnlGrid As System.Windows.Forms.Panel
    Friend WithEvents C1FlexGrid1 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlcombo As System.Windows.Forms.Panel
    Friend WithEvents lblVisitDate As System.Windows.Forms.Label
    Friend WithEvents lbl_VisitDate As System.Windows.Forms.Label
    Friend WithEvents lblPatientName As System.Windows.Forms.Label
    Friend WithEvents lbl_PatientName As System.Windows.Forms.Label
    Friend WithEvents lblPatientCode As System.Windows.Forms.Label
    Friend WithEvents lblPatient As System.Windows.Forms.Label
    Friend WithEvents imgFlowSheet As System.Windows.Forms.ImageList
    Friend WithEvents Spt_PatientFlowSheetLeft As System.Windows.Forms.Splitter
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientFlowSheet))
        Me.imgFlowSheet = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolBarButton1 = New System.Windows.Forms.ToolBarButton()
        Me.cntFlowSheetHistory = New System.Windows.Forms.ContextMenu()
        Me.mnuDeleteFlowSheet = New System.Windows.Forms.MenuItem()
        Me.ToolBarButton2 = New System.Windows.Forms.ToolBarButton()
        Me.pnlPrevFlowSheet = New System.Windows.Forms.Panel()
        Me.pnlFlowSheetHistory = New System.Windows.Forms.Panel()
        Me.trFlowsheetHistory = New System.Windows.Forms.TreeView()
        Me.lbl_pnlFlowSheetHistoryBottomBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlFlowSheetHistoryLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlFlowSheetHistoryRightBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlFlowSheetHistoryTopBrd = New System.Windows.Forms.Label()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtSearchFlowsheetHistory = New System.Windows.Forms.TextBox()
        Me.btnClearSearchFlowsheetHistory = New System.Windows.Forms.Button()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.PicBx_Search = New System.Windows.Forms.PictureBox()
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tlsFlowSheet = New gloToolStrip.gloToolStrip()
        Me.tls_btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.tls_btnPreview = New System.Windows.Forms.ToolStripButton()
        Me.tls_btnSetting = New System.Windows.Forms.ToolStripButton()
        Me.tls_btnShow = New System.Windows.Forms.ToolStripButton()
        Me.tls_btnFinish = New System.Windows.Forms.ToolStripButton()
        Me.tls_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.pnl_Grid = New System.Windows.Forms.Panel()
        Me.C1FlexGrid1 = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.lbl_pnlGridBottomBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlGridLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlGridRightBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlGridTopBrd = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.pnlcombo = New System.Windows.Forms.Panel()
        Me.lbl_pnlcomboBottomBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlcomboLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlcomboRightBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlcomboTopBrd = New System.Windows.Forms.Label()
        Me.lblVisitDate = New System.Windows.Forms.Label()
        Me.lbl_VisitDate = New System.Windows.Forms.Label()
        Me.lblPatientName = New System.Windows.Forms.Label()
        Me.lbl_PatientName = New System.Windows.Forms.Label()
        Me.lblPatientCode = New System.Windows.Forms.Label()
        Me.lblPatient = New System.Windows.Forms.Label()
        Me.Spt_PatientFlowSheetLeft = New System.Windows.Forms.Splitter()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlPrevFlowSheet.SuspendLayout()
        Me.pnlFlowSheetHistory.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.tlsFlowSheet.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        Me.pnl_Grid.SuspendLayout()
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnlcombo.SuspendLayout()
        Me.SuspendLayout()
        '
        'imgFlowSheet
        '
        Me.imgFlowSheet.ImageStream = CType(resources.GetObject("imgFlowSheet.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgFlowSheet.TransparentColor = System.Drawing.Color.Transparent
        Me.imgFlowSheet.Images.SetKeyName(0, "FLow sheet.ico")
        Me.imgFlowSheet.Images.SetKeyName(1, "Bullet06.ico")
        '
        'ToolBarButton1
        '
        Me.ToolBarButton1.ImageIndex = 5
        Me.ToolBarButton1.Name = "ToolBarButton1"
        Me.ToolBarButton1.Text = "Show"
        '
        'cntFlowSheetHistory
        '
        Me.cntFlowSheetHistory.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDeleteFlowSheet})
        '
        'mnuDeleteFlowSheet
        '
        Me.mnuDeleteFlowSheet.Index = 0
        Me.mnuDeleteFlowSheet.Text = "Delete Flowsheet Records"
        '
        'ToolBarButton2
        '
        Me.ToolBarButton2.Name = "ToolBarButton2"
        Me.ToolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'pnlPrevFlowSheet
        '
        Me.pnlPrevFlowSheet.Controls.Add(Me.pnlFlowSheetHistory)
        Me.pnlPrevFlowSheet.Controls.Add(Me.pnlSearch)
        Me.pnlPrevFlowSheet.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlPrevFlowSheet.Location = New System.Drawing.Point(0, 56)
        Me.pnlPrevFlowSheet.Name = "pnlPrevFlowSheet"
        Me.pnlPrevFlowSheet.Size = New System.Drawing.Size(216, 510)
        Me.pnlPrevFlowSheet.TabIndex = 14
        '
        'pnlFlowSheetHistory
        '
        Me.pnlFlowSheetHistory.Controls.Add(Me.trFlowsheetHistory)
        Me.pnlFlowSheetHistory.Controls.Add(Me.lbl_pnlFlowSheetHistoryBottomBrd)
        Me.pnlFlowSheetHistory.Controls.Add(Me.lbl_pnlFlowSheetHistoryLeftBrd)
        Me.pnlFlowSheetHistory.Controls.Add(Me.lbl_pnlFlowSheetHistoryRightBrd)
        Me.pnlFlowSheetHistory.Controls.Add(Me.lbl_pnlFlowSheetHistoryTopBrd)
        Me.pnlFlowSheetHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFlowSheetHistory.Location = New System.Drawing.Point(0, 26)
        Me.pnlFlowSheetHistory.Name = "pnlFlowSheetHistory"
        Me.pnlFlowSheetHistory.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlFlowSheetHistory.Size = New System.Drawing.Size(216, 484)
        Me.pnlFlowSheetHistory.TabIndex = 17
        '
        'trFlowsheetHistory
        '
        Me.trFlowsheetHistory.BackColor = System.Drawing.Color.White
        Me.trFlowsheetHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trFlowsheetHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trFlowsheetHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trFlowsheetHistory.ForeColor = System.Drawing.Color.Black
        Me.trFlowsheetHistory.HideSelection = False
        Me.trFlowsheetHistory.ImageIndex = 0
        Me.trFlowsheetHistory.ImageList = Me.imgFlowSheet
        Me.trFlowsheetHistory.Indent = 19
        Me.trFlowsheetHistory.ItemHeight = 20
        Me.trFlowsheetHistory.Location = New System.Drawing.Point(4, 1)
        Me.trFlowsheetHistory.Name = "trFlowsheetHistory"
        Me.trFlowsheetHistory.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.trFlowsheetHistory.SelectedImageIndex = 0
        Me.trFlowsheetHistory.ShowLines = False
        Me.trFlowsheetHistory.Size = New System.Drawing.Size(211, 479)
        Me.trFlowsheetHistory.TabIndex = 1
        '
        'lbl_pnlFlowSheetHistoryBottomBrd
        '
        Me.lbl_pnlFlowSheetHistoryBottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlFlowSheetHistoryBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlFlowSheetHistoryBottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_pnlFlowSheetHistoryBottomBrd.Location = New System.Drawing.Point(4, 480)
        Me.lbl_pnlFlowSheetHistoryBottomBrd.Name = "lbl_pnlFlowSheetHistoryBottomBrd"
        Me.lbl_pnlFlowSheetHistoryBottomBrd.Size = New System.Drawing.Size(211, 1)
        Me.lbl_pnlFlowSheetHistoryBottomBrd.TabIndex = 8
        Me.lbl_pnlFlowSheetHistoryBottomBrd.Text = "label2"
        '
        'lbl_pnlFlowSheetHistoryLeftBrd
        '
        Me.lbl_pnlFlowSheetHistoryLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlFlowSheetHistoryLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlFlowSheetHistoryLeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pnlFlowSheetHistoryLeftBrd.Location = New System.Drawing.Point(3, 1)
        Me.lbl_pnlFlowSheetHistoryLeftBrd.Name = "lbl_pnlFlowSheetHistoryLeftBrd"
        Me.lbl_pnlFlowSheetHistoryLeftBrd.Size = New System.Drawing.Size(1, 480)
        Me.lbl_pnlFlowSheetHistoryLeftBrd.TabIndex = 7
        Me.lbl_pnlFlowSheetHistoryLeftBrd.Text = "label4"
        '
        'lbl_pnlFlowSheetHistoryRightBrd
        '
        Me.lbl_pnlFlowSheetHistoryRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlFlowSheetHistoryRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlFlowSheetHistoryRightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_pnlFlowSheetHistoryRightBrd.Location = New System.Drawing.Point(215, 1)
        Me.lbl_pnlFlowSheetHistoryRightBrd.Name = "lbl_pnlFlowSheetHistoryRightBrd"
        Me.lbl_pnlFlowSheetHistoryRightBrd.Size = New System.Drawing.Size(1, 480)
        Me.lbl_pnlFlowSheetHistoryRightBrd.TabIndex = 6
        Me.lbl_pnlFlowSheetHistoryRightBrd.Text = "label3"
        '
        'lbl_pnlFlowSheetHistoryTopBrd
        '
        Me.lbl_pnlFlowSheetHistoryTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlFlowSheetHistoryTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlFlowSheetHistoryTopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pnlFlowSheetHistoryTopBrd.Location = New System.Drawing.Point(3, 0)
        Me.lbl_pnlFlowSheetHistoryTopBrd.Name = "lbl_pnlFlowSheetHistoryTopBrd"
        Me.lbl_pnlFlowSheetHistoryTopBrd.Size = New System.Drawing.Size(213, 1)
        Me.lbl_pnlFlowSheetHistoryTopBrd.TabIndex = 5
        Me.lbl_pnlFlowSheetHistoryTopBrd.Text = "label1"
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSearch.Controls.Add(Me.txtSearchFlowsheetHistory)
        Me.pnlSearch.Controls.Add(Me.btnClearSearchFlowsheetHistory)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlSearch.Controls.Add(Me.PicBx_Search)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchBottomBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchTopBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlSearch.Size = New System.Drawing.Size(216, 26)
        Me.pnlSearch.TabIndex = 16
        '
        'txtSearchFlowsheetHistory
        '
        Me.txtSearchFlowsheetHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchFlowsheetHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchFlowsheetHistory.ForeColor = System.Drawing.Color.Black
        Me.txtSearchFlowsheetHistory.Location = New System.Drawing.Point(32, 5)
        Me.txtSearchFlowsheetHistory.Name = "txtSearchFlowsheetHistory"
        Me.txtSearchFlowsheetHistory.Size = New System.Drawing.Size(162, 15)
        Me.txtSearchFlowsheetHistory.TabIndex = 2
        '
        'btnClearSearchFlowsheetHistory
        '
        Me.btnClearSearchFlowsheetHistory.BackColor = System.Drawing.Color.Transparent
        Me.btnClearSearchFlowsheetHistory.BackgroundImage = CType(resources.GetObject("btnClearSearchFlowsheetHistory.BackgroundImage"), System.Drawing.Image)
        Me.btnClearSearchFlowsheetHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearSearchFlowsheetHistory.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearSearchFlowsheetHistory.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearSearchFlowsheetHistory.FlatAppearance.BorderSize = 0
        Me.btnClearSearchFlowsheetHistory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearSearchFlowsheetHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearSearchFlowsheetHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearSearchFlowsheetHistory.Image = CType(resources.GetObject("btnClearSearchFlowsheetHistory.Image"), System.Drawing.Image)
        Me.btnClearSearchFlowsheetHistory.Location = New System.Drawing.Point(194, 5)
        Me.btnClearSearchFlowsheetHistory.Name = "btnClearSearchFlowsheetHistory"
        Me.btnClearSearchFlowsheetHistory.Size = New System.Drawing.Size(21, 15)
        Me.btnClearSearchFlowsheetHistory.TabIndex = 48
        Me.ToolTip1.SetToolTip(Me.btnClearSearchFlowsheetHistory, "Clear search")
        Me.btnClearSearchFlowsheetHistory.UseVisualStyleBackColor = False
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(32, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(183, 4)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(32, 20)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(183, 2)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'PicBx_Search
        '
        Me.PicBx_Search.BackColor = System.Drawing.Color.White
        Me.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicBx_Search.Image = CType(resources.GetObject("PicBx_Search.Image"), System.Drawing.Image)
        Me.PicBx_Search.Location = New System.Drawing.Point(4, 1)
        Me.PicBx_Search.Name = "PicBx_Search"
        Me.PicBx_Search.Size = New System.Drawing.Size(28, 21)
        Me.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicBx_Search.TabIndex = 9
        Me.PicBx_Search.TabStop = False
        '
        'lbl_pnlSearchBottomBrd
        '
        Me.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlSearchBottomBrd.Location = New System.Drawing.Point(4, 22)
        Me.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd"
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(211, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(4, 0)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(211, 1)
        Me.lbl_pnlSearchTopBrd.TabIndex = 36
        Me.lbl_pnlSearchTopBrd.Text = "label1"
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(3, 0)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(215, 0)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlToolStrip.Controls.Add(Me.tlsFlowSheet)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(984, 56)
        Me.pnlToolStrip.TabIndex = 19
        '
        'tlsFlowSheet
        '
        Me.tlsFlowSheet.AddSeparatorsBetweenEachButton = False
        Me.tlsFlowSheet.BackColor = System.Drawing.Color.Transparent
        Me.tlsFlowSheet.BackgroundImage = CType(resources.GetObject("tlsFlowSheet.BackgroundImage"), System.Drawing.Image)
        Me.tlsFlowSheet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsFlowSheet.ButtonsToHide = CType(resources.GetObject("tlsFlowSheet.ButtonsToHide"), System.Collections.ArrayList)
        Me.tlsFlowSheet.ConnectionString = Nothing
        Me.tlsFlowSheet.CustomizeButtonNameType = gloToolStrip.gloToolStrip.enumButtonNameType.ShowToolTipText
        Me.tlsFlowSheet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsFlowSheet.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tlsFlowSheet.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsFlowSheet.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tls_btnPrint, Me.tls_btnPreview, Me.tls_btnSetting, Me.tls_btnShow, Me.tls_btnFinish, Me.tls_btnClose})
        Me.tlsFlowSheet.Location = New System.Drawing.Point(0, 0)
        Me.tlsFlowSheet.ModuleName = Nothing
        Me.tlsFlowSheet.Name = "tlsFlowSheet"
        Me.tlsFlowSheet.Size = New System.Drawing.Size(984, 53)
        Me.tlsFlowSheet.TabIndex = 0
        Me.tlsFlowSheet.Text = "ToolStrip1"
        Me.tlsFlowSheet.UserID = CType(0, Long)
        '
        'tls_btnPrint
        '
        Me.tls_btnPrint.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_btnPrint.Image = CType(resources.GetObject("tls_btnPrint.Image"), System.Drawing.Image)
        Me.tls_btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_btnPrint.Name = "tls_btnPrint"
        Me.tls_btnPrint.Size = New System.Drawing.Size(45, 50)
        Me.tls_btnPrint.Tag = "Print"
        Me.tls_btnPrint.Text = "&Print "
        Me.tls_btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_btnPrint.ToolTipText = "Print"
        '
        'tls_btnPreview
        '
        Me.tls_btnPreview.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_btnPreview.Image = CType(resources.GetObject("tls_btnPreview.Image"), System.Drawing.Image)
        Me.tls_btnPreview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_btnPreview.Name = "tls_btnPreview"
        Me.tls_btnPreview.Size = New System.Drawing.Size(59, 50)
        Me.tls_btnPreview.Tag = "Preview"
        Me.tls_btnPreview.Text = "Pre&view"
        Me.tls_btnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tls_btnSetting
        '
        Me.tls_btnSetting.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_btnSetting.Image = CType(resources.GetObject("tls_btnSetting.Image"), System.Drawing.Image)
        Me.tls_btnSetting.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_btnSetting.Name = "tls_btnSetting"
        Me.tls_btnSetting.Size = New System.Drawing.Size(57, 50)
        Me.tls_btnSetting.Tag = "Setting"
        Me.tls_btnSetting.Text = "&Setting"
        Me.tls_btnSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tls_btnShow
        '
        Me.tls_btnShow.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_btnShow.Image = CType(resources.GetObject("tls_btnShow.Image"), System.Drawing.Image)
        Me.tls_btnShow.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_btnShow.Name = "tls_btnShow"
        Me.tls_btnShow.Size = New System.Drawing.Size(46, 50)
        Me.tls_btnShow.Tag = "Show"
        Me.tls_btnShow.Text = "Sho&w"
        Me.tls_btnShow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tls_btnFinish
        '
        Me.tls_btnFinish.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_btnFinish.Image = CType(resources.GetObject("tls_btnFinish.Image"), System.Drawing.Image)
        Me.tls_btnFinish.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_btnFinish.Name = "tls_btnFinish"
        Me.tls_btnFinish.Size = New System.Drawing.Size(66, 50)
        Me.tls_btnFinish.Tag = "Finish"
        Me.tls_btnFinish.Text = "S&ave&&Cls"
        Me.tls_btnFinish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_btnFinish.ToolTipText = "Save and Close"
        '
        'tls_btnClose
        '
        Me.tls_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_btnClose.Image = CType(resources.GetObject("tls_btnClose.Image"), System.Drawing.Image)
        Me.tls_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tls_btnClose.Name = "tls_btnClose"
        Me.tls_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.tls_btnClose.Tag = "Close"
        Me.tls_btnClose.Text = "&Close"
        Me.tls_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tls_btnClose.ToolTipText = "Close"
        '
        'pnlGrid
        '
        Me.pnlGrid.Controls.Add(Me.pnl_Grid)
        Me.pnlGrid.Controls.Add(Me.Panel1)
        Me.pnlGrid.Controls.Add(Me.pnlcombo)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.Location = New System.Drawing.Point(219, 56)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Size = New System.Drawing.Size(765, 510)
        Me.pnlGrid.TabIndex = 20
        '
        'pnl_Grid
        '
        Me.pnl_Grid.Controls.Add(Me.C1FlexGrid1)
        Me.pnl_Grid.Controls.Add(Me.lbl_pnlGridBottomBrd)
        Me.pnl_Grid.Controls.Add(Me.lbl_pnlGridLeftBrd)
        Me.pnl_Grid.Controls.Add(Me.lbl_pnlGridRightBrd)
        Me.pnl_Grid.Controls.Add(Me.lbl_pnlGridTopBrd)
        Me.pnl_Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Grid.Location = New System.Drawing.Point(0, 36)
        Me.pnl_Grid.Name = "pnl_Grid"
        Me.pnl_Grid.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnl_Grid.Size = New System.Drawing.Size(765, 474)
        Me.pnl_Grid.TabIndex = 8
        '
        'C1FlexGrid1
        '
        Me.C1FlexGrid1.AllowAddNew = True
        Me.C1FlexGrid1.AllowDelete = True
        Me.C1FlexGrid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1FlexGrid1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Rows
        Me.C1FlexGrid1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1FlexGrid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1FlexGrid1.ColumnInfo = "8,1,0,0,0,95,Columns:0{Width:34;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1FlexGrid1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1FlexGrid1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1FlexGrid1.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1FlexGrid1.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1FlexGrid1.Location = New System.Drawing.Point(1, 1)
        Me.C1FlexGrid1.Name = "C1FlexGrid1"
        Me.C1FlexGrid1.Rows.Count = 16
        Me.C1FlexGrid1.Rows.DefaultSize = 19
        Me.C1FlexGrid1.Size = New System.Drawing.Size(760, 469)
        Me.C1FlexGrid1.StyleInfo = resources.GetString("C1FlexGrid1.StyleInfo")
        Me.C1FlexGrid1.TabIndex = 7
        '
        'lbl_pnlGridBottomBrd
        '
        Me.lbl_pnlGridBottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlGridBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlGridBottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_pnlGridBottomBrd.Location = New System.Drawing.Point(1, 470)
        Me.lbl_pnlGridBottomBrd.Name = "lbl_pnlGridBottomBrd"
        Me.lbl_pnlGridBottomBrd.Size = New System.Drawing.Size(760, 1)
        Me.lbl_pnlGridBottomBrd.TabIndex = 8
        Me.lbl_pnlGridBottomBrd.Text = "label2"
        '
        'lbl_pnlGridLeftBrd
        '
        Me.lbl_pnlGridLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlGridLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlGridLeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pnlGridLeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlGridLeftBrd.Name = "lbl_pnlGridLeftBrd"
        Me.lbl_pnlGridLeftBrd.Size = New System.Drawing.Size(1, 470)
        Me.lbl_pnlGridLeftBrd.TabIndex = 7
        Me.lbl_pnlGridLeftBrd.Text = "label4"
        '
        'lbl_pnlGridRightBrd
        '
        Me.lbl_pnlGridRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlGridRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlGridRightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_pnlGridRightBrd.Location = New System.Drawing.Point(761, 1)
        Me.lbl_pnlGridRightBrd.Name = "lbl_pnlGridRightBrd"
        Me.lbl_pnlGridRightBrd.Size = New System.Drawing.Size(1, 470)
        Me.lbl_pnlGridRightBrd.TabIndex = 6
        Me.lbl_pnlGridRightBrd.Text = "label3"
        '
        'lbl_pnlGridTopBrd
        '
        Me.lbl_pnlGridTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlGridTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlGridTopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pnlGridTopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlGridTopBrd.Name = "lbl_pnlGridTopBrd"
        Me.lbl_pnlGridTopBrd.Size = New System.Drawing.Size(762, 1)
        Me.lbl_pnlGridTopBrd.TabIndex = 5
        Me.lbl_pnlGridTopBrd.Text = "label1"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(765, 36)
        Me.Panel1.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(760, 1)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 29)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(761, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 29)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(762, 1)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "label1"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(19, 11)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(114, 14)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Flowsheet Name :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(140, 11)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 14)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "1002"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlcombo
        '
        Me.pnlcombo.BackColor = System.Drawing.Color.Transparent
        Me.pnlcombo.Controls.Add(Me.lbl_pnlcomboBottomBrd)
        Me.pnlcombo.Controls.Add(Me.lbl_pnlcomboLeftBrd)
        Me.pnlcombo.Controls.Add(Me.lbl_pnlcomboRightBrd)
        Me.pnlcombo.Controls.Add(Me.lbl_pnlcomboTopBrd)
        Me.pnlcombo.Controls.Add(Me.lblVisitDate)
        Me.pnlcombo.Controls.Add(Me.lbl_VisitDate)
        Me.pnlcombo.Controls.Add(Me.lblPatientName)
        Me.pnlcombo.Controls.Add(Me.lbl_PatientName)
        Me.pnlcombo.Controls.Add(Me.lblPatientCode)
        Me.pnlcombo.Controls.Add(Me.lblPatient)
        Me.pnlcombo.Location = New System.Drawing.Point(81, 121)
        Me.pnlcombo.Name = "pnlcombo"
        Me.pnlcombo.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlcombo.Size = New System.Drawing.Size(765, 59)
        Me.pnlcombo.TabIndex = 5
        '
        'lbl_pnlcomboBottomBrd
        '
        Me.lbl_pnlcomboBottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlcomboBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlcomboBottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_pnlcomboBottomBrd.Location = New System.Drawing.Point(1, 55)
        Me.lbl_pnlcomboBottomBrd.Name = "lbl_pnlcomboBottomBrd"
        Me.lbl_pnlcomboBottomBrd.Size = New System.Drawing.Size(760, 1)
        Me.lbl_pnlcomboBottomBrd.TabIndex = 24
        Me.lbl_pnlcomboBottomBrd.Text = "label2"
        '
        'lbl_pnlcomboLeftBrd
        '
        Me.lbl_pnlcomboLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlcomboLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlcomboLeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pnlcomboLeftBrd.Location = New System.Drawing.Point(0, 4)
        Me.lbl_pnlcomboLeftBrd.Name = "lbl_pnlcomboLeftBrd"
        Me.lbl_pnlcomboLeftBrd.Size = New System.Drawing.Size(1, 52)
        Me.lbl_pnlcomboLeftBrd.TabIndex = 23
        Me.lbl_pnlcomboLeftBrd.Text = "label4"
        '
        'lbl_pnlcomboRightBrd
        '
        Me.lbl_pnlcomboRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlcomboRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlcomboRightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_pnlcomboRightBrd.Location = New System.Drawing.Point(761, 4)
        Me.lbl_pnlcomboRightBrd.Name = "lbl_pnlcomboRightBrd"
        Me.lbl_pnlcomboRightBrd.Size = New System.Drawing.Size(1, 52)
        Me.lbl_pnlcomboRightBrd.TabIndex = 22
        Me.lbl_pnlcomboRightBrd.Text = "label3"
        '
        'lbl_pnlcomboTopBrd
        '
        Me.lbl_pnlcomboTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlcomboTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlcomboTopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pnlcomboTopBrd.Location = New System.Drawing.Point(0, 3)
        Me.lbl_pnlcomboTopBrd.Name = "lbl_pnlcomboTopBrd"
        Me.lbl_pnlcomboTopBrd.Size = New System.Drawing.Size(762, 1)
        Me.lbl_pnlcomboTopBrd.TabIndex = 21
        Me.lbl_pnlcomboTopBrd.Text = "label1"
        '
        'lblVisitDate
        '
        Me.lblVisitDate.AutoSize = True
        Me.lblVisitDate.BackColor = System.Drawing.Color.Transparent
        Me.lblVisitDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVisitDate.Location = New System.Drawing.Point(436, 10)
        Me.lblVisitDate.Name = "lblVisitDate"
        Me.lblVisitDate.Size = New System.Drawing.Size(73, 14)
        Me.lblVisitDate.TabIndex = 19
        Me.lblVisitDate.Text = "08/29/2005"
        Me.lblVisitDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_VisitDate
        '
        Me.lbl_VisitDate.AutoSize = True
        Me.lbl_VisitDate.BackColor = System.Drawing.Color.Transparent
        Me.lbl_VisitDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_VisitDate.Location = New System.Drawing.Point(366, 10)
        Me.lbl_VisitDate.Name = "lbl_VisitDate"
        Me.lbl_VisitDate.Size = New System.Drawing.Size(67, 14)
        Me.lbl_VisitDate.TabIndex = 20
        Me.lbl_VisitDate.Text = "Visit Date :"
        Me.lbl_VisitDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPatientName
        '
        Me.lblPatientName.AutoSize = True
        Me.lblPatientName.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientName.Location = New System.Drawing.Point(107, 34)
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Size = New System.Drawing.Size(71, 14)
        Me.lblPatientName.TabIndex = 18
        Me.lblPatientName.Text = "Mike Dodge"
        Me.lblPatientName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_PatientName
        '
        Me.lbl_PatientName.AutoSize = True
        Me.lbl_PatientName.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PatientName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PatientName.Location = New System.Drawing.Point(19, 10)
        Me.lbl_PatientName.Name = "lbl_PatientName"
        Me.lbl_PatientName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbl_PatientName.Size = New System.Drawing.Size(86, 14)
        Me.lbl_PatientName.TabIndex = 17
        Me.lbl_PatientName.Text = "Patient Code :"
        Me.lbl_PatientName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPatientCode
        '
        Me.lblPatientCode.AutoSize = True
        Me.lblPatientCode.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientCode.Location = New System.Drawing.Point(107, 10)
        Me.lblPatientCode.Name = "lblPatientCode"
        Me.lblPatientCode.Size = New System.Drawing.Size(35, 14)
        Me.lblPatientCode.TabIndex = 16
        Me.lblPatientCode.Text = "1001"
        Me.lblPatientCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPatient
        '
        Me.lblPatient.AutoSize = True
        Me.lblPatient.BackColor = System.Drawing.Color.Transparent
        Me.lblPatient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatient.Location = New System.Drawing.Point(16, 34)
        Me.lblPatient.Name = "lblPatient"
        Me.lblPatient.Size = New System.Drawing.Size(89, 14)
        Me.lblPatient.TabIndex = 15
        Me.lblPatient.Text = "Patient Name :"
        Me.lblPatient.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Spt_PatientFlowSheetLeft
        '
        Me.Spt_PatientFlowSheetLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Spt_PatientFlowSheetLeft.Location = New System.Drawing.Point(216, 56)
        Me.Spt_PatientFlowSheetLeft.Name = "Spt_PatientFlowSheetLeft"
        Me.Spt_PatientFlowSheetLeft.Size = New System.Drawing.Size(3, 510)
        Me.Spt_PatientFlowSheetLeft.TabIndex = 22
        Me.Spt_PatientFlowSheetLeft.TabStop = False
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmPatientFlowSheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(984, 566)
        Me.Controls.Add(Me.pnlGrid)
        Me.Controls.Add(Me.Spt_PatientFlowSheetLeft)
        Me.Controls.Add(Me.pnlPrevFlowSheet)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPatientFlowSheet"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Flowsheet"
        Me.pnlPrevFlowSheet.ResumeLayout(False)
        Me.pnlFlowSheetHistory.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tlsFlowSheet.ResumeLayout(False)
        Me.tlsFlowSheet.PerformLayout()
        Me.pnlGrid.ResumeLayout(False)
        Me.pnl_Grid.ResumeLayout(False)
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlcombo.ResumeLayout(False)
        Me.pnlcombo.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Enum enmAlign
        LeftCenter
        CenterCenter
        RightCenter
    End Enum


#Region " Patient Details Strip "
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip
    Private Sub InitializeToolStrip()
        tlsFlowSheet.ConnectionString = GetConnectionString()
        tlsFlowSheet.ModuleName = Me.Name
        tlsFlowSheet.UserID = gnLoginID
    End Sub
    Private Sub GloUC_PatientStrip1_ControlSizeChanged() Handles gloUC_PatientStrip1.ControlSizeChanged
        Try
            '' pnlPatientHeader.Height = gloUC_PatientStrip1.Height
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Set_PatientDetailStrip()
        ' '' Add Patient Details Control
        gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip

        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(0, 0, 3, 0)
            '' Pass Paarameters Type of Form
            'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            '.ShowDetail(gnPatientID, gloUC_PatientStrip.enumFormName.FlowSheet)
            .ShowDetail(_PatientID, gloUC_PatientStrip.enumFormName.FlowSheet)
            'end modification
            '.DTPValue = Format(CDate(lblVisitDate.Text), "MM/dd/yyyy" )
            ' .DTPValue = Format(CDate(lblVisitDate.Text), "MM/dd/yyyy  hh:mm tt")
            '.DTPValue = Format(Now, "MM/dd/yyyy  hh:mm tt")
            .DTPValue = Format(CDate(lblVisitDate.Text), "MM/dd/yyyy") & " " & Format(CDate(TimeOfDay.ToShortTimeString()), "hh:mm tt")
            .SendToBack()
        End With
        pnlGrid.Controls.Add(gloUC_PatientStrip1)
        ''''
        C1FlexGrid1.BringToFront()
        '' Hide Previous Patient Details
        pnlcombo.Visible = False
        ' ''
    End Sub

#End Region

    Public Function FormLevelLock() As Boolean
        Try
            Dim dtLock As DataTable ''slr new not needed
            dtLock = Scan_n_Lock_FormLevel(_PatientID, 0, 0, "Flowsheet")

            If dtLock.Rows.Count > 0 Then
                FormLevelLockID = Convert.ToInt64(dtLock.Rows(0)("FormLevelID"))
                If Convert.ToString(dtLock.Rows(0)("IsOpen")) = "1" Then ''This means form is allready open 
                    'New One
                    If MessageBox.Show("Flowsheet for this patient is currently being modified by " & Convert.ToString(dtLock.Rows(0)("UserName")) & " on " & Convert.ToString(dtLock.Rows(0)("MachineName")) & ".  Do you want to open the Flowsheet for view only mode?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        Call Set_RecordLock(True)
                        _blnFormLock = True
                    Else
                        If Not IsNothing(dtLock) Then ''slr free dtlock
                            dtLock.Dispose()
                            dtLock = Nothing
                        End If
                        Return False
                    End If
                Else
                End If
            End If
            If Not IsNothing(dtLock) Then ''slr free dtlock
                dtLock.Dispose()
                dtLock = Nothing
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub frmPatientFlowSheet_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            'Developer:Sanjog Dhamke
            'Date: 20 Dec. 2011 (Sprint-6060)
            'Bug ID/PRD Name/Sales force Case: Bug ID=17160- Showing exception in log file for flowsheet (Application log). 
            'Reason: we are getting nothing value for Me.parentform, so bcoz of this its shows exception. Now we are checking whether the value is nothing or not.
            If (IsNothing(Me.ParentForm) = False) Then
                CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmPatientFlowSheet_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        '' <><><> Unlock the Record <><><>
        '' Mahesh - 20070718
        If _blnRecordLock = False Then
            '' if the Locked by by the Current User & on Current Machine only
            UnLock_Transaction(TrnType.Flowsheet, _FlowSheetRecordID, 0, Now)
        End If
        '' <><><> Unlock the Record <>
        If FormLevelLockID > 0 Then
            Delete_Lock_FormLevel(FormLevelLockID, _PatientID)
        End If
        DisposedGlobal()  'obj Disposed by Mitesh
    End Sub
    Private Sub DisposedGlobal()
        If Not IsNothing(dt) Then
            dt.Dispose()
            dt = Nothing
        End If
        If Not IsNothing(dtStructure) Then
            dtStructure.Dispose()
            dtStructure = Nothing
        End If
        If Not IsNothing(dtgrid) Then
            dtgrid.Dispose()
            dtgrid = Nothing
        End If
        If Not IsNothing(PrintDialog1) Then
            PrintDialog1.Dispose()
            PrintDialog1 = Nothing
        End If
        If Not IsNothing(PrintPreviewDialog1) Then
            PrintPreviewDialog1.Dispose()
            PrintPreviewDialog1 = Nothing
        End If
        If Not IsNothing(objFlowSheet) Then
            objFlowSheet.Dispose()
            objFlowSheet = Nothing
        End If
        If Not IsNothing(dtFlowSheet) Then
            dtFlowSheet.Dispose()
            dtFlowSheet = Nothing
        End If
        If Not IsNothing(dtFlowSheetDeletedRowId) Then
            dtFlowSheetDeletedRowId.Dispose()
            dtFlowSheetDeletedRowId = Nothing
        End If
    End Sub
    Private Sub frmPatientFlowSheet_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(C1FlexGrid1)

        Try
            InitializeToolStrip()
            tls_btnShow.Text = " &Hide "
            Call Get_PatientDetails()
            ' objReferralsDBLayer = New ClsReferralsDBLayer
            lblPatientCode.Text = strPatientCode
            'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'lblPatientCode.Tag = gnPatientID
            lblPatientCode.Tag = _PatientID
            'end modification

            lblPatientName.Text = strPatientFirstName & " " & strPatientLastName
            lblVisitDate.Text = Now.Date
            lblVisitDate.Tag = 0
            'pnlPrevFlowSheet.Visible = False

            '' Show Patient Details Strip
            Call Set_PatientDetailStrip()
            ''''

            '' By degault Openn For New 
            ' cmbFlowSheet.Visible = True
            'lblFlowSheetName.Visible = False
            blnModify = False

            C1FlexGrid1.Cols(0).Width = 25
            C1FlexGrid1.Rows.Count = 1

            FillFlowSheetTree()

            'For Fixing Bug ID 15566 i.e if no flowSheet is Present then Hide the grid.
            If trFlowsheetHistory.Nodes.Count <= 0 Then
                C1FlexGrid1.Visible = False
            End If
            'End of Code For Fixing Bug ID 15566 i.e if no flowSheet is Present then Hide the grid.

            InitializeFlowSheetTable() '' TO CREATE NEW LOCAL FLOWSHEET TABLE ''

            ''Saving Desabled if no FlowSheet Selected.
            tls_btnFinish.Enabled = False
            tls_btnPreview.Enabled = False
            tls_btnPrint.Enabled = False

            '' SUDHIR 20090522 '' IF FLOWSHEET TO OPEN ''
            If _FlowSheetName <> "" Then
                For Each oNode As TreeNode In trFlowsheetHistory.Nodes
                    If oNode.Text = _FlowSheetName Then
                        trFlowsheetHistory.SelectedNode = oNode
                        trFlowsheetHistory_DoubleClick(sender, e)
                        Exit For
                    End If
                Next
            Else
                trFlowsheetHistory_DoubleClick(sender, e)
            End If

            ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
            If Not IsNothing(Array_Flow_List) Then
                If (Array_Flow_List.Count > 0) Then
                    Dim lst As myList
                    lst = CType(Array_Flow_List(0), myList)
                    selectparticularnode(lst)
                Else
                    trFlowsheetHistory_DoubleClick(sender, e)
                End If
                'Unnecessary calling 2 time on Load.
                'Else
                '    trFlowsheetHistory_DoubleClick(sender, e)
            End If
            _isLoaded = True

            ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try

            ''Added for Displaying current FlowSheet Name
            ''Handle Null Condition.
            If Not IsNothing(strFlowSheetName) Then
                If strFlowSheetName.Trim() <> "" Then
                    Label9.Text = strFlowSheetName
                Else
                    Label9.Text = ""
                End If
            Else
                Label9.Text = ""
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub tblFlowSheet_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)
    '    Try
    '        Select Case e.Button.Text
    '            Case "&Save"
    '                Call SavePatientFlowSheet()
    '                lblVisitDate.Text = Now.Date
    '                lblVisitDate.Tag = -1
    '                blnModify = False
    '                ' Me.Close()
    '            Case "&Close"
    '                Me.Close()
    '            Case "&Print"
    '                Call SavePatientFlowSheet()
    '                lblVisitDate.Text = Now.Date
    '                lblVisitDate.Tag = -1
    '                blnModify = False
    '                If C1FlexGrid1.Rows.Count > 1 Then
    '                    PrintFlowSheet(True)
    '                End If

    '            Case "&Preview"
    '                Call SavePatientFlowSheet()
    '                lblVisitDate.Text = Now.Date
    '                lblVisitDate.Tag = -1
    '                blnModify = False
    '                If C1FlexGrid1.Rows.Count > 1 Then
    '                    PrintFlowSheet(False)
    '                End If
    '            Case "&Setting"
    '                PageSettings()
    '            Case "Show", "Hide"
    '                ShowHidePreviousFlowSheet()
    '        End Select
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Patient Flow Sheet", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub


    'Commented Due to change in code for resolving FlowSheet Issuse
    'Function name  "Selectparticularnode"
    Private Sub selectparticularnode(ByVal lst As myList)
        Dim dtResult As DataTable = Nothing
        Dim myFlowSheetNode As myTreeNode = Nothing
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
        Try
            For Each oNode As TreeNode In trFlowsheetHistory.Nodes
                If oNode.Text = lst.Value Then
                    trFlowsheetHistory.SelectedNode = oNode

                    myFlowSheetNode = CType(trFlowsheetHistory.SelectedNode, myTreeNode)
                    Exit For
                End If
            Next


            'trFlowsheetHistory.Nodes(0)  
            If IsNothing(lst.Value) = False Then
                'If Not trFlowsheetHistory.SelectedNode Is trFlowsheetHistory.Nodes(0) Then
                If Not _blnFormLock Then
                    tls_btnFinish.Enabled = True ''User Can save FlowSheetDetail when FlowSheet has selected.
                    tls_btnPreview.Enabled = True
                    tls_btnPrint.Enabled = True
                End If



                strFlowSheetName = lst.Value
                intFlowSheetID = lst.Index  '' FlowSheet ID FlowSheet Master
                ''myFlowSheetNode.Tag ''  FlowsheetRecordID Patient FlowSheet

                ''Bug : 00000828: Record locking. Form Level locking implemented
                ''''''''''<><><> Record Level Locking <><><><>
                'If Not IsNothing(myFlowSheetNode) Then
                '    If gblnRecordLocking = True Then
                '        Dim mydt As New mytable
                '        mydt = Scan_n_Lock_Transaction(TrnType.Flowsheet, myFlowSheetNode.Tag, 0, Now)
                '        If mydt.Description <> gstrClientMachineName Then
                '            If MessageBox.Show("This Flowsheet is being modified by " & mydt.Code & " on " & mydt.Description & ". Do you want to open the History for view ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '                '' Open For view 
                '                _blnRecordLock = True
                '            Else
                '                '' 
                '                Exit Sub
                '            End If
                '        Else
                '            '' If Flow Sheet is not locked 
                '            If _blnRecordLock = True Then
                '                '' Currently Opened Flowsheet is locked by some other User on other Machine
                '                '' do nothing
                '            Else
                '                '' Currently Opened FlowSheet is locked by current User on same Machine
                '                '' Unlock Currently Opened FlowSheet  , Pass Currently Opened 
                '                Call UnLock_Transaction(TrnType.Flowsheet, _FlowSheetRecordID, 0, Now)
                '            End If
                '            _blnRecordLock = False
                '        End If

                '        Call Set_RecordLock(_blnRecordLock)
                '        '''' <><><> Record Level Locking <><><><> 
                '    End If
                'End If
                'dtResult = objFlowSheet.ScanFlowSheet(myFlowSheetNode.Tag)
                'dtResult = objFlowSheet.ScanFlowSheet(lst.Value, _PatientID)

                _FlowSheetRecordID = lst.Index

                C1FlexGrid1.Clear(ClearFlags.All)

                ' Dim tmp As Long = dtResult.Rows(0)(0) 
                '' FlowsheetMSTID

                'Call setGridStyle(dtResult.Rows(0)(0)) ''ORIGINAL
                ''sudhir 20081212

                C1FlexGrid1.Clear(ClearFlags.All)
                'C1FlexGrid1.Clear()
                C1FlexGrid1.DataSource = Nothing

                setGridStyle_New(lst.Value)

                If IsNothing(dtFlowSheetDeletedRowId) Then
                    dtFlowSheetDeletedRowId = fillDeletedRowIdinTempTable()
                End If

                LoadFlowsheetData(lst.Value)

                blnModify = True
            End If

            'If IsNothing(dtResult) = False Then
            '    If dtResult.Rows.Count > 0 Then
            ''COMMENT BY SUDHIR 20081212
            ' '' Update
            'Dim mstream As ADODB.Stream
            'Dim strFileName As String
            'mstream = New ADODB.Stream
            'mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
            'mstream.Open()

            'mstream.Write(dtResult.Rows(0)(1)) '' Flow Sheet Image
            ''cmbFlowSheet.SelectedItem = dt.Rows(0)(0)

            ''cmbFlowSheet.Visible = False
            ''lblFlowSheetName.Visible = True
            ''lblFlowSheetName.Text = FlowSheetNode.Text
            ''lblFlowSheetName.Tag = dt.Rows(0)(0)  '' FlowSheetID 

            'strFileName = Application.StartupPath & "\Temp\TempFlowSheet.txt"
            'mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
            'mstream.Close()
            ''SUDHIR COMMENT 

            'C1FlexGrid1.Clear(ClearFlags.Style)
            'C1FlexGrid1.Clear(ClearFlags.All)

            ' Dim tmp As Long = dtResult.Rows(0)(0) 
            '' FlowsheetMSTID

            'Call setGridStyle(dtResult.Rows(0)(0)) ''ORIGINAL
            ''sudhir 20081212
            'C1FlexGrid1.DataSource = Nothing


            ' ''To fill FlexGrid from database
            'Dim dtFlex As New DataTable ''datatable to be bind to flexGrid
            'For i As Int32 = 1 To C1FlexGrid1.Cols.Count - 1  ''Setting Columns
            '    dtFlex.Columns.Add(C1FlexGrid1.Rows(0)(i).ToString)
            'Next

            'Dim dtResultIndex As Int32 = 0
            'Dim newRow As DataRow

            ' ''Read each value from database and store as a datarow.
            'While dtResultIndex < dtResult.Rows.Count
            '    newRow = dtFlex.NewRow
            '    For i As Int32 = 0 To dtFlex.Columns.Count - 1
            '        newRow.Item(i) = dtResult.Rows.Item(dtResultIndex)(1)
            '        dtResultIndex += 1
            '    Next
            '    dtFlex.Rows.Add(newRow)
            'End While

            ''C1FlexGrid1.DataSource = dtFlex

            'For i As Int32 = 0 To dtFlex.Rows.Count - 1
            '    C1FlexGrid1.Rows.Insert(C1FlexGrid1.Rows.Count)
            '    For j As Int32 = 0 To dtFlex.Columns.Count - 1
            '        C1FlexGrid1.Rows.Item(i + 1)(j + 1) = dtFlex.Rows.Item(i)(j)
            '    Next

            'Next
            ' ''end sudhir 


            ''C1FlexGrid1.LoadGrid(strFileName, C1.Win.C1FlexGrid.FileFormatEnum.TextTab, C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells) ' ORIGINAL
            ' ''C1FlexGrid1.WriteXml(Application.StartupPath & "\Temp\TempFlowSheet.XML")
            'C1FlexGrid1.Row = C1FlexGrid1.Rows.Count - 1

            ''If C1FlexGrid1.Row = 0 Then
            ''    C1FlexGrid1.Row = 1 '' C1FlexGrid1.Rows.Count
            ''End If

            ''If C1FlexGrid1.Row <> 0 Then
            ''    Dim i As Integer
            ''    For i = 1 To C1FlexGrid1.Cols.Count - 1
            ''        If C1FlexGrid1.Cols(i).DataType Is GetType(System.DateTime) Then
            ''            C1FlexGrid1.SetData(C1FlexGrid1.Row, i, Now.Date)
            ''        End If
            ''    Next
            ''End If
            'If C1FlexGrid1.Row = 0 Then
            '    C1FlexGrid1.Row = 1 '' C1FlexGrid1.Rows.Count
            'ElseIf C1FlexGrid1.Row = 1 Then
            '    C1FlexGrid1.Rows.Add()
            '    C1FlexGrid1.Rows.Move(C1FlexGrid1.Row - 1, C1FlexGrid1.Row)
            '    C1FlexGrid1.Row = 2
            'End If

            'If C1FlexGrid1.Row >= 1 Then
            '    For i As Int32 = 1 To C1FlexGrid1.Cols.Count - 1
            '        If C1FlexGrid1.Cols(i).DataType Is GetType(System.DateTime) Then
            '            If IsDBNull(C1FlexGrid1.GetData(C1FlexGrid1.Row, i)) Or C1FlexGrid1.GetData(C1FlexGrid1.Row, i) Is Nothing Then
            '                C1FlexGrid1.SetData(C1FlexGrid1.Row, i, Now.Date)
            '            End If
            '        End If
            '    Next
            'End If


            'blnModify = True
            ''''''''''''''''''
            'setGridStyle(tmp)

            'Else
            '    C1FlexGrid1.Clear(ClearFlags.All)
            '    setGridStyle_New(lst.Value)
            'End If
            '    Else
            'C1FlexGrid1.Clear(ClearFlags.All)
            'setGridStyle_New(lst.Value)
            '    End If
            ''End If

            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(Array_Flow_List) Then
                Array_Flow_List = Nothing
            End If
            'If Not IsNothing(dtFlex) Then    'obj Disposed by mitesh
            '    dtFlex.Dispose()
            '    dtFlex = Nothing
            'End If
            If Not IsNothing(myFlowSheetNode) Then
                myFlowSheetNode = Nothing
            End If
            If Not IsNothing(dtResult) Then    'obj Disposed by mitesh
                dtResult.Dispose()
                dtResult = Nothing
            End If
        End Try
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
    End Sub

    Private Sub FillFlowSheetTree()
        trFlowsheetHistory.BeginUpdate()
        Dim dtResult As DataTable = Nothing
        Try
            'Commented by Shubhangi according to Bug No: 1241

            'Dim myRootNode As New myTreeNode("Flow Sheets", -1)
            'myRootNode.ImageIndex = 0
            'myRootNode.SelectedImageIndex = 0
            'trFlowsheetHistory.Nodes.Add(myRootNode)
            'Dim dt As DataTable

            trFlowsheetHistory.Nodes.Clear()
            'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'dt = objFlowSheet.GetAllFlowSheet(gnPatientID).Table()
            dt = objFlowSheet.GetAllFlowSheet(_PatientID).Table()
            'end modification

            trFlowsheetHistory.BeginUpdate()
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then     'Check record is there or not
                Dim i As Integer
                For i = 0 To dt.Rows.Count - 1
                    Dim myFlowSheetNode As New myTreeNode(CType(dt.Rows(i)(1), String), CType(dt.Rows(i)("nFlowSheetID"), Long))

                    dtResult = Nothing   'Added by mitesh
                    'dtResult = objFlowSheet.SelectPatientFlowSheet(gnPatientID, CType(dt.Rows(i)("nFlowSheetID"), Long)) ''by sudhir 20081212

                    'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                    'dtResult = objFlowSheet.SelectPatientFlowSheet(gnPatientID, CType(dt.Rows(i)("sFlowSheetName"), String))
                    dtResult = objFlowSheet.SelectPatientFlowSheet(_PatientID, CType(dt.Rows(i)("sFlowSheetName"), String))
                    'end modification

                    myFlowSheetNode.Text = dt.Rows(i)("sFlowSheetName")
                    If IsNothing(dtResult) = False AndAlso dtResult.Rows.Count > 0 Then
                        If IsDBNull(dtResult.Rows(0)) = False Then
                            myFlowSheetNode.Tag = CType(dtResult.Rows(0)("nFlowSheetRecordID"), Long)
                            myFlowSheetNode.ForeColor = Color.Red
                        Else
                            myFlowSheetNode.Tag = 0
                            myFlowSheetNode.ForeColor = Color.Green
                        End If
                    Else
                        myFlowSheetNode.Tag = 0
                        myFlowSheetNode.ForeColor = Color.Green

                    End If
                    myFlowSheetNode.ImageIndex = 1
                    myFlowSheetNode.SelectedImageIndex = 1
                    trFlowsheetHistory.Nodes.Add(myFlowSheetNode)

                    myFlowSheetNode = Nothing

                Next
                trFlowsheetHistory.SelectedNode = trFlowsheetHistory.Nodes.Item(0)
                trFlowsheetHistory.EndUpdate()
                trFlowsheetHistory.ExpandAll()
            End If

        Catch ex As Exception
            Throw ex
            ' MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(dtResult) Then    'obj Disposed by mitesh
                dtResult.Dispose()
                dtResult = Nothing
            End If
        End Try
        trFlowsheetHistory.EndUpdate()
    End Sub

    Private Sub PageSettings()
        Dim psDlg As New PageSetupDialog
        Try

            If (storedPageSettings Is Nothing) Then
                storedPageSettings = New PageSettings
            End If

            psDlg.PageSettings = storedPageSettings
            psDlg.ShowDialog(System.Windows.Forms.Form.ActiveForm)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("An error occurred - " + ex.Message)
        Finally
            If Not IsNothing(psDlg) Then    'obj Disposed by mitesh
                psDlg.Dispose()
                psDlg = Nothing
            End If
        End Try
    End Sub

   Private Sub PrintFlowSheet(ByVal blnPrint As Boolean)
        Dim frm As New Form
        Try

            Dim objdatagrid As New DataGrid

            Dim objlabel As Label
            Dim ArialFontFamily As FontFamily = New FontFamily("Arial")
            Dim fntTitle As New Font(ArialFontFamily, 16, FontStyle.Bold)
            Dim objPrintFlowsheet As New ClsPrintFlowSheet

            Dim objstruct As ClinicDetails
            objstruct = objPrintFlowsheet.ScanClinicInfo



            objlabel = New Label
            '' For Resolving Bug Id : 7564 in 6040
            '' Flowsheet >> Flowsheet name is showing wrong while while Print/Preview the flowSheet.
            objlabel.Text = strFlowSheetName
            objlabel.AutoSize = True
            objlabel.Tag = 1 & "Title"
            objlabel.Font = fntTitle
            frm.Controls.Add(objlabel)


            objlabel = New Label
            objlabel.Text = objstruct.m_Clinicname
            objlabel.AutoSize = True
            objlabel.Tag = 1 & "Clinicname"

            Dim fntTitle2 As New Font(ArialFontFamily, 13, FontStyle.Bold)
            objlabel.Font = fntTitle2
            frm.Controls.Add(objlabel)

            Dim fnt As New Font(ArialFontFamily, 9)
            objlabel = New Label
            objlabel.AutoSize = True
            objlabel.Text = "Clinic Address : " & Replace(Trim(objstruct.m_ClinicAddress1), vbCrLf, " ") '& " " & objstruct.m_ClincAddress2
            objlabel.Tag = 1 & "ClinicAddress1"

            objlabel.Font = fnt
            frm.Controls.Add(objlabel)

            objlabel = New Label
            objlabel.AutoSize = True
            objlabel.Font = fnt
            objlabel.Text = Replace(Trim(objstruct.m_ClincAddress2), vbCrLf, "")
            objlabel.Tag = 1 & "ClinicAddress2"
            frm.Controls.Add(objlabel)

            objlabel = New Label
            objlabel.AutoSize = True
            objlabel.Text = "Phone No : " & Replace(Trim(objstruct.m_PhoneNo), vbCrLf, "")
            objlabel.Tag = 1 & "PhoneNo"
            objlabel.Font = fnt
            frm.Controls.Add(objlabel)

            objlabel = New Label
            objlabel.AutoSize = True
            objlabel.Text = "Patient Name: " & strPatientFirstName & " " & strPatientLastName
            objlabel.Tag = 2 & "PatientName"
            Dim fntTitle3 As New Font(ArialFontFamily, 11, FontStyle.Bold)
            objlabel.Font = fntTitle3
            frm.Controls.Add(objlabel)



            objlabel = New Label
            objlabel.AutoSize = True
            objlabel.Text = "Date of Birth : " & strPatientDOB
            objlabel.Tag = 2 & "PatientDOB"
            objlabel.Font = fntTitle
            frm.Controls.Add(objlabel)


            objlabel = New Label
            objlabel.AutoSize = True
            objlabel.Text = ""
            objlabel.Tag = 4 & "PageNo"
            objlabel.Font = fnt
            frm.Controls.Add(objlabel)

            objlabel = New Label
            objlabel.AutoSize = True
            objlabel.Text = ""
            objlabel.Tag = 4 & "PrintDate"
            objlabel.Font = fnt
            frm.Controls.Add(objlabel)

            objdatagrid.Font = fnt
            frm.Controls.Add(objdatagrid)
            BindToGrid(objdatagrid)



            If blnPrint Then
                InvokePrint(frm, objdatagrid)
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Flow Sheet Printed", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Flow Sheet Printed", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                ' gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.PHIExport, "Flow Sheet Printed", gstrLoginName, gstrClientMachineName, gnPatientID)

            Else
                InvokePrintPreview(frm, objdatagrid)
            End If
            Try
                Try
                    For iCount As Integer = frm.Controls.Count - 1 To 0 Step -1
                        Try
                            Dim thisControl As Control = frm.Controls(iCount)
                            thisControl.Dispose()
                            thisControl = Nothing
                        Catch ex As Exception

                        End Try
                    Next
                Catch ex As Exception

                End Try
                Try
                    frm.Dispose()
                    frm = Nothing
                Catch ex As Exception

                End Try
                Try
                    fnt.Dispose()
                    fnt = Nothing
                    fntTitle.Dispose()
                    fntTitle = Nothing
                    fntTitle2.Dispose()
                    fntTitle2 = Nothing
                    fntTitle3.Dispose()
                    fntTitle3 = Nothing
                Catch ex As Exception

                End Try
                Try
                    ArialFontFamily.Dispose()
                    ArialFontFamily = Nothing
                Catch ex As Exception

                End Try
                Try
                    objdatagrid.TableStyles.Clear()
                    objdatagrid.Dispose()
                    objdatagrid = Nothing
                Catch ex As Exception

                End Try
                Try
                    objlabel.Dispose()
                    objlabel = Nothing

                Catch ex As Exception

                End Try

                objPrintFlowsheet = Nothing
            Catch ex As Exception

            End Try
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Error Printing Report - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Error Printing Report - " & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(frm) Then    'obj Disposed by mitesh
                frm.Dispose()
                frm = Nothing
            End If
        End Try

    End Sub

    Private Function PrintFlowSheet_ClinicalQueue(ByVal blnPrint As Boolean, ByVal flowSheetFileDocPath As String) As Boolean
        Dim glwrd As gloWord.LoadAndCloseWord
        Dim Copied As Boolean = False
        Dim objdatagridView As DataGridView ''added in 8081-replace DataGridPrinter
        Dim objPrintFlowsheet As ClsPrintFlowSheet = Nothing
        Dim objstruct As ClinicDetails = Nothing
        Try

            If flowSheetFileDocPath <> "" Then ''send the existing created flowsheet doc file for printing
                If blnPrint = True Then ''''means print button was clicked
                    glwrd = New gloWord.LoadAndCloseWord
                    Dim objDoc As Word.Document = Nothing
                    objDoc = glwrd.LoadWordApplication(flowSheetFileDocPath)
                    Copied = gloWord.LoadAndCloseWord.CopyPrintDoc(objDoc, 0)
                    If Not IsNothing(glwrd) Then
                        glwrd.CloseWordApplication(objDoc)
                        glwrd = Nothing
                    End If

                    Return Copied ''''issue in CopyPrintDoc()
                Else ''''means print preview button was clicked
                    Dim oPreview As New frmWordPreview(sFlwShtFilePath)
                    oPreview.ShowDialog()
                    CopiedbyCCQPrint = oPreview.CopiedbyCCQPrint
                    oPreview.Dispose()
                    oPreview = Nothing

                    ''CopiedbyCCQPrint = true --means it will NOT Print from CCQ
                    ''CopiedbyCCQPrint = False --means it will print from CCQ
                    ''''we deliberately set flow sheet file name to blank so that it will not print 
                    'If CopiedbyCCQPrint Then
                    '    Return False
                    'Else
                    '    Return True ''''return true so that it will not be printed by normal printing also which is function called if printing is not done through new CCQ tsprint functionality.
                    'End If

                    Return CopiedbyCCQPrint

                End If
                

            Else ''create new flowsheet doc file for printing
                'Dim objdatagrid As New DataGrid
                objdatagridView = New DataGridView ''added in 8081-replace DataGridPrinter

                objPrintFlowsheet = New ClsPrintFlowSheet
                objstruct = objPrintFlowsheet.ScanClinicInfo

                objstruct.m_FlowSheetName = strFlowSheetName ''added in 8081-replace DataGridPrinter

                objstruct.m_PatientName = strPatientFirstName & " " & strPatientLastName ''added in 8081-replace DataGridPrinter

                objstruct.m_PatDOB = strPatientDOB ''added in 8081-replace DataGridPrinter

                BindToGridView(objdatagridView)

                'objdatagridView.DataSource = objdatagrid.DataSource ''added in 8081-replace DataGridPrinter

                sFlwShtFilePath = CreatFlowSheetWordDocument(objdatagridView, objstruct) ''added in 8081-replace DataGridPrinter'"C:\Users\sagar\AppData\Local\gloEMR\Temp\112820161459000771.docx" 
                If sFlwShtFilePath <> "" Then
                    ''''blnPrint=false meanse event Preview is called
                    If blnPrint = False Then
                        Dim oPreview As New frmWordPreview(sFlwShtFilePath)
                        oPreview.ShowDialog()
                        CopiedbyCCQPrint = oPreview.CopiedbyCCQPrint
                        oPreview.Dispose()
                        oPreview = Nothing

                        ''CopiedbyCCQPrint = true --means it will NOT Print from and stay as it is
                        ''CopiedbyCCQPrint = False --means it will print from CCQ it
                        ''''we deliberately set flow sheet file name to blank so that it will not print 
                        'If CopiedbyCCQPrint Then
                        '    Return True ''''return true so that it will not be printed by normal printing also.
                        'Else
                        '    Return False
                        'End If

                        Return CopiedbyCCQPrint
                    End If

                Else
                    ''CopiedbyCCQPrint = true --means it will NOT Print from CCQ
                    ''CopiedbyCCQPrint = False --means it will print from CCQ
                    ''''we deliberately set flow sheet file name to blank so that it will not print 
                    If CopiedbyCCQPrint = True Then
                        Return True ''''return true so that it will not be printed by normal printing also.
                    Else
                        Return False
                    End If

                End If
            End If



        Catch ex As Exception
            'If Not IsNothing(objdatagrid) Then
            '    objdatagrid.Dispose()
            '    objdatagrid = Nothing
            'End If
            If Not IsNothing(objPrintFlowsheet) Then
                objPrintFlowsheet = Nothing
            End If
            If Not IsNothing(objstruct) Then
                objstruct = Nothing
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Error Printing Report - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Error Printing Report - " & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            'If Not IsNothing(objdatagrid) Then
            '    objdatagrid.Dispose()
            '    objdatagrid = Nothing
            'End If
            If Not IsNothing(objPrintFlowsheet) Then
                objPrintFlowsheet = Nothing
            End If
            If Not IsNothing(objstruct) Then
                objstruct = Nothing
            End If
        End Try
        Return CopiedbyCCQPrint
    End Function
    ''added in 8081-replace DataGridPrinter
    Private Sub CreatFlowSheeteWordDocument_Old(ByRef objdatagrid As DataGridView, ByRef objstrct As ClinicDetails)
        Dim objWord As Word.Application
        Dim objDoc As Word.Document = Nothing

        Try
            objDoc = New Word.Document
            objWord = CreateObject("Word.Application")
            objWord.Visible = False
            objDoc = objWord.Documents.Add

            For Each wordSection As Microsoft.Office.Interop.Word.Section In objDoc.Sections
                Dim footerRange As Microsoft.Office.Interop.Word.Range = wordSection.Footers(Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                footerRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd)
                footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                'footerRange.Fields.Add(footerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldDate);
                footerRange.Fields.Add(footerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage)
                Dim p2 As Microsoft.Office.Interop.Word.Paragraph = footerRange.Paragraphs.Add()
                'MessageBox.Show(CurrentPage.ToString());
                Dim footer As [String] = (Convert.ToString("Print date: " + DateTime.Now.ToString("MM/dd/yyyy")) & New String(" "c, 120)) + "Page No: "
                p2.Range.Text = footer
            Next

            Dim para1 As Microsoft.Office.Interop.Word.Paragraph = objDoc.Content.Paragraphs.Add()
            para1.Range.Bold = 1
            para1.Range.Font.Size = 14
            para1.Range.Text = objstrct.m_FlowSheetName
            para1.Range.[Select]()
            para1.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
            para1.Range.InsertParagraphAfter()


            Dim para2 As Microsoft.Office.Interop.Word.Paragraph = objDoc.Content.Paragraphs.Add()
            para2.Range.Bold = 1
            para2.Range.Font.Size = 13
            para2.Range.Text = objstrct.m_Clinicname
            para2.Range.[Select]()
            para2.SpaceAfter = 10
            para2.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
            para2.Range.InsertParagraphAfter()

            Dim para3 As Microsoft.Office.Interop.Word.Paragraph = objDoc.Content.Paragraphs.Add()
            para3.Range.Bold = 0
            para3.Range.Font.Size = 11
            para3.Range.Text = "Clinic Address :" + objstrct.m_ClinicAddress1
            para3.Range.[Select]()
            para3.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
            para3.SpaceAfter = 0
            para3.Range.InsertParagraphAfter()


            Dim para4 As Microsoft.Office.Interop.Word.Paragraph = objDoc.Content.Paragraphs.Add()
            para4.Range.Bold = 0
            para4.Range.Font.Size = 11
            para4.Range.Text = New String(" "c, 27) & Convert.ToString(objstrct.m_ClincAddress2)
            para4.Range.[Select]()
            para4.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
            para4.Range.InsertParagraphAfter()

            Dim para5 As Microsoft.Office.Interop.Word.Paragraph = objDoc.Content.Paragraphs.Add()
            para5.Range.Bold = 0
            para5.Range.Font.Size = 11
            para5.Range.Text = (New String(" "c, 27) & Convert.ToString("Phone No : ")) + objstrct.m_PhoneNo
            para5.Range.[Select]()
            para5.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
            para5.SpaceAfter = 15
            para5.Range.InsertParagraphAfter()

            Dim para6 As Microsoft.Office.Interop.Word.Paragraph = objDoc.Content.Paragraphs.Add()
            para6.Range.Bold = 1
            para6.Range.Font.Size = 12
            para6.Range.Text = "Patient Name : " + objstrct.m_PatientName
            para6.Range.[Select]()
            para6.SpaceAfter = 0
            para6.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
            para6.Range.InsertParagraphAfter()


            Dim para7 As Microsoft.Office.Interop.Word.Paragraph = objDoc.Content.Paragraphs.Add()
            para7.Range.Bold = 1
            para7.Range.Font.Size = 12
            para7.Range.Text = "Date of Birth : " + objstrct.m_PatDOB
            para7.Range.[Select]()
            para7.SpaceAfter = 15
            para7.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
            para7.Range.InsertParagraphAfter()
            Dim _RowCount As Integer = objdatagrid.DataSource.Rows.Count
            Dim _ColCount As Integer = objdatagrid.DataSource.Columns.Count - 1

            Dim ht1 As Word.Table

            ht1 = objDoc.Tables.Add(objDoc.Bookmarks.Item("\endofdoc").Range, _
                                    _RowCount + 1, _ColCount + 1)
            ht1.Borders.OutsideColor = Word.WdColor.wdColorBlack
            ht1.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle
            ht1.Borders.InsideColor = Word.WdColor.wdColorBlack
            ht1.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle
            objDoc.Paragraphs.SpaceAfter = 0




            '  ht1.Rows.Add()
            For _col As Integer = 0 To _ColCount
                Dim colType As Type = objdatagrid.DataSource.Columns(_col).GetType
                'If colType.Name = "DataGridViewImageColumn" Then
                '    Dim _image As Image = DirectCast(objdatagrid.DataSource.Rows(i)(_col), Image)
                '    Clipboard.SetImage(_image)
                '    ht1.Cell(i + 1, _col + 1).Range.Paste()
                'Else
                '    ht1.Cell(i + 1, _col + 1).Range.Text = _
                '    objdatagrid.DataSource.Rows(i)(_col).ToString()

                'End If
                ht1.Cell(1, _col + 1).Range.Bold = 1

                ht1.Cell(1, _col + 1).Range.Text = _
             objdatagrid.DataSource.Columns(_col).ToString()


            Next


            For i As Integer = 1 To _RowCount
                ' ht1.Rows.Add()
                For _col As Integer = 0 To _ColCount
                    Dim colType As Type = objdatagrid.DataSource.Columns(_col).GetType
                    'If colType.Name = "DataGridViewImageColumn" Then
                    '    Dim _image As Image = DirectCast(objdatagrid.DataSource.Rows(i)(_col), Image)
                    '    Clipboard.SetImage(_image)
                    '    ht1.Cell(i + 1, _col + 1).Range.Paste()
                    'Else
                    '    ht1.Cell(i + 1, _col + 1).Range.Text = _
                    '    objdatagrid.DataSource.Rows(i)(_col).ToString()

                    'End If


                    ht1.Cell(i + 1, _col + 1).Range.Text = _
                   objdatagrid.DataSource.Rows(i - 1)(_col).ToString()

                Next
            Next

            Dim sfloSheetfilePath As String = gloGlobal.gloTSPrint.TempPath + Guid.NewGuid().ToString() + ".docx"

            objDoc.SaveAs2(sfloSheetfilePath)
            objDoc.Close()

            '   objWord.Quit()

        Catch ex As Exception
            If IsNothing(objDoc) Then
                objDoc.Close()
            End If
            objDoc.Close()
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Error Printing Report - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Error Printing Report - " & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''added in 8081-replace DataGridPrinter
    Private Function CreatFlowSheetWordDocument(ByRef objdatagrid As DataGridView, ByRef objstrct As ClinicDetails) As String
        'Dim objWord As Word.Application
        Dim objDoc As Word.Document = Nothing

        Dim sflwshtfilename As String = ""
        Dim glWrd As gloWord.LoadAndCloseWord = Nothing
        Try


            'objDoc = New Word.Document
            'objWord = CreateObject("Word.Application")
            'objWord.Visible = False
            'objDoc = objWord.Documents.Add
            'Dim sfloSheetfilePath As String = gloGlobal.gloTSPrint.TempPath + Guid.NewGuid().ToString() + ".docx"

            glWrd = New gloWord.LoadAndCloseWord()
            objDoc = glWrd.LoadWordApplication(Nothing)

            For Each wordSection As Microsoft.Office.Interop.Word.Section In objDoc.Sections
                Dim footerRange As Microsoft.Office.Interop.Word.Range = wordSection.Footers(Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary).Range
                footerRange.Collapse(Microsoft.Office.Interop.Word.WdCollapseDirection.wdCollapseEnd)
                footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
                'footerRange.Fields.Add(footerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldDate);
                footerRange.Fields.Add(footerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage)
                Dim p2 As Microsoft.Office.Interop.Word.Paragraph = footerRange.Paragraphs.Add()
                'MessageBox.Show(CurrentPage.ToString());
                Dim footer As [String] = (Convert.ToString("Print date: " + DateTime.Now.ToString("MM/dd/yyyy")) & New String(" "c, 120)) + "Page No: "
                p2.Range.Text = footer
            Next

            Dim para1 As Microsoft.Office.Interop.Word.Paragraph = objDoc.Content.Paragraphs.Add()
            para1.Range.Bold = 1
            para1.Range.Font.Size = 14
            para1.Range.Text = objstrct.m_FlowSheetName
            para1.Range.[Select]()
            para1.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter
            para1.Range.InsertParagraphAfter()


            Dim para2 As Microsoft.Office.Interop.Word.Paragraph = objDoc.Content.Paragraphs.Add()
            para2.Range.Bold = 1
            para2.Range.Font.Size = 13
            para2.Range.Text = objstrct.m_Clinicname
            para2.Range.[Select]()
            para2.SpaceAfter = 10
            para2.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
            para2.Range.InsertParagraphAfter()

            Dim para3 As Microsoft.Office.Interop.Word.Paragraph = objDoc.Content.Paragraphs.Add()
            para3.Range.Bold = 0
            para3.Range.Font.Size = 11
            para3.Range.Text = "Clinic Address :" + objstrct.m_ClinicAddress1
            para3.Range.[Select]()
            para3.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
            para3.SpaceAfter = 0
            para3.Range.InsertParagraphAfter()


            Dim para4 As Microsoft.Office.Interop.Word.Paragraph = objDoc.Content.Paragraphs.Add()
            para4.Range.Bold = 0
            para4.Range.Font.Size = 11
            para4.Range.Text = New String(" "c, 27) & Convert.ToString(objstrct.m_ClincAddress2)
            para4.Range.[Select]()
            para4.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
            para4.Range.InsertParagraphAfter()

            Dim para5 As Microsoft.Office.Interop.Word.Paragraph = objDoc.Content.Paragraphs.Add()
            para5.Range.Bold = 0
            para5.Range.Font.Size = 11
            para5.Range.Text = (New String(" "c, 27) & Convert.ToString("Phone No : ")) + objstrct.m_PhoneNo
            para5.Range.[Select]()
            para5.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
            para5.SpaceAfter = 15
            para5.Range.InsertParagraphAfter()

            Dim para6 As Microsoft.Office.Interop.Word.Paragraph = objDoc.Content.Paragraphs.Add()
            para6.Range.Bold = 1
            para6.Range.Font.Size = 12
            para6.Range.Text = "Patient Name : " + objstrct.m_PatientName
            para6.Range.[Select]()
            para6.SpaceAfter = 0
            para6.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
            para6.Range.InsertParagraphAfter()


            Dim para7 As Microsoft.Office.Interop.Word.Paragraph = objDoc.Content.Paragraphs.Add()
            para7.Range.Bold = 1
            para7.Range.Font.Size = 12
            para7.Range.Text = "Date of Birth : " + objstrct.m_PatDOB
            para7.Range.[Select]()
            para7.SpaceAfter = 15
            para7.Range.Paragraphs.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft
            para7.Range.InsertParagraphAfter()
            Dim _RowCount As Integer = objdatagrid.DataSource.Rows.Count
            Dim _ColCount As Integer = objdatagrid.DataSource.Columns.Count - 1

            Dim ht1 As Word.Table

            ht1 = objDoc.Tables.Add(objDoc.Bookmarks.Item("\endofdoc").Range, _RowCount + 1, _ColCount + 1)
            ht1.Rows(1).HeadingFormat = True
            ht1.ApplyStyleHeadingRows = True
            ht1.Borders.OutsideColor = Word.WdColor.wdColorBlack
            ht1.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle
            ht1.Borders.InsideColor = Word.WdColor.wdColorBlack
            ht1.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle
            objDoc.Paragraphs.SpaceAfter = 0




            '  ht1.Rows.Add()
            For _col As Integer = 0 To _ColCount
                Dim colType As Type = objdatagrid.DataSource.Columns(_col).GetType
                'If colType.Name = "DataGridViewImageColumn" Then
                '    Dim _image As Image = DirectCast(objdatagrid.DataSource.Rows(i)(_col), Image)
                '    Clipboard.SetImage(_image)
                '    ht1.Cell(i + 1, _col + 1).Range.Paste()
                'Else
                '    ht1.Cell(i + 1, _col + 1).Range.Text = _
                '    objdatagrid.DataSource.Rows(i)(_col).ToString()

                'End If
                ht1.Cell(1, _col + 1).Range.Bold = 1

                ht1.Cell(1, _col + 1).Range.Text = _
             objdatagrid.DataSource.Columns(_col).ToString()


            Next


            For i As Integer = 1 To _RowCount
                ' ht1.Rows.Add()
                For _col As Integer = 0 To _ColCount
                    Dim colType As Type = objdatagrid.DataSource.Columns(_col).GetType
                    'If colType.Name = "DataGridViewImageColumn" Then
                    '    Dim _image As Image = DirectCast(objdatagrid.DataSource.Rows(i)(_col), Image)
                    '    Clipboard.SetImage(_image)
                    '    ht1.Cell(i + 1, _col + 1).Range.Paste()
                    'Else
                    '    ht1.Cell(i + 1, _col + 1).Range.Text = _
                    '    objdatagrid.DataSource.Rows(i)(_col).ToString()

                    'End If


                    ht1.Cell(i + 1, _col + 1).Range.Text = _
                   objdatagrid.DataSource.Rows(i - 1)(_col).ToString()

                Next
            Next

            ''''first save and then send objDoc to clinical que printing
            sflwshtfilename = gloWord.LoadAndCloseWord.SaveNewWordDocument(objDoc, gloGlobal.gloTSPrint.TempPath)

            '''''this code is adde because sometimes we do not have mapped drive or CCQ service hence we get a message box, 
            'in order to avoid this message box we added this code because preview needs to be shown. rest of code is handled in the flowsheet preview form
            If blnPrint Then ''''means called from Print button.
                CopiedbyCCQPrint = gloWord.LoadAndCloseWord.CopyPrintDoc(objDoc, 0)
                If Not IsNothing(glWrd) Then
                    glWrd.CloseWordApplication(objDoc)
                    glWrd = Nothing
                End If
            Else ''''means called from printpreview button. show the created file in flowsheet preview form.
                Return sflwshtfilename
            End If
            
            'Dim sfloSheetfilePath As String = gloGlobal.gloTSPrint.TempPath + Guid.NewGuid().ToString() + ".docx"
            'Dim newFileName As String = gloWord.LoadAndCloseWord.SaveNewWordDocument(objDoc, gloGlobal.gloTSPrint.TempPath)


            ''CopiedbyCCQPrint = true --means it will NOT Print
            ''CopiedbyCCQPrint = false --means it will print
            If CopiedbyCCQPrint = True Then
                If blnPrint Then
                    ''''if clicked from Print button we deliberately set flow sheet file name to blank so that it will not print. 
                    sflwshtfilename = ""
                    Return sflwshtfilename
                Else ''''''we will still show the preview and rest will be handled on the flowsheetpreview form
                    Return sflwshtfilename
                End If
               
            Else
                Return sflwshtfilename
            End If


        Catch ex As Exception
            If Not IsNothing(glWrd) Then
                glWrd.CloseWordApplication(objDoc)
                glWrd = Nothing
            End If

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Error Printing Report - " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Error Printing Report - " & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        Finally
            If Not IsNothing(glWrd) Then
                glWrd.CloseWordApplication(objDoc)
                glWrd = Nothing
            End If

        End Try

    End Function
    Private Sub BindToGrid(ByVal objdatagrid As DataGrid)
        Dim i As Int16
        Dim j As Int16

        Dim objcol As DataColumn = Nothing
        Dim objrow As DataRow = Nothing
        'C1FlexGrid1.Styles.ClearUnused()
        Try

            dtgrid = New DataTable

            For i = 2 To C1FlexGrid1.Cols.Count - 1
                objcol = New DataColumn(C1FlexGrid1.Cols.Item(i).Caption, GetType(System.String))
                'objcol = New DataColumn(C1FlexGrid1.Cols.Item(i).Caption, C1FlexGrid1.Cols(i).DataType)
                dtgrid.Columns.Add(objcol)
                objcol = Nothing
            Next

            'C1FlexGrid1.Styles.ClearUnused()
            If C1FlexGrid1.Rows.Count - 1 > 1 Then
                '' Remove Last Row '' Which is always Empty
                C1FlexGrid1.Rows.Remove(C1FlexGrid1.Rows.Count - 1)
            End If

            C1FlexGrid1.Styles.ClearUnused()

            For i = 1 To C1FlexGrid1.Rows.Count - 2
                objrow = dtgrid.NewRow
                For j = 2 To C1FlexGrid1.Cols.Count - 1
                    If IsNothing(C1FlexGrid1.GetData(i, j)) = False Then
                        Try
                            If C1FlexGrid1.Cols(j).EditMask.Trim = "" Then
                                If C1FlexGrid1.Cols(j).Format.Trim = "General" Then
                                    objrow.Item(j - 2) = C1FlexGrid1.GetData(i, j)
                                Else
                                    objrow.Item(j - 2) = Format(C1FlexGrid1.GetData(i, j), C1FlexGrid1.Cols(j).Format)
                                End If

                            Else
                                objrow.Item(j - 2) = C1FlexGrid1.GetData(i, j)
                            End If

                        Catch ex As Exception
                        End Try
                        'Else
                        '    objrow.Item(j - 1) = ctype( "", GetDataType( C1FlexGrid1.Cols(j).DataType ))' gettype( C1FlexGrid1.Cols(j).DataType )' Format("", C1FlexGrid1.Cols(j).DataType)
                    End If
                Next
                dtgrid.Rows.Add(objrow)
                objrow = Nothing
            Next
            objdatagrid.DataSource = dtgrid
            HideColumn(objdatagrid)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(objcol) Then    'obj Disposed by mitesh
                objcol.Dispose()
                objcol = Nothing
            End If
        End Try
    End Sub


    ''added in 8081-replace DataGridPrinter
    Private Sub BindToGridView(ByVal objdatagridView As DataGridView)
        Dim i As Int16
        Dim j As Int16

        Dim objcol As DataColumn = Nothing
        Dim objrow As DataRow = Nothing
        'C1FlexGrid1.Styles.ClearUnused()
        Try

            dtgrid = New DataTable

            For i = 2 To C1FlexGrid1.Cols.Count - 1
                objcol = New DataColumn(C1FlexGrid1.Cols.Item(i).Caption, GetType(System.String))
                'objcol = New DataColumn(C1FlexGrid1.Cols.Item(i).Caption, C1FlexGrid1.Cols(i).DataType)
                dtgrid.Columns.Add(objcol)
                objcol = Nothing
            Next

            'C1FlexGrid1.Styles.ClearUnused()
            If C1FlexGrid1.Rows.Count - 1 > 1 Then
                '' Remove Last Row '' Which is always Empty
                C1FlexGrid1.Rows.Remove(C1FlexGrid1.Rows.Count - 1)
            End If

            C1FlexGrid1.Styles.ClearUnused()

            For i = 1 To C1FlexGrid1.Rows.Count - 2
                objrow = dtgrid.NewRow
                For j = 2 To C1FlexGrid1.Cols.Count - 1
                    If IsNothing(C1FlexGrid1.GetData(i, j)) = False Then
                        Try
                            If C1FlexGrid1.Cols(j).EditMask.Trim = "" Then
                                If C1FlexGrid1.Cols(j).Format.Trim = "General" Then
                                    objrow.Item(j - 2) = C1FlexGrid1.GetData(i, j)
                                Else
                                    objrow.Item(j - 2) = Format(C1FlexGrid1.GetData(i, j), C1FlexGrid1.Cols(j).Format)
                                End If

                            Else
                                objrow.Item(j - 2) = C1FlexGrid1.GetData(i, j)
                            End If

                        Catch ex As Exception
                        End Try
                        'Else
                        '    objrow.Item(j - 1) = ctype( "", GetDataType( C1FlexGrid1.Cols(j).DataType ))' gettype( C1FlexGrid1.Cols(j).DataType )' Format("", C1FlexGrid1.Cols(j).DataType)
                    End If
                Next
                dtgrid.Rows.Add(objrow)
                objrow = Nothing
            Next
            objdatagridView.DataSource = dtgrid
            'HideColumnView(objdatagridView)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(objcol) Then    'obj Disposed by mitesh
                objcol.Dispose()
                objcol = Nothing
            End If
        End Try
    End Sub
    'Used to set grid Style Base on FlowSheet Selected.
    Private Sub setGridStyle_New(ByVal flowSheetName As String)
        Dim dtStructure_Local As DataTable = Nothing
        Dim i As Integer = 2
        Dim colName As String = String.Empty
        Dim objFlowSheet_Local As clsFlowSheet = Nothing
        Try
            objFlowSheet_Local = New clsFlowSheet

            objFlowSheet_Local.SelectFlowSheet(flowSheetName, _PatientID)
            'end modification
            dtStructure_Local = objFlowSheet_Local.GetDataview.Table

            If IsNothing(dtStructure_Local) Then
                Exit Sub
            End If

            C1FlexGrid1.DataSource = Nothing
            C1FlexGrid1.Clear(ClearFlags.All)
            'C1FlexGrid1.Clear()
            C1FlexGrid1.Rows.Count = 1
            C1FlexGrid1.Rows.Fixed = 1

            C1FlexGrid1.Cols.Count = dtStructure_Local.Rows.Count + 2
            C1FlexGrid1.Cols.Fixed = 1

            C1FlexGrid1.AutoGenerateColumns = False
            C1FlexGrid1.Cols(0).Width = 25

            'C1FlexGrid1.Cols.Count = dtStructure_Local.Rows.Count + 1
            'C1FlexGrid1.Cols.Add()
            C1FlexGrid1.Cols(1).Name = "nRowId"
            C1FlexGrid1.Cols(1).Caption = "nRowId"
            C1FlexGrid1.Cols(1).DataType = GetDataType("1234")
            C1FlexGrid1.Cols(1).AllowEditing = False
            C1FlexGrid1.Cols(1).Visible = False
            'C1FlexGrid1.AllowEditing = False
            'C1FlexGrid1.Cols("nRowID").Width = 0
            'Dim dtFilteredStructure As DataRow() = dtStructure_Local.Select()

            For Each _DataRow As DataRow In dtStructure_Local.Rows
                colName = _DataRow("Columnname").ToString()

                C1FlexGrid1.Cols(i).Name = _DataRow("Columnname").ToString()
                C1FlexGrid1.Cols(colName).Caption = _DataRow("Columnname").ToString()
                C1FlexGrid1.Cols(colName).DataType = GetDataType(_DataRow("Format")) ''Format
                C1FlexGrid1.Cols(colName).TextAlignFixed = TextAlignEnum.CenterCenter

                If InStr((_DataRow("Format")), "#") Then
                    C1FlexGrid1.Cols(colName).EditMask = GetMask(_DataRow("Format"))
                Else
                    C1FlexGrid1.Cols(colName).Format = GetMask(_DataRow("Format"))
                End If
                C1FlexGrid1.Cols(colName).Width = CType(_DataRow("Width"), Integer) ''Col width
                'Changed by Rahul  -uncommented for style 
                C1FlexGrid1.Cols(colName).Style.ForeColor = Color.FromArgb(_DataRow("ForeColor"))  ''Fore Color
                C1FlexGrid1.Cols(colName).Style.BackColor = Color.FromArgb(_DataRow("BackColor")) ''Back Color
                C1FlexGrid1.Cols(colName).Style.TextAlign = GetTextAlign(_DataRow("Alignment"))
                C1FlexGrid1.Cols(colName).Style.WordWrap = True
                i += 1
            Next
            'C1FlexGrid1.Styles.ClearUnused()
            C1FlexGrid1.AllowResizing = AllowResizingEnum.Rows
            C1FlexGrid1.AllowSorting = AllowSortingEnum.None

            C1FlexGrid1.AutoResize = True
            'C1FlexGrid1.Cols(1).Visible = False

            C1FlexGrid1.Update()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If Not IsNothing(dtStructure_Local) Then
                dtStructure_Local.Dispose()
                dtStructure_Local = Nothing
            End If
            If Not IsNothing(objFlowSheet_Local) Then
                objFlowSheet_Local.Dispose()
                objFlowSheet_Local = Nothing
            End If
        End Try

    End Sub



    Private Sub DeleteLocalFlowSheet(ByVal sFlowSheetName As String)
        Dim dv As DataView
        dv = dtFlowSheet.DefaultView
        dv.RowFilter = "sFlowSheetName <> '" & sFlowSheetName.Trim.Replace("'", "''") & "'"
        dtFlowSheet = dv.ToTable
    End Sub

    Private Sub InitializeFlowSheetTable()
        dtFlowSheet = New DataTable
        dtFlowSheet.Columns.Add("sFlowSheetName")
        dtFlowSheet.Columns.Add("sFieldName")
        dtFlowSheet.Columns.Add("sValue")
        dtFlowSheet.Columns.Add("sDataType")
        dtFlowSheet.Columns.Add("dWidth")
        dtFlowSheet.Columns.Add("sFormat")
        dtFlowSheet.Columns.Add("sAlignment")
        dtFlowSheet.Columns.Add("nTotalCols")
        dtFlowSheet.Columns.Add("nColNumber")
        dtFlowSheet.Columns.Add("nForeColor")
        dtFlowSheet.Columns.Add("nBackColor")

        'Added for Resolving FlowSheet Issuse.
        dtFlowSheet.Columns.Add("nRowId")

    End Sub

    Public Sub SavePatientFlowSheet()  '''''''' Made as Public function for Smart Diagnosis Changes
        Dim objFlowSheet_Local As clsFlowSheet = Nothing
        Try
            objFlowSheet_Local = New clsFlowSheet
            '' SAVE LAST OPENED FLOWSHEET IN LOCAL TABLE ''
            If _FlowSheetModified Then
                C1FlexGrid1.FinishEditing()
                SaveLocalFlowSheet(strFlowSheetName)
            End If

            If dtFlowSheet.Rows.Count = 0 Then
                'Added By Rahul Patel on 13-10-2010
                'For Deleting last record for flow sheet from grid i.e resolving issuse in case id GLO2010-0006166
                If C1FlexGrid1.Rows.Count = 2 Then
                    objFlowSheet_Local.DeletePatientFlowSheetBasedOnLastRow(strFlowSheetName, _PatientID)
                End If
                'End of code added by Rahul Patel
                Exit Sub
            End If

            If _VisitID <= 0 Then
                _VisitID = GenerateVisitID(_PatientID)
            End If
            'Change for resolving the flowSheet Issuse.
            objFlowSheet_Local.SaveFlowSheet(_VisitID, Convert.ToInt64(lblPatientCode.Tag), dtFlowSheet, dtFlowSheetDeletedRowId)
            ''Sanjog -Added on 2011 Jan 14 to show message on more than 1000 character
            If objFlowSheet_Local.blnIsSave = True Then
                blnSave = True
            Else
                blnSave = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Save Patient FlowSheet - " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(objFlowSheet_Local) Then
                objFlowSheet_Local.Dispose()
                objFlowSheet_Local = Nothing
            End If
        End Try
    End Sub
    '' END SUDHIR ''

    Private Sub cmbFlowSheet_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'C1FlexGrid1.Clear(ClearFlags.Style)
            C1FlexGrid1.Clear(ClearFlags.All)
            'setGridStyle(cmbFlowSheet.SelectedValue)

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub setGridStyle(ByVal Id As Long)
    '    Dim dt As DataTable

    '    objFlowSheet.SelectFlowSheet(Id)
    '    dt = objFlowSheet.GetDataview.Table

    '    If IsNothing(dt) Then
    '        Exit Sub
    '    End If

    '    C1FlexGrid1.Styles.ClearUnused()
    '    C1FlexGrid1.Rows.Count = 1

    '    Dim i As Integer
    '    '' 0-sFlowSheetName ,1-nCols ,2-nColNumber ,3-sColumnName ,4-sFormat ,5-dWidth ,6-sFontName ,7-nFontSize ,
    '    '' 8-nForeColor, 9-bIsBold, 10-bIsItalic, 11-bIsUnderline, 12-sAlignment, 13-nBackColor
    '    If dt.Rows.Count > 0 Then
    '        C1FlexGrid1.Cols.Count = dt.Rows(i)(1) + 1
    '        C1FlexGrid1.Cols(0).Width = 25

    '        For i = 0 To dt.Rows.Count - 1
    '            With C1FlexGrid1
    '                Dim j As Integer
    '                j = dt.Rows(i)(2)
    '                '' to Set FixedRow's Alingnment to Center
    '                .Cols(j).TextAlignFixed = TextAlignEnum.CenterCenter

    '                .Cols(j).Caption = CType(dt.Rows(i)(3), String) ''Column Name
    '                C1FlexGrid1.Cols(j).DataType = GetDataType(dt.Rows(i)(4)) ''Format
    '                If InStr((dt.Rows(i)(4)), "#") Then
    '                    C1FlexGrid1.Cols(j).EditMask = GetMask(dt.Rows(i)(4))
    '                Else
    '                    C1FlexGrid1.Cols(j).Format = GetMask(dt.Rows(i)(4))
    '                End If
    '                .Cols(j).Width = CType(dt.Rows(i)(5), Integer) ''Col width
    '                ' .Cols(j).Style.Font = CType(dt.Rows(i)(6), Font)  ''Font Name
    '                '.Cols(j).Style.Font = CType(dt.Rows(i)(7), Single)  ''Font Size
    '                .Cols(j).Style.ForeColor = Color.FromArgb(dt.Rows(i)(8))  ''Fore Color
    '                .Cols(j).Style.BackColor setGridStyle= Color.FromArgb(dt.Rows(i)(13)) ''Back Color
    '                .Cols(j).Style.TextAlign = GetTextAlign(dt.Rows(i)(12))
    '                '' Text Alignment 
    '                '.Font = CType(dt.Rows(i)(6), Object)
    '                '.FontHeight = dt.Rows(i)(7)
    '            End With
    '        Next
    '    End If
    '    C1FlexGrid1.Styles.ClearUnused()

    'End Sub


    ''COMMENT BY SUDHIR 2090407 ''
    'Private Sub setGridStyle(ByVal Id As Long)
    '    Dim dt As DataTable

    '    objFlowSheet.SelectFlowSheet(Id)
    '    dt = objFlowSheet.GetDataview.Table

    '    If IsNothing(dt) Then
    '        Exit Sub
    '    End If

    '    C1FlexGrid1.Styles.ClearUnused()
    '    C1FlexGrid1.Rows.Count = 1

    '    Dim i As Integer
    '    '' 0-sFlowSheetName ,1-nCols ,2-nColNumber ,3-sColumnName ,4-sFormat ,5-dWidth ,6-sFontName ,7-nFontSize ,
    '    '' 8-nForeColor, 9-bIsBold, 10-bIsItalic, 11-bIsUnderline, 12-sAlignment, 13-nBackColor
    '    If dt.Rows.Count > 0 Then
    '        C1FlexGrid1.Cols.Count = dt.Rows(i)(1) + 1
    '        C1FlexGrid1.Cols(0).Width = 25

    '        For i = 0 To dt.Rows.Count - 1
    '            With C1FlexGrid1
    '                Dim j As Integer
    '                j = dt.Rows(i)(2)
    '                '' to Set FixedRow's Alingnment to Center
    '                .Cols(j).TextAlignFixed = TextAlignEnum.CenterCenter

    '                .Cols(j).Caption = CType(dt.Rows(i)(3), String) ''Column Name
    '                C1FlexGrid1.Cols(j).DataType = GetDataType(dt.Rows(i)(4)) ''Format
    '                If InStr((dt.Rows(i)(4)), "#") Then
    '                    C1FlexGrid1.Cols(j).EditMask = GetMask(dt.Rows(i)(4))
    '                Else
    '                    C1FlexGrid1.Cols(j).Format = GetMask(dt.Rows(i)(4))
    '                End If
    '                .Cols(j).Width = CType(dt.Rows(i)(5), Integer) ''Col width
    '                ' .Cols(j).Style.Font = CType(dt.Rows(i)(6), Font)  ''Font Name
    '                '.Cols(j).Style.Font = CType(dt.Rows(i)(7), Single)  ''Font Size
    '                .Cols(j).Style.ForeColor = Color.FromArgb(dt.Rows(i)(8))  ''Fore Color
    '                .Cols(j).Style.BackColor = Color.FromArgb(dt.Rows(i)(13)) ''Back Color
    '                .Cols(j).Style.TextAlign = GetTextAlign(dt.Rows(i)(12))
    '                .Cols(j).Style.WordWrap = True
    '                '' Text Alignment 
    '                '.Font = CType(dt.Rows(i)(6), Object)
    '                '.FontHeight = dt.Rows(i)(7)
    '            End With
    '        Next
    '    End If
    '    C1FlexGrid1.Styles.ClearUnused()

    'End Sub
    'Old Function for Setting Gris Style.
    'Do Not Delete


    'Not In Use Can

    Private Sub setGridStyle_Old(ByVal flowSheetName As String)

        Try
            'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'objFlowSheet.SelectFlowSheet(flowSheetName, gnPatientID)
            objFlowSheet.SelectFlowSheet(flowSheetName, _PatientID)
            'end modification
            dtStructure = objFlowSheet.GetDataview.Table

            If IsNothing(dtStructure) Then
                Exit Sub
            End If
            'SHUBHANGI 20101125 RESOLVED 6635
            If dtStructure.Rows.Count = 0 Then
                trFlowsheetHistory.SelectedNode = trFlowsheetHistory.Nodes.Item(0)
                Dim myFlowSheetNode As myTreeNode
                myFlowSheetNode = CType(trFlowsheetHistory.SelectedNode, myTreeNode)
                objFlowSheet.SelectFlowSheet(myFlowSheetNode.Text, _PatientID)
                dtStructure = objFlowSheet.GetDataview.Table
            End If


            C1FlexGrid1.Styles.ClearUnused()
            C1FlexGrid1.Rows.Count = 1

            Dim i As Integer

            If dtStructure.Rows.Count > 0 Then
                'Shubhangi20091126
                ' Commented by Shubhangi B'coz to maintain inconsistancy between Column no & actual entered columns.
                Dim _max As Integer = 0
                For iRow As Integer = 0 To dtStructure.Rows.Count - 1
                    If _max < Convert.ToInt32(dtStructure.Rows(iRow)("ColumnNumber")) Then
                        _max = Convert.ToInt32(dtStructure.Rows(iRow)("ColumnNumber"))
                    End If
                Next
                'C1FlexGrid1.Cols.Count = dtStructure.Rows(i)("TotalColumns") + 1
                'C1FlexGrid1.Cols.Count = dtStructure.Rows.Count + 1
                C1FlexGrid1.Cols.Count = _max + 1
                C1FlexGrid1.Cols(0).Width = 25
            End If
            If dtStructure.Rows.Count > 0 Then

                '' TO GET MAX COLUMN NUMBER FROM MASTER FLOWSHEET ''
                Dim _Max As Integer = 0
                For iRow As Integer = 0 To dtStructure.Rows.Count - 1
                    If _Max < Val(dtStructure.Rows(iRow)("ColumnNumber")) Then
                        _Max = Val(dtStructure.Rows(iRow)("ColumnNumber"))
                    End If
                Next
                '' END MAX ''


                C1FlexGrid1.Cols.Count = _Max + 1
                'C1FlexGrid1.Cols.Count = dtStructure.Rows(i)("TotalColumns") + 1
                'C1FlexGrid1.Cols.Count = dtStructure.Rows.Count + 1
                C1FlexGrid1.Cols(0).Width = 25

                For i = 0 To dtStructure.Rows.Count - 1

                    Dim j As Integer
                    j = dtStructure.Rows(i)("ColumnNumber")
                    ' j = dtStructure.Rows(i)("")
                    '' to Set FixedRow's Alingnment to Center
                    C1FlexGrid1.Cols(j).TextAlignFixed = TextAlignEnum.CenterCenter

                    C1FlexGrid1.Cols(j).Caption = CType(dtStructure.Rows(i)("ColumnName"), String) ''Column Name
                    C1FlexGrid1.Cols(j).DataType = GetDataType(dtStructure.Rows(i)("Format")) ''Format
                    If InStr((dtStructure.Rows(i)("Format")), "#") Then
                        C1FlexGrid1.Cols(j).EditMask = GetMask(dtStructure.Rows(i)("Format"))
                    Else
                        C1FlexGrid1.Cols(j).Format = GetMask(dtStructure.Rows(i)("Format"))
                    End If
                    C1FlexGrid1.Cols(j).Width = CType(dtStructure.Rows(i)("Width"), Integer) ''Col width
                    ' .Cols(j).Style.Font = CType(dt.Rows(i)(6), Font)  ''Font Name
                    '.Cols(j).Style.Font = CType(dt.Rows(i)(7), Single)  ''Font Size
                    C1FlexGrid1.Cols(j).Style.ForeColor = Color.FromArgb(dtStructure.Rows(i)("ForeColor"))  ''Fore Color
                    C1FlexGrid1.Cols(j).Style.BackColor = Color.FromArgb(dtStructure.Rows(i)("BackColor")) ''Back Color
                    C1FlexGrid1.Cols(j).Style.TextAlign = GetTextAlign(dtStructure.Rows(i)("Alignment"))
                    C1FlexGrid1.Cols(j).Style.WordWrap = True
                    '' Text Alignment 
                    '.Font = CType(dt.Rows(i)(6), Object)
                    '.FontHeight = dt.Rows(i)(7)

                Next
            End If

            C1FlexGrid1.Styles.ClearUnused()
            C1FlexGrid1.AllowResizing = AllowResizingEnum.Rows

            C1FlexGrid1.AutoResize = True



        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Private Function GetDataType(ByVal StrType As String) As Type

        Select Case StrType
            Case "General"
                Return GetType(System.String)
                ''cmbFormat.Items.Add("1234") ''Int64
            Case "1234"
                Return GetType(System.Int64)
            Case "1234.00"
                Return GetType(System.Double)
            Case "-1,234.00"
                Return GetType(System.Double)
            Case "(1,234.00)"
                Return GetType(System.Double)
            Case "(1234.00)"
                Return GetType(System.Double)
            Case "-1234.00"
                Return GetType(System.Double)
            Case "-$1234.00"
                Return GetType(System.Double)
            Case "$1,234.00"
                Return GetType(System.Double)

            Case "Percentage"
                Return GetType(System.Double)

            Case "MM/DD/YYYY"
                Return GetType(System.DateTime)
            Case "DD/MM/YYYY"
                Return GetType(System.DateTime)

            Case "DD/MMMM/YYYY"
                Return GetType(System.DateTime)

            Case "MMMM DD/YYYY"
                Return GetType(System.DateTime)

            Case "HH:MM:ss"
                Return GetType(System.DateTime)
            Case "HH:MM"
                Return GetType(System.DateTime)
            Case "HH:MM:ss PM"
                Return GetType(System.DateTime)
            Case "HH:MM PM"
                Return GetType(System.DateTime)
            Case "Masked"
                Return GetType(System.String)
            Case Else
                Return GetType(System.String)

        End Select
    End Function

    Private Function GetMask(ByVal StrType As String) As String
        Dim myString As String = String.Empty
        Select Case StrType

            Case "1234"
                myString = "####"
            Case "1234.00"
                myString = "####.##"
            Case "-1,234.00"
                myString = "-#,###.##"
            Case "(1,234.00)"
                myString = "(#,###.##)"
            Case "(1234.00)"
                myString = "(####.##)"

            Case "-1234.00"
                myString = "-####.##"
            Case "-$1234.00"
                myString = "-$####.##"
            Case "$1,234.00"
                myString = "$#,###.##"
            Case "Percentage"
                myString = "0%"
            Case "MM/DD/YYYY"
                myString = "MM/dd/yyyy"
            Case "DD/MM/YYYY"
                myString = "dd/MM/yyyy"
            Case "MMMM DD/YYYY"
                myString = "MMMM dd/yyyy"
            Case "DD/MMMM/YYYY"
                Return "dd/MMMM/yyyy"
            Case "HH:MM:ss"
                myString = "hh:mm:ss"
            Case "HH:MM"
                myString = "hh:mm"
            Case "HH:MM:ss PM"
                myString = "hh:mm:ss tt"
            Case "HH:MM PM"
                myString = "hh:mm tt"
            Case StrType
                myString = StrType
        End Select
        Return myString
    End Function

    'Private Function GetTextAlign(ByVal StrAlign As String) As [Enum]
    Private Function GetTextAlign(ByVal StrAlign As String) As TextAlignEnum
        '  Dim obje1 As enmAlign
        Select Case StrAlign
            Case "Left"
                Return TextAlignEnum.LeftCenter
            Case "Center"
                Return TextAlignEnum.CenterCenter
            Case "Right"
                Return TextAlignEnum.RightCenter
            Case Else
                Return Nothing
        End Select
    End Function

    Public Sub ShowHidePreviousFlowSheet()
        If pnlPrevFlowSheet.Visible = False Then
            pnlPrevFlowSheet.Visible = True
            'btnPrevHistory.Text = "Hide Prev History"
            tls_btnShow.Text = " &Hide "
            tls_btnShow.Image = Global.gloEMR.My.Resources.Resources.Hide
            tls_btnShow.ImageAlign = ContentAlignment.MiddleCenter
            'tls_btnShow.ToolTipText = "Hide Patient Flow Sheet History" '' BUG : 1242, SUDHIR 20090620 ''
            tls_btnShow.ToolTipText = "Hide"
            'PopulateFlowsheetHistory()
            trFlowsheetHistory.ExpandAll()
        Else
            pnlPrevFlowSheet.Visible = False
            'btnPrevHistory.Text = "Show Prev History"
            tls_btnShow.Text = " &Show "
            tls_btnShow.Image = Global.gloEMR.My.Resources.Resources.Show
            tls_btnShow.ImageAlign = ContentAlignment.MiddleCenter
            'tls_btnShow.ToolTipText = "Show Patient Flow Sheet History"  '' BUG : 1242, SUDHIR 20090620 ''
            tls_btnShow.ToolTipText = "Show"
        End If
    End Sub
    'function commented as not in use
    'Private Sub PopulateFlowsheetHistory()

    '    trFlowsheetHistory.Nodes.Clear()

    '    Dim rootnode As myTreeNode
    '    rootnode = New myTreeNode("Flow Sheet History", -1)
    '    rootnode.ImageIndex = 2
    '    rootnode.SelectedImageIndex = 2
    '    trFlowsheetHistory.Nodes.Add(rootnode)
    '    trFlowsheetHistory.ExpandAll()

    '    Dim mychild As myTreeNode

    '    mychild = New myTreeNode("Current", 0, "Current")
    '    mychild.ForeColor = Color.Blue

    '    If objFlowSheet.CheckRecordCount("C") Then
    '        mychild.ImageIndex = 8
    '        mychild.SelectedImageIndex = 8
    '    Else
    '        mychild.ImageIndex = 9
    '        mychild.SelectedImageIndex = 9
    '    End If
    '    rootnode.Nodes.Add(mychild)

    '    mychild = New myTreeNode("Yesterday", 1, "Yesterday")
    '    mychild.ForeColor = System.Drawing.Color.FromArgb(46, 14, 140)

    '    If objFlowSheet.CheckRecordCount("Y") Then
    '        mychild.ImageIndex = 8
    '        mychild.SelectedImageIndex = 8
    '    Else
    '        mychild.ImageIndex = 9
    '        mychild.SelectedImageIndex = 9
    '    End If
    '    rootnode.Nodes.Add(mychild)

    '    mychild = New myTreeNode("Last Week", 2, "Last Week")
    '    mychild.ForeColor = System.Drawing.Color.FromArgb(188, 0, 169)

    '    If objFlowSheet.CheckRecordCount("W") Then
    '        mychild.ImageIndex = 8
    '        mychild.SelectedImageIndex = 8
    '    Else
    '        mychild.ImageIndex = 9
    '        mychild.SelectedImageIndex = 9
    '    End If
    '    rootnode.Nodes.Add(mychild)

    '    mychild = New myTreeNode("Last Month", 3, "Last Month")
    '    mychild.ForeColor = System.Drawing.Color.FromArgb(25, 142, 255)

    '    If objFlowSheet.CheckRecordCount("M") Then
    '        mychild.ImageIndex = 8
    '        mychild.SelectedImageIndex = 8
    '    Else
    '        mychild.ImageIndex = 9
    '        mychild.SelectedImageIndex = 9
    '    End If
    '    rootnode.Nodes.Add(mychild)

    '    mychild = New myTreeNode("Older", 4, "Older")
    '    mychild.ForeColor = System.Drawing.Color.FromArgb(39, 69, 100)

    '    If objFlowSheet.CheckRecordCount("O") Then
    '        mychild.ImageIndex = 8
    '        mychild.SelectedImageIndex = 8
    '    Else
    '        mychild.ImageIndex = 9
    '        mychild.SelectedImageIndex = 9
    '    End If
    '    rootnode.Nodes.Add(mychild)
    'End Sub

    Private Sub trFlowsheetHistory_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trFlowsheetHistory.KeyPress
        Try
            If e.KeyChar = ChrW(13) Then
                trFlowsheetHistory_DoubleClick(sender, e)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient ROS", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trFlowsheetHistory_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trFlowsheetHistory.MouseDown
        Try
            If e.Button = MouseButtons.Right Then

                Dim trvNode As TreeNode
                trvNode = trFlowsheetHistory.GetNodeAt(e.X, e.Y)
                If IsNothing(trvNode) = False Then
                    trFlowsheetHistory.SelectedNode = trvNode

                    If IsNothing(trFlowsheetHistory.SelectedNode) = False Then
                        Dim FlowSheetNode As myTreeNode
                        FlowSheetNode = trFlowsheetHistory.SelectedNode
                        'Try
                        '    If (IsNothing(trFlowsheetHistory.ContextMenu) = False) Then
                        '        trFlowsheetHistory.ContextMenu.Dispose()
                        '        trFlowsheetHistory.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trFlowsheetHistory.ContextMenu = cntFlowSheetHistory
                    Else
                        'Try
                        '    If (IsNothing(trFlowsheetHistory.ContextMenu) = False) Then
                        '        trFlowsheetHistory.ContextMenu.Dispose()
                        '        trFlowsheetHistory.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trFlowsheetHistory.ContextMenu = Nothing
                    End If
                Else
                    'Try
                    '    If (IsNothing(trFlowsheetHistory.ContextMenu) = False) Then
                    '        trFlowsheetHistory.ContextMenu.Dispose()
                    '        trFlowsheetHistory.ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    trFlowsheetHistory.ContextMenu = Nothing
                End If
            Else
                'Try
                '    If (IsNothing(trFlowsheetHistory.ContextMenu) = False) Then
                '        trFlowsheetHistory.ContextMenu.Dispose()
                '        trFlowsheetHistory.ContextMenu = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                trFlowsheetHistory.ContextMenu = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Set_RecordLock(ByVal locked As Boolean)
        If locked = True Then
            tls_btnFinish.Enabled = False
            tls_btnSetting.Enabled = False
            tls_btnPreview.Enabled = False
            tls_btnPrint.Enabled = False
        Else
            tls_btnFinish.Enabled = True
            tls_btnSetting.Enabled = True
            tls_btnPreview.Enabled = True
            tls_btnPrint.Enabled = True
        End If
    End Sub

    'Private Sub trFlowsheetHistory_DoubleClick_OLD(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trFlowsheetHistory.DoubleClick
    '    Dim dtResult As DataTable
    '    Try
    '        If IsNothing(trFlowsheetHistory.SelectedNode) = False Then
    '            '' SUDHIR '' SAVE PREVIOUS FLOWSHEET IN LOCAL TABLE BEFORE OPENING OTHER FLOWSHEET ''
    '            If _FlowSheetModified Then
    '                SaveLocalFlowSheet(strFlowSheetName)
    '            End If



    '            'If Not trFlowsheetHistory.SelectedNode Is trFlowsheetHistory.Nodes(0) Then
    '            tls_btnFinish.Enabled = True ''User Can save FlowSheetDetail when FlowSheet has selected.
    '            tls_btnPreview.Enabled = True
    '            tls_btnPrint.Enabled = True

    '            _FlowSheetModified = False

    '            Dim myFlowSheetNode As myTreeNode

    '            myFlowSheetNode = CType(trFlowsheetHistory.SelectedNode, myTreeNode)


    '            strFlowSheetName = myFlowSheetNode.Text
    '            intFlowSheetID = myFlowSheetNode.Key '' FlowSheet ID FlowSheet Master
    '            ''myFlowSheetNode.Tag ''  FlowsheetRecordID Patient FlowSheet

    '            ''''''''''<><><> Record Level Locking <><><><>
    '            If gblnRecordLocking = True Then
    '                Dim mydt As New mytable
    '                mydt = Scan_n_Lock_Transaction(TrnType.Flowsheet, myFlowSheetNode.Tag, 0, Now)
    '                If mydt.Description <> gstrClientMachineName Then
    '                    If MessageBox.Show("This Flowsheet is being modified by " & mydt.Code & " on " & mydt.Description & ". Do you want to open the History for view ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
    '                        '' Open For view 
    '                        _blnRecordLock = True
    '                    Else
    '                        '' 
    '                        Exit Sub
    '                    End If
    '                Else
    '                    '' If Flow Sheet is not locked 
    '                    If _blnRecordLock = True Then
    '                        '' Currently Opened Flowsheet is locked by some other User on other Machine
    '                        '' do nothing
    '                    Else
    '                        '' Currently Opened FlowSheet is locked by current User on same Machine
    '                        '' Unlock Currently Opened FlowSheet  , Pass Currently Opened 
    '                        Call UnLock_Transaction(TrnType.Flowsheet, _FlowSheetRecordID, 0, Now)
    '                    End If
    '                    _blnRecordLock = False
    '                End If

    '                Call Set_RecordLock(_blnRecordLock)
    '                '''' <><><> Record Level Locking <><><><> 
    '            End If

    '            '' CHECK WHETHER FLOWSHEET PRESENT IN LOCAL TABLE OR NOT AND OPEN ACCORDINGLY ''
    '            Dim dv As DataView
    '            dv = dtFlowSheet.DefaultView
    '            dv.RowFilter = "sFlowSheetName = '" & myFlowSheetNode.Text.Trim.Replace("'", "''") & "'"
    '            dtResult = dv.ToTable
    '            If dtResult.Rows.Count = 0 Then
    '                dtResult = objFlowSheet.ScanFlowSheet(myFlowSheetNode.Text.Trim, _PatientID)
    '            End If

    '            _FlowSheetRecordID = myFlowSheetNode.Tag

    '            _IsLoading = True
    '            If IsNothing(dtResult) = False Then
    '                If dtResult.Rows.Count > 0 Then

    '                    ''COMMENT BY SUDHIR 20081212
    '                    ' '' Update
    '                    'Dim mstream As ADODB.Stream
    '                    'Dim strFileName As String
    '                    'mstream = New ADODB.Stream
    '                    'mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
    '                    'mstream.Open()

    '                    'mstream.Write(dtResult.Rows(0)(1)) '' Flow Sheet Image
    '                    ''cmbFlowSheet.SelectedItem = dt.Rows(0)(0)

    '                    ''cmbFlowSheet.Visible = False
    '                    ''lblFlowSheetName.Visible = True
    '                    ''lblFlowSheetName.Text = FlowSheetNode.Text
    '                    ''lblFlowSheetName.Tag = dt.Rows(0)(0)  '' FlowSheetID 

    '                    'strFileName = Application.StartupPath & "\Temp\TempFlowSheet.txt"
    '                    'mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
    '                    'mstream.Close()
    '                    ''SUDHIR COMMENT 

    '                    'C1FlexGrid1.Clear(ClearFlags.Style)

    '                    C1FlexGrid1.Clear(ClearFlags.All)


    '                    ' Dim tmp As Long = dtResult.Rows(0)(0) 
    '                    '' FlowsheetMSTID

    '                    'Call setGridStyle(dtResult.Rows(0)(0)) ''ORIGINAL
    '                    ''sudhir 20081212
    '                    C1FlexGrid1.DataSource = Nothing
    '                    setGridStyle(myFlowSheetNode.Text)

    '                    ''To fill FlexGrid from database
    '                    Dim dtFlex As New DataTable ''datatable to be bind to flexGrid
    '                    For i As Int32 = 1 To C1FlexGrid1.Cols.Count - 1  ''Setting Columns
    '                        ''dtFlex.Columns.Add(C1FlexGrid1.Rows(0)(i).ToString) '' COMMENT BY SUDHIR 20100114 '' AS COLUMN NAME OF C1 IS NOT CONSIDERED IN FUTURE CODE OF dtFlex. ANY COLUMN NAME COULD BE ASSIGNED ''
    '                        dtFlex.Columns.Add("COLUMN " & i)
    '                    Next

    '                    Dim dtResultIndex As Int32 = 0
    '                    Dim newRow As DataRow

    '                    ''Read each value from database and store as a datarow.
    '                    While dtResultIndex < dtResult.Rows.Count
    '                        newRow = dtFlex.NewRow
    '                        For i As Int32 = 0 To dtFlex.Columns.Count - 1
    '                            newRow.Item(i) = dtResult.Rows.Item(dtResultIndex)("sValue")
    '                            dtResultIndex += 1



    '                            '' OUT OF ROW READ HANDLED ''
    '                            If dtResultIndex >= dtResult.Rows.Count Then
    '                                dtFlex.Rows.Add(newRow)
    '                                Exit While
    '                            End If


    '                        Next
    '                        dtFlex.Rows.Add(newRow)
    '                    End While

    '                    'C1FlexGrid1.DataSource = dtFlex

    '                    For i As Int32 = 0 To dtFlex.Rows.Count - 1
    '                        C1FlexGrid1.Rows.Insert(C1FlexGrid1.Rows.Count)
    '                        For j As Int32 = 0 To dtFlex.Columns.Count - 1
    '                            C1FlexGrid1.Rows.Item(i + 1)(j + 1) = dtFlex.Rows.Item(i)(j)
    '                            ''shubhangi
    '                            Dim icnt As String = CType(dtFlex.Rows.Item(i)(j), String)
    '                            Dim noofChar As Int32
    '                            Dim lengthchar As String = icnt.Length
    '                            noofChar = (lengthchar / 8)
    '                            'Dim str As String
    '                            'If noofChar <> 0 Then
    '                            '    If C1FlexGrid1.Rows(i + 1).Height = -1 Then
    '                            '        'C1FlexGrid1.Rows(i + 1).Height = C1FlexGrid1.Rows(i + 1).Height + 35
    '                            '        'Dim width As Int32 = dtResult.Rows(i)("dWidth")
    '                            '        'C1FlexGrid1.Rows(i + 1).Height = ((width) / (15 * noofChar)) * 15
    '                            '        C1FlexGrid1.Rows(i + 1).Height = noofChar * 15

    '                            '    End If
    '                            'End If
    '                            'end
    '                            If noofChar <> 0 Then
    '                                'str = C1FlexGrid1.Editor.Text
    '                                'C1FlexGrid1.Rows(i + 1)(j + 1). = 35
    '                                C1FlexGrid1.Rows(i + 1).Height = C1FlexGrid1.Rows(i + 1).Height + 35
    '                            End If
    '                        Next

    '                    Next
    '                    ''end sudhir 


    '                    'C1FlexGrid1.LoadGrid(strFileName, C1.Win.C1FlexGrid.FileFormatEnum.TextTab, C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells) ' ORIGINAL
    '                    ''C1FlexGrid1.WriteXml(Application.StartupPath & "\Temp\TempFlowSheet.XML")
    '                    C1FlexGrid1.Row = C1FlexGrid1.Rows.Count - 1

    '                    'If C1FlexGrid1.Row = 0 Then
    '                    '    C1FlexGrid1.Row = 1 '' C1FlexGrid1.Rows.Count
    '                    'End If

    '                    'If C1FlexGrid1.Row <> 0 Then
    '                    '    Dim i As Integer
    '                    '    For i = 1 To C1FlexGrid1.Cols.Count - 1
    '                    '        If C1FlexGrid1.Cols(i).DataType Is GetType(System.DateTime) Then
    '                    '            C1FlexGrid1.SetData(C1FlexGrid1.Row, i, Now.Date)
    '                    '        End If
    '                    '    Next
    '                    'End If
    '                    If C1FlexGrid1.Row = 0 Then
    '                        C1FlexGrid1.Row = 1 '' C1FlexGrid1.Rows.Count
    '                    ElseIf C1FlexGrid1.Row = 1 Then
    '                        C1FlexGrid1.Rows.Add()
    '                        C1FlexGrid1.Rows.Move(C1FlexGrid1.Row - 1, C1FlexGrid1.Row)
    '                        C1FlexGrid1.Row = 2
    '                    End If

    '                    If C1FlexGrid1.Row >= 1 Then
    '                        For i As Int32 = 1 To C1FlexGrid1.Cols.Count - 1
    '                            If C1FlexGrid1.Cols(i).DataType Is GetType(System.DateTime) Then
    '                                If IsDBNull(C1FlexGrid1.GetData(C1FlexGrid1.Row, i)) Or C1FlexGrid1.GetData(C1FlexGrid1.Row, i) Is Nothing Then
    '                                    C1FlexGrid1.SetData(C1FlexGrid1.Row, i, Now.Date)
    '                                End If
    '                            End If
    '                        Next
    '                    End If

    '                    If Not IsNothing(dtFlex) Then    'obj Disposed by mitesh
    '                        dtFlex.Dispose()
    '                        dtFlex = Nothing
    '                    End If


    '                    blnModify = True
    '                    ''''''''''''''''''
    '                    'setGridStyle(tmp)

    '                Else
    '                    C1FlexGrid1.Clear(ClearFlags.All)
    '                    setGridStyle(myFlowSheetNode.Text)
    '                End If
    '            Else
    '                _IsLoading = True
    '                C1FlexGrid1.Clear(ClearFlags.All)
    '                setGridStyle(myFlowSheetNode.Text)
    '            End If
    '        End If

    '        _IsLoading = False


    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        If Not IsNothing(dtResult) Then    'obj Disposed by mitesh
    '            dtResult.Dispose()
    '            dtResult = Nothing
    '        End If
    '    End Try

    'End Sub

    Private Sub mnuDeleteFlowSheet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteFlowSheet.Click
        Try
            If MessageBox.Show("Are you sure to delete this Flowsheet for Selected patient?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                DeleteLocalFlowSheet(trFlowsheetHistory.SelectedNode.Text)
                If objFlowSheet.DeletePatientFlowSheet(trFlowsheetHistory.SelectedNode.Tag, trFlowsheetHistory.SelectedNode.Text, _PatientID) = True Then

                    'trFlowsheetHistory.SelectedNode.Tag = 0
                    trFlowsheetHistory.SelectedNode.ForeColor = Color.Green
                    If trFlowsheetHistory.SelectedNode.Text = strFlowSheetName Then
                        _IsLoading = True
                        C1FlexGrid1.Clear(ClearFlags.All)
                        C1FlexGrid1.Rows.Count = 1
                        _IsLoading = False
                    End If
                    If objFlowSheet.IsFlowSheetPresent(trFlowsheetHistory.SelectedNode.Text, _PatientID) = False Then
                        trFlowsheetHistory.Nodes.Remove(trFlowsheetHistory.SelectedNode)
                    End If
                End If
            End If

            trFlowsheetHistory_DoubleClick(sender, e)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub InvokePrint(ByRef frm As Form, ByVal dggrid As DataGrid)

        Try


            'Instantiate GridPrinter class
            If GridPrinter Is Nothing Then
                GridPrinter = New DataGridPrinter(dggrid)
            End If

            'Invoke PrintDialog
            With Me.PrintDialog1
                .Document = GridPrinter.PrintDocument
                Dim objReportHeadercol As New Collection
                Dim objPageHeadercol As New Collection
                Dim objDetailscol As New Collection
                Dim objPageFootercol As New Collection
                Dim objReportFootercol As New Collection

                Dim objcol1 As New ArrayList


                'Fill Temporary sections collection 
                Dim objcontrol As Control
                For Each objcontrol In frm.Controls
                    If Not objcontrol.Tag Is Nothing Then
                        If objcontrol.Tag.substring(0, 1) = "1" Then
                            objReportHeadercol.Add(objcontrol)
                        ElseIf objcontrol.Tag.substring(0, 1) = "2" Then
                            objPageHeadercol.Add(objcontrol)
                       
                        ElseIf objcontrol.Tag.substring(0, 1) = "3" Then
                            objcol1.Add(objcontrol)
                        ElseIf objcontrol.Tag.substring(0, 1) = "4" Then
                            objPageFootercol.Add(objcontrol)
                        End If
                    End If

                Next
                'Populate Sections Collection
                GridPrinter.SetHeaderControls(objReportHeadercol, 1)
                GridPrinter.SetHeaderControls(objPageHeadercol, 2)
                GridPrinter.SetHeaderControls(objPageFootercol, 4)
                GridPrinter.SetHeaderControls(objReportFootercol, 5)

                If Not (storedPageSettings Is Nothing) Then
                    .Document.DefaultPageSettings = storedPageSettings
                End If
                If gblnUseDefaultPrinter = True Then
                    GridPrinter.Print()
                Else
                    'Invoke the Print Method of GridPrinter
                    If .ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                        GridPrinter.Print()
                    End If
                End If
                objReportHeadercol.Clear()
                objPageHeadercol.Clear()
                objDetailscol.Clear()
                objPageFootercol.Clear()
                objReportFootercol.Clear()
                objcol1.Clear()
            End With
            PrintDialog1.Document = Nothing

            If (IsNothing(GridPrinter) = False) Then
                GridPrinter.Dispose()
                GridPrinter = Nothing
            End If
            'dggrid = Nothing
            ' frm = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Dim WithEvents PDB As New ToolStripButton("Printer")

    Private Sub PDB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PDB.Click
        MessageBox.Show("Clicked!")
    End Sub

    Private Sub InvokePrintPreview(ByRef frm As Form, ByVal dggrid As DataGrid)
        Try

            'Instantiate GridPrinter class
            If GridPrinter Is Nothing Then
                GridPrinter = New DataGridPrinter(dggrid)
            End If

            'Invoke PrintDialog

            With Me.PrintPreviewDialog1
                '.Controls.Item(0).Enabled = False
                '.Controls.Item(1).Enabled = False

                .Document = GridPrinter.PrintDocument
                Dim objReportHeadercol As New Collection
                Dim objPageHeadercol As New Collection
                Dim objDetailscol As New Collection
                Dim objPageFootercol As New Collection
                Dim objReportFootercol As New Collection

                Dim objcol1 As New ArrayList

                'Fill Temporary sections collection 
                Dim objcontrol As Control
                For Each objcontrol In frm.Controls
                    If Not objcontrol.Tag Is Nothing Then
                        If objcontrol.Tag.substring(0, 1) = "1" Then
                            objReportHeadercol.Add(objcontrol)
                        ElseIf objcontrol.Tag.substring(0, 1) = "2" Then
                            objPageHeadercol.Add(objcontrol)
                        ElseIf objcontrol.Tag.substring(0, 1) = "3" Then
                            objcol1.Add(objcontrol)
                        ElseIf objcontrol.Tag.substring(0, 1) = "4" Then
                            objPageFootercol.Add(objcontrol)
                        End If
                    End If

                Next
                'Populate Sections Collection
                GridPrinter.SetHeaderControls(objReportHeadercol, 1)
                GridPrinter.SetHeaderControls(objPageHeadercol, 2)
                GridPrinter.SetHeaderControls(objPageFootercol, 4)
                GridPrinter.SetHeaderControls(objReportFootercol, 5)

                If Not (storedPageSettings Is Nothing) Then
                    .Document.DefaultPageSettings = storedPageSettings
                End If


                'Invoke the Print Method of GridPrinter
                If .ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                    GridPrinter.Print()
                End If
                objReportHeadercol.Clear()
                objPageHeadercol.Clear()
                objDetailscol.Clear()
                objPageFootercol.Clear()
                objReportFootercol.Clear()
                objcol1.Clear()

            End With
            PrintPreviewDialog1.Document = Nothing

            If (IsNothing(GridPrinter) = False) Then
                GridPrinter.Dispose()
                GridPrinter = Nothing
            End If
            'dggrid = Nothing
            'frm = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try

    End Sub

 

    'Private preview As Boolean = False
    'Private Sub PrintDocument1_BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles PrintDocument1.BeginPrint
    '    If preview = True Then e.Cancel = True
    'End Sub

    Private Sub HideColumn(ByVal dggrid As DataGrid)
        Dim ts As New DataGridTableStyle

        Dim dgID As DataGridTextBoxColumn
        Dim i As Int16
        For i = 2 To C1FlexGrid1.Cols.Count - 1
            dgID = New DataGridTextBoxColumn
            With dgID

                '.Alignment = HorizontalAlignment.Center
                '.NullText = ""
                .Width = C1FlexGrid1.Cols.Item(i).Width
                .MappingName = C1FlexGrid1.Cols.Item(i).Caption
                .HeaderText = C1FlexGrid1.Cols.Item(i).Caption
            End With
            ts.GridColumnStyles.Add(dgID)
            dgID = Nothing
        Next
        dggrid.TableStyles.Clear()
        dggrid.TableStyles.Add(ts)

    End Sub

    Private Sub tblFlowSheet_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsFlowSheet.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Finish"
                    ''  20070703
                    '' Save Changes to Finished [Save & Close]
                    _isSaveClicked = True
                    Save_Flowsheet()

                    If blnSave = True Then
                        '_isClose = True
                        '  Me.Close()
                        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                    End If

                    ''Sanjog -Added on 2011 Jan 14 to show message on more than 1000 character
                Case "Close"
                    If _blnFormLock Then
                        _isChanges = False
                    End If
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                    ' Me.Close()
                Case "Print"
                    blnPrint = True
                    Call SavePatientFlowSheet()
                    lblVisitDate.Text = Now.Date
                    lblVisitDate.Tag = -1
                    blnModify = False
                    ''Sanjog -Added on 2011 Jan 14 to show message on more than 1000 character
                    If C1FlexGrid1.Rows.Count > 1 And blnSave = True Then

                        Dim Copied As Boolean = False
                        If gloGlobal.gloTSPrint.isCopyPrint Then
                            Copied = PrintFlowSheet_ClinicalQueue(blnPrint, sFlwShtFilePath) ''added in 8081-replace DataGridPrinter

                            ''CopiedbyCCQPrint = true --means it will NOT Print
                            ''CopiedbyCCQPrint = dalse --means it will print
                            ''''we deliberately set flow sheet file name to blank so that it will not print 
                            If Copied = True And CopiedbyCCQPrint = False Then ''''CopiedbyCCQPrint =false means since sometime mapped drive is not set then we get message box to select printing and if we set NO then the value is set to false
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Flow Sheet print through clinical queue service", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            Else
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Unable or rejected to print flow Sheet data using clinical queue service", gloAuditTrail.ActivityOutCome.Failure)
                            End If

                        End If
                        If Copied = False Then
                            ''''if clinical printing is return false then send to normal printing.
                            PrintFlowSheet(blnPrint)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Flow Sheet printed through normal print method", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        End If
                    End If

                Case "Preview"
                    blnPrint = False
                    Call SavePatientFlowSheet()
                    lblVisitDate.Text = Now.Date
                    lblVisitDate.Tag = -1
                    blnModify = False
                    ''Sanjog -Added on 2011 Jan 14 to show message on more than 1000 character
                    If C1FlexGrid1.Rows.Count > 1 And blnSave = True Then

                        Dim Copied As Boolean = False
                        If gloGlobal.gloTSPrint.isCopyPrint Then
                            Copied = PrintFlowSheet_ClinicalQueue(blnPrint, sFlwShtFilePath) ''added in 8081-replace DataGridPrinter
                            ''''audit trail added in flowsheet preview form, hence no need to add it here
                            If Copied = False Then
                                ''''if clinical printing is return false then send to normal word docprinting.
                                PrintFlowSheet(True) ''''send true because print prieview is already shown so dont call old print preview functionality.
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Flow Sheet printed through normal print method", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            End If
                        Else
                            PrintFlowSheet(blnPrint)
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Print, "Flow Sheet printed through normal print method", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        End If
                        

                    End If

                Case "Setting"
                    PageSettings()
                Case "Show"
                    ShowHidePreviousFlowSheet()
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Save_Flowsheet()

        Call SavePatientFlowSheet()
        lblVisitDate.Text = Now.Date
        lblVisitDate.Tag = -1
        blnModify = False
        ''Sanjog -Added on 2011 Jan 14 to show message on more than 1000 character
        'If blnSave = True And _isClose = False Then
        '    '_isClose = True
        '    Me.Close()
        'End If
    End Sub
    Private Sub txtSearchFlowsheetHistory_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearchFlowsheetHistory.KeyPress
        'Shubhangi 20090825 for Key press event
        If (e.KeyChar = ChrW(13)) Then
            trFlowsheetHistory.Select()
        Else
            ' trFlowsheetHistory.SelectedNode = trFlowsheetHistory.Nodes.Item(0)
        End If
    End Sub
    ''Sandip Darade 20090505
    ''search  flowsheet tree
    Private Sub txtSearchFlowsheetHistory_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchFlowsheetHistory.TextChanged
        Dim tdt As DataTable = Nothing
        Dim dtResult As New DataTable
        Try

            'Shubhangi 20090825 For InString Search  
            Dim SearchFlowsheet As String
            SearchFlowsheet = txtSearchFlowsheetHistory.Text

            'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'tdt = objFlowSheet.GetAllFlowSheet(gnPatientID).Table()
            tdt = objFlowSheet.GetAllFlowSheet(_PatientID).Table()
            'end modification

            'For appling row filter 
            Dim dv As New DataView(tdt)
            dv.RowFilter = "sFlowSheetName Like '%" & SearchFlowsheet.Trim.Replace("'", "''") & "%'"
            tdt = dv.ToTable

            'tdt contain all data which we want to display in Flowshhet tree view
            If Not tdt Is Nothing Then
                trFlowsheetHistory.Nodes.Clear()

                For i As Integer = 0 To tdt.Rows.Count - 1
                    dtResult = Nothing

                    'Display the data for which   we enter data
                    'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                    'dtResult = objFlowSheet.SelectPatientFlowSheet(gnPatientID, CType(tdt.Rows(i)("sFlowSheetName"), String))
                    dtResult = objFlowSheet.SelectPatientFlowSheet(_PatientID, CType(tdt.Rows(i)("sFlowSheetName"), String))
                    'end modification
                    Dim newFlowsheetNode As New myTreeNode
                    newFlowsheetNode.Tag = CType(tdt.Rows(i)(0).ToString, Long)
                    newFlowsheetNode.Text = CType(tdt.Rows(i)(1).ToString, String)

                    If IsNothing(dtResult) = False AndAlso dtResult.Rows.Count > 0 Then

                        'Checking for NULL
                        If IsDBNull(dtResult.Rows(0)) = False Then
                            newFlowsheetNode.Tag = CType(dtResult.Rows(0)("nFlowSheetRecordID"), Long)
                            newFlowsheetNode.ForeColor = Color.Red
                        Else
                            newFlowsheetNode.Tag = 0
                            newFlowsheetNode.ForeColor = Color.Green

                        End If
                    Else
                        newFlowsheetNode.Tag = 0
                        newFlowsheetNode.ForeColor = Color.Green
                    End If
                    newFlowsheetNode.ImageIndex = 1
                    newFlowsheetNode.SelectedImageIndex = 1
                    'trFlowsheetHistory.Nodes.Item(0).Nodes.Add(newFlowsheetNode)
                    trFlowsheetHistory.Nodes.Add(newFlowsheetNode)
                    If trFlowsheetHistory.Nodes.Count > 0 Then
                        trFlowsheetHistory.SelectedNode = trFlowsheetHistory.Nodes.Item(0)
                    End If
                Next
            Else
            End If
        Catch ex As Exception
            'Developer: Mitesh Patel
            'Date:18-Feb-2012'
            'Bug ID=21239
            If ex.Message.Contains("Error in Like operator") = True Then
                MessageBox.Show("Invalid search criteria.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Finally
            If Not IsNothing(dtResult) Then    'obj Disposed by mitesh
                dtResult.Dispose()
                dtResult = Nothing
            End If
            If Not IsNothing(tdt) Then
                tdt.Dispose()
                tdt = Nothing
            End If
        End Try
        'trFlowsheetHistory.Nodes.Item(0).EnsureVisible() 'Commented by Shubhangi B'coz when their is no record according to entered text in search text box
    End Sub

    Private Sub C1FlexGrid1_AfterDeleteRow(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1FlexGrid1.AfterDeleteRow
        If _IsLoading = False Then
            _FlowSheetModified = True
            _isChanges = True
        End If
    End Sub


    Private Sub C1FlexGrid1_CellChanged(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1FlexGrid1.CellChanged
        'If _IsLoading = False Then _FlowSheetModified = True
        'SHUBHANGI 20101125 RESOLVED 6475
        'SHUBHANGI 20101123
        C1FlexGrid1.AutoSizeRow(e.Row)
        Dim oRange As C1.Win.C1FlexGrid.CellRange
        oRange = C1FlexGrid1.GetCellRange(e.Row, 1, e.Row, C1FlexGrid1.Cols.Count - 1)
        If oRange.Clip.ToString.Trim = "" Then
            C1FlexGrid1.Rows(e.Row).Height = 17
        End If

    End Sub
    'Added By Rahul Patel on 13-10-2010
    'For Deleting last record for flow sheet from grid i.e resolving issuse in case id GLO2010-0006166
    Private Sub C1FlexGrid1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles C1FlexGrid1.KeyDown
        If _IsLoading = False Then _FlowSheetModified = True
        If e.KeyData = Keys.Delete Then
            sFlwShtFilePath = "" ''when we delete some data from flex grid then make the flowsheet file name blank
        End If

    End Sub


    Private Sub C1FlexGrid1_KeyDownEdit(sender As Object, e As C1.Win.C1FlexGrid.KeyEditEventArgs) Handles C1FlexGrid1.KeyDownEdit
        sFlwShtFilePath = "" ''when we delete or make any changes to data from flex grid then make the flowsheet file name blank
    End Sub

    'Private Sub C1FlexGrid1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles C1FlexGrid1.KeyPress

    '    Dim i As Int16 = 0
    '    Dim noofChar As Int32 = 0
    '    Try


    '        Dim icnt As Int32 = CType(C1FlexGrid1.Cols(C1FlexGrid1.ColSel).Width, Integer)

    '        noofChar = (icnt / 8)
    '        If (C1FlexGrid1.Rows(1)(0).ToString().Length > noofChar) Then

    '            C1FlexGrid1.Rows(C1FlexGrid1.RowSel)(C1FlexGrid1.ColSel).Height = ht + 35
    '            'cnt = cnt + 1
    '            'If cnt > cntwidth Then


    '            '    cnt = 0
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub



    Private Sub C1FlexGrid1_KeyPressEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.KeyPressEditEventArgs) Handles C1FlexGrid1.KeyPressEdit
        'SHUBHANGI 20101125 RESOLVED 6475
        C1FlexGrid1.AutoResize = True
        C1FlexGrid1.AutoSizeRow(e.Row)

    End Sub

    Private Sub C1FlexGrid1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1FlexGrid1.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub btnClearSearchFlowsheetHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearchFlowsheetHistory.Click
        'SHUBHANGI 20091006
        'USE CLEAR BUTTON TO CLEAR SEARCH TEXT BOX
        txtSearchFlowsheetHistory.ResetText()
        txtSearchFlowsheetHistory.Select()
    End Sub
    ''solving sales force case-GLO2010-0006166
    Private Sub C1FlexGrid1_StartEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1FlexGrid1.StartEdit
        If _IsLoading = False Then _FlowSheetModified = True
    End Sub

    Private Function Get_PatientDetails()
        Dim dtPatient As DataTable = Nothing
        Try
            '   dtPatient = New DataTable
            dtPatient = GetPatientInfo(_PatientID)
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


        End Try
        Return Nothing
    End Function
    ' <summary>

    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    Private Sub frmPatientFlowSheet_SaveFunction() Handles Me.SaveFunction
        'For Fixing Bug ID 15566 i.e if no flowSheet is Present then Hide the grid.
        If trFlowsheetHistory.Nodes.Count > 0 Then
            Save_Flowsheet()
        Else
            _isClose = True
            'MessageBox.Show("There is no Flowsheet present.Please enter the flowsheet.", gstrMessageBoxCaption, MessageBoxButtons.OK)
        End If
        'End of Code for Fixing Bug ID 15566 i.e if no flowSheet is Present then Hide the grid.

    End Sub

    'New Function For saving Local flowsheet data 
    'For Resolving the FlowSheet Issuse.

    Private Sub SaveLocalFlowSheet(ByVal sFlowSheetName As String)
        Dim oRow As DataRow = Nothing
        Dim nRowID As Long = 0
        Dim colNumber As Integer = 1
        Dim oView As DataView = Nothing
        Dim dtRowTable As DataTable = Nothing
        Dim objFlowSheet_Local As clsFlowSheet = Nothing
        Dim dtStructure_Local As DataTable = Nothing
        Try
            objFlowSheet_Local = New clsFlowSheet

            If Not IsNothing(dtFlowSheet) Then
                dtFlowSheet.Clear()
            End If
            'If Not IsNothing(dtStructure) Then
            '    dtStructure.Clear()
            'End If

            objFlowSheet_Local.SelectFlowSheet(sFlowSheetName, _PatientID)

            dtStructure_Local = objFlowSheet_Local.GetDataview.Table

            If C1FlexGrid1.Rows.Count > 0 Then
                For rowIndex As Int32 = 1 To C1FlexGrid1.Rows.Count - 2

                    '' Taken rowID
                    nRowID = C1FlexGrid1.GetData(rowIndex, 1)

                    '' Colnumber reset to 1
                    colNumber = 1

                    For colIndex As Int32 = 2 To C1FlexGrid1.Cols.Count - 1

                        oRow = dtFlowSheet.NewRow()

                        oView = dtStructure_Local.DefaultView
                        'Bug #88781: 00000986: Quotation mark getting double in flowsheets
                        oView.RowFilter = "ColumnName = '" & C1FlexGrid1.Cols(colIndex).Name.ToString.Replace("'", "''") & "'"
                        dtRowTable = oView.ToTable()

                        oRow("sFlowSheetName") = sFlowSheetName
                        If (Not IsNothing(dtRowTable)) Then
                            oRow("sFieldName") = dtRowTable.Rows(0)(2)
                            oRow("sDataType") = C1FlexGrid1.Cols(colIndex).DataType.Name
                            oRow("dWidth") = dtRowTable.Rows(0)(4)
                            oRow("sFormat") = dtRowTable.Rows(0)(3)
                            oRow("sAlignment") = dtRowTable.Rows(0)(5)
                            oRow("nTotalCols") = dtRowTable.Rows(0)(0)
                            oRow("nColNumber") = colNumber
                            oRow("nForeColor") = dtRowTable.Rows(0)(6)
                            oRow("nBackColor") = dtRowTable.Rows(0)(7)
                            oRow("nRowId") = nRowID

                            If Not IsNothing(C1FlexGrid1.GetData(rowIndex, colIndex)) Then
                                '' For Resolving Bug ID 15505 
                                '' i.e  if pulled flowsheet in liquid link then for time format column it shows date also.
                                If (Convert.ToString(oRow("sFormat")).Contains("HH:MM") = True) Then
                                    '' for resolving bug Id 7924 in 6040
                                    ''  If we pulled flowsheet liquid link in Exam which has time format column, then it does not display AM.
                                    If (Convert.ToString(oRow("sFormat")).Contains("PM") = True) Then
                                        If (Convert.ToString(oRow("sFormat")).Contains("ss") = True) Then
                                            oRow("sValue") = CType(C1FlexGrid1(rowIndex, colIndex), Date).ToLongTimeString()
                                        Else
                                            oRow("sValue") = CType(C1FlexGrid1(rowIndex, colIndex), Date).ToShortTimeString()
                                        End If
                                    ElseIf (Convert.ToString(oRow("sFormat")).Contains("ss") = True) Then
                                        oRow("sValue") = CType(C1FlexGrid1(rowIndex, colIndex), Date).ToLongTimeString.Replace("PM", "").Replace("AM", "")
                                    Else
                                        oRow("sValue") = CType(C1FlexGrid1(rowIndex, colIndex), Date).ToShortTimeString.Replace("PM", "").Replace("AM", "")
                                    End If
                                Else
                                    oRow("sValue") = CType(C1FlexGrid1(rowIndex, colIndex), String)
                                End If
                            Else
                                oRow("sValue") = ""
                            End If
                        End If

                        'oRow("sValue") = ""
                        ''oRow("sDataType") = C1FlexGrid1.Cols(j).DataType.Name
                        ''oRow("dWidth") = Convert.ToDouble(dt.Rows(j - 2)("Width"))
                        ''oRow("sFormat") = dt.Rows(j - 2)("Format")
                        ''oRow("sAlignment") = dt.Rows(j - 2)("Alignment")
                        ''oRow("nTotalCols") = Convert.ToInt16(dt.Rows(j - 2)("TotalColumns"))
                        'oRow("nColNumber") = colNumber
                        ''oRow("nForeColor") = Convert.ToInt64(dt.Rows(j - 2)("ForeColor"))
                        ''oRow("nBackColor") = Convert.ToInt64(dt.Rows(j - 2)("BackColor"))
                        'oRow("nRowId") = nRowID

                        dtFlowSheet.Rows.Add(oRow)

                        colNumber += 1
                    Next
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(dtRowTable) Then
                dtRowTable.Dispose()
                dtRowTable = Nothing
            End If
            If Not IsNothing(dtStructure_Local) Then
                dtStructure_Local.Dispose()
                dtStructure_Local = Nothing
            End If
            If Not IsNothing(objFlowSheet_Local) Then
                objFlowSheet_Local.Dispose()
                objFlowSheet_Local = Nothing
            End If
        End Try

    End Sub

    Private Sub trFlowsheetHistory_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trFlowsheetHistory.DoubleClick
        Dim oResult As Windows.Forms.DialogResult
        Dim myFlowSheetNode As myTreeNode = Nothing
        Dim dtResult As DataTable = Nothing
        Try

            ''added for 8081 data grid printer replacement
            sFlwShtFilePath = "" ''''reset the flowsheet file name since new flowsheet is being loaded.

            If IsNothing(trFlowsheetHistory.SelectedNode) = False Then

                '' SUDHIR '' SAVE PREVIOUS FLOWSHEET IN LOCAL TABLE BEFORE OPENING OTHER FLOWSHEET ''
                'Check for wheather the opened flowsheet is Modified or Not
                If _FlowSheetModified And (Not _blnFormLock) Then
                    oResult = MessageBox.Show("Do you want to save the changes to Flowsheet?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                    If oResult = Windows.Forms.DialogResult.Yes Then
                        SavePatientFlowSheet()
                    Else
                        _FlowSheetModified = False
                        trFlowsheetHistory_DoubleClick(sender, e)
                        Return
                    End If
                End If

                If Not _blnFormLock Then
                    tls_btnFinish.Enabled = True ''User Can save FlowSheetDetail when FlowSheet has selected.
                    tls_btnPreview.Enabled = True
                    tls_btnPrint.Enabled = True
                End If



                _FlowSheetModified = False

                myFlowSheetNode = CType(trFlowsheetHistory.SelectedNode, myTreeNode)

                strFlowSheetName = myFlowSheetNode.Text
                intFlowSheetID = myFlowSheetNode.Key '' FlowSheet ID FlowSheet Master

                ''Bug : 00000828: Record locking. Form Level locking implemented
                ''''''''''<><><> Record Level Locking <><><><>
                'If gblnRecordLocking = True Then
                '    Dim mydt As New mytable
                '    mydt = Scan_n_Lock_Transaction(TrnType.Flowsheet, myFlowSheetNode.Tag, 0, Now)
                '    If mydt.Description <> gstrClientMachineName Then
                '        If MessageBox.Show("This Flowsheet is being modified by " & mydt.Code & " on " & mydt.Description & ". Do you want to open the History for view ?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                '            '' Open For view 
                '            _blnRecordLock = True
                '        Else
                '            '' 
                '            Exit Sub
                '        End If
                '    Else
                '        '' If Flow Sheet is not locked 
                '        If _blnRecordLock = True Then
                '            '' Currently Opened Flowsheet is locked by some other User on other Machine
                '            '' do nothing
                '        Else
                '            '' Currently Opened FlowSheet is locked by current User on same Machine
                '            '' Unlock Currently Opened FlowSheet  , Pass Currently Opened 
                '            Call UnLock_Transaction(TrnType.Flowsheet, _FlowSheetRecordID, 0, Now)
                '        End If
                '        _blnRecordLock = False
                '    End If

                '    Call Set_RecordLock(_blnRecordLock)
                '    '''' <><><> Record Level Locking <><><><> 
                'End If

                ''Created a structure for selected flowsheet
                'setGridStyle(strFlowSheetName)

                ''Added for Displaying current FlowSheet Name 
                Label9.Text = strFlowSheetName

                setGridStyle_New(strFlowSheetName)

                dtFlowSheetDeletedRowId = fillDeletedRowIdinTempTable()

                LoadFlowsheetData(strFlowSheetName)

                'dtFlowSheetStructure = objFlowSheet.GetFlowSheetStruture(strFlowSheetName, _PatientID)

                Return
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'For holding the deleted record rowId.
    'Newly added Function for resolving the Flowsheet Issuse.
    Public Function fillDeletedRowIdinTempTable() As DataTable
        Dim dtTemp As DataTable = Nothing
        Dim dt As DataTable = Nothing
        Try
            dtTemp = New DataTable
            dtTemp.Columns.Add("nRowId")
            dtTemp.Columns.Add("isDeleted")
            dt = dtTemp.Copy()
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(dtTemp) Then
                dtTemp.Dispose()
                dtTemp = Nothing
            End If
        End Try
    End Function

    Private Sub LoadFlowsheetData(ByVal myFlowsheetName As String)
        Dim dtResult As DataTable = Nothing
        'Dim reader As SqlClient.SqlDataReader = Nothing
        'Dim dt As DataTable = Nothing

        Dim iC1Col As Int32 = 1
        Dim iC1Row As Int32 = 0

        Dim objFlowSheet_local As clsFlowSheet = Nothing

        Try
            objFlowSheet_local = New clsFlowSheet

            ' dtResult = New DataTable()

            'If dtResult.Rows.Count = 0 Then

            dtResult = objFlowSheet_local.ScanFlowSheetReaderAsTable(myFlowsheetName.Trim, _PatientID)

            'dt = objFlowSheet.ScanFlowSheet(myFlowsheetName.Trim, _PatientID)
            ' dtResult = Pivot(reader, "nRowID", "sFieldName", "sValue")

            'dtFlowsheetData = dtResult
            'C1FlexGrid1.DataSource = dtResult

            'rowIDs.Clear()
            For Each _iDataRow As DataRow In dtResult.Rows

                iC1Row = C1FlexGrid1.Rows.Count - 1
                If Not _iDataRow(0).Equals(System.DBNull.Value) Then

                    C1FlexGrid1.Rows.Add()
                    iC1Col = 1

                    'If Not String.IsNullOrEmpty(_iDataRow(0)) Then
                    '    rowIDs.Add(_iDataRow(0), False)
                    'End If

                    For Each _iDataCol As DataColumn In dtResult.Columns
                        'If (_iDataCol.Ordinal.Equals(0)) Then
                        '    rowIDs.Add(System.Convert.ToString(_iDataRow(_iDataCol.Ordinal)))
                        'End If

                        'If (Not _iDataRow(_iDataCol.Ordinal).Equals(System.DBNull.Value)) And (Not String.IsNullOrEmpty(_iDataRow(_iDataCol.Ordinal))) Then
                        If Not _iDataRow(_iDataCol.Ordinal).Equals(System.DBNull.Value) Then
                            If Not String.IsNullOrEmpty(_iDataRow(_iDataCol.Ordinal)) Then
                                C1FlexGrid1.SetData(iC1Row, iC1Col, System.Convert.ToString(_iDataRow(_iDataCol.Ordinal)))
                            Else
                                C1FlexGrid1.SetData(iC1Row, iC1Col, Nothing)
                            End If

                        Else
                            C1FlexGrid1.SetData(iC1Row, iC1Col, Nothing)
                        End If

                        iC1Col += 1
                    Next
                End If
            Next
            '**C1FlexGrid1.DataSource = dtResult
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(objFlowSheet_local) Then
                objFlowSheet_local.Dispose()
                objFlowSheet_local = Nothing
            End If
            If Not IsNothing(dtResult) Then
                dtResult.Dispose()
                dtResult = Nothing
            End If
        End Try
    End Sub

    Public Shared Function Pivot(ByVal dataValues As IDataReader, ByVal keyColumn As String, ByVal pivotNameColumn As String, ByVal pivotValueColumn As String) As DataTable
        Dim tmp As DataTable = Nothing
        Dim r As DataRow = Nothing
        Dim LastKey As String = "//dummy//"
        Dim i As Integer, pValIndex As Integer, pNameIndex As Integer
        Dim s As String = String.Empty
        Dim FirstRow As Boolean = True
        Dim dtPivotFlowSheetTable As DataTable = Nothing
        Try
            tmp = New DataTable()

            ' Add non-pivot columns to the data table:

            pValIndex = dataValues.GetOrdinal(pivotValueColumn)
            pNameIndex = dataValues.GetOrdinal(pivotNameColumn)

            For i = 0 To dataValues.FieldCount - 1
                If i <> pValIndex AndAlso i <> pNameIndex Then
                    tmp.Columns.Add(dataValues.GetName(i), dataValues.GetFieldType(i))
                End If
            Next

            r = tmp.NewRow()

            ' now, fill up the table with the data:
            While dataValues.Read()
                ' see if we need to start a new row
                If dataValues(keyColumn).ToString() <> LastKey Then
                    ' if this isn't the very first row, we need to add the last one to the table
                    If Not FirstRow Then
                        tmp.Rows.Add(r)
                    End If
                    r = tmp.NewRow()
                    FirstRow = False
                    ' Add all non-pivot column values to the new row:
                    For i = 0 To dataValues.FieldCount - 3
                        r(i) = dataValues(tmp.Columns(i).ColumnName)
                    Next
                    LastKey = dataValues(keyColumn).ToString()
                End If
                ' assign the pivot values to the proper column; add new columns if needed:
                s = dataValues(pNameIndex).ToString()
                If Not tmp.Columns.Contains(s) Then
                    tmp.Columns.Add(s, dataValues.GetFieldType(pValIndex))
                End If
                r(s) = dataValues(pValIndex)
            End While

            ' add that final row to the datatable:
            tmp.Rows.Add(r)

            ' Close the DataReader
            dataValues.Close()
            ' and that's it!

            dtPivotFlowSheetTable = tmp.Copy()

            Return dtPivotFlowSheetTable
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(r) Then
                r = Nothing
            End If
            If Not IsNothing(tmp) Then
                tmp.Dispose()
                tmp = Nothing
            End If
        End Try
    End Function

    'Used to generate Unique RowId for FlowSheet record.
    Private Sub C1FlexGrid1_BeforeAddRow(ByVal sender As System.Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1FlexGrid1.BeforeAddRow
        Dim objclsFlowSheet As clsFlowSheet = Nothing
        Try
            objclsFlowSheet = New clsFlowSheet
            C1FlexGrid1.SetData(e.Row, 1, objclsFlowSheet.GenerateRowID())
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(objclsFlowSheet) Then
                objclsFlowSheet.Dispose()
                objclsFlowSheet = Nothing
            End If
        End Try
    End Sub

    Private Sub C1FlexGrid1_BeforeDeleteRow(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1FlexGrid1.BeforeDeleteRow
        'Added for Delete operation on record. 
        Dim deletedRowID As Long = 0
        Dim dr As DataRow = Nothing
        Try
            deletedRowID = C1FlexGrid1.GetData(e.Row, 1)
            'rowIDs.Item(deletedRowID) = True
            If Not IsNothing(dtFlowSheetDeletedRowId) Then
                dr = dtFlowSheetDeletedRowId.NewRow
                dr("nRowId") = deletedRowID
                dr("isDeleted") = 1
                dtFlowSheetDeletedRowId.Rows.Add(dr)
            End If
            If _IsLoading = False Then _FlowSheetModified = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(dr) Then
                dr = Nothing
            End If
        End Try
    End Sub

    Private Sub C1FlexGrid1_AfterEdit(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RowColEventArgs) Handles C1FlexGrid1.AfterEdit
        If _IsLoading = False Then _FlowSheetModified = True
    End Sub

    'New Function For saving Local flowsheet data 
    'For Resolving the FlowSheet Issuse.

   
   
End Class
