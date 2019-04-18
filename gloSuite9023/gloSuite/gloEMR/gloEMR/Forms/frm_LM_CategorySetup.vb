Imports gloEMR.gloStream.LabModule.Category

Public Class frm_LM_CategorySetup
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    'Public Sub New()
    '    MyBase.New()

    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()

    '    'Add any initialization after the InitializeComponent() call

    'End Sub

    ''Form overrides dispose to clean up the component list.
    'Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
    '    If disposing Then
    '        If Not (components Is Nothing) Then
    '            components.Dispose()
    '        End If
    '    End If
    '    MyBase.Dispose(disposing)
    'End Sub

    ''Required by the Windows Form Designer
    'Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents lblCategory As System.Windows.Forms.Label
    Friend WithEvents pnlSetup As System.Windows.Forms.Panel
    Friend WithEvents pnlView As System.Windows.Forms.Panel
    Friend WithEvents lstCategory As System.Windows.Forms.ListBox
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnModify As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents pnlCommandView As System.Windows.Forms.Panel
    Friend WithEvents lblDividerView As System.Windows.Forms.Label
    Friend WithEvents pnlCommandSetup As System.Windows.Forms.Panel
    Friend WithEvents lblDividerSetup As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    ' Friend WithEvents lstCategory As System.Windows.Forms.ListBox

    '<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    '    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_LM_CategorySetup))
    '    Me.lblDescription = New System.Windows.Forms.Label
    '    Me.lblCategory = New System.Windows.Forms.Label
    '    Me.txtDescription = New System.Windows.Forms.TextBox
    '    Me.cmbCategory = New System.Windows.Forms.ComboBox
    '    Me.pnlSetup = New System.Windows.Forms.Panel
    '    Me.Panel1 = New System.Windows.Forms.Panel
    '    Me.Label2 = New System.Windows.Forms.Label
    '    Me.Label3 = New System.Windows.Forms.Label
    '    Me.Label4 = New System.Windows.Forms.Label
    '    Me.Label9 = New System.Windows.Forms.Label
    '    Me.pnlTooStripSetup = New System.Windows.Forms.Panel
    '    Me.ToolStripSetup = New gloGlobal.gloToolStripIgnoreFocus
    '    Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton
    '    Me.ToolStripButton7 = New System.Windows.Forms.ToolStripButton
    '    Me.lblDividerSetup = New System.Windows.Forms.Label
    '    Me.pnlCommandSetup = New System.Windows.Forms.Panel
    '    Me.btnCancel = New System.Windows.Forms.Button
    '    Me.btnOK = New System.Windows.Forms.Button
    '    Me.pnlView = New System.Windows.Forms.Panel
    '    Me.Panel2 = New System.Windows.Forms.Panel
    '    Me.lstCategory = New System.Windows.Forms.ListBox
    '    Me.Label1 = New System.Windows.Forms.Label
    '    Me.pnlCommandView = New System.Windows.Forms.Panel
    '    Me.btnClose = New System.Windows.Forms.Button
    '    Me.btnRefresh = New System.Windows.Forms.Button
    '    Me.btnDelete = New System.Windows.Forms.Button
    '    Me.btnModify = New System.Windows.Forms.Button
    '    Me.btnNew = New System.Windows.Forms.Button
    '    Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label
    '    Me.Label5 = New System.Windows.Forms.Label
    '    Me.Label6 = New System.Windows.Forms.Label
    '    Me.Label7 = New System.Windows.Forms.Label
    '    Me.Label8 = New System.Windows.Forms.Label
    '    Me.pnlToolStripView = New System.Windows.Forms.Panel
    '    Me.ToolStripView = New gloGlobal.gloToolStripIgnoreFocus
    '    Me.tlsbtnNew = New System.Windows.Forms.ToolStripButton
    '    Me.tlsbtnModify = New System.Windows.Forms.ToolStripButton
    '    Me.tlsbtnRefresh = New System.Windows.Forms.ToolStripButton
    '    Me.tlsbtnDelete = New System.Windows.Forms.ToolStripButton
    '    Me.tlsbtnClose = New System.Windows.Forms.ToolStripButton
    '    Me.lblDividerView = New System.Windows.Forms.Label
    '    Me.pnlSetup.SuspendLayout()
    '    Me.Panel1.SuspendLayout()
    '    Me.pnlTooStripSetup.SuspendLayout()
    '    Me.ToolStripSetup.SuspendLayout()
    '    Me.pnlCommandSetup.SuspendLayout()
    '    Me.pnlView.SuspendLayout()
    '    Me.Panel2.SuspendLayout()
    '    Me.pnlCommandView.SuspendLayout()
    '    Me.pnlToolStripView.SuspendLayout()
    '    Me.ToolStripView.SuspendLayout()
    '    Me.SuspendLayout()
    '    '
    '    'lblDescription
    '    '
    '    Me.lblDescription.AutoSize = True
    '    Me.lblDescription.Location = New System.Drawing.Point(142, 115)
    '    Me.lblDescription.Name = "lblDescription"
    '    Me.lblDescription.Size = New System.Drawing.Size(75, 14)
    '    Me.lblDescription.TabIndex = 3
    '    Me.lblDescription.Text = "Description :"
    '    Me.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '    '
    '    'lblCategory
    '    '
    '    Me.lblCategory.AutoSize = True
    '    Me.lblCategory.Location = New System.Drawing.Point(174, 159)
    '    Me.lblCategory.Name = "lblCategory"
    '    Me.lblCategory.Size = New System.Drawing.Size(43, 14)
    '    Me.lblCategory.TabIndex = 4
    '    Me.lblCategory.Text = "Type :"
    '    Me.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleRight
    '    '
    '    'txtDescription
    '    '
    '    Me.txtDescription.ForeColor = System.Drawing.Color.Black
    '    Me.txtDescription.Location = New System.Drawing.Point(221, 112)
    '    Me.txtDescription.Name = "txtDescription"
    '    Me.txtDescription.Size = New System.Drawing.Size(194, 22)
    '    Me.txtDescription.TabIndex = 1
    '    '
    '    'cmbCategory
    '    '
    '    Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    '    Me.cmbCategory.ForeColor = System.Drawing.Color.Black
    '    Me.cmbCategory.Location = New System.Drawing.Point(221, 156)
    '    Me.cmbCategory.Name = "cmbCategory"
    '    Me.cmbCategory.Size = New System.Drawing.Size(194, 22)
    '    Me.cmbCategory.TabIndex = 2
    '    '
    '    'pnlSetup
    '    '
    '    Me.pnlSetup.BackColor = System.Drawing.Color.Transparent
    '    Me.pnlSetup.Controls.Add(Me.Panel1)
    '    Me.pnlSetup.Controls.Add(Me.pnlTooStripSetup)
    '    Me.pnlSetup.Controls.Add(Me.lblDividerSetup)
    '    Me.pnlSetup.Controls.Add(Me.pnlCommandSetup)
    '    Me.pnlSetup.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.pnlSetup.Location = New System.Drawing.Point(0, 0)
    '    Me.pnlSetup.Name = "pnlSetup"
    '    Me.pnlSetup.Size = New System.Drawing.Size(608, 404)
    '    Me.pnlSetup.TabIndex = 5
    '    '
    '    'Panel1
    '    '
    '    Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
    '    Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    '    Me.Panel1.Controls.Add(Me.Label2)
    '    Me.Panel1.Controls.Add(Me.Label3)
    '    Me.Panel1.Controls.Add(Me.Label4)
    '    Me.Panel1.Controls.Add(Me.Label9)
    '    Me.Panel1.Controls.Add(Me.lblCategory)
    '    Me.Panel1.Controls.Add(Me.cmbCategory)
    '    Me.Panel1.Controls.Add(Me.lblDescription)
    '    Me.Panel1.Controls.Add(Me.txtDescription)
    '    Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.Panel1.Location = New System.Drawing.Point(0, 54)
    '    Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
    '    Me.Panel1.Name = "Panel1"
    '    Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
    '    Me.Panel1.Size = New System.Drawing.Size(608, 349)
    '    Me.Panel1.TabIndex = 20
    '    '
    '    'Label2
    '    '
    '    Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
    '    Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
    '    Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
    '    Me.Label2.Location = New System.Drawing.Point(4, 345)
    '    Me.Label2.Name = "Label2"
    '    Me.Label2.Size = New System.Drawing.Size(600, 1)
    '    Me.Label2.TabIndex = 8
    '    Me.Label2.Text = "label2"
    '    '
    '    'Label3
    '    '
    '    Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
    '    Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
    '    Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.Label3.Location = New System.Drawing.Point(3, 4)
    '    Me.Label3.Name = "Label3"
    '    Me.Label3.Size = New System.Drawing.Size(1, 342)
    '    Me.Label3.TabIndex = 7
    '    Me.Label3.Text = "label4"
    '    '
    '    'Label4
    '    '
    '    Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
    '    Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
    '    Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
    '    Me.Label4.Location = New System.Drawing.Point(604, 4)
    '    Me.Label4.Name = "Label4"
    '    Me.Label4.Size = New System.Drawing.Size(1, 342)
    '    Me.Label4.TabIndex = 6
    '    Me.Label4.Text = "label3"
    '    '
    '    'Label9
    '    '
    '    Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
    '    Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
    '    Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.Label9.Location = New System.Drawing.Point(3, 3)
    '    Me.Label9.Name = "Label9"
    '    Me.Label9.Size = New System.Drawing.Size(602, 1)
    '    Me.Label9.TabIndex = 5
    '    Me.Label9.Text = "label1"
    '    '
    '    'pnlTooStripSetup
    '    '
    '    Me.pnlTooStripSetup.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
    '    Me.pnlTooStripSetup.Controls.Add(Me.ToolStripSetup)
    '    Me.pnlTooStripSetup.Dock = System.Windows.Forms.DockStyle.Top
    '    Me.pnlTooStripSetup.Location = New System.Drawing.Point(0, 0)
    '    Me.pnlTooStripSetup.Name = "pnlTooStripSetup"
    '    Me.pnlTooStripSetup.Size = New System.Drawing.Size(608, 54)
    '    Me.pnlTooStripSetup.TabIndex = 12
    '    '
    '    'ToolStripSetup
    '    '
    '    Me.ToolStripSetup.BackColor = System.Drawing.Color.Transparent
    '    Me.ToolStripSetup.BackgroundImage = Global.gloEMR.My.Resources.Img_Toolstrip 'CType(resources.GetObject("ToolStripSetup.BackgroundImage"), System.Drawing.Image)
    '    Me.ToolStripSetup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    '    Me.ToolStripSetup.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.ToolStripSetup.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
    '    Me.ToolStripSetup.ImageScalingSize = New System.Drawing.Size(32, 32)
    '    Me.ToolStripSetup.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton6, Me.ToolStripButton7})
    '    Me.ToolStripSetup.Location = New System.Drawing.Point(0, 0)
    '    Me.ToolStripSetup.Name = "ToolStripSetup"
    '    Me.ToolStripSetup.Size = New System.Drawing.Size(608, 53)
    '    Me.ToolStripSetup.TabIndex = 1
    '    Me.ToolStripSetup.Text = "ToolStrip1"
    '    '
    '    'ToolStripButton6
    '    '
    '    Me.ToolStripButton6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.ToolStripButton6.Image = Global.gloEMR.My.Resources.OK 'CType(resources.GetObject("ToolStripButton6.Image"), System.Drawing.Image)
    '    Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
    '    Me.ToolStripButton6.Name = "ToolStripButton6"
    '    Me.ToolStripButton6.Size = New System.Drawing.Size(36, 50)
    '    Me.ToolStripButton6.Tag = "OK"
    '    Me.ToolStripButton6.Text = "&OK"
    '    Me.ToolStripButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    '    '
    '    'ToolStripButton7
    '    '
    '    Me.ToolStripButton7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.ToolStripButton7.Image = Global.gloEMR.My.Resources.Cancel_Orders 'CType(resources.GetObject("ToolStripButton7.Image"), System.Drawing.Image)
    '    Me.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta
    '    Me.ToolStripButton7.Name = "ToolStripButton7"
    '    Me.ToolStripButton7.Size = New System.Drawing.Size(50, 50)
    '    Me.ToolStripButton7.Tag = "Cancel"
    '    Me.ToolStripButton7.Text = "&Cancel"
    '    Me.ToolStripButton7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    '    Me.ToolStripButton7.ToolTipText = "Cancel"
    '    '
    '    'lblDividerSetup
    '    '
    '    Me.lblDividerSetup.BackColor = System.Drawing.Color.CornflowerBlue
    '    Me.lblDividerSetup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    '    Me.lblDividerSetup.Dock = System.Windows.Forms.DockStyle.Bottom
    '    Me.lblDividerSetup.Location = New System.Drawing.Point(0, 403)
    '    Me.lblDividerSetup.Name = "lblDividerSetup"
    '    Me.lblDividerSetup.Size = New System.Drawing.Size(608, 1)
    '    Me.lblDividerSetup.TabIndex = 4
    '    '
    '    'pnlCommandSetup
    '    '
    '    Me.pnlCommandSetup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    '    Me.pnlCommandSetup.Controls.Add(Me.btnCancel)
    '    Me.pnlCommandSetup.Controls.Add(Me.btnOK)
    '    Me.pnlCommandSetup.Location = New System.Drawing.Point(368, 219)
    '    Me.pnlCommandSetup.Name = "pnlCommandSetup"
    '    Me.pnlCommandSetup.Size = New System.Drawing.Size(122, 30)
    '    Me.pnlCommandSetup.TabIndex = 2
    '    Me.pnlCommandSetup.Visible = False
    '    '
    '    'btnCancel
    '    '
    '    Me.btnCancel.BackColor = System.Drawing.Color.Transparent
    '    Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
    '    Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
    '    Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
    '    Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    '    Me.btnCancel.Location = New System.Drawing.Point(-28, 0)
    '    Me.btnCancel.Name = "btnCancel"
    '    Me.btnCancel.Size = New System.Drawing.Size(75, 30)
    '    Me.btnCancel.TabIndex = 6
    '    Me.btnCancel.Text = "Cancel"
    '    Me.btnCancel.UseVisualStyleBackColor = False
    '    '
    '    'btnOK
    '    '
    '    Me.btnOK.BackColor = System.Drawing.Color.Transparent
    '    Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
    '    Me.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
    '    Me.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
    '    Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    '    Me.btnOK.Location = New System.Drawing.Point(47, 0)
    '    Me.btnOK.Name = "btnOK"
    '    Me.btnOK.Size = New System.Drawing.Size(75, 30)
    '    Me.btnOK.TabIndex = 5
    '    Me.btnOK.Text = "OK"
    '    Me.btnOK.UseVisualStyleBackColor = False
    '    '
    '    'pnlView
    '    '
    '    Me.pnlView.BackColor = System.Drawing.Color.Transparent
    '    Me.pnlView.Controls.Add(Me.Panel2)
    '    Me.pnlView.Controls.Add(Me.pnlToolStripView)
    '    Me.pnlView.Controls.Add(Me.lblDividerView)
    '    Me.pnlView.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.pnlView.Location = New System.Drawing.Point(0, 0)
    '    Me.pnlView.Name = "pnlView"
    '    Me.pnlView.Size = New System.Drawing.Size(608, 404)
    '    Me.pnlView.TabIndex = 6
    '    '
    '    'Panel2
    '    '
    '    Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
    '    Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    '    Me.Panel2.Controls.Add(Me.lstCategory)
    '    Me.Panel2.Controls.Add(Me.Label1)
    '    Me.Panel2.Controls.Add(Me.pnlCommandView)
    '    Me.Panel2.Controls.Add(Me.lbl_WhiteSpaceTop)
    '    Me.Panel2.Controls.Add(Me.Label5)
    '    Me.Panel2.Controls.Add(Me.Label6)
    '    Me.Panel2.Controls.Add(Me.Label7)
    '    Me.Panel2.Controls.Add(Me.Label8)
    '    Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.Panel2.Location = New System.Drawing.Point(0, 54)
    '    Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
    '    Me.Panel2.Name = "Panel2"
    '    Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
    '    Me.Panel2.Size = New System.Drawing.Size(608, 349)
    '    Me.Panel2.TabIndex = 20
    '    '
    '    'lstCategory
    '    '
    '    Me.lstCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
    '    Me.lstCategory.Dock = System.Windows.Forms.DockStyle.Fill
    '    Me.lstCategory.ForeColor = System.Drawing.Color.Black
    '    Me.lstCategory.ItemHeight = 14
    '    Me.lstCategory.Location = New System.Drawing.Point(7, 7)
    '    Me.lstCategory.Name = "lstCategory"
    '    Me.lstCategory.Size = New System.Drawing.Size(597, 336)
    '    Me.lstCategory.TabIndex = 10
    '    '
    '    'Label1
    '    '
    '    Me.Label1.BackColor = System.Drawing.Color.White
    '    Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
    '    Me.Label1.Location = New System.Drawing.Point(4, 7)
    '    Me.Label1.Name = "Label1"
    '    Me.Label1.Size = New System.Drawing.Size(3, 338)
    '    Me.Label1.TabIndex = 39
    '    '
    '    'pnlCommandView
    '    '
    '    Me.pnlCommandView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    '    Me.pnlCommandView.Controls.Add(Me.btnClose)
    '    Me.pnlCommandView.Controls.Add(Me.btnRefresh)
    '    Me.pnlCommandView.Controls.Add(Me.btnDelete)
    '    Me.pnlCommandView.Controls.Add(Me.btnModify)
    '    Me.pnlCommandView.Controls.Add(Me.btnNew)
    '    Me.pnlCommandView.Location = New System.Drawing.Point(188, 112)
    '    Me.pnlCommandView.Name = "pnlCommandView"
    '    Me.pnlCommandView.Size = New System.Drawing.Size(251, 34)
    '    Me.pnlCommandView.TabIndex = 9
    '    Me.pnlCommandView.Visible = False
    '    '
    '    'btnClose
    '    '
    '    Me.btnClose.BackColor = System.Drawing.Color.Transparent
    '    Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
    '    Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
    '    Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    '    Me.btnClose.Location = New System.Drawing.Point(396, 5)
    '    Me.btnClose.Name = "btnClose"
    '    Me.btnClose.Size = New System.Drawing.Size(88, 25)
    '    Me.btnClose.TabIndex = 13
    '    Me.btnClose.Text = "Close"
    '    Me.btnClose.UseVisualStyleBackColor = False
    '    '
    '    'btnRefresh
    '    '
    '    Me.btnRefresh.BackColor = System.Drawing.Color.Transparent
    '    Me.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
    '    Me.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
    '    Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    '    Me.btnRefresh.Location = New System.Drawing.Point(298, 5)
    '    Me.btnRefresh.Name = "btnRefresh"
    '    Me.btnRefresh.Size = New System.Drawing.Size(88, 25)
    '    Me.btnRefresh.TabIndex = 12
    '    Me.btnRefresh.Text = "Refresh"
    '    Me.btnRefresh.UseVisualStyleBackColor = False
    '    '
    '    'btnDelete
    '    '
    '    Me.btnDelete.BackColor = System.Drawing.Color.Transparent
    '    Me.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
    '    Me.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
    '    Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    '    Me.btnDelete.Location = New System.Drawing.Point(203, 5)
    '    Me.btnDelete.Name = "btnDelete"
    '    Me.btnDelete.Size = New System.Drawing.Size(87, 25)
    '    Me.btnDelete.TabIndex = 11
    '    Me.btnDelete.Text = "Delete"
    '    Me.btnDelete.UseVisualStyleBackColor = False
    '    '
    '    'btnModify
    '    '
    '    Me.btnModify.BackColor = System.Drawing.Color.Transparent
    '    Me.btnModify.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
    '    Me.btnModify.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
    '    Me.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    '    Me.btnModify.Location = New System.Drawing.Point(105, 5)
    '    Me.btnModify.Name = "btnModify"
    '    Me.btnModify.Size = New System.Drawing.Size(87, 25)
    '    Me.btnModify.TabIndex = 10
    '    Me.btnModify.Text = "Modify"
    '    Me.btnModify.UseVisualStyleBackColor = False
    '    '
    '    'btnNew
    '    '
    '    Me.btnNew.BackColor = System.Drawing.Color.Transparent
    '    Me.btnNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
    '    Me.btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
    '    Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
    '    Me.btnNew.Location = New System.Drawing.Point(9, 5)
    '    Me.btnNew.Name = "btnNew"
    '    Me.btnNew.Size = New System.Drawing.Size(87, 25)
    '    Me.btnNew.TabIndex = 9
    '    Me.btnNew.Text = "New"
    '    Me.btnNew.UseVisualStyleBackColor = False
    '    '
    '    'lbl_WhiteSpaceTop
    '    '
    '    Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
    '    Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
    '    Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(4, 4)
    '    Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
    '    Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(600, 3)
    '    Me.lbl_WhiteSpaceTop.TabIndex = 38
    '    '
    '    'Label5
    '    '
    '    Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
    '    Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
    '    Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
    '    Me.Label5.Location = New System.Drawing.Point(4, 345)
    '    Me.Label5.Name = "Label5"
    '    Me.Label5.Size = New System.Drawing.Size(600, 1)
    '    Me.Label5.TabIndex = 8
    '    Me.Label5.Text = "label2"
    '    '
    '    'Label6
    '    '
    '    Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
    '    Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
    '    Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.Label6.Location = New System.Drawing.Point(3, 4)
    '    Me.Label6.Name = "Label6"
    '    Me.Label6.Size = New System.Drawing.Size(1, 342)
    '    Me.Label6.TabIndex = 7
    '    Me.Label6.Text = "label4"
    '    '
    '    'Label7
    '    '
    '    Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
    '    Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
    '    Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
    '    Me.Label7.Location = New System.Drawing.Point(604, 4)
    '    Me.Label7.Name = "Label7"
    '    Me.Label7.Size = New System.Drawing.Size(1, 342)
    '    Me.Label7.TabIndex = 6
    '    Me.Label7.Text = "label3"
    '    '
    '    'Label8
    '    '
    '    Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
    '    Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
    '    Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.Label8.Location = New System.Drawing.Point(3, 3)
    '    Me.Label8.Name = "Label8"
    '    Me.Label8.Size = New System.Drawing.Size(602, 1)
    '    Me.Label8.TabIndex = 5
    '    Me.Label8.Text = "label1"
    '    '
    '    'pnlToolStripView
    '    '
    '    Me.pnlToolStripView.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
    '    Me.pnlToolStripView.Controls.Add(Me.ToolStripView)
    '    Me.pnlToolStripView.Dock = System.Windows.Forms.DockStyle.Top
    '    Me.pnlToolStripView.Location = New System.Drawing.Point(0, 0)
    '    Me.pnlToolStripView.Name = "pnlToolStripView"
    '    Me.pnlToolStripView.Size = New System.Drawing.Size(608, 54)
    '    Me.pnlToolStripView.TabIndex = 11
    '    '
    '    'ToolStripView
    '    '
    '    Me.ToolStripView.BackColor = System.Drawing.Color.Transparent
    '    Me.ToolStripView.BackgroundImage = Global.gloEMR.My.Resources.Img_Toolstrip 'CType(resources.GetObject("ToolStripView.BackgroundImage"), System.Drawing.Image)
    '    Me.ToolStripView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    '    Me.ToolStripView.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.ToolStripView.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
    '    Me.ToolStripView.ImageScalingSize = New System.Drawing.Size(32, 32)
    '    Me.ToolStripView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtnNew, Me.tlsbtnModify, Me.tlsbtnRefresh, Me.tlsbtnDelete, Me.tlsbtnClose})
    '    Me.ToolStripView.Location = New System.Drawing.Point(0, 0)
    '    Me.ToolStripView.Name = "ToolStripView"
    '    Me.ToolStripView.Size = New System.Drawing.Size(608, 53)
    '    Me.ToolStripView.TabIndex = 1
    '    Me.ToolStripView.Text = "ToolStrip1"
    '    '
    '    'tlsbtnNew
    '    '
    '    Me.tlsbtnNew.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.tlsbtnNew.Image = Global.gloEMR.My.Resources.New05 'CType(resources.GetObject("tlsbtnNew.Image"), System.Drawing.Image)
    '    Me.tlsbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
    '    Me.tlsbtnNew.Name = "tlsbtnNew"
    '    Me.tlsbtnNew.Size = New System.Drawing.Size(37, 50)
    '    Me.tlsbtnNew.Tag = "New"
    '    Me.tlsbtnNew.Text = "&New"
    '    Me.tlsbtnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    '    Me.tlsbtnNew.ToolTipText = "New"
    '    '
    '    'tlsbtnModify
    '    '
    '    Me.tlsbtnModify.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.tlsbtnModify.Image = Global.gloEMR.My.Resources.Modify1 'CType(resources.GetObject("tlsbtnModify.Image"), System.Drawing.Image)
    '    Me.tlsbtnModify.ImageTransparentColor = System.Drawing.Color.Magenta
    '    Me.tlsbtnModify.Name = "tlsbtnModify"
    '    Me.tlsbtnModify.Size = New System.Drawing.Size(53, 50)
    '    Me.tlsbtnModify.Tag = "Modify"
    '    Me.tlsbtnModify.Text = "&Modify"
    '    Me.tlsbtnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    '    Me.tlsbtnModify.ToolTipText = "Modify"
    '    '
    '    'tlsbtnRefresh
    '    '
    '    Me.tlsbtnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.tlsbtnRefresh.Image = Global.gloEMR.My.Resources.Refresh_Orders 'CType(resources.GetObject("tlsbtnRefresh.Image"), System.Drawing.Image)
    '    Me.tlsbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
    '    Me.tlsbtnRefresh.Name = "tlsbtnRefresh"
    '    Me.tlsbtnRefresh.Size = New System.Drawing.Size(58, 50)
    '    Me.tlsbtnRefresh.Tag = "Refresh"
    '    Me.tlsbtnRefresh.Text = "&Refresh"
    '    Me.tlsbtnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    '    Me.tlsbtnRefresh.ToolTipText = "Refresh"
    '    '
    '    'tlsbtnDelete
    '    '
    '    Me.tlsbtnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.tlsbtnDelete.Image = Global.gloEMR.My.Resources.Delete1 'CType(resources.GetObject("tlsbtnDelete.Image"), System.Drawing.Image)
    '    Me.tlsbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
    '    Me.tlsbtnDelete.Name = "tlsbtnDelete"
    '    Me.tlsbtnDelete.Size = New System.Drawing.Size(50, 50)
    '    Me.tlsbtnDelete.Tag = "Delete"
    '    Me.tlsbtnDelete.Text = "&Delete"
    '    Me.tlsbtnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    '    '
    '    'tlsbtnClose
    '    '
    '    Me.tlsbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.tlsbtnClose.Image = Global.gloEMR.My.Resources.Close01 'CType(resources.GetObject("tlsbtnClose.Image"), System.Drawing.Image)
    '    Me.tlsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
    '    Me.tlsbtnClose.Name = "tlsbtnClose"
    '    Me.tlsbtnClose.Size = New System.Drawing.Size(43, 50)
    '    Me.tlsbtnClose.Tag = "Close"
    '    Me.tlsbtnClose.Text = "&Close"
    '    Me.tlsbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    '    Me.tlsbtnClose.ToolTipText = "Close"
    '    '
    '    'lblDividerView
    '    '
    '    Me.lblDividerView.BackColor = System.Drawing.Color.CornflowerBlue
    '    Me.lblDividerView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    '    Me.lblDividerView.Dock = System.Windows.Forms.DockStyle.Bottom
    '    Me.lblDividerView.Location = New System.Drawing.Point(0, 403)
    '    Me.lblDividerView.Name = "lblDividerView"
    '    Me.lblDividerView.Size = New System.Drawing.Size(608, 1)
    '    Me.lblDividerView.TabIndex = 3
    '    '
    '    'CategoryDialog
    '    '
    '    Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
    '    Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
    '    Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    '    Me.ClientSize = New System.Drawing.Size(608, 404)
    '    Me.Controls.Add(Me.pnlView)
    '    Me.Controls.Add(Me.pnlSetup)
    '    Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    '    Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
    '    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    '    Me.Icon = Global.gloEMR.My.Resources.Category_New  'CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    '    Me.MaximizeBox = False
    '    Me.MinimizeBox = False
    '    Me.Name = "CategoryDialog"
    '    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    '    Me.Text = "Category"
    '    Me.pnlSetup.ResumeLayout(False)
    '    Me.Panel1.ResumeLayout(False)
    '    Me.Panel1.PerformLayout()
    '    Me.pnlTooStripSetup.ResumeLayout(False)
    '    Me.pnlTooStripSetup.PerformLayout()
    '    Me.ToolStripSetup.ResumeLayout(False)
    '    Me.ToolStripSetup.PerformLayout()
    '    Me.pnlCommandSetup.ResumeLayout(False)
    '    Me.pnlView.ResumeLayout(False)
    '    Me.Panel2.ResumeLayout(False)
    '    Me.pnlCommandView.ResumeLayout(False)
    '    Me.pnlToolStripView.ResumeLayout(False)
    '    Me.pnlToolStripView.PerformLayout()
    '    Me.ToolStripView.ResumeLayout(False)
    '    Me.ToolStripView.PerformLayout()
    '    Me.ResumeLayout(False)

    'End Sub

