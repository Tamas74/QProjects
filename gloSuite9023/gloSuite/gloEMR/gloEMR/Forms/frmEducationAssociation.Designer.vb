<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEducationAssociation
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Dim CmppControls() As System.Windows.Forms.ContextMenuStrip = {contextRemoveParent, contextMenus}

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
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
#Region " Windows Controls "
    Friend WithEvents tblMedication As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblFinish As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnltxtsearchDrugs As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnltrICD9 As System.Windows.Forms.Panel
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnProviderReference As System.Windows.Forms.Panel
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Private WithEvents Label47 As System.Windows.Forms.Label
    Private WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnPatientEducation As System.Windows.Forms.Panel
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label33 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents CodesTreeView As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents cntTags As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents ts_gloCommunityDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label50 As System.Windows.Forms.Label
#End Region
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.

    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents txtsearchDrugs As System.Windows.Forms.TextBox
    Friend WithEvents pnlCodesAssociation As System.Windows.Forms.Panel
    Friend WithEvents trICD9 As System.Windows.Forms.TreeView
    Friend WithEvents pnlRight As System.Windows.Forms.Panel
    Friend WithEvents btnProviderReference As System.Windows.Forms.Button
    Friend WithEvents btnPatientEducation As System.Windows.Forms.Button
    Friend WithEvents cntICD9Association As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuDeleteICD9Item As System.Windows.Forms.MenuItem
    Friend WithEvents pnltblMedication As System.Windows.Forms.Panel
    Friend WithEvents treeViewCodesAssociation As System.Windows.Forms.TreeView
    Friend WithEvents rbICD9Desc As System.Windows.Forms.RadioButton
    Friend WithEvents rbICD9Code As System.Windows.Forms.RadioButton
    'Friend WithEvents imgTreeView As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEducationAssociation))
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.CodesTreeView = New gloUserControlLibrary.gloUC_TreeView()
        Me.pnlLeftRadioBtnTop = New System.Windows.Forms.Panel()
        Me.pnlLeftRadioBtn = New System.Windows.Forms.Panel()
        Me.rbtUnassociated = New System.Windows.Forms.RadioButton()
        Me.rbtAssociated = New System.Windows.Forms.RadioButton()
        Me.rbtAll = New System.Windows.Forms.RadioButton()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.pnlICD10 = New System.Windows.Forms.Panel()
        Me.btnICD10 = New System.Windows.Forms.Button()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.pnlLabs = New System.Windows.Forms.Panel()
        Me.btnLabs = New System.Windows.Forms.Button()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.pnlMedication = New System.Windows.Forms.Panel()
        Me.btnMedication = New System.Windows.Forms.Button()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.pnlSnomed = New System.Windows.Forms.Panel()
        Me.pnlSnomed_add = New System.Windows.Forms.Panel()
        Me.btnSnoMed = New System.Windows.Forms.Button()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btn_AddSnoMed = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlICD9 = New System.Windows.Forms.Panel()
        Me.btnICD9 = New System.Windows.Forms.Button()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.pnltrICD9 = New System.Windows.Forms.Panel()
        Me.trICD9 = New System.Windows.Forms.TreeView()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.pnltxtsearchDrugs = New System.Windows.Forms.Panel()
        Me.txtsearchDrugs = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.label9 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.rbICD9Desc = New System.Windows.Forms.RadioButton()
        Me.rbICD9Code = New System.Windows.Forms.RadioButton()
        Me.pnlRight = New System.Windows.Forms.Panel()
        Me.UCtrlReferenceMaterial = New gloUserControlLibrary.gloUC_TreeView()
        Me.pnlbtnProviderReference = New System.Windows.Forms.Panel()
        Me.btnProviderReference = New System.Windows.Forms.Button()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.pnlbtnPatientEducation = New System.Windows.Forms.Panel()
        Me.btnPatientEducation = New System.Windows.Forms.Button()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.pnlCodesAssociation = New System.Windows.Forms.Panel()
        Me.treeViewCodesAssociation = New System.Windows.Forms.TreeView()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnltblMedication = New System.Windows.Forms.Panel()
        Me.tblMedication = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblNew = New System.Windows.Forms.ToolStripButton()
        Me.tblSave = New System.Windows.Forms.ToolStripButton()
        Me.tblFinish = New System.Windows.Forms.ToolStripButton()
        Me.ts_gloCommunityDownload = New System.Windows.Forms.ToolStripButton()
        Me.tblRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.cntICD9Association = New System.Windows.Forms.ContextMenu()
        Me.mnuDeleteICD9Item = New System.Windows.Forms.MenuItem()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.cntTags = New System.Windows.Forms.ContextMenu()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.imgTreeView = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlDemographicsFilter = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkAdvancedProviderReference = New System.Windows.Forms.CheckBox()
        Me.pnlCriteriaLab = New System.Windows.Forms.Panel()
        Me.txtValueOne = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbOperator = New System.Windows.Forms.ComboBox()
        Me.txtValueTwo = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.pnlCriteriaAge = New System.Windows.Forms.Panel()
        Me.cmbGender = New System.Windows.Forms.ComboBox()
        Me.Label155 = New System.Windows.Forms.Label()
        Me.Label119 = New System.Windows.Forms.Label()
        Me.cmbAgeMax = New System.Windows.Forms.ComboBox()
        Me.cmbAgeMin = New System.Windows.Forms.ComboBox()
        Me.Label157 = New System.Windows.Forms.Label()
        Me.lblAgeMax = New System.Windows.Forms.Label()
        Me.lblGender = New System.Windows.Forms.Label()
        Me.Label156 = New System.Windows.Forms.Label()
        Me.lblAgeMin = New System.Windows.Forms.Label()
        Me.pnlCriteriaCheckboxes = New System.Windows.Forms.Panel()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.chkEnableDemographics = New System.Windows.Forms.CheckBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.contextMenus = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RemoveAssociationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveDemographicsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.contextRemoveParent = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.toolStripParent = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlLeft.SuspendLayout()
        Me.pnlLeftRadioBtnTop.SuspendLayout()
        Me.pnlLeftRadioBtn.SuspendLayout()
        Me.pnlICD10.SuspendLayout()
        Me.pnlLabs.SuspendLayout()
        Me.pnlMedication.SuspendLayout()
        Me.pnlSnomed.SuspendLayout()
        Me.pnlSnomed_add.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlICD9.SuspendLayout()
        Me.pnltrICD9.SuspendLayout()
        Me.pnltxtsearchDrugs.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRight.SuspendLayout()
        Me.pnlbtnProviderReference.SuspendLayout()
        Me.pnlbtnPatientEducation.SuspendLayout()
        Me.pnlCodesAssociation.SuspendLayout()
        Me.pnltblMedication.SuspendLayout()
        Me.tblMedication.SuspendLayout()
        Me.pnlDemographicsFilter.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlCriteriaLab.SuspendLayout()
        Me.pnlCriteriaAge.SuspendLayout()
        Me.pnlCriteriaCheckboxes.SuspendLayout()
        Me.contextMenus.SuspendLayout()
        Me.contextRemoveParent.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlLeft
        '
        Me.pnlLeft.Controls.Add(Me.CodesTreeView)
        Me.pnlLeft.Controls.Add(Me.pnlLeftRadioBtnTop)
        Me.pnlLeft.Controls.Add(Me.pnlICD10)
        Me.pnlLeft.Controls.Add(Me.pnlLabs)
        Me.pnlLeft.Controls.Add(Me.pnlMedication)
        Me.pnlLeft.Controls.Add(Me.pnlSnomed)
        Me.pnlLeft.Controls.Add(Me.pnlICD9)
        Me.pnlLeft.Controls.Add(Me.pnltrICD9)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(0, 54)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.pnlLeft.Size = New System.Drawing.Size(334, 584)
        Me.pnlLeft.TabIndex = 1
        '
        'CodesTreeView
        '
        Me.CodesTreeView.AllergyClassID = Nothing
        Me.CodesTreeView.BackColor = System.Drawing.Color.Transparent
        Me.CodesTreeView.CheckBoxes = False
        Me.CodesTreeView.CodeMember = Nothing
        Me.CodesTreeView.ColonAsSeparator = False
        Me.CodesTreeView.Comment = Nothing
        Me.CodesTreeView.ConceptID = Nothing
        Me.CodesTreeView.CPT = Nothing
        Me.CodesTreeView.CQMDESC = Nothing
        Me.CodesTreeView.CQMID = Nothing
        Me.CodesTreeView.DDIDMember = Nothing
        Me.CodesTreeView.DescriptionMember = Nothing
        Me.CodesTreeView.DisplayContextMenuStrip = Nothing
        Me.CodesTreeView.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.CodesTreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CodesTreeView.DrugFlag = CType(16, Short)
        Me.CodesTreeView.DrugFormMember = Nothing
        Me.CodesTreeView.DrugQtyQualifierMember = Nothing
        Me.CodesTreeView.DurationMember = Nothing
        Me.CodesTreeView.EducationMappingSearchType = 1
        Me.CodesTreeView.FrequencyMember = Nothing
        Me.CodesTreeView.HistoryType = Nothing
        Me.CodesTreeView.ICD9 = Nothing
        Me.CodesTreeView.ICDRevision = Nothing
        Me.CodesTreeView.ImageIndex = 0
        Me.CodesTreeView.ImageObject = Nothing
        Me.CodesTreeView.Indicator = Nothing
        Me.CodesTreeView.IsCPTSearch = False
        Me.CodesTreeView.IsDiagnosisSearch = False
        Me.CodesTreeView.IsDrug = False
        Me.CodesTreeView.IsNarcoticsMember = Nothing
        Me.CodesTreeView.IsSearchForEducationMapping = False
        Me.CodesTreeView.IsSystemCategory = Nothing
        Me.CodesTreeView.Location = New System.Drawing.Point(3, 63)
        Me.CodesTreeView.Margin = New System.Windows.Forms.Padding(4)
        Me.CodesTreeView.MaximumNodes = 1000
        Me.CodesTreeView.mpidmember = Nothing
        Me.CodesTreeView.Name = "CodesTreeView"
        Me.CodesTreeView.NDCCodeMember = Nothing
        Me.CodesTreeView.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.CodesTreeView.ParentImageIndex = 0
        Me.CodesTreeView.ParentMember = Nothing
        Me.CodesTreeView.RouteMember = Nothing
        Me.CodesTreeView.RowOrderMember = Nothing
        Me.CodesTreeView.RxNormCode = Nothing
        Me.CodesTreeView.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.CodesTreeView.SearchBox = True
        Me.CodesTreeView.SearchText = Nothing
        Me.CodesTreeView.SelectedImageIndex = 0
        Me.CodesTreeView.SelectedNode = Nothing
        Me.CodesTreeView.SelectedNodeIDs = CType(resources.GetObject("CodesTreeView.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.CodesTreeView.SelectedParentImageIndex = 0
        Me.CodesTreeView.Size = New System.Drawing.Size(331, 407)
        Me.CodesTreeView.SmartTreatmentId = Nothing
        Me.CodesTreeView.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.CodesTreeView.TabIndex = 1
        Me.CodesTreeView.Tag = Nothing
        Me.CodesTreeView.UnitMember = Nothing
        Me.CodesTreeView.ValueMember = Nothing
        '
        'pnlLeftRadioBtnTop
        '
        Me.pnlLeftRadioBtnTop.Controls.Add(Me.pnlLeftRadioBtn)
        Me.pnlLeftRadioBtnTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLeftRadioBtnTop.Location = New System.Drawing.Point(3, 33)
        Me.pnlLeftRadioBtnTop.Name = "pnlLeftRadioBtnTop"
        Me.pnlLeftRadioBtnTop.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlLeftRadioBtnTop.Size = New System.Drawing.Size(331, 30)
        Me.pnlLeftRadioBtnTop.TabIndex = 46
        '
        'pnlLeftRadioBtn
        '
        Me.pnlLeftRadioBtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.pnlLeftRadioBtn.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlLeftRadioBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeftRadioBtn.Controls.Add(Me.rbtUnassociated)
        Me.pnlLeftRadioBtn.Controls.Add(Me.rbtAssociated)
        Me.pnlLeftRadioBtn.Controls.Add(Me.rbtAll)
        Me.pnlLeftRadioBtn.Controls.Add(Me.Label26)
        Me.pnlLeftRadioBtn.Controls.Add(Me.Label8)
        Me.pnlLeftRadioBtn.Controls.Add(Me.lbl_pnlRight)
        Me.pnlLeftRadioBtn.Controls.Add(Me.lbl_pnlTop)
        Me.pnlLeftRadioBtn.Controls.Add(Me.Label10)
        Me.pnlLeftRadioBtn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftRadioBtn.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeftRadioBtn.Name = "pnlLeftRadioBtn"
        Me.pnlLeftRadioBtn.Size = New System.Drawing.Size(328, 27)
        Me.pnlLeftRadioBtn.TabIndex = 0
        '
        'rbtUnassociated
        '
        Me.rbtUnassociated.AutoSize = True
        Me.rbtUnassociated.BackColor = System.Drawing.Color.Transparent
        Me.rbtUnassociated.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtUnassociated.Location = New System.Drawing.Point(105, 4)
        Me.rbtUnassociated.Name = "rbtUnassociated"
        Me.rbtUnassociated.Size = New System.Drawing.Size(96, 18)
        Me.rbtUnassociated.TabIndex = 1
        Me.rbtUnassociated.TabStop = True
        Me.rbtUnassociated.Text = "Unassociated"
        Me.rbtUnassociated.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtUnassociated.UseVisualStyleBackColor = False
        '
        'rbtAssociated
        '
        Me.rbtAssociated.AutoSize = True
        Me.rbtAssociated.BackColor = System.Drawing.Color.Transparent
        Me.rbtAssociated.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtAssociated.Location = New System.Drawing.Point(9, 4)
        Me.rbtAssociated.Name = "rbtAssociated"
        Me.rbtAssociated.Size = New System.Drawing.Size(91, 18)
        Me.rbtAssociated.TabIndex = 0
        Me.rbtAssociated.TabStop = True
        Me.rbtAssociated.Text = "Associated  "
        Me.rbtAssociated.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtAssociated.UseVisualStyleBackColor = False
        '
        'rbtAll
        '
        Me.rbtAll.AutoSize = True
        Me.rbtAll.BackColor = System.Drawing.Color.Transparent
        Me.rbtAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtAll.Location = New System.Drawing.Point(213, 4)
        Me.rbtAll.Name = "rbtAll"
        Me.rbtAll.Size = New System.Drawing.Size(41, 18)
        Me.rbtAll.TabIndex = 2
        Me.rbtAll.TabStop = True
        Me.rbtAll.Text = "All "
        Me.rbtAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.rbtAll.UseVisualStyleBackColor = False
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label26.Location = New System.Drawing.Point(1, 1)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(5, 25)
        Me.Label26.TabIndex = 38
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Location = New System.Drawing.Point(0, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 25)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(327, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 25)
        Me.lbl_pnlRight.TabIndex = 19
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(328, 1)
        Me.lbl_pnlTop.TabIndex = 4
        Me.lbl_pnlTop.Text = "label1"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Location = New System.Drawing.Point(0, 26)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(328, 1)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "label1"
        '
        'pnlICD10
        '
        Me.pnlICD10.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlICD10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlICD10.Controls.Add(Me.btnICD10)
        Me.pnlICD10.Controls.Add(Me.Label29)
        Me.pnlICD10.Controls.Add(Me.Label30)
        Me.pnlICD10.Controls.Add(Me.Label31)
        Me.pnlICD10.Controls.Add(Me.Label36)
        Me.pnlICD10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlICD10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlICD10.Location = New System.Drawing.Point(3, 470)
        Me.pnlICD10.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlICD10.Name = "pnlICD10"
        Me.pnlICD10.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlICD10.Size = New System.Drawing.Size(331, 30)
        Me.pnlICD10.TabIndex = 47
        '
        'btnICD10
        '
        Me.btnICD10.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnICD10.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnICD10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnICD10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnICD10.FlatAppearance.BorderSize = 0
        Me.btnICD10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnICD10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnICD10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnICD10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnICD10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnICD10.Location = New System.Drawing.Point(1, 1)
        Me.btnICD10.Name = "btnICD10"
        Me.btnICD10.Size = New System.Drawing.Size(326, 25)
        Me.btnICD10.TabIndex = 0
        Me.btnICD10.Tag = "Selected"
        Me.btnICD10.Text = "Problem(ICD10)"
        Me.btnICD10.UseVisualStyleBackColor = False
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label29.Location = New System.Drawing.Point(1, 26)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(326, 1)
        Me.Label29.TabIndex = 8
        Me.Label29.Text = "label2"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(0, 1)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(1, 26)
        Me.Label30.TabIndex = 7
        Me.Label30.Text = "label4"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label31.Location = New System.Drawing.Point(327, 1)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(1, 26)
        Me.Label31.TabIndex = 6
        Me.Label31.Text = "label3"
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(0, 0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(328, 1)
        Me.Label36.TabIndex = 5
        Me.Label36.Text = "label1"
        '
        'pnlLabs
        '
        Me.pnlLabs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlLabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLabs.Controls.Add(Me.btnLabs)
        Me.pnlLabs.Controls.Add(Me.Label60)
        Me.pnlLabs.Controls.Add(Me.Label61)
        Me.pnlLabs.Controls.Add(Me.Label62)
        Me.pnlLabs.Controls.Add(Me.Label63)
        Me.pnlLabs.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlLabs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlLabs.Location = New System.Drawing.Point(3, 500)
        Me.pnlLabs.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlLabs.Name = "pnlLabs"
        Me.pnlLabs.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlLabs.Size = New System.Drawing.Size(331, 28)
        Me.pnlLabs.TabIndex = 44
        '
        'btnLabs
        '
        Me.btnLabs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnLabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLabs.FlatAppearance.BorderSize = 0
        Me.btnLabs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLabs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLabs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLabs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLabs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnLabs.Location = New System.Drawing.Point(1, 1)
        Me.btnLabs.Name = "btnLabs"
        Me.btnLabs.Size = New System.Drawing.Size(326, 23)
        Me.btnLabs.TabIndex = 2
        Me.btnLabs.Tag = "UnSelected"
        Me.btnLabs.Text = "Lab Test/Result"
        Me.btnLabs.UseVisualStyleBackColor = False
        '
        'Label60
        '
        Me.Label60.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label60.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label60.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label60.Location = New System.Drawing.Point(1, 24)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(326, 1)
        Me.Label60.TabIndex = 8
        Me.Label60.Text = "label2"
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label61.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.Location = New System.Drawing.Point(0, 1)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(1, 24)
        Me.Label61.TabIndex = 7
        Me.Label61.Text = "label4"
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label62.Location = New System.Drawing.Point(327, 1)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(1, 24)
        Me.Label62.TabIndex = 6
        Me.Label62.Text = "label3"
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.Location = New System.Drawing.Point(0, 0)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(328, 1)
        Me.Label63.TabIndex = 5
        Me.Label63.Text = "label1"
        '
        'pnlMedication
        '
        Me.pnlMedication.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMedication.Controls.Add(Me.btnMedication)
        Me.pnlMedication.Controls.Add(Me.Label56)
        Me.pnlMedication.Controls.Add(Me.Label57)
        Me.pnlMedication.Controls.Add(Me.Label58)
        Me.pnlMedication.Controls.Add(Me.Label59)
        Me.pnlMedication.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMedication.Location = New System.Drawing.Point(3, 528)
        Me.pnlMedication.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlMedication.Name = "pnlMedication"
        Me.pnlMedication.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlMedication.Size = New System.Drawing.Size(331, 28)
        Me.pnlMedication.TabIndex = 43
        '
        'btnMedication
        '
        Me.btnMedication.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnMedication.BackgroundImage = CType(resources.GetObject("btnMedication.BackgroundImage"), System.Drawing.Image)
        Me.btnMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnMedication.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnMedication.FlatAppearance.BorderSize = 0
        Me.btnMedication.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnMedication.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnMedication.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMedication.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnMedication.Location = New System.Drawing.Point(1, 1)
        Me.btnMedication.Name = "btnMedication"
        Me.btnMedication.Size = New System.Drawing.Size(326, 23)
        Me.btnMedication.TabIndex = 3
        Me.btnMedication.Tag = "UnSelected"
        Me.btnMedication.Text = "Medication"
        Me.btnMedication.UseVisualStyleBackColor = False
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label56.Location = New System.Drawing.Point(1, 24)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(326, 1)
        Me.Label56.TabIndex = 8
        Me.Label56.Text = "label2"
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label57.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.Location = New System.Drawing.Point(0, 1)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(1, 24)
        Me.Label57.TabIndex = 7
        Me.Label57.Text = "label4"
        '
        'Label58
        '
        Me.Label58.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label58.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label58.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label58.Location = New System.Drawing.Point(327, 1)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(1, 24)
        Me.Label58.TabIndex = 6
        Me.Label58.Text = "label3"
        '
        'Label59
        '
        Me.Label59.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label59.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label59.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.Location = New System.Drawing.Point(0, 0)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(328, 1)
        Me.Label59.TabIndex = 5
        Me.Label59.Text = "label1"
        '
        'pnlSnomed
        '
        Me.pnlSnomed.Controls.Add(Me.pnlSnomed_add)
        Me.pnlSnomed.Controls.Add(Me.Panel1)
        Me.pnlSnomed.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlSnomed.Location = New System.Drawing.Point(3, 556)
        Me.pnlSnomed.Name = "pnlSnomed"
        Me.pnlSnomed.Size = New System.Drawing.Size(331, 28)
        Me.pnlSnomed.TabIndex = 45
        '
        'pnlSnomed_add
        '
        Me.pnlSnomed_add.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSnomed_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSnomed_add.Controls.Add(Me.btnSnoMed)
        Me.pnlSnomed_add.Controls.Add(Me.Label37)
        Me.pnlSnomed_add.Controls.Add(Me.Label38)
        Me.pnlSnomed_add.Controls.Add(Me.Label39)
        Me.pnlSnomed_add.Controls.Add(Me.Label40)
        Me.pnlSnomed_add.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSnomed_add.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSnomed_add.Location = New System.Drawing.Point(0, 0)
        Me.pnlSnomed_add.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlSnomed_add.Name = "pnlSnomed_add"
        Me.pnlSnomed_add.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlSnomed_add.Size = New System.Drawing.Size(295, 28)
        Me.pnlSnomed_add.TabIndex = 41
        Me.pnlSnomed_add.Tag = "UnSelected"
        '
        'btnSnoMed
        '
        Me.btnSnoMed.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnSnoMed.BackgroundImage = CType(resources.GetObject("btnSnoMed.BackgroundImage"), System.Drawing.Image)
        Me.btnSnoMed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSnoMed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnSnoMed.FlatAppearance.BorderSize = 0
        Me.btnSnoMed.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSnoMed.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSnoMed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSnoMed.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSnoMed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnSnoMed.Location = New System.Drawing.Point(1, 1)
        Me.btnSnoMed.Name = "btnSnoMed"
        Me.btnSnoMed.Size = New System.Drawing.Size(290, 23)
        Me.btnSnoMed.TabIndex = 4
        Me.btnSnoMed.Tag = "UnSelected"
        Me.btnSnoMed.Text = "Problem(SnoMed)"
        Me.btnSnoMed.UseVisualStyleBackColor = False
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label37.Location = New System.Drawing.Point(1, 24)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(290, 1)
        Me.Label37.TabIndex = 8
        Me.Label37.Text = "label2"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(0, 1)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(1, 24)
        Me.Label38.TabIndex = 7
        Me.Label38.Text = "label4"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label39.Location = New System.Drawing.Point(291, 1)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(1, 24)
        Me.Label39.TabIndex = 6
        Me.Label39.Text = "label3"
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(0, 0)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(292, 1)
        Me.Label40.TabIndex = 5
        Me.Label40.Text = "label1"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.btn_AddSnoMed)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(295, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(36, 28)
        Me.Panel1.TabIndex = 42
        Me.Panel1.Tag = "UnSelected"
        '
        'btn_AddSnoMed
        '
        Me.btn_AddSnoMed.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btn_AddSnoMed.BackgroundImage = CType(resources.GetObject("btn_AddSnoMed.BackgroundImage"), System.Drawing.Image)
        Me.btn_AddSnoMed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btn_AddSnoMed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_AddSnoMed.FlatAppearance.BorderSize = 0
        Me.btn_AddSnoMed.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btn_AddSnoMed.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btn_AddSnoMed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_AddSnoMed.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_AddSnoMed.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_AddSnoMed.Location = New System.Drawing.Point(1, 1)
        Me.btn_AddSnoMed.Name = "btn_AddSnoMed"
        Me.btn_AddSnoMed.Size = New System.Drawing.Size(31, 23)
        Me.btn_AddSnoMed.TabIndex = 0
        Me.btn_AddSnoMed.Tag = "UnSelected"
        Me.btn_AddSnoMed.Text = "..."
        Me.btn_AddSnoMed.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 24)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "label4"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(32, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 24)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "label3"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(33, 1)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "label1"
        '
        'pnlICD9
        '
        Me.pnlICD9.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlICD9.Controls.Add(Me.btnICD9)
        Me.pnlICD9.Controls.Add(Me.Label41)
        Me.pnlICD9.Controls.Add(Me.Label42)
        Me.pnlICD9.Controls.Add(Me.Label43)
        Me.pnlICD9.Controls.Add(Me.Label44)
        Me.pnlICD9.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlICD9.Location = New System.Drawing.Point(3, 3)
        Me.pnlICD9.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlICD9.Name = "pnlICD9"
        Me.pnlICD9.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlICD9.Size = New System.Drawing.Size(331, 30)
        Me.pnlICD9.TabIndex = 42
        '
        'btnICD9
        '
        Me.btnICD9.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnICD9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Orange
        Me.btnICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnICD9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnICD9.FlatAppearance.BorderSize = 0
        Me.btnICD9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnICD9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnICD9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnICD9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnICD9.Location = New System.Drawing.Point(1, 1)
        Me.btnICD9.Name = "btnICD9"
        Me.btnICD9.Size = New System.Drawing.Size(326, 25)
        Me.btnICD9.TabIndex = 0
        Me.btnICD9.Tag = "Selected"
        Me.btnICD9.Text = "Problem(ICD9)"
        Me.btnICD9.UseVisualStyleBackColor = False
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label41.Location = New System.Drawing.Point(1, 26)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(326, 1)
        Me.Label41.TabIndex = 8
        Me.Label41.Text = "label2"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(0, 1)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(1, 26)
        Me.Label42.TabIndex = 7
        Me.Label42.Text = "label4"
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label43.Location = New System.Drawing.Point(327, 1)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(1, 26)
        Me.Label43.TabIndex = 6
        Me.Label43.Text = "label3"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(0, 0)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(328, 1)
        Me.Label44.TabIndex = 5
        Me.Label44.Text = "label1"
        '
        'pnltrICD9
        '
        Me.pnltrICD9.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrICD9.Controls.Add(Me.trICD9)
        Me.pnltrICD9.Controls.Add(Me.Label23)
        Me.pnltrICD9.Controls.Add(Me.Label17)
        Me.pnltrICD9.Controls.Add(Me.Label18)
        Me.pnltrICD9.Controls.Add(Me.Label19)
        Me.pnltrICD9.Controls.Add(Me.Label22)
        Me.pnltrICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrICD9.Location = New System.Drawing.Point(178, 232)
        Me.pnltrICD9.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnltrICD9.Name = "pnltrICD9"
        Me.pnltrICD9.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnltrICD9.Size = New System.Drawing.Size(24, 53)
        Me.pnltrICD9.TabIndex = 2
        Me.pnltrICD9.Visible = False
        '
        'trICD9
        '
        Me.trICD9.BackColor = System.Drawing.Color.White
        Me.trICD9.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trICD9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trICD9.ForeColor = System.Drawing.Color.Black
        Me.trICD9.HideSelection = False
        Me.trICD9.Indent = 20
        Me.trICD9.ItemHeight = 20
        Me.trICD9.Location = New System.Drawing.Point(4, 5)
        Me.trICD9.Name = "trICD9"
        Me.trICD9.ShowLines = False
        Me.trICD9.Size = New System.Drawing.Size(19, 44)
        Me.trICD9.TabIndex = 0
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.White
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label23.Location = New System.Drawing.Point(4, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(19, 4)
        Me.Label23.TabIndex = 38
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(4, 49)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(19, 1)
        Me.Label17.TabIndex = 8
        Me.Label17.Text = "label2"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(3, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 49)
        Me.Label18.TabIndex = 7
        Me.Label18.Text = "label4"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(23, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 49)
        Me.Label19.TabIndex = 6
        Me.Label19.Text = "label3"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(3, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(21, 1)
        Me.Label22.TabIndex = 5
        Me.Label22.Text = "label1"
        '
        'pnltxtsearchDrugs
        '
        Me.pnltxtsearchDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltxtsearchDrugs.Controls.Add(Me.txtsearchDrugs)
        Me.pnltxtsearchDrugs.Controls.Add(Me.Label20)
        Me.pnltxtsearchDrugs.Controls.Add(Me.Label21)
        Me.pnltxtsearchDrugs.Controls.Add(Me.PictureBox1)
        Me.pnltxtsearchDrugs.Controls.Add(Me.label9)
        Me.pnltxtsearchDrugs.Controls.Add(Me.Label12)
        Me.pnltxtsearchDrugs.Controls.Add(Me.Label24)
        Me.pnltxtsearchDrugs.Controls.Add(Me.Label25)
        Me.pnltxtsearchDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltxtsearchDrugs.ForeColor = System.Drawing.Color.Black
        Me.pnltxtsearchDrugs.Location = New System.Drawing.Point(308, 78)
        Me.pnltxtsearchDrugs.Name = "pnltxtsearchDrugs"
        Me.pnltxtsearchDrugs.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.pnltxtsearchDrugs.Size = New System.Drawing.Size(96, 26)
        Me.pnltxtsearchDrugs.TabIndex = 1
        Me.pnltxtsearchDrugs.Visible = False
        '
        'txtsearchDrugs
        '
        Me.txtsearchDrugs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearchDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsearchDrugs.Location = New System.Drawing.Point(32, 5)
        Me.txtsearchDrugs.Name = "txtsearchDrugs"
        Me.txtsearchDrugs.Size = New System.Drawing.Size(63, 15)
        Me.txtsearchDrugs.TabIndex = 0
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.White
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(32, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(63, 4)
        Me.Label20.TabIndex = 37
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Location = New System.Drawing.Point(32, 20)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(63, 2)
        Me.Label21.TabIndex = 38
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Location = New System.Drawing.Point(4, 1)
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
        Me.label9.Location = New System.Drawing.Point(4, 22)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(91, 1)
        Me.label9.TabIndex = 35
        Me.label9.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Location = New System.Drawing.Point(4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(91, 1)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "label1"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label24.Location = New System.Drawing.Point(3, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 23)
        Me.Label24.TabIndex = 39
        Me.Label24.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Location = New System.Drawing.Point(95, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 23)
        Me.Label25.TabIndex = 40
        Me.Label25.Text = "label4"
        '
        'rbICD9Desc
        '
        Me.rbICD9Desc.BackColor = System.Drawing.Color.Transparent
        Me.rbICD9Desc.Checked = True
        Me.rbICD9Desc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbICD9Desc.Location = New System.Drawing.Point(458, 60)
        Me.rbICD9Desc.Name = "rbICD9Desc"
        Me.rbICD9Desc.Size = New System.Drawing.Size(103, 22)
        Me.rbICD9Desc.TabIndex = 1
        Me.rbICD9Desc.TabStop = True
        Me.rbICD9Desc.Text = "Description"
        Me.rbICD9Desc.UseVisualStyleBackColor = False
        Me.rbICD9Desc.Visible = False
        '
        'rbICD9Code
        '
        Me.rbICD9Code.BackColor = System.Drawing.Color.Transparent
        Me.rbICD9Code.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbICD9Code.Location = New System.Drawing.Point(432, 81)
        Me.rbICD9Code.Name = "rbICD9Code"
        Me.rbICD9Code.Size = New System.Drawing.Size(97, 22)
        Me.rbICD9Code.TabIndex = 0
        Me.rbICD9Code.Text = "ICD9 Code"
        Me.rbICD9Code.UseVisualStyleBackColor = False
        Me.rbICD9Code.Visible = False
        '
        'pnlRight
        '
        Me.pnlRight.Controls.Add(Me.UCtrlReferenceMaterial)
        Me.pnlRight.Controls.Add(Me.pnlbtnProviderReference)
        Me.pnlRight.Controls.Add(Me.pnlbtnPatientEducation)
        Me.pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlRight.Location = New System.Drawing.Point(652, 54)
        Me.pnlRight.Name = "pnlRight"
        Me.pnlRight.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlRight.Size = New System.Drawing.Size(348, 584)
        Me.pnlRight.TabIndex = 3
        '
        'UCtrlReferenceMaterial
        '
        Me.UCtrlReferenceMaterial.AllergyClassID = Nothing
        Me.UCtrlReferenceMaterial.BackColor = System.Drawing.Color.Transparent
        Me.UCtrlReferenceMaterial.CheckBoxes = False
        Me.UCtrlReferenceMaterial.CodeMember = Nothing
        Me.UCtrlReferenceMaterial.ColonAsSeparator = False
        Me.UCtrlReferenceMaterial.Comment = Nothing
        Me.UCtrlReferenceMaterial.ConceptID = Nothing
        Me.UCtrlReferenceMaterial.CPT = Nothing
        Me.UCtrlReferenceMaterial.CQMDESC = Nothing
        Me.UCtrlReferenceMaterial.CQMID = Nothing
        Me.UCtrlReferenceMaterial.DDIDMember = Nothing
        Me.UCtrlReferenceMaterial.DescriptionMember = Nothing
        Me.UCtrlReferenceMaterial.DisplayContextMenuStrip = Nothing
        Me.UCtrlReferenceMaterial.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.UCtrlReferenceMaterial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UCtrlReferenceMaterial.DrugFlag = CType(16, Short)
        Me.UCtrlReferenceMaterial.DrugFormMember = Nothing
        Me.UCtrlReferenceMaterial.DrugQtyQualifierMember = Nothing
        Me.UCtrlReferenceMaterial.DurationMember = Nothing
        Me.UCtrlReferenceMaterial.EducationMappingSearchType = 1
        Me.UCtrlReferenceMaterial.FrequencyMember = Nothing
        Me.UCtrlReferenceMaterial.HistoryType = Nothing
        Me.UCtrlReferenceMaterial.ICD9 = Nothing
        Me.UCtrlReferenceMaterial.ICDRevision = Nothing
        Me.UCtrlReferenceMaterial.ImageIndex = 0
        Me.UCtrlReferenceMaterial.ImageObject = Nothing
        Me.UCtrlReferenceMaterial.Indicator = Nothing
        Me.UCtrlReferenceMaterial.IsCPTSearch = False
        Me.UCtrlReferenceMaterial.IsDiagnosisSearch = False
        Me.UCtrlReferenceMaterial.IsDrug = False
        Me.UCtrlReferenceMaterial.IsNarcoticsMember = Nothing
        Me.UCtrlReferenceMaterial.IsSearchForEducationMapping = False
        Me.UCtrlReferenceMaterial.IsSystemCategory = Nothing
        Me.UCtrlReferenceMaterial.Location = New System.Drawing.Point(0, 31)
        Me.UCtrlReferenceMaterial.Margin = New System.Windows.Forms.Padding(4)
        Me.UCtrlReferenceMaterial.MaximumNodes = 1000
        Me.UCtrlReferenceMaterial.mpidmember = Nothing
        Me.UCtrlReferenceMaterial.Name = "UCtrlReferenceMaterial"
        Me.UCtrlReferenceMaterial.NDCCodeMember = Nothing
        Me.UCtrlReferenceMaterial.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.UCtrlReferenceMaterial.ParentImageIndex = 0
        Me.UCtrlReferenceMaterial.ParentMember = Nothing
        Me.UCtrlReferenceMaterial.RouteMember = Nothing
        Me.UCtrlReferenceMaterial.RowOrderMember = Nothing
        Me.UCtrlReferenceMaterial.RxNormCode = Nothing
        Me.UCtrlReferenceMaterial.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.UCtrlReferenceMaterial.SearchBox = True
        Me.UCtrlReferenceMaterial.SearchText = Nothing
        Me.UCtrlReferenceMaterial.SelectedImageIndex = 0
        Me.UCtrlReferenceMaterial.SelectedNode = Nothing
        Me.UCtrlReferenceMaterial.SelectedNodeIDs = CType(resources.GetObject("UCtrlReferenceMaterial.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.UCtrlReferenceMaterial.SelectedParentImageIndex = 0
        Me.UCtrlReferenceMaterial.Size = New System.Drawing.Size(348, 525)
        Me.UCtrlReferenceMaterial.SmartTreatmentId = Nothing
        Me.UCtrlReferenceMaterial.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.UCtrlReferenceMaterial.TabIndex = 15
        Me.UCtrlReferenceMaterial.Tag = Nothing
        Me.UCtrlReferenceMaterial.UnitMember = Nothing
        Me.UCtrlReferenceMaterial.ValueMember = Nothing
        '
        'pnlbtnProviderReference
        '
        Me.pnlbtnProviderReference.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlbtnProviderReference.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnProviderReference.Controls.Add(Me.btnProviderReference)
        Me.pnlbtnProviderReference.Controls.Add(Me.Label45)
        Me.pnlbtnProviderReference.Controls.Add(Me.Label46)
        Me.pnlbtnProviderReference.Controls.Add(Me.Label47)
        Me.pnlbtnProviderReference.Controls.Add(Me.Label48)
        Me.pnlbtnProviderReference.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnProviderReference.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnProviderReference.Location = New System.Drawing.Point(0, 556)
        Me.pnlbtnProviderReference.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnProviderReference.Name = "pnlbtnProviderReference"
        Me.pnlbtnProviderReference.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnProviderReference.Size = New System.Drawing.Size(348, 28)
        Me.pnlbtnProviderReference.TabIndex = 5
        '
        'btnProviderReference
        '
        Me.btnProviderReference.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnProviderReference.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.btnProviderReference.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnProviderReference.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnProviderReference.FlatAppearance.BorderSize = 0
        Me.btnProviderReference.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnProviderReference.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnProviderReference.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProviderReference.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProviderReference.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnProviderReference.Location = New System.Drawing.Point(1, 1)
        Me.btnProviderReference.Name = "btnProviderReference"
        Me.btnProviderReference.Size = New System.Drawing.Size(343, 23)
        Me.btnProviderReference.TabIndex = 16
        Me.btnProviderReference.Tag = "UnSelected"
        Me.btnProviderReference.Text = "Provider Reference"
        Me.btnProviderReference.UseVisualStyleBackColor = False
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label45.Location = New System.Drawing.Point(1, 24)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(343, 1)
        Me.Label45.TabIndex = 8
        Me.Label45.Text = "label2"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(0, 1)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(1, 24)
        Me.Label46.TabIndex = 7
        Me.Label46.Text = "label4"
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label47.Location = New System.Drawing.Point(344, 1)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(1, 24)
        Me.Label47.TabIndex = 6
        Me.Label47.Text = "label3"
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(0, 0)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(345, 1)
        Me.Label48.TabIndex = 5
        Me.Label48.Text = "label1"
        '
        'pnlbtnPatientEducation
        '
        Me.pnlbtnPatientEducation.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlbtnPatientEducation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlbtnPatientEducation.Controls.Add(Me.btnPatientEducation)
        Me.pnlbtnPatientEducation.Controls.Add(Me.Label32)
        Me.pnlbtnPatientEducation.Controls.Add(Me.Label33)
        Me.pnlbtnPatientEducation.Controls.Add(Me.Label34)
        Me.pnlbtnPatientEducation.Controls.Add(Me.Label35)
        Me.pnlbtnPatientEducation.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbtnPatientEducation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlbtnPatientEducation.Location = New System.Drawing.Point(0, 3)
        Me.pnlbtnPatientEducation.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlbtnPatientEducation.Name = "pnlbtnPatientEducation"
        Me.pnlbtnPatientEducation.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnPatientEducation.Size = New System.Drawing.Size(348, 28)
        Me.pnlbtnPatientEducation.TabIndex = 6
        '
        'btnPatientEducation
        '
        Me.btnPatientEducation.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnPatientEducation.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Orange
        Me.btnPatientEducation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPatientEducation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnPatientEducation.FlatAppearance.BorderSize = 0
        Me.btnPatientEducation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnPatientEducation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnPatientEducation.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPatientEducation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPatientEducation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnPatientEducation.Location = New System.Drawing.Point(1, 1)
        Me.btnPatientEducation.Name = "btnPatientEducation"
        Me.btnPatientEducation.Size = New System.Drawing.Size(343, 23)
        Me.btnPatientEducation.TabIndex = 14
        Me.btnPatientEducation.Tag = "Selected"
        Me.btnPatientEducation.Text = "Patient Reference"
        Me.btnPatientEducation.UseVisualStyleBackColor = False
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label32.Location = New System.Drawing.Point(1, 24)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(343, 1)
        Me.Label32.TabIndex = 8
        Me.Label32.Text = "label2"
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(0, 1)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(1, 24)
        Me.Label33.TabIndex = 7
        Me.Label33.Text = "label4"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label34.Location = New System.Drawing.Point(344, 1)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(1, 24)
        Me.Label34.TabIndex = 6
        Me.Label34.Text = "label3"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(0, 0)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(345, 1)
        Me.Label35.TabIndex = 5
        Me.Label35.Text = "label1"
        '
        'pnlCodesAssociation
        '
        Me.pnlCodesAssociation.Controls.Add(Me.treeViewCodesAssociation)
        Me.pnlCodesAssociation.Controls.Add(Me.Label50)
        Me.pnlCodesAssociation.Controls.Add(Me.Label2)
        Me.pnlCodesAssociation.Controls.Add(Me.Label5)
        Me.pnlCodesAssociation.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlCodesAssociation.Controls.Add(Me.Label4)
        Me.pnlCodesAssociation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCodesAssociation.Location = New System.Drawing.Point(337, 223)
        Me.pnlCodesAssociation.Name = "pnlCodesAssociation"
        Me.pnlCodesAssociation.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlCodesAssociation.Size = New System.Drawing.Size(312, 415)
        Me.pnlCodesAssociation.TabIndex = 2
        '
        'treeViewCodesAssociation
        '
        Me.treeViewCodesAssociation.BackColor = System.Drawing.Color.White
        Me.treeViewCodesAssociation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.treeViewCodesAssociation.CheckBoxes = True
        Me.treeViewCodesAssociation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.treeViewCodesAssociation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.treeViewCodesAssociation.HideSelection = False
        Me.treeViewCodesAssociation.Indent = 20
        Me.treeViewCodesAssociation.ItemHeight = 20
        Me.treeViewCodesAssociation.Location = New System.Drawing.Point(1, 5)
        Me.treeViewCodesAssociation.Name = "treeViewCodesAssociation"
        Me.treeViewCodesAssociation.ShowLines = False
        Me.treeViewCodesAssociation.Size = New System.Drawing.Size(310, 406)
        Me.treeViewCodesAssociation.TabIndex = 13
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.White
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label50.Location = New System.Drawing.Point(1, 1)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(310, 4)
        Me.Label50.TabIndex = 38
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Location = New System.Drawing.Point(1, 411)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(310, 1)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "label2"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(1, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(310, 1)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "label1"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 412)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(311, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 412)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label3"
        '
        'pnltblMedication
        '
        Me.pnltblMedication.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltblMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltblMedication.Controls.Add(Me.tblMedication)
        Me.pnltblMedication.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnltblMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltblMedication.Location = New System.Drawing.Point(0, 0)
        Me.pnltblMedication.Name = "pnltblMedication"
        Me.pnltblMedication.Size = New System.Drawing.Size(1000, 54)
        Me.pnltblMedication.TabIndex = 0
        '
        'tblMedication
        '
        Me.tblMedication.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tblMedication.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblMedication.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblMedication.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblMedication.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblNew, Me.tblSave, Me.tblFinish, Me.ts_gloCommunityDownload, Me.tblRefresh, Me.tblClose})
        Me.tblMedication.Location = New System.Drawing.Point(0, 0)
        Me.tblMedication.Name = "tblMedication"
        Me.tblMedication.Size = New System.Drawing.Size(1000, 53)
        Me.tblMedication.TabIndex = 0
        Me.tblMedication.Text = "ToolStrip1"
        '
        'tblNew
        '
        Me.tblNew.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblNew.Image = CType(resources.GetObject("tblNew.Image"), System.Drawing.Image)
        Me.tblNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblNew.Name = "tblNew"
        Me.tblNew.Size = New System.Drawing.Size(37, 50)
        Me.tblNew.Tag = "New"
        Me.tblNew.Text = "&New"
        Me.tblNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblNew.ToolTipText = "New"
        Me.tblNew.Visible = False
        '
        'tblSave
        '
        Me.tblSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSave.Image = CType(resources.GetObject("tblSave.Image"), System.Drawing.Image)
        Me.tblSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblSave.Name = "tblSave"
        Me.tblSave.Size = New System.Drawing.Size(66, 50)
        Me.tblSave.Tag = "Save"
        Me.tblSave.Text = "&Save&&Cls"
        Me.tblSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblSave.ToolTipText = "Save and Close"
        '
        'tblFinish
        '
        Me.tblFinish.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblFinish.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblFinish.Name = "tblFinish"
        Me.tblFinish.Size = New System.Drawing.Size(45, 50)
        Me.tblFinish.Tag = "Finish"
        Me.tblFinish.Text = "&Finish"
        Me.tblFinish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblFinish.ToolTipText = "Finish"
        Me.tblFinish.Visible = False
        '
        'ts_gloCommunityDownload
        '
        Me.ts_gloCommunityDownload.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ts_gloCommunityDownload.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_gloCommunityDownload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_gloCommunityDownload.Name = "ts_gloCommunityDownload"
        Me.ts_gloCommunityDownload.Size = New System.Drawing.Size(73, 50)
        Me.ts_gloCommunityDownload.Tag = "gloCommunityDownload"
        Me.ts_gloCommunityDownload.Text = "Download"
        Me.ts_gloCommunityDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_gloCommunityDownload.ToolTipText = "Download from gloCommunity"
        Me.ts_gloCommunityDownload.Visible = False
        '
        'tblRefresh
        '
        Me.tblRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblRefresh.Name = "tblRefresh"
        Me.tblRefresh.Size = New System.Drawing.Size(58, 50)
        Me.tblRefresh.Text = "&Refresh"
        Me.tblRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblRefresh.Visible = False
        '
        'tblClose
        '
        Me.tblClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblClose.Image = CType(resources.GetObject("tblClose.Image"), System.Drawing.Image)
        Me.tblClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblClose.Name = "tblClose"
        Me.tblClose.Size = New System.Drawing.Size(43, 50)
        Me.tblClose.Tag = "Close"
        Me.tblClose.Text = "&Close"
        Me.tblClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblClose.ToolTipText = "Close"
        '
        'cntICD9Association
        '
        Me.cntICD9Association.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDeleteICD9Item})
        '
        'mnuDeleteICD9Item
        '
        Me.mnuDeleteICD9Item.Index = 0
        Me.mnuDeleteICD9Item.Text = "Remove Associate"
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter2.Location = New System.Drawing.Point(649, 54)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 584)
        Me.Splitter2.TabIndex = 5
        Me.Splitter2.TabStop = False
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(334, 54)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 584)
        Me.Splitter1.TabIndex = 6
        Me.Splitter1.TabStop = False
        '
        'cntTags
        '
        Me.cntTags.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.Text = "Remove Associate"
        '
        'imgTreeView
        '
        Me.imgTreeView.ImageStream = CType(resources.GetObject("imgTreeView.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTreeView.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeView.Images.SetKeyName(0, "Bullet06.ico")
        Me.imgTreeView.Images.SetKeyName(1, "ICD 9_01.ico")
        Me.imgTreeView.Images.SetKeyName(2, "CPT_01.ico")
        Me.imgTreeView.Images.SetKeyName(3, "Drugs.ico")
        Me.imgTreeView.Images.SetKeyName(4, "Tag.ico")
        Me.imgTreeView.Images.SetKeyName(5, "Pat Education.ico")
        Me.imgTreeView.Images.SetKeyName(6, "ICD9 Association.ico")
        Me.imgTreeView.Images.SetKeyName(7, "Small Arrow.ico")
        Me.imgTreeView.Images.SetKeyName(8, "FLow sheet.ico")
        Me.imgTreeView.Images.SetKeyName(9, "Lab orders.ico")
        Me.imgTreeView.Images.SetKeyName(10, "Radiology Orders.ico")
        Me.imgTreeView.Images.SetKeyName(11, "Refferal.ico")
        Me.imgTreeView.Images.SetKeyName(12, "Template.ico")
        Me.imgTreeView.Images.SetKeyName(13, "Doctor Speaker.ico")
        Me.imgTreeView.Images.SetKeyName(14, "Surescripts.ico")
        Me.imgTreeView.Images.SetKeyName(15, "ICD10GalleryGreen.png")
        '
        'pnlDemographicsFilter
        '
        Me.pnlDemographicsFilter.AutoSize = True
        Me.pnlDemographicsFilter.BackColor = System.Drawing.Color.Transparent
        Me.pnlDemographicsFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDemographicsFilter.Controls.Add(Me.Panel2)
        Me.pnlDemographicsFilter.Controls.Add(Me.pnlCriteriaLab)
        Me.pnlDemographicsFilter.Controls.Add(Me.pnlCriteriaAge)
        Me.pnlDemographicsFilter.Controls.Add(Me.pnlCriteriaCheckboxes)
        Me.pnlDemographicsFilter.Controls.Add(Me.Label28)
        Me.pnlDemographicsFilter.Controls.Add(Me.Label15)
        Me.pnlDemographicsFilter.Controls.Add(Me.Label14)
        Me.pnlDemographicsFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDemographicsFilter.ForeColor = System.Drawing.Color.Black
        Me.pnlDemographicsFilter.Location = New System.Drawing.Point(337, 54)
        Me.pnlDemographicsFilter.Name = "pnlDemographicsFilter"
        Me.pnlDemographicsFilter.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlDemographicsFilter.Size = New System.Drawing.Size(312, 169)
        Me.pnlDemographicsFilter.TabIndex = 7
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkAdvancedProviderReference)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(1, 133)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(310, 33)
        Me.Panel2.TabIndex = 51
        '
        'chkAdvancedProviderReference
        '
        Me.chkAdvancedProviderReference.AutoSize = True
        Me.chkAdvancedProviderReference.Enabled = False
        Me.chkAdvancedProviderReference.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.chkAdvancedProviderReference.Location = New System.Drawing.Point(37, 5)
        Me.chkAdvancedProviderReference.Name = "chkAdvancedProviderReference"
        Me.chkAdvancedProviderReference.Size = New System.Drawing.Size(188, 18)
        Me.chkAdvancedProviderReference.TabIndex = 12
        Me.chkAdvancedProviderReference.Text = "Advanced Provider Reference"
        Me.chkAdvancedProviderReference.UseVisualStyleBackColor = True
        '
        'pnlCriteriaLab
        '
        Me.pnlCriteriaLab.Controls.Add(Me.txtValueOne)
        Me.pnlCriteriaLab.Controls.Add(Me.Label27)
        Me.pnlCriteriaLab.Controls.Add(Me.Label16)
        Me.pnlCriteriaLab.Controls.Add(Me.Label13)
        Me.pnlCriteriaLab.Controls.Add(Me.cmbOperator)
        Me.pnlCriteriaLab.Controls.Add(Me.txtValueTwo)
        Me.pnlCriteriaLab.Controls.Add(Me.Label11)
        Me.pnlCriteriaLab.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCriteriaLab.Location = New System.Drawing.Point(1, 81)
        Me.pnlCriteriaLab.Name = "pnlCriteriaLab"
        Me.pnlCriteriaLab.Size = New System.Drawing.Size(310, 52)
        Me.pnlCriteriaLab.TabIndex = 44
        '
        'txtValueOne
        '
        Me.txtValueOne.Enabled = False
        Me.txtValueOne.Location = New System.Drawing.Point(164, 24)
        Me.txtValueOne.Name = "txtValueOne"
        Me.txtValueOne.Size = New System.Drawing.Size(55, 22)
        Me.txtValueOne.TabIndex = 9
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Location = New System.Drawing.Point(363, 6)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(48, 14)
        Me.Label27.TabIndex = 50
        Me.Label27.Text = "Value 2"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(166, 6)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(48, 14)
        Me.Label16.TabIndex = 49
        Me.Label16.Text = "Value 1"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(261, 6)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(56, 14)
        Me.Label13.TabIndex = 48
        Me.Label13.Text = "Operator"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbOperator
        '
        Me.cmbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOperator.Enabled = False
        Me.cmbOperator.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOperator.ForeColor = System.Drawing.Color.Black
        Me.cmbOperator.Items.AddRange(New Object() {"None", "Between", "Equals", "Greater than", "Less than"})
        Me.cmbOperator.Location = New System.Drawing.Point(234, 24)
        Me.cmbOperator.Name = "cmbOperator"
        Me.cmbOperator.Size = New System.Drawing.Size(110, 22)
        Me.cmbOperator.TabIndex = 10
        '
        'txtValueTwo
        '
        Me.txtValueTwo.Enabled = False
        Me.txtValueTwo.Location = New System.Drawing.Point(360, 24)
        Me.txtValueTwo.Name = "txtValueTwo"
        Me.txtValueTwo.Size = New System.Drawing.Size(55, 22)
        Me.txtValueTwo.TabIndex = 11
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(52, 27)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(106, 14)
        Me.Label11.TabIndex = 43
        Me.Label11.Text = "Lab Result Values:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlCriteriaAge
        '
        Me.pnlCriteriaAge.Controls.Add(Me.cmbGender)
        Me.pnlCriteriaAge.Controls.Add(Me.Label155)
        Me.pnlCriteriaAge.Controls.Add(Me.Label119)
        Me.pnlCriteriaAge.Controls.Add(Me.cmbAgeMax)
        Me.pnlCriteriaAge.Controls.Add(Me.cmbAgeMin)
        Me.pnlCriteriaAge.Controls.Add(Me.Label157)
        Me.pnlCriteriaAge.Controls.Add(Me.lblAgeMax)
        Me.pnlCriteriaAge.Controls.Add(Me.lblGender)
        Me.pnlCriteriaAge.Controls.Add(Me.Label156)
        Me.pnlCriteriaAge.Controls.Add(Me.lblAgeMin)
        Me.pnlCriteriaAge.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCriteriaAge.Location = New System.Drawing.Point(1, 40)
        Me.pnlCriteriaAge.Name = "pnlCriteriaAge"
        Me.pnlCriteriaAge.Size = New System.Drawing.Size(310, 41)
        Me.pnlCriteriaAge.TabIndex = 44
        '
        'cmbGender
        '
        Me.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGender.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbGender.Items.AddRange(New Object() {"", "Male", "Female", "Other", "Unknown"})
        Me.cmbGender.Location = New System.Drawing.Point(369, 4)
        Me.cmbGender.Name = "cmbGender"
        Me.cmbGender.Size = New System.Drawing.Size(127, 22)
        Me.cmbGender.TabIndex = 8
        '
        'Label155
        '
        Me.Label155.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label155.AutoSize = True
        Me.Label155.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label155.ForeColor = System.Drawing.Color.Red
        Me.Label155.Location = New System.Drawing.Point(249, 26)
        Me.Label155.Name = "Label155"
        Me.Label155.Size = New System.Drawing.Size(18, 11)
        Me.Label155.TabIndex = 39
        Me.Label155.Text = "yrs"
        '
        'Label119
        '
        Me.Label119.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label119.AutoSize = True
        Me.Label119.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label119.ForeColor = System.Drawing.Color.Red
        Me.Label119.Location = New System.Drawing.Point(167, 26)
        Me.Label119.Name = "Label119"
        Me.Label119.Size = New System.Drawing.Size(18, 11)
        Me.Label119.TabIndex = 38
        Me.Label119.Text = "yrs"
        '
        'cmbAgeMax
        '
        Me.cmbAgeMax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAgeMax.Location = New System.Drawing.Point(232, 3)
        Me.cmbAgeMax.Name = "cmbAgeMax"
        Me.cmbAgeMax.Size = New System.Drawing.Size(53, 22)
        Me.cmbAgeMax.TabIndex = 7
        '
        'cmbAgeMin
        '
        Me.cmbAgeMin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAgeMin.ForeColor = System.Drawing.Color.Black
        Me.cmbAgeMin.Location = New System.Drawing.Point(149, 3)
        Me.cmbAgeMin.Name = "cmbAgeMin"
        Me.cmbAgeMin.Size = New System.Drawing.Size(55, 22)
        Me.cmbAgeMin.TabIndex = 6
        '
        'Label157
        '
        Me.Label157.AutoSize = True
        Me.Label157.BackColor = System.Drawing.Color.Transparent
        Me.Label157.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label157.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label157.Location = New System.Drawing.Point(347, 7)
        Me.Label157.Name = "Label157"
        Me.Label157.Size = New System.Drawing.Size(16, 14)
        Me.Label157.TabIndex = 37
        Me.Label157.Text = "in"
        Me.Label157.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAgeMax
        '
        Me.lblAgeMax.AutoSize = True
        Me.lblAgeMax.BackColor = System.Drawing.Color.Transparent
        Me.lblAgeMax.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAgeMax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblAgeMax.Location = New System.Drawing.Point(209, 7)
        Me.lblAgeMax.Name = "lblAgeMax"
        Me.lblAgeMax.Size = New System.Drawing.Size(19, 14)
        Me.lblAgeMax.TabIndex = 36
        Me.lblAgeMax.Text = "to"
        Me.lblAgeMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblGender
        '
        Me.lblGender.AutoSize = True
        Me.lblGender.BackColor = System.Drawing.Color.Transparent
        Me.lblGender.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGender.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGender.Location = New System.Drawing.Point(297, 7)
        Me.lblGender.Name = "lblGender"
        Me.lblGender.Size = New System.Drawing.Size(55, 14)
        Me.lblGender.TabIndex = 35
        Me.lblGender.Text = "Gender :"
        Me.lblGender.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label156
        '
        Me.Label156.AutoSize = True
        Me.Label156.BackColor = System.Drawing.Color.Transparent
        Me.Label156.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label156.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label156.Location = New System.Drawing.Point(91, 7)
        Me.Label156.Name = "Label156"
        Me.Label156.Size = New System.Drawing.Size(57, 14)
        Me.Label156.TabIndex = 33
        Me.Label156.Text = "between"
        Me.Label156.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAgeMin
        '
        Me.lblAgeMin.AutoSize = True
        Me.lblAgeMin.BackColor = System.Drawing.Color.Transparent
        Me.lblAgeMin.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAgeMin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblAgeMin.Location = New System.Drawing.Point(52, 7)
        Me.lblAgeMin.Name = "lblAgeMin"
        Me.lblAgeMin.Size = New System.Drawing.Size(37, 14)
        Me.lblAgeMin.TabIndex = 32
        Me.lblAgeMin.Text = "Age :"
        Me.lblAgeMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlCriteriaCheckboxes
        '
        Me.pnlCriteriaCheckboxes.Controls.Add(Me.Label49)
        Me.pnlCriteriaCheckboxes.Controls.Add(Me.chkEnableDemographics)
        Me.pnlCriteriaCheckboxes.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCriteriaCheckboxes.Location = New System.Drawing.Point(1, 1)
        Me.pnlCriteriaCheckboxes.Name = "pnlCriteriaCheckboxes"
        Me.pnlCriteriaCheckboxes.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlCriteriaCheckboxes.Size = New System.Drawing.Size(310, 39)
        Me.pnlCriteriaCheckboxes.TabIndex = 43
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label49.Location = New System.Drawing.Point(0, 3)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(310, 1)
        Me.Label49.TabIndex = 9
        Me.Label49.Text = "label2"
        '
        'chkEnableDemographics
        '
        Me.chkEnableDemographics.AutoSize = True
        Me.chkEnableDemographics.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.chkEnableDemographics.Location = New System.Drawing.Point(37, 15)
        Me.chkEnableDemographics.Name = "chkEnableDemographics"
        Me.chkEnableDemographics.Size = New System.Drawing.Size(103, 18)
        Me.chkEnableDemographics.TabIndex = 5
        Me.chkEnableDemographics.Text = "Enable Criteria"
        Me.chkEnableDemographics.UseVisualStyleBackColor = True
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(0, 1)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 165)
        Me.Label28.TabIndex = 50
        Me.Label28.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(311, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 165)
        Me.Label15.TabIndex = 49
        Me.Label15.Text = "label3"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(312, 1)
        Me.Label14.TabIndex = 48
        Me.Label14.Text = "label1"
        '
        'contextMenus
        '
        Me.contextMenus.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveAssociationToolStripMenuItem, Me.RemoveDemographicsToolStripMenuItem})
        Me.contextMenus.Name = "contextMenu"
        Me.contextMenus.Size = New System.Drawing.Size(182, 48)
        '
        'RemoveAssociationToolStripMenuItem
        '
        Me.RemoveAssociationToolStripMenuItem.Name = "RemoveAssociationToolStripMenuItem"
        Me.RemoveAssociationToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.RemoveAssociationToolStripMenuItem.Text = "Remove Association"
        '
        'RemoveDemographicsToolStripMenuItem
        '
        Me.RemoveDemographicsToolStripMenuItem.Name = "RemoveDemographicsToolStripMenuItem"
        Me.RemoveDemographicsToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.RemoveDemographicsToolStripMenuItem.Text = "Remove Criteria"
        '
        'contextRemoveParent
        '
        Me.contextRemoveParent.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripParent})
        Me.contextRemoveParent.Name = "contextMenu"
        Me.contextRemoveParent.Size = New System.Drawing.Size(118, 26)
        '
        'toolStripParent
        '
        Me.toolStripParent.Name = "toolStripParent"
        Me.toolStripParent.Size = New System.Drawing.Size(117, 22)
        Me.toolStripParent.Text = "Remove"
        '
        'frmEducationAssociation
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1000, 638)
        Me.Controls.Add(Me.pnlCodesAssociation)
        Me.Controls.Add(Me.pnlDemographicsFilter)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.rbICD9Desc)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.rbICD9Code)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.pnlRight)
        Me.Controls.Add(Me.pnltblMedication)
        Me.Controls.Add(Me.pnltxtsearchDrugs)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmEducationAssociation"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Education Material Mapping"
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlLeftRadioBtnTop.ResumeLayout(False)
        Me.pnlLeftRadioBtn.ResumeLayout(False)
        Me.pnlLeftRadioBtn.PerformLayout()
        Me.pnlICD10.ResumeLayout(False)
        Me.pnlLabs.ResumeLayout(False)
        Me.pnlMedication.ResumeLayout(False)
        Me.pnlSnomed.ResumeLayout(False)
        Me.pnlSnomed_add.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlICD9.ResumeLayout(False)
        Me.pnltrICD9.ResumeLayout(False)
        Me.pnltxtsearchDrugs.ResumeLayout(False)
        Me.pnltxtsearchDrugs.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRight.ResumeLayout(False)
        Me.pnlbtnProviderReference.ResumeLayout(False)
        Me.pnlbtnPatientEducation.ResumeLayout(False)
        Me.pnlCodesAssociation.ResumeLayout(False)
        Me.pnltblMedication.ResumeLayout(False)
        Me.pnltblMedication.PerformLayout()
        Me.tblMedication.ResumeLayout(False)
        Me.tblMedication.PerformLayout()
        Me.pnlDemographicsFilter.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlCriteriaLab.ResumeLayout(False)
        Me.pnlCriteriaLab.PerformLayout()
        Me.pnlCriteriaAge.ResumeLayout(False)
        Me.pnlCriteriaAge.PerformLayout()
        Me.pnlCriteriaCheckboxes.ResumeLayout(False)
        Me.pnlCriteriaCheckboxes.PerformLayout()
        Me.contextMenus.ResumeLayout(False)
        Me.contextRemoveParent.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents imgTreeView As System.Windows.Forms.ImageList
    Friend WithEvents UCtrlReferenceMaterial As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents pnlLabs As System.Windows.Forms.Panel
    Friend WithEvents btnLabs As System.Windows.Forms.Button
    Private WithEvents Label60 As System.Windows.Forms.Label
    Private WithEvents Label61 As System.Windows.Forms.Label
    Private WithEvents Label62 As System.Windows.Forms.Label
    Private WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents pnlMedication As System.Windows.Forms.Panel
    Friend WithEvents btnMedication As System.Windows.Forms.Button
    Private WithEvents Label56 As System.Windows.Forms.Label
    Private WithEvents Label57 As System.Windows.Forms.Label
    Private WithEvents Label58 As System.Windows.Forms.Label
    Private WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents pnlICD9 As System.Windows.Forms.Panel
    Friend WithEvents btnICD9 As System.Windows.Forms.Button
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents pnlSnomed_add As System.Windows.Forms.Panel
    Friend WithEvents btnSnoMed As System.Windows.Forms.Button
    Private WithEvents Label37 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Private WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents pnlDemographicsFilter As System.Windows.Forms.Panel
    Friend WithEvents contextMenus As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RemoveAssociationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlSnomed As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btn_AddSnoMed As System.Windows.Forms.Button
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents RemoveDemographicsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlLeftRadioBtnTop As System.Windows.Forms.Panel
    Friend WithEvents pnlLeftRadioBtn As System.Windows.Forms.Panel
    Friend WithEvents rbtUnassociated As System.Windows.Forms.RadioButton
    Friend WithEvents rbtAssociated As System.Windows.Forms.RadioButton
    Friend WithEvents rbtAll As System.Windows.Forms.RadioButton
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents pnlCriteriaLab As System.Windows.Forms.Panel
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbOperator As System.Windows.Forms.ComboBox
    Friend WithEvents txtValueTwo As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents pnlCriteriaAge As System.Windows.Forms.Panel
    Friend WithEvents cmbGender As System.Windows.Forms.ComboBox
    Friend WithEvents Label155 As System.Windows.Forms.Label
    Friend WithEvents Label119 As System.Windows.Forms.Label
    Friend WithEvents cmbAgeMax As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAgeMin As System.Windows.Forms.ComboBox
    Friend WithEvents Label157 As System.Windows.Forms.Label
    Friend WithEvents lblAgeMax As System.Windows.Forms.Label
    Friend WithEvents lblGender As System.Windows.Forms.Label
    Friend WithEvents Label156 As System.Windows.Forms.Label
    Friend WithEvents lblAgeMin As System.Windows.Forms.Label
    Friend WithEvents pnlCriteriaCheckboxes As System.Windows.Forms.Panel
    Friend WithEvents chkEnableDemographics As System.Windows.Forms.CheckBox
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtValueOne As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkAdvancedProviderReference As System.Windows.Forms.CheckBox
    Friend WithEvents contextRemoveParent As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents toolStripParent As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlICD10 As System.Windows.Forms.Panel
    Friend WithEvents btnICD10 As System.Windows.Forms.Button
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label36 As System.Windows.Forms.Label
    Private WithEvents Label49 As System.Windows.Forms.Label
End Class
