<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPHIReview
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPHIReview))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlToolstrip = New System.Windows.Forms.Panel()
        Me.ts_Main = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbbtn_Refresh = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_AcceptPatient = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_RejectPatient = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.pnlPHITaskList = New System.Windows.Forms.Panel()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.c1PHITaskList = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.btnSearchClose = New System.Windows.Forms.Button()
        Me.PicBx_Search = New System.Windows.Forms.PictureBox()
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.panel3 = New System.Windows.Forms.Panel()
        Me.panel5 = New System.Windows.Forms.Panel()
        Me.label13 = New System.Windows.Forms.Label()
        Me.label12 = New System.Windows.Forms.Label()
        Me.label10 = New System.Windows.Forms.Label()
        Me.label15 = New System.Windows.Forms.Label()
        Me.labIntuitPatientList = New System.Windows.Forms.Label()
        Me.splitter1 = New System.Windows.Forms.Splitter()
        Me.pnlPHIDetails = New System.Windows.Forms.Panel()
        Me.pnlProgress = New System.Windows.Forms.Panel()
        Me.Label151 = New System.Windows.Forms.Label()
        Me.Label152 = New System.Windows.Forms.Label()
        Me.Label153 = New System.Windows.Forms.Label()
        Me.Label154 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label155 = New System.Windows.Forms.Label()
        Me.txtSubmissionDate = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtUserComments = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.dgDocuments = New System.Windows.Forms.DataGridView()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtLink = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlToolstrip.SuspendLayout()
        Me.ts_Main.SuspendLayout()
        Me.pnlPHITaskList.SuspendLayout()
        Me.panel4.SuspendLayout()
        CType(Me.c1PHITaskList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel3.SuspendLayout()
        Me.panel5.SuspendLayout()
        Me.pnlPHIDetails.SuspendLayout()
        Me.pnlProgress.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.dgDocuments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlToolstrip
        '
        Me.pnlToolstrip.Controls.Add(Me.ts_Main)
        Me.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolstrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolstrip.Name = "pnlToolstrip"
        Me.pnlToolstrip.Size = New System.Drawing.Size(961, 56)
        Me.pnlToolstrip.TabIndex = 7
        '
        'ts_Main
        '
        Me.ts_Main.BackColor = System.Drawing.Color.Transparent
        Me.ts_Main.BackgroundImage = CType(resources.GetObject("ts_Main.BackgroundImage"), System.Drawing.Image)
        Me.ts_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_Main.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_Main.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_Main.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtn_Refresh, Me.tlbbtn_AcceptPatient, Me.tlbbtn_RejectPatient, Me.tlbbtn_Close})
        Me.ts_Main.Location = New System.Drawing.Point(0, 0)
        Me.ts_Main.Name = "ts_Main"
        Me.ts_Main.Size = New System.Drawing.Size(961, 53)
        Me.ts_Main.TabIndex = 1
        '
        'tlbbtn_Refresh
        '
        Me.tlbbtn_Refresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Refresh.Image = CType(resources.GetObject("tlbbtn_Refresh.Image"), System.Drawing.Image)
        Me.tlbbtn_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Refresh.Name = "tlbbtn_Refresh"
        Me.tlbbtn_Refresh.Size = New System.Drawing.Size(58, 50)
        Me.tlbbtn_Refresh.Tag = "REFRESH"
        Me.tlbbtn_Refresh.Text = "&Refresh"
        Me.tlbbtn_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_AcceptPatient
        '
        Me.tlbbtn_AcceptPatient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_AcceptPatient.Image = CType(resources.GetObject("tlbbtn_AcceptPatient.Image"), System.Drawing.Image)
        Me.tlbbtn_AcceptPatient.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_AcceptPatient.Name = "tlbbtn_AcceptPatient"
        Me.tlbbtn_AcceptPatient.Size = New System.Drawing.Size(53, 50)
        Me.tlbbtn_AcceptPatient.Tag = "NEW&ACCEPT"
        Me.tlbbtn_AcceptPatient.Text = "&Accept"
        Me.tlbbtn_AcceptPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_AcceptPatient.ToolTipText = "Accept Patient"
        '
        'tlbbtn_RejectPatient
        '
        Me.tlbbtn_RejectPatient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_RejectPatient.Image = CType(resources.GetObject("tlbbtn_RejectPatient.Image"), System.Drawing.Image)
        Me.tlbbtn_RejectPatient.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_RejectPatient.Name = "tlbbtn_RejectPatient"
        Me.tlbbtn_RejectPatient.Size = New System.Drawing.Size(50, 50)
        Me.tlbbtn_RejectPatient.Tag = "REJECT"
        Me.tlbbtn_RejectPatient.Text = "R&eject"
        Me.tlbbtn_RejectPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_RejectPatient.ToolTipText = "Reject Patient"
        '
        'tlbbtn_Close
        '
        Me.tlbbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Close.Image = CType(resources.GetObject("tlbbtn_Close.Image"), System.Drawing.Image)
        Me.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Close.Name = "tlbbtn_Close"
        Me.tlbbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tlbbtn_Close.Tag = "CLOSE"
        Me.tlbbtn_Close.Text = "&Close"
        Me.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlPHITaskList
        '
        Me.pnlPHITaskList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlPHITaskList.Controls.Add(Me.panel4)
        Me.pnlPHITaskList.Controls.Add(Me.pnlSearch)
        Me.pnlPHITaskList.Controls.Add(Me.panel3)
        Me.pnlPHITaskList.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlPHITaskList.Location = New System.Drawing.Point(0, 56)
        Me.pnlPHITaskList.Name = "pnlPHITaskList"
        Me.pnlPHITaskList.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlPHITaskList.Size = New System.Drawing.Size(266, 538)
        Me.pnlPHITaskList.TabIndex = 8
        '
        'panel4
        '
        Me.panel4.Controls.Add(Me.c1PHITaskList)
        Me.panel4.Controls.Add(Me.label2)
        Me.panel4.Controls.Add(Me.label3)
        Me.panel4.Controls.Add(Me.label1)
        Me.panel4.Controls.Add(Me.label4)
        Me.panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel4.Location = New System.Drawing.Point(3, 56)
        Me.panel4.Name = "panel4"
        Me.panel4.Size = New System.Drawing.Size(263, 479)
        Me.panel4.TabIndex = 38
        '
        'c1PHITaskList
        '
        Me.c1PHITaskList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1PHITaskList.AllowEditing = False
        Me.c1PHITaskList.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1PHITaskList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1PHITaskList.ColumnInfo = "3,0,0,0,0,95,Columns:1{Width:120;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.c1PHITaskList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1PHITaskList.ExtendLastCol = True
        Me.c1PHITaskList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1PHITaskList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1PHITaskList.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.c1PHITaskList.Location = New System.Drawing.Point(1, 1)
        Me.c1PHITaskList.Name = "c1PHITaskList"
        Me.c1PHITaskList.Rows.Count = 1
        Me.c1PHITaskList.Rows.DefaultSize = 19
        Me.c1PHITaskList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1PHITaskList.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.c1PHITaskList.ShowCellLabels = True
        Me.c1PHITaskList.Size = New System.Drawing.Size(261, 477)
        Me.c1PHITaskList.StyleInfo = resources.GetString("c1PHITaskList.StyleInfo")
        Me.c1PHITaskList.TabIndex = 37
        '
        'label2
        '
        Me.label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label2.Location = New System.Drawing.Point(1, 478)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(261, 1)
        Me.label2.TabIndex = 7
        '
        'label3
        '
        Me.label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.label3.Location = New System.Drawing.Point(262, 1)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(1, 478)
        Me.label3.TabIndex = 6
        '
        'label1
        '
        Me.label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.label1.Location = New System.Drawing.Point(1, 0)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(262, 1)
        Me.label1.TabIndex = 5
        '
        'label4
        '
        Me.label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.label4.Location = New System.Drawing.Point(0, 0)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(1, 479)
        Me.label4.TabIndex = 4
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlSearch.Controls.Add(Me.txtSearch)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlSearch.Controls.Add(Me.btnSearchClose)
        Me.pnlSearch.Controls.Add(Me.PicBx_Search)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchBottomBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchTopBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(3, 28)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.pnlSearch.Size = New System.Drawing.Size(263, 28)
        Me.pnlSearch.TabIndex = 38
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(29, 22)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(215, 2)
        Me.lbl_WhiteSpaceBottom.TabIndex = 41
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Location = New System.Drawing.Point(29, 7)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(215, 15)
        Me.txtSearch.TabIndex = 5
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(29, 4)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(215, 3)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'btnSearchClose
        '
        Me.btnSearchClose.BackColor = System.Drawing.Color.White
        Me.btnSearchClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSearchClose.FlatAppearance.BorderSize = 0
        Me.btnSearchClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSearchClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSearchClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchClose.Image = CType(resources.GetObject("btnSearchClose.Image"), System.Drawing.Image)
        Me.btnSearchClose.Location = New System.Drawing.Point(244, 4)
        Me.btnSearchClose.Name = "btnSearchClose"
        Me.btnSearchClose.Size = New System.Drawing.Size(18, 20)
        Me.btnSearchClose.TabIndex = 32
        Me.btnSearchClose.UseVisualStyleBackColor = False
        '
        'PicBx_Search
        '
        Me.PicBx_Search.BackColor = System.Drawing.Color.White
        Me.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicBx_Search.Image = CType(resources.GetObject("PicBx_Search.Image"), System.Drawing.Image)
        Me.PicBx_Search.Location = New System.Drawing.Point(1, 4)
        Me.PicBx_Search.Name = "PicBx_Search"
        Me.PicBx_Search.Size = New System.Drawing.Size(28, 20)
        Me.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicBx_Search.TabIndex = 9
        Me.PicBx_Search.TabStop = False
        '
        'lbl_pnlSearchBottomBrd
        '
        Me.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlSearchBottomBrd.Location = New System.Drawing.Point(1, 24)
        Me.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd"
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(261, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(1, 3)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(261, 1)
        Me.lbl_pnlSearchTopBrd.TabIndex = 36
        Me.lbl_pnlSearchTopBrd.Text = "label1"
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(0, 3)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 22)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(262, 3)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 22)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'panel3
        '
        Me.panel3.Controls.Add(Me.panel5)
        Me.panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel3.Location = New System.Drawing.Point(3, 0)
        Me.panel3.Name = "panel3"
        Me.panel3.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.panel3.Size = New System.Drawing.Size(263, 28)
        Me.panel3.TabIndex = 38
        '
        'panel5
        '
        Me.panel5.BackColor = System.Drawing.Color.Transparent
        Me.panel5.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_ToolStrip_Small
        Me.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panel5.Controls.Add(Me.label13)
        Me.panel5.Controls.Add(Me.label12)
        Me.panel5.Controls.Add(Me.label10)
        Me.panel5.Controls.Add(Me.label15)
        Me.panel5.Controls.Add(Me.labIntuitPatientList)
        Me.panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel5.Location = New System.Drawing.Point(0, 3)
        Me.panel5.Name = "panel5"
        Me.panel5.Size = New System.Drawing.Size(263, 25)
        Me.panel5.TabIndex = 36
        '
        'label13
        '
        Me.label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.label13.Location = New System.Drawing.Point(1, 0)
        Me.label13.Name = "label13"
        Me.label13.Size = New System.Drawing.Size(261, 1)
        Me.label13.TabIndex = 7
        '
        'label12
        '
        Me.label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.label12.Location = New System.Drawing.Point(262, 0)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(1, 24)
        Me.label12.TabIndex = 6
        '
        'label10
        '
        Me.label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.label10.Location = New System.Drawing.Point(0, 0)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(1, 24)
        Me.label10.TabIndex = 5
        '
        'label15
        '
        Me.label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label15.Location = New System.Drawing.Point(0, 24)
        Me.label15.Name = "label15"
        Me.label15.Size = New System.Drawing.Size(263, 1)
        Me.label15.TabIndex = 2
        '
        'labIntuitPatientList
        '
        Me.labIntuitPatientList.BackColor = System.Drawing.Color.Transparent
        Me.labIntuitPatientList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.labIntuitPatientList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labIntuitPatientList.Location = New System.Drawing.Point(0, 0)
        Me.labIntuitPatientList.Name = "labIntuitPatientList"
        Me.labIntuitPatientList.Size = New System.Drawing.Size(263, 25)
        Me.labIntuitPatientList.TabIndex = 4
        Me.labIntuitPatientList.Text = "PHI Task List"
        Me.labIntuitPatientList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'splitter1
        '
        Me.splitter1.Location = New System.Drawing.Point(266, 56)
        Me.splitter1.Name = "splitter1"
        Me.splitter1.Size = New System.Drawing.Size(3, 538)
        Me.splitter1.TabIndex = 39
        Me.splitter1.TabStop = False
        '
        'pnlPHIDetails
        '
        Me.pnlPHIDetails.Controls.Add(Me.pnlProgress)
        Me.pnlPHIDetails.Controls.Add(Me.txtSubmissionDate)
        Me.pnlPHIDetails.Controls.Add(Me.Label19)
        Me.pnlPHIDetails.Controls.Add(Me.txtUserComments)
        Me.pnlPHIDetails.Controls.Add(Me.Label18)
        Me.pnlPHIDetails.Controls.Add(Me.Label17)
        Me.pnlPHIDetails.Controls.Add(Me.cmbCategory)
        Me.pnlPHIDetails.Controls.Add(Me.Panel1)
        Me.pnlPHIDetails.Controls.Add(Me.Label16)
        Me.pnlPHIDetails.Controls.Add(Me.txtDescription)
        Me.pnlPHIDetails.Controls.Add(Me.Label14)
        Me.pnlPHIDetails.Controls.Add(Me.txtLink)
        Me.pnlPHIDetails.Controls.Add(Me.Label11)
        Me.pnlPHIDetails.Controls.Add(Me.txtTitle)
        Me.pnlPHIDetails.Controls.Add(Me.Label9)
        Me.pnlPHIDetails.Controls.Add(Me.Label5)
        Me.pnlPHIDetails.Controls.Add(Me.Label6)
        Me.pnlPHIDetails.Controls.Add(Me.Label7)
        Me.pnlPHIDetails.Controls.Add(Me.Label8)
        Me.pnlPHIDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlPHIDetails.Location = New System.Drawing.Point(269, 56)
        Me.pnlPHIDetails.Name = "pnlPHIDetails"
        Me.pnlPHIDetails.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pnlPHIDetails.Size = New System.Drawing.Size(692, 538)
        Me.pnlPHIDetails.TabIndex = 40
        '
        'pnlProgress
        '
        Me.pnlProgress.BackColor = System.Drawing.Color.White
        Me.pnlProgress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlProgress.Controls.Add(Me.Label151)
        Me.pnlProgress.Controls.Add(Me.Label152)
        Me.pnlProgress.Controls.Add(Me.Label153)
        Me.pnlProgress.Controls.Add(Me.Label154)
        Me.pnlProgress.Controls.Add(Me.PictureBox2)
        Me.pnlProgress.Controls.Add(Me.Label155)
        Me.pnlProgress.Location = New System.Drawing.Point(237, 183)
        Me.pnlProgress.Name = "pnlProgress"
        Me.pnlProgress.Size = New System.Drawing.Size(219, 173)
        Me.pnlProgress.TabIndex = 42
        Me.pnlProgress.Visible = False
        '
        'Label151
        '
        Me.Label151.BackColor = System.Drawing.Color.FromArgb(CType(CType(206, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(206, Byte), Integer))
        Me.Label151.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label151.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label151.Font = New System.Drawing.Font("Georgia", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label151.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label151.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label151.Location = New System.Drawing.Point(218, 1)
        Me.Label151.Name = "Label151"
        Me.Label151.Size = New System.Drawing.Size(1, 171)
        Me.Label151.TabIndex = 23
        '
        'Label152
        '
        Me.Label152.BackColor = System.Drawing.Color.FromArgb(CType(CType(206, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(206, Byte), Integer))
        Me.Label152.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label152.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label152.Font = New System.Drawing.Font("Georgia", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label152.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label152.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label152.Location = New System.Drawing.Point(0, 1)
        Me.Label152.Name = "Label152"
        Me.Label152.Size = New System.Drawing.Size(1, 171)
        Me.Label152.TabIndex = 22
        '
        'Label153
        '
        Me.Label153.BackColor = System.Drawing.Color.FromArgb(CType(CType(206, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(206, Byte), Integer))
        Me.Label153.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label153.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label153.Font = New System.Drawing.Font("Georgia", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label153.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label153.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label153.Location = New System.Drawing.Point(0, 172)
        Me.Label153.Name = "Label153"
        Me.Label153.Size = New System.Drawing.Size(219, 1)
        Me.Label153.TabIndex = 21
        '
        'Label154
        '
        Me.Label154.BackColor = System.Drawing.Color.FromArgb(CType(CType(206, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(206, Byte), Integer))
        Me.Label154.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label154.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label154.Font = New System.Drawing.Font("Georgia", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label154.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label154.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label154.Location = New System.Drawing.Point(0, 0)
        Me.Label154.Name = "Label154"
        Me.Label154.Size = New System.Drawing.Size(219, 1)
        Me.Label154.TabIndex = 19
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.gloEMR.My.Resources.Resources.Wait
        Me.PictureBox2.Location = New System.Drawing.Point(55, 16)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(100, 98)
        Me.PictureBox2.TabIndex = 18
        Me.PictureBox2.TabStop = False
        '
        'Label155
        '
        Me.Label155.AutoEllipsis = True
        Me.Label155.AutoSize = True
        Me.Label155.BackColor = System.Drawing.Color.Transparent
        Me.Label155.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label155.Font = New System.Drawing.Font("Georgia", 13.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label155.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label155.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label155.Location = New System.Drawing.Point(39, 129)
        Me.Label155.Name = "Label155"
        Me.Label155.Size = New System.Drawing.Size(132, 21)
        Me.Label155.TabIndex = 17
        Me.Label155.Text = "Please Wait!!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'txtSubmissionDate
        '
        Me.txtSubmissionDate.Location = New System.Drawing.Point(123, 436)
        Me.txtSubmissionDate.Name = "txtSubmissionDate"
        Me.txtSubmissionDate.ReadOnly = True
        Me.txtSubmissionDate.Size = New System.Drawing.Size(190, 22)
        Me.txtSubmissionDate.TabIndex = 21
        Me.txtSubmissionDate.Visible = False
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(15, 441)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(104, 14)
        Me.Label19.TabIndex = 20
        Me.Label19.Text = "Submission Date :"
        Me.Label19.Visible = False
        '
        'txtUserComments
        '
        Me.txtUserComments.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUserComments.Location = New System.Drawing.Point(123, 324)
        Me.txtUserComments.Multiline = True
        Me.txtUserComments.Name = "txtUserComments"
        Me.txtUserComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtUserComments.Size = New System.Drawing.Size(541, 106)
        Me.txtUserComments.TabIndex = 19
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(18, 329)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(101, 14)
        Me.Label18.TabIndex = 18
        Me.Label18.Text = "User Comments :"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(55, 300)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(64, 14)
        Me.Label17.TabIndex = 17
        Me.Label17.Text = "Category :"
        '
        'cmbCategory
        '
        Me.cmbCategory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Location = New System.Drawing.Point(123, 296)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(541, 22)
        Me.cmbCategory.TabIndex = 16
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.dgDocuments)
        Me.Panel1.Location = New System.Drawing.Point(123, 184)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(541, 106)
        Me.Panel1.TabIndex = 15
        '
        'dgDocuments
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        Me.dgDocuments.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgDocuments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgDocuments.BackgroundColor = System.Drawing.Color.White
        Me.dgDocuments.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(108, Byte), Integer), CType(CType(143, Byte), Integer), CType(CType(217, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgDocuments.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgDocuments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgDocuments.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgDocuments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgDocuments.EnableHeadersVisualStyles = False
        Me.dgDocuments.GridColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.dgDocuments.Location = New System.Drawing.Point(0, 0)
        Me.dgDocuments.Name = "dgDocuments"
        Me.dgDocuments.RowHeadersVisible = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        Me.dgDocuments.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgDocuments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgDocuments.Size = New System.Drawing.Size(541, 106)
        Me.dgDocuments.TabIndex = 0
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(42, 189)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(77, 14)
        Me.Label16.TabIndex = 14
        Me.Label16.Text = "Documents :"
        '
        'txtDescription
        '
        Me.txtDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescription.Location = New System.Drawing.Point(123, 72)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.ReadOnly = True
        Me.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDescription.Size = New System.Drawing.Size(541, 106)
        Me.txtDescription.TabIndex = 13
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(44, 77)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(75, 14)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "Description :"
        '
        'txtLink
        '
        Me.txtLink.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLink.Location = New System.Drawing.Point(123, 44)
        Me.txtLink.Name = "txtLink"
        Me.txtLink.ReadOnly = True
        Me.txtLink.Size = New System.Drawing.Size(541, 22)
        Me.txtLink.TabIndex = 11
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(83, 49)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(36, 14)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Link :"
        '
        'txtTitle
        '
        Me.txtTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTitle.Location = New System.Drawing.Point(123, 16)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.ReadOnly = True
        Me.txtTitle.Size = New System.Drawing.Size(541, 22)
        Me.txtTitle.TabIndex = 9
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(80, 19)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 14)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Title :"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(1, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(687, 1)
        Me.Label5.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Location = New System.Drawing.Point(688, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 531)
        Me.Label6.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(0, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 531)
        Me.Label7.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Location = New System.Drawing.Point(0, 534)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(689, 1)
        Me.Label8.TabIndex = 2
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmPHIReview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(961, 594)
        Me.Controls.Add(Me.pnlPHIDetails)
        Me.Controls.Add(Me.splitter1)
        Me.Controls.Add(Me.pnlPHITaskList)
        Me.Controls.Add(Me.pnlToolstrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPHIReview"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Portal PHI Review"
        Me.pnlToolstrip.ResumeLayout(False)
        Me.pnlToolstrip.PerformLayout()
        Me.ts_Main.ResumeLayout(False)
        Me.ts_Main.PerformLayout()
        Me.pnlPHITaskList.ResumeLayout(False)
        Me.panel4.ResumeLayout(False)
        CType(Me.c1PHITaskList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel3.ResumeLayout(False)
        Me.panel5.ResumeLayout(False)
        Me.pnlPHIDetails.ResumeLayout(False)
        Me.pnlPHIDetails.PerformLayout()
        Me.pnlProgress.ResumeLayout(False)
        Me.pnlProgress.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgDocuments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents pnlToolstrip As System.Windows.Forms.Panel
    Friend WithEvents ts_Main As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbbtn_AcceptPatient As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_RejectPatient As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Refresh As System.Windows.Forms.ToolStripButton
    Private WithEvents pnlPHITaskList As System.Windows.Forms.Panel
    Private WithEvents panel4 As System.Windows.Forms.Panel
    Friend WithEvents c1PHITaskList As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Private WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Private WithEvents btnSearchClose As System.Windows.Forms.Button
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Private WithEvents panel3 As System.Windows.Forms.Panel
    Private WithEvents panel5 As System.Windows.Forms.Panel
    Private WithEvents label13 As System.Windows.Forms.Label
    Private WithEvents label12 As System.Windows.Forms.Label
    Private WithEvents label10 As System.Windows.Forms.Label
    Private WithEvents label15 As System.Windows.Forms.Label
    Private WithEvents labIntuitPatientList As System.Windows.Forms.Label
    Private WithEvents splitter1 As System.Windows.Forms.Splitter
    Private WithEvents pnlPHIDetails As System.Windows.Forms.Panel
    Friend WithEvents txtUserComments As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents dgDocuments As System.Windows.Forms.DataGridView
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtLink As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents txtSubmissionDate As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents pnlProgress As System.Windows.Forms.Panel
    Private WithEvents Label151 As System.Windows.Forms.Label
    Private WithEvents Label152 As System.Windows.Forms.Label
    Private WithEvents Label153 As System.Windows.Forms.Label
    Private WithEvents Label154 As System.Windows.Forms.Label
    Private WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Private WithEvents Label155 As System.Windows.Forms.Label
End Class
