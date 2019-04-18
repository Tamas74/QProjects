<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOutstandingOrdersAndResult
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                Dim dtpControls As DateTimePicker() = {dtUnsentOrdersTo, dtUnsentOrdersFrom, dtUnresultedTo, dtUnresultedFrom, dtUnacknowledgedTo,dtUnacknowledgedFrom, dtAllOrdersTo, dtAllOrdersFrom,dtUnfinishedOrderTo,dtUnfinishedOrderFrom}
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                    gloGlobal.cEventHelper.DisposeAllControls(dtpControls)
                Catch ex As Exception

                End Try



            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOutstandingOrdersAndResult))
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnOpen = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TabOutstandingOrders = New System.Windows.Forms.TabControl()
        Me.tabUnsentOrders = New System.Windows.Forms.TabPage()
        Me.pnlUnsentGrid = New System.Windows.Forms.Panel()
        Me.GridUnsentOrders = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pnlUnsentSearch = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtSearchUnsentOrders = New System.Windows.Forms.TextBox()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.btnClearUnsentOrdersSearch = New System.Windows.Forms.Button()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.pnlUnsentAging = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtUnsentOrdersTo = New System.Windows.Forms.DateTimePicker()
        Me.lblUnsentOrdersTo = New System.Windows.Forms.Label()
        Me.dtUnsentOrdersFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblUnsentOrdersFrom = New System.Windows.Forms.Label()
        Me.cmbUnsentOrdersDate = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlUnsentlabel = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label103 = New System.Windows.Forms.Label()
        Me.Label112 = New System.Windows.Forms.Label()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label115 = New System.Windows.Forms.Label()
        Me.Label114 = New System.Windows.Forms.Label()
        Me.Label106 = New System.Windows.Forms.Label()
        Me.tabUnresulted = New System.Windows.Forms.TabPage()
        Me.pnlUnresultedGrid = New System.Windows.Forms.Panel()
        Me.GridUnResultedOrders = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.pnlUnresultedSearch = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.txtSearchUnresulted = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label98 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.btnClearUnresultedSearch = New System.Windows.Forms.Button()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.pnlUnresultedAging = New System.Windows.Forms.Panel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.dtUnresultedTo = New System.Windows.Forms.DateTimePicker()
        Me.lblUnresultedTo = New System.Windows.Forms.Label()
        Me.dtUnresultedFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblUnresultedFrom = New System.Windows.Forms.Label()
        Me.cmbUnresultedDate = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.pnlUnresultedLabel = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Label133 = New System.Windows.Forms.Label()
        Me.Label134 = New System.Windows.Forms.Label()
        Me.Label105 = New System.Windows.Forms.Label()
        Me.Label135 = New System.Windows.Forms.Label()
        Me.Label104 = New System.Windows.Forms.Label()
        Me.Label136 = New System.Windows.Forms.Label()
        Me.tabUnacknowledged = New System.Windows.Forms.TabPage()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.GridUnAcknowledgedOrders = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.txtSearchUnacknowledged = New System.Windows.Forms.TextBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label99 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.btnClearUnacknowledgedSearch = New System.Windows.Forms.Button()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Label102 = New System.Windows.Forms.Label()
        Me.rdoUnacknowledgedTests = New System.Windows.Forms.RadioButton()
        Me.rdoResultedOrderDate = New System.Windows.Forms.RadioButton()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.dtUnacknowledgedTo = New System.Windows.Forms.DateTimePicker()
        Me.lblUnacknowledgedTo = New System.Windows.Forms.Label()
        Me.dtUnacknowledgedFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblUnacknowledgedFrom = New System.Windows.Forms.Label()
        Me.cmbUnacknowledgedDate = New System.Windows.Forms.ComboBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Label139 = New System.Windows.Forms.Label()
        Me.Label140 = New System.Windows.Forms.Label()
        Me.Label107 = New System.Windows.Forms.Label()
        Me.Label108 = New System.Windows.Forms.Label()
        Me.Label141 = New System.Windows.Forms.Label()
        Me.Label142 = New System.Windows.Forms.Label()
        Me.tabAllOrders = New System.Windows.Forms.TabPage()
        Me.Panel21 = New System.Windows.Forms.Panel()
        Me.GridAllOrdersOrders = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.Panel20 = New System.Windows.Forms.Panel()
        Me.txtSearchAllOrders = New System.Windows.Forms.TextBox()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.btnClearAllOrdersSearch = New System.Windows.Forms.Button()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.cmbAllOrdersStatus = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.dtAllOrdersTo = New System.Windows.Forms.DateTimePicker()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.dtAllOrdersFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.cmbAllOrderDate = New System.Windows.Forms.ComboBox()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel27 = New System.Windows.Forms.Panel()
        Me.Label145 = New System.Windows.Forms.Label()
        Me.Label146 = New System.Windows.Forms.Label()
        Me.Label147 = New System.Windows.Forms.Label()
        Me.Label110 = New System.Windows.Forms.Label()
        Me.Label148 = New System.Windows.Forms.Label()
        Me.tabUnfinishedOrderTemplates = New System.Windows.Forms.TabPage()
        Me.Panel26 = New System.Windows.Forms.Panel()
        Me.GridUnfinishedOrderTemplates = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.Label97 = New System.Windows.Forms.Label()
        Me.Panel23 = New System.Windows.Forms.Panel()
        Me.Panel24 = New System.Windows.Forms.Panel()
        Me.Panel25 = New System.Windows.Forms.Panel()
        Me.txtSearchUnfinishedOrders = New System.Windows.Forms.TextBox()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.Label101 = New System.Windows.Forms.Label()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.btnClearUnfinishedOrdersSearch = New System.Windows.Forms.Button()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.Label91 = New System.Windows.Forms.Label()
        Me.Label92 = New System.Windows.Forms.Label()
        Me.Label93 = New System.Windows.Forms.Label()
        Me.Panel22 = New System.Windows.Forms.Panel()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.dtUnfinishedOrderTo = New System.Windows.Forms.DateTimePicker()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.dtUnfinishedOrderFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.cmbUnfinishedOrderDate = New System.Windows.Forms.ComboBox()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel28 = New System.Windows.Forms.Panel()
        Me.Label109 = New System.Windows.Forms.Label()
        Me.Label151 = New System.Windows.Forms.Label()
        Me.Label152 = New System.Windows.Forms.Label()
        Me.Label111 = New System.Windows.Forms.Label()
        Me.Label153 = New System.Windows.Forms.Label()
        Me.Label154 = New System.Windows.Forms.Label()
        Me.pnlLeftTopTop = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbProviders = New System.Windows.Forms.ComboBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.ts_btnPrintPreview = New System.Windows.Forms.ToolStripButton()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TabOutstandingOrders.SuspendLayout()
        Me.tabUnsentOrders.SuspendLayout()
        Me.pnlUnsentGrid.SuspendLayout()
        CType(Me.GridUnsentOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlUnsentSearch.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.pnlUnsentAging.SuspendLayout()
        Me.pnlUnsentlabel.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.tabUnresulted.SuspendLayout()
        Me.pnlUnresultedGrid.SuspendLayout()
        CType(Me.GridUnResultedOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlUnresultedSearch.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.pnlUnresultedAging.SuspendLayout()
        Me.pnlUnresultedLabel.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.tabUnacknowledged.SuspendLayout()
        Me.Panel16.SuspendLayout()
        CType(Me.GridUnAcknowledgedOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel13.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.tabAllOrders.SuspendLayout()
        Me.Panel21.SuspendLayout()
        CType(Me.GridAllOrdersOrders, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel18.SuspendLayout()
        Me.Panel19.SuspendLayout()
        Me.Panel20.SuspendLayout()
        Me.Panel17.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel27.SuspendLayout()
        Me.tabUnfinishedOrderTemplates.SuspendLayout()
        Me.Panel26.SuspendLayout()
        CType(Me.GridUnfinishedOrderTemplates, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel23.SuspendLayout()
        Me.Panel24.SuspendLayout()
        Me.Panel25.SuspendLayout()
        Me.Panel22.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel28.SuspendLayout()
        Me.pnlLeftTopTop.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnOpen, Me.ts_btnPrint, Me.ts_btnPrintPreview, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(1065, 53)
        Me.ts_ViewButtons.TabIndex = 1
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnOpen
        '
        Me.ts_btnOpen.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnOpen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnOpen.Image = CType(resources.GetObject("ts_btnOpen.Image"), System.Drawing.Image)
        Me.ts_btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnOpen.Name = "ts_btnOpen"
        Me.ts_btnOpen.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnOpen.Tag = "Open"
        Me.ts_btnOpen.Text = "&Open"
        Me.ts_btnOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnPrint
        '
        Me.ts_btnPrint.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnPrint.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnPrint.Image = CType(resources.GetObject("ts_btnPrint.Image"), System.Drawing.Image)
        Me.ts_btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnPrint.Name = "ts_btnPrint"
        Me.ts_btnPrint.Size = New System.Drawing.Size(41, 50)
        Me.ts_btnPrint.Tag = "Print"
        Me.ts_btnPrint.Text = "&Print"
        Me.ts_btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnRefresh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 50)
        Me.ts_btnRefresh.Tag = "Refresh"
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        'Panel2
        '
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.TabOutstandingOrders)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 89)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1065, 415)
        Me.Panel2.TabIndex = 6
        '
        'TabOutstandingOrders
        '
        Me.TabOutstandingOrders.Controls.Add(Me.tabUnsentOrders)
        Me.TabOutstandingOrders.Controls.Add(Me.tabUnresulted)
        Me.TabOutstandingOrders.Controls.Add(Me.tabUnacknowledged)
        Me.TabOutstandingOrders.Controls.Add(Me.tabAllOrders)
        Me.TabOutstandingOrders.Controls.Add(Me.tabUnfinishedOrderTemplates)
        Me.TabOutstandingOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabOutstandingOrders.Location = New System.Drawing.Point(0, 0)
        Me.TabOutstandingOrders.Name = "TabOutstandingOrders"
        Me.TabOutstandingOrders.Padding = New System.Drawing.Point(8, 5)
        Me.TabOutstandingOrders.SelectedIndex = 0
        Me.TabOutstandingOrders.Size = New System.Drawing.Size(1065, 415)
        Me.TabOutstandingOrders.TabIndex = 15
        '
        'tabUnsentOrders
        '
        Me.tabUnsentOrders.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tabUnsentOrders.Controls.Add(Me.pnlUnsentGrid)
        Me.tabUnsentOrders.Controls.Add(Me.pnlUnsentSearch)
        Me.tabUnsentOrders.Controls.Add(Me.pnlUnsentAging)
        Me.tabUnsentOrders.Controls.Add(Me.pnlUnsentlabel)
        Me.tabUnsentOrders.Location = New System.Drawing.Point(4, 27)
        Me.tabUnsentOrders.Name = "tabUnsentOrders"
        Me.tabUnsentOrders.Size = New System.Drawing.Size(1057, 384)
        Me.tabUnsentOrders.TabIndex = 1
        Me.tabUnsentOrders.Text = " Unsent Orders "
        '
        'pnlUnsentGrid
        '
        Me.pnlUnsentGrid.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlUnsentGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnlUnsentGrid.Controls.Add(Me.GridUnsentOrders)
        Me.pnlUnsentGrid.Controls.Add(Me.Label9)
        Me.pnlUnsentGrid.Controls.Add(Me.Label8)
        Me.pnlUnsentGrid.Controls.Add(Me.Label7)
        Me.pnlUnsentGrid.Controls.Add(Me.Label6)
        Me.pnlUnsentGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlUnsentGrid.Location = New System.Drawing.Point(0, 142)
        Me.pnlUnsentGrid.Name = "pnlUnsentGrid"
        Me.pnlUnsentGrid.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlUnsentGrid.Size = New System.Drawing.Size(1057, 242)
        Me.pnlUnsentGrid.TabIndex = 22
        '
        'GridUnsentOrders
        '
        Me.GridUnsentOrders.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.GridUnsentOrders.AllowEditing = False
        Me.GridUnsentOrders.AutoGenerateColumns = False
        Me.GridUnsentOrders.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridUnsentOrders.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.GridUnsentOrders.ColumnInfo = resources.GetString("GridUnsentOrders.ColumnInfo")
        Me.GridUnsentOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridUnsentOrders.ExtendLastCol = True
        Me.GridUnsentOrders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridUnsentOrders.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GridUnsentOrders.Location = New System.Drawing.Point(4, 1)
        Me.GridUnsentOrders.Name = "GridUnsentOrders"
        Me.GridUnsentOrders.Rows.Count = 13
        Me.GridUnsentOrders.Rows.DefaultSize = 19
        Me.GridUnsentOrders.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.GridUnsentOrders.Size = New System.Drawing.Size(1049, 237)
        Me.GridUnsentOrders.StyleInfo = resources.GetString("GridUnsentOrders.StyleInfo")
        Me.GridUnsentOrders.TabIndex = 22
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Enabled = False
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(4, 238)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1049, 1)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "From"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Enabled = False
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1049, 1)
        Me.Label8.TabIndex = 25
        Me.Label8.Text = "From"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Enabled = False
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(1053, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 239)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "From"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Enabled = False
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(3, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 239)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "From"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlUnsentSearch
        '
        Me.pnlUnsentSearch.Controls.Add(Me.Panel3)
        Me.pnlUnsentSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUnsentSearch.Location = New System.Drawing.Point(0, 114)
        Me.pnlUnsentSearch.Name = "pnlUnsentSearch"
        Me.pnlUnsentSearch.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlUnsentSearch.Size = New System.Drawing.Size(1057, 28)
        Me.pnlUnsentSearch.TabIndex = 25
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.pnlSearch)
        Me.Panel3.Controls.Add(Me.Label14)
        Me.Panel3.Controls.Add(Me.Label15)
        Me.Panel3.Controls.Add(Me.Label16)
        Me.Panel3.Controls.Add(Me.Label17)
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(3, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1051, 25)
        Me.Panel3.TabIndex = 24
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearch.Controls.Add(Me.txtSearchUnsentOrders)
        Me.pnlSearch.Controls.Add(Me.Label77)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlSearch.Controls.Add(Me.btnClearUnsentOrdersSearch)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(69, 1)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(241, 23)
        Me.pnlSearch.TabIndex = 23
        '
        'txtSearchUnsentOrders
        '
        Me.txtSearchUnsentOrders.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchUnsentOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchUnsentOrders.Location = New System.Drawing.Point(5, 3)
        Me.txtSearchUnsentOrders.Name = "txtSearchUnsentOrders"
        Me.txtSearchUnsentOrders.Size = New System.Drawing.Size(214, 15)
        Me.txtSearchUnsentOrders.TabIndex = 42
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.White
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label77.Location = New System.Drawing.Point(5, 18)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(214, 5)
        Me.Label77.TabIndex = 43
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(5, 0)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(214, 3)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(1, 0)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(4, 23)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'btnClearUnsentOrdersSearch
        '
        Me.btnClearUnsentOrdersSearch.BackgroundImage = CType(resources.GetObject("btnClearUnsentOrdersSearch.BackgroundImage"), System.Drawing.Image)
        Me.btnClearUnsentOrdersSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearUnsentOrdersSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearUnsentOrdersSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearUnsentOrdersSearch.FlatAppearance.BorderSize = 0
        Me.btnClearUnsentOrdersSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearUnsentOrdersSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearUnsentOrdersSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearUnsentOrdersSearch.Image = CType(resources.GetObject("btnClearUnsentOrdersSearch.Image"), System.Drawing.Image)
        Me.btnClearUnsentOrdersSearch.Location = New System.Drawing.Point(219, 0)
        Me.btnClearUnsentOrdersSearch.Name = "btnClearUnsentOrdersSearch"
        Me.btnClearUnsentOrdersSearch.Size = New System.Drawing.Size(21, 23)
        Me.btnClearUnsentOrdersSearch.TabIndex = 41
        Me.btnClearUnsentOrdersSearch.UseVisualStyleBackColor = True
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(240, 0)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Enabled = False
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(1050, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 23)
        Me.Label14.TabIndex = 24
        Me.Label14.Text = "From"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(1, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(68, 23)
        Me.Label15.TabIndex = 15
        Me.Label15.Text = "  Search :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Enabled = False
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(0, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 23)
        Me.Label16.TabIndex = 23
        Me.Label16.Text = "From"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Enabled = False
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(0, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1051, 1)
        Me.Label17.TabIndex = 25
        Me.Label17.Text = "From"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label18.Enabled = False
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(0, 24)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1051, 1)
        Me.Label18.TabIndex = 26
        Me.Label18.Text = "From"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlUnsentAging
        '
        Me.pnlUnsentAging.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlUnsentAging.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnlUnsentAging.Controls.Add(Me.Label5)
        Me.pnlUnsentAging.Controls.Add(Me.dtUnsentOrdersTo)
        Me.pnlUnsentAging.Controls.Add(Me.lblUnsentOrdersTo)
        Me.pnlUnsentAging.Controls.Add(Me.dtUnsentOrdersFrom)
        Me.pnlUnsentAging.Controls.Add(Me.lblUnsentOrdersFrom)
        Me.pnlUnsentAging.Controls.Add(Me.cmbUnsentOrdersDate)
        Me.pnlUnsentAging.Controls.Add(Me.Label3)
        Me.pnlUnsentAging.Controls.Add(Me.Label1)
        Me.pnlUnsentAging.Controls.Add(Me.Label2)
        Me.pnlUnsentAging.Controls.Add(Me.Label4)
        Me.pnlUnsentAging.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUnsentAging.Location = New System.Drawing.Point(0, 74)
        Me.pnlUnsentAging.Name = "pnlUnsentAging"
        Me.pnlUnsentAging.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlUnsentAging.Size = New System.Drawing.Size(1057, 40)
        Me.pnlUnsentAging.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Enabled = False
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(1053, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 32)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "From"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtUnsentOrdersTo
        '
        Me.dtUnsentOrdersTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtUnsentOrdersTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtUnsentOrdersTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtUnsentOrdersTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtUnsentOrdersTo.CustomFormat = "MM/dd/yyyy"
        Me.dtUnsentOrdersTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtUnsentOrdersTo.Location = New System.Drawing.Point(405, 9)
        Me.dtUnsentOrdersTo.Name = "dtUnsentOrdersTo"
        Me.dtUnsentOrdersTo.Size = New System.Drawing.Size(88, 22)
        Me.dtUnsentOrdersTo.TabIndex = 17
        '
        'lblUnsentOrdersTo
        '
        Me.lblUnsentOrdersTo.AutoSize = True
        Me.lblUnsentOrdersTo.BackColor = System.Drawing.Color.Transparent
        Me.lblUnsentOrdersTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnsentOrdersTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblUnsentOrdersTo.Location = New System.Drawing.Point(374, 13)
        Me.lblUnsentOrdersTo.Name = "lblUnsentOrdersTo"
        Me.lblUnsentOrdersTo.Size = New System.Drawing.Size(30, 14)
        Me.lblUnsentOrdersTo.TabIndex = 18
        Me.lblUnsentOrdersTo.Text = "To :"
        Me.lblUnsentOrdersTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtUnsentOrdersFrom
        '
        Me.dtUnsentOrdersFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtUnsentOrdersFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtUnsentOrdersFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtUnsentOrdersFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtUnsentOrdersFrom.CustomFormat = "MM/dd/yyyy"
        Me.dtUnsentOrdersFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtUnsentOrdersFrom.Location = New System.Drawing.Point(268, 9)
        Me.dtUnsentOrdersFrom.Name = "dtUnsentOrdersFrom"
        Me.dtUnsentOrdersFrom.Size = New System.Drawing.Size(87, 22)
        Me.dtUnsentOrdersFrom.TabIndex = 16
        '
        'lblUnsentOrdersFrom
        '
        Me.lblUnsentOrdersFrom.AutoSize = True
        Me.lblUnsentOrdersFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblUnsentOrdersFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnsentOrdersFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblUnsentOrdersFrom.Location = New System.Drawing.Point(221, 13)
        Me.lblUnsentOrdersFrom.Name = "lblUnsentOrdersFrom"
        Me.lblUnsentOrdersFrom.Size = New System.Drawing.Size(42, 14)
        Me.lblUnsentOrdersFrom.TabIndex = 19
        Me.lblUnsentOrdersFrom.Text = "From :"
        Me.lblUnsentOrdersFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbUnsentOrdersDate
        '
        Me.cmbUnsentOrdersDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnsentOrdersDate.ForeColor = System.Drawing.Color.Black
        Me.cmbUnsentOrdersDate.Items.AddRange(New Object() {"Today", "Yesterday", "Last 7 Days", "This Month", "This Year", "All Time"})
        Me.cmbUnsentOrdersDate.Location = New System.Drawing.Point(70, 9)
        Me.cmbUnsentOrdersDate.Name = "cmbUnsentOrdersDate"
        Me.cmbUnsentOrdersDate.Size = New System.Drawing.Size(123, 22)
        Me.cmbUnsentOrdersDate.TabIndex = 14
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label3.Location = New System.Drawing.Point(4, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 32)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "  Aging :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Enabled = False
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(4, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1050, 1)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "From"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Enabled = False
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(4, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1050, 1)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "From"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Enabled = False
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 34)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "From"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlUnsentlabel
        '
        Me.pnlUnsentlabel.BackColor = System.Drawing.Color.Transparent
        Me.pnlUnsentlabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnlUnsentlabel.Controls.Add(Me.Panel7)
        Me.pnlUnsentlabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUnsentlabel.Location = New System.Drawing.Point(0, 0)
        Me.pnlUnsentlabel.Name = "pnlUnsentlabel"
        Me.pnlUnsentlabel.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.pnlUnsentlabel.Size = New System.Drawing.Size(1057, 74)
        Me.pnlUnsentlabel.TabIndex = 26
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel7.Controls.Add(Me.Label103)
        Me.Panel7.Controls.Add(Me.Label112)
        Me.Panel7.Controls.Add(Me.Label113)
        Me.Panel7.Controls.Add(Me.Label115)
        Me.Panel7.Controls.Add(Me.Label114)
        Me.Panel7.Controls.Add(Me.Label106)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(3, 3)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(1051, 71)
        Me.Panel7.TabIndex = 31
        '
        'Label103
        '
        Me.Label103.AutoSize = True
        Me.Label103.BackColor = System.Drawing.Color.Transparent
        Me.Label103.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label103.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label103.ForeColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label103.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label103.Location = New System.Drawing.Point(7, 38)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(372, 14)
        Me.Label103.TabIndex = 16
        Me.Label103.Text = "Tip: Remember to make sure sent Orders have the correct status."
        Me.Label103.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label112
        '
        Me.Label112.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label112.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label112.Enabled = False
        Me.Label112.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label112.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label112.Location = New System.Drawing.Point(1, 70)
        Me.Label112.Name = "Label112"
        Me.Label112.Size = New System.Drawing.Size(1049, 1)
        Me.Label112.TabIndex = 27
        Me.Label112.Text = "From"
        Me.Label112.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label113
        '
        Me.Label113.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label113.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label113.Enabled = False
        Me.Label113.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label113.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label113.Location = New System.Drawing.Point(1, 0)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(1049, 1)
        Me.Label113.TabIndex = 28
        Me.Label113.Text = "From"
        Me.Label113.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label115
        '
        Me.Label115.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label115.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label115.Enabled = False
        Me.Label115.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label115.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label115.Location = New System.Drawing.Point(0, 0)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(1, 71)
        Me.Label115.TabIndex = 30
        Me.Label115.Text = "From"
        Me.Label115.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label114
        '
        Me.Label114.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label114.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label114.Enabled = False
        Me.Label114.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label114.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label114.Location = New System.Drawing.Point(1050, 0)
        Me.Label114.Name = "Label114"
        Me.Label114.Size = New System.Drawing.Size(1, 71)
        Me.Label114.TabIndex = 29
        Me.Label114.Text = "From"
        Me.Label114.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label106
        '
        Me.Label106.AutoSize = True
        Me.Label106.BackColor = System.Drawing.Color.Transparent
        Me.Label106.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label106.Font = New System.Drawing.Font("Tahoma", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label106.ForeColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label106.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label106.Location = New System.Drawing.Point(7, 10)
        Me.Label106.Name = "Label106"
        Me.Label106.Size = New System.Drawing.Size(444, 14)
        Me.Label106.TabIndex = 15
        Me.Label106.Text = "Use this display to find new Orders that need to be prepared and sent."
        Me.Label106.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tabUnresulted
        '
        Me.tabUnresulted.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tabUnresulted.Controls.Add(Me.pnlUnresultedGrid)
        Me.tabUnresulted.Controls.Add(Me.pnlUnresultedSearch)
        Me.tabUnresulted.Controls.Add(Me.pnlUnresultedAging)
        Me.tabUnresulted.Controls.Add(Me.pnlUnresultedLabel)
        Me.tabUnresulted.Location = New System.Drawing.Point(4, 26)
        Me.tabUnresulted.Name = "tabUnresulted"
        Me.tabUnresulted.Size = New System.Drawing.Size(1057, 385)
        Me.tabUnresulted.TabIndex = 2
        Me.tabUnresulted.Text = " Unresulted "
        '
        'pnlUnresultedGrid
        '
        Me.pnlUnresultedGrid.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlUnresultedGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnlUnresultedGrid.Controls.Add(Me.GridUnResultedOrders)
        Me.pnlUnresultedGrid.Controls.Add(Me.Label36)
        Me.pnlUnresultedGrid.Controls.Add(Me.Label37)
        Me.pnlUnresultedGrid.Controls.Add(Me.Label38)
        Me.pnlUnresultedGrid.Controls.Add(Me.Label39)
        Me.pnlUnresultedGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlUnresultedGrid.Location = New System.Drawing.Point(0, 142)
        Me.pnlUnresultedGrid.Name = "pnlUnresultedGrid"
        Me.pnlUnresultedGrid.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlUnresultedGrid.Size = New System.Drawing.Size(1057, 243)
        Me.pnlUnresultedGrid.TabIndex = 27
        '
        'GridUnResultedOrders
        '
        Me.GridUnResultedOrders.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.GridUnResultedOrders.AllowEditing = False
        Me.GridUnResultedOrders.AutoGenerateColumns = False
        Me.GridUnResultedOrders.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridUnResultedOrders.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.GridUnResultedOrders.ColumnInfo = resources.GetString("GridUnResultedOrders.ColumnInfo")
        Me.GridUnResultedOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridUnResultedOrders.ExtendLastCol = True
        Me.GridUnResultedOrders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridUnResultedOrders.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GridUnResultedOrders.Location = New System.Drawing.Point(4, 1)
        Me.GridUnResultedOrders.Name = "GridUnResultedOrders"
        Me.GridUnResultedOrders.Rows.Count = 13
        Me.GridUnResultedOrders.Rows.DefaultSize = 19
        Me.GridUnResultedOrders.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.GridUnResultedOrders.Size = New System.Drawing.Size(1049, 238)
        Me.GridUnResultedOrders.StyleInfo = resources.GetString("GridUnResultedOrders.StyleInfo")
        Me.GridUnResultedOrders.TabIndex = 22
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label36.Enabled = False
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Location = New System.Drawing.Point(4, 239)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(1049, 1)
        Me.Label36.TabIndex = 26
        Me.Label36.Text = "From"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label37.Enabled = False
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Location = New System.Drawing.Point(4, 0)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(1049, 1)
        Me.Label37.TabIndex = 25
        Me.Label37.Text = "From"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label38.Enabled = False
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Location = New System.Drawing.Point(1053, 0)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(1, 240)
        Me.Label38.TabIndex = 24
        Me.Label38.Text = "From"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label39.Enabled = False
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Location = New System.Drawing.Point(3, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(1, 240)
        Me.Label39.TabIndex = 23
        Me.Label39.Text = "From"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlUnresultedSearch
        '
        Me.pnlUnresultedSearch.Controls.Add(Me.Panel8)
        Me.pnlUnresultedSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUnresultedSearch.Location = New System.Drawing.Point(0, 114)
        Me.pnlUnresultedSearch.Name = "pnlUnresultedSearch"
        Me.pnlUnresultedSearch.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlUnresultedSearch.Size = New System.Drawing.Size(1057, 28)
        Me.pnlUnresultedSearch.TabIndex = 26
        '
        'Panel8
        '
        Me.Panel8.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.Panel9)
        Me.Panel8.Controls.Add(Me.Label31)
        Me.Panel8.Controls.Add(Me.Label32)
        Me.Panel8.Controls.Add(Me.Label33)
        Me.Panel8.Controls.Add(Me.Label34)
        Me.Panel8.Controls.Add(Me.Label35)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(3, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1051, 25)
        Me.Panel8.TabIndex = 24
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.Transparent
        Me.Panel9.Controls.Add(Me.txtSearchUnresulted)
        Me.Panel9.Controls.Add(Me.Label28)
        Me.Panel9.Controls.Add(Me.Label98)
        Me.Panel9.Controls.Add(Me.Label27)
        Me.Panel9.Controls.Add(Me.btnClearUnresultedSearch)
        Me.Panel9.Controls.Add(Me.Label29)
        Me.Panel9.Controls.Add(Me.Label30)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel9.ForeColor = System.Drawing.Color.Black
        Me.Panel9.Location = New System.Drawing.Point(69, 1)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(241, 23)
        Me.Panel9.TabIndex = 23
        '
        'txtSearchUnresulted
        '
        Me.txtSearchUnresulted.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchUnresulted.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchUnresulted.Location = New System.Drawing.Point(5, 3)
        Me.txtSearchUnresulted.Name = "txtSearchUnresulted"
        Me.txtSearchUnresulted.Size = New System.Drawing.Size(214, 15)
        Me.txtSearchUnresulted.TabIndex = 42
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.White
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label28.Location = New System.Drawing.Point(5, 18)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(214, 5)
        Me.Label28.TabIndex = 45
        '
        'Label98
        '
        Me.Label98.BackColor = System.Drawing.Color.White
        Me.Label98.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label98.Location = New System.Drawing.Point(1, 3)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(4, 20)
        Me.Label98.TabIndex = 44
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.White
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label27.Location = New System.Drawing.Point(1, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(218, 3)
        Me.Label27.TabIndex = 37
        '
        'btnClearUnresultedSearch
        '
        Me.btnClearUnresultedSearch.BackgroundImage = CType(resources.GetObject("btnClearUnresultedSearch.BackgroundImage"), System.Drawing.Image)
        Me.btnClearUnresultedSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearUnresultedSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearUnresultedSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearUnresultedSearch.FlatAppearance.BorderSize = 0
        Me.btnClearUnresultedSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearUnresultedSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearUnresultedSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearUnresultedSearch.Image = CType(resources.GetObject("btnClearUnresultedSearch.Image"), System.Drawing.Image)
        Me.btnClearUnresultedSearch.Location = New System.Drawing.Point(219, 0)
        Me.btnClearUnresultedSearch.Name = "btnClearUnresultedSearch"
        Me.btnClearUnresultedSearch.Size = New System.Drawing.Size(21, 23)
        Me.btnClearUnresultedSearch.TabIndex = 41
        Me.btnClearUnresultedSearch.UseVisualStyleBackColor = True
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label29.Location = New System.Drawing.Point(0, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 23)
        Me.Label29.TabIndex = 39
        Me.Label29.Text = "label4"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label30.Location = New System.Drawing.Point(240, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(1, 23)
        Me.Label30.TabIndex = 40
        Me.Label30.Text = "label4"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label31.Enabled = False
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Location = New System.Drawing.Point(1050, 1)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(1, 23)
        Me.Label31.TabIndex = 24
        Me.Label31.Text = "From"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label32.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Location = New System.Drawing.Point(1, 1)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(68, 23)
        Me.Label32.TabIndex = 15
        Me.Label32.Text = "  Search :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label33.Enabled = False
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Location = New System.Drawing.Point(0, 1)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(1, 23)
        Me.Label33.TabIndex = 23
        Me.Label33.Text = "From"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label34.Enabled = False
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Location = New System.Drawing.Point(0, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(1051, 1)
        Me.Label34.TabIndex = 25
        Me.Label34.Text = "From"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label35.Enabled = False
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Location = New System.Drawing.Point(0, 24)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(1051, 1)
        Me.Label35.TabIndex = 26
        Me.Label35.Text = "From"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlUnresultedAging
        '
        Me.pnlUnresultedAging.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlUnresultedAging.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnlUnresultedAging.Controls.Add(Me.Label19)
        Me.pnlUnresultedAging.Controls.Add(Me.dtUnresultedTo)
        Me.pnlUnresultedAging.Controls.Add(Me.lblUnresultedTo)
        Me.pnlUnresultedAging.Controls.Add(Me.dtUnresultedFrom)
        Me.pnlUnresultedAging.Controls.Add(Me.lblUnresultedFrom)
        Me.pnlUnresultedAging.Controls.Add(Me.cmbUnresultedDate)
        Me.pnlUnresultedAging.Controls.Add(Me.Label22)
        Me.pnlUnresultedAging.Controls.Add(Me.Label23)
        Me.pnlUnresultedAging.Controls.Add(Me.Label24)
        Me.pnlUnresultedAging.Controls.Add(Me.Label25)
        Me.pnlUnresultedAging.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUnresultedAging.Location = New System.Drawing.Point(0, 74)
        Me.pnlUnresultedAging.Name = "pnlUnresultedAging"
        Me.pnlUnresultedAging.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlUnresultedAging.Size = New System.Drawing.Size(1057, 40)
        Me.pnlUnresultedAging.TabIndex = 6
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Enabled = False
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Location = New System.Drawing.Point(1053, 4)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 32)
        Me.Label19.TabIndex = 23
        Me.Label19.Text = "From"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtUnresultedTo
        '
        Me.dtUnresultedTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtUnresultedTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtUnresultedTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtUnresultedTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtUnresultedTo.CustomFormat = "MM/dd/yyyy"
        Me.dtUnresultedTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtUnresultedTo.Location = New System.Drawing.Point(405, 9)
        Me.dtUnresultedTo.Name = "dtUnresultedTo"
        Me.dtUnresultedTo.Size = New System.Drawing.Size(88, 22)
        Me.dtUnresultedTo.TabIndex = 17
        '
        'lblUnresultedTo
        '
        Me.lblUnresultedTo.AutoSize = True
        Me.lblUnresultedTo.BackColor = System.Drawing.Color.Transparent
        Me.lblUnresultedTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnresultedTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblUnresultedTo.Location = New System.Drawing.Point(374, 13)
        Me.lblUnresultedTo.Name = "lblUnresultedTo"
        Me.lblUnresultedTo.Size = New System.Drawing.Size(30, 14)
        Me.lblUnresultedTo.TabIndex = 18
        Me.lblUnresultedTo.Text = "To :"
        Me.lblUnresultedTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtUnresultedFrom
        '
        Me.dtUnresultedFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtUnresultedFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtUnresultedFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtUnresultedFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtUnresultedFrom.CustomFormat = "MM/dd/yyyy"
        Me.dtUnresultedFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtUnresultedFrom.Location = New System.Drawing.Point(268, 9)
        Me.dtUnresultedFrom.Name = "dtUnresultedFrom"
        Me.dtUnresultedFrom.Size = New System.Drawing.Size(87, 22)
        Me.dtUnresultedFrom.TabIndex = 16
        '
        'lblUnresultedFrom
        '
        Me.lblUnresultedFrom.AutoSize = True
        Me.lblUnresultedFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblUnresultedFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnresultedFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblUnresultedFrom.Location = New System.Drawing.Point(221, 13)
        Me.lblUnresultedFrom.Name = "lblUnresultedFrom"
        Me.lblUnresultedFrom.Size = New System.Drawing.Size(42, 14)
        Me.lblUnresultedFrom.TabIndex = 19
        Me.lblUnresultedFrom.Text = "From :"
        Me.lblUnresultedFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbUnresultedDate
        '
        Me.cmbUnresultedDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnresultedDate.ForeColor = System.Drawing.Color.Black
        Me.cmbUnresultedDate.Items.AddRange(New Object() {"0 - 15 Days", "16 - 60 Days", "61 - 120 Days", "120 + Days"})
        Me.cmbUnresultedDate.Location = New System.Drawing.Point(70, 9)
        Me.cmbUnresultedDate.Name = "cmbUnresultedDate"
        Me.cmbUnresultedDate.Size = New System.Drawing.Size(123, 22)
        Me.cmbUnresultedDate.TabIndex = 14
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label22.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label22.Location = New System.Drawing.Point(4, 4)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(62, 32)
        Me.Label22.TabIndex = 15
        Me.Label22.Text = "  Aging :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label23.Enabled = False
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Location = New System.Drawing.Point(4, 3)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1050, 1)
        Me.Label23.TabIndex = 20
        Me.Label23.Text = "From"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label24.Enabled = False
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Location = New System.Drawing.Point(4, 36)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1050, 1)
        Me.Label24.TabIndex = 21
        Me.Label24.Text = "From"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label25.Enabled = False
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Location = New System.Drawing.Point(3, 3)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 34)
        Me.Label25.TabIndex = 22
        Me.Label25.Text = "From"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlUnresultedLabel
        '
        Me.pnlUnresultedLabel.BackColor = System.Drawing.Color.Transparent
        Me.pnlUnresultedLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnlUnresultedLabel.Controls.Add(Me.Panel10)
        Me.pnlUnresultedLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUnresultedLabel.Location = New System.Drawing.Point(0, 0)
        Me.pnlUnresultedLabel.Name = "pnlUnresultedLabel"
        Me.pnlUnresultedLabel.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.pnlUnresultedLabel.Size = New System.Drawing.Size(1057, 74)
        Me.pnlUnresultedLabel.TabIndex = 28
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel10.Controls.Add(Me.Label133)
        Me.Panel10.Controls.Add(Me.Label134)
        Me.Panel10.Controls.Add(Me.Label105)
        Me.Panel10.Controls.Add(Me.Label135)
        Me.Panel10.Controls.Add(Me.Label104)
        Me.Panel10.Controls.Add(Me.Label136)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel10.Location = New System.Drawing.Point(3, 3)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(1051, 71)
        Me.Panel10.TabIndex = 32
        '
        'Label133
        '
        Me.Label133.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label133.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label133.Enabled = False
        Me.Label133.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label133.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label133.Location = New System.Drawing.Point(1, 70)
        Me.Label133.Name = "Label133"
        Me.Label133.Size = New System.Drawing.Size(1049, 1)
        Me.Label133.TabIndex = 27
        Me.Label133.Text = "From"
        Me.Label133.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label134
        '
        Me.Label134.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label134.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label134.Enabled = False
        Me.Label134.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label134.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label134.Location = New System.Drawing.Point(1, 0)
        Me.Label134.Name = "Label134"
        Me.Label134.Size = New System.Drawing.Size(1049, 1)
        Me.Label134.TabIndex = 28
        Me.Label134.Text = "From"
        Me.Label134.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label105
        '
        Me.Label105.AutoSize = True
        Me.Label105.BackColor = System.Drawing.Color.Transparent
        Me.Label105.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label105.Font = New System.Drawing.Font("Tahoma", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label105.ForeColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label105.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label105.Location = New System.Drawing.Point(7, 10)
        Me.Label105.Name = "Label105"
        Me.Label105.Size = New System.Drawing.Size(459, 14)
        Me.Label105.TabIndex = 15
        Me.Label105.Text = "Use this display to monitor sent Order Tests that have not been resulted."
        Me.Label105.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label135
        '
        Me.Label135.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label135.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label135.Enabled = False
        Me.Label135.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label135.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label135.Location = New System.Drawing.Point(0, 0)
        Me.Label135.Name = "Label135"
        Me.Label135.Size = New System.Drawing.Size(1, 71)
        Me.Label135.TabIndex = 30
        Me.Label135.Text = "From"
        Me.Label135.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label104
        '
        Me.Label104.AutoSize = True
        Me.Label104.BackColor = System.Drawing.Color.Transparent
        Me.Label104.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label104.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label104.ForeColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label104.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label104.Location = New System.Drawing.Point(7, 38)
        Me.Label104.Name = "Label104"
        Me.Label104.Size = New System.Drawing.Size(286, 14)
        Me.Label104.TabIndex = 16
        Me.Label104.Text = "Tip: Investigate Tests that are overdue for results."
        Me.Label104.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label136
        '
        Me.Label136.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label136.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label136.Enabled = False
        Me.Label136.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label136.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label136.Location = New System.Drawing.Point(1050, 0)
        Me.Label136.Name = "Label136"
        Me.Label136.Size = New System.Drawing.Size(1, 71)
        Me.Label136.TabIndex = 29
        Me.Label136.Text = "From"
        Me.Label136.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tabUnacknowledged
        '
        Me.tabUnacknowledged.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tabUnacknowledged.Controls.Add(Me.Panel16)
        Me.tabUnacknowledged.Controls.Add(Me.Panel13)
        Me.tabUnacknowledged.Controls.Add(Me.Panel12)
        Me.tabUnacknowledged.Controls.Add(Me.Panel4)
        Me.tabUnacknowledged.Location = New System.Drawing.Point(4, 26)
        Me.tabUnacknowledged.Name = "tabUnacknowledged"
        Me.tabUnacknowledged.Size = New System.Drawing.Size(1057, 385)
        Me.tabUnacknowledged.TabIndex = 3
        Me.tabUnacknowledged.Text = " Unacknowledged "
        '
        'Panel16
        '
        Me.Panel16.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel16.Controls.Add(Me.GridUnAcknowledgedOrders)
        Me.Panel16.Controls.Add(Me.Label54)
        Me.Panel16.Controls.Add(Me.Label55)
        Me.Panel16.Controls.Add(Me.Label56)
        Me.Panel16.Controls.Add(Me.Label57)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel16.Location = New System.Drawing.Point(0, 162)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel16.Size = New System.Drawing.Size(1057, 223)
        Me.Panel16.TabIndex = 28
        '
        'GridUnAcknowledgedOrders
        '
        Me.GridUnAcknowledgedOrders.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.GridUnAcknowledgedOrders.AllowEditing = False
        Me.GridUnAcknowledgedOrders.AutoGenerateColumns = False
        Me.GridUnAcknowledgedOrders.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridUnAcknowledgedOrders.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.GridUnAcknowledgedOrders.ColumnInfo = resources.GetString("GridUnAcknowledgedOrders.ColumnInfo")
        Me.GridUnAcknowledgedOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridUnAcknowledgedOrders.ExtendLastCol = True
        Me.GridUnAcknowledgedOrders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridUnAcknowledgedOrders.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GridUnAcknowledgedOrders.Location = New System.Drawing.Point(4, 1)
        Me.GridUnAcknowledgedOrders.Name = "GridUnAcknowledgedOrders"
        Me.GridUnAcknowledgedOrders.Rows.Count = 13
        Me.GridUnAcknowledgedOrders.Rows.DefaultSize = 19
        Me.GridUnAcknowledgedOrders.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.GridUnAcknowledgedOrders.Size = New System.Drawing.Size(1049, 218)
        Me.GridUnAcknowledgedOrders.StyleInfo = resources.GetString("GridUnAcknowledgedOrders.StyleInfo")
        Me.GridUnAcknowledgedOrders.TabIndex = 22
        '
        'Label54
        '
        Me.Label54.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label54.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label54.Enabled = False
        Me.Label54.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label54.Location = New System.Drawing.Point(4, 219)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(1049, 1)
        Me.Label54.TabIndex = 26
        Me.Label54.Text = "From"
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label55.Enabled = False
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label55.Location = New System.Drawing.Point(4, 0)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(1049, 1)
        Me.Label55.TabIndex = 25
        Me.Label55.Text = "From"
        Me.Label55.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label56.Enabled = False
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label56.Location = New System.Drawing.Point(1053, 0)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(1, 220)
        Me.Label56.TabIndex = 24
        Me.Label56.Text = "From"
        Me.Label56.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label57.Enabled = False
        Me.Label57.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label57.Location = New System.Drawing.Point(3, 0)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(1, 220)
        Me.Label57.TabIndex = 23
        Me.Label57.Text = "From"
        Me.Label57.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.Panel14)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel13.Location = New System.Drawing.Point(0, 134)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel13.Size = New System.Drawing.Size(1057, 28)
        Me.Panel13.TabIndex = 27
        '
        'Panel14
        '
        Me.Panel14.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel14.Controls.Add(Me.Panel15)
        Me.Panel14.Controls.Add(Me.Label49)
        Me.Panel14.Controls.Add(Me.Label50)
        Me.Panel14.Controls.Add(Me.Label51)
        Me.Panel14.Controls.Add(Me.Label52)
        Me.Panel14.Controls.Add(Me.Label53)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel14.Location = New System.Drawing.Point(3, 0)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(1051, 25)
        Me.Panel14.TabIndex = 24
        '
        'Panel15
        '
        Me.Panel15.BackColor = System.Drawing.Color.Transparent
        Me.Panel15.Controls.Add(Me.txtSearchUnacknowledged)
        Me.Panel15.Controls.Add(Me.Label46)
        Me.Panel15.Controls.Add(Me.Label99)
        Me.Panel15.Controls.Add(Me.Label45)
        Me.Panel15.Controls.Add(Me.btnClearUnacknowledgedSearch)
        Me.Panel15.Controls.Add(Me.Label47)
        Me.Panel15.Controls.Add(Me.Label48)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel15.ForeColor = System.Drawing.Color.Black
        Me.Panel15.Location = New System.Drawing.Point(69, 1)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(241, 23)
        Me.Panel15.TabIndex = 23
        '
        'txtSearchUnacknowledged
        '
        Me.txtSearchUnacknowledged.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchUnacknowledged.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchUnacknowledged.Location = New System.Drawing.Point(5, 3)
        Me.txtSearchUnacknowledged.Name = "txtSearchUnacknowledged"
        Me.txtSearchUnacknowledged.Size = New System.Drawing.Size(214, 15)
        Me.txtSearchUnacknowledged.TabIndex = 42
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.White
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label46.Location = New System.Drawing.Point(5, 18)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(214, 5)
        Me.Label46.TabIndex = 45
        '
        'Label99
        '
        Me.Label99.BackColor = System.Drawing.Color.White
        Me.Label99.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label99.Location = New System.Drawing.Point(1, 3)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(4, 20)
        Me.Label99.TabIndex = 44
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.White
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label45.Location = New System.Drawing.Point(1, 0)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(218, 3)
        Me.Label45.TabIndex = 37
        '
        'btnClearUnacknowledgedSearch
        '
        Me.btnClearUnacknowledgedSearch.BackgroundImage = CType(resources.GetObject("btnClearUnacknowledgedSearch.BackgroundImage"), System.Drawing.Image)
        Me.btnClearUnacknowledgedSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearUnacknowledgedSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearUnacknowledgedSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearUnacknowledgedSearch.FlatAppearance.BorderSize = 0
        Me.btnClearUnacknowledgedSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearUnacknowledgedSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearUnacknowledgedSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearUnacknowledgedSearch.Image = CType(resources.GetObject("btnClearUnacknowledgedSearch.Image"), System.Drawing.Image)
        Me.btnClearUnacknowledgedSearch.Location = New System.Drawing.Point(219, 0)
        Me.btnClearUnacknowledgedSearch.Name = "btnClearUnacknowledgedSearch"
        Me.btnClearUnacknowledgedSearch.Size = New System.Drawing.Size(21, 23)
        Me.btnClearUnacknowledgedSearch.TabIndex = 41
        Me.btnClearUnacknowledgedSearch.UseVisualStyleBackColor = True
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label47.Location = New System.Drawing.Point(0, 0)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(1, 23)
        Me.Label47.TabIndex = 39
        Me.Label47.Text = "label4"
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label48.Location = New System.Drawing.Point(240, 0)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(1, 23)
        Me.Label48.TabIndex = 40
        Me.Label48.Text = "label4"
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label49.Enabled = False
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label49.Location = New System.Drawing.Point(1050, 1)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(1, 23)
        Me.Label49.TabIndex = 24
        Me.Label49.Text = "From"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.Transparent
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label50.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label50.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label50.Location = New System.Drawing.Point(1, 1)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(68, 23)
        Me.Label50.TabIndex = 15
        Me.Label50.Text = "  Search :"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label51.Enabled = False
        Me.Label51.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label51.Location = New System.Drawing.Point(0, 1)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(1, 23)
        Me.Label51.TabIndex = 23
        Me.Label51.Text = "From"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label52.Enabled = False
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label52.Location = New System.Drawing.Point(0, 0)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(1051, 1)
        Me.Label52.TabIndex = 25
        Me.Label52.Text = "From"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label53.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label53.Enabled = False
        Me.Label53.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label53.Location = New System.Drawing.Point(0, 24)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(1051, 1)
        Me.Label53.TabIndex = 26
        Me.Label53.Text = "From"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel12
        '
        Me.Panel12.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel12.Controls.Add(Me.Label102)
        Me.Panel12.Controls.Add(Me.rdoUnacknowledgedTests)
        Me.Panel12.Controls.Add(Me.rdoResultedOrderDate)
        Me.Panel12.Controls.Add(Me.Label20)
        Me.Panel12.Controls.Add(Me.dtUnacknowledgedTo)
        Me.Panel12.Controls.Add(Me.lblUnacknowledgedTo)
        Me.Panel12.Controls.Add(Me.dtUnacknowledgedFrom)
        Me.Panel12.Controls.Add(Me.lblUnacknowledgedFrom)
        Me.Panel12.Controls.Add(Me.cmbUnacknowledgedDate)
        Me.Panel12.Controls.Add(Me.Label41)
        Me.Panel12.Controls.Add(Me.Label42)
        Me.Panel12.Controls.Add(Me.Label43)
        Me.Panel12.Controls.Add(Me.Label44)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel12.Location = New System.Drawing.Point(0, 94)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel12.Size = New System.Drawing.Size(1057, 40)
        Me.Panel12.TabIndex = 7
        '
        'Label102
        '
        Me.Label102.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label102.AutoSize = True
        Me.Label102.BackColor = System.Drawing.Color.Transparent
        Me.Label102.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label102.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label102.Location = New System.Drawing.Point(665, 13)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(45, 14)
        Me.Label102.TabIndex = 26
        Me.Label102.Text = "Mode :"
        Me.Label102.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rdoUnacknowledgedTests
        '
        Me.rdoUnacknowledgedTests.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdoUnacknowledgedTests.AutoSize = True
        Me.rdoUnacknowledgedTests.Location = New System.Drawing.Point(950, 11)
        Me.rdoUnacknowledgedTests.Name = "rdoUnacknowledgedTests"
        Me.rdoUnacknowledgedTests.Size = New System.Drawing.Size(94, 17)
        Me.rdoUnacknowledgedTests.TabIndex = 25
        Me.rdoUnacknowledgedTests.TabStop = True
        Me.rdoUnacknowledgedTests.Text = "Individual Test"
        Me.rdoUnacknowledgedTests.UseVisualStyleBackColor = True
        '
        'rdoResultedOrderDate
        '
        Me.rdoResultedOrderDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rdoResultedOrderDate.AutoSize = True
        Me.rdoResultedOrderDate.Location = New System.Drawing.Point(750, 11)
        Me.rdoResultedOrderDate.Name = "rdoResultedOrderDate"
        Me.rdoResultedOrderDate.Size = New System.Drawing.Size(170, 17)
        Me.rdoResultedOrderDate.TabIndex = 24
        Me.rdoResultedOrderDate.TabStop = True
        Me.rdoResultedOrderDate.Text = "All Patient Tests by Order Date"
        Me.rdoResultedOrderDate.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label20.Enabled = False
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Location = New System.Drawing.Point(1053, 4)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 32)
        Me.Label20.TabIndex = 23
        Me.Label20.Text = "From"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtUnacknowledgedTo
        '
        Me.dtUnacknowledgedTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtUnacknowledgedTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtUnacknowledgedTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtUnacknowledgedTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtUnacknowledgedTo.CustomFormat = "MM/dd/yyyy"
        Me.dtUnacknowledgedTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtUnacknowledgedTo.Location = New System.Drawing.Point(405, 9)
        Me.dtUnacknowledgedTo.Name = "dtUnacknowledgedTo"
        Me.dtUnacknowledgedTo.Size = New System.Drawing.Size(88, 22)
        Me.dtUnacknowledgedTo.TabIndex = 17
        '
        'lblUnacknowledgedTo
        '
        Me.lblUnacknowledgedTo.AutoSize = True
        Me.lblUnacknowledgedTo.BackColor = System.Drawing.Color.Transparent
        Me.lblUnacknowledgedTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnacknowledgedTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblUnacknowledgedTo.Location = New System.Drawing.Point(374, 13)
        Me.lblUnacknowledgedTo.Name = "lblUnacknowledgedTo"
        Me.lblUnacknowledgedTo.Size = New System.Drawing.Size(30, 14)
        Me.lblUnacknowledgedTo.TabIndex = 18
        Me.lblUnacknowledgedTo.Text = "To :"
        Me.lblUnacknowledgedTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtUnacknowledgedFrom
        '
        Me.dtUnacknowledgedFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtUnacknowledgedFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtUnacknowledgedFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtUnacknowledgedFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtUnacknowledgedFrom.CustomFormat = "MM/dd/yyyy"
        Me.dtUnacknowledgedFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtUnacknowledgedFrom.Location = New System.Drawing.Point(268, 9)
        Me.dtUnacknowledgedFrom.Name = "dtUnacknowledgedFrom"
        Me.dtUnacknowledgedFrom.Size = New System.Drawing.Size(87, 22)
        Me.dtUnacknowledgedFrom.TabIndex = 16
        '
        'lblUnacknowledgedFrom
        '
        Me.lblUnacknowledgedFrom.AutoSize = True
        Me.lblUnacknowledgedFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblUnacknowledgedFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnacknowledgedFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblUnacknowledgedFrom.Location = New System.Drawing.Point(221, 13)
        Me.lblUnacknowledgedFrom.Name = "lblUnacknowledgedFrom"
        Me.lblUnacknowledgedFrom.Size = New System.Drawing.Size(42, 14)
        Me.lblUnacknowledgedFrom.TabIndex = 19
        Me.lblUnacknowledgedFrom.Text = "From :"
        Me.lblUnacknowledgedFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbUnacknowledgedDate
        '
        Me.cmbUnacknowledgedDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnacknowledgedDate.ForeColor = System.Drawing.Color.Black
        Me.cmbUnacknowledgedDate.Items.AddRange(New Object() {"0 - 7 Days", "8 - 30 Days", "30 + Days"})
        Me.cmbUnacknowledgedDate.Location = New System.Drawing.Point(70, 9)
        Me.cmbUnacknowledgedDate.Name = "cmbUnacknowledgedDate"
        Me.cmbUnacknowledgedDate.Size = New System.Drawing.Size(123, 22)
        Me.cmbUnacknowledgedDate.TabIndex = 14
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.Transparent
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label41.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label41.Location = New System.Drawing.Point(4, 4)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(62, 32)
        Me.Label41.TabIndex = 15
        Me.Label41.Text = "  Aging :"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label42.Enabled = False
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Location = New System.Drawing.Point(4, 3)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(1050, 1)
        Me.Label42.TabIndex = 20
        Me.Label42.Text = "From"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label43.Enabled = False
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Location = New System.Drawing.Point(4, 36)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(1050, 1)
        Me.Label43.TabIndex = 21
        Me.Label43.Text = "From"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label44.Enabled = False
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Location = New System.Drawing.Point(3, 3)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(1, 34)
        Me.Label44.TabIndex = 22
        Me.Label44.Text = "From"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel4.Controls.Add(Me.Panel11)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Panel4.Size = New System.Drawing.Size(1057, 94)
        Me.Panel4.TabIndex = 29
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel11.Controls.Add(Me.Label139)
        Me.Panel11.Controls.Add(Me.Label140)
        Me.Panel11.Controls.Add(Me.Label107)
        Me.Panel11.Controls.Add(Me.Label108)
        Me.Panel11.Controls.Add(Me.Label141)
        Me.Panel11.Controls.Add(Me.Label142)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel11.Location = New System.Drawing.Point(3, 3)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(1051, 91)
        Me.Panel11.TabIndex = 32
        '
        'Label139
        '
        Me.Label139.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label139.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label139.Enabled = False
        Me.Label139.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label139.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label139.Location = New System.Drawing.Point(1, 90)
        Me.Label139.Name = "Label139"
        Me.Label139.Size = New System.Drawing.Size(1049, 1)
        Me.Label139.TabIndex = 27
        Me.Label139.Text = "From"
        Me.Label139.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label140
        '
        Me.Label140.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label140.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label140.Enabled = False
        Me.Label140.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label140.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label140.Location = New System.Drawing.Point(1, 0)
        Me.Label140.Name = "Label140"
        Me.Label140.Size = New System.Drawing.Size(1049, 1)
        Me.Label140.TabIndex = 28
        Me.Label140.Text = "From"
        Me.Label140.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label107
        '
        Me.Label107.BackColor = System.Drawing.Color.Transparent
        Me.Label107.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label107.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label107.ForeColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label107.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label107.Location = New System.Drawing.Point(7, 38)
        Me.Label107.Margin = New System.Windows.Forms.Padding(0)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(1037, 39)
        Me.Label107.TabIndex = 16
        Me.Label107.Text = resources.GetString("Label107.Text")
        Me.Label107.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label108
        '
        Me.Label108.AutoSize = True
        Me.Label108.BackColor = System.Drawing.Color.Transparent
        Me.Label108.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label108.Font = New System.Drawing.Font("Tahoma", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label108.ForeColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label108.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label108.Location = New System.Drawing.Point(7, 10)
        Me.Label108.Name = "Label108"
        Me.Label108.Size = New System.Drawing.Size(579, 14)
        Me.Label108.TabIndex = 15
        Me.Label108.Text = "Use this display to monitor Order Tests which have been resulted but not yet ackn" & _
    "owledged."
        Me.Label108.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label141
        '
        Me.Label141.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label141.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label141.Enabled = False
        Me.Label141.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label141.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label141.Location = New System.Drawing.Point(0, 0)
        Me.Label141.Name = "Label141"
        Me.Label141.Size = New System.Drawing.Size(1, 91)
        Me.Label141.TabIndex = 30
        Me.Label141.Text = "From"
        Me.Label141.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label142
        '
        Me.Label142.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label142.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label142.Enabled = False
        Me.Label142.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label142.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label142.Location = New System.Drawing.Point(1050, 0)
        Me.Label142.Name = "Label142"
        Me.Label142.Size = New System.Drawing.Size(1, 91)
        Me.Label142.TabIndex = 29
        Me.Label142.Text = "From"
        Me.Label142.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tabAllOrders
        '
        Me.tabAllOrders.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tabAllOrders.Controls.Add(Me.Panel21)
        Me.tabAllOrders.Controls.Add(Me.Panel18)
        Me.tabAllOrders.Controls.Add(Me.Panel17)
        Me.tabAllOrders.Controls.Add(Me.Panel5)
        Me.tabAllOrders.Location = New System.Drawing.Point(4, 26)
        Me.tabAllOrders.Name = "tabAllOrders"
        Me.tabAllOrders.Size = New System.Drawing.Size(1057, 385)
        Me.tabAllOrders.TabIndex = 4
        Me.tabAllOrders.Text = " All Orders "
        '
        'Panel21
        '
        Me.Panel21.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel21.Controls.Add(Me.GridAllOrdersOrders)
        Me.Panel21.Controls.Add(Me.Label72)
        Me.Panel21.Controls.Add(Me.Label73)
        Me.Panel21.Controls.Add(Me.Label74)
        Me.Panel21.Controls.Add(Me.Label75)
        Me.Panel21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel21.Location = New System.Drawing.Point(0, 110)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel21.Size = New System.Drawing.Size(1057, 275)
        Me.Panel21.TabIndex = 27
        '
        'GridAllOrdersOrders
        '
        Me.GridAllOrdersOrders.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.GridAllOrdersOrders.AllowEditing = False
        Me.GridAllOrdersOrders.AutoGenerateColumns = False
        Me.GridAllOrdersOrders.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridAllOrdersOrders.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.GridAllOrdersOrders.ColumnInfo = resources.GetString("GridAllOrdersOrders.ColumnInfo")
        Me.GridAllOrdersOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridAllOrdersOrders.ExtendLastCol = True
        Me.GridAllOrdersOrders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridAllOrdersOrders.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GridAllOrdersOrders.Location = New System.Drawing.Point(4, 1)
        Me.GridAllOrdersOrders.Name = "GridAllOrdersOrders"
        Me.GridAllOrdersOrders.Rows.Count = 13
        Me.GridAllOrdersOrders.Rows.DefaultSize = 19
        Me.GridAllOrdersOrders.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.GridAllOrdersOrders.Size = New System.Drawing.Size(1049, 270)
        Me.GridAllOrdersOrders.StyleInfo = resources.GetString("GridAllOrdersOrders.StyleInfo")
        Me.GridAllOrdersOrders.TabIndex = 22
        '
        'Label72
        '
        Me.Label72.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label72.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label72.Enabled = False
        Me.Label72.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label72.Location = New System.Drawing.Point(4, 271)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(1049, 1)
        Me.Label72.TabIndex = 26
        Me.Label72.Text = "From"
        Me.Label72.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label73
        '
        Me.Label73.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label73.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label73.Enabled = False
        Me.Label73.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label73.Location = New System.Drawing.Point(4, 0)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(1049, 1)
        Me.Label73.TabIndex = 25
        Me.Label73.Text = "From"
        Me.Label73.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label74
        '
        Me.Label74.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label74.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label74.Enabled = False
        Me.Label74.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label74.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label74.Location = New System.Drawing.Point(1053, 0)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(1, 272)
        Me.Label74.TabIndex = 24
        Me.Label74.Text = "From"
        Me.Label74.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label75
        '
        Me.Label75.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label75.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label75.Enabled = False
        Me.Label75.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label75.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label75.Location = New System.Drawing.Point(3, 0)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(1, 272)
        Me.Label75.TabIndex = 23
        Me.Label75.Text = "From"
        Me.Label75.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel18
        '
        Me.Panel18.Controls.Add(Me.Panel19)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel18.Location = New System.Drawing.Point(0, 82)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel18.Size = New System.Drawing.Size(1057, 28)
        Me.Panel18.TabIndex = 26
        '
        'Panel19
        '
        Me.Panel19.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel19.Controls.Add(Me.Panel20)
        Me.Panel19.Controls.Add(Me.Label67)
        Me.Panel19.Controls.Add(Me.Label68)
        Me.Panel19.Controls.Add(Me.Label69)
        Me.Panel19.Controls.Add(Me.Label70)
        Me.Panel19.Controls.Add(Me.Label71)
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel19.Location = New System.Drawing.Point(3, 0)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(1051, 25)
        Me.Panel19.TabIndex = 24
        '
        'Panel20
        '
        Me.Panel20.BackColor = System.Drawing.Color.Transparent
        Me.Panel20.Controls.Add(Me.txtSearchAllOrders)
        Me.Panel20.Controls.Add(Me.Label64)
        Me.Panel20.Controls.Add(Me.Label100)
        Me.Panel20.Controls.Add(Me.Label63)
        Me.Panel20.Controls.Add(Me.btnClearAllOrdersSearch)
        Me.Panel20.Controls.Add(Me.Label65)
        Me.Panel20.Controls.Add(Me.Label66)
        Me.Panel20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel20.ForeColor = System.Drawing.Color.Black
        Me.Panel20.Location = New System.Drawing.Point(69, 1)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Size = New System.Drawing.Size(241, 23)
        Me.Panel20.TabIndex = 23
        '
        'txtSearchAllOrders
        '
        Me.txtSearchAllOrders.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchAllOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchAllOrders.Location = New System.Drawing.Point(5, 3)
        Me.txtSearchAllOrders.Name = "txtSearchAllOrders"
        Me.txtSearchAllOrders.Size = New System.Drawing.Size(214, 15)
        Me.txtSearchAllOrders.TabIndex = 42
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.White
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label64.Location = New System.Drawing.Point(5, 18)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(214, 5)
        Me.Label64.TabIndex = 45
        '
        'Label100
        '
        Me.Label100.BackColor = System.Drawing.Color.White
        Me.Label100.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label100.Location = New System.Drawing.Point(1, 3)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(4, 20)
        Me.Label100.TabIndex = 44
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.White
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label63.Location = New System.Drawing.Point(1, 0)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(218, 3)
        Me.Label63.TabIndex = 37
        '
        'btnClearAllOrdersSearch
        '
        Me.btnClearAllOrdersSearch.BackgroundImage = CType(resources.GetObject("btnClearAllOrdersSearch.BackgroundImage"), System.Drawing.Image)
        Me.btnClearAllOrdersSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearAllOrdersSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearAllOrdersSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearAllOrdersSearch.FlatAppearance.BorderSize = 0
        Me.btnClearAllOrdersSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearAllOrdersSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearAllOrdersSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearAllOrdersSearch.Image = CType(resources.GetObject("btnClearAllOrdersSearch.Image"), System.Drawing.Image)
        Me.btnClearAllOrdersSearch.Location = New System.Drawing.Point(219, 0)
        Me.btnClearAllOrdersSearch.Name = "btnClearAllOrdersSearch"
        Me.btnClearAllOrdersSearch.Size = New System.Drawing.Size(21, 23)
        Me.btnClearAllOrdersSearch.TabIndex = 41
        Me.btnClearAllOrdersSearch.UseVisualStyleBackColor = True
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label65.Location = New System.Drawing.Point(0, 0)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(1, 23)
        Me.Label65.TabIndex = 39
        Me.Label65.Text = "label4"
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label66.Location = New System.Drawing.Point(240, 0)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(1, 23)
        Me.Label66.TabIndex = 40
        Me.Label66.Text = "label4"
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label67.Enabled = False
        Me.Label67.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label67.Location = New System.Drawing.Point(1050, 1)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(1, 23)
        Me.Label67.TabIndex = 24
        Me.Label67.Text = "From"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label68
        '
        Me.Label68.BackColor = System.Drawing.Color.Transparent
        Me.Label68.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label68.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label68.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label68.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label68.Location = New System.Drawing.Point(1, 1)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(68, 23)
        Me.Label68.TabIndex = 15
        Me.Label68.Text = "  Search :"
        Me.Label68.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label69
        '
        Me.Label69.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label69.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label69.Enabled = False
        Me.Label69.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label69.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label69.Location = New System.Drawing.Point(0, 1)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(1, 23)
        Me.Label69.TabIndex = 23
        Me.Label69.Text = "From"
        Me.Label69.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label70
        '
        Me.Label70.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label70.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label70.Enabled = False
        Me.Label70.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label70.Location = New System.Drawing.Point(0, 0)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(1051, 1)
        Me.Label70.TabIndex = 25
        Me.Label70.Text = "From"
        Me.Label70.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label71
        '
        Me.Label71.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label71.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label71.Enabled = False
        Me.Label71.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label71.Location = New System.Drawing.Point(0, 24)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(1051, 1)
        Me.Label71.TabIndex = 26
        Me.Label71.Text = "From"
        Me.Label71.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel17
        '
        Me.Panel17.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel17.Controls.Add(Me.Label76)
        Me.Panel17.Controls.Add(Me.cmbAllOrdersStatus)
        Me.Panel17.Controls.Add(Me.Label21)
        Me.Panel17.Controls.Add(Me.dtAllOrdersTo)
        Me.Panel17.Controls.Add(Me.Label40)
        Me.Panel17.Controls.Add(Me.dtAllOrdersFrom)
        Me.Panel17.Controls.Add(Me.Label58)
        Me.Panel17.Controls.Add(Me.cmbAllOrderDate)
        Me.Panel17.Controls.Add(Me.Label59)
        Me.Panel17.Controls.Add(Me.Label60)
        Me.Panel17.Controls.Add(Me.Label61)
        Me.Panel17.Controls.Add(Me.Label62)
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel17.Location = New System.Drawing.Point(0, 42)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel17.Size = New System.Drawing.Size(1057, 40)
        Me.Panel17.TabIndex = 6
        '
        'Label76
        '
        Me.Label76.AutoSize = True
        Me.Label76.BackColor = System.Drawing.Color.Transparent
        Me.Label76.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label76.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label76.Location = New System.Drawing.Point(556, 13)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(85, 14)
        Me.Label76.TabIndex = 25
        Me.Label76.Text = "Order Status :"
        Me.Label76.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbAllOrdersStatus
        '
        Me.cmbAllOrdersStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAllOrdersStatus.ForeColor = System.Drawing.Color.Black
        Me.cmbAllOrdersStatus.Items.AddRange(New Object() {"Today", "Yesterday", "Last Week", "Last Month"})
        Me.cmbAllOrdersStatus.Location = New System.Drawing.Point(654, 9)
        Me.cmbAllOrdersStatus.Name = "cmbAllOrdersStatus"
        Me.cmbAllOrdersStatus.Size = New System.Drawing.Size(191, 22)
        Me.cmbAllOrdersStatus.TabIndex = 24
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label21.Enabled = False
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Location = New System.Drawing.Point(1053, 4)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(1, 32)
        Me.Label21.TabIndex = 23
        Me.Label21.Text = "From"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtAllOrdersTo
        '
        Me.dtAllOrdersTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtAllOrdersTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtAllOrdersTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtAllOrdersTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtAllOrdersTo.CustomFormat = "MM/dd/yyyy"
        Me.dtAllOrdersTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtAllOrdersTo.Location = New System.Drawing.Point(405, 9)
        Me.dtAllOrdersTo.Name = "dtAllOrdersTo"
        Me.dtAllOrdersTo.Size = New System.Drawing.Size(88, 22)
        Me.dtAllOrdersTo.TabIndex = 17
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.Color.Transparent
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Location = New System.Drawing.Point(374, 13)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(30, 14)
        Me.Label40.TabIndex = 18
        Me.Label40.Text = "To :"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtAllOrdersFrom
        '
        Me.dtAllOrdersFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtAllOrdersFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtAllOrdersFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtAllOrdersFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtAllOrdersFrom.CustomFormat = "MM/dd/yyyy"
        Me.dtAllOrdersFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtAllOrdersFrom.Location = New System.Drawing.Point(268, 9)
        Me.dtAllOrdersFrom.Name = "dtAllOrdersFrom"
        Me.dtAllOrdersFrom.Size = New System.Drawing.Size(87, 22)
        Me.dtAllOrdersFrom.TabIndex = 16
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.BackColor = System.Drawing.Color.Transparent
        Me.Label58.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label58.Location = New System.Drawing.Point(221, 13)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(42, 14)
        Me.Label58.TabIndex = 19
        Me.Label58.Text = "From :"
        Me.Label58.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbAllOrderDate
        '
        Me.cmbAllOrderDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAllOrderDate.ForeColor = System.Drawing.Color.Black
        Me.cmbAllOrderDate.Items.AddRange(New Object() {"Today", "Yesterday", "Last Week", "Last Month"})
        Me.cmbAllOrderDate.Location = New System.Drawing.Point(70, 9)
        Me.cmbAllOrderDate.Name = "cmbAllOrderDate"
        Me.cmbAllOrderDate.Size = New System.Drawing.Size(123, 22)
        Me.cmbAllOrderDate.TabIndex = 14
        '
        'Label59
        '
        Me.Label59.BackColor = System.Drawing.Color.Transparent
        Me.Label59.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label59.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label59.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label59.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label59.Location = New System.Drawing.Point(4, 4)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(62, 32)
        Me.Label59.TabIndex = 15
        Me.Label59.Text = "  Aging :"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label60
        '
        Me.Label60.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label60.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label60.Enabled = False
        Me.Label60.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label60.Location = New System.Drawing.Point(4, 3)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(1050, 1)
        Me.Label60.TabIndex = 20
        Me.Label60.Text = "From"
        Me.Label60.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label61.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label61.Enabled = False
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label61.Location = New System.Drawing.Point(4, 36)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(1050, 1)
        Me.Label61.TabIndex = 21
        Me.Label61.Text = "From"
        Me.Label61.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label62.Enabled = False
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label62.Location = New System.Drawing.Point(3, 3)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(1, 34)
        Me.Label62.TabIndex = 22
        Me.Label62.Text = "From"
        Me.Label62.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Transparent
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel5.Controls.Add(Me.Panel27)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Panel5.Size = New System.Drawing.Size(1057, 42)
        Me.Panel5.TabIndex = 29
        '
        'Panel27
        '
        Me.Panel27.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel27.Controls.Add(Me.Label145)
        Me.Panel27.Controls.Add(Me.Label146)
        Me.Panel27.Controls.Add(Me.Label147)
        Me.Panel27.Controls.Add(Me.Label110)
        Me.Panel27.Controls.Add(Me.Label148)
        Me.Panel27.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel27.Location = New System.Drawing.Point(3, 3)
        Me.Panel27.Name = "Panel27"
        Me.Panel27.Size = New System.Drawing.Size(1051, 39)
        Me.Panel27.TabIndex = 32
        '
        'Label145
        '
        Me.Label145.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label145.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label145.Enabled = False
        Me.Label145.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label145.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label145.Location = New System.Drawing.Point(1, 38)
        Me.Label145.Name = "Label145"
        Me.Label145.Size = New System.Drawing.Size(1049, 1)
        Me.Label145.TabIndex = 27
        Me.Label145.Text = "From"
        Me.Label145.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label146
        '
        Me.Label146.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label146.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label146.Enabled = False
        Me.Label146.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label146.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label146.Location = New System.Drawing.Point(1, 0)
        Me.Label146.Name = "Label146"
        Me.Label146.Size = New System.Drawing.Size(1049, 1)
        Me.Label146.TabIndex = 28
        Me.Label146.Text = "From"
        Me.Label146.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label147
        '
        Me.Label147.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label147.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label147.Enabled = False
        Me.Label147.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label147.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label147.Location = New System.Drawing.Point(0, 0)
        Me.Label147.Name = "Label147"
        Me.Label147.Size = New System.Drawing.Size(1, 39)
        Me.Label147.TabIndex = 30
        Me.Label147.Text = "From"
        Me.Label147.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label110
        '
        Me.Label110.AutoSize = True
        Me.Label110.BackColor = System.Drawing.Color.Transparent
        Me.Label110.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label110.Font = New System.Drawing.Font("Tahoma", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label110.ForeColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label110.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label110.Location = New System.Drawing.Point(7, 10)
        Me.Label110.Name = "Label110"
        Me.Label110.Size = New System.Drawing.Size(596, 14)
        Me.Label110.TabIndex = 15
        Me.Label110.Text = "Use this display to find any Order.  Search by Order Status, Order Date, Patient," & _
    " Test Name, etc."
        Me.Label110.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label148
        '
        Me.Label148.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label148.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label148.Enabled = False
        Me.Label148.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label148.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label148.Location = New System.Drawing.Point(1050, 0)
        Me.Label148.Name = "Label148"
        Me.Label148.Size = New System.Drawing.Size(1, 39)
        Me.Label148.TabIndex = 29
        Me.Label148.Text = "From"
        Me.Label148.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tabUnfinishedOrderTemplates
        '
        Me.tabUnfinishedOrderTemplates.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tabUnfinishedOrderTemplates.Controls.Add(Me.Panel26)
        Me.tabUnfinishedOrderTemplates.Controls.Add(Me.Panel23)
        Me.tabUnfinishedOrderTemplates.Controls.Add(Me.Panel22)
        Me.tabUnfinishedOrderTemplates.Controls.Add(Me.Panel6)
        Me.tabUnfinishedOrderTemplates.Location = New System.Drawing.Point(4, 26)
        Me.tabUnfinishedOrderTemplates.Name = "tabUnfinishedOrderTemplates"
        Me.tabUnfinishedOrderTemplates.Size = New System.Drawing.Size(1057, 385)
        Me.tabUnfinishedOrderTemplates.TabIndex = 5
        Me.tabUnfinishedOrderTemplates.Text = " Unfinished Order Templates "
        '
        'Panel26
        '
        Me.Panel26.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel26.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel26.Controls.Add(Me.GridUnfinishedOrderTemplates)
        Me.Panel26.Controls.Add(Me.Label94)
        Me.Panel26.Controls.Add(Me.Label95)
        Me.Panel26.Controls.Add(Me.Label96)
        Me.Panel26.Controls.Add(Me.Label97)
        Me.Panel26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel26.Location = New System.Drawing.Point(0, 142)
        Me.Panel26.Name = "Panel26"
        Me.Panel26.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel26.Size = New System.Drawing.Size(1057, 243)
        Me.Panel26.TabIndex = 28
        '
        'GridUnfinishedOrderTemplates
        '
        Me.GridUnfinishedOrderTemplates.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.GridUnfinishedOrderTemplates.AllowEditing = False
        Me.GridUnfinishedOrderTemplates.AutoGenerateColumns = False
        Me.GridUnfinishedOrderTemplates.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridUnfinishedOrderTemplates.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.GridUnfinishedOrderTemplates.ColumnInfo = resources.GetString("GridUnfinishedOrderTemplates.ColumnInfo")
        Me.GridUnfinishedOrderTemplates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridUnfinishedOrderTemplates.ExtendLastCol = True
        Me.GridUnfinishedOrderTemplates.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridUnfinishedOrderTemplates.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GridUnfinishedOrderTemplates.Location = New System.Drawing.Point(4, 1)
        Me.GridUnfinishedOrderTemplates.Name = "GridUnfinishedOrderTemplates"
        Me.GridUnfinishedOrderTemplates.Rows.Count = 13
        Me.GridUnfinishedOrderTemplates.Rows.DefaultSize = 19
        Me.GridUnfinishedOrderTemplates.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.GridUnfinishedOrderTemplates.Size = New System.Drawing.Size(1049, 238)
        Me.GridUnfinishedOrderTemplates.StyleInfo = resources.GetString("GridUnfinishedOrderTemplates.StyleInfo")
        Me.GridUnfinishedOrderTemplates.TabIndex = 22
        '
        'Label94
        '
        Me.Label94.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label94.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label94.Enabled = False
        Me.Label94.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label94.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label94.Location = New System.Drawing.Point(4, 239)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(1049, 1)
        Me.Label94.TabIndex = 26
        Me.Label94.Text = "From"
        Me.Label94.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label95
        '
        Me.Label95.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label95.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label95.Enabled = False
        Me.Label95.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label95.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label95.Location = New System.Drawing.Point(4, 0)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(1049, 1)
        Me.Label95.TabIndex = 25
        Me.Label95.Text = "From"
        Me.Label95.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label96
        '
        Me.Label96.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label96.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label96.Enabled = False
        Me.Label96.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label96.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label96.Location = New System.Drawing.Point(1053, 0)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(1, 240)
        Me.Label96.TabIndex = 24
        Me.Label96.Text = "From"
        Me.Label96.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label97
        '
        Me.Label97.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label97.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label97.Enabled = False
        Me.Label97.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label97.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label97.Location = New System.Drawing.Point(3, 0)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(1, 240)
        Me.Label97.TabIndex = 23
        Me.Label97.Text = "From"
        Me.Label97.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel23
        '
        Me.Panel23.Controls.Add(Me.Panel24)
        Me.Panel23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel23.Location = New System.Drawing.Point(0, 114)
        Me.Panel23.Name = "Panel23"
        Me.Panel23.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel23.Size = New System.Drawing.Size(1057, 28)
        Me.Panel23.TabIndex = 27
        '
        'Panel24
        '
        Me.Panel24.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel24.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel24.Controls.Add(Me.Panel25)
        Me.Panel24.Controls.Add(Me.Label89)
        Me.Panel24.Controls.Add(Me.Label90)
        Me.Panel24.Controls.Add(Me.Label91)
        Me.Panel24.Controls.Add(Me.Label92)
        Me.Panel24.Controls.Add(Me.Label93)
        Me.Panel24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel24.Location = New System.Drawing.Point(3, 0)
        Me.Panel24.Name = "Panel24"
        Me.Panel24.Size = New System.Drawing.Size(1051, 25)
        Me.Panel24.TabIndex = 24
        '
        'Panel25
        '
        Me.Panel25.BackColor = System.Drawing.Color.Transparent
        Me.Panel25.Controls.Add(Me.txtSearchUnfinishedOrders)
        Me.Panel25.Controls.Add(Me.Label86)
        Me.Panel25.Controls.Add(Me.Label101)
        Me.Panel25.Controls.Add(Me.Label85)
        Me.Panel25.Controls.Add(Me.btnClearUnfinishedOrdersSearch)
        Me.Panel25.Controls.Add(Me.Label87)
        Me.Panel25.Controls.Add(Me.Label88)
        Me.Panel25.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel25.ForeColor = System.Drawing.Color.Black
        Me.Panel25.Location = New System.Drawing.Point(69, 1)
        Me.Panel25.Name = "Panel25"
        Me.Panel25.Size = New System.Drawing.Size(241, 23)
        Me.Panel25.TabIndex = 23
        '
        'txtSearchUnfinishedOrders
        '
        Me.txtSearchUnfinishedOrders.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchUnfinishedOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchUnfinishedOrders.Location = New System.Drawing.Point(5, 3)
        Me.txtSearchUnfinishedOrders.Name = "txtSearchUnfinishedOrders"
        Me.txtSearchUnfinishedOrders.Size = New System.Drawing.Size(214, 15)
        Me.txtSearchUnfinishedOrders.TabIndex = 42
        '
        'Label86
        '
        Me.Label86.BackColor = System.Drawing.Color.White
        Me.Label86.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label86.Location = New System.Drawing.Point(5, 18)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(214, 5)
        Me.Label86.TabIndex = 45
        '
        'Label101
        '
        Me.Label101.BackColor = System.Drawing.Color.White
        Me.Label101.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label101.Location = New System.Drawing.Point(1, 3)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(4, 20)
        Me.Label101.TabIndex = 44
        '
        'Label85
        '
        Me.Label85.BackColor = System.Drawing.Color.White
        Me.Label85.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label85.Location = New System.Drawing.Point(1, 0)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(218, 3)
        Me.Label85.TabIndex = 37
        '
        'btnClearUnfinishedOrdersSearch
        '
        Me.btnClearUnfinishedOrdersSearch.BackgroundImage = CType(resources.GetObject("btnClearUnfinishedOrdersSearch.BackgroundImage"), System.Drawing.Image)
        Me.btnClearUnfinishedOrdersSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearUnfinishedOrdersSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearUnfinishedOrdersSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearUnfinishedOrdersSearch.FlatAppearance.BorderSize = 0
        Me.btnClearUnfinishedOrdersSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearUnfinishedOrdersSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearUnfinishedOrdersSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearUnfinishedOrdersSearch.Image = CType(resources.GetObject("btnClearUnfinishedOrdersSearch.Image"), System.Drawing.Image)
        Me.btnClearUnfinishedOrdersSearch.Location = New System.Drawing.Point(219, 0)
        Me.btnClearUnfinishedOrdersSearch.Name = "btnClearUnfinishedOrdersSearch"
        Me.btnClearUnfinishedOrdersSearch.Size = New System.Drawing.Size(21, 23)
        Me.btnClearUnfinishedOrdersSearch.TabIndex = 41
        Me.btnClearUnfinishedOrdersSearch.UseVisualStyleBackColor = True
        '
        'Label87
        '
        Me.Label87.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label87.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label87.Location = New System.Drawing.Point(0, 0)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(1, 23)
        Me.Label87.TabIndex = 39
        Me.Label87.Text = "label4"
        '
        'Label88
        '
        Me.Label88.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label88.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label88.Location = New System.Drawing.Point(240, 0)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(1, 23)
        Me.Label88.TabIndex = 40
        Me.Label88.Text = "label4"
        '
        'Label89
        '
        Me.Label89.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label89.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label89.Enabled = False
        Me.Label89.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label89.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label89.Location = New System.Drawing.Point(1050, 1)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(1, 23)
        Me.Label89.TabIndex = 24
        Me.Label89.Text = "From"
        Me.Label89.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label90
        '
        Me.Label90.BackColor = System.Drawing.Color.Transparent
        Me.Label90.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label90.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label90.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label90.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label90.Location = New System.Drawing.Point(1, 1)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(68, 23)
        Me.Label90.TabIndex = 15
        Me.Label90.Text = "  Search :"
        Me.Label90.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label91
        '
        Me.Label91.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label91.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label91.Enabled = False
        Me.Label91.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label91.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label91.Location = New System.Drawing.Point(0, 1)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(1, 23)
        Me.Label91.TabIndex = 23
        Me.Label91.Text = "From"
        Me.Label91.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label92
        '
        Me.Label92.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label92.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label92.Enabled = False
        Me.Label92.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label92.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label92.Location = New System.Drawing.Point(0, 0)
        Me.Label92.Name = "Label92"
        Me.Label92.Size = New System.Drawing.Size(1051, 1)
        Me.Label92.TabIndex = 25
        Me.Label92.Text = "From"
        Me.Label92.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label93
        '
        Me.Label93.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label93.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label93.Enabled = False
        Me.Label93.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label93.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label93.Location = New System.Drawing.Point(0, 24)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(1051, 1)
        Me.Label93.TabIndex = 26
        Me.Label93.Text = "From"
        Me.Label93.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel22
        '
        Me.Panel22.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel22.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel22.Controls.Add(Me.Label78)
        Me.Panel22.Controls.Add(Me.dtUnfinishedOrderTo)
        Me.Panel22.Controls.Add(Me.Label79)
        Me.Panel22.Controls.Add(Me.dtUnfinishedOrderFrom)
        Me.Panel22.Controls.Add(Me.Label80)
        Me.Panel22.Controls.Add(Me.cmbUnfinishedOrderDate)
        Me.Panel22.Controls.Add(Me.Label81)
        Me.Panel22.Controls.Add(Me.Label82)
        Me.Panel22.Controls.Add(Me.Label83)
        Me.Panel22.Controls.Add(Me.Label84)
        Me.Panel22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel22.Location = New System.Drawing.Point(0, 74)
        Me.Panel22.Name = "Panel22"
        Me.Panel22.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel22.Size = New System.Drawing.Size(1057, 40)
        Me.Panel22.TabIndex = 7
        '
        'Label78
        '
        Me.Label78.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label78.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label78.Enabled = False
        Me.Label78.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label78.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label78.Location = New System.Drawing.Point(1053, 4)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(1, 32)
        Me.Label78.TabIndex = 23
        Me.Label78.Text = "From"
        Me.Label78.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtUnfinishedOrderTo
        '
        Me.dtUnfinishedOrderTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtUnfinishedOrderTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtUnfinishedOrderTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtUnfinishedOrderTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtUnfinishedOrderTo.CustomFormat = "MM/dd/yyyy"
        Me.dtUnfinishedOrderTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtUnfinishedOrderTo.Location = New System.Drawing.Point(405, 9)
        Me.dtUnfinishedOrderTo.Name = "dtUnfinishedOrderTo"
        Me.dtUnfinishedOrderTo.Size = New System.Drawing.Size(88, 22)
        Me.dtUnfinishedOrderTo.TabIndex = 17
        '
        'Label79
        '
        Me.Label79.AutoSize = True
        Me.Label79.BackColor = System.Drawing.Color.Transparent
        Me.Label79.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label79.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label79.Location = New System.Drawing.Point(374, 13)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(30, 14)
        Me.Label79.TabIndex = 18
        Me.Label79.Text = "To :"
        Me.Label79.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtUnfinishedOrderFrom
        '
        Me.dtUnfinishedOrderFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtUnfinishedOrderFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtUnfinishedOrderFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtUnfinishedOrderFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtUnfinishedOrderFrom.CustomFormat = "MM/dd/yyyy"
        Me.dtUnfinishedOrderFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtUnfinishedOrderFrom.Location = New System.Drawing.Point(268, 9)
        Me.dtUnfinishedOrderFrom.Name = "dtUnfinishedOrderFrom"
        Me.dtUnfinishedOrderFrom.Size = New System.Drawing.Size(87, 22)
        Me.dtUnfinishedOrderFrom.TabIndex = 16
        '
        'Label80
        '
        Me.Label80.AutoSize = True
        Me.Label80.BackColor = System.Drawing.Color.Transparent
        Me.Label80.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label80.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label80.Location = New System.Drawing.Point(221, 13)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(42, 14)
        Me.Label80.TabIndex = 19
        Me.Label80.Text = "From :"
        Me.Label80.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbUnfinishedOrderDate
        '
        Me.cmbUnfinishedOrderDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnfinishedOrderDate.ForeColor = System.Drawing.Color.Black
        Me.cmbUnfinishedOrderDate.Items.AddRange(New Object() {"Today", "Yesterday", "Last Week", "Last Month"})
        Me.cmbUnfinishedOrderDate.Location = New System.Drawing.Point(70, 9)
        Me.cmbUnfinishedOrderDate.Name = "cmbUnfinishedOrderDate"
        Me.cmbUnfinishedOrderDate.Size = New System.Drawing.Size(123, 22)
        Me.cmbUnfinishedOrderDate.TabIndex = 14
        '
        'Label81
        '
        Me.Label81.BackColor = System.Drawing.Color.Transparent
        Me.Label81.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label81.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label81.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label81.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label81.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label81.Location = New System.Drawing.Point(4, 4)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(62, 32)
        Me.Label81.TabIndex = 15
        Me.Label81.Text = "  Aging :"
        Me.Label81.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label82
        '
        Me.Label82.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label82.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label82.Enabled = False
        Me.Label82.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label82.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label82.Location = New System.Drawing.Point(4, 3)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(1050, 1)
        Me.Label82.TabIndex = 20
        Me.Label82.Text = "From"
        Me.Label82.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label83
        '
        Me.Label83.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label83.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label83.Enabled = False
        Me.Label83.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label83.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label83.Location = New System.Drawing.Point(4, 36)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(1050, 1)
        Me.Label83.TabIndex = 21
        Me.Label83.Text = "From"
        Me.Label83.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label84
        '
        Me.Label84.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label84.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label84.Enabled = False
        Me.Label84.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label84.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label84.Location = New System.Drawing.Point(3, 3)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(1, 34)
        Me.Label84.TabIndex = 22
        Me.Label84.Text = "From"
        Me.Label84.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Transparent
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel6.Controls.Add(Me.Panel28)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Panel6.Size = New System.Drawing.Size(1057, 74)
        Me.Panel6.TabIndex = 29
        '
        'Panel28
        '
        Me.Panel28.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel28.Controls.Add(Me.Label109)
        Me.Panel28.Controls.Add(Me.Label151)
        Me.Panel28.Controls.Add(Me.Label152)
        Me.Panel28.Controls.Add(Me.Label111)
        Me.Panel28.Controls.Add(Me.Label153)
        Me.Panel28.Controls.Add(Me.Label154)
        Me.Panel28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel28.Location = New System.Drawing.Point(3, 3)
        Me.Panel28.Name = "Panel28"
        Me.Panel28.Size = New System.Drawing.Size(1051, 71)
        Me.Panel28.TabIndex = 32
        '
        'Label109
        '
        Me.Label109.AutoSize = True
        Me.Label109.BackColor = System.Drawing.Color.Transparent
        Me.Label109.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label109.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label109.ForeColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label109.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label109.Location = New System.Drawing.Point(7, 41)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(1033, 14)
        Me.Label109.TabIndex = 16
        Me.Label109.Text = "Tip: Orders and Order Templates are different.  Use Orders for more complete trac" & _
    "king of Lab Tests, Radiology Orders, Referrals, etc.  Orders are used to meet Me" & _
    "aningful Use objectives."
        Me.Label109.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label151
        '
        Me.Label151.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label151.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label151.Enabled = False
        Me.Label151.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label151.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label151.Location = New System.Drawing.Point(1, 70)
        Me.Label151.Name = "Label151"
        Me.Label151.Size = New System.Drawing.Size(1049, 1)
        Me.Label151.TabIndex = 27
        Me.Label151.Text = "From"
        Me.Label151.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label152
        '
        Me.Label152.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label152.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label152.Enabled = False
        Me.Label152.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label152.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label152.Location = New System.Drawing.Point(1, 0)
        Me.Label152.Name = "Label152"
        Me.Label152.Size = New System.Drawing.Size(1049, 1)
        Me.Label152.TabIndex = 28
        Me.Label152.Text = "From"
        Me.Label152.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label111
        '
        Me.Label111.AutoSize = True
        Me.Label111.BackColor = System.Drawing.Color.Transparent
        Me.Label111.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label111.Font = New System.Drawing.Font("Tahoma", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label111.ForeColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label111.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label111.Location = New System.Drawing.Point(7, 10)
        Me.Label111.Name = "Label111"
        Me.Label111.Size = New System.Drawing.Size(372, 14)
        Me.Label111.TabIndex = 15
        Me.Label111.Text = "Use this display to monitor unfinished ""Order Templates"".   "
        Me.Label111.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label153
        '
        Me.Label153.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label153.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label153.Enabled = False
        Me.Label153.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label153.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label153.Location = New System.Drawing.Point(0, 0)
        Me.Label153.Name = "Label153"
        Me.Label153.Size = New System.Drawing.Size(1, 71)
        Me.Label153.TabIndex = 30
        Me.Label153.Text = "From"
        Me.Label153.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label154
        '
        Me.Label154.BackColor = System.Drawing.Color.FromArgb(CType(CType(133, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(3, Byte), Integer))
        Me.Label154.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label154.Enabled = False
        Me.Label154.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label154.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label154.Location = New System.Drawing.Point(1050, 0)
        Me.Label154.Name = "Label154"
        Me.Label154.Size = New System.Drawing.Size(1, 71)
        Me.Label154.TabIndex = 29
        Me.Label154.Text = "From"
        Me.Label154.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlLeftTopTop
        '
        Me.pnlLeftTopTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnlLeftTopTop.Controls.Add(Me.Label11)
        Me.pnlLeftTopTop.Controls.Add(Me.cmbProviders)
        Me.pnlLeftTopTop.Controls.Add(Me.Label26)
        Me.pnlLeftTopTop.Controls.Add(Me.Label10)
        Me.pnlLeftTopTop.Controls.Add(Me.Label12)
        Me.pnlLeftTopTop.Controls.Add(Me.Label13)
        Me.pnlLeftTopTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLeftTopTop.Location = New System.Drawing.Point(0, 54)
        Me.pnlLeftTopTop.Name = "pnlLeftTopTop"
        Me.pnlLeftTopTop.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlLeftTopTop.Size = New System.Drawing.Size(1065, 35)
        Me.pnlLeftTopTop.TabIndex = 5
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Enabled = False
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(1061, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 27)
        Me.Label11.TabIndex = 24
        Me.Label11.Text = "From"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbProviders
        '
        Me.cmbProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProviders.ForeColor = System.Drawing.Color.Black
        Me.cmbProviders.Location = New System.Drawing.Point(82, 6)
        Me.cmbProviders.Name = "cmbProviders"
        Me.cmbProviders.Size = New System.Drawing.Size(258, 22)
        Me.cmbProviders.TabIndex = 14
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label26.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(4, 4)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(78, 27)
        Me.Label26.TabIndex = 15
        Me.Label26.Text = "  Provider :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Enabled = False
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(3, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 27)
        Me.Label10.TabIndex = 23
        Me.Label10.Text = "From"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Enabled = False
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(3, 3)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1059, 1)
        Me.Label12.TabIndex = 25
        Me.Label12.Text = "From"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Enabled = False
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(3, 31)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1059, 1)
        Me.Label13.TabIndex = 26
        Me.Label13.Text = "From"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ts_ViewButtons)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1065, 54)
        Me.Panel1.TabIndex = 27
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'ts_btnPrintPreview
        '
        Me.ts_btnPrintPreview.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnPrintPreview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnPrintPreview.Image = CType(resources.GetObject("ts_btnPrintPreview.Image"), System.Drawing.Image)
        Me.ts_btnPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnPrintPreview.Name = "ts_btnPrintPreview"
        Me.ts_btnPrintPreview.Size = New System.Drawing.Size(93, 50)
        Me.ts_btnPrintPreview.Tag = "Print Preview"
        Me.ts_btnPrintPreview.Text = "&Print Preview"
        Me.ts_btnPrintPreview.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmOutstandingOrdersAndResult
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1065, 504)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlLeftTopTop)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmOutstandingOrdersAndResult"
        Me.Text = "Outstanding Orders"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.TabOutstandingOrders.ResumeLayout(False)
        Me.tabUnsentOrders.ResumeLayout(False)
        Me.pnlUnsentGrid.ResumeLayout(False)
        CType(Me.GridUnsentOrders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlUnsentSearch.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnlUnsentAging.ResumeLayout(False)
        Me.pnlUnsentAging.PerformLayout()
        Me.pnlUnsentlabel.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.tabUnresulted.ResumeLayout(False)
        Me.pnlUnresultedGrid.ResumeLayout(False)
        CType(Me.GridUnResultedOrders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlUnresultedSearch.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.pnlUnresultedAging.ResumeLayout(False)
        Me.pnlUnresultedAging.PerformLayout()
        Me.pnlUnresultedLabel.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.tabUnacknowledged.ResumeLayout(False)
        Me.Panel16.ResumeLayout(False)
        CType(Me.GridUnAcknowledgedOrders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel13.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        Me.tabAllOrders.ResumeLayout(False)
        Me.Panel21.ResumeLayout(False)
        CType(Me.GridAllOrdersOrders, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel18.ResumeLayout(False)
        Me.Panel19.ResumeLayout(False)
        Me.Panel20.ResumeLayout(False)
        Me.Panel20.PerformLayout()
        Me.Panel17.ResumeLayout(False)
        Me.Panel17.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel27.ResumeLayout(False)
        Me.Panel27.PerformLayout()
        Me.tabUnfinishedOrderTemplates.ResumeLayout(False)
        Me.Panel26.ResumeLayout(False)
        CType(Me.GridUnfinishedOrderTemplates, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel23.ResumeLayout(False)
        Me.Panel24.ResumeLayout(False)
        Me.Panel25.ResumeLayout(False)
        Me.Panel25.PerformLayout()
        Me.Panel22.ResumeLayout(False)
        Me.Panel22.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel28.ResumeLayout(False)
        Me.Panel28.PerformLayout()
        Me.pnlLeftTopTop.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnOpen As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TabOutstandingOrders As System.Windows.Forms.TabControl
    Friend WithEvents tabUnsentOrders As System.Windows.Forms.TabPage
    Friend WithEvents tabUnresulted As System.Windows.Forms.TabPage
    Friend WithEvents tabUnacknowledged As System.Windows.Forms.TabPage
    Friend WithEvents tabAllOrders As System.Windows.Forms.TabPage
    Friend WithEvents tabUnfinishedOrderTemplates As System.Windows.Forms.TabPage
    Friend WithEvents pnlLeftTopTop As System.Windows.Forms.Panel
    Friend WithEvents cmbProviders As System.Windows.Forms.ComboBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents pnlUnsentAging As System.Windows.Forms.Panel
    Friend WithEvents dtUnsentOrdersTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblUnsentOrdersTo As System.Windows.Forms.Label
    Friend WithEvents dtUnsentOrdersFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblUnsentOrdersFrom As System.Windows.Forms.Label
    Friend WithEvents cmbUnsentOrdersDate As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pnlUnsentGrid As System.Windows.Forms.Panel
    Private WithEvents GridUnsentOrders As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents txtSearchUnsentOrders As System.Windows.Forms.TextBox
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents btnClearUnsentOrdersSearch As System.Windows.Forms.Button
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Friend WithEvents pnlUnsentSearch As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents pnlUnresultedGrid As System.Windows.Forms.Panel
    Private WithEvents GridUnResultedOrders As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents pnlUnresultedSearch As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents txtSearchUnresulted As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents btnClearUnresultedSearch As System.Windows.Forms.Button
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents pnlUnresultedAging As System.Windows.Forms.Panel
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents dtUnresultedTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblUnresultedTo As System.Windows.Forms.Label
    Friend WithEvents dtUnresultedFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblUnresultedFrom As System.Windows.Forms.Label
    Friend WithEvents cmbUnresultedDate As System.Windows.Forms.ComboBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Private WithEvents GridUnAcknowledgedOrders As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents txtSearchUnacknowledged As System.Windows.Forms.TextBox
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents btnClearUnacknowledgedSearch As System.Windows.Forms.Button
    Private WithEvents Label47 As System.Windows.Forms.Label
    Private WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents dtUnacknowledgedTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblUnacknowledgedTo As System.Windows.Forms.Label
    Friend WithEvents dtUnacknowledgedFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblUnacknowledgedFrom As System.Windows.Forms.Label
    Friend WithEvents cmbUnacknowledgedDate As System.Windows.Forms.ComboBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents rdoUnacknowledgedTests As System.Windows.Forms.RadioButton
    Friend WithEvents rdoResultedOrderDate As System.Windows.Forms.RadioButton
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Private WithEvents GridAllOrdersOrders As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
    Friend WithEvents txtSearchAllOrders As System.Windows.Forms.TextBox
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents btnClearAllOrdersSearch As System.Windows.Forms.Button
    Private WithEvents Label65 As System.Windows.Forms.Label
    Private WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents dtAllOrdersTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents dtAllOrdersFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents cmbAllOrderDate As System.Windows.Forms.ComboBox
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents cmbAllOrdersStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Panel26 As System.Windows.Forms.Panel
    Private WithEvents GridUnfinishedOrderTemplates As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Label94 As System.Windows.Forms.Label
    Friend WithEvents Label95 As System.Windows.Forms.Label
    Friend WithEvents Label96 As System.Windows.Forms.Label
    Friend WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents Panel23 As System.Windows.Forms.Panel
    Friend WithEvents Panel24 As System.Windows.Forms.Panel
    Friend WithEvents Panel25 As System.Windows.Forms.Panel
    Friend WithEvents txtSearchUnfinishedOrders As System.Windows.Forms.TextBox
    Friend WithEvents Label85 As System.Windows.Forms.Label
    Friend WithEvents btnClearUnfinishedOrdersSearch As System.Windows.Forms.Button
    Private WithEvents Label87 As System.Windows.Forms.Label
    Private WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents Label89 As System.Windows.Forms.Label
    Friend WithEvents Label90 As System.Windows.Forms.Label
    Friend WithEvents Label91 As System.Windows.Forms.Label
    Friend WithEvents Label92 As System.Windows.Forms.Label
    Friend WithEvents Label93 As System.Windows.Forms.Label
    Friend WithEvents Panel22 As System.Windows.Forms.Panel
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents dtUnfinishedOrderTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents dtUnfinishedOrderFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents cmbUnfinishedOrderDate As System.Windows.Forms.ComboBox
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label99 As System.Windows.Forms.Label
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents Label100 As System.Windows.Forms.Label
    Friend WithEvents Label86 As System.Windows.Forms.Label
    Friend WithEvents Label101 As System.Windows.Forms.Label
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents Label102 As System.Windows.Forms.Label
    Friend WithEvents pnlUnsentlabel As System.Windows.Forms.Panel
    Friend WithEvents Label106 As System.Windows.Forms.Label
    Friend WithEvents Label103 As System.Windows.Forms.Label
    Friend WithEvents pnlUnresultedLabel As System.Windows.Forms.Panel
    Friend WithEvents Label104 As System.Windows.Forms.Label
    Friend WithEvents Label105 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label107 As System.Windows.Forms.Label
    Friend WithEvents Label108 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label110 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label109 As System.Windows.Forms.Label
    Friend WithEvents Label111 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label115 As System.Windows.Forms.Label
    Friend WithEvents Label114 As System.Windows.Forms.Label
    Friend WithEvents Label113 As System.Windows.Forms.Label
    Friend WithEvents Label112 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Label133 As System.Windows.Forms.Label
    Friend WithEvents Label134 As System.Windows.Forms.Label
    Friend WithEvents Label135 As System.Windows.Forms.Label
    Friend WithEvents Label136 As System.Windows.Forms.Label
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Label139 As System.Windows.Forms.Label
    Friend WithEvents Label140 As System.Windows.Forms.Label
    Friend WithEvents Label141 As System.Windows.Forms.Label
    Friend WithEvents Label142 As System.Windows.Forms.Label
    Friend WithEvents Panel27 As System.Windows.Forms.Panel
    Friend WithEvents Label145 As System.Windows.Forms.Label
    Friend WithEvents Label146 As System.Windows.Forms.Label
    Friend WithEvents Label147 As System.Windows.Forms.Label
    Friend WithEvents Label148 As System.Windows.Forms.Label
    Friend WithEvents Panel28 As System.Windows.Forms.Panel
    Friend WithEvents Label151 As System.Windows.Forms.Label
    Friend WithEvents Label152 As System.Windows.Forms.Label
    Friend WithEvents Label153 As System.Windows.Forms.Label
    Friend WithEvents Label154 As System.Windows.Forms.Label
    Friend WithEvents ts_btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnPrintPreview As System.Windows.Forms.ToolStripButton
End Class
