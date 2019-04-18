Imports Microsoft.Win32
Imports gloPMAdmin.gloReports
Imports System.Xml
Imports System.IO
Imports System.Data.SqlClient
Public Class frmRxReportDesigner
    Inherits System.Windows.Forms.Form
    Dim x As Int16
    Dim y As Int16
    Dim pnlPresciptionReport As Panel
    Dim objDataDictionary As IDataDictionary
    Dim objectlbl As New Label
    Private blnMoving As Boolean = False
    Private MouseDownX As Integer
    Private MouseDownY As Integer
    'Dim WithEvents PrintDocument As New Printing.PrintDocument
    'Dim WithEvents PrintDialog As New PrintDialog
    'Dim WithEvents printPreviewDilaog As New PrintPreviewDialog
    Dim selectedcontrol As Control
    Private GridPrinter As RxReportPrinter
    Private intTextBoxTag As String
    Dim PD1 As New Printing.PrintDocument
    Dim objReport As Report
    Dim DetailsField As ArrayList
    Dim obj As ClsRxReportDictionary
    ' Dim objcontrol As TextBox
    Dim strLocation As String
    Dim ReportHeaderCol As New Collection
    Dim PageHeaderCol As New Collection
    Dim PageFooterCol As New Collection
    Dim ReportFooterCol As New Collection
    Dim SectionsCol As Collection
    Dim oDetails As DataTable
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tlsbtnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnPreview As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Pr As ExtendedPrintPreviewDialog

    Dim strFileName As String = Application.StartupPath & "\PrescriptionReport.xml"

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


    Friend WithEvents CntExplorer As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuDeleteCriteria As System.Windows.Forms.MenuItem
    Friend WithEvents mnuInsert As System.Windows.Forms.MenuItem
    Friend WithEvents pnlCenter As System.Windows.Forms.Panel
    Friend WithEvents pnlCenterDesigner As System.Windows.Forms.Panel
    Friend WithEvents pnlReportFooter As System.Windows.Forms.Panel
    Friend WithEvents btnReportFooter As System.Windows.Forms.Button
    Friend WithEvents pnlPageFooter As System.Windows.Forms.Panel
    Friend WithEvents btnPageFooter As System.Windows.Forms.Button
    Friend WithEvents splDetails As System.Windows.Forms.Splitter
    Friend WithEvents pnlDetails As System.Windows.Forms.Panel
    Friend WithEvents btnDetails As System.Windows.Forms.Button
    Friend WithEvents splPageHeader As System.Windows.Forms.Splitter
    Friend WithEvents pnlPageHeader As System.Windows.Forms.Panel
    Friend WithEvents btnPageHeader As System.Windows.Forms.Button
    Friend WithEvents pnlReportHeader As System.Windows.Forms.Panel
    Friend WithEvents btnReportHeader As System.Windows.Forms.Button
    Friend WithEvents splReportHeader As System.Windows.Forms.Splitter
    Friend WithEvents pnlCenterTop As System.Windows.Forms.Panel
    Friend WithEvents tblDesigner As System.Windows.Forms.ToolBar
    Friend WithEvents tblPrint As System.Windows.Forms.ToolBarButton
    Friend WithEvents tblsep2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tblPrintPreview As System.Windows.Forms.ToolBarButton
    Friend WithEvents tblsep3 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tblRefresh As System.Windows.Forms.ToolBarButton
    Friend WithEvents tblsep4 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tblOK As System.Windows.Forms.ToolBarButton
    Friend WithEvents tblSep5 As System.Windows.Forms.ToolBarButton
    Friend WithEvents tblclose As System.Windows.Forms.ToolBarButton
    Friend WithEvents tblSep7 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ImgToolbar As System.Windows.Forms.ImageList
    Friend WithEvents CntFieldMenu As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuProperties As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDelete As System.Windows.Forms.MenuItem
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents ImgTreeview As System.Windows.Forms.ImageList
    Friend WithEvents splRight As System.Windows.Forms.Splitter
    Friend WithEvents splLeft As System.Windows.Forms.Splitter
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents ImgExplorer As System.Windows.Forms.ImageList
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents pnlSections As System.Windows.Forms.Panel
    Friend WithEvents trSections As System.Windows.Forms.TreeView
    Friend WithEvents pnlTitleSections As System.Windows.Forms.Panel
    Friend WithEvents lblSections As System.Windows.Forms.Label
    Friend WithEvents spltDataDictionary As System.Windows.Forms.Splitter
    Friend WithEvents pnlDataDictionary As System.Windows.Forms.Panel
    Friend WithEvents trDataDictionary As System.Windows.Forms.TreeView
    Friend WithEvents pnlDataTitle As System.Windows.Forms.Panel
    Friend WithEvents lblDataDictionary As System.Windows.Forms.Label
    Friend WithEvents ImgSections As System.Windows.Forms.ImageList
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents pnlReportExplorer As System.Windows.Forms.Panel
    Friend WithEvents prpProperties As System.Windows.Forms.PropertyGrid
    Friend WithEvents pnlTitleProperty As System.Windows.Forms.Panel
    Friend WithEvents lblProperty As System.Windows.Forms.Label
    Friend WithEvents spltReportExplorer As System.Windows.Forms.Splitter
    Friend WithEvents pnlProperty As System.Windows.Forms.Panel
    Friend WithEvents trReportExplorer As System.Windows.Forms.TreeView
    Friend WithEvents pnlTitleReportExplorer As System.Windows.Forms.Panel
    Friend WithEvents lblReportExplorer As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRxReportDesigner))
        Me.CntExplorer = New System.Windows.Forms.ContextMenu
        Me.mnuDeleteCriteria = New System.Windows.Forms.MenuItem
        Me.mnuInsert = New System.Windows.Forms.MenuItem
        Me.pnlCenter = New System.Windows.Forms.Panel
        Me.pnlCenterDesigner = New System.Windows.Forms.Panel
        Me.pnlReportFooter = New System.Windows.Forms.Panel
        Me.btnReportFooter = New System.Windows.Forms.Button
        Me.pnlPageFooter = New System.Windows.Forms.Panel
        Me.btnPageFooter = New System.Windows.Forms.Button
        Me.splDetails = New System.Windows.Forms.Splitter
        Me.pnlDetails = New System.Windows.Forms.Panel
        Me.btnDetails = New System.Windows.Forms.Button
        Me.splPageHeader = New System.Windows.Forms.Splitter
        Me.pnlPageHeader = New System.Windows.Forms.Panel
        Me.tblDesigner = New System.Windows.Forms.ToolBar
        Me.tblPrint = New System.Windows.Forms.ToolBarButton
        Me.tblsep2 = New System.Windows.Forms.ToolBarButton
        Me.tblPrintPreview = New System.Windows.Forms.ToolBarButton
        Me.tblsep3 = New System.Windows.Forms.ToolBarButton
        Me.tblRefresh = New System.Windows.Forms.ToolBarButton
        Me.tblsep4 = New System.Windows.Forms.ToolBarButton
        Me.tblOK = New System.Windows.Forms.ToolBarButton
        Me.tblSep5 = New System.Windows.Forms.ToolBarButton
        Me.tblclose = New System.Windows.Forms.ToolBarButton
        Me.tblSep7 = New System.Windows.Forms.ToolBarButton
        Me.ImgToolbar = New System.Windows.Forms.ImageList(Me.components)
        Me.btnPageHeader = New System.Windows.Forms.Button
        Me.pnlReportHeader = New System.Windows.Forms.Panel
        Me.btnReportHeader = New System.Windows.Forms.Button
        Me.splReportHeader = New System.Windows.Forms.Splitter
        Me.pnlCenterTop = New System.Windows.Forms.Panel
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tlsbtnPrint = New System.Windows.Forms.ToolStripButton
        Me.tlsbtnPreview = New System.Windows.Forms.ToolStripButton
        Me.tlsbtnSave = New System.Windows.Forms.ToolStripButton
        Me.tlsbtnClose = New System.Windows.Forms.ToolStripButton
        Me.CntFieldMenu = New System.Windows.Forms.ContextMenu
        Me.mnuProperties = New System.Windows.Forms.MenuItem
        Me.mnuDelete = New System.Windows.Forms.MenuItem
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog
        Me.ImgTreeview = New System.Windows.Forms.ImageList(Me.components)
        Me.splRight = New System.Windows.Forms.Splitter
        Me.splLeft = New System.Windows.Forms.Splitter
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument
        Me.ImgExplorer = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlLeft = New System.Windows.Forms.Panel
        Me.pnlSections = New System.Windows.Forms.Panel
        Me.trSections = New System.Windows.Forms.TreeView
        Me.ImgSections = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlTitleSections = New System.Windows.Forms.Panel
        Me.lblSections = New System.Windows.Forms.Label
        Me.spltDataDictionary = New System.Windows.Forms.Splitter
        Me.pnlDataDictionary = New System.Windows.Forms.Panel
        Me.trDataDictionary = New System.Windows.Forms.TreeView
        Me.pnlDataTitle = New System.Windows.Forms.Panel
        Me.lblDataDictionary = New System.Windows.Forms.Label
        Me.pnlRight = New System.Windows.Forms.Panel
        Me.pnlReportExplorer = New System.Windows.Forms.Panel
        Me.prpProperties = New System.Windows.Forms.PropertyGrid
        Me.pnlTitleProperty = New System.Windows.Forms.Panel
        Me.lblProperty = New System.Windows.Forms.Label
        Me.spltReportExplorer = New System.Windows.Forms.Splitter
        Me.pnlProperty = New System.Windows.Forms.Panel
        Me.trReportExplorer = New System.Windows.Forms.TreeView
        Me.pnlTitleReportExplorer = New System.Windows.Forms.Panel
        Me.lblReportExplorer = New System.Windows.Forms.Label
        Me.pnlCenter.SuspendLayout()
        Me.pnlCenterDesigner.SuspendLayout()
        Me.pnlReportFooter.SuspendLayout()
        Me.pnlPageFooter.SuspendLayout()
        Me.pnlDetails.SuspendLayout()
        Me.pnlPageHeader.SuspendLayout()
        Me.pnlReportHeader.SuspendLayout()
        Me.pnlCenterTop.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.pnlSections.SuspendLayout()
        Me.pnlTitleSections.SuspendLayout()
        Me.pnlDataDictionary.SuspendLayout()
        Me.pnlDataTitle.SuspendLayout()
        Me.pnlRight.SuspendLayout()
        Me.pnlReportExplorer.SuspendLayout()
        Me.pnlTitleProperty.SuspendLayout()
        Me.pnlProperty.SuspendLayout()
        Me.pnlTitleReportExplorer.SuspendLayout()
        Me.SuspendLayout()
        '
        'CntExplorer
        '
        Me.CntExplorer.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDeleteCriteria})
        '
        'mnuDeleteCriteria
        '
        Me.mnuDeleteCriteria.Index = 0
        Me.mnuDeleteCriteria.Text = "Delete Criteria"
        '
        'mnuInsert
        '
        Me.mnuInsert.Index = 2
        Me.mnuInsert.Text = "Insert TextBox"
        '
        'pnlCenter
        '
        Me.pnlCenter.AutoScroll = True
        Me.pnlCenter.AutoScrollMargin = New System.Drawing.Size(10, 10)
        Me.pnlCenter.BackColor = System.Drawing.Color.White
        Me.pnlCenter.BackgroundImage = CType(resources.GetObject("pnlCenter.BackgroundImage"), System.Drawing.Image)
        Me.pnlCenter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlCenter.Controls.Add(Me.pnlCenterDesigner)
        Me.pnlCenter.Controls.Add(Me.pnlCenterTop)
        Me.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCenter.Location = New System.Drawing.Point(181, 0)
        Me.pnlCenter.Name = "pnlCenter"
        Me.pnlCenter.Size = New System.Drawing.Size(663, 710)
        Me.pnlCenter.TabIndex = 9
        '
        'pnlCenterDesigner
        '
        Me.pnlCenterDesigner.BackColor = System.Drawing.Color.White
        Me.pnlCenterDesigner.Controls.Add(Me.pnlReportFooter)
        Me.pnlCenterDesigner.Controls.Add(Me.pnlPageFooter)
        Me.pnlCenterDesigner.Controls.Add(Me.splDetails)
        Me.pnlCenterDesigner.Controls.Add(Me.pnlDetails)
        Me.pnlCenterDesigner.Controls.Add(Me.splPageHeader)
        Me.pnlCenterDesigner.Controls.Add(Me.pnlPageHeader)
        Me.pnlCenterDesigner.Controls.Add(Me.pnlReportHeader)
        Me.pnlCenterDesigner.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCenterDesigner.Location = New System.Drawing.Point(0, 58)
        Me.pnlCenterDesigner.Name = "pnlCenterDesigner"
        Me.pnlCenterDesigner.Size = New System.Drawing.Size(663, 652)
        Me.pnlCenterDesigner.TabIndex = 1
        '
        'pnlReportFooter
        '
        Me.pnlReportFooter.Controls.Add(Me.btnReportFooter)
        Me.pnlReportFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlReportFooter.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlReportFooter.Location = New System.Drawing.Point(0, 636)
        Me.pnlReportFooter.Name = "pnlReportFooter"
        Me.pnlReportFooter.Size = New System.Drawing.Size(663, 16)
        Me.pnlReportFooter.TabIndex = 8
        '
        'btnReportFooter
        '
        Me.btnReportFooter.BackColor = System.Drawing.Color.Transparent
        Me.btnReportFooter.BackgroundImage = CType(resources.GetObject("btnReportFooter.BackgroundImage"), System.Drawing.Image)
        Me.btnReportFooter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnReportFooter.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnReportFooter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReportFooter.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReportFooter.Location = New System.Drawing.Point(0, 0)
        Me.btnReportFooter.Name = "btnReportFooter"
        Me.btnReportFooter.Size = New System.Drawing.Size(663, 25)
        Me.btnReportFooter.TabIndex = 2
        Me.btnReportFooter.Text = "Report Footer"
        Me.btnReportFooter.UseVisualStyleBackColor = False
        '
        'pnlPageFooter
        '
        Me.pnlPageFooter.BackColor = System.Drawing.Color.GhostWhite
        Me.pnlPageFooter.Controls.Add(Me.btnPageFooter)
        Me.pnlPageFooter.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPageFooter.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPageFooter.Location = New System.Drawing.Point(0, 446)
        Me.pnlPageFooter.Name = "pnlPageFooter"
        Me.pnlPageFooter.Size = New System.Drawing.Size(663, 196)
        Me.pnlPageFooter.TabIndex = 6
        '
        'btnPageFooter
        '
        Me.btnPageFooter.BackColor = System.Drawing.Color.Gainsboro
        Me.btnPageFooter.BackgroundImage = CType(resources.GetObject("btnPageFooter.BackgroundImage"), System.Drawing.Image)
        Me.btnPageFooter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPageFooter.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnPageFooter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPageFooter.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPageFooter.Location = New System.Drawing.Point(0, 0)
        Me.btnPageFooter.Name = "btnPageFooter"
        Me.btnPageFooter.Size = New System.Drawing.Size(663, 25)
        Me.btnPageFooter.TabIndex = 5
        Me.btnPageFooter.Text = "Page Footer"
        Me.btnPageFooter.UseVisualStyleBackColor = False
        '
        'splDetails
        '
        Me.splDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.splDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.splDetails.Dock = System.Windows.Forms.DockStyle.Top
        Me.splDetails.Enabled = False
        Me.splDetails.Location = New System.Drawing.Point(0, 445)
        Me.splDetails.Name = "splDetails"
        Me.splDetails.Size = New System.Drawing.Size(663, 1)
        Me.splDetails.TabIndex = 4
        Me.splDetails.TabStop = False
        '
        'pnlDetails
        '
        Me.pnlDetails.BackColor = System.Drawing.Color.GhostWhite
        Me.pnlDetails.Controls.Add(Me.btnDetails)
        Me.pnlDetails.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDetails.Location = New System.Drawing.Point(0, 267)
        Me.pnlDetails.Name = "pnlDetails"
        Me.pnlDetails.Size = New System.Drawing.Size(663, 178)
        Me.pnlDetails.TabIndex = 4
        '
        'btnDetails
        '
        Me.btnDetails.BackColor = System.Drawing.Color.Transparent
        Me.btnDetails.BackgroundImage = CType(resources.GetObject("btnDetails.BackgroundImage"), System.Drawing.Image)
        Me.btnDetails.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDetails.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDetails.ForeColor = System.Drawing.Color.Black
        Me.btnDetails.Location = New System.Drawing.Point(0, 0)
        Me.btnDetails.Name = "btnDetails"
        Me.btnDetails.Size = New System.Drawing.Size(663, 25)
        Me.btnDetails.TabIndex = 2
        Me.btnDetails.Text = "Details"
        Me.btnDetails.UseVisualStyleBackColor = False
        '
        'splPageHeader
        '
        Me.splPageHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(112, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.splPageHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.splPageHeader.Cursor = System.Windows.Forms.Cursors.HSplit
        Me.splPageHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.splPageHeader.Location = New System.Drawing.Point(0, 266)
        Me.splPageHeader.Name = "splPageHeader"
        Me.splPageHeader.Size = New System.Drawing.Size(663, 1)
        Me.splPageHeader.TabIndex = 3
        Me.splPageHeader.TabStop = False
        '
        'pnlPageHeader
        '
        Me.pnlPageHeader.BackColor = System.Drawing.Color.GhostWhite
        Me.pnlPageHeader.Controls.Add(Me.tblDesigner)
        Me.pnlPageHeader.Controls.Add(Me.btnPageHeader)
        Me.pnlPageHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPageHeader.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPageHeader.Location = New System.Drawing.Point(0, 16)
        Me.pnlPageHeader.Name = "pnlPageHeader"
        Me.pnlPageHeader.Size = New System.Drawing.Size(663, 250)
        Me.pnlPageHeader.TabIndex = 2
        '
        'tblDesigner
        '
        Me.tblDesigner.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.tblDesigner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tblDesigner.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.tblPrint, Me.tblsep2, Me.tblPrintPreview, Me.tblsep3, Me.tblRefresh, Me.tblsep4, Me.tblOK, Me.tblSep5, Me.tblclose, Me.tblSep7})
        Me.tblDesigner.ButtonSize = New System.Drawing.Size(60, 40)
        Me.tblDesigner.Dock = System.Windows.Forms.DockStyle.None
        Me.tblDesigner.DropDownArrows = True
        Me.tblDesigner.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblDesigner.ImageList = Me.ImgToolbar
        Me.tblDesigner.Location = New System.Drawing.Point(2, 97)
        Me.tblDesigner.Name = "tblDesigner"
        Me.tblDesigner.ShowToolTips = True
        Me.tblDesigner.Size = New System.Drawing.Size(658, 45)
        Me.tblDesigner.TabIndex = 20
        Me.tblDesigner.Visible = False
        '
        'tblPrint
        '
        Me.tblPrint.ImageIndex = 0
        Me.tblPrint.Name = "tblPrint"
        Me.tblPrint.Text = "&Print"
        Me.tblPrint.ToolTipText = "Print Report"
        '
        'tblsep2
        '
        Me.tblsep2.Name = "tblsep2"
        Me.tblsep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tblPrintPreview
        '
        Me.tblPrintPreview.ImageIndex = 1
        Me.tblPrintPreview.Name = "tblPrintPreview"
        Me.tblPrintPreview.Text = "&Preview"
        Me.tblPrintPreview.ToolTipText = "Preview"
        '
        'tblsep3
        '
        Me.tblsep3.Name = "tblsep3"
        Me.tblsep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tblRefresh
        '
        Me.tblRefresh.ImageIndex = 2
        Me.tblRefresh.Name = "tblRefresh"
        Me.tblRefresh.Text = "&New"
        Me.tblRefresh.ToolTipText = "New Report"
        Me.tblRefresh.Visible = False
        '
        'tblsep4
        '
        Me.tblsep4.Name = "tblsep4"
        Me.tblsep4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        Me.tblsep4.Visible = False
        '
        'tblOK
        '
        Me.tblOK.ImageIndex = 3
        Me.tblOK.Name = "tblOK"
        Me.tblOK.Text = "&Save"
        Me.tblOK.ToolTipText = "Save Report"
        '
        'tblSep5
        '
        Me.tblSep5.Name = "tblSep5"
        Me.tblSep5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'tblclose
        '
        Me.tblclose.ImageIndex = 4
        Me.tblclose.Name = "tblclose"
        Me.tblclose.Text = "&Close"
        Me.tblclose.ToolTipText = "Close "
        '
        'tblSep7
        '
        Me.tblSep7.Name = "tblSep7"
        Me.tblSep7.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'ImgToolbar
        '
        Me.ImgToolbar.ImageStream = CType(resources.GetObject("ImgToolbar.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgToolbar.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgToolbar.Images.SetKeyName(0, "")
        Me.ImgToolbar.Images.SetKeyName(1, "")
        Me.ImgToolbar.Images.SetKeyName(2, "")
        Me.ImgToolbar.Images.SetKeyName(3, "")
        Me.ImgToolbar.Images.SetKeyName(4, "")
        Me.ImgToolbar.Images.SetKeyName(5, "")
        '
        'btnPageHeader
        '
        Me.btnPageHeader.BackColor = System.Drawing.Color.Gainsboro
        Me.btnPageHeader.BackgroundImage = CType(resources.GetObject("btnPageHeader.BackgroundImage"), System.Drawing.Image)
        Me.btnPageHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPageHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnPageHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPageHeader.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPageHeader.Location = New System.Drawing.Point(0, 0)
        Me.btnPageHeader.Name = "btnPageHeader"
        Me.btnPageHeader.Size = New System.Drawing.Size(663, 25)
        Me.btnPageHeader.TabIndex = 2
        Me.btnPageHeader.Text = "Page Header"
        Me.btnPageHeader.UseVisualStyleBackColor = False
        '
        'pnlReportHeader
        '
        Me.pnlReportHeader.Controls.Add(Me.btnReportHeader)
        Me.pnlReportHeader.Controls.Add(Me.splReportHeader)
        Me.pnlReportHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlReportHeader.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlReportHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlReportHeader.Name = "pnlReportHeader"
        Me.pnlReportHeader.Size = New System.Drawing.Size(663, 16)
        Me.pnlReportHeader.TabIndex = 0
        '
        'btnReportHeader
        '
        Me.btnReportHeader.BackColor = System.Drawing.Color.Transparent
        Me.btnReportHeader.BackgroundImage = CType(resources.GetObject("btnReportHeader.BackgroundImage"), System.Drawing.Image)
        Me.btnReportHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnReportHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReportHeader.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReportHeader.Location = New System.Drawing.Point(0, 0)
        Me.btnReportHeader.Name = "btnReportHeader"
        Me.btnReportHeader.Size = New System.Drawing.Size(663, 25)
        Me.btnReportHeader.TabIndex = 1
        Me.btnReportHeader.Text = "Report Header"
        Me.btnReportHeader.UseVisualStyleBackColor = False
        '
        'splReportHeader
        '
        Me.splReportHeader.BackColor = System.Drawing.Color.WhiteSmoke
        Me.splReportHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.splReportHeader.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.splReportHeader.Location = New System.Drawing.Point(0, 12)
        Me.splReportHeader.Name = "splReportHeader"
        Me.splReportHeader.Size = New System.Drawing.Size(663, 4)
        Me.splReportHeader.TabIndex = 1
        Me.splReportHeader.TabStop = False
        '
        'pnlCenterTop
        '
        Me.pnlCenterTop.BackColor = System.Drawing.Color.White
        Me.pnlCenterTop.BackgroundImage = CType(resources.GetObject("pnlCenterTop.BackgroundImage"), System.Drawing.Image)
        Me.pnlCenterTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlCenterTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCenterTop.Controls.Add(Me.ToolStrip1)
        Me.pnlCenterTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCenterTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlCenterTop.Name = "pnlCenterTop"
        Me.pnlCenterTop.Size = New System.Drawing.Size(663, 58)
        Me.pnlCenterTop.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.White
        Me.ToolStrip1.BackgroundImage = CType(resources.GetObject("ToolStrip1.BackgroundImage"), System.Drawing.Image)
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStrip1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtnPrint, Me.tlsbtnPreview, Me.tlsbtnSave, Me.tlsbtnClose})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(661, 56)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tlsbtnPrint
        '
        Me.tlsbtnPrint.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnPrint.Image = CType(resources.GetObject("tlsbtnPrint.Image"), System.Drawing.Image)
        Me.tlsbtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnPrint.Name = "tlsbtnPrint"
        Me.tlsbtnPrint.Size = New System.Drawing.Size(43, 53)
        Me.tlsbtnPrint.Text = "&Print"
        Me.tlsbtnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsbtnPreview
        '
        Me.tlsbtnPreview.Image = CType(resources.GetObject("tlsbtnPreview.Image"), System.Drawing.Image)
        Me.tlsbtnPreview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnPreview.Name = "tlsbtnPreview"
        Me.tlsbtnPreview.Size = New System.Drawing.Size(63, 53)
        Me.tlsbtnPreview.Text = "&Preview"
        Me.tlsbtnPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsbtnSave
        '
        Me.tlsbtnSave.Image = CType(resources.GetObject("tlsbtnSave.Image"), System.Drawing.Image)
        Me.tlsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnSave.Name = "tlsbtnSave"
        Me.tlsbtnSave.Size = New System.Drawing.Size(43, 53)
        Me.tlsbtnSave.Text = "&Save"
        Me.tlsbtnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnSave.ToolTipText = "Save"
        '
        'tlsbtnClose
        '
        Me.tlsbtnClose.Image = CType(resources.GetObject("tlsbtnClose.Image"), System.Drawing.Image)
        Me.tlsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnClose.Name = "tlsbtnClose"
        Me.tlsbtnClose.Size = New System.Drawing.Size(46, 53)
        Me.tlsbtnClose.Text = "&Close"
        Me.tlsbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnClose.ToolTipText = "Close"
        '
        'CntFieldMenu
        '
        Me.CntFieldMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuProperties, Me.mnuDelete, Me.mnuInsert})
        '
        'mnuProperties
        '
        Me.mnuProperties.Index = 0
        Me.mnuProperties.Text = "Properties"
        '
        'mnuDelete
        '
        Me.mnuDelete.Index = 1
        Me.mnuDelete.Text = "Delete"
        '
        'ImgTreeview
        '
        Me.ImgTreeview.ImageStream = CType(resources.GetObject("ImgTreeview.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgTreeview.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgTreeview.Images.SetKeyName(0, "Table_04.ico")
        Me.ImgTreeview.Images.SetKeyName(1, "Arrow_02.ico")
        '
        'splRight
        '
        Me.splRight.BackColor = System.Drawing.Color.WhiteSmoke
        Me.splRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.splRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.splRight.Enabled = False
        Me.splRight.Location = New System.Drawing.Point(844, 0)
        Me.splRight.Name = "splRight"
        Me.splRight.Size = New System.Drawing.Size(4, 710)
        Me.splRight.TabIndex = 8
        Me.splRight.TabStop = False
        '
        'splLeft
        '
        Me.splLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(112, Byte), Integer), CType(CType(168, Byte), Integer), CType(CType(205, Byte), Integer))
        Me.splLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.splLeft.Enabled = False
        Me.splLeft.Location = New System.Drawing.Point(180, 0)
        Me.splLeft.Name = "splLeft"
        Me.splLeft.Size = New System.Drawing.Size(1, 710)
        Me.splLeft.TabIndex = 7
        Me.splLeft.TabStop = False
        '
        'ImgExplorer
        '
        Me.ImgExplorer.ImageStream = CType(resources.GetObject("ImgExplorer.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgExplorer.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgExplorer.Images.SetKeyName(0, "Current Date.ico")
        Me.ImgExplorer.Images.SetKeyName(1, "Current Page.ico")
        Me.ImgExplorer.Images.SetKeyName(2, "")
        Me.ImgExplorer.Images.SetKeyName(3, "")
        Me.ImgExplorer.Images.SetKeyName(4, "Special Fields.ico")
        Me.ImgExplorer.Images.SetKeyName(5, "")
        Me.ImgExplorer.Images.SetKeyName(6, "")
        Me.ImgExplorer.Images.SetKeyName(7, "")
        Me.ImgExplorer.Images.SetKeyName(8, "Current Time_01.ico")
        Me.ImgExplorer.Images.SetKeyName(9, "")
        '
        'pnlLeft
        '
        Me.pnlLeft.Controls.Add(Me.pnlSections)
        Me.pnlLeft.Controls.Add(Me.spltDataDictionary)
        Me.pnlLeft.Controls.Add(Me.pnlDataDictionary)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(180, 710)
        Me.pnlLeft.TabIndex = 5
        '
        'pnlSections
        '
        Me.pnlSections.Controls.Add(Me.trSections)
        Me.pnlSections.Controls.Add(Me.pnlTitleSections)
        Me.pnlSections.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSections.Location = New System.Drawing.Point(0, 404)
        Me.pnlSections.Name = "pnlSections"
        Me.pnlSections.Size = New System.Drawing.Size(180, 306)
        Me.pnlSections.TabIndex = 2
        '
        'trSections
        '
        Me.trSections.BackColor = System.Drawing.Color.GhostWhite
        Me.trSections.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trSections.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trSections.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trSections.ForeColor = System.Drawing.Color.Black
        Me.trSections.ImageIndex = 0
        Me.trSections.ImageList = Me.ImgSections
        Me.trSections.Indent = 21
        Me.trSections.ItemHeight = 18
        Me.trSections.Location = New System.Drawing.Point(0, 24)
        Me.trSections.Name = "trSections"
        Me.trSections.SelectedImageIndex = 0
        Me.trSections.ShowLines = False
        Me.trSections.ShowRootLines = False
        Me.trSections.Size = New System.Drawing.Size(180, 282)
        Me.trSections.TabIndex = 1
        '
        'ImgSections
        '
        Me.ImgSections.ImageStream = CType(resources.GetObject("ImgSections.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgSections.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgSections.Images.SetKeyName(0, "Section.ico")
        Me.ImgSections.Images.SetKeyName(1, "Report Header_01.ico")
        Me.ImgSections.Images.SetKeyName(2, "Page Header_01.ico")
        Me.ImgSections.Images.SetKeyName(3, "Details.ico")
        Me.ImgSections.Images.SetKeyName(4, "Page Footer_01.ico")
        Me.ImgSections.Images.SetKeyName(5, "Report Footer.ico")
        Me.ImgSections.Images.SetKeyName(6, "Arrow_02.ico")
        '
        'pnlTitleSections
        '
        Me.pnlTitleSections.BackgroundImage = CType(resources.GetObject("pnlTitleSections.BackgroundImage"), System.Drawing.Image)
        Me.pnlTitleSections.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTitleSections.Controls.Add(Me.lblSections)
        Me.pnlTitleSections.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTitleSections.Location = New System.Drawing.Point(0, 0)
        Me.pnlTitleSections.Name = "pnlTitleSections"
        Me.pnlTitleSections.Size = New System.Drawing.Size(180, 24)
        Me.pnlTitleSections.TabIndex = 0
        '
        'lblSections
        '
        Me.lblSections.BackColor = System.Drawing.Color.Transparent
        Me.lblSections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSections.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblSections.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSections.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSections.ForeColor = System.Drawing.Color.Black
        Me.lblSections.Location = New System.Drawing.Point(0, 0)
        Me.lblSections.Name = "lblSections"
        Me.lblSections.Size = New System.Drawing.Size(180, 23)
        Me.lblSections.TabIndex = 1
        Me.lblSections.Text = "Section Details"
        Me.lblSections.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'spltDataDictionary
        '
        Me.spltDataDictionary.BackColor = System.Drawing.Color.WhiteSmoke
        Me.spltDataDictionary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.spltDataDictionary.Dock = System.Windows.Forms.DockStyle.Top
        Me.spltDataDictionary.Location = New System.Drawing.Point(0, 400)
        Me.spltDataDictionary.Name = "spltDataDictionary"
        Me.spltDataDictionary.Size = New System.Drawing.Size(180, 4)
        Me.spltDataDictionary.TabIndex = 1
        Me.spltDataDictionary.TabStop = False
        '
        'pnlDataDictionary
        '
        Me.pnlDataDictionary.Controls.Add(Me.trDataDictionary)
        Me.pnlDataDictionary.Controls.Add(Me.pnlDataTitle)
        Me.pnlDataDictionary.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDataDictionary.Location = New System.Drawing.Point(0, 0)
        Me.pnlDataDictionary.Name = "pnlDataDictionary"
        Me.pnlDataDictionary.Size = New System.Drawing.Size(180, 400)
        Me.pnlDataDictionary.TabIndex = 0
        '
        'trDataDictionary
        '
        Me.trDataDictionary.BackColor = System.Drawing.Color.GhostWhite
        Me.trDataDictionary.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trDataDictionary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trDataDictionary.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trDataDictionary.ImageIndex = 0
        Me.trDataDictionary.ImageList = Me.ImgTreeview
        Me.trDataDictionary.Indent = 21
        Me.trDataDictionary.ItemHeight = 18
        Me.trDataDictionary.Location = New System.Drawing.Point(0, 24)
        Me.trDataDictionary.Name = "trDataDictionary"
        Me.trDataDictionary.SelectedImageIndex = 0
        Me.trDataDictionary.ShowLines = False
        Me.trDataDictionary.ShowRootLines = False
        Me.trDataDictionary.Size = New System.Drawing.Size(180, 376)
        Me.trDataDictionary.TabIndex = 1
        '
        'pnlDataTitle
        '
        Me.pnlDataTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnlDataTitle.BackgroundImage = CType(resources.GetObject("pnlDataTitle.BackgroundImage"), System.Drawing.Image)
        Me.pnlDataTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDataTitle.Controls.Add(Me.lblDataDictionary)
        Me.pnlDataTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDataTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlDataTitle.Name = "pnlDataTitle"
        Me.pnlDataTitle.Size = New System.Drawing.Size(180, 24)
        Me.pnlDataTitle.TabIndex = 0
        '
        'lblDataDictionary
        '
        Me.lblDataDictionary.BackColor = System.Drawing.Color.Transparent
        Me.lblDataDictionary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDataDictionary.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblDataDictionary.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDataDictionary.ForeColor = System.Drawing.Color.Black
        Me.lblDataDictionary.Location = New System.Drawing.Point(0, 0)
        Me.lblDataDictionary.Name = "lblDataDictionary"
        Me.lblDataDictionary.Size = New System.Drawing.Size(180, 23)
        Me.lblDataDictionary.TabIndex = 0
        Me.lblDataDictionary.Text = "Data Dictionary"
        Me.lblDataDictionary.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlRight
        '
        Me.pnlRight.Controls.Add(Me.pnlReportExplorer)
        Me.pnlRight.Controls.Add(Me.spltReportExplorer)
        Me.pnlRight.Controls.Add(Me.pnlProperty)
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlRight.Location = New System.Drawing.Point(848, 0)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Size = New System.Drawing.Size(180, 710)
        Me.pnlRight.TabIndex = 6
        '
        'pnlReportExplorer
        '
        Me.pnlReportExplorer.Controls.Add(Me.prpProperties)
        Me.pnlReportExplorer.Controls.Add(Me.pnlTitleProperty)
        Me.pnlReportExplorer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlReportExplorer.Location = New System.Drawing.Point(0, 361)
        Me.pnlReportExplorer.Name = "pnlReportExplorer"
        Me.pnlReportExplorer.Size = New System.Drawing.Size(180, 349)
        Me.pnlReportExplorer.TabIndex = 2
        '
        'prpProperties
        '
        Me.prpProperties.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.prpProperties.CategoryForeColor = System.Drawing.Color.Black
        Me.prpProperties.CommandsActiveLinkColor = System.Drawing.Color.Maroon
        Me.prpProperties.CommandsBackColor = System.Drawing.Color.GhostWhite
        Me.prpProperties.CommandsDisabledLinkColor = System.Drawing.Color.SlateGray
        Me.prpProperties.CommandsForeColor = System.Drawing.Color.White
        Me.prpProperties.CommandsLinkColor = System.Drawing.Color.Navy
        Me.prpProperties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.prpProperties.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.prpProperties.HelpBackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.prpProperties.HelpForeColor = System.Drawing.Color.Black
        Me.prpProperties.LineColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.prpProperties.Location = New System.Drawing.Point(0, 24)
        Me.prpProperties.Name = "prpProperties"
        Me.prpProperties.Size = New System.Drawing.Size(180, 325)
        Me.prpProperties.TabIndex = 6
        Me.prpProperties.ViewBackColor = System.Drawing.Color.GhostWhite
        Me.prpProperties.ViewForeColor = System.Drawing.Color.Black
        '
        'pnlTitleProperty
        '
        Me.pnlTitleProperty.BackgroundImage = CType(resources.GetObject("pnlTitleProperty.BackgroundImage"), System.Drawing.Image)
        Me.pnlTitleProperty.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTitleProperty.Controls.Add(Me.lblProperty)
        Me.pnlTitleProperty.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTitleProperty.Location = New System.Drawing.Point(0, 0)
        Me.pnlTitleProperty.Name = "pnlTitleProperty"
        Me.pnlTitleProperty.Size = New System.Drawing.Size(180, 24)
        Me.pnlTitleProperty.TabIndex = 0
        '
        'lblProperty
        '
        Me.lblProperty.BackColor = System.Drawing.Color.Transparent
        Me.lblProperty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblProperty.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblProperty.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProperty.ForeColor = System.Drawing.Color.Black
        Me.lblProperty.Location = New System.Drawing.Point(0, 0)
        Me.lblProperty.Name = "lblProperty"
        Me.lblProperty.Size = New System.Drawing.Size(180, 23)
        Me.lblProperty.TabIndex = 1
        Me.lblProperty.Text = "Properties"
        Me.lblProperty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'spltReportExplorer
        '
        Me.spltReportExplorer.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.spltReportExplorer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.spltReportExplorer.Dock = System.Windows.Forms.DockStyle.Top
        Me.spltReportExplorer.Location = New System.Drawing.Point(0, 360)
        Me.spltReportExplorer.Name = "spltReportExplorer"
        Me.spltReportExplorer.Size = New System.Drawing.Size(180, 1)
        Me.spltReportExplorer.TabIndex = 1
        Me.spltReportExplorer.TabStop = False
        '
        'pnlProperty
        '
        Me.pnlProperty.Controls.Add(Me.trReportExplorer)
        Me.pnlProperty.Controls.Add(Me.pnlTitleReportExplorer)
        Me.pnlProperty.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlProperty.Location = New System.Drawing.Point(0, 0)
        Me.pnlProperty.Name = "pnlProperty"
        Me.pnlProperty.Size = New System.Drawing.Size(180, 360)
        Me.pnlProperty.TabIndex = 0
        '
        'trReportExplorer
        '
        Me.trReportExplorer.BackColor = System.Drawing.Color.GhostWhite
        Me.trReportExplorer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trReportExplorer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trReportExplorer.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trReportExplorer.ImageIndex = 0
        Me.trReportExplorer.ImageList = Me.ImgExplorer
        Me.trReportExplorer.Indent = 21
        Me.trReportExplorer.ItemHeight = 18
        Me.trReportExplorer.Location = New System.Drawing.Point(0, 24)
        Me.trReportExplorer.Name = "trReportExplorer"
        Me.trReportExplorer.SelectedImageIndex = 0
        Me.trReportExplorer.ShowLines = False
        Me.trReportExplorer.ShowRootLines = False
        Me.trReportExplorer.Size = New System.Drawing.Size(180, 336)
        Me.trReportExplorer.TabIndex = 1
        '
        'pnlTitleReportExplorer
        '
        Me.pnlTitleReportExplorer.BackgroundImage = CType(resources.GetObject("pnlTitleReportExplorer.BackgroundImage"), System.Drawing.Image)
        Me.pnlTitleReportExplorer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTitleReportExplorer.Controls.Add(Me.lblReportExplorer)
        Me.pnlTitleReportExplorer.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTitleReportExplorer.Location = New System.Drawing.Point(0, 0)
        Me.pnlTitleReportExplorer.Name = "pnlTitleReportExplorer"
        Me.pnlTitleReportExplorer.Size = New System.Drawing.Size(180, 24)
        Me.pnlTitleReportExplorer.TabIndex = 0
        '
        'lblReportExplorer
        '
        Me.lblReportExplorer.BackColor = System.Drawing.Color.Transparent
        Me.lblReportExplorer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReportExplorer.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblReportExplorer.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReportExplorer.ForeColor = System.Drawing.Color.Black
        Me.lblReportExplorer.Location = New System.Drawing.Point(0, 0)
        Me.lblReportExplorer.Name = "lblReportExplorer"
        Me.lblReportExplorer.Size = New System.Drawing.Size(180, 23)
        Me.lblReportExplorer.TabIndex = 1
        Me.lblReportExplorer.Text = "Report Explorer"
        Me.lblReportExplorer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmRxReportDesigner
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1028, 710)
        Me.Controls.Add(Me.pnlCenter)
        Me.Controls.Add(Me.splRight)
        Me.Controls.Add(Me.splLeft)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.pnlRight)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.ForeColor = System.Drawing.Color.Black
        Me.Name = "frmRxReportDesigner"
        Me.ShowInTaskbar = False
        Me.Text = "gloEMR Admin - Rx Report Designer"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlCenter.ResumeLayout(False)
        Me.pnlCenterDesigner.ResumeLayout(False)
        Me.pnlReportFooter.ResumeLayout(False)
        Me.pnlPageFooter.ResumeLayout(False)
        Me.pnlDetails.ResumeLayout(False)
        Me.pnlPageHeader.ResumeLayout(False)
        Me.pnlPageHeader.PerformLayout()
        Me.pnlReportHeader.ResumeLayout(False)
        Me.pnlCenterTop.ResumeLayout(False)
        Me.pnlCenterTop.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlSections.ResumeLayout(False)
        Me.pnlTitleSections.ResumeLayout(False)
        Me.pnlDataDictionary.ResumeLayout(False)
        Me.pnlDataTitle.ResumeLayout(False)
        Me.pnlRight.ResumeLayout(False)
        Me.pnlReportExplorer.ResumeLayout(False)
        Me.pnlTitleProperty.ResumeLayout(False)
        Me.pnlProperty.ResumeLayout(False)
        Me.pnlTitleReportExplorer.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmRxReportDesigner_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' If gstrRxReportpath <> "" Then
            'If IsSettings() = False Then
            '    MessageBox.Show("SQL Server or Database are not set. Please set these parameters by running gloEMR. Now application will be closed")
            'End If

            objDataDictionary = New ClsRxReportDictionary

            'Fill Patient and Provider Data
            InitialiseDataDictionary()

            'Fill Sections Treeview
            InitialiseSections()

            'Fill Report Explorer with Custom Fields
            InitialiseReportExplorer()

            pnlReportHeader.AllowDrop = True
            pnlPageHeader.AllowDrop = True
            pnlDetails.AllowDrop = True
            pnlPageFooter.AllowDrop = True
            pnlReportFooter.AllowDrop = True

            'InsertRxReport()
            If Not File.Exists(strFileName) Then
                FillReportDesigner()
            End If
            If File.Exists(strFileName) Then
                Generatefrm()
            End If
        Catch ex As Exception
            MsgBox("Error Loading Report Designer", MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly, gstrMessageBoxCaption)
            'sarika  27th apr
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Modify, "Error Loading Report Designer", gstrLoginName, gstrClientMachineName, 0, , clsAudit.enmOutcome.Failure)
            objAudit = Nothing
            '-------------
        End Try

    End Sub
    'Private Function IsSettings() As Boolean
    '    Dim regKey As RegistryKey
    '    If IsNothing(Registry.LocalMachine.OpenSubKey("Software\gloEMR")) = True Then
    '        Return False
    '    End If
    '    regKey = Registry.LocalMachine.OpenSubKey("Software\gloEMR", True)
    '    If IsNothing(regKey.GetValue("SQLServer")) = True Then
    '        regKey.Close()
    '        Return False
    '    End If
    '    If IsNothing(regKey.GetValue("Database")) = True Then
    '        regKey.Close()
    '        Return False
    '    End If
    '    gstrSQLServerName = regKey.GetValue("SQLServer")
    '    gstrDatabaseName = regKey.GetValue("Database")
    '    regKey.Close()
    '    If gstrSQLServerName = "" Or gstrDatabaseName = "" Then
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function
    Private Sub InitialiseDataDictionary()
        trDataDictionary.Nodes.Add("Tables")
        trDataDictionary.Nodes.Item(0).SelectedImageIndex = 0

        trDataDictionary.Nodes.Item(0).Nodes.Add("Prescription")
        trDataDictionary.Nodes.Item(0).Nodes.Item(0).SelectedImageIndex = 0

        FillTreeView(True)
        trDataDictionary.Nodes.Item(0).ExpandAll()
    End Sub
    Private Sub InitialiseSections()
        'Add nodes to sections treeview
        trSections.Nodes.Add("Sections")
        trSections.Nodes.Item(0).ImageIndex = 0
        trSections.Nodes.Item(0).SelectedImageIndex = 0

        trSections.Nodes.Item(0).Nodes.Add("Report Header")
        trSections.Nodes.Item(0).Nodes.Item(0).ImageIndex = 1
        trSections.Nodes.Item(0).Nodes.Item(0).SelectedImageIndex = 1

        trSections.Nodes.Item(0).Nodes.Add("Page Header")
        trSections.Nodes.Item(0).Nodes.Item(1).ImageIndex = 2
        trSections.Nodes.Item(0).Nodes.Item(1).SelectedImageIndex = 2

        trSections.Nodes.Item(0).Nodes.Add("Details")
        trSections.Nodes.Item(0).Nodes.Item(2).ImageIndex = 3
        trSections.Nodes.Item(0).Nodes.Item(2).SelectedImageIndex = 3

        trSections.Nodes.Item(0).Nodes.Add("Page Footer")
        trSections.Nodes.Item(0).Nodes.Item(3).ImageIndex = 4
        trSections.Nodes.Item(0).Nodes.Item(3).SelectedImageIndex = 4

        trSections.Nodes.Item(0).Nodes.Add("Report Footer")
        trSections.Nodes.Item(0).Nodes.Item(4).ImageIndex = 4
        trSections.Nodes.Item(0).Nodes.Item(4).SelectedImageIndex = 4

        trSections.Nodes.Item(0).ExpandAll()
    End Sub
    Private Function Splittext(ByVal strsplittext As String, Optional ByVal bflg As Boolean = False) As String
        If Trim(strsplittext) <> "" Then
            Dim arrstring() As String
            arrstring = Split(strsplittext, ".")
            If arrstring.Length > 0 Then
                If bflg Then
                    Return arrstring(0)
                Else
                    Return arrstring(1)
                End If
            Else
                Return strsplittext
            End If
        Else
            Return ""
        End If
    End Function
    Private Sub FillTreeView(ByVal m_flag As Boolean)
        Try
            Dim objdatatable As New DataTable
            Dim objClinicImage As New DataTable
            Dim objProviderSign As New DataTable
            'Populate Patient Data
            objdatatable = objDataDictionary.GetDictionary(m_flag)
            Dim i As Int16
            Dim objnode As TreeNode
            If m_flag Then
                trDataDictionary.Nodes.Item(0).Nodes.Item(0).Nodes.Clear()
                'Populate treeview with patient data
                For i = 0 To objdatatable.Columns.Count - 1
                    objnode = New TreeNode(objdatatable.Columns.Item(i).ColumnName)
                    objnode.ImageIndex = 1
                    objnode.SelectedImageIndex = 1
                    trDataDictionary.Nodes.Item(0).Nodes.Item(0).Nodes.Add(objnode)
                Next
                ' Code added by Ravikiran
                objClinicImage = objDataDictionary.GetClinicLogo()
                If Not objClinicImage Is Nothing Then
                    objnode = New TreeNode
                    objnode.Tag = "Image"
                    objnode.Text = "ClinicLogo"
                    objnode.ImageIndex = 1
                    objnode.SelectedImageIndex = 1
                    trDataDictionary.Nodes.Item(0).Nodes.Item(0).Nodes.Add(objnode)
                End If
                objProviderSign = objDataDictionary.GetProviderSign
                If Not objProviderSign Is Nothing Then
                    objnode = New TreeNode
                    objnode.Tag = "Image"
                    objnode.Text = "ProviderSignature"
                    objnode.ImageIndex = 1
                    objnode.SelectedImageIndex = 1
                    trDataDictionary.Nodes.Item(0).Nodes.Item(0).Nodes.Add(objnode)

                End If

                objnode = New TreeNode
                objnode.Text = "SeniorProvider1"
                objnode.ImageIndex = 1
                objnode.SelectedImageIndex = 1
                trDataDictionary.Nodes.Item(0).Nodes.Item(0).Nodes.Add(objnode)
                objnode = Nothing

                objnode = New TreeNode
                objnode.Text = "SeniorProvider2"
                objnode.ImageIndex = 1
                objnode.SelectedImageIndex = 1
                trDataDictionary.Nodes.Item(0).Nodes.Item(0).Nodes.Add(objnode)
                objnode = Nothing

                objnode = New TreeNode
                objnode.Text = "DisclaimerNotes"
                objnode.ImageIndex = 1
                objnode.SelectedImageIndex = 1
                trDataDictionary.Nodes.Item(0).Nodes.Item(0).Nodes.Add(objnode)
                objnode = Nothing

                trDataDictionary.Nodes.Item(0).Nodes.Item(0).ExpandAll()
                ' Updation Ends
            Else
                trDataDictionary.Nodes.Item(0).Nodes.Item(1).Nodes.Clear()
                'Populate treeview with provider data

                For i = 0 To objdatatable.Columns.Count - 1
                    objnode = New TreeNode(objdatatable.Columns.Item(i).ColumnName)
                    objnode.ImageIndex = 1
                    objnode.SelectedImageIndex = 1
                    trDataDictionary.Nodes.Item(0).Nodes.Item(1).Nodes.Add(objnode)
                Next
                trDataDictionary.Nodes.Item(0).Nodes.Item(1).ExpandAll()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub InitialiseReportExplorer()
        'Add special fields to the report explorer
        trReportExplorer.Nodes.Add("Special Fields")
        trReportExplorer.Nodes.Item(0).ImageIndex = 4
        trReportExplorer.Nodes.Item(0).SelectedImageIndex = 4

        trReportExplorer.Nodes.Item(0).Nodes.Add("Current Date")
        trReportExplorer.Nodes.Item(0).Nodes.Item(0).ImageIndex = 0
        trReportExplorer.Nodes.Item(0).Nodes.Item(0).SelectedImageIndex = 0

        trReportExplorer.Nodes.Item(0).Nodes.Add("Current Time")
        trReportExplorer.Nodes.Item(0).Nodes.Item(1).ImageIndex = 8
        trReportExplorer.Nodes.Item(0).Nodes.Item(1).SelectedImageIndex = 8

        trReportExplorer.Nodes.Item(0).Nodes.Add("Current Page")
        trReportExplorer.Nodes.Item(0).Nodes.Item(2).ImageIndex = 1
        trReportExplorer.Nodes.Item(0).Nodes.Item(2).SelectedImageIndex = 1

        'trReportExplorer.Nodes.Item(0).Nodes.Add("File Author")
        'trReportExplorer.Nodes.Item(0).Nodes.Item(3).ImageIndex = 2
        'trReportExplorer.Nodes.Item(0).Nodes.Item(3).SelectedImageIndex = 2

        trReportExplorer.Nodes.Item(0).ExpandAll()

        'trReportExplorer.Nodes.Add("Selection Criteria")
        'trReportExplorer.Nodes.Item(1).ImageIndex = 3
        'trReportExplorer.Nodes.Item(1).SelectedImageIndex = 3

        'trReportExplorer.Nodes.Add("Formula")
        'trReportExplorer.Nodes.Item(2).ImageIndex = 6
        'trReportExplorer.Nodes.Item(2).SelectedImageIndex = 6

        'trReportExplorer.Nodes.Item(2).Nodes.Add("Record Count")
        'trReportExplorer.Nodes.Item(2).Nodes.Item(0).ImageIndex = 7
        'trReportExplorer.Nodes.Item(2).Nodes.Item(0).SelectedImageIndex = 7

        trReportExplorer.ExpandAll()
    End Sub
    Private Function Generatefrm() As Boolean
        Dim ds As New DataSet
        '  ds.ReadXml(gstrRxReportpath & "\PrescriptionReport.xml", XmlReadMode.InferSchema)
        ds.ReadXml(strFileName, XmlReadMode.InferSchema)
        Dim dt As DataTable
        '' Dim xxx As String = "Provider=sqloledb;Data Source=SakarServer;User Id=sa;Password=;database=DrugInter"
        Setsection(ds)
        For Each dt In ds.Tables

            Select Case dt.TableName.Substring(0, 3)

                Case "RHF"
                    BindControl(dt, dt.TableName.Substring(0, 3))
                    ' BindCollection(dt, dt.TableName.Substring(0, dt.TableName.Length - 1))
                    ' MsgBox("RHField")
                Case "PHF"
                    BindControl(dt, dt.TableName.Substring(0, 3))
                    'BindCollection(dt, dt.TableName.Substring(0, dt.TableName.Length - 1))
                Case "DTF"
                    BindControl(dt, dt.TableName.Substring(0, 3))
                    ' MsgBox("DTField")
                Case "PFF"
                    BindControl(dt, dt.TableName.Substring(0, 3))
                    'BindCollection(dt, dt.TableName.Substring(0, dt.TableName.Length - 1))
                Case "RFF"
                    BindControl(dt, dt.TableName.Substring(0, 3))
                    'BindCollection(dt, dt.TableName.Substring(0, dt.TableName.Length - 1))

            End Select
        Next

    End Function

    Private Function BindControl(ByVal objTable As DataTable, ByVal Section As String) As Boolean
        Dim objDV As New DataView
        objDV = objTable.DefaultView
        If objDV.Count > 0 Then

            If objTable.Rows(0).Item("FieldType") <> "Image" And objTable.Rows(0).Item("FieldType") <> "Caption" Then
                Dim objlabel As New Label
                AddHandler objlabel.MouseDown, AddressOf objectlbl_MouseDown
                AddHandler objlabel.MouseUp, AddressOf objectlbl_MouseUp
                AddHandler objlabel.MouseMove, AddressOf objectlbl_MouseMove
                AddHandler objlabel.Click, AddressOf objectlbl_Click
                With objlabel
                    .Tag = objTable.Rows(0).Item("Name")
                    .Text = objTable.Rows(0).Item("Text")
                    .Width = objTable.Rows(0).Item("Width")
                    .Height = objTable.Rows(0).Item("Height")
                    .ContextMenu = CntFieldMenu
                    ' If objTable.Rows(0).Item(7) <> "" Then
                    .Font = New System.Drawing.Font(CStr(objTable.Rows(0).Item("FontName") & ""), CSng(Val(objTable.Rows(0).Item("FontSize"))), CInt(objTable.Rows(0).Item("FontStyle")), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    'Else
                    '    .Font = New System.Drawing.Font(CStr(objTable.Rows(0).Item("FontName") & ""), CSng(Val(objTable.Rows(0).Item("FontSize"))), FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    'End If

                    Select Case Section
                        Case "RHF"
                            .Top = (objTable.Rows(0).Item("Top") * 0.01 * pnlReportHeader.Height) + tblDesigner.Height
                            .Left = (objTable.Rows(0).Item("Left") * 0.01 * pnlReportHeader.Width) + pnlCenter.Left

                        Case "PHF"
                            .Top = (objTable.Rows(0).Item("Top") * 0.01 * pnlPageHeader.Height) + (pnlPageHeader.Top + tblDesigner.Height)
                            .Left = (objTable.Rows(0).Item("Left") * 0.01 * pnlPageHeader.Width) + pnlCenter.Left
                        Case "DTF"
                            .Top = (objTable.Rows(0).Item("Top") * 0.01 * pnlDetails.Height) + (pnlDetails.Top + tblDesigner.Height)
                            .Left = (objTable.Rows(0).Item("Left") * 0.01 * pnlDetails.Width) + pnlCenter.Left

                        Case "PFF"
                            .Top = (objTable.Rows(0).Item("Top") * 0.01 * pnlPageFooter.Height) + (pnlPageFooter.Top + tblDesigner.Height)
                            .Left = (objTable.Rows(0).Item("Left") * 0.01 * pnlPageFooter.Width) + pnlCenter.Left

                        Case "RFF"
                            .Top = (objTable.Rows(0).Item("Top") * 0.01 * pnlReportFooter.Height) + (pnlReportFooter.Top + tblDesigner.Height)
                            .Left = (objTable.Rows(0).Item("Left") * 0.01 * pnlReportFooter.Width) + pnlCenter.Left
                    End Select
                End With
                Me.Controls.Add(objlabel)
                objlabel.BringToFront()

            ElseIf objTable.Rows(0).Item("FieldType") <> "Image" Then
                Dim objtxt As New TextBox
                AddHandler objtxt.MouseDown, AddressOf objecttxt_MouseDown
                AddHandler objtxt.MouseUp, AddressOf objecttxt_MouseUp
                AddHandler objtxt.MouseMove, AddressOf objecttxt_MouseMove
                AddHandler objtxt.Click, AddressOf objecttxt_Click

                With objtxt
                    .Tag = objTable.Rows(0).Item("Name")
                    .Text = objTable.Rows(0).Item("Text")
                    '.Top = objTable.Rows(0).Item("Top")
                    '.Left = objTable.Rows(0).Item("Left")
                    .Width = objTable.Rows(0).Item("Width")

                    .Height = objTable.Rows(0).Item("Height")
                    .ContextMenu = CntFieldMenu
                    ' If objTable.Rows(0).Item(7) <> "" Then
                    .Font = New System.Drawing.Font(CStr(objTable.Rows(0).Item("FontName") & ""), CSng(Val(objTable.Rows(0).Item("FontSize"))), CInt(objTable.Rows(0).Item("FontStyle")), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    'Else
                    '    .Font = New System.Drawing.Font(CStr(objTable.Rows(0).Item("FontName") & ""), CSng(Val(objTable.Rows(0).Item("FontSize"))), FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                    'End If

                    Select Case Section
                        Case "RHF"
                            .Top = (objTable.Rows(0).Item("Top") * 0.01 * pnlReportHeader.Height) + tblDesigner.Height
                            .Left = (objTable.Rows(0).Item("Left") * 0.01 * pnlReportHeader.Width) + pnlCenter.Left

                        Case "PHF"
                            .Top = (objTable.Rows(0).Item("Top") * 0.01 * pnlPageHeader.Height) + (pnlPageHeader.Top + tblDesigner.Height)
                            .Left = (objTable.Rows(0).Item("Left") * 0.01 * pnlPageHeader.Width) + pnlCenter.Left
                        Case "DTF"
                            .Top = (objTable.Rows(0).Item("Top") * 0.01 * pnlDetails.Height) + (pnlDetails.Top + tblDesigner.Height)
                            .Left = (objTable.Rows(0).Item("Left") * 0.01 * pnlDetails.Width) + pnlCenter.Left

                        Case "PFF"
                            .Top = (objTable.Rows(0).Item("Top") * 0.01 * pnlPageFooter.Height) + (pnlPageFooter.Top + tblDesigner.Height)

                            .Left = (objTable.Rows(0).Item("Left") * 0.01 * pnlPageFooter.Width) + pnlCenter.Left

                        Case "RFF"
                            .Top = (objTable.Rows(0).Item("Top") * 0.01 * pnlReportFooter.Height) + (pnlReportFooter.Top + tblDesigner.Height)
                            .Left = (objTable.Rows(0).Item("Left") * 0.01 * pnlReportFooter.Width) + pnlCenter.Left

                    End Select
                End With
                objtxt.Multiline = True
                objtxt.AcceptsReturn = True
                Me.Controls.Add(objtxt)
                objtxt.BringToFront()
            Else
                Dim imgCtrl As New PictureBox
                AddHandler imgCtrl.MouseDown, AddressOf objectimg_MouseDown
                AddHandler imgCtrl.MouseUp, AddressOf objectimg_MouseUp
                AddHandler imgCtrl.MouseMove, AddressOf objectimg_MouseMove
                AddHandler imgCtrl.Click, AddressOf objectimg_Click
                obj = New ClsRxReportDictionary
                With imgCtrl
                    .Tag = objTable.Rows(0).Item("Name")
                    .Text = objTable.Rows(0).Item("Text")
                    ' .Top = objTable.Rows(0).Item("Top")
                    ' .Left = objTable.Rows(0).Item("Left")
                    .Width = objTable.Rows(0).Item("Width")
                    .Height = objTable.Rows(0).Item("Height")
                    .ContextMenu = CntFieldMenu
                    Select Case Section
                        Case "RHF"
                            .Top = (objTable.Rows(0).Item("Top") * 0.01 * pnlReportHeader.Height) + tblDesigner.Height
                            .Left = (objTable.Rows(0).Item("Left") * 0.01 * pnlReportHeader.Width) + pnlCenter.Left

                        Case "PHF"
                            .Top = (objTable.Rows(0).Item("Top") * 0.01 * pnlPageHeader.Height) + (pnlPageHeader.Top + tblDesigner.Height)
                            .Left = (objTable.Rows(0).Item("Left") * 0.01 * pnlPageHeader.Width) + pnlCenter.Left
                        Case "DTF"
                            .Top = (objTable.Rows(0).Item("Top") * 0.01 * pnlDetails.Height) + (pnlDetails.Top + tblDesigner.Height)
                            .Left = (objTable.Rows(0).Item("Left") * 0.01 * pnlDetails.Width) + pnlCenter.Left

                        Case "PFF"
                            .Top = (objTable.Rows(0).Item("Top") * 0.01 * pnlPageFooter.Height) + (pnlPageFooter.Top + tblDesigner.Height)

                            .Left = (objTable.Rows(0).Item("Left") * 0.01 * pnlPageFooter.Width) + pnlCenter.Left

                        Case "RFF"
                            .Top = (objTable.Rows(0).Item("Top") * 0.01 * pnlReportFooter.Height) + (pnlReportFooter.Top + tblDesigner.Height)
                            .Left = (objTable.Rows(0).Item("Left") * 0.01 * pnlReportFooter.Width) + pnlCenter.Left


                    End Select
                End With
                Me.Controls.Add(imgCtrl)
                imgCtrl.BringToFront()
            End If

            'PrintDocument.
            'frm.ActiveForm.Show()
        End If
    End Function

    Private Function ReadSection(ByVal oData As DataSet) As Boolean
        Dim otable As New DataTable
        For Each otable In oData.Tables
            Select Case otable.TableName
                Case "ReportHeader"
                    SectionDetails(otable, otable.TableName)
                Case "PageHeader"
                    SectionDetails(otable, otable.TableName)
                Case "Details"
                    SectionDetails(otable, otable.TableName)
                Case "PageFooter"
                    SectionDetails(otable, otable.TableName)
                Case "ReportFooter"
                    SectionDetails(otable, otable.TableName)

            End Select
        Next
    End Function
    Private Function RetreiveSection(ByVal oTable As DataTable, ByVal TableName As String) As Boolean

        Select Case TableName
            Case "ReportHeader"
                pnlReportHeader.Height = CalAbsHeight(oTable.Rows(0).Item("Height"))
                pnlReportHeader.Width = CalAbsWidth(oTable.Rows(0).Item("Width"))

            Case "PageHeader"
                pnlPageHeader.Height = CalAbsHeight(oTable.Rows(0).Item("Height"))
                pnlPageHeader.Width = CalAbsWidth(oTable.Rows(0).Item("Width"))
            Case "Details"
                pnlDetails.Height = CalAbsHeight(oTable.Rows(0).Item("Height"))
                pnlDetails.Width = CalAbsWidth(oTable.Rows(0).Item("Width"))
            Case "PageFooter"
                pnlPageFooter.Height = CalAbsHeight(oTable.Rows(0).Item("Height"))
                pnlPageFooter.Width = CalAbsWidth(oTable.Rows(0).Item("Width"))
            Case "ReportFooter"
                pnlReportFooter.Height = CalAbsHeight(oTable.Rows(0).Item("Height"))
                pnlReportFooter.Width = CalAbsWidth(oTable.Rows(0).Item("Width"))

        End Select

    End Function

    Private Function CalAbsWidth(ByVal RelValue As Int16) As Integer
        Dim AbsValue As Integer = RelValue * 0.01 * pnlCenter.Width
        Return AbsValue
    End Function
    Private Function CalAbsHeight(ByVal RelValue As Int16) As Integer
        Dim AbsValue As Integer = RelValue * 0.01 * pnlCenter.Height
        Return AbsValue
    End Function
    Private Function Setsection(ByVal oDataSet As DataSet) As Boolean
        Dim otable As DataTable
        For Each otable In oDataSet.Tables
            Select Case otable.TableName
                Case "ReportHeader"
                    pnlReportHeader.Height = CalAbsHeight(otable.Rows(0).Item("Height"))
                    pnlReportHeader.Width = CalAbsWidth(otable.Rows(0).Item("Width"))
                Case "PageHeader"
                    pnlPageHeader.Height = CalAbsHeight(otable.Rows(0).Item("Height"))
                    pnlPageHeader.Width = CalAbsWidth(otable.Rows(0).Item("Width"))
                Case "Details"
                    pnlDetails.Height = CalAbsHeight(otable.Rows(0).Item("Height"))
                    pnlDetails.Width = CalAbsWidth(otable.Rows(0).Item("Width"))
                Case "PageFooter"
                    pnlPageFooter.Height = CalAbsHeight(otable.Rows(0).Item("Height"))
                    pnlPageFooter.Width = CalAbsWidth(otable.Rows(0).Item("Width"))
                Case "ReportFooter"
                    pnlReportFooter.Height = CalAbsHeight(otable.Rows(0).Item("Height"))
                    pnlReportFooter.Width = CalAbsWidth(otable.Rows(0).Item("Width"))

            End Select
        Next

    End Function
    Private Sub InvokePrintPreview(ByRef dggrid As DataGrid, ByVal height As Double)
        'original code commented by sandip on 19/10/2007
        'If GridPrinter Is Nothing Then
        '    GridPrinter = New RxReportPrinter(dggrid)
        'End If

        'With Me.PrintPreviewDialog1
        '    .Document = GridPrinter.PrintDocument

        '    'Fill Section Collection Class
        '    GridPrinter.SetHeaderControls(ReportHeaderCol, 1)
        '    GridPrinter.SetHeaderControls(PageHeaderCol, 2)
        '    GridPrinter.SetHeaderControls(PageFooterCol, 4)
        '    GridPrinter.SetHeaderControls(ReportFooterCol, 5)
        '    GridPrinter.SetHeaderControls(SectionsCol, 6) ' vmg temp
        '    'Invoke Print Method of the GridPrinter Class

        '    'If .ShowDialog = DialogResult.OK Then
        '    GridPrinter.Rowheight = height
        '    GridPrinter.CurrentDate = Date.Today.ToShortDateString
        '    GridPrinter.CurrentTime = Now.ToShortTimeString
        '    ''

        '    ''
        '    'GridPrinter.Print()
        '    'End If
        'End With
        'PrintPreviewDialog1.Document.Dispose()
        'GridPrinter.Dispose()
        ''GridPrinter.PrintDocument.Dispose()
        'dggrid.Dispose()
        'GridPrinter = Nothing
        'original code commented by sandip on 19/10/2007

        If GridPrinter Is Nothing Then
            GridPrinter = New RxReportPrinter(dggrid)
        End If

        'Fill Section Collection Class
        GridPrinter.SetHeaderControls(ReportHeaderCol, 1)
        GridPrinter.SetHeaderControls(PageHeaderCol, 2)
        GridPrinter.SetHeaderControls(PageFooterCol, 4)
        GridPrinter.SetHeaderControls(ReportFooterCol, 5)
        GridPrinter.SetHeaderControls(SectionsCol, 6) ' vmg temp
        'Invoke Print Method of the GridPrinter Class

        GridPrinter.Rowheight = height
        GridPrinter.CurrentDate = Date.Today.ToShortDateString
        GridPrinter.CurrentTime = Now.ToShortTimeString
        '
        '' ''Dim rxpreview As frmrxpreview
        '' ''rxpreview = New frmrxpreview()
        '' ''rxpreview.PrntPreDlg.Document = GridPrinter.PrintDocument
        '' ''rxpreview.ShowDialog()
        '
        '******By Sandip Deshmukh 19 Oct 07 6.30PM Bug# 331
        '******the code is added for the page as previously on clicking the Preview Button 
        '******it prints the report Hence Error and the folowing code is added against the problem 
        Pr = New ExtendedPrintPreviewDialog

        Pr.Document = GridPrinter.PrintDocument

        Pr.Setheight = height
        Pr.SetDatagrid = dggrid
        'PrintPreviewDialog1.ParentForm.MaximizeBox = True
        'PrintPreviewDialog1.Size = New Size(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height - 15)
        ' PrintPreviewDialog1.AutoSizeMode = Windows.Forms.AutoSizeMode.GrowOnly
        Pr.ShowDialog()
        If Not IsNothing(Pr) Then
            Pr.Dispose()
            Pr = Nothing
        End If

        '******By Sandip Deshmukh 19 Oct 07 6.30PM Bug# 331
        GridPrinter.Dispose()
        'GridPrinter.PrintDocument.Dispose()
        dggrid.Dispose()
        GridPrinter = Nothing



    End Sub
    Private Sub InvokePrint(ByRef dggrid As DataGrid, ByVal height As Double)
        'Instantiate GridPrinter class
        If GridPrinter Is Nothing Then
            GridPrinter = New RxReportPrinter(dggrid)
        End If

        'Invoke PrintDialog
        With Me.PrintDialog1
            .Document = GridPrinter.PrintDocument

            'Populate Sections Collection
            GridPrinter.SetHeaderControls(ReportHeaderCol, 1)
            GridPrinter.SetHeaderControls(PageHeaderCol, 2)
            GridPrinter.SetHeaderControls(PageFooterCol, 4)
            GridPrinter.SetHeaderControls(ReportFooterCol, 5)
            GridPrinter.SetHeaderControls(SectionsCol, 6)

            'Invoke the Print Method of GridPrinter
            'If .ShowDialog = DialogResult.OK Then
            GridPrinter.Rowheight = height
            GridPrinter.Print()
            'End If
        End With
        PrintDialog1.Document.Dispose()
        GridPrinter.PrintDocument.Dispose()
        dggrid.Dispose()

    End Sub
    Private Function BindGrid(ByVal oTable As DataTable) As DataTable
        Dim strQuery As String
        obj = New ClsRxReportDictionary
        Dim oView As New DataView
        oView = oTable.DefaultView
        Dim strsort As String = oTable.Columns(1).ColumnName
        oView.Sort = "[" & strsort & "]"
        strQuery = "select "
        'build query to bind to datagrid
        If oView.Count > 1 Then
            For cnt As Int16 = 0 To oView.Count - 1
                Dim strtemp As String = ""
                If oView.Item(cnt).Item("Text") = "May Substitute" Then
                    strtemp = "[May Substitute]"
                    strQuery &= strtemp
                ElseIf oView.Item(cnt).Item("Text") = "May" Then
                    strtemp = "[May Substitute]"
                    strQuery &= strtemp
                Else
                    strQuery &= oView.Item(cnt).Item("Text")
                End If
                If cnt < oView.Count - 1 Then
                    strQuery &= ","
                End If
            Next
        End If


        'sarika 23rd oct 07
        'code commented
        'strQuery &= oView.Item(oView.Count - 1).Item("Text") & " from Rxreport" 'DetailsField(DetailsField.Count - 1) 
        'code added

        strQuery &= " from Rxreport1" 'DetailsField(DetailsField.Count - 1) 

        '----------------------------


        Dim objTable As New DataTable
        objTable = obj.getReportData(strQuery)
        Return objTable

    End Function
    Private Function SectionDetails(ByVal oTable As DataTable, ByVal sectiontype As String) As Boolean
        Dim objSection As New Section
        Dim dv As New DataView
        dv = oTable.DefaultView
        objSection.SectionType = sectiontype
        objSection.SectionWidth = oTable.Rows(0).Item("Width")
        objSection.SectionHeight = oTable.Rows(0).Item("Height")
        SectionsCol.Add(objSection)
    End Function

    Private Sub ClearCollection()
        Dim i As Int16
        'Make sure to Clear the collection before u can populate
        'it with new items
        If Not IsNothing(SectionsCol) Then

            For i = SectionsCol.Count To 1 Step -1
                SectionsCol.Remove(i)
            Next
        End If
        If Not IsNothing(ReportHeaderCol) Then
            For i = ReportHeaderCol.Count To 1 Step -1
                ReportHeaderCol.Remove(i)
            Next
        End If
        If Not IsNothing(PageHeaderCol) Then
            For i = PageHeaderCol.Count To 1 Step -1
                PageHeaderCol.Remove(i)
            Next
        End If
        If Not IsNothing(PageFooterCol) Then
            For i = PageFooterCol.Count To 1 Step -1
                PageFooterCol.Remove(i)
            Next
        End If
        If Not IsNothing(ReportFooterCol) Then
            For i = ReportFooterCol.Count To 1 Step -1
                ReportFooterCol.Remove(i)
            Next
        End If
    End Sub

    Private Function readXML(ByVal blnPrintStatus As Boolean) As Boolean
        Dim ds As New DataSet
        Dim objdatagrid As DataGrid = Nothing
        Dim objDetails As DataTable = Nothing

        ds.ReadXml(strFileName, XmlReadMode.InferSchema)
        Dim dt As DataTable
        '' Dim xxx As String = "Provider=sqloledb;Data Source=SakarServer;User Id=sa;Password=;database=DrugInter"


        ''Set Sections
        Call ClearCollection()
        SectionsCol = New Collection
        ReadSection(ds)

        ''Set For Datagrid 
        oDetails = New DataTable
        oDetails.Columns.Add("Text", GetType(String))
        oDetails.Columns.Add("Left", GetType(Int16))

        ''Reading each table for setting the controls
        For Each dt In ds.Tables
            ' Check for Database tag

            Select Case dt.TableName.Substring(0, 3)
                'Case "ReportHeade"
                '    SectionDetails(dt, dt.TableName)
                'Case "PageHeade"
                '    SectionDetails(dt, dt.TableName)
                'Case "Detail"
                '    SectionDetails(dt, dt.TableName)
                'Case "PageFoote"
                '    SectionDetails(dt, dt.TableName)
                'Case "ReportFoote"
                '    SectionDetails(dt, dt.TableName)
                Case "RHF"
                    ' BindControl(dt)
                    BindCollection(dt, dt.TableName.Substring(0, 3))
                    ' MsgBox("RHField")
                Case "PHF"
                    'BindControl(dt)
                    BindCollection(dt, dt.TableName.Substring(0, 3))
                Case "DTF"
                    Dim objDV As New DataView
                    objDV = dt.DefaultView
                    If objDV.Count > 0 Then
                        If dt.Rows(0).Item("FieldType") <> "Image" And dt.Rows(0).Item("FieldType") <> "Caption" Then
                            Dim objectdr As DataRow = oDetails.NewRow
                            objectdr.Item("Text") = dt.Rows(0).Item("Text")
                            objectdr.Item("Left") = dt.Rows(0).Item("Left")
                            oDetails.Rows.Add(objectdr)
                            'DetailsField.Add(dt.Rows(0).Item("Text"))
                            ' strLocation = dt.Rows(0).Item("Top") & "," & dt.Rows(0).Item("Left")

                        End If
                    End If

                    ' MsgBox("DTField")
                Case "PFF"
                    ' BindControl(dt)
                    BindCollection(dt, dt.TableName.Substring(0, 3))
                Case "RFF"
                    ' BindControl(dt)
                    BindCollection(dt, dt.TableName.Substring(0, 3))

            End Select

        Next

        'Dim objdatagrid As New DataGrid
        'If oDetails.Rows.Count > 0 Then
        '    Dim objDetails As New DataTable '// vmg add new
        '    objDetails = BindGrid(oDetails)
        '    objdatagrid.DataSource = objDetails
        '    Call CustomGridStyle(objdatagrid, objDetails)
        '    pnlDetails.Controls.Clear()
        '    pnlDetails.Controls.Add(objdatagrid)
        '    objdatagrid.Visible = False
        'End If
        If Not IsNothing(objdatagrid) Then
            objdatagrid = Nothing
        End If

        Dim _PrintFont As New Font(System.Drawing.FontFamily.GenericSansSerif, 8)
        objdatagrid = New DataGrid

        objdatagrid.Font = _PrintFont
        If oDetails.Rows.Count > 0 Then

            objDetails = New DataTable '// vmg add new
            If Not IsNothing(pnlPresciptionReport) Then
                Me.Controls.Remove(pnlPresciptionReport)
                pnlPresciptionReport = Nothing
            End If

            Dim pnlwidth As Long = 0


            pnlPresciptionReport = New Panel
            pnlPresciptionReport.Dock = DockStyle.Fill
            pnlPresciptionReport.Width = 650
            Dim objloc As New System.Drawing.Point(100, 379)

            pnlPresciptionReport.Location = objloc
            pnlPresciptionReport.Height = 234

            Me.Controls.Add(pnlPresciptionReport)

            objDetails = BindGrid(oDetails)
            objdatagrid.DataSource = objDetails
            pnlPresciptionReport.Controls.Clear()
            objdatagrid.Dock = DockStyle.Fill
            pnlPresciptionReport.Controls.Add(objdatagrid)
            CustomGridStyle(objdatagrid, objDetails)
            'pnlDetails.Controls.Clear()
            'pnlDetails.Controls.Add(objdatagrid)
            objdatagrid.Visible = False
        End If

        Dim j As Int32
        Dim height As Double
        For j = 0 To objDetails.Rows.Count - 1
            If objdatagrid.GetCellBounds(j, 0).Size.Height > height Then
                height = objdatagrid.GetCellBounds(j, 0).Size.Height
            End If
        Next

        If Not blnPrintStatus Then
            InvokePrintPreview(objdatagrid, height)
        Else
            InvokePrint(objdatagrid, height)
        End If

    End Function

    Public Sub CustomGridStyle(ByVal Grid As DataGrid, ByVal dt As DataTable)
        'Dim dv As DataView
        'dv = dt.DefaultView
        Dim ts As New clsDataGridTableStyle(dt.TableName)
        'Dim objclsCPT As New clsCPT

        'objclsCPT.SortDataview(dv.Table.Columns(2).ColumnName)
        Dim i As Integer
        'Code written by mahesh on 15/02/2007
        'Dim Drug As New DataGridTextBoxColumn
        'Dim SIG As New DataGridTextBoxColumn
        'For i = 0 To dt.Columns.Count - 1

        '    If UCase(dt.Columns(i).ColumnName) = UCase("Drug") Then
        '        Drug.Width = Grid.Width * 0.2
        '    ElseIf UCase(dt.Columns(i).ColumnName) = UCase("SIG") Then
        '        SIG.Width = Grid.Width * 0.3
        '    Else

        '    End If

        'Next
        'Code written by mahesh on 15/02/2007
        Dim objstyle As MultiLineColumn
        Dim _PrinttextFont As New Font(System.Drawing.FontFamily.GenericSansSerif, 8)

        Dim dbwidth As Double
        dbwidth = Grid.Width
        For i = 0 To dt.Columns.Count - 1
            objstyle = New MultiLineColumn
            objstyle.TextBox.Font = _PrinttextFont
            objstyle.MappingName = dt.Columns(i).ColumnName
            objstyle.HeaderText = dt.Columns(i).ColumnName
            objstyle.Alignment = HorizontalAlignment.Left

            If UCase(dt.Columns(i).ColumnName) = UCase("Drug") Then
                objstyle.Width = Grid.Width * 0.28
                objstyle.AutoAdjustHeight = True
                dbwidth = dbwidth - objstyle.Width
            ElseIf UCase(dt.Columns(i).ColumnName) = UCase("SIG") Then
                objstyle.Width = Grid.Width * 0.28
                objstyle.AutoAdjustHeight = True
                dbwidth = dbwidth - objstyle.Width
            ElseIf UCase(dt.Columns(i).ColumnName) = UCase("Dispense") Then
                objstyle.Width = Grid.Width * 0.1
                objstyle.AutoAdjustHeight = False
            ElseIf UCase(dt.Columns(i).ColumnName) = UCase("Refill") Then
                objstyle.Width = Grid.Width * 0.07
                objstyle.AutoAdjustHeight = False
            ElseIf UCase(dt.Columns(i).ColumnName) = UCase("May Substitute") Then
                objstyle.Width = Grid.Width * 0.17
                objstyle.AutoAdjustHeight = False
            ElseIf UCase(dt.Columns(i).ColumnName) = UCase("Notes") Then
                objstyle.Width = Grid.Width * 0.2
                objstyle.AutoAdjustHeight = False
            Else
                objstyle.Width = Grid.Width * 0.18
                objstyle.AutoAdjustHeight = False
            End If
            ts.GridColumnStyles.Add(objstyle)
            objstyle.Dispose()
            objstyle = Nothing
        Next

        'ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {Drug, SIG})
        Grid.TableStyles.Clear()
        Grid.TableStyles.Add(ts)

    End Sub

    Private Function BindCollection(ByVal objTable As DataTable, ByVal Section As String) As Boolean
        Dim objField As Field
        Dim objDV As New DataView
        objDV = objTable.DefaultView
        If objDV.Count > 0 Then
            objField = New Field
            If objTable.Rows(0).Item("FieldType") <> "Image" And objTable.Rows(0).Item("FieldType") <> "Caption" Then

                With objField
                    .FieldName = objTable.Rows(0).Item("Name")
                    .FieldText = objTable.Rows(0).Item("Text")
                    .FieldTop = objTable.Rows(0).Item("Top") '* scaley
                    .FieldLeft = objTable.Rows(0).Item("Left") '* scalex
                    .FieldWidth = objTable.Rows(0).Item("Width")
                    .FieldType = objTable.Rows(0).Item("FieldType")
                    .FieldHeight = objTable.Rows(0).Item("Height")

                    .FontName = objTable.Rows(0).Item("FontName")
                    .FontSize = Val(objTable.Rows(0).Item("FontSize"))
                    .FontStyle = objTable.Rows(0).Item("FontStyle")
                End With
                'sarika 26th june 07
                '                Select Case objTable.Rows(0).Item("Text")
                Select Case objTable.Rows(0).Item("Text").ToString
                    Case "Current Date"
                        'objField.FieldText = Date.Today.ToShortDateString
                    Case "Current Time"
                        'objField.FieldText = Date.Today.ToShortTimeString
                    Case "Current Page"

                    Case "File Author"

                    Case "Record Count"

                    Case "DisclaimerNotes"

                    Case "SeniorProvider1"
                        Dim Arrlist As New ArrayList
                        Arrlist = objDataDictionary.GetProviders
                        If Not Arrlist Is Nothing Then
                            If Arrlist.Count > 0 Then
                                Try
                                    If Arrlist(0) <> "" Then
                                        objField.FieldText = Arrlist(0)
                                    End If
                                Catch ex As Exception
                                    objField.FieldText = ""
                                End Try

                            Else
                                objField.FieldText = ""
                            End If
                        Else
                            objField.FieldText = ""
                        End If
                    Case "SeniorProvider2"
                        Dim Arrlist As New ArrayList
                        Arrlist = objDataDictionary.GetProviders
                        If Not Arrlist Is Nothing Then
                            If Arrlist.Count > 0 Then
                                Try
                                    If Arrlist(1) <> "" Then
                                        objField.FieldText = Arrlist(1)
                                    End If
                                Catch ex As Exception
                                    objField.FieldText = ""

                                End Try

                            Else
                                objField.FieldText = ""
                            End If
                        Else
                            objField.FieldText = ""
                        End If

                    Case Else
                        Dim FieldValue As String = GetData(objField.FieldText)
                        If Not FieldValue Is Nothing Then
                            objField.FieldText = FieldValue
                        End If

                End Select
                Select Case Section
                    Case "RHF"
                        ReportHeaderCol.Add(objField)
                    Case "PHF"
                        PageHeaderCol.Add(objField)
                    Case "PFF"
                        PageFooterCol.Add(objField)
                    Case "RFF"
                        ReportFooterCol.Add(objField)

                End Select

            ElseIf objTable.Rows(0).Item("FieldType") <> "Image" Then
                With objField
                    .FieldName = objTable.Rows(0).Item("Name")
                    .FieldText = objTable.Rows(0).Item("Text")
                    .FieldTop = objTable.Rows(0).Item("Top") '* scaley
                    .FieldLeft = objTable.Rows(0).Item("Left") '* scalex
                    .FieldWidth = objTable.Rows(0).Item("Width")
                    .FieldType = objTable.Rows(0).Item("FieldType")
                    .FieldHeight = objTable.Rows(0).Item("Height")

                    .FontName = objTable.Rows(0).Item("FontName")

                    .FontSize = Val(objTable.Rows(0).Item("FontSize"))
                    .FontStyle = objTable.Rows(0).Item("FontStyle")
                End With
                Select Case Section
                    Case "RHF"
                        ReportHeaderCol.Add(objField)
                    Case "PHF"
                        PageHeaderCol.Add(objField)
                    Case "PFF"
                        PageFooterCol.Add(objField)
                    Case "RFF"
                        ReportFooterCol.Add(objField)
                End Select

            Else
                obj = New ClsRxReportDictionary
                With objField
                    .FieldName = objTable.Rows(0).Item("Name")
                    .FieldText = objTable.Rows(0).Item("Text")
                    .FieldTop = objTable.Rows(0).Item("Top") '* scaley
                    .FieldLeft = objTable.Rows(0).Item("Left") '* scalex
                    .FieldWidth = objTable.Rows(0).Item("Width")
                    .FieldType = objTable.Rows(0).Item("FieldType")
                    .FieldHeight = objTable.Rows(0).Item("Height")
                End With

                If objField.FieldText = "ClinicLogo" Then
                    Dim objdatatable As DataTable
                    objdatatable = obj.GetClinicLogo()
                    Dim content As Byte() = CType(objdatatable.Rows(0).Item("ClinicLogo"), Byte())
                    Dim stream As MemoryStream = New MemoryStream(content)
                    objField.FieldImage = Image.FromStream(stream)

                ElseIf objField.FieldText = "ProviderSignature" Then
                    Dim objdatatable As DataTable
                    objdatatable = obj.GetProviderSign()
                    Dim content As Byte() = CType(objdatatable.Rows(0).Item("ProviderSignature"), Byte())
                    Dim stream As MemoryStream = New MemoryStream(content)
                    objField.FieldImage = Image.FromStream(stream)
                End If
                Select Case Section
                    Case "RHF"
                        ReportHeaderCol.Add(objField)
                    Case "PHF"
                        PageHeaderCol.Add(objField)
                    Case "PFF"
                        PageFooterCol.Add(objField)
                    Case "RFF"
                        ReportFooterCol.Add(objField)
                End Select
            End If

        End If


    End Function

    Private Function GetData(ByVal strfield As String, Optional ByRef blnstatus As Boolean = False) As String
        Dim strQuery As String
        Dim objconn As SqlConnection = Nothing
        Dim objcmd As New SqlCommand
        Dim oReader As SqlDataReader
        Try

            Dim strconn As String
            strconn = gloPMAdmin.mdlGeneral.GetConnectionString()
            objconn = New SqlConnection(strconn)
            objcmd.Connection = objconn


            'sarika 23rd oct 07
            'code commented
            '            strQuery = "select " & strfield & " from rxReport"
            'code added
            strQuery = "select " & strfield & " from rxReport1"
            '-------------------------

            objcmd.CommandText = strQuery
            objconn.Open()
            oReader = objcmd.ExecuteReader
            If Not IsDBNull(oReader) Then
                If oReader.HasRows Then
                    blnstatus = True
                    oReader.Read()
                    Return oReader(0)
                Else
                    blnstatus = False
                    'MsgBox("You can not preview the report as there are no Prescriptions ", MsgBoxStyle.ApplicationModal + MsgBoxStyle.Information, gstrMessageBoxCaption)
                End If
            Else
                blnstatus = False
                'MsgBox("You can not preview the report as there are no Prescriptions ", MsgBoxStyle.ApplicationModal + MsgBoxStyle.Information, gstrMessageBoxCaption)
            End If

            Return strQuery
        Catch err As Exception
            If objconn.State = ConnectionState.Open Then
                objconn.Close()
            End If
            blnstatus = False
            Return ""
            'MsgBox("You can not preview the report as ther are no Prescriptions ", MsgBoxStyle.ApplicationModal + MsgBoxStyle.Information, gstrMessageBoxCaption)
        Finally
            If objconn.State = ConnectionState.Open Then
                objconn.Close()
            End If

        End Try

    End Function

    Private Function generateXML() As Boolean
        objReport = New Report
        Dim objfield As Field

        objReport.ReportHeader.SectionHeight = CalRelHeightPosition(pnlReportHeader.Height)
        objReport.ReportHeader.SectionWidth = CalRelWidthPosition(pnlReportHeader.Width)

        objReport.PageHeader.SectionHeight = CalRelHeightPosition(pnlPageHeader.Height)
        objReport.PageHeader.SectionWidth = CalRelWidthPosition(pnlPageHeader.Width)

        objReport.Details.SectionHeight = CalRelHeightPosition(pnlDetails.Height)
        objReport.Details.SectionWidth = CalRelWidthPosition(pnlDetails.Width)

        objReport.PageFooter.SectionHeight = CalRelHeightPosition(pnlPageFooter.Height)
        objReport.PageFooter.SectionWidth = CalRelWidthPosition(pnlPageFooter.Width)

        objReport.ReportFooter.SectionHeight = CalRelHeightPosition(pnlReportFooter.Height)
        objReport.ReportFooter.SectionWidth = CalRelWidthPosition(pnlReportFooter.Width)

        'Add controls to the temporary form
        Dim objcontrol As Control
        Dim i As Int16 = 0
        For Each objcontrol In Me.Controls
            If TypeOf objcontrol Is Label Then
                If Not objcontrol.Tag Is Nothing Then
                    If objcontrol.Tag.Substring(0, 1) <> "3" Then
                        ' Code Added by Ravikiran
                        objfield = New Field
                        With objfield
                            .FieldForeColor = objcontrol.ForeColor
                            .FieldText = objcontrol.Text
                            .FieldName = objcontrol.Tag
                            .FieldWidth = objcontrol.Width
                            .FontName = objcontrol.Font.FontFamily.Name.ToString
                            .FontSize = objcontrol.Font.Size
                            .FontStyle = objcontrol.Font.Style
                            .FieldHeight = objcontrol.Height
                            .FieldType = "Parameter"
                        End With

                        'sarika 26th june 07
                        'Select Case objcontrol.Tag.Substring(0, 1)
                        Select Case objcontrol.Tag.Substring(0, 1).ToString()
                            '----
                            Case "1"
                                objfield.FieldTop = (objcontrol.Top - tblDesigner.Height) * 100 / pnlReportHeader.Height
                                objfield.FieldLeft = (objcontrol.Left - pnlCenter.Left) * 100 / pnlReportHeader.Width
                                objReport.ReportHeader.Fields.Add(objfield)
                            Case "2"
                                objfield.FieldTop = (objcontrol.Top - (pnlPageHeader.Top + tblDesigner.Height)) * 100 / pnlPageHeader.Height
                                objfield.FieldLeft = (objcontrol.Left - pnlCenter.Left) * 100 / pnlPageHeader.Width
                                objReport.PageHeader.Fields.Add(objfield)
                            Case "4"
                                objfield.FieldTop = (objcontrol.Top - (pnlPageFooter.Top + tblDesigner.Height)) * 100 / pnlPageFooter.Height
                                objfield.FieldLeft = (objcontrol.Left - pnlCenter.Left) * 100 / pnlPageFooter.Width
                                objReport.PageFooter.Fields.Add(objfield)
                            Case "5"
                                objfield.FieldTop = (objcontrol.Top - (pnlReportFooter.Top + tblDesigner.Height)) * 100 / pnlReportFooter.Height
                                objfield.FieldLeft = (objcontrol.Left - pnlCenter.Left) * 100 / pnlReportFooter.Width
                                objReport.ReportFooter.Fields.Add(objfield)

                        End Select
                        objfield = Nothing
                    Else
                        objfield = New Field
                        With objfield
                            .FieldForeColor = objcontrol.ForeColor
                            .FieldText = objcontrol.Text
                            .FieldTop = (objcontrol.Top - (tblDesigner.Height + pnlDetails.Top)) * 100 / pnlDetails.Height
                            .FieldLeft = (objcontrol.Left - pnlCenter.Left) * 100 / pnlDetails.Width
                            .FieldName = objcontrol.Tag
                            .FieldWidth = objcontrol.Width
                            .FontName = objcontrol.Font.FontFamily.Name.ToString
                            .FontSize = objcontrol.Font.Size
                            .FontStyle = objcontrol.Font.Style
                            .FieldHeight = objcontrol.Height
                            .FieldType = "Parameter"
                        End With
                        objReport.Details.Fields.Add(objfield)
                        objfield = Nothing

                    End If
                End If

            ElseIf TypeOf objcontrol Is TextBox Then
                If objcontrol.Tag.Substring(1, 1) <> "-" Then

                    objfield = New Field
                    With objfield
                        .FieldForeColor = objcontrol.ForeColor
                        ' .FieldTop = objcontrol.Top
                        .FieldText = objcontrol.Text
                        '   .FieldLeft = objcontrol.Left
                        .FieldName = objcontrol.Tag
                        .FieldWidth = objcontrol.Width
                        .FontName = objcontrol.Font.FontFamily.Name.ToString
                        .FontSize = objcontrol.Font.Size
                        .FontStyle = objcontrol.Font.Style
                        .FieldHeight = objcontrol.Height
                        .FieldType = "Caption"
                    End With

                    'sarika 26th june 07
                    'Select Case objcontrol.Tag.Substring(0, 1)
                    Select Case objcontrol.Tag.Substring(0, 1).ToString()
                        '---
                        Case "1"
                            objfield.FieldTop = (objcontrol.Top - tblDesigner.Height) * 100 / pnlReportHeader.Height
                            objfield.FieldLeft = (objcontrol.Left - pnlCenter.Left) * 100 / pnlReportHeader.Width
                            objReport.ReportHeader.Fields.Add(objfield)
                        Case "2"
                            objfield.FieldTop = (objcontrol.Top - (pnlPageHeader.Top + tblDesigner.Height)) * 100 / pnlPageHeader.Height
                            objfield.FieldLeft = (objcontrol.Left - pnlCenter.Left) * 100 / pnlPageHeader.Width
                            objReport.PageHeader.Fields.Add(objfield)
                        Case "4"
                            objfield.FieldTop = (objcontrol.Top - (pnlPageFooter.Top + tblDesigner.Height)) * 100 / pnlPageFooter.Height
                            objfield.FieldLeft = (objcontrol.Left - pnlCenter.Left) * 100 / pnlPageFooter.Width
                            objReport.PageFooter.Fields.Add(objfield)
                        Case "5"
                            objfield.FieldTop = (objcontrol.Top - (pnlReportFooter.Top + tblDesigner.Height)) * 100 / pnlReportFooter.Height
                            objfield.FieldLeft = (objcontrol.Left - pnlCenter.Left) * 100 / pnlReportFooter.Width
                            objReport.ReportFooter.Fields.Add(objfield)
                    End Select
                Else

                    objfield = New Field
                    With objfield
                        .FieldForeColor = objcontrol.ForeColor
                        Dim strdisclaimer As New System.Text.StringBuilder
                        strdisclaimer.Append(objcontrol.Text)
                        '                        .FieldText = objcontrol.Text
                        .FieldText = strdisclaimer.ToString
                        .FieldName = objcontrol.Tag
                        .FieldWidth = objcontrol.Width
                        .FontName = objcontrol.Font.FontFamily.Name.ToString
                        .FontSize = objcontrol.Font.Size
                        .FontStyle = objcontrol.Font.Style
                        .FieldHeight = objcontrol.Height
                        .FieldType = "Caption"
                    End With

                    'sarika 26th june 07
                    'Select Case objcontrol.Tag.Substring(0, 1)
                    Select Case objcontrol.Tag.Substring(0, 1).ToString()
                        '-------
                        Case "1"
                            objfield.FieldTop = (objcontrol.Top - tblDesigner.Height) * 100 / pnlReportHeader.Height
                            objfield.FieldLeft = (objcontrol.Left - pnlCenter.Left) * 100 / pnlReportHeader.Width
                            objReport.ReportHeader.Fields.Add(objfield)
                        Case "2"
                            objfield.FieldTop = (objcontrol.Top - (pnlPageHeader.Top + tblDesigner.Height)) * 100 / pnlPageHeader.Height
                            objfield.FieldLeft = (objcontrol.Left - pnlCenter.Left) * 100 / pnlPageHeader.Width
                            objReport.PageHeader.Fields.Add(objfield)
                        Case "4"
                            objfield.FieldTop = (objcontrol.Top - (pnlPageFooter.Top + tblDesigner.Height)) * 100 / pnlPageFooter.Height
                            objfield.FieldLeft = (objcontrol.Left - pnlCenter.Left) * 100 / pnlPageFooter.Width
                            objReport.PageFooter.Fields.Add(objfield)
                        Case "5"
                            objfield.FieldTop = (objcontrol.Top - (pnlReportFooter.Top + tblDesigner.Height)) * 100 / pnlReportFooter.Height
                            objfield.FieldLeft = (objcontrol.Left - pnlCenter.Left) * 100 / pnlReportFooter.Width
                            objReport.ReportFooter.Fields.Add(objfield)
                    End Select

                End If

            ElseIf TypeOf objcontrol Is PictureBox Then

                objfield = New Field
                With objfield
                    .FieldText = objcontrol.Text
                    .FieldName = objcontrol.Tag
                    .FieldWidth = objcontrol.Width
                    .FontSize = objcontrol.Font.Size
                    .FieldHeight = objcontrol.Height
                    .FieldType = "Image"
                    .SizeMode = PictureBoxSizeMode.Normal
                End With

                'sarika 26th june 07
                'Select Case objcontrol.Tag.Substring(0, 1)
                Select Case objcontrol.Tag.Substring(0, 1).ToString()
                    '----
                    Case "1"
                        objfield.FieldTop = (objcontrol.Top - tblDesigner.Height) * 100 / pnlReportHeader.Height
                        objfield.FieldLeft = (objcontrol.Left - pnlCenter.Left) * 100 / pnlReportHeader.Width
                        objReport.ReportHeader.Fields.Add(objfield)
                    Case "2"
                        objfield.FieldTop = (objcontrol.Top - (pnlPageHeader.Top + tblDesigner.Height)) * 100 / pnlPageHeader.Height
                        objfield.FieldLeft = (objcontrol.Left - pnlCenter.Left) * 100 / pnlPageHeader.Width
                        objReport.PageHeader.Fields.Add(objfield)


                    Case "4"
                        objfield.FieldTop = (objcontrol.Top - (pnlPageFooter.Top + tblDesigner.Height)) * 100 / pnlPageFooter.Height
                        objfield.FieldLeft = (objcontrol.Left - pnlCenter.Left) * 100 / pnlPageFooter.Width
                        objReport.PageFooter.Fields.Add(objfield)
                    Case "5"
                        objfield.FieldTop = (objcontrol.Top - (pnlReportFooter.Top + tblDesigner.Height)) * 100 / pnlReportFooter.Height
                        objfield.FieldLeft = (objcontrol.Left - pnlCenter.Left) * 100 / pnlReportFooter.Width
                        objReport.ReportFooter.Fields.Add(objfield)
                End Select
                objfield = Nothing

            End If
        Next
        If Not objReport Is Nothing Then
            WriteXML(objReport)
        End If

    End Function

    Private Function WriteXML(ByVal objReport As Report) As Boolean
        'sarika 26th june 07
        'Dim xmlwriter As XmlTextWriter
        Dim xmlwriter As XmlTextWriter = Nothing
        '--


        Try
            If Not objReport Is Nothing Then
                If File.Exists(strFileName) Then
                    File.Delete(strFileName)
                End If

                xmlwriter = New XmlTextWriter(strFileName, Nothing)

                xmlwriter.WriteStartDocument()
                xmlwriter.WriteStartElement("Report") 'Open the Main Parent Node 

                xmlwriter.WriteStartElement("ReportInformation")
                xmlwriter.WriteEndElement()

                xmlwriter.WriteStartElement("DataSource")
                xmlwriter.WriteEndElement()

                xmlwriter.WriteStartElement("Layout")

                xmlwriter.WriteEndElement()

                Dim cntFields As Int16
                xmlwriter.WriteStartElement("ReportHeader") ''Open the ReportHeader Node 
                '  xmlwriter.WriteStartAttribute("ReportHeader", "Height", objReport.ReportHeader.SectionHeight)
                ' xmlwriter.WriteEndAttribute()
                xmlwriter.WriteAttributeString("Height", objReport.ReportHeader.SectionHeight)
                xmlwriter.WriteAttributeString("Width", objReport.ReportHeader.SectionWidth)
                cntFields = objReport.ReportHeader.Fields.Count
                If cntFields <> 0 Then
                    For iCnt As Int16 = 1 To cntFields
                        xmlwriter.WriteStartElement("RHField" & iCnt) 'Open the Child Parent Node 
                        If Not objReport.ReportHeader.Fields.Item(iCnt).FieldName Is Nothing Then
                            xmlwriter.WriteElementString("Name", objReport.ReportHeader.Fields.Item(iCnt).FieldName) 'The Child 
                        End If
                        If Not objReport.ReportHeader.Fields.Item(iCnt).FieldText Is Nothing Then
                            xmlwriter.WriteElementString("Text", objReport.ReportHeader.Fields.Item(iCnt).FieldText) 'The Child 
                        End If
                        If Not objReport.ReportHeader.Fields.Item(iCnt).FieldTop = Nothing Then
                            xmlwriter.WriteElementString("Top", objReport.ReportHeader.Fields.Item(iCnt).FieldTop) 'The Child 
                        Else
                            xmlwriter.WriteElementString("Top", 0)
                        End If
                        If Not objReport.ReportHeader.Fields.Item(iCnt).FieldLeft = Nothing Then
                            xmlwriter.WriteElementString("Left", objReport.ReportHeader.Fields.Item(iCnt).FieldLeft) 'The Child 
                        Else
                            xmlwriter.WriteElementString("Left", 0)
                        End If
                        If Not objReport.ReportHeader.Fields.Item(iCnt).FieldWidth = Nothing Then
                            xmlwriter.WriteElementString("Width", objReport.ReportHeader.Fields.Item(iCnt).FieldWidth) 'The Child 
                        End If
                        If Not objReport.ReportHeader.Fields.Item(iCnt).FieldHeight = Nothing Then
                            xmlwriter.WriteElementString("Height", objReport.ReportHeader.Fields.Item(iCnt).FieldHeight) 'The Child 
                        End If

                        If Not objReport.ReportHeader.Fields.Item(iCnt).FontName Is Nothing Then
                            xmlwriter.WriteElementString("FontName", objReport.ReportHeader.Fields.Item(iCnt).FontName) 'The Child 
                        End If

                        If Not objReport.ReportHeader.Fields.Item(iCnt).FontStyle = Nothing Then
                            xmlwriter.WriteElementString("FontStyle", objReport.ReportHeader.Fields.Item(iCnt).FontStyle) 'The Child 
                        Else
                            xmlwriter.WriteElementString("FontStyle", FontStyle.Regular) 'The Child 
                        End If

                        If Not objReport.ReportHeader.Fields.Item(iCnt).FontSize = Nothing Then
                            xmlwriter.WriteElementString("FontSize", objReport.ReportHeader.Fields.Item(iCnt).FontSize) 'The Child 
                        End If

                        If Not objReport.ReportHeader.Fields.Item(iCnt).FieldType Is Nothing Then
                            xmlwriter.WriteElementString("FieldType", objReport.ReportHeader.Fields.Item(iCnt).FieldType) 'The Child 
                        End If

                        'There is no need to close the child tags because the method does it automatically. 
                        '   xmlwriter.WriteEndElement()
                        xmlwriter.WriteEndElement() 'Close the child Parent Node 
                    Next
                End If
                xmlwriter.WriteEndElement() 'Close the Report Header node

                cntFields = 0
                xmlwriter.WriteStartElement("PageHeader") ''open the Page Header Node
                'xmlwriter.WriteStartAttribute("PageHeader", "Height", objReport.PageHeader.SectionHeight)
                xmlwriter.WriteAttributeString("Height", objReport.PageHeader.SectionHeight)

                xmlwriter.WriteAttributeString("Width", objReport.PageHeader.SectionWidth)

                cntFields = objReport.PageHeader.Fields.Count
                If cntFields <> 0 Then

                    For iCnt As Int16 = 1 To cntFields

                        xmlwriter.WriteStartElement("PHField" & iCnt)  'Open the Child Parent Node 

                        If Not objReport.PageHeader.Fields.Item(iCnt).FieldName Is Nothing Then
                            xmlwriter.WriteElementString("Name", objReport.PageHeader.Fields.Item(iCnt).FieldName) 'The Child 
                        End If

                        If Not objReport.PageHeader.Fields.Item(iCnt).FieldText Is Nothing Then
                            xmlwriter.WriteElementString("Text", objReport.PageHeader.Fields.Item(iCnt).FieldText) 'The Child 
                        End If
                        If Not objReport.PageHeader.Fields.Item(iCnt).FieldTop = Nothing Then
                            xmlwriter.WriteElementString("Top", objReport.PageHeader.Fields.Item(iCnt).FieldTop) 'The Child 
                        End If
                        If Not objReport.PageHeader.Fields.Item(iCnt).FieldLeft = Nothing Then
                            xmlwriter.WriteElementString("Left", objReport.PageHeader.Fields.Item(iCnt).FieldLeft) 'The Child 
                        End If
                        If Not objReport.PageHeader.Fields.Item(iCnt).FieldWidth = Nothing Then
                            xmlwriter.WriteElementString("Width", objReport.PageHeader.Fields.Item(iCnt).FieldWidth) 'The Child 
                        End If
                        If Not objReport.PageHeader.Fields.Item(iCnt).FieldHeight = Nothing Then
                            xmlwriter.WriteElementString("Height", objReport.PageHeader.Fields.Item(iCnt).FieldHeight) 'The Child 
                        End If

                        If Not objReport.PageHeader.Fields.Item(iCnt).FontName Is Nothing Then
                            xmlwriter.WriteElementString("FontName", objReport.PageHeader.Fields.Item(iCnt).FontName) 'The Child 
                        End If

                        If Not objReport.PageHeader.Fields.Item(iCnt).FontStyle = Nothing Then
                            xmlwriter.WriteElementString("FontStyle", objReport.PageHeader.Fields.Item(iCnt).FontStyle) 'The Child 
                        Else
                            xmlwriter.WriteElementString("FontStyle", FontStyle.Regular) 'The Child 
                        End If

                        If Not objReport.PageHeader.Fields.Item(iCnt).FontSize = Nothing Then
                            xmlwriter.WriteElementString("FontSize", objReport.PageHeader.Fields.Item(iCnt).FontSize) 'The Child 
                        End If

                        If Not objReport.PageHeader.Fields.Item(iCnt).FieldType Is Nothing Then
                            xmlwriter.WriteElementString("FieldType", objReport.PageHeader.Fields.Item(iCnt).FieldType) 'The Child 
                        End If

                        'There is no need to close the child tags because the method does it automatically. 

                        xmlwriter.WriteEndElement() 'Close the child Parent Node 
                    Next
                End If
                'xmlwriter.WriteEndElement()
                xmlwriter.WriteEndElement() ''Close the Page Header Node 

                cntFields = 0
                xmlwriter.WriteStartElement("Details") ' Open the Details node
                xmlwriter.WriteAttributeString("Height", objReport.Details.SectionHeight)
                'xmlwriter.WriteEndAttribute()
                xmlwriter.WriteAttributeString("Width", objReport.Details.SectionWidth)
                'xmlwriter.WriteEndAttribute()
                cntFields = objReport.Details.Fields.Count
                If cntFields <> 0 Then
                    For iCnt As Int16 = 1 To cntFields

                        xmlwriter.WriteStartElement("DTField" & iCnt) 'Open the Child Parent Node 

                        If Not objReport.Details.Fields.Item(iCnt).FieldName Is Nothing Then
                            xmlwriter.WriteElementString("Name", objReport.Details.Fields.Item(iCnt).FieldName) 'The Child 
                        End If
                        If Not objReport.Details.Fields.Item(iCnt).FieldText Is Nothing Then
                            xmlwriter.WriteElementString("Text", objReport.Details.Fields.Item(iCnt).FieldText) 'The Child 
                        End If
                        If Not objReport.Details.Fields.Item(iCnt).FieldTop = Nothing Then
                            xmlwriter.WriteElementString("Top", objReport.Details.Fields.Item(iCnt).FieldTop) 'The Child 
                        End If
                        If Not objReport.Details.Fields.Item(iCnt).FieldLeft = Nothing Then
                            xmlwriter.WriteElementString("Left", objReport.Details.Fields.Item(iCnt).FieldLeft) 'The Child 
                        End If
                        If Not objReport.Details.Fields.Item(iCnt).FieldWidth = Nothing Then
                            xmlwriter.WriteElementString("Width", objReport.Details.Fields.Item(iCnt).FieldWidth) 'The Child 
                        End If
                        If Not objReport.Details.Fields.Item(iCnt).FieldHeight = Nothing Then
                            xmlwriter.WriteElementString("Height", objReport.Details.Fields.Item(iCnt).FieldHeight) 'The Child 
                        End If

                        If Not objReport.Details.Fields.Item(iCnt).FontName Is Nothing Then
                            xmlwriter.WriteElementString("FontName", objReport.Details.Fields.Item(iCnt).FontName) 'The Child 
                        End If

                        If Not objReport.Details.Fields.Item(iCnt).FontStyle = Nothing Then
                            xmlwriter.WriteElementString("FontStyle", objReport.Details.Fields.Item(iCnt).FontStyle) 'The Child 
                        Else
                            xmlwriter.WriteElementString("FontStyle", FontStyle.Regular) 'The Child 
                        End If

                        If Not objReport.Details.Fields.Item(iCnt).FontSize = Nothing Then
                            xmlwriter.WriteElementString("FontSize", objReport.Details.Fields.Item(iCnt).FontSize) 'The Child 
                        End If

                        If Not objReport.Details.Fields.Item(iCnt).FieldType Is Nothing Then
                            xmlwriter.WriteElementString("FieldType", objReport.Details.Fields.Item(iCnt).FieldType) 'The Child 
                        End If

                        'There is no need to close the child tags because the method does it automatically. 

                        xmlwriter.WriteEndElement() 'Close the child Parent Node 
                    Next
                End If
                ' xmlwriter.WriteEndElement()
                xmlwriter.WriteEndElement() 'close the Details Section Node

                cntFields = 0
                xmlwriter.WriteStartElement("PageFooter") ''Open the PageFooter node
                xmlwriter.WriteAttributeString("Height", objReport.PageFooter.SectionHeight)
                'xmlwriter.WriteEndAttribute()
                xmlwriter.WriteAttributeString("Width", objReport.PageFooter.SectionWidth)
                'xmlwriter.WriteEndAttribute()
                cntFields = objReport.PageFooter.Fields.Count
                If cntFields <> 0 Then
                    For iCnt As Int16 = 1 To cntFields

                        xmlwriter.WriteStartElement("PFField" & iCnt) 'Open the Child Parent Node 

                        If Not objReport.PageFooter.Fields.Item(iCnt).FieldName Is Nothing Then
                            xmlwriter.WriteElementString("Name", objReport.PageFooter.Fields.Item(iCnt).FieldName) 'The Child 
                        End If
                        If Not objReport.PageFooter.Fields.Item(iCnt).FieldText Is Nothing Then
                            xmlwriter.WriteElementString("Text", objReport.PageFooter.Fields.Item(iCnt).FieldText) 'The Child 
                        End If
                        If Not objReport.PageFooter.Fields.Item(iCnt).FieldTop = Nothing Then
                            xmlwriter.WriteElementString("Top", objReport.PageFooter.Fields.Item(iCnt).FieldTop) 'The Child 
                        End If
                        If Not objReport.PageFooter.Fields.Item(iCnt).FieldLeft = Nothing Then
                            xmlwriter.WriteElementString("Left", objReport.PageFooter.Fields.Item(iCnt).FieldLeft) 'The Child 
                        End If
                        If Not objReport.PageFooter.Fields.Item(iCnt).FieldWidth = Nothing Then
                            xmlwriter.WriteElementString("Width", objReport.PageFooter.Fields.Item(iCnt).FieldWidth) 'The Child 
                        End If
                        If Not objReport.PageFooter.Fields.Item(iCnt).FieldHeight = Nothing Then
                            xmlwriter.WriteElementString("Height", objReport.PageFooter.Fields.Item(iCnt).FieldHeight) 'The Child 
                        End If

                        If Not objReport.PageFooter.Fields.Item(iCnt).FontName Is Nothing Then
                            xmlwriter.WriteElementString("FontName", objReport.PageFooter.Fields.Item(iCnt).FontName) 'The Child 
                        End If

                        If Not objReport.PageFooter.Fields.Item(iCnt).FontStyle = Nothing Then
                            xmlwriter.WriteElementString("FontStyle", objReport.PageFooter.Fields.Item(iCnt).FontStyle) 'The Child 
                        Else
                            xmlwriter.WriteElementString("FontStyle", FontStyle.Regular) 'The Child 
                        End If

                        If Not objReport.PageFooter.Fields.Item(iCnt).FontSize = Nothing Then
                            xmlwriter.WriteElementString("FontSize", objReport.PageFooter.Fields.Item(iCnt).FontSize) 'The Child 
                        End If


                        If Not objReport.PageFooter.Fields.Item(iCnt).FieldType Is Nothing Then
                            xmlwriter.WriteElementString("FieldType", objReport.PageFooter.Fields.Item(iCnt).FieldType) 'The Child 
                        End If

                        'There is no need to close the child tags because the method does it automatically. 

                        xmlwriter.WriteEndElement() 'Close the child Parent Node 
                    Next
                End If
                ' xmlwriter.WriteEndElement()
                xmlwriter.WriteEndElement() '' close the PageFooter Node

                cntFields = 0
                xmlwriter.WriteStartElement("ReportFooter") ''Open the Report Footer Node
                xmlwriter.WriteAttributeString("Height", objReport.ReportFooter.SectionHeight)
                'xmlwriter.WriteEndAttribute()
                xmlwriter.WriteAttributeString("Width", objReport.ReportFooter.SectionWidth)
                'xmlwriter.WriteEndAttribute()
                cntFields = objReport.ReportFooter.Fields.Count
                If cntFields <> 0 Then
                    For iCnt As Int16 = 1 To cntFields

                        xmlwriter.WriteStartElement("RFField" & iCnt) 'Open the Child Parent Node 

                        If Not objReport.ReportFooter.Fields.Item(iCnt).FieldName Is Nothing Then
                            xmlwriter.WriteElementString("Name", objReport.ReportFooter.Fields.Item(iCnt).FieldName) 'The Child 
                        End If
                        If Not objReport.ReportFooter.Fields.Item(iCnt).FieldText Is Nothing Then
                            xmlwriter.WriteElementString("Text", objReport.ReportFooter.Fields.Item(iCnt).FieldText) 'The Child 
                        End If
                        If Not objReport.ReportFooter.Fields.Item(iCnt).FieldTop = Nothing Then
                            xmlwriter.WriteElementString("Top", objReport.ReportFooter.Fields.Item(iCnt).FieldTop) 'The Child 
                        End If
                        If Not objReport.ReportFooter.Fields.Item(iCnt).FieldLeft = Nothing Then
                            xmlwriter.WriteElementString("Left", objReport.ReportFooter.Fields.Item(iCnt).FieldLeft) 'The Child 
                        End If
                        If Not objReport.ReportFooter.Fields.Item(iCnt).FieldWidth = Nothing Then
                            xmlwriter.WriteElementString("Width", objReport.ReportFooter.Fields.Item(iCnt).FieldWidth) 'The Child 
                        End If
                        If Not objReport.ReportFooter.Fields.Item(iCnt).FieldHeight = Nothing Then
                            xmlwriter.WriteElementString("Height", objReport.ReportFooter.Fields.Item(iCnt).FieldHeight) 'The Child 
                        End If

                        If Not objReport.ReportFooter.Fields.Item(iCnt).FontName Is Nothing Then
                            xmlwriter.WriteElementString("FontName", objReport.ReportFooter.Fields.Item(iCnt).FontName) 'The Child 
                        End If

                        If Not objReport.ReportFooter.Fields.Item(iCnt).FontStyle = Nothing Then
                            xmlwriter.WriteElementString("FontStyle", objReport.ReportFooter.Fields.Item(iCnt).FontStyle) 'The Child 
                        Else
                            xmlwriter.WriteElementString("FontStyle", FontStyle.Regular) 'The Child 
                        End If

                        If Not objReport.ReportFooter.Fields.Item(iCnt).FontSize = Nothing Then
                            xmlwriter.WriteElementString("FontSize", objReport.ReportFooter.Fields.Item(iCnt).FontSize) 'The Child 
                        End If

                        If Not objReport.ReportFooter.Fields.Item(iCnt).FieldType Is Nothing Then
                            xmlwriter.WriteElementString("FieldType", objReport.ReportFooter.Fields.Item(iCnt).FieldType) 'The Child 
                        End If
                        'There is no need to close the child tags because the method does it automatically. 

                        xmlwriter.WriteEndElement() 'Close the child Parent Node 
                    Next
                End If
                ' xmlwriter.WriteEndElement()
                xmlwriter.WriteEndElement() 'Close the Report footer node

                xmlwriter.WriteEndElement() 'close the Report Node

                xmlwriter.WriteEndDocument() ' Close the document
                '' MsgBox("Xml File Generated")

            End If
        Catch err As XmlException
            MessageBox.Show(err.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            xmlwriter.Close() 'Close the file 
        End Try

    End Function

    Private Function CalRelWidthPosition(ByVal PosValue As Int16) As Single

        '  Dim nWidth As Integer = PD1.PrinterSettings.DefaultPageSettings.PaperSize.Width - (PD1.PrinterSettings.DefaultPageSettings.Margins.Left + PD1.PrinterSettings.DefaultPageSettings.Margins.Right)
        Dim relValue As Single = (PosValue * 100) / pnlCenter.Width
        Return relValue

    End Function

    Private Function CalRelHeightPosition(ByVal PosValue As Int16) As Single

        ' Dim nHeight As Integer = PD1.PrinterSettings.DefaultPageSettings.PaperSize.Height - (PD1.PrinterSettings.DefaultPageSettings.Margins.Top + PD1.PrinterSettings.DefaultPageSettings.Margins.Bottom)

        Dim relValue As Single = (PosValue * 100) / pnlCenter.Height
        Return relValue

    End Function

    Private Sub trReportExplorer_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trReportExplorer.DoubleClick
        Try
            If Not IsNothing(trReportExplorer.SelectedNode) Then
                If trReportExplorer.SelectedNode.Text = "Selection Criteria" Then
                    Dim arrlist As New ArrayList

                    'Fill the arrlist with Patient and Provider_Mst fields
                    Dim objnode As TreeNode
                    For Each objnode In trDataDictionary.Nodes.Item(0).Nodes.Item(0).Nodes
                        arrlist.Add("patient." & objnode.Text)
                    Next
                    For Each objnode In trDataDictionary.Nodes.Item(0).Nodes.Item(1).Nodes
                        arrlist.Add("provider_mst." & objnode.Text)
                    Next
                    'Load the criteria form
                    Dim frm As New frmCriteria(arrlist)
                    frm.ShowDialog()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error Loading Selection Criteria", "Report Designer", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub mnuDeleteCriteria_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteCriteria.Click
        Try
            'Clear Selection Criteria
            ClsReportExplorer.SelectionCriteria = ""
            ClsReportExplorer.CriteriaField = ""
            ClsReportExplorer.CriteriaValue = ""
        Catch ex As Exception
            MessageBox.Show("Cannot Clear Selection Criteria", "Report Designer", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub trReportExplorer_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trReportExplorer.MouseDown
        If e.Button = MouseButtons.Right Then
            If ClsReportExplorer.SelectionCriteria <> "" Then
                Dim trvnode As TreeNode
                trvnode = CType(trReportExplorer.GetNodeAt(e.X, e.Y), TreeNode)
                'Assign ContextMenu to "Selection Criteria" node 
                If Not IsNothing(trvnode) Then
                    If trvnode.Text = "Selection Criteria" Then
                        trReportExplorer.ContextMenu = CntExplorer
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub RefreshReport()

        'Clear the Report Sections
        Dim i As Int16
        Dim objcontrol As Control
        i = Me.Controls.Count - 1
        While i >= 0
            objcontrol = Me.Controls.Item(i)
            If TypeOf objcontrol Is Label Then
                If Not objcontrol.Tag Is Nothing Then
                    Me.Controls.Remove(objcontrol)
                End If
            ElseIf TypeOf objcontrol Is TextBox Then
                Me.Controls.Remove(objcontrol)
            ElseIf TypeOf objcontrol Is PictureBox Then
                Me.Controls.Remove(objcontrol)
            End If
            i = i - 1
        End While


        'For Each objcontrol In Me.Controls
        '    If TypeOf objcontrol Is Label Then
        '        If Not objcontrol.Tag Is Nothing Then
        '            Me.Controls.Remove(objcontrol)
        '        End If
        '        'ElseIf TypeOf objcontrol Is TextBox Then
        '        '    Me.Controls.Remove(objcontrol)
        '    End If
        'Next
        'Clear the Sections treeview
        trSections.Nodes.Item(0).Nodes.Item(0).Nodes.Clear()
        trSections.Nodes.Item(0).Nodes.Item(1).Nodes.Clear()
        trSections.Nodes.Item(0).Nodes.Item(2).Nodes.Clear()
        trSections.Nodes.Item(0).Nodes.Item(3).Nodes.Clear()
        trSections.Nodes.Item(0).Nodes.Item(4).Nodes.Clear()

        'Clear the selections Criteria
        ClsReportExplorer.SelectionCriteria = ""
        ClsReportExplorer.CriteriaValue = ""
        ClsReportExplorer.CriteriaField = ""
    End Sub
    Private Sub mnuProperties_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuProperties.Click
        prpProperties.SelectedObject = selectedcontrol
    End Sub
    Private Sub InitialiseCustomTextBox(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlReportHeader.MouseDown, pnlPageHeader.MouseDown, pnlPageFooter.MouseDown, pnlReportFooter.MouseDown
        If e.Button = MouseButtons.Right Then
            If CType(sender, Panel).Name <> "pnlDetails" Then
                x = 0
                y = 0
                CType(sender, Panel).ContextMenu = CntFieldMenu
                CntFieldMenu.MenuItems.Item(0).Visible = False
                CntFieldMenu.MenuItems.Item(1).Visible = False
                CntFieldMenu.MenuItems.Item(2).Visible = True

                x = e.X + pnlCenterDesigner.Left + pnlCenter.Left
                'y = e.Y + pnlCenterDesigner.Top + pnlCenter.Top
                Select Case CType(sender, Panel).Name

                    Case "pnlReportHeader"
                        intTextBoxTag = "1" & "-"
                        y = e.Y + pnlCenterDesigner.Top + pnlCenter.Top

                    Case "pnlPageHeader"
                        intTextBoxTag = "2" & "-"
                        y = e.Y + pnlReportHeader.Height + pnlCenterDesigner.Top + pnlCenter.Top

                    Case "pnlDetails"
                        intTextBoxTag = "3" & "-"
                        y = e.Y + pnlReportHeader.Height + pnlPageHeader.Height + pnlCenterDesigner.Top + pnlCenter.Top

                    Case "pnlPageFooter"
                        intTextBoxTag = "4" & "-"
                        y = e.Y + pnlReportHeader.Height + pnlPageHeader.Height + pnlDetails.Height + pnlCenterDesigner.Top + pnlCenter.Top

                    Case "pnlReportFooter"
                        intTextBoxTag = "5" & "-"
                        y = e.Y + pnlReportHeader.Height + pnlPageHeader.Height + pnlDetails.Height + pnlPageFooter.Height + pnlCenterDesigner.Top + pnlCenter.Top
                End Select
            End If
        End If
    End Sub
    Private Sub mnuInsert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuInsert.Click
        Try
            Dim txtnew As New TextBox

            'Add event handlers for textbox added
            AddHandler txtnew.MouseDown, AddressOf objecttxt_MouseDown
            AddHandler txtnew.MouseUp, AddressOf objecttxt_MouseUp
            AddHandler txtnew.MouseMove, AddressOf objecttxt_MouseMove
            AddHandler txtnew.Click, AddressOf objecttxt_Click

            txtnew.Top = y
            txtnew.Left = x
            txtnew.Visible = True
            txtnew.BackColor = System.Drawing.Color.White
            txtnew.ForeColor = System.Drawing.Color.Black
            txtnew.BorderStyle = BorderStyle.FixedSingle
            txtnew.AutoSize = True

            'txtnew.Text = dropNode.Text.Substring(1, Len(dropNode.Text) - 1)
            'txtnew.ReadOnly = True
            txtnew.Tag = intTextBoxTag
            'txtnew.Tag = dropNode.Text
            txtnew.ContextMenu = CntFieldMenu
            'Append Section Id for every label and textbox added


            Me.Controls.Add(txtnew)
            txtnew.BringToFront()
        Catch ex As Exception
            MsgBox("error Inserting Textbox", MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly, gstrMessageBoxCaption)
        End Try

    End Sub
    Private Sub trDataDictionary_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trDataDictionary.MouseDown
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim trvNode As TreeNode
            trvNode = trDataDictionary.GetNodeAt(e.X, e.Y)

            If IsNothing(trvNode) = False Then
                trDataDictionary.SelectedNode = trvNode
                If trvNode.Text = "Prescription" Then
                    FillTreeView(True)
                    'ElseIf trvNode.Text = "Provider" Then
                    '    FillTreeView(False)
                End If
            End If
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, "Report Designer", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub trDataDictionary_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles trDataDictionary.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
        x = 0
        y = 0
    End Sub
    Private Sub trDataDictionary_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trDataDictionary.DragEnter
        e.Effect = DragDropEffects.Move
    End Sub
    '    Private Sub trDataDictionary_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trDataDictionary.DragDrop, pnlReportHeader.DragDrop, pnlPageHeader.DragDrop, pnlDetails.DragDrop, pnlPageFooter.DragDrop, pnlReportFooter.DragDrop
    Private Sub trDataDictionary_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trDataDictionary.DragDrop, pnlReportHeader.DragDrop, pnlPageHeader.DragDrop, pnlPageFooter.DragDrop, pnlReportFooter.DragDrop, pnlDetails.DragDrop
        Try
            Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)
            Dim inttop As Int32 = pnlCenter.Top
            If Not dropNode.TreeView Is trReportExplorer Then
                If dropNode.Tag <> "Image" Then
                    If dropNode.TreeView Is trDataDictionary Then

                        Dim lblnew As New Label
                        'Add event handlers for label added
                        AddHandler lblnew.MouseDown, AddressOf objectlbl_MouseDown
                        AddHandler lblnew.MouseUp, AddressOf objectlbl_MouseUp
                        AddHandler lblnew.MouseMove, AddressOf objectlbl_MouseMove
                        AddHandler lblnew.Click, AddressOf objectlbl_Click

                        lblnew.Visible = True
                        lblnew.BackColor = System.Drawing.Color.Gray
                        lblnew.ForeColor = System.Drawing.Color.White
                        lblnew.AutoSize = True
                        lblnew.Text = dropNode.Text
                        'lblnew.Top = y

                        If x <= pnlCenter.Location.X Then
                            lblnew.Left = x + lblnew.Width
                        Else
                            lblnew.Left = x
                        End If

                        'lblnew.Tag = dropNode.Text
                        lblnew.ContextMenu = CntFieldMenu

                        Select Case CType(sender, Panel).Name

                            Case "pnlReportHeader"
                                lblnew.Tag = "1" & lblnew.Tag & lblnew.Text

                            Case "pnlPageHeader"
                                lblnew.Tag = "2" & lblnew.Tag & lblnew.Text
                                If y > inttop + pnlPageHeader.Height Then
                                    y = (inttop + pnlPageHeader.Height) - lblnew.Height
                                ElseIf y < inttop + btnPageHeader.Height + btnPageHeader.Height Then
                                    y = inttop + btnPageHeader.Height + btnPageHeader.Height
                                End If
                            Case "pnlDetails"
                                If dropNode.Text <> "SIG" And dropNode.Text <> "Dispense" And dropNode.Text <> "Drug" And dropNode.Text <> "Refill" And dropNode.Text <> "May Substitute" And dropNode.Text <> "Notes" And dropNode.Text <> "Problems" Then
                                    MsgBox("You can only Add Drug/SIG/Dispense/Refill/May Substitute/Notes to the Details Section", MsgBoxStyle.ApplicationModal + MsgBoxStyle.Information, gstrMessageBoxCaption)
                                    Exit Sub
                                End If
                                If Not CheckforDuplication(dropNode.Text) Then
                                    MsgBox("Duplicate Fields not allowed in Details Section", MsgBoxStyle.ApplicationModal + MsgBoxStyle.Information, gstrMessageBoxCaption)
                                    Exit Sub
                                End If
                                lblnew.Tag = "3" & lblnew.Tag & lblnew.Text '& 
                                If y > inttop + pnlPageHeader.Height + pnlDetails.Height Then
                                    y = (inttop + pnlPageHeader.Height + pnlDetails.Height) - lblnew.Height
                                ElseIf y < (inttop + pnlPageHeader.Height) + btnDetails.Height + btnDetails.Height Then
                                    y = (inttop + pnlPageHeader.Height) + btnDetails.Height + btnDetails.Height
                                End If
                            Case "pnlPageFooter"
                                lblnew.Tag = "4" & lblnew.Tag & lblnew.Text
                                If y > inttop + pnlPageHeader.Height + pnlDetails.Height + pnlPageFooter.Height Then
                                    y = (inttop + pnlPageHeader.Height + pnlDetails.Height + pnlPageFooter.Height) - lblnew.Height
                                ElseIf y < (inttop + pnlPageHeader.Height + pnlDetails.Height) + btnPageFooter.Height + btnPageFooter.Height Then
                                    y = (inttop + pnlPageHeader.Height + pnlDetails.Height) + btnPageFooter.Height + btnPageFooter.Height
                                End If
                            Case "pnlReportFooter"
                                lblnew.Tag = "5" & lblnew.Tag & lblnew.Text
                        End Select
                        lblnew.Top = y
                        Me.Controls.Add(lblnew)
                        lblnew.BringToFront()

                        If CType(sender, Panel).Name <> "pnlDetails" Then
                            If (dropNode.Text = "SeniorProvider1") Or (dropNode.Text = "SeniorProvider2") Then

                            ElseIf dropNode.Text = "DisclaimerNotes" Then

                                Dim txtnew As New TextBox

                                'Add event handlers for textbox added
                                AddHandler txtnew.MouseDown, AddressOf objecttxt_MouseDown
                                AddHandler txtnew.MouseUp, AddressOf objecttxt_MouseUp
                                AddHandler txtnew.MouseMove, AddressOf objecttxt_MouseMove
                                AddHandler txtnew.Click, AddressOf objecttxt_Click

                                txtnew.Visible = True
                                txtnew.BackColor = System.Drawing.Color.White
                                txtnew.ForeColor = System.Drawing.Color.Black
                                txtnew.AutoSize = True
                                txtnew.Height = 50
                                txtnew.Width = 400
                                txtnew.Text = dropNode.Text
                                'txtnew.Text = dropNode.Text.Substring(1, Len(dropNode.Text) - 1)
                                txtnew.AcceptsReturn = True
                                txtnew.Multiline = True

                                txtnew.Top = y

                                If x <= pnlCenter.Location.X Then
                                    txtnew.Left = x + txtnew.Width
                                Else
                                    txtnew.Left = x
                                End If

                                txtnew.ContextMenu = CntFieldMenu
                                'Append Section Id for every label and textbox added

                                Select Case CType(sender, Panel).Name

                                    Case "pnlPageFooter"
                                        txtnew.Tag = "4" & txtnew.Tag & dropNode.Text
                                        Me.Controls.Add(txtnew)
                                        txtnew.BringToFront()
                                    Case "pnlReportFooter"
                                        txtnew.Tag = "5" & txtnew.Tag & dropNode.Text
                                        If y > (inttop + pnlPageHeader.Height + pnlDetails.Height + pnlPageFooter.Height) Then '- (CType(sender, TextBox).Height) Then
                                            y = (inttop + pnlPageHeader.Height + pnlDetails.Height + pnlPageFooter.Height) - txtnew.Height
                                        ElseIf y < (inttop + pnlPageHeader.Height + pnlDetails.Height) + btnPageFooter.Height + btnPageFooter.Height Then
                                            y = (inttop + pnlPageHeader.Height + pnlDetails.Height) + btnPageFooter.Height + btnPageFooter.Height
                                        End If
                                        txtnew.Top = y
                                        Me.Controls.Add(txtnew)
                                        txtnew.BringToFront()
                                End Select
                            Else

                                Dim txtnew As New TextBox

                                'Add event handlers for textbox added
                                AddHandler txtnew.MouseDown, AddressOf objecttxt_MouseDown
                                AddHandler txtnew.MouseUp, AddressOf objecttxt_MouseUp
                                AddHandler txtnew.MouseMove, AddressOf objecttxt_MouseMove
                                AddHandler txtnew.Click, AddressOf objecttxt_Click

                                txtnew.Visible = True
                                txtnew.BackColor = System.Drawing.Color.White
                                txtnew.ForeColor = System.Drawing.Color.Black
                                txtnew.AutoSize = True
                                txtnew.Text = dropNode.Text
                                'txtnew.Top = y
                                'txtnew.Left = x
                                If x <= pnlCenter.Location.X Then
                                    txtnew.Left = x + txtnew.Width
                                Else
                                    txtnew.Left = x
                                End If

                                txtnew.ContextMenu = CntFieldMenu
                                'Append Section Id for every label and textbox added

                                Select Case CType(sender, Panel).Name

                                    Case "pnlReportHeader"
                                        txtnew.Tag = "1" & txtnew.Tag & dropNode.Text
                                    Case "pnlPageHeader"
                                        txtnew.Tag = "2" & txtnew.Tag & dropNode.Text
                                        If y > (inttop + pnlPageHeader.Height) Then '- (CType(sender, TextBox).Height) Then
                                            y = (inttop + pnlPageHeader.Height) - txtnew.Height
                                        ElseIf y < (inttop + btnPageHeader.Height + btnPageHeader.Height) Then
                                            y = (inttop + btnPageHeader.Height + btnPageHeader.Height)
                                        End If
                                    Case "pnlDetails"
                                        txtnew.Tag = "3" & txtnew.Tag & dropNode.Text
                                        If y > (inttop + pnlPageHeader.Height + pnlDetails.Height) Then '- (CType(sender, TextBox).Height) Then
                                            y = (inttop + pnlPageHeader.Height + pnlDetails.Height) - txtnew.Height
                                        ElseIf y < (inttop + pnlPageHeader.Height) + btnDetails.Height + btnDetails.Height Then
                                            y = (inttop + pnlPageHeader.Height) + btnDetails.Height + btnDetails.Height
                                        End If
                                    Case "pnlPageFooter"
                                        txtnew.Tag = "4" & txtnew.Tag & dropNode.Text
                                        If y > (inttop + pnlPageHeader.Height + pnlDetails.Height + pnlPageFooter.Height) Then '- (CType(sender, TextBox).Height) Then
                                            y = (inttop + pnlPageHeader.Height + pnlDetails.Height + pnlPageFooter.Height) - txtnew.Height
                                        ElseIf y < (inttop + pnlPageHeader.Height + pnlDetails.Height) + btnPageFooter.Height + btnPageFooter.Height Then
                                            y = (inttop + pnlPageHeader.Height + pnlDetails.Height) + btnPageFooter.Height + btnPageFooter.Height
                                        End If
                                    Case "pnlReportFooter"
                                        txtnew.Tag = "5" & txtnew.Tag & dropNode.Text
                                End Select
                                txtnew.Top = y
                                Me.Controls.Add(txtnew)
                                txtnew.BringToFront()
                            End If
                        End If

                        'Fill Sections for every label and textbox added
                        Dim mynode As New TreeNode(dropNode.Text)
                        mynode.ImageIndex = 2
                        mynode.SelectedImageIndex = 2

                        Select Case CType(sender, Panel).Name


                            Case "pnlReportHeader"
                                trSections.Nodes.Item(0).Nodes.Item(0).Nodes.Add(mynode)
                                trSections.Nodes.Item(0).Nodes.Item(0).ExpandAll()
                            Case "pnlPageHeader"
                                trSections.Nodes.Item(0).Nodes.Item(1).Nodes.Add(mynode)
                                trSections.Nodes.Item(0).Nodes.Item(1).ExpandAll()
                            Case "pnlDetails"
                                trSections.Nodes.Item(0).Nodes.Item(2).Nodes.Add(mynode)
                                trSections.Nodes.Item(0).Nodes.Item(2).ExpandAll()
                            Case "pnlPageFooter"
                                trSections.Nodes.Item(0).Nodes.Item(3).Nodes.Add(mynode)
                                trSections.Nodes.Item(0).Nodes.Item(3).ExpandAll()
                            Case "pnlReportFooter"
                                trSections.Nodes.Item(0).Nodes.Item(4).Nodes.Add(mynode)
                                trSections.Nodes.Item(0).Nodes.Item(4).ExpandAll()
                        End Select

                    End If
                Else
                    If dropNode.TreeView Is trDataDictionary Then
                        If CType(sender, Panel).Name = "pnlDetails" Then
                            If dropNode.Text = "ProviderSignature" Then
                                MsgBox("You can only Add Provider Signature to Page Footer Section", MsgBoxStyle.ApplicationModal + MsgBoxStyle.Information, gstrMessageBoxCaption)
                            ElseIf dropNode.Text = "ClinicLogo" Then
                                MsgBox("You can only Add ClinicLogo to Page Header Section", MsgBoxStyle.ApplicationModal + MsgBoxStyle.Information, gstrMessageBoxCaption)
                            End If
                            Exit Sub
                        End If
                        Dim imgCtrl As New PictureBox
                        AddHandler imgCtrl.MouseDown, AddressOf objectimg_MouseDown
                        AddHandler imgCtrl.MouseUp, AddressOf objectimg_MouseUp
                        AddHandler imgCtrl.MouseMove, AddressOf objectimg_MouseMove
                        AddHandler imgCtrl.Click, AddressOf objectimg_Click

                        imgCtrl.Visible = True
                        imgCtrl.Text = dropNode.Text
                        imgCtrl.SizeMode = PictureBoxSizeMode.StretchImage
                        'imgCtrl.Top = y

                        If x <= pnlCenter.Location.X Then
                            imgCtrl.Left = x + imgCtrl.Width
                        Else
                            imgCtrl.Left = x
                        End If
                        'lblnew.Tag = dropNode.Text
                        imgCtrl.ContextMenu = CntFieldMenu
                        Select Case CType(sender, Panel).Name

                            Case "pnlReportHeader"
                                Exit Sub
                                'imgCtrl.Tag = "1" & imgCtrl.Tag & imgCtrl.Text
                                'Me.Controls.Add(imgCtrl)
                                'imgCtrl.BringToFront()
                            Case "pnlPageHeader"
                                If dropNode.Text = "ProviderSignature" Then
                                    MsgBox("You can only Add Provider Signature to Page Footer Section", MsgBoxStyle.ApplicationModal + MsgBoxStyle.Information, gstrMessageBoxCaption)
                                    Exit Sub
                                Else
                                    imgCtrl.Tag = "2" & imgCtrl.Tag & imgCtrl.Text
                                    If y > (inttop + pnlPageHeader.Height) - (imgCtrl.Height) Then
                                        y = (inttop + pnlPageHeader.Height)
                                    ElseIf y < (inttop + btnPageHeader.Height + btnPageHeader.Height) Then
                                        y = (inttop + btnPageHeader.Height + btnPageHeader.Height)
                                    End If
                                    imgCtrl.Top = y
                                    Me.Controls.Add(imgCtrl)
                                    imgCtrl.BringToFront()
                                End If
                                'Case "pnlDetails"
                                '    If dropNode.Text <> "ProviderSignature" Then
                                '        MsgBox("You can only Add ClinicLogo to PageHeader Section", MsgBoxStyle.ApplicationModal + MsgBoxStyle.Information, gstrMessageBoxCaption)
                                '        Exit Sub
                                '    ElseIf dropNode.Text <> "ClinicLogo" Then
                                '        MsgBox("You can only Add Provider Signature to PageFooter Section", MsgBoxStyle.ApplicationModal + MsgBoxStyle.Information, gstrMessageBoxCaption)
                                '        Exit Sub
                                '    End If

                            Case "pnlPageFooter"
                                If dropNode.Text = "ClinicLogo" Then
                                    MsgBox("You can only Add Clinic Logo to PageHeader Section", MsgBoxStyle.ApplicationModal + MsgBoxStyle.Information, gstrMessageBoxCaption)
                                    Exit Sub
                                Else
                                    imgCtrl.Tag = "4" & imgCtrl.Tag & imgCtrl.Text
                                    If y > (inttop + pnlPageHeader.Height + pnlDetails.Height + pnlPageFooter.Height) - (imgCtrl.Height) Then
                                        y = (inttop + pnlPageHeader.Height + pnlDetails.Height + pnlPageFooter.Height)
                                    ElseIf y < (inttop + pnlPageHeader.Height + pnlDetails.Height) + btnPageFooter.Height + btnPageFooter.Height Then
                                        y = (inttop + pnlPageHeader.Height + pnlDetails.Height) + btnPageFooter.Height + btnPageFooter.Height
                                    End If
                                    imgCtrl.Top = y
                                    Me.Controls.Add(imgCtrl)
                                    imgCtrl.BringToFront()
                                End If

                            Case "pnlReportFooter"
                                'imgCtrl.Tag = "5" & imgCtrl.Tag & imgCtrl.Text
                                'Me.Controls.Add(imgCtrl)
                                'imgCtrl.BringToFront()
                                Exit Sub
                        End Select

                    End If

                End If
            End If

        Catch ex As Exception
            MsgBox("Error Adding field to Report", MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly, gstrMessageBoxCaption)
        End Try

    End Sub
    Private Sub objectlbl_MouseDown( _
    ByVal sender As Object, _
    ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If e.Button = MouseButtons.Left Then
                blnMoving = True
                MouseDownX = e.X
                MouseDownY = e.Y
            ElseIf e.Button = MouseButtons.Right Then
                selectedcontrol = CType(sender, Label)
                CntFieldMenu.MenuItems.Item(0).Visible = True
                CntFieldMenu.MenuItems.Item(1).Visible = True
                CntFieldMenu.MenuItems.Item(2).Visible = False
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub objectlbl_MouseUp( _
    ByVal sender As Object, _
    ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If e.Button = MouseButtons.Left Then
                blnMoving = False
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub objectlbl_MouseMove( _
    ByVal sender As Object, _
    ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If blnMoving Then
                Dim temp As Point = New Point
                Dim x As Int32 = 0
                Dim y As Int32 = 0
                x = CType(sender, Label).Location.X + (e.X - MouseDownX)
                y = CType(sender, Label).Location.Y + (e.Y - MouseDownY)

                Dim strtag As String = CType(sender, Label).Tag
                Dim inttop As Int32 = pnlCenterTop.Height
                strtag = strtag.Substring(0, 1)
                'Make sure the X position does not exceed
                If x < pnlCenter.Location.X + splLeft.Width Then
                    Exit Sub
                ElseIf x > (pnlCenter.Location.X + pnlCenterDesigner.Location.X + pnlPageHeader.Width) - (splLeft.Width + splRight.Width + CType(sender, Label).Width) Then
                    Exit Sub
                End If
                Select Case strtag

                    'Container is Page Header section


                    Case "2"
                        'Make sure the Y position does not exceed the PageHeader section
                        If y > inttop + pnlPageHeader.Height Then
                            Exit Sub
                        ElseIf y < inttop + btnPageHeader.Height + btnPageHeader.Height Then
                            Exit Sub
                        End If

                        'Container is Details section
                    Case "3"
                        'Make sure the Y position does not exceed the Details section
                        If y > inttop + pnlPageHeader.Height + pnlDetails.Height Then
                            Exit Sub
                        ElseIf y < (inttop + pnlPageHeader.Height) + btnDetails.Height + btnDetails.Height Then
                            Exit Sub
                        End If
                        'Container is Page Footer section
                    Case "4"
                        'Make sure the Y position does not exceed the PageFooter section
                        If y > inttop + pnlPageHeader.Height + pnlDetails.Height + pnlPageFooter.Height Then
                            Exit Sub
                        ElseIf y < (inttop + pnlPageHeader.Height + pnlDetails.Height) + btnPageFooter.Height + btnPageFooter.Height Then
                            Exit Sub
                        End If
                End Select
                temp.X = x
                temp.Y = y
                CType(sender, Label).Location = temp
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub objectlbl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        prpProperties.SelectedObject = CType(sender, Label)
    End Sub
    Private Sub objectimg_MouseDown( _
       ByVal sender As Object, _
       ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If e.Button = MouseButtons.Left Then
                blnMoving = True
                MouseDownX = e.X
                MouseDownY = e.Y
            ElseIf e.Button = MouseButtons.Right Then
                selectedcontrol = CType(sender, PictureBox)
                CntFieldMenu.MenuItems.Item(0).Visible = True
                CntFieldMenu.MenuItems.Item(1).Visible = True
                CntFieldMenu.MenuItems.Item(2).Visible = False
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub objectimg_MouseUp( _
    ByVal sender As Object, _
    ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If e.Button = MouseButtons.Left Then
                blnMoving = False
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub objectimg_MouseMove( _
    ByVal sender As Object, _
    ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If blnMoving Then
                Dim x As Int32 = 0
                Dim y As Int32 = 0
                x = CType(sender, PictureBox).Location.X + (e.X - MouseDownX)
                y = CType(sender, PictureBox).Location.Y + (e.Y - MouseDownY)

                Dim strtag As String = CType(sender, PictureBox).Tag
                Dim inttop As Int32 = pnlCenterTop.Height
                strtag = strtag.Substring(0, 1)
                If x < pnlCenter.Location.X + +splLeft.Width Then
                    Exit Sub
                ElseIf x > (pnlCenter.Location.X + pnlCenterDesigner.Location.X + pnlPageHeader.Width) - (splLeft.Width + splRight.Width + CType(sender, PictureBox).Width) Then
                    Exit Sub
                End If
                Select Case strtag
                    'Container is Page Header section
                    Case "2"

                        If y > (inttop + pnlPageHeader.Height) - (CType(sender, PictureBox).Height) Then
                            Exit Sub
                        ElseIf y < (inttop + btnPageHeader.Height + btnPageHeader.Height) Then
                            Exit Sub
                        End If

                        'Container is Details section
                    Case "3"
                        If y > (inttop + pnlPageHeader.Height + pnlDetails.Height) - (CType(sender, PictureBox).Height) Then
                            Exit Sub
                        ElseIf y < (inttop + pnlPageHeader.Height) + btnDetails.Height + btnDetails.Height Then
                            Exit Sub
                        End If
                        'Container is Page Footer section
                    Case "4"
                        If y > (inttop + pnlPageHeader.Height + pnlDetails.Height + pnlPageFooter.Height) - (CType(sender, PictureBox).Height) Then
                            Exit Sub
                        ElseIf y < (inttop + pnlPageHeader.Height + pnlDetails.Height) + btnPageFooter.Height + btnPageFooter.Height Then
                            Exit Sub
                        End If
                End Select
                Dim temp As Point = New Point
                temp.X = CType(sender, PictureBox).Location.X + (e.X - MouseDownX)
                temp.Y = CType(sender, PictureBox).Location.Y + (e.Y - MouseDownY)
                CType(sender, PictureBox).Location = temp
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub objectimg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        prpProperties.SelectedObject = CType(sender, PictureBox)
    End Sub

    Private Sub objecttxt_MouseDown( _
        ByVal sender As Object, _
        ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If e.Button = MouseButtons.Left Then
                blnMoving = True
                MouseDownX = e.X
                MouseDownY = e.Y
            ElseIf e.Button = MouseButtons.Right Then
                selectedcontrol = CType(sender, TextBox)
                CntFieldMenu.MenuItems.Item(0).Visible = True
                CntFieldMenu.MenuItems.Item(1).Visible = True
                CntFieldMenu.MenuItems.Item(2).Visible = False
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub objecttxt_MouseUp( _
    ByVal sender As Object, _
    ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If e.Button = MouseButtons.Left Then
                blnMoving = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub objecttxt_MouseMove( _
    ByVal sender As Object, _
    ByVal e As System.Windows.Forms.MouseEventArgs)
        Try
            If blnMoving Then
                Dim x As Int32 = 0
                Dim y As Int32 = 0
                x = CType(sender, TextBox).Location.X + (e.X - MouseDownX)
                y = CType(sender, TextBox).Location.Y + (e.Y - MouseDownY)

                Dim strtag As String = CType(sender, TextBox).Tag
                Dim inttop As Int32 = pnlCenterTop.Height
                strtag = strtag.Substring(0, 1)

                'Make sure the X position does not exceed
                If x < pnlCenter.Location.X + splLeft.Width Then
                    Exit Sub
                ElseIf x > (pnlCenter.Location.X + pnlCenterDesigner.Location.X + pnlPageHeader.Width) - (splLeft.Width + splRight.Width + CType(sender, TextBox).Width) Then
                    Exit Sub
                End If
                Select Case strtag
                    'Container is Page Header section
                    Case "2"
                        'Make sure the Y position does not exceed the PageHeader section
                        If y > (inttop + pnlPageHeader.Height) Then '- (CType(sender, TextBox).Height) Then
                            Exit Sub
                        ElseIf y < (inttop + btnPageHeader.Height + btnPageHeader.Height) Then
                            Exit Sub
                        End If

                        'Container is Details section
                    Case "3"
                        'Make sure the Y position does not exceed the Details section
                        If y > (inttop + pnlPageHeader.Height + pnlDetails.Height) Then '- (CType(sender, TextBox).Height) Then
                            Exit Sub
                        ElseIf y < (inttop + pnlPageHeader.Height) + btnDetails.Height + btnDetails.Height Then
                            Exit Sub
                        End If
                        'Container is Page Footer section
                    Case "4"
                        'Make sure the Y position does not exceed the PageFooter section
                        If y > (inttop + pnlPageHeader.Height + pnlDetails.Height + pnlPageFooter.Height) Then '- (CType(sender, TextBox).Height) Then
                            Exit Sub
                        ElseIf y < (inttop + pnlPageHeader.Height + pnlDetails.Height) + btnPageFooter.Height + btnPageFooter.Height Then
                            Exit Sub
                        End If
                End Select
                Dim temp As Point = New Point
                temp.X = x
                temp.Y = y
                CType(sender, TextBox).Location = temp
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub objecttxt_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        prpProperties.SelectedObject = CType(sender, TextBox)
    End Sub
    Private Sub mnuDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDelete.Click
        Try
            Me.Controls.Remove(selectedcontrol)
            selectedcontrol = Nothing
            prpProperties.SelectedObject = Nothing
        Catch ex As Exception
            MsgBox("Error deleting selected Report Object", MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly, gstrMessageBoxCaption)
        End Try

    End Sub
    Private Sub PanelDragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles pnlReportHeader.DragOver, pnlPageHeader.DragOver, pnlDetails.DragOver, pnlPageFooter.DragOver, pnlReportFooter.DragOver
        e.Effect = DragDropEffects.Move
        x = 0
        y = 0
        x = e.X
        y = e.Y
    End Sub
    Private Sub trReportExplorer_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trReportExplorer.DragEnter
        e.Effect = DragDropEffects.Move
    End Sub
    Private Sub trReportExplorer_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trReportExplorer.DragDrop, pnlReportHeader.DragDrop, pnlPageHeader.DragDrop, pnlPageFooter.DragDrop, pnlReportFooter.DragDrop
        Try
            Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)
            If dropNode.TreeView Is trReportExplorer Then
                Dim lblnew As New Label

                'Add event handlers for label added at runtime
                AddHandler lblnew.MouseDown, AddressOf objectlbl_MouseDown
                AddHandler lblnew.MouseUp, AddressOf objectlbl_MouseUp
                AddHandler lblnew.MouseMove, AddressOf objectlbl_MouseMove
                AddHandler lblnew.Click, AddressOf objectlbl_Click


                lblnew.Visible = True
                lblnew.BackColor = System.Drawing.Color.Gray
                lblnew.ForeColor = System.Drawing.Color.White
                lblnew.AutoSize = True
                lblnew.Text = dropNode.Text

                If x - lblnew.Width > 0 Then
                    lblnew.Left = x - lblnew.Width
                Else
                    lblnew.Left = x
                End If
                Dim inttop As Int32 = pnlCenter.Top

                'Define SectionID and formulatype for every special field added
                Select Case CType(sender, Panel).Name
                    Case "pnlReportHeader"
                        If dropNode.Text = "Current Date" Then
                            lblnew.Tag = "16"
                        ElseIf dropNode.Text = "Current Time" Then
                            lblnew.Tag = "17"
                        ElseIf dropNode.Text = "File Author" Then
                            lblnew.Tag = "18"
                        ElseIf dropNode.Text = "Current Page" Then
                            lblnew.Tag = "19"
                        ElseIf dropNode.Text = "Record Count" Then
                            lblnew.Tag = "10"
                        End If
                    Case "pnlPageHeader"
                        If y > inttop + pnlPageHeader.Height Then
                            y = (inttop + pnlPageHeader.Height) - lblnew.Height
                        ElseIf y < inttop + btnPageHeader.Height + btnPageHeader.Height Then
                            y = inttop + btnPageHeader.Height + btnPageHeader.Height
                        End If

                        If dropNode.Text = "Current Date" Then
                            lblnew.Tag = "26"
                        ElseIf dropNode.Text = "Current Time" Then
                            lblnew.Tag = "27"
                        ElseIf dropNode.Text = "File Author" Then
                            lblnew.Tag = "28"
                        ElseIf dropNode.Text = "Current Page" Then
                            lblnew.Tag = "29"
                        ElseIf dropNode.Text = "Record Count" Then
                            lblnew.Tag = "20"
                        End If
                    Case "pnlPageFooter"
                        If y > inttop + pnlPageHeader.Height + pnlDetails.Height + pnlPageFooter.Height Then
                            Exit Sub
                        ElseIf y < (inttop + pnlPageHeader.Height + pnlDetails.Height) + btnPageFooter.Height + btnPageFooter.Height Then
                            Exit Sub
                        End If
                        If dropNode.Text = "Current Date" Then
                            lblnew.Tag = "46"
                        ElseIf dropNode.Text = "Current Time" Then
                            lblnew.Tag = "47"
                        ElseIf dropNode.Text = "File Author" Then
                            lblnew.Tag = "48"
                        ElseIf dropNode.Text = "Current Page" Then
                            lblnew.Tag = "49"
                        ElseIf dropNode.Text = "Record Count" Then
                            lblnew.Tag = "40"
                        End If
                    Case "pnlReportFooter"
                        If dropNode.Text = "Current Date" Then
                            lblnew.Tag = "56"
                        ElseIf dropNode.Text = "Current Time" Then
                            lblnew.Tag = "57"
                        ElseIf dropNode.Text = "File Author" Then
                            lblnew.Tag = "58"
                        ElseIf dropNode.Text = "Current Page" Then
                            lblnew.Tag = "59"
                        ElseIf dropNode.Text = "Record Count" Then
                            lblnew.Tag = "50"
                        End If
                End Select

                lblnew.Top = y
                'Define Contextmenu for Label added
                lblnew.ContextMenu = CntFieldMenu
                lblnew.TextAlign = ContentAlignment.BottomRight
                lblnew.Anchor = AnchorStyles.Bottom
                Me.Controls.Add(lblnew)
                lblnew.BringToFront()
            End If
        Catch ex As Exception
            MsgBox("Error Adding fields to Report", MsgBoxStyle.ApplicationModal + MsgBoxStyle.OkOnly, gstrMessageBoxCaption)
        End Try

    End Sub
    Private Sub trReportExplorer_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles trReportExplorer.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
        x = 0
        y = 0
    End Sub
    Private Sub tblDesigner_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tblDesigner.ButtonClick
        'Try
        '    Select Case e.Button.Text

        '        Case "&Close"
        '            'If File.Exists(strFileName) Then
        '            '    Dim oTable As New DataTable
        '            '    oTable = GetReport()
        '            '    If oTable.Rows.Count > 0 Then
        '            '        UpdateReport(oTable.Rows(0).Item("nReportID"))
        '            '        File.Delete(strFileName)
        '            '    End If
        '            '    Me.Close()
        '            'End If
        '            Try
        '                Dim intyesnocancel As Int16
        '                intyesnocancel = MsgBox("Do you want to save changes to the Report Layout", MsgBoxStyle.ApplicationModal + MsgBoxStyle.YesNoCancel, gstrMessageBoxCaption)
        '                'save the Report layout and then save the xml file.
        '                If intyesnocancel = vbYes Then
        '                    If File.Exists(strFileName) Then
        '                        Dim oTable As New DataTable
        '                        oTable = GetReport()
        '                        If oTable.Rows.Count > 0 Then
        '                            UpdateReport(oTable.Rows(0).Item("nReportID"))
        '                            File.Delete(strFileName)
        '                        End If
        '                        Me.Close()
        '                    End If
        '                    'just delete the xml file without saving the report layout
        '                ElseIf intyesnocancel = vbNo Then
        '                    If File.Exists(strFileName) Then
        '                        'Dim oTable As New DataTable
        '                        'oTable = GetReport()
        '                        'If oTable.Rows.Count > 0 Then
        '                        'UpdateReport(oTable.Rows(0).Item("nReportID"))
        '                        File.Delete(strFileName)
        '                        'End If
        '                        Me.Close()
        '                    End If
        '                End If
        '            Catch ex As Exception
        '                'MessageBox.Show("Error Closing Report ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '                Me.Close()
        '            End Try

        '        Case "&Print"
        '            Try
        '                If CheckIfFieldExists() Then
        '                    CallPrint(True)
        '                Else
        '                    MsgBox("Make sure fields are Added to the Details Section", MsgBoxStyle.ApplicationModal + MsgBoxStyle.Information, gstrMessageBoxCaption)
        '                End If
        '            Catch ex As Exception
        '                MessageBox.Show("Error Printing Report ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            End Try

        '        Case "&Preview"
        '            Try
        '                If CheckIfFieldExists() Then
        '                    CallPrint(False)
        '                Else
        '                    MsgBox("Make sure fields are Added to the Details Section", MsgBoxStyle.ApplicationModal + MsgBoxStyle.Information, gstrMessageBoxCaption)
        '                End If
        '            Catch ex As Exception
        '                MessageBox.Show("Error Previewing Report ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        '            End Try

        '        Case "&New"
        '            Try
        '                RefreshReport()
        '            Catch ex As Exception
        '                MessageBox.Show("Error Refreshing Report ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '            End Try

        '        Case "&Save"
        '            Try
        '                If CheckIfFieldExists() Then
        '                    If Me.Controls.Count > 5 Then
        '                        generateXML()
        '                        If File.Exists(strFileName) Then
        '                            Dim oTable As New DataTable
        '                            oTable = GetReport()
        '                            If oTable.Rows.Count > 0 Then
        '                                UpdateReport(oTable.Rows(0).Item("nReportID"))
        '                            End If

        '                        End If
        '                    Else
        '                        'If MessageBox.Show("Do you want to save a blank Report", "gloEMR Prescription Report Designer", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
        '                        '    generateXML()
        '                        'End If
        '                    End If
        '                Else
        '                    MsgBox("Make sure fields are added to the Details Section", MsgBoxStyle.ApplicationModal + MsgBoxStyle.Information, gstrMessageBoxCaption)
        '                End If
        '            Catch ex As Exception
        '                MessageBox.Show("Error Saving Report ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        '            End Try

        '    End Select
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "Report Designer", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try

    End Sub

    Private Sub CallPrint(ByVal blnflag As Boolean)
        ''check for empty form 
        If Me.Controls.Count > 5 Then
            If File.Exists(strFileName) Then
                generateXML()
                readXML(blnflag)
            Else
                MessageBox.Show("Please set the path of the RxReport on the Settings form", "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Me.Close()
            End If

        Else
            MessageBox.Show("No Report Structure defined", "gloEMR Prescription Report Designer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub
    Private Sub FillReportDesigner()
        Dim objDatatable As DataTable = GetReport()
        If Not objDatatable.Rows.Count > 1 Then

            Dim content As Byte() = CType(objDatatable.Rows(0).Item("ReportData"), Byte())
            Dim stream As MemoryStream = New MemoryStream(content)

            Dim oFile As New System.IO.FileStream(strFileName, System.IO.FileMode.Create)
            stream.WriteTo(oFile)
            oFile.Close()
            'Else
            '    InsertRxReport()
        End If

    End Sub
    Private Function GetReport() As DataTable
        'sarika 26th june 07
        Dim objconn As SqlConnection = Nothing
        '----
        Dim objcmd As New SqlCommand
        Dim oAdapter As SqlDataAdapter

        Try
            Dim oTable As DataTable
            Dim strconn As String
            strconn = gloPMAdmin.mdlGeneral.GetConnectionString()
            objconn = New SqlConnection(strconn)
            objcmd.Connection = objconn
            objcmd.CommandText = "ScanReports_MST"
            objcmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = objcmd.Parameters.Add("@sReportType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = "RxReport"

            objconn.Open()
            'cmd.ExecuteNonQuery()
            oAdapter = New SqlDataAdapter
            oAdapter.SelectCommand = objcmd
            oTable = New DataTable
            oAdapter.Fill(oTable)
            Return oTable

        Catch ex As SqlException
            Return Nothing
            'MessageBox.Show(ex.ToString, "gloEMRAdmin - RxReportDesigner", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            Return Nothing
            'MessageBox.Show(ex.ToString, "gloEMRAdmin - RxReportDesigner", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objconn.Close()
        End Try
    End Function
    Private Function UpdateReport(ByVal ReportID As Long) As Boolean

        Dim objcmd As New SqlCommand
        ' Dim oReader As SqlDataReader
        'sarika 26th june 07
        Dim trUpdateReport As SqlTransaction = Nothing
        Dim objconn As SqlConnection = Nothing
        '---
        Try

            Dim strconn As String
            strconn = gloPMAdmin.mdlGeneral.GetConnectionString()
            objconn = New SqlConnection(strconn)
            objcmd.Connection = objconn
            objcmd.CommandText = "Delete from Reports_Mst"
            objcmd.CommandType = CommandType.Text
            objconn.Open()
            trUpdateReport = objconn.BeginTransaction
            objcmd.Transaction = trUpdateReport

            objcmd.ExecuteNonQuery()

            objcmd.Dispose()
            objcmd = Nothing

            objcmd = New SqlCommand
            objcmd.CommandText = "InsertReports_MST"
            objcmd.CommandType = CommandType.StoredProcedure
            objcmd.Connection = objconn

            Dim sqlParam As SqlParameter

            sqlParam = objcmd.Parameters.Add("@sReportType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = "RxReport"

            sqlParam = objcmd.Parameters.Add("@nReportID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ReportID

            sqlParam = objcmd.Parameters.Add("@nReportVersionNo", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 1

            sqlParam = objcmd.Parameters.Add("@ReportData", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input

            Dim ofile As FileStream

            ofile = New FileStream(strFileName, FileMode.Open, FileAccess.Read)

            Dim Br As BinaryReader
            Br = New BinaryReader(ofile)
            Dim bytesRead As Byte() = Br.ReadBytes(ofile.Length)
            sqlParam.Value = bytesRead


            'Dim mstream As ADODB.Stream
            'mstream = New ADODB.Stream
            'mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
            'mstream.Open()
            'mstream.LoadFromFile(strfilename)
            'mstream.LoadFromFile(Description)
            '            Return mstream

            'sqlParam.Value = mstream.Read
            'mstream.Close()

            objcmd.Transaction = trUpdateReport
            objcmd.ExecuteNonQuery()
            trUpdateReport.Commit()
            objconn.Close()
            ofile.Close()
            Br.Close()

            'sarika  22nd feb
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Modify, gstrLoginName & " user has modified the Prescription Report Layout.", gstrLoginName, gstrClientMachineName, 0, True)
            objAudit = Nothing
            '-------------

        Catch ex As SqlException
            trUpdateReport.Rollback()
            If objconn.State = ConnectionState.Open Then
                objconn.Close()
            End If
            'MessageBox.Show(ex.ToString, "gloEMRAdmin - RxReportDesigner", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            trUpdateReport.Rollback()
            If objconn.State = ConnectionState.Open Then
                objconn.Close()
            End If
            'MessageBox.Show(ex.ToString, "gloEMRAdmin - RxReportDesigner", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Function
    Private Function InsertRxReport() As Boolean
        'sarika 26th june 07
        Dim objconn As SqlConnection = Nothing
        '--
        Dim objcmd As New SqlCommand
        '  Dim oReader As SqlDataReader

        Try

            Dim strconn As String
            strconn = gloPMAdmin.mdlGeneral.GetConnectionString()
            objconn = New SqlConnection(strconn)
            objcmd.Connection = objconn
            objcmd.CommandText = "InsertReports_MST"
            objcmd.CommandType = CommandType.StoredProcedure

            Dim sqlParam As SqlParameter

            sqlParam = objcmd.Parameters.Add("@sReportType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = "RxReport"

            sqlParam = objcmd.Parameters.Add("@nReportID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 1

            sqlParam = objcmd.Parameters.Add("@nReportVersionNo", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 1

            sqlParam = objcmd.Parameters.Add("@ReportData", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input

            Dim ofile As FileStream

            ofile = New FileStream(gstrRxReportpath & "PrescriptionReport.xml", FileMode.Open, FileAccess.Read)

            Dim Br As BinaryReader
            Br = New BinaryReader(ofile)
            Dim bytesRead As Byte() = Br.ReadBytes(ofile.Length)

            sqlParam.Value = bytesRead


            'Dim mstream As ADODB.Stream
            'mstream = New ADODB.Stream
            'mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
            'mstream.Open()
            'mstream.LoadFromFile(strfilename)
            'mstream.LoadFromFile(Description)
            '            Return mstream

            'sqlParam.Value = mstream.Read
            'mstream.Close()

            objconn.Open()
            objcmd.ExecuteNonQuery()
            objconn.Close()
            ofile.Close()
            Br.Close()

            'sarika  22nd feb
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Modify, gstrLoginName & " user has modified the Prescription Report Layout.", gstrLoginName, gstrClientMachineName, 0, True)
            objAudit = Nothing
            '-------------
        Catch ex As SqlException
            'MessageBox.Show(ex.ToString, "gloEMRAdmin - RxReportDesigner", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            'MessageBox.Show(ex.ToString, "gloEMRAdmin - RxReportDesigner", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objconn.Close()
        End Try
    End Function
    'Check if the SIG/dispense/Refill/Maysubstitute/Drugs field has already been added
    'if so then do not allow duplicates
    Private Function CheckforDuplication(ByVal strnodename As String) As Boolean
        Dim objcontrol As Control
        Dim strsectionid As String
        Dim strcontrolname As String
        Try
            For Each objcontrol In Me.Controls
                If TypeOf (objcontrol) Is Label Then
                    If Not IsNothing(objcontrol.Tag) Then
                        strsectionid = ""
                        strsectionid = CType(objcontrol.Tag, String)
                        If strsectionid.ToString.Substring(0, 1) = "3" Then
                            strcontrolname = ""
                            strcontrolname = CType(objcontrol.Tag, String)
                            If strcontrolname.ToString.Substring(1, Len(strcontrolname.ToString) - 1) = strnodename Then
                                Return False
                                Exit For
                            End If
                        End If
                    Else

                    End If
                End If
                strsectionid = Nothing
                strcontrolname = Nothing
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    'Private Sub InitialiseCursor(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlDetails.MouseUp, pnlPageHeader.MouseUp, pnlPageFooter.MouseUp
    '    If Me.Cursor Is Cursors.No Then
    '        Me.Cursor = Cursors.Default
    '    End If
    'End Sub
    'Private Sub InitialiseCursor1(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pnlDetails.MouseDown, pnlPageFooter.MouseDown, pnlPageHeader.MouseDown
    '    If Me.Cursor Is Cursors.No Then
    '        Me.Cursor = Cursors.Default
    '    End If
    'End Sub


    'check if selected field is present or not in details section
    Private Function CheckIfFieldExists() As Boolean
        Dim objcontrol As Control
        Dim strsectionid As String
        Dim strcontrolname As String
        Try
            For Each objcontrol In Me.Controls
                If TypeOf (objcontrol) Is Label Then
                    If Not IsNothing(objcontrol.Tag) Then
                        strsectionid = ""
                        strsectionid = CType(objcontrol.Tag, String)
                        'if field exists then return true
                        If strsectionid.ToString.Substring(0, 1) = "3" Then
                            Return True
                            Exit For
                        End If
                    End If
                End If
                strsectionid = Nothing
                strcontrolname = Nothing
            Next
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function Checkboundaries(ByVal strtag As String, ByRef y As Int32, ByVal objlabel As Label) As Boolean
        Try
            Dim inttop As Int32 = pnlCenter.Top
            Select Case strtag

                'Container is Page Header section
                Case "pnlPageHeader"
                    'Make sure the Y position does not exceed the PageHeader section
                    If y > inttop + pnlPageHeader.Height Then
                        y = (inttop + pnlPageHeader.Height) - objlabel.Height
                        Return False
                        Exit Function
                    ElseIf y < inttop + btnPageHeader.Height + btnPageHeader.Height Then
                        y = inttop + btnPageHeader.Height + btnPageHeader.Height
                        Return False
                        Exit Function
                    End If

                    'Container is Page Footer section
                Case "pnlPageFooter"
                    'Make sure the Y position does not exceed the PageFooter section
                    If y > inttop + pnlPageHeader.Height + pnlDetails.Height + pnlPageFooter.Height Then
                        y = (inttop + pnlPageHeader.Height + pnlDetails.Height + pnlPageFooter.Height) - objlabel.Height
                        Return False
                        Exit Function
                    ElseIf y < (inttop + pnlPageHeader.Height + pnlDetails.Height) + btnPageFooter.Height + btnPageFooter.Height Then
                        y = (inttop + pnlPageHeader.Height + pnlDetails.Height) + btnPageFooter.Height + btnPageFooter.Height
                        Return False
                        Exit Function
                    End If
            End Select
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnPrint.Click

    End Sub

    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked
        Try
            Select Case e.ClickedItem.Text

                Case "&Close"
                    'If File.Exists(strFileName) Then
                    '    Dim oTable As New DataTable
                    '    oTable = GetReport()
                    '    If oTable.Rows.Count > 0 Then
                    '        UpdateReport(oTable.Rows(0).Item("nReportID"))
                    '        File.Delete(strFileName)
                    '    End If
                    '    Me.Close()
                    'End If
                    Try
                        'Dim intyesnocancel As Int16
                        'intyesnocancel = MsgBox("Do you want to save changes to the Report Layout", MsgBoxStyle.ApplicationModal + MsgBoxStyle.YesNoCancel + MsgBoxStyle., gstrMessageBoxCaption)
                        Dim result As DialogResult
                        result = MessageBox.Show("Do you want to save changes to the Report Layout?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                        'save the Report layout and then save the xml file.
                        If result = Windows.Forms.DialogResult.Yes Then
                            If File.Exists(strFileName) Then
                                Dim oTable As New DataTable
                                oTable = GetReport()
                                If oTable.Rows.Count > 0 Then
                                    UpdateReport(oTable.Rows(0).Item("nReportID"))
                                    File.Delete(strFileName)
                                End If
                                Me.Close()
                            End If
                            'just delete the xml file without saving the report layout
                        ElseIf result = Windows.Forms.DialogResult.No Then
                            If File.Exists(strFileName) Then
                                'Dim oTable As New DataTable
                                'oTable = GetReport()
                                'If oTable.Rows.Count > 0 Then
                                'UpdateReport(oTable.Rows(0).Item("nReportID"))
                                File.Delete(strFileName)
                                'End If
                                Me.Close()
                            End If
                        End If
                    Catch ex As Exception
                        'MessageBox.Show("Error Closing Report ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.Close()
                    End Try

                Case "&Print"
                    Try
                        If CheckIfFieldExists() Then
                            CallPrint(True)
                        Else
                            MsgBox("Make sure fields are Added to the Details Section", MsgBoxStyle.ApplicationModal + MsgBoxStyle.Information, gstrMessageBoxCaption)
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error Printing Report ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                Case "&Preview"
                    Try
                        If CheckIfFieldExists() Then
                            CallPrint(False)
                        Else
                            MsgBox("Make sure fields are Added to the Details Section", MsgBoxStyle.ApplicationModal + MsgBoxStyle.Information, gstrMessageBoxCaption)
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error Previewing Report ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

                    End Try

                Case "&New"
                    Try
                        RefreshReport()
                    Catch ex As Exception
                        MessageBox.Show("Error Refreshing Report ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try

                Case "&Save"
                    Try
                        If CheckIfFieldExists() Then
                            If Me.Controls.Count > 5 Then
                                generateXML()
                                If File.Exists(strFileName) Then
                                    Dim oTable As New DataTable
                                    oTable = GetReport()
                                    If oTable.Rows.Count > 0 Then
                                        UpdateReport(oTable.Rows(0).Item("nReportID"))
                                    End If

                                End If
                            Else
                                'If MessageBox.Show("Do you want to save a blank Report", "gloEMR Prescription Report Designer", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                                '    generateXML()
                                'End If
                            End If
                        Else
                            MsgBox("Make sure fields are added to the Details Section", MsgBoxStyle.ApplicationModal + MsgBoxStyle.Information, gstrMessageBoxCaption)
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Error Saving Report ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

                    End Try

            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Report Designer", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlsbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnSave.Click

    End Sub

    Private Sub Pr_ClickPrint(ByVal sender As Object, ByVal e As System.EventArgs, ByVal Dg As System.Windows.Forms.DataGrid, ByVal height As Double) Handles Pr.ClickPrint
        InvokePrint(Dg, height)
    End Sub
End Class
