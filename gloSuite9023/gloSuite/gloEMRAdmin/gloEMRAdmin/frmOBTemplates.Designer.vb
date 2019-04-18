<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOBTemplates
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOBTemplates))
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.tlb_OBTemplates = New System.Windows.Forms.ToolStrip()
        Me.tlbbtn_SelectAll = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_DeselectAll = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtn_SavenClose = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GloUC_trvTemplate = New gloEMRAdmin.gloUC_TreeView()
        Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.trvTemplate = New System.Windows.Forms.TreeView()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel3.SuspendLayout()
        Me.tlb_OBTemplates.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.tlb_OBTemplates)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(607, 54)
        Me.Panel3.TabIndex = 113
        '
        'tlb_OBTemplates
        '
        Me.tlb_OBTemplates.BackColor = System.Drawing.Color.Transparent
        Me.tlb_OBTemplates.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlb_OBTemplates.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_OBTemplates.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlb_OBTemplates.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtn_SelectAll, Me.tlbbtn_DeselectAll, Me.tlsbtn_SavenClose, Me.tlbbtn_Close})
        Me.tlb_OBTemplates.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlb_OBTemplates.Location = New System.Drawing.Point(0, 0)
        Me.tlb_OBTemplates.Name = "tlb_OBTemplates"
        Me.tlb_OBTemplates.Size = New System.Drawing.Size(607, 53)
        Me.tlb_OBTemplates.TabIndex = 1
        Me.tlb_OBTemplates.Text = "toolStrip1"
        '
        'tlbbtn_SelectAll
        '
        Me.tlbbtn_SelectAll.Image = CType(resources.GetObject("tlbbtn_SelectAll.Image"), System.Drawing.Image)
        Me.tlbbtn_SelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_SelectAll.Name = "tlbbtn_SelectAll"
        Me.tlbbtn_SelectAll.Size = New System.Drawing.Size(67, 50)
        Me.tlbbtn_SelectAll.Tag = "SelectAll"
        Me.tlbbtn_SelectAll.Text = "Select &All"
        Me.tlbbtn_SelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_DeselectAll
        '
        Me.tlbbtn_DeselectAll.Image = CType(resources.GetObject("tlbbtn_DeselectAll.Image"), System.Drawing.Image)
        Me.tlbbtn_DeselectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_DeselectAll.Name = "tlbbtn_DeselectAll"
        Me.tlbbtn_DeselectAll.Size = New System.Drawing.Size(88, 50)
        Me.tlbbtn_DeselectAll.Tag = "DeselectAll"
        Me.tlbbtn_DeselectAll.Text = "&De-Select All"
        Me.tlbbtn_DeselectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsbtn_SavenClose
        '
        Me.tlsbtn_SavenClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtn_SavenClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsbtn_SavenClose.Image = CType(resources.GetObject("tlsbtn_SavenClose.Image"), System.Drawing.Image)
        Me.tlsbtn_SavenClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtn_SavenClose.Name = "tlsbtn_SavenClose"
        Me.tlsbtn_SavenClose.Size = New System.Drawing.Size(66, 50)
        Me.tlsbtn_SavenClose.Tag = "SavenClose"
        Me.tlsbtn_SavenClose.Text = "&Save&&Cls"
        Me.tlsbtn_SavenClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtn_SavenClose.ToolTipText = "Save and Close"
        '
        'tlbbtn_Close
        '
        Me.tlbbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Close.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_Close.Image = CType(resources.GetObject("tlbbtn_Close.Image"), System.Drawing.Image)
        Me.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Close.Name = "tlbbtn_Close"
        Me.tlbbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tlbbtn_Close.Tag = "Close"
        Me.tlbbtn_Close.Text = "&Close"
        Me.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GloUC_trvTemplate)
        Me.Panel1.Controls.Add(Me.trvTemplate)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 81)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(607, 389)
        Me.Panel1.TabIndex = 115
        '
        'GloUC_trvTemplate
        '
        Me.GloUC_trvTemplate.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvTemplate.CheckBoxes = True
        Me.GloUC_trvTemplate.CodeMember = Nothing
        Me.GloUC_trvTemplate.ColonAsSeparator = False
        Me.GloUC_trvTemplate.Comment = Nothing
        Me.GloUC_trvTemplate.ConceptID = Nothing
        Me.GloUC_trvTemplate.CPT = Nothing
        Me.GloUC_trvTemplate.DDIDMember = Nothing
        Me.GloUC_trvTemplate.DescriptionMember = Nothing
        Me.GloUC_trvTemplate.DisplayType = gloEMRAdmin.gloUC_TreeView.enumDisplayType.Descripation
        Me.GloUC_trvTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvTemplate.DrugFlag = CType(16, Short)
        Me.GloUC_trvTemplate.DrugFormMember = Nothing
        Me.GloUC_trvTemplate.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvTemplate.DurationMember = Nothing
        Me.GloUC_trvTemplate.EducationMappingSearchType = 1
        Me.GloUC_trvTemplate.FrequencyMember = Nothing
        Me.GloUC_trvTemplate.HistoryType = Nothing
        Me.GloUC_trvTemplate.ICD9 = Nothing
        Me.GloUC_trvTemplate.ICDRevision = Nothing
        Me.GloUC_trvTemplate.ImageIndex = 0
        Me.GloUC_trvTemplate.ImageList = Me.imageList1
        Me.GloUC_trvTemplate.ImageObject = Nothing
        Me.GloUC_trvTemplate.Indicator = Nothing
        Me.GloUC_trvTemplate.IsCPTSearch = False
        Me.GloUC_trvTemplate.IsDiagnosisSearch = False
        Me.GloUC_trvTemplate.IsDrug = False
        Me.GloUC_trvTemplate.IsNarcoticsMember = Nothing
        Me.GloUC_trvTemplate.IsSearchForEducationMapping = False
        Me.GloUC_trvTemplate.IsSystemCategory = Nothing
        Me.GloUC_trvTemplate.Location = New System.Drawing.Point(4, 1)
        Me.GloUC_trvTemplate.MaximumNodes = 1000
        Me.GloUC_trvTemplate.Name = "GloUC_trvTemplate"
        Me.GloUC_trvTemplate.NDCCodeMember = Nothing
        Me.GloUC_trvTemplate.ParentImageIndex = 0
        Me.GloUC_trvTemplate.ParentMember = Nothing
        Me.GloUC_trvTemplate.RouteMember = Nothing
        Me.GloUC_trvTemplate.Search = gloEMRAdmin.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvTemplate.SearchBox = True
        Me.GloUC_trvTemplate.SearchText = Nothing
        Me.GloUC_trvTemplate.SelectedImageIndex = 0
        Me.GloUC_trvTemplate.SelectedNode = Nothing
        Me.GloUC_trvTemplate.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvTemplate.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvTemplate.SelectedParentImageIndex = 0
        Me.GloUC_trvTemplate.Size = New System.Drawing.Size(599, 384)
        Me.GloUC_trvTemplate.SmartTreatmentId = Nothing
        Me.GloUC_trvTemplate.Sort = gloEMRAdmin.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvTemplate.TabIndex = 118
        Me.GloUC_trvTemplate.Tag = Nothing
        Me.GloUC_trvTemplate.UnitMember = Nothing
        Me.GloUC_trvTemplate.ValueMember = Nothing
        '
        'imageList1
        '
        Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.imageList1.Images.SetKeyName(0, "Bullet06.ico")
        Me.imageList1.Images.SetKeyName(1, "Tempate Category.ico")
        '
        'trvTemplate
        '
        Me.trvTemplate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvTemplate.CheckBoxes = True
        Me.trvTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvTemplate.ImageIndex = 0
        Me.trvTemplate.ImageList = Me.imageList1
        Me.trvTemplate.Location = New System.Drawing.Point(4, 1)
        Me.trvTemplate.Name = "trvTemplate"
        Me.trvTemplate.SelectedImageIndex = 0
        Me.trvTemplate.Size = New System.Drawing.Size(599, 384)
        Me.trvTemplate.TabIndex = 117
        Me.trvTemplate.Visible = False
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Location = New System.Drawing.Point(603, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 384)
        Me.Label12.TabIndex = 116
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Location = New System.Drawing.Point(3, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 384)
        Me.Label11.TabIndex = 115
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Location = New System.Drawing.Point(3, 385)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(601, 1)
        Me.Label9.TabIndex = 114
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Location = New System.Drawing.Point(3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(601, 1)
        Me.Label8.TabIndex = 113
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Controls.Add(Me.Label4)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 54)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel4.Size = New System.Drawing.Size(607, 27)
        Me.Panel4.TabIndex = 119
        Me.Panel4.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackgroundImage = CType(resources.GetObject("Panel5.BackgroundImage"), System.Drawing.Image)
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.Button1)
        Me.Panel5.Controls.Add(Me.txtSearch)
        Me.Panel5.Controls.Add(Me.Label1)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(4, 1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(599, 22)
        Me.Panel5.TabIndex = 118
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Transparent
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(264, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(33, 22)
        Me.Button1.TabIndex = 2
        Me.Button1.UseVisualStyleBackColor = False
        Me.Button1.Visible = False
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSearch.Location = New System.Drawing.Point(75, 0)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(189, 22)
        Me.txtSearch.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 22)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Search :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Location = New System.Drawing.Point(603, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 22)
        Me.Label2.TabIndex = 117
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(3, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 22)
        Me.Label3.TabIndex = 116
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Location = New System.Drawing.Point(3, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(601, 1)
        Me.Label4.TabIndex = 113
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(601, 1)
        Me.Label5.TabIndex = 112
        '
        'frmOBTemplates
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(607, 470)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOBTemplates"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "OB Templates"
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.tlb_OBTemplates.ResumeLayout(False)
        Me.tlb_OBTemplates.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents tlb_OBTemplates As System.Windows.Forms.ToolStrip
    Friend WithEvents tlbbtn_SelectAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_DeselectAll As System.Windows.Forms.ToolStripButton
    Private WithEvents tlsbtn_SavenClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents trvTemplate As System.Windows.Forms.TreeView
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents imageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GloUC_trvTemplate As gloUC_TreeView
End Class
