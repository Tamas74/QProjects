<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloUC_CustomSearchInC1Flexgrid
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()

            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try

            If (IsNothing(dvNext) = False) Then
                dvNext.Dispose()
                dvNext = Nothing
            End If
        End If
        MyBase.Dispose(disposing)

        Dim dtpControls As System.Windows.Forms.ContextMenuStrip() = {cntListmenuStrip, cntListmenuStrip1}

        Try
            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
            gloGlobal.cEventHelper.DisposeContextMenuStrip(dtpControls)
        Catch

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloUC_CustomSearchInC1Flexgrid))
        Me.ImagSearchFlex = New System.Windows.Forms.ImageList(Me.components)
        Me.cntListmenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.btnClearSearchFlexGrid = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSearchFlexGrid = New System.Windows.Forms.TextBox()
        Me.lblColName = New System.Windows.Forms.Label()
        Me.pic_OK = New System.Windows.Forms.PictureBox()
        Me.pnl_NextOK = New System.Windows.Forms.Panel()
        Me.pic_ADDNew = New System.Windows.Forms.PictureBox()
        Me.pnl_NextAddNew = New System.Windows.Forms.Panel()
        Me.pic_Modify = New System.Windows.Forms.PictureBox()
        Me.pnl_NextModify = New System.Windows.Forms.Panel()
        Me.pnl_Close = New System.Windows.Forms.Panel()
        Me.pnl_NextClose = New System.Windows.Forms.Panel()
        Me.pnlColName = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblSearchString = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.btnUC_Mod = New System.Windows.Forms.Button()
        Me.btnCloseRefill = New System.Windows.Forms.Button()
        Me.btnUC_Add = New System.Windows.Forms.Button()
        Me.btnUC_OK = New System.Windows.Forms.Button()
        Me.btnUC_Close = New System.Windows.Forms.Button()
        Me._Flex = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnl_Base = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cntListmenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ModifySigInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveDrugToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.label20 = New System.Windows.Forms.Label()
        Me.label21 = New System.Windows.Forms.Label()
        Me.label22 = New System.Windows.Forms.Label()
        Me.label23 = New System.Windows.Forms.Label()
        Me.label24 = New System.Windows.Forms.Label()
        Me.cntListmenuStrip1.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.pic_OK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_ADDNew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_Modify, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_Base.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.cntListmenuStrip.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'ImagSearchFlex
        '
        Me.ImagSearchFlex.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImagSearchFlex.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImagSearchFlex.TransparentColor = System.Drawing.Color.Transparent
        '
        'cntListmenuStrip1
        '
        Me.cntListmenuStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.cntListmenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddToolStripMenuItem, Me.EditToolStripMenuItem})
        Me.cntListmenuStrip1.Name = "cntListmenuStrip"
        Me.cntListmenuStrip1.Size = New System.Drawing.Size(105, 48)
        '
        'AddToolStripMenuItem
        '
        Me.AddToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
        Me.AddToolStripMenuItem.Name = "AddToolStripMenuItem"
        Me.AddToolStripMenuItem.Size = New System.Drawing.Size(104, 22)
        Me.AddToolStripMenuItem.Text = "Add"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.BackColor = System.Drawing.Color.WhiteSmoke
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(104, 22)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearch.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Img_Button
        Me.pnlSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSearch.Controls.Add(Me.panel4)
        Me.pnlSearch.Controls.Add(Me.Label9)
        Me.pnlSearch.Controls.Add(Me.lblColName)
        Me.pnlSearch.Controls.Add(Me.pic_OK)
        Me.pnlSearch.Controls.Add(Me.pnl_NextOK)
        Me.pnlSearch.Controls.Add(Me.pic_ADDNew)
        Me.pnlSearch.Controls.Add(Me.pnl_NextAddNew)
        Me.pnlSearch.Controls.Add(Me.pic_Modify)
        Me.pnlSearch.Controls.Add(Me.pnl_NextModify)
        Me.pnlSearch.Controls.Add(Me.pnl_Close)
        Me.pnlSearch.Controls.Add(Me.pnl_NextClose)
        Me.pnlSearch.Controls.Add(Me.pnlColName)
        Me.pnlSearch.Controls.Add(Me.Label2)
        Me.pnlSearch.Controls.Add(Me.Panel1)
        Me.pnlSearch.Controls.Add(Me.pnlTop)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlRight)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlTop)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearch.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(738, 24)
        Me.pnlSearch.TabIndex = 3
        '
        'btnClearSearchFlexGrid
        '
        Me.btnClearSearchFlexGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearSearchFlexGrid.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearSearchFlexGrid.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearSearchFlexGrid.FlatAppearance.BorderSize = 0
        Me.btnClearSearchFlexGrid.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearSearchFlexGrid.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearSearchFlexGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearSearchFlexGrid.Image = CType(resources.GetObject("btnClearSearchFlexGrid.Image"), System.Drawing.Image)
        Me.btnClearSearchFlexGrid.Location = New System.Drawing.Point(219, 0)
        Me.btnClearSearchFlexGrid.Name = "btnClearSearchFlexGrid"
        Me.btnClearSearchFlexGrid.Size = New System.Drawing.Size(21, 22)
        Me.btnClearSearchFlexGrid.TabIndex = 48
        Me.btnClearSearchFlexGrid.Text = "  "
        Me.btnClearSearchFlexGrid.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(84, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label9.Size = New System.Drawing.Size(4, 20)
        Me.Label9.TabIndex = 49
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSearchFlexGrid
        '
        Me.txtSearchFlexGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchFlexGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchFlexGrid.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchFlexGrid.Location = New System.Drawing.Point(5, 3)
        Me.txtSearchFlexGrid.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtSearchFlexGrid.Name = "txtSearchFlexGrid"
        Me.txtSearchFlexGrid.ShortcutsEnabled = False
        Me.txtSearchFlexGrid.Size = New System.Drawing.Size(214, 15)
        Me.txtSearchFlexGrid.TabIndex = 3
        '
        'lblColName
        '
        Me.lblColName.AutoSize = True
        Me.lblColName.BackColor = System.Drawing.Color.Transparent
        Me.lblColName.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblColName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblColName.Location = New System.Drawing.Point(65, 1)
        Me.lblColName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblColName.Name = "lblColName"
        Me.lblColName.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.lblColName.Size = New System.Drawing.Size(19, 20)
        Me.lblColName.TabIndex = 2
        Me.lblColName.Text = "  "
        Me.lblColName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblColName.Visible = False
        '
        'pic_OK
        '
        Me.pic_OK.BackColor = System.Drawing.Color.Transparent
        Me.pic_OK.BackgroundImage = CType(resources.GetObject("pic_OK.BackgroundImage"), System.Drawing.Image)
        Me.pic_OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pic_OK.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pic_OK.Dock = System.Windows.Forms.DockStyle.Right
        Me.pic_OK.Location = New System.Drawing.Point(641, 1)
        Me.pic_OK.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pic_OK.Name = "pic_OK"
        Me.pic_OK.Size = New System.Drawing.Size(21, 22)
        Me.pic_OK.TabIndex = 15
        Me.pic_OK.TabStop = False
        Me.pic_OK.WaitOnLoad = True
        '
        'pnl_NextOK
        '
        Me.pnl_NextOK.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnl_NextOK.Location = New System.Drawing.Point(662, 1)
        Me.pnl_NextOK.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnl_NextOK.Name = "pnl_NextOK"
        Me.pnl_NextOK.Size = New System.Drawing.Size(4, 22)
        Me.pnl_NextOK.TabIndex = 22
        '
        'pic_ADDNew
        '
        Me.pic_ADDNew.BackColor = System.Drawing.Color.Transparent
        Me.pic_ADDNew.BackgroundImage = CType(resources.GetObject("pic_ADDNew.BackgroundImage"), System.Drawing.Image)
        Me.pic_ADDNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pic_ADDNew.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pic_ADDNew.Dock = System.Windows.Forms.DockStyle.Right
        Me.pic_ADDNew.InitialImage = CType(resources.GetObject("pic_ADDNew.InitialImage"), System.Drawing.Image)
        Me.pic_ADDNew.Location = New System.Drawing.Point(666, 1)
        Me.pic_ADDNew.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pic_ADDNew.Name = "pic_ADDNew"
        Me.pic_ADDNew.Size = New System.Drawing.Size(18, 22)
        Me.pic_ADDNew.TabIndex = 14
        Me.pic_ADDNew.TabStop = False
        '
        'pnl_NextAddNew
        '
        Me.pnl_NextAddNew.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnl_NextAddNew.Location = New System.Drawing.Point(684, 1)
        Me.pnl_NextAddNew.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnl_NextAddNew.Name = "pnl_NextAddNew"
        Me.pnl_NextAddNew.Size = New System.Drawing.Size(4, 22)
        Me.pnl_NextAddNew.TabIndex = 20
        '
        'pic_Modify
        '
        Me.pic_Modify.BackColor = System.Drawing.Color.Transparent
        Me.pic_Modify.BackgroundImage = CType(resources.GetObject("pic_Modify.BackgroundImage"), System.Drawing.Image)
        Me.pic_Modify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pic_Modify.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pic_Modify.Dock = System.Windows.Forms.DockStyle.Right
        Me.pic_Modify.Location = New System.Drawing.Point(688, 1)
        Me.pic_Modify.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pic_Modify.Name = "pic_Modify"
        Me.pic_Modify.Size = New System.Drawing.Size(18, 22)
        Me.pic_Modify.TabIndex = 23
        Me.pic_Modify.TabStop = False
        '
        'pnl_NextModify
        '
        Me.pnl_NextModify.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnl_NextModify.Location = New System.Drawing.Point(706, 1)
        Me.pnl_NextModify.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnl_NextModify.Name = "pnl_NextModify"
        Me.pnl_NextModify.Size = New System.Drawing.Size(4, 22)
        Me.pnl_NextModify.TabIndex = 24
        '
        'pnl_Close
        '
        Me.pnl_Close.BackgroundImage = CType(resources.GetObject("pnl_Close.BackgroundImage"), System.Drawing.Image)
        Me.pnl_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pnl_Close.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pnl_Close.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnl_Close.Location = New System.Drawing.Point(710, 1)
        Me.pnl_Close.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnl_Close.Name = "pnl_Close"
        Me.pnl_Close.Size = New System.Drawing.Size(18, 22)
        Me.pnl_Close.TabIndex = 17
        '
        'pnl_NextClose
        '
        Me.pnl_NextClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnl_NextClose.Location = New System.Drawing.Point(728, 1)
        Me.pnl_NextClose.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnl_NextClose.Name = "pnl_NextClose"
        Me.pnl_NextClose.Size = New System.Drawing.Size(9, 22)
        Me.pnl_NextClose.TabIndex = 21
        '
        'pnlColName
        '
        Me.pnlColName.Location = New System.Drawing.Point(353, 1)
        Me.pnlColName.Name = "pnlColName"
        Me.pnlColName.Size = New System.Drawing.Size(149, 22)
        Me.pnlColName.TabIndex = 26
        Me.pnlColName.Visible = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(1, 1)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 22)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "Search :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Location = New System.Drawing.Point(1, 24)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(736, 1)
        Me.Panel1.TabIndex = 26
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.Transparent
        Me.pnlTop.Controls.Add(Me.Label1)
        Me.pnlTop.Controls.Add(Me.lblSearchString)
        Me.pnlTop.Location = New System.Drawing.Point(1, 1)
        Me.pnlTop.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(736, 1)
        Me.pnlTop.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(90, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 14)
        Me.Label1.TabIndex = 18
        '
        'lblSearchString
        '
        Me.lblSearchString.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSearchString.AutoSize = True
        Me.lblSearchString.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearchString.Location = New System.Drawing.Point(4, -45)
        Me.lblSearchString.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSearchString.Name = "lblSearchString"
        Me.lblSearchString.Size = New System.Drawing.Size(73, 14)
        Me.lblSearchString.TabIndex = 2
        Me.lblSearchString.Text = "Search String"
        Me.lblSearchString.Visible = False
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(737, 1)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 22)
        Me.lbl_pnlRight.TabIndex = 28
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(1, 0)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(737, 1)
        Me.lbl_pnlTop.TabIndex = 27
        Me.lbl_pnlTop.Text = "label1"
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(1, 23)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(737, 1)
        Me.lbl_pnlBottom.TabIndex = 30
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 24)
        Me.lbl_pnlLeft.TabIndex = 29
        Me.lbl_pnlLeft.Text = "label4"
        '
        'btnUC_Mod
        '
        Me.btnUC_Mod.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnUC_Mod.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnUC_Mod.Location = New System.Drawing.Point(620, 80)
        Me.btnUC_Mod.Name = "btnUC_Mod"
        Me.btnUC_Mod.Size = New System.Drawing.Size(20, 19)
        Me.btnUC_Mod.TabIndex = 25
        Me.btnUC_Mod.Text = "Button1"
        Me.btnUC_Mod.UseVisualStyleBackColor = True
        Me.btnUC_Mod.Visible = False
        '
        'btnCloseRefill
        '
        Me.btnCloseRefill.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCloseRefill.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCloseRefill.FlatAppearance.BorderSize = 0
        Me.btnCloseRefill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCloseRefill.Image = CType(resources.GetObject("btnCloseRefill.Image"), System.Drawing.Image)
        Me.btnCloseRefill.Location = New System.Drawing.Point(559, 65)
        Me.btnCloseRefill.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnCloseRefill.Name = "btnCloseRefill"
        Me.btnCloseRefill.Size = New System.Drawing.Size(21, 19)
        Me.btnCloseRefill.TabIndex = 6
        Me.btnCloseRefill.Visible = False
        '
        'btnUC_Add
        '
        Me.btnUC_Add.BackColor = System.Drawing.Color.Transparent
        Me.btnUC_Add.FlatAppearance.BorderSize = 0
        Me.btnUC_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUC_Add.Image = CType(resources.GetObject("btnUC_Add.Image"), System.Drawing.Image)
        Me.btnUC_Add.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnUC_Add.Location = New System.Drawing.Point(453, 79)
        Me.btnUC_Add.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnUC_Add.Name = "btnUC_Add"
        Me.btnUC_Add.Size = New System.Drawing.Size(21, 20)
        Me.btnUC_Add.TabIndex = 11
        Me.btnUC_Add.UseVisualStyleBackColor = False
        Me.btnUC_Add.Visible = False
        '
        'btnUC_OK
        '
        Me.btnUC_OK.BackColor = System.Drawing.Color.Transparent
        Me.btnUC_OK.FlatAppearance.BorderSize = 0
        Me.btnUC_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUC_OK.Image = CType(resources.GetObject("btnUC_OK.Image"), System.Drawing.Image)
        Me.btnUC_OK.Location = New System.Drawing.Point(474, 44)
        Me.btnUC_OK.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnUC_OK.Name = "btnUC_OK"
        Me.btnUC_OK.Size = New System.Drawing.Size(18, 22)
        Me.btnUC_OK.TabIndex = 12
        Me.btnUC_OK.Text = "."
        Me.btnUC_OK.UseVisualStyleBackColor = False
        Me.btnUC_OK.Visible = False
        '
        'btnUC_Close
        '
        Me.btnUC_Close.BackColor = System.Drawing.Color.Transparent
        Me.btnUC_Close.FlatAppearance.BorderSize = 0
        Me.btnUC_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUC_Close.Image = CType(resources.GetObject("btnUC_Close.Image"), System.Drawing.Image)
        Me.btnUC_Close.Location = New System.Drawing.Point(500, 63)
        Me.btnUC_Close.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnUC_Close.Name = "btnUC_Close"
        Me.btnUC_Close.Size = New System.Drawing.Size(21, 20)
        Me.btnUC_Close.TabIndex = 13
        Me.btnUC_Close.UseVisualStyleBackColor = False
        Me.btnUC_Close.Visible = False
        '
        '_Flex
        '
        Me._Flex.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me._Flex.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._Flex.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me._Flex.ColumnInfo = "10,1,0,0,0,95,Columns:"
        Me._Flex.Dock = System.Windows.Forms.DockStyle.Fill
        Me._Flex.ExtendLastCol = True
        Me._Flex.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Flex.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me._Flex.Location = New System.Drawing.Point(1, 4)
        Me._Flex.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me._Flex.Name = "_Flex"
        Me._Flex.Rows.DefaultSize = 19
        Me._Flex.Size = New System.Drawing.Size(736, 405)
        Me._Flex.StyleInfo = resources.GetString("_Flex.StyleInfo")
        Me._Flex.TabIndex = 4
        '
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me._Flex)
        Me.pnl_Base.Controls.Add(Me.Label3)
        Me.pnl_Base.Controls.Add(Me.Label4)
        Me.pnl_Base.Controls.Add(Me.Label5)
        Me.pnl_Base.Controls.Add(Me.Label6)
        Me.pnl_Base.Controls.Add(Me.btnUC_Mod)
        Me.pnl_Base.Controls.Add(Me.btnCloseRefill)
        Me.pnl_Base.Controls.Add(Me.btnUC_Close)
        Me.pnl_Base.Controls.Add(Me.btnUC_OK)
        Me.pnl_Base.Controls.Add(Me.btnUC_Add)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 24)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnl_Base.Size = New System.Drawing.Size(738, 410)
        Me.pnl_Base.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Location = New System.Drawing.Point(1, 409)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(736, 1)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Location = New System.Drawing.Point(0, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 406)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "label4"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Location = New System.Drawing.Point(737, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 406)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "label3"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Location = New System.Drawing.Point(0, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(738, 1)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlSearch)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(738, 24)
        Me.Panel2.TabIndex = 6
        '
        'cntListmenuStrip
        '
        Me.cntListmenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ModifySigInfoToolStripMenuItem, Me.RemoveDrugToolStripMenuItem})
        Me.cntListmenuStrip.Name = "ContextMenuStrip_Sig"
        Me.cntListmenuStrip.Size = New System.Drawing.Size(105, 48)
        '
        'ModifySigInfoToolStripMenuItem
        '
        Me.ModifySigInfoToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ModifySigInfoToolStripMenuItem.Image = CType(resources.GetObject("ModifySigInfoToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ModifySigInfoToolStripMenuItem.Name = "ModifySigInfoToolStripMenuItem"
        Me.ModifySigInfoToolStripMenuItem.Size = New System.Drawing.Size(104, 22)
        Me.ModifySigInfoToolStripMenuItem.Text = "Add"
        '
        'RemoveDrugToolStripMenuItem
        '
        Me.RemoveDrugToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.RemoveDrugToolStripMenuItem.Image = CType(resources.GetObject("RemoveDrugToolStripMenuItem.Image"), System.Drawing.Image)
        Me.RemoveDrugToolStripMenuItem.Name = "RemoveDrugToolStripMenuItem"
        Me.RemoveDrugToolStripMenuItem.Size = New System.Drawing.Size(104, 22)
        Me.RemoveDrugToolStripMenuItem.Text = "Edit"
        '
        'panel4
        '
        Me.panel4.BackColor = System.Drawing.Color.White
        Me.panel4.Controls.Add(Me.txtSearchFlexGrid)
        Me.panel4.Controls.Add(Me.label20)
        Me.panel4.Controls.Add(Me.label21)
        Me.panel4.Controls.Add(Me.btnClearSearchFlexGrid)
        Me.panel4.Controls.Add(Me.label22)
        Me.panel4.Controls.Add(Me.label23)
        Me.panel4.Controls.Add(Me.label24)
        Me.panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panel4.ForeColor = System.Drawing.Color.Black
        Me.panel4.Location = New System.Drawing.Point(88, 1)
        Me.panel4.Name = "panel4"
        Me.panel4.Size = New System.Drawing.Size(241, 22)
        Me.panel4.TabIndex = 50
        '
        'label20
        '
        Me.label20.BackColor = System.Drawing.Color.White
        Me.label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label20.Location = New System.Drawing.Point(5, 17)
        Me.label20.Name = "label20"
        Me.label20.Size = New System.Drawing.Size(214, 5)
        Me.label20.TabIndex = 43
        '
        'label21
        '
        Me.label21.BackColor = System.Drawing.Color.White
        Me.label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.label21.Location = New System.Drawing.Point(5, 0)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(214, 3)
        Me.label21.TabIndex = 37
        '
        'label22
        '
        Me.label22.BackColor = System.Drawing.Color.White
        Me.label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.label22.Location = New System.Drawing.Point(1, 0)
        Me.label22.Name = "label22"
        Me.label22.Size = New System.Drawing.Size(4, 22)
        Me.label22.TabIndex = 38
        '
        'label23
        '
        Me.label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.label23.Location = New System.Drawing.Point(0, 0)
        Me.label23.Name = "label23"
        Me.label23.Size = New System.Drawing.Size(1, 22)
        Me.label23.TabIndex = 39
        Me.label23.Text = "label4"
        '
        'label24
        '
        Me.label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.label24.Location = New System.Drawing.Point(240, 0)
        Me.label24.Name = "label24"
        Me.label24.Size = New System.Drawing.Size(1, 22)
        Me.label24.TabIndex = 40
        Me.label24.Text = "label4"
        '
        'gloUC_CustomSearchInC1Flexgrid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Controls.Add(Me.pnl_Base)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "gloUC_CustomSearchInC1Flexgrid"
        Me.Size = New System.Drawing.Size(738, 434)
        Me.cntListmenuStrip1.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.pic_OK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_ADDNew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_Modify, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me._Flex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_Base.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.cntListmenuStrip.ResumeLayout(False)
        Me.panel4.ResumeLayout(False)
        Me.panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents ImagSearchFlex As System.Windows.Forms.ImageList
    Protected WithEvents cntListmenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Protected WithEvents pnlSearch As System.Windows.Forms.Panel
    Protected WithEvents txtSearchFlexGrid As System.Windows.Forms.TextBox
    Protected WithEvents lblSearchString As System.Windows.Forms.Label
    Protected WithEvents _Flex As C1.Win.C1FlexGrid.C1FlexGrid
    Protected WithEvents btnCloseRefill As System.Windows.Forms.Button
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents btnUC_Close As System.Windows.Forms.Button
    Friend WithEvents btnUC_OK As System.Windows.Forms.Button
    Friend WithEvents btnUC_Add As System.Windows.Forms.Button

    'Friend WithEvents pic_ADDNew As System.Windows.Forms.PictureBox
    'Friend WithEvents pic_OK As System.Windows.Forms.PictureBox

    Public WithEvents pic_ADDNew As System.Windows.Forms.PictureBox
    Public WithEvents pic_OK As System.Windows.Forms.PictureBox

    Friend WithEvents AddToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    ' Friend WithEvents pnl_Close As System.Windows.Forms.Panel
    Public WithEvents pnl_Close As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnl_NextAddNew As System.Windows.Forms.Panel
    Protected WithEvents lblColName As System.Windows.Forms.Label
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnl_NextOK As System.Windows.Forms.Panel
    Friend WithEvents pnl_NextClose As System.Windows.Forms.Panel
    Friend WithEvents pnl_NextModify As System.Windows.Forms.Panel
    Friend WithEvents btnUC_Mod As System.Windows.Forms.Button
    'Friend WithEvents pic_Modify As System.Windows.Forms.PictureBox
    Public WithEvents pic_Modify As System.Windows.Forms.PictureBox

    Friend WithEvents pnlColName As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnClearSearchFlexGrid As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cntListmenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ModifySigInfoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveDrugToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents panel4 As System.Windows.Forms.Panel
    Friend WithEvents label20 As System.Windows.Forms.Label
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents label24 As System.Windows.Forms.Label

End Class
