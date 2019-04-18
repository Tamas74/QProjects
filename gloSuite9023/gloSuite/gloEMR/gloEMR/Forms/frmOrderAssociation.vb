Option Compare Text
Public Class frmOrderAssociation
    Inherits System.Windows.Forms.Form
    Dim objSmartOrderDBLayer As ClsSmartorderDBLayer

    Private Const strSortByCode As String = "CODE"
    Private Const strSortByDesc As String = "DESC"
    Dim _IsLabs As Boolean = False
    ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101012
    Private bParentTrigger As Boolean = True
    Private bChildTrigger As Boolean = True
    ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101012
    ' Dim dtOrderbyCode As DataTable
    '  Dim dtOrderbyDesc As DataTable
    Dim dtCPT As New DataTable
    'sarika 26th sept 07
    Dim dtAssociates As New DataTable
    Dim tooltipnew As ToolTip = Nothing
    '-----------------------------------------------
    Dim dt_Orderset As DataTable
    Dim dvTemplate As DataView
    Public IsOpenfrmSmartOrder As Boolean = False
    Public OrderNode As myTreeNode
    Public arrSelectedNodes As ArrayList
    Dim strParentToAssociate As String = "Orders & Results"

#Region " Windows Controls "
    Friend WithEvents tblOrderAss As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblFinish As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtsearchAssociates As System.Windows.Forms.TextBox
    Friend WithEvents tblRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents pnlRightSearch As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents btnDrugs As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents pnlbtnTemplates As System.Windows.Forms.Panel
    Private WithEvents Label36 As System.Windows.Forms.Label
    Private WithEvents Label37 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnRadiology As System.Windows.Forms.Panel
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label33 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents pnlbtnDrugs As System.Windows.Forms.Panel
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents pnltrAssociates As System.Windows.Forms.Panel
    Friend WithEvents pnlbtnLabs As System.Windows.Forms.Panel
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents GloUC_trvAssociates As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents GloUC_trvOrderSet As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents cmnuAddOrderSet As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlbtnFlowsheet As System.Windows.Forms.Panel
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnFlowsheet As System.Windows.Forms.Button
    Friend WithEvents ts_gloCommunityDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
