<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBusinessCenterRulesSetup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBusinessCenterRulesSetup))
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.menuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.mnuBilling = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBilling_AddLine = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBilling_RemoveLine = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBilling_Save = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBilling_Close = New System.Windows.Forms.ToolStripMenuItem()
        Me.TopToolStrip = New System.Windows.Forms.ToolStrip()
        Me.ts_btnAddLine = New System.Windows.Forms.ToolStripButton()
        Me.tsb_btnRemoveLine = New System.Windows.Forms.ToolStripButton()
        Me.tsb_Saveclose = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlFooter = New System.Windows.Forms.Panel()
        Me.pnl_Shortkeys = New System.Windows.Forms.Panel()
        Me.label4 = New System.Windows.Forms.Label()
        Me.lbl_KeyClose = New System.Windows.Forms.Label()
        Me.lbl_shrtctKeyClose = New System.Windows.Forms.Label()
        Me.lbl_KeySave = New System.Windows.Forms.Label()
        Me.lbl_shrtctKeySave = New System.Windows.Forms.Label()
        Me.lbl_Keyremoveline = New System.Windows.Forms.Label()
        Me.lbl_lshrtctKeyremoveline = New System.Windows.Forms.Label()
        Me.lbl_KeyAddline = New System.Windows.Forms.Label()
        Me.lbl_shrtctKeyAddline = New System.Windows.Forms.Label()
        Me.label6 = New System.Windows.Forms.Label()
        Me.label7 = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.pnlDetails = New System.Windows.Forms.Panel()
        Me.c1BusinessCenter = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.label102 = New System.Windows.Forms.Label()
        Me.label103 = New System.Windows.Forms.Label()
        Me.label104 = New System.Windows.Forms.Label()
        Me.label105 = New System.Windows.Forms.Label()
        Me.cmnu_Row = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuItem_Add_Above = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuItem_Add_Below = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuItem_Delete = New System.Windows.Forms.ToolStripMenuItem()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.panel1.SuspendLayout()
        Me.menuStrip1.SuspendLayout()
        Me.TopToolStrip.SuspendLayout()
        Me.pnlFooter.SuspendLayout()
        Me.pnl_Shortkeys.SuspendLayout()
        Me.pnlDetails.SuspendLayout()
        CType(Me.c1BusinessCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmnu_Row.SuspendLayout()
        Me.SuspendLayout()
        '
        'panel1
        '
        Me.panel1.Controls.Add(Me.menuStrip1)
        Me.panel1.Controls.Add(Me.TopToolStrip)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel1.Location = New System.Drawing.Point(0, 0)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(659, 55)
        Me.panel1.TabIndex = 2
        '
        'menuStrip1
        '
        Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuBilling})
        Me.menuStrip1.Location = New System.Drawing.Point(0, 53)
        Me.menuStrip1.Name = "menuStrip1"
        Me.menuStrip1.Size = New System.Drawing.Size(659, 24)
        Me.menuStrip1.TabIndex = 9
        Me.menuStrip1.Tag = "menuStrip1"
        Me.menuStrip1.Text = "menuStrip1"
        Me.menuStrip1.Visible = False
        '
        'mnuBilling
        '
        Me.mnuBilling.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuBilling_AddLine, Me.mnuBilling_RemoveLine, Me.mnuBilling_Save, Me.mnuBilling_Close})
        Me.mnuBilling.Name = "mnuBilling"
        Me.mnuBilling.Size = New System.Drawing.Size(22, 20)
        Me.mnuBilling.Text = " "
        Me.mnuBilling.Visible = False
        '
        'mnuBilling_AddLine
        '
        Me.mnuBilling_AddLine.Name = "mnuBilling_AddLine"
        Me.mnuBilling_AddLine.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.mnuBilling_AddLine.Size = New System.Drawing.Size(161, 22)
        Me.mnuBilling_AddLine.Text = "Add Line"
        '
        'mnuBilling_RemoveLine
        '
        Me.mnuBilling_RemoveLine.Name = "mnuBilling_RemoveLine"
        Me.mnuBilling_RemoveLine.ShortcutKeys = System.Windows.Forms.Keys.F3
        Me.mnuBilling_RemoveLine.Size = New System.Drawing.Size(161, 22)
        Me.mnuBilling_RemoveLine.Text = "Remove Line"
        '
        'mnuBilling_Save
        '
        Me.mnuBilling_Save.Name = "mnuBilling_Save"
        Me.mnuBilling_Save.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mnuBilling_Save.Size = New System.Drawing.Size(161, 22)
        Me.mnuBilling_Save.Text = "Save"
        '
        'mnuBilling_Close
        '
        Me.mnuBilling_Close.Name = "mnuBilling_Close"
        Me.mnuBilling_Close.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.mnuBilling_Close.Size = New System.Drawing.Size(161, 22)
        Me.mnuBilling_Close.Text = "Close"
        '
        'TopToolStrip
        '
        Me.TopToolStrip.BackColor = System.Drawing.Color.Transparent
        Me.TopToolStrip.BackgroundImage = CType(resources.GetObject("TopToolStrip.BackgroundImage"), System.Drawing.Image)
        Me.TopToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TopToolStrip.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.TopToolStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.TopToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAddLine, Me.tsb_btnRemoveLine, Me.tsb_Saveclose, Me.ts_btnClose})
        Me.TopToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.TopToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.TopToolStrip.Name = "TopToolStrip"
        Me.TopToolStrip.Size = New System.Drawing.Size(659, 53)
        Me.TopToolStrip.TabIndex = 8
        Me.TopToolStrip.Text = "toolStrip1"
        '
        'ts_btnAddLine
        '
        Me.ts_btnAddLine.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnAddLine.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnAddLine.Image = CType(resources.GetObject("ts_btnAddLine.Image"), System.Drawing.Image)
        Me.ts_btnAddLine.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAddLine.Name = "ts_btnAddLine"
        Me.ts_btnAddLine.Size = New System.Drawing.Size(65, 50)
        Me.ts_btnAddLine.Tag = "AddLine"
        Me.ts_btnAddLine.Text = "&Add Line"
        Me.ts_btnAddLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsb_btnRemoveLine
        '
        Me.tsb_btnRemoveLine.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_btnRemoveLine.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsb_btnRemoveLine.Image = CType(resources.GetObject("tsb_btnRemoveLine.Image"), System.Drawing.Image)
        Me.tsb_btnRemoveLine.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_btnRemoveLine.Name = "tsb_btnRemoveLine"
        Me.tsb_btnRemoveLine.Size = New System.Drawing.Size(89, 50)
        Me.tsb_btnRemoveLine.Tag = "RemoveLine"
        Me.tsb_btnRemoveLine.Text = "Re&move Line"
        Me.tsb_btnRemoveLine.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsb_Saveclose
        '
        Me.tsb_Saveclose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsb_Saveclose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsb_Saveclose.Image = CType(resources.GetObject("tsb_Saveclose.Image"), System.Drawing.Image)
        Me.tsb_Saveclose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsb_Saveclose.Name = "tsb_Saveclose"
        Me.tsb_Saveclose.Size = New System.Drawing.Size(66, 50)
        Me.tsb_Saveclose.Tag = "SaveFeeSchedule"
        Me.tsb_Saveclose.Text = "Sa&ve&&Cls"
        Me.tsb_Saveclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tsb_Saveclose.ToolTipText = "Save and Close"
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
        Me.ts_btnClose.ToolTipText = "Close"
        '
        'pnlFooter
        '
        Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlFooter.Controls.Add(Me.pnl_Shortkeys)
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Location = New System.Drawing.Point(0, 479)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlFooter.Size = New System.Drawing.Size(659, 28)
        Me.pnlFooter.TabIndex = 335
        '
        'pnl_Shortkeys
        '
        Me.pnl_Shortkeys.BackgroundImage = Global.gloPMAdmin.My.Resources.Resources.Img_Button
        Me.pnl_Shortkeys.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_Shortkeys.Controls.Add(Me.label4)
        Me.pnl_Shortkeys.Controls.Add(Me.lbl_KeyClose)
        Me.pnl_Shortkeys.Controls.Add(Me.lbl_shrtctKeyClose)
        Me.pnl_Shortkeys.Controls.Add(Me.lbl_KeySave)
        Me.pnl_Shortkeys.Controls.Add(Me.lbl_shrtctKeySave)
        Me.pnl_Shortkeys.Controls.Add(Me.lbl_Keyremoveline)
        Me.pnl_Shortkeys.Controls.Add(Me.lbl_lshrtctKeyremoveline)
        Me.pnl_Shortkeys.Controls.Add(Me.lbl_KeyAddline)
        Me.pnl_Shortkeys.Controls.Add(Me.lbl_shrtctKeyAddline)
        Me.pnl_Shortkeys.Controls.Add(Me.label6)
        Me.pnl_Shortkeys.Controls.Add(Me.label7)
        Me.pnl_Shortkeys.Controls.Add(Me.label5)
        Me.pnl_Shortkeys.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Shortkeys.Location = New System.Drawing.Point(3, 0)
        Me.pnl_Shortkeys.Name = "pnl_Shortkeys"
        Me.pnl_Shortkeys.Size = New System.Drawing.Size(653, 25)
        Me.pnl_Shortkeys.TabIndex = 0
        '
        'label4
        '
        Me.label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.label4.Location = New System.Drawing.Point(652, 1)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(1, 23)
        Me.label4.TabIndex = 68
        Me.label4.Text = "label4"
        '
        'lbl_KeyClose
        '
        Me.lbl_KeyClose.AutoSize = True
        Me.lbl_KeyClose.BackColor = System.Drawing.Color.Transparent
        Me.lbl_KeyClose.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_KeyClose.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_KeyClose.Location = New System.Drawing.Point(392, 1)
        Me.lbl_KeyClose.Name = "lbl_KeyClose"
        Me.lbl_KeyClose.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.lbl_KeyClose.Size = New System.Drawing.Size(45, 18)
        Me.lbl_KeyClose.TabIndex = 63
        Me.lbl_KeyClose.Text = "- Close"
        '
        'lbl_shrtctKeyClose
        '
        Me.lbl_shrtctKeyClose.AutoSize = True
        Me.lbl_shrtctKeyClose.BackColor = System.Drawing.Color.Transparent
        Me.lbl_shrtctKeyClose.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_shrtctKeyClose.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_shrtctKeyClose.ForeColor = System.Drawing.Color.Maroon
        Me.lbl_shrtctKeyClose.Location = New System.Drawing.Point(338, 1)
        Me.lbl_shrtctKeyClose.Name = "lbl_shrtctKeyClose"
        Me.lbl_shrtctKeyClose.Padding = New System.Windows.Forms.Padding(3, 5, 0, 0)
        Me.lbl_shrtctKeyClose.Size = New System.Drawing.Size(54, 18)
        Me.lbl_shrtctKeyClose.TabIndex = 64
        Me.lbl_shrtctKeyClose.Text = "Alt + F4"
        '
        'lbl_KeySave
        '
        Me.lbl_KeySave.AutoSize = True
        Me.lbl_KeySave.BackColor = System.Drawing.Color.Transparent
        Me.lbl_KeySave.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_KeySave.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_KeySave.Location = New System.Drawing.Point(250, 1)
        Me.lbl_KeySave.Name = "lbl_KeySave"
        Me.lbl_KeySave.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.lbl_KeySave.Size = New System.Drawing.Size(88, 18)
        Me.lbl_KeySave.TabIndex = 56
        Me.lbl_KeySave.Text = "- Save && Close"
        '
        'lbl_shrtctKeySave
        '
        Me.lbl_shrtctKeySave.AutoSize = True
        Me.lbl_shrtctKeySave.BackColor = System.Drawing.Color.Transparent
        Me.lbl_shrtctKeySave.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_shrtctKeySave.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_shrtctKeySave.ForeColor = System.Drawing.Color.Maroon
        Me.lbl_shrtctKeySave.Location = New System.Drawing.Point(198, 1)
        Me.lbl_shrtctKeySave.Name = "lbl_shrtctKeySave"
        Me.lbl_shrtctKeySave.Padding = New System.Windows.Forms.Padding(3, 5, 0, 0)
        Me.lbl_shrtctKeySave.Size = New System.Drawing.Size(52, 18)
        Me.lbl_shrtctKeySave.TabIndex = 55
        Me.lbl_shrtctKeySave.Text = "Ctrl + S"
        '
        'lbl_Keyremoveline
        '
        Me.lbl_Keyremoveline.AutoSize = True
        Me.lbl_Keyremoveline.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Keyremoveline.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_Keyremoveline.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Keyremoveline.Location = New System.Drawing.Point(110, 1)
        Me.lbl_Keyremoveline.Name = "lbl_Keyremoveline"
        Me.lbl_Keyremoveline.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.lbl_Keyremoveline.Size = New System.Drawing.Size(88, 18)
        Me.lbl_Keyremoveline.TabIndex = 46
        Me.lbl_Keyremoveline.Text = "- Remove Line"
        '
        'lbl_lshrtctKeyremoveline
        '
        Me.lbl_lshrtctKeyremoveline.AutoSize = True
        Me.lbl_lshrtctKeyremoveline.BackColor = System.Drawing.Color.Transparent
        Me.lbl_lshrtctKeyremoveline.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_lshrtctKeyremoveline.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_lshrtctKeyremoveline.ForeColor = System.Drawing.Color.Maroon
        Me.lbl_lshrtctKeyremoveline.Location = New System.Drawing.Point(87, 1)
        Me.lbl_lshrtctKeyremoveline.Name = "lbl_lshrtctKeyremoveline"
        Me.lbl_lshrtctKeyremoveline.Padding = New System.Windows.Forms.Padding(3, 5, 0, 0)
        Me.lbl_lshrtctKeyremoveline.Size = New System.Drawing.Size(23, 18)
        Me.lbl_lshrtctKeyremoveline.TabIndex = 45
        Me.lbl_lshrtctKeyremoveline.Text = "F3"
        '
        'lbl_KeyAddline
        '
        Me.lbl_KeyAddline.AutoSize = True
        Me.lbl_KeyAddline.BackColor = System.Drawing.Color.Transparent
        Me.lbl_KeyAddline.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_KeyAddline.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_KeyAddline.Location = New System.Drawing.Point(24, 1)
        Me.lbl_KeyAddline.Name = "lbl_KeyAddline"
        Me.lbl_KeyAddline.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.lbl_KeyAddline.Size = New System.Drawing.Size(63, 18)
        Me.lbl_KeyAddline.TabIndex = 44
        Me.lbl_KeyAddline.Text = "- Add Line"
        '
        'lbl_shrtctKeyAddline
        '
        Me.lbl_shrtctKeyAddline.AutoSize = True
        Me.lbl_shrtctKeyAddline.BackColor = System.Drawing.Color.Transparent
        Me.lbl_shrtctKeyAddline.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_shrtctKeyAddline.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_shrtctKeyAddline.ForeColor = System.Drawing.Color.Maroon
        Me.lbl_shrtctKeyAddline.Location = New System.Drawing.Point(1, 1)
        Me.lbl_shrtctKeyAddline.Name = "lbl_shrtctKeyAddline"
        Me.lbl_shrtctKeyAddline.Padding = New System.Windows.Forms.Padding(3, 5, 0, 0)
        Me.lbl_shrtctKeyAddline.Size = New System.Drawing.Size(23, 18)
        Me.lbl_shrtctKeyAddline.TabIndex = 44
        Me.lbl_shrtctKeyAddline.Text = "F2"
        '
        'label6
        '
        Me.label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.label6.Location = New System.Drawing.Point(0, 1)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(1, 23)
        Me.label6.TabIndex = 67
        Me.label6.Text = "label6"
        '
        'label7
        '
        Me.label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.label7.Location = New System.Drawing.Point(0, 0)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(653, 1)
        Me.label7.TabIndex = 69
        Me.label7.Text = "label7"
        '
        'label5
        '
        Me.label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label5.Location = New System.Drawing.Point(0, 24)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(653, 1)
        Me.label5.TabIndex = 65
        Me.label5.Text = "label5"
        '
        'pnlDetails
        '
        Me.pnlDetails.Controls.Add(Me.c1BusinessCenter)
        Me.pnlDetails.Controls.Add(Me.label102)
        Me.pnlDetails.Controls.Add(Me.label103)
        Me.pnlDetails.Controls.Add(Me.label104)
        Me.pnlDetails.Controls.Add(Me.label105)
        Me.pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDetails.Location = New System.Drawing.Point(0, 55)
        Me.pnlDetails.Name = "pnlDetails"
        Me.pnlDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlDetails.Size = New System.Drawing.Size(659, 424)
        Me.pnlDetails.TabIndex = 336
        Me.pnlDetails.TabStop = True
        '
        'c1BusinessCenter
        '
        Me.c1BusinessCenter.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1BusinessCenter.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.c1BusinessCenter.AutoGenerateColumns = False
        Me.c1BusinessCenter.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1BusinessCenter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.c1BusinessCenter.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1BusinessCenter.ColumnInfo = resources.GetString("c1BusinessCenter.ColumnInfo")
        Me.c1BusinessCenter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1BusinessCenter.ExtendLastCol = True
        Me.c1BusinessCenter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1BusinessCenter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1BusinessCenter.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.c1BusinessCenter.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.c1BusinessCenter.Location = New System.Drawing.Point(4, 4)
        Me.c1BusinessCenter.Name = "c1BusinessCenter"
        Me.c1BusinessCenter.Rows.Count = 1
        Me.c1BusinessCenter.Rows.DefaultSize = 21
        Me.c1BusinessCenter.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1BusinessCenter.Size = New System.Drawing.Size(651, 416)
        Me.c1BusinessCenter.StyleInfo = resources.GetString("c1BusinessCenter.StyleInfo")
        Me.c1BusinessCenter.TabIndex = 8
        '
        'label102
        '
        Me.label102.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label102.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label102.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label102.Location = New System.Drawing.Point(4, 420)
        Me.label102.Name = "label102"
        Me.label102.Size = New System.Drawing.Size(651, 1)
        Me.label102.TabIndex = 7
        Me.label102.Text = "label1"
        '
        'label103
        '
        Me.label103.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label103.Dock = System.Windows.Forms.DockStyle.Top
        Me.label103.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label103.Location = New System.Drawing.Point(4, 3)
        Me.label103.Name = "label103"
        Me.label103.Size = New System.Drawing.Size(651, 1)
        Me.label103.TabIndex = 6
        Me.label103.Text = "label1"
        '
        'label104
        '
        Me.label104.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label104.Dock = System.Windows.Forms.DockStyle.Right
        Me.label104.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label104.Location = New System.Drawing.Point(655, 3)
        Me.label104.Name = "label104"
        Me.label104.Size = New System.Drawing.Size(1, 418)
        Me.label104.TabIndex = 5
        Me.label104.Text = "label4"
        '
        'label105
        '
        Me.label105.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label105.Dock = System.Windows.Forms.DockStyle.Left
        Me.label105.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label105.Location = New System.Drawing.Point(3, 3)
        Me.label105.Name = "label105"
        Me.label105.Size = New System.Drawing.Size(1, 418)
        Me.label105.TabIndex = 4
        Me.label105.Text = "label4"
        '
        'cmnu_Row
        '
        Me.cmnu_Row.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmnu_Row.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuItem_Add_Above, Me.mnuItem_Add_Below, Me.mnuItem_Delete})
        Me.cmnu_Row.Name = "cmnu_Appointment"
        Me.cmnu_Row.Size = New System.Drawing.Size(152, 70)
        '
        'mnuItem_Add_Above
        '
        Me.mnuItem_Add_Above.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.mnuItem_Add_Above.Image = CType(resources.GetObject("mnuItem_Add_Above.Image"), System.Drawing.Image)
        Me.mnuItem_Add_Above.Name = "mnuItem_Add_Above"
        Me.mnuItem_Add_Above.Size = New System.Drawing.Size(151, 22)
        Me.mnuItem_Add_Above.Text = "Add Row Above"
        '
        'mnuItem_Add_Below
        '
        Me.mnuItem_Add_Below.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.mnuItem_Add_Below.Image = Global.gloPMAdmin.My.Resources.Resources.Add_Below_row
        Me.mnuItem_Add_Below.Name = "mnuItem_Add_Below"
        Me.mnuItem_Add_Below.Size = New System.Drawing.Size(151, 22)
        Me.mnuItem_Add_Below.Text = "Add Row Below"
        '
        'mnuItem_Delete
        '
        Me.mnuItem_Delete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.mnuItem_Delete.Image = Global.gloPMAdmin.My.Resources.Resources.Delete_Rows
        Me.mnuItem_Delete.Name = "mnuItem_Delete"
        Me.mnuItem_Delete.Size = New System.Drawing.Size(151, 22)
        Me.mnuItem_Delete.Text = "Delete"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmBusinessCenterRulesSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(659, 507)
        Me.Controls.Add(Me.pnlDetails)
        Me.Controls.Add(Me.pnlFooter)
        Me.Controls.Add(Me.panel1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBusinessCenterRulesSetup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Business Center Rules Setup"
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        Me.menuStrip1.ResumeLayout(False)
        Me.menuStrip1.PerformLayout()
        Me.TopToolStrip.ResumeLayout(False)
        Me.TopToolStrip.PerformLayout()
        Me.pnlFooter.ResumeLayout(False)
        Me.pnl_Shortkeys.ResumeLayout(False)
        Me.pnl_Shortkeys.PerformLayout()
        Me.pnlDetails.ResumeLayout(False)
        CType(Me.c1BusinessCenter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmnu_Row.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Private WithEvents TopToolStrip As System.Windows.Forms.ToolStrip
    Private WithEvents ts_btnAddLine As System.Windows.Forms.ToolStripButton
    Private WithEvents tsb_btnRemoveLine As System.Windows.Forms.ToolStripButton
    Private WithEvents tsb_Saveclose As System.Windows.Forms.ToolStripButton
    Private WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents pnlFooter As System.Windows.Forms.Panel
    Private WithEvents pnl_Shortkeys As System.Windows.Forms.Panel
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents lbl_KeyClose As System.Windows.Forms.Label
    Private WithEvents lbl_shrtctKeyClose As System.Windows.Forms.Label
    Private WithEvents lbl_KeySave As System.Windows.Forms.Label
    Private WithEvents lbl_shrtctKeySave As System.Windows.Forms.Label
    Private WithEvents lbl_Keyremoveline As System.Windows.Forms.Label
    Private WithEvents lbl_lshrtctKeyremoveline As System.Windows.Forms.Label
    Private WithEvents lbl_KeyAddline As System.Windows.Forms.Label
    Private WithEvents lbl_shrtctKeyAddline As System.Windows.Forms.Label
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents label7 As System.Windows.Forms.Label
    Friend WithEvents pnlDetails As System.Windows.Forms.Panel
    Private WithEvents c1BusinessCenter As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents label102 As System.Windows.Forms.Label
    Private WithEvents label103 As System.Windows.Forms.Label
    Private WithEvents label104 As System.Windows.Forms.Label
    Private WithEvents label105 As System.Windows.Forms.Label
    Private WithEvents cmnu_Row As System.Windows.Forms.ContextMenuStrip
    Private WithEvents mnuItem_Add_Above As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuItem_Add_Below As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuItem_Delete As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents menuStrip1 As System.Windows.Forms.MenuStrip
    Private WithEvents mnuBilling As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuBilling_AddLine As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuBilling_RemoveLine As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuBilling_Save As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuBilling_Close As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
End Class
