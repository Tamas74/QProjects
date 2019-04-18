Imports System.IO
Imports oOffice = Microsoft.Office.Core
Imports Wd = Microsoft.Office.Interop.Word
Imports gloEMRGeneralLibrary.gloEMRDatabase
Public Class frmExportTemplates
    Inherits System.Windows.Forms.Form

    Private COL_SELECT = 0
    Private COL_TEMPLATEID = 1
    Private COL_TEMPLATENAME = 2
    Private COL_CATEGORY = 3
    Private COL_PROVIDER = 4

#Region " Windows Controls "
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents tls_ExportTemplate As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnDocClearAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnExportTemplate As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDocSelectAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnCatSelectAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnCatClearAll As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_BottomBorder As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBorder As System.Windows.Forms.Label
    Private WithEvents lbl_RightBorder As System.Windows.Forms.Label
    Private WithEvents lbl_TopBorder As System.Windows.Forms.Label
    Friend WithEvents pnlMainMain As System.Windows.Forms.Panel
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents pnlTemplates As System.Windows.Forms.Panel
    Friend WithEvents flxTemplates As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlSimmary As System.Windows.Forms.Panel
    Friend WithEvents lblTemplatesSummary As System.Windows.Forms.Label
    Friend WithEvents pnlProvider As System.Windows.Forms.Panel
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_pnlProviderName As System.Windows.Forms.Label
    Friend WithEvents pnlCategory As System.Windows.Forms.Panel
    Friend WithEvents trvCategories As System.Windows.Forms.TreeView
    Friend WithEvents pnlCategoryHeader As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlCategoryHeaderBB As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents lnl_pnlTemplatesBB As System.Windows.Forms.Label
    Private WithEvents lnl_pnlTemplatesLB As System.Windows.Forms.Label
    Private WithEvents lnl_pnlTemplatesRB As System.Windows.Forms.Label
    Private WithEvents lnl_pnlTemplatesTB As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSummaryBB As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSummaryLB As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSummaryRB As System.Windows.Forms.Label
    Private WithEvents lbl_pnlProviderBB As System.Windows.Forms.Label
    Private WithEvents lbl_pnlProviderLB As System.Windows.Forms.Label
    Private WithEvents lbl_pnlProviderRB As System.Windows.Forms.Label
    Private WithEvents lbl_pnlProviderTB As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlCategoryRB As System.Windows.Forms.Label
    Private WithEvents lbl_pnlCategoryLB As System.Windows.Forms.Label
    Private WithEvents lbl_pnlCategoryTB As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlDirectorySettingsTop As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlSimmaryTop As System.Windows.Forms.Panel
    Friend WithEvents pnlProviderTop As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlSummaryTB As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkExportLiquidDt As System.Windows.Forms.CheckBox
    Friend WithEvents wdTemplate As AxDSOFramer.AxFramerControl
