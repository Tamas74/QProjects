Imports System.Reflection
Imports System.Data.SqlClient
Imports gloEMR.gloStream.LabModule

Public Class frm_LM_TestSetuptDialog

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
    'Friend WithEvents lblCommandRight3 As System.Windows.Forms.Label
    'Friend WithEvents lblCommandRight2 As System.Windows.Forms.Label
    'Friend WithEvents lblCommandRight1 As System.Windows.Forms.Label
    'Friend WithEvents lblCommandBottom As System.Windows.Forms.Label
    'Friend WithEvents lblCommandTop As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbTestGroup As System.Windows.Forms.ComboBox
    Friend WithEvents cmbGroup As System.Windows.Forms.ComboBox
    Friend WithEvents cmbTemplates As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txtDimension As System.Windows.Forms.TextBox
    Friend WithEvents txtMaleLowerValue As System.Windows.Forms.TextBox
    Friend WithEvents txtMaleHigherValue As System.Windows.Forms.TextBox
    Friend WithEvents txtFemaleHigherValue As System.Windows.Forms.TextBox
    Friend WithEvents txtFemaleLowerValue As System.Windows.Forms.TextBox
    Friend WithEvents chkIsSpecimen As System.Windows.Forms.CheckBox
    Friend WithEvents cmbIsSpecimen As System.Windows.Forms.ComboBox
    Friend WithEvents btnSpeciman As System.Windows.Forms.Button
    Friend WithEvents cmbLabResults As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents CmbAssociatedItems As System.Windows.Forms.ComboBox
    Friend WithEvents lblAssociatedItem As System.Windows.Forms.Label
    Friend WithEvents pnlToolStripSetupDialog As System.Windows.Forms.Panel
    Friend WithEvents ToolStripSetupDialog As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsbtnCancel As System.Windows.Forms.ToolStripButton
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_LM_TestSetuptDialog))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.cmbTestGroup = New System.Windows.Forms.ComboBox
        Me.cmbGroup = New System.Windows.Forms.ComboBox
        Me.cmbTemplates = New System.Windows.Forms.ComboBox
        Me.cmbCategory = New System.Windows.Forms.ComboBox
        Me.txtName = New System.Windows.Forms.TextBox
        Me.txtDimension = New System.Windows.Forms.TextBox
        Me.txtMaleLowerValue = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtMaleHigherValue = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtFemaleHigherValue = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtFemaleLowerValue = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.chkIsSpecimen = New System.Windows.Forms.CheckBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.cmbIsSpecimen = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.btnSpeciman = New System.Windows.Forms.Button
        Me.cmbLabResults = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.CmbAssociatedItems = New System.Windows.Forms.ComboBox
        Me.lblAssociatedItem = New System.Windows.Forms.Label
        Me.pnlToolStripSetupDialog = New System.Windows.Forms.Panel
        Me.ToolStripSetupDialog = New gloGlobal.gloToolStripIgnoreFocus
        Me.tblSave = New System.Windows.Forms.ToolStripButton
        Me.tlsbtnCancel = New System.Windows.Forms.ToolStripButton
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.txtLoincCode = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.btnAssociatedEMItem = New System.Windows.Forms.Button
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.pnlToolStripSetupDialog.SuspendLayout()
        Me.ToolStripSetupDialog.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(72, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 14)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Description :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(69, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 14)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Test\Group :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(99, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 14)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Group :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(83, 152)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 14)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Category :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(80, 186)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 14)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Template :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(86, 270)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 14)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Dimension :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label6.Visible = False
        '
        'cmbTestGroup
        '
        Me.cmbTestGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTestGroup.Enabled = False
        Me.cmbTestGroup.ForeColor = System.Drawing.Color.Black
        Me.cmbTestGroup.Location = New System.Drawing.Point(150, 11)
        Me.cmbTestGroup.Name = "cmbTestGroup"
        Me.cmbTestGroup.Size = New System.Drawing.Size(269, 22)
        Me.cmbTestGroup.TabIndex = 0
        '
        'cmbGroup
        '
        Me.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGroup.ForeColor = System.Drawing.Color.Black
        Me.cmbGroup.Location = New System.Drawing.Point(150, 114)
        Me.cmbGroup.Name = "cmbGroup"
        Me.cmbGroup.Size = New System.Drawing.Size(269, 22)
        Me.cmbGroup.TabIndex = 3
        '
        'cmbTemplates
        '
        Me.cmbTemplates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplates.ForeColor = System.Drawing.Color.Black
        Me.cmbTemplates.Location = New System.Drawing.Point(150, 182)
        Me.cmbTemplates.Name = "cmbTemplates"
        Me.cmbTemplates.Size = New System.Drawing.Size(269, 22)
        Me.cmbTemplates.TabIndex = 5
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.Enabled = False
        Me.cmbCategory.ForeColor = System.Drawing.Color.Black
        Me.cmbCategory.Location = New System.Drawing.Point(150, 148)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(269, 22)
        Me.cmbCategory.TabIndex = 4
        '
        'txtName
        '
        Me.txtName.ForeColor = System.Drawing.Color.Black
        Me.txtName.Location = New System.Drawing.Point(150, 45)
        Me.txtName.MaxLength = 150
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(269, 22)
        Me.txtName.TabIndex = 1
        '
        'txtDimension
        '
        Me.txtDimension.ForeColor = System.Drawing.Color.Black
        Me.txtDimension.Location = New System.Drawing.Point(161, 266)
        Me.txtDimension.Name = "txtDimension"
        Me.txtDimension.Size = New System.Drawing.Size(103, 22)
        Me.txtDimension.TabIndex = 7
        Me.txtDimension.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDimension.Visible = False
        '
        'txtMaleLowerValue
        '
        Me.txtMaleLowerValue.ForeColor = System.Drawing.Color.Black
        Me.txtMaleLowerValue.Location = New System.Drawing.Point(161, 301)
        Me.txtMaleLowerValue.Name = "txtMaleLowerValue"
        Me.txtMaleLowerValue.Size = New System.Drawing.Size(103, 22)
        Me.txtMaleLowerValue.TabIndex = 8
        Me.txtMaleLowerValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMaleLowerValue.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(45, 305)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(111, 14)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Male Lower Value :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label7.Visible = False
        '
        'txtMaleHigherValue
        '
        Me.txtMaleHigherValue.ForeColor = System.Drawing.Color.Black
        Me.txtMaleHigherValue.Location = New System.Drawing.Point(398, 301)
        Me.txtMaleHigherValue.Name = "txtMaleHigherValue"
        Me.txtMaleHigherValue.Size = New System.Drawing.Size(103, 22)
        Me.txtMaleHigherValue.TabIndex = 9
        Me.txtMaleHigherValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtMaleHigherValue.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(285, 305)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(112, 14)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "Male Higher Value :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label8.Visible = False
        '
        'txtFemaleHigherValue
        '
        Me.txtFemaleHigherValue.ForeColor = System.Drawing.Color.Black
        Me.txtFemaleHigherValue.Location = New System.Drawing.Point(398, 336)
        Me.txtFemaleHigherValue.Name = "txtFemaleHigherValue"
        Me.txtFemaleHigherValue.Size = New System.Drawing.Size(103, 22)
        Me.txtFemaleHigherValue.TabIndex = 11
        Me.txtFemaleHigherValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFemaleHigherValue.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(271, 340)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(126, 14)
        Me.Label9.TabIndex = 22
        Me.Label9.Text = "Female Higher Value :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label9.Visible = False
        '
        'txtFemaleLowerValue
        '
        Me.txtFemaleLowerValue.ForeColor = System.Drawing.Color.Black
        Me.txtFemaleLowerValue.Location = New System.Drawing.Point(161, 336)
        Me.txtFemaleLowerValue.Name = "txtFemaleLowerValue"
        Me.txtFemaleLowerValue.Size = New System.Drawing.Size(103, 22)
        Me.txtFemaleLowerValue.TabIndex = 10
        Me.txtFemaleLowerValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtFemaleLowerValue.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(31, 340)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(125, 14)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Female Lower Value :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label10.Visible = False
        '
        'chkIsSpecimen
        '
        Me.chkIsSpecimen.AutoSize = True
        Me.chkIsSpecimen.BackColor = System.Drawing.Color.Transparent
        Me.chkIsSpecimen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkIsSpecimen.Location = New System.Drawing.Point(161, 372)
        Me.chkIsSpecimen.Name = "chkIsSpecimen"
        Me.chkIsSpecimen.Size = New System.Drawing.Size(12, 11)
        Me.chkIsSpecimen.TabIndex = 12
        Me.chkIsSpecimen.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkIsSpecimen.UseVisualStyleBackColor = False
        Me.chkIsSpecimen.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(229, 370)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(68, 14)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "Specimen :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label11.Visible = False
        '
        'cmbIsSpecimen
        '
        Me.cmbIsSpecimen.ForeColor = System.Drawing.Color.Black
        Me.cmbIsSpecimen.Location = New System.Drawing.Point(298, 366)
        Me.cmbIsSpecimen.Name = "cmbIsSpecimen"
        Me.cmbIsSpecimen.Size = New System.Drawing.Size(175, 22)
        Me.cmbIsSpecimen.TabIndex = 13
        Me.cmbIsSpecimen.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(13, 370)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(143, 14)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "Is Specimen Required ? :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label12.Visible = False
        '
        'btnSpeciman
        '
        Me.btnSpeciman.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnSpeciman.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnSpeciman.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSpeciman.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSpeciman.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSpeciman.Image = Global.gloEMR.My.Resources.Resources.Browse
        Me.btnSpeciman.Location = New System.Drawing.Point(479, 366)
        Me.btnSpeciman.Name = "btnSpeciman"
        Me.btnSpeciman.Size = New System.Drawing.Size(22, 25)
        Me.btnSpeciman.TabIndex = 14
        Me.btnSpeciman.UseVisualStyleBackColor = False
        Me.btnSpeciman.Visible = False
        '
        'cmbLabResults
        '
        Me.cmbLabResults.ForeColor = System.Drawing.Color.Black
        Me.cmbLabResults.Location = New System.Drawing.Point(161, 269)
        Me.cmbLabResults.Name = "cmbLabResults"
        Me.cmbLabResults.Size = New System.Drawing.Size(343, 22)
        Me.cmbLabResults.TabIndex = 6
        Me.cmbLabResults.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Location = New System.Drawing.Point(80, 273)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(76, 14)
        Me.Label13.TabIndex = 30
        Me.Label13.Text = "Lab Results :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label13.Visible = False
        '
        'CmbAssociatedItems
        '
        Me.CmbAssociatedItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbAssociatedItems.ForeColor = System.Drawing.Color.Black
        Me.CmbAssociatedItems.FormattingEnabled = True
        Me.CmbAssociatedItems.Location = New System.Drawing.Point(424, 264)
        Me.CmbAssociatedItems.Name = "CmbAssociatedItems"
        Me.CmbAssociatedItems.Size = New System.Drawing.Size(57, 22)
        Me.CmbAssociatedItems.TabIndex = 2
        Me.CmbAssociatedItems.Visible = False
        '
        'lblAssociatedItem
        '
        Me.lblAssociatedItem.AutoSize = True
        Me.lblAssociatedItem.BackColor = System.Drawing.Color.Transparent
        Me.lblAssociatedItem.Location = New System.Drawing.Point(20, 84)
        Me.lblAssociatedItem.Name = "lblAssociatedItem"
        Me.lblAssociatedItem.Size = New System.Drawing.Size(127, 14)
        Me.lblAssociatedItem.TabIndex = 32
        Me.lblAssociatedItem.Text = "Associate E&&M Fields :"
        Me.lblAssociatedItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlToolStripSetupDialog
        '
        Me.pnlToolStripSetupDialog.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStripSetupDialog.Controls.Add(Me.ToolStripSetupDialog)
        Me.pnlToolStripSetupDialog.Controls.Add(Me.Label19)
        Me.pnlToolStripSetupDialog.Controls.Add(Me.Label32)
        Me.pnlToolStripSetupDialog.Controls.Add(Me.Label31)
        Me.pnlToolStripSetupDialog.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStripSetupDialog.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStripSetupDialog.Name = "pnlToolStripSetupDialog"
        Me.pnlToolStripSetupDialog.Size = New System.Drawing.Size(531, 54)
        Me.pnlToolStripSetupDialog.TabIndex = 0
        '
        'ToolStripSetupDialog
        '
        Me.ToolStripSetupDialog.BackColor = System.Drawing.Color.Transparent
        Me.ToolStripSetupDialog.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStripSetupDialog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStripSetupDialog.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripSetupDialog.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStripSetupDialog.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStripSetupDialog.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblSave, Me.tlsbtnCancel})
        Me.ToolStripSetupDialog.Location = New System.Drawing.Point(1, 1)
        Me.ToolStripSetupDialog.Name = "ToolStripSetupDialog"
        Me.ToolStripSetupDialog.Size = New System.Drawing.Size(529, 53)
        Me.ToolStripSetupDialog.TabIndex = 3
        Me.ToolStripSetupDialog.Text = "ToolStrip1"
        '
        'tblSave
        '
        Me.tblSave.BackColor = System.Drawing.Color.Transparent
        Me.tblSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblSave.Image = CType(resources.GetObject("tblSave.Image"), System.Drawing.Image)
        Me.tblSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblSave.Name = "tblSave"
        Me.tblSave.Size = New System.Drawing.Size(66, 50)
        Me.tblSave.Tag = "Save"
        Me.tblSave.Text = "&Save&&Cls"
        Me.tblSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblSave.ToolTipText = "Save and Close "
        '
        'tlsbtnCancel
        '
        Me.tlsbtnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnCancel.Image = CType(resources.GetObject("tlsbtnCancel.Image"), System.Drawing.Image)
        Me.tlsbtnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnCancel.Name = "tlsbtnCancel"
        Me.tlsbtnCancel.Size = New System.Drawing.Size(43, 50)
        Me.tlsbtnCancel.Tag = "Cancel"
        Me.tlsbtnCancel.Text = "&Close"
        Me.tlsbtnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(0, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 53)
        Me.Label19.TabIndex = 46
        Me.Label19.Text = "label4"
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(0, 0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(530, 1)
        Me.Label32.TabIndex = 49
        Me.Label32.Text = "label4"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(530, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(1, 54)
        Me.Label31.TabIndex = 47
        Me.Label31.Text = "label4"
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.txtLoincCode)
        Me.pnlMain.Controls.Add(Me.Label17)
        Me.pnlMain.Controls.Add(Me.btnAssociatedEMItem)
        Me.pnlMain.Controls.Add(Me.Label16)
        Me.pnlMain.Controls.Add(Me.Label15)
        Me.pnlMain.Controls.Add(Me.Label22)
        Me.pnlMain.Controls.Add(Me.Label21)
        Me.pnlMain.Controls.Add(Me.lblAssociatedItem)
        Me.pnlMain.Controls.Add(Me.Label20)
        Me.pnlMain.Controls.Add(Me.CmbAssociatedItems)
        Me.pnlMain.Controls.Add(Me.Label14)
        Me.pnlMain.Controls.Add(Me.cmbLabResults)
        Me.pnlMain.Controls.Add(Me.cmbTemplates)
        Me.pnlMain.Controls.Add(Me.Label13)
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.btnSpeciman)
        Me.pnlMain.Controls.Add(Me.Label2)
        Me.pnlMain.Controls.Add(Me.Label12)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.cmbIsSpecimen)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.Label11)
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.chkIsSpecimen)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.txtFemaleHigherValue)
        Me.pnlMain.Controls.Add(Me.cmbTestGroup)
        Me.pnlMain.Controls.Add(Me.Label9)
        Me.pnlMain.Controls.Add(Me.cmbGroup)
        Me.pnlMain.Controls.Add(Me.txtFemaleLowerValue)
        Me.pnlMain.Controls.Add(Me.cmbCategory)
        Me.pnlMain.Controls.Add(Me.Label10)
        Me.pnlMain.Controls.Add(Me.txtName)
        Me.pnlMain.Controls.Add(Me.txtMaleHigherValue)
        Me.pnlMain.Controls.Add(Me.txtDimension)
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Controls.Add(Me.Label7)
        Me.pnlMain.Controls.Add(Me.txtMaleLowerValue)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 54)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(531, 253)
        Me.pnlMain.TabIndex = 1
        '
        'txtLoincCode
        '
        Me.txtLoincCode.ForeColor = System.Drawing.Color.Black
        Me.txtLoincCode.Location = New System.Drawing.Point(150, 216)
        Me.txtLoincCode.MaxLength = 50
        Me.txtLoincCode.Name = "txtLoincCode"
        Me.txtLoincCode.Size = New System.Drawing.Size(269, 22)
        Me.txtLoincCode.TabIndex = 6
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Location = New System.Drawing.Point(65, 220)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(81, 14)
        Me.Label17.TabIndex = 59
        Me.Label17.Text = "LOINC Code :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnAssociatedEMItem
        '
        Me.btnAssociatedEMItem.BackColor = System.Drawing.Color.Transparent
        Me.btnAssociatedEMItem.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnAssociatedEMItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAssociatedEMItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAssociatedEMItem.Image = CType(resources.GetObject("btnAssociatedEMItem.Image"), System.Drawing.Image)
        Me.btnAssociatedEMItem.Location = New System.Drawing.Point(150, 79)
        Me.btnAssociatedEMItem.Name = "btnAssociatedEMItem"
        Me.btnAssociatedEMItem.Size = New System.Drawing.Size(38, 23)
        Me.btnAssociatedEMItem.TabIndex = 55
        Me.btnAssociatedEMItem.UseVisualStyleBackColor = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.ForeColor = System.Drawing.Color.Red
        Me.Label16.Location = New System.Drawing.Point(88, 115)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(14, 14)
        Me.Label16.TabIndex = 54
        Me.Label16.Text = "*"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.ForeColor = System.Drawing.Color.Red
        Me.Label15.Location = New System.Drawing.Point(59, 49)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(14, 14)
        Me.Label15.TabIndex = 53
        Me.Label15.Text = "*"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(4, 249)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(523, 1)
        Me.Label22.TabIndex = 52
        Me.Label22.Text = "label4"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(4, 3)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(523, 1)
        Me.Label21.TabIndex = 51
        Me.Label21.Text = "label4"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(527, 3)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 247)
        Me.Label20.TabIndex = 50
        Me.Label20.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 3)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 247)
        Me.Label14.TabIndex = 49
        Me.Label14.Text = "label4"
        '
        'frm_LM_TestSetuptDialog
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(531, 307)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlToolStripSetupDialog)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_LM_TestSetuptDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Test"
        Me.pnlToolStripSetupDialog.ResumeLayout(False)
        Me.pnlToolStripSetupDialog.PerformLayout()
        Me.ToolStripSetupDialog.ResumeLayout(False)
        Me.ToolStripSetupDialog.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Public _TestGroupFlag As String
    Public _Category As String
    Public _EditTestGroupName As String
    Public _EditTestGroupID As Long
    Public _SaveFlag As Boolean = False
    'SHUBHANGI
    Dim _arrLabs As New ArrayList
    Dim _arrManagment As New ArrayList
    Dim _arrOrders As New ArrayList
    Dim _arrOtherDiag As New ArrayList
    Dim ToolTip1 As New System.Windows.Forms.ToolTip

    Private Sub frm_LM_TestSetuptDialog_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        ToolTip1.Dispose()
        ToolTip1 = Nothing
        _arrLabs.Clear()
        _arrLabs = Nothing
        _arrManagment.Clear()
        _arrManagment = Nothing
        _arrOrders.Clear()
        _arrOrders = Nothing
        _arrOtherDiag.Clear()
        _arrOtherDiag = Nothing

    End Sub

    Private Sub TestSetupDialog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '// Fill Group/Test
        With cmbTestGroup
            .Items.Clear()
            Dim oSupporting As New gloStream.LabModule.Test.Supporting.Supporting
            .Items.Add(oSupporting.GetTestGroupFlagName("T"))
            .Items.Add(oSupporting.GetTestGroupFlagName("G"))

            oSupporting = Nothing
        End With

        '// Fill Categories
        With cmbCategory
            .Items.Clear()
            Dim oCategories As gloStream.LabModule.Category.Supporting.Categories
            Dim oMaintainCategories As New gloStream.LabModule.Category.MaintainCategory

            Dim oSupporting As New gloStream.LabModule.Category.Supporting.Supporting
            Dim _Type As String = oSupporting.CategoryType_enum_AsString(gloStream.LabModule.Category.Supporting.Supporting.enumCategoryType.Order)
            oSupporting = Nothing

            oCategories = oMaintainCategories.Categories(_Type)
            If Not oCategories Is Nothing Then
                For i As Int16 = 1 To oCategories.Count
                    .Items.Add(oCategories(i).Description)
                Next
                oCategories.Dispose()
            End If

            oCategories = Nothing
            oMaintainCategories = Nothing
        End With

        '// Fill Groups
        With cmbGroup
            .Items.Clear()
            Dim oSupporting As New gloStream.LabModule.Test.Supporting.Supporting
            Dim oGroups As Collection
            oGroups = oSupporting.Groups(_Category.Trim.Replace("'", "''"))
            If Not oGroups Is Nothing Then
                For i As Int16 = 1 To oGroups.Count
                    .Items.Add(oGroups(i))
                Next
            End If
            oGroups = Nothing
            oSupporting = Nothing
        End With

        '// Fill Templates
        With cmbTemplates
            .Items.Clear()
            Dim oSupporting As New gloStream.LabModule.Test.Supporting.Supporting
            Dim oTemplates As Collection
            oTemplates = oSupporting.Templates()
            If Not oTemplates Is Nothing Then
                For i As Int16 = 1 To oTemplates.Count
                    .Items.Add(oTemplates(i))
                Next
            End If
            oTemplates = Nothing
            oSupporting = Nothing
        End With

        '// Fill Lab Results
        With cmbLabResults
            .Items.Clear()
            Dim oSupporting As New gloStream.LabModule.Test.Supporting.Supporting
            Dim oLabResults As Collection
            oLabResults = oSupporting.LabResults
            If Not oLabResults Is Nothing Then
                For i As Int16 = 1 To oLabResults.Count
                    .Items.Add(oLabResults(i))
                Next
            End If
            oLabResults = Nothing
            oSupporting = Nothing
        End With

        '// Fill Specimens
        With cmbIsSpecimen
            .Items.Clear()
            Dim oSpecimen As gloStream.LabModule.Specimen.Supporting.Specimens
            Dim oMaintainSpecimen As New gloStream.LabModule.Specimen.MaintainSpecimen

            oSpecimen = oMaintainSpecimen.Specimens()
            If Not oSpecimen Is Nothing Then
                For i As Int16 = 1 To oSpecimen.Count
                    .Items.Add(oSpecimen(i).Description)
                Next
            End If
            oSpecimen = Nothing
            oMaintainSpecimen = Nothing
        End With

        '// Controls
        cmbTestGroup.Enabled = False
        cmbCategory.Enabled = False
        cmbIsSpecimen.Enabled = False


        cmbTestGroup.SelectedItem = _TestGroupFlag
        cmbCategory.SelectedItem = _Category

        cmbGroup.Text = ""
        cmbTemplates.Text = ""
        txtDimension.Text = ""
        txtMaleLowerValue.Text = ""
        txtMaleHigherValue.Text = ""
        txtFemaleLowerValue.Text = ""
        txtFemaleHigherValue.Text = ""
        chkIsSpecimen.Checked = False
        cmbIsSpecimen.Text = ""

        Dim oTestGroupFlag As New gloStream.LabModule.Test.Supporting.Supporting
        If _TestGroupFlag = oTestGroupFlag.GetTestGroupFlagName("T") Then
            cmbGroup.Enabled = True
            cmbTemplates.Enabled = True
            cmbLabResults.Enabled = True
            txtLoincCode.Enabled = True

            txtDimension.Enabled = True
            txtMaleLowerValue.Enabled = True
            txtMaleHigherValue.Enabled = True
            txtFemaleLowerValue.Enabled = True
            txtFemaleHigherValue.Enabled = True
            chkIsSpecimen.Enabled = True
            cmbIsSpecimen.Enabled = False
            btnSpeciman.Enabled = False
            CmbAssociatedItems.Enabled = True
        Else
            cmbGroup.Enabled = False
            cmbGroup.SelectedItem = Nothing
            cmbTemplates.Enabled = False
            cmbLabResults.Enabled = False
            cmbTemplates.SelectedItem = Nothing
            txtLoincCode.Enabled = False

            txtDimension.Enabled = False
            txtMaleLowerValue.Enabled = False
            txtMaleHigherValue.Enabled = False
            txtFemaleLowerValue.Enabled = False
            txtFemaleHigherValue.Enabled = False
            chkIsSpecimen.Enabled = False
            cmbIsSpecimen.Enabled = False
            btnSpeciman.Enabled = False
            cmbIsSpecimen.SelectedItem = Nothing
            CmbAssociatedItems.Enabled = False
            'Shubhangi 20100617
            btnAssociatedEMItem.Enabled = False
        End If
        'COMMENTED BY SHUBHANGI 20100617
        'FillAssociateEM()
        'ADDED BY SHUBHANGI 20100617 COZ FOR ADDING MULTIPLE ASSOCIATION FOR EM 
        FillAssociateEMNew()
        If Not _SaveFlag = True Then
            If Not _EditTestGroupID = 0 Then
                Dim oEditTestGroupDetail As New gloStream.LabModule.Test.MaintainTest
                Dim oTestGroupDetail As gloStream.LabModule.Test.Supporting.Test

                oTestGroupDetail = oEditTestGroupDetail.Test(_EditTestGroupName.Trim.Replace("'", "''"), _Category.Trim.Replace("'", "''"))

                If Not oTestGroupDetail Is Nothing Then
                    With oTestGroupDetail
                        txtName.Text = .Name.Trim
                        ''Added Rahul for LONIC Code on 20101020
                        txtLoincCode.Text = .LoincCode.Trim
                        ''
                        If _TestGroupFlag = oTestGroupFlag.GetTestGroupFlagName("T") Then ' Test
                            cmbGroup.SelectedItem = .GroupName
                            If .TemplateName.Trim <> "" Then
                                cmbTemplates.Text = .TemplateName
                                For i As Int16 = 0 To cmbTemplates.Items.Count - 1
                                    If cmbTemplates.Items(i) = cmbTemplates.Text Then
                                        cmbTemplates.SelectedIndex = i
                                        Exit For
                                    End If
                                Next
                            End If
                            If .LabResultName.Trim <> "" Then
                                cmbLabResults.Text = .LabResultName
                                For i As Int16 = 0 To cmbLabResults.Items.Count - 1
                                    If cmbLabResults.Items(i) = cmbLabResults.Text Then
                                        cmbLabResults.SelectedIndex = i
                                        Exit For
                                    End If
                                Next
                            End If

                            txtDimension.Text = .Dimension
                            txtMaleLowerValue.Text = .MaleLowerValue
                            txtMaleHigherValue.Text = .MaleHigherValue
                            txtFemaleLowerValue.Text = .FemaleLowerValue
                            txtFemaleHigherValue.Text = .FemaleHigherValue
                            chkIsSpecimen.Checked = .IsSpecimenRequired
                            If .SpecimenName.Trim <> "" Then
                                cmbIsSpecimen.SelectedItem = .SpecimenName
                            End If
                            If chkIsSpecimen.Checked = True Then
                                cmbIsSpecimen.Enabled = True
                                btnSpeciman.Enabled = True
                            End If
                        End If
                        Dim oTGCode As New gloStream.LabModule.Test.Supporting.Supporting
                        Dim strAssociateEMItem As String = oTGCode.GetAssociatedEMField(.ID)
                        'Dim strarr() As String
                        'strarr = Split(strAssociateEMItem, "-")
                        If strAssociateEMItem <> "" Then
                            '20090909
                            If CmbAssociatedItems.FindStringExact(strAssociateEMItem) > 0 Then
                                CmbAssociatedItems.Text = strAssociateEMItem
                            End If
                        Else
                            CmbAssociatedItems.Text = "Select EM Field"
                        End If

                        oTGCode = Nothing
                    End With
                End If

                oTestGroupDetail = Nothing

                oEditTestGroupDetail = Nothing
            End If
        End If

        oTestGroupFlag = Nothing


        ToolTip1.SetToolTip(Me.btnAssociatedEMItem, "Associate E&M Fields")

    End Sub
    'ADDED BY SHUBHANGI 20100617 
    Private Sub FillAssociateEMNew()

        Dim con As New SqlClient.SqlConnection(GetConnectionString)
        Dim query As String
        query = " SELECT sAssociatedEMName,sAssociatedEMCategory,sStatus FROM AssociatedEMField WHERE nFieldID = " & _EditTestGroupID & ""

        Dim cmd As New SqlCommand(query, con)
        Dim adp As New SqlDataAdapter(cmd)
        Dim _dt As New DataTable
        adp.Fill(_dt)
        _arrOrders.Clear()
        _arrOtherDiag.Clear()
        _arrLabs.Clear()
        _arrManagment.Clear()
        'Dim _arrOrders As New List(Of myList)
        _arrOrders = New ArrayList
        _arrOtherDiag = New ArrayList
        _arrLabs = New ArrayList
        _arrManagment = New ArrayList
        'Dim mylist As New myList
        'Dim oListItem As gloGeneralItem.gloItem
        'If dtAssociated.Rows.Count > 0 Then
        '    'For i As Integer = 0 To dtAssociated.Rows.Count - 1
        '    '    mylist = New myList
        '    '    mylist.AssociatedProperty = dtAssociated.Rows(i)(0).ToString
        '    '    mylist.AssociatedCategory = dtAssociated.Rows(i)(1).ToString
        '    '    If dtAssociated.Rows(i)(1).ToString = "Orders" Then
        '    '        _arrOrders.Add(mylist)
        '    '    ElseIf dtAssociated.Rows(i)(1).ToString = "OtherDiagnosis" Then
        '    '        _arrOtherDiag.Add(mylist)
        '    '    End If
        '    'Next
        '    For i As Integer = 0 To dtAssociated.Rows.Count - 1

        '        oListItem = New gloGeneralItem.gloItem
        '        oListItem.Description = dtAssociated.Rows(i)(0).ToString
        '        oListItem.Code = dtAssociated.Rows(i)(1).ToString
        '        If dtAssociated.Rows(i)(1).ToString = "Orders" Then
        '            _arrOrders.Add(oListItem)
        '        ElseIf dtAssociated.Rows(i)(1).ToString = "OtherDiagnosis" Then
        '            _arrOtherDiag.Add(oListItem)
        '        ElseIf dtAssociated.Rows(i)(1).ToString = "Labs" Then
        '            _arrLabs.Add(oListItem)
        '        ElseIf dtAssociated.Rows(i)(1).ToString = "ManagementOption" Then
        '            _arrManagment.Add(oListItem)
        '        End If
        '    Next
        'End If
        If cmd IsNot Nothing Then
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
        End If
        If adp IsNot Nothing Then

            adp.Dispose()
            adp = Nothing
        End If
        If (IsNothing(con) = False) Then
            con.Dispose()
            con = Nothing
        End If
        Dim oListItem As gloGeneralItem.gloItem
        If Not IsNothing(_dt) Then
            For i As Integer = 0 To _dt.Rows.Count - 1
                ''Labs
                If _dt.Rows(i)("sAssociatedEMName") = "IncisionalBiopsyRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IncisionalBiopsyRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "IncisionalBiopsyRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "SuperficialBiopsyRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "SuperficialBiopsyRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "SuperficialBiopsyRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "TypeCrossmatchRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "TypeCrossmatchRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "TypeCrossmatchRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "PTRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PTRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PTRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "ABGsRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ABGsRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ABGsRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "CardiacEnzymesRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CardiacEnzymesRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CardiacEnzymesRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ChemicalProfileRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ChemicalProfileRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ChemicalProfileRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DrugScreenRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DrugScreenRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DrugScreenRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ElectrolytesRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ElectrolytesRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ElectrolytesRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "BunCreatinineRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "BunCreatinineRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "BunCreatinineRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "AmylaseRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "AmylaseRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "AmylaseRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "PregnancyTestRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PregnancyTestRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PregnancyTestRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "FluStrepMonoRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "FluStrepMonoRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "FluStrepMonoRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "CbcUaRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CbcUaRoutine"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CbcUaRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)

                End If

                If _dt.Rows(i)("sAssociatedEMName") = "IndependentVisualTest" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IndependentVisualTest"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CbcUaRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)

                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiscussionWPerformingPhys" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscussionWPerformingPhys"
                    oListItem.Code = strLabs.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CbcUaRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)

                End If
                If _dt.Rows(i)("sAssociatedEMName") = "OtherLabsCount" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strLabs.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "OtherLabsCount"
                    oListItem.Code = strLabs.ToString()
                    oListItem.Status = _dt.Rows(i)("sStatus").ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrLabs.Add(oListItem)
                    oListItem.Dispose()
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CbcUaRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)

                End If


                ''orders

                If _dt.Rows(i)("sAssociatedEMName") = "VascularStudiesWRiskRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "VascularStudiesWRiskRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "IncisionalBiopsyRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "VascularStudiesRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "VascularStudiesRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "SuperficialBiopsyRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "MRIRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MRIRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "TypeCrossmatchRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "CATScanRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CATScanRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PTRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "IVPRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IVPRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ABGsRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "GIGallbladderRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "GIGallbladderRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CardiacEnzymesRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "TLSpineRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "TLSpineRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ChemicalProfileRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiscographyRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscographyRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DrugScreenRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiagUltrasoundRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiagUltrasoundRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ElectrolytesRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "CSpineRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CSpineRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "BunCreatinineRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "HipPelvisRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "HipPelvisRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "AmylaseRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "AbdomenRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "AbdomenRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PregnancyTestRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ExtremitiesRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ExtremitiesRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "FluStrepMonoRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "ChestRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ChestRoutine"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CbcUaRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)

                End If
                If _dt.Rows(i)("sAssociatedEMName") = "IndependentVisualTest" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IndependentVisualTest"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()
                End If

                '    'Emylist = New myList
                '    'Emylist.AssociatedProperty = "IndependentVisualTest"
                '    'Emylist.AssociatedCategory = strLabs.ToString()
                '    'Emylist.AssociatedItem = "True"
                '    '_arrLabs.Add(Emylist)
                'End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiscussWPerformingPhys" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscussWPerformingPhys"
                    oListItem.Code = strOrders.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DiscussionWPerformingPhys"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "OtherXRaysCount" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOrders.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "OtherXRaysCount"
                    oListItem.Code = strOrders.ToString()
                    oListItem.Status = _dt.Rows(i)("sStatus").ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOrders.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "OtherLabsCount"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = _dt.Rows(i)("sStatus").ToString()
                    '_arrLabs.Add(Emylist)
                End If

                ''other diagnosis

                If _dt.Rows(i)("sAssociatedEMName") = "EndoscopeWRiskRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "EndoscopeWRiskRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "IncisionalBiopsyRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "EndoscopeRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "EndoscopeRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "SuperficialBiopsyRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "CuldocentesesRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "CuldocentesesRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "TypeCrossmatchRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "ThoracentesisRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ThoracentesisRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PTRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "LumbarPunctureRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "LumbarPunctureRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ABGsRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "NuclearScanRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "NuclearScanRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CardiacEnzymesRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "PulmonaryStudiesRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PulmonaryStudiesRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ChemicalProfileRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "DopplerFlowStudiesRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DopplerFlowStudiesRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DrugScreenRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "VectorcardiogramRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "VectorcardiogramRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ElectrolytesRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "EegEmgRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "EegEmgRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "BunCreatinineRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "TreadmillStressTestRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "TreadmillStressTestRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "AmylaseRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "HolterMonitorRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "HolterMonitorRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PregnancyTestRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "EkgEcgRoutine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "EkgEcgRoutine"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "FluStrepMonoRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If


                If _dt.Rows(i)("sAssociatedEMName") = "IndependentVisualTest" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IndependentVisualTest"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()
                End If

                '    'Emylist = New myList
                '    'Emylist.AssociatedProperty = "IndependentVisualTest"
                '    'Emylist.AssociatedCategory = strLabs.ToString()
                '    'Emylist.AssociatedItem = "True"
                '    '_arrLabs.Add(Emylist)
                'End If
                If _dt.Rows(i)("sAssociatedEMName") = "DiscussWPerformingPhys" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscussWPerformingPhys"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DiscussionWPerformingPhys"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "OtherDiagnosticStudiesCount" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strOtherDiagnosis.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "OtherDiagnosticStudiesCount"
                    oListItem.Code = strOtherDiagnosis.ToString()
                    oListItem.Status = _dt.Rows(i)("sStatus").ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrOtherDiag.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "OtherLabsCount"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = _dt.Rows(i)("sStatus").ToString()
                    '_arrLabs.Add(Emylist)
                End If


                ''management option


                If _dt.Rows(i)("sAssociatedEMName") = "DiscussCaseWHealthProvider" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DiscussCaseWHealthProvider"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "IncisionalBiopsyRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "ReviewMedicalRecsOther" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ReviewMedicalRecsOther"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "SuperficialBiopsyRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "DecisionObtainMedicalRecsOther" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DecisionObtainMedicalRecsOther"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "TypeCrossmatchRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "DecisionNotResuscitate" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "DecisionNotResuscitate"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PTRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "MajorEmergencySurgery" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MajorEmergencySurgery"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ABGsRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "MajorSurgeryWRiskFactors" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MajorSurgeryWRiskFactors"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "CardiacEnzymesRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "MajorSurgery" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MajorSurgery"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ChemicalProfileRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "MinorSurgeryWRiskFactors" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MinorSurgeryWRiskFactors"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DrugScreenRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "MinorSurgery" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "MinorSurgery"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "ElectrolytesRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "ClosedFx" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ClosedFx"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "BunCreatinineRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "PhysicalTherapy" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PhysicalTherapy"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "AmylaseRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "NuclearMedicine" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "NuclearMedicine"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "PregnancyTestRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If
                If _dt.Rows(i)("sAssociatedEMName") = "RespiratoryTreatments" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "RespiratoryTreatments"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "FluStrepMonoRoutine"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If


                If _dt.Rows(i)("sAssociatedEMName") = "Telemetry" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then

                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "Telemetry"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()
                End If

                '    'Emylist = New myList
                '    'Emylist.AssociatedProperty = "IndependentVisualTest"
                '    'Emylist.AssociatedCategory = strLabs.ToString()
                '    'Emylist.AssociatedItem = "True"
                '    '_arrLabs.Add(Emylist)
                'End If
                If _dt.Rows(i)("sAssociatedEMName") = "HighRiskMeds" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "HighRiskMeds"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DiscussionWPerformingPhys"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "IVMedsWAdditives" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IVMedsWAdditives"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DiscussionWPerformingPhys"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "IVMeds" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "IVMeds"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DiscussionWPerformingPhys"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "PrescripIMMeds" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "PrescripIMMeds"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DiscussionWPerformingPhys"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "OverCounterMeds" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "OverCounterMeds"
                    oListItem.Code = strMangementOption.ToString()
                    'EGeneralItems.Add(oListItem)
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "DiscussionWPerformingPhys"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = "True"
                    '_arrLabs.Add(Emylist)
                End If

                If _dt.Rows(i)("sAssociatedEMName") = "ConfWPatientFamilyMinutes" AndAlso _dt.Rows(i)("sAssociatedEMCategory") = strMangementOption.ToString() Then
                    oListItem = New gloGeneralItem.gloItem
                    oListItem.Description = "ConfWPatientFamilyMinutes"
                    oListItem.Code = strMangementOption.ToString()
                    oListItem.Status = _dt.Rows(i)("sStatus").ToString()
                    _arrManagment.Add(oListItem)
                    oListItem.Dispose()

                    'Emylist = New myList
                    'Emylist.AssociatedProperty = "OtherLabsCount"
                    'Emylist.AssociatedCategory = strLabs.ToString()
                    'Emylist.AssociatedItem = _dt.Rows(i)("sStatus").ToString()
                    '_arrLabs.Add(Emylist)
                End If
            Next


            _dt.Dispose()
            _dt = Nothing


        End If


    End Sub
    Private Sub FillAssociateEM()
        Try
            Dim strprop As String = ""
            Dim pType As Type





            Dim arrlist As New List(Of myList)
            Dim mylist As myList
            Dim cnt As Integer = 0
            'Declare a variable for PropertyInfos
            Dim propertyInfos As PropertyInfo()
            propertyInfos = GetType(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexXrayRadiology).GetProperties(BindingFlags.CreateInstance Or BindingFlags.DeclaredOnly Or BindingFlags.Default Or BindingFlags.ExactBinding Or BindingFlags.FlattenHierarchy Or BindingFlags.GetField Or BindingFlags.GetProperty Or BindingFlags.IgnoreCase Or BindingFlags.IgnoreReturn Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.OptionalParamBinding Or BindingFlags.Public Or BindingFlags.PutDispProperty Or BindingFlags.PutRefDispProperty Or BindingFlags.SetField Or BindingFlags.SetProperty Or BindingFlags.Static Or BindingFlags.SuppressChangeType)
            'For Loop for Traversing all Elements in PropertyInfos List
            If CmbAssociatedItems.Text = "" Then
                For Each propertyInfo As PropertyInfo In propertyInfos
                    strprop = propertyInfo.Name
                    pType = propertyInfo.PropertyType
                    'Add Items in combo box
                    If pType.Name = "Boolean" Then    ''FullName = "System.Boolean"
                        If strprop <> "IndependentVisualTest" AndAlso strprop <> "DiscussWPerformingPhys" Then
                            If strprop.EndsWith("Urgent") <> True Then
                                If cnt = 0 Then
                                    mylist = New myList
                                    mylist.AssociatedProperty = "Select EM Field"
                                    arrlist.Add(mylist)
                                Else
                                    mylist = New myList
                                    mylist.AssociatedProperty = strprop
                                    mylist.CategoryType = CategoryType.X_Ray_Radiology
                                    arrlist.Add(mylist)
                                End If
                                cnt = cnt + 1
                            End If
                        End If
                    End If
                Next


                'For Other Diagnostic tests
                'Dim propertyInfos As PropertyInfo()
                propertyInfos = GetType(AlphaII.CodeWizard.Objects.EvaluationManagement.MedComplexOtherDiagnosticTests).GetProperties(BindingFlags.CreateInstance Or BindingFlags.DeclaredOnly Or BindingFlags.Default Or BindingFlags.ExactBinding Or BindingFlags.FlattenHierarchy Or BindingFlags.GetField Or BindingFlags.GetProperty Or BindingFlags.IgnoreCase Or BindingFlags.IgnoreReturn Or BindingFlags.Instance Or BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.OptionalParamBinding Or BindingFlags.Public Or BindingFlags.PutDispProperty Or BindingFlags.PutRefDispProperty Or BindingFlags.SetField Or BindingFlags.SetProperty Or BindingFlags.Static Or BindingFlags.SuppressChangeType)

                'For Loop for Traversing all Elements in PropertyInfos List
                For Each propertyInfo As PropertyInfo In propertyInfos
                    strprop = propertyInfo.Name
                    pType = propertyInfo.PropertyType

                    'Add Items in combo box
                    If pType.Name = "Boolean" Then    ''FullName = "System.Boolean"
                        If strprop <> "IndependentVisualTest" AndAlso strprop <> "DiscussWPerformingPhys" Then
                            If strprop.EndsWith("Urgent") <> True Then
                                If arrlist.Count = 0 Then
                                    mylist = New myList
                                    mylist.AssociatedProperty = "Select EM Field"
                                    arrlist.Add(mylist)
                                Else
                                    mylist = New myList
                                    mylist.AssociatedProperty = strprop
                                    mylist.CategoryType = CategoryType.Other_Diagonsis_Tests
                                    arrlist.Add(mylist)
                                End If
                            End If
                        End If
                    End If
                Next
            End If
            CmbAssociatedItems.DataSource = arrlist
            CmbAssociatedItems.DisplayMember = "AssociatedProperty"
            CmbAssociatedItems.ValueMember = "CategoryType" '"Group_ID"
            If (arrlist.Count > 0) Then
                CmbAssociatedItems.SelectedIndex = 0
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkIsSpecimen_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkIsSpecimen.CheckedChanged
        If chkIsSpecimen.Checked = True Then
            cmbIsSpecimen.Enabled = True
            If cmbIsSpecimen.Items.Count > 0 Then
                cmbIsSpecimen.SelectedIndex = 0
                btnSpeciman.Enabled = True
            End If
        Else
            cmbIsSpecimen.Enabled = False
            cmbIsSpecimen.SelectedItem = Nothing
            btnSpeciman.Enabled = False
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblSave.Click
        Dim oTestSupporting As New gloStream.LabModule.Test.Supporting.Supporting
        Dim _ChkTestGroupFlag As String = oTestSupporting.GetTestGroupFlagName("T")
        oTestSupporting = Nothing

        If txtName.Text.Trim = "" Then
            MessageBox.Show("Please enter description to continue", gstrMessageBoxCaption, MessageBoxButtons.OK)
            Exit Sub
        End If

        If cmbTestGroup.SelectedItem Is Nothing Then
            MessageBox.Show("Please specify is test or group", gstrMessageBoxCaption, MessageBoxButtons.OK)
            Exit Sub
        End If

        If cmbCategory.SelectedItem Is Nothing Then
            MessageBox.Show("Please specify category", gstrMessageBoxCaption, MessageBoxButtons.OK)
            Exit Sub
        End If

        If cmbTestGroup.SelectedItem = _ChkTestGroupFlag Then
            If cmbGroup.SelectedItem Is Nothing Then
                MessageBox.Show("Please select group to continue", gstrMessageBoxCaption, MessageBoxButtons.OK)
                Exit Sub
            End If
        Else
            If Not cmbGroup.SelectedItem Is Nothing Then
                MessageBox.Show("Please unselect group to continue", gstrMessageBoxCaption, MessageBoxButtons.OK)
                Exit Sub
            End If
        End If
        Dim Result As DialogResult
        Dim oMaintainTest As New gloStream.LabModule.Test.MaintainTest
        Dim oTestDetail As New gloStream.LabModule.Test.Supporting.Test
        'Code Added by Mayuri:20091001
        'To fix Bug ID:#4052 If template not associated with test then display MessageBox
        If cmbTestGroup.SelectedItem <> "Group" AndAlso cmbTemplates.Text = "" Then
            Result = MessageBox.Show("Template is not associated with this test, Do you want to associate it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        End If
        'If Result is No and Template field is blank

        'Shubhangi 20091120 
        'Add this condition 'Coz for group type we dont require to associate template for that.
        If Result = Windows.Forms.DialogResult.No OrElse (cmbTemplates.Text <> "" AndAlso cmbTestGroup.SelectedItem <> "Group") OrElse (cmbTestGroup.SelectedItem = "Group") Then

            With oTestDetail
                If _SaveFlag = True Then
                    .ID = 0
                Else
                    .ID = _EditTestGroupID
                End If

                .Name = txtName.Text.Trim.Replace("'", "''")
                ''Added Rahul for LOINC Code on 20101020
                .LoincCode = txtLoincCode.Text.Trim.Replace("'", "''")
                ''
                If CmbAssociatedItems.Text <> "Select EM Field" Then
                    'Dim strAssociatedEMField As String = CmbAssociatedItems.Text & "-" & CmbAssociatedItems.SelectedValue
                    If CmbAssociatedItems.SelectedValue = CategoryType.X_Ray_Radiology Then
                        .AssociateEMCategory = strOrders
                    ElseIf CmbAssociatedItems.SelectedValue = CategoryType.Other_Diagonsis_Tests Then
                        .AssociateEMCategory = strOtherDiagnosis
                    Else
                        .AssociateEMCategory = ""
                    End If
                    .AssociateEM = CmbAssociatedItems.Text
                Else
                    .AssociateEM = ""
                    .AssociateEMCategory = ""
                End If

                Dim oTGCode As New gloStream.LabModule.Test.Supporting.Supporting
                .TestGroupFlagCode = oTGCode.GetTestGroupFlagCode(_TestGroupFlag)
                oTGCode = Nothing
                .TestGroupFlagName = _TestGroupFlag

                .CategoryID = 0
                .CategoryName = _Category.Trim
                If cmbTestGroup.SelectedItem = _ChkTestGroupFlag Then ' Test
                    .TemplateID = 0
                    If Not cmbTemplates.SelectedItem Is Nothing Then
                        .TemplateName = cmbTemplates.SelectedItem.Trim.Replace("'", "''")
                    End If

                    .LabResultID = 0
                    If Not cmbLabResults.SelectedItem Is Nothing Then
                        .LabResultName = cmbLabResults.SelectedItem.Trim.Replace("'", "''")
                    End If

                    .GroupNo = 0
                    If Not cmbGroup.SelectedItem Is Nothing Then
                        .GroupName = cmbGroup.SelectedItem.Trim.Replace("'", "''")
                    End If
                    .LevelNo = 0
                    .Dimension = txtDimension.Text.Trim
                    .MaleLowerValue = CDbl(Val(txtMaleLowerValue.Text.Trim))
                    .MaleHigherValue = CDbl(Val(txtMaleHigherValue.Text.Trim))
                    .FemaleLowerValue = CDbl(Val(txtFemaleLowerValue.Text.Trim))
                    .FemaleHigherValue = CDbl(Val(txtFemaleHigherValue.Text.Trim))
                    .IsSpecimenRequired = chkIsSpecimen.Checked
                    .SpecimenID = 0
                    If Not cmbIsSpecimen.SelectedItem Is Nothing Then
                        If chkIsSpecimen.Checked = True Then
                            .SpecimenName = cmbIsSpecimen.SelectedItem.Trim.Replace("'", "''")
                        End If
                    End If
                Else ' Group
                    .TemplateID = 0
                    .TemplateName = ""
                    .LabResultID = 0
                    .LabResultName = ""
                    .GroupNo = 0
                    .GroupName = ""
                    .LevelNo = 0
                    .Dimension = ""
                    .MaleLowerValue = 0
                    .MaleHigherValue = 0
                    .FemaleLowerValue = 0
                    .FemaleHigherValue = 0
                    .IsSpecimenRequired = False
                    .SpecimenID = 0
                    .SpecimenName = ""
                    ''Added Rahul on 20100918
                    .LoincCode = ""
                    ''
                End If

            End With
            If Not oTestDetail Is Nothing Then
                If _SaveFlag = True Then
                    ''
                    ' If Result = Windows.Forms.DialogResult.No Then
                    'COMMENTED BY SHUBHANGI 20100617
                    'If oMaintainTest.Add(oTestDetail) = False Then
                    'ADDED BY SHUBHANGI 20100617
                    If oMaintainTest.IsPresent_TestOrGroup(oTestDetail) = False Then
                        If oMaintainTest.Add(oTestDetail, _arrOrders, _arrOtherDiag, _arrLabs, _arrManagment) = False Then

                            MessageBox.Show(_TestGroupFlag & " not added successfully, please try after some time", gstrMessageBoxCaption, MessageBoxButtons.OK)
                            ''Sandip Darade 20090901
                            ''Let the user know ,entry added successfully
                        Else
                            ' MessageBox.Show(_TestGroupFlag & "  added successfully !  ", gstrMessageBoxCaption, MessageBoxButtons.OK)
                            Me.Close()
                        End If
                    Else
                        MessageBox.Show("The " & Me.Text & " is already exist.", gstrMessageBoxCaption, MessageBoxButtons.OK)
                    End If
                Else

                    Dim oLab As New gloEMRGeneralLibrary.gloEMRLab.gloEMRLabTest

                    If oMaintainTest.IsPresent_TestOrGroup(oTestDetail) = False Then
                        If oMaintainTest.Modify(_EditTestGroupName.Trim.Replace("'", "''"), oTestDetail, _arrOrders, _arrOtherDiag, _arrLabs, _arrManagment) = False Then
                            MessageBox.Show(_TestGroupFlag & " not modified successfully, please try after some time", gstrMessageBoxCaption, MessageBoxButtons.OK)
                            ''Sandip Darade 20090901
                            ''Let the user know ,entry added successfully
                        Else
                            '' ---------------
                            ''GLO2010-0004476 : Updates to Labs/Orders not reflected in Smart Orders already configured
                            oLab.UpdateAssociatedSmartOrders(oTestDetail.Name.Trim.Replace("'", "''"), oTestDetail.ID)
                            oLab.UpdateAssociatedSmartDiagnosis(oTestDetail.Name.Trim.Replace("'", "''"), oTestDetail.ID)
                            '' UpdateAssociatedSmartTreatment not available due to in assocation table on id is considered
                            '' ---------------
                            Me.Close()
                        End If

                        'Release object after use
                        If Not IsNothing(oLab) Then
                            oLab.Dispose()
                        End If
                    Else
                        MessageBox.Show("The " & Me.Text & " is already exist.", gstrMessageBoxCaption, MessageBoxButtons.OK)
                        If Not IsNothing(oLab) Then
                            oLab.Dispose()
                        End If

                        Return
                    End If
                End If
            End If
            'Added by Mayuri:20091001
        Else '' if user want to associate templete with the test
            cmbTemplates.Focus()
            Exit Sub
            'End code by Mayuri:20091001

        End If
        oMaintainTest = Nothing
        oTestDetail = Nothing
        ' End If
        If _SaveFlag = False Then
            Me.Close()
        Else
            txtName.Text = ""
            txtDimension.Text = ""
            txtMaleLowerValue.Text = ""
            txtMaleHigherValue.Text = ""
            txtFemaleLowerValue.Text = ""
            txtFemaleHigherValue.Text = ""
        End If

    End Sub

    Private Sub btnSpeciman_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSpeciman.Click
        Dim _PreviousSpecimen As String = ""
        Dim _SelectedIndex As Integer = -1
        Dim _CurrentSpecimen As String = ""

        If Not cmbIsSpecimen.SelectedItem Is Nothing Then
            _PreviousSpecimen = cmbIsSpecimen.SelectedItem
        End If

        Dim oSpecimenDialog As New frm_LM_SpecimenSetup
        oSpecimenDialog.ShowDialog(IIf(IsNothing(oSpecimenDialog.Parent), Me, oSpecimenDialog.Parent))
        _CurrentSpecimen = oSpecimenDialog.txtDescription.Text.Trim.Replace("'", "''")
        oSpecimenDialog.Dispose()

        '// Fill Specimens
        With cmbIsSpecimen
            .Items.Clear()
            Dim oSpecimen As gloStream.LabModule.Specimen.Supporting.Specimens
            Dim oMaintainSpecimen As New gloStream.LabModule.Specimen.MaintainSpecimen

            oSpecimen = oMaintainSpecimen.Specimens()
            If Not oSpecimen Is Nothing Then
                For i As Int16 = 1 To oSpecimen.Count
                    .Items.Add(oSpecimen(i).Description)
                Next
            End If
            oSpecimen = Nothing
            oMaintainSpecimen = Nothing
        End With

        For i As Int16 = 0 To cmbIsSpecimen.Items.Count - 1
            If cmbIsSpecimen.Items(i) = _CurrentSpecimen Then
                _SelectedIndex = i
                Exit For
            End If
        Next

        If _SelectedIndex = -1 Then
            For i As Int16 = 0 To cmbIsSpecimen.Items.Count - 1
                If cmbIsSpecimen.Items(i) = _PreviousSpecimen Then
                    _SelectedIndex = i
                    Exit For
                End If
            Next
        End If

        If Not _SelectedIndex = -1 Then
            cmbIsSpecimen.SelectedIndex = _SelectedIndex
        End If

    End Sub

    Private Sub txtName_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtName.Leave
        On Error Resume Next
        Dim oTestSupporting As New gloStream.LabModule.Test.Supporting.Supporting
        Dim _ChkTestGroupFlag As String = oTestSupporting.GetTestGroupFlagName("G")
        oTestSupporting = Nothing
        If cmbTestGroup.SelectedItem = _ChkTestGroupFlag Then
            txtName.Text = UCase(txtName.Text)
        End If
    End Sub

    Private Sub tlsbtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        btnOK_Click(sender, e)
    End Sub
    Private Sub tlsbtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsbtnCancel.Click
        btnCancel_Click(sender, e)
    End Sub

    'Private Sub btnOK_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnOK.MouseDown
    '    btnOK.BackgroundImage = Global.gloEMR.My.Resources.Resources.yellowbtn1
    '    btnOK.BackgroundImageLayout = ImageLayout.Stretch

    'End Sub

    'Private Sub btnOK_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.MouseEnter
    '    btnOK.BackgroundImage = Global.gloEMR.My.Resources.Resources.yellowbtn1
    '    btnOK.BackgroundImageLayout = ImageLayout.Stretch
    'End Sub

    'Private Sub btnOK_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.MouseLeave
    '    btnOK.BackgroundImage = Global.gloEMR.My.Resources.Resources.bg_image
    '    btnOK.BackgroundImageLayout = ImageLayout.Stretch
    'End Sub

    'Private Sub btnCancel_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnCancel.MouseDown
    '    btnCancel.BackgroundImage = Global.gloEMR.My.Resources.Resources.yellowbtn1
    '    btnCancel.BackgroundImageLayout = ImageLayout.Stretch
    'End Sub

    'Private Sub btnCancel_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.MouseEnter
    '    btnCancel.BackgroundImage = Global.gloEMR.My.Resources.Resources.yellowbtn1
    '    btnCancel.BackgroundImageLayout = ImageLayout.Stretch
    'End Sub

    'Private Sub btnCancel_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.MouseLeave
    '    btnCancel.BackgroundImage = Global.gloEMR.My.Resources.Resources.bg_image
    '    btnCancel.BackgroundImageLayout = ImageLayout.Stretch
    'End Sub

    'ADDED BY SHUBHANGI 20100617 TO ADD MULTIPLE EM FIELDS
    Private Sub btnAssociatedEMItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssociatedEMItem.Click

        Dim frmEMTag As New frmEMTagAssociation(_arrLabs, _arrOrders, _arrOtherDiag, _arrManagment, "Orders")
        frmEMTag.StartPosition = FormStartPosition.CenterScreen
        frmEMTag.ShowDialog(IIf(IsNothing(frmEMTag.Parent), Me, frmEMTag.Parent))
        _arrLabs = frmEMTag.arrLabs
        _arrOrders = frmEMTag.arrOrders
        _arrManagment = frmEMTag.arrManagment
        _arrOtherDiag = frmEMTag.arrOtherDiag
        frmEMTag.Dispose()

    End Sub


End Class
