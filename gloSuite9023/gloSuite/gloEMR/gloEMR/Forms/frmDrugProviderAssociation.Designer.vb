<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDrugProviderAssociation
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Dim CmppControls() As System.Windows.Forms.ContextMenuStrip = {cntxDeleteDrugs, ContextMenuStrip_Sig}

            components.Dispose()
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try



            If (IsNothing(CmppControls) = False) Then
                If CmppControls.Length > 0 Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(CmppControls)
                End If
            End If



            If (IsNothing(CmppControls) = False) Then
                If CmppControls.Length > 0 Then
                    gloGlobal.cEventHelper.DisposeContextMenuStrip(CmppControls)
                End If
            End If


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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDrugProviderAssociation))
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tblStrip_32 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblbtn_Save_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Finish_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Close_32 = New System.Windows.Forms.ToolStripButton()
        Me.pnlRight = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.GloUC_trvAllDrugs = New gloUserControlLibrary.gloUC_TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.trvAllDrugs = New System.Windows.Forms.TreeView()
        Me.pnlbtnClinical = New System.Windows.Forms.Panel()
        Me.btnClinical = New System.Windows.Forms.Button()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.pnlbtnAllergies = New System.Windows.Forms.Panel()
        Me.btnAllergies = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.pnlbtnC2 = New System.Windows.Forms.Panel()
        Me.btnC2 = New System.Windows.Forms.Button()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.txtSearchDrugs = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.label9 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.pnlbtnAllDrugs = New System.Windows.Forms.Panel()
        Me.btnAllDrugs = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.pnltrvDrugs = New System.Windows.Forms.Panel()
        Me.GloUC_trvDrugs = New gloUserControlLibrary.gloUC_TreeView()
        Me.trvDrugs = New System.Windows.Forms.TreeView()
        Me.pnlSearchLeft = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.pnlbtnProvider = New System.Windows.Forms.Panel()
        Me.btnProvider = New System.Windows.Forms.Button()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.C1DrugstoProvider = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnl_ProviderName = New System.Windows.Forms.Panel()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.lbl_ProviderName = New System.Windows.Forms.Label()
        Me.lblProviderCaption = New System.Windows.Forms.Label()
        Me.pnlDrugSigCtrl = New System.Windows.Forms.Panel()
        Me.txtDoseUnit = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.lblNDCCodetxt = New System.Windows.Forms.Label()
        Me.lblNDCCode = New System.Windows.Forms.Label()
        Me.lblDrugNametxt = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtRefills = New System.Windows.Forms.TextBox()
        Me.lblRefills = New System.Windows.Forms.Label()
        Me.cmbDuration = New System.Windows.Forms.ComboBox()
        Me.txtFrequency = New System.Windows.Forms.TextBox()
        Me.txtRoute = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtDuration = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.tlsp_MSTSIG = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Splitter3 = New System.Windows.Forms.Splitter()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.trvAddDrugsToProvider = New System.Windows.Forms.TreeView()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cntxDeleteDrugs = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.ContextMenuStrip_Sig = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddNewSigInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModifySigInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveDrugToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.cmbDoseUnit = New System.Windows.Forms.ComboBox()
        Me.pnlTop.SuspendLayout()
        Me.tblStrip_32.SuspendLayout()
        Me.pnlRight.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlbtnClinical.SuspendLayout()
        Me.pnlbtnAllergies.SuspendLayout()
        Me.pnlbtnC2.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlbtnAllDrugs.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.pnltrvDrugs.SuspendLayout()
        Me.pnlSearchLeft.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlbtnProvider.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        CType(Me.C1DrugstoProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_ProviderName.SuspendLayout()
        Me.pnlDrugSigCtrl.SuspendLayout()
        Me.tlsp_MSTSIG.SuspendLayout()
        Me.cntxDeleteDrugs.SuspendLayout()
        Me.ContextMenuStrip_Sig.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.tblStrip_32)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(878, 54)
        Me.pnlTop.TabIndex = 0
        '
        'tblStrip_32
        '
        Me.tblStrip_32.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip_32.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblStrip_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblStrip_32.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip_32.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_Save_32, Me.tblbtn_Finish_32, Me.tblbtn_Close_32})
        Me.tblStrip_32.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tblStrip_32.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip_32.Name = "tblStrip_32"
        Me.tblStrip_32.Size = New System.Drawing.Size(878, 53)
        Me.tblStrip_32.TabIndex = 0
        Me.tblStrip_32.Text = "ToolStrip1"
        '
        'tblbtn_Save_32
        '
        Me.tblbtn_Save_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Save_32.Image = CType(resources.GetObject("tblbtn_Save_32.Image"), System.Drawing.Image)
        Me.tblbtn_Save_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Save_32.Name = "tblbtn_Save_32"
        Me.tblbtn_Save_32.Size = New System.Drawing.Size(66, 50)
        Me.tblbtn_Save_32.Text = "&Save&&Cls"
        Me.tblbtn_Save_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Save_32.ToolTipText = "Save and Close"
        '
        'tblbtn_Finish_32
        '
        Me.tblbtn_Finish_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Finish_32.Image = CType(resources.GetObject("tblbtn_Finish_32.Image"), System.Drawing.Image)
        Me.tblbtn_Finish_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Finish_32.Name = "tblbtn_Finish_32"
        Me.tblbtn_Finish_32.Size = New System.Drawing.Size(45, 50)
        Me.tblbtn_Finish_32.Text = "&Finish"
        Me.tblbtn_Finish_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Finish_32.Visible = False
        '
        'tblbtn_Close_32
        '
        Me.tblbtn_Close_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close_32.Image = CType(resources.GetObject("tblbtn_Close_32.Image"), System.Drawing.Image)
        Me.tblbtn_Close_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close_32.Name = "tblbtn_Close_32"
        Me.tblbtn_Close_32.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close_32.Text = "&Close"
        Me.tblbtn_Close_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlRight
        '
        Me.pnlRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlRight.Controls.Add(Me.Panel3)
        Me.pnlRight.Controls.Add(Me.pnlbtnClinical)
        Me.pnlRight.Controls.Add(Me.pnlbtnAllergies)
        Me.pnlRight.Controls.Add(Me.pnlbtnC2)
        Me.pnlRight.Controls.Add(Me.Panel6)
        Me.pnlRight.Controls.Add(Me.pnlbtnAllDrugs)
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlRight.Location = New System.Drawing.Point(658, 54)
        Me.pnlRight.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.pnlRight.Size = New System.Drawing.Size(220, 571)
        Me.pnlRight.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.GloUC_trvAllDrugs)
        Me.Panel3.Controls.Add(Me.trvAllDrugs)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 30)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.Panel3.Size = New System.Drawing.Size(220, 457)
        Me.Panel3.TabIndex = 1
        '
        'GloUC_trvAllDrugs
        '
        Me.GloUC_trvAllDrugs.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvAllDrugs.CheckBoxes = False
        Me.GloUC_trvAllDrugs.CodeMember = Nothing
        Me.GloUC_trvAllDrugs.ColonAsSeparator = False
        Me.GloUC_trvAllDrugs.Comment = Nothing
        Me.GloUC_trvAllDrugs.ConceptID = Nothing
        Me.GloUC_trvAllDrugs.CPT = Nothing

        Me.GloUC_trvAllDrugs.DescriptionMember = Nothing
        Me.GloUC_trvAllDrugs.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvAllDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvAllDrugs.DrugFlag = CType(16, Short)
        Me.GloUC_trvAllDrugs.DrugFormMember = Nothing
        Me.GloUC_trvAllDrugs.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvAllDrugs.DurationMember = Nothing
        Me.GloUC_trvAllDrugs.EducationMappingSearchType = 1
        Me.GloUC_trvAllDrugs.FrequencyMember = Nothing
        Me.GloUC_trvAllDrugs.HistoryType = Nothing
        Me.GloUC_trvAllDrugs.ICD9 = Nothing
        Me.GloUC_trvAllDrugs.ImageIndex = 0
        Me.GloUC_trvAllDrugs.ImageList = Me.ImageList1
        Me.GloUC_trvAllDrugs.ImageObject = Nothing
        Me.GloUC_trvAllDrugs.Indicator = Nothing
        Me.GloUC_trvAllDrugs.IsDrug = False
        Me.GloUC_trvAllDrugs.IsNarcoticsMember = Nothing
        Me.GloUC_trvAllDrugs.IsSearchForEducationMapping = False
        Me.GloUC_trvAllDrugs.IsSystemCategory = Nothing
        Me.GloUC_trvAllDrugs.Location = New System.Drawing.Point(0, 0)
        Me.GloUC_trvAllDrugs.MaximumNodes = 1000
        Me.GloUC_trvAllDrugs.Name = "GloUC_trvAllDrugs"
        Me.GloUC_trvAllDrugs.NDCCodeMember = Nothing
        Me.GloUC_trvAllDrugs.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.GloUC_trvAllDrugs.ParentImageIndex = 0
        Me.GloUC_trvAllDrugs.ParentMember = Nothing
        Me.GloUC_trvAllDrugs.RouteMember = Nothing
        Me.GloUC_trvAllDrugs.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Simple
        Me.GloUC_trvAllDrugs.SearchBox = True
        Me.GloUC_trvAllDrugs.SearchText = Nothing
        Me.GloUC_trvAllDrugs.SelectedImageIndex = 0
        Me.GloUC_trvAllDrugs.SelectedNode = Nothing
        Me.GloUC_trvAllDrugs.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvAllDrugs.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvAllDrugs.SelectedParentImageIndex = 0
        Me.GloUC_trvAllDrugs.Size = New System.Drawing.Size(217, 457)
        Me.GloUC_trvAllDrugs.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvAllDrugs.TabIndex = 0
        Me.GloUC_trvAllDrugs.Tag = Nothing
        Me.GloUC_trvAllDrugs.UnitMember = Nothing
        Me.GloUC_trvAllDrugs.ValueMember = Nothing
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "DoctorType.ico")
        Me.ImageList1.Images.SetKeyName(1, "Drugs.ico")
        Me.ImageList1.Images.SetKeyName(2, "Bullet06.ico")
        Me.ImageList1.Images.SetKeyName(3, "Small Arrow.ico")
        Me.ImageList1.Images.SetKeyName(4, "Red Arrow.ico")
        Me.ImageList1.Images.SetKeyName(5, "Green Arrow.ico")
        Me.ImageList1.Images.SetKeyName(6, "Provider Specific Drugs.ico")
        '
        'trvAllDrugs
        '
        Me.trvAllDrugs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvAllDrugs.ForeColor = System.Drawing.Color.Black
        Me.trvAllDrugs.HideSelection = False
        Me.trvAllDrugs.ImageIndex = 0
        Me.trvAllDrugs.ImageList = Me.ImageList1
        Me.trvAllDrugs.Indent = 20
        Me.trvAllDrugs.ItemHeight = 20
        Me.trvAllDrugs.Location = New System.Drawing.Point(1, 5)
        Me.trvAllDrugs.Name = "trvAllDrugs"
        Me.trvAllDrugs.SelectedImageIndex = 0
        Me.trvAllDrugs.ShowNodeToolTips = True
        Me.trvAllDrugs.Size = New System.Drawing.Size(205, 477)
        Me.trvAllDrugs.TabIndex = 9
        Me.trvAllDrugs.Visible = False
        '
        'pnlbtnClinical
        '
        Me.pnlbtnClinical.Controls.Add(Me.btnClinical)
        Me.pnlbtnClinical.Controls.Add(Me.Label41)
        Me.pnlbtnClinical.Controls.Add(Me.Label42)
        Me.pnlbtnClinical.Controls.Add(Me.Label43)
        Me.pnlbtnClinical.Controls.Add(Me.Label44)
        Me.pnlbtnClinical.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnClinical.Location = New System.Drawing.Point(0, 487)
        Me.pnlbtnClinical.Name = "pnlbtnClinical"
        Me.pnlbtnClinical.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnClinical.Size = New System.Drawing.Size(220, 28)
        Me.pnlbtnClinical.TabIndex = 2
        '
        'btnClinical
        '
        Me.btnClinical.BackgroundImage = CType(resources.GetObject("btnClinical.BackgroundImage"), System.Drawing.Image)
        Me.btnClinical.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClinical.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnClinical.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClinical.FlatAppearance.BorderSize = 0
        Me.btnClinical.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClinical.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClinical.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClinical.Location = New System.Drawing.Point(1, 1)
        Me.btnClinical.Name = "btnClinical"
        Me.btnClinical.Size = New System.Drawing.Size(215, 23)
        Me.btnClinical.TabIndex = 0
        Me.btnClinical.Text = "Practice Favorites"
        Me.btnClinical.UseVisualStyleBackColor = True
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label41.Location = New System.Drawing.Point(1, 24)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(215, 1)
        Me.Label41.TabIndex = 12
        Me.Label41.Text = "label2"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(0, 1)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(1, 24)
        Me.Label42.TabIndex = 11
        Me.Label42.Text = "label4"
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label43.Location = New System.Drawing.Point(216, 1)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(1, 24)
        Me.Label43.TabIndex = 10
        Me.Label43.Text = "label3"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(0, 0)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(217, 1)
        Me.Label44.TabIndex = 9
        Me.Label44.Text = "label1"
        '
        'pnlbtnAllergies
        '
        Me.pnlbtnAllergies.Controls.Add(Me.btnAllergies)
        Me.pnlbtnAllergies.Controls.Add(Me.Label5)
        Me.pnlbtnAllergies.Controls.Add(Me.Label6)
        Me.pnlbtnAllergies.Controls.Add(Me.Label7)
        Me.pnlbtnAllergies.Controls.Add(Me.Label10)
        Me.pnlbtnAllergies.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnAllergies.Location = New System.Drawing.Point(0, 515)
        Me.pnlbtnAllergies.Name = "pnlbtnAllergies"
        Me.pnlbtnAllergies.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnAllergies.Size = New System.Drawing.Size(220, 28)
        Me.pnlbtnAllergies.TabIndex = 3
        '
        'btnAllergies
        '
        Me.btnAllergies.BackgroundImage = CType(resources.GetObject("btnAllergies.BackgroundImage"), System.Drawing.Image)
        Me.btnAllergies.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAllergies.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnAllergies.FlatAppearance.BorderSize = 0
        Me.btnAllergies.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAllergies.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAllergies.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnAllergies.Location = New System.Drawing.Point(1, 1)
        Me.btnAllergies.Name = "btnAllergies"
        Me.btnAllergies.Size = New System.Drawing.Size(215, 23)
        Me.btnAllergies.TabIndex = 0
        Me.btnAllergies.Text = "Allergic Drugs"
        Me.btnAllergies.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(215, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 24)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(216, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 24)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(217, 1)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "label1"
        '
        'pnlbtnC2
        '
        Me.pnlbtnC2.Controls.Add(Me.btnC2)
        Me.pnlbtnC2.Controls.Add(Me.Label33)
        Me.pnlbtnC2.Controls.Add(Me.Label34)
        Me.pnlbtnC2.Controls.Add(Me.Label35)
        Me.pnlbtnC2.Controls.Add(Me.Label36)
        Me.pnlbtnC2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnC2.Location = New System.Drawing.Point(0, 543)
        Me.pnlbtnC2.Name = "pnlbtnC2"
        Me.pnlbtnC2.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnC2.Size = New System.Drawing.Size(220, 28)
        Me.pnlbtnC2.TabIndex = 4
        '
        'btnC2
        '
        Me.btnC2.BackgroundImage = CType(resources.GetObject("btnC2.BackgroundImage"), System.Drawing.Image)
        Me.btnC2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnC2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnC2.FlatAppearance.BorderSize = 0
        Me.btnC2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnC2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnC2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnC2.Location = New System.Drawing.Point(1, 1)
        Me.btnC2.Name = "btnC2"
        Me.btnC2.Size = New System.Drawing.Size(215, 23)
        Me.btnC2.TabIndex = 0
        Me.btnC2.Text = "Schedule II"
        Me.btnC2.UseVisualStyleBackColor = True
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label33.Location = New System.Drawing.Point(1, 24)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(215, 1)
        Me.Label33.TabIndex = 12
        Me.Label33.Text = "label2"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(0, 1)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(1, 24)
        Me.Label34.TabIndex = 11
        Me.Label34.Text = "label4"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label35.Location = New System.Drawing.Point(216, 1)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(1, 24)
        Me.Label35.TabIndex = 10
        Me.Label35.Text = "label3"
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(0, 0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(217, 1)
        Me.Label36.TabIndex = 9
        Me.Label36.Text = "label1"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel6.Controls.Add(Me.txtSearchDrugs)
        Me.Panel6.Controls.Add(Me.Label20)
        Me.Panel6.Controls.Add(Me.Label21)
        Me.Panel6.Controls.Add(Me.PictureBox1)
        Me.Panel6.Controls.Add(Me.label9)
        Me.Panel6.Controls.Add(Me.Label12)
        Me.Panel6.Controls.Add(Me.Label16)
        Me.Panel6.Controls.Add(Me.Label17)
        Me.Panel6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel6.ForeColor = System.Drawing.Color.Black
        Me.Panel6.Location = New System.Drawing.Point(0, 31)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel6.Size = New System.Drawing.Size(210, 26)
        Me.Panel6.TabIndex = 0
        Me.Panel6.Visible = False
        '
        'txtSearchDrugs
        '
        Me.txtSearchDrugs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchDrugs.ForeColor = System.Drawing.Color.Black
        Me.txtSearchDrugs.Location = New System.Drawing.Point(29, 5)
        Me.txtSearchDrugs.Multiline = True
        Me.txtSearchDrugs.Name = "txtSearchDrugs"
        Me.txtSearchDrugs.Size = New System.Drawing.Size(177, 15)
        Me.txtSearchDrugs.TabIndex = 8
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.White
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(29, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(177, 4)
        Me.Label20.TabIndex = 37
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Location = New System.Drawing.Point(29, 20)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(177, 2)
        Me.Label21.TabIndex = 38
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'label9
        '
        Me.label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label9.Location = New System.Drawing.Point(1, 22)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(205, 1)
        Me.label9.TabIndex = 35
        Me.label9.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(1, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(205, 1)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "label1"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Location = New System.Drawing.Point(206, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 23)
        Me.Label16.TabIndex = 39
        Me.Label16.Text = "label3"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Location = New System.Drawing.Point(0, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 23)
        Me.Label17.TabIndex = 40
        Me.Label17.Text = "label3"
        '
        'pnlbtnAllDrugs
        '
        Me.pnlbtnAllDrugs.Controls.Add(Me.btnAllDrugs)
        Me.pnlbtnAllDrugs.Controls.Add(Me.Label18)
        Me.pnlbtnAllDrugs.Controls.Add(Me.Label19)
        Me.pnlbtnAllDrugs.Controls.Add(Me.Label22)
        Me.pnlbtnAllDrugs.Controls.Add(Me.Label23)
        Me.pnlbtnAllDrugs.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbtnAllDrugs.Location = New System.Drawing.Point(0, 2)
        Me.pnlbtnAllDrugs.Name = "pnlbtnAllDrugs"
        Me.pnlbtnAllDrugs.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnAllDrugs.Size = New System.Drawing.Size(220, 28)
        Me.pnlbtnAllDrugs.TabIndex = 0
        '
        'btnAllDrugs
        '
        Me.btnAllDrugs.BackgroundImage = CType(resources.GetObject("btnAllDrugs.BackgroundImage"), System.Drawing.Image)
        Me.btnAllDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAllDrugs.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnAllDrugs.FlatAppearance.BorderSize = 0
        Me.btnAllDrugs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAllDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAllDrugs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnAllDrugs.Location = New System.Drawing.Point(1, 1)
        Me.btnAllDrugs.Name = "btnAllDrugs"
        Me.btnAllDrugs.Size = New System.Drawing.Size(215, 23)
        Me.btnAllDrugs.TabIndex = 0
        Me.btnAllDrugs.Text = "All Drugs"
        Me.btnAllDrugs.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(1, 24)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(215, 1)
        Me.Label18.TabIndex = 12
        Me.Label18.Text = "label2"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(0, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 24)
        Me.Label19.TabIndex = 11
        Me.Label19.Text = "label4"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(216, 1)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 24)
        Me.Label22.TabIndex = 10
        Me.Label22.Text = "label3"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(217, 1)
        Me.Label23.TabIndex = 9
        Me.Label23.Text = "label1"
        '
        'pnlLeft
        '
        Me.pnlLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeft.Controls.Add(Me.pnltrvDrugs)
        Me.pnlLeft.Controls.Add(Me.pnlSearchLeft)
        Me.pnlLeft.Controls.Add(Me.pnlbtnProvider)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 54)
        Me.pnlLeft.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.pnlLeft.Size = New System.Drawing.Size(220, 571)
        Me.pnlLeft.TabIndex = 0
        '
        'pnltrvDrugs
        '
        Me.pnltrvDrugs.Controls.Add(Me.GloUC_trvDrugs)
        Me.pnltrvDrugs.Controls.Add(Me.trvDrugs)
        Me.pnltrvDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrvDrugs.Location = New System.Drawing.Point(0, 30)
        Me.pnltrvDrugs.Name = "pnltrvDrugs"
        Me.pnltrvDrugs.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnltrvDrugs.Size = New System.Drawing.Size(220, 541)
        Me.pnltrvDrugs.TabIndex = 1
        '
        'GloUC_trvDrugs
        '
        Me.GloUC_trvDrugs.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvDrugs.CheckBoxes = False
        Me.GloUC_trvDrugs.CodeMember = Nothing
        Me.GloUC_trvDrugs.ColonAsSeparator = False
        Me.GloUC_trvDrugs.Comment = Nothing
        Me.GloUC_trvDrugs.ConceptID = Nothing
        Me.GloUC_trvDrugs.CPT = Nothing

        Me.GloUC_trvDrugs.DescriptionMember = Nothing
        Me.GloUC_trvDrugs.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvDrugs.DrugFlag = CType(16, Short)
        Me.GloUC_trvDrugs.DrugFormMember = Nothing
        Me.GloUC_trvDrugs.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvDrugs.DurationMember = Nothing
        Me.GloUC_trvDrugs.EducationMappingSearchType = 1
        Me.GloUC_trvDrugs.FrequencyMember = Nothing
        Me.GloUC_trvDrugs.HistoryType = Nothing
        Me.GloUC_trvDrugs.ICD9 = Nothing
        Me.GloUC_trvDrugs.ImageIndex = 0
        Me.GloUC_trvDrugs.ImageList = Me.ImageList1
        Me.GloUC_trvDrugs.ImageObject = Nothing
        Me.GloUC_trvDrugs.Indicator = Nothing
        Me.GloUC_trvDrugs.IsDrug = False
        Me.GloUC_trvDrugs.IsNarcoticsMember = Nothing
        Me.GloUC_trvDrugs.IsSearchForEducationMapping = False
        Me.GloUC_trvDrugs.IsSystemCategory = Nothing
        Me.GloUC_trvDrugs.Location = New System.Drawing.Point(3, 0)
        Me.GloUC_trvDrugs.MaximumNodes = 1000
        Me.GloUC_trvDrugs.Name = "GloUC_trvDrugs"
        Me.GloUC_trvDrugs.NDCCodeMember = Nothing
        Me.GloUC_trvDrugs.ParentImageIndex = 0
        Me.GloUC_trvDrugs.ParentMember = Nothing
        Me.GloUC_trvDrugs.RouteMember = Nothing
        Me.GloUC_trvDrugs.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvDrugs.SearchBox = True
        Me.GloUC_trvDrugs.SearchText = Nothing
        Me.GloUC_trvDrugs.SelectedImageIndex = 0
        Me.GloUC_trvDrugs.SelectedNode = Nothing
        Me.GloUC_trvDrugs.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvDrugs.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvDrugs.SelectedParentImageIndex = 0
        Me.GloUC_trvDrugs.Size = New System.Drawing.Size(217, 538)
        Me.GloUC_trvDrugs.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvDrugs.TabIndex = 0
        Me.GloUC_trvDrugs.Tag = Nothing
        Me.GloUC_trvDrugs.UnitMember = Nothing
        Me.GloUC_trvDrugs.ValueMember = Nothing
        '
        'trvDrugs
        '
        Me.trvDrugs.BackColor = System.Drawing.Color.White
        Me.trvDrugs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvDrugs.ForeColor = System.Drawing.Color.Black
        Me.trvDrugs.HideSelection = False
        Me.trvDrugs.ImageIndex = 0
        Me.trvDrugs.ImageList = Me.ImageList1
        Me.trvDrugs.Indent = 20
        Me.trvDrugs.ItemHeight = 20
        Me.trvDrugs.Location = New System.Drawing.Point(4, 4)
        Me.trvDrugs.Name = "trvDrugs"
        Me.trvDrugs.SelectedImageIndex = 0
        Me.trvDrugs.Size = New System.Drawing.Size(205, 422)
        Me.trvDrugs.TabIndex = 3
        Me.trvDrugs.Visible = False
        '
        'pnlSearchLeft
        '
        Me.pnlSearchLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSearchLeft.Controls.Add(Me.txtSearch)
        Me.pnlSearchLeft.Controls.Add(Me.Label1)
        Me.pnlSearchLeft.Controls.Add(Me.Label2)
        Me.pnlSearchLeft.Controls.Add(Me.PictureBox2)
        Me.pnlSearchLeft.Controls.Add(Me.Label3)
        Me.pnlSearchLeft.Controls.Add(Me.Label4)
        Me.pnlSearchLeft.Controls.Add(Me.Label8)
        Me.pnlSearchLeft.Controls.Add(Me.Label24)
        Me.pnlSearchLeft.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearchLeft.ForeColor = System.Drawing.Color.Black
        Me.pnlSearchLeft.Location = New System.Drawing.Point(0, 31)
        Me.pnlSearchLeft.Name = "pnlSearchLeft"
        Me.pnlSearchLeft.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlSearchLeft.Size = New System.Drawing.Size(210, 26)
        Me.pnlSearchLeft.TabIndex = 0
        Me.pnlSearchLeft.Visible = False
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(32, 5)
        Me.txtSearch.Multiline = True
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(177, 15)
        Me.txtSearch.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Location = New System.Drawing.Point(32, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(177, 4)
        Me.Label1.TabIndex = 37
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(32, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(177, 2)
        Me.Label2.TabIndex = 38
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.White
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(4, 1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(28, 21)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 9
        Me.PictureBox2.TabStop = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(4, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(205, 1)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 22)
        Me.Label4.TabIndex = 41
        Me.Label4.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(209, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 22)
        Me.Label8.TabIndex = 40
        Me.Label8.Text = "label3"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(3, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(207, 1)
        Me.Label24.TabIndex = 39
        Me.Label24.Text = "label1"
        '
        'pnlbtnProvider
        '
        Me.pnlbtnProvider.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlbtnProvider.Controls.Add(Me.btnProvider)
        Me.pnlbtnProvider.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlbtnProvider.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlbtnProvider.Controls.Add(Me.lbl_pnlRight)
        Me.pnlbtnProvider.Controls.Add(Me.lbl_pnlTop)
        Me.pnlbtnProvider.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbtnProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnProvider.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlbtnProvider.Location = New System.Drawing.Point(0, 2)
        Me.pnlbtnProvider.Name = "pnlbtnProvider"
        Me.pnlbtnProvider.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnlbtnProvider.Size = New System.Drawing.Size(220, 28)
        Me.pnlbtnProvider.TabIndex = 0
        '
        'btnProvider
        '
        Me.btnProvider.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnProvider.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnProvider.FlatAppearance.BorderSize = 0
        Me.btnProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProvider.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnProvider.Location = New System.Drawing.Point(4, 1)
        Me.btnProvider.Name = "btnProvider"
        Me.btnProvider.Size = New System.Drawing.Size(215, 23)
        Me.btnProvider.TabIndex = 0
        Me.btnProvider.Text = "Provider Favorite Drugs"
        Me.btnProvider.UseVisualStyleBackColor = True
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 24)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(215, 1)
        Me.lbl_pnlBottom.TabIndex = 4
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 1)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 24)
        Me.lbl_pnlLeft.TabIndex = 3
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(219, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 24)
        Me.lbl_pnlRight.TabIndex = 2
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(217, 1)
        Me.lbl_pnlTop.TabIndex = 0
        Me.lbl_pnlTop.Text = "label1"
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.C1DrugstoProvider)
        Me.pnlMain.Controls.Add(Me.pnl_ProviderName)
        Me.pnlMain.Controls.Add(Me.pnlDrugSigCtrl)
        Me.pnlMain.Controls.Add(Me.trvAddDrugsToProvider)
        Me.pnlMain.Controls.Add(Me.Label11)
        Me.pnlMain.Controls.Add(Me.Label13)
        Me.pnlMain.Controls.Add(Me.Label14)
        Me.pnlMain.Controls.Add(Me.Label15)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(223, 54)
        Me.pnlMain.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(0, 2, 0, 3)
        Me.pnlMain.Size = New System.Drawing.Size(432, 571)
        Me.pnlMain.TabIndex = 2
        '
        'C1DrugstoProvider
        '
        Me.C1DrugstoProvider.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1DrugstoProvider.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1DrugstoProvider.BackColor = System.Drawing.Color.White
        Me.C1DrugstoProvider.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1DrugstoProvider.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1DrugstoProvider.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1DrugstoProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1DrugstoProvider.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1DrugstoProvider.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1DrugstoProvider.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1DrugstoProvider.Location = New System.Drawing.Point(1, 26)
        Me.C1DrugstoProvider.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1DrugstoProvider.Name = "C1DrugstoProvider"
        Me.C1DrugstoProvider.Rows.Count = 1
        Me.C1DrugstoProvider.Rows.DefaultSize = 19
        Me.C1DrugstoProvider.Rows.Fixed = 0
        Me.C1DrugstoProvider.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1DrugstoProvider.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1DrugstoProvider.ShowCellLabels = True
        Me.C1DrugstoProvider.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1DrugstoProvider.Size = New System.Drawing.Size(430, 241)
        Me.C1DrugstoProvider.StyleInfo = resources.GetString("C1DrugstoProvider.StyleInfo")
        Me.C1DrugstoProvider.TabIndex = 40
        '
        'pnl_ProviderName
        '
        Me.pnl_ProviderName.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnl_ProviderName.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_ProviderName.Controls.Add(Me.Label31)
        Me.pnl_ProviderName.Controls.Add(Me.lbl_ProviderName)
        Me.pnl_ProviderName.Controls.Add(Me.lblProviderCaption)
        Me.pnl_ProviderName.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_ProviderName.Location = New System.Drawing.Point(1, 3)
        Me.pnl_ProviderName.Name = "pnl_ProviderName"
        Me.pnl_ProviderName.Size = New System.Drawing.Size(430, 23)
        Me.pnl_ProviderName.TabIndex = 42
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label31.Location = New System.Drawing.Point(0, 22)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(430, 1)
        Me.Label31.TabIndex = 5
        Me.Label31.Text = "label2"
        '
        'lbl_ProviderName
        '
        Me.lbl_ProviderName.AutoSize = True
        Me.lbl_ProviderName.BackColor = System.Drawing.Color.Transparent
        Me.lbl_ProviderName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ProviderName.Location = New System.Drawing.Point(116, 4)
        Me.lbl_ProviderName.Name = "lbl_ProviderName"
        Me.lbl_ProviderName.Size = New System.Drawing.Size(11, 14)
        Me.lbl_ProviderName.TabIndex = 1
        Me.lbl_ProviderName.Text = " "
        '
        'lblProviderCaption
        '
        Me.lblProviderCaption.AutoSize = True
        Me.lblProviderCaption.BackColor = System.Drawing.Color.Transparent
        Me.lblProviderCaption.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProviderCaption.Location = New System.Drawing.Point(7, 4)
        Me.lblProviderCaption.Name = "lblProviderCaption"
        Me.lblProviderCaption.Size = New System.Drawing.Size(103, 14)
        Me.lblProviderCaption.TabIndex = 0
        Me.lblProviderCaption.Text = "Provider Name :"
        '
        'pnlDrugSigCtrl
        '
        Me.pnlDrugSigCtrl.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlDrugSigCtrl.Controls.Add(Me.cmbDoseUnit)
        Me.pnlDrugSigCtrl.Controls.Add(Me.txtDoseUnit)
        Me.pnlDrugSigCtrl.Controls.Add(Me.Label32)
        Me.pnlDrugSigCtrl.Controls.Add(Me.txtAmount)
        Me.pnlDrugSigCtrl.Controls.Add(Me.lblNDCCodetxt)
        Me.pnlDrugSigCtrl.Controls.Add(Me.lblNDCCode)
        Me.pnlDrugSigCtrl.Controls.Add(Me.lblDrugNametxt)
        Me.pnlDrugSigCtrl.Controls.Add(Me.Label30)
        Me.pnlDrugSigCtrl.Controls.Add(Me.Label25)
        Me.pnlDrugSigCtrl.Controls.Add(Me.txtRefills)
        Me.pnlDrugSigCtrl.Controls.Add(Me.lblRefills)
        Me.pnlDrugSigCtrl.Controls.Add(Me.cmbDuration)
        Me.pnlDrugSigCtrl.Controls.Add(Me.txtFrequency)
        Me.pnlDrugSigCtrl.Controls.Add(Me.txtRoute)
        Me.pnlDrugSigCtrl.Controls.Add(Me.Label26)
        Me.pnlDrugSigCtrl.Controls.Add(Me.txtDuration)
        Me.pnlDrugSigCtrl.Controls.Add(Me.Label27)
        Me.pnlDrugSigCtrl.Controls.Add(Me.Label28)
        Me.pnlDrugSigCtrl.Controls.Add(Me.tlsp_MSTSIG)
        Me.pnlDrugSigCtrl.Controls.Add(Me.Splitter3)
        Me.pnlDrugSigCtrl.Controls.Add(Me.Label29)
        Me.pnlDrugSigCtrl.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlDrugSigCtrl.Location = New System.Drawing.Point(1, 267)
        Me.pnlDrugSigCtrl.Name = "pnlDrugSigCtrl"
        Me.pnlDrugSigCtrl.Size = New System.Drawing.Size(430, 300)
        Me.pnlDrugSigCtrl.TabIndex = 41
        Me.pnlDrugSigCtrl.Visible = False
        '
        'txtDoseUnit
        '
        Me.txtDoseUnit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDoseUnit.ForeColor = System.Drawing.Color.Black
        Me.txtDoseUnit.Location = New System.Drawing.Point(196, 270)
        Me.txtDoseUnit.MaxLength = 30
        Me.txtDoseUnit.Name = "txtDoseUnit"
        Me.txtDoseUnit.ShortcutsEnabled = False
        Me.txtDoseUnit.Size = New System.Drawing.Size(58, 22)
        Me.txtDoseUnit.TabIndex = 50
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(55, 274)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(62, 14)
        Me.Label32.TabIndex = 48
        Me.Label32.Text = "Quantity :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAmount
        '
        Me.txtAmount.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.ForeColor = System.Drawing.Color.Black
        Me.txtAmount.Location = New System.Drawing.Point(120, 270)
        Me.txtAmount.MaxLength = 15
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(70, 22)
        Me.txtAmount.TabIndex = 49
        '
        'lblNDCCodetxt
        '
        Me.lblNDCCodetxt.AutoSize = True
        Me.lblNDCCodetxt.BackColor = System.Drawing.Color.Transparent
        Me.lblNDCCodetxt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNDCCodetxt.Location = New System.Drawing.Point(123, 75)
        Me.lblNDCCodetxt.Name = "lblNDCCodetxt"
        Me.lblNDCCodetxt.Size = New System.Drawing.Size(11, 14)
        Me.lblNDCCodetxt.TabIndex = 35
        Me.lblNDCCodetxt.Text = " "
        Me.lblNDCCodetxt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblNDCCode
        '
        Me.lblNDCCode.AutoSize = True
        Me.lblNDCCode.BackColor = System.Drawing.Color.Transparent
        Me.lblNDCCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNDCCode.Location = New System.Drawing.Point(47, 75)
        Me.lblNDCCode.Name = "lblNDCCode"
        Me.lblNDCCode.Size = New System.Drawing.Size(70, 14)
        Me.lblNDCCode.TabIndex = 34
        Me.lblNDCCode.Text = "NDC Code :"
        Me.lblNDCCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDrugNametxt
        '
        Me.lblDrugNametxt.AutoSize = True
        Me.lblDrugNametxt.BackColor = System.Drawing.Color.Transparent
        Me.lblDrugNametxt.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDrugNametxt.Location = New System.Drawing.Point(122, 104)
        Me.lblDrugNametxt.Name = "lblDrugNametxt"
        Me.lblDrugNametxt.Size = New System.Drawing.Size(11, 14)
        Me.lblDrugNametxt.TabIndex = 33
        Me.lblDrugNametxt.Text = " "
        Me.lblDrugNametxt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(41, 104)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(76, 14)
        Me.Label30.TabIndex = 32
        Me.Label30.Text = "Drug Name :"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.ForeColor = System.Drawing.Color.Red
        Me.Label25.Location = New System.Drawing.Point(58, 136)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(14, 14)
        Me.Label25.TabIndex = 30
        Me.Label25.Text = "*"
        '
        'txtRefills
        '
        Me.txtRefills.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefills.ForeColor = System.Drawing.Color.Black
        Me.txtRefills.Location = New System.Drawing.Point(120, 239)
        Me.txtRefills.MaxLength = 2
        Me.txtRefills.Name = "txtRefills"
        Me.txtRefills.Size = New System.Drawing.Size(238, 22)
        Me.txtRefills.TabIndex = 24
        Me.txtRefills.Text = "0"
        '
        'lblRefills
        '
        Me.lblRefills.AutoSize = True
        Me.lblRefills.BackColor = System.Drawing.Color.Transparent
        Me.lblRefills.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRefills.Location = New System.Drawing.Point(35, 243)
        Me.lblRefills.Name = "lblRefills"
        Me.lblRefills.Size = New System.Drawing.Size(82, 14)
        Me.lblRefills.TabIndex = 29
        Me.lblRefills.Text = "No. of Refills :"
        Me.lblRefills.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbDuration
        '
        Me.cmbDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDuration.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDuration.ForeColor = System.Drawing.Color.Black
        Me.cmbDuration.Location = New System.Drawing.Point(263, 204)
        Me.cmbDuration.Name = "cmbDuration"
        Me.cmbDuration.Size = New System.Drawing.Size(95, 22)
        Me.cmbDuration.TabIndex = 23
        '
        'txtFrequency
        '
        Me.txtFrequency.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFrequency.ForeColor = System.Drawing.Color.Black
        Me.txtFrequency.Location = New System.Drawing.Point(120, 169)
        Me.txtFrequency.MaxLength = 255
        Me.txtFrequency.Name = "txtFrequency"
        Me.txtFrequency.Size = New System.Drawing.Size(238, 22)
        Me.txtFrequency.TabIndex = 21
        '
        'txtRoute
        '
        Me.txtRoute.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoute.ForeColor = System.Drawing.Color.Black
        Me.txtRoute.Location = New System.Drawing.Point(120, 134)
        Me.txtRoute.MaxLength = 50
        Me.txtRoute.Name = "txtRoute"
        Me.txtRoute.Size = New System.Drawing.Size(238, 22)
        Me.txtRoute.TabIndex = 20
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(56, 208)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(61, 14)
        Me.Label26.TabIndex = 26
        Me.Label26.Text = "Duration :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDuration
        '
        Me.txtDuration.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDuration.ForeColor = System.Drawing.Color.Black
        Me.txtDuration.Location = New System.Drawing.Point(120, 204)
        Me.txtDuration.MaxLength = 3
        Me.txtDuration.Name = "txtDuration"
        Me.txtDuration.ShortcutsEnabled = False
        Me.txtDuration.Size = New System.Drawing.Size(139, 22)
        Me.txtDuration.TabIndex = 22
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(69, 138)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(48, 14)
        Me.Label27.TabIndex = 28
        Me.Label27.Text = "Route :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(6, 173)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(111, 14)
        Me.Label28.TabIndex = 27
        Me.Label28.Text = "Patient Directions :"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tlsp_MSTSIG
        '
        Me.tlsp_MSTSIG.BackColor = System.Drawing.Color.Transparent
        Me.tlsp_MSTSIG.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsp_MSTSIG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsp_MSTSIG.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsp_MSTSIG.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsp_MSTSIG.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.tlsp_MSTSIG.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsp_MSTSIG.Location = New System.Drawing.Point(0, 4)
        Me.tlsp_MSTSIG.Name = "tlsp_MSTSIG"
        Me.tlsp_MSTSIG.Size = New System.Drawing.Size(430, 53)
        Me.tlsp_MSTSIG.TabIndex = 6
        Me.tlsp_MSTSIG.Text = "toolStrip1"
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSave.Image = CType(resources.GetObject("ts_btnSave.Image"), System.Drawing.Image)
        Me.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSave.Name = "ts_btnSave"
        Me.ts_btnSave.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnSave.Tag = "Save"
        Me.ts_btnSave.Text = "&Save&&Cls"
        Me.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnSave.ToolTipText = "Save and Close"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Splitter3
        '
        Me.Splitter3.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter3.Location = New System.Drawing.Point(0, 1)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(430, 3)
        Me.Splitter3.TabIndex = 5
        Me.Splitter3.TabStop = False
        Me.Splitter3.Visible = False
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label29.Location = New System.Drawing.Point(0, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(430, 1)
        Me.Label29.TabIndex = 31
        Me.Label29.Text = "label1"
        '
        'trvAddDrugsToProvider
        '
        Me.trvAddDrugsToProvider.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvAddDrugsToProvider.ForeColor = System.Drawing.Color.Black
        Me.trvAddDrugsToProvider.HideSelection = False
        Me.trvAddDrugsToProvider.ImageIndex = 2
        Me.trvAddDrugsToProvider.ImageList = Me.ImageList1
        Me.trvAddDrugsToProvider.Indent = 20
        Me.trvAddDrugsToProvider.ItemHeight = 20
        Me.trvAddDrugsToProvider.Location = New System.Drawing.Point(1, 193)
        Me.trvAddDrugsToProvider.Name = "trvAddDrugsToProvider"
        Me.trvAddDrugsToProvider.SelectedImageIndex = 2
        Me.trvAddDrugsToProvider.Size = New System.Drawing.Size(450, 374)
        Me.trvAddDrugsToProvider.TabIndex = 0
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Location = New System.Drawing.Point(1, 567)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(430, 1)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "label2"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Location = New System.Drawing.Point(0, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 565)
        Me.Label13.TabIndex = 7
        Me.Label13.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Location = New System.Drawing.Point(431, 3)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 565)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "label3"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Location = New System.Drawing.Point(0, 2)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(432, 1)
        Me.Label15.TabIndex = 5
        Me.Label15.Text = "label1"
        '
        'cntxDeleteDrugs
        '
        Me.cntxDeleteDrugs.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem})
        Me.cntxDeleteDrugs.Name = "ContextMenuStrip1"
        Me.cntxDeleteDrugs.Size = New System.Drawing.Size(117, 26)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(220, 54)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 571)
        Me.Splitter1.TabIndex = 4
        Me.Splitter1.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter2.Location = New System.Drawing.Point(655, 54)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 571)
        Me.Splitter2.TabIndex = 5
        Me.Splitter2.TabStop = False
        '
        'ContextMenuStrip_Sig
        '
        Me.ContextMenuStrip_Sig.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddNewSigInfoToolStripMenuItem, Me.ModifySigInfoToolStripMenuItem, Me.RemoveDrugToolStripMenuItem})
        Me.ContextMenuStrip_Sig.Name = "ContextMenuStrip_Sig"
        Me.ContextMenuStrip_Sig.Size = New System.Drawing.Size(175, 70)
        '
        'AddNewSigInfoToolStripMenuItem
        '
        Me.AddNewSigInfoToolStripMenuItem.Image = CType(resources.GetObject("AddNewSigInfoToolStripMenuItem.Image"), System.Drawing.Image)
        Me.AddNewSigInfoToolStripMenuItem.Name = "AddNewSigInfoToolStripMenuItem"
        Me.AddNewSigInfoToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.AddNewSigInfoToolStripMenuItem.Text = "Add New SIG Info "
        '
        'ModifySigInfoToolStripMenuItem
        '
        Me.ModifySigInfoToolStripMenuItem.Image = CType(resources.GetObject("ModifySigInfoToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ModifySigInfoToolStripMenuItem.Name = "ModifySigInfoToolStripMenuItem"
        Me.ModifySigInfoToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.ModifySigInfoToolStripMenuItem.Text = "Modify SIG Info"
        '
        'RemoveDrugToolStripMenuItem
        '
        Me.RemoveDrugToolStripMenuItem.Image = CType(resources.GetObject("RemoveDrugToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RemoveDrugToolStripMenuItem.Name = "RemoveDrugToolStripMenuItem"
        Me.RemoveDrugToolStripMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.RemoveDrugToolStripMenuItem.Text = "Remove"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'cmbDoseUnit
        '
        Me.cmbDoseUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDoseUnit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDoseUnit.ForeColor = System.Drawing.Color.Black
        Me.cmbDoseUnit.Location = New System.Drawing.Point(196, 270)
        Me.cmbDoseUnit.Name = "cmbDoseUnit"
        Me.cmbDoseUnit.Size = New System.Drawing.Size(162, 22)
        Me.cmbDoseUnit.TabIndex = 51
        '
        'frmDrugProviderAssociation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(878, 625)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.pnlRight)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "frmDrugProviderAssociation"
        Me.Text = "Drugs Configuration"
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tblStrip_32.ResumeLayout(False)
        Me.tblStrip_32.PerformLayout()
        Me.pnlRight.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlbtnClinical.ResumeLayout(False)
        Me.pnlbtnAllergies.ResumeLayout(False)
        Me.pnlbtnC2.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlbtnAllDrugs.ResumeLayout(False)
        Me.pnlLeft.ResumeLayout(False)
        Me.pnltrvDrugs.ResumeLayout(False)
        Me.pnlSearchLeft.ResumeLayout(False)
        Me.pnlSearchLeft.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlbtnProvider.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        CType(Me.C1DrugstoProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_ProviderName.ResumeLayout(False)
        Me.pnl_ProviderName.PerformLayout()
        Me.pnlDrugSigCtrl.ResumeLayout(False)
        Me.pnlDrugSigCtrl.PerformLayout()
        Me.tlsp_MSTSIG.ResumeLayout(False)
        Me.tlsp_MSTSIG.PerformLayout()
        Me.cntxDeleteDrugs.ResumeLayout(False)
        Me.ContextMenuStrip_Sig.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents tblStrip_32 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtn_Save_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtSearchDrugs As System.Windows.Forms.TextBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnProvider As System.Windows.Forms.Button
    Friend WithEvents btnClinical As System.Windows.Forms.Button
    Friend WithEvents btnAllDrugs As System.Windows.Forms.Button
    Friend WithEvents trvDrugs As System.Windows.Forms.TreeView
    Friend WithEvents trvAddDrugsToProvider As System.Windows.Forms.TreeView
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cntxDeleteDrugs As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tblbtn_Finish_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents pnlSearchLeft As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Private WithEvents pnlbtnProvider As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents pnltrvDrugs As System.Windows.Forms.Panel
    Friend WithEvents pnlbtnClinical As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents pnlbtnAllDrugs As System.Windows.Forms.Panel
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents GloUC_trvDrugs As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents GloUC_trvAllDrugs As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents trvAllDrugs As System.Windows.Forms.TreeView
    Friend WithEvents pnlbtnC2 As System.Windows.Forms.Panel
    Friend WithEvents btnC2 As System.Windows.Forms.Button
    Private WithEvents Label33 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents Label35 As System.Windows.Forms.Label
    Private WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnAllergies As System.Windows.Forms.Panel
    Friend WithEvents btnAllergies As System.Windows.Forms.Button
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents C1DrugstoProvider As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlDrugSigCtrl As System.Windows.Forms.Panel
    Friend WithEvents Splitter3 As System.Windows.Forms.Splitter
    Private WithEvents tlsp_MSTSIG As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtRefills As System.Windows.Forms.TextBox
    Friend WithEvents lblRefills As System.Windows.Forms.Label
    Friend WithEvents cmbDuration As System.Windows.Forms.ComboBox
    Friend WithEvents txtFrequency As System.Windows.Forms.TextBox
    Friend WithEvents txtRoute As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtDuration As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip_Sig As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddNewSigInfoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ModifySigInfoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblDrugNametxt As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents lblNDCCodetxt As System.Windows.Forms.Label
    Friend WithEvents lblNDCCode As System.Windows.Forms.Label
    Friend WithEvents pnl_ProviderName As System.Windows.Forms.Panel
    Friend WithEvents lbl_ProviderName As System.Windows.Forms.Label
    Friend WithEvents lblProviderCaption As System.Windows.Forms.Label
    Friend WithEvents RemoveDrugToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents txtDoseUnit As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents cmbDoseUnit As System.Windows.Forms.ComboBox
End Class
