<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Diagnosis
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Try
                If (IsNothing(ContextMenuDiagnosis) = False) Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(ContextMenuDiagnosis)
                    If (IsNothing(ContextMenuDiagnosis.Items) = False) Then
                        ContextMenuDiagnosis.Items.Clear()
                    End If
                    ContextMenuDiagnosis.Dispose()
                    ContextMenuDiagnosis = Nothing
                End If
            Catch

            End Try


            components.Dispose()
        End If
        If disposing AndAlso GloUC_trvICD IsNot Nothing Then
            GloUC_trvICD.Dispose()
        End If
        If disposing AndAlso GloUC_trvAssociates IsNot Nothing Then
            GloUC_trvAssociates.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Diagnosis))
        Me.tblNew = New System.Windows.Forms.ToolStripButton()
        Me.tblsave = New System.Windows.Forms.ToolStripButton()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.tblFinish = New System.Windows.Forms.ToolStripButton()
        Me.pnlleft = New System.Windows.Forms.Panel()
        Me.GloUC_trvICD = New gloUserControlLibrary.gloUC_TreeView()
        Me.imglistTrvICD9 = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.trICD9 = New System.Windows.Forms.TreeView()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtsearch = New System.Windows.Forms.TextBox()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.PicBx_Search = New System.Windows.Forms.PictureBox()
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.pnl_radiolft = New System.Windows.Forms.Panel()
        Me.pnlradiolft = New System.Windows.Forms.Panel()
        Me.RbICD10 = New System.Windows.Forms.RadioButton()
        Me.RbICD9 = New System.Windows.Forms.RadioButton()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.pnlrht = New System.Windows.Forms.Panel()
        Me.pnlflexgrid = New System.Windows.Forms.Panel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.C1Dignosis = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.splpnlElementHost = New System.Windows.Forms.Splitter()
        Me.pnlElementHost = New System.Windows.Forms.Panel()
        Me.elementHost = New System.Windows.Forms.Integration.ElementHost()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.pnlUpdown = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnUP = New System.Windows.Forms.Button()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.pnltrvrht = New System.Windows.Forms.Panel()
        Me.GloUC_trvAssociates = New gloUserControlLibrary.gloUC_TreeView()
        Me.imglistTrvCPT = New System.Windows.Forms.ImageList(Me.components)
        Me.panel9 = New System.Windows.Forms.Panel()
        Me.trvCPT = New System.Windows.Forms.TreeView()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.label21 = New System.Windows.Forms.Label()
        Me.label22 = New System.Windows.Forms.Label()
        Me.label23 = New System.Windows.Forms.Label()
        Me.label24 = New System.Windows.Forms.Label()
        Me.pnl_btnCPT = New System.Windows.Forms.Panel()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.btnCPT = New System.Windows.Forms.Button()
        Me.pnl_btnmodifier = New System.Windows.Forms.Panel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.btnmodifier = New System.Windows.Forms.Button()
        Me.pnl_txtsearchrht = New System.Windows.Forms.Panel()
        Me.txtsearchrht = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.pnlradiorht = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.rdoDescriptionrht = New System.Windows.Forms.RadioButton()
        Me.rdocode = New System.Windows.Forms.RadioButton()
        Me.imglistTrvModifier = New System.Windows.Forms.ImageList(Me.components)
        Me.ContextMenuDiagnosis = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuRemoveAllDiagnosis = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRemoveSelectedDiagnosis = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAddICD9 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAddCPT = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAddModifier = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEditICD9 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEditCPT = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEditModifier = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAssociateCPTWithAllICD9 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAssociateCPTWithAllUnassociatedICD9 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSetAsPrimary = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAssociateAllCPTtoAllICD9 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAssociateAllCPTToAllUnassociatedICD9 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRemoveAllModifierforSelectedCPT = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAssociateModifierWithAllCPT = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAddICD10 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEditICD10 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.tstripDiagnosis = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlsbtnNew = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnFinish = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnSave = New System.Windows.Forms.ToolStripButton()
        Me.tlsShowICD10Codes = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnCodingRule = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnClose = New System.Windows.Forms.ToolStripButton()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.pnlleft.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_radiolft.SuspendLayout()
        Me.pnlradiolft.SuspendLayout()
        Me.pnlrht.SuspendLayout()
        Me.pnlflexgrid.SuspendLayout()
        CType(Me.C1Dignosis, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlElementHost.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.pnlUpdown.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnltrvrht.SuspendLayout()
        Me.panel9.SuspendLayout()
        Me.pnl_btnCPT.SuspendLayout()
        Me.pnl_btnmodifier.SuspendLayout()
        Me.pnl_txtsearchrht.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlradiorht.SuspendLayout()
        Me.ContextMenuDiagnosis.SuspendLayout()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstripDiagnosis.SuspendLayout()
        Me.SuspendLayout()
        '
        'tblNew
        '
        Me.tblNew.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblNew.Image = CType(resources.GetObject("tblNew.Image"), System.Drawing.Image)
        Me.tblNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblNew.Name = "tblNew"
        Me.tblNew.Size = New System.Drawing.Size(57, 50)
        Me.tblNew.Text = "  &New  "
        Me.tblNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblsave
        '
        Me.tblsave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblsave.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblsave.Image = CType(resources.GetObject("tblsave.Image"), System.Drawing.Image)
        Me.tblsave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblsave.Name = "tblsave"
        Me.tblsave.Size = New System.Drawing.Size(60, 50)
        Me.tblsave.Text = "  &Save  "
        Me.tblsave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblClose
        '
        Me.tblClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblClose.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblClose.Image = CType(resources.GetObject("tblClose.Image"), System.Drawing.Image)
        Me.tblClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblClose.Name = "tblClose"
        Me.tblClose.Size = New System.Drawing.Size(63, 50)
        Me.tblClose.Text = "  &Close  "
        Me.tblClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblFinish
        '
        Me.tblFinish.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblFinish.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblFinish.Image = CType(resources.GetObject("tblFinish.Image"), System.Drawing.Image)
        Me.tblFinish.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblFinish.Name = "tblFinish"
        Me.tblFinish.Size = New System.Drawing.Size(66, 50)
        Me.tblFinish.Text = "  &Finish  "
        Me.tblFinish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlleft
        '
        Me.pnlleft.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlleft.Controls.Add(Me.GloUC_trvICD)
        Me.pnlleft.Controls.Add(Me.Panel2)
        Me.pnlleft.Controls.Add(Me.pnlSearch)
        Me.pnlleft.Controls.Add(Me.pnl_radiolft)
        Me.pnlleft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlleft.Location = New System.Drawing.Point(0, 53)
        Me.pnlleft.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlleft.Name = "pnlleft"
        Me.pnlleft.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlleft.Size = New System.Drawing.Size(533, 597)
        Me.pnlleft.TabIndex = 2
        '
        'GloUC_trvICD
        '
        Me.GloUC_trvICD.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvICD.CheckBoxes = False
        Me.GloUC_trvICD.CodeMember = Nothing
        Me.GloUC_trvICD.ColonAsSeparator = False
        Me.GloUC_trvICD.Comment = Nothing
        Me.GloUC_trvICD.ConceptID = Nothing
        Me.GloUC_trvICD.CPT = Nothing
        Me.GloUC_trvICD.DDIDMember = Nothing
        Me.GloUC_trvICD.DescriptionMember = Nothing
        Me.GloUC_trvICD.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvICD.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
        Me.GloUC_trvICD.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvICD.DrugFlag = CType(16, Short)
        Me.GloUC_trvICD.DrugFormMember = Nothing
        Me.GloUC_trvICD.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvICD.DurationMember = Nothing
        Me.GloUC_trvICD.EducationMappingSearchType = 1
        Me.GloUC_trvICD.FrequencyMember = Nothing
        Me.GloUC_trvICD.HistoryType = Nothing
        Me.GloUC_trvICD.ICD9 = Nothing
        Me.GloUC_trvICD.ICDRevision = Nothing
        Me.GloUC_trvICD.ImageIndex = 0
        Me.GloUC_trvICD.ImageList = Me.imglistTrvICD9
        Me.GloUC_trvICD.ImageObject = Nothing
        Me.GloUC_trvICD.Indicator = Nothing
        Me.GloUC_trvICD.IsCPTSearch = False
        Me.GloUC_trvICD.IsDiagnosisSearch = False
        Me.GloUC_trvICD.IsDrug = False
        Me.GloUC_trvICD.IsNarcoticsMember = Nothing
        Me.GloUC_trvICD.IsSearchForEducationMapping = False
        Me.GloUC_trvICD.IsSystemCategory = Nothing
        Me.GloUC_trvICD.Location = New System.Drawing.Point(0, 31)
        Me.GloUC_trvICD.MaximumNodes = 1000
        Me.GloUC_trvICD.Name = "GloUC_trvICD"
        Me.GloUC_trvICD.NDCCodeMember = Nothing
        Me.GloUC_trvICD.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.GloUC_trvICD.ParentImageIndex = 0
        Me.GloUC_trvICD.ParentMember = Nothing
        Me.GloUC_trvICD.RouteMember = Nothing
        Me.GloUC_trvICD.RowOrderMember = Nothing
        Me.GloUC_trvICD.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvICD.SearchBox = True
        Me.GloUC_trvICD.SearchText = Nothing
        Me.GloUC_trvICD.SelectedImageIndex = 0
        Me.GloUC_trvICD.SelectedNode = Nothing
        Me.GloUC_trvICD.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvICD.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvICD.SelectedParentImageIndex = 0
        Me.GloUC_trvICD.Size = New System.Drawing.Size(533, 566)
        Me.GloUC_trvICD.SmartTreatmentId = Nothing
        Me.GloUC_trvICD.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvICD.TabIndex = 17
        Me.GloUC_trvICD.Tag = Nothing
        Me.GloUC_trvICD.UnitMember = Nothing
        Me.GloUC_trvICD.ValueMember = Nothing
        '
        'imglistTrvICD9
        '
        Me.imglistTrvICD9.ImageStream = CType(resources.GetObject("imglistTrvICD9.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imglistTrvICD9.TransparentColor = System.Drawing.Color.Transparent
        Me.imglistTrvICD9.Images.SetKeyName(0, "ICD 09.ico")
        Me.imglistTrvICD9.Images.SetKeyName(1, "Bullet06.ico")
        Me.imglistTrvICD9.Images.SetKeyName(2, "Small Arrow.ico")
        Me.imglistTrvICD9.Images.SetKeyName(3, "Arrow03.ico")
        Me.imglistTrvICD9.Images.SetKeyName(4, "Square01.ico")
        Me.imglistTrvICD9.Images.SetKeyName(5, "")
        Me.imglistTrvICD9.Images.SetKeyName(6, "")
        Me.imglistTrvICD9.Images.SetKeyName(7, "")
        Me.imglistTrvICD9.Images.SetKeyName(8, "90 Degree Arrow.ico")
        Me.imglistTrvICD9.Images.SetKeyName(9, "90 Degree BlueArrow.ico")
        Me.imglistTrvICD9.Images.SetKeyName(10, "ICD10GalleryGreen.ico")
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.trICD9)
        Me.Panel2.Controls.Add(Me.Label33)
        Me.Panel2.Controls.Add(Me.Label31)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Location = New System.Drawing.Point(51, 467)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3, 1, 1, 3)
        Me.Panel2.Size = New System.Drawing.Size(110, 98)
        Me.Panel2.TabIndex = 6
        '
        'trICD9
        '
        Me.trICD9.BackColor = System.Drawing.Color.White
        Me.trICD9.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trICD9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trICD9.ForeColor = System.Drawing.Color.Black
        Me.trICD9.HideSelection = False
        Me.trICD9.ImageIndex = 0
        Me.trICD9.ImageList = Me.imglistTrvICD9
        Me.trICD9.Indent = 20
        Me.trICD9.ItemHeight = 20
        Me.trICD9.Location = New System.Drawing.Point(8, 6)
        Me.trICD9.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.trICD9.Name = "trICD9"
        Me.trICD9.SelectedImageIndex = 0
        Me.trICD9.ShowLines = False
        Me.trICD9.ShowPlusMinus = False
        Me.trICD9.Size = New System.Drawing.Size(100, 88)
        Me.trICD9.TabIndex = 0
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.White
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label33.Location = New System.Drawing.Point(4, 6)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(4, 88)
        Me.Label33.TabIndex = 39
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.White
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label31.Location = New System.Drawing.Point(4, 2)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(104, 4)
        Me.Label31.TabIndex = 38
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 94)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 2)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 93)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(108, 2)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 93)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(106, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearch.Controls.Add(Me.txtsearch)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlSearch.Controls.Add(Me.PicBx_Search)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchBottomBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchTopBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(51, 420)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3, 1, 1, 3)
        Me.pnlSearch.Size = New System.Drawing.Size(108, 27)
        Me.pnlSearch.TabIndex = 16
        '
        'txtsearch
        '
        Me.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearch.ForeColor = System.Drawing.Color.Black
        Me.txtsearch.Location = New System.Drawing.Point(32, 6)
        Me.txtsearch.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.txtsearch.Name = "txtsearch"
        Me.txtsearch.Size = New System.Drawing.Size(74, 15)
        Me.txtsearch.TabIndex = 0
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(32, 2)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(74, 4)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(32, 21)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(74, 2)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'PicBx_Search
        '
        Me.PicBx_Search.BackColor = System.Drawing.Color.White
        Me.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicBx_Search.Image = CType(resources.GetObject("PicBx_Search.Image"), System.Drawing.Image)
        Me.PicBx_Search.Location = New System.Drawing.Point(4, 2)
        Me.PicBx_Search.Name = "PicBx_Search"
        Me.PicBx_Search.Size = New System.Drawing.Size(28, 21)
        Me.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicBx_Search.TabIndex = 9
        Me.PicBx_Search.TabStop = False
        '
        'lbl_pnlSearchBottomBrd
        '
        Me.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlSearchBottomBrd.Location = New System.Drawing.Point(4, 23)
        Me.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd"
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(102, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(4, 1)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(102, 1)
        Me.lbl_pnlSearchTopBrd.TabIndex = 36
        Me.lbl_pnlSearchTopBrd.Text = "label1"
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(3, 1)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(106, 1)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'pnl_radiolft
        '
        Me.pnl_radiolft.Controls.Add(Me.pnlradiolft)
        Me.pnl_radiolft.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_radiolft.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_radiolft.Location = New System.Drawing.Point(0, 3)
        Me.pnl_radiolft.Name = "pnl_radiolft"
        Me.pnl_radiolft.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnl_radiolft.Size = New System.Drawing.Size(533, 28)
        Me.pnl_radiolft.TabIndex = 5
        '
        'pnlradiolft
        '
        Me.pnlradiolft.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlradiolft.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.Img_LongOrange
        Me.pnlradiolft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlradiolft.Controls.Add(Me.RbICD10)
        Me.pnlradiolft.Controls.Add(Me.RbICD9)
        Me.pnlradiolft.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlradiolft.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlradiolft.Controls.Add(Me.lbl_RightBrd)
        Me.pnlradiolft.Controls.Add(Me.lbl_TopBrd)
        Me.pnlradiolft.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlradiolft.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlradiolft.Location = New System.Drawing.Point(3, 0)
        Me.pnlradiolft.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlradiolft.Name = "pnlradiolft"
        Me.pnlradiolft.Size = New System.Drawing.Size(530, 25)
        Me.pnlradiolft.TabIndex = 4
        '
        'RbICD10
        '
        Me.RbICD10.AutoSize = True
        Me.RbICD10.BackColor = System.Drawing.Color.Transparent
        Me.RbICD10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbICD10.Location = New System.Drawing.Point(118, 4)
        Me.RbICD10.Name = "RbICD10"
        Me.RbICD10.Size = New System.Drawing.Size(58, 18)
        Me.RbICD10.TabIndex = 12
        Me.RbICD10.TabStop = True
        Me.RbICD10.Text = "ICD10"
        Me.RbICD10.UseVisualStyleBackColor = False
        '
        'RbICD9
        '
        Me.RbICD9.AutoSize = True
        Me.RbICD9.BackColor = System.Drawing.Color.Transparent
        Me.RbICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbICD9.Location = New System.Drawing.Point(34, 4)
        Me.RbICD9.Name = "RbICD9"
        Me.RbICD9.Size = New System.Drawing.Size(51, 18)
        Me.RbICD9.TabIndex = 11
        Me.RbICD9.TabStop = True
        Me.RbICD9.Text = "ICD9"
        Me.RbICD9.UseVisualStyleBackColor = False
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 24)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(528, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(0, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 24)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(529, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 24)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(530, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnlrht
        '
        Me.pnlrht.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlrht.Controls.Add(Me.pnlflexgrid)
        Me.pnlrht.Controls.Add(Me.splpnlElementHost)
        Me.pnlrht.Controls.Add(Me.pnlElementHost)
        Me.pnlrht.Controls.Add(Me.pnlUpdown)
        Me.pnlrht.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlrht.Location = New System.Drawing.Point(536, 53)
        Me.pnlrht.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlrht.Name = "pnlrht"
        Me.pnlrht.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlrht.Size = New System.Drawing.Size(345, 597)
        Me.pnlrht.TabIndex = 9
        '
        'pnlflexgrid
        '
        Me.pnlflexgrid.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlflexgrid.Controls.Add(Me.Label27)
        Me.pnlflexgrid.Controls.Add(Me.Label28)
        Me.pnlflexgrid.Controls.Add(Me.Label29)
        Me.pnlflexgrid.Controls.Add(Me.Label30)
        Me.pnlflexgrid.Controls.Add(Me.C1Dignosis)
        Me.pnlflexgrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlflexgrid.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlflexgrid.Location = New System.Drawing.Point(0, 3)
        Me.pnlflexgrid.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlflexgrid.Name = "pnlflexgrid"
        Me.pnlflexgrid.Padding = New System.Windows.Forms.Padding(0, 0, 2, 3)
        Me.pnlflexgrid.Size = New System.Drawing.Size(315, 344)
        Me.pnlflexgrid.TabIndex = 6
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label27.Location = New System.Drawing.Point(1, 340)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(311, 1)
        Me.Label27.TabIndex = 10
        Me.Label27.Text = "label2"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(0, 1)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 340)
        Me.Label28.TabIndex = 9
        Me.Label28.Text = "label4"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label29.Location = New System.Drawing.Point(312, 1)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 340)
        Me.Label29.TabIndex = 8
        Me.Label29.Text = "label3"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(0, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(313, 1)
        Me.Label30.TabIndex = 7
        Me.Label30.Text = "label1"
        '
        'C1Dignosis
        '
        Me.C1Dignosis.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1Dignosis.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.C1Dignosis.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1Dignosis.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Dignosis.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Dignosis.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1Dignosis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Dignosis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Dignosis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Dignosis.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Dignosis.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Dignosis.Location = New System.Drawing.Point(0, 0)
        Me.C1Dignosis.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1Dignosis.Name = "C1Dignosis"
        Me.C1Dignosis.Rows.Count = 1
        Me.C1Dignosis.Rows.DefaultSize = 19
        Me.C1Dignosis.Rows.Fixed = 0
        Me.C1Dignosis.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Dignosis.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Dignosis.ShowCellLabels = True
        Me.C1Dignosis.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1Dignosis.Size = New System.Drawing.Size(313, 341)
        Me.C1Dignosis.StyleInfo = resources.GetString("C1Dignosis.StyleInfo")
        Me.C1Dignosis.TabIndex = 6
        '
        'splpnlElementHost
        '
        Me.splpnlElementHost.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.splpnlElementHost.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.splpnlElementHost.Location = New System.Drawing.Point(0, 347)
        Me.splpnlElementHost.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.splpnlElementHost.Name = "splpnlElementHost"
        Me.splpnlElementHost.Size = New System.Drawing.Size(315, 3)
        Me.splpnlElementHost.TabIndex = 22
        Me.splpnlElementHost.TabStop = False
        '
        'pnlElementHost
        '
        Me.pnlElementHost.Controls.Add(Me.elementHost)
        Me.pnlElementHost.Controls.Add(Me.Panel5)
        Me.pnlElementHost.Controls.Add(Me.Label51)
        Me.pnlElementHost.Controls.Add(Me.Label50)
        Me.pnlElementHost.Controls.Add(Me.Label49)
        Me.pnlElementHost.Controls.Add(Me.Label48)
        Me.pnlElementHost.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlElementHost.Location = New System.Drawing.Point(0, 350)
        Me.pnlElementHost.Name = "pnlElementHost"
        Me.pnlElementHost.Size = New System.Drawing.Size(315, 247)
        Me.pnlElementHost.TabIndex = 23
        Me.pnlElementHost.Visible = False
        '
        'elementHost
        '
        Me.elementHost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.elementHost.Location = New System.Drawing.Point(1, 26)
        Me.elementHost.Name = "elementHost"
        Me.elementHost.Size = New System.Drawing.Size(313, 220)
        Me.elementHost.TabIndex = 0
        Me.elementHost.Text = "ElementHost1"
        Me.elementHost.Child = Nothing
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.btnClose)
        Me.Panel5.Controls.Add(Me.Label53)
        Me.Panel5.Controls.Add(Me.Label52)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel5.Location = New System.Drawing.Point(1, 1)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(313, 25)
        Me.Panel5.TabIndex = 4
        '
        'btnClose
        '
        Me.btnClose.AutoSize = True
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(283, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(30, 24)
        Me.btnClose.TabIndex = 10
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.Transparent
        Me.Label53.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label53.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.Location = New System.Drawing.Point(0, 0)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(225, 24)
        Me.Label53.TabIndex = 9
        Me.Label53.Text = "ICD 9 to 10 Mapping Scenarios"
        Me.Label53.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label52.Location = New System.Drawing.Point(0, 24)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(313, 1)
        Me.Label52.TabIndex = 8
        Me.Label52.Text = "label2"
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label51.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.Location = New System.Drawing.Point(1, 0)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(313, 1)
        Me.Label51.TabIndex = 14
        Me.Label51.Text = "label1"
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label50.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label50.Location = New System.Drawing.Point(314, 0)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(1, 246)
        Me.Label50.TabIndex = 13
        Me.Label50.Text = "label3"
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.Location = New System.Drawing.Point(0, 0)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(1, 246)
        Me.Label49.TabIndex = 12
        Me.Label49.Text = "label4"
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label48.Location = New System.Drawing.Point(0, 246)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(315, 1)
        Me.Label48.TabIndex = 11
        Me.Label48.Text = "label2"
        '
        'pnlUpdown
        '
        Me.pnlUpdown.BackColor = System.Drawing.Color.Transparent
        Me.pnlUpdown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlUpdown.Controls.Add(Me.Panel1)
        Me.pnlUpdown.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlUpdown.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.pnlUpdown.Location = New System.Drawing.Point(315, 3)
        Me.pnlUpdown.Name = "pnlUpdown"
        Me.pnlUpdown.Padding = New System.Windows.Forms.Padding(1, 0, 1, 3)
        Me.pnlUpdown.Size = New System.Drawing.Size(30, 594)
        Me.pnlUpdown.TabIndex = 8
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.Controls.Add(Me.Label39)
        Me.Panel1.Controls.Add(Me.Label38)
        Me.Panel1.Controls.Add(Me.Label37)
        Me.Panel1.Controls.Add(Me.btnDown)
        Me.Panel1.Controls.Add(Me.btnUP)
        Me.Panel1.Controls.Add(Me.Label40)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(1, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(28, 591)
        Me.Panel1.TabIndex = 15
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label39.Location = New System.Drawing.Point(27, 1)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(1, 589)
        Me.Label39.TabIndex = 13
        Me.Label39.Text = "label3"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(0, 1)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(1, 589)
        Me.Label38.TabIndex = 12
        Me.Label38.Text = "label4"
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label37.Location = New System.Drawing.Point(0, 590)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(28, 1)
        Me.Label37.TabIndex = 11
        Me.Label37.Text = "label2"
        '
        'btnDown
        '
        Me.btnDown.BackgroundImage = CType(resources.GetObject("btnDown.BackgroundImage"), System.Drawing.Image)
        Me.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnDown.FlatAppearance.BorderSize = 0
        Me.btnDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Location = New System.Drawing.Point(-1, 298)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(30, 30)
        Me.btnDown.TabIndex = 0
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'btnUP
        '
        Me.btnUP.AutoSize = True
        Me.btnUP.BackgroundImage = CType(resources.GetObject("btnUP.BackgroundImage"), System.Drawing.Image)
        Me.btnUP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnUP.FlatAppearance.BorderSize = 0
        Me.btnUP.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnUP.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUP.Location = New System.Drawing.Point(-1, 262)
        Me.btnUP.Name = "btnUP"
        Me.btnUP.Size = New System.Drawing.Size(30, 30)
        Me.btnUP.TabIndex = 0
        Me.btnUP.UseVisualStyleBackColor = True
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(0, 0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(28, 1)
        Me.Label40.TabIndex = 14
        Me.Label40.Text = "label1"
        '
        'pnltrvrht
        '
        Me.pnltrvrht.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrvrht.Controls.Add(Me.GloUC_trvAssociates)
        Me.pnltrvrht.Controls.Add(Me.panel9)
        Me.pnltrvrht.Controls.Add(Me.pnl_btnCPT)
        Me.pnltrvrht.Controls.Add(Me.pnl_btnmodifier)
        Me.pnltrvrht.Controls.Add(Me.pnl_txtsearchrht)
        Me.pnltrvrht.Controls.Add(Me.pnlradiorht)
        Me.pnltrvrht.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnltrvrht.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrvrht.Location = New System.Drawing.Point(884, 53)
        Me.pnltrvrht.Margin = New System.Windows.Forms.Padding(0)
        Me.pnltrvrht.Name = "pnltrvrht"
        Me.pnltrvrht.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnltrvrht.Size = New System.Drawing.Size(210, 597)
        Me.pnltrvrht.TabIndex = 11
        '
        'GloUC_trvAssociates
        '
        Me.GloUC_trvAssociates.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvAssociates.CheckBoxes = False
        Me.GloUC_trvAssociates.CodeMember = Nothing
        Me.GloUC_trvAssociates.ColonAsSeparator = False
        Me.GloUC_trvAssociates.Comment = Nothing
        Me.GloUC_trvAssociates.ConceptID = Nothing
        Me.GloUC_trvAssociates.CPT = Nothing
        Me.GloUC_trvAssociates.DDIDMember = Nothing
        Me.GloUC_trvAssociates.DescriptionMember = Nothing
        Me.GloUC_trvAssociates.DisplayContextMenuStrip = Nothing
        Me.GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
        Me.GloUC_trvAssociates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvAssociates.DrugFlag = CType(16, Short)
        Me.GloUC_trvAssociates.DrugFormMember = Nothing
        Me.GloUC_trvAssociates.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvAssociates.DurationMember = Nothing
        Me.GloUC_trvAssociates.EducationMappingSearchType = 1
        Me.GloUC_trvAssociates.FrequencyMember = Nothing
        Me.GloUC_trvAssociates.HistoryType = Nothing
        Me.GloUC_trvAssociates.ICD9 = Nothing
        Me.GloUC_trvAssociates.ICDRevision = Nothing
        Me.GloUC_trvAssociates.ImageIndex = 0
        Me.GloUC_trvAssociates.ImageList = Me.imglistTrvCPT
        Me.GloUC_trvAssociates.ImageObject = Nothing
        Me.GloUC_trvAssociates.Indicator = Nothing
        Me.GloUC_trvAssociates.IsCPTSearch = False
        Me.GloUC_trvAssociates.IsDiagnosisSearch = False
        Me.GloUC_trvAssociates.IsDrug = False
        Me.GloUC_trvAssociates.IsNarcoticsMember = Nothing
        Me.GloUC_trvAssociates.IsSearchForEducationMapping = False
        Me.GloUC_trvAssociates.IsSystemCategory = Nothing
        Me.GloUC_trvAssociates.Location = New System.Drawing.Point(0, 31)
        Me.GloUC_trvAssociates.MaximumNodes = 1000
        Me.GloUC_trvAssociates.Name = "GloUC_trvAssociates"
        Me.GloUC_trvAssociates.NDCCodeMember = Nothing
        Me.GloUC_trvAssociates.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.GloUC_trvAssociates.ParentImageIndex = 0
        Me.GloUC_trvAssociates.ParentMember = Nothing
        Me.GloUC_trvAssociates.RouteMember = Nothing
        Me.GloUC_trvAssociates.RowOrderMember = Nothing
        Me.GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvAssociates.SearchBox = True
        Me.GloUC_trvAssociates.SearchText = Nothing
        Me.GloUC_trvAssociates.SelectedImageIndex = 0
        Me.GloUC_trvAssociates.SelectedNode = Nothing
        Me.GloUC_trvAssociates.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvAssociates.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvAssociates.SelectedParentImageIndex = 0
        Me.GloUC_trvAssociates.Size = New System.Drawing.Size(210, 538)
        Me.GloUC_trvAssociates.SmartTreatmentId = Nothing
        Me.GloUC_trvAssociates.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvAssociates.TabIndex = 19
        Me.GloUC_trvAssociates.Tag = Nothing
        Me.GloUC_trvAssociates.UnitMember = Nothing
        Me.GloUC_trvAssociates.ValueMember = Nothing
        '
        'imglistTrvCPT
        '
        Me.imglistTrvCPT.ImageStream = CType(resources.GetObject("imglistTrvCPT.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imglistTrvCPT.TransparentColor = System.Drawing.Color.Transparent
        Me.imglistTrvCPT.Images.SetKeyName(0, "CPT.ico")
        Me.imglistTrvCPT.Images.SetKeyName(1, "Bullet06.ico")
        Me.imglistTrvCPT.Images.SetKeyName(2, "Small Arrow.ico")
        Me.imglistTrvCPT.Images.SetKeyName(3, "Arrow03.ico")
        Me.imglistTrvCPT.Images.SetKeyName(4, "Square01.ico")
        Me.imglistTrvCPT.Images.SetKeyName(5, "CPT.ico")
        Me.imglistTrvCPT.Images.SetKeyName(6, "arrow_01.ico")
        Me.imglistTrvCPT.Images.SetKeyName(7, "bullet.ico")
        Me.imglistTrvCPT.Images.SetKeyName(8, "90 Degree BlueArrow.ico")
        '
        'panel9
        '
        Me.panel9.BackColor = System.Drawing.Color.Transparent
        Me.panel9.Controls.Add(Me.trvCPT)
        Me.panel9.Controls.Add(Me.Label34)
        Me.panel9.Controls.Add(Me.Label32)
        Me.panel9.Controls.Add(Me.label21)
        Me.panel9.Controls.Add(Me.label22)
        Me.panel9.Controls.Add(Me.label23)
        Me.panel9.Controls.Add(Me.label24)
        Me.panel9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panel9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.panel9.Location = New System.Drawing.Point(74, 403)
        Me.panel9.Name = "panel9"
        Me.panel9.Padding = New System.Windows.Forms.Padding(1, 1, 3, 3)
        Me.panel9.Size = New System.Drawing.Size(91, 102)
        Me.panel9.TabIndex = 17
        '
        'trvCPT
        '
        Me.trvCPT.BackColor = System.Drawing.Color.White
        Me.trvCPT.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvCPT.ForeColor = System.Drawing.Color.Black
        Me.trvCPT.HideSelection = False
        Me.trvCPT.Indent = 20
        Me.trvCPT.ItemHeight = 20
        Me.trvCPT.Location = New System.Drawing.Point(6, 6)
        Me.trvCPT.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.trvCPT.Name = "trvCPT"
        Me.trvCPT.ShowLines = False
        Me.trvCPT.ShowPlusMinus = False
        Me.trvCPT.ShowRootLines = False
        Me.trvCPT.Size = New System.Drawing.Size(81, 92)
        Me.trvCPT.TabIndex = 3
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.White
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label34.Location = New System.Drawing.Point(2, 6)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(4, 92)
        Me.Label34.TabIndex = 40
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.White
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label32.Location = New System.Drawing.Point(2, 2)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(85, 4)
        Me.Label32.TabIndex = 38
        '
        'label21
        '
        Me.label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label21.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label21.Location = New System.Drawing.Point(2, 98)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(85, 1)
        Me.label21.TabIndex = 4
        Me.label21.Text = "label2"
        '
        'label22
        '
        Me.label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label22.Location = New System.Drawing.Point(1, 2)
        Me.label22.Name = "label22"
        Me.label22.Size = New System.Drawing.Size(1, 97)
        Me.label22.TabIndex = 3
        Me.label22.Text = "label4"
        '
        'label23
        '
        Me.label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label23.Dock = System.Windows.Forms.DockStyle.Right
        Me.label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.label23.Location = New System.Drawing.Point(87, 2)
        Me.label23.Name = "label23"
        Me.label23.Size = New System.Drawing.Size(1, 97)
        Me.label23.TabIndex = 2
        Me.label23.Text = "label3"
        '
        'label24
        '
        Me.label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label24.Location = New System.Drawing.Point(1, 1)
        Me.label24.Name = "label24"
        Me.label24.Size = New System.Drawing.Size(87, 1)
        Me.label24.TabIndex = 0
        Me.label24.Text = "label1"
        '
        'pnl_btnCPT
        '
        Me.pnl_btnCPT.Controls.Add(Me.Label15)
        Me.pnl_btnCPT.Controls.Add(Me.Label16)
        Me.pnl_btnCPT.Controls.Add(Me.Label17)
        Me.pnl_btnCPT.Controls.Add(Me.Label18)
        Me.pnl_btnCPT.Controls.Add(Me.btnCPT)
        Me.pnl_btnCPT.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_btnCPT.Location = New System.Drawing.Point(0, 3)
        Me.pnl_btnCPT.Name = "pnl_btnCPT"
        Me.pnl_btnCPT.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnl_btnCPT.Size = New System.Drawing.Size(210, 28)
        Me.pnl_btnCPT.TabIndex = 12
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(1, 24)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(205, 1)
        Me.Label15.TabIndex = 14
        Me.Label15.Text = "label2"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(0, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 24)
        Me.Label16.TabIndex = 13
        Me.Label16.Text = "label4"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(206, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 24)
        Me.Label17.TabIndex = 12
        Me.Label17.Text = "label3"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(0, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(207, 1)
        Me.Label18.TabIndex = 11
        Me.Label18.Text = "label1"
        '
        'btnCPT
        '
        Me.btnCPT.BackColor = System.Drawing.Color.Transparent
        Me.btnCPT.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.Img_LongOrange
        Me.btnCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCPT.FlatAppearance.BorderSize = 0
        Me.btnCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCPT.Location = New System.Drawing.Point(0, 0)
        Me.btnCPT.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnCPT.Name = "btnCPT"
        Me.btnCPT.Size = New System.Drawing.Size(207, 25)
        Me.btnCPT.TabIndex = 10
        Me.btnCPT.Tag = "Selected"
        Me.btnCPT.Text = "&CPT"
        Me.btnCPT.UseVisualStyleBackColor = False
        '
        'pnl_btnmodifier
        '
        Me.pnl_btnmodifier.BackColor = System.Drawing.Color.Transparent
        Me.pnl_btnmodifier.Controls.Add(Me.Label19)
        Me.pnl_btnmodifier.Controls.Add(Me.Label20)
        Me.pnl_btnmodifier.Controls.Add(Me.Label25)
        Me.pnl_btnmodifier.Controls.Add(Me.Label26)
        Me.pnl_btnmodifier.Controls.Add(Me.btnmodifier)
        Me.pnl_btnmodifier.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnl_btnmodifier.Location = New System.Drawing.Point(0, 569)
        Me.pnl_btnmodifier.Name = "pnl_btnmodifier"
        Me.pnl_btnmodifier.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnl_btnmodifier.Size = New System.Drawing.Size(210, 28)
        Me.pnl_btnmodifier.TabIndex = 18
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(1, 24)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(205, 1)
        Me.Label19.TabIndex = 13
        Me.Label19.Text = "label2"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(0, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 24)
        Me.Label20.TabIndex = 12
        Me.Label20.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(206, 1)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 24)
        Me.Label25.TabIndex = 11
        Me.Label25.Text = "label3"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(0, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(207, 1)
        Me.Label26.TabIndex = 10
        Me.Label26.Text = "label1"
        '
        'btnmodifier
        '
        Me.btnmodifier.BackColor = System.Drawing.Color.Transparent
        Me.btnmodifier.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.Img_Button
        Me.btnmodifier.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnmodifier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnmodifier.FlatAppearance.BorderSize = 0
        Me.btnmodifier.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnmodifier.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnmodifier.Location = New System.Drawing.Point(0, 0)
        Me.btnmodifier.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnmodifier.Name = "btnmodifier"
        Me.btnmodifier.Size = New System.Drawing.Size(207, 25)
        Me.btnmodifier.TabIndex = 9
        Me.btnmodifier.Tag = "UnSelected"
        Me.btnmodifier.Text = "Modifier"
        Me.btnmodifier.UseVisualStyleBackColor = False
        '
        'pnl_txtsearchrht
        '
        Me.pnl_txtsearchrht.BackColor = System.Drawing.Color.Transparent
        Me.pnl_txtsearchrht.Controls.Add(Me.txtsearchrht)
        Me.pnl_txtsearchrht.Controls.Add(Me.Label3)
        Me.pnl_txtsearchrht.Controls.Add(Me.Label4)
        Me.pnl_txtsearchrht.Controls.Add(Me.PictureBox1)
        Me.pnl_txtsearchrht.Controls.Add(Me.Label11)
        Me.pnl_txtsearchrht.Controls.Add(Me.Label12)
        Me.pnl_txtsearchrht.Controls.Add(Me.Label13)
        Me.pnl_txtsearchrht.Controls.Add(Me.Label14)
        Me.pnl_txtsearchrht.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_txtsearchrht.ForeColor = System.Drawing.Color.Black
        Me.pnl_txtsearchrht.Location = New System.Drawing.Point(74, 370)
        Me.pnl_txtsearchrht.Name = "pnl_txtsearchrht"
        Me.pnl_txtsearchrht.Padding = New System.Windows.Forms.Padding(1, 1, 3, 3)
        Me.pnl_txtsearchrht.Size = New System.Drawing.Size(96, 27)
        Me.pnl_txtsearchrht.TabIndex = 16
        '
        'txtsearchrht
        '
        Me.txtsearchrht.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearchrht.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsearchrht.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsearchrht.ForeColor = System.Drawing.Color.Black
        Me.txtsearchrht.Location = New System.Drawing.Point(30, 6)
        Me.txtsearchrht.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.txtsearchrht.Name = "txtsearchrht"
        Me.txtsearchrht.Size = New System.Drawing.Size(62, 15)
        Me.txtsearchrht.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Location = New System.Drawing.Point(30, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 4)
        Me.Label3.TabIndex = 37
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Location = New System.Drawing.Point(30, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 2)
        Me.Label4.TabIndex = 38
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(2, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Location = New System.Drawing.Point(2, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(90, 1)
        Me.Label11.TabIndex = 35
        Me.Label11.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(2, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(90, 1)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "label1"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Location = New System.Drawing.Point(1, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 23)
        Me.Label13.TabIndex = 39
        Me.Label13.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Location = New System.Drawing.Point(92, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 23)
        Me.Label14.TabIndex = 40
        Me.Label14.Text = "label4"
        '
        'pnlradiorht
        '
        Me.pnlradiorht.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlradiorht.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.Img_Button
        Me.pnlradiorht.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlradiorht.Controls.Add(Me.Label2)
        Me.pnlradiorht.Controls.Add(Me.Label1)
        Me.pnlradiorht.Controls.Add(Me.Label10)
        Me.pnlradiorht.Controls.Add(Me.Label9)
        Me.pnlradiorht.Controls.Add(Me.rdoDescriptionrht)
        Me.pnlradiorht.Controls.Add(Me.rdocode)
        Me.pnlradiorht.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlradiorht.ForeColor = System.Drawing.Color.Black
        Me.pnlradiorht.Location = New System.Drawing.Point(0, 3)
        Me.pnlradiorht.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlradiorht.Name = "pnlradiorht"
        Me.pnlradiorht.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlradiorht.Size = New System.Drawing.Size(210, 27)
        Me.pnlradiorht.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(206, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 22)
        Me.Label2.TabIndex = 16
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1, 22)
        Me.Label1.TabIndex = 15
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Location = New System.Drawing.Point(0, 23)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(207, 1)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "label1"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(207, 1)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "label1"
        '
        'rdoDescriptionrht
        '
        Me.rdoDescriptionrht.AutoSize = True
        Me.rdoDescriptionrht.BackColor = System.Drawing.Color.Transparent
        Me.rdoDescriptionrht.Checked = True
        Me.rdoDescriptionrht.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoDescriptionrht.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.rdoDescriptionrht.Location = New System.Drawing.Point(106, 3)
        Me.rdoDescriptionrht.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.rdoDescriptionrht.Name = "rdoDescriptionrht"
        Me.rdoDescriptionrht.Size = New System.Drawing.Size(94, 18)
        Me.rdoDescriptionrht.TabIndex = 1
        Me.rdoDescriptionrht.TabStop = True
        Me.rdoDescriptionrht.Text = "Description"
        Me.rdoDescriptionrht.UseVisualStyleBackColor = False
        Me.rdoDescriptionrht.Visible = False
        '
        'rdocode
        '
        Me.rdocode.AutoSize = True
        Me.rdocode.BackColor = System.Drawing.Color.Transparent
        Me.rdocode.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.rdocode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdocode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.rdocode.Location = New System.Drawing.Point(16, 3)
        Me.rdocode.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.rdocode.Name = "rdocode"
        Me.rdocode.Size = New System.Drawing.Size(53, 18)
        Me.rdocode.TabIndex = 0
        Me.rdocode.Text = "Code"
        Me.rdocode.UseVisualStyleBackColor = False
        '
        'imglistTrvModifier
        '
        Me.imglistTrvModifier.ImageStream = CType(resources.GetObject("imglistTrvModifier.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imglistTrvModifier.TransparentColor = System.Drawing.Color.Transparent
        Me.imglistTrvModifier.Images.SetKeyName(0, "Modify.ico")
        Me.imglistTrvModifier.Images.SetKeyName(1, "Bullet06.ico")
        Me.imglistTrvModifier.Images.SetKeyName(2, "Small Arrow.ico")
        Me.imglistTrvModifier.Images.SetKeyName(3, "Arrow03.ico")
        Me.imglistTrvModifier.Images.SetKeyName(4, "Square01.ico")
        Me.imglistTrvModifier.Images.SetKeyName(5, "modifier.ico")
        Me.imglistTrvModifier.Images.SetKeyName(6, "arrow_01.ico")
        Me.imglistTrvModifier.Images.SetKeyName(7, "bullet.ico")
        Me.imglistTrvModifier.Images.SetKeyName(8, "90 Degree BlueArrow.ico")
        '
        'ContextMenuDiagnosis
        '
        Me.ContextMenuDiagnosis.BackColor = System.Drawing.Color.White
        Me.ContextMenuDiagnosis.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuRemoveAllDiagnosis, Me.mnuRemoveSelectedDiagnosis, Me.mnuAddICD9, Me.mnuAddCPT, Me.mnuAddModifier, Me.mnuEditICD9, Me.mnuEditCPT, Me.mnuEditModifier, Me.mnuAssociateCPTWithAllICD9, Me.mnuAssociateCPTWithAllUnassociatedICD9, Me.mnuSetAsPrimary, Me.mnuAssociateAllCPTtoAllICD9, Me.mnuAssociateAllCPTToAllUnassociatedICD9, Me.mnuRemoveAllModifierforSelectedCPT, Me.mnuAssociateModifierWithAllCPT, Me.mnuAddICD10, Me.mnuEditICD10})
        Me.ContextMenuDiagnosis.Name = "ContextMenuDiagnosis"
        Me.ContextMenuDiagnosis.Size = New System.Drawing.Size(295, 378)
        '
        'mnuRemoveAllDiagnosis
        '
        Me.mnuRemoveAllDiagnosis.Name = "mnuRemoveAllDiagnosis"
        Me.mnuRemoveAllDiagnosis.Size = New System.Drawing.Size(294, 22)
        Me.mnuRemoveAllDiagnosis.Text = "Remove All Diagnosis"
        '
        'mnuRemoveSelectedDiagnosis
        '
        Me.mnuRemoveSelectedDiagnosis.Name = "mnuRemoveSelectedDiagnosis"
        Me.mnuRemoveSelectedDiagnosis.Size = New System.Drawing.Size(294, 22)
        Me.mnuRemoveSelectedDiagnosis.Text = "Remove Selected Diagnosis"
        '
        'mnuAddICD9
        '
        Me.mnuAddICD9.Name = "mnuAddICD9"
        Me.mnuAddICD9.Size = New System.Drawing.Size(294, 22)
        Me.mnuAddICD9.Text = "Add ICD9"
        '
        'mnuAddCPT
        '
        Me.mnuAddCPT.Name = "mnuAddCPT"
        Me.mnuAddCPT.Size = New System.Drawing.Size(294, 22)
        Me.mnuAddCPT.Text = "Add CPT"
        '
        'mnuAddModifier
        '
        Me.mnuAddModifier.Name = "mnuAddModifier"
        Me.mnuAddModifier.Size = New System.Drawing.Size(294, 22)
        Me.mnuAddModifier.Text = "Add Modifier"
        '
        'mnuEditICD9
        '
        Me.mnuEditICD9.Name = "mnuEditICD9"
        Me.mnuEditICD9.Size = New System.Drawing.Size(294, 22)
        Me.mnuEditICD9.Text = "Edit ICD9"
        '
        'mnuEditCPT
        '
        Me.mnuEditCPT.Name = "mnuEditCPT"
        Me.mnuEditCPT.Size = New System.Drawing.Size(294, 22)
        Me.mnuEditCPT.Text = "Edit CPT"
        '
        'mnuEditModifier
        '
        Me.mnuEditModifier.Name = "mnuEditModifier"
        Me.mnuEditModifier.Size = New System.Drawing.Size(294, 22)
        Me.mnuEditModifier.Text = "Edit Modifier"
        '
        'mnuAssociateCPTWithAllICD9
        '
        Me.mnuAssociateCPTWithAllICD9.Name = "mnuAssociateCPTWithAllICD9"
        Me.mnuAssociateCPTWithAllICD9.Size = New System.Drawing.Size(294, 22)
        Me.mnuAssociateCPTWithAllICD9.Text = "Associate CPT with all ICD9"
        '
        'mnuAssociateCPTWithAllUnassociatedICD9
        '
        Me.mnuAssociateCPTWithAllUnassociatedICD9.Name = "mnuAssociateCPTWithAllUnassociatedICD9"
        Me.mnuAssociateCPTWithAllUnassociatedICD9.Size = New System.Drawing.Size(294, 22)
        Me.mnuAssociateCPTWithAllUnassociatedICD9.Text = "Associate CPT with all unassociated ICD9"
        '
        'mnuSetAsPrimary
        '
        Me.mnuSetAsPrimary.Name = "mnuSetAsPrimary"
        Me.mnuSetAsPrimary.Size = New System.Drawing.Size(294, 22)
        Me.mnuSetAsPrimary.Text = "Set as Primary Diagnosis"
        '
        'mnuAssociateAllCPTtoAllICD9
        '
        Me.mnuAssociateAllCPTtoAllICD9.Name = "mnuAssociateAllCPTtoAllICD9"
        Me.mnuAssociateAllCPTtoAllICD9.Size = New System.Drawing.Size(294, 22)
        Me.mnuAssociateAllCPTtoAllICD9.Text = "Associate all CPT to all ICD9"
        '
        'mnuAssociateAllCPTToAllUnassociatedICD9
        '
        Me.mnuAssociateAllCPTToAllUnassociatedICD9.Name = "mnuAssociateAllCPTToAllUnassociatedICD9"
        Me.mnuAssociateAllCPTToAllUnassociatedICD9.Size = New System.Drawing.Size(294, 22)
        Me.mnuAssociateAllCPTToAllUnassociatedICD9.Text = "Associate all CPT to all Unassociated ICD9"
        '
        'mnuRemoveAllModifierforSelectedCPT
        '
        Me.mnuRemoveAllModifierforSelectedCPT.Name = "mnuRemoveAllModifierforSelectedCPT"
        Me.mnuRemoveAllModifierforSelectedCPT.Size = New System.Drawing.Size(294, 22)
        Me.mnuRemoveAllModifierforSelectedCPT.Text = "Remove all Modifier for selected CPT"
        '
        'mnuAssociateModifierWithAllCPT
        '
        Me.mnuAssociateModifierWithAllCPT.Name = "mnuAssociateModifierWithAllCPT"
        Me.mnuAssociateModifierWithAllCPT.Size = New System.Drawing.Size(294, 22)
        Me.mnuAssociateModifierWithAllCPT.Text = "Associate Modifier with all CPT"
        '
        'mnuAddICD10
        '
        Me.mnuAddICD10.Name = "mnuAddICD10"
        Me.mnuAddICD10.Size = New System.Drawing.Size(294, 22)
        Me.mnuAddICD10.Text = "Add ICD10"
        '
        'mnuEditICD10
        '
        Me.mnuEditICD10.Name = "mnuEditICD10"
        Me.mnuEditICD10.Size = New System.Drawing.Size(294, 22)
        Me.mnuEditICD10.Text = "Edit ICD10"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(533, 53)
        Me.Splitter1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 597)
        Me.Splitter1.TabIndex = 12
        Me.Splitter1.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter2.Location = New System.Drawing.Point(881, 53)
        Me.Splitter2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 597)
        Me.Splitter2.TabIndex = 13
        Me.Splitter2.TabStop = False
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton2.Text = "ToolStripButton2"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton3.Text = "ToolStripButton3"
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton4.Text = "ToolStripButton4"
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton5.Image = CType(resources.GetObject("ToolStripButton5.Image"), System.Drawing.Image)
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(36, 36)
        Me.ToolStripButton5.Text = "ToolStripButton5"
        '
        'pnl_tlsp_Top
        '
        Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp_Top.BackgroundImage = Global.gloEmdeonCommon.My.Resources.Resources.Img_Toolstrip
        Me.pnl_tlsp_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_tlsp_Top.Controls.Add(Me.Label35)
        Me.pnl_tlsp_Top.Controls.Add(Me.tstripDiagnosis)
        Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(1094, 53)
        Me.pnl_tlsp_Top.TabIndex = 16
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.Color.Transparent
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(461, 34)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(397, 14)
        Me.Label35.TabIndex = 8
        Me.Label35.Text = "CPT™ copyright  2012 American Medical Association. All rights reserved"
        '
        'tstripDiagnosis
        '
        Me.tstripDiagnosis.BackColor = System.Drawing.Color.Transparent
        Me.tstripDiagnosis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstripDiagnosis.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstripDiagnosis.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstripDiagnosis.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstripDiagnosis.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtnNew, Me.tlsbtnFinish, Me.tlsbtnSave, Me.tlsShowICD10Codes, Me.tlsbtnCodingRule, Me.tlsbtnClose})
        Me.tstripDiagnosis.Location = New System.Drawing.Point(0, 0)
        Me.tstripDiagnosis.Name = "tstripDiagnosis"
        Me.tstripDiagnosis.Size = New System.Drawing.Size(1094, 53)
        Me.tstripDiagnosis.TabIndex = 0
        Me.tstripDiagnosis.Text = "ToolStrip1"
        '
        'tlsbtnNew
        '
        Me.tlsbtnNew.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnNew.Image = CType(resources.GetObject("tlsbtnNew.Image"), System.Drawing.Image)
        Me.tlsbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnNew.Name = "tlsbtnNew"
        Me.tlsbtnNew.Size = New System.Drawing.Size(37, 50)
        Me.tlsbtnNew.Text = "&New"
        Me.tlsbtnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnNew.ToolTipText = "New"
        Me.tlsbtnNew.Visible = False
        '
        'tlsbtnFinish
        '
        Me.tlsbtnFinish.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnFinish.Image = CType(resources.GetObject("tlsbtnFinish.Image"), System.Drawing.Image)
        Me.tlsbtnFinish.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnFinish.Name = "tlsbtnFinish"
        Me.tlsbtnFinish.Size = New System.Drawing.Size(45, 50)
        Me.tlsbtnFinish.Text = "&Finish"
        Me.tlsbtnFinish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnFinish.ToolTipText = "Finish"
        Me.tlsbtnFinish.Visible = False
        '
        'tlsbtnSave
        '
        Me.tlsbtnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnSave.Image = CType(resources.GetObject("tlsbtnSave.Image"), System.Drawing.Image)
        Me.tlsbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnSave.Name = "tlsbtnSave"
        Me.tlsbtnSave.Size = New System.Drawing.Size(66, 50)
        Me.tlsbtnSave.Text = "&Save&&Cls"
        Me.tlsbtnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnSave.ToolTipText = "Save and Close"
        '
        'tlsShowICD10Codes
        '
        Me.tlsShowICD10Codes.Image = CType(resources.GetObject("tlsShowICD10Codes.Image"), System.Drawing.Image)
        Me.tlsShowICD10Codes.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsShowICD10Codes.Name = "tlsShowICD10Codes"
        Me.tlsShowICD10Codes.Size = New System.Drawing.Size(78, 50)
        Me.tlsShowICD10Codes.Text = "Show &ICD10"
        Me.tlsShowICD10Codes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsShowICD10Codes.ToolTipText = "Show ICD-10 Mapping Codes"
        '
        'tlsbtnCodingRule
        '
        Me.tlsbtnCodingRule.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tlsbtnCodingRule.Image = CType(resources.GetObject("tlsbtnCodingRule.Image"), System.Drawing.Image)
        Me.tlsbtnCodingRule.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnCodingRule.Name = "tlsbtnCodingRule"
        Me.tlsbtnCodingRule.Size = New System.Drawing.Size(85, 50)
        Me.tlsbtnCodingRule.Text = "Coding &Rule"
        Me.tlsbtnCodingRule.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsbtnClose
        '
        Me.tlsbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnClose.Image = CType(resources.GetObject("tlsbtnClose.Image"), System.Drawing.Image)
        Me.tlsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnClose.Name = "tlsbtnClose"
        Me.tlsbtnClose.Size = New System.Drawing.Size(43, 50)
        Me.tlsbtnClose.Text = "&Close"
        Me.tlsbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnClose.ToolTipText = "Close"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frm_Diagnosis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1094, 650)
        Me.Controls.Add(Me.pnlrht)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnltrvrht)
        Me.Controls.Add(Me.pnlleft)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Name = "frm_Diagnosis"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Diagnosis"
        Me.pnlleft.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_radiolft.ResumeLayout(False)
        Me.pnlradiolft.ResumeLayout(False)
        Me.pnlradiolft.PerformLayout()
        Me.pnlrht.ResumeLayout(False)
        Me.pnlflexgrid.ResumeLayout(False)
        CType(Me.C1Dignosis, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlElementHost.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.pnlUpdown.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnltrvrht.ResumeLayout(False)
        Me.panel9.ResumeLayout(False)
        Me.pnl_btnCPT.ResumeLayout(False)
        Me.pnl_btnmodifier.ResumeLayout(False)
        Me.pnl_txtsearchrht.ResumeLayout(False)
        Me.pnl_txtsearchrht.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlradiorht.ResumeLayout(False)
        Me.pnlradiorht.PerformLayout()
        Me.ContextMenuDiagnosis.ResumeLayout(False)
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstripDiagnosis.ResumeLayout(False)
        Me.tstripDiagnosis.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tblsave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblFinish As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlleft As System.Windows.Forms.Panel
    Friend WithEvents txtsearch As System.Windows.Forms.TextBox
    Friend WithEvents trICD9 As System.Windows.Forms.TreeView
    Friend WithEvents pnlradiolft As System.Windows.Forms.Panel
    Friend WithEvents pnlrht As System.Windows.Forms.Panel
    Friend WithEvents pnlflexgrid As System.Windows.Forms.Panel
    Friend WithEvents C1Dignosis As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnltrvrht As System.Windows.Forms.Panel
    Friend WithEvents trvCPT As System.Windows.Forms.TreeView
    Friend WithEvents pnlradiorht As System.Windows.Forms.Panel
    Friend WithEvents rdoDescriptionrht As System.Windows.Forms.RadioButton
    Friend WithEvents rdocode As System.Windows.Forms.RadioButton
    Friend WithEvents btnmodifier As System.Windows.Forms.Button
    Friend WithEvents btnCPT As System.Windows.Forms.Button
    Friend WithEvents imglistTrvICD9 As System.Windows.Forms.ImageList
    Friend WithEvents tblNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents imglistTrvCPT As System.Windows.Forms.ImageList
    Friend WithEvents imglistTrvModifier As System.Windows.Forms.ImageList
    Friend WithEvents ContextMenuDiagnosis As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuRemoveSelectedDiagnosis As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton5 As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuAddICD9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAddCPT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAddModifier As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtsearchrht As System.Windows.Forms.TextBox
    Friend WithEvents mnuEditICD9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEditCPT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEditModifier As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents tstripDiagnosis As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsbtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnFinish As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlsbtnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnl_radiolft As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Friend WithEvents pnl_btnCPT As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnl_btnmodifier As System.Windows.Forms.Panel
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents panel9 As System.Windows.Forms.Panel
    Private WithEvents label21 As System.Windows.Forms.Label
    Private WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents label24 As System.Windows.Forms.Label
    Friend WithEvents pnl_txtsearchrht As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents mnuSetAsPrimary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAssociateCPTWithAllICD9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAssociateCPTWithAllUnassociatedICD9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GloUC_trvICD As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents GloUC_trvAssociates As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents mnuRemoveAllDiagnosis As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAssociateAllCPTtoAllICD9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAssociateAllCPTToAllUnassociatedICD9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRemoveAllModifierforSelectedCPT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAssociateModifierWithAllCPT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlUpdown As System.Windows.Forms.Panel
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Private WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUP As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RbICD10 As System.Windows.Forms.RadioButton
    Friend WithEvents RbICD9 As System.Windows.Forms.RadioButton
    Friend WithEvents mnuAddICD10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEditICD10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlsShowICD10Codes As System.Windows.Forms.ToolStripButton
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents tlsbtnCodingRule As System.Windows.Forms.ToolStripButton
    Friend WithEvents splpnlElementHost As System.Windows.Forms.Splitter
    Friend WithEvents pnlElementHost As System.Windows.Forms.Panel
    Friend WithEvents elementHost As System.Windows.Forms.Integration.ElementHost
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Private WithEvents Label52 As System.Windows.Forms.Label
    Private WithEvents Label51 As System.Windows.Forms.Label
    Private WithEvents Label50 As System.Windows.Forms.Label
    Private WithEvents Label49 As System.Windows.Forms.Label
    Private WithEvents Label48 As System.Windows.Forms.Label
    'Friend WithEvents ServiceController1 As System.ServiceProcess.ServiceController
End Class
