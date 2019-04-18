<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDM_Infobutton
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDM_Infobutton))
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tlsPatientDM = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.c1DmProblem = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlDemographics = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblchkIncludeAge = New System.Windows.Forms.Label()
        Me.lblchkIncludeGender = New System.Windows.Forms.Label()
        Me.chkIncludeAge = New System.Windows.Forms.CheckBox()
        Me.cmbAudience = New System.Windows.Forms.ComboBox()
        Me.chkIncludeGender = New System.Windows.Forms.CheckBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlInfoLinks = New System.Windows.Forms.Panel()
        Me.trvLinks = New System.Windows.Forms.TreeView()
        Me.ImgList = New System.Windows.Forms.ImageList(Me.components)
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.c1InfoLinks = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnHideLinks = New System.Windows.Forms.Button()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.c1DmMedication = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.c1DmLab = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.cntAssociateExams = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuMedlineInfobutton = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPatientEducation = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuProviderReference = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlToolStrip.SuspendLayout()
        Me.tlsPatientDM.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        CType(Me.c1DmProblem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDemographics.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlInfoLinks.SuspendLayout()
        CType(Me.c1InfoLinks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.c1DmMedication, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.c1DmLab, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cntAssociateExams.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.tlsPatientDM)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(750, 54)
        Me.pnlToolStrip.TabIndex = 16
        '
        'tlsPatientDM
        '
        Me.tlsPatientDM.BackColor = System.Drawing.Color.Transparent
        Me.tlsPatientDM.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsPatientDM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsPatientDM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsPatientDM.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tlsPatientDM.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsPatientDM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnClose})
        Me.tlsPatientDM.Location = New System.Drawing.Point(0, 0)
        Me.tlsPatientDM.Name = "tlsPatientDM"
        Me.tlsPatientDM.Size = New System.Drawing.Size(750, 53)
        Me.tlsPatientDM.TabIndex = 0
        Me.tlsPatientDM.Text = "ToolStrip1"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlGrid
        '
        Me.pnlGrid.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlGrid.Controls.Add(Me.c1DmProblem)
        Me.pnlGrid.Controls.Add(Me.Label5)
        Me.pnlGrid.Controls.Add(Me.Label6)
        Me.pnlGrid.Controls.Add(Me.Label7)
        Me.pnlGrid.Controls.Add(Me.Label8)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlGrid.Location = New System.Drawing.Point(0, 101)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlGrid.Size = New System.Drawing.Size(750, 141)
        Me.pnlGrid.TabIndex = 17
        '
        'c1DmProblem
        '
        Me.c1DmProblem.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.Rows
        Me.c1DmProblem.BackColor = System.Drawing.Color.White
        Me.c1DmProblem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.c1DmProblem.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1DmProblem.ColumnInfo = resources.GetString("c1DmProblem.ColumnInfo")
        Me.c1DmProblem.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1DmProblem.DragMode = C1.Win.C1FlexGrid.DragModeEnum.AutomaticMove
        Me.c1DmProblem.ExtendLastCol = True
        Me.c1DmProblem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1DmProblem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1DmProblem.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.c1DmProblem.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.c1DmProblem.Location = New System.Drawing.Point(4, 1)
        Me.c1DmProblem.Name = "c1DmProblem"
        Me.c1DmProblem.Rows.Count = 1
        Me.c1DmProblem.Rows.DefaultSize = 21
        Me.c1DmProblem.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.RowRange
        Me.c1DmProblem.Size = New System.Drawing.Size(742, 136)
        Me.c1DmProblem.StyleInfo = resources.GetString("c1DmProblem.StyleInfo")
        Me.c1DmProblem.TabIndex = 25
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Location = New System.Drawing.Point(746, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 136)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Label5"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Location = New System.Drawing.Point(3, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 136)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Label6"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Location = New System.Drawing.Point(3, 137)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(744, 1)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Label7"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Location = New System.Drawing.Point(3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(744, 1)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Label8"
        '
        'pnlDemographics
        '
        Me.pnlDemographics.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlDemographics.Controls.Add(Me.Panel4)
        Me.pnlDemographics.Controls.Add(Me.Label4)
        Me.pnlDemographics.Controls.Add(Me.Label3)
        Me.pnlDemographics.Controls.Add(Me.Label2)
        Me.pnlDemographics.Controls.Add(Me.Label1)
        Me.pnlDemographics.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDemographics.Location = New System.Drawing.Point(0, 54)
        Me.pnlDemographics.Name = "pnlDemographics"
        Me.pnlDemographics.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlDemographics.Size = New System.Drawing.Size(750, 47)
        Me.pnlDemographics.TabIndex = 18
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.Panel4.Controls.Add(Me.lblchkIncludeAge)
        Me.Panel4.Controls.Add(Me.lblchkIncludeGender)
        Me.Panel4.Controls.Add(Me.chkIncludeAge)
        Me.Panel4.Controls.Add(Me.cmbAudience)
        Me.Panel4.Controls.Add(Me.chkIncludeGender)
        Me.Panel4.Controls.Add(Me.Label37)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(4, 4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(742, 39)
        Me.Panel4.TabIndex = 13
        '
        'lblchkIncludeAge
        '
        Me.lblchkIncludeAge.AutoSize = True
        Me.lblchkIncludeAge.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblchkIncludeAge.Font = New System.Drawing.Font("Tahoma", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblchkIncludeAge.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblchkIncludeAge.Location = New System.Drawing.Point(258, 10)
        Me.lblchkIncludeAge.Name = "lblchkIncludeAge"
        Me.lblchkIncludeAge.Size = New System.Drawing.Size(24, 16)
        Me.lblchkIncludeAge.TabIndex = 14
        Me.lblchkIncludeAge.Text = "35"
        '
        'lblchkIncludeGender
        '
        Me.lblchkIncludeGender.AutoSize = True
        Me.lblchkIncludeGender.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblchkIncludeGender.Font = New System.Drawing.Font("Tahoma", 9.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblchkIncludeGender.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblchkIncludeGender.Location = New System.Drawing.Point(102, 10)
        Me.lblchkIncludeGender.Name = "lblchkIncludeGender"
        Me.lblchkIncludeGender.Size = New System.Drawing.Size(38, 16)
        Me.lblchkIncludeGender.TabIndex = 13
        Me.lblchkIncludeGender.Text = "Male"
        '
        'chkIncludeAge
        '
        Me.chkIncludeAge.AutoSize = True
        Me.chkIncludeAge.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIncludeAge.Location = New System.Drawing.Point(207, 9)
        Me.chkIncludeAge.Name = "chkIncludeAge"
        Me.chkIncludeAge.Size = New System.Drawing.Size(58, 20)
        Me.chkIncludeAge.TabIndex = 1
        Me.chkIncludeAge.Text = "Age :"
        Me.chkIncludeAge.UseVisualStyleBackColor = True
        '
        'cmbAudience
        '
        Me.cmbAudience.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAudience.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAudience.FormattingEnabled = True
        Me.cmbAudience.Items.AddRange(New Object() {"Provider", "Patient"})
        Me.cmbAudience.Location = New System.Drawing.Point(603, 8)
        Me.cmbAudience.Name = "cmbAudience"
        Me.cmbAudience.Size = New System.Drawing.Size(127, 22)
        Me.cmbAudience.TabIndex = 12
        '
        'chkIncludeGender
        '
        Me.chkIncludeGender.AutoSize = True
        Me.chkIncludeGender.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIncludeGender.Location = New System.Drawing.Point(32, 9)
        Me.chkIncludeGender.Name = "chkIncludeGender"
        Me.chkIncludeGender.Size = New System.Drawing.Size(77, 20)
        Me.chkIncludeGender.TabIndex = 0
        Me.chkIncludeGender.Text = "Gender :"
        Me.chkIncludeGender.UseVisualStyleBackColor = True
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Location = New System.Drawing.Point(471, 12)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(131, 14)
        Me.Label37.TabIndex = 11
        Me.Label37.Text = "Information audience :"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(746, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 39)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 39)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Label3"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(3, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(744, 1)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Label2"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(744, 1)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Label1"
        '
        'pnlInfoLinks
        '
        Me.pnlInfoLinks.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlInfoLinks.Controls.Add(Me.trvLinks)
        Me.pnlInfoLinks.Controls.Add(Me.Label28)
        Me.pnlInfoLinks.Controls.Add(Me.Label27)
        Me.pnlInfoLinks.Controls.Add(Me.c1InfoLinks)
        Me.pnlInfoLinks.Controls.Add(Me.Panel1)
        Me.pnlInfoLinks.Controls.Add(Me.Label9)
        Me.pnlInfoLinks.Controls.Add(Me.Label10)
        Me.pnlInfoLinks.Controls.Add(Me.Label11)
        Me.pnlInfoLinks.Controls.Add(Me.Label12)
        Me.pnlInfoLinks.Location = New System.Drawing.Point(66, 165)
        Me.pnlInfoLinks.Name = "pnlInfoLinks"
        Me.pnlInfoLinks.Size = New System.Drawing.Size(600, 264)
        Me.pnlInfoLinks.TabIndex = 24
        Me.pnlInfoLinks.Visible = False
        '
        'trvLinks
        '
        Me.trvLinks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvLinks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvLinks.ImageIndex = 0
        Me.trvLinks.ImageList = Me.ImgList
        Me.trvLinks.Indent = 20
        Me.trvLinks.ItemHeight = 20
        Me.trvLinks.Location = New System.Drawing.Point(9, 37)
        Me.trvLinks.Name = "trvLinks"
        Me.trvLinks.SelectedImageIndex = 1
        Me.trvLinks.ShowLines = False
        Me.trvLinks.ShowPlusMinus = False
        Me.trvLinks.ShowRootLines = False
        Me.trvLinks.Size = New System.Drawing.Size(590, 226)
        Me.trvLinks.StateImageList = Me.ImgList
        Me.trvLinks.TabIndex = 25
        '
        'ImgList
        '
        Me.ImgList.ImageStream = CType(resources.GetObject("ImgList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgList.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgList.Images.SetKeyName(0, "Bullet06.ico")
        Me.ImgList.Images.SetKeyName(1, "BulletOrangeDiamond.ico")
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.White
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label28.Location = New System.Drawing.Point(9, 29)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(590, 8)
        Me.Label28.TabIndex = 27
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.White
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label27.Location = New System.Drawing.Point(1, 29)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(8, 234)
        Me.Label27.TabIndex = 26
        '
        'c1InfoLinks
        '
        Me.c1InfoLinks.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.Rows
        Me.c1InfoLinks.BackColor = System.Drawing.Color.White
        Me.c1InfoLinks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.c1InfoLinks.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1InfoLinks.ColumnInfo = "1,0,0,0,0,105,Columns:0{Width:250;AllowSorting:False;Name:""colInks"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.c1InfoLinks.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1InfoLinks.DragMode = C1.Win.C1FlexGrid.DragModeEnum.AutomaticMove
        Me.c1InfoLinks.ExtendLastCol = True
        Me.c1InfoLinks.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1InfoLinks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1InfoLinks.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.c1InfoLinks.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.c1InfoLinks.Location = New System.Drawing.Point(1, 29)
        Me.c1InfoLinks.Name = "c1InfoLinks"
        Me.c1InfoLinks.Padding = New System.Windows.Forms.Padding(3)
        Me.c1InfoLinks.Rows.Count = 13
        Me.c1InfoLinks.Rows.DefaultSize = 21
        Me.c1InfoLinks.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1InfoLinks.Size = New System.Drawing.Size(598, 234)
        Me.c1InfoLinks.StyleInfo = resources.GetString("c1InfoLinks.StyleInfo")
        Me.c1InfoLinks.TabIndex = 23
        Me.c1InfoLinks.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button1
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.btnHideLinks)
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(1, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(598, 28)
        Me.Panel1.TabIndex = 24
        '
        'btnHideLinks
        '
        Me.btnHideLinks.BackColor = System.Drawing.Color.Transparent
        Me.btnHideLinks.BackgroundImage = CType(resources.GetObject("btnHideLinks.BackgroundImage"), System.Drawing.Image)
        Me.btnHideLinks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnHideLinks.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnHideLinks.FlatAppearance.BorderSize = 0
        Me.btnHideLinks.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnHideLinks.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnHideLinks.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHideLinks.Location = New System.Drawing.Point(566, 0)
        Me.btnHideLinks.Margin = New System.Windows.Forms.Padding(0)
        Me.btnHideLinks.Name = "btnHideLinks"
        Me.btnHideLinks.Size = New System.Drawing.Size(32, 27)
        Me.btnHideLinks.TabIndex = 0
        Me.btnHideLinks.UseVisualStyleBackColor = False
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Location = New System.Drawing.Point(0, 27)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(598, 1)
        Me.Label22.TabIndex = 3
        Me.Label22.Text = "Label22"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(6, 7)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(87, 14)
        Me.Label13.TabIndex = 1
        Me.Label13.Text = "Browse Links"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Location = New System.Drawing.Point(599, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 262)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "Label9"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Location = New System.Drawing.Point(0, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 262)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "Label10"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Location = New System.Drawing.Point(0, 263)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(600, 1)
        Me.Label11.TabIndex = 3
        Me.Label11.Text = "Label11"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(600, 1)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Label12"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.Panel2.Controls.Add(Me.c1DmMedication)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 242)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel2.Size = New System.Drawing.Size(750, 150)
        Me.Panel2.TabIndex = 25
        '
        'c1DmMedication
        '
        Me.c1DmMedication.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.Rows
        Me.c1DmMedication.BackColor = System.Drawing.Color.White
        Me.c1DmMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.c1DmMedication.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1DmMedication.ColumnInfo = resources.GetString("c1DmMedication.ColumnInfo")
        Me.c1DmMedication.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1DmMedication.DragMode = C1.Win.C1FlexGrid.DragModeEnum.AutomaticMove
        Me.c1DmMedication.ExtendLastCol = True
        Me.c1DmMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1DmMedication.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1DmMedication.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.c1DmMedication.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.c1DmMedication.Location = New System.Drawing.Point(4, 1)
        Me.c1DmMedication.Name = "c1DmMedication"
        Me.c1DmMedication.Rows.Count = 1
        Me.c1DmMedication.Rows.DefaultSize = 21
        Me.c1DmMedication.Size = New System.Drawing.Size(742, 145)
        Me.c1DmMedication.StyleInfo = resources.GetString("c1DmMedication.StyleInfo")
        Me.c1DmMedication.TabIndex = 24
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Location = New System.Drawing.Point(746, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 145)
        Me.Label14.TabIndex = 5
        Me.Label14.Text = "Label14"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Location = New System.Drawing.Point(3, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 145)
        Me.Label15.TabIndex = 4
        Me.Label15.Text = "Label15"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label16.Location = New System.Drawing.Point(3, 146)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(744, 1)
        Me.Label16.TabIndex = 3
        Me.Label16.Text = "Label16"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Location = New System.Drawing.Point(3, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(744, 1)
        Me.Label17.TabIndex = 2
        Me.Label17.Text = "Label17"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.Panel3.Controls.Add(Me.c1DmLab)
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.Label19)
        Me.Panel3.Controls.Add(Me.Label20)
        Me.Panel3.Controls.Add(Me.Label21)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 392)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel3.Size = New System.Drawing.Size(750, 161)
        Me.Panel3.TabIndex = 26
        '
        'c1DmLab
        '
        Me.c1DmLab.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.Rows
        Me.c1DmLab.BackColor = System.Drawing.Color.White
        Me.c1DmLab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.c1DmLab.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1DmLab.ColumnInfo = resources.GetString("c1DmLab.ColumnInfo")
        Me.c1DmLab.Dock = System.Windows.Forms.DockStyle.Top
        Me.c1DmLab.DragMode = C1.Win.C1FlexGrid.DragModeEnum.AutomaticMove
        Me.c1DmLab.ExtendLastCol = True
        Me.c1DmLab.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1DmLab.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1DmLab.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.c1DmLab.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.c1DmLab.Location = New System.Drawing.Point(4, 1)
        Me.c1DmLab.Name = "c1DmLab"
        Me.c1DmLab.Rows.Count = 1
        Me.c1DmLab.Rows.DefaultSize = 21
        Me.c1DmLab.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1DmLab.Size = New System.Drawing.Size(742, 155)
        Me.c1DmLab.StyleInfo = resources.GetString("c1DmLab.StyleInfo")
        Me.c1DmLab.TabIndex = 24
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Location = New System.Drawing.Point(746, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 156)
        Me.Label18.TabIndex = 5
        Me.Label18.Text = "Label18"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Location = New System.Drawing.Point(3, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 156)
        Me.Label19.TabIndex = 4
        Me.Label19.Text = "Label19"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label20.Location = New System.Drawing.Point(3, 157)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(744, 1)
        Me.Label20.TabIndex = 3
        Me.Label20.Text = "Label20"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label21.Location = New System.Drawing.Point(3, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(744, 1)
        Me.Label21.TabIndex = 2
        Me.Label21.Text = "Label21"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'cntAssociateExams
        '
        Me.cntAssociateExams.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMedlineInfobutton, Me.mnuPatientEducation, Me.mnuProviderReference})
        Me.cntAssociateExams.Name = "cntAssociateExams"
        Me.cntAssociateExams.Size = New System.Drawing.Size(220, 70)
        '
        'mnuMedlineInfobutton
        '
        Me.mnuMedlineInfobutton.Image = Global.gloEMR.My.Resources.Resources.infobutton
        Me.mnuMedlineInfobutton.Name = "mnuMedlineInfobutton"
        Me.mnuMedlineInfobutton.Size = New System.Drawing.Size(219, 22)
        Me.mnuMedlineInfobutton.Text = "Infobutton"
        '
        'mnuPatientEducation
        '
        Me.mnuPatientEducation.Image = Global.gloEMR.My.Resources.Resources.Patient_reference_material_img
        Me.mnuPatientEducation.Name = "mnuPatientEducation"
        Me.mnuPatientEducation.Size = New System.Drawing.Size(219, 22)
        Me.mnuPatientEducation.Text = "Patient Reference Material"
        '
        'mnuProviderReference
        '
        Me.mnuProviderReference.Image = Global.gloEMR.My.Resources.Resources.Provider_reference_material_img
        Me.mnuProviderReference.Name = "mnuProviderReference"
        Me.mnuProviderReference.Size = New System.Drawing.Size(219, 22)
        Me.mnuProviderReference.Text = "Provider Reference Material"
        '
        'frmDM_Infobutton
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(750, 553)
        Me.Controls.Add(Me.pnlInfoLinks)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlGrid)
        Me.Controls.Add(Me.pnlDemographics)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmDM_Infobutton"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Infobutton Documents"
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tlsPatientDM.ResumeLayout(False)
        Me.tlsPatientDM.PerformLayout()
        Me.pnlGrid.ResumeLayout(False)
        CType(Me.c1DmProblem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDemographics.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.pnlInfoLinks.ResumeLayout(False)
        CType(Me.c1InfoLinks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.c1DmMedication, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        CType(Me.c1DmLab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cntAssociateExams.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents tlsPatientDM As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlGrid As System.Windows.Forms.Panel
    Friend WithEvents pnlDemographics As System.Windows.Forms.Panel
    Friend WithEvents chkIncludeAge As System.Windows.Forms.CheckBox
    Friend WithEvents chkIncludeGender As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlInfoLinks As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents c1InfoLinks As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnHideLinks As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbAudience As System.Windows.Forms.ComboBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents c1DmMedication As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents c1DmLab As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents c1DmProblem As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents lblchkIncludeAge As System.Windows.Forms.Label
    Friend WithEvents lblchkIncludeGender As System.Windows.Forms.Label
    Friend WithEvents trvLinks As System.Windows.Forms.TreeView
    Friend WithEvents ImgList As System.Windows.Forms.ImageList
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents cntAssociateExams As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuMedlineInfobutton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPatientEducation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuProviderReference As System.Windows.Forms.ToolStripMenuItem
End Class
