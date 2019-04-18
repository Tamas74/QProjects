<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_LM_CategorySetup
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_LM_CategorySetup))
        Me.lblDescription = New System.Windows.Forms.Label
        Me.lblCategory = New System.Windows.Forms.Label
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.cmbCategory = New System.Windows.Forms.ComboBox
        Me.pnlSetup = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.pnlTooStripSetup = New System.Windows.Forms.Panel
        Me.ToolStripSetup = New gloGlobal.gloToolStripIgnoreFocus
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton7 = New System.Windows.Forms.ToolStripButton
        Me.lblDividerSetup = New System.Windows.Forms.Label
        Me.pnlCommandSetup = New System.Windows.Forms.Panel
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        Me.pnlView = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lstCategory = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlCommandView = New System.Windows.Forms.Panel
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnModify = New System.Windows.Forms.Button
        Me.btnNew = New System.Windows.Forms.Button
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.pnlToolStripView = New System.Windows.Forms.Panel
        Me.ToolStripView = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlsbtnNew = New System.Windows.Forms.ToolStripButton
        Me.tlsbtnModify = New System.Windows.Forms.ToolStripButton
        Me.tlsbtnRefresh = New System.Windows.Forms.ToolStripButton
        Me.tlsbtnDelete = New System.Windows.Forms.ToolStripButton
        Me.tlsbtnClose = New System.Windows.Forms.ToolStripButton
        Me.lblDividerView = New System.Windows.Forms.Label
        Me.pnlSetup.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlTooStripSetup.SuspendLayout()
        Me.ToolStripSetup.SuspendLayout()
        Me.pnlCommandSetup.SuspendLayout()
        Me.pnlView.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlCommandView.SuspendLayout()
        Me.pnlToolStripView.SuspendLayout()
        Me.ToolStripView.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Location = New System.Drawing.Point(142, 115)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(75, 14)
        Me.lblDescription.TabIndex = 3
        Me.lblDescription.Text = "Description :"
        Me.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.Location = New System.Drawing.Point(174, 159)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(43, 14)
        Me.lblCategory.TabIndex = 4
        Me.lblCategory.Text = "Type :"
        Me.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCategory.Visible = False
        '
        'txtDescription
        '
        Me.txtDescription.ForeColor = System.Drawing.Color.Black
        Me.txtDescription.Location = New System.Drawing.Point(221, 112)
        Me.txtDescription.MaxLength = 100
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(194, 22)
        Me.txtDescription.TabIndex = 1
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.ForeColor = System.Drawing.Color.Black
        Me.cmbCategory.Location = New System.Drawing.Point(221, 156)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(194, 22)
        Me.cmbCategory.TabIndex = 2
        Me.cmbCategory.Visible = False
        '
        'pnlSetup
        '
        Me.pnlSetup.BackColor = System.Drawing.Color.Transparent
        Me.pnlSetup.Controls.Add(Me.Panel1)
        Me.pnlSetup.Controls.Add(Me.pnlTooStripSetup)
        Me.pnlSetup.Controls.Add(Me.lblDividerSetup)
        Me.pnlSetup.Controls.Add(Me.pnlCommandSetup)
        Me.pnlSetup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSetup.Location = New System.Drawing.Point(0, 0)
        Me.pnlSetup.Name = "pnlSetup"
        Me.pnlSetup.Size = New System.Drawing.Size(608, 403)
        Me.pnlSetup.TabIndex = 5
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.lblCategory)
        Me.Panel1.Controls.Add(Me.cmbCategory)
        Me.Panel1.Controls.Add(Me.lblDescription)
        Me.Panel1.Controls.Add(Me.txtDescription)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(608, 348)
        Me.Panel1.TabIndex = 20
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(131, 115)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(14, 14)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "*"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(4, 344)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(600, 1)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 341)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(604, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 341)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(602, 1)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "label1"
        '
        'pnlTooStripSetup
        '
        Me.pnlTooStripSetup.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlTooStripSetup.Controls.Add(Me.ToolStripSetup)
        Me.pnlTooStripSetup.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTooStripSetup.Location = New System.Drawing.Point(0, 0)
        Me.pnlTooStripSetup.Name = "pnlTooStripSetup"
        Me.pnlTooStripSetup.Size = New System.Drawing.Size(608, 54)
        Me.pnlTooStripSetup.TabIndex = 12
        '
        'ToolStripSetup
        '
        Me.ToolStripSetup.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripSetup.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStripSetup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStripSetup.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripSetup.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStripSetup.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStripSetup.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton6, Me.ToolStripButton7})
        Me.ToolStripSetup.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripSetup.Name = "ToolStripSetup"
        Me.ToolStripSetup.Size = New System.Drawing.Size(608, 53)
        Me.ToolStripSetup.TabIndex = 1
        Me.ToolStripSetup.Text = "ToolStrip1"
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton6.Image = CType(resources.GetObject("ToolStripButton6.Image"), System.Drawing.Image)
        Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(66, 50)
        Me.ToolStripButton6.Tag = "OK"
        Me.ToolStripButton6.Text = "&Save&&Cls"
        Me.ToolStripButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton6.ToolTipText = "Save and Close"
        '
        'ToolStripButton7
        '
        Me.ToolStripButton7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton7.Image = CType(resources.GetObject("ToolStripButton7.Image"), System.Drawing.Image)
        Me.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton7.Name = "ToolStripButton7"
        Me.ToolStripButton7.Size = New System.Drawing.Size(43, 50)
        Me.ToolStripButton7.Tag = "Cancel"
        Me.ToolStripButton7.Text = "&Close"
        Me.ToolStripButton7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton7.ToolTipText = "Close"
        '
        'lblDividerSetup
        '
        Me.lblDividerSetup.BackColor = System.Drawing.Color.CornflowerBlue
        Me.lblDividerSetup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDividerSetup.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblDividerSetup.Location = New System.Drawing.Point(0, 402)
        Me.lblDividerSetup.Name = "lblDividerSetup"
        Me.lblDividerSetup.Size = New System.Drawing.Size(608, 1)
        Me.lblDividerSetup.TabIndex = 4
        '
        'pnlCommandSetup
        '
        Me.pnlCommandSetup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlCommandSetup.Controls.Add(Me.btnCancel)
        Me.pnlCommandSetup.Controls.Add(Me.btnOK)
        Me.pnlCommandSetup.Location = New System.Drawing.Point(368, 219)
        Me.pnlCommandSetup.Name = "pnlCommandSetup"
        Me.pnlCommandSetup.Size = New System.Drawing.Size(122, 30)
        Me.pnlCommandSetup.TabIndex = 2
        Me.pnlCommandSetup.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Transparent
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(-28, 0)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 30)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.Transparent
        Me.btnOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Location = New System.Drawing.Point(47, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 30)
        Me.btnOK.TabIndex = 5
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'pnlView
        '
        Me.pnlView.BackColor = System.Drawing.Color.Transparent
        Me.pnlView.Controls.Add(Me.Panel2)
        Me.pnlView.Controls.Add(Me.pnlToolStripView)
        Me.pnlView.Controls.Add(Me.lblDividerView)
        Me.pnlView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlView.Location = New System.Drawing.Point(0, 0)
        Me.pnlView.Name = "pnlView"
        Me.pnlView.Size = New System.Drawing.Size(608, 403)
        Me.pnlView.TabIndex = 6
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.lstCategory)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.pnlCommandView)
        Me.Panel2.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 54)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(608, 348)
        Me.Panel2.TabIndex = 20
        '
        'lstCategory
        '
        Me.lstCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstCategory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstCategory.ForeColor = System.Drawing.Color.Black
        Me.lstCategory.ItemHeight = 14
        Me.lstCategory.Location = New System.Drawing.Point(7, 7)
        Me.lstCategory.Name = "lstCategory"
        Me.lstCategory.Size = New System.Drawing.Size(597, 336)
        Me.lstCategory.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Location = New System.Drawing.Point(4, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(3, 337)
        Me.Label1.TabIndex = 39
        '
        'pnlCommandView
        '
        Me.pnlCommandView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlCommandView.Controls.Add(Me.btnClose)
        Me.pnlCommandView.Controls.Add(Me.btnRefresh)
        Me.pnlCommandView.Controls.Add(Me.btnDelete)
        Me.pnlCommandView.Controls.Add(Me.btnModify)
        Me.pnlCommandView.Controls.Add(Me.btnNew)
        Me.pnlCommandView.Location = New System.Drawing.Point(188, 112)
        Me.pnlCommandView.Name = "pnlCommandView"
        Me.pnlCommandView.Size = New System.Drawing.Size(251, 34)
        Me.pnlCommandView.TabIndex = 9
        Me.pnlCommandView.Visible = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(396, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(88, 25)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.Transparent
        Me.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Location = New System.Drawing.Point(298, 5)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(88, 25)
        Me.btnRefresh.TabIndex = 12
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.Transparent
        Me.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.Location = New System.Drawing.Point(203, 5)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(87, 25)
        Me.btnDelete.TabIndex = 11
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnModify
        '
        Me.btnModify.BackColor = System.Drawing.Color.Transparent
        Me.btnModify.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnModify.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnModify.Location = New System.Drawing.Point(105, 5)
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(87, 25)
        Me.btnModify.TabIndex = 10
        Me.btnModify.Text = "Modify"
        Me.btnModify.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.Transparent
        Me.btnNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.Location = New System.Drawing.Point(9, 5)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(87, 25)
        Me.btnNew.TabIndex = 9
        Me.btnNew.Text = "New"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(4, 4)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(600, 3)
        Me.lbl_WhiteSpaceTop.TabIndex = 38
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 344)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(600, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 341)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(604, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 341)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(602, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'pnlToolStripView
        '
        Me.pnlToolStripView.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStripView.Controls.Add(Me.ToolStripView)
        Me.pnlToolStripView.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStripView.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStripView.Name = "pnlToolStripView"
        Me.pnlToolStripView.Size = New System.Drawing.Size(608, 54)
        Me.pnlToolStripView.TabIndex = 11
        '
        'ToolStripView
        '
        Me.ToolStripView.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripView.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStripView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStripView.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripView.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStripView.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStripView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtnNew, Me.tlsbtnModify, Me.tlsbtnRefresh, Me.tlsbtnDelete, Me.tlsbtnClose})
        Me.ToolStripView.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripView.Name = "ToolStripView"
        Me.ToolStripView.Size = New System.Drawing.Size(608, 53)
        Me.ToolStripView.TabIndex = 1
        Me.ToolStripView.Text = "ToolStrip1"
        '
        'tlsbtnNew
        '
        Me.tlsbtnNew.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnNew.Image = Global.gloEMR.My.Resources.Resources.New05
        Me.tlsbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnNew.Name = "tlsbtnNew"
        Me.tlsbtnNew.Size = New System.Drawing.Size(37, 50)
        Me.tlsbtnNew.Tag = "New"
        Me.tlsbtnNew.Text = "&New"
        Me.tlsbtnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnNew.ToolTipText = "New"
        '
        'tlsbtnModify
        '
        Me.tlsbtnModify.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnModify.Image = CType(resources.GetObject("tlsbtnModify.Image"), System.Drawing.Image)
        Me.tlsbtnModify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnModify.Name = "tlsbtnModify"
        Me.tlsbtnModify.Size = New System.Drawing.Size(53, 50)
        Me.tlsbtnModify.Tag = "Modify"
        Me.tlsbtnModify.Text = "&Modify"
        Me.tlsbtnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnModify.ToolTipText = "Modify"
        '
        'tlsbtnRefresh
        '
        Me.tlsbtnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnRefresh.Image = Global.gloEMR.My.Resources.Resources.Refresh_Orders
        Me.tlsbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnRefresh.Name = "tlsbtnRefresh"
        Me.tlsbtnRefresh.Size = New System.Drawing.Size(58, 50)
        Me.tlsbtnRefresh.Tag = "Refresh"
        Me.tlsbtnRefresh.Text = "&Refresh"
        Me.tlsbtnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnRefresh.ToolTipText = "Refresh"
        '
        'tlsbtnDelete
        '
        Me.tlsbtnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnDelete.Image = Global.gloEMR.My.Resources.Resources.Delete1
        Me.tlsbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnDelete.Name = "tlsbtnDelete"
        Me.tlsbtnDelete.Size = New System.Drawing.Size(50, 50)
        Me.tlsbtnDelete.Tag = "Delete"
        Me.tlsbtnDelete.Text = "&Delete"
        Me.tlsbtnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsbtnClose
        '
        Me.tlsbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnClose.Image = Global.gloEMR.My.Resources.Resources.Close01
        Me.tlsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnClose.Name = "tlsbtnClose"
        Me.tlsbtnClose.Size = New System.Drawing.Size(43, 50)
        Me.tlsbtnClose.Tag = "Close"
        Me.tlsbtnClose.Text = "&Close"
        Me.tlsbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnClose.ToolTipText = "Close"
        '
        'lblDividerView
        '
        Me.lblDividerView.BackColor = System.Drawing.Color.CornflowerBlue
        Me.lblDividerView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDividerView.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblDividerView.Location = New System.Drawing.Point(0, 402)
        Me.lblDividerView.Name = "lblDividerView"
        Me.lblDividerView.Size = New System.Drawing.Size(608, 1)
        Me.lblDividerView.TabIndex = 3
        '
        'frm_LM_CategorySetup
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(608, 403)
        Me.Controls.Add(Me.pnlSetup)
        Me.Controls.Add(Me.pnlView)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = Global.gloEMR.My.Resources.Resources.Category_New
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_LM_CategorySetup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Category"
        Me.pnlSetup.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlTooStripSetup.ResumeLayout(False)
        Me.pnlTooStripSetup.PerformLayout()
        Me.ToolStripSetup.ResumeLayout(False)
        Me.ToolStripSetup.PerformLayout()
        Me.pnlCommandSetup.ResumeLayout(False)
        Me.pnlView.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlCommandView.ResumeLayout(False)
        Me.pnlToolStripView.ResumeLayout(False)
        Me.pnlToolStripView.PerformLayout()
        Me.ToolStripView.ResumeLayout(False)
        Me.ToolStripView.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
