<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAPIAccessActivationReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAPIAccessActivationReport))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.tlpViewReport = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlpClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlFilterClear = New System.Windows.Forms.Panel()
        Me.panel5 = New System.Windows.Forms.Panel()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbRole = New System.Windows.Forms.ComboBox()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnClearSearch = New System.Windows.Forms.Button()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.label7 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.dtpfrom = New System.Windows.Forms.DateTimePicker()
        Me.cmbPortalAccountStatus = New System.Windows.Forms.ComboBox()
        Me.label5 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.label6 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.cmbProvider = New System.Windows.Forms.ComboBox()
        Me.panel9 = New System.Windows.Forms.Panel()
        Me.btnViewReport = New System.Windows.Forms.Button()
        Me.btnActivation = New System.Windows.Forms.Button()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.label25 = New System.Windows.Forms.Label()
        Me.label16 = New System.Windows.Forms.Label()
        Me.panel6 = New System.Windows.Forms.Panel()
        Me.label21 = New System.Windows.Forms.Label()
        Me.label17 = New System.Windows.Forms.Label()
        Me.label12 = New System.Windows.Forms.Label()
        Me.label14 = New System.Windows.Forms.Label()
        Me.label18 = New System.Windows.Forms.Label()
        Me.btnBlock = New System.Windows.Forms.Button()
        Me.btnunblock = New System.Windows.Forms.Button()
        Me.btnChangeCredential = New System.Windows.Forms.Button()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.label8 = New System.Windows.Forms.Label()
        Me.lblRowCount = New System.Windows.Forms.Label()
        Me.btnClearFilter = New System.Windows.Forms.Button()
        Me.BtnLast = New System.Windows.Forms.Button()
        Me.label15 = New System.Windows.Forms.Label()
        Me.cmbPageSize = New System.Windows.Forms.ComboBox()
        Me.lblSelected = New System.Windows.Forms.Label()
        Me.BtnPrev = New System.Windows.Forms.Button()
        Me.Btn_Next = New System.Windows.Forms.Button()
        Me.BtnFirst = New System.Windows.Forms.Button()
        Me.label26 = New System.Windows.Forms.Label()
        Me.label27 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.pnlResetCredentials = New System.Windows.Forms.Panel()
        Me.btnSaveResetCredentials = New System.Windows.Forms.Button()
        Me.btnClosePanel = New System.Windows.Forms.Button()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtReserPassword = New System.Windows.Forms.TextBox()
        Me.txtResetUserName = New System.Windows.Forms.TextBox()
        Me.chkSelectAll = New System.Windows.Forms.CheckBox()
        Me.gvData = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.tlpViewReport.SuspendLayout()
        Me.panel5.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.panel9.SuspendLayout()
        Me.panel6.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlResetCredentials.SuspendLayout()
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.tlpViewReport)
        Me.Panel1.Controls.Add(Me.pnlFilterClear)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1075, 54)
        Me.Panel1.TabIndex = 91
        '
        'tlpViewReport
        '
        Me.tlpViewReport.BackgroundImage = CType(resources.GetObject("tlpViewReport.BackgroundImage"), System.Drawing.Image)
        Me.tlpViewReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlpViewReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlpViewReport.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlpViewReport.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlpClose})
        Me.tlpViewReport.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlpViewReport.Location = New System.Drawing.Point(0, 0)
        Me.tlpViewReport.Name = "tlpViewReport"
        Me.tlpViewReport.Size = New System.Drawing.Size(1075, 53)
        Me.tlpViewReport.TabIndex = 77
        Me.tlpViewReport.Text = "ToolStrip1"
        '
        'tlpClose
        '
        Me.tlpClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlpClose.Image = CType(resources.GetObject("tlpClose.Image"), System.Drawing.Image)
        Me.tlpClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlpClose.Name = "tlpClose"
        Me.tlpClose.Size = New System.Drawing.Size(51, 50)
        Me.tlpClose.Text = "&Close  "
        Me.tlpClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlFilterClear
        '
        Me.pnlFilterClear.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlFilterClear.Location = New System.Drawing.Point(789, 12)
        Me.pnlFilterClear.Name = "pnlFilterClear"
        Me.pnlFilterClear.Size = New System.Drawing.Size(7, 19)
        Me.pnlFilterClear.TabIndex = 82
        '
        'panel5
        '
        Me.panel5.Controls.Add(Me.Label30)
        Me.panel5.Controls.Add(Me.Label3)
        Me.panel5.Controls.Add(Me.cmbRole)
        Me.panel5.Controls.Add(Me.pnlSearch)
        Me.panel5.Controls.Add(Me.label7)
        Me.panel5.Controls.Add(Me.label4)
        Me.panel5.Controls.Add(Me.dtpfrom)
        Me.panel5.Controls.Add(Me.cmbPortalAccountStatus)
        Me.panel5.Controls.Add(Me.label5)
        Me.panel5.Controls.Add(Me.dtpTo)
        Me.panel5.Controls.Add(Me.label6)
        Me.panel5.Controls.Add(Me.label1)
        Me.panel5.Controls.Add(Me.cmbProvider)
        Me.panel5.Controls.Add(Me.panel9)
        Me.panel5.Controls.Add(Me.label16)
        Me.panel5.Controls.Add(Me.panel6)
        Me.panel5.Controls.Add(Me.label12)
        Me.panel5.Controls.Add(Me.label14)
        Me.panel5.Controls.Add(Me.label18)
        Me.panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel5.Location = New System.Drawing.Point(0, 54)
        Me.panel5.Name = "panel5"
        Me.panel5.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.panel5.Size = New System.Drawing.Size(1075, 141)
        Me.panel5.TabIndex = 92
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(409, 44)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(99, 14)
        Me.Label30.TabIndex = 93
        Me.Label30.Text = "Activation Date :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(88, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 14)
        Me.Label3.TabIndex = 92
        Me.Label3.Text = "Role :"
        '
        'cmbRole
        '
        Me.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRole.FormattingEnabled = True
        Me.cmbRole.Items.AddRange(New Object() {"Patient", "Patient Representative", "Others"})
        Me.cmbRole.Location = New System.Drawing.Point(131, 40)
        Me.cmbRole.Name = "cmbRole"
        Me.cmbRole.Size = New System.Drawing.Size(243, 22)
        Me.cmbRole.TabIndex = 91
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearch.Controls.Add(Me.txtSearch)
        Me.pnlSearch.Controls.Add(Me.btnClearSearch)
        Me.pnlSearch.Controls.Add(Me.Label77)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlSearch.Controls.Add(Me.Label23)
        Me.pnlSearch.Controls.Add(Me.Label24)
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(132, 71)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(243, 23)
        Me.pnlSearch.TabIndex = 90
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Location = New System.Drawing.Point(5, 4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(216, 15)
        Me.txtSearch.TabIndex = 1
        '
        'btnClearSearch
        '
        Me.btnClearSearch.BackColor = System.Drawing.Color.White
        Me.btnClearSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearSearch.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearSearch.FlatAppearance.BorderSize = 0
        Me.btnClearSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearSearch.Image = CType(resources.GetObject("btnClearSearch.Image"), System.Drawing.Image)
        Me.btnClearSearch.Location = New System.Drawing.Point(221, 4)
        Me.btnClearSearch.Name = "btnClearSearch"
        Me.btnClearSearch.Size = New System.Drawing.Size(21, 13)
        Me.btnClearSearch.TabIndex = 89
        Me.btnClearSearch.UseVisualStyleBackColor = False
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.White
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label77.Location = New System.Drawing.Point(5, 17)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(237, 5)
        Me.Label77.TabIndex = 0
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(5, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(237, 3)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(1, 1)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(4, 21)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.Gray
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 21)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.Gray
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(242, 1)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 21)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.Gray
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 22)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(243, 1)
        Me.Label23.TabIndex = 44
        Me.Label23.Text = "3"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.Gray
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(0, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(243, 1)
        Me.Label24.TabIndex = 45
        Me.Label24.Text = "3"
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.Location = New System.Drawing.Point(74, 75)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(52, 14)
        Me.label7.TabIndex = 88
        Me.label7.Text = "Search :"
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.Location = New System.Drawing.Point(511, 44)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(34, 14)
        Me.label4.TabIndex = 83
        Me.label4.Text = "From"
        '
        'dtpfrom
        '
        Me.dtpfrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfrom.Location = New System.Drawing.Point(547, 40)
        Me.dtpfrom.Name = "dtpfrom"
        Me.dtpfrom.ShowCheckBox = True
        Me.dtpfrom.Size = New System.Drawing.Size(104, 22)
        Me.dtpfrom.TabIndex = 3
        Me.dtpfrom.Value = New Date(2014, 4, 25, 15, 3, 16, 0)
        '
        'cmbPortalAccountStatus
        '
        Me.cmbPortalAccountStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPortalAccountStatus.FormattingEnabled = True
        Me.cmbPortalAccountStatus.Items.AddRange(New Object() {"", "ACTIVATED", "NOT ACTIVATED", "BLOCKED"})
        Me.cmbPortalAccountStatus.Location = New System.Drawing.Point(132, 103)
        Me.cmbPortalAccountStatus.Name = "cmbPortalAccountStatus"
        Me.cmbPortalAccountStatus.Size = New System.Drawing.Size(243, 22)
        Me.cmbPortalAccountStatus.TabIndex = 0
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(665, 44)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(22, 14)
        Me.label5.TabIndex = 84
        Me.label5.Text = "To"
        '
        'dtpTo
        '
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(690, 40)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.ShowCheckBox = True
        Me.dtpTo.Size = New System.Drawing.Size(104, 22)
        Me.dtpTo.TabIndex = 4
        Me.dtpTo.Value = New Date(2014, 4, 25, 15, 3, 16, 0)
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.Location = New System.Drawing.Point(26, 107)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(100, 14)
        Me.label6.TabIndex = 85
        Me.label6.Text = "Account Status :"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.Location = New System.Drawing.Point(449, 75)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(59, 14)
        Me.label1.TabIndex = 77
        Me.label1.Text = "Provider :"
        '
        'cmbProvider
        '
        Me.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProvider.FormattingEnabled = True
        Me.cmbProvider.Location = New System.Drawing.Point(512, 71)
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.Size = New System.Drawing.Size(282, 22)
        Me.cmbProvider.TabIndex = 1
        '
        'panel9
        '
        Me.panel9.Controls.Add(Me.btnViewReport)
        Me.panel9.Controls.Add(Me.btnActivation)
        Me.panel9.Controls.Add(Me.btnReset)
        Me.panel9.Controls.Add(Me.label25)
        Me.panel9.Dock = System.Windows.Forms.DockStyle.Right
        Me.panel9.Location = New System.Drawing.Point(903, 29)
        Me.panel9.Name = "panel9"
        Me.panel9.Size = New System.Drawing.Size(168, 111)
        Me.panel9.TabIndex = 76
        '
        'btnViewReport
        '
        Me.btnViewReport.BackgroundImage = CType(resources.GetObject("btnViewReport.BackgroundImage"), System.Drawing.Image)
        Me.btnViewReport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnViewReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnViewReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnViewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewReport.Location = New System.Drawing.Point(12, 6)
        Me.btnViewReport.Name = "btnViewReport"
        Me.btnViewReport.Size = New System.Drawing.Size(146, 29)
        Me.btnViewReport.TabIndex = 0
        Me.btnViewReport.Text = "View Report"
        Me.btnViewReport.UseVisualStyleBackColor = True
        '
        'btnActivation
        '
        Me.btnActivation.BackgroundImage = CType(resources.GetObject("btnActivation.BackgroundImage"), System.Drawing.Image)
        Me.btnActivation.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnActivation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnActivation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnActivation.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnActivation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnActivation.Location = New System.Drawing.Point(12, 41)
        Me.btnActivation.Name = "btnActivation"
        Me.btnActivation.Size = New System.Drawing.Size(146, 29)
        Me.btnActivation.TabIndex = 1
        Me.btnActivation.Text = "Activate"
        Me.btnActivation.UseVisualStyleBackColor = True
        '
        'btnReset
        '
        Me.btnReset.BackgroundImage = CType(resources.GetObject("btnReset.BackgroundImage"), System.Drawing.Image)
        Me.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReset.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReset.Location = New System.Drawing.Point(12, 76)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(146, 29)
        Me.btnReset.TabIndex = 2
        Me.btnReset.Text = "Reset Report"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'label25
        '
        Me.label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label25.Dock = System.Windows.Forms.DockStyle.Left
        Me.label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label25.Location = New System.Drawing.Point(0, 0)
        Me.label25.Name = "label25"
        Me.label25.Size = New System.Drawing.Size(1, 111)
        Me.label25.TabIndex = 39
        Me.label25.Text = "3"
        '
        'label16
        '
        Me.label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label16.Location = New System.Drawing.Point(4, 140)
        Me.label16.Name = "label16"
        Me.label16.Size = New System.Drawing.Size(1067, 1)
        Me.label16.TabIndex = 39
        Me.label16.Text = "3"
        '
        'panel6
        '
        Me.panel6.BackgroundImage = CType(resources.GetObject("panel6.BackgroundImage"), System.Drawing.Image)
        Me.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panel6.Controls.Add(Me.label21)
        Me.panel6.Controls.Add(Me.label17)
        Me.panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel6.Location = New System.Drawing.Point(4, 4)
        Me.panel6.Name = "panel6"
        Me.panel6.Size = New System.Drawing.Size(1067, 25)
        Me.panel6.TabIndex = 36
        '
        'label21
        '
        Me.label21.BackColor = System.Drawing.Color.Transparent
        Me.label21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label21.Location = New System.Drawing.Point(0, 0)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(1067, 24)
        Me.label21.TabIndex = 0
        Me.label21.Text = "  Filter Criteria"
        Me.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label17
        '
        Me.label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label17.Location = New System.Drawing.Point(0, 24)
        Me.label17.Name = "label17"
        Me.label17.Size = New System.Drawing.Size(1067, 1)
        Me.label17.TabIndex = 1
        Me.label17.Text = "3"
        '
        'label12
        '
        Me.label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label12.Location = New System.Drawing.Point(3, 4)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(1, 137)
        Me.label12.TabIndex = 37
        Me.label12.Text = "3"
        '
        'label14
        '
        Me.label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label14.Location = New System.Drawing.Point(1071, 4)
        Me.label14.Name = "label14"
        Me.label14.Size = New System.Drawing.Size(1, 137)
        Me.label14.TabIndex = 38
        Me.label14.Text = "3"
        '
        'label18
        '
        Me.label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label18.Location = New System.Drawing.Point(3, 3)
        Me.label18.Name = "label18"
        Me.label18.Size = New System.Drawing.Size(1069, 1)
        Me.label18.TabIndex = 40
        Me.label18.Text = "3"
        '
        'btnBlock
        '
        Me.btnBlock.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBlock.BackgroundImage = CType(resources.GetObject("btnBlock.BackgroundImage"), System.Drawing.Image)
        Me.btnBlock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBlock.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBlock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnBlock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnBlock.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBlock.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBlock.Location = New System.Drawing.Point(665, 6)
        Me.btnBlock.Name = "btnBlock"
        Me.btnBlock.Size = New System.Drawing.Size(59, 25)
        Me.btnBlock.TabIndex = 94
        Me.btnBlock.Text = "Block"
        Me.btnBlock.UseVisualStyleBackColor = True
        '
        'btnunblock
        '
        Me.btnunblock.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnunblock.BackgroundImage = CType(resources.GetObject("btnunblock.BackgroundImage"), System.Drawing.Image)
        Me.btnunblock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnunblock.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnunblock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnunblock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnunblock.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnunblock.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnunblock.Location = New System.Drawing.Point(730, 6)
        Me.btnunblock.Name = "btnunblock"
        Me.btnunblock.Size = New System.Drawing.Size(72, 25)
        Me.btnunblock.TabIndex = 93
        Me.btnunblock.Text = "Unblock"
        Me.btnunblock.UseVisualStyleBackColor = True
        '
        'btnChangeCredential
        '
        Me.btnChangeCredential.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChangeCredential.BackgroundImage = CType(resources.GetObject("btnChangeCredential.BackgroundImage"), System.Drawing.Image)
        Me.btnChangeCredential.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnChangeCredential.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnChangeCredential.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnChangeCredential.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnChangeCredential.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnChangeCredential.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangeCredential.Location = New System.Drawing.Point(808, 6)
        Me.btnChangeCredential.Name = "btnChangeCredential"
        Me.btnChangeCredential.Size = New System.Drawing.Size(134, 25)
        Me.btnChangeCredential.TabIndex = 40
        Me.btnChangeCredential.Text = "Change Credentials"
        Me.btnChangeCredential.UseVisualStyleBackColor = True
        '
        'panel4
        '
        Me.panel4.BackColor = System.Drawing.Color.Transparent
        Me.panel4.Controls.Add(Me.btnBlock)
        Me.panel4.Controls.Add(Me.btnunblock)
        Me.panel4.Controls.Add(Me.btnChangeCredential)
        Me.panel4.Controls.Add(Me.Label22)
        Me.panel4.Controls.Add(Me.Label20)
        Me.panel4.Controls.Add(Me.Label19)
        Me.panel4.Controls.Add(Me.Label13)
        Me.panel4.Controls.Add(Me.label8)
        Me.panel4.Controls.Add(Me.lblRowCount)
        Me.panel4.Controls.Add(Me.btnClearFilter)
        Me.panel4.Controls.Add(Me.BtnLast)
        Me.panel4.Controls.Add(Me.label15)
        Me.panel4.Controls.Add(Me.cmbPageSize)
        Me.panel4.Controls.Add(Me.lblSelected)
        Me.panel4.Controls.Add(Me.BtnPrev)
        Me.panel4.Controls.Add(Me.Btn_Next)
        Me.panel4.Controls.Add(Me.BtnFirst)
        Me.panel4.Controls.Add(Me.label26)
        Me.panel4.Controls.Add(Me.label27)
        Me.panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel4.Location = New System.Drawing.Point(0, 195)
        Me.panel4.Name = "panel4"
        Me.panel4.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.panel4.Size = New System.Drawing.Size(1075, 40)
        Me.panel4.TabIndex = 93
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(4, 2)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1067, 1)
        Me.Label22.TabIndex = 88
        Me.Label22.Text = "3"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(4, 35)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1067, 1)
        Me.Label20.TabIndex = 87
        Me.Label20.Text = "3"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(1071, 2)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 34)
        Me.Label19.TabIndex = 86
        Me.Label19.Text = "3"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(3, 2)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 34)
        Me.Label13.TabIndex = 85
        Me.Label13.Text = "3"
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label8.Location = New System.Drawing.Point(423, 11)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(85, 14)
        Me.label8.TabIndex = 84
        Me.label8.Text = "Total Record :"
        '
        'lblRowCount
        '
        Me.lblRowCount.AutoSize = True
        Me.lblRowCount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRowCount.Location = New System.Drawing.Point(512, 12)
        Me.lblRowCount.Name = "lblRowCount"
        Me.lblRowCount.Size = New System.Drawing.Size(42, 13)
        Me.lblRowCount.TabIndex = 83
        Me.lblRowCount.Text = "44444"
        '
        'btnClearFilter
        '
        Me.btnClearFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearFilter.BackgroundImage = CType(resources.GetObject("btnClearFilter.BackgroundImage"), System.Drawing.Image)
        Me.btnClearFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearFilter.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearFilter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearFilter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearFilter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearFilter.Location = New System.Drawing.Point(948, 6)
        Me.btnClearFilter.Name = "btnClearFilter"
        Me.btnClearFilter.Size = New System.Drawing.Size(111, 25)
        Me.btnClearFilter.TabIndex = 1
        Me.btnClearFilter.Text = "Clear Grid Filter"
        Me.btnClearFilter.UseVisualStyleBackColor = True
        '
        'BtnLast
        '
        Me.BtnLast.BackColor = System.Drawing.Color.Transparent
        Me.BtnLast.BackgroundImage = CType(resources.GetObject("BtnLast.BackgroundImage"), System.Drawing.Image)
        Me.BtnLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.BtnLast.FlatAppearance.BorderSize = 0
        Me.BtnLast.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.BtnLast.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.BtnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnLast.Location = New System.Drawing.Point(402, 7)
        Me.BtnLast.Name = "BtnLast"
        Me.BtnLast.Size = New System.Drawing.Size(24, 23)
        Me.BtnLast.TabIndex = 80
        Me.BtnLast.UseVisualStyleBackColor = False
        '
        'label15
        '
        Me.label15.AutoSize = True
        Me.label15.BackColor = System.Drawing.Color.Transparent
        Me.label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label15.Location = New System.Drawing.Point(16, 11)
        Me.label15.Name = "label15"
        Me.label15.Size = New System.Drawing.Size(111, 14)
        Me.label15.TabIndex = 74
        Me.label15.Text = "Records per Page :"
        '
        'cmbPageSize
        '
        Me.cmbPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPageSize.FormattingEnabled = True
        Me.cmbPageSize.Location = New System.Drawing.Point(132, 7)
        Me.cmbPageSize.Name = "cmbPageSize"
        Me.cmbPageSize.Size = New System.Drawing.Size(82, 22)
        Me.cmbPageSize.TabIndex = 0
        '
        'lblSelected
        '
        Me.lblSelected.AutoSize = True
        Me.lblSelected.BackColor = System.Drawing.Color.Transparent
        Me.lblSelected.Font = New System.Drawing.Font("Tahoma", 9.5!, System.Drawing.FontStyle.Bold)
        Me.lblSelected.Location = New System.Drawing.Point(273, 9)
        Me.lblSelected.Name = "lblSelected"
        Me.lblSelected.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.lblSelected.Size = New System.Drawing.Size(81, 19)
        Me.lblSelected.TabIndex = 79
        Me.lblSelected.Text = "Page 1 of 1"
        Me.lblSelected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnPrev
        '
        Me.BtnPrev.BackColor = System.Drawing.Color.Transparent
        Me.BtnPrev.BackgroundImage = CType(resources.GetObject("BtnPrev.BackgroundImage"), System.Drawing.Image)
        Me.BtnPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.BtnPrev.FlatAppearance.BorderSize = 0
        Me.BtnPrev.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.BtnPrev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.BtnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPrev.Location = New System.Drawing.Point(246, 7)
        Me.BtnPrev.Name = "BtnPrev"
        Me.BtnPrev.Size = New System.Drawing.Size(24, 23)
        Me.BtnPrev.TabIndex = 77
        Me.BtnPrev.UseVisualStyleBackColor = False
        '
        'Btn_Next
        '
        Me.Btn_Next.BackColor = System.Drawing.Color.Transparent
        Me.Btn_Next.BackgroundImage = CType(resources.GetObject("Btn_Next.BackgroundImage"), System.Drawing.Image)
        Me.Btn_Next.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Btn_Next.FlatAppearance.BorderSize = 0
        Me.Btn_Next.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.Btn_Next.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.Btn_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Next.Location = New System.Drawing.Point(378, 7)
        Me.Btn_Next.Name = "Btn_Next"
        Me.Btn_Next.Size = New System.Drawing.Size(24, 23)
        Me.Btn_Next.TabIndex = 78
        Me.Btn_Next.UseVisualStyleBackColor = False
        '
        'BtnFirst
        '
        Me.BtnFirst.BackColor = System.Drawing.Color.Transparent
        Me.BtnFirst.BackgroundImage = CType(resources.GetObject("BtnFirst.BackgroundImage"), System.Drawing.Image)
        Me.BtnFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.BtnFirst.FlatAppearance.BorderSize = 0
        Me.BtnFirst.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.BtnFirst.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.BtnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFirst.Location = New System.Drawing.Point(222, 7)
        Me.BtnFirst.Name = "BtnFirst"
        Me.BtnFirst.Size = New System.Drawing.Size(24, 23)
        Me.BtnFirst.TabIndex = 76
        Me.BtnFirst.UseVisualStyleBackColor = False
        '
        'label26
        '
        Me.label26.BackColor = System.Drawing.Color.Transparent
        Me.label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label26.Location = New System.Drawing.Point(3, 0)
        Me.label26.Name = "label26"
        Me.label26.Size = New System.Drawing.Size(1069, 2)
        Me.label26.TabIndex = 72
        Me.label26.Text = "3"
        '
        'label27
        '
        Me.label27.BackColor = System.Drawing.Color.Transparent
        Me.label27.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label27.Location = New System.Drawing.Point(3, 36)
        Me.label27.Name = "label27"
        Me.label27.Size = New System.Drawing.Size(1069, 1)
        Me.label27.TabIndex = 73
        Me.label27.Text = "3"
        '
        'Panel2
        '
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.pnlResetCredentials)
        Me.Panel2.Controls.Add(Me.chkSelectAll)
        Me.Panel2.Controls.Add(Me.gvData)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 235)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel2.Size = New System.Drawing.Size(1075, 344)
        Me.Panel2.TabIndex = 94
        '
        'pnlResetCredentials
        '
        Me.pnlResetCredentials.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlResetCredentials.Controls.Add(Me.btnSaveResetCredentials)
        Me.pnlResetCredentials.Controls.Add(Me.btnClosePanel)
        Me.pnlResetCredentials.Controls.Add(Me.Label29)
        Me.pnlResetCredentials.Controls.Add(Me.Label28)
        Me.pnlResetCredentials.Controls.Add(Me.txtReserPassword)
        Me.pnlResetCredentials.Controls.Add(Me.txtResetUserName)
        Me.pnlResetCredentials.Location = New System.Drawing.Point(338, 107)
        Me.pnlResetCredentials.Name = "pnlResetCredentials"
        Me.pnlResetCredentials.Size = New System.Drawing.Size(399, 131)
        Me.pnlResetCredentials.TabIndex = 92
        Me.pnlResetCredentials.Visible = False
        '
        'btnSaveResetCredentials
        '
        Me.btnSaveResetCredentials.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveResetCredentials.BackgroundImage = CType(resources.GetObject("btnSaveResetCredentials.BackgroundImage"), System.Drawing.Image)
        Me.btnSaveResetCredentials.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSaveResetCredentials.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnSaveResetCredentials.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSaveResetCredentials.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSaveResetCredentials.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveResetCredentials.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveResetCredentials.Location = New System.Drawing.Point(153, 83)
        Me.btnSaveResetCredentials.Name = "btnSaveResetCredentials"
        Me.btnSaveResetCredentials.Size = New System.Drawing.Size(72, 27)
        Me.btnSaveResetCredentials.TabIndex = 96
        Me.btnSaveResetCredentials.Text = "&Save"
        Me.btnSaveResetCredentials.UseVisualStyleBackColor = True
        '
        'btnClosePanel
        '
        Me.btnClosePanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClosePanel.BackgroundImage = CType(resources.GetObject("btnClosePanel.BackgroundImage"), System.Drawing.Image)
        Me.btnClosePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClosePanel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClosePanel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClosePanel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClosePanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClosePanel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClosePanel.Location = New System.Drawing.Point(231, 83)
        Me.btnClosePanel.Name = "btnClosePanel"
        Me.btnClosePanel.Size = New System.Drawing.Size(72, 27)
        Me.btnClosePanel.TabIndex = 95
        Me.btnClosePanel.Text = "&Cancel"
        Me.btnClosePanel.UseVisualStyleBackColor = True
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(39, 52)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(66, 14)
        Me.Label29.TabIndex = 3
        Me.Label29.Text = "Password :"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(31, 22)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(74, 14)
        Me.Label28.TabIndex = 2
        Me.Label28.Text = "User Name :"
        '
        'txtReserPassword
        '
        Me.txtReserPassword.Location = New System.Drawing.Point(108, 49)
        Me.txtReserPassword.MaxLength = 20
        Me.txtReserPassword.Name = "txtReserPassword"
        Me.txtReserPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtReserPassword.Size = New System.Drawing.Size(258, 22)
        Me.txtReserPassword.TabIndex = 1
        '
        'txtResetUserName
        '
        Me.txtResetUserName.Location = New System.Drawing.Point(108, 19)
        Me.txtResetUserName.MaxLength = 20
        Me.txtResetUserName.Name = "txtResetUserName"
        Me.txtResetUserName.Size = New System.Drawing.Size(258, 22)
        Me.txtResetUserName.TabIndex = 0
        '
        'chkSelectAll
        '
        Me.chkSelectAll.BackColor = System.Drawing.Color.Transparent
        Me.chkSelectAll.Location = New System.Drawing.Point(36, 8)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(12, 11)
        Me.chkSelectAll.TabIndex = 91
        Me.chkSelectAll.UseVisualStyleBackColor = False
        '
        'gvData
        '
        Me.gvData.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.gvData.AllowFiltering = True
        Me.gvData.AutoSearch = C1.Win.C1FlexGrid.AutoSearchEnum.FromTop
        Me.gvData.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvData.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.gvData.ColumnInfo = resources.GetString("gvData.ColumnInfo")
        Me.gvData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvData.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
        Me.gvData.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvData.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gvData.Location = New System.Drawing.Point(4, 1)
        Me.gvData.Name = "gvData"
        Me.gvData.Rows.DefaultSize = 21
        Me.gvData.ScrollOptions = C1.Win.C1FlexGrid.ScrollFlags.AlwaysVisible
        Me.gvData.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.gvData.Size = New System.Drawing.Size(1067, 339)
        Me.gvData.StyleInfo = resources.GetString("gvData.StyleInfo")
        Me.gvData.TabIndex = 76
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(1071, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 339)
        Me.Label11.TabIndex = 39
        Me.Label11.Text = "3"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 339)
        Me.Label10.TabIndex = 38
        Me.Label10.Text = "3"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1069, 1)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 340)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1069, 1)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "3"
        '
        'frmAPIAccessActivationReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1075, 579)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.panel4)
        Me.Controls.Add(Me.panel5)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAPIAccessActivationReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "API Activation Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tlpViewReport.ResumeLayout(False)
        Me.tlpViewReport.PerformLayout()
        Me.panel5.ResumeLayout(False)
        Me.panel5.PerformLayout()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.panel9.ResumeLayout(False)
        Me.panel6.ResumeLayout(False)
        Me.panel4.ResumeLayout(False)
        Me.panel4.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.pnlResetCredentials.ResumeLayout(False)
        Me.pnlResetCredentials.PerformLayout()
        CType(Me.gvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents tlpViewReport As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlpClose As System.Windows.Forms.ToolStripButton
    Private WithEvents panel5 As System.Windows.Forms.Panel
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Private WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnClearSearch As System.Windows.Forms.Button
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents label7 As System.Windows.Forms.Label
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents dtpfrom As System.Windows.Forms.DateTimePicker
    Private WithEvents cmbPortalAccountStatus As System.Windows.Forms.ComboBox
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Private WithEvents panel9 As System.Windows.Forms.Panel
    Private WithEvents btnViewReport As System.Windows.Forms.Button
    Private WithEvents btnActivation As System.Windows.Forms.Button
    Private WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents label25 As System.Windows.Forms.Label
    Friend WithEvents label16 As System.Windows.Forms.Label
    Private WithEvents panel6 As System.Windows.Forms.Panel
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents label17 As System.Windows.Forms.Label
    Friend WithEvents label12 As System.Windows.Forms.Label
    Friend WithEvents label14 As System.Windows.Forms.Label
    Friend WithEvents label18 As System.Windows.Forms.Label
    Private WithEvents panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents label8 As System.Windows.Forms.Label
    Private WithEvents lblRowCount As System.Windows.Forms.Label
    Private WithEvents btnClearFilter As System.Windows.Forms.Button
    Private WithEvents pnlFilterClear As System.Windows.Forms.Panel
    Private WithEvents BtnLast As System.Windows.Forms.Button
    Private WithEvents label15 As System.Windows.Forms.Label
    Private WithEvents cmbPageSize As System.Windows.Forms.ComboBox
    Private WithEvents lblSelected As System.Windows.Forms.Label
    Private WithEvents BtnPrev As System.Windows.Forms.Button
    Private WithEvents Btn_Next As System.Windows.Forms.Button
    Private WithEvents BtnFirst As System.Windows.Forms.Button
    Friend WithEvents label26 As System.Windows.Forms.Label
    Friend WithEvents label27 As System.Windows.Forms.Label
    Private WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkSelectAll As System.Windows.Forms.CheckBox
    Private WithEvents gvData As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents cmbRole As System.Windows.Forms.ComboBox
    Private WithEvents btnunblock As System.Windows.Forms.Button
    Private WithEvents btnChangeCredential As System.Windows.Forms.Button
    Private WithEvents btnBlock As System.Windows.Forms.Button
    Friend WithEvents pnlResetCredentials As System.Windows.Forms.Panel
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtReserPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtResetUserName As System.Windows.Forms.TextBox
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents btnSaveResetCredentials As System.Windows.Forms.Button
    Private WithEvents btnClosePanel As System.Windows.Forms.Button
End Class