#End Region

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
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Dim cntcontextmenu As ContextMenu() = {cntICD9Association}
            Dim cntcontextmenustrip As ContextMenuStrip() = {cmnuAddOrderSet}

            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(cntcontextmenu)
                gloGlobal.cEventHelper.DisposeContextMenu(cntcontextmenu)
            Catch
            End Try

            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(cntcontextmenustrip)
                gloGlobal.cEventHelper.DisposeContextMenuStrip(cntcontextmenustrip)
            Catch
            End Try


            If (IsNothing(tooltipnew) = False) Then
                tooltipnew.Dispose()
                tooltipnew = Nothing
            End If
            If (IsNothing(objSmartOrderDBLayer) = False) Then
                objSmartOrderDBLayer.Dispose()
                objSmartOrderDBLayer = Nothing
            End If

            If (IsNothing(dtCPT) = False) Then
                dtCPT.Dispose()
                dtCPT = Nothing
            End If
            If (IsNothing(dtAssociates) = False) Then
                dtAssociates.Dispose()
                dtAssociates = Nothing
            End If
            If (IsNothing(dt_Orderset) = False) Then
                dt_Orderset.Dispose()
                dt_Orderset = Nothing
            End If
            If (IsNothing(dvTemplate) = False) Then
                dvTemplate.Dispose()
                dvTemplate = Nothing
            End If
            If (IsNothing(OrderNode) = False) Then
                OrderNode.Dispose()
                OrderNode = Nothing
            End If

            If (IsNothing(arrSelectedNodes) = False) Then
                arrSelectedNodes.Clear()
                arrSelectedNodes = Nothing
            End If

        End If
        '''''
        frm = Nothing
        '''''
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents txtsearchOrderset As System.Windows.Forms.TextBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents trvOrderset As System.Windows.Forms.TreeView
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents btnTemplates As System.Windows.Forms.Button
    Friend WithEvents btnLabs As System.Windows.Forms.Button
    Friend WithEvents cntICD9Association As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuDeleteICD9Item As System.Windows.Forms.MenuItem
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents trOrderAssociation As System.Windows.Forms.TreeView
    Friend WithEvents btnRadiology As System.Windows.Forms.Button
    Friend WithEvents trAssociates As System.Windows.Forms.TreeView
    Friend WithEvents imgTreeView As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOrderAssociation))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.GloUC_trvOrderSet = New gloUserControlLibrary.gloUC_TreeView()
        Me.imgTreeView = New System.Windows.Forms.ImageList(Me.components)
        Me.trvOrderset = New System.Windows.Forms.TreeView()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.txtsearchOrderset = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.pnltrAssociates = New System.Windows.Forms.Panel()
        Me.GloUC_trvAssociates = New gloUserControlLibrary.gloUC_TreeView()
        Me.trAssociates = New System.Windows.Forms.TreeView()
        Me.pnlbtnDrugs = New System.Windows.Forms.Panel()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.btnDrugs = New System.Windows.Forms.Button()
        Me.pnlbtnRadiology = New System.Windows.Forms.Panel()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.btnRadiology = New System.Windows.Forms.Button()
        Me.pnlRightSearch = New System.Windows.Forms.Panel()
        Me.txtsearchAssociates = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlbtnTemplates = New System.Windows.Forms.Panel()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.btnTemplates = New System.Windows.Forms.Button()
        Me.pnlbtnLabs = New System.Windows.Forms.Panel()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.btnLabs = New System.Windows.Forms.Button()
        Me.pnlbtnFlowsheet = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnFlowsheet = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.trOrderAssociation = New System.Windows.Forms.TreeView()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.tblOrderAss = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblNew = New System.Windows.Forms.ToolStripButton()
        Me.tblFinish = New System.Windows.Forms.ToolStripButton()
        Me.tblSave = New System.Windows.Forms.ToolStripButton()
        Me.ts_gloCommunityDownload = New System.Windows.Forms.ToolStripButton()
        Me.tblRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tblClose = New System.Windows.Forms.ToolStripButton()
        Me.cntICD9Association = New System.Windows.Forms.ContextMenu()
        Me.mnuDeleteICD9Item = New System.Windows.Forms.MenuItem()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.cmnuAddOrderSet = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel10.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnltrAssociates.SuspendLayout()
        Me.pnlbtnDrugs.SuspendLayout()
        Me.pnlbtnRadiology.SuspendLayout()
        Me.pnlRightSearch.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlbtnTemplates.SuspendLayout()
        Me.pnlbtnLabs.SuspendLayout()
        Me.pnlbtnFlowsheet.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.tblOrderAss.SuspendLayout()
        Me.cmnuAddOrderSet.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel9)
        Me.Panel1.Controls.Add(Me.Panel8)
        Me.Panel1.Controls.Add(Me.Panel10)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel1.Size = New System.Drawing.Size(230, 584)
        Me.Panel1.TabIndex = 1
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.GloUC_trvOrderSet)
        Me.Panel9.Controls.Add(Me.trvOrderset)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel9.Location = New System.Drawing.Point(0, 33)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(230, 551)
        Me.Panel9.TabIndex = 1
        '
        'GloUC_trvOrderSet
        '
        Me.GloUC_trvOrderSet.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvOrderSet.CheckBoxes = False
        Me.GloUC_trvOrderSet.CodeMember = Nothing
        Me.GloUC_trvOrderSet.Comment = Nothing
        Me.GloUC_trvOrderSet.ConceptID = Nothing
        Me.GloUC_trvOrderSet.CPT = Nothing
        Me.GloUC_trvOrderSet.mpidmember = Nothing
        Me.GloUC_trvOrderSet.DescriptionMember = Nothing
        Me.GloUC_trvOrderSet.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
        Me.GloUC_trvOrderSet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvOrderSet.DrugFlag = CType(16, Short)
        Me.GloUC_trvOrderSet.DrugFormMember = Nothing
        Me.GloUC_trvOrderSet.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvOrderSet.DurationMember = Nothing
        Me.GloUC_trvOrderSet.FrequencyMember = Nothing
        Me.GloUC_trvOrderSet.HistoryType = Nothing
        Me.GloUC_trvOrderSet.ICD9 = Nothing
        Me.GloUC_trvOrderSet.ImageIndex = 0
        Me.GloUC_trvOrderSet.ImageList = Me.imgTreeView
        Me.GloUC_trvOrderSet.ImageObject = Nothing
        Me.GloUC_trvOrderSet.Indicator = Nothing
        Me.GloUC_trvOrderSet.IsDrug = False
        Me.GloUC_trvOrderSet.IsNarcoticsMember = Nothing
        Me.GloUC_trvOrderSet.IsSystemCategory = Nothing
        Me.GloUC_trvOrderSet.Location = New System.Drawing.Point(0, 0)
        Me.GloUC_trvOrderSet.MaximumNodes = 1000
        Me.GloUC_trvOrderSet.Name = "GloUC_trvOrderSet"
        Me.GloUC_trvOrderSet.NDCCodeMember = Nothing
        Me.GloUC_trvOrderSet.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.GloUC_trvOrderSet.ParentImageIndex = 0
        Me.GloUC_trvOrderSet.ParentMember = Nothing
        Me.GloUC_trvOrderSet.RouteMember = Nothing
        Me.GloUC_trvOrderSet.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvOrderSet.SearchBox = True
        Me.GloUC_trvOrderSet.SearchText = Nothing
        Me.GloUC_trvOrderSet.SelectedImageIndex = 0
        Me.GloUC_trvOrderSet.SelectedNode = Nothing
        Me.GloUC_trvOrderSet.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvOrderSet.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvOrderSet.SelectedParentImageIndex = 0
        Me.GloUC_trvOrderSet.Size = New System.Drawing.Size(230, 551)
        Me.GloUC_trvOrderSet.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvOrderSet.TabIndex = 27
        Me.GloUC_trvOrderSet.Tag = Nothing
        Me.GloUC_trvOrderSet.UnitMember = Nothing
        Me.GloUC_trvOrderSet.ValueMember = Nothing
        '
        'imgTreeView
        '
        Me.imgTreeView.ImageStream = CType(resources.GetObject("imgTreeView.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTreeView.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeView.Images.SetKeyName(0, "Bullet06.ico")
        Me.imgTreeView.Images.SetKeyName(1, "Lab.ico")
        Me.imgTreeView.Images.SetKeyName(2, "Radiology_01.ico")
        Me.imgTreeView.Images.SetKeyName(3, "All Template.ico")
        Me.imgTreeView.Images.SetKeyName(4, "Small Arrow.ico")
        Me.imgTreeView.Images.SetKeyName(5, "Order Set.ico")
        Me.imgTreeView.Images.SetKeyName(6, "Orders  Association.ico")
        Me.imgTreeView.Images.SetKeyName(7, "SubTemplate.ico")
        Me.imgTreeView.Images.SetKeyName(8, "Drugs.ico")
        Me.imgTreeView.Images.SetKeyName(9, "FLow sheet.ico")
        '
        'trvOrderset
        '
        Me.trvOrderset.BackColor = System.Drawing.Color.White
        Me.trvOrderset.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvOrderset.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvOrderset.ForeColor = System.Drawing.Color.Black
        Me.trvOrderset.HideSelection = False
        Me.trvOrderset.ImageIndex = 7
        Me.trvOrderset.ImageList = Me.imgTreeView
        Me.trvOrderset.ItemHeight = 21
        Me.trvOrderset.Location = New System.Drawing.Point(3, 0)
        Me.trvOrderset.Name = "trvOrderset"
        Me.trvOrderset.SelectedImageIndex = 7
        Me.trvOrderset.ShowLines = False
        Me.trvOrderset.Size = New System.Drawing.Size(227, 520)
        Me.trvOrderset.TabIndex = 1
        Me.trvOrderset.Visible = False
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.txtsearchOrderset)
        Me.Panel8.Controls.Add(Me.Label20)
        Me.Panel8.Controls.Add(Me.Label21)
        Me.Panel8.Controls.Add(Me.PictureBox1)
        Me.Panel8.Controls.Add(Me.Label9)
        Me.Panel8.Controls.Add(Me.Label11)
        Me.Panel8.Controls.Add(Me.Label12)
        Me.Panel8.Controls.Add(Me.Label13)
        Me.Panel8.Location = New System.Drawing.Point(0, 33)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel8.Size = New System.Drawing.Size(230, 28)
        Me.Panel8.TabIndex = 0
        Me.Panel8.Visible = False
        '
        'txtsearchOrderset
        '
        Me.txtsearchOrderset.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearchOrderset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsearchOrderset.ForeColor = System.Drawing.Color.Black
        Me.txtsearchOrderset.Location = New System.Drawing.Point(32, 5)
        Me.txtsearchOrderset.Multiline = True
        Me.txtsearchOrderset.Name = "txtsearchOrderset"
        Me.txtsearchOrderset.Size = New System.Drawing.Size(197, 17)
        Me.txtsearchOrderset.TabIndex = 0
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.White
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(32, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(197, 4)
        Me.Label20.TabIndex = 0
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Location = New System.Drawing.Point(32, 22)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(197, 2)
        Me.Label21.TabIndex = 38
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(4, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 23)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.Location = New System.Drawing.Point(4, 24)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(225, 1)
        Me.Label9.TabIndex = 42
        Me.Label9.Text = "label2"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(3, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 24)
        Me.Label11.TabIndex = 41
        Me.Label11.Text = "label4"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(229, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 24)
        Me.Label12.TabIndex = 40
        Me.Label12.Text = "label3"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(3, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(227, 1)
        Me.Label13.TabIndex = 39
        Me.Label13.Text = "label1"
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.Panel2)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(0, 3)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel10.Size = New System.Drawing.Size(230, 30)
        Me.Panel10.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label22)
        Me.Panel2.Controls.Add(Me.Label23)
        Me.Panel2.Controls.Add(Me.Label24)
        Me.Panel2.Controls.Add(Me.Label25)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(3, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(227, 27)
        Me.Panel2.TabIndex = 0
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(1, 26)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(225, 1)
        Me.Label22.TabIndex = 12
        Me.Label22.Text = "label2"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 26)
        Me.Label23.TabIndex = 11
        Me.Label23.Text = "label4"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label24.Location = New System.Drawing.Point(226, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 26)
        Me.Label24.TabIndex = 10
        Me.Label24.Text = "label3"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(0, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(227, 1)
        Me.Label25.TabIndex = 9
        Me.Label25.Text = "label1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(227, 27)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = " &Orders Set"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel7)
        Me.Panel3.Controls.Add(Me.pnlbtnFlowsheet)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(798, 54)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel3.Size = New System.Drawing.Size(230, 584)
        Me.Panel3.TabIndex = 3
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.pnltrAssociates)
        Me.Panel7.Controls.Add(Me.pnlbtnDrugs)
        Me.Panel7.Controls.Add(Me.pnlbtnRadiology)
        Me.Panel7.Controls.Add(Me.pnlRightSearch)
        Me.Panel7.Controls.Add(Me.pnlbtnTemplates)
        Me.Panel7.Controls.Add(Me.pnlbtnLabs)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 3)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(230, 551)
        Me.Panel7.TabIndex = 1
        '
        'pnltrAssociates
        '
        Me.pnltrAssociates.Controls.Add(Me.GloUC_trvAssociates)
        Me.pnltrAssociates.Controls.Add(Me.trAssociates)
        Me.pnltrAssociates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrAssociates.Location = New System.Drawing.Point(0, 30)
        Me.pnltrAssociates.Name = "pnltrAssociates"
        Me.pnltrAssociates.Size = New System.Drawing.Size(230, 431)
        Me.pnltrAssociates.TabIndex = 1
        '
        'GloUC_trvAssociates
        '
        Me.GloUC_trvAssociates.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvAssociates.CheckBoxes = False
        Me.GloUC_trvAssociates.CodeMember = Nothing
        Me.GloUC_trvAssociates.Comment = Nothing
        Me.GloUC_trvAssociates.ConceptID = Nothing
        Me.GloUC_trvAssociates.CPT = Nothing
        Me.GloUC_trvAssociates.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GloUC_trvAssociates.mpidmember = Nothing
        Me.GloUC_trvAssociates.DescriptionMember = Nothing
        Me.GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
        Me.GloUC_trvAssociates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvAssociates.DrugFlag = CType(16, Short)
        Me.GloUC_trvAssociates.DrugFormMember = Nothing
        Me.GloUC_trvAssociates.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvAssociates.DurationMember = Nothing
        Me.GloUC_trvAssociates.FrequencyMember = Nothing
        Me.GloUC_trvAssociates.HistoryType = Nothing
        Me.GloUC_trvAssociates.ICD9 = Nothing
        Me.GloUC_trvAssociates.ImageIndex = 0
        Me.GloUC_trvAssociates.ImageList = Me.imgTreeView
        Me.GloUC_trvAssociates.ImageObject = Nothing
        Me.GloUC_trvAssociates.Indicator = Nothing
        Me.GloUC_trvAssociates.IsDrug = False
        Me.GloUC_trvAssociates.IsNarcoticsMember = Nothing
        Me.GloUC_trvAssociates.IsSystemCategory = Nothing
        Me.GloUC_trvAssociates.Location = New System.Drawing.Point(0, 0)
        Me.GloUC_trvAssociates.MaximumNodes = 1000
        Me.GloUC_trvAssociates.Name = "GloUC_trvAssociates"
        Me.GloUC_trvAssociates.NDCCodeMember = Nothing
        Me.GloUC_trvAssociates.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.GloUC_trvAssociates.ParentImageIndex = 0
        Me.GloUC_trvAssociates.ParentMember = Nothing
        Me.GloUC_trvAssociates.RouteMember = Nothing
        Me.GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvAssociates.SearchBox = True
        Me.GloUC_trvAssociates.SearchText = Nothing
        Me.GloUC_trvAssociates.SelectedImageIndex = 0
        Me.GloUC_trvAssociates.SelectedNode = Nothing
        Me.GloUC_trvAssociates.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvAssociates.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvAssociates.SelectedParentImageIndex = 0
        Me.GloUC_trvAssociates.Size = New System.Drawing.Size(230, 431)
        Me.GloUC_trvAssociates.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvAssociates.TabIndex = 26
        Me.GloUC_trvAssociates.Tag = Nothing
        Me.GloUC_trvAssociates.UnitMember = Nothing
        Me.GloUC_trvAssociates.ValueMember = Nothing
        '
        'trAssociates
        '
        Me.trAssociates.BackColor = System.Drawing.Color.White
        Me.trAssociates.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trAssociates.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trAssociates.ForeColor = System.Drawing.Color.Black
        Me.trAssociates.HideSelection = False
        Me.trAssociates.ImageIndex = 7
        Me.trAssociates.ImageList = Me.imgTreeView
        Me.trAssociates.ItemHeight = 21
        Me.trAssociates.Location = New System.Drawing.Point(0, 0)
        Me.trAssociates.Name = "trAssociates"
        Me.trAssociates.SelectedImageIndex = 7
        Me.trAssociates.ShowLines = False
        Me.trAssociates.Size = New System.Drawing.Size(227, 428)
        Me.trAssociates.TabIndex = 4
        Me.trAssociates.Visible = False
        '
        'pnlbtnDrugs
        '
        Me.pnlbtnDrugs.Controls.Add(Me.Label28)
        Me.pnlbtnDrugs.Controls.Add(Me.Label29)
        Me.pnlbtnDrugs.Controls.Add(Me.Label30)
        Me.pnlbtnDrugs.Controls.Add(Me.Label31)
        Me.pnlbtnDrugs.Controls.Add(Me.btnDrugs)
        Me.pnlbtnDrugs.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnDrugs.Location = New System.Drawing.Point(0, 461)
        Me.pnlbtnDrugs.Name = "pnlbtnDrugs"
        Me.pnlbtnDrugs.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnDrugs.Size = New System.Drawing.Size(230, 30)
        Me.pnlbtnDrugs.TabIndex = 2
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label28.Location = New System.Drawing.Point(1, 26)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(225, 1)
        Me.Label28.TabIndex = 12
        Me.Label28.Text = "label2"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(0, 1)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 26)
        Me.Label29.TabIndex = 11
        Me.Label29.Text = "label4"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label30.Location = New System.Drawing.Point(226, 1)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(1, 26)
        Me.Label30.TabIndex = 10
        Me.Label30.Text = "label3"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(0, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(227, 1)
        Me.Label31.TabIndex = 9
        Me.Label31.Text = "label1"
        '
        'btnDrugs
        '
        Me.btnDrugs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnDrugs.BackgroundImage = CType(resources.GetObject("btnDrugs.BackgroundImage"), System.Drawing.Image)
        Me.btnDrugs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDrugs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnDrugs.FlatAppearance.BorderSize = 0
        Me.btnDrugs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDrugs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDrugs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDrugs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnDrugs.Location = New System.Drawing.Point(0, 0)
        Me.btnDrugs.Name = "btnDrugs"
        Me.btnDrugs.Size = New System.Drawing.Size(227, 27)
        Me.btnDrugs.TabIndex = 5
        Me.btnDrugs.Tag = "0"
        Me.btnDrugs.Text = "&Drugs"
        Me.btnDrugs.UseVisualStyleBackColor = False
        '
        'pnlbtnRadiology
        '
        Me.pnlbtnRadiology.Controls.Add(Me.Label32)
        Me.pnlbtnRadiology.Controls.Add(Me.Label33)
        Me.pnlbtnRadiology.Controls.Add(Me.Label34)
        Me.pnlbtnRadiology.Controls.Add(Me.Label35)
        Me.pnlbtnRadiology.Controls.Add(Me.btnRadiology)
        Me.pnlbtnRadiology.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnRadiology.Location = New System.Drawing.Point(0, 491)
        Me.pnlbtnRadiology.Name = "pnlbtnRadiology"
        Me.pnlbtnRadiology.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnRadiology.Size = New System.Drawing.Size(230, 30)
        Me.pnlbtnRadiology.TabIndex = 3
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label32.Location = New System.Drawing.Point(1, 26)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(225, 1)
        Me.Label32.TabIndex = 12
        Me.Label32.Text = "label2"
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(0, 1)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(1, 26)
        Me.Label33.TabIndex = 11
        Me.Label33.Text = "label4"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label34.Location = New System.Drawing.Point(226, 1)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(1, 26)
        Me.Label34.TabIndex = 10
        Me.Label34.Text = "label3"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(0, 0)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(227, 1)
        Me.Label35.TabIndex = 9
        Me.Label35.Text = "label1"
        '
        'btnRadiology
        '
        Me.btnRadiology.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnRadiology.BackgroundImage = CType(resources.GetObject("btnRadiology.BackgroundImage"), System.Drawing.Image)
        Me.btnRadiology.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRadiology.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRadiology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnRadiology.FlatAppearance.BorderSize = 0
        Me.btnRadiology.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRadiology.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRadiology.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRadiology.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRadiology.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnRadiology.Location = New System.Drawing.Point(0, 0)
        Me.btnRadiology.Name = "btnRadiology"
        Me.btnRadiology.Size = New System.Drawing.Size(227, 27)
        Me.btnRadiology.TabIndex = 6
        Me.btnRadiology.Tag = "0"
        Me.btnRadiology.Text = "&Order Templates"
        Me.btnRadiology.UseVisualStyleBackColor = False
        '
        'pnlRightSearch
        '
        Me.pnlRightSearch.Controls.Add(Me.txtsearchAssociates)
        Me.pnlRightSearch.Controls.Add(Me.Label2)
        Me.pnlRightSearch.Controls.Add(Me.Label3)
        Me.pnlRightSearch.Controls.Add(Me.PictureBox2)
        Me.pnlRightSearch.Controls.Add(Me.Label5)
        Me.pnlRightSearch.Controls.Add(Me.Label6)
        Me.pnlRightSearch.Controls.Add(Me.Label7)
        Me.pnlRightSearch.Controls.Add(Me.Label8)
        Me.pnlRightSearch.Location = New System.Drawing.Point(0, 30)
        Me.pnlRightSearch.Name = "pnlRightSearch"
        Me.pnlRightSearch.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlRightSearch.Size = New System.Drawing.Size(230, 30)
        Me.pnlRightSearch.TabIndex = 0
        Me.pnlRightSearch.Visible = False
        '
        'txtsearchAssociates
        '
        Me.txtsearchAssociates.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearchAssociates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsearchAssociates.ForeColor = System.Drawing.Color.Black
        Me.txtsearchAssociates.Location = New System.Drawing.Point(29, 5)
        Me.txtsearchAssociates.Multiline = True
        Me.txtsearchAssociates.Name = "txtsearchAssociates"
        Me.txtsearchAssociates.Size = New System.Drawing.Size(197, 19)
        Me.txtsearchAssociates.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Location = New System.Drawing.Point(29, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(197, 4)
        Me.Label2.TabIndex = 37
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(29, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(197, 2)
        Me.Label3.TabIndex = 38
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.White
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(28, 25)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 9
        Me.PictureBox2.TabStop = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(1, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(225, 1)
        Me.Label5.TabIndex = 42
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 26)
        Me.Label6.TabIndex = 41
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(226, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 26)
        Me.Label7.TabIndex = 40
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(227, 1)
        Me.Label8.TabIndex = 39
        Me.Label8.Text = "label1"
        '
        'pnlbtnTemplates
        '
        Me.pnlbtnTemplates.Controls.Add(Me.Label36)
        Me.pnlbtnTemplates.Controls.Add(Me.Label37)
        Me.pnlbtnTemplates.Controls.Add(Me.Label38)
        Me.pnlbtnTemplates.Controls.Add(Me.Label39)
        Me.pnlbtnTemplates.Controls.Add(Me.btnTemplates)
        Me.pnlbtnTemplates.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnTemplates.Location = New System.Drawing.Point(0, 521)
        Me.pnlbtnTemplates.Name = "pnlbtnTemplates"
        Me.pnlbtnTemplates.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnTemplates.Size = New System.Drawing.Size(230, 30)
        Me.pnlbtnTemplates.TabIndex = 4
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label36.Location = New System.Drawing.Point(1, 26)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(225, 1)
        Me.Label36.TabIndex = 12
        Me.Label36.Text = "label2"
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(0, 1)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(1, 26)
        Me.Label37.TabIndex = 11
        Me.Label37.Text = "label4"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label38.Location = New System.Drawing.Point(226, 1)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(1, 26)
        Me.Label38.TabIndex = 10
        Me.Label38.Text = "label3"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(0, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(227, 1)
        Me.Label39.TabIndex = 9
        Me.Label39.Text = "label1"
        '
        'btnTemplates
        '
        Me.btnTemplates.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnTemplates.BackgroundImage = CType(resources.GetObject("btnTemplates.BackgroundImage"), System.Drawing.Image)
        Me.btnTemplates.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnTemplates.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTemplates.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnTemplates.FlatAppearance.BorderSize = 0
        Me.btnTemplates.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnTemplates.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnTemplates.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTemplates.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTemplates.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnTemplates.Location = New System.Drawing.Point(0, 0)
        Me.btnTemplates.Name = "btnTemplates"
        Me.btnTemplates.Size = New System.Drawing.Size(227, 27)
        Me.btnTemplates.TabIndex = 7
        Me.btnTemplates.Tag = "0"
        Me.btnTemplates.Text = "&Referral Letter"
        Me.btnTemplates.UseVisualStyleBackColor = False
        '
        'pnlbtnLabs
        '
        Me.pnlbtnLabs.Controls.Add(Me.Label40)
        Me.pnlbtnLabs.Controls.Add(Me.Label41)
        Me.pnlbtnLabs.Controls.Add(Me.Label42)
        Me.pnlbtnLabs.Controls.Add(Me.Label43)
        Me.pnlbtnLabs.Controls.Add(Me.btnLabs)
        Me.pnlbtnLabs.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlbtnLabs.Location = New System.Drawing.Point(0, 0)
        Me.pnlbtnLabs.Name = "pnlbtnLabs"
        Me.pnlbtnLabs.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnLabs.Size = New System.Drawing.Size(230, 30)
        Me.pnlbtnLabs.TabIndex = 5
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label40.Location = New System.Drawing.Point(1, 26)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(225, 1)
        Me.Label40.TabIndex = 12
        Me.Label40.Text = "label2"
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(0, 1)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(1, 26)
        Me.Label41.TabIndex = 11
        Me.Label41.Text = "label4"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label42.Location = New System.Drawing.Point(226, 1)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(1, 26)
        Me.Label42.TabIndex = 10
        Me.Label42.Text = "label3"
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(0, 0)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(227, 1)
        Me.Label43.TabIndex = 9
        Me.Label43.Text = "label1"
        '
        'btnLabs
        '
        Me.btnLabs.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Orange
        Me.btnLabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLabs.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnLabs.FlatAppearance.BorderSize = 0
        Me.btnLabs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLabs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLabs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLabs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLabs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnLabs.Location = New System.Drawing.Point(0, 0)
        Me.btnLabs.Name = "btnLabs"
        Me.btnLabs.Size = New System.Drawing.Size(227, 27)
        Me.btnLabs.TabIndex = 0
        Me.btnLabs.Tag = "0"
        Me.btnLabs.Text = "&Orders && Results"
        Me.btnLabs.UseVisualStyleBackColor = False
        '
        'pnlbtnFlowsheet
        '
        Me.pnlbtnFlowsheet.Controls.Add(Me.Label4)
        Me.pnlbtnFlowsheet.Controls.Add(Me.Label10)
        Me.pnlbtnFlowsheet.Controls.Add(Me.Label14)
        Me.pnlbtnFlowsheet.Controls.Add(Me.Label15)
        Me.pnlbtnFlowsheet.Controls.Add(Me.btnFlowsheet)
        Me.pnlbtnFlowsheet.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbtnFlowsheet.Location = New System.Drawing.Point(0, 554)
        Me.pnlbtnFlowsheet.Name = "pnlbtnFlowsheet"
        Me.pnlbtnFlowsheet.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.pnlbtnFlowsheet.Size = New System.Drawing.Size(230, 30)
        Me.pnlbtnFlowsheet.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(1, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(225, 1)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "label2"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(0, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 26)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(226, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 26)
        Me.Label14.TabIndex = 10
        Me.Label14.Text = "label3"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(0, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(227, 1)
        Me.Label15.TabIndex = 9
        Me.Label15.Text = "label1"
        '
        'btnFlowsheet
        '
        Me.btnFlowsheet.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.btnFlowsheet.BackgroundImage = CType(resources.GetObject("btnFlowsheet.BackgroundImage"), System.Drawing.Image)
        Me.btnFlowsheet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnFlowsheet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnFlowsheet.FlatAppearance.BorderSize = 0
        Me.btnFlowsheet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnFlowsheet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnFlowsheet.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFlowsheet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFlowsheet.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnFlowsheet.Location = New System.Drawing.Point(0, 0)
        Me.btnFlowsheet.Name = "btnFlowsheet"
        Me.btnFlowsheet.Size = New System.Drawing.Size(227, 27)
        Me.btnFlowsheet.TabIndex = 5
        Me.btnFlowsheet.Tag = "0"
        Me.btnFlowsheet.Text = "&FlowSheet"
        Me.btnFlowsheet.UseVisualStyleBackColor = False
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.trOrderAssociation)
        Me.Panel5.Controls.Add(Me.Label19)
        Me.Panel5.Controls.Add(Me.Label18)
        Me.Panel5.Controls.Add(Me.lbl_pnlBottom)
        Me.Panel5.Controls.Add(Me.lbl_pnlLeft)
        Me.Panel5.Controls.Add(Me.lbl_pnlRight)
        Me.Panel5.Controls.Add(Me.lbl_pnlTop)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(233, 54)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.Panel5.Size = New System.Drawing.Size(562, 584)
        Me.Panel5.TabIndex = 2
        '
        'trOrderAssociation
        '
        Me.trOrderAssociation.BackColor = System.Drawing.Color.White
        Me.trOrderAssociation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trOrderAssociation.CheckBoxes = True
        Me.trOrderAssociation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trOrderAssociation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trOrderAssociation.ForeColor = System.Drawing.Color.Black
        Me.trOrderAssociation.HideSelection = False
        Me.trOrderAssociation.ImageIndex = 7
        Me.trOrderAssociation.ImageList = Me.imgTreeView
        Me.trOrderAssociation.ItemHeight = 21
        Me.trOrderAssociation.Location = New System.Drawing.Point(4, 8)
        Me.trOrderAssociation.Name = "trOrderAssociation"
        Me.trOrderAssociation.SelectedImageIndex = 7
        Me.trOrderAssociation.ShowLines = False
        Me.trOrderAssociation.Size = New System.Drawing.Size(557, 572)
        Me.trOrderAssociation.TabIndex = 2
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.White
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Location = New System.Drawing.Point(1, 8)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(3, 572)
        Me.Label19.TabIndex = 0
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.White
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label18.Location = New System.Drawing.Point(1, 4)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(560, 4)
        Me.Label18.TabIndex = 38
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(1, 580)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(560, 1)
        Me.lbl_pnlBottom.TabIndex = 8
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 577)
        Me.lbl_pnlLeft.TabIndex = 7
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(561, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 577)
        Me.lbl_pnlRight.TabIndex = 6
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(0, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(562, 1)
        Me.lbl_pnlTop.TabIndex = 5
        Me.lbl_pnlTop.Text = "label1"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.tblOrderAss)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1028, 54)
        Me.Panel4.TabIndex = 0
        '
        'tblOrderAss
        '
        Me.tblOrderAss.BackColor = System.Drawing.Color.Transparent
        Me.tblOrderAss.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblOrderAss.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblOrderAss.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblOrderAss.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tblOrderAss.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblOrderAss.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblNew, Me.tblFinish, Me.tblSave, Me.ts_gloCommunityDownload, Me.tblRefresh, Me.tblClose})
        Me.tblOrderAss.Location = New System.Drawing.Point(0, 0)
        Me.tblOrderAss.Name = "tblOrderAss"
        Me.tblOrderAss.Size = New System.Drawing.Size(1028, 53)
        Me.tblOrderAss.TabIndex = 0
        Me.tblOrderAss.Text = "ToolStrip1"
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
        'tblFinish
        '
        Me.tblFinish.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblFinish.Image = CType(resources.GetObject("tblFinish.Image"), System.Drawing.Image)
        Me.tblFinish.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblFinish.Name = "tblFinish"
        Me.tblFinish.Size = New System.Drawing.Size(45, 50)
        Me.tblFinish.Tag = "Finish"
        Me.tblFinish.Text = "&Finish"
        Me.tblFinish.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblFinish.Visible = False
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
        'tblRefresh
        '
        Me.tblRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblRefresh.Image = CType(resources.GetObject("tblRefresh.Image"), System.Drawing.Image)
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
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(230, 54)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 584)
        Me.Splitter1.TabIndex = 4
        Me.Splitter1.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter2.Location = New System.Drawing.Point(795, 54)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(3, 584)
        Me.Splitter2.TabIndex = 5
        Me.Splitter2.TabStop = False
        '
        'cmnuAddOrderSet
        '
        Me.cmnuAddOrderSet.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem})
        Me.cmnuAddOrderSet.Name = "cmnuAddCategory"
        Me.cmnuAddOrderSet.Size = New System.Drawing.Size(79, 26)
        '
        'ToolStripMenuItem
        '
        Me.ToolStripMenuItem.Name = "ToolStripMenuItem"
        Me.ToolStripMenuItem.Size = New System.Drawing.Size(78, 22)
        '
        'frmOrderAssociation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1028, 638)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel4)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmOrderAssociation"
        Me.ShowInTaskbar = False
        Me.Text = "Smart Order"
        Me.Panel1.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel10.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.pnltrAssociates.ResumeLayout(False)
        Me.pnlbtnDrugs.ResumeLayout(False)
        Me.pnlbtnRadiology.ResumeLayout(False)
        Me.pnlRightSearch.ResumeLayout(False)
        Me.pnlRightSearch.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlbtnTemplates.ResumeLayout(False)
        Me.pnlbtnLabs.ResumeLayout(False)
        Me.pnlbtnFlowsheet.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.tblOrderAss.ResumeLayout(False)
        Me.tblOrderAss.PerformLayout()
        Me.cmnuAddOrderSet.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmOrderAssociation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            'Code Start added by kanchan on 20120102 for gloCommunity integration
            If gblnIsShareUserRights = True Then ''Added condition to fixed Bug # : 37984 on 20120927
                ts_gloCommunityDownload.Visible = gblngloCommunity
            End If
            'Code end added by kanchan on 20120102 for gloCommunity integration
            ' Dim objSmartOrderDBLayer As ClsSmartorderDBLayer
            If (IsNothing(objSmartOrderDBLayer) = False) Then
                objSmartOrderDBLayer.Dispose()
                objSmartOrderDBLayer = Nothing
            End If
            objSmartOrderDBLayer = New ClsSmartorderDBLayer
            trOrderAssociation.AllowDrop = True

            Dim rootnode As myTreeNode
            'Dim i As Integer
            'Dim dt As New DataTable

            ''''Fill Left Treeview i.e. trvOderset with orderset.

            '''''''' FillOrderSet()
            FillOrderSet_New()

            ''''Add Root Node To Association Treeview
            rootnode = New myTreeNode("Orders Association", -1)
            rootnode.ImageIndex = 6
            rootnode.SelectedImageIndex = 6
            trOrderAssociation.Nodes.Add(rootnode)

            ''''Fill Labs Date at Form load using ID as 1
            PopulateAssociates(1)
            trAssociates.SelectedNode = trAssociates.Nodes.Item(0)
            trAssociates.ExpandAll()
            _IsLabs = True

            '''' Open Form Smart Order with selected Order
            If IsOpenfrmSmartOrder = True Then

                If Not IsNothing(OrderNode) Then
                    Dim mynode As myTreeNode

                    trOrderAssociation.Nodes.Item(0).Nodes.Clear()
                    'Dim lst As New myList
                    'lst = CType(arrSelectedNodes.Item(0), myList)
                    ' mynode = New myTreeNode(CType(lst.Value, String), CType(lst.Index, Long))
                    mynode = CType(arrSelectedNodes.Item(0), myTreeNode)
                    AddNode(mynode)
                    trOrderAssociation.ExpandAll()
                End If
            End If
            ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101012
            CheckAllParentNodes()
            ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101012
            txtsearchOrderset.Text = ""
            txtsearchOrderset.Focus()
            txtsearchOrderset.Select()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '
    Private Sub CheckAllChildren(ByVal tn As TreeNode, ByVal bCheck As [Boolean])
        ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101012
        bParentTrigger = False
        For Each ctn As TreeNode In tn.Nodes
            bChildTrigger = False
            ctn.Checked = bCheck
            bChildTrigger = True

            CheckAllChildren(ctn, bCheck)
        Next
        bParentTrigger = True
        ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101012
    End Sub


    Private Sub CheckMyParent(ByVal tn As TreeNode, ByVal bCheck As [Boolean])
        ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101012
        If tn Is Nothing Then
            Exit Sub
        End If
        If tn.Parent Is Nothing Then
            Exit Sub
        End If

        bChildTrigger = False
        bParentTrigger = False

        If bCheck Then
            Dim bNodeFound As Boolean = False
            For Each _Node As TreeNode In tn.Parent.Nodes
                If _Node.Checked = False Then
                    tn.Parent.Checked = False
                    bNodeFound = True
                    Exit For
                End If
            Next
            If bNodeFound = False Then
                tn.Parent.Checked = True
            End If
        Else
            tn.Parent.Checked = bCheck
        End If

        CheckMyParent(tn.Parent, bCheck)
        bParentTrigger = True
        bChildTrigger = True
        ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101012
    End Sub


    Private Sub CheckAllParentNodes()
        ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101012        
        Dim innerchildflag As Boolean = False
        Dim outerchildflag As Boolean = False
        Dim parentflag As Boolean = False

        For Each ptn As TreeNode In trOrderAssociation.Nodes.Item(0).Nodes
            For Each otherptn As TreeNode In ptn.Nodes
                For Each ootherptn As TreeNode In otherptn.Nodes
                    If ootherptn.Checked = False Then
                        innerchildflag = True
                        Exit For
                    End If
                Next
                If innerchildflag = False And otherptn.Nodes.Count > 0 Then
                    otherptn.Checked = True

                Else

                    outerchildflag = True
                End If
                innerchildflag = False
            Next

            If outerchildflag = False And ptn.Nodes.Count > 0 Then
                ptn.Checked = True
            Else

                parentflag = True
            End If
            outerchildflag = False
        Next
        ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101012
    End Sub

    Private Sub FillOrderSet()
        ''''Send O to retrive Odrerset

        '  dt_Orderset = New DataTable
        Try
            If (IsNothing(dt_Orderset) = False) Then
                dt_Orderset.Dispose()
                dt_Orderset = Nothing
            End If
            If (IsNothing(objSmartOrderDBLayer)) Then
                objSmartOrderDBLayer = New ClsSmartorderDBLayer
            End If

            dt_Orderset = objSmartOrderDBLayer.FillControl(0)
            If IsNothing(dt_Orderset) = False Then
                trvOrderset.Hide()
                trvOrderset.Nodes.Clear()
                Dim rootnode As TreeNode
                rootnode = New myTreeNode("OrderSet", -1)
                rootnode.ImageIndex = 5
                rootnode.SelectedImageIndex = 5
                trvOrderset.Nodes.Add(rootnode)

                'Populate OrderSet Data
                For i As Integer = 0 To dt_Orderset.Rows.Count - 1
                    Dim mychildnode As myTreeNode

                    mychildnode = New myTreeNode(dt_Orderset.Rows(i)(1), dt_Orderset.Rows(i)(0), CType(dt_Orderset.Rows(i)(2), String))
                    mychildnode.ImageIndex = 4
                    mychildnode.SelectedImageIndex = 4
                    trvOrderset.Nodes.Item(0).Nodes.Add(mychildnode)
                Next

            End If
            trvOrderset.ExpandAll()
            trvOrderset.Show()
            trvOrderset.Select()
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub PopulateAssociates(ByVal id As Int16, Optional ByVal strsearch As String = "")
        ''''Clear Node
        trAssociates.Nodes.Clear()

        'Dim dv As DataView
        'dv.ToTable()
        ''''Dock all the Button at Bottom
        Dim rootnode As myTreeNode = Nothing
        pnlbtnTemplates.Dock = DockStyle.Bottom
        btnTemplates.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnTemplates.BackgroundImageLayout = ImageLayout.Stretch
        btnTemplates.ForeColor = Color.FromArgb(31, 73, 125)


        pnlbtnRadiology.Dock = DockStyle.Bottom
        btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnRadiology.BackgroundImageLayout = ImageLayout.Stretch

        btnRadiology.ForeColor = Color.FromArgb(31, 73, 125)

        pnlbtnLabs.Dock = DockStyle.Bottom
        btnLabs.ForeColor = Color.FromArgb(31, 73, 125)
        btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnLabs.BackgroundImageLayout = ImageLayout.Stretch


        pnlbtnDrugs.Dock = DockStyle.Bottom
        btnDrugs.ForeColor = Color.FromArgb(31, 73, 125)
        btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnDrugs.BackgroundImageLayout = ImageLayout.Stretch

        pnlbtnFlowsheet.Dock = DockStyle.Bottom
        btnFlowsheet.ForeColor = Color.FromArgb(31, 73, 125)
        btnFlowsheet.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        btnFlowsheet.BackgroundImageLayout = ImageLayout.Stretch


        '''' Templates
        If id = 0 Then
            With btnTemplates
                pnlbtnTemplates.Dock = DockStyle.Top
                btnTemplates.ForeColor = Color.FromArgb(31, 73, 125)
                btnTemplates.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                btnTemplates.BackgroundImageLayout = ImageLayout.Stretch
                btnTemplates.BringToFront()
                pnlRightSearch.BringToFront()
                pnltrAssociates.BringToFront()

            End With
            rootnode = New myTreeNode("Referral Letter", -1)
            rootnode.ImageIndex = 2
            rootnode.SelectedImageIndex = 2
            trAssociates.Nodes.Add(rootnode)
            '' FillTemplate()
            strParentToAssociate = "Referral Letter"
            FillTemplates_New()
        ElseIf id = 1 Then ''''Labs
            With btnLabs
                pnlbtnLabs.Dock = DockStyle.Top
                btnLabs.ForeColor = Color.FromArgb(31, 73, 125)
                btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                btnLabs.BackgroundImageLayout = ImageLayout.Stretch
                btnLabs.BringToFront()
                pnlRightSearch.BringToFront()
                pnltrAssociates.BringToFront()


            End With
            rootnode = New myTreeNode("Orders & Results", -1)
            rootnode.ImageIndex = 0
            rootnode.SelectedImageIndex = 0
            trAssociates.Nodes.Add(rootnode)
            ''FillLab()
            strParentToAssociate = "Orders & Results"

            FillLabTest()
        ElseIf id = 4 Then ''''Orders
            With btnRadiology
                pnlbtnRadiology.Dock = DockStyle.Top
                btnRadiology.ForeColor = Color.FromArgb(31, 73, 125)
                btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                btnRadiology.BackgroundImageLayout = ImageLayout.Stretch
                btnRadiology.BringToFront()
                pnlRightSearch.BringToFront()
                pnltrAssociates.BringToFront()


            End With
            rootnode = New myTreeNode("Order Templates", -1)
            rootnode.ImageIndex = 1
            rootnode.SelectedImageIndex = 1
            trAssociates.Nodes.Add(rootnode)
            ''FillRadiology()
            strParentToAssociate = "Order Templates"

            FillRadiologyTest()
        ElseIf id = 5 Then ''''Orders
            With btnDrugs
                pnlbtnDrugs.Dock = DockStyle.Top
                btnDrugs.ForeColor = Color.FromArgb(31, 73, 125)
                btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
                btnDrugs.BringToFront()
                pnlRightSearch.BringToFront()
                pnltrAssociates.BringToFront()


            End With
            rootnode = New myTreeNode("Drugs", -1)
            rootnode.ImageIndex = 8
            rootnode.SelectedImageIndex = 8
            trAssociates.Nodes.Add(rootnode)
            'FillDrugs()
            strParentToAssociate = "Drugs"

            fill_Drugs_new()

        ElseIf id = 6 Then ''FlowSheet
            With btnFlowsheet
                pnlbtnFlowsheet.Dock = DockStyle.Top
                btnFlowsheet.ForeColor = Color.FromArgb(31, 73, 125)
                btnFlowsheet.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongOrange
                btnFlowsheet.BackgroundImageLayout = ImageLayout.Stretch
                btnFlowsheet.BringToFront()
                pnlRightSearch.BringToFront()
                pnltrAssociates.BringToFront()


            End With
            rootnode = New myTreeNode("FlowSheet", -1)
            rootnode.ImageIndex = 9
            rootnode.SelectedImageIndex = 9
            trAssociates.Nodes.Add(rootnode)
            'FillDrugs()
            strParentToAssociate = "FlowSheet"

            fill_Flowsheet()
        End If

        trAssociates.ExpandAll()
        trAssociates.Select()
        trAssociates.SelectedNode = trAssociates.Nodes.Item(0)
    End Sub

    Private Shared frm As frmOrderAssociation
    Public Shared IsOpen As Boolean = False

    Public Shared Function GetInstance() As frmOrderAssociation
        '_mu.WaitOne()
        Try
            IsOpen = False
            ''If frm Is Nothing Then

            For Each f As Form In Application.OpenForms
                If f.Name = "frmOrderAssociation" Then
                    ''If CType(f, frmICD9Association) = PatientID Then
                    IsOpen = True
                    frm = f
                    ''End If

                End If
            Next
            If (IsOpen = False) Then
                ''frm = New frmICD9Association(VisitID, VisitDate, PatientID, blnRecordLock, _RecordLock)
                frm = New frmOrderAssociation()
            End If
            'frm = New frmHistory(VisitID, VisitDate, PatientID, blnRecordLock, _RecordLock)
            ''Else
            ''For Each f As Form In Application.OpenForms
            ''    If f.Name = "frmHistory" Then
            ''        If CType(f, frmHistory).m_PatientID = PatientID Then
            ''            IsOpen = True
            ''            frm = f
            ''        End If

            ''    End If
            ''Next
            ''If (IsOpen = False) Then
            ''    frm = New frmHistory(VisitID, VisitDate, PatientID, blnRecordLock, _RecordLock)
            ''End If

            ''End If
        Finally
            '_mu.ReleaseMutex()
        End Try
        Return frm
    End Function


    Public Sub fill_Flowsheet()
        If (IsNothing(objSmartOrderDBLayer)) Then
            objSmartOrderDBLayer = New ClsSmartorderDBLayer
        End If
        Dim dt_FlowSheet As DataTable = objSmartOrderDBLayer.FillFlowsheet()
        GloUC_trvAssociates.Clear()
        GloUC_trvAssociates.DataSource = dt_FlowSheet


        If Not dt_FlowSheet Is Nothing Then
            GloUC_trvAssociates.DataSource = dt_FlowSheet
            GloUC_trvAssociates.ValueMember = dt_FlowSheet.Columns(0).ColumnName
            GloUC_trvAssociates.DescriptionMember = dt_FlowSheet.Columns(1).ColumnName
            GloUC_trvAssociates.CodeMember = dt_FlowSheet.Columns(1).ColumnName
            GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
            GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
            GloUC_trvAssociates.FillTreeView()
        End If
    End Sub

    Public Sub FillDrugs()
        Dim oNode As myTreeNode = Nothing
        Dim oDrugs As gloStream.DiseaseManagement.Supporting.Drugs
        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria

        trAssociates.BeginUpdate()
        Try
            trAssociates.Nodes.Item(0).Nodes.Clear()
            oDrugs = oDM.Drugs(txtsearchAssociates.Text.Trim)
            If Not IsNothing(oDrugs) Then
                If oDrugs.Count > 0 Then
                    For i As Integer = 1 To oDrugs.Count
                        '' SUDHIR 20090429 '' SMART ORDER DENORMALIZATION ''
                        'rootnode = New myTreeNode(oDrugs(_C).Name, oDrugs(_C).ID)

                        oNode = New myTreeNode
                        oNode.Text = oDrugs(i).Name
                        oNode.Key = oDrugs(i).ID
                        oNode.Dosage = oDrugs(i).Dosage
                        oNode.DrugForm = oDrugs(i).DrugForm
                        oNode.Route = oDrugs(i).Route
                        oNode.Frequency = oDrugs(i).Frequency
                        oNode.NDCCode = oDrugs(i).NDCCode
                        oNode.IsNarcotics = oDrugs(i).IsNarcotics
                        oNode.Duration = oDrugs(i).Duration
                        oNode.mpid = oDrugs(i).mpid
                        oNode.DrugQtyQualifier = oDrugs(i).DrugQtyQualifier

                        oNode.ImageIndex = 4
                        oNode.SelectedImageIndex = 4
                        trAssociates.Nodes.Item(0).Nodes.Add(oNode)

                        'For _G = 1 To oLabsModule.Item(_C).LabModuleTestResults.Count
                        '    'trAssociates.Nodes.Item(1).Nodes.Add(New myTreeNode(oLabsModule.Item(_C).LabModuleTestResults(_G).ResultName, oLabsModule.Item(_C).LabModuleTestResults(_G).ResultID))
                        '    Dim mychildnode As myTreeNode
                        '    mychildnode = New myTreeNode(oLabsModule.Item(_C).LabModuleTestResults(_G).ResultName, oLabsModule.Item(_C).LabModuleTestResults(_G).ResultID)
                        '    'trAssociates.Nodes.Item(0).Nodes.Add(mychildnode)
                        '    rootnode.Nodes.Add(mychildnode)

                        'Next
                    Next
                End If
                oDrugs.Dispose()
                oDrugs = Nothing
            End If
            oDM.Dispose()
            oDM = Nothing
            'With rootnode
            '    ' .BeginUpdate()

            '    If Not oDrugs Is Nothing Then
            '        For i As Int64 = 1 To oDrugs.Count
            '            'oNode = New myTreeNode(oDrugs(i).Name, oDrugs(i).ID)
            '            oNode = New myTreeNode
            '            With oNode
            '                .Text = oDrugs(i).Name
            '                .Key = oDrugs(i).ID
            '            End With
            '            oNode.ImageIndex = 11
            '            oNode.SelectedImageIndex = 11
            '            .Nodes.Add(oNode)
            '            oNode = Nothing
            '        Next
            '    End If
            '    ' .EndUpdate()
            '    .ExpandAll()
            'End With
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        trAssociates.EndUpdate()
    End Sub

    ''' <summary>
    ''' Fill Templates on Templates Button Click in Right Side Treeview
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTemplate()
        'Dim newNode As New TreeNode
        Dim objMyTreeView As myTreeNode
        Dim objTemplateGallery As New clsTemplateGallery
        Dim objCategory As myTreeNode = Nothing
        Dim objTemplate As myTreeNode
        Dim dt_temp As DataTable = objTemplateGallery.GetAllCategory

        Dim j As Integer
        trAssociates.Nodes.Clear()

        objMyTreeView = New myTreeNode("Referral Letter", 0)
        objMyTreeView.ImageIndex = 2  '' Category ICon
        objMyTreeView.SelectedImageIndex = 2 '' Category ICon
        trAssociates.Nodes.Add(objMyTreeView)
        If (IsNothing(dt_temp) = False) Then


            Dim ValueMember As Int64
            Dim DisplayMember As String

            For i As Integer = 0 To dt_temp.Rows.Count - 1
                'Dim ValueMember As Int64
                'Dim DisplayMember As String
                ValueMember = dt_temp.Rows(i)(0)
                DisplayMember = dt_temp.Rows(i)(1)
                ''''Select only Referral Letter templates
                If DisplayMember = "Referral Letter" Then
                    'objCategory = New myTreeNode(DisplayMember, ValueMember)
                    'objCategory.ImageIndex = 7 '''' Template ICon
                    'objCategory.SelectedImageIndex = 7 '''' Template ICon
                    'objMyTreeView.Nodes.Add(objCategory)

                    '        dvTemplate = New DataView
                    If (IsNothing(dvTemplate) = False) Then
                        dvTemplate.Dispose()
                        dvTemplate = Nothing
                    End If
                    dvTemplate = objTemplateGallery.GetAllTemplateGallery(ValueMember)
                    If (IsNothing(dvTemplate) = False) Then


                        For j = 0 To dvTemplate.Table.Rows.Count - 1
                            ''Dim ValueMember As Int64
                            ''Dim DisplayMember As String
                            ValueMember = dvTemplate.Table.Rows(j)(0)
                            DisplayMember = dvTemplate.Table.Rows(j)(1)
                            objTemplate = New myTreeNode(DisplayMember, ValueMember)
                            objTemplate.ImageIndex = 4 '''' Play ICon
                            objTemplate.SelectedImageIndex = 4 '''' Play ICon
                            'objMyTreeView.Nodes.Add(objTemplate)
                            objMyTreeView.Nodes.Add(objTemplate)
                            objMyTreeView.EnsureVisible()
                            objMyTreeView.ExpandAll()
                        Next
                    End If
                End If
            Next
        End If
        objTemplateGallery.Dispose()
        objTemplateGallery = Nothing

         'trvTemplate.ExpandAll()
    End Sub

    'Fill Lab  Data On lab Double click to Right side Treeview
   
    Public Sub FillLab()
        Dim rootnode As myTreeNode = Nothing
        Dim _C As Integer
        'Dim _G As Integer
        ''''Create object for the class
        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
        Dim oLabsModule As gloStream.DiseaseManagement.Supporting.LabModuleTests

        ''''assign Lab Test and Result to collection
        oLabsModule = oDM.LabModuleTests

        If Not oLabsModule Is Nothing Then
            If oLabsModule.Count > 0 Then

                'Fill Test
                For _C = 1 To oLabsModule.Count

                    rootnode = New myTreeNode(oLabsModule(_C).Name, oLabsModule(_C).TestID)
                    rootnode.ImageIndex = 4
                    rootnode.SelectedImageIndex = 4
                    trAssociates.Nodes.Item(0).Nodes.Add(rootnode)

                    'For _G = 1 To oLabsModule.Item(_C).LabModuleTestResults.Count
                    '    'trAssociates.Nodes.Item(1).Nodes.Add(New myTreeNode(oLabsModule.Item(_C).LabModuleTestResults(_G).ResultName, oLabsModule.Item(_C).LabModuleTestResults(_G).ResultID))
                    '    Dim mychildnode As myTreeNode
                    '    mychildnode = New myTreeNode(oLabsModule.Item(_C).LabModuleTestResults(_G).ResultName, oLabsModule.Item(_C).LabModuleTestResults(_G).ResultID)
                    '    'trAssociates.Nodes.Item(0).Nodes.Add(mychildnode)
                    '    rootnode.Nodes.Add(mychildnode)

                    'Next
                Next
            End If
            oLabsModule.Dispose()
            oLabsModule = Nothing
        End If
        oDM.Dispose()
        oDM = Nothing
        'oLabsModule = Nothing
    End Sub
    ''' <summary>
    ''' Fill Orders Data On Orders Double click to Right side Treeview
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub FillRadiology()
        Dim rootnode As myTreeNode = Nothing
        Dim _C As Integer, _G As Integer, _T As Integer
        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
        Dim oLabs As gloStream.DiseaseManagement.Supporting.Orders

        oLabs = oDM.Orders
        If Not oLabs Is Nothing Then
            If oLabs.Count > 0 Then

                'Fill Category
                For _C = 1 To oLabs.Count

                    'rootnode = New myTreeNode(oLabs(_C).Category, oLabs(_C).ID)
                    'trAssociates.Nodes.Item(0).Nodes.Add(rootnode)
                    For _G = 1 To oLabs.Item(_C).OrderGroups.Count
                        'Dim mychildnode As myTreeNode
                        'mychildnode = New myTreeNode(oLabs.Item(_C).LabGroups(_G).Name, oLabs.Item(_C).LabGroups(_G).ID)
                        'rootnode.Nodes.Add(mychildnode)
                        'Fill Tests Start
                        For _T = 1 To oLabs.Item(_C).OrderGroups(_G).Tests.Count
                            Dim mychildnode_ As myTreeNode
                            mychildnode_ = New myTreeNode(oLabs.Item(_C).OrderGroups(_G).Tests(_T).Description, oLabs.Item(_C).OrderGroups(_G).Tests(_T).ID)
                            ' rootnode.Nodes.Add(mychildnode_)
                            mychildnode_.ImageIndex = 4
                            mychildnode_.SelectedImageIndex = 4
                            trAssociates.Nodes.Item(0).Nodes.Add(mychildnode_)
                        Next
                    Next
                    'Fill Tests Finish
                Next ' For _G = 1 To oLabs.Item(_C).LabGroups.Count
                'Fill Groups & Category
            End If
            oLabs.Dispose()
            oLabs = Nothing
        End If
        oDM.Dispose()
        oDM = Nothing
        'oLabs = Nothing
    End Sub
    Private Sub txtsearchOrderset_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearchOrderset.TextChanged

        Try

            '''''''''''''''''''####################''''''''''''''''''''''''''
            '''''Code lines below are added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
            Dim strSearchDetails As String
            If Trim(txtsearchOrderset.Text) <> "" Then
                strSearchDetails = Replace(txtsearchOrderset.Text, "'", "''")
                strSearchDetails = Replace(strSearchDetails, "[", "") & ""
                strSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strSearchDetails)
            Else
                strSearchDetails = ""
            End If
            '''''''''''''''''''####################''''''''''''''''''''''''''


            FillOrderTreeView(dt_Orderset, strSearchDetails)

            txtsearchOrderset.Focus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub trOrderAssociation_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trOrderAssociation.DragOver
        Try
            'If IsNothing(trICD9Association.SelectedNode) = True Then
            '    Exit Sub
            'End If

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
            'If Not (selectedTreeview Is targetNode) Then
            '    'Select the node currently under the cursor
            '    selectedTreeview.SelectedNode = targetNode


            '    'Check that the selected node is not the dropNode and also that it
            '    'is not a child of the dropNode and therefore an invalid target
            '    Dim dropNode As TreeNode = CType(e.Data.GetData("gloEMR.myTreeNode"), myTreeNode)
            '    Do Until targetNode Is Nothing
            '        If targetNode Is dropNode Then
            '            e.Effect = DragDropEffects.None
            '            Exit Sub
            '        End If
            '        targetNode = targetNode.Parent
            '    Loop
            'End If

            'Currently selected node is a suitable target, allow the move
            e.Effect = DragDropEffects.Move
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trICD9_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles trvOrderset.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub trvOrderset_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trvOrderset.DragDrop, trOrderAssociation.DragDrop
        Try
            'If IsNothing(trICD9Association.SelectedNode) = True Then
            '    Exit Sub
            'End If

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
            If dropNode.TreeView Is trvOrderset Then
                If dropNode.Parent Is trvOrderset.Nodes.Item(0) Then
                    Dim str As String
                    str = dropNode.Key
                    Dim mytragetnode As myTreeNode
                    For Each mytragetnode In trOrderAssociation.Nodes.Item(0).Nodes
                        If mytragetnode.Key = str Then
                            txtsearchOrderset.Text = ""
                            txtsearchOrderset.Focus()
                            Exit Sub
                        End If
                    Next
                    'dropNode.Remove()
                    'If PopulateMedication(dropNode.Key) Then
                    Dim associatenode As myTreeNode

                    associatenode = dropNode.Clone
                    associatenode.Key = dropNode.Key
                    associatenode.Text = dropNode.Text
                    associatenode.ImageIndex = 1
                    associatenode.SelectedImageIndex = 1

                    trOrderAssociation.Nodes.Item(0).Nodes.Add(associatenode)

                    Dim MyChild As New myTreeNode

                    MyChild.Text = "CPT"
                    MyChild.Key = -1
                    MyChild.ImageIndex = 2
                    MyChild.SelectedImageIndex = 2
                    associatenode.Nodes.Add(MyChild)

                    MyChild = New myTreeNode
                    MyChild.Text = "Drugs"
                    MyChild.Key = -1
                    MyChild.ImageIndex = 8
                    MyChild.SelectedImageIndex = 8
                    associatenode.Nodes.Add(MyChild)

                    MyChild = New myTreeNode
                    MyChild.Text = "Patient Education"
                    MyChild.Key = -1
                    MyChild.ImageIndex = 5
                    MyChild.SelectedImageIndex = 5
                    associatenode.Nodes.Add(MyChild)

                    MyChild = New myTreeNode
                    MyChild.Text = "Tags"
                    MyChild.Key = -1
                    MyChild.ImageIndex = 4
                    MyChild.SelectedImageIndex = 4
                    associatenode.Nodes.Add(MyChild)

                    'trICD9Association.Nodes.Item(0).Nodes.Add(associatenode)

                    'associatenode.Nodes.Add(New myTreeNode("CPT", -1))
                    'associatenode.Nodes.Add(New myTreeNode("Drugs", -1))
                    'associatenode.Nodes.Add(New myTreeNode("Patient Education", -1))
                    'associatenode.Nodes.Add(New myTreeNode("Tags", -1))


                    Dim dt As DataTable
                    Dim objICD9AssociationDBLayer As ClsICD9AssociationDBLayer = New ClsICD9AssociationDBLayer
                    dt = objICD9AssociationDBLayer.FetchICD9forUpdate(associatenode.Key)
                    objICD9AssociationDBLayer.Dispose()
                    objICD9AssociationDBLayer = Nothing

                    If (IsNothing(dt) = False) Then


                        Dim i As Integer
                        For i = 0 To dt.Rows.Count - 1
                            If dt.Rows(i).Item(2) = "c" Then
                                associatenode.Nodes.Item(0).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                            ElseIf dt.Rows(i).Item(2) = "d" Then
                                associatenode.Nodes.Item(1).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                            ElseIf dt.Rows(i).Item(2) = "p" Then
                                associatenode.Nodes.Item(2).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                            ElseIf dt.Rows(i).Item(2) = "t" Then
                                associatenode.Nodes.Item(2).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                            End If
                        Next
                        dt.Dispose()
                        dt = Nothing
                    End If
                    trOrderAssociation.ExpandAll()
                    trOrderAssociation.Select()



                    'Ensure the newly created node is visible to the user and select it
                    associatenode.EnsureVisible()
                    trOrderAssociation.SelectedNode = associatenode
                End If
            End If
            txtsearchOrderset.Text = ""
            txtsearchOrderset.Focus()
            'commented from 14/09/2005
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '  Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            RefreshOrders()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RefreshOrders()
        trOrderAssociation.Nodes.Item(0).Nodes.Clear()
        'Call FillICD9(dtOrderbyCode, strSortByCode)
        txtsearchOrderset.Text = ""
        txtsearchOrderset.Focus()
        'trICD9Association.Nodes.Item(0).Nodes.Clear()
        'trICD9.Nodes.Item(0).Nodes.Clear()
        'Dim dt As DataTable
        'dt = objICD9AssociationDBLayer.FillControls(3)
        'Dim i As Integer
        'For i = 0 To dt.Rows.Count - 1
        '    Dim mychildnode As myTreeNode
        '    mychildnode = New myTreeNode(dt.Rows(i)(1), dt.Rows(i)(0))
        '    trICD9.Nodes.Item(0).Nodes.Add(mychildnode)
        'Next
        'trICD9.ExpandAll()
        'trICD9.Select()

    End Sub

    Private Sub SaveAssociation()
        'Get node count of child nodes in trICD9Associates
        If trOrderAssociation.Nodes.Item(0).GetNodeCount(False) > 0 Then

            Dim i As Integer
            For i = 0 To trOrderAssociation.Nodes.Item(0).GetNodeCount(False) - 1
                Dim OrdersetNode As myTreeNode
                'get the ICD9Node associated sequentially
                OrdersetNode = trOrderAssociation.Nodes.Item(0).Nodes.Item(i)
                If OrdersetNode.GetNodeCount(True) > 0 Then
                    Dim k As Integer
                    Dim arrlist As New ArrayList
                    For k = 0 To 4
                        Dim AssociateNode As myTreeNode
                        AssociateNode = OrdersetNode.Nodes.Item(k)
                        Dim j As Integer
                        For j = 0 To AssociateNode.GetNodeCount(False) - 1
                            Dim thisNode As myTreeNode = CType(AssociateNode.Nodes.Item(j), myTreeNode)
                            ''''''''''''''' Added by Ujwala - Smart Order Changes - checkbox - as on 20101012
                            If AssociateNode.Text = "Orders & Results" Then
                                'arrlist.Add(New myList(thisNode.Key, "L"))
                                arrlist.Add(New myList("L", thisNode.Text, thisNode.Key, AssociateNode.Nodes.Item(j).Checked))

                            ElseIf AssociateNode.Text = "Order Templates" Then
                                'arrlist.Add(New myList(thisNode.Key, "R"))
                                arrlist.Add(New myList("R", thisNode.Text, thisNode.Key, AssociateNode.Nodes.Item(j).Checked))

                            ElseIf AssociateNode.Text = "Referral Letter" Then
                                'arrlist.Add(New myList(thisNode.Key, "T"))
                                arrlist.Add(New myList("T", thisNode.Text, thisNode.Key, AssociateNode.Nodes.Item(j).Checked))
                            ElseIf AssociateNode.Text = "Drugs" Then
                                'arrlist.Add(New myList(thisNode.Key, "D"))
                                'arrlist.Add(New myList("D", thisNode.Text, thisNode.Key))
                                '' SUDHIR 20090429 '' SMART ORDER DENORMALIZATION ''
                                Dim _strDrugNM As String = ""

                                Dim oDrugNode As myTreeNode = thisNode
                                Dim oMyList As New myList
                                oMyList.Description = "D"
                                'oMyList.ParameterName = oDrugNode.Text
                                oMyList.Index = oDrugNode.Key
                                oMyList.Dosage = oDrugNode.Dosage
                                oMyList.DrugForm = oDrugNode.DrugForm
                                oMyList.Route = oDrugNode.Route
                                oMyList.Frequency = oDrugNode.Frequency
                                oMyList.NDCCode = oDrugNode.NDCCode
                                oMyList.IsNarcotic = oDrugNode.IsNarcotics
                                oMyList.Duration = oDrugNode.Duration
                                oMyList.mpid = oDrugNode.mpid
                                oMyList.DrugQtyQualifier = oDrugNode.DrugQtyQualifier
                                'oMyList.DrugName = oDrugNode.DrugName
                                ''''''''''''''
                                oMyList.ItemChecked = oDrugNode.Checked
                                ''''''''''''''

                                oMyList.DrugName = oDrugNode.DrugName

                                arrlist.Add(oMyList)

                            ElseIf AssociateNode.Text = "FlowSheet" Then
                                'arrlist.Add(New myList(thisNode.Key, "R"))
                                arrlist.Add(New myList("F", thisNode.Text, thisNode.Key, AssociateNode.Nodes.Item(j).Checked))

                            End If
                            ''''''''''''''' Added by Ujwala - Smart Order Changes - checkbox - as on 20101012
                        Next

                    Next
                    If (IsNothing(objSmartOrderDBLayer)) Then
                        objSmartOrderDBLayer = New ClsSmartorderDBLayer
                    End If
                    objSmartOrderDBLayer.AddData(OrdersetNode.Key, OrdersetNode.Tag, arrlist)
                    arrlist.Clear()
                End If
            Next
            RefreshOrders()
        End If
        'Shubhangi 20091202
        'Change the save button to save & close.
        '  Me.Close()
        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
    End Sub

    Private Sub trAssociates_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles trAssociates.DragDrop, trOrderAssociation.DragDrop
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



            Dim targetNode1 As myTreeNode = CType(selectedTreeview.SelectedNode, myTreeNode)


            If dropNode.TreeView Is trAssociates Then
                If trOrderAssociation.Nodes.Item(0).GetNodeCount(False) > 0 Then
                    If Not trOrderAssociation.SelectedNode Is trOrderAssociation.Nodes.Item(0) Then
                        If Not IsNothing(targetNode1) AndAlso Not IsNothing(dropNode) Then
                            'AddAssociates(dropNode, targetNode1.Parent.Text, "")
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnLabs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLabs.Click
        Try
            _IsLabs = True
            PopulateAssociates(1)
            txtsearchAssociates.Text = ""
            txtsearchAssociates.Tag = ""
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRadiology_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRadiology.Click
        Try
            _IsLabs = False
            PopulateAssociates(4)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnTemplates_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTemplates.Click
        Try
            _IsLabs = False
            PopulateAssociates(0)
            txtsearchAssociates.Text = ""
            txtsearchAssociates.Tag = ""
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trAssociates_ItemDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles trAssociates.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub trOrderAssociation_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trOrderAssociation.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Dim trvNode As TreeNode
                trvNode = trOrderAssociation.GetNodeAt(e.X, e.Y)
                If IsNothing(trvNode) = False Then
                    trOrderAssociation.SelectedNode = trvNode
                End If

                If Not IsNothing(trOrderAssociation.SelectedNode) Then
                    If trOrderAssociation.Nodes.Item(0).Text = trOrderAssociation.SelectedNode.Text Or trOrderAssociation.SelectedNode.Parent Is trOrderAssociation.Nodes.Item(0) Or (CType(trOrderAssociation.SelectedNode, myTreeNode).Key = -1) Then
                        'Try
                        '    If (IsNothing(trOrderAssociation.ContextMenu) = False) Then
                        '        trOrderAssociation.ContextMenu.Dispose()
                        '        trOrderAssociation.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trOrderAssociation.ContextMenu = Nothing
                    Else
                        'Try
                        '    If (IsNothing(trOrderAssociation.ContextMenu) = False) Then
                        '        trOrderAssociation.ContextMenu.Dispose()
                        '        trOrderAssociation.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trOrderAssociation.ContextMenu = cntICD9Association
                        'treeindex = trPrescriptionDetails.SelectedNode.Index
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuDeleteICD9Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteICD9Item.Click
        Try
            If Not trOrderAssociation.SelectedNode Is trOrderAssociation.Nodes.Item(0) OrElse Not trOrderAssociation.SelectedNode.Parent Is trOrderAssociation.Nodes.Item(0) Then

                Dim mychildnode As myTreeNode
                Dim key As Int64 = 0
                mychildnode = CType(trOrderAssociation.SelectedNode, myTreeNode)
                'objMedicationDBLayer.DeleteMedication(mychildnode.Index) 'delete from collection
                'key = mychildnode.Key
                mychildnode.Remove() 'delete from Medicationdetails treeview

                'Add the deleted node to Medication treeview

            End If
        Catch ex As SqlClient.SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvOrderset_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvOrderset.DoubleClick
        Try
            Dim mynode As myTreeNode
            mynode = CType(trvOrderset.SelectedNode, myTreeNode)
            If mynode.Text = "OrderSet" Then
                Exit Sub
            End If
            If Not IsNothing(mynode) Then
                AddNode(mynode)
                'AddNode_1(mynode)
            End If
            'selectedTreeview.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddNode(ByVal mynode As myTreeNode)
       
        'If mynode.Parent Is trvOrderset.Nodes.Item(0) Then
        'Dim str As String

        '' solving salesforce case-GLO2010-0004920
        Dim str As Long

        str = mynode.Key
        Dim mytragetnode As myTreeNode

        For Each mytragetnode In trOrderAssociation.Nodes.Item(0).Nodes
            If mytragetnode.Key = str Then
                Exit Sub
            End If
        Next

        'Add CPT/Drugs/PE to icd9 node
        'trICD9.SelectedNode.Remove()

        Dim associatenode As myTreeNode

        associatenode = mynode.Clone
        associatenode.Key = mynode.Key
        associatenode.Text = mynode.Text
        associatenode.ImageIndex = 5
        associatenode.SelectedImageIndex = 5

        trOrderAssociation.Nodes.Item(0).Nodes.Add(associatenode)
       
        Dim MyChild As New myTreeNode

        MyChild.Text = "Drugs"
        MyChild.Key = -1
        MyChild.ImageIndex = 8
        MyChild.SelectedImageIndex = 8
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "FlowSheet"
        MyChild.Key = -1
        MyChild.ImageIndex = 9
        MyChild.SelectedImageIndex = 9
        associatenode.Nodes.Add(MyChild)


        MyChild = New myTreeNode
        MyChild.Text = "Orders & Results"
        MyChild.Key = -1
        MyChild.ImageIndex = 1
        MyChild.SelectedImageIndex = 1
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
        MyChild.Text = "Order Templates"
        MyChild.Key = -1
        MyChild.ImageIndex = 2
        MyChild.SelectedImageIndex = 2
        associatenode.Nodes.Add(MyChild)

        MyChild = New myTreeNode
            MyChild.Text = "Referral Letter"
            MyChild.Name = "Referral Letter"
        MyChild.Key = -1
        MyChild.ImageIndex = 7
        MyChild.SelectedImageIndex = 7
        associatenode.Nodes.Add(MyChild)
        associatenode.Expand()

        Dim dt As DataTable
        If (IsNothing(objSmartOrderDBLayer)) Then
            objSmartOrderDBLayer = New ClsSmartorderDBLayer
        End If
        dt = objSmartOrderDBLayer.FetchOrderforUpdate(associatenode.Key)
        If (IsNothing(dt) = False) Then


            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1

                'add Labs 
                If dt.Rows(i).Item("AssociateType") = "L" Then              'Lab Test              
                    Dim strLabName As String
                    ''strLabName = objSmartOrderDBLayer.FetchLabName(dt.Rows(i).Item(1))
                    ''Sandip Darade 20090401 
                    ''Instead of using ID use name of the LAB 
                    strLabName = Convert.ToString(dt.Rows(i)("sAssociateName"))
                    Dim mytreenode As myTreeNode
                    mytreenode = New myTreeNode(strLabName, dt.Rows(i).Item("nAssociateID"))
                    ''''''''''''''' Added by Ujwala - Smart Order Changes  - as on 20101012
                    mytreenode.Checked = dt.Rows(i).Item("Status")
                    ''''''''''''''' Added by Ujwala - Smart Order Changes  - as on 20101012
                    mytreenode.ImageIndex = 4
                    mytreenode.SelectedImageIndex = 4
                    associatenode.Nodes.Item(2).Nodes.Add(mytreenode)
                    associatenode.Nodes.Item(2).Expand()
                    'add orders 
                ElseIf dt.Rows(i).Item("AssociateType") = "R" Then
                    Dim strRadiology As String
                    ''strRadiology = objSmartOrderDBLayer.FetchRadiologyName(dt.Rows(i).Item(1))
                    ''Sandip Darade 20090401 
                    ''Instead of using ID use name of the Radiology 
                    strRadiology = Convert.ToString(dt.Rows(i)("sAssociateName"))
                    Dim mytreenode As myTreeNode
                    mytreenode = New myTreeNode(strRadiology, dt.Rows(i).Item("nAssociateID"))
                    ''''''''''''''' Added by Ujwala - Smart Order Changes  - as on 20101012
                    mytreenode.Checked = dt.Rows(i).Item("Status")
                    ''''''''''''''' Added by Ujwala - Smart Order Changes  - as on 20101012
                    mytreenode.ImageIndex = 4
                    mytreenode.SelectedImageIndex = 4
                    associatenode.Nodes.Item(3).Nodes.Add(mytreenode)
                    associatenode.Nodes.Item(3).Expand()
                    'add  referral letter template 
                ElseIf dt.Rows(i).Item("AssociateType") = "T" Then
                    Dim strTemplate As String
                    ''strTemplate = objSmartOrderDBLayer.FetchTemplateName(dt.Rows(i).Item(1))
                    ''Sandip Darade 20090401 
                    ''Instead of using ID use name of the Template 
                    strTemplate = Convert.ToString(dt.Rows(i)("sAssociateName"))
                    Dim mytreenode As New myTreeNode
                    mytreenode = New myTreeNode(strTemplate, dt.Rows(i).Item("nAssociateID"))
                    ''''''''''''''' Added by Ujwala - Smart Order Changes  - as on 20101012
                    mytreenode.Checked = dt.Rows(i).Item("Status")
                    ''''''''''''''' Added by Ujwala - Smart Order Changes  - as on 20101012
                    mytreenode.ImageIndex = 4
                    mytreenode.SelectedImageIndex = 4
                    associatenode.Nodes.Item(4).Nodes.Add(mytreenode)
                    associatenode.Nodes.Item(4).Expand()
                    ''Add drugs 
                ElseIf dt.Rows(i).Item("AssociateType") = "D" Then
                    'Dim strDrugs As String
                    ''strDrugs = objSmartOrderDBLayer.FetchDrugName(dt.Rows(i).Item(1))
                    ''Sandip Darade 20090401 
                    ''Instead of using ID use name of the Drug 
                    'strDrugs = Convert.ToString(dt.Rows(i)("sAssociateName"))
                    Dim oNode As New myTreeNode
                    'Code Added by Mayuri:20091009
                    'To display both DrugName and Drug Form
                    'To check whether Drug Form is blank or not
                    oNode.Text = dt.Rows(i)("sAssociateName")
                    
                    oNode.Key = Convert.ToInt64(dt.Rows(i)("nAssociateID"))
                    oNode.Dosage = dt.Rows(i)("sDosage")
                    oNode.DrugForm = dt.Rows(i)("sDrugForm")
                    oNode.Route = dt.Rows(i)("sRoute")
                    oNode.Frequency = dt.Rows(i)("sFrequency")
                    oNode.NDCCode = dt.Rows(i)("sNDCCode")
                    oNode.IsNarcotics = Convert.ToInt16(dt.Rows(i)("nIsNarcotics"))
                    oNode.Duration = dt.Rows(i)("sDuration")
                    oNode.mpid = Convert.ToInt32(dt.Rows(i)("mpid"))
                    oNode.DrugQtyQualifier = dt.Rows(i)("sDrugQtyQualifier")
                    oNode.DrugName = dt.Rows(i)("sAssociateName")
                    oNode.ImageIndex = 4
                    oNode.SelectedImageIndex = 4
                    ''''''''''''''' Added by Ujwala - Smart Order Changes  - as on 20101012
                    oNode.Checked = dt.Rows(i).Item("Status")
                    ''''''''''''''' Added by Ujwala - Smart Order Changes  - as on 20101012
                    associatenode.Nodes.Item(0).Nodes.Add(oNode)
                    associatenode.Nodes.Item(0).Expand()

                ElseIf dt.Rows(i).Item("AssociateType") = "F" Then              'FlowSheet             
                    Dim strFlowshName As String

                    strFlowshName = Convert.ToString(dt.Rows(i)("sAssociateName"))
                    Dim mytreenode As myTreeNode
                    mytreenode = New myTreeNode(strFlowshName, dt.Rows(i).Item("nAssociateID"))
                    mytreenode.ImageIndex = 4
                    mytreenode.SelectedImageIndex = 4
                    mytreenode.Checked = dt.Rows(i).Item("Status")
                    associatenode.Nodes.Item(1).Nodes.Add(mytreenode)
                    associatenode.Nodes.Item(1).Expand()
                End If

            Next
            dt.Dispose()
            dt = Nothing
        End If

        'trOrderAssociation.ExpandAll()
        trOrderAssociation.Select()

        'treeindex = -1
        'End If

        'Ensure the newly created node is visible to the user and select it
        associatenode.EnsureVisible()
        trOrderAssociation.SelectedNode = associatenode
        'treeindex = mynode.Index
        'End If
        ''''''''''''''' Added by Ujwala - Smart Order Changes  - as on 20101012
        CheckAllParentNodes()
        ''''''''''''''' Added by Ujwala - Smart Order Changes  - as on 20101012
    End Sub

    Private Sub txtsearchOrderset_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearchOrderset.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trvOrderset.Select()
        Else
            trvOrderset.SelectedNode = trvOrderset.Nodes.Item(0)
        End If
        ''--Added by Anil on 20071213
        mdlGeneral.ValidateText(txtsearchOrderset.Text, e)
        ''----
    End Sub

    Private Sub txtsearchAssociates_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsearchAssociates.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            trAssociates.Select()
        Else
            trAssociates.SelectedNode = trAssociates.Nodes.Item(0)
        End If
        ''--Added by Anil on 20071213
        mdlGeneral.ValidateText(txtsearchAssociates.Text, e)
        ''----
    End Sub

    Private Sub trvOrderset_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trvOrderset.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            Try
                Dim mynode As myTreeNode
                mynode = CType(trvOrderset.SelectedNode, myTreeNode)
                If Not IsNothing(mynode) Then
                    AddNode(mynode)
                End If
                'selectedTreeview.ExpandAll()
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, "Orders Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnLabs_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLabs.MouseHover

        btnLabs.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnLabs.BackgroundImageLayout = ImageLayout.Stretch

        If (IsNothing(tooltipnew)) Then
            tooltipnew = New ToolTip
        End If
        tooltipnew.SetToolTip(btnLabs, "Orders & Results List")

    End Sub

    Private Sub btnLabs_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLabs.MouseLeave
        If pnlbtnLabs.Dock = DockStyle.Bottom Then
            btnLabs.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongButton
            btnLabs.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnLabs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnLabs.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub

    Private Sub btnTemplates_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTemplates.MouseHover
        btnTemplates.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnTemplates.BackgroundImageLayout = ImageLayout.Stretch

        If (IsNothing(tooltipnew)) Then
            tooltipnew = New ToolTip
        End If
        tooltipnew.SetToolTip(btnTemplates, "Referral Letter")
    End Sub

    Private Sub btnTemplates_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTemplates.MouseLeave
        If pnlbtnTemplates.Dock = DockStyle.Bottom Then
            btnTemplates.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongButton
            btnTemplates.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnTemplates.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnTemplates.BackgroundImageLayout = ImageLayout.Stretch
        End If

    End Sub

    Private Sub btnRadiology_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRadiology.MouseHover
        btnRadiology.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnRadiology.BackgroundImageLayout = ImageLayout.Stretch

        If (IsNothing(tooltipnew)) Then
            tooltipnew = New ToolTip
        End If
        tooltipnew.SetToolTip(btnRadiology, "Order Templates")
    End Sub

    Private Sub btnRadiology_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRadiology.MouseLeave
        If pnlbtnRadiology.Dock = DockStyle.Bottom Then
            btnRadiology.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongButton
            btnRadiology.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnRadiology.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnRadiology.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub btnDrugs_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrugs.MouseHover
        btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongYellow
        btnDrugs.BackgroundImageLayout = ImageLayout.Stretch

        If (IsNothing(tooltipnew)) Then
            tooltipnew = New ToolTip
        End If
        tooltipnew.SetToolTip(btnDrugs, "Drugs")
    End Sub

    Private Sub btnDrugs_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDrugs.MouseLeave
        If pnlbtnDrugs.Dock = DockStyle.Bottom Then
            btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
            btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnDrugs.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnDrugs.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub trAssociates_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles trAssociates.KeyPress
        If (e.KeyChar = ChrW(13)) Then
            Try
                Dim mynode As myTreeNode
                Dim targetnode1 As myTreeNode
                targetnode1 = CType(trOrderAssociation.SelectedNode, myTreeNode)
                mynode = CType(trAssociates.SelectedNode, myTreeNode)

                'check if selected node is rootnode
                If Not IsNothing(targetnode1) AndAlso Not IsNothing(mynode) Then
                    'AddAssociates(mynode, targetnode1.Parent.Text, "")
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(ex.Message, "Orders Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub AddAssociates_old(ByVal mynode As myTreeNode, ByVal strType As String, ByVal targetText As String)

        For Each myParentRootNode As myTreeNode In trOrderAssociation.Nodes(0).Nodes
            If myParentRootNode.Text = targetText Then '''' Check OrderSet Name
                'If targetText <> myParentRootNode Then
                For Each myRootNode As myTreeNode In myParentRootNode.Nodes 'trOrderAssociation.Nodes(0).Nodes(0).Nodes

                    If myRootNode.Text = "Referral Letter" And myRootNode.Text = strType Then ''''Don't allow to enter more then one Template
                        If myRootNode.Nodes.Count >= 1 Then
                            Exit Sub
                        End If
                    End If
                    If myRootNode.Text = strType Then
                        ''Loop for all field nodes in each Root node
                        For Each myTargetNode As myTreeNode In myRootNode.Nodes
                            ''Check whether the node already exists
                            If myTargetNode.Key = mynode.Key Then
                                ''If present do nothing
                                Exit Sub
                            End If
                        Next
                        ''Map all the node values to the associated node
                        Dim Associatenode As myTreeNode
                        Associatenode = mynode.Clone
                        Associatenode.Key = mynode.Key
                        Associatenode.Text = mynode.Text
                        Associatenode.Tag = mynode.Key
                        Associatenode.Dosage = mynode.Dosage
                        Associatenode.DrugForm = mynode.DrugForm
                        Associatenode.Route = mynode.Route
                        Associatenode.Frequency = mynode.Frequency
                        Associatenode.NDCCode = mynode.NDCCode
                        Associatenode.IsNarcotics = mynode.IsNarcotics
                        Associatenode.Duration = mynode.Duration
                        Associatenode.mpid = mynode.mpid
                        Associatenode.DrugQtyQualifier = mynode.DrugQtyQualifier
                        Associatenode.ImageIndex = 4
                        Associatenode.SelectedImageIndex = 4
                        myRootNode.Nodes.Add(Associatenode)

                    End If
                Next
            End If
        Next
        trOrderAssociation.ExpandAll()

    End Sub

    Private Sub AddAssociates(ByVal mynode As myTreeNode, ByVal strType As String, ByVal targetText As String, ByVal targetParentText As String)
        Dim IsAdded As Boolean
        For Each myParentRootNode As myTreeNode In trOrderAssociation.Nodes(0).Nodes
            'If myParentRootNode.Text = targetText Or myParentRootNode.Text = targetParentText Or targetText <> "Order Association" Then  '''' Check OrderSet Name
            If targetText <> "Orders Association" And IsAdded = False Then  '''' Check OrderSet Name
                'If targetText <> myParentRootNode Then
                For Each myRootNode As myTreeNode In myParentRootNode.Nodes 'trOrderAssociation.Nodes(0).Nodes(0).Nodes
                    ''Sanjog- Commented on 20101124 to allow multiple reference letter 
                    'If myRootNode.Text = "Referral Letter" And myRootNode.Text = strType Then
                    '    ''''Don't allow to enter more then one Template
                    '    If myRootNode.Nodes.Count >= 1 Then
                    '        Exit Sub
                    '    End If
                    'End If
                    ''Sanjog- Commented on 20101124 to allow multiple reference letter 
                    If myRootNode.Text = strType Then
                        If myRootNode.Parent.Text = targetText Or myRootNode.Parent.Text = targetParentText Then
                            ''Loop for all field nodes in each Root node
                            For Each myTargetNode As myTreeNode In myRootNode.Nodes
                                ''Check whether the node already exists
                                If myTargetNode.Key = mynode.Key Then
                                    ''If present do nothing
                                    Exit Sub
                                End If
                            Next
                            ''Map all the node values to the associated node
                            Dim Associatenode As myTreeNode
                            Associatenode = mynode.Clone
                            Associatenode.Key = mynode.Key
                            Associatenode.Text = mynode.Text
                            Associatenode.Tag = mynode.Key
                            Associatenode.Dosage = mynode.Dosage
                            Associatenode.DrugForm = mynode.DrugForm
                            Associatenode.Route = mynode.Route
                            Associatenode.Frequency = mynode.Frequency
                            Associatenode.NDCCode = mynode.NDCCode
                            Associatenode.IsNarcotics = mynode.IsNarcotics
                            Associatenode.Duration = mynode.Duration
                            Associatenode.mpid = mynode.mpid
                            Associatenode.DrugQtyQualifier = mynode.DrugQtyQualifier
                            Associatenode.DrugName = mynode.DrugName
                            Associatenode.ImageIndex = 4
                            Associatenode.SelectedImageIndex = 4
                            myRootNode.Nodes.Add(Associatenode)
                            myRootNode.Expand()
                            IsAdded = True
                            Exit For
                        End If
                    End If
                Next
            End If
        Next
        'trOrderAssociation.ExpandAll()

    End Sub


    Private Sub trAssociates_ContextMenuChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trAssociates.ContextMenuChanged
        txtsearchOrderset.Text = ""
    End Sub

    Private Sub frmOrderAssociation_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        txtsearchAssociates.Text = ""
        txtsearchAssociates.Tag = ""
    End Sub

    Private Sub tblOrderAss_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblOrderAss.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "New"
                    RefreshOrders()
                Case "Save"
                    SaveAssociation()
                Case "Finish"
                    SaveAssociation()
                    'Me.Close()
                    'gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                    'SLR: same isclaled in side saveasscocaton
                Case "Close"
                    ' Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

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

            If _IsLabs = True Then

                FillLabTreeView(strSearchDetails)
                Exit Sub
            Else
                ''****************
                If btnRadiology.Dock = DockStyle.Top Then
                    AddSearchAssociates(Trim(strSearchDetails), dtAssociates, 3)
                    Exit Sub
                End If

                If btnTemplates.Dock = DockStyle.Top Then
                    AddSearchAssociates(Trim(strSearchDetails), dtAssociates, 0)
                    Exit Sub
                End If


                If pnlbtnDrugs.Dock = DockStyle.Top Then
                    AddSearchAssociates(Trim(strSearchDetails), dtAssociates, 0)
                    Exit Sub
                End If


            End If
            '---------------------------------------
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

        End Try
    End Sub

    'sarika 26th sept 07
    Public Sub AddSearchAssociates(ByVal strSearch As String, ByVal dt As DataTable, ByVal type As Integer)
        Dim ODB As New gloStream.gloDataBase.gloDataBase
        Try
            Dim i As Integer
            Dim tdt As DataTable
            Dim dv As DataView = Nothing
            Dim oDTLab_Tests As DataTable = Nothing
            Dim CategoryID As Long
            Dim CategoryName As String
            Dim oDTLab_Category As DataTable = Nothing
            Dim oDTLab_Groups As DataTable = Nothing
            Dim Groupid As Integer
            Dim GroupName As String
            Dim myCategoryNode As myTreeNode = Nothing




            If btnTemplates.Dock = DockStyle.Top Then
                '          dv = New DataView
                dv = dvTemplate
                If (IsNothing(dv) = False) Then
                    dv.RowFilter = "sTemplateName Like '%" & strSearch & "%'"
                End If


            ElseIf btnRadiology.Dock = DockStyle.Top Then

                ODB.Connect(GetConnectionString)
                'fill the oDTLab_Category table by the data(id,description) from the LM_Category table  
                oDTLab_Category = ODB.ReadData("DM_SelectLMCategory")

                If (IsNothing(oDTLab_Category) = False) Then


                    'for each category in the oDTLab_Category(LM_Category) table
                    For i = 0 To oDTLab_Category.Rows.Count - 1

                        CategoryID = oDTLab_Category.Rows(i)("lm_category_ID")

                        CategoryName = oDTLab_Category.Rows(i)("lm_category_Description")

                        ODB.DBParameters.Clear()
                        ODB.DBParameters.Add("@id", CategoryID, ParameterDirection.Input, SqlDbType.BigInt)
                        oDTLab_Groups = ODB.ReadData("DM_SelectCategoryWiseLabGroup")
                        If (IsNothing(oDTLab_Groups) = False) Then
                            For j As Integer = 0 To oDTLab_Groups.Rows.Count - 1
                                Groupid = oDTLab_Groups.Rows(j)("lm_test_ID")
                                GroupName = oDTLab_Groups.Rows(j)("lm_test_Name")
                                'fill the oDTLab_Tests table by data (id,description) from the LM_test table 
                                'where LM_Test(TestGroupFlag = 'T' and CategoryID = LM_Category(i) and groupNo = LM_Test(j).id)
                                ODB.DBParameters.Clear()
                                ODB.DBParameters.Add("@id", CategoryID, ParameterDirection.Input, SqlDbType.BigInt)
                                ODB.DBParameters.Add("@Groupid", Groupid, ParameterDirection.Input, SqlDbType.BigInt)
                                oDTLab_Tests = ODB.ReadData("DM_SelectGroupWiseLabTests")
                                If (IsNothing(oDTLab_Tests) = False) Then
                                    If (IsNothing(dv) = False) Then
                                        dv.Dispose()
                                        dv = Nothing
                                    End If
                                    dv = New DataView(oDTLab_Tests)
                                End If
                            Next
                            oDTLab_Groups.Dispose()
                            oDTLab_Groups = Nothing
                        End If

                    Next
                    oDTLab_Category.Dispose()
                    oDTLab_Category = Nothing
                End If

                'select nTemplateID AS TemplateID , sTemplateName AS TemplateName
                If (IsNothing(dv) = False) Then
                    dv.RowFilter = "lm_test_Name Like '%" & strSearch & "%'"
                End If


            ElseIf pnlbtnDrugs.Dock = DockStyle.Top Then
                Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
                If txtsearchAssociates.Text.Trim.Length >= 0 Then
                    ''''To get the drugs with first character in search textbox
                    oDM.Drugs(txtsearchAssociates.Text.Trim.ToLower)
                    FillDrugs()
                    'Else
                    ''''''''''code to select the drug with name greater than one character string
                    Dim mychildnode As TreeNode
                    'child node collection
                    For Each mychildnode In trAssociates.Nodes.Item(0).Nodes
                        Dim str As String
                        str = UCase(mychildnode.Text)
                        If Mid(str, 1, Len(Trim(txtsearchAssociates.Text))) = UCase(Trim(txtsearchAssociates.Text)) Then
                            trAssociates.SelectedNode = trAssociates.Nodes.Item(0).Nodes(trAssociates.Nodes.Item(0).Nodes.Count - 1)
                            trAssociates.SelectedNode = mychildnode
                            txtsearchAssociates.Focus()
                            If (IsNothing(oDM) = False) Then
                                oDM.Dispose()
                                oDM = Nothing
                            End If
                            Exit Sub
                        End If
                    Next
                End If
                If (IsNothing(oDM) = False) Then
                    oDM.Dispose()
                    oDM = Nothing
                End If
            End If

            'tdt = New DataTable
            If (IsNothing(dv) = False) Then


                If dv.Count > 0 Then
                    tdt = dv.ToTable

                    'add the nodes to treenode
                    trAssociates.BeginUpdate()

                    trAssociates.Nodes(0).Nodes.Clear()
                    If type = 0 Then
                        ''''Add Template Category
                        myCategoryNode = New myTreeNode("Referral Letter", 0)
                        trAssociates.Nodes.Item(0).Nodes.Add(myCategoryNode)
                        myCategoryNode.ImageIndex = 3
                        myCategoryNode.SelectedImageIndex = 3
                    End If
                    trAssociates.Visible = False

                    For i = 0 To tdt.Rows.Count - 1
                        'Dim mychildnode As myTreeNode
                        If type = 0 Then '''' Template
                            ''''Add Template Node
                            Dim myAssociateNode As myTreeNode
                            myAssociateNode = New myTreeNode(tdt.Rows(i)("sTemplateName"), tdt.Rows(i)("nTemplateID"))
                            myAssociateNode.ImageIndex = 4
                            myAssociateNode.SelectedImageIndex = 4
                            myCategoryNode.Nodes.Add(myAssociateNode)
                        ElseIf type = 3 Then ''''Orders

                            Dim myAssociateNode As myTreeNode
                            myAssociateNode = New myTreeNode(tdt.Rows(i)("lm_test_Name"), tdt.Rows(i)("lm_test_ID"))
                            myAssociateNode.ImageIndex = 4
                            myAssociateNode.SelectedImageIndex = 4
                            trAssociates.Nodes.Item(0).Nodes.Add(myAssociateNode)
                        End If
                    Next
                    trAssociates.Visible = True
                    trAssociates.ExpandAll()
                    trAssociates.Select()
                    trAssociates.SelectedNode = trAssociates.Nodes.Item(0)
                    txtsearchAssociates.Focus()
                    tdt.Dispose()
                    tdt = Nothing
                End If
            End If
            trAssociates.Visible = True
            trAssociates.ExpandAll()
            trAssociates.Select()
            trAssociates.SelectedNode = trAssociates.Nodes.Item(0)
            txtsearchAssociates.Focus()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            trAssociates.EndUpdate()
            ODB.Dispose()
            ODB = Nothing
        End Try
    End Sub
    '-----------------------------------------------------------------------
    Private Sub FillLabTreeView(ByVal strSearchDetails As String)

        Dim ODB As New gloStream.gloDataBase.gloDataBase

        Dim odtl_Test As DataTable
        '      Dim odtl_Result As New DataTable
        'Dim TestID As Long
        'Dim Name As String
        'Dim ResultId As Long
        'Dim ResultName As String
        ODB.Connect(GetConnectionString)

        Dim strSelectQryTest As String = "SELECT DISTINCT labtm_Name,labtm_id FROM Lab_Test_Mst"
        odtl_Test = ODB.ReadQueryDataTable(strSelectQryTest)
        ODB.Dispose()
        ODB = Nothing

        If IsNothing(odtl_Test) = False Then
            Dim rootnode As myTreeNode = Nothing
            trAssociates.Nodes.Clear()
            rootnode = New myTreeNode("Orders & Results", -1)
            rootnode.ImageIndex = 0
            rootnode.SelectedImageIndex = 0
            trAssociates.Nodes.Add(rootnode)



            Dim dv As DataView
            dv = odtl_Test.DefaultView

            dv.RowFilter = dv.Table.Columns("labtm_Name").ColumnName & " Like '%" & Trim(strSearchDetails) & "%'"


            Dim dt1 As DataTable
            dt1 = dv.ToTable

            Dim i As Integer

            For i = 0 To dt1.Rows.Count - 1
                Dim mychildnode As myTreeNode
                mychildnode = New myTreeNode(dt1.Rows(i)("labtm_Name"), dt1.Rows(i)("labtm_id"))
                'trAssociates.Nodes.Item(0).Nodes.Add(New myTreeNode(dt1.Rows(i)("labtm_Name"), dt1.Rows(i)("labtm_id")))  ' dt1.Rows(i)(2), dt1.Rows(i)("CPTID"), CType(dt1.Rows(i)("sDescription"), String)))
                trAssociates.Nodes.Item(0).Nodes.Add(mychildnode)
                mychildnode.ImageIndex = 4
                mychildnode.SelectedImageIndex = 4

                'trICD9.Nodes.Item(0).Nodes.Add(mychildnode)
            Next
            dt1.Dispose()
            dt1 = Nothing
            dv.Dispose()
            dv = Nothing
            If trAssociates.Nodes(0).GetNodeCount(False) > 0 Then
                trAssociates.SelectedNode = trAssociates.Nodes(0).Nodes(0)
                trAssociates.SelectedNode = trAssociates.SelectedNode.LastNode
                trAssociates.SelectedNode = trAssociates.Nodes(0).Nodes(0)
                '*************
                ''select matching node
                'trICD9.SelectedNode = mychildnode
                txtsearchAssociates.Focus()
            End If
            odtl_Test.Dispose()
            odtl_Test = Nothing
        End If
        trAssociates.ExpandAll()
        trAssociates.Show()
        trAssociates.Select()
        txtsearchAssociates.Focus()

    End Sub


    Private Sub FillOrderTreeView(ByVal dt As DataTable, ByVal strSearchDetails As String)

        If IsNothing(dt) = False Then
            trvOrderset.Hide()
            trvOrderset.Nodes.Clear()
            Dim rootnode As TreeNode
            rootnode = New myTreeNode("OrderSet", -1)
            rootnode.ImageIndex = 6
            rootnode.SelectedImageIndex = 6
            trvOrderset.Nodes.Add(rootnode)

            Dim dv As DataView
            dv = dt.DefaultView

            dv.RowFilter = dv.Table.Columns("sDescription").ColumnName & " Like '%" & Trim(strSearchDetails) & "%'"
            Dim dt1 As DataTable
            dt1 = dv.ToTable

            Dim i As Integer

            For i = 0 To dt1.Rows.Count - 1
                Dim mychildnode As myTreeNode
                mychildnode = New myTreeNode(dt1.Rows(i)(1), dt1.Rows(i)(0), CType(dt1.Rows(i)(2), String))
                trvOrderset.Nodes.Item(0).Nodes.Add(mychildnode)
                mychildnode.ImageIndex = 5
                mychildnode.SelectedImageIndex = 5
            Next
            dt1.Dispose()
            dt1 = Nothing
            dv.Dispose()
            dv = Nothing
            If trvOrderset.Nodes(0).GetNodeCount(False) > 0 Then
                trvOrderset.SelectedNode = trvOrderset.Nodes(0).Nodes(0)
                trvOrderset.SelectedNode = trvOrderset.SelectedNode.LastNode
                trvOrderset.SelectedNode = trvOrderset.Nodes(0).Nodes(0)
                txtsearchOrderset.Focus()
            End If

        End If
        trvOrderset.ExpandAll()
        trvOrderset.Show()
        trvOrderset.Select()


    End Sub
    '''''Following code lines are addded by Anil 0n 27/09/07 at 10:45 a.m.
    '''''This code clears the search textboxes, gets the focus on the root of the TreeViews  on click of Refresh button.
    Private Sub tblRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tblRefresh.Click
        txtsearchAssociates.Clear()
        txtsearchOrderset.Clear()

        trAssociates.BeginUpdate()
        trOrderAssociation.BeginUpdate()
        trvOrderset.BeginUpdate()

        trAssociates.CollapseAll()
        trAssociates.ExpandAll()
        trOrderAssociation.CollapseAll()
        trOrderAssociation.ExpandAll()
        trvOrderset.CollapseAll()
        trvOrderset.Focus()
        trvOrderset.ExpandAll()

        trAssociates.EndUpdate()
        trOrderAssociation.EndUpdate()
        trvOrderset.EndUpdate()

        If Not IsNothing(trOrderAssociation) Then
            trOrderAssociation.SelectedNode = trOrderAssociation.Nodes.Item(0)
        End If

        txtsearchOrderset.Select()
        '''''upto here the code is added
    End Sub

    Private Sub trAssociates_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trAssociates.NodeMouseDoubleClick
        Try
            If trOrderAssociation.Nodes(0).Nodes.Count = 0 Then
                Exit Sub
            End If
            trAssociates.SelectedNode = e.Node

            Dim mynode As myTreeNode
            mynode = CType(trAssociates.SelectedNode, myTreeNode)

            Dim targetnode As myTreeNode
            targetnode = CType(trOrderAssociation.SelectedNode, myTreeNode)
            'check if selected node is rootnode
            ''commented Sandip Darade 20090402
            'If targetnode.Text = "Labs" Or targetnode.Text = "Orders and Results" Or targetnode.Text = "Referral Letter Template" Or targetnode.Parent.Text = "Labs" Or targetnode.Parent.Text = "Orders and Results" Or targetnode.Parent.Text = "Referral Letter Template" Then
            '    MessageBox.Show("Please select Order", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    'Exit Sub
            'End If
            ''Sandip Darade 20090402
            ''If tslected node is not an order (rootnode)
            If targetnode.Level <> 1 Then
                MessageBox.Show("Select Orders", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If Not IsNothing(mynode) Then
                If Not mynode Is trAssociates.Nodes(0) Then
                    ''Add nodes to the reuired Orders node
                    'AddAssociates(mynode, mynode.Parent.Text, targetnode.Text)
                End If

            End If
            trAssociates.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Orders Association", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDrugs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrugs.Click
        Try
            _IsLabs = False
            PopulateAssociates(5)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Public Sub FillLabTest()
        ''''Create object for the class
        Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
        Dim dtLabsModule As DataTable
        dtLabsModule = oDM.LabModuleTest
        GloUC_trvAssociates.Clear()
        If Not dtLabsModule Is Nothing Then
            GloUC_trvAssociates.DataSource = dtLabsModule
            GloUC_trvAssociates.ValueMember = dtLabsModule.Columns(1).ColumnName
            GloUC_trvAssociates.DescriptionMember = dtLabsModule.Columns(0).ColumnName
            GloUC_trvAssociates.CodeMember = dtLabsModule.Columns(0).ColumnName
            GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
            GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
            GloUC_trvAssociates.FillTreeView()
        End If
        oDM.Dispose()
        oDM = Nothing
    End Sub
    Private Sub FillOrderSet_New()
        ''''Send O to retrive Odrerset
        Dim dtOrderset As DataTable
        If (IsNothing(objSmartOrderDBLayer) = False) Then
            objSmartOrderDBLayer.Dispose()
            objSmartOrderDBLayer = Nothing
        End If
        objSmartOrderDBLayer = New ClsSmartorderDBLayer()
        dtOrderset = objSmartOrderDBLayer.FillControl(0)
        GloUC_trvAssociates.Clear()
        If Not dtOrderset Is Nothing Then
            GloUC_trvOrderSet.DataSource = dtOrderset
            ' GloUC_trvOrderSet.ParentMember = dtOrderset.Columns("sCategoryType").ColumnName
            GloUC_trvOrderSet.ValueMember = dtOrderset.Columns("nCategoryID").ColumnName
            GloUC_trvOrderSet.Tag = dtOrderset.Columns("nCategoryID").ColumnName
            GloUC_trvOrderSet.DescriptionMember = dtOrderset.Columns("sDescription").ColumnName
            GloUC_trvOrderSet.CodeMember = dtOrderset.Columns("nCategoryID").ColumnName
            GloUC_trvOrderSet.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
            GloUC_trvOrderSet.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
            GloUC_trvOrderSet.FillTreeView()
        End If
    End Sub
    Private Sub fill_Drugs_new()
        'Dim oDrugs As New gloStream.DiseaseManagement.Supporting.Drugs
        'Dim oDM As New gloStream.DiseaseManagement.Common.Criteria
        'Dim oNode As myTreeNode = Nothing
        Dim dtdrugs As DataTable
        'Dim objclsDrugs As New clsDrugs
        Dim objorderAssociationDBLayer As New ClsICD9AssociationDBLayer

        Try
            Dim strsearch As String = ""
            ''Sandip Darade 20091014
            If Not IsNothing(GloUC_trvAssociates.txtsearch.Text) Then
                strsearch = GloUC_trvAssociates.txtsearch.Text
            End If

            If gblnResetSearchTextBox = True Then
                strsearch = ""
            End If
            dtdrugs = objorderAssociationDBLayer.FillControls(0, strsearch)

           

            'Sandip Darade  20091014
            ''Abode code comented to replace it with code below 
            If Not IsNothing(dtdrugs) Then
                GloUC_trvAssociates.Clear()
                GloUC_trvAssociates.DataSource = dtdrugs
                ''Sandip Darade 20091014  if drugs to be filled in the treview 
                GloUC_trvAssociates.IsDrug = True
                GloUC_trvAssociates.DrugFlag = 16 ''For all drugs 
                GloUC_trvAssociates.ValueMember = dtdrugs.Columns("DrugsID").ColumnName
                GloUC_trvAssociates.DescriptionMember = dtdrugs.Columns("Dosage").ColumnName
                GloUC_trvAssociates.CodeMember = dtdrugs.Columns("DrugName").ColumnName
                GloUC_trvAssociates.DrugFormMember = dtdrugs.Columns("DrugForm").ColumnName
                GloUC_trvAssociates.RouteMember = Convert.ToString(dtdrugs.Columns("sRoute").ColumnName)
                GloUC_trvAssociates.NDCCodeMember = Convert.ToString(dtdrugs.Columns("sNDCCode").ColumnName) ''''bug fixed for 6851
                GloUC_trvAssociates.IsNarcoticsMember = dtdrugs.Columns("nIsNarcotics").ColumnName
                GloUC_trvAssociates.FrequencyMember = dtdrugs.Columns("sFrequency").ColumnName
                GloUC_trvAssociates.DurationMember = dtdrugs.Columns("sDuration").ColumnName
                GloUC_trvAssociates.DrugQtyQualifierMember = dtdrugs.Columns("sDrugQtyQualifier").ColumnName
                GloUC_trvAssociates.mpidmember = dtdrugs.Columns("mpid").ColumnName
                GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Simple
                'Display Type Changed by Mayuri:20091008
                'To display drugs in form of DrugName and Drug Form
                GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description_DrugForm
                GloUC_trvAssociates.FillTreeView()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objorderAssociationDBLayer.Dispose()
            objorderAssociationDBLayer = Nothing
        End Try
    End Sub
    Public Sub FillRadiologyTest()
        Try
            Dim dtOrders As DataTable
            Dim oCls As New gloStream.DiseaseManagement.Common.Criteria
            Try
                dtOrders = oCls.OrdersTable
                If Not IsNothing(dtOrders) Then
                    GloUC_trvAssociates.Clear()
                    GloUC_trvAssociates.DataSource = dtOrders
                    GloUC_trvAssociates.CodeMember = Convert.ToString(dtOrders.Columns(1).ColumnName)
                    GloUC_trvAssociates.ValueMember = Convert.ToString(dtOrders.Columns(0).ColumnName)
                    GloUC_trvAssociates.DescriptionMember = Convert.ToString(dtOrders.Columns(1).ColumnName)
                    GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
                    GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
                    GloUC_trvAssociates.FillTreeView()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                oCls.Dispose()
                oCls = Nothing
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub FillTemplates_New()
        Try
            Dim rootnode As myTreeNode = Nothing
            'Dim newNode As New TreeNode
            'Dim objMyTreeView As myTreeNode
            Dim objTemplateGallery As New clsTemplateGallery
            'Dim objCategory As myTreeNode
            'Dim objTemplate As myTreeNode
            Dim dvTemplate As DataView
            Dim dt_temp As DataTable = objTemplateGallery.GetAllCategory
            'Dim j As Integer
            Dim ValueMember As Int64
            Dim DisplayMember As String
            If (IsNothing(dt_temp) = False) Then


                For i As Integer = 0 To dt_temp.Rows.Count - 1

                    ValueMember = dt_temp.Rows(i)(0)
                    DisplayMember = dt_temp.Rows(i)(1)
                    If DisplayMember = "Referral Letter" Then
                        '  objCategory = New myTreeNode(DisplayMember, ValueMember)

                        '                       dvTemplate = New DataView
                        dvTemplate = objTemplateGallery.GetAllTemplateGallery(ValueMember)
                        Dim dt As DataTable
                        dt = dvTemplate.Table
                        If Not dt Is Nothing Then
                            GloUC_trvAssociates.Clear()
                            GloUC_trvAssociates.DataSource = dt
                            GloUC_trvAssociates.ValueMember = dt.Columns(0).ColumnName
                            GloUC_trvAssociates.DescriptionMember = dt.Columns(1).ColumnName
                            GloUC_trvAssociates.CodeMember = dt.Columns(1).ColumnName
                            GloUC_trvAssociates.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code
                            GloUC_trvAssociates.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
                            GloUC_trvAssociates.FillTreeView()
                        End If

                    End If
                Next
                dt_temp.Dispose()
                dt_temp = Nothing
 
            End If
            objTemplateGallery.Dispose()
            objTemplateGallery = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''20090909
    ''Added by Mayuri to show contextMenu "Add Order Set"

    Private Sub GloUC_trvOrderSet_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GloUC_trvOrderSet.MouseDown
        Try
            If (IsNothing(cmnuAddOrderSet) = False) Then
                gloGlobal.cEventHelper.RemoveAllEventHandlers(cmnuAddOrderSet)
                If (IsNothing(cmnuAddOrderSet.Items) = False) Then
                    cmnuAddOrderSet.Items.Clear()
                End If
                cmnuAddOrderSet.Dispose()
                cmnuAddOrderSet = Nothing
            End If
        Catch

        End Try
        cmnuAddOrderSet = New ContextMenuStrip
        Dim MenuItem As New ToolStripMenuItem
        '= New ToolStripMenuItem
        MenuItem.Text = "Add Order Set"
        MenuItem.ForeColor = Color.FromArgb(31, 73, 125)
        cmnuAddOrderSet.Items.Add(MenuItem)
        Try
            If (IsNothing(GloUC_trvOrderSet.ContextMenuStrip) = False) Then
                gloGlobal.cEventHelper.RemoveAllEventHandlers(GloUC_trvOrderSet.ContextMenuStrip)
                If (IsNothing(GloUC_trvOrderSet.ContextMenuStrip.Items) = False) Then
                    GloUC_trvOrderSet.ContextMenuStrip.Items.Clear()
                End If
                GloUC_trvOrderSet.ContextMenuStrip.Dispose()
                GloUC_trvOrderSet.ContextMenuStrip = Nothing
            End If
        Catch

        End Try
        'Try
        '    If (IsNothing(GloUC_trvOrderSet.ContextMenuStrip) = False) Then
        '        GloUC_trvOrderSet.ContextMenuStrip.Dispose()
        '        GloUC_trvOrderSet.ContextMenuStrip = Nothing
        '    End If
        'Catch ex As Exception

        'End Try
        GloUC_trvOrderSet.ContextMenuStrip = cmnuAddOrderSet

        AddHandler MenuItem.Click, AddressOf AddOrderSet
        MenuItem = Nothing
        'cmnuAddOrderSet = Nothing

        ''end code
    End Sub
    ''20090909
    ''code Added by Mayuri
    Private Sub AddOrderSet(ByVal sender As Object, ByVal e As EventArgs)

        Dim frm As New CategoryMaster
        Dim nd As TreeNode
        Try
            frm.Text = "Add Order Set"

            '27-May-13 Aniket: Resolving Bug 51426
            frm.IsFromOrderSet = True
            'frm.cmbCategoryType.Text = "OrderSet"
            frm.cmbCategoryType.Enabled = False
            'frm.Label4.Visible = False
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            'GloUC_trvOrderSet.Nodes.Clear()
            FillOrderSet_New()


            GloUC_trvOrderSet.ExpandAll()
            'for each loop is used for select resently added category
            'if condition added by dipak 20091110 to fix bug no :5087:-Exam ->Smart order-Order Association
            If (GloUC_trvOrderSet.Nodes.Count > 0) Then
                For Each nd In GloUC_trvOrderSet.Nodes(0).Nodes
                    If (nd.Text = frm.txtCategoryDesc.Text) Then
                        GloUC_trvOrderSet.SelectedNode = nd
                        Exit For
                    End If
                Next
            End If
            'end of if condition added by dipak 20091110
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If
        End Try
        ''end code
    End Sub


    Private Sub GloUC_trvOrderSet_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvOrderSet.NodeMouseDoubleClick
        Try
            Dim oNode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)
            Dim mynode As New myTreeNode

            mynode.Key = oNode.ID
            mynode.Text = oNode.Text
            If Not IsNothing(mynode) Then
                AddNode(mynode)
                'AddNode_1(mynode)
            End If
            'selectedTreeview.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvOrderSet_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvOrderSet.KeyPress
        Try
            Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvOrderSet.SelectedNode, gloUserControlLibrary.myTreeNode)
            Dim mynode As New myTreeNode
            If Not IsNothing(oNode) Then
                mynode.Key = oNode.ID
                mynode.Text = oNode.Text
                If Not IsNothing(mynode) Then
                    AddNode(mynode)
                    'AddNode_1(mynode)
                End If

            End If
            'selectedTreeview.ExpandAll()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GloUC_trvAssociates_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles GloUC_trvAssociates.KeyPress

        Try
            Dim mynode As New myTreeNode
            Dim targetnode1 As myTreeNode
            Dim oNode As gloUserControlLibrary.myTreeNode = CType(GloUC_trvAssociates.SelectedNode, gloUserControlLibrary.myTreeNode)

            targetnode1 = CType(trOrderAssociation.SelectedNode, myTreeNode)

            '25-Sep-15 Aniket: Resolving Bug #88525: (getDate): gloEMR: Flowsheet: Application gives exception on smart order 
            If IsNothing(oNode) = False Then
                mynode.Key = oNode.ID
                If (pnlbtnDrugs.Dock = DockStyle.Top) Then
                    If (oNode.Description <> "") Then ' IF DOSAGE FIELD IS NOT EMPTY
                        mynode.Text = oNode.Code & " - " & oNode.Description & " - " & oNode.DrugForm
                    Else
                        mynode.Text = oNode.Code & " - " & oNode.DrugForm
                    End If
                    mynode.DrugName = oNode.Code
                    mynode.DrugForm = oNode.DrugForm
                    mynode.Route = oNode.Route
                    mynode.Frequency = oNode.Frequency
                    mynode.NDCCode = oNode.NDCCode
                    mynode.IsNarcotics = oNode.IsNarcotics
                    mynode.Duration = oNode.Duration
                    mynode.mpid = oNode.mpid
                    mynode.DrugQtyQualifier = oNode.DrugQtyQualifier
                Else
                    mynode.Text = oNode.Text
                End If


                'check if selected node is rootnode
                If Not IsNothing(targetnode1) AndAlso Not IsNothing(mynode) Then
                    AddAssociates(mynode, strParentToAssociate, targetnode1.Text, targetnode1.Parent.Text)
                End If

            End If

            If Not IsNothing(mynode) Then
                mynode.Dispose()
                mynode = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub





    Private Sub GloUC_trvAssociates_NodeMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles GloUC_trvAssociates.NodeMouseDoubleClick
        Try
            Dim mynode As New myTreeNode
            Dim targetnode1 As myTreeNode
            Dim oNode As gloUserControlLibrary.myTreeNode = CType(e.Node, gloUserControlLibrary.myTreeNode)
            targetnode1 = CType(trOrderAssociation.SelectedNode, myTreeNode)
            mynode.Key = oNode.ID
            mynode.Text = oNode.Text
            mynode.Route = oNode.Route
            mynode.Frequency = oNode.Frequency
            mynode.Duration = oNode.Duration
            mynode.DrugForm = oNode.DrugForm
            mynode.IsNarcotics = oNode.IsNarcotics
            mynode.mpid = oNode.mpid
            mynode.DrugQtyQualifier = oNode.DrugQtyQualifier
            mynode.NDCCode = oNode.NDCCode
            mynode.DrugName = oNode.Code
            'Added By Rahul Patel on 19-11-2010
            'For Resolving case no :GLO2010-0006371 i.e Not showing Dosage While Adding SmtOrder from New Exam or Past Exam.
            mynode.Dosage = oNode.Description

            'check if selected node is rootnode
            If Not IsNothing(targetnode1) AndAlso Not IsNothing(mynode) Then
                If Not IsNothing(targetnode1.Parent) Then
                    ''Sanjog- Added on 20101124 to Inset the child node in parent
                    If CType(targetnode1.Parent, myTreeNode).Key = -1 And targetnode1.Level > 1 Then
                        targetnode1 = targetnode1.Parent
                    End If
                    ''Sanjog- Added on 20101124 to Inset the child node in parent
                    AddAssociates(mynode, strParentToAssociate, targetnode1.Text, targetnode1.Parent.Text)
                    'Shubhangi 20091208
                    'Check the setting Reset search text box after assiging category
                    If gblnResetSearchTextBox = True Then
                        GloUC_trvAssociates.txtsearch.ResetText()
                    End If
                End If
            End If
            If Not IsNothing(mynode) Then
                mynode.Dispose()
                mynode = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub trvOrderset_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvOrderset.MouseDown

    End Sub

    Private Sub trOrderAssociation_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)

    End Sub
    '' Mayuri- Event handled on 20090910 
    Private Sub GloUC_trvOrderSet_NodeAdded(ByVal ChildNode As gloUserControlLibrary.myTreeNode) Handles GloUC_trvOrderSet.NodeAdded

        Try
            Dim dtAssociation As DataTable
            '' To Get Already Associated Template with Selected CPT
            If (IsNothing(objSmartOrderDBLayer)) Then
                objSmartOrderDBLayer = New ClsSmartorderDBLayer
            End If
            dtAssociation = objSmartOrderDBLayer.FetchOrderforUpdate(ChildNode.Tag)
            '' If Association found then change the Image of Treenode 
            If Not IsNothing(dtAssociation) Then
                If dtAssociation.Rows.Count > 0 Then
                    ChildNode.ImageIndex = 5
                    ChildNode.SelectedImageIndex = 5
                End If
                dtAssociation.Dispose()
                dtAssociation = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trOrderAssociation_AfterCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trOrderAssociation.AfterCheck
        Try
            ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101012
            If bChildTrigger Then
                CheckAllChildren(e.Node, e.Node.Checked)
            End If
            If bParentTrigger Then
                CheckMyParent(e.Node, e.Node.Checked)
            End If
            ''''''''''''''' Added by Ujwala - Smart Order Changes Integration - as on 20101012
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnFlowsheet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFlowsheet.Click
        Try
            _IsLabs = False
            PopulateAssociates(6) 'Flow Sheet
            GloUC_trvAssociates.txtsearch.Text = ""
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnFlowsheet_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFlowsheet.MouseHover
        btnFlowsheet.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongYellow
        btnFlowsheet.BackgroundImageLayout = ImageLayout.Stretch

        If (IsNothing(tooltipnew)) Then
            tooltipnew = New ToolTip
        End If
        tooltipnew.SetToolTip(btnTemplates, "Flowsheet")
    End Sub

    Private Sub btnFlowsheet_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFlowsheet.MouseLeave
        If pnlbtnFlowsheet.Dock = DockStyle.Bottom Then
            btnFlowsheet.BackgroundImage = gloEMR.My.Resources.Resources.Img_LongButton
            btnFlowsheet.BackgroundImageLayout = ImageLayout.Stretch
        Else
            btnFlowsheet.BackgroundImage = Global.gloEMR.My.Resources.Img_LongOrange
            btnFlowsheet.BackgroundImageLayout = ImageLayout.Stretch
        End If
    End Sub

    Private Sub trOrderAssociation_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trOrderAssociation.NodeMouseClick
        Try
            Dim ev As System.EventArgs = Nothing
            Select Case e.Node.Text

                Case "Drugs"

                    btnDrugs_Click(sender, ev)

                Case "Referral Letter"

                    btnTemplates_Click(sender, ev)

                Case "FlowSheet"
                    btnFlowsheet_Click(sender, ev)

                Case "Order Templates"
                    btnRadiology_Click(sender, ev)

                Case "Orders & Results"
                    btnLabs_Click(sender, ev)
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''Sanjog- Added on 2011 Jan 17 to show the context menu on Property key press 
    Private Sub trOrderAssociation_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trOrderAssociation.KeyDown
        Try
            If e.KeyCode = Keys.Apps Then
                If Not IsNothing(trOrderAssociation.SelectedNode) Then
                    If trOrderAssociation.Nodes.Item(0).Text = trOrderAssociation.SelectedNode.Text Or trOrderAssociation.SelectedNode.Parent Is trOrderAssociation.Nodes.Item(0) Or (CType(trOrderAssociation.SelectedNode, myTreeNode).Key = -1) Then
                        'Try
                        '    If (IsNothing(trOrderAssociation.ContextMenu) = False) Then
                        '        trOrderAssociation.ContextMenu.Dispose()
                        '        trOrderAssociation.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trOrderAssociation.ContextMenu = Nothing
                    Else
                        'Try
                        '    If (IsNothing(trOrderAssociation.ContextMenu) = False) Then
                        '        trOrderAssociation.ContextMenu.Dispose()
                        '        trOrderAssociation.ContextMenu = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        trOrderAssociation.ContextMenu = cntICD9Association
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''Sanjog- Added on 2011 Jan 17 to show the context menu on Property key press 
    'Code Start added by kanchan on 20120102 for gloCommunity integration
    Private Sub ts_gloCommunityDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_gloCommunityDownload.Click
        If CheckUser() = False Then ''Added for fixed Bug # : 35658 on 20120904
            Dim FrmgloCommunityViewData As New gloCommunity.Forms.gloCommunityViewData("Order", "Download")
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
