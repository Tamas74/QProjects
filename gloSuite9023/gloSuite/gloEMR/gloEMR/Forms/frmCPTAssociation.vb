Option Compare Text
Public Class frmCPTAssociation
    Inherits System.Windows.Forms.Form
    Dim objclsCPTAssociation As clsCPTAssociation
    Dim dtCPTCode As DataTable
    Dim dtCPTDesc As DataTable
    Dim dtTemplate As DataTable

    Friend WithEvents tblMedication As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblFinish As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton

    Friend WithEvents tblRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents pnl_SearchRight As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents pnlRadioBtn As System.Windows.Forms.Panel
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents pnltrvCPT As System.Windows.Forms.Panel
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents GloUC_trvCPT As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents GloUC_trvTemplate As gloUserControlLibrary.gloUC_TreeView
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Private WithEvents Label36 As System.Windows.Forms.Label
    Private WithEvents Label37 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents ts_gloCommunityDownload As System.Windows.Forms.ToolStripButton

    'sarika 26th sept 07


    '------------------------------
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try

            If Not (components Is Nothing) Then
                components.Dispose()
            End If

            If (IsNothing(objclsCPTAssociation) = False) Then
                objclsCPTAssociation.Dispose()
                objclsCPTAssociation = Nothing
            End If

            If (IsNothing(dtCPTCode) = False) Then
                dtCPTCode.Dispose()
                dtCPTCode = Nothing
            End If

            If (IsNothing(dtCPTDesc) = False) Then
                dtCPTDesc.Dispose()
                dtCPTDesc = Nothing
            End If

            If (IsNothing(dtTemplate) = False) Then
                dtTemplate.Dispose()
                dtTemplate = Nothing
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnl_Left As System.Windows.Forms.Panel
    Friend WithEvents pnl_Right As System.Windows.Forms.Panel
    Friend WithEvents pnl_Middle As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents txtsearchAssociates As System.Windows.Forms.TextBox
    Friend WithEvents cntICD9Association As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuDeleteICD9Item As System.Windows.Forms.MenuItem
    Friend WithEvents pnl_ToolStrip As System.Windows.Forms.Panel
    Friend WithEvents trvCPT As System.Windows.Forms.TreeView
    Friend WithEvents trvTemplate As System.Windows.Forms.TreeView
    Friend WithEvents trvCPTAssociation As System.Windows.Forms.TreeView
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents rbSearch2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbSearch1 As System.Windows.Forms.RadioButton
    Friend WithEvents txtsearchDrugs As System.Windows.Forms.TextBox
    Friend WithEvents imgTreeView As System.Windows.Forms.ImageList
    Friend WithEvents mnuRemoveCPT As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCPTAssociation))
        Me.pnl_Left = New System.Windows.Forms.Panel()
        Me.GloUC_trvCPT = New gloUserControlLibrary.gloUC_TreeView()
        Me.imgTreeView = New System.Windows.Forms.ImageList(Me.components)
        Me.pnltrvCPT = New System.Windows.Forms.Panel()
        Me.trvCPT = New System.Windows.Forms.TreeView()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtsearchDrugs = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.pnlRadioBtn = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.rbSearch2 = New System.Windows.Forms.RadioButton()
        Me.rbSearch1 = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.pnl_Right = New System.Windows.Forms.Panel()
        Me.GloUC_trvTemplate = New gloUserControlLibrary.gloUC_TreeView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.trvTemplate = New System.Windows.Forms.TreeView()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.pnl_SearchRight = New System.Windows.Forms.Panel()
        Me.txtsearchAssociates = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.pnl_Middle = New System.Windows.Forms.Panel()
        Me.trvCPTAssociation = New System.Windows.Forms.TreeView()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnl_ToolStrip = New System.Windows.Forms.Panel()
        Me.tblMedication = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblNew = New System.Windows.Forms.ToolStripButton()
        Me.tblRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tblSave = New System.Windows.Forms.ToolStripButton()
        Me.tblFinish = New System.Windows.Forms.ToolStripButton()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.cntICD9Association = New System.Windows.Forms.ContextMenu()
        Me.mnuRemoveCPT = New System.Windows.Forms.MenuItem()
        Me.mnuDeleteICD9Item = New System.Windows.Forms.MenuItem()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.ts_gloCommunityDownload = New System.Windows.Forms.ToolStripButton()
        Me.pnl_Left.SuspendLayout()
        Me.pnltrvCPT.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRadioBtn.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.pnl_Right.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnl_SearchRight.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_Middle.SuspendLayout()
        Me.pnl_ToolStrip.SuspendLayout()
        Me.tblMedication.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnl_Left
        '
        Me.pnl_Left.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Left.Controls.Add(Me.GloUC_trvCPT)
        Me.pnl_Left.Controls.Add(Me.pnltrvCPT)
        Me.pnl_Left.Controls.Add(Me.pnlSearch)
        Me.pnl_Left.Controls.Add(Me.pnlRadioBtn)
        Me.pnl_Left.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnl_Left.Location = New System.Drawing.Point(0, 53)
        Me.pnl_Left.Name = "pnl_Left"
        Me.pnl_Left.Size = New System.Drawing.Size(240, 585)
        Me.pnl_Left.TabIndex = 0
        '
        'GloUC_trvCPT
        '
        Me.GloUC_trvCPT.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvCPT.CheckBoxes = False
        Me.GloUC_trvCPT.CodeMember = Nothing
        Me.GloUC_trvCPT.Comment = Nothing
        Me.GloUC_trvCPT.ConceptID = Nothing
        Me.GloUC_trvCPT.DescriptionMember = Nothing
        Me.GloUC_trvCPT.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvCPT.DrugFlag = CType(16, Short)
        Me.GloUC_trvCPT.DrugFormMember = Nothing
        Me.GloUC_trvCPT.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvCPT.DurationMember = Nothing
        Me.GloUC_trvCPT.FrequencyMember = Nothing
        Me.GloUC_trvCPT.ImageIndex = 0
        Me.GloUC_trvCPT.ImageList = Me.imgTreeView
        Me.GloUC_trvCPT.ImageObject = Nothing
        Me.GloUC_trvCPT.Indicator = Nothing
        Me.GloUC_trvCPT.IsDrug = False
        Me.GloUC_trvCPT.IsNarcoticsMember = Nothing
        Me.GloUC_trvCPT.IsSystemCategory = Nothing
        Me.GloUC_trvCPT.Location = New System.Drawing.Point(0, 30)
        Me.GloUC_trvCPT.MaximumNodes = 1000
        Me.GloUC_trvCPT.Name = "GloUC_trvCPT"
        Me.GloUC_trvCPT.NDCCodeMember = Nothing
        Me.GloUC_trvCPT.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.GloUC_trvCPT.ParentImageIndex = 0
        Me.GloUC_trvCPT.ParentMember = Nothing
        Me.GloUC_trvCPT.RouteMember = Nothing
        Me.GloUC_trvCPT.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvCPT.SearchBox = True
        Me.GloUC_trvCPT.SearchText = Nothing
        Me.GloUC_trvCPT.SelectedImageIndex = 0
        Me.GloUC_trvCPT.SelectedNode = Nothing
        Me.GloUC_trvCPT.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvCPT.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvCPT.SelectedParentImageIndex = 0
        Me.GloUC_trvCPT.Size = New System.Drawing.Size(240, 555)
        Me.GloUC_trvCPT.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvCPT.TabIndex = 50
        Me.GloUC_trvCPT.Tag = Nothing
        Me.GloUC_trvCPT.UnitMember = Nothing
        Me.GloUC_trvCPT.ValueMember = Nothing
        '
        'imgTreeView
        '
        Me.imgTreeView.ImageStream = CType(resources.GetObject("imgTreeView.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTreeView.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeView.Images.SetKeyName(0, "Bullet06.ico")
        Me.imgTreeView.Images.SetKeyName(1, "CPT_01.ico")
        Me.imgTreeView.Images.SetKeyName(2, "category.ico")
        Me.imgTreeView.Images.SetKeyName(3, "SubTemplate.ico")
        Me.imgTreeView.Images.SetKeyName(4, "CPT Association.ico")
        Me.imgTreeView.Images.SetKeyName(5, "Small Arrow.ico")
        '
        'pnltrvCPT
        '
        Me.pnltrvCPT.Controls.Add(Me.trvCPT)
        Me.pnltrvCPT.Controls.Add(Me.Label34)
        Me.pnltrvCPT.Controls.Add(Me.Label35)
        Me.pnltrvCPT.Controls.Add(Me.Label18)
        Me.pnltrvCPT.Controls.Add(Me.Label19)
        Me.pnltrvCPT.Controls.Add(Me.Label22)
        Me.pnltrvCPT.Controls.Add(Me.Label23)
        Me.pnltrvCPT.Location = New System.Drawing.Point(0, 58)
        Me.pnltrvCPT.Name = "pnltrvCPT"
        Me.pnltrvCPT.Padding = New System.Windows.Forms.Padding(3, 1, 1, 3)
        Me.pnltrvCPT.Size = New System.Drawing.Size(240, 527)
        Me.pnltrvCPT.TabIndex = 18
        Me.pnltrvCPT.Visible = False
        '
        'trvCPT
        '
        Me.trvCPT.BackColor = System.Drawing.Color.White
        Me.trvCPT.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvCPT.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvCPT.ForeColor = System.Drawing.Color.Black
        Me.trvCPT.HideSelection = False
        Me.trvCPT.ImageIndex = 5
        Me.trvCPT.ImageList = Me.imgTreeView
        Me.trvCPT.ItemHeight = 20
        Me.trvCPT.Location = New System.Drawing.Point(7, 6)
        Me.trvCPT.Name = "trvCPT"
        Me.trvCPT.SelectedImageIndex = 5
        Me.trvCPT.ShowLines = False
        Me.trvCPT.Size = New System.Drawing.Size(231, 517)
        Me.trvCPT.TabIndex = 1
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.White
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label34.Location = New System.Drawing.Point(4, 6)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(3, 517)
        Me.Label34.TabIndex = 41
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.White
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label35.Location = New System.Drawing.Point(4, 2)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(234, 4)
        Me.Label35.TabIndex = 40
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(4, 523)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(234, 1)
        Me.Label18.TabIndex = 12
        Me.Label18.Text = "label2"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(3, 2)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 522)
        Me.Label19.TabIndex = 11
        Me.Label19.Text = "label4"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(238, 2)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 522)
        Me.Label22.TabIndex = 10
        Me.Label22.Text = "label3"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(3, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(236, 1)
        Me.Label23.TabIndex = 9
        Me.Label23.Text = "label1"
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlSearch.Controls.Add(Me.txtsearchDrugs)
        Me.pnlSearch.Controls.Add(Me.Label10)
        Me.pnlSearch.Controls.Add(Me.Label11)
        Me.pnlSearch.Controls.Add(Me.PictureBox2)
        Me.pnlSearch.Controls.Add(Me.Label8)
        Me.pnlSearch.Controls.Add(Me.Label13)
        Me.pnlSearch.Controls.Add(Me.Label14)
        Me.pnlSearch.Controls.Add(Me.Label17)
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(0, 30)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3, 1, 1, 3)
        Me.pnlSearch.Size = New System.Drawing.Size(240, 28)
        Me.pnlSearch.TabIndex = 16
        Me.pnlSearch.Visible = False
        '
        'txtsearchDrugs
        '
        Me.txtsearchDrugs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearchDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsearchDrugs.ForeColor = System.Drawing.Color.Black
        Me.txtsearchDrugs.Location = New System.Drawing.Point(32, 6)
        Me.txtsearchDrugs.Multiline = True
        Me.txtsearchDrugs.Name = "txtsearchDrugs"
        Me.txtsearchDrugs.Size = New System.Drawing.Size(206, 16)
        Me.txtsearchDrugs.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.White
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Location = New System.Drawing.Point(32, 2)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(206, 4)
        Me.Label10.TabIndex = 37
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.White
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Location = New System.Drawing.Point(32, 22)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(206, 2)
        Me.Label11.TabIndex = 38
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.White
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(4, 2)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(28, 22)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 9
        Me.PictureBox2.TabStop = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(4, 24)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(234, 1)
        Me.Label8.TabIndex = 42
        Me.Label8.Text = "label2"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(3, 2)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 23)
        Me.Label13.TabIndex = 41
        Me.Label13.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(238, 2)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 23)
        Me.Label14.TabIndex = 40
        Me.Label14.Text = "label3"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(3, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(236, 1)
        Me.Label17.TabIndex = 39
        Me.Label17.Text = "label1"
        '
        'pnlRadioBtn
        '
        Me.pnlRadioBtn.Controls.Add(Me.Panel8)
        Me.pnlRadioBtn.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRadioBtn.Location = New System.Drawing.Point(0, 0)
        Me.pnlRadioBtn.Name = "pnlRadioBtn"
        Me.pnlRadioBtn.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.pnlRadioBtn.Size = New System.Drawing.Size(240, 30)
        Me.pnlRadioBtn.TabIndex = 17
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Transparent
        Me.Panel8.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.Label5)
        Me.Panel8.Controls.Add(Me.rbSearch2)
        Me.Panel8.Controls.Add(Me.rbSearch1)
        Me.Panel8.Controls.Add(Me.Label7)
        Me.Panel8.Controls.Add(Me.Label6)
        Me.Panel8.Controls.Add(Me.Label16)
        Me.Panel8.Controls.Add(Me.Label15)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(3, 3)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(237, 24)
        Me.Panel8.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(1, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(235, 22)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "CPT"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'rbSearch2
        '
        Me.rbSearch2.BackColor = System.Drawing.Color.Transparent
        Me.rbSearch2.Checked = True
        Me.rbSearch2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbSearch2.Location = New System.Drawing.Point(131, 1)
        Me.rbSearch2.Name = "rbSearch2"
        Me.rbSearch2.Size = New System.Drawing.Size(104, 22)
        Me.rbSearch2.TabIndex = 3
        Me.rbSearch2.TabStop = True
        Me.rbSearch2.Text = "Description"
        Me.rbSearch2.UseVisualStyleBackColor = False
        Me.rbSearch2.Visible = False
        '
        'rbSearch1
        '
        Me.rbSearch1.BackColor = System.Drawing.Color.Transparent
        Me.rbSearch1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbSearch1.Location = New System.Drawing.Point(11, 1)
        Me.rbSearch1.Name = "rbSearch1"
        Me.rbSearch1.Size = New System.Drawing.Size(91, 22)
        Me.rbSearch1.TabIndex = 2
        Me.rbSearch1.Text = "CPT Code"
        Me.rbSearch1.UseVisualStyleBackColor = False
        Me.rbSearch1.Visible = False
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Location = New System.Drawing.Point(236, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 22)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "label3"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 22)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label16.Location = New System.Drawing.Point(0, 23)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(237, 1)
        Me.Label16.TabIndex = 7
        Me.Label16.Text = "label1"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Location = New System.Drawing.Point(0, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(237, 1)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "label1"
        '
        'pnl_Right
        '
        Me.pnl_Right.BackColor = System.Drawing.Color.Transparent
        Me.pnl_Right.Controls.Add(Me.GloUC_trvTemplate)
        Me.pnl_Right.Controls.Add(Me.Panel2)
        Me.pnl_Right.Controls.Add(Me.Panel7)
        Me.pnl_Right.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnl_Right.Location = New System.Drawing.Point(668, 53)
        Me.pnl_Right.Name = "pnl_Right"
        Me.pnl_Right.Size = New System.Drawing.Size(240, 585)
        Me.pnl_Right.TabIndex = 3
        '
        'GloUC_trvTemplate
        '
        Me.GloUC_trvTemplate.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvTemplate.CheckBoxes = False
        Me.GloUC_trvTemplate.CodeMember = Nothing
        Me.GloUC_trvTemplate.Comment = Nothing
        Me.GloUC_trvTemplate.ConceptID = Nothing
        Me.GloUC_trvTemplate.mpidmember = Nothing
        Me.GloUC_trvTemplate.DescriptionMember = Nothing
        Me.GloUC_trvTemplate.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
        Me.GloUC_trvTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvTemplate.DrugFlag = CType(16, Short)
        Me.GloUC_trvTemplate.DrugFormMember = Nothing
        Me.GloUC_trvTemplate.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvTemplate.DurationMember = Nothing
        Me.GloUC_trvTemplate.FrequencyMember = Nothing
        Me.GloUC_trvTemplate.ImageIndex = 0
        Me.GloUC_trvTemplate.ImageList = Me.imgTreeView
        Me.GloUC_trvTemplate.ImageObject = Nothing
        Me.GloUC_trvTemplate.Indicator = Nothing
        Me.GloUC_trvTemplate.IsDrug = False
        Me.GloUC_trvTemplate.IsNarcoticsMember = Nothing
        Me.GloUC_trvTemplate.IsSystemCategory = Nothing
        Me.GloUC_trvTemplate.Location = New System.Drawing.Point(0, 30)
        Me.GloUC_trvTemplate.MaximumNodes = 1000
        Me.GloUC_trvTemplate.Name = "GloUC_trvTemplate"
        Me.GloUC_trvTemplate.NDCCodeMember = Nothing
        Me.GloUC_trvTemplate.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.GloUC_trvTemplate.ParentImageIndex = 0
        Me.GloUC_trvTemplate.ParentMember = Nothing
        Me.GloUC_trvTemplate.RouteMember = Nothing
        Me.GloUC_trvTemplate.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvTemplate.SearchBox = True
        Me.GloUC_trvTemplate.SearchText = Nothing
        Me.GloUC_trvTemplate.SelectedImageIndex = 0
        Me.GloUC_trvTemplate.SelectedNode = Nothing
        Me.GloUC_trvTemplate.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvTemplate.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvTemplate.SelectedParentImageIndex = 0
        Me.GloUC_trvTemplate.Size = New System.Drawing.Size(240, 555)
        Me.GloUC_trvTemplate.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvTemplate.TabIndex = 51
        Me.GloUC_trvTemplate.Tag = Nothing
        Me.GloUC_trvTemplate.UnitMember = Nothing
        Me.GloUC_trvTemplate.ValueMember = Nothing
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel2.Size = New System.Drawing.Size(240, 30)
        Me.Panel2.TabIndex = 52
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.Label32)
        Me.Panel3.Controls.Add(Me.RadioButton1)
        Me.Panel3.Controls.Add(Me.RadioButton2)
        Me.Panel3.Controls.Add(Me.Label36)
        Me.Panel3.Controls.Add(Me.Label37)
        Me.Panel3.Controls.Add(Me.Label38)
        Me.Panel3.Controls.Add(Me.Label39)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(237, 24)
        Me.Panel3.TabIndex = 2
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(1, 1)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(235, 22)
        Me.Label32.TabIndex = 10
        Me.Label32.Text = "Template"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RadioButton1
        '
        Me.RadioButton1.BackColor = System.Drawing.Color.Transparent
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton1.Location = New System.Drawing.Point(131, 1)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(104, 22)
        Me.RadioButton1.TabIndex = 3
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "Description"
        Me.RadioButton1.UseVisualStyleBackColor = False
        Me.RadioButton1.Visible = False
        '
        'RadioButton2
        '
        Me.RadioButton2.BackColor = System.Drawing.Color.Transparent
        Me.RadioButton2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadioButton2.Location = New System.Drawing.Point(11, 1)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(91, 22)
        Me.RadioButton2.TabIndex = 2
        Me.RadioButton2.Text = "CPT Code"
        Me.RadioButton2.UseVisualStyleBackColor = False
        Me.RadioButton2.Visible = False
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label36.Location = New System.Drawing.Point(236, 1)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(1, 22)
        Me.Label36.TabIndex = 9
        Me.Label36.Text = "label3"
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label37.Location = New System.Drawing.Point(0, 1)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(1, 22)
        Me.Label37.TabIndex = 8
        Me.Label37.Text = "label4"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label38.Location = New System.Drawing.Point(0, 23)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(237, 1)
        Me.Label38.TabIndex = 7
        Me.Label38.Text = "label1"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label39.Location = New System.Drawing.Point(0, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(237, 1)
        Me.Label39.TabIndex = 6
        Me.Label39.Text = "label1"
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Transparent
        Me.Panel7.Controls.Add(Me.Panel1)
        Me.Panel7.Controls.Add(Me.pnl_SearchRight)
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(272, 585)
        Me.Panel7.TabIndex = 1
        Me.Panel7.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.trvTemplate)
        Me.Panel1.Controls.Add(Me.Label31)
        Me.Panel1.Controls.Add(Me.Label30)
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Controls.Add(Me.Label27)
        Me.Panel1.Controls.Add(Me.Label28)
        Me.Panel1.Controls.Add(Me.Label29)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 30)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(1, 1, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(272, 555)
        Me.Panel1.TabIndex = 17
        '
        'trvTemplate
        '
        Me.trvTemplate.BackColor = System.Drawing.Color.White
        Me.trvTemplate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvTemplate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvTemplate.ForeColor = System.Drawing.Color.Black
        Me.trvTemplate.HideSelection = False
        Me.trvTemplate.ImageIndex = 5
        Me.trvTemplate.ImageList = Me.imgTreeView
        Me.trvTemplate.ItemHeight = 20
        Me.trvTemplate.Location = New System.Drawing.Point(5, 6)
        Me.trvTemplate.Name = "trvTemplate"
        Me.trvTemplate.SelectedImageIndex = 5
        Me.trvTemplate.ShowLines = False
        Me.trvTemplate.Size = New System.Drawing.Size(263, 545)
        Me.trvTemplate.TabIndex = 1
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.White
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label31.Location = New System.Drawing.Point(2, 6)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(3, 545)
        Me.Label31.TabIndex = 39
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.White
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label30.Location = New System.Drawing.Point(2, 2)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(266, 4)
        Me.Label30.TabIndex = 38
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label26.Location = New System.Drawing.Point(2, 551)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(266, 1)
        Me.Label26.TabIndex = 12
        Me.Label26.Text = "label2"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(1, 2)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 550)
        Me.Label27.TabIndex = 11
        Me.Label27.Text = "label4"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label28.Location = New System.Drawing.Point(268, 2)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 550)
        Me.Label28.TabIndex = 10
        Me.Label28.Text = "label3"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(1, 1)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(268, 1)
        Me.Label29.TabIndex = 9
        Me.Label29.Text = "label1"
        '
        'pnl_SearchRight
        '
        Me.pnl_SearchRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_SearchRight.Controls.Add(Me.txtsearchAssociates)
        Me.pnl_SearchRight.Controls.Add(Me.Label20)
        Me.pnl_SearchRight.Controls.Add(Me.Label21)
        Me.pnl_SearchRight.Controls.Add(Me.PictureBox1)
        Me.pnl_SearchRight.Controls.Add(Me.Label9)
        Me.pnl_SearchRight.Controls.Add(Me.Label12)
        Me.pnl_SearchRight.Controls.Add(Me.Label24)
        Me.pnl_SearchRight.Controls.Add(Me.Label25)
        Me.pnl_SearchRight.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_SearchRight.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_SearchRight.ForeColor = System.Drawing.Color.Black
        Me.pnl_SearchRight.Location = New System.Drawing.Point(0, 0)
        Me.pnl_SearchRight.Name = "pnl_SearchRight"
        Me.pnl_SearchRight.Padding = New System.Windows.Forms.Padding(1, 3, 3, 3)
        Me.pnl_SearchRight.Size = New System.Drawing.Size(272, 30)
        Me.pnl_SearchRight.TabIndex = 16
        '
        'txtsearchAssociates
        '
        Me.txtsearchAssociates.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearchAssociates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsearchAssociates.ForeColor = System.Drawing.Color.Black
        Me.txtsearchAssociates.Location = New System.Drawing.Point(30, 8)
        Me.txtsearchAssociates.Multiline = True
        Me.txtsearchAssociates.Name = "txtsearchAssociates"
        Me.txtsearchAssociates.Size = New System.Drawing.Size(238, 16)
        Me.txtsearchAssociates.TabIndex = 0
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.White
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(30, 4)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(238, 4)
        Me.Label20.TabIndex = 37
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Location = New System.Drawing.Point(30, 24)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(238, 2)
        Me.Label21.TabIndex = 38
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(2, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 22)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(2, 26)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(266, 1)
        Me.Label9.TabIndex = 42
        Me.Label9.Text = "label2"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(1, 4)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 23)
        Me.Label12.TabIndex = 41
        Me.Label12.Text = "label4"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label24.Location = New System.Drawing.Point(268, 4)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 23)
        Me.Label24.TabIndex = 40
        Me.Label24.Text = "label3"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(1, 3)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(268, 1)
        Me.Label25.TabIndex = 39
        Me.Label25.Text = "label1"
        '
        'pnl_Middle
        '
        Me.pnl_Middle.BackColor = System.Drawing.Color.Transparent
        Me.pnl_Middle.Controls.Add(Me.trvCPTAssociation)
        Me.pnl_Middle.Controls.Add(Me.Label33)
        Me.pnl_Middle.Controls.Add(Me.Label1)
        Me.pnl_Middle.Controls.Add(Me.Label2)
        Me.pnl_Middle.Controls.Add(Me.Label3)
        Me.pnl_Middle.Controls.Add(Me.Label4)
        Me.pnl_Middle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Middle.Location = New System.Drawing.Point(243, 53)
        Me.pnl_Middle.Name = "pnl_Middle"
        Me.pnl_Middle.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.pnl_Middle.Size = New System.Drawing.Size(422, 585)
        Me.pnl_Middle.TabIndex = 1
        '
        'trvCPTAssociation
        '
        Me.trvCPTAssociation.BackColor = System.Drawing.Color.White
        Me.trvCPTAssociation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvCPTAssociation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvCPTAssociation.ForeColor = System.Drawing.Color.Black
        Me.trvCPTAssociation.HideSelection = False
        Me.trvCPTAssociation.ImageIndex = 5
        Me.trvCPTAssociation.ImageList = Me.imgTreeView
        Me.trvCPTAssociation.ItemHeight = 20
        Me.trvCPTAssociation.Location = New System.Drawing.Point(1, 8)
        Me.trvCPTAssociation.Name = "trvCPTAssociation"
        Me.trvCPTAssociation.SelectedImageIndex = 5
        Me.trvCPTAssociation.ShowLines = False
        Me.trvCPTAssociation.Size = New System.Drawing.Size(420, 573)
        Me.trvCPTAssociation.TabIndex = 4
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.White
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label33.Location = New System.Drawing.Point(1, 4)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(420, 4)
        Me.Label33.TabIndex = 40
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(1, 581)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(420, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(0, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 578)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Location = New System.Drawing.Point(421, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 578)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(0, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(422, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'pnl_ToolStrip
        '
        Me.pnl_ToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_ToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnl_ToolStrip.Controls.Add(Me.tblMedication)
        Me.pnl_ToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnl_ToolStrip.Name = "pnl_ToolStrip"
        Me.pnl_ToolStrip.Size = New System.Drawing.Size(908, 53)
        Me.pnl_ToolStrip.TabIndex = 3
        '
        'tblMedication
        '
        Me.tblMedication.BackColor = System.Drawing.Color.Transparent
        Me.tblMedication.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblMedication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblMedication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblMedication.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblMedication.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblMedication.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblNew, Me.tblSave, Me.tblFinish, Me.ts_gloCommunityDownload, Me.tblRefresh, Me.tblClose})
        Me.tblMedication.Location = New System.Drawing.Point(0, 0)
        Me.tblMedication.Name = "tblMedication"
        Me.tblMedication.Size = New System.Drawing.Size(908, 53)
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
        Me.tblNew.Visible = False
        '
        'tblRefresh
        '
        Me.tblRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblRefresh.Image = CType(resources.GetObject("tblRefresh.Image"), System.Drawing.Image)
        Me.tblRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblRefresh.Name = "tblRefresh"
        Me.tblRefresh.Size = New System.Drawing.Size(58, 50)
        Me.tblRefresh.Text = "Re&fresh"
        Me.tblRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblRefresh.Visible = False
        '
        'tblSave
        '
        Me.tblSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblSave.Image = CType(resources.GetObject("tblSave.Image"), System.Drawing.Image)
        Me.tblSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblSave.Name = "tblSave"
        Me.tblSave.Size = New System.Drawing.Size(40, 50)
        Me.tblSave.Tag = "Save"
        Me.tblSave.Text = "&Save"
        Me.tblSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblSave.ToolTipText = "Save"
        Me.tblSave.Visible = False
        '
        'tblFinish
        '
        Me.tblFinish.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblFinish.Image = CType(resources.GetObject("tblFinish.Image"), System.Drawing.Image)
        Me.tblFinish.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblFinish.Name = "tblFinish"
        Me.tblFinish.Size = New System.Drawing.Size(66, 50)
        Me.tblFinish.Tag = "Finish"
        Me.tblFinish.Text = "&Save&&Cls"
        Me.tblFinish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblFinish.ToolTipText = "Save and Close"
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
        '
        'cntICD9Association
        '
        Me.cntICD9Association.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuRemoveCPT, Me.mnuDeleteICD9Item})
        '
        'mnuRemoveCPT
        '
        Me.mnuRemoveCPT.Index = 0
        Me.mnuRemoveCPT.Text = "Remove All Template"
        '
        'mnuDeleteICD9Item
        '
        Me.mnuDeleteICD9Item.Index = 1
        Me.mnuDeleteICD9Item.Text = "Remove Template"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(240, 53)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 585)
        Me.Splitter1.TabIndex = 4
        Me.Splitter1.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter2.Location = New System.Drawing.Point(665, 53)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 585)
        Me.Splitter2.TabIndex = 5
        Me.Splitter2.TabStop = False
        '
        'ts_gloCommunityDownload
        '
        Me.ts_gloCommunityDownload.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ts_gloCommunityDownload.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_gloCommunityDownload.Image = CType(resources.GetObject("ts_gloCommunityDownload.Image"), System.Drawing.Image)
        Me.ts_gloCommunityDownload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_gloCommunityDownload.Name = "ts_gloCommunityDownload"
        Me.ts_gloCommunityDownload.Size = New System.Drawing.Size(73, 50)
        Me.ts_gloCommunityDownload.Tag = "gloCommunityDownload"
        Me.ts_gloCommunityDownload.Text = "Download"
        Me.ts_gloCommunityDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_gloCommunityDownload.ToolTipText = "Download from gloCommunity"
        Me.ts_gloCommunityDownload.Visible = False
        '
        'frmCPTAssociation
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(908, 638)
        Me.Controls.Add(Me.pnl_Middle)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnl_Right)
        Me.Controls.Add(Me.pnl_Left)
        Me.Controls.Add(Me.pnl_ToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCPTAssociation"
        Me.ShowInTaskbar = False
        Me.Text = "Form Gallery"
        Me.pnl_Left.ResumeLayout(False)
        Me.pnltrvCPT.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRadioBtn.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.pnl_Right.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnl_SearchRight.ResumeLayout(False)
        Me.pnl_SearchRight.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_Middle.ResumeLayout(False)
        Me.pnl_ToolStrip.ResumeLayout(False)
        Me.pnl_ToolStrip.PerformLayout()
        Me.tblMedication.ResumeLayout(False)
        Me.tblMedication.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region



    Private Sub frmCPTAssociation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Code Start added by kanchan on 20120102 for gloCommunity integration
            If gblnIsShareUserRights = True Then ''Added condition to fixed Bug # : 37984 on 20120927
                ts_gloCommunityDownload.Visible = gblngloCommunity
            End If
            'Code end added by kanchan on 20120102 for gloCommunity integration
            If (IsNothing(objclsCPTAssociation) = False) Then
                objclsCPTAssociation.Dispose()
                objclsCPTAssociation = Nothing
            End If
            objclsCPTAssociation = New clsCPTAssociation
            trvCPTAssociation.AllowDrop = True

            Dim rootnode As myTreeNode
            ' Dim i As Integer
            'Dim dt As DataTable

            rootnode = New myTreeNode("CPT Association", -1)
            rootnode.ImageIndex = 4
            rootnode.SelectedImageIndex = 4
            trvCPTAssociation.Nodes.Add(rootnode)

            'rootnode = New myTreeNode("CPT", -1)
            'rootnode.ImageIndex = 1
            'rootnode.SelectedImageIndex = 1
            'trvCPT.Nodes.Add(rootnode)
            ''Populate CPT data
            '' TO Pull CPTs Order By CPTCode Pass 0
            UpdateLog(" Loading of CPT Association Started")
            If (IsNothing(dtCPTDesc) = False) Then
                dtCPTDesc.Dispose()
                dtCPTDesc = Nothing
            End If
            dtCPTDesc = objclsCPTAssociation.FillCPT(1)
            If Not IsNothing(dtCPTDesc) Then
                GloUC_trvCPT.DataSource = dtCPTDesc
                GloUC_trvCPT.ValueMember = dtCPTDesc.Columns(0).ColumnName
                GloUC_trvCPT.Tag = dtCPTDesc.Columns(0).ColumnName
                GloUC_trvCPT.CodeMember = dtCPTDesc.Columns(3).ColumnName
                GloUC_trvCPT.DescriptionMember = dtCPTDesc.Columns(1).ColumnName
                GloUC_trvCPT.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description
                GloUC_trvCPT.FillTreeView()
            End If

            UpdateLog(" Loading of CPT Association END")

            'If IsNothing(dtCPTDesc) = False Then
            '    For i = 0 To dtCPTDesc.Rows.Count - 1
            '        Dim mychildnode As myTreeNode
            '        mychildnode = New myTreeNode(dtCPTDesc.Rows(i)(2), dtCPTDesc.Rows(i)(0), CType(dtCPTDesc.Rows(i)(1), String))
            '        'mychildnode.Text = CPTCode + Desc
            '        'mychildnode.Tag = CPT Desc
            '        'mychildnode.Key = ID
            '        mychildnode.ImageIndex = 5
            '        mychildnode.SelectedImageIndex = 5
            '        trvCPT.Nodes.Item(0).Nodes.Add(mychildnode)
            '    Next
            'End If

            'trvCPT.SelectedNode = trvCPT.Nodes.Item(0)
            'trvCPT.ExpandAll()

            'Call FillTemplate()

            'dtTemplate = New DataTable
            'dtTemplate = FillAllTemplates()
            Dim dt As DataTable
            dt = FillAllTemplates()
            If Not IsNothing(dt) Then
                GloUC_trvTemplate.DataSource = dt
                GloUC_trvTemplate.ValueMember = dt.Columns(0).ColumnName
                GloUC_trvTemplate.CodeMember = dt.Columns(1).ColumnName
                GloUC_trvTemplate.DescriptionMember = dt.Columns(2).ColumnName
                GloUC_trvTemplate.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
                GloUC_trvTemplate.FillTreeView()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function FillAllTemplates() As DataTable
        Dim dt As DataTable
        Dim objTemplateGallery As New clsTemplateGallery
        Try



            dt = objTemplateGallery.FillAllTemplates()

            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            objTemplateGallery.Dispose()
            objTemplateGallery = Nothing
        End Try

    End Function

    Private Sub FillTemplate()
        Dim newNode As New TreeNode
        Dim objMyTreeView As myTreeNode
        Dim objTemplateGallery As New clsTemplateGallery
        Dim objCategory As myTreeNode
        Dim objTemplate As myTreeNode
        Dim dt As DataTable = objTemplateGallery.GetAllCategory

        'dtTemplate = New DataTable
        If (IsNothing(dtTemplate) = False) Then
            dtTemplate.Dispose()
            dtTemplate = Nothing
        End If
        If (IsNothing(dt) = False) Then


            dtTemplate = dt.Copy()

            Dim i As Integer
            Dim j As Integer
            trvTemplate.Nodes.Clear()

            objMyTreeView = New myTreeNode("Template", 0)
            objMyTreeView.ImageIndex = 2  '' Category ICon
            objMyTreeView.SelectedImageIndex = 2 '' Category ICon
            trvTemplate.Nodes.Add(objMyTreeView)

            Dim ValueMember As Int64
            Dim DisplayMember As String

            For i = 0 To dt.Rows.Count - 1
                'Dim ValueMember As Int64
                'Dim DisplayMember As String
                ValueMember = dt.Rows(i)(0)
                DisplayMember = dt.Rows(i)(1)
                objCategory = New myTreeNode(DisplayMember, ValueMember)
                objCategory.ImageIndex = 3 '''' Template ICon
                objCategory.SelectedImageIndex = 3 '''' Template ICon
                objMyTreeView.Nodes.Add(objCategory)

                Dim dvTemplate As DataView = objTemplateGallery.GetAllTemplateGallery(ValueMember)
                If (IsNothing(dvTemplate) = False) Then


                    For j = 0 To dvTemplate.Table.Rows.Count - 1
                        ''Dim ValueMember As Int64
                        ''Dim DisplayMember As String
                        ValueMember = dvTemplate.Table.Rows(j)(0)
                        DisplayMember = dvTemplate.Table.Rows(j)(1)
                        objTemplate = New myTreeNode(DisplayMember, ValueMember)
                        objTemplate.ImageIndex = 5 '''' Play ICon
                        objTemplate.SelectedImageIndex = 5 '''' Play ICon
                        objCategory.Nodes.Add(objTemplate)
                        objCategory.EnsureVisible()
                        'objCategory.ExpandAll()
                    Next
                    dvTemplate.Dispose()
                    dvTemplate = Nothing
                End If
            Next
            dt.Dispose()
            dt = Nothing
        End If
        objTemplateGallery.Dispose()
        objTemplateGallery = Nothing
        'trvTemplate.ExpandAll()
    End Sub

    Private Sub txtsearchDrugs_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearchDrugs.TextChanged

        '''''''''''''''''''####################''''''''''''''''''''''''''
        '''''Code lines below are added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
        Dim strSearchDetails As String
        If Trim(txtsearchDrugs.Text) <> "" Then
            strSearchDetails = Replace(txtsearchDrugs.Text, "'", "''")
            strSearchDetails = Replace(strSearchDetails, "[", "") & ""
            strSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strSearchDetails)
        Else
            strSearchDetails = ""
        End If
        '''''''''''''''''''####################''''''''''''''''''''''''''


        'Try
        '    Dim mychildnode As myTreeNode
        '    'child node collection
        '    For Each mychildnode In trvCPT.Nodes.Item(0).Nodes
        '        'compare selected node text and entered text
        '        Dim str As String
        '        str = Mid(UCase(Trim(mychildnode.Tag)), 1, Len(UCase(Trim(txtsearchDrugs.Text))))
        '        If str = UCase(Trim(txtsearchDrugs.Text)) Then
        '            trvCPT.SelectedNode = mychildnode
        '            txtsearchDrugs.Focus()
        '            Exit Sub
        '        End If
        '    Next
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "CPT Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try

        'sarika 26th sept 07
        'implement the instring search 

        If txtsearchDrugs.Tag <> Trim(strSearchDetails) Then
            ' If btnAllDrugs.Dock = DockStyle.Top Then
            If rbSearch2.Checked = False Then
                AddCPT(Trim(strSearchDetails), dtCPTCode)
            Else
                AddCPT(Trim(strSearchDetails), dtCPTDesc)
            End If

            'ElseIf btnClinicalDrugs.Dock = DockStyle.Top Then
            '    AddDrugs(Trim(txtsearchCPT.Text))
            'ElseIf btnFreqDrugs.Dock = DockStyle.Top Then
            '    AddDrugs(Trim(txtsearchCPT.Text))
            'End If
            'If Len(Trim(txtsearchDrug.Text)) = 1 Then
            txtsearchDrugs.Tag = Trim(strSearchDetails)
            'End If
        End If
        Exit Sub
        '----------------------------------------------------
        'SLR : 8/5/2014: Code review: What is the purpose of following code : i commented ?

        'Try
        '    If Trim(txtsearchDrugs.Text) <> "" Then
        '        If trvCPT.Nodes.Item(0).GetNodeCount(False) > 0 Then
        '            Dim mychildnode As myTreeNode
        '            'child node collection
        '            For Each mychildnode In trvCPT.Nodes.Item(0).Nodes
        '                'search against Description
        '                If rbSearch2.Checked = True Then
        '                    If UCase(Mid(mychildnode.Tag, 1, Len(Trim(txtsearchDrugs.Text)))) = UCase(Trim(txtsearchDrugs.Text)) Then
        '                        '*************code added by sagar for showing the selected drug at the top on 4 july 2007
        '                        trvCPT.SelectedNode = trvCPT.SelectedNode.LastNode
        '                        '*************
        '                        'select matching node
        '                        trvCPT.SelectedNode = mychildnode
        '                        txtsearchDrugs.Focus()
        '                        Exit Sub
        '                    End If
        '                Else
        '                    'search against CPT Code
        '                    Dim str As String
        '                    str = Mid(mychildnode.Text, 1, Len(Trim(mychildnode.Text)) - (Len(mychildnode.Tag) + 1))
        '                    str = Mid(str, 1, Len(Trim(txtsearchDrugs.Text)))
        '                    If UCase(str) = UCase(Trim(txtsearchDrugs.Text)) Then
        '                        '*************code added by sagar for showing the selected drug at the top on 4 july 2007
        '                        trvCPT.SelectedNode = trvCPT.SelectedNode.LastNode
        '                        '*************
        '                        'select matching node
        '                        trvCPT.SelectedNode = mychildnode
        '                        txtsearchDrugs.Focus()
        '                        Exit Sub
        '                    End If
        '                End If
        '            Next


        '            ' '' 
        '            ' '' 20070922 - Mahesh - InString Search 
        '            For Each mychildnode In trvCPT.Nodes.Item(0).Nodes
        '                'search against Description
        '                If rbSearch2.Checked = True Then
        '                    If InStr(UCase(mychildnode.Tag.ToString.Trim), UCase(txtsearchDrugs.Text.Trim), CompareMethod.Text) > 0 Then
        '                        trvCPT.SelectedNode = trvCPT.SelectedNode.LastNode
        '                        'select matching node
        '                        trvCPT.SelectedNode = mychildnode
        '                        txtsearchDrugs.Focus()
        '                        Exit Sub
        '                    End If
        '                Else
        '                    'search against CPT Code
        '                    Dim str As String
        '                    str = Mid(mychildnode.Text, 1, Len(Trim(mychildnode.Text)) - (Len(mychildnode.Tag) + 1))
        '                    '  str = Mid(str, 1, Len(Trim(txtsearchDrugs.Text)))
        '                    If InStr(UCase(str.Trim), UCase(txtsearchDrugs.Text.Trim), CompareMethod.Text) > 0 Then
        '                        trvCPT.SelectedNode = trvCPT.SelectedNode.LastNode
        '                        'select matching node
        '                        trvCPT.SelectedNode = mychildnode
        '                        txtsearchDrugs.Focus()
        '                        Exit Sub
        '                    End If
        '                End If
        '            Next
        '            ' '' 
        '        End If
        '    End If
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub
    Private Sub trCPTAssociation_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trvCPTAssociation.DragOver
        Try
            'Check that there is a TreeNode being dragged
            'commented on 30/8/2005 If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
            If e.Data.GetDataPresent("gloEMR.myTreeNode", True) = False Then Exit Sub

            'Get the TreeView raising the event (incase multiple on form)
            Dim selectedTreeview As TreeView = CType(sender, TreeView)

            'As the mouse moves over nodes, provide feedback to the user
            'by highlighting the node that is the current drop target
            Dim pt As Point = CType(sender, TreeView).PointToClient(New Point(e.X, e.Y))

            'commented on 30/8/2005 Dim targetNode As TreeNode = selectedTreeview.GetNodeAt(pt)
            Dim targetNode As myTreeNode = selectedTreeview.GetNodeAt(pt)

            'See if the targetNode is currently selected, if so no need to validate again
            If Not (selectedTreeview Is targetNode) Then
                'Select the node currently under the cursor
                selectedTreeview.SelectedNode = targetNode

                'Check that the selected node is not the dropNode and also that it
                'is not a child of the dropNode and therefore an invalid target
                Dim dropNode As TreeNode = CType(e.Data.GetData("gloEMR.myTreeNode"), myTreeNode)
                Do Until targetNode Is Nothing
                    If targetNode Is dropNode Then
                        e.Effect = DragDropEffects.None
                        Exit Sub
                    End If
                    targetNode = targetNode.Parent
                Loop
            End If

            'Currently selected node is a suitable target, allow the move
            e.Effect = DragDropEffects.Move
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "CPT Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trCPT_ItemDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles trvCPT.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub trCPT_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trvCPT.DragDrop, trvCPTAssociation.DragDrop
        Try
            'Check that there is a TreeNode being dragged

            'commented on 30/08/2005 If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
            If e.Data.GetDataPresent("gloEMR.myTreeNode", True) = False Then Exit Sub

            'Get the TreeView raising the event (incase multiple on form)
            Dim selectedTreeview As TreeView = CType(sender, TreeView)

            'Get the TreeNode being dragged
            'commented on 30/08/2005 Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

            Dim dropNode As myTreeNode = CType(e.Data.GetData("gloEMR.myTreeNode"), myTreeNode)

            'The target node should be selected from the DragOver event

            'commented on 30/08/2005 Dim targetNode As TreeNode = selectedTreeview.SelectedNode

            Dim targetNode As myTreeNode = CType(selectedTreeview.SelectedNode, myTreeNode)

            'Remove the drop node from its current location

            'If there is no targetNode add dropNode to the bottom of the TreeView root
            'nodes, otherwise add it to the end of the dropNode child nodes
            'If targetNode Is Nothing Then
            '    dropNode.Remove()
            '    selectedTreeview.Nodes.Add(dropNode)
            '    AddControl()
            '    PopulateMedication(dropNode.Key)

            'targetnode is the node selected on which the dropnode is to be dropped.
            'If targetNode Is selectedTreeview.Nodes.Item(0) Then
            'If Not IsNothing(dropNode) Then
            '    AddNode(dropNode)
            'End If
            'commented from 14/09/2005
            If dropNode.Parent Is trvCPT.Nodes.Item(0) Then
                Dim str As String
                str = dropNode.Key
                Dim mytragetnode As myTreeNode
                For Each mytragetnode In trvCPTAssociation.Nodes.Item(0).Nodes
                    If mytragetnode.Key = str Then
                        Exit Sub
                    End If
                Next

                Dim associatenode As myTreeNode
                associatenode = dropNode.Clone
                associatenode.Key = dropNode.Key
                associatenode.Text = dropNode.Text

                associatenode.ImageIndex = 1
                associatenode.SelectedImageIndex = 1

                'If PopulateMedication(dropNode.Key) Then
                trvCPTAssociation.Nodes.Item(0).Nodes.Add(associatenode)

                Dim dt As DataTable
                dt = objclsCPTAssociation.FetchCPTforUpdate(associatenode.Key)
                Dim i As Integer
                For i = 0 To dt.Rows.Count - 1
                    associatenode.Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                Next
                'Else
                'RemoveControl() 'treeindex = targetNode.GetNodeCount(False) - 1
                'End If
                'treeindex = -1
                'End If

                'Ensure the newley created node is visible to the user and select it
                'dropNode.EnsureVisible()
                'dropNode.Expand()
                'trvCPTAssociation.ExpandAll()
                ''trvCPTAssociation.Select()
                'selectedTreeview.SelectedNode = dropNode

                trvCPTAssociation.ExpandAll()
                trvCPTAssociation.Select()

                'treeindex = -1
                'End If

                'Ensure the newly created node is visible to the user and select it
                dropNode.EnsureVisible()
                trvCPTAssociation.SelectedNode = dropNode
            End If
            'commented from 14/09/2005
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "CPT Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '  Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'RefreshCPT()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshCPT()
        'Get Refresh the Search on CPT 
        txtsearchDrugs.Text = ""
        txtsearchDrugs.Focus()
        'Refresh the Search in Templates 
        txtsearchAssociates.Text = ""
        trvCPTAssociation.Nodes(0).Nodes.Clear()
        'Code Commented by Mayuri:20091001
        'To fix BUG ID:#4347
        'trvCPT.Nodes(0).Nodes.Clear()

        'objclsCPTAssociation = New clsCPTAssociation
        'trvCPTAssociation.AllowDrop = True

        'Dim rootnode As myTreeNode
        'Dim i As Integer

        'Populate CPT Order by CPT Code 
        'dtCPTCode = objclsCPTAssociation.FillCPT(0) ' TO Pull CPTs

        'trvCPT.Hide()

        'For i = 0 To dtCPTCode.Rows.Count - 1
        '    Dim mychildnode As myTreeNode
        '    mychildnode = New myTreeNode(dtCPTCode.Rows(i)(2), dtCPTCode.Rows(i)(0), CType(dtCPTCode.Rows(i)(1), String))
        '    mychildnode.ImageIndex = 5
        '    mychildnode.SelectedImageIndex = 5
        '    trvCPT.Nodes.Item(0).Nodes.Add(mychildnode)
        'Next
        'trvCPT.SelectedNode = trvCPT.Nodes(0)
        'trvCPT.ExpandAll()
        'trvCPTAssociation.Nodes(0).Nodes.Clear()

        'trvCPT.Show()
        'trvCPT.Select()


    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            SaveAssociation()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub SaveAssociation()
        'Get node count of child nodes in trICD9Associates
        Dim i As Integer
        Dim j As Integer

        Dim blnAllowSave As Boolean

        ' Code to check whether there are Histories Entered to save or Nor
        '''''For i = 0 To trvTarget.GetNodeCount(False) - 1
        '''''    If trvTarget.Nodes.Item(i).GetNodeCount(False) > 0 Then
        '''''        blnAllowSave = True
        '''''        Exit For
        '''''    End If
        '''''Next

        blnAllowSave = True

        'For i = 0 To trvCPTAssociation.Nodes.Item(0).GetNodeCount(False) - 1
        '    If trvCPTAssociation.Nodes.Item(0).Nodes.Item(i).GetNodeCount(False) < 1 Then
        '        blnAllowSave = False
        '        trvCPTAssociation.SelectedNode = trvCPTAssociation.Nodes.Item(0).Nodes.Item(i)
        '        Exit For
        '    End If
        'Next

        'If blnAllowSave = False Then
        '    MessageBox.Show("Every CPT should associate with atleast one Template", "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Exit Sub
        'End If

        If trvCPTAssociation.Nodes.Item(0).GetNodeCount(False) > 0 Then
            'Dim i As Integer
            For i = 0 To trvCPTAssociation.Nodes.Item(0).GetNodeCount(False) - 1
                Dim CPTNode As myTreeNode
                'get the CPTNode associated sequentially
                CPTNode = trvCPTAssociation.Nodes.Item(0).Nodes.Item(i)
                If CPTNode.GetNodeCount(True) >= 0 Then
                    'Dim k As Integer
                    Dim arrlist As New ArrayList
                    'For k = 0 To 2
                    Dim TemplateNode As myTreeNode
                    'TemplateNode = CType(CPTNode.Nodes, myTreeNode)
                    'Dim j As Integer
                    For j = 0 To CPTNode.GetNodeCount(True) - 1
                        TemplateNode = CType(CPTNode.Nodes.Item(j), myTreeNode)
                        arrlist.Add(New myList(TemplateNode.Key, "c"))
                    Next

                    ' Next
                    'objclsCPTAssociation.AddData(CPTNode.Key, CPTNode.Tag, arrlist)
                    objclsCPTAssociation.AddData(CPTNode.Key, CPTNode.Text, arrlist)
                    arrlist.Clear()
                    arrlist = Nothing
                Else

                End If

            Next
            trvCPTAssociation.Nodes(0).Nodes.Clear()
            'RefreshCPT()
        End If
        'Shubhangi 20091202
        'Change button to save & close
        'Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub
    Private Sub trAssociates_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trvTemplate.DragDrop
        Try
            'Check that there is a TreeNode being dragged

            'commented on 30/08/2005 If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
            If e.Data.GetDataPresent("gloEMR.myTreeNode", True) = False Then Exit Sub

            'Get the TreeView raising the event (incase multiple on form)
            Dim selectedTreeview As TreeView = CType(sender, TreeView)

            'Get the TreeNode being dragged
            'commented on 30/08/2005 Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

            Dim dropNode As myTreeNode = CType(e.Data.GetData("gloEMR.myTreeNode"), myTreeNode)

            'The target node should be selected from the DragOver event

            'commented on 30/08/2005 Dim targetNode As TreeNode = selectedTreeview.SelectedNode

            Dim targetNode1 As myTreeNode = CType(selectedTreeview.SelectedNode, myTreeNode)

            'Remove the drop node from its current location

            'If there is no targetNode add dropNode to the bottom of the TreeView root
            'nodes, otherwise add it to the end of the dropNode child nodes
            'If targetNode Is Nothing Then
            '    dropNode.Remove()
            '    selectedTreeview.Nodes.Add(dropNode)
            '    AddControl()
            '    PopulateMedication(dropNode.Key)

            'targetnode is the node selected on which the dropnode is to be dropped.
            'If targetNode Is selectedTreeview.Nodes.Item(0) Then

            'check if dropnode is node selected from trvTemplate treeview
            If Not IsNothing(targetNode1) AndAlso Not IsNothing(dropNode) Then
                AddAssociates(dropNode, targetNode1)
            End If
            'commented from 14/09/2005
            'If dropNode.Parent Is trvTemplate.Nodes.Item(0) Then
            '    Dim targetnode As myTreeNode
            '    'check if targetnode is node at second level in trvCPTAssociation treeview
            '    If targetNode1.Parent Is trvCPTAssociation.Nodes.Item(0) Or (targetNode1.Key = -1) Then
            '        If targetNode1.Parent Is trvCPTAssociation.Nodes.Item(0) Then
            '            targetnode = targetNode1
            '        Else

            '            targetnode = targetNode1.Parent
            '        End If

            '        Dim str As String
            '        str = dropNode.Key
            '        Dim mytragetnode As myTreeNode
            '        Dim associatenode As myTreeNode

            '        associatenode = dropNode.Clone
            '        associatenode.Key = dropNode.Key
            '        associatenode.Text = dropNode.Text

            '        'if selected category is cpt, add node to cpt child node 
            '        'in trICD9Associates
            '        If btnCPT.Dock = DockStyle.Top Then
            '            For Each mytragetnode In targetnode.Nodes.Item(0).Nodes
            '                If mytragetnode.Key = str Then
            '                    Exit Sub
            '                End If
            '            Next
            '            targetnode.Nodes.Item(0).Nodes.Add(associatenode)
            '            'if selected category is Drugs, add node to Drugs child node 
            '            'in trICD9Associates
            '        ElseIf btnDrugs.Dock = DockStyle.Top Then
            '            For Each mytragetnode In targetnode.Nodes.Item(1).Nodes
            '                If mytragetnode.Key = str Then
            '                    Exit Sub
            '                End If
            '            Next
            '            targetnode.Nodes.Item(1).Nodes.Add(associatenode)

            '            'if selected category is PatientEducation, add node to PatientEducation child node 
            '            'in trICD9Associates
            '        ElseIf btnPatientEducation.Dock = DockStyle.Top Then
            '            For Each mytragetnode In targetnode.Nodes.Item(2).Nodes
            '                If mytragetnode.Key = str Then
            '                    Exit Sub
            '                End If
            '            Next
            '            targetnode.Nodes.Item(2).Nodes.Add(associatenode)
            '        End If
            '        dropNode.EnsureVisible()
            '        selectedTreeview.ExpandAll()
            '        selectedTreeview.SelectedNode = dropNode

            '    End If
            'End If
            'commendted from 14/09/2005
            'Ensure the newley created node is visible to the user and select it
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "CPT Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub trvTemplate_ItemDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles trvTemplate.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub trICD9Association_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvCPTAssociation.MouseDown
        Try
            If e.Button = MouseButtons.Right Then
                Dim trvNode As myTreeNode
                trvNode = trvCPTAssociation.GetNodeAt(e.X, e.Y)
                If IsNothing(trvNode) = False Then
                    trvCPTAssociation.SelectedNode = trvNode
                End If


                If Not IsNothing(trvCPTAssociation.SelectedNode) Then
                    '''' if Selected Node is Root Node
                    'If trvCPTAssociation.Nodes.Item(0).Text = trvCPTAssociation.SelectedNode.Text Or trvCPTAssociation.SelectedNode.Parent Is trvCPTAssociation.Nodes.Item(0) Or (CType(trvCPTAssociation.SelectedNode, myTreeNode).Key = -1) Then
                    If trvCPTAssociation.Nodes.Item(0).Text = trvCPTAssociation.SelectedNode.Text Or (CType(trvCPTAssociation.SelectedNode, myTreeNode).Key = -1) Then ' Or trvCPTAssociation.SelectedNode.Parent Is trvCPTAssociation.Nodes.Item(0) Then
                        'Try
                        '    If (IsNothing(trvCPTAssociation.ContextMenu) = False) Then
                        '        trvCPTAssociation.ContextMenu.Dispose()
                        '        trvCPTAssociation.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvCPTAssociation.ContextMenu = Nothing
                
                    Else
                        'Try
                        '    If (IsNothing(trvCPTAssociation.ContextMenu) = False) Then
                        '        trvCPTAssociation.ContextMenu.Dispose()
                        '        trvCPTAssociation.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trvCPTAssociation.ContextMenu = cntICD9Association
                        If trvCPTAssociation.SelectedNode.Parent Is trvCPTAssociation.Nodes.Item(0) Then
                            cntICD9Association.MenuItems(0).Visible = True
                            cntICD9Association.MenuItems(1).Visible = False
                            'Exit Sub
                        Else
                            cntICD9Association.MenuItems(0).Visible = False
                            cntICD9Association.MenuItems(1).Visible = True
                        End If
                        If (trvCPTAssociation.SelectedNode.Level = 1 AndAlso trvCPTAssociation.SelectedNode.Nodes.Count <= 0) Then
                            'Try
                            '    If (IsNothing(trvCPTAssociation.ContextMenu) = False) Then
                            '        trvCPTAssociation.ContextMenu.Dispose()
                            '        trvCPTAssociation.ContextMenu = Nothing
                            '    End If
                            'Catch ex As Exception

                            'End Try
                            trvCPTAssociation.ContextMenu = Nothing
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuDeleteICD9Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteICD9Item.Click
        Try
            If Not trvCPTAssociation.SelectedNode Is trvCPTAssociation.Nodes.Item(0) OrElse Not trvCPTAssociation.SelectedNode.Parent Is trvCPTAssociation.Nodes.Item(0) Then

                Dim mychildnode As myTreeNode
                '  Dim key As Int64
                mychildnode = CType(trvCPTAssociation.SelectedNode, myTreeNode)
                'objMedicationDBLayer.DeleteMedication(mychildnode.Index) 'delete from collection
                'key = mychildnode.Key
                mychildnode.Remove() 'delete from Medicationdetails treeview

                'Add the deleted node to Medication treeview

            End If
        Catch ex As SqlClient.SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvCPT_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trvCPT.DoubleClick
        Try
            Dim mynode As myTreeNode
            mynode = CType(trvCPT.SelectedNode, myTreeNode)
            If Not IsNothing(mynode) Then
                AddNode(mynode)
            End If
            'selectedTreeview.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub AddNode(ByVal mynode As myTreeNode)
        'If mynode.Parent Is trvCPT.Nodes.Item(0) Then
        Dim str As Long 'String
        str = mynode.Key

        Dim mytragetnode As myTreeNode
        For Each mytragetnode In trvCPTAssociation.Nodes.Item(0).Nodes
            If mytragetnode.Key = str Then
                '''' check if CPT Node is already Exit 
                Exit Sub
            End If
        Next

        Dim associatenode As myTreeNode
        associatenode = mynode.Clone
        associatenode.Key = mynode.Key
        associatenode.Text = mynode.Text

        associatenode.ImageIndex = 1
        associatenode.SelectedImageIndex = 1

        trvCPTAssociation.Nodes(0).Nodes.Add(associatenode)
        ''  mynode.Nodes.Add(New myTreeNode("Template", -1))

        Dim dt As DataTable
        '''' To Get Already Associated Template with Selected CPT
        dt = objclsCPTAssociation.FetchCPTforUpdate(associatenode.Key)
        If (IsNothing(dt) = False) Then


            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1
                ''''Add Templaets to cpt node 
                associatenode.Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
            Next
            trvCPTAssociation.ExpandAll()
            trvCPTAssociation.Select()

            'Ensure the newly created node is visible to the user and select it
            associatenode.EnsureVisible()
            trvCPTAssociation.SelectedNode = associatenode
            'treeindex = mynode.Index
            'End If
            dt.Dispose()
            dt = Nothing
        End If
    End Sub

    Private Sub txtsearchAssociates_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtsearchAssociates.MouseUp

    End Sub

    Private Sub txtsearchAssociates_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtsearchAssociates.Validating
        'Try
        '    Dim mychildnode As myTreeNode
        '    'child node collection
        '    For Each mychildnode In trvTemplate.Nodes.Item(0).Nodes
        '        'compare selected node text and entered text
        '        Dim str As String
        '        str = Mid(UCase(Trim(mychildnode.Tag)), 1, Len(UCase(Trim(txtsearchAssociates.Text))))
        '        If str = UCase(Trim(txtsearchAssociates.Text)) Then
        '            trvTemplate.SelectedNode = mychildnode
        '            Exit Sub
        '        End If
        '    Next
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub txtsearchDrugs_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearchDrugs.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trvCPT.Select()
        Else
            trvCPT.SelectedNode = trvCPT.Nodes.Item(0)
        End If
        ''--Added by Anil on 20071213
        mdlGeneral.ValidateText(txtsearchDrugs.Text, e)
        ''----
    End Sub

    Private Sub txtsearchAssociates_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearchAssociates.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trvTemplate.Select()
        Else
            trvTemplate.SelectedNode = trvTemplate.Nodes.Item(0)
        End If
        ''--Added by Anil on 20071213
        mdlGeneral.ValidateText(txtsearchAssociates.Text, e)
        ''----
    End Sub

    Private Sub txtsearchDrugs_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtsearchDrugs.Validating
        'Try
        '    Dim mychildnode As myTreeNode
        '    'child node collection
        '    For Each mychildnode In trvCPT.Nodes.Item(0).Nodes
        '        'compare selected node text and entered text
        '        Dim str As String
        '        str = Mid(UCase(Trim(mychildnode.Tag)), 1, Len(UCase(Trim(txtsearchDrugs.Text))))
        '        If str = UCase(Trim(txtsearchDrugs.Text)) Then
        '            trvCPT.SelectedNode = mychildnode
        '            Exit Sub
        '        End If
        '    Next
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub trICD9_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvCPT.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            Try
                Dim mynode As myTreeNode
                mynode = CType(trvCPT.SelectedNode, myTreeNode)
                If Not IsNothing(mynode) Then
                    AddNode(mynode)
                End If
                'selectedTreeview.ExpandAll()
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    'Private Sub btnNew_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim tooltipnew As New ToolTip
    '    tooltipnew.SetToolTip(btnNew, "Clear ICD9 Associations")
    'End Sub

    'Private Sub btnSave_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim tooltipnew As New ToolTip
    '    tooltipnew.SetToolTip(btnSave, "Save ICD9 Associations")
    'End Sub

    'Private Sub btnclose_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim tooltipnew As New ToolTip
    '    tooltipnew.SetToolTip(btnclose, "Close ICD9 Associations")
    'End Sub

    Private Sub trvTemplate_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles trvTemplate.DoubleClick
        'MsgBox(CType(trvCPTAssociation.SelectedNode, myTreeNode).Text)
        Try
            Dim mynode As myTreeNode
            Dim targetnode1 As myTreeNode
            targetnode1 = CType(trvCPTAssociation.SelectedNode, myTreeNode)
            mynode = CType(trvTemplate.SelectedNode, myTreeNode)
            'check if selected node is rootnode
            If Not IsNothing(targetnode1) AndAlso Not IsNothing(mynode) Then
                AddAssociates(mynode, targetnode1)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trAssociates_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvTemplate.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            Try
                Dim mynode As myTreeNode
                Dim targetnode1 As myTreeNode
                targetnode1 = CType(trvCPTAssociation.SelectedNode, myTreeNode)
                mynode = CType(trvTemplate.SelectedNode, myTreeNode)
                'check if selected node is rootnode
                If Not IsNothing(targetnode1) AndAlso Not IsNothing(mynode) Then
                    AddAssociates(mynode, targetnode1)
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    Private Sub AddAssociates(ByVal mynode As myTreeNode, ByVal targetnode1 As myTreeNode)

        If trvCPTAssociation.Nodes(0).GetNodeCount(False) < 1 Then
            Exit Sub
        End If

        'If mynode Is trvTemplate.Nodes(0) Then
        '    Exit Sub
        'End If

        'If mynode.Parent Is trvTemplate.Nodes(0) Then
        '    Exit Sub
        'End If
        ''If the Node is root node then do not add template
        ''Added by Anil on 20071220
        If targetnode1 Is trvCPTAssociation.Nodes.Item(0) Then
            Exit Sub
        End If
        ''
        'If Not mynode Is trvTemplate.Nodes(0) Then   'not root node
        Dim targetnode As myTreeNode
        'check if targetnode is node at second level in trvCPTAssociation treeview
        If targetnode1.Parent Is trvCPTAssociation.Nodes.Item(0) Or (targetnode1.Key = -1) Then
            If targetnode1.Parent Is trvCPTAssociation.Nodes(0) Then
                targetnode = targetnode1
            Else
                targetnode = targetnode1.Parent
            End If

            Dim str As String
            str = mynode.Key
            Dim mytragetnode As myTreeNode
            Dim Templatenode As myTreeNode

            Templatenode = mynode.Clone
            Templatenode.Key = mynode.Key
            Templatenode.Text = mynode.Text

            'if selected category is cpt, add node to cpt child node 
            'in trvCPTAssociates

            For Each mytragetnode In targetnode.Nodes
                If mytragetnode.Key.ToString().Trim() = str Then
                    Exit Sub
                End If
            Next
            Templatenode.ImageIndex = 5
            Templatenode.SelectedImageIndex = 5
            targetnode.Nodes.Add(Templatenode)
            'if selected category is Drugs, add node to Drugs child node 
            'in trICD9Associates
            mynode.EnsureVisible()
            trvCPTAssociation.ExpandAll()
            trvCPTAssociation.SelectedNode = mynode
            'trvTemplate.SelectedNode = trvTemplate.Nodes.Item(0)
        End If
        'End If
    End Sub

    'Private Sub tblMedication_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs)
    '    Try
    '        Select Case e.Button.Text
    '            Case "&New"
    '                RefreshCPT()
    '            Case "&Save"
    '                SaveAssociation()
    '            Case "&Finish"
    '                SaveAssociation()
    '                Me.Close()
    '            Case "&Close"
    '                Me.Close()
    '        End Select
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub txtsearchAssociates_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsearchAssociates.TextChanged
        Try
            '''''''''''''''''''####################''''''''''''''''''''''''''
            '''''Code lines below are added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
            Dim strSearchDetails As String
            If Trim(txtsearchAssociates.Text) <> "" Then
                strSearchDetails = Replace(txtsearchAssociates.Text, "'", "''")
                strSearchDetails = Replace(strSearchDetails, "[", "") & ""
                strSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strSearchDetails)
            Else
                strSearchDetails = ""
            End If
            '''''''''''''''''''####################''''''''''''''''''''''''''

            'sarika 26th sept 07
            'implement the instring search 

            If txtsearchAssociates.Tag <> Trim(strSearchDetails) Then
                ' If btnAllDrugs.Dock = DockStyle.Top Then

                AddTemplate(Trim(strSearchDetails), dtTemplate)


                'ElseIf btnClinicalDrugs.Dock = DockStyle.Top Then
                '    AddDrugs(Trim(txtsearchCPT.Text))
                'ElseIf btnFreqDrugs.Dock = DockStyle.Top Then
                '    AddDrugs(Trim(txtsearchCPT.Text))
                'End If
                'If Len(Trim(txtsearchDrug.Text)) = 1 Then
                txtsearchAssociates.Tag = Trim(strSearchDetails)
                txtsearchAssociates.Focus()
                'End If
            End If
            Exit Sub
            '----------------------------------------------------
            'SLR : 8/5/2014: Code review: What is the purpose of following code : i commented ?


            'Dim mychildnode As myTreeNode
            ''child node collection
            'For Each mychildnode In trvTemplate.Nodes.Item(0).Nodes
            '    'compare selected node text and entered text
            '    Dim str As String
            '    str = Mid(UCase(Trim(mychildnode.Tag)), 1, Len(UCase(Trim(strSearchDetails))))
            '    If str = UCase(Trim(strSearchDetails)) Then
            '        '*************code added by sagar for showing the selected drug at the top on 4 july 2007
            '        trvTemplate.SelectedNode = trvTemplate.SelectedNode.LastNode
            '        '*************
            '        trvTemplate.SelectedNode = mychildnode
            '        txtsearchAssociates.Focus()
            '        Exit Sub
            '    End If
            'Next
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "ICD9 Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub AddTemplate(ByVal strsearch As String, ByVal dt As DataTable)
        Dim newNode As New TreeNode
        Dim objMyTreeView As myTreeNode
        Dim objTemplateGallery As New clsTemplateGallery
        ' Dim objCategory As myTreeNode
        ' Dim objTemplate As myTreeNode
        Dim i As Integer
        '  Dim j As Integer

        Try
            Dim tdt As DataTable
            'For i = 0 To dt.Rows.Count - 1
            Dim dv As New DataView(dt)

            'nTemplateID,sTemplateName,sDescription

            dv.RowFilter = dt.Columns(1).ToString() & " Like '%" & strsearch & "%'"

            ' tdt = New DataTable
            tdt = dv.ToTable

            'add the nodes to treenode
            trvTemplate.BeginUpdate()
            trvTemplate.Visible = False

            trvTemplate.Nodes.Clear()
            ' trvTemplate.Nodes(0).Nodes.Clear()

            objMyTreeView = New myTreeNode("Template", 0)
            objMyTreeView.ImageIndex = 2  '' Category ICon
            objMyTreeView.SelectedImageIndex = 2 '' Category ICon
            trvTemplate.Nodes.Add(objMyTreeView)
            ' Dim ValueMember As Int64
            ' Dim DisplayMember As String

            For i = 0 To tdt.Rows.Count - 1
               

                Dim rootnode As myTreeNode
                rootnode = Searchnode(CType(tdt.Rows(i)(2), String))
                If IsNothing(rootnode) Then
                    Dim specialtynode As myTreeNode
                    specialtynode = New myTreeNode(CType(tdt.Rows(i)(2), String), -1)

                    specialtynode.ImageIndex = 3
                    specialtynode.SelectedImageIndex = 3
                    trvTemplate.Nodes.Item(0).Nodes.Add(specialtynode)
                    rootnode = specialtynode
                End If
                Dim myNode As myTreeNode

                myNode = New myTreeNode(tdt.Rows(i)(1), CType(tdt.Rows(i)(0), Long))
                myNode.ImageIndex = 5
                myNode.SelectedImageIndex = 5
                rootnode.Nodes.Add(myNode)
                rootnode.Expand()
            Next


            'For i = 0 To tdt.Rows.Count - 1
            '    'Dim ValueMember As Int64
            '    'Dim DisplayMember As String
            '    ValueMember = tdt.Rows(i)(0)
            '    DisplayMember = tdt.Rows(i)(1)
            '    objCategory = New myTreeNode(DisplayMember, ValueMember)
            '    objCategory.ImageIndex = 3 '''' Template ICon
            '    objCategory.SelectedImageIndex = 3 '''' Template ICon
            '    objMyTreeView.Nodes.Add(objCategory)

            '    Dim dvTemplate As DataView = objTemplateGallery.GetAllTemplateGallery(ValueMember)
            '    For j = 0 To dvTemplate.Table.Rows.Count - 1
            '        ''Dim ValueMember As Int64
            '        ''Dim DisplayMember As String
            '        ValueMember = dvTemplate.Table.Rows(j)(0)
            '        DisplayMember = dvTemplate.Table.Rows(j)(1)
            '        objTemplate = New myTreeNode(DisplayMember, ValueMember)
            '        objTemplate.ImageIndex = 5 '''' Play ICon
            '        objTemplate.SelectedImageIndex = 5 '''' Play ICon
            '        objCategory.Nodes.Add(objTemplate)
            '        objCategory.EnsureVisible()
            '        'objCategory.ExpandAll()
            '    Next
            'Next
            trvTemplate.Visible = True
            trvTemplate.ExpandAll()
            trvTemplate.Select()
            dv.Dispose()
            dv = Nothing
            tdt.Dispose()
            tdt = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            trvTemplate.EndUpdate()
        End Try
        objTemplateGallery.Dispose()
        objTemplateGallery = Nothing
    End Sub

    Private Function Searchnode(ByVal strspecialty As String) As myTreeNode
        Dim mynode As myTreeNode
        For Each mynode In trvTemplate.Nodes.Item(0).Nodes
            If mynode.Text = strspecialty Then
                Return mynode
                Exit For
            End If
        Next
        Return Nothing
    End Function

    Private Sub trvCPT_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvCPT.AfterSelect

    End Sub

    Private Sub rbSearch1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbSearch1.Click
        Try
            If IsNothing(dtCPTCode) = True Then
                '' Initalize Datatabel
                dtCPTCode = New DataTable
            End If

            If dtCPTCode.Rows.Count < 1 Then
                '' Populate CPT data
                '' TO Pull CPTs Order By CPTCode Pass 0
                If (IsNothing(dtCPTCode) = False) Then
                    dtCPTCode.Dispose()
                    dtCPTCode = Nothing
                End If
                dtCPTCode = objclsCPTAssociation.FillCPT(0)
            End If

            trvCPT.Hide()
            '' Clear TreeView
            trvCPT.Nodes(0).Nodes.Clear()

            Dim i As Integer
            For i = 0 To dtCPTCode.Rows.Count - 1
                Dim mychildnode As myTreeNode
                mychildnode = New myTreeNode(dtCPTCode.Rows(i)(2), dtCPTCode.Rows(i)(0), CType(dtCPTCode.Rows(i)(1), String))
                mychildnode.ImageIndex = 5
                mychildnode.SelectedImageIndex = 5
                trvCPT.Nodes(0).Nodes.Add(mychildnode)
            Next

            trvCPT.SelectedNode = trvCPT.Nodes.Item(0)
            trvCPT.ExpandAll()

            trvCPT.Show()

            txtsearchDrugs.Text = ""
            txtsearchDrugs.Focus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "CPT Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
            trvCPT.Show()
        End Try
    End Sub

    Private Sub rbSearch2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbSearch2.Click
        Try
            If IsNothing(dtCPTDesc) = True Then
                '' Initalize Datatabel
                dtCPTDesc = New DataTable
            End If

            If dtCPTDesc.Rows.Count < 1 Then
                '' Populate CPT data
                '' TO Pull CPTs Order By CPTDescription Pass 1
                If (IsNothing(dtCPTDesc) = False) Then
                    dtCPTDesc.Dispose()
                    dtCPTDesc = Nothing
                End If
                dtCPTDesc = objclsCPTAssociation.FillCPT(1)
            End If

            trvCPT.Hide()
            '' Clear TreeView
            trvCPT.Nodes(0).Nodes.Clear()
            Dim i As Integer
            For i = 0 To dtCPTDesc.Rows.Count - 1
                Dim mychildnode As myTreeNode
                mychildnode = New myTreeNode(dtCPTDesc.Rows(i)(2), dtCPTDesc.Rows(i)(0), CType(dtCPTDesc.Rows(i)(1), String))
                mychildnode.ImageIndex = 5
                mychildnode.SelectedImageIndex = 5
                trvCPT.Nodes(0).Nodes.Add(mychildnode)
            Next

            trvCPT.SelectedNode = trvCPT.Nodes.Item(0)
            trvCPT.ExpandAll()

            trvCPT.Show()
            txtsearchDrugs.Text = ""
            txtsearchDrugs.Focus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "CPT Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
            trvCPT.Show()
        End Try
    End Sub

    Private Sub cntRemoveCPT_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub mnuRemoveCPT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRemoveCPT.Click
        Try
            If Not trvCPTAssociation.SelectedNode Is trvCPTAssociation.Nodes.Item(0) Then
                '''''if Selected Node is not Root Node 
                'Dim _TempNode As myTreeNode
                '_TempNode = CType(trvCPTAssociation.SelectedNode, myTreeNode)
                ''trvCPTAssociation.SelectedNode.Remove()
                ''trvCPTAssociation.Nodes.Insert(_TempNode.Index, _TempNode)
                'For i As Integer = 0 To trvCPTAssociation.SelectedNode.GetNodeCount(False) - 1
                '    'If _TempNode.NextNode Is trvCPTAssociation.Parent Then
                '    '    Exit Sub
                '    'End If
                '    trvCPTAssociation.Nodes.RemoveAt(trvCPTAssociation.SelectedNode.Index + i)  'Nodes(i).Remove()
                'Next
                trvCPTAssociation.SelectedNode.Nodes.Clear()
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "CPT Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub tblMedication_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblMedication.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "New"
                    RefreshCPT()
                Case "Save"
                    SaveAssociation()
                Case "Finish"
                    SaveAssociation()
                    ' Me.Close()
                    ' gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                    'SLR: Save Association has a close call..
                Case "Close"
                    'Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    'sarika 26th sept 07
    Private Sub AddCPT(ByVal strsearch As String, ByVal dt As DataTable)
        Try
            Dim i As Integer
            Dim tdt As DataTable
            'For i = 0 To dt.Rows.Count - 1
            Dim dv As New DataView(dt)

            If rbSearch2.Checked = True Then
                ''description 
                dv.RowFilter = "sDescription Like '%" & strsearch & "%'"
            Else
                ''code
                dv.RowFilter = "CPTCode Like '%" & strsearch & "%'"
            End If
            '       tdt = New DataTable
            tdt = dv.ToTable

            'add the nodes to treenode
            trvCPT.BeginUpdate()
            trvCPT.Visible = False
            If trvCPT.GetNodeCount(False) > 0 Then
                trvCPT.Nodes.Item(0).Remove()
                Dim rootnode As TreeNode
                rootnode = New myTreeNode("CPT", -1)
                rootnode.ImageIndex = 1
                rootnode.SelectedImageIndex = 1
                trvCPT.Nodes.Add(rootnode)
            End If

            'fill the treeview with the dv
            If Not tdt Is Nothing Then
                trvCPT.Visible = True
                For i = 0 To tdt.Rows.Count - 1
                    Dim mychildnode As myTreeNode
                    mychildnode = New myTreeNode(tdt.Rows(i)(2), tdt.Rows(i)(0), CType(tdt.Rows(i)(1), String))
                    mychildnode.ImageIndex = 5
                    mychildnode.SelectedImageIndex = 5
                    trvCPT.Nodes.Item(0).Nodes.Add(mychildnode)
                Next
                If tdt.Rows.Count > 0 Then
                    trvCPT.SelectedNode = trvCPT.Nodes.Item(0)
                End If
                tdt.Dispose()
                tdt = Nothing
            Else
            End If
            trvCPT.ExpandAll()
            dv.Dispose()
            dv = Nothing
        Catch ex As Exception
            'Dim objex As New gloUserControlLibrary.gloUserControlExceptions
            'objex.ErrorMessage = ""
            'Throw objex
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            trvCPT.EndUpdate()
        End Try

    End Sub
    '----------------------------------------------------------------------------------------------
    '''''Following code lines are addded by Anil 0n 27/09/07 at 11:30 a.m.
    '''''This code clears the search textboxes, gets the focus on the root of theTreeView  on click of Refresh button.
    Private Sub tblRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblRefresh.Click
        txtsearchAssociates.Clear()
        txtsearchDrugs.Clear()
        trvTemplate.CollapseAll()
        FillTemplate()
        trvCPTAssociation.CollapseAll()
        trvCPTAssociation.Focus()
        trvCPTAssociation.ExpandAll()
        trvTemplate.Focus()
        trvCPT.CollapseAll()
        trvCPT.Focus()
        trvCPT.ExpandAll()
        rbSearch1.Checked = False
        rbSearch2.Checked = True
        '''''upto here the code is added
    End Sub

    Private Sub rbSearch1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSearch1.CheckedChanged
        If rbSearch1.Checked = True Then
            rbSearch1.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbSearch1.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub

    Private Sub rbSearch2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSearch2.CheckedChanged
        If rbSearch2.Checked = True Then
            rbSearch2.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbSearch2.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If

    End Sub

    '' Mayuri- Event handled on 20090910 
    Private Sub GloUC_trvCPT_NodeAdded(ByVal ChildNode As gloUserControlLibrary.myTreeNode) Handles GloUC_trvCPT.NodeAdded
        Try
            Dim dtAssociation As DataTable
            '' To Get Already Associated Template with Selected CPT
            dtAssociation = objclsCPTAssociation.FetchCPTforUpdate(ChildNode.Tag)
            '' If Association found then change the Image of Treenode 
            If Not IsNothing(dtAssociation) Then
                If dtAssociation.Rows.Count > 0 Then
                    ChildNode.ImageIndex = 4
                    ChildNode.SelectedImageIndex = 4
                End If
                dtAssociation.Dispose()
                dtAssociation = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''

    Private Sub GloUC_trvCPT_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvCPT.NodeMouseDoubleClick
        Try
            Dim oNode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)

           
            If Not IsNothing(oNode) Then
                Dim mynode As New myTreeNode
                mynode.Key = oNode.ID
                mynode.Text = oNode.Text
                AddNode(mynode)
                mynode.Dispose()
                mynode = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "CPT Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvCPT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvCPT.KeyPress
        If e.KeyChar = ChrW(13) Then
            Try
                Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvCPT.SelectedNode, gloUserControlLibrary.myTreeNode)

               
                If Not IsNothing(oNode) Then
                    Dim mynode As New myTreeNode
                    mynode.Key = oNode.ID
                    mynode.Text = oNode.Text
                    AddNode(mynode)
                    mynode.Dispose()
                    mynode = Nothing
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, "CPT Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub GloUC_trvTemplate_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvTemplate.NodeMouseDoubleClick
        Try

            Dim targetnode1 As myTreeNode
            targetnode1 = CType(trvCPTAssociation.SelectedNode, myTreeNode)

            Dim oNode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)
           
            If Not IsNothing(targetnode1) AndAlso Not IsNothing(oNode) Then
                Dim mynode As New myTreeNode
                mynode.Key = oNode.ID
                mynode.Text = oNode.Text
                AddAssociates(mynode, targetnode1)
                mynode.Dispose()
                mynode = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "CPT Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvTemplate_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvTemplate.KeyPress

        Try
            If e.KeyChar = ChrW(13) Then

                Dim targetnode1 As myTreeNode
                targetnode1 = CType(trvCPTAssociation.SelectedNode, myTreeNode)

                Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvTemplate.SelectedNode, gloUserControlLibrary.myTreeNode)
              
                If Not IsNothing(targetnode1) AndAlso Not IsNothing(oNode) Then
                    Dim mynode As New myTreeNode
                    mynode.Key = oNode.ID
                    mynode.Text = oNode.Text
                    AddAssociates(mynode, targetnode1)
                    mynode.Dispose()
                    mynode = Nothing
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "CPT Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub frmCPTAssociation_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.Activate()
        Catch ex As Exception

        End Try

    End Sub
    'Code Start added by kanchan on 20120102 for gloCommunity integration
    Private Sub ts_gloCommunityDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_gloCommunityDownload.Click
        If CheckUser() = False Then ''Added for fixed Bug # : 35658 on 20120904
            Dim FrmgloCommunityViewData As New gloCommunity.Forms.gloCommunityViewData("Formgallery", "Download")
            'code added by seema on 20120221 to prevent opening of multiple windows
            If gloCommunity.Classes.clsGeneral.getInstance(FrmgloCommunityViewData.Name, FrmgloCommunityViewData.Text) = False Then
                Try

                    With FrmgloCommunityViewData
                        .MdiParent = Application.OpenForms("MainMenu")
                        .WindowState = FormWindowState.Maximized
                        .Show()
                    End With

                Catch objErr As Exception
                    MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
    End Sub

    ''Added for fixed Bug # : 35658 on 20120904
    Private Function CheckUser() As Boolean
        Dim oCommunity As gloCommunity.Classes.clsgloCommunityUsers = Nothing
        Dim _blnUserCheck As Boolean = False
        Try
            oCommunity = New gloCommunity.Classes.clsgloCommunityUsers()
            _blnUserCheck = oCommunity.CheckAuthentication()
        Catch ex As Exception
        Finally
            If Not IsNothing(oCommunity) Then
                oCommunity = Nothing
            End If
        End Try
        Return _blnUserCheck
    End Function
    ''End
End Class