#End Region

    Private _SaveFlag As Boolean = False

    Private Sub CategoryDialog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '// Load Category Types
        Dim oCategoryTypes As New Supporting.Supporting
        cmbCategory.Items.Clear()
        cmbCategory.Items.Add(oCategoryTypes.CategoryType_enum_AsString(Supporting.Supporting.enumCategoryType.Order))
        oCategoryTypes = Nothing

        '// Control Status
        pnlView.Visible = True
        pnlSetup.Visible = False


        btnRefresh_Click(sender, e)
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click, tlsbtnNew.Click
        txtDescription.Text = ""
        If (cmbCategory.Items.Count > 0) Then
            cmbCategory.SelectedIndex = 0
        End If

        _SaveFlag = True
        '// Control Status
        pnlView.Visible = False
        pnlSetup.Visible = True
        txtDescription.Select()
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.Click, tlsbtnModify.Click
        If Not lstCategory.Items.Count = 0 Then
            If Not lstCategory.SelectedItem Is Nothing Then
                txtDescription.Text = ""
                cmbCategory.SelectedIndex = -1
                '//Record

                Dim _Result As Boolean = False
                Dim _Type As String
                Dim oType As New gloStream.LabModule.Category.Supporting.Supporting
                _Type = oType.CategoryType_enum_AsString(Supporting.Supporting.enumCategoryType.Order)


                Dim oCategory As gloStream.LabModule.Category.Supporting.Category
                Dim oMaintainCategory As New gloStream.LabModule.Category.MaintainCategory
                oCategory = oMaintainCategory.Category(lstCategory.SelectedItem.Trim.Replace("'", "''"), _Type)
                oMaintainCategory = Nothing

                If Not oCategory Is Nothing Then
                    If oCategory.Description <> "" Then
                        txtDescription.Text = oCategory.Description
                        cmbCategory.Text = _Type
                        _SaveFlag = False
                        _Result = True
                    End If
                End If
                oType = Nothing
                oCategory = Nothing

                '// Control Status
                If _Result = True Then
                    pnlView.Visible = False
                    pnlSetup.Visible = True
                    txtDescription.Select()
                Else
                    MessageBox.Show("Category not found to update", gstrMessageBoxCaption, MessageBoxButtons.OK)
                End If
            Else
                MessageBox.Show("Please select category to modify")
                Exit Sub
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click, tlsbtnDelete.Click
        If Not lstCategory.Items.Count = 0 Then
            If Not lstCategory.SelectedItem Is Nothing Then
                If MessageBox.Show("Are you sure you want to delete this category?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    Dim oMaintainTest As New gloStream.LabModule.Test.MaintainTest
                    Dim categoryName = lstCategory.SelectedItem.Trim.Replace("'", "''")

                    If oMaintainTest.IsDeleteCategory(categoryName) = True Then
                        '//Record
                        Dim _Type As String
                        Dim oType As New gloStream.LabModule.Category.Supporting.Supporting
                        _Type = oType.CategoryType_enum_AsString(Supporting.Supporting.enumCategoryType.Order)
                        oType = Nothing

                        Dim oMaintainCategory As New gloStream.LabModule.Category.MaintainCategory
                        If oMaintainCategory.Delete(lstCategory.SelectedItem.Trim.Replace("'", "''"), _Type.Trim.Replace("'", "''")) = False Then
                            MessageBox.Show("Category not deleted, please try after some time", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        Else
                            btnRefresh_Click(sender, e)
                        End If
                        oMaintainCategory = Nothing
                    Else
                        If oMaintainTest.ErrorMessage <> "" Then
                            MessageBox.Show(oMaintainTest.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK)
                        End If
                    End If
                End If
            Else
                MessageBox.Show("Please select category to modify")
                Exit Sub
            End If
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click, tlsbtnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click, tlsbtnRefresh.Click
        lstCategory.Items.Clear()
        Dim oMaintainCategory As New MaintainCategory
        Dim oCategories As Supporting.Categories

        Dim oType As String
        Dim oTypeSupporting As New Supporting.Supporting
        oType = oTypeSupporting.CategoryType_enum_AsString(Supporting.Supporting.enumCategoryType.Order)
        oTypeSupporting = Nothing

        oCategories = oMaintainCategory.Categories(oType)
        If Not oCategories Is Nothing Then
            With lstCategory
                For i As Int16 = 1 To oCategories.Count
                    .Items.Add(oCategories.Item(i).Description)
                Next
            End With
            oCategories.Dispose()
        End If

        oCategories = Nothing
        oMaintainCategory = Nothing

        If lstCategory.Items.Count > 0 Then
            lstCategory.SelectedIndex = 0
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, ToolStripButton7.Click
        pnlSetup.Visible = False
        pnlView.Visible = True
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click, ToolStripButton6.Click
        If txtDescription.Text.Trim = "" Then
            MessageBox.Show("Please enter description to continue", gstrMessageBoxCaption, MessageBoxButtons.OK)
            Exit Sub
        End If

        If cmbCategory.SelectedItem Is Nothing Then
            MessageBox.Show("Please select category type to continue", gstrMessageBoxCaption, MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim oMaintainCategory As New gloStream.LabModule.Category.MaintainCategory
        If _SaveFlag = True Then
            If oMaintainCategory.IsExists(txtDescription.Text.Trim.Replace("'", "''"), cmbCategory.SelectedItem.Trim.Replace("'", "''")) = True Then
                MessageBox.Show("Category with same name already exists, please enter another description", gstrMessageBoxCaption, MessageBoxButtons.OK)
                Exit Sub
            End If
        ElseIf _SaveFlag = False Then
            If txtDescription.Text.Trim <> lstCategory.SelectedItem Then
                If oMaintainCategory.IsExists(txtDescription.Text.Trim.Replace("'", "''"), cmbCategory.SelectedItem.Trim.Replace("'", "''")) = True Then
                    MessageBox.Show("Category with same name already exists, please enter another description", gstrMessageBoxCaption, MessageBoxButtons.OK)
                    Exit Sub
                End If
            End If
        End If

        If _SaveFlag = True Then
            If oMaintainCategory.Add(txtDescription.Text.Trim.Replace("'", "''"), cmbCategory.SelectedItem.Trim.Replace("'", "''")) = False Then
                MessageBox.Show("Category not added successfully, please try after some time", gstrMessageBoxCaption, MessageBoxButtons.OK)
            End If
        ElseIf _SaveFlag = False Then
            If oMaintainCategory.Modify(lstCategory.SelectedItem.Trim.Replace("'", "''"), txtDescription.Text.Trim.Replace("'", "''"), cmbCategory.SelectedItem.Trim.Replace("'", "''")) = False Then
                MessageBox.Show("Category not added successfully, please try after some time", gstrMessageBoxCaption, MessageBoxButtons.OK)
            End If
        End If
        oMaintainCategory = Nothing

        btnRefresh_Click(sender, e)

        pnlSetup.Visible = False
        pnlView.Visible = True

    End Sub

    Friend WithEvents pnlToolStripView As System.Windows.Forms.Panel
    Friend WithEvents ToolStripView As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsbtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlTooStripSetup As System.Windows.Forms.Panel
    Friend WithEvents ToolStripSetup As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ToolStripButton6 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton7 As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label


End Class