#End Region

    ' Private WithEvents oWordApp As Wd.Application
    Private WithEvents oCurDoc As Wd.Document
    Friend WithEvents PnlOptions As System.Windows.Forms.Panel
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents rdbLiquidData As System.Windows.Forms.RadioButton
    Friend WithEvents rdbTemplates As System.Windows.Forms.RadioButton


    Dim IsCategegory As Boolean = False
    'isSelect_Clear_All variable decleared to avoid recursive call on tree node check change 
    'it will set to true on select or clear all function and set to false in finally block of respective function
    Dim isSelect_Clear_All As Boolean = False
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
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
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
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlDirectorySettings As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDirectoryPath As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExportTemplates))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnlMainMain = New System.Windows.Forms.Panel
        Me.pnlTemplates = New System.Windows.Forms.Panel
        Me.lnl_pnlTemplatesBB = New System.Windows.Forms.Label
        Me.lnl_pnlTemplatesLB = New System.Windows.Forms.Label
        Me.lnl_pnlTemplatesRB = New System.Windows.Forms.Label
        Me.lnl_pnlTemplatesTB = New System.Windows.Forms.Label
        Me.flxTemplates = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.wdTemplate = New AxDSOFramer.AxFramerControl
        Me.pnlSimmaryTop = New System.Windows.Forms.Panel
        Me.pnlSimmary = New System.Windows.Forms.Panel
        Me.lbl_pnlSummaryBB = New System.Windows.Forms.Label
        Me.lbl_pnlSummaryLB = New System.Windows.Forms.Label
        Me.lbl_pnlSummaryRB = New System.Windows.Forms.Label
        Me.lbl_pnlSummaryTB = New System.Windows.Forms.Label
        Me.lblTemplatesSummary = New System.Windows.Forms.Label
        Me.pnlProviderTop = New System.Windows.Forms.Panel
        Me.pnlProvider = New System.Windows.Forms.Panel
        Me.cmbProvider = New System.Windows.Forms.ComboBox
        Me.lbl_pnlProviderName = New System.Windows.Forms.Label
        Me.lbl_pnlProviderBB = New System.Windows.Forms.Label
        Me.lbl_pnlProviderLB = New System.Windows.Forms.Label
        Me.lbl_pnlProviderRB = New System.Windows.Forms.Label
        Me.lbl_pnlProviderTB = New System.Windows.Forms.Label
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.pnlCategory = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.trvCategories = New System.Windows.Forms.TreeView
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.pnlCategoryHeader = New System.Windows.Forms.Panel
        Me.lbl_pnlCategoryRB = New System.Windows.Forms.Label
        Me.lbl_pnlCategoryLB = New System.Windows.Forms.Label
        Me.lbl_pnlCategoryTB = New System.Windows.Forms.Label
        Me.lbl_pnlCategoryHeaderBB = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlDirectorySettingsTop = New System.Windows.Forms.Panel
        Me.pnlDirectorySettings = New System.Windows.Forms.Panel
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtDirectoryPath = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lbl_BottomBorder = New System.Windows.Forms.Label
        Me.lbl_LeftBorder = New System.Windows.Forms.Label
        Me.lbl_RightBorder = New System.Windows.Forms.Label
        Me.lbl_TopBorder = New System.Windows.Forms.Label
        Me.chkExportLiquidDt = New System.Windows.Forms.CheckBox
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.tls_ExportTemplate = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnCatSelectAll = New System.Windows.Forms.ToolStripButton
        Me.ts_btnCatClearAll = New System.Windows.Forms.ToolStripButton
        Me.ts_btnDocSelectAll = New System.Windows.Forms.ToolStripButton
        Me.ts_btnDocClearAll = New System.Windows.Forms.ToolStripButton
        Me.ts_btnExportTemplate = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.PnlOptions = New System.Windows.Forms.Panel
        Me.rdbLiquidData = New System.Windows.Forms.RadioButton
        Me.rdbTemplates = New System.Windows.Forms.RadioButton
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.pnlMain.SuspendLayout()
        Me.pnlMainMain.SuspendLayout()
        Me.pnlTemplates.SuspendLayout()
        CType(Me.flxTemplates, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wdTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSimmaryTop.SuspendLayout()
        Me.pnlSimmary.SuspendLayout()
        Me.pnlProviderTop.SuspendLayout()
        Me.pnlProvider.SuspendLayout()
        Me.pnlCategory.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlCategoryHeader.SuspendLayout()
        Me.pnlDirectorySettingsTop.SuspendLayout()
        Me.pnlDirectorySettings.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.tls_ExportTemplate.SuspendLayout()
        Me.PnlOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.Transparent
        Me.pnlMain.Controls.Add(Me.pnlMainMain)
        Me.pnlMain.Controls.Add(Me.pnlDirectorySettingsTop)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 89)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1028, 657)
        Me.pnlMain.TabIndex = 2
        '
        'pnlMainMain
        '
        Me.pnlMainMain.Controls.Add(Me.pnlTemplates)
        Me.pnlMainMain.Controls.Add(Me.pnlSimmaryTop)
        Me.pnlMainMain.Controls.Add(Me.pnlProviderTop)
        Me.pnlMainMain.Controls.Add(Me.Splitter1)
        Me.pnlMainMain.Controls.Add(Me.pnlCategory)
        Me.pnlMainMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMainMain.Location = New System.Drawing.Point(0, 30)
        Me.pnlMainMain.Name = "pnlMainMain"
        Me.pnlMainMain.Size = New System.Drawing.Size(1028, 627)
        Me.pnlMainMain.TabIndex = 2
        '
        'pnlTemplates
        '
        Me.pnlTemplates.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlTemplates.Controls.Add(Me.lnl_pnlTemplatesBB)
        Me.pnlTemplates.Controls.Add(Me.lnl_pnlTemplatesLB)
        Me.pnlTemplates.Controls.Add(Me.lnl_pnlTemplatesRB)
        Me.pnlTemplates.Controls.Add(Me.lnl_pnlTemplatesTB)
        Me.pnlTemplates.Controls.Add(Me.flxTemplates)
        Me.pnlTemplates.Controls.Add(Me.wdTemplate)
        Me.pnlTemplates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTemplates.Location = New System.Drawing.Point(264, 27)
        Me.pnlTemplates.Name = "pnlTemplates"
        Me.pnlTemplates.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlTemplates.Size = New System.Drawing.Size(764, 574)
        Me.pnlTemplates.TabIndex = 2
        '
        'lnl_pnlTemplatesBB
        '
        Me.lnl_pnlTemplatesBB.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lnl_pnlTemplatesBB.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lnl_pnlTemplatesBB.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lnl_pnlTemplatesBB.Location = New System.Drawing.Point(1, 570)
        Me.lnl_pnlTemplatesBB.Name = "lnl_pnlTemplatesBB"
        Me.lnl_pnlTemplatesBB.Size = New System.Drawing.Size(759, 1)
        Me.lnl_pnlTemplatesBB.TabIndex = 8
        Me.lnl_pnlTemplatesBB.Text = "label2"
        '
        'lnl_pnlTemplatesLB
        '
        Me.lnl_pnlTemplatesLB.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lnl_pnlTemplatesLB.Dock = System.Windows.Forms.DockStyle.Left
        Me.lnl_pnlTemplatesLB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnl_pnlTemplatesLB.Location = New System.Drawing.Point(0, 1)
        Me.lnl_pnlTemplatesLB.Name = "lnl_pnlTemplatesLB"
        Me.lnl_pnlTemplatesLB.Size = New System.Drawing.Size(1, 570)
        Me.lnl_pnlTemplatesLB.TabIndex = 7
        Me.lnl_pnlTemplatesLB.Text = "label4"
        '
        'lnl_pnlTemplatesRB
        '
        Me.lnl_pnlTemplatesRB.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lnl_pnlTemplatesRB.Dock = System.Windows.Forms.DockStyle.Right
        Me.lnl_pnlTemplatesRB.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lnl_pnlTemplatesRB.Location = New System.Drawing.Point(760, 1)
        Me.lnl_pnlTemplatesRB.Name = "lnl_pnlTemplatesRB"
        Me.lnl_pnlTemplatesRB.Size = New System.Drawing.Size(1, 570)
        Me.lnl_pnlTemplatesRB.TabIndex = 6
        Me.lnl_pnlTemplatesRB.Text = "label3"
        '
        'lnl_pnlTemplatesTB
        '
        Me.lnl_pnlTemplatesTB.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lnl_pnlTemplatesTB.Dock = System.Windows.Forms.DockStyle.Top
        Me.lnl_pnlTemplatesTB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnl_pnlTemplatesTB.Location = New System.Drawing.Point(0, 0)
        Me.lnl_pnlTemplatesTB.Name = "lnl_pnlTemplatesTB"
        Me.lnl_pnlTemplatesTB.Size = New System.Drawing.Size(761, 1)
        Me.lnl_pnlTemplatesTB.TabIndex = 5
        Me.lnl_pnlTemplatesTB.Text = "label1"
        '
        'flxTemplates
        '
        Me.flxTemplates.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.flxTemplates.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.flxTemplates.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.flxTemplates.ColumnInfo = "12,0,0,0,0,95,Columns:0{Style:""DataType:System.Boolean;ImageAlign:CenterCenter;"";" & _
            "}" & Global.Microsoft.VisualBasic.ChrW(9) & "1{StyleFixed:""TextAlign:CenterCenter;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.flxTemplates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flxTemplates.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.flxTemplates.Location = New System.Drawing.Point(0, 0)
        Me.flxTemplates.Name = "flxTemplates"
        Me.flxTemplates.Rows.DefaultSize = 19
        Me.flxTemplates.Size = New System.Drawing.Size(761, 571)
        Me.flxTemplates.StyleInfo = resources.GetString("flxTemplates.StyleInfo")
        Me.flxTemplates.TabIndex = 3
        '
        'wdTemplate
        '
        Me.wdTemplate.Enabled = True
        Me.wdTemplate.Location = New System.Drawing.Point(186, 63)
        Me.wdTemplate.Name = "wdTemplate"
        Me.wdTemplate.OcxState = CType(resources.GetObject("wdTemplate.OcxState"), System.Windows.Forms.AxHost.State)
        Me.wdTemplate.Size = New System.Drawing.Size(62, 36)
        Me.wdTemplate.TabIndex = 15
        Me.wdTemplate.Visible = False
        '
        'pnlSimmaryTop
        '
        Me.pnlSimmaryTop.Controls.Add(Me.pnlSimmary)
        Me.pnlSimmaryTop.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlSimmaryTop.Location = New System.Drawing.Point(264, 601)
        Me.pnlSimmaryTop.Name = "pnlSimmaryTop"
        Me.pnlSimmaryTop.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlSimmaryTop.Size = New System.Drawing.Size(764, 26)
        Me.pnlSimmaryTop.TabIndex = 5
        '
        'pnlSimmary
        '
        Me.pnlSimmary.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlSimmary.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSimmary.Controls.Add(Me.lbl_pnlSummaryBB)
        Me.pnlSimmary.Controls.Add(Me.lbl_pnlSummaryLB)
        Me.pnlSimmary.Controls.Add(Me.lbl_pnlSummaryRB)
        Me.pnlSimmary.Controls.Add(Me.lbl_pnlSummaryTB)
        Me.pnlSimmary.Controls.Add(Me.lblTemplatesSummary)
        Me.pnlSimmary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSimmary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSimmary.Location = New System.Drawing.Point(0, 0)
        Me.pnlSimmary.Name = "pnlSimmary"
        Me.pnlSimmary.Size = New System.Drawing.Size(761, 23)
        Me.pnlSimmary.TabIndex = 1
        '
        'lbl_pnlSummaryBB
        '
        Me.lbl_pnlSummaryBB.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSummaryBB.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlSummaryBB.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_pnlSummaryBB.Location = New System.Drawing.Point(1, 22)
        Me.lbl_pnlSummaryBB.Name = "lbl_pnlSummaryBB"
        Me.lbl_pnlSummaryBB.Size = New System.Drawing.Size(759, 1)
        Me.lbl_pnlSummaryBB.TabIndex = 8
        Me.lbl_pnlSummaryBB.Text = "label2"
        '
        'lbl_pnlSummaryLB
        '
        Me.lbl_pnlSummaryLB.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSummaryLB.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSummaryLB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pnlSummaryLB.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlSummaryLB.Name = "lbl_pnlSummaryLB"
        Me.lbl_pnlSummaryLB.Size = New System.Drawing.Size(1, 22)
        Me.lbl_pnlSummaryLB.TabIndex = 7
        Me.lbl_pnlSummaryLB.Text = "label4"
        '
        'lbl_pnlSummaryRB
        '
        Me.lbl_pnlSummaryRB.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSummaryRB.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSummaryRB.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_pnlSummaryRB.Location = New System.Drawing.Point(760, 1)
        Me.lbl_pnlSummaryRB.Name = "lbl_pnlSummaryRB"
        Me.lbl_pnlSummaryRB.Size = New System.Drawing.Size(1, 22)
        Me.lbl_pnlSummaryRB.TabIndex = 6
        Me.lbl_pnlSummaryRB.Text = "label3"
        '
        'lbl_pnlSummaryTB
        '
        Me.lbl_pnlSummaryTB.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSummaryTB.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSummaryTB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pnlSummaryTB.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlSummaryTB.Name = "lbl_pnlSummaryTB"
        Me.lbl_pnlSummaryTB.Size = New System.Drawing.Size(761, 1)
        Me.lbl_pnlSummaryTB.TabIndex = 5
        Me.lbl_pnlSummaryTB.Text = "label1"
        '
        'lblTemplatesSummary
        '
        Me.lblTemplatesSummary.BackColor = System.Drawing.Color.Transparent
        Me.lblTemplatesSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTemplatesSummary.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblTemplatesSummary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTemplatesSummary.Location = New System.Drawing.Point(0, 0)
        Me.lblTemplatesSummary.Name = "lblTemplatesSummary"
        Me.lblTemplatesSummary.Size = New System.Drawing.Size(761, 23)
        Me.lblTemplatesSummary.TabIndex = 3
        Me.lblTemplatesSummary.Text = "  Total Templates = 0"
        Me.lblTemplatesSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlProviderTop
        '
        Me.pnlProviderTop.Controls.Add(Me.pnlProvider)
        Me.pnlProviderTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlProviderTop.Location = New System.Drawing.Point(264, 0)
        Me.pnlProviderTop.Name = "pnlProviderTop"
        Me.pnlProviderTop.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlProviderTop.Size = New System.Drawing.Size(764, 27)
        Me.pnlProviderTop.TabIndex = 4
        '
        'pnlProvider
        '
        Me.pnlProvider.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlProvider.Controls.Add(Me.cmbProvider)
        Me.pnlProvider.Controls.Add(Me.lbl_pnlProviderName)
        Me.pnlProvider.Controls.Add(Me.lbl_pnlProviderBB)
        Me.pnlProvider.Controls.Add(Me.lbl_pnlProviderLB)
        Me.pnlProvider.Controls.Add(Me.lbl_pnlProviderRB)
        Me.pnlProvider.Controls.Add(Me.lbl_pnlProviderTB)
        Me.pnlProvider.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlProvider.Location = New System.Drawing.Point(0, 0)
        Me.pnlProvider.Name = "pnlProvider"
        Me.pnlProvider.Size = New System.Drawing.Size(761, 24)
        Me.pnlProvider.TabIndex = 0
        '
        'cmbProvider
        '
        Me.cmbProvider.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProvider.ForeColor = System.Drawing.Color.Black
        Me.cmbProvider.Location = New System.Drawing.Point(112, 1)
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.Size = New System.Drawing.Size(321, 22)
        Me.cmbProvider.TabIndex = 1
        '
        'lbl_pnlProviderName
        '
        Me.lbl_pnlProviderName.BackColor = System.Drawing.Color.Transparent
        Me.lbl_pnlProviderName.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlProviderName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pnlProviderName.Location = New System.Drawing.Point(1, 1)
        Me.lbl_pnlProviderName.Name = "lbl_pnlProviderName"
        Me.lbl_pnlProviderName.Size = New System.Drawing.Size(111, 22)
        Me.lbl_pnlProviderName.TabIndex = 0
        Me.lbl_pnlProviderName.Text = "Provider Name :"
        Me.lbl_pnlProviderName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_pnlProviderBB
        '
        Me.lbl_pnlProviderBB.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlProviderBB.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlProviderBB.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_pnlProviderBB.Location = New System.Drawing.Point(1, 23)
        Me.lbl_pnlProviderBB.Name = "lbl_pnlProviderBB"
        Me.lbl_pnlProviderBB.Size = New System.Drawing.Size(759, 1)
        Me.lbl_pnlProviderBB.TabIndex = 8
        Me.lbl_pnlProviderBB.Text = "label2"
        '
        'lbl_pnlProviderLB
        '
        Me.lbl_pnlProviderLB.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlProviderLB.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlProviderLB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pnlProviderLB.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlProviderLB.Name = "lbl_pnlProviderLB"
        Me.lbl_pnlProviderLB.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlProviderLB.TabIndex = 7
        Me.lbl_pnlProviderLB.Text = "label4"
        '
        'lbl_pnlProviderRB
        '
        Me.lbl_pnlProviderRB.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlProviderRB.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlProviderRB.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_pnlProviderRB.Location = New System.Drawing.Point(760, 1)
        Me.lbl_pnlProviderRB.Name = "lbl_pnlProviderRB"
        Me.lbl_pnlProviderRB.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlProviderRB.TabIndex = 6
        Me.lbl_pnlProviderRB.Text = "label3"
        '
        'lbl_pnlProviderTB
        '
        Me.lbl_pnlProviderTB.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlProviderTB.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlProviderTB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pnlProviderTB.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlProviderTB.Name = "lbl_pnlProviderTB"
        Me.lbl_pnlProviderTB.Size = New System.Drawing.Size(761, 1)
        Me.lbl_pnlProviderTB.TabIndex = 5
        Me.lbl_pnlProviderTB.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(261, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 627)
        Me.Splitter1.TabIndex = 3
        Me.Splitter1.TabStop = False
        '
        'pnlCategory
        '
        Me.pnlCategory.Controls.Add(Me.Panel1)
        Me.pnlCategory.Controls.Add(Me.Panel2)
        Me.pnlCategory.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlCategory.Location = New System.Drawing.Point(0, 0)
        Me.pnlCategory.Name = "pnlCategory"
        Me.pnlCategory.Size = New System.Drawing.Size(261, 627)
        Me.pnlCategory.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.trvCategories)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 27)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel1.Size = New System.Drawing.Size(261, 600)
        Me.Panel1.TabIndex = 9
        '
        'trvCategories
        '
        Me.trvCategories.BackColor = System.Drawing.Color.White
        Me.trvCategories.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCategories.CheckBoxes = True
        Me.trvCategories.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvCategories.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvCategories.ForeColor = System.Drawing.Color.Black
        Me.trvCategories.Indent = 20
        Me.trvCategories.ItemHeight = 20
        Me.trvCategories.Location = New System.Drawing.Point(8, 5)
        Me.trvCategories.Name = "trvCategories"
        Me.trvCategories.ShowLines = False
        Me.trvCategories.ShowPlusMinus = False
        Me.trvCategories.ShowRootLines = False
        Me.trvCategories.Size = New System.Drawing.Size(252, 591)
        Me.trvCategories.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(8, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(252, 4)
        Me.Label9.TabIndex = 14
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(4, 595)
        Me.Label4.TabIndex = 13
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 596)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(256, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 596)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(260, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 596)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(258, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlCategoryHeader)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel2.Size = New System.Drawing.Size(261, 27)
        Me.Panel2.TabIndex = 9
        '
        'pnlCategoryHeader
        '
        Me.pnlCategoryHeader.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlCategoryHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlCategoryHeader.Controls.Add(Me.lbl_pnlCategoryRB)
        Me.pnlCategoryHeader.Controls.Add(Me.lbl_pnlCategoryLB)
        Me.pnlCategoryHeader.Controls.Add(Me.lbl_pnlCategoryTB)
        Me.pnlCategoryHeader.Controls.Add(Me.lbl_pnlCategoryHeaderBB)
        Me.pnlCategoryHeader.Controls.Add(Me.Label1)
        Me.pnlCategoryHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCategoryHeader.Location = New System.Drawing.Point(3, 0)
        Me.pnlCategoryHeader.Name = "pnlCategoryHeader"
        Me.pnlCategoryHeader.Size = New System.Drawing.Size(258, 24)
        Me.pnlCategoryHeader.TabIndex = 3
        '
        'lbl_pnlCategoryRB
        '
        Me.lbl_pnlCategoryRB.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlCategoryRB.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlCategoryRB.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_pnlCategoryRB.Location = New System.Drawing.Point(257, 1)
        Me.lbl_pnlCategoryRB.Name = "lbl_pnlCategoryRB"
        Me.lbl_pnlCategoryRB.Size = New System.Drawing.Size(1, 22)
        Me.lbl_pnlCategoryRB.TabIndex = 9
        Me.lbl_pnlCategoryRB.Text = "label3"
        '
        'lbl_pnlCategoryLB
        '
        Me.lbl_pnlCategoryLB.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlCategoryLB.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlCategoryLB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pnlCategoryLB.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlCategoryLB.Name = "lbl_pnlCategoryLB"
        Me.lbl_pnlCategoryLB.Size = New System.Drawing.Size(1, 22)
        Me.lbl_pnlCategoryLB.TabIndex = 8
        Me.lbl_pnlCategoryLB.Text = "label4"
        '
        'lbl_pnlCategoryTB
        '
        Me.lbl_pnlCategoryTB.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlCategoryTB.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlCategoryTB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pnlCategoryTB.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlCategoryTB.Name = "lbl_pnlCategoryTB"
        Me.lbl_pnlCategoryTB.Size = New System.Drawing.Size(258, 1)
        Me.lbl_pnlCategoryTB.TabIndex = 7
        Me.lbl_pnlCategoryTB.Text = "label1"
        '
        'lbl_pnlCategoryHeaderBB
        '
        Me.lbl_pnlCategoryHeaderBB.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlCategoryHeaderBB.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlCategoryHeaderBB.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_pnlCategoryHeaderBB.Location = New System.Drawing.Point(0, 23)
        Me.lbl_pnlCategoryHeaderBB.Name = "lbl_pnlCategoryHeaderBB"
        Me.lbl_pnlCategoryHeaderBB.Size = New System.Drawing.Size(258, 1)
        Me.lbl_pnlCategoryHeaderBB.TabIndex = 6
        Me.lbl_pnlCategoryHeaderBB.Text = "label1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(258, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Templates Categories"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlDirectorySettingsTop
        '
        Me.pnlDirectorySettingsTop.Controls.Add(Me.pnlDirectorySettings)
        Me.pnlDirectorySettingsTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDirectorySettingsTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlDirectorySettingsTop.Name = "pnlDirectorySettingsTop"
        Me.pnlDirectorySettingsTop.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlDirectorySettingsTop.Size = New System.Drawing.Size(1028, 30)
        Me.pnlDirectorySettingsTop.TabIndex = 13
        '
        'pnlDirectorySettings
        '
        Me.pnlDirectorySettings.BackColor = System.Drawing.Color.Transparent
        Me.pnlDirectorySettings.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlDirectorySettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDirectorySettings.Controls.Add(Me.btnBrowse)
        Me.pnlDirectorySettings.Controls.Add(Me.Label2)
        Me.pnlDirectorySettings.Controls.Add(Me.txtDirectoryPath)
        Me.pnlDirectorySettings.Controls.Add(Me.Label3)
        Me.pnlDirectorySettings.Controls.Add(Me.lbl_BottomBorder)
        Me.pnlDirectorySettings.Controls.Add(Me.lbl_LeftBorder)
        Me.pnlDirectorySettings.Controls.Add(Me.lbl_RightBorder)
        Me.pnlDirectorySettings.Controls.Add(Me.lbl_TopBorder)
        Me.pnlDirectorySettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDirectorySettings.Location = New System.Drawing.Point(3, 3)
        Me.pnlDirectorySettings.Name = "pnlDirectorySettings"
        Me.pnlDirectorySettings.Size = New System.Drawing.Size(1022, 24)
        Me.pnlDirectorySettings.TabIndex = 1
        '
        'btnBrowse
        '
        Me.btnBrowse.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowse.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowse.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowse.FlatAppearance.BorderSize = 0
        Me.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Image = CType(resources.GetObject("btnBrowse.Image"), System.Drawing.Image)
        Me.btnBrowse.Location = New System.Drawing.Point(699, 1)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(24, 22)
        Me.btnBrowse.TabIndex = 2
        Me.btnBrowse.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(694, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(5, 22)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "     "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDirectoryPath
        '
        Me.txtDirectoryPath.BackColor = System.Drawing.Color.White
        Me.txtDirectoryPath.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtDirectoryPath.ForeColor = System.Drawing.Color.Black
        Me.txtDirectoryPath.Location = New System.Drawing.Point(106, 1)
        Me.txtDirectoryPath.Name = "txtDirectoryPath"
        Me.txtDirectoryPath.ReadOnly = False
        Me.txtDirectoryPath.Size = New System.Drawing.Size(588, 22)
        Me.txtDirectoryPath.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(1, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(105, 22)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "     Folder Path :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_BottomBorder
        '
        Me.lbl_BottomBorder.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBorder.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBorder.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBorder.Location = New System.Drawing.Point(1, 23)
        Me.lbl_BottomBorder.Name = "lbl_BottomBorder"
        Me.lbl_BottomBorder.Size = New System.Drawing.Size(1020, 1)
        Me.lbl_BottomBorder.TabIndex = 8
        Me.lbl_BottomBorder.Text = "label2"
        '
        'lbl_LeftBorder
        '
        Me.lbl_LeftBorder.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBorder.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBorder.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBorder.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBorder.Name = "lbl_LeftBorder"
        Me.lbl_LeftBorder.Size = New System.Drawing.Size(1, 23)
        Me.lbl_LeftBorder.TabIndex = 7
        Me.lbl_LeftBorder.Text = "label4"
        '
        'lbl_RightBorder
        '
        Me.lbl_RightBorder.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBorder.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBorder.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBorder.Location = New System.Drawing.Point(1021, 1)
        Me.lbl_RightBorder.Name = "lbl_RightBorder"
        Me.lbl_RightBorder.Size = New System.Drawing.Size(1, 23)
        Me.lbl_RightBorder.TabIndex = 6
        Me.lbl_RightBorder.Text = "label3"
        '
        'lbl_TopBorder
        '
        Me.lbl_TopBorder.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBorder.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBorder.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBorder.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBorder.Name = "lbl_TopBorder"
        Me.lbl_TopBorder.Size = New System.Drawing.Size(1022, 1)
        Me.lbl_TopBorder.TabIndex = 5
        Me.lbl_TopBorder.Text = "label1"
        '
        'chkExportLiquidDt
        '
        Me.chkExportLiquidDt.AutoSize = True
        Me.chkExportLiquidDt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkExportLiquidDt.Location = New System.Drawing.Point(338, 10)
        Me.chkExportLiquidDt.Name = "chkExportLiquidDt"
        Me.chkExportLiquidDt.Size = New System.Drawing.Size(174, 18)
        Me.chkExportLiquidDt.TabIndex = 11
        Me.chkExportLiquidDt.Text = "Export With Liquid Data"
        Me.chkExportLiquidDt.UseVisualStyleBackColor = True
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.tls_ExportTemplate)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(1028, 54)
        Me.pnlToolStrip.TabIndex = 13
        '
        'tls_ExportTemplate
        '
        Me.tls_ExportTemplate.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tls_ExportTemplate.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_ExportTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_ExportTemplate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_ExportTemplate.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tls_ExportTemplate.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_ExportTemplate.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnCatSelectAll, Me.ts_btnCatClearAll, Me.ts_btnDocSelectAll, Me.ts_btnDocClearAll, Me.ts_btnExportTemplate, Me.ts_btnClose})
        Me.tls_ExportTemplate.Location = New System.Drawing.Point(0, 0)
        Me.tls_ExportTemplate.Name = "tls_ExportTemplate"
        Me.tls_ExportTemplate.Size = New System.Drawing.Size(1028, 53)
        Me.tls_ExportTemplate.TabIndex = 0
        Me.tls_ExportTemplate.Text = "ToolStrip1"
        '
        'ts_btnCatSelectAll
        '
        Me.ts_btnCatSelectAll.Image = CType(resources.GetObject("ts_btnCatSelectAll.Image"), System.Drawing.Image)
        Me.ts_btnCatSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCatSelectAll.Name = "ts_btnCatSelectAll"
        Me.ts_btnCatSelectAll.Size = New System.Drawing.Size(96, 50)
        Me.ts_btnCatSelectAll.Tag = "CategorySelectAll"
        Me.ts_btnCatSelectAll.Text = "&Select All Cat."
        Me.ts_btnCatSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnCatSelectAll.ToolTipText = "Select All Categories"
        '
        'ts_btnCatClearAll
        '
        Me.ts_btnCatClearAll.Image = CType(resources.GetObject("ts_btnCatClearAll.Image"), System.Drawing.Image)
        Me.ts_btnCatClearAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnCatClearAll.Name = "ts_btnCatClearAll"
        Me.ts_btnCatClearAll.Size = New System.Drawing.Size(89, 50)
        Me.ts_btnCatClearAll.Tag = "CategoryClearAll"
        Me.ts_btnCatClearAll.Text = "&Clear All Cat."
        Me.ts_btnCatClearAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnCatClearAll.ToolTipText = "Clear All Categories"
        Me.ts_btnCatClearAll.Visible = False
        '
        'ts_btnDocSelectAll
        '
        Me.ts_btnDocSelectAll.Image = CType(resources.GetObject("ts_btnDocSelectAll.Image"), System.Drawing.Image)
        Me.ts_btnDocSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDocSelectAll.Name = "ts_btnDocSelectAll"
        Me.ts_btnDocSelectAll.Size = New System.Drawing.Size(98, 50)
        Me.ts_btnDocSelectAll.Tag = "DocumentSelectAll"
        Me.ts_btnDocSelectAll.Text = "&Select All Doc."
        Me.ts_btnDocSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnDocSelectAll.ToolTipText = "Select All Documents "
        Me.ts_btnDocSelectAll.Visible = False
        '
        'ts_btnDocClearAll
        '
        Me.ts_btnDocClearAll.Image = CType(resources.GetObject("ts_btnDocClearAll.Image"), System.Drawing.Image)
        Me.ts_btnDocClearAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDocClearAll.Name = "ts_btnDocClearAll"
        Me.ts_btnDocClearAll.Size = New System.Drawing.Size(91, 50)
        Me.ts_btnDocClearAll.Tag = "DocumentClearAll"
        Me.ts_btnDocClearAll.Text = "&Clear All Doc."
        Me.ts_btnDocClearAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnDocClearAll.ToolTipText = "Clear All Documents"
        Me.ts_btnDocClearAll.Visible = False
        '
        'ts_btnExportTemplate
        '
        Me.ts_btnExportTemplate.Image = CType(resources.GetObject("ts_btnExportTemplate.Image"), System.Drawing.Image)
        Me.ts_btnExportTemplate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnExportTemplate.Name = "ts_btnExportTemplate"
        Me.ts_btnExportTemplate.Size = New System.Drawing.Size(118, 50)
        Me.ts_btnExportTemplate.Tag = "ExportTemplate"
        Me.ts_btnExportTemplate.Text = "&Export Templates"
        Me.ts_btnExportTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnClose
        '
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
        'PnlOptions
        '
        Me.PnlOptions.Controls.Add(Me.chkExportLiquidDt)
        Me.PnlOptions.Controls.Add(Me.rdbLiquidData)
        Me.PnlOptions.Controls.Add(Me.rdbTemplates)
        Me.PnlOptions.Controls.Add(Me.Label13)
        Me.PnlOptions.Controls.Add(Me.Label12)
        Me.PnlOptions.Controls.Add(Me.Label11)
        Me.PnlOptions.Controls.Add(Me.Label10)
        Me.PnlOptions.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlOptions.Location = New System.Drawing.Point(0, 54)
        Me.PnlOptions.Name = "PnlOptions"
        Me.PnlOptions.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.PnlOptions.Size = New System.Drawing.Size(1028, 35)
        Me.PnlOptions.TabIndex = 17
        '
        'rdbLiquidData
        '
        Me.rdbLiquidData.AutoSize = True
        Me.rdbLiquidData.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbLiquidData.Location = New System.Drawing.Point(186, 10)
        Me.rdbLiquidData.Name = "rdbLiquidData"
        Me.rdbLiquidData.Size = New System.Drawing.Size(140, 18)
        Me.rdbLiquidData.TabIndex = 15
        Me.rdbLiquidData.Text = "Export &Liquid Data"
        Me.rdbLiquidData.UseVisualStyleBackColor = True
        '
        'rdbTemplates
        '
        Me.rdbTemplates.AutoSize = True
        Me.rdbTemplates.Checked = True
        Me.rdbTemplates.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdbTemplates.Location = New System.Drawing.Point(48, 10)
        Me.rdbTemplates.Name = "rdbTemplates"
        Me.rdbTemplates.Size = New System.Drawing.Size(132, 18)
        Me.rdbTemplates.TabIndex = 14
        Me.rdbTemplates.TabStop = True
        Me.rdbTemplates.Text = "Export &Templates"
        Me.rdbTemplates.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(4, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1020, 1)
        Me.Label13.TabIndex = 19
        Me.Label13.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(4, 34)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1020, 1)
        Me.Label12.TabIndex = 18
        Me.Label12.Text = "label1"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(1024, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 32)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 32)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "label4"
        '
        'frmExportTemplates
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1028, 746)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.PnlOptions)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmExportTemplates"
        Me.Text = "Export Templates"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMainMain.ResumeLayout(False)
        Me.pnlTemplates.ResumeLayout(False)
        CType(Me.flxTemplates, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wdTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSimmaryTop.ResumeLayout(False)
        Me.pnlSimmary.ResumeLayout(False)
        Me.pnlProviderTop.ResumeLayout(False)
        Me.pnlProvider.ResumeLayout(False)
        Me.pnlCategory.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlCategoryHeader.ResumeLayout(False)
        Me.pnlDirectorySettingsTop.ResumeLayout(False)
        Me.pnlDirectorySettings.ResumeLayout(False)
        Me.pnlDirectorySettings.PerformLayout()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tls_ExportTemplate.ResumeLayout(False)
        Me.tls_ExportTemplate.PerformLayout()
        Me.PnlOptions.ResumeLayout(False)
        Me.PnlOptions.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region



    'Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
    '    Try
    '        Dim nCount As Int16
    '        For nCount = 0 To trvCategories.GetNodeCount(True) - 1
    '            trvCategories.Nodes(nCount).Checked = True
    '        Next
    '    Catch ex As Exception
    '        MessageBox.Show("Unable to select all the Categories due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try
    'End Sub
    Public Sub CategorySelectAll()
        Try
            Dim nCount As Int16
            isSelect_Clear_All = True
            For nCount = 0 To trvCategories.GetNodeCount(True) - 1
                trvCategories.Nodes(nCount).Checked = True
            Next
            If trvCategories.GetNodeCount(True) > 0 Then
                ts_btnCatClearAll.Visible = True
                ts_btnCatSelectAll.Visible = False
                ts_btnDocSelectAll.Visible = False
                ts_btnDocClearAll.Visible = True
                For index As Integer = 1 To flxTemplates.Rows.Count - 1
                    flxTemplates.SetCellCheck(index, COL_SELECT, C1.Win.C1FlexGrid.CheckEnum.Checked)
                Next
            End If
            Fill_Templates()
            DocumentSelectClearAll(True)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to select all the categories due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            isSelect_Clear_All = False
        End Try
    End Sub
    'Private Sub btnClearAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearAll.Click
    '    Try
    '        Dim nCount As Int16
    '        For nCount = 0 To trvCategories.GetNodeCount(True) - 1
    '            trvCategories.Nodes(nCount).Checked = False
    '        Next
    '    Catch ex As Exception
    '        MessageBox.Show("Unable to deselect all the Categories due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try
    'End Sub
    Public Sub CategoryClearAll()
        Try
            Dim nCount As Int16
            isSelect_Clear_All = True
            For nCount = 0 To trvCategories.GetNodeCount(True) - 1
                trvCategories.Nodes(nCount).Checked = False
            Next
            If trvCategories.GetNodeCount(True) > 0 Then
                ts_btnCatClearAll.Visible = False
                ts_btnCatSelectAll.Visible = True
                ts_btnDocClearAll.Visible = False
                ts_btnDocSelectAll.Visible = False
            End If
            Fill_Templates()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to deselect all the categories due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            isSelect_Clear_All = False
        End Try
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            With FolderBrowserDialog1
                If rdbLiquidData.Checked = True Then
                    .Description = "Select directory in which liquid data to export"
                Else
                    .Description = "Select directory in which templates to export"
                End If

                .ShowNewFolderButton = True
            End With
            If FolderBrowserDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = DialogResult.OK Then
                txtDirectoryPath.Text = FolderBrowserDialog1.SelectedPath
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to browse the export directory path due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Private Sub Fill_Categories()
        With trvCategories
            .BeginUpdate()
            .Nodes.Clear()
            Dim trvNde As TreeNode
            Dim dtTemplates As DataTable = Nothing
            Dim objTemplateGallery As New clsTemplateGallery
            dtTemplates = objTemplateGallery.GetAllCategory
            objTemplateGallery.Dispose()
            objTemplateGallery = Nothing
            Dim nCount As Int16
            For nCount = 0 To dtTemplates.Rows.Count - 1
                trvNde = New TreeNode
                With trvNde
                    .Tag = dtTemplates.Rows(nCount).Item(0)
                    .Text = dtTemplates.Rows(nCount).Item(1)
                    .ImageIndex = 0
                    .SelectedImageIndex = 0
                End With
                .Nodes.Add(trvNde)
            Next
            .ExpandAll()
            .EndUpdate()
        End With
    End Sub
    Private Sub FillProviders()
        Dim dtProviders As DataTable
        Dim objTemplateGallery As New clsTemplateGallery
        dtProviders = objTemplateGallery.GetAllProvider()
        objTemplateGallery.Dispose()
        objTemplateGallery = Nothing
        If Not IsNothing(dtProviders) Then
            '' Here we add "All"(indicating All Doctors / Providers) 
            '' To datatable dt which contains Provider Name & ID's 

            Dim objrow As DataRow
            objrow = dtProviders.NewRow
            objrow.Item(0) = 0
            objrow.Item(1) = "All"
            dtProviders.Rows.Add(objrow)

            '' Attach DataSource to  CmbProvider 
            cmbProvider.DataSource = dtProviders
            cmbProvider.DisplayMember = dtProviders.Columns(1).ColumnName 'Provider Name
            cmbProvider.ValueMember = dtProviders.Columns(0).ColumnName 'Provider ID
            ''cmbProviders.SelectedValue = 0

            'cmbProvider.Text = gstrLoginProviderName
            ''Sandip Darade 20090505
            ''Select login provider
            cmbProvider.SelectedValue = gnLoginProviderID

        End If
    End Sub

    Private Sub frmExportTemplates_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(flxTemplates)
        Try
            Call Fill_Categories()
            Call FillProviders()
            Call DesignGrid()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to load the export template form due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Private Sub DesignGrid()
        With flxTemplates
            .Cols.Count = 5
            .Rows.Count = 1
            .Rows.Fixed = 1
            .SetData(0, COL_SELECT, "Select")
            .SetData(0, COL_TEMPLATEID, "Template ID")
            .SetData(0, COL_TEMPLATENAME, "Template Name")
            .SetData(0, COL_CATEGORY, "Category")
            .SetData(0, COL_PROVIDER, "Provider")

            .Cols(COL_SELECT).Width = 50
            .Cols(COL_TEMPLATEID).Width = 0
            .Cols(COL_TEMPLATENAME).Width = 200
            .Cols(COL_CATEGORY).Width = 200
            .Cols(COL_PROVIDER).Width = 200


            .Cols(COL_SELECT).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(COL_TEMPLATEID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(COL_TEMPLATENAME).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(COL_CATEGORY).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(COL_PROVIDER).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter


            .Cols(COL_SELECT).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
            .Cols(COL_TEMPLATEID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_TEMPLATENAME).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_CATEGORY).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
            .Cols(COL_PROVIDER).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
        End With
    End Sub
    Private Sub DocumentSelectClearAll(ByVal blnSelect As Boolean)
        'Select or clear rows for deletion
        Dim nCount As Int16
        For nCount = 1 To flxTemplates.Rows.Count - 1
            flxTemplates.Rows(nCount).Item(COL_SELECT) = blnSelect
        Next
        If flxTemplates.Rows.Count > 1 Then
            If ts_btnDocSelectAll.Visible = False And blnSelect = False Then
                ts_btnDocSelectAll.Visible = True
                ts_btnDocClearAll.Visible = False
            End If
            If ts_btnDocClearAll.Visible = False And blnSelect = True Then
                ts_btnDocSelectAll.Visible = False
                ts_btnDocClearAll.Visible = True
            End If
        End If

    End Sub

    Private Sub trvCategories_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvCategories.AfterCheck
        Try
            If (isSelect_Clear_All = False) Then
                Call Fill_Templates()
            End If
        Catch ex As Exception
            MessageBox.Show("Unable to fill the templates due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Private Sub Fill_Templates()
        Dim nSelectedCategoriesCount As Int16
        Dim dsAllTemplates As New DataSet
        Dim nCount As Int16
        Dim objTemplateGallery As New clsTemplateGallery
        Dim IsCheck As Boolean = False
        IsCategegory = True
        For nCount = 0 To trvCategories.GetNodeCount(True) - 1
            If trvCategories.Nodes(nCount).Checked Then
                nSelectedCategoriesCount = nSelectedCategoriesCount + 1
                dsAllTemplates.Merge(objTemplateGallery.GetAllTemplates(trvCategories.Nodes(nCount).Tag, cmbProvider.SelectedValue), True)

                IsCheck = True
            End If
        Next
        If (IsNothing(objTemplateGallery) = False) Then
            objTemplateGallery.Dispose()
            objTemplateGallery = Nothing
        End If
        'Dim Node As TreeNode = trvCategories.SelectedNode

        'If trvCategories.Nodes(nCount).Checked Then
        '    ts_btnDocClearAll.Visible = True
        '    ts_btnDocSelectAll.Visible = False
        'End If
        If IsCheck = False Then
            If ts_btnDocClearAll.Visible = True Then
                ts_btnDocClearAll.Visible = False
            End If
            If ts_btnDocSelectAll.Visible = True Then
                ts_btnDocSelectAll.Visible = False
            End If
        End If

        If nSelectedCategoriesCount >= 1 Then
            '  objTemplateGallery = Nothing
            With flxTemplates

                .Rows.Count = 1
                For nCount = 0 To dsAllTemplates.Tables(0).Rows.Count - 1
                    .Rows.Add()
                    .SetData(.Rows.Count - 1, COL_SELECT, False)
                    .SetData(.Rows.Count - 1, COL_TEMPLATEID, dsAllTemplates.Tables(0).Rows(nCount).Item(0))
                    .SetData(.Rows.Count - 1, COL_TEMPLATENAME, dsAllTemplates.Tables(0).Rows(nCount).Item(1))
                    .SetData(.Rows.Count - 1, COL_CATEGORY, dsAllTemplates.Tables(0).Rows(nCount).Item(2))
                    .SetData(.Rows.Count - 1, COL_PROVIDER, dsAllTemplates.Tables(0).Rows(nCount).Item(3))
                Next
                If dsAllTemplates.Tables(0).Rows.Count > 0 Then
                    ts_btnDocSelectAll.Visible = True
                    ts_btnDocClearAll.Visible = False
                End If
            End With
        Else
            Call DesignGrid()
        End If
        If nSelectedCategoriesCount >= 1 Then
            lblTemplatesSummary.Text = "  Total Templates=" & dsAllTemplates.Tables(0).Rows.Count
        Else
            lblTemplatesSummary.Text = "  Total Templates=0"
        End If
        IsCategegory = False
        dsAllTemplates.Dispose()
        dsAllTemplates = Nothing
    End Sub

    Private Sub cmbProvider_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProvider.SelectedIndexChanged
        Try
            Call Fill_Templates()
        Catch ex As Exception
            MessageBox.Show("Unable to fill the templates of the selected provider due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub ExportTemplate()

        Try
            If Trim(txtDirectoryPath.Text) = "" Then
                MessageBox.Show("Select the export directory.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDirectoryPath.Focus()
                Exit Sub
            End If
            If Directory.Exists(Trim(txtDirectoryPath.Text)) = False Then
                MessageBox.Show("Select the valid export directory path.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDirectoryPath.Focus()
                Exit Sub
            End If
            'SHUBHANGI
            If flxTemplates.Rows.Count <= 1 Then
                MessageBox.Show("Select at least one template category to export.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            'END
            'If MessageBox.Show("Are you sure, you want to export the selected templates?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub
            If flxTemplates.Rows.Count <= 0 Then Exit Sub
            Me.Cursor = Cursors.WaitCursor
            'Check any row is slected or not
            Dim nCount As Int16
            Dim strFileName As String
            Dim objFAX As New clsFAX
            Dim nTemplateID As Long
            Dim strCategoryName As String
            Dim objExam As New ClsReferralsDBLayer
            Dim strTemplatesWhichAreNotExported As String = ""
            For nCount = 1 To flxTemplates.Rows.Count - 1
                If CBool(flxTemplates.GetData(nCount, COL_SELECT)) Then
                    strFileName = ""
                    nTemplateID = flxTemplates.Rows(nCount).Item(COL_TEMPLATEID)
                    strCategoryName = flxTemplates.Rows(nCount).Item(COL_CATEGORY)
                    strCategoryName = ValidFileDirectoryName(strCategoryName)
                    If Directory.Exists(txtDirectoryPath.Text & "\" & strCategoryName) = False Then
                        Directory.CreateDirectory(txtDirectoryPath.Text & "\" & strCategoryName)
                    End If
                    'Retrieve the Contents and save the File
                    Dim dtData As DataTable = Nothing
                    Try
                        dtData = objExam.GetTemplate(flxTemplates.GetData(nCount, COL_TEMPLATEID))
                        Dim strDirName As String = ValidFileDirectoryName(dtData.Rows(0).Item(1))
                        strFileName = txtDirectoryPath.Text & "\" & strCategoryName & "\" & strDirName & ".docx"
                        If dtData.Rows.Count > 0 Then
                            Dim mstream As ADODB.Stream
                            mstream = New ADODB.Stream
                            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
                            mstream.Open()
                            mstream.Write(dtData.Rows(0).Item(0))
                            If File.Exists(strFileName) Then
                                Dim i As Int16 = 1
                                strFileName = txtDirectoryPath.Text & "\" & strCategoryName & "\" & strDirName & "-" & i & ".docx"
                                While File.Exists(strFileName) = True
                                    i = i + 1
                                    strFileName = txtDirectoryPath.Text & "\" & strCategoryName & "\" & strDirName & "-" & i & ".docx"
                                End While
                                mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
                            Else
                                mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
                            End If
                            mstream.Close()
                            mstream = Nothing
                            ''''''''Integrated by Mayuri:20100729 - to Export Liquid Data to XML 
                            If chkExportLiquidDt.Checked = True Then
                                If strFileName <> "" Then
                                    Try

                                        'wdTemplate.Open(strFileName)
                                        Dim oWordApp As Wd.Application = Nothing
                                        Dim strError As String = gloWord.LoadAndCloseWord.OpenDSO(wdTemplate, strFileName, oCurDoc, oWordApp)
                                        If (strError <> String.Empty) Then
                                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.Modify, strError, gloAuditTrail.ActivityOutCome.Failure)
                                            MessageBox.Show("Template cannot be open because there are problems with the contents.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            wdTemplate.CreateNew("Word.Document")
                                        Else
                                            ExportLiquidFields()
                                        End If

                                    Catch ex As Exception
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.Modify, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                                        MessageBox.Show("Template cannot be open because there are problems with the contents.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        wdTemplate.CreateNew("Word.Document")
                                    End Try

                                End If
                            End If
                            ''''''''Integrated by Mayuri:20100729 - to Export Liquid Data to XML 
                        End If
                        If (IsNothing(dtData) = False) Then
                            dtData.Dispose()
                            dtData = Nothing
                        End If
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        strTemplatesWhichAreNotExported = strTemplatesWhichAreNotExported & dtData.Rows(0).Item(1) & ","
                    End Try
                End If
            Next
            objExam.Dispose()
            objExam = Nothing
            If Trim(strTemplatesWhichAreNotExported) <> "" Then
                MessageBox.Show("Following Template(s) are not exported" & vbCrLf & strTemplatesWhichAreNotExported.Substring(0, strTemplatesWhichAreNotExported.Length - 1), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Else
                If (nTemplateID <> 0) Then
                    MessageBox.Show("Selected templates successfully exported.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Select at least one template to export.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

            End If

            frmExportTemplates_Load(Nothing, Nothing)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to export all the templates due to " & ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Public Sub ExportLiquidFields()
        ''''''''Added by Ujwala Atre as on 07262010 - to Export Liquid Data to XML 
        Dim oDB As New DataBaseLayer
        Try
            Dim IsItemVisited As Boolean = False
            Dim strValue() As String
            Dim m_elementId As Int64
            Dim ElementID As String = ""
            Dim dt As New DataTable
            Dim sSQL As String

            For Each _cntcontrol As Wd.ContentControl In oCurDoc.ContentControls
                If Not IsNothing(_cntcontrol.Tag) Then
                    IsItemVisited = False
                    strValue = _cntcontrol.Tag.Split("|")
                    If strValue.Length = 4 Then
                        m_elementId = CType(strValue(0), Int64)
                        If ElementID = "" Then

                            ElementID = m_elementId.ToString
                        Else
                            ElementID = ElementID & "," & m_elementId.ToString
                        End If
                    End If
                End If
            Next
            If ElementID <> "" Then
                'Problem : 00000163
                'Issue : When creating liquid data fields, the sorting does not order itself in a logical way (it's completely random). 
                'Change : Order By nSequenceNo clause added to retrieve data as per sequence maintained by user.
                sSQL = "select nElementID, sElementName, sElementType, bIsMandatory, nGroupID, nColumnID, sCategoryName, sItemName, nControlType, sAssociatedCategory, sAssociateditem, sAssociatedProperty FROM LiquidData_MST where nelementid in (" & ElementID & ") or ngroupid in (" & ElementID & ") ORDER BY nSequenceNo"
                dt = oDB.GetDataTable_Query(sSQL)
                If dt.Rows.Count > 0 Then
                    Dim fNM As String = oCurDoc.FullName.ToString
                    fNM = fNM.Replace(".docx", ".xml")
                    dt.WriteXml(fNM, False)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Export Template", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB = Nothing
        End Try
        ''''''''Added by Ujwala Atre as on 07262010 - to Export Liquid Data to XML 
    End Sub
    Private Sub wdTemplate_OnDocumentOpened(ByVal sender As Object, ByVal e As AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent) Handles wdTemplate.OnDocumentOpened

        Try
            oCurDoc = wdTemplate.ActiveDocument
            '    oWordApp = oCurDoc.Application
        Catch ex As Exception

        End Try

    End Sub
    ''Integrated by Mayuri:Export liquid data to XML:20100730
    Private Sub ExportLiquidData()

        Dim dt As New DataTable
        Dim sSQL As String
        Dim oDB As New DataBaseLayer
        Dim FullPath As String
        Try
            If Trim(txtDirectoryPath.Text) = "" Then
                MessageBox.Show("Select the export directory.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDirectoryPath.Focus()
                Exit Sub
            End If
            If Directory.Exists(txtDirectoryPath.Text) = False Then
                MessageBox.Show("Select the valid export directory path.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDirectoryPath.Focus()
                Exit Sub
            End If

            ''''''''''''''
            If Directory.Exists(txtDirectoryPath.Text & "\LiquidData") = False Then
                Directory.CreateDirectory(txtDirectoryPath.Text & "\LiquidData")
            End If
            ''''''''''''''      
            Me.Cursor = Cursors.WaitCursor

            For Each nd As TreeNode In trvCategories.Nodes
                If nd.Checked = True Then
                    'Problem : 00000163
                    'Issue : When creating liquid data fields, the sorting does not order itself in a logical way (it's completely random). 
                    'Change : Order By nSequenceNo clause added to retrieve data as per sequence maintained by user.
                    sSQL = "select nElementID, sElementName, sElementType, bIsMandatory, nGroupID, nColumnID, sCategoryName, sItemName, " _
                        & " nControlType, sAssociatedCategory, sAssociateditem, sAssociatedProperty FROM LiquidData_MST where nelementid =" & nd.Tag & " or ngroupid =" & nd.Tag & " ORDER BY nSequenceNo"
                    dt = oDB.GetDataTable_Query(sSQL)
                    FullPath = Trim(txtDirectoryPath.Text) & "\LiquidData\" & nd.Text.ToString & ".XML"
                    dt.WriteXml(FullPath, False)
                End If
            Next
            Me.Cursor = Cursors.Default
            If dt.Rows.Count > 0 Then
                MessageBox.Show("Liquid Data successfully exported.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Select at least one liquid data element to export.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

        Catch ex As Exception
        Finally
            oDB = Nothing
            dt = Nothing
        End Try

    End Sub


    Private Sub Fill_LiquidData()
        ''Integrated by Mayuri:Export liquid data to XML:20100730
        With trvCategories
            .BeginUpdate()
            .Nodes.Clear()
            Dim trvNde As TreeNode
            Dim dtLiquidDt As New DataTable
            Dim objTemplateGallery As New clsTemplateGallery
            ''''''''''''
            dtLiquidDt = objTemplateGallery.GetLiquidData
            objTemplateGallery.Dispose()
            objTemplateGallery = Nothing
            ''''''''''''
            Dim nCount As Int16
            For nCount = 0 To dtLiquidDt.Rows.Count - 1
                trvNde = New TreeNode
                With trvNde
                    .Tag = dtLiquidDt.Rows(nCount).Item(0)
                    .Text = dtLiquidDt.Rows(nCount).Item(1)
                    .ImageIndex = 0
                    .SelectedImageIndex = 0
                End With
                .Nodes.Add(trvNde)
            Next
            .ExpandAll()
            .EndUpdate()
        End With

    End Sub
    Private Sub rdbLiquidData_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbLiquidData.CheckedChanged
        ''Integrated by Mayuri:Export liquid data to XML:20100730
        If rdbLiquidData.Checked = True Then
            pnlProviderTop.Visible = False
            pnlTemplates.Visible = False
            chkExportLiquidDt.Visible = False
            Label1.Text = "Liquid Data Elements"
            pnlSimmaryTop.Visible = False
            '''''''''''''''
            Call Fill_LiquidData()
        Else
            pnlProviderTop.Visible = True
            pnlTemplates.Visible = True
            chkExportLiquidDt.Visible = True
            Label1.Text = "Templates Categories"
            pnlSimmaryTop.Visible = True
            '''''''''''''''
            Call Fill_Categories()
            Call FillProviders()
            Call DesignGrid()
        End If

    End Sub

    Private Sub rdbTemplates_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTemplates.CheckedChanged
        ''Integrated by Mayuri:Export liquid data to XML:20100730
        If rdbTemplates.Checked = True Then
            pnlProviderTop.Visible = True
            pnlTemplates.Visible = True
            chkExportLiquidDt.Visible = True
            Label1.Text = "Templates Categories"
            pnlSimmaryTop.Visible = True
            '''''''''''''''
            Call Fill_Categories()
            Call FillProviders()
            Call DesignGrid()
        Else
            pnlProviderTop.Visible = False
            pnlTemplates.Visible = False
            chkExportLiquidDt.Visible = False
            Label1.Text = "Liquid Data Elements"
            pnlSimmaryTop.Visible = False
            '''''''''''''''
            Call Fill_LiquidData()
        End If

    End Sub

    Private Sub rdbTemplates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTemplates.Click
        ''Integrated by Mayuri:Export liquid data to XML:20100730
        ' chkExportLiquidDt.Visible = True
        pnlProviderTop.Visible = True
        pnlTemplates.Visible = True
        chkExportLiquidDt.Visible = True
        Label1.Text = "Templates Categories"
        pnlSimmaryTop.Visible = True
        '''''''''''''''
        Call Fill_Categories()
        Call FillProviders()
        Call DesignGrid()

    End Sub

    Private Sub rdbLiquidData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbLiquidData.Click
        ''Integrated by Mayuri:Export liquid data to XML:20100730
        pnlProviderTop.Visible = False
        pnlTemplates.Visible = False
        chkExportLiquidDt.Visible = False
        Label1.Text = "Liquid Data Elements"
        pnlSimmaryTop.Visible = False
        '''''''''''''''
        Call Fill_LiquidData()

    End Sub
    ''End code Integrated by Mayuri:20100730

    Private Function ValidFileDirectoryName(ByVal strFileDirectoryName As String) As String
        ' Return Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(strFileDirectoryName, "\", " "), "/", " "), ":", " "), "*", " "), "?", " "), """", " "), "<", " "), ">", " "), "!", " ")
        Return [String].Join("", strFileDirectoryName.Split(Path.GetInvalidFileNameChars()))
    End Function


    Private Sub tls_ExportTemplate_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_ExportTemplate.ItemClicked
        Try
            Select Case e.ClickedItem.Tag

                Case "CategorySelectAll"
                    CategorySelectAll()

                Case "CategoryClearAll"
                    CategoryClearAll()

                Case "DocumentSelectAll"
                    DocumentSelectClearAll(True)

                Case "DocumentClearAll"
                    DocumentSelectClearAll(False)

                Case "ExportTemplate"
                    If rdbTemplates.Checked = True Then
                        ExportTemplate()
                    Else
                        ExportLiquidData()
                    End If

                Case "Close"
                    Me.Close()

            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub ts_btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnDocSelectAll.Click

    End Sub

    Private Sub flxTemplates_AfterSelChange(ByVal sender As Object, ByVal e As C1.Win.C1FlexGrid.RangeEventArgs) Handles flxTemplates.AfterSelChange

    End Sub

    Private Sub flxTemplates_EnterCell(ByVal sender As Object, ByVal e As System.EventArgs) Handles flxTemplates.EnterCell
        If flxTemplates.Col = 0 Then
            Dim isfound As Boolean = False
            With flxTemplates
                For i As Integer = 0 To flxTemplates.Rows.Count - 1
                    If .GetCellCheck(i, COL_SELECT) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                        isfound = True
                        Exit For
                    End If
                Next
                If isfound = False And ts_btnDocClearAll.Visible = True Then
                    ts_btnDocClearAll.Visible = False
                    ts_btnDocSelectAll.Visible = True
                End If

                isfound = False
                If ts_btnDocClearAll.Visible = True Then
                    If .GetCellCheck(.Row, COL_SELECT) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                        For i As Integer = 0 To .Rows.Count - 1
                            If i <> .Row Then
                                If .GetCellCheck(i, COL_SELECT) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                                    isfound = True
                                    Exit For
                                End If
                            End If
                        Next
                        If isfound = False Then
                            ts_btnDocClearAll.Visible = False
                            ts_btnDocSelectAll.Visible = True
                        End If
                    End If
                End If

                isfound = False
                If ts_btnDocSelectAll.Visible = True And IsCategegory = False Then
                    If .Row >= 0 Then
                        If .GetCellCheck(.Row, COL_SELECT) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                            For i As Integer = 0 To .Rows.Count - 1
                                If i <> .Row Then
                                    If .GetCellCheck(i, COL_SELECT) = C1.Win.C1FlexGrid.CheckEnum.Unchecked Then
                                        isfound = True
                                        Exit For
                                    End If
                                End If
                            Next
                            If isfound = False Then
                                ts_btnDocClearAll.Visible = True
                                ts_btnDocSelectAll.Visible = False
                            End If
                        End If
                    End If
                End If

            End With
        End If
    End Sub

    Private Sub flxTemplates_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles flxTemplates.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class
