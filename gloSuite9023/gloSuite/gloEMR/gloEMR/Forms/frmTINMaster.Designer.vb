<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTINMaster
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTINMaster))
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.ts_Commands = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tsb_OK = New System.Windows.Forms.ToolStripButton()
        Me.tsb_Cancel = New System.Windows.Forms.ToolStripButton()
        Me.pnl_Bottom = New System.Windows.Forms.Panel()
        Me.gvProvider = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.panel5 = New System.Windows.Forms.Panel()
        Me.label13 = New System.Windows.Forms.Label()
        Me.label14 = New System.Windows.Forms.Label()
        Me.btnDeactivateProvider = New System.Windows.Forms.Button()
        Me.btnAddProvider = New System.Windows.Forms.Button()
        Me.label20 = New System.Windows.Forms.Label()
        Me.label21 = New System.Windows.Forms.Label()
        Me.label6 = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.pnlGIContactDetails = New System.Windows.Forms.Panel()
        Me.panel2 = New System.Windows.Forms.Panel()
        Me.label7 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.txtTINTitle = New System.Windows.Forms.TextBox()
        Me.lblAccountDescription = New System.Windows.Forms.Label()
        Me.txtTINNo = New System.Windows.Forms.TextBox()
        Me.lblTINNo = New System.Windows.Forms.Label()
        Me.label39 = New System.Windows.Forms.Label()
        Me.pnlAccountInfo = New System.Windows.Forms.Panel()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.pnlGIHeader = New System.Windows.Forms.Panel()
        Me.label9 = New System.Windows.Forms.Label()
        Me.label10 = New System.Windows.Forms.Label()
        Me.label11 = New System.Windows.Forms.Label()
        Me.label12 = New System.Windows.Forms.Label()
        Me.lblGIHeader = New System.Windows.Forms.Label()
        Me.pnlContainer = New System.Windows.Forms.Panel()
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlControl = New System.Windows.Forms.Panel()
        Me.pnlTop.SuspendLayout()
        Me.ts_Commands.SuspendLayout()
        Me.pnl_Bottom.SuspendLayout()
        CType(Me.gvProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel5.SuspendLayout()
        Me.pnlGIContactDetails.SuspendLayout()
        Me.panel2.SuspendLayout()
        Me.pnlAccountInfo.SuspendLayout()
        Me.panel1.SuspendLayout()
        Me.pnlGIHeader.SuspendLayout()
        Me.pnlContainer.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.AutoSize = True
        Me.pnlTop.Controls.Add(Me.ts_Commands)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(744, 53)
        Me.pnlTop.TabIndex = 28
        '
        'ts_Commands
        '
        Me.ts_Commands.BackColor = System.Drawing.Color.Transparent
        Me.ts_Commands.BackgroundImage = CType(resources.GetObject("ts_Commands.BackgroundImage"), System.Drawing.Image)
        Me.ts_Commands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_Commands.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_Commands.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_Commands.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsb_OK, Me.tsb_Cancel})
        Me.ts_Commands.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ts_Commands.Location = New System.Drawing.Point(0, 0)
        Me.ts_Commands.Name = "ts_Commands"
        Me.ts_Commands.Size = New System.Drawing.Size(744, 53)
        Me.ts_Commands.TabIndex = 22
        Me.ts_Commands.Text = "ToolStrip1"
        '
        'tsb_OK
        '
        Me.tsb_OK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_OK.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsb_OK.Image = CType(resources.GetObject("tsb_OK.Image"), System.Drawing.Image)
        Me.tsb_OK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_OK.Name = "tsb_OK"
        Me.tsb_OK.Size = New System.Drawing.Size(66, 50)
        Me.tsb_OK.Tag = "Save"
        Me.tsb_OK.Text = "&Save&&Cls"
        Me.tsb_OK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsb_OK.ToolTipText = "Save and Close"
        '
        'tsb_Cancel
        '
        Me.tsb_Cancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_Cancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsb_Cancel.Image = CType(resources.GetObject("tsb_Cancel.Image"), System.Drawing.Image)
        Me.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_Cancel.Name = "tsb_Cancel"
        Me.tsb_Cancel.Size = New System.Drawing.Size(43, 50)
        Me.tsb_Cancel.Tag = "Cancel"
        Me.tsb_Cancel.Text = "&Close"
        Me.tsb_Cancel.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal
        Me.tsb_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnl_Bottom
        '
        Me.pnl_Bottom.Controls.Add(Me.gvProvider)
        Me.pnl_Bottom.Controls.Add(Me.panel5)
        Me.pnl_Bottom.Controls.Add(Me.label20)
        Me.pnl_Bottom.Controls.Add(Me.label21)
        Me.pnl_Bottom.Controls.Add(Me.label6)
        Me.pnl_Bottom.Controls.Add(Me.label5)
        Me.pnl_Bottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Bottom.Location = New System.Drawing.Point(0, 95)
        Me.pnl_Bottom.Name = "pnl_Bottom"
        Me.pnl_Bottom.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnl_Bottom.Size = New System.Drawing.Size(744, 506)
        Me.pnl_Bottom.TabIndex = 113
        '
        'gvProvider
        '
        Me.gvProvider.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.gvProvider.AllowEditing = False
        Me.gvProvider.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gvProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.gvProvider.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.gvProvider.ColumnInfo = "1,0,0,0,0,95,Columns:"
        Me.gvProvider.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gvProvider.ExtendLastCol = True
        Me.gvProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gvProvider.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.gvProvider.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.gvProvider.Location = New System.Drawing.Point(4, 38)
        Me.gvProvider.Name = "gvProvider"
        Me.gvProvider.Rows.Count = 1
        Me.gvProvider.Rows.DefaultSize = 19
        Me.gvProvider.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.gvProvider.Size = New System.Drawing.Size(736, 464)
        Me.gvProvider.StyleInfo = resources.GetString("gvProvider.StyleInfo")
        Me.gvProvider.TabIndex = 96
        '
        'panel5
        '
        Me.panel5.Controls.Add(Me.label13)
        Me.panel5.Controls.Add(Me.label14)
        Me.panel5.Controls.Add(Me.btnDeactivateProvider)
        Me.panel5.Controls.Add(Me.btnAddProvider)
        Me.panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel5.Location = New System.Drawing.Point(4, 1)
        Me.panel5.Name = "panel5"
        Me.panel5.Size = New System.Drawing.Size(736, 37)
        Me.panel5.TabIndex = 98
        '
        'label13
        '
        Me.label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label13.Location = New System.Drawing.Point(0, 36)
        Me.label13.Name = "label13"
        Me.label13.Size = New System.Drawing.Size(736, 1)
        Me.label13.TabIndex = 96
        Me.label13.Text = "label2"
        '
        'label14
        '
        Me.label14.AutoSize = True
        Me.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label14.Location = New System.Drawing.Point(12, 12)
        Me.label14.Name = "label14"
        Me.label14.Size = New System.Drawing.Size(134, 14)
        Me.label14.TabIndex = 0
        Me.label14.Text = "Associated Providers"
        Me.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnDeactivateProvider
        '
        Me.btnDeactivateProvider.AutoEllipsis = True
        Me.btnDeactivateProvider.BackColor = System.Drawing.Color.Transparent
        Me.btnDeactivateProvider.BackgroundImage = CType(resources.GetObject("btnDeactivateProvider.BackgroundImage"), System.Drawing.Image)
        Me.btnDeactivateProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDeactivateProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDeactivateProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeactivateProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeactivateProvider.Location = New System.Drawing.Point(587, 4)
        Me.btnDeactivateProvider.Name = "btnDeactivateProvider"
        Me.btnDeactivateProvider.Size = New System.Drawing.Size(141, 28)
        Me.btnDeactivateProvider.TabIndex = 95
        Me.btnDeactivateProvider.Text = "Deactivate Provider"
        Me.btnDeactivateProvider.UseVisualStyleBackColor = False
        '
        'btnAddProvider
        '
        Me.btnAddProvider.AutoEllipsis = True
        Me.btnAddProvider.BackColor = System.Drawing.Color.Transparent
        Me.btnAddProvider.BackgroundImage = CType(resources.GetObject("btnAddProvider.BackgroundImage"), System.Drawing.Image)
        Me.btnAddProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnAddProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddProvider.Location = New System.Drawing.Point(482, 4)
        Me.btnAddProvider.Name = "btnAddProvider"
        Me.btnAddProvider.Size = New System.Drawing.Size(100, 28)
        Me.btnAddProvider.TabIndex = 75
        Me.btnAddProvider.Text = "Add Provider"
        Me.btnAddProvider.UseVisualStyleBackColor = False
        '
        'label20
        '
        Me.label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label20.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label20.Location = New System.Drawing.Point(4, 502)
        Me.label20.Name = "label20"
        Me.label20.Size = New System.Drawing.Size(736, 1)
        Me.label20.TabIndex = 27
        '
        'label21
        '
        Me.label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.label21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label21.Location = New System.Drawing.Point(4, 0)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(736, 1)
        Me.label21.TabIndex = 28
        '
        'label6
        '
        Me.label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label6.Location = New System.Drawing.Point(740, 0)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(1, 503)
        Me.label6.TabIndex = 26
        '
        'label5
        '
        Me.label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label5.Location = New System.Drawing.Point(3, 0)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(1, 503)
        Me.label5.TabIndex = 25
        '
        'pnlGIContactDetails
        '
        Me.pnlGIContactDetails.Controls.Add(Me.panel2)
        Me.pnlGIContactDetails.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlGIContactDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlGIContactDetails.Location = New System.Drawing.Point(0, 34)
        Me.pnlGIContactDetails.Name = "pnlGIContactDetails"
        Me.pnlGIContactDetails.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlGIContactDetails.Size = New System.Drawing.Size(744, 61)
        Me.pnlGIContactDetails.TabIndex = 111
        '
        'panel2
        '
        Me.panel2.Controls.Add(Me.label7)
        Me.panel2.Controls.Add(Me.label4)
        Me.panel2.Controls.Add(Me.label3)
        Me.panel2.Controls.Add(Me.label2)
        Me.panel2.Controls.Add(Me.label1)
        Me.panel2.Controls.Add(Me.txtTINTitle)
        Me.panel2.Controls.Add(Me.lblAccountDescription)
        Me.panel2.Controls.Add(Me.txtTINNo)
        Me.panel2.Controls.Add(Me.lblTINNo)
        Me.panel2.Controls.Add(Me.label39)
        Me.panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panel2.Location = New System.Drawing.Point(3, 0)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(738, 58)
        Me.panel2.TabIndex = 118
        '
        'label7
        '
        Me.label7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label7.AutoEllipsis = True
        Me.label7.AutoSize = True
        Me.label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label7.ForeColor = System.Drawing.Color.Red
        Me.label7.Location = New System.Drawing.Point(25, 35)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(14, 14)
        Me.label7.TabIndex = 118
        Me.label7.Text = "*"
        '
        'label4
        '
        Me.label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.Location = New System.Drawing.Point(1, 0)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(736, 1)
        Me.label4.TabIndex = 117
        '
        'label3
        '
        Me.label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label3.Location = New System.Drawing.Point(1, 57)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(736, 1)
        Me.label3.TabIndex = 116
        '
        'label2
        '
        Me.label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label2.Location = New System.Drawing.Point(737, 0)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(1, 58)
        Me.label2.TabIndex = 115
        '
        'label1
        '
        Me.label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.Location = New System.Drawing.Point(0, 0)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(1, 58)
        Me.label1.TabIndex = 114
        '
        'txtTINTitle
        '
        Me.txtTINTitle.AcceptsReturn = True
        Me.txtTINTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.txtTINTitle.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTINTitle.Location = New System.Drawing.Point(79, 34)
        Me.txtTINTitle.MaxLength = 100
        Me.txtTINTitle.Name = "txtTINTitle"
        Me.txtTINTitle.Size = New System.Drawing.Size(300, 22)
        Me.txtTINTitle.TabIndex = 113
        '
        'lblAccountDescription
        '
        Me.lblAccountDescription.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAccountDescription.AutoEllipsis = True
        Me.lblAccountDescription.AutoSize = True
        Me.lblAccountDescription.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccountDescription.Location = New System.Drawing.Point(38, 35)
        Me.lblAccountDescription.Name = "lblAccountDescription"
        Me.lblAccountDescription.Size = New System.Drawing.Size(39, 14)
        Me.lblAccountDescription.TabIndex = 112
        Me.lblAccountDescription.Text = "Title :"
        Me.lblAccountDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTINNo
        '
        Me.txtTINNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(251, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.txtTINNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTINNo.Location = New System.Drawing.Point(79, 8)
        Me.txtTINNo.MaxLength = 10
        Me.txtTINNo.Name = "txtTINNo"
        Me.txtTINNo.Size = New System.Drawing.Size(300, 22)
        Me.txtTINNo.TabIndex = 71
        '
        'lblTINNo
        '
        Me.lblTINNo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTINNo.AutoEllipsis = True
        Me.lblTINNo.AutoSize = True
        Me.lblTINNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTINNo.Location = New System.Drawing.Point(42, 9)
        Me.lblTINNo.Name = "lblTINNo"
        Me.lblTINNo.Size = New System.Drawing.Size(35, 14)
        Me.lblTINNo.TabIndex = 70
        Me.lblTINNo.Text = "TIN :"
        Me.lblTINNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label39
        '
        Me.label39.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label39.AutoEllipsis = True
        Me.label39.AutoSize = True
        Me.label39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label39.ForeColor = System.Drawing.Color.Red
        Me.label39.Location = New System.Drawing.Point(29, 9)
        Me.label39.Name = "label39"
        Me.label39.Size = New System.Drawing.Size(14, 14)
        Me.label39.TabIndex = 111
        Me.label39.Text = "*"
        '
        'pnlAccountInfo
        '
        Me.pnlAccountInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlAccountInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlAccountInfo.Controls.Add(Me.pnl_Bottom)
        Me.pnlAccountInfo.Controls.Add(Me.pnlGIContactDetails)
        Me.pnlAccountInfo.Controls.Add(Me.panel1)
        Me.pnlAccountInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAccountInfo.Location = New System.Drawing.Point(0, 0)
        Me.pnlAccountInfo.Name = "pnlAccountInfo"
        Me.pnlAccountInfo.Size = New System.Drawing.Size(744, 601)
        Me.pnlAccountInfo.TabIndex = 111
        '
        'panel1
        '
        Me.panel1.Controls.Add(Me.pnlGIHeader)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel1.Location = New System.Drawing.Point(0, 0)
        Me.panel1.Name = "panel1"
        Me.panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.panel1.Size = New System.Drawing.Size(744, 34)
        Me.panel1.TabIndex = 26
        '
        'pnlGIHeader
        '
        Me.pnlGIHeader.BackColor = System.Drawing.Color.Transparent
        Me.pnlGIHeader.BackgroundImage = CType(resources.GetObject("pnlGIHeader.BackgroundImage"), System.Drawing.Image)
        Me.pnlGIHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlGIHeader.Controls.Add(Me.label9)
        Me.pnlGIHeader.Controls.Add(Me.label10)
        Me.pnlGIHeader.Controls.Add(Me.label11)
        Me.pnlGIHeader.Controls.Add(Me.label12)
        Me.pnlGIHeader.Controls.Add(Me.lblGIHeader)
        Me.pnlGIHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGIHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlGIHeader.Location = New System.Drawing.Point(3, 3)
        Me.pnlGIHeader.Name = "pnlGIHeader"
        Me.pnlGIHeader.Size = New System.Drawing.Size(738, 28)
        Me.pnlGIHeader.TabIndex = 0
        '
        'label9
        '
        Me.label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label9.Location = New System.Drawing.Point(1, 27)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(736, 1)
        Me.label9.TabIndex = 8
        Me.label9.Text = "label2"
        '
        'label10
        '
        Me.label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label10.Location = New System.Drawing.Point(0, 1)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(1, 27)
        Me.label10.TabIndex = 7
        Me.label10.Text = "label4"
        '
        'label11
        '
        Me.label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label11.Location = New System.Drawing.Point(737, 1)
        Me.label11.Name = "label11"
        Me.label11.Size = New System.Drawing.Size(1, 27)
        Me.label11.TabIndex = 6
        Me.label11.Text = "label3"
        '
        'label12
        '
        Me.label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label12.Location = New System.Drawing.Point(0, 0)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(738, 1)
        Me.label12.TabIndex = 5
        Me.label12.Text = "label1"
        '
        'lblGIHeader
        '
        Me.lblGIHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblGIHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGIHeader.ForeColor = System.Drawing.Color.White
        Me.lblGIHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblGIHeader.Name = "lblGIHeader"
        Me.lblGIHeader.Size = New System.Drawing.Size(738, 28)
        Me.lblGIHeader.TabIndex = 0
        Me.lblGIHeader.Text = "   Tax Identification Number Information"
        Me.lblGIHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlContainer
        '
        Me.pnlContainer.Controls.Add(Me.pnlAccountInfo)
        Me.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContainer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlContainer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlContainer.Location = New System.Drawing.Point(0, 53)
        Me.pnlContainer.Name = "pnlContainer"
        Me.pnlContainer.Size = New System.Drawing.Size(744, 601)
        Me.pnlContainer.TabIndex = 29
        '
        'pnlControl
        '
        Me.pnlControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlControl.Location = New System.Drawing.Point(0, 0)
        Me.pnlControl.Name = "pnlControl"
        Me.pnlControl.Size = New System.Drawing.Size(744, 654)
        Me.pnlControl.TabIndex = 30
        '
        'frmTINMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(744, 654)
        Me.Controls.Add(Me.pnlContainer)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlControl)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTINMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tax ID Setup"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.ts_Commands.ResumeLayout(False)
        Me.ts_Commands.PerformLayout()
        Me.pnl_Bottom.ResumeLayout(False)
        CType(Me.gvProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel5.ResumeLayout(False)
        Me.panel5.PerformLayout()
        Me.pnlGIContactDetails.ResumeLayout(False)
        Me.panel2.ResumeLayout(False)
        Me.panel2.PerformLayout()
        Me.pnlAccountInfo.ResumeLayout(False)
        Me.panel1.ResumeLayout(False)
        Me.pnlGIHeader.ResumeLayout(False)
        Me.pnlContainer.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents ts_Commands As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tsb_OK As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsb_Cancel As System.Windows.Forms.ToolStripButton
    Private WithEvents pnl_Bottom As System.Windows.Forms.Panel
    Private WithEvents gvProvider As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents panel5 As System.Windows.Forms.Panel
    Private WithEvents label13 As System.Windows.Forms.Label
    Private WithEvents label14 As System.Windows.Forms.Label
    Private WithEvents btnDeactivateProvider As System.Windows.Forms.Button
    Private WithEvents btnAddProvider As System.Windows.Forms.Button
    Private WithEvents label20 As System.Windows.Forms.Label
    Private WithEvents label21 As System.Windows.Forms.Label
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents pnlGIContactDetails As System.Windows.Forms.Panel
    Private WithEvents panel2 As System.Windows.Forms.Panel
    Private WithEvents label7 As System.Windows.Forms.Label
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents txtTINTitle As System.Windows.Forms.TextBox
    Private WithEvents lblAccountDescription As System.Windows.Forms.Label
    Public WithEvents txtTINNo As System.Windows.Forms.TextBox
    Private WithEvents lblTINNo As System.Windows.Forms.Label
    Private WithEvents label39 As System.Windows.Forms.Label
    Private WithEvents pnlAccountInfo As System.Windows.Forms.Panel
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Private WithEvents pnlGIHeader As System.Windows.Forms.Panel
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents label10 As System.Windows.Forms.Label
    Private WithEvents label11 As System.Windows.Forms.Label
    Private WithEvents label12 As System.Windows.Forms.Label
    Private WithEvents lblGIHeader As System.Windows.Forms.Label
    Private WithEvents pnlContainer As System.Windows.Forms.Panel
    Private WithEvents toolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlControl As System.Windows.Forms.Panel
End Class
