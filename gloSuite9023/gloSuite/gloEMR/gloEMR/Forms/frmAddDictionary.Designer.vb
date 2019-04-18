<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddDictionary
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            Dim CmpControls() As System.Windows.Forms.ContextMenuStrip = {GloUC_trvDataDictionary.ContextMenuStrip}
            Dim CmppControls() As System.Windows.Forms.ContextMenu = {GloUC_trvDataDictionary.ContextMenu}
            components.Dispose()

            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try



            If (IsNothing(CmpControls) = False) Then
                If CmpControls.Length > 0 Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(CmpControls)
                End If
            End If



            If (IsNothing(CmpControls) = False) Then
                If CmpControls.Length > 0 Then
                    gloGlobal.cEventHelper.DisposeContextMenuStrip(CmpControls)
                End If
            End If


            If (IsNothing(CmppControls) = False) Then
                If CmppControls.Length > 0 Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(CmppControls)
                End If
            End If

            If (IsNothing(CmppControls) = False) Then
                If CmppControls.Length > 0 Then
                    gloGlobal.cEventHelper.DisposeContextMenu(CmppControls)
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddDictionary))
        Me.pnlflexgrid = New System.Windows.Forms.Panel
        Me.pnlSearch = New System.Windows.Forms.Panel
        Me.txtsearch = New System.Windows.Forms.TextBox
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label
        Me.PicBx_Search = New System.Windows.Forms.PictureBox
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label
        Me.GloUC_trvDataDictionary = New gloUserControlLibrary.gloUC_TreeView
        Me.imgTemplate = New System.Windows.Forms.ImageList(Me.components)
        Me.PanelEx12 = New DevComponents.DotNetBar.PanelEx
        Me.chckCaption = New System.Windows.Forms.CheckBox
        Me.pnl_tls_ = New System.Windows.Forms.Panel
        Me.tlsDictionary = New gloGlobal.gloToolStripIgnoreFocus
        Me.btn_tls_Ok = New System.Windows.Forms.ToolStripButton
        Me.btn_tls_Cancel = New System.Windows.Forms.ToolStripButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.pnlflexgrid.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_tls_.SuspendLayout()
        Me.tlsDictionary.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlflexgrid
        '
        Me.pnlflexgrid.BackColor = System.Drawing.Color.Transparent
        Me.pnlflexgrid.Controls.Add(Me.pnlSearch)
        Me.pnlflexgrid.Controls.Add(Me.GloUC_trvDataDictionary)
        Me.pnlflexgrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlflexgrid.Location = New System.Drawing.Point(0, 83)
        Me.pnlflexgrid.Name = "pnlflexgrid"
        Me.pnlflexgrid.Padding = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.pnlflexgrid.Size = New System.Drawing.Size(520, 340)
        Me.pnlflexgrid.TabIndex = 1
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
        Me.pnlSearch.Location = New System.Drawing.Point(42, 191)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlSearch.Size = New System.Drawing.Size(403, 29)
        Me.pnlSearch.TabIndex = 19
        Me.pnlSearch.Visible = False
        '
        'txtsearch
        '
        Me.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsearch.Location = New System.Drawing.Point(26, 8)
        Me.txtsearch.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.txtsearch.Name = "txtsearch"
        Me.txtsearch.Size = New System.Drawing.Size(373, 15)
        Me.txtsearch.TabIndex = 4
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(26, 4)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(373, 4)
        Me.lbl_WhiteSpaceTop.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(26, 23)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(373, 2)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'PicBx_Search
        '
        Me.PicBx_Search.BackColor = System.Drawing.Color.White
        Me.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicBx_Search.Image = CType(resources.GetObject("PicBx_Search.Image"), System.Drawing.Image)
        Me.PicBx_Search.Location = New System.Drawing.Point(4, 4)
        Me.PicBx_Search.Name = "PicBx_Search"
        Me.PicBx_Search.Size = New System.Drawing.Size(22, 21)
        Me.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicBx_Search.TabIndex = 9
        Me.PicBx_Search.TabStop = False
        '
        'lbl_pnlSearchBottomBrd
        '
        Me.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlSearchBottomBrd.Location = New System.Drawing.Point(4, 25)
        Me.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd"
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(395, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(4, 3)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(395, 1)
        Me.lbl_pnlSearchTopBrd.TabIndex = 36
        Me.lbl_pnlSearchTopBrd.Text = "label1"
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(399, 3)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'GloUC_trvDataDictionary
        '
        Me.GloUC_trvDataDictionary.BackColor = System.Drawing.Color.Transparent
        Me.GloUC_trvDataDictionary.CheckBoxes = True
        Me.GloUC_trvDataDictionary.CodeMember = Nothing
        Me.GloUC_trvDataDictionary.DescriptionMember = Nothing
        Me.GloUC_trvDataDictionary.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation
        Me.GloUC_trvDataDictionary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_trvDataDictionary.DrugFlag = CType(16, Short)
        Me.GloUC_trvDataDictionary.DrugFormMember = Nothing
        Me.GloUC_trvDataDictionary.DrugQtyQualifierMember = Nothing
        Me.GloUC_trvDataDictionary.DurationMember = Nothing
        Me.GloUC_trvDataDictionary.FrequencyMember = Nothing
        Me.GloUC_trvDataDictionary.ImageIndex = 3
        Me.GloUC_trvDataDictionary.ImageList = Me.imgTemplate
        Me.GloUC_trvDataDictionary.ImageObject = Nothing
        Me.GloUC_trvDataDictionary.IsDrug = False
        Me.GloUC_trvDataDictionary.IsNarcoticsMember = Nothing
        Me.GloUC_trvDataDictionary.Location = New System.Drawing.Point(3, 1)
        Me.GloUC_trvDataDictionary.MaximumNodes = 1000
        Me.GloUC_trvDataDictionary.Name = "GloUC_trvDataDictionary"
        Me.GloUC_trvDataDictionary.NDCCodeMember = Nothing
        Me.GloUC_trvDataDictionary.ParentImageIndex = 0
        Me.GloUC_trvDataDictionary.ParentMember = Nothing
        Me.GloUC_trvDataDictionary.RouteMember = Nothing
        Me.GloUC_trvDataDictionary.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring
        Me.GloUC_trvDataDictionary.SearchBox = True
        Me.GloUC_trvDataDictionary.SearchText = Nothing
        Me.GloUC_trvDataDictionary.SelectedImageIndex = 3
        Me.GloUC_trvDataDictionary.SelectedNode = Nothing
        Me.GloUC_trvDataDictionary.SelectedNodeIDs = CType(resources.GetObject("GloUC_trvDataDictionary.SelectedNodeIDs"), System.Collections.ArrayList)
        Me.GloUC_trvDataDictionary.SelectedParentImageIndex = 0
        Me.GloUC_trvDataDictionary.Size = New System.Drawing.Size(514, 336)
        Me.GloUC_trvDataDictionary.Sort = gloUserControlLibrary.gloUC_TreeView.enumSortType.ByDescription
        Me.GloUC_trvDataDictionary.TabIndex = 1
        Me.GloUC_trvDataDictionary.Tag = Nothing
        Me.GloUC_trvDataDictionary.UnitMember = Nothing
        Me.GloUC_trvDataDictionary.ValueMember = Nothing
        '
        'imgTemplate
        '
        Me.imgTemplate.ImageStream = CType(resources.GetObject("imgTemplate.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTemplate.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTemplate.Images.SetKeyName(0, "Liduid Data.ico")
        Me.imgTemplate.Images.SetKeyName(1, "datadictionary.ico")
        Me.imgTemplate.Images.SetKeyName(2, "SubTemplate.ico")
        Me.imgTemplate.Images.SetKeyName(3, "Bullet06.ico")
        Me.imgTemplate.Images.SetKeyName(4, "Table_04.ico")
        Me.imgTemplate.Images.SetKeyName(5, "Arrow_02.ico")
        Me.imgTemplate.Images.SetKeyName(6, "Small Arrow.ico")
        '
        'PanelEx12
        '
        Me.PanelEx12.CanvasColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.PanelEx12.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.PanelEx12.Location = New System.Drawing.Point(55, 386)
        Me.PanelEx12.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.PanelEx12.Name = "PanelEx12"
        Me.PanelEx12.Size = New System.Drawing.Size(343, 25)
        Me.PanelEx12.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx12.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.PanelEx12.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.PanelEx12.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx12.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx12.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx12.Style.GradientAngle = 90
        Me.PanelEx12.TabIndex = 3
        Me.PanelEx12.Visible = False
        '
        'chckCaption
        '
        Me.chckCaption.BackColor = System.Drawing.Color.Transparent
        Me.chckCaption.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.chckCaption.Dock = System.Windows.Forms.DockStyle.Left
        Me.chckCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.chckCaption.Location = New System.Drawing.Point(6, 1)
        Me.chckCaption.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.chckCaption.Name = "chckCaption"
        Me.chckCaption.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.chckCaption.Size = New System.Drawing.Size(115, 21)
        Me.chckCaption.TabIndex = 29
        Me.chckCaption.Text = "Include Caption"
        Me.chckCaption.UseVisualStyleBackColor = False
        '
        'pnl_tls_
        '
        Me.pnl_tls_.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tls_.Controls.Add(Me.tlsDictionary)
        Me.pnl_tls_.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tls_.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tls_.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnl_tls_.Name = "pnl_tls_"
        Me.pnl_tls_.Size = New System.Drawing.Size(520, 54)
        Me.pnl_tls_.TabIndex = 5
        '
        'tlsDictionary
        '
        Me.tlsDictionary.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tlsDictionary.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlsDictionary.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlsDictionary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsDictionary.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlsDictionary.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_Ok, Me.btn_tls_Cancel})
        Me.tlsDictionary.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlsDictionary.Location = New System.Drawing.Point(0, 0)
        Me.tlsDictionary.Name = "tlsDictionary"
        Me.tlsDictionary.Size = New System.Drawing.Size(520, 53)
        Me.tlsDictionary.TabIndex = 0
        Me.tlsDictionary.Text = "toolStrip1"
        '
        'btn_tls_Ok
        '
        Me.btn_tls_Ok.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Ok.Image = CType(resources.GetObject("btn_tls_Ok.Image"), System.Drawing.Image)
        Me.btn_tls_Ok.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Ok.Name = "btn_tls_Ok"
        Me.btn_tls_Ok.Size = New System.Drawing.Size(66, 50)
        Me.btn_tls_Ok.Tag = "Ok"
        Me.btn_tls_Ok.Text = "&Save&&Cls"
        Me.btn_tls_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_Ok.ToolTipText = "Save and Close"
        '
        'btn_tls_Cancel
        '
        Me.btn_tls_Cancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Cancel.Image = CType(resources.GetObject("btn_tls_Cancel.Image"), System.Drawing.Image)
        Me.btn_tls_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Cancel.Name = "btn_tls_Cancel"
        Me.btn_tls_Cancel.Size = New System.Drawing.Size(43, 50)
        Me.btn_tls_Cancel.Tag = "Cancel"
        Me.btn_tls_Cancel.Text = "&Close"
        Me.btn_tls_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.chckCaption)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(514, 23)
        Me.Panel1.TabIndex = 30
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Location = New System.Drawing.Point(1, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(5, 21)
        Me.Label9.TabIndex = 34
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Location = New System.Drawing.Point(0, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 21)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Location = New System.Drawing.Point(0, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(513, 1)
        Me.Label8.TabIndex = 33
        Me.Label8.Text = "label1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label1.Location = New System.Drawing.Point(513, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1, 22)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "label4"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(514, 1)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "label1"
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(520, 29)
        Me.Panel2.TabIndex = 31
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.Panel2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 54)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(520, 29)
        Me.Panel3.TabIndex = 32
        '
        'frmAddDictionary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(520, 423)
        Me.Controls.Add(Me.pnlflexgrid)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.PanelEx12)
        Me.Controls.Add(Me.pnl_tls_)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddDictionary"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add Data Dictionary Fields"
        Me.pnlflexgrid.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_tls_.ResumeLayout(False)
        Me.pnl_tls_.PerformLayout()
        Me.tlsDictionary.ResumeLayout(False)
        Me.tlsDictionary.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlflexgrid As System.Windows.Forms.Panel
    Friend WithEvents txtsearch As System.Windows.Forms.TextBox
    Friend WithEvents chckCaption As System.Windows.Forms.CheckBox
    Friend WithEvents PanelEx12 As DevComponents.DotNetBar.PanelEx
    Private WithEvents pnl_tls_ As System.Windows.Forms.Panel
    Private WithEvents tlsDictionary As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents btn_tls_Ok As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_Cancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents GloUC_trvDataDictionary As gloUserControlLibrary.gloUC_TreeView
    Friend WithEvents imgTemplate As System.Windows.Forms.ImageList
End Class
