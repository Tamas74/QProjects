<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmICD9CPTGallery
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmICD9CPTGallery))
        Me.frmMain = New System.Windows.Forms.Panel()
        Me.pnlTab = New System.Windows.Forms.Panel()
        Me.tbICD9CPTGallery = New System.Windows.Forms.TabControl()
        Me.tbPageICD9 = New System.Windows.Forms.TabPage()
        Me.pnltbICD9 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.pnlCurrentICD9 = New System.Windows.Forms.Panel()
        Me.gloTrvCurrenICD9 = New gloUserControlLibrary.gloUC_TreeView()
        Me.ImgICD9CPT = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.cmbSpecialityICD9 = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.pnlICD9Gallery = New System.Windows.Forms.Panel()
        Me.gloTrvICD9Gallery = New gloUserControlLibrary.gloUC_TreeView()
        Me.pnlICD9Indicator = New System.Windows.Forms.Panel()
        Me.cmbICD9Gallery = New System.Windows.Forms.ComboBox()
        Me.pnllftHeaderICD9 = New System.Windows.Forms.Panel()
        Me.pnlICD9bottom = New System.Windows.Forms.Panel()
        Me.tbPageCPT = New System.Windows.Forms.TabPage()
        Me.pnltbCPT = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnlCurrentCPT = New System.Windows.Forms.Panel()
        Me.gloTrvCurrentCPT = New gloUserControlLibrary.gloUC_TreeView()
        Me.pnlComboCurrentCPT = New System.Windows.Forms.Panel()
        Me.cmbSpecialityCurrentCPT = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.cmbSpecialityICD9CPT = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.pnlCPTGallery = New System.Windows.Forms.Panel()
        Me.gloTrvCPTGallery = New gloUserControlLibrary.gloUC_TreeView()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.pnlICD9Center = New System.Windows.Forms.Panel()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblSpeciality = New System.Windows.Forms.Label()
        Me.cmbSpecialityCPT = New System.Windows.Forms.ComboBox()
        Me.pnlTOP = New System.Windows.Forms.Panel()
        Me.lblCopyRight = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnICD9Add = New System.Windows.Forms.Button()
        Me.btnICD9Remove = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnDeselectAllICD9 = New System.Windows.Forms.Button()
        Me.btnSelectAllICD9 = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lblCurrentICD9Header = New System.Windows.Forms.Label()
        Me.pnllftICD9 = New System.Windows.Forms.Panel()
        Me.btnDeselectAllICD9Gallery = New System.Windows.Forms.Button()
        Me.btnSelectAllICD9Gallery = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.lblICD9Galleryheader = New System.Windows.Forms.Label()
        Me.pnlLeftbottom = New System.Windows.Forms.Panel()
        Me.lblBlank = New System.Windows.Forms.Label()
        Me.PicBlank = New System.Windows.Forms.PictureBox()
        Me.lblrevise = New System.Windows.Forms.Label()
        Me.PicRevise = New System.Windows.Forms.PictureBox()
        Me.lblNew = New System.Windows.Forms.Label()
        Me.PicNew = New System.Windows.Forms.PictureBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnCPTRemove = New System.Windows.Forms.Button()
        Me.btnCPTAdd = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.btnDeselectAllCPT = New System.Windows.Forms.Button()
        Me.btnSelectAllCPT = New System.Windows.Forms.Button()
        Me.lblCurrentCPT = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.pnlhederCPT = New System.Windows.Forms.Panel()
        Me.btnDeselectAllCPTGallery = New System.Windows.Forms.Button()
        Me.btnSelectAllCPTGallery = New System.Windows.Forms.Button()
        Me.lblCPTGalleryheader = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.btnInsertCPT = New System.Windows.Forms.Button()
        Me.btnInsetICD9 = New System.Windows.Forms.Button()
        Me.tlICD9CptGallery = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbImportCPT = New System.Windows.Forms.ToolStripButton()
        Me.tlbImportIDC9 = New System.Windows.Forms.ToolStripButton()
        Me.tlbClearAll = New System.Windows.Forms.ToolStripButton()
        Me.tlbSelectAll = New System.Windows.Forms.ToolStripButton()
        Me.tlbCPTGallery = New System.Windows.Forms.ToolStripButton()
        Me.tlbICD9Gallery = New System.Windows.Forms.ToolStripButton()
        Me.tlbCurrentCPT = New System.Windows.Forms.ToolStripButton()
        Me.tlbCurrentICD9 = New System.Windows.Forms.ToolStripButton()
        Me.tlbClose = New System.Windows.Forms.ToolStripButton()
        Me.frmMain.SuspendLayout()
        Me.pnlTab.SuspendLayout()
        Me.tbICD9CPTGallery.SuspendLayout()
        Me.tbPageICD9.SuspendLayout()
        Me.pnltbICD9.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.pnlCurrentICD9.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlICD9Gallery.SuspendLayout()
        Me.pnlICD9Indicator.SuspendLayout()
        Me.pnllftHeaderICD9.SuspendLayout()
        Me.pnlICD9bottom.SuspendLayout()
        Me.tbPageCPT.SuspendLayout()
        Me.pnltbCPT.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlCurrentCPT.SuspendLayout()
        Me.pnlComboCurrentCPT.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlCPTGallery.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.pnlICD9Center.SuspendLayout()
        Me.pnlTOP.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnllftICD9.SuspendLayout()
        Me.pnlLeftbottom.SuspendLayout()
        CType(Me.PicBlank, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicRevise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.pnlhederCPT.SuspendLayout()
        Me.tlICD9CptGallery.SuspendLayout()
        Me.SuspendLayout()
        '
        'frmMain
        '
        Me.frmMain.Controls.Add(Me.pnlTab)
        Me.frmMain.Controls.Add(Me.pnlTOP)
        Me.frmMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.frmMain.Location = New System.Drawing.Point(0, 0)
        Me.frmMain.Name = "frmMain"
        Me.frmMain.Size = New System.Drawing.Size(925, 772)
        Me.frmMain.TabIndex = 0
        '
        'pnlTab
        '
        Me.pnlTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTab.Controls.Add(Me.tbICD9CPTGallery)
        Me.pnlTab.Controls.Add(Me.pnlICD9Center)
        Me.pnlTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTab.Location = New System.Drawing.Point(0, 56)
        Me.pnlTab.Name = "pnlTab"
        Me.pnlTab.Size = New System.Drawing.Size(925, 716)
        Me.pnlTab.TabIndex = 0
        '
        'tbICD9CPTGallery
        '
        Me.tbICD9CPTGallery.Controls.Add(Me.tbPageICD9)
        Me.tbICD9CPTGallery.Controls.Add(Me.tbPageCPT)
        Me.tbICD9CPTGallery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbICD9CPTGallery.Location = New System.Drawing.Point(0, 39)
        Me.tbICD9CPTGallery.Name = "tbICD9CPTGallery"
        Me.tbICD9CPTGallery.SelectedIndex = 0
        Me.tbICD9CPTGallery.Size = New System.Drawing.Size(925, 677)
        Me.tbICD9CPTGallery.TabIndex = 0
        '
        'tbPageICD9
        '
        Me.tbPageICD9.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbPageICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tbPageICD9.Controls.Add(Me.pnltbICD9)
        Me.tbPageICD9.Controls.Add(Me.pnlICD9bottom)
        Me.tbPageICD9.Location = New System.Drawing.Point(4, 23)
        Me.tbPageICD9.Name = "tbPageICD9"
        Me.tbPageICD9.Padding = New System.Windows.Forms.Padding(3)
        Me.tbPageICD9.Size = New System.Drawing.Size(917, 650)
        Me.tbPageICD9.TabIndex = 0
        Me.tbPageICD9.Tag = "0"
        Me.tbPageICD9.Text = "ICD9"
        '
        'pnltbICD9
        '
        Me.pnltbICD9.Controls.Add(Me.Panel6)
        Me.pnltbICD9.Controls.Add(Me.pnlCurrentICD9)
        Me.pnltbICD9.Controls.Add(Me.pnlICD9Gallery)
        Me.pnltbICD9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltbICD9.Location = New System.Drawing.Point(3, 3)
        Me.pnltbICD9.Name = "pnltbICD9"
        Me.pnltbICD9.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnltbICD9.Size = New System.Drawing.Size(911, 620)
        Me.pnltbICD9.TabIndex = 1
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.btnICD9Add)
        Me.Panel6.Controls.Add(Me.btnICD9Remove)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(400, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(111, 617)
        Me.Panel6.TabIndex = 1
        '
        'pnlCurrentICD9
        '
        Me.pnlCurrentICD9.Controls.Add(Me.gloTrvCurrenICD9)
        Me.pnlCurrentICD9.Controls.Add(Me.Panel9)
        Me.pnlCurrentICD9.Controls.Add(Me.Panel3)
        Me.pnlCurrentICD9.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlCurrentICD9.Location = New System.Drawing.Point(511, 0)
        Me.pnlCurrentICD9.Name = "pnlCurrentICD9"
        Me.pnlCurrentICD9.Size = New System.Drawing.Size(400, 617)
        Me.pnlCurrentICD9.TabIndex = 2
        '
        'gloTrvCurrenICD9
        '
        Me.gloTrvCurrenICD9.BackColor = System.Drawing.Color.Transparent
        Me.gloTrvCurrenICD9.CheckBoxes = False
        Me.gloTrvCurrenICD9.CodeMember = Nothing
        Me.gloTrvCurrenICD9.ColonAsSeparator = False
        Me.gloTrvCurrenICD9.Comment = Nothing
        Me.gloTrvCurrenICD9.ConceptID = Nothing
        Me.gloTrvCurrenICD9.CPT = Nothing
        Me.gloTrvCurrenICD9.DDIDMember = Nothing
        Me.gloTrvCurrenICD9.DescriptionMember = Nothing
        Me.gloTrvCurrenICD9.DisplayContextMenuStrip = Nothing
        Me.gloTrvCurrenICD9.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.gloTrvCurrenICD9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gloTrvCurrenICD9.DrugFlag = CType(16, Short)
        Me.gloTrvCurrenICD9.DrugFormMember = Nothing
        Me.gloTrvCurrenICD9.DrugQtyQualifierMember = Nothing
        Me.gloTrvCurrenICD9.DurationMember = Nothing
        Me.gloTrvCurrenICD9.EducationMappingSearchType = 1
        Me.gloTrvCurrenICD9.FrequencyMember = Nothing
        Me.gloTrvCurrenICD9.HistoryType = Nothing
        Me.gloTrvCurrenICD9.ICD9 = Nothing
        Me.gloTrvCurrenICD9.ICDRevision = Nothing
        Me.gloTrvCurrenICD9.ImageIndex = 0
        Me.gloTrvCurrenICD9.ImageList = Me.ImgICD9CPT
        Me.gloTrvCurrenICD9.ImageObject = Nothing
        Me.gloTrvCurrenICD9.Indicator = Nothing
        Me.gloTrvCurrenICD9.IsCPTSearch = False
        Me.gloTrvCurrenICD9.IsDiagnosisSearch = False
        Me.gloTrvCurrenICD9.IsDrug = False
        Me.gloTrvCurrenICD9.IsNarcoticsMember = Nothing
        Me.gloTrvCurrenICD9.IsSearchForEducationMapping = False
        Me.gloTrvCurrenICD9.IsSystemCategory = Nothing
        Me.gloTrvCurrenICD9.Location = New System.Drawing.Point(0, 53)
        Me.gloTrvCurrenICD9.MaximumNodes = 500
        Me.gloTrvCurrenICD9.mpidmember = Nothing
        Me.gloTrvCurrenICD9.Name = "gloTrvCurrenICD9"
        Me.gloTrvCurrenICD9.NDCCodeMember = Nothing
        Me.gloTrvCurrenICD9.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.gloTrvCurrenICD9.ParentImageIndex = 0
        Me.gloTrvCurrenICD9.ParentMember = Nothing
        Me.gloTrvCurrenICD9.RouteMember = Nothing
        Me.gloTrvCurrenICD9.RowOrderMember = Nothing
        Me.gloTrvCurrenICD9.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.gloTrvCurrenICD9.SearchBox = True
        Me.gloTrvCurrenICD9.SearchText = Nothing
        Me.gloTrvCurrenICD9.SelectedImageIndex = 0
        Me.gloTrvCurrenICD9.SelectedNode = Nothing
        Me.gloTrvCurrenICD9.SelectedNodeIDs = CType(resources.GetObject("gloTrvCurrenICD9.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.gloTrvCurrenICD9.SelectedParentImageIndex = 0
        Me.gloTrvCurrenICD9.Size = New System.Drawing.Size(400, 564)
        Me.gloTrvCurrenICD9.SmartTreatmentId = Nothing
        Me.gloTrvCurrenICD9.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.gloTrvCurrenICD9.TabIndex = 3
        Me.gloTrvCurrenICD9.Tag = Nothing
        Me.gloTrvCurrenICD9.UnitMember = Nothing
        Me.gloTrvCurrenICD9.ValueMember = Nothing
        '
        'ImgICD9CPT
        '
        Me.ImgICD9CPT.ImageStream = CType(resources.GetObject("ImgICD9CPT.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgICD9CPT.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgICD9CPT.Images.SetKeyName(0, "ICD 09.ico")
        Me.ImgICD9CPT.Images.SetKeyName(1, "CPT.ico")
        Me.ImgICD9CPT.Images.SetKeyName(2, "ICD9Gallery_01.ico")
        Me.ImgICD9CPT.Images.SetKeyName(3, "CPTGallery_01.ico")
        Me.ImgICD9CPT.Images.SetKeyName(4, "Bullet06.ico")
        Me.ImgICD9CPT.Images.SetKeyName(5, "Specialty.ico")
        Me.ImgICD9CPT.Images.SetKeyName(6, "Category_New.ico")
        Me.ImgICD9CPT.Images.SetKeyName(7, "Blank_02.ico")
        Me.ImgICD9CPT.Images.SetKeyName(8, "News_01.ico")
        Me.ImgICD9CPT.Images.SetKeyName(9, "Revied_04.ico")
        Me.ImgICD9CPT.Images.SetKeyName(10, "Small Speciality.ico")
        Me.ImgICD9CPT.Images.SetKeyName(11, "Category_01.ico")
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.cmbSpecialityICD9)
        Me.Panel9.Controls.Add(Me.Label4)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(0, 28)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel9.Size = New System.Drawing.Size(400, 25)
        Me.Panel9.TabIndex = 0
        '
        'cmbSpecialityICD9
        '
        Me.cmbSpecialityICD9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbSpecialityICD9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSpecialityICD9.ForeColor = System.Drawing.Color.Black
        Me.cmbSpecialityICD9.FormattingEnabled = True
        Me.cmbSpecialityICD9.Items.AddRange(New Object() {"All", "New", "Revised", "No Change"})
        Me.cmbSpecialityICD9.Location = New System.Drawing.Point(76, 0)
        Me.cmbSpecialityICD9.Name = "cmbSpecialityICD9"
        Me.cmbSpecialityICD9.Size = New System.Drawing.Size(324, 22)
        Me.cmbSpecialityICD9.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 22)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Specialty :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel3.Size = New System.Drawing.Size(400, 28)
        Me.Panel3.TabIndex = 1
        '
        'pnlICD9Gallery
        '
        Me.pnlICD9Gallery.Controls.Add(Me.gloTrvICD9Gallery)
        Me.pnlICD9Gallery.Controls.Add(Me.pnlICD9Indicator)
        Me.pnlICD9Gallery.Controls.Add(Me.pnllftHeaderICD9)
        Me.pnlICD9Gallery.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlICD9Gallery.Location = New System.Drawing.Point(0, 0)
        Me.pnlICD9Gallery.Name = "pnlICD9Gallery"
        Me.pnlICD9Gallery.Size = New System.Drawing.Size(400, 617)
        Me.pnlICD9Gallery.TabIndex = 0
        '
        'gloTrvICD9Gallery
        '
        Me.gloTrvICD9Gallery.BackColor = System.Drawing.Color.Transparent
        Me.gloTrvICD9Gallery.CheckBoxes = True
        Me.gloTrvICD9Gallery.CodeMember = Nothing
        Me.gloTrvICD9Gallery.ColonAsSeparator = False
        Me.gloTrvICD9Gallery.Comment = Nothing
        Me.gloTrvICD9Gallery.ConceptID = Nothing
        Me.gloTrvICD9Gallery.CPT = Nothing
        Me.gloTrvICD9Gallery.DDIDMember = Nothing
        Me.gloTrvICD9Gallery.DescriptionMember = Nothing
        Me.gloTrvICD9Gallery.DisplayContextMenuStrip = Nothing
        Me.gloTrvICD9Gallery.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.gloTrvICD9Gallery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gloTrvICD9Gallery.DrugFlag = CType(16, Short)
        Me.gloTrvICD9Gallery.DrugFormMember = Nothing
        Me.gloTrvICD9Gallery.DrugQtyQualifierMember = Nothing
        Me.gloTrvICD9Gallery.DurationMember = Nothing
        Me.gloTrvICD9Gallery.EducationMappingSearchType = 1
        Me.gloTrvICD9Gallery.FrequencyMember = Nothing
        Me.gloTrvICD9Gallery.HistoryType = Nothing
        Me.gloTrvICD9Gallery.ICD9 = Nothing
        Me.gloTrvICD9Gallery.ICDRevision = Nothing
        Me.gloTrvICD9Gallery.ImageIndex = 0
        Me.gloTrvICD9Gallery.ImageList = Me.ImgICD9CPT
        Me.gloTrvICD9Gallery.ImageObject = Nothing
        Me.gloTrvICD9Gallery.Indicator = Nothing
        Me.gloTrvICD9Gallery.IsCPTSearch = False
        Me.gloTrvICD9Gallery.IsDiagnosisSearch = False
        Me.gloTrvICD9Gallery.IsDrug = False
        Me.gloTrvICD9Gallery.IsNarcoticsMember = Nothing
        Me.gloTrvICD9Gallery.IsSearchForEducationMapping = False
        Me.gloTrvICD9Gallery.IsSystemCategory = Nothing
        Me.gloTrvICD9Gallery.Location = New System.Drawing.Point(0, 53)
        Me.gloTrvICD9Gallery.MaximumNodes = 500
        Me.gloTrvICD9Gallery.mpidmember = Nothing
        Me.gloTrvICD9Gallery.Name = "gloTrvICD9Gallery"
        Me.gloTrvICD9Gallery.NDCCodeMember = Nothing
        Me.gloTrvICD9Gallery.ParentImageIndex = 0
        Me.gloTrvICD9Gallery.ParentMember = Nothing
        Me.gloTrvICD9Gallery.RouteMember = Nothing
        Me.gloTrvICD9Gallery.RowOrderMember = Nothing
        Me.gloTrvICD9Gallery.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.gloTrvICD9Gallery.SearchBox = True
        Me.gloTrvICD9Gallery.SearchText = Nothing
        Me.gloTrvICD9Gallery.SelectedImageIndex = 0
        Me.gloTrvICD9Gallery.SelectedNode = Nothing
        Me.gloTrvICD9Gallery.SelectedNodeIDs = CType(resources.GetObject("gloTrvICD9Gallery.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.gloTrvICD9Gallery.SelectedParentImageIndex = 0
        Me.gloTrvICD9Gallery.Size = New System.Drawing.Size(400, 564)
        Me.gloTrvICD9Gallery.SmartTreatmentId = Nothing
        Me.gloTrvICD9Gallery.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.gloTrvICD9Gallery.TabIndex = 3
        Me.gloTrvICD9Gallery.Tag = Nothing
        Me.gloTrvICD9Gallery.UnitMember = Nothing
        Me.gloTrvICD9Gallery.ValueMember = Nothing
        '
        'pnlICD9Indicator
        '
        Me.pnlICD9Indicator.Controls.Add(Me.cmbICD9Gallery)
        Me.pnlICD9Indicator.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlICD9Indicator.Location = New System.Drawing.Point(0, 28)
        Me.pnlICD9Indicator.Name = "pnlICD9Indicator"
        Me.pnlICD9Indicator.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlICD9Indicator.Size = New System.Drawing.Size(400, 25)
        Me.pnlICD9Indicator.TabIndex = 0
        '
        'cmbICD9Gallery
        '
        Me.cmbICD9Gallery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbICD9Gallery.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbICD9Gallery.ForeColor = System.Drawing.Color.Black
        Me.cmbICD9Gallery.FormattingEnabled = True
        Me.cmbICD9Gallery.Items.AddRange(New Object() {"All", "New", "Revised", "No Change"})
        Me.cmbICD9Gallery.Location = New System.Drawing.Point(0, 0)
        Me.cmbICD9Gallery.Name = "cmbICD9Gallery"
        Me.cmbICD9Gallery.Size = New System.Drawing.Size(400, 22)
        Me.cmbICD9Gallery.TabIndex = 0
        '
        'pnllftHeaderICD9
        '
        Me.pnllftHeaderICD9.Controls.Add(Me.pnllftICD9)
        Me.pnllftHeaderICD9.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnllftHeaderICD9.Location = New System.Drawing.Point(0, 0)
        Me.pnllftHeaderICD9.Name = "pnllftHeaderICD9"
        Me.pnllftHeaderICD9.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnllftHeaderICD9.Size = New System.Drawing.Size(400, 28)
        Me.pnllftHeaderICD9.TabIndex = 1
        '
        'pnlICD9bottom
        '
        Me.pnlICD9bottom.Controls.Add(Me.pnlLeftbottom)
        Me.pnlICD9bottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlICD9bottom.Location = New System.Drawing.Point(3, 623)
        Me.pnlICD9bottom.Name = "pnlICD9bottom"
        Me.pnlICD9bottom.Size = New System.Drawing.Size(911, 24)
        Me.pnlICD9bottom.TabIndex = 2
        Me.pnlICD9bottom.TabStop = True
        '
        'tbPageCPT
        '
        Me.tbPageCPT.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbPageCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tbPageCPT.Controls.Add(Me.pnltbCPT)
        Me.tbPageCPT.Location = New System.Drawing.Point(4, 23)
        Me.tbPageCPT.Name = "tbPageCPT"
        Me.tbPageCPT.Padding = New System.Windows.Forms.Padding(3)
        Me.tbPageCPT.Size = New System.Drawing.Size(917, 650)
        Me.tbPageCPT.TabIndex = 1
        Me.tbPageCPT.Tag = "1"
        Me.tbPageCPT.Text = "CPT"
        '
        'pnltbCPT
        '
        Me.pnltbCPT.Controls.Add(Me.Panel1)
        Me.pnltbCPT.Controls.Add(Me.pnlCurrentCPT)
        Me.pnltbCPT.Controls.Add(Me.pnlCPTGallery)
        Me.pnltbCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltbCPT.Location = New System.Drawing.Point(3, 3)
        Me.pnltbCPT.Name = "pnltbCPT"
        Me.pnltbCPT.Size = New System.Drawing.Size(911, 645)
        Me.pnltbCPT.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnCPTRemove)
        Me.Panel1.Controls.Add(Me.btnCPTAdd)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(400, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(111, 645)
        Me.Panel1.TabIndex = 2
        '
        'pnlCurrentCPT
        '
        Me.pnlCurrentCPT.Controls.Add(Me.gloTrvCurrentCPT)
        Me.pnlCurrentCPT.Controls.Add(Me.pnlComboCurrentCPT)
        Me.pnlCurrentCPT.Controls.Add(Me.Panel8)
        Me.pnlCurrentCPT.Controls.Add(Me.Panel2)
        Me.pnlCurrentCPT.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlCurrentCPT.Location = New System.Drawing.Point(511, 0)
        Me.pnlCurrentCPT.Name = "pnlCurrentCPT"
        Me.pnlCurrentCPT.Size = New System.Drawing.Size(400, 645)
        Me.pnlCurrentCPT.TabIndex = 3
        '
        'gloTrvCurrentCPT
        '
        Me.gloTrvCurrentCPT.AutoSize = True
        Me.gloTrvCurrentCPT.BackColor = System.Drawing.Color.Transparent
        Me.gloTrvCurrentCPT.CheckBoxes = False
        Me.gloTrvCurrentCPT.CodeMember = Nothing
        Me.gloTrvCurrentCPT.ColonAsSeparator = False
        Me.gloTrvCurrentCPT.Comment = Nothing
        Me.gloTrvCurrentCPT.ConceptID = Nothing
        Me.gloTrvCurrentCPT.CPT = Nothing
        Me.gloTrvCurrentCPT.DDIDMember = Nothing
        Me.gloTrvCurrentCPT.DescriptionMember = Nothing
        Me.gloTrvCurrentCPT.DisplayContextMenuStrip = Nothing
        Me.gloTrvCurrentCPT.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.gloTrvCurrentCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gloTrvCurrentCPT.DrugFlag = CType(16, Short)
        Me.gloTrvCurrentCPT.DrugFormMember = Nothing
        Me.gloTrvCurrentCPT.DrugQtyQualifierMember = Nothing
        Me.gloTrvCurrentCPT.DurationMember = Nothing
        Me.gloTrvCurrentCPT.EducationMappingSearchType = 1
        Me.gloTrvCurrentCPT.FrequencyMember = Nothing
        Me.gloTrvCurrentCPT.HistoryType = Nothing
        Me.gloTrvCurrentCPT.ICD9 = Nothing
        Me.gloTrvCurrentCPT.ICDRevision = Nothing
        Me.gloTrvCurrentCPT.ImageIndex = 0
        Me.gloTrvCurrentCPT.ImageList = Me.ImgICD9CPT
        Me.gloTrvCurrentCPT.ImageObject = Nothing
        Me.gloTrvCurrentCPT.Indicator = Nothing
        Me.gloTrvCurrentCPT.IsCPTSearch = False
        Me.gloTrvCurrentCPT.IsDiagnosisSearch = False
        Me.gloTrvCurrentCPT.IsDrug = False
        Me.gloTrvCurrentCPT.IsNarcoticsMember = Nothing
        Me.gloTrvCurrentCPT.IsSearchForEducationMapping = False
        Me.gloTrvCurrentCPT.IsSystemCategory = Nothing
        Me.gloTrvCurrentCPT.Location = New System.Drawing.Point(0, 76)
        Me.gloTrvCurrentCPT.MaximumNodes = 500
        Me.gloTrvCurrentCPT.mpidmember = Nothing
        Me.gloTrvCurrentCPT.Name = "gloTrvCurrentCPT"
        Me.gloTrvCurrentCPT.NDCCodeMember = Nothing
        Me.gloTrvCurrentCPT.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.gloTrvCurrentCPT.ParentImageIndex = 0
        Me.gloTrvCurrentCPT.ParentMember = Nothing
        Me.gloTrvCurrentCPT.RouteMember = Nothing
        Me.gloTrvCurrentCPT.RowOrderMember = Nothing
        Me.gloTrvCurrentCPT.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.gloTrvCurrentCPT.SearchBox = True
        Me.gloTrvCurrentCPT.SearchText = Nothing
        Me.gloTrvCurrentCPT.SelectedImageIndex = 0
        Me.gloTrvCurrentCPT.SelectedNode = Nothing
        Me.gloTrvCurrentCPT.SelectedNodeIDs = CType(resources.GetObject("gloTrvCurrentCPT.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.gloTrvCurrentCPT.SelectedParentImageIndex = 0
        Me.gloTrvCurrentCPT.Size = New System.Drawing.Size(400, 569)
        Me.gloTrvCurrentCPT.SmartTreatmentId = Nothing
        Me.gloTrvCurrentCPT.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.gloTrvCurrentCPT.TabIndex = 3
        Me.gloTrvCurrentCPT.Tag = Nothing
        Me.gloTrvCurrentCPT.UnitMember = Nothing
        Me.gloTrvCurrentCPT.ValueMember = Nothing
        '
        'pnlComboCurrentCPT
        '
        Me.pnlComboCurrentCPT.Controls.Add(Me.cmbSpecialityCurrentCPT)
        Me.pnlComboCurrentCPT.Controls.Add(Me.Label3)
        Me.pnlComboCurrentCPT.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlComboCurrentCPT.Location = New System.Drawing.Point(0, 51)
        Me.pnlComboCurrentCPT.Name = "pnlComboCurrentCPT"
        Me.pnlComboCurrentCPT.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlComboCurrentCPT.Size = New System.Drawing.Size(400, 25)
        Me.pnlComboCurrentCPT.TabIndex = 1
        '
        'cmbSpecialityCurrentCPT
        '
        Me.cmbSpecialityCurrentCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbSpecialityCurrentCPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSpecialityCurrentCPT.ForeColor = System.Drawing.Color.Black
        Me.cmbSpecialityCurrentCPT.FormattingEnabled = True
        Me.cmbSpecialityCurrentCPT.Items.AddRange(New Object() {"All", "New", "Revised", "No Change"})
        Me.cmbSpecialityCurrentCPT.Location = New System.Drawing.Point(76, 3)
        Me.cmbSpecialityCurrentCPT.Name = "cmbSpecialityCurrentCPT"
        Me.cmbSpecialityCurrentCPT.Size = New System.Drawing.Size(324, 22)
        Me.cmbSpecialityCurrentCPT.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 22)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Category :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.cmbSpecialityICD9CPT)
        Me.Panel8.Controls.Add(Me.Label1)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(0, 28)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(400, 23)
        Me.Panel8.TabIndex = 0
        '
        'cmbSpecialityICD9CPT
        '
        Me.cmbSpecialityICD9CPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cmbSpecialityICD9CPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSpecialityICD9CPT.FormattingEnabled = True
        Me.cmbSpecialityICD9CPT.Location = New System.Drawing.Point(76, 0)
        Me.cmbSpecialityICD9CPT.Name = "cmbSpecialityICD9CPT"
        Me.cmbSpecialityICD9CPT.Size = New System.Drawing.Size(324, 22)
        Me.cmbSpecialityICD9CPT.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 23)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Specialty :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel2.Size = New System.Drawing.Size(400, 28)
        Me.Panel2.TabIndex = 2
        '
        'pnlCPTGallery
        '
        Me.pnlCPTGallery.Controls.Add(Me.gloTrvCPTGallery)
        Me.pnlCPTGallery.Controls.Add(Me.Panel11)
        Me.pnlCPTGallery.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlCPTGallery.Location = New System.Drawing.Point(0, 0)
        Me.pnlCPTGallery.Name = "pnlCPTGallery"
        Me.pnlCPTGallery.Size = New System.Drawing.Size(400, 645)
        Me.pnlCPTGallery.TabIndex = 0
        '
        'gloTrvCPTGallery
        '
        Me.gloTrvCPTGallery.BackColor = System.Drawing.Color.Transparent
        Me.gloTrvCPTGallery.CheckBoxes = True
        Me.gloTrvCPTGallery.CodeMember = Nothing
        Me.gloTrvCPTGallery.ColonAsSeparator = False
        Me.gloTrvCPTGallery.Comment = Nothing
        Me.gloTrvCPTGallery.ConceptID = Nothing
        Me.gloTrvCPTGallery.CPT = Nothing
        Me.gloTrvCPTGallery.DDIDMember = Nothing
        Me.gloTrvCPTGallery.DescriptionMember = Nothing
        Me.gloTrvCPTGallery.DisplayContextMenuStrip = Nothing
        Me.gloTrvCPTGallery.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.gloTrvCPTGallery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gloTrvCPTGallery.DrugFlag = CType(16, Short)
        Me.gloTrvCPTGallery.DrugFormMember = Nothing
        Me.gloTrvCPTGallery.DrugQtyQualifierMember = Nothing
        Me.gloTrvCPTGallery.DurationMember = Nothing
        Me.gloTrvCPTGallery.EducationMappingSearchType = 1
        Me.gloTrvCPTGallery.FrequencyMember = Nothing
        Me.gloTrvCPTGallery.HistoryType = Nothing
        Me.gloTrvCPTGallery.ICD9 = Nothing
        Me.gloTrvCPTGallery.ICDRevision = Nothing
        Me.gloTrvCPTGallery.ImageIndex = 0
        Me.gloTrvCPTGallery.ImageList = Me.ImgICD9CPT
        Me.gloTrvCPTGallery.ImageObject = Nothing
        Me.gloTrvCPTGallery.Indicator = Nothing
        Me.gloTrvCPTGallery.IsCPTSearch = False
        Me.gloTrvCPTGallery.IsDiagnosisSearch = False
        Me.gloTrvCPTGallery.IsDrug = False
        Me.gloTrvCPTGallery.IsNarcoticsMember = Nothing
        Me.gloTrvCPTGallery.IsSearchForEducationMapping = False
        Me.gloTrvCPTGallery.IsSystemCategory = Nothing
        Me.gloTrvCPTGallery.Location = New System.Drawing.Point(0, 28)
        Me.gloTrvCPTGallery.MaximumNodes = 500
        Me.gloTrvCPTGallery.mpidmember = Nothing
        Me.gloTrvCPTGallery.Name = "gloTrvCPTGallery"
        Me.gloTrvCPTGallery.NDCCodeMember = Nothing
        Me.gloTrvCPTGallery.ParentImageIndex = 0
        Me.gloTrvCPTGallery.ParentMember = Nothing
        Me.gloTrvCPTGallery.RouteMember = Nothing
        Me.gloTrvCPTGallery.RowOrderMember = Nothing
        Me.gloTrvCPTGallery.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.gloTrvCPTGallery.SearchBox = True
        Me.gloTrvCPTGallery.SearchText = Nothing
        Me.gloTrvCPTGallery.SelectedImageIndex = 0
        Me.gloTrvCPTGallery.SelectedNode = Nothing
        Me.gloTrvCPTGallery.SelectedNodeIDs = CType(resources.GetObject("gloTrvCPTGallery.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.gloTrvCPTGallery.SelectedParentImageIndex = 0
        Me.gloTrvCPTGallery.Size = New System.Drawing.Size(400, 617)
        Me.gloTrvCPTGallery.SmartTreatmentId = Nothing
        Me.gloTrvCPTGallery.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.gloTrvCPTGallery.TabIndex = 0
        Me.gloTrvCPTGallery.Tag = Nothing
        Me.gloTrvCPTGallery.UnitMember = Nothing
        Me.gloTrvCPTGallery.ValueMember = Nothing
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.pnlhederCPT)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(0, 0)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel11.Size = New System.Drawing.Size(400, 28)
        Me.Panel11.TabIndex = 1
        '
        'pnlICD9Center
        '
        Me.pnlICD9Center.BackColor = System.Drawing.Color.Transparent
        Me.pnlICD9Center.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlICD9Center.Controls.Add(Me.Label15)
        Me.pnlICD9Center.Controls.Add(Me.btnInsertCPT)
        Me.pnlICD9Center.Controls.Add(Me.Label12)
        Me.pnlICD9Center.Controls.Add(Me.btnInsetICD9)
        Me.pnlICD9Center.Controls.Add(Me.Label11)
        Me.pnlICD9Center.Controls.Add(Me.Label10)
        Me.pnlICD9Center.Controls.Add(Me.lblSpeciality)
        Me.pnlICD9Center.Controls.Add(Me.cmbSpecialityCPT)
        Me.pnlICD9Center.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlICD9Center.Location = New System.Drawing.Point(0, 0)
        Me.pnlICD9Center.MinimumSize = New System.Drawing.Size(52, 0)
        Me.pnlICD9Center.Name = "pnlICD9Center"
        Me.pnlICD9Center.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlICD9Center.Size = New System.Drawing.Size(925, 39)
        Me.pnlICD9Center.TabIndex = 1
        Me.pnlICD9Center.TabStop = True
        Me.pnlICD9Center.Visible = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(4, 35)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(917, 1)
        Me.Label15.TabIndex = 15
        Me.Label15.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(4, 3)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(917, 1)
        Me.Label12.TabIndex = 14
        Me.Label12.Text = "label1"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(921, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 33)
        Me.Label11.TabIndex = 13
        Me.Label11.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 33)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "label4"
        '
        'lblSpeciality
        '
        Me.lblSpeciality.AutoSize = True
        Me.lblSpeciality.Location = New System.Drawing.Point(14, 11)
        Me.lblSpeciality.Name = "lblSpeciality"
        Me.lblSpeciality.Size = New System.Drawing.Size(63, 14)
        Me.lblSpeciality.TabIndex = 4
        Me.lblSpeciality.Text = "Specialty :"
        Me.lblSpeciality.Visible = False
        '
        'cmbSpecialityCPT
        '
        Me.cmbSpecialityCPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSpecialityCPT.ForeColor = System.Drawing.Color.Black
        Me.cmbSpecialityCPT.FormattingEnabled = True
        Me.cmbSpecialityCPT.Items.AddRange(New Object() {"All", "New", "Revised", "No Change"})
        Me.cmbSpecialityCPT.Location = New System.Drawing.Point(80, 7)
        Me.cmbSpecialityCPT.Name = "cmbSpecialityCPT"
        Me.cmbSpecialityCPT.Size = New System.Drawing.Size(320, 22)
        Me.cmbSpecialityCPT.TabIndex = 9
        Me.cmbSpecialityCPT.Visible = False
        '
        'pnlTOP
        '
        Me.pnlTOP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTOP.Controls.Add(Me.lblCopyRight)
        Me.pnlTOP.Controls.Add(Me.tlICD9CptGallery)
        Me.pnlTOP.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTOP.Location = New System.Drawing.Point(0, 0)
        Me.pnlTOP.Name = "pnlTOP"
        Me.pnlTOP.Size = New System.Drawing.Size(925, 56)
        Me.pnlTOP.TabIndex = 0
        '
        'lblCopyRight
        '
        Me.lblCopyRight.AutoSize = True
        Me.lblCopyRight.BackColor = System.Drawing.Color.Transparent
        Me.lblCopyRight.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyRight.Location = New System.Drawing.Point(522, 31)
        Me.lblCopyRight.Name = "lblCopyRight"
        Me.lblCopyRight.Size = New System.Drawing.Size(397, 14)
        Me.lblCopyRight.TabIndex = 9
        Me.lblCopyRight.Text = "CPT™ copyright 2012 American Medical Association. All rights reserved."
        '
        'btnICD9Add
        '
        Me.btnICD9Add.AutoSize = True
        Me.btnICD9Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnICD9Add.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnICD9Add.FlatAppearance.BorderSize = 0
        Me.btnICD9Add.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnICD9Add.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnICD9Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnICD9Add.Image = CType(resources.GetObject("btnICD9Add.Image"), System.Drawing.Image)
        Me.btnICD9Add.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnICD9Add.Location = New System.Drawing.Point(37, 266)
        Me.btnICD9Add.Name = "btnICD9Add"
        Me.btnICD9Add.Size = New System.Drawing.Size(36, 36)
        Me.btnICD9Add.TabIndex = 0
        Me.btnICD9Add.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnICD9Add, "Add and Save ICD9")
        Me.btnICD9Add.UseVisualStyleBackColor = True
        '
        'btnICD9Remove
        '
        Me.btnICD9Remove.AutoSize = True
        Me.btnICD9Remove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnICD9Remove.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnICD9Remove.FlatAppearance.BorderSize = 0
        Me.btnICD9Remove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnICD9Remove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnICD9Remove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnICD9Remove.Image = CType(resources.GetObject("btnICD9Remove.Image"), System.Drawing.Image)
        Me.btnICD9Remove.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnICD9Remove.Location = New System.Drawing.Point(37, 314)
        Me.btnICD9Remove.Name = "btnICD9Remove"
        Me.btnICD9Remove.Size = New System.Drawing.Size(36, 36)
        Me.btnICD9Remove.TabIndex = 1
        Me.btnICD9Remove.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.btnICD9Remove, "Remove and Save ICD9")
        Me.btnICD9Remove.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.btnDeselectAllICD9)
        Me.Panel4.Controls.Add(Me.btnSelectAllICD9)
        Me.Panel4.Controls.Add(Me.Label17)
        Me.Panel4.Controls.Add(Me.Label18)
        Me.Panel4.Controls.Add(Me.Label19)
        Me.Panel4.Controls.Add(Me.Label20)
        Me.Panel4.Controls.Add(Me.lblCurrentICD9Header)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.ForeColor = System.Drawing.Color.White
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(400, 25)
        Me.Panel4.TabIndex = 1
        '
        'btnDeselectAllICD9
        '
        Me.btnDeselectAllICD9.BackColor = System.Drawing.Color.Transparent
        Me.btnDeselectAllICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDeselectAllICD9.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDeselectAllICD9.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDeselectAllICD9.FlatAppearance.BorderSize = 0
        Me.btnDeselectAllICD9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDeselectAllICD9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDeselectAllICD9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeselectAllICD9.Image = CType(resources.GetObject("btnDeselectAllICD9.Image"), System.Drawing.Image)
        Me.btnDeselectAllICD9.Location = New System.Drawing.Point(349, 1)
        Me.btnDeselectAllICD9.Name = "btnDeselectAllICD9"
        Me.btnDeselectAllICD9.Size = New System.Drawing.Size(25, 23)
        Me.btnDeselectAllICD9.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.btnDeselectAllICD9, "Deselect All")
        Me.btnDeselectAllICD9.UseVisualStyleBackColor = False
        Me.btnDeselectAllICD9.Visible = False
        '
        'btnSelectAllICD9
        '
        Me.btnSelectAllICD9.BackColor = System.Drawing.Color.Transparent
        Me.btnSelectAllICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSelectAllICD9.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSelectAllICD9.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSelectAllICD9.FlatAppearance.BorderSize = 0
        Me.btnSelectAllICD9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSelectAllICD9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSelectAllICD9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectAllICD9.Image = CType(resources.GetObject("btnSelectAllICD9.Image"), System.Drawing.Image)
        Me.btnSelectAllICD9.Location = New System.Drawing.Point(374, 1)
        Me.btnSelectAllICD9.Name = "btnSelectAllICD9"
        Me.btnSelectAllICD9.Size = New System.Drawing.Size(25, 23)
        Me.btnSelectAllICD9.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnSelectAllICD9, "Select All")
        Me.btnSelectAllICD9.UseVisualStyleBackColor = False
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(1, 24)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(398, 1)
        Me.Label17.TabIndex = 12
        Me.Label17.Text = "label2"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(0, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 24)
        Me.Label18.TabIndex = 11
        Me.Label18.Text = "label4"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(399, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 24)
        Me.Label19.TabIndex = 10
        Me.Label19.Text = "label3"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(0, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(400, 1)
        Me.Label20.TabIndex = 9
        Me.Label20.Text = "label1"
        '
        'lblCurrentICD9Header
        '
        Me.lblCurrentICD9Header.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCurrentICD9Header.Location = New System.Drawing.Point(0, 0)
        Me.lblCurrentICD9Header.Name = "lblCurrentICD9Header"
        Me.lblCurrentICD9Header.Size = New System.Drawing.Size(400, 25)
        Me.lblCurrentICD9Header.TabIndex = 0
        Me.lblCurrentICD9Header.Text = "Current ICD9"
        Me.lblCurrentICD9Header.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnllftICD9
        '
        Me.pnllftICD9.BackColor = System.Drawing.Color.Transparent
        Me.pnllftICD9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.pnllftICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnllftICD9.Controls.Add(Me.btnDeselectAllICD9Gallery)
        Me.pnllftICD9.Controls.Add(Me.btnSelectAllICD9Gallery)
        Me.pnllftICD9.Controls.Add(Me.Label2)
        Me.pnllftICD9.Controls.Add(Me.Label13)
        Me.pnllftICD9.Controls.Add(Me.Label14)
        Me.pnllftICD9.Controls.Add(Me.Label23)
        Me.pnllftICD9.Controls.Add(Me.lblICD9Galleryheader)
        Me.pnllftICD9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnllftICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnllftICD9.ForeColor = System.Drawing.Color.White
        Me.pnllftICD9.Location = New System.Drawing.Point(0, 0)
        Me.pnllftICD9.Name = "pnllftICD9"
        Me.pnllftICD9.Size = New System.Drawing.Size(400, 25)
        Me.pnllftICD9.TabIndex = 1
        '
        'btnDeselectAllICD9Gallery
        '
        Me.btnDeselectAllICD9Gallery.BackColor = System.Drawing.Color.Transparent
        Me.btnDeselectAllICD9Gallery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDeselectAllICD9Gallery.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDeselectAllICD9Gallery.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDeselectAllICD9Gallery.FlatAppearance.BorderSize = 0
        Me.btnDeselectAllICD9Gallery.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDeselectAllICD9Gallery.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDeselectAllICD9Gallery.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeselectAllICD9Gallery.Image = CType(resources.GetObject("btnDeselectAllICD9Gallery.Image"), System.Drawing.Image)
        Me.btnDeselectAllICD9Gallery.Location = New System.Drawing.Point(349, 1)
        Me.btnDeselectAllICD9Gallery.Name = "btnDeselectAllICD9Gallery"
        Me.btnDeselectAllICD9Gallery.Size = New System.Drawing.Size(25, 23)
        Me.btnDeselectAllICD9Gallery.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.btnDeselectAllICD9Gallery, "Deselect All")
        Me.btnDeselectAllICD9Gallery.UseVisualStyleBackColor = False
        Me.btnDeselectAllICD9Gallery.Visible = False
        '
        'btnSelectAllICD9Gallery
        '
        Me.btnSelectAllICD9Gallery.BackColor = System.Drawing.Color.Transparent
        Me.btnSelectAllICD9Gallery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSelectAllICD9Gallery.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSelectAllICD9Gallery.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSelectAllICD9Gallery.FlatAppearance.BorderSize = 0
        Me.btnSelectAllICD9Gallery.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSelectAllICD9Gallery.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSelectAllICD9Gallery.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectAllICD9Gallery.Image = CType(resources.GetObject("btnSelectAllICD9Gallery.Image"), System.Drawing.Image)
        Me.btnSelectAllICD9Gallery.Location = New System.Drawing.Point(374, 1)
        Me.btnSelectAllICD9Gallery.Name = "btnSelectAllICD9Gallery"
        Me.btnSelectAllICD9Gallery.Size = New System.Drawing.Size(25, 23)
        Me.btnSelectAllICD9Gallery.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnSelectAllICD9Gallery, "Select All")
        Me.btnSelectAllICD9Gallery.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(1, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(398, 1)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "label2"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(0, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 24)
        Me.Label13.TabIndex = 11
        Me.Label13.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(399, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 24)
        Me.Label14.TabIndex = 10
        Me.Label14.Text = "label3"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(400, 1)
        Me.Label23.TabIndex = 9
        Me.Label23.Text = "label1"
        '
        'lblICD9Galleryheader
        '
        Me.lblICD9Galleryheader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblICD9Galleryheader.Location = New System.Drawing.Point(0, 0)
        Me.lblICD9Galleryheader.Name = "lblICD9Galleryheader"
        Me.lblICD9Galleryheader.Size = New System.Drawing.Size(400, 25)
        Me.lblICD9Galleryheader.TabIndex = 0
        Me.lblICD9Galleryheader.Text = "ICD9 Gallery"
        Me.lblICD9Galleryheader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlLeftbottom
        '
        Me.pnlLeftbottom.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlLeftbottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLeftbottom.Controls.Add(Me.lblBlank)
        Me.pnlLeftbottom.Controls.Add(Me.PicBlank)
        Me.pnlLeftbottom.Controls.Add(Me.lblrevise)
        Me.pnlLeftbottom.Controls.Add(Me.PicRevise)
        Me.pnlLeftbottom.Controls.Add(Me.lblNew)
        Me.pnlLeftbottom.Controls.Add(Me.PicNew)
        Me.pnlLeftbottom.Controls.Add(Me.Label16)
        Me.pnlLeftbottom.Controls.Add(Me.Label5)
        Me.pnlLeftbottom.Controls.Add(Me.Label6)
        Me.pnlLeftbottom.Controls.Add(Me.Label7)
        Me.pnlLeftbottom.Controls.Add(Me.Label8)
        Me.pnlLeftbottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeftbottom.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeftbottom.Name = "pnlLeftbottom"
        Me.pnlLeftbottom.Size = New System.Drawing.Size(911, 24)
        Me.pnlLeftbottom.TabIndex = 6
        '
        'lblBlank
        '
        Me.lblBlank.BackColor = System.Drawing.Color.Transparent
        Me.lblBlank.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblBlank.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBlank.Location = New System.Drawing.Point(177, 1)
        Me.lblBlank.Name = "lblBlank"
        Me.lblBlank.Size = New System.Drawing.Size(90, 22)
        Me.lblBlank.TabIndex = 4
        Me.lblBlank.Text = "No Change"
        Me.lblBlank.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PicBlank
        '
        Me.PicBlank.BackColor = System.Drawing.Color.Transparent
        Me.PicBlank.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicBlank.Image = CType(resources.GetObject("PicBlank.Image"), System.Drawing.Image)
        Me.PicBlank.Location = New System.Drawing.Point(159, 1)
        Me.PicBlank.Name = "PicBlank"
        Me.PicBlank.Size = New System.Drawing.Size(18, 22)
        Me.PicBlank.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicBlank.TabIndex = 5
        Me.PicBlank.TabStop = False
        '
        'lblrevise
        '
        Me.lblrevise.BackColor = System.Drawing.Color.Transparent
        Me.lblrevise.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblrevise.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblrevise.Location = New System.Drawing.Point(89, 1)
        Me.lblrevise.Name = "lblrevise"
        Me.lblrevise.Size = New System.Drawing.Size(70, 22)
        Me.lblrevise.TabIndex = 2
        Me.lblrevise.Text = "Revised"
        Me.lblrevise.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PicRevise
        '
        Me.PicRevise.BackColor = System.Drawing.Color.Transparent
        Me.PicRevise.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicRevise.Image = CType(resources.GetObject("PicRevise.Image"), System.Drawing.Image)
        Me.PicRevise.Location = New System.Drawing.Point(71, 1)
        Me.PicRevise.Name = "PicRevise"
        Me.PicRevise.Size = New System.Drawing.Size(18, 22)
        Me.PicRevise.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicRevise.TabIndex = 3
        Me.PicRevise.TabStop = False
        '
        'lblNew
        '
        Me.lblNew.BackColor = System.Drawing.Color.Transparent
        Me.lblNew.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblNew.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNew.Location = New System.Drawing.Point(24, 1)
        Me.lblNew.Name = "lblNew"
        Me.lblNew.Size = New System.Drawing.Size(47, 22)
        Me.lblNew.TabIndex = 0
        Me.lblNew.Text = "New    "
        Me.lblNew.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PicNew
        '
        Me.PicNew.BackColor = System.Drawing.Color.Transparent
        Me.PicNew.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicNew.Image = CType(resources.GetObject("PicNew.Image"), System.Drawing.Image)
        Me.PicNew.Location = New System.Drawing.Point(7, 1)
        Me.PicNew.Name = "PicNew"
        Me.PicNew.Size = New System.Drawing.Size(17, 22)
        Me.PicNew.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicNew.TabIndex = 1
        Me.PicNew.TabStop = False
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(1, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(6, 22)
        Me.Label16.TabIndex = 13
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(909, 1)
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
        Me.Label6.Size = New System.Drawing.Size(1, 23)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(910, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 23)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(911, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'btnCPTRemove
        '
        Me.btnCPTRemove.AutoSize = True
        Me.btnCPTRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCPTRemove.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCPTRemove.FlatAppearance.BorderSize = 0
        Me.btnCPTRemove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnCPTRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnCPTRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCPTRemove.Image = CType(resources.GetObject("btnCPTRemove.Image"), System.Drawing.Image)
        Me.btnCPTRemove.Location = New System.Drawing.Point(41, 336)
        Me.btnCPTRemove.Name = "btnCPTRemove"
        Me.btnCPTRemove.Size = New System.Drawing.Size(36, 36)
        Me.btnCPTRemove.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnCPTRemove, "Remove and Save CPT")
        Me.btnCPTRemove.UseVisualStyleBackColor = True
        '
        'btnCPTAdd
        '
        Me.btnCPTAdd.AutoSize = True
        Me.btnCPTAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCPTAdd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCPTAdd.FlatAppearance.BorderSize = 0
        Me.btnCPTAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnCPTAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnCPTAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCPTAdd.Image = CType(resources.GetObject("btnCPTAdd.Image"), System.Drawing.Image)
        Me.btnCPTAdd.Location = New System.Drawing.Point(41, 283)
        Me.btnCPTAdd.Name = "btnCPTAdd"
        Me.btnCPTAdd.Size = New System.Drawing.Size(36, 36)
        Me.btnCPTAdd.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.btnCPTAdd, "Add and Save CPT")
        Me.btnCPTAdd.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.btnDeselectAllCPT)
        Me.Panel5.Controls.Add(Me.btnSelectAllCPT)
        Me.Panel5.Controls.Add(Me.lblCurrentCPT)
        Me.Panel5.Controls.Add(Me.Label22)
        Me.Panel5.Controls.Add(Me.Label24)
        Me.Panel5.Controls.Add(Me.Label25)
        Me.Panel5.Controls.Add(Me.Label26)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel5.ForeColor = System.Drawing.Color.White
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(400, 25)
        Me.Panel5.TabIndex = 1
        '
        'btnDeselectAllCPT
        '
        Me.btnDeselectAllCPT.BackColor = System.Drawing.Color.Transparent
        Me.btnDeselectAllCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnDeselectAllCPT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDeselectAllCPT.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDeselectAllCPT.FlatAppearance.BorderSize = 0
        Me.btnDeselectAllCPT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDeselectAllCPT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDeselectAllCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeselectAllCPT.Image = CType(resources.GetObject("btnDeselectAllCPT.Image"), System.Drawing.Image)
        Me.btnDeselectAllCPT.Location = New System.Drawing.Point(349, 1)
        Me.btnDeselectAllCPT.Name = "btnDeselectAllCPT"
        Me.btnDeselectAllCPT.Size = New System.Drawing.Size(25, 23)
        Me.btnDeselectAllCPT.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.btnDeselectAllCPT, "Deselect All")
        Me.btnDeselectAllCPT.UseVisualStyleBackColor = False
        Me.btnDeselectAllCPT.Visible = False
        '
        'btnSelectAllCPT
        '
        Me.btnSelectAllCPT.BackColor = System.Drawing.Color.Transparent
        Me.btnSelectAllCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSelectAllCPT.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSelectAllCPT.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSelectAllCPT.FlatAppearance.BorderSize = 0
        Me.btnSelectAllCPT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSelectAllCPT.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSelectAllCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectAllCPT.Image = CType(resources.GetObject("btnSelectAllCPT.Image"), System.Drawing.Image)
        Me.btnSelectAllCPT.Location = New System.Drawing.Point(374, 1)
        Me.btnSelectAllCPT.Name = "btnSelectAllCPT"
        Me.btnSelectAllCPT.Size = New System.Drawing.Size(25, 23)
        Me.btnSelectAllCPT.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnSelectAllCPT, "Select All")
        Me.btnSelectAllCPT.UseVisualStyleBackColor = False
        '
        'lblCurrentCPT
        '
        Me.lblCurrentCPT.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrentCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCurrentCPT.Location = New System.Drawing.Point(1, 1)
        Me.lblCurrentCPT.Name = "lblCurrentCPT"
        Me.lblCurrentCPT.Size = New System.Drawing.Size(398, 23)
        Me.lblCurrentCPT.TabIndex = 1
        Me.lblCurrentCPT.Text = "Current CPT"
        Me.lblCurrentCPT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Location = New System.Drawing.Point(1, 24)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(398, 1)
        Me.Label22.TabIndex = 8
        Me.Label22.Text = "label2"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label24.Location = New System.Drawing.Point(0, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 24)
        Me.Label24.TabIndex = 7
        Me.Label24.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Location = New System.Drawing.Point(399, 1)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 24)
        Me.Label25.TabIndex = 6
        Me.Label25.Text = "label3"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Location = New System.Drawing.Point(0, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(400, 1)
        Me.Label26.TabIndex = 5
        Me.Label26.Text = "label1"
        '
        'pnlhederCPT
        '
        Me.pnlhederCPT.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.pnlhederCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlhederCPT.Controls.Add(Me.btnDeselectAllCPTGallery)
        Me.pnlhederCPT.Controls.Add(Me.btnSelectAllCPTGallery)
        Me.pnlhederCPT.Controls.Add(Me.lblCPTGalleryheader)
        Me.pnlhederCPT.Controls.Add(Me.Label43)
        Me.pnlhederCPT.Controls.Add(Me.Label44)
        Me.pnlhederCPT.Controls.Add(Me.Label45)
        Me.pnlhederCPT.Controls.Add(Me.Label46)
        Me.pnlhederCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlhederCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlhederCPT.ForeColor = System.Drawing.Color.White
        Me.pnlhederCPT.Location = New System.Drawing.Point(0, 0)
        Me.pnlhederCPT.Name = "pnlhederCPT"
        Me.pnlhederCPT.Size = New System.Drawing.Size(400, 25)
        Me.pnlhederCPT.TabIndex = 1
        '
        'btnDeselectAllCPTGallery
        '
        Me.btnDeselectAllCPTGallery.BackColor = System.Drawing.Color.Transparent
        Me.btnDeselectAllCPTGallery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnDeselectAllCPTGallery.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDeselectAllCPTGallery.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDeselectAllCPTGallery.FlatAppearance.BorderSize = 0
        Me.btnDeselectAllCPTGallery.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDeselectAllCPTGallery.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDeselectAllCPTGallery.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeselectAllCPTGallery.Image = CType(resources.GetObject("btnDeselectAllCPTGallery.Image"), System.Drawing.Image)
        Me.btnDeselectAllCPTGallery.Location = New System.Drawing.Point(349, 1)
        Me.btnDeselectAllCPTGallery.Name = "btnDeselectAllCPTGallery"
        Me.btnDeselectAllCPTGallery.Size = New System.Drawing.Size(25, 23)
        Me.btnDeselectAllCPTGallery.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.btnDeselectAllCPTGallery, "Deselect All")
        Me.btnDeselectAllCPTGallery.UseVisualStyleBackColor = False
        Me.btnDeselectAllCPTGallery.Visible = False
        '
        'btnSelectAllCPTGallery
        '
        Me.btnSelectAllCPTGallery.BackColor = System.Drawing.Color.Transparent
        Me.btnSelectAllCPTGallery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSelectAllCPTGallery.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSelectAllCPTGallery.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSelectAllCPTGallery.FlatAppearance.BorderSize = 0
        Me.btnSelectAllCPTGallery.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSelectAllCPTGallery.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSelectAllCPTGallery.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectAllCPTGallery.Image = CType(resources.GetObject("btnSelectAllCPTGallery.Image"), System.Drawing.Image)
        Me.btnSelectAllCPTGallery.Location = New System.Drawing.Point(374, 1)
        Me.btnSelectAllCPTGallery.Name = "btnSelectAllCPTGallery"
        Me.btnSelectAllCPTGallery.Size = New System.Drawing.Size(25, 23)
        Me.btnSelectAllCPTGallery.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnSelectAllCPTGallery, "Select All")
        Me.btnSelectAllCPTGallery.UseVisualStyleBackColor = False
        '
        'lblCPTGalleryheader
        '
        Me.lblCPTGalleryheader.BackColor = System.Drawing.Color.Transparent
        Me.lblCPTGalleryheader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCPTGalleryheader.Location = New System.Drawing.Point(1, 1)
        Me.lblCPTGalleryheader.Name = "lblCPTGalleryheader"
        Me.lblCPTGalleryheader.Size = New System.Drawing.Size(398, 23)
        Me.lblCPTGalleryheader.TabIndex = 1
        Me.lblCPTGalleryheader.Text = "CPT Gallery"
        Me.lblCPTGalleryheader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label43.Location = New System.Drawing.Point(1, 24)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(398, 1)
        Me.Label43.TabIndex = 8
        Me.Label43.Text = "label2"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label44.Location = New System.Drawing.Point(0, 1)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(1, 24)
        Me.Label44.TabIndex = 7
        Me.Label44.Text = "label4"
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label45.Location = New System.Drawing.Point(399, 1)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(1, 24)
        Me.Label45.TabIndex = 6
        Me.Label45.Text = "label3"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label46.Location = New System.Drawing.Point(0, 0)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(400, 1)
        Me.Label46.TabIndex = 5
        Me.Label46.Text = "label1"
        '
        'btnInsertCPT
        '
        Me.btnInsertCPT.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnInsertCPT.BackgroundImage = CType(resources.GetObject("btnInsertCPT.BackgroundImage"), System.Drawing.Image)
        Me.btnInsertCPT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnInsertCPT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInsertCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInsertCPT.Location = New System.Drawing.Point(525, 7)
        Me.btnInsertCPT.Name = "btnInsertCPT"
        Me.btnInsertCPT.Size = New System.Drawing.Size(81, 24)
        Me.btnInsertCPT.TabIndex = 0
        Me.btnInsertCPT.Text = "Import"
        Me.btnInsertCPT.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnInsertCPT.UseVisualStyleBackColor = True
        Me.btnInsertCPT.Visible = False
        '
        'btnInsetICD9
        '
        Me.btnInsetICD9.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnInsetICD9.BackgroundImage = CType(resources.GetObject("btnInsetICD9.BackgroundImage"), System.Drawing.Image)
        Me.btnInsetICD9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnInsetICD9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnInsetICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInsetICD9.Location = New System.Drawing.Point(437, 7)
        Me.btnInsetICD9.Name = "btnInsetICD9"
        Me.btnInsetICD9.Size = New System.Drawing.Size(81, 24)
        Me.btnInsetICD9.TabIndex = 0
        Me.btnInsetICD9.Text = "Import"
        Me.btnInsetICD9.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnInsetICD9.UseVisualStyleBackColor = True
        Me.btnInsetICD9.Visible = False
        '
        'tlICD9CptGallery
        '
        Me.tlICD9CptGallery.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlICD9CptGallery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlICD9CptGallery.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlICD9CptGallery.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlICD9CptGallery.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbImportCPT, Me.tlbImportIDC9, Me.tlbClearAll, Me.tlbSelectAll, Me.tlbCPTGallery, Me.tlbICD9Gallery, Me.tlbCurrentCPT, Me.tlbCurrentICD9, Me.tlbClose})
        Me.tlICD9CptGallery.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlICD9CptGallery.Location = New System.Drawing.Point(0, 0)
        Me.tlICD9CptGallery.Name = "tlICD9CptGallery"
        Me.tlICD9CptGallery.Size = New System.Drawing.Size(925, 53)
        Me.tlICD9CptGallery.TabIndex = 0
        Me.tlICD9CptGallery.Text = "ToolStrip1"
        '
        'tlbImportCPT
        '
        Me.tlbImportCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbImportCPT.Image = CType(resources.GetObject("tlbImportCPT.Image"), System.Drawing.Image)
        Me.tlbImportCPT.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbImportCPT.Name = "tlbImportCPT"
        Me.tlbImportCPT.Size = New System.Drawing.Size(81, 50)
        Me.tlbImportCPT.Tag = "ImportCPT"
        Me.tlbImportCPT.Text = "Import CPT"
        Me.tlbImportCPT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbImportCPT.ToolTipText = "Import CPT"
        Me.tlbImportCPT.Visible = False
        '
        'tlbImportIDC9
        '
        Me.tlbImportIDC9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbImportIDC9.Image = CType(resources.GetObject("tlbImportIDC9.Image"), System.Drawing.Image)
        Me.tlbImportIDC9.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbImportIDC9.Name = "tlbImportIDC9"
        Me.tlbImportIDC9.Size = New System.Drawing.Size(88, 50)
        Me.tlbImportIDC9.Tag = "ImportICD9"
        Me.tlbImportIDC9.Text = "Import ICD9"
        Me.tlbImportIDC9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbImportIDC9.ToolTipText = "Import ICD9"
        Me.tlbImportIDC9.Visible = False
        '
        'tlbClearAll
        '
        Me.tlbClearAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbClearAll.Image = CType(resources.GetObject("tlbClearAll.Image"), System.Drawing.Image)
        Me.tlbClearAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbClearAll.Name = "tlbClearAll"
        Me.tlbClearAll.Size = New System.Drawing.Size(60, 50)
        Me.tlbClearAll.Tag = "ClearAll"
        Me.tlbClearAll.Text = "Clear All"
        Me.tlbClearAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbClearAll.ToolTipText = "Clear All"
        Me.tlbClearAll.Visible = False
        '
        'tlbSelectAll
        '
        Me.tlbSelectAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbSelectAll.Image = CType(resources.GetObject("tlbSelectAll.Image"), System.Drawing.Image)
        Me.tlbSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbSelectAll.Name = "tlbSelectAll"
        Me.tlbSelectAll.Size = New System.Drawing.Size(67, 50)
        Me.tlbSelectAll.Tag = "SelectAll"
        Me.tlbSelectAll.Text = "Select All"
        Me.tlbSelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbSelectAll.ToolTipText = "Select All"
        Me.tlbSelectAll.Visible = False
        '
        'tlbCPTGallery
        '
        Me.tlbCPTGallery.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbCPTGallery.Image = CType(resources.GetObject("tlbCPTGallery.Image"), System.Drawing.Image)
        Me.tlbCPTGallery.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbCPTGallery.Name = "tlbCPTGallery"
        Me.tlbCPTGallery.Size = New System.Drawing.Size(78, 50)
        Me.tlbCPTGallery.Tag = "CPTGallery"
        Me.tlbCPTGallery.Text = "CPT Gallery"
        Me.tlbCPTGallery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbCPTGallery.ToolTipText = "CPT Gallery"
        Me.tlbCPTGallery.Visible = False
        '
        'tlbICD9Gallery
        '
        Me.tlbICD9Gallery.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbICD9Gallery.Image = CType(resources.GetObject("tlbICD9Gallery.Image"), System.Drawing.Image)
        Me.tlbICD9Gallery.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbICD9Gallery.Name = "tlbICD9Gallery"
        Me.tlbICD9Gallery.Size = New System.Drawing.Size(85, 50)
        Me.tlbICD9Gallery.Tag = "ICD9Gallery"
        Me.tlbICD9Gallery.Text = "ICD9 Gallery"
        Me.tlbICD9Gallery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbICD9Gallery.ToolTipText = "ICD9 Gallery"
        Me.tlbICD9Gallery.Visible = False
        '
        'tlbCurrentCPT
        '
        Me.tlbCurrentCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbCurrentCPT.Image = CType(resources.GetObject("tlbCurrentCPT.Image"), System.Drawing.Image)
        Me.tlbCurrentCPT.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbCurrentCPT.Name = "tlbCurrentCPT"
        Me.tlbCurrentCPT.Size = New System.Drawing.Size(85, 50)
        Me.tlbCurrentCPT.Tag = "CurrentCPT"
        Me.tlbCurrentCPT.Text = "Current CPT"
        Me.tlbCurrentCPT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbCurrentCPT.ToolTipText = "Current CPT"
        Me.tlbCurrentCPT.Visible = False
        '
        'tlbCurrentICD9
        '
        Me.tlbCurrentICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbCurrentICD9.Image = CType(resources.GetObject("tlbCurrentICD9.Image"), System.Drawing.Image)
        Me.tlbCurrentICD9.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbCurrentICD9.Name = "tlbCurrentICD9"
        Me.tlbCurrentICD9.Size = New System.Drawing.Size(92, 50)
        Me.tlbCurrentICD9.Tag = "CurrentICD9"
        Me.tlbCurrentICD9.Text = "Current ICD9"
        Me.tlbCurrentICD9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbCurrentICD9.ToolTipText = "Current ICD9"
        Me.tlbCurrentICD9.Visible = False
        '
        'tlbClose
        '
        Me.tlbClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbClose.Image = CType(resources.GetObject("tlbClose.Image"), System.Drawing.Image)
        Me.tlbClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbClose.Name = "tlbClose"
        Me.tlbClose.Size = New System.Drawing.Size(59, 50)
        Me.tlbClose.Tag = "Close"
        Me.tlbClose.Text = "  &Close  "
        Me.tlbClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbClose.ToolTipText = "Close  "
        '
        'frmICD9CPTGallery
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(925, 772)
        Me.Controls.Add(Me.frmMain)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmICD9CPTGallery"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " ICD9-CPT Gallery"
        Me.frmMain.ResumeLayout(False)
        Me.pnlTab.ResumeLayout(False)
        Me.tbICD9CPTGallery.ResumeLayout(False)
        Me.tbPageICD9.ResumeLayout(False)
        Me.pnltbICD9.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.pnlCurrentICD9.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlICD9Gallery.ResumeLayout(False)
        Me.pnlICD9Indicator.ResumeLayout(False)
        Me.pnllftHeaderICD9.ResumeLayout(False)
        Me.pnlICD9bottom.ResumeLayout(False)
        Me.tbPageCPT.ResumeLayout(False)
        Me.pnltbCPT.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlCurrentCPT.ResumeLayout(False)
        Me.pnlCurrentCPT.PerformLayout()
        Me.pnlComboCurrentCPT.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlCPTGallery.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.pnlICD9Center.ResumeLayout(False)
        Me.pnlICD9Center.PerformLayout()
        Me.pnlTOP.ResumeLayout(False)
        Me.pnlTOP.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.pnllftICD9.ResumeLayout(False)
        Me.pnlLeftbottom.ResumeLayout(False)
        CType(Me.PicBlank, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicRevise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.pnlhederCPT.ResumeLayout(False)
        Me.tlICD9CptGallery.ResumeLayout(False)
        Me.tlICD9CptGallery.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents frmMain As System.Windows.Forms.Panel
    Friend WithEvents pnlTOP As System.Windows.Forms.Panel
    Friend WithEvents pnlTab As System.Windows.Forms.Panel
    Friend WithEvents tbICD9CPTGallery As System.Windows.Forms.TabControl
    Friend WithEvents tbPageICD9 As System.Windows.Forms.TabPage
    Friend WithEvents tbPageCPT As System.Windows.Forms.TabPage
    Friend WithEvents pnlICD9Center As System.Windows.Forms.Panel
    Friend WithEvents pnlICD9Gallery As System.Windows.Forms.Panel
    Friend WithEvents pnllftICD9 As System.Windows.Forms.Panel
    Friend WithEvents pnlCPTGallery As System.Windows.Forms.Panel
    Friend WithEvents pnlhederCPT As System.Windows.Forms.Panel
    Friend WithEvents btnInsetICD9 As System.Windows.Forms.Button
    Friend WithEvents lblICD9Galleryheader As System.Windows.Forms.Label
    Friend WithEvents lblCPTGalleryheader As System.Windows.Forms.Label
    Friend WithEvents btnInsertCPT As System.Windows.Forms.Button
    Friend WithEvents ImgICD9CPT As System.Windows.Forms.ImageList
    Friend WithEvents tlICD9CptGallery As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnltbICD9 As System.Windows.Forms.Panel
    Friend WithEvents pnltbCPT As System.Windows.Forms.Panel
    Friend WithEvents pnlICD9Indicator As System.Windows.Forms.Panel
    Friend WithEvents cmbICD9Gallery As System.Windows.Forms.ComboBox
    Friend WithEvents pnlLeftbottom As System.Windows.Forms.Panel
    Friend WithEvents PicRevise As System.Windows.Forms.PictureBox
    Friend WithEvents lblrevise As System.Windows.Forms.Label
    Friend WithEvents PicNew As System.Windows.Forms.PictureBox
    Friend WithEvents lblNew As System.Windows.Forms.Label
    Friend WithEvents PicBlank As System.Windows.Forms.PictureBox
    Friend WithEvents lblBlank As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents pnllftHeaderICD9 As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents pnlICD9bottom As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents gloTrvICD9Gallery As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents lblSpeciality As System.Windows.Forms.Label
    Friend WithEvents cmbSpecialityICD9 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSpecialityCPT As System.Windows.Forms.ComboBox
    Friend WithEvents gloTrvCPTGallery As gloUserControlLibrary.gloUC_TreeView
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents tlbSelectAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbClearAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbCurrentICD9 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbICD9Gallery As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbCurrentCPT As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbCPTGallery As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlCurrentICD9 As System.Windows.Forms.Panel
    Friend WithEvents gloTrvCurrenICD9 As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblCurrentICD9Header As System.Windows.Forms.Label
    Friend WithEvents tlbImportCPT As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlCurrentCPT As System.Windows.Forms.Panel
    Friend WithEvents gloTrvCurrentCPT As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lblCurrentCPT As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents pnlComboCurrentCPT As System.Windows.Forms.Panel
    Friend WithEvents cmbSpecialityCurrentCPT As System.Windows.Forms.ComboBox
    Friend WithEvents tlbImportIDC9 As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblCopyRight As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnCPTRemove As System.Windows.Forms.Button
    Friend WithEvents btnCPTAdd As System.Windows.Forms.Button
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents btnICD9Remove As System.Windows.Forms.Button
    Friend WithEvents btnICD9Add As System.Windows.Forms.Button
    Friend WithEvents btnDeselectAllICD9 As System.Windows.Forms.Button
    Friend WithEvents btnSelectAllICD9 As System.Windows.Forms.Button
    Friend WithEvents btnDeselectAllICD9Gallery As System.Windows.Forms.Button
    Friend WithEvents btnSelectAllICD9Gallery As System.Windows.Forms.Button
    Friend WithEvents btnDeselectAllCPT As System.Windows.Forms.Button
    Friend WithEvents btnSelectAllCPT As System.Windows.Forms.Button
    Friend WithEvents btnDeselectAllCPTGallery As System.Windows.Forms.Button
    Friend WithEvents btnSelectAllCPTGallery As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbSpecialityICD9CPT As System.Windows.Forms.ComboBox
End Class
