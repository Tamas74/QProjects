Imports gloEMR.gloStream.LabModule.Category
'// Test Type Master
Public Class frm_LM_SpecimenSetup

    '// Test Type Master
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

    'Required by the Windows Form Designer
    'Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents pnlSetup As System.Windows.Forms.Panel
    Friend WithEvents pnlView As System.Windows.Forms.Panel
    Friend WithEvents lstSpecimens As System.Windows.Forms.ListBox
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnModify As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblDividerView As System.Windows.Forms.Label
    Friend WithEvents pnlCommandSetup As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_LM_SpecimenSetup))
        Me.lblDescription = New System.Windows.Forms.Label
        Me.txtDescription = New System.Windows.Forms.TextBox
        Me.pnlSetup = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.lblDividerSetup = New System.Windows.Forms.Label
        Me.pnlCommandSetup = New System.Windows.Forms.Panel
        Me.pnlToolStripView = New System.Windows.Forms.Panel
        Me.ToolStripView = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlsbtnDelete = New System.Windows.Forms.ToolStripButton
        Me.tlsbtnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlView = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lstSpecimens = New System.Windows.Forms.ListBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.pnlCommandView = New System.Windows.Forms.Panel
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlsbtnNew = New System.Windows.Forms.ToolStripButton
        Me.tlsbtnModify = New System.Windows.Forms.ToolStripButton
        Me.tlsbtnRefresh = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnModify = New System.Windows.Forms.Button
        Me.lblDividerView = New System.Windows.Forms.Label
        Me.btnNew = New System.Windows.Forms.Button
        Me.pnlSetup.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlCommandSetup.SuspendLayout()
        Me.pnlToolStripView.SuspendLayout()
        Me.ToolStripView.SuspendLayout()
        Me.pnlView.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlCommandView.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblDescription
        '
        Me.lblDescription.Location = New System.Drawing.Point(73, 73)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(84, 23)
        Me.lblDescription.TabIndex = 3
        Me.lblDescription.Text = "Description :"
        Me.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDescription
        '
        Me.txtDescription.Location = New System.Drawing.Point(160, 73)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(194, 22)
        Me.txtDescription.TabIndex = 1
        '
        'pnlSetup
        '
        Me.pnlSetup.BackColor = System.Drawing.Color.Transparent
        Me.pnlSetup.Controls.Add(Me.Panel1)
        Me.pnlSetup.Controls.Add(Me.btnOK)
        Me.pnlSetup.Controls.Add(Me.btnCancel)
        Me.pnlSetup.Controls.Add(Me.lblDividerSetup)
        Me.pnlSetup.Controls.Add(Me.pnlCommandSetup)
        Me.pnlSetup.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSetup.Location = New System.Drawing.Point(0, 0)
        Me.pnlSetup.Name = "pnlSetup"
        Me.pnlSetup.Size = New System.Drawing.Size(424, 276)
        Me.pnlSetup.TabIndex = 5
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtDescription)
        Me.Panel1.Controls.Add(Me.lblDescription)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 56)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel1.Size = New System.Drawing.Size(424, 219)
        Me.Panel1.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 215)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(416, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 212)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(420, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 212)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(418, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.Transparent
        Me.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Location = New System.Drawing.Point(241, 169)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 28)
        Me.btnOK.TabIndex = 5
        Me.btnOK.Text = "&OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Transparent
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(160, 169)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 28)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'lblDividerSetup
        '
        Me.lblDividerSetup.BackColor = System.Drawing.Color.CornflowerBlue
        Me.lblDividerSetup.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblDividerSetup.Location = New System.Drawing.Point(0, 275)
        Me.lblDividerSetup.Name = "lblDividerSetup"
        Me.lblDividerSetup.Size = New System.Drawing.Size(424, 1)
        Me.lblDividerSetup.TabIndex = 4
        Me.lblDividerSetup.Visible = False
        '
        'pnlCommandSetup
        '
        Me.pnlCommandSetup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlCommandSetup.Controls.Add(Me.pnlToolStripView)
        Me.pnlCommandSetup.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCommandSetup.Location = New System.Drawing.Point(0, 0)
        Me.pnlCommandSetup.Name = "pnlCommandSetup"
        Me.pnlCommandSetup.Size = New System.Drawing.Size(424, 56)
        Me.pnlCommandSetup.TabIndex = 2
        '
        'pnlToolStripView
        '
        Me.pnlToolStripView.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStripView.Controls.Add(Me.ToolStripView)
        Me.pnlToolStripView.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStripView.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStripView.Name = "pnlToolStripView"
        Me.pnlToolStripView.Size = New System.Drawing.Size(424, 54)
        Me.pnlToolStripView.TabIndex = 12
        '
        'ToolStripView
        '
        Me.ToolStripView.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripView.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStripView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStripView.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripView.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStripView.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStripView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtnDelete, Me.tlsbtnClose})
        Me.ToolStripView.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripView.Name = "ToolStripView"
        Me.ToolStripView.Size = New System.Drawing.Size(424, 53)
        Me.ToolStripView.TabIndex = 1
        Me.ToolStripView.Text = "ToolStrip1"
        '
        'tlsbtnDelete
        '
        Me.tlsbtnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnDelete.Image = CType(resources.GetObject("tlsbtnDelete.Image"), System.Drawing.Image)
        Me.tlsbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnDelete.Name = "tlsbtnDelete"
        Me.tlsbtnDelete.Size = New System.Drawing.Size(57, 50)
        Me.tlsbtnDelete.Tag = "OK"
        Me.tlsbtnDelete.Text = "&Save&Cls"
        Me.tlsbtnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnDelete.ToolTipText = "Save and Close"
        '
        'tlsbtnClose
        '
        Me.tlsbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnClose.Image = CType(resources.GetObject("tlsbtnClose.Image"), System.Drawing.Image)
        Me.tlsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnClose.Name = "tlsbtnClose"
        Me.tlsbtnClose.Size = New System.Drawing.Size(43, 50)
        Me.tlsbtnClose.Tag = "Cancel"
        Me.tlsbtnClose.Text = "&Close"
        Me.tlsbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnClose.ToolTipText = "Close"
        '
        'pnlView
        '
        Me.pnlView.BackColor = System.Drawing.Color.Transparent
        Me.pnlView.Controls.Add(Me.Panel2)
        Me.pnlView.Controls.Add(Me.pnlCommandView)
        Me.pnlView.Controls.Add(Me.btnClose)
        Me.pnlView.Controls.Add(Me.btnRefresh)
        Me.pnlView.Controls.Add(Me.btnDelete)
        Me.pnlView.Controls.Add(Me.btnModify)
        Me.pnlView.Controls.Add(Me.lblDividerView)
        Me.pnlView.Controls.Add(Me.btnNew)
        Me.pnlView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlView.Location = New System.Drawing.Point(0, 0)
        Me.pnlView.Name = "pnlView"
        Me.pnlView.Size = New System.Drawing.Size(424, 276)
        Me.pnlView.TabIndex = 6
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lstSpecimens)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 54)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(424, 221)
        Me.Panel2.TabIndex = 13
        '
        'lstSpecimens
        '
        Me.lstSpecimens.BackColor = System.Drawing.Color.White
        Me.lstSpecimens.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstSpecimens.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstSpecimens.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstSpecimens.ForeColor = System.Drawing.Color.Black
        Me.lstSpecimens.ItemHeight = 14
        Me.lstSpecimens.Location = New System.Drawing.Point(6, 6)
        Me.lstSpecimens.Name = "lstSpecimens"
        Me.lstSpecimens.Size = New System.Drawing.Size(414, 210)
        Me.lstSpecimens.TabIndex = 4
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.White
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(6, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(414, 2)
        Me.Label10.TabIndex = 14
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(4, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(2, 213)
        Me.Label9.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(4, 217)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(416, 1)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 214)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(420, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 214)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(418, 1)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "label1"
        '
        'pnlCommandView
        '
        Me.pnlCommandView.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlCommandView.Controls.Add(Me.ToolStrip1)
        Me.pnlCommandView.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCommandView.Location = New System.Drawing.Point(0, 0)
        Me.pnlCommandView.Name = "pnlCommandView"
        Me.pnlCommandView.Size = New System.Drawing.Size(424, 54)
        Me.pnlCommandView.TabIndex = 12
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStrip1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtnNew, Me.tlsbtnModify, Me.tlsbtnRefresh, Me.ToolStripButton1, Me.ToolStripButton2})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(424, 53)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
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
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton1.Image = Global.gloEMR.My.Resources.Resources.Delete1
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(50, 50)
        Me.ToolStripButton1.Tag = "Delete"
        Me.ToolStripButton1.Text = "&Delete"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton2.Image = Global.gloEMR.My.Resources.Resources.Close01
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(43, 50)
        Me.ToolStripButton2.Tag = "Close"
        Me.ToolStripButton2.Text = "&Close"
        Me.ToolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton2.ToolTipText = "Close"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Location = New System.Drawing.Point(344, 197)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 25)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.Transparent
        Me.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Location = New System.Drawing.Point(260, 197)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 25)
        Me.btnRefresh.TabIndex = 7
        Me.btnRefresh.Text = "&Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.Transparent
        Me.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(178, 197)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 25)
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'btnModify
        '
        Me.btnModify.BackColor = System.Drawing.Color.Transparent
        Me.btnModify.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnModify.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnModify.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnModify.Location = New System.Drawing.Point(94, 197)
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(75, 25)
        Me.btnModify.TabIndex = 5
        Me.btnModify.Text = "&Modify"
        Me.btnModify.UseVisualStyleBackColor = False
        '
        'lblDividerView
        '
        Me.lblDividerView.BackColor = System.Drawing.Color.CornflowerBlue
        Me.lblDividerView.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblDividerView.Location = New System.Drawing.Point(0, 275)
        Me.lblDividerView.Name = "lblDividerView"
        Me.lblDividerView.Size = New System.Drawing.Size(424, 1)
        Me.lblDividerView.TabIndex = 3
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.Transparent
        Me.btnNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Location = New System.Drawing.Point(12, 197)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 25)
        Me.btnNew.TabIndex = 4
        Me.btnNew.Text = "&New"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'frm_LM_SpecimenSetup
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(424, 276)
        Me.Controls.Add(Me.pnlView)
        Me.Controls.Add(Me.pnlSetup)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_LM_SpecimenSetup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Specimen"
        Me.pnlSetup.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlCommandSetup.ResumeLayout(False)
        Me.pnlToolStripView.ResumeLayout(False)
        Me.pnlToolStripView.PerformLayout()
        Me.ToolStripView.ResumeLayout(False)
        Me.ToolStripView.PerformLayout()
        Me.pnlView.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlCommandView.ResumeLayout(False)
        Me.pnlCommandView.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private _SaveFlag As Boolean = False

    Private Sub SpecimenDialog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '// Control Status
        pnlView.Visible = True
        pnlSetup.Visible = False

        Me.Height = 260
        btnRefresh_Click(sender, e)
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click, tlsbtnNew.Click
        txtDescription.Text = ""
        _SaveFlag = True
        '// Control Status
        pnlView.Visible = False
        pnlSetup.Visible = True
        txtDescription.Select()
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.Click, tlsbtnModify.Click
        If Not lstSpecimens.Items.Count = 0 Then
            If Not lstSpecimens.SelectedItem Is Nothing Then
                txtDescription.Text = ""
                '//Record

                Dim _Result As Boolean = False

                Dim oSpecimen As gloStream.LabModule.Specimen.Supporting.Specimen
                Dim oMaintainSpecimen As New gloStream.LabModule.Specimen.MaintainSpecimen
                oSpecimen = oMaintainSpecimen.Specimen(lstSpecimens.SelectedItem.Trim.Replace("'", "''"))
                oMaintainSpecimen = Nothing

                If Not oSpecimen Is Nothing Then
                    If oSpecimen.Description <> "" Then
                        txtDescription.Text = oSpecimen.Description
                        _SaveFlag = False
                        _Result = True
                    End If
                End If
                oSpecimen = Nothing

                '// Control Status
                If _Result = True Then
                    pnlView.Visible = False
                    pnlSetup.Visible = True
                    txtDescription.Select()
                Else
                    MessageBox.Show("Specimen not found to update", gstrMessageBoxCaption, MessageBoxButtons.OK)
                End If
            Else
                MessageBox.Show("Please select Specimen to modify")
                Exit Sub
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click, ToolStripButton1.Click
        If Not lstSpecimens.Items.Count = 0 Then
            If Not lstSpecimens.SelectedItem Is Nothing Then
                If MessageBox.Show("Are you sure you want to delete this Specimen?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                    '//Record
                    Dim oMaintainSpecimen As New gloStream.LabModule.Specimen.MaintainSpecimen
                    If oMaintainSpecimen.Delete(lstSpecimens.SelectedItem.Trim.Replace("'", "''")) = False Then
                        MessageBox.Show("Specimen not deleted, please try after some time", gstrMessageBoxCaption, MessageBoxButtons.OK)
                        Exit Sub
                    Else
                        btnRefresh_Click(sender, e)
                    End If
                    oMaintainSpecimen = Nothing
                End If
            Else
                MessageBox.Show("Please select Specimen to modify")
                Exit Sub
            End If
        End If

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click, ToolStripButton2.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click, tlsbtnRefresh.Click
        lstSpecimens.Focus()
        lstSpecimens.Items.Clear()
        Dim oMaintainSpecimen As New gloStream.LabModule.Specimen.MaintainSpecimen
        Dim oSpecimens As gloStream.LabModule.Specimen.Supporting.Specimens

        oSpecimens = oMaintainSpecimen.Specimens()
        If Not oSpecimens Is Nothing Then
            With lstSpecimens
                For i As Int16 = 1 To oSpecimens.Count
                    .Items.Add(oSpecimens.Item(i).Description)
                Next
            End With
        End If
        oSpecimens = Nothing
        oMaintainSpecimen = Nothing

        If lstSpecimens.Items.Count > 0 Then
            lstSpecimens.SelectedIndex = 0
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click, tlsbtnClose.Click
        pnlSetup.Visible = False
        pnlView.Visible = True
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click, tlsbtnDelete.Click
        If txtDescription.Text.Trim = "" Then
            MessageBox.Show("Please enter description to continue", gstrMessageBoxCaption, MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim oMaintainSpecimen As New gloStream.LabModule.Specimen.MaintainSpecimen
        If _SaveFlag = True Then
            If oMaintainSpecimen.IsExists(txtDescription.Text.Trim.Replace("'", "''")) = True Then
                MessageBox.Show("Specimen with same name already exists, please enter another description", gstrMessageBoxCaption, MessageBoxButtons.OK)
                Exit Sub
            End If
        ElseIf _SaveFlag = False Then
            If txtDescription.Text.Trim <> lstSpecimens.SelectedItem Then
                If oMaintainSpecimen.IsExists(txtDescription.Text.Trim.Replace("'", "''")) = True Then
                    MessageBox.Show("Specimen with same name already exists, please enter another description", gstrMessageBoxCaption, MessageBoxButtons.OK)
                    Exit Sub
                End If
            End If
        End If

        If _SaveFlag = True Then
            If oMaintainSpecimen.Add(txtDescription.Text.Trim.Replace("'", "''")) = False Then
                MessageBox.Show("Specimen not added successfully, please try after some time", gstrMessageBoxCaption, MessageBoxButtons.OK)
            End If
        ElseIf _SaveFlag = False Then
            If oMaintainSpecimen.Modify(lstSpecimens.SelectedItem.Trim.Replace("'", "''"), txtDescription.Text.Trim.Replace("'", "''")) = False Then
                MessageBox.Show("Specimen not added successfully, please try after some time", gstrMessageBoxCaption, MessageBoxButtons.OK)
            End If
        End If
        oMaintainSpecimen = Nothing

        btnRefresh_Click(sender, e)

        pnlSetup.Visible = False
        pnlView.Visible = True

    End Sub
    'Private Sub btnNew_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnNew.MouseDown
    '    btnNew.BackgroundImage = Global.gloEMR.my.Resources.resources.yellowbtn1

    'End Sub

    'Private Sub btnNew_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.MouseEnter
    '    btnNew.BackgroundImage = Global.gloEMR.My.Resources.Resources.yellowbtn1
    'End Sub


    'Private Sub btnNew_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.MouseLeave
    '    btnNew.BackgroundImage = Global.gloEMR.My.Resources.Resources.bg_image
    'End Sub

    'Private Sub btnCancel_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnCancel.MouseDown
    '    btnCancel.BackgroundImage = Global.gloEMR.My.Resources.Resources.yellowbtn1
    'End Sub

    'Private Sub btnCancel_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.MouseEnter
    '    btnCancel.BackgroundImage = Global.gloEMR.My.Resources.Resources.yellowbtn1
    'End Sub

    'Private Sub btnCancel_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.MouseLeave
    '    btnCancel.BackgroundImage = Global.gloEMR.My.Resources.Resources.bg_image
    'End Sub

    'Private Sub btnClose_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnClose.MouseDown
    '    btnClose.BackgroundImage = Global.gloEMR.My.Resources.Resources.yellowbtn1
    'End Sub

    'Private Sub btnClose_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.MouseEnter
    '    btnClose.BackgroundImage = Global.gloEMR.My.Resources.Resources.yellowbtn1
    'End Sub

    'Private Sub btnClose_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.MouseLeave
    '    btnClose.BackgroundImage = Global.gloEMR.My.Resources.Resources.bg_image
    'End Sub


    'Private Sub btnDelete_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnDelete.MouseDown
    '    btnDelete.BackgroundImage = Global.gloEMR.My.Resources.Resources.yellowbtn1
    'End Sub

    'Private Sub btnDelete_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.MouseEnter
    '    btnDelete.BackgroundImage = Global.gloEMR.My.Resources.Resources.yellowbtn1
    'End Sub

    'Private Sub btnDelete_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.MouseLeave
    '    btnDelete.BackgroundImage = Global.gloEMR.My.Resources.Resources.bg_image
    'End Sub

    'Private Sub btnModify_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnModify.MouseDown
    '    btnModify.BackgroundImage = Global.gloEMR.My.Resources.Resources.yellowbtn1
    'End Sub

    'Private Sub btnModify_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModify.MouseEnter
    '    btnModify.BackgroundImage = Global.gloEMR.My.Resources.Resources.yellowbtn1
    'End Sub

    'Private Sub btnModify_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModify.MouseLeave
    '    btnModify.BackgroundImage = Global.gloEMR.My.Resources.Resources.bg_image
    'End Sub

    'Private Sub btnOK_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnOK.MouseDown
    '    btnOK.BackgroundImage = Global.gloEMR.My.Resources.Resources.yellowbtn1
    'End Sub

    'Private Sub btnOK_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.MouseEnter
    '    btnOK.BackgroundImage = Global.gloEMR.My.Resources.Resources.yellowbtn1
    'End Sub

    'Private Sub btnOK_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.MouseLeave
    '    btnOK.BackgroundImage = Global.gloEMR.My.Resources.Resources.bg_image
    'End Sub

    'Private Sub btnRefresh_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnRefresh.MouseDown
    '    btnRefresh.BackgroundImage = Global.gloEMR.My.Resources.Resources.yellowbtn1
    'End Sub

    'Private Sub btnRefresh_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.MouseEnter
    '    btnRefresh.BackgroundImage = Global.gloEMR.My.Resources.Resources.yellowbtn1
    'End Sub

    'Private Sub btnRefresh_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.MouseLeave
    '    btnRefresh.BackgroundImage = Global.gloEMR.My.Resources.Resources.bg_image
    'End Sub

End Class
