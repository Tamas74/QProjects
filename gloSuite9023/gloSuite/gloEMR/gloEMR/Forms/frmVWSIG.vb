Public Class frmVwsig
    Inherits System.Windows.Forms.Form
    Public Shared blnModify As Boolean
    Dim objSIG As New clsSIG
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents panel4 As System.Windows.Forms.Panel
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents label20 As System.Windows.Forms.Label
    Friend WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents label24 As System.Windows.Forms.Label
    Dim _blnSearch As Boolean = True

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
            Try

                If (IsNothing(grdSIG) = False) Then
                    grdSIG.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(grdSIG)
                    grdSIG.Dispose()
                    grdSIG = Nothing
                End If
            Catch ex As Exception

            End Try
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents grdSIG As System.Windows.Forms.DataGrid
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVwsig))
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.grdSIG = New System.Windows.Forms.DataGrid()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.label20 = New System.Windows.Forms.Label()
        Me.label21 = New System.Windows.Forms.Label()
        Me.label22 = New System.Windows.Forms.Label()
        Me.label23 = New System.Windows.Forms.Label()
        Me.label24 = New System.Windows.Forms.Label()
        Me.pnlTopRight.SuspendLayout()
        CType(Me.grdSIG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.panel4)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Label1)
        Me.pnlTopRight.Controls.Add(Me.Label2)
        Me.pnlTopRight.Controls.Add(Me.Label3)
        Me.pnlTopRight.Controls.Add(Me.Label4)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(666, 24)
        Me.pnlTopRight.TabIndex = 0
        '
        'btnClear
        '
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(219, 0)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 22)
        Me.btnClear.TabIndex = 43
        Me.ToolTip1.SetToolTip(Me.btnClear, "Clear Search")
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(5, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(214, 15)
        Me.txtSearch.TabIndex = 0
        '
        'lblSearch
        '
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(64, 22)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(664, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 23)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(665, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 23)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(666, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'grdSIG
        '
        Me.grdSIG.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.grdSIG.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdSIG.BackgroundColor = System.Drawing.Color.White
        Me.grdSIG.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdSIG.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdSIG.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSIG.CaptionForeColor = System.Drawing.Color.White
        Me.grdSIG.CaptionVisible = False
        Me.grdSIG.DataMember = ""
        Me.grdSIG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSIG.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSIG.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grdSIG.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.grdSIG.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdSIG.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSIG.HeaderForeColor = System.Drawing.Color.White
        Me.grdSIG.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.grdSIG.Location = New System.Drawing.Point(4, 1)
        Me.grdSIG.Name = "grdSIG"
        Me.grdSIG.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdSIG.ParentRowsForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grdSIG.ReadOnly = True
        Me.grdSIG.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.grdSIG.SelectionForeColor = System.Drawing.Color.Black
        Me.grdSIG.Size = New System.Drawing.Size(664, 429)
        Me.grdSIG.TabIndex = 0
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(672, 54)
        Me.pnlToolStrip.TabIndex = 2
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.Transparent
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(672, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnAdd
        '
        Me.ts_btnAdd.Image = CType(resources.GetObject("ts_btnAdd.Image"), System.Drawing.Image)
        Me.ts_btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnAdd.Name = "ts_btnAdd"
        Me.ts_btnAdd.Size = New System.Drawing.Size(37, 50)
        Me.ts_btnAdd.Tag = "Add"
        Me.ts_btnAdd.Text = "&New"
        Me.ts_btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnModify
        '
        Me.ts_btnModify.Image = CType(resources.GetObject("ts_btnModify.Image"), System.Drawing.Image)
        Me.ts_btnModify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnModify.Name = "ts_btnModify"
        Me.ts_btnModify.Size = New System.Drawing.Size(53, 50)
        Me.ts_btnModify.Tag = "Modify"
        Me.ts_btnModify.Text = "&Modify"
        Me.ts_btnModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnDelete
        '
        Me.ts_btnDelete.Image = CType(resources.GetObject("ts_btnDelete.Image"), System.Drawing.Image)
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(50, 50)
        Me.ts_btnDelete.Tag = "Delete"
        Me.ts_btnDelete.Text = "&Delete"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Image = CType(resources.GetObject("ts_btnRefresh.Image"), System.Drawing.Image)
        Me.ts_btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnRefresh.Name = "ts_btnRefresh"
        Me.ts_btnRefresh.Size = New System.Drawing.Size(58, 50)
        Me.ts_btnRefresh.Tag = "Refresh"
        Me.ts_btnRefresh.Text = "&Refresh"
        Me.ts_btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.grdSIG)
        Me.Panel1.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel1.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel1.Controls.Add(Me.lbl_RightBrd)
        Me.Panel1.Controls.Add(Me.lbl_TopBrd)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 84)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(672, 434)
        Me.Panel1.TabIndex = 1
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 430)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(664, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 1)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 430)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(668, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 430)
        Me.lbl_RightBrd.TabIndex = 6
        Me.lbl_RightBrd.Text = "label3"
        '
        'lbl_TopBrd
        '
        Me.lbl_TopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_TopBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 0)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(666, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlTopRight)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 54)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(672, 30)
        Me.Panel2.TabIndex = 0
        '
        'panel4
        '
        Me.panel4.BackColor = System.Drawing.Color.White
        Me.panel4.Controls.Add(Me.txtSearch)
        Me.panel4.Controls.Add(Me.label21)
        Me.panel4.Controls.Add(Me.label20)
        Me.panel4.Controls.Add(Me.btnClear)
        Me.panel4.Controls.Add(Me.label22)
        Me.panel4.Controls.Add(Me.label23)
        Me.panel4.Controls.Add(Me.label24)
        Me.panel4.Dock = System.Windows.Forms.DockStyle.Left
        Me.panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panel4.ForeColor = System.Drawing.Color.Black
        Me.panel4.Location = New System.Drawing.Point(65, 1)
        Me.panel4.Name = "panel4"
        Me.panel4.Size = New System.Drawing.Size(241, 22)
        Me.panel4.TabIndex = 47
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
        'frmVwsig
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(672, 518)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmVwsig"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SIG"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTopRight.ResumeLayout(False)
        CType(Me.grdSIG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.panel4.ResumeLayout(False)
        Me.panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmVwsig_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            grdSIG.DataSource = objSIG.GetAllSIG
            CustomGridStyle()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "SIG", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub UpdateSIG()
        Dim ID As Long
        Dim objfrmMSTSIG As frmMSTSIG
        Try
            If grdSIG.VisibleRowCount >= 1 Then
                blnModify = True
                ID = grdSIG.Item(grdSIG.CurrentRowIndex, 0).ToString
                Dim grdIndex As Integer = grdSIG.CurrentRowIndex

                '''''Code is added by Anil 20071102
                Dim myDataView As DataView = CType(grdSIG.DataSource, DataView)
                If (IsNothing(myDataView) = False) Then


                    Dim sortOrder As String = myDataView.Sort
                    Dim strSearchstring As String = txtSearch.Text.Trim
                    Dim arrcolumnsort() As String = Split(sortOrder, "]")
                    Dim strcolumnName As String = arrcolumnsort.GetValue(0)
                    Dim strsortorder As String = ""
                    If arrcolumnsort.Length > 1 Then
                        strsortorder = arrcolumnsort.GetValue(1)
                    End If
                    ''''''''''''
                    objfrmMSTSIG = New frmMSTSIG(ID)
                    objfrmMSTSIG.Text = "Modify SIG"
                    objfrmMSTSIG.ShowDialog(IIf(IsNothing(objfrmMSTSIG.Parent), Me, objfrmMSTSIG.Parent))
                    objfrmMSTSIG.BringToFront()

                    If objfrmMSTSIG.CancelClick = False Then
                        grdSIG.DataSource = objSIG.GetAllSIG
                        objSIG.SortDataview(objSIG.GetDataview.Table.Columns(1).ColumnName)

                        CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                        '''' To Remember the Selection of Row 
                        Dim myDatagView As DataView = CType(grdSIG.DataSource, DataView)
                        If (IsNothing(myDatagView) = False) Then


                            Dim i As Integer
                            For i = 0 To myDatagView.Count - 1
                                '''' when ID Found select that matching Row
                                If ID = grdSIG.Item(i, 0) Then
                                    grdSIG.CurrentRowIndex = i
                                    grdSIG.Select(i)
                                    Exit For
                                End If
                            Next
                        End If
                    Else
                        grdSIG.Select(grdIndex)
                    End If
                    objfrmMSTSIG.Dispose()
                    objfrmMSTSIG = Nothing
                End If

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "SIG", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objfrmMSTSIG = Nothing
        End Try
    End Sub
    Public Sub CustomGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        Dim ts As New clsDataGridTableStyle(objSIG.GetDataview.Table.TableName)

        Try

            objSIG.SortDataview(objSIG.GetDataview.Table.Columns(1).ColumnName)

            Dim IDCol As New DataGridTextBoxColumn
            IDCol.Width = 0
            IDCol.MappingName = objSIG.GetDataview.Table.Columns(0).ColumnName
            IDCol.HeaderText = "ID"

            'PER NO: 2140 - 20 Jun 2009 - Saagar K
            Dim DosageCol As New DataGridTextBoxColumn
            With DosageCol
                .Width = 0 '0.25 * (grdSIG.Width - 100)
                .MappingName = objSIG.GetDataview.Table.Columns(1).ColumnName
                .HeaderText = "Dosage"
                .NullText = ""
            End With
            'PER NO: 2140 - 20 Jun 2009 - Saagar K

            Dim RouteCol As New DataGridTextBoxColumn
            With RouteCol
                .Width = 0.25 * (grdSIG.Width - 100)
                .MappingName = objSIG.GetDataview.Table.Columns(2).ColumnName
                .HeaderText = "Route"
                .NullText = ""
            End With

            Dim FrequencyCol As New DataGridTextBoxColumn
            With FrequencyCol
                .Width = 0.25 * (grdSIG.Width - 100)
                .MappingName = objSIG.GetDataview.Table.Columns(3).ColumnName
                .HeaderText = "Frequency"
                .NullText = ""
            End With

            Dim DurationCol As New DataGridTextBoxColumn
            With DurationCol
                .Width = 0.25 * (grdSIG.Width - 100)
                .MappingName = objSIG.GetDataview.Table.Columns(4).ColumnName
                .HeaderText = "Duration"
                .NullText = ""
            End With

            'PER NO: 2140 - 20 Jun 2009 - Saagar K
            Dim RefillsCol As New DataGridTextBoxColumn
            With RefillsCol
                .Width = 0.25 * (grdSIG.Width - 100)
                .MappingName = objSIG.GetDataview.Table.Columns(7).ColumnName
                .HeaderText = "Refills"
                .NullText = ""
            End With
            'PER NO: 2140 - 20 Jun 2009 - Saagar K

            Dim AsNeeedCol As New DataGridBoolColumn
            With AsNeeedCol
                .Width = 100
                .MappingName = objSIG.GetDataview.Table.Columns(5).ColumnName
                .HeaderText = "Needed"
                .NullText = ""
            End With


            'CCHIT 08
            Dim Duration_SpltChrCol As New DataGridTextBoxColumn
            With Duration_SpltChrCol
                .Width = 0
                .MappingName = objSIG.GetDataview.Table.Columns(6).ColumnName
                .HeaderText = "Duration_SpltChr"
                .NullText = ""
            End With
            'CCHIT 08

            '''''''Code is added by Anil on 02/11/2007
            txtSearch.Text = ""
            txtSearch.Text = strsearchtxt
            If strcolumnName = "" Then
                objSIG.SortDataview(objSIG.GetDataview.Table.Columns(1).ColumnName)
            Else
                Dim strColumn As String = Replace(strcolumnName, "[", "")

                objSIG.SortDataview(strColumn, strSortBy)
            End If
            ''''''''''''''''''''''''''''''''

            'CCHIT 08
            ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {IDCol, DosageCol, RouteCol, FrequencyCol, DurationCol, RefillsCol, AsNeeedCol, Duration_SpltChrCol})
            'CCHIT 08
            grdSIG.TableStyles.Clear()
            grdSIG.TableStyles.Add(ts)

        Catch ex As Exception

        End Try
    End Sub
    Private Sub grdSIG_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSIG.CurrentCellChanged
        ''''''Commented by Anil on 20071102
        ''Try
        ''    Select Case grdSIG.CurrentCell.ColumnNumber
        ''        Case 1
        ''            lblSearch.Text = "Dosage"
        ''        Case 2
        ''            lblSearch.Text = "Route"
        ''        Case 3
        ''            lblSearch.Text = "Frequency"
        ''        Case 4
        ''            lblSearch.Text = "Duration"
        ''    End Select
        ''Catch objErr As Exception
        ''    MessageBox.Show(objErr.ToString, "SIG", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim dvPatient As DataView
                dvPatient = CType(grdSIG.DataSource(), DataView)
                If IsNothing(dvPatient) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                grdSIG.DataSource = dvPatient
                Dim strPatientSearchDetails As String
                If Trim(txtSearch.Text) <> "" Then
                    strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                    ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
                Else
                    strPatientSearchDetails = ""
                End If
                'Shubhangi 20090930 Apply general Search

                dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                                                  & dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                                                  & dvPatient.Table.Columns(4).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                                                  & dvPatient.Table.Columns(7).ColumnName & " Like '%" & strPatientSearchDetails & "%' "
                                                 


                'COMMENTED BY SHUBHANGI 20090930

                ''Modification by dipak 20090902 for removing ":" Label serch text
                'Select Case Trim(lblSearch.Text.Replace(":", ""))
                '    'end modification
                '    Case "Dosage"
                '        '''''Code Modified by Anil on 24/09/2007 at 3:05 p.m.
                '        '''''This change is made to get In-String search i.e.,for any string which has the character/characters present in the search textbox.
                '        '''''Previously the commented code was searching for the strings which are having first character same as that in search textbox 
                '        '''''and also it was searching strings which are having the character in between the words but for that we had to use " % " or " * " sign before that character. But now it is not required to add % or * signs.

                '        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '        dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '        ''Else
                '        ''dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                '        ''End If
                '    Case "Route"
                '        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '        dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '        ''Else
                '        ''dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                '        ''End If
                '    Case "Frequency"
                '        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '        dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '        ''Else
                '        ''dvPatient.RowFilter = dvPatient.Table.Columns(3).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                '        ''End If
                '    Case "Duration"
                '        ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                '        dvPatient.RowFilter = dvPatient.Table.Columns(4).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                '        ''Else
                '        ''dvPatient.RowFilter = dvPatient.Table.Columns(4).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                '        ''End If
                '        '''''Upto here the changes are made by Anil on 24/09/2007 at 3:05 p.m.
                'End Select
                Me.Cursor = Cursors.Default
            Catch objErr As Exception
                Me.Cursor = Cursors.Default
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Query, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ''Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
    ''    Try
    ''        If Not IsNothing(objSIG.GetDataview) Then
    ''            Dim Search As String
    ''            Search = Replace(Trim(txtSearch.Text), "'", "''")
    ''            'Check if sort column is not of type boolean
    ''            objSIG.SetRowFilter(Trim(Search))
    ''            'HideColumn()
    ''        End If
    ''        'objSIG.Search(grdSIG.DataSource, 1, txtSearch.Text)
    ''    Catch ex As Exception
    ''        MessageBox.Show(ex.ToString, "SIG", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ''    End Try
    ''End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If grdSIG.CurrentRowIndex >= 0 Then
                    grdSIG.Select(0)
                    grdSIG.CurrentRowIndex = 0
                End If
            End If
            ''--Added by Anil on 20071213
            mdlGeneral.ValidateText(txtSearch.Text, e)
            ''----
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub grdSIG_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdSIG.MouseUp
        If grdSIG.CurrentRowIndex >= 0 Then
            grdSIG.Select(grdSIG.CurrentRowIndex)
        End If
    End Sub

    Private Sub grdSIG_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSIG.DoubleClick
        ''Try
        ''    UpdateSIG()
        ''Catch ex As Exception

        ''End Try
        ''Dim ID As Long
        ''Dim objfrmMSTSIG As frmMSTSIG
        ''Try
        ''    If grdSIG.VisibleRowCount >= 1 Then
        ''        blnModify = True
        ''        ID = grdSIG.Item(grdSIG.CurrentRowIndex, 0).ToString
        ''        Dim grdIndex As Integer = grdSIG.CurrentRowIndex

        ''        objfrmMSTSIG = New frmMSTSIG(ID)
        ''        objfrmMSTSIG.Text = "Modify SIG"
        ''        objfrmMSTSIG.ShowDialog(Me)
        ''        objfrmMSTSIG.BringToFront()
        ''        If objfrmMSTSIG.CancelClick = False Then
        ''            grdSIG.DataSource = objSIG.GetAllSIG
        ''        End If
        ''        objSIG.SortDataview(objSIG.GetDataview.Table.Columns(1).ColumnName)

        ''        grdSIG.Select(grdIndex)
        ''    End If
        ''Catch ex As Exception
        ''    MessageBox.Show(ex.ToString, "SIG", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''Finally
        ''    objfrmMSTSIG = Nothing
        ''End Try
    End Sub


    Private Sub frmVwsig_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try

        Catch ex As Exception
        End Try
    End Sub
    ''''''''Code/Event ia Added by Anil on 20071102
    Private Sub grdSIG_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdSIG.MouseDoubleClick
        'COMMENTED BY SHUBHANGI 20090930
        'INSTEAD OF THIS USE GENERAL 
        'Added on 20091028:by Mayuri
        'To fix Bug:#4525
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = grdSIG.HitTest(ptPoint)
            '    If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then
            '        'modification by dipak 20090902 to set ":" after label

            '        Select Case htInfo.Column
            '            Case 1
            '                lblSearch.Text = "Dosage" & " :"
            '                txtSearch.Focus()
            '            Case 2
            '                lblSearch.Text = "Route" & " :"
            '                txtSearch.Focus()
            '            Case 3
            '                lblSearch.Text = "Frequency" & " :"
            '                txtSearch.Focus()
            '            Case 4
            '                lblSearch.Text = "Duration" & " :"
            '                txtSearch.Focus()
            '        End Select
            '        'modification ends
            '        If txtSearch.Text = "" Then
            '            _blnSearch = True
            '        Else
            '            _blnSearch = False
            '            txtSearch.Text = ""
            '            _blnSearch = True
            '        End If

            If htInfo.Type = DataGrid.HitTestType.Cell Then
                _blnSearch = True
                UpdateSIG()
            End If
            'End code Added by Mayuri:20091028

            'code added by dipak  20090902 for refresh after changing serch field
            'RefreshCategory()
            'end change
        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '''''''''''''''''''''
    ''Code Added by Shilpa for adding the new buttons on 13th Nov 2007
    Private Sub AddCategory()
        Dim objfrmMSTSIG As New frmMSTSIG
        Try
            blnModify = False
            objfrmMSTSIG.Text = "Add New SIG"
            objfrmMSTSIG.ShowDialog(IIf(IsNothing(objfrmMSTSIG.Parent), Me, objfrmMSTSIG.Parent))

            If objfrmMSTSIG.CancelClick = False Then
                grdSIG.DataSource = objSIG.GetAllSIG

                '''' To Remember the Selection of Row 
                Dim myDataView As DataView = CType(grdSIG.DataSource, DataView)
                If (IsNothing(myDataView) = False) Then


                    Dim i As Integer
                    For i = 0 To myDataView.Table.Rows.Count - 1
                        '''' when ID Found select that matching Row
                        If objfrmMSTSIG._Dosage = grdSIG.Item(i, 1) Then
                            grdSIG.CurrentRowIndex = i
                            grdSIG.Select(i)
                            Exit For
                        End If
                    Next
                End If
            End If
            objSIG.SortDataview(objSIG.GetDataview.Table.Columns(1).ColumnName)
            objfrmMSTSIG.Dispose()
            objfrmMSTSIG = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objfrmMSTSIG = Nothing
        End Try
    End Sub
    Private Sub UpdateCategory()

        Try
            UpdateSIG()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        ''Dim ID As Long
        ''Dim objfrmMSTSIG As frmMSTSIG
        ''Try
        ''    If grdSIG.VisibleRowCount >= 1 Then
        ''        blnModify = True
        ''        ID = grdSIG.Item(grdSIG.CurrentRowIndex, 0).ToString
        ''        Dim grdIndex As Integer = grdSIG.CurrentRowIndex

        ''        objfrmMSTSIG = New frmMSTSIG(ID)
        ''        objfrmMSTSIG.Text = "Modify SIG"
        ''        objfrmMSTSIG.ShowDialog(Me)
        ''        objfrmMSTSIG.BringToFront()

        ''        If objfrmMSTSIG.CancelClick = False Then
        ''            grdSIG.DataSource = objSIG.GetAllSIG
        ''        End If
        ''        objSIG.SortDataview(objSIG.GetDataview.Table.Columns(1).ColumnName)
        ''        grdSIG.Select(grdIndex)
        ''    End If
        ''Catch ex As Exception
        ''    MessageBox.Show(ex.ToString, "SIG", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''Finally
        ''    objfrmMSTSIG = Nothing
        ''End Try
    End Sub
    Private Sub DeleteCategory()
        Dim ID As Long
        Dim Dosage As String
        Dim strcolumnName As String
        Dim strsortorder As String = ""
        Try
            If grdSIG.VisibleRowCount >= 1 Then
                'blnModify = True
                If MessageBox.Show("Are you sure to delete this SIG's detail?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                    ID = grdSIG.Item(grdSIG.CurrentRowIndex, 0).ToString
                    Dosage = grdSIG.Item(grdSIG.CurrentRowIndex, 1).ToString
                    objSIG.DeleteSIG(ID, Dosage)
                    grdSIG.DataSource = objSIG.GetAllSIG
                    ''''''Code is Added by Anil 0n 20071102
                    Dim myDataView As DataView = CType(grdSIG.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then


                        Dim sortOrder As String = myDataView.Sort
                        Dim strSearchstring As String = txtSearch.Text.Trim
                        Dim arrcolumnsort() As String = Split(sortOrder, "]")
                        strcolumnName = arrcolumnsort.GetValue(0)
                        If arrcolumnsort.Length > 1 Then
                            strsortorder = arrcolumnsort.GetValue(1)
                        End If
                        CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                        ''''''''''''''''''
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "SIG", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshCategory()
        Try
            grdSIG.DataSource = objSIG.GetAllSIG
            CustomGridStyle()
            '''''Following code lines are addded by Anil 0n 26/09/07 at 10:50 a.m.
            '''''This code clears the search textbox and gets the focus on the first row of the grid on click of Refresh button.
            txtSearch.Clear()
            If Not grdSIG.VisibleRowCount = 0 Then
                grdSIG.CurrentRowIndex = 0
                grdSIG.Select(grdSIG.CurrentRowIndex)
            End If
            '''''added upto here
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "SIG", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FormClose()
        Me.Close()
    End Sub
    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Add"
                Call AddCategory()
            Case "Modify"
                Call UpdateCategory()
            Case "Delete"
                Call DeleteCategory()
            Case "Refresh"
                lblSearch.Text = "Search :"
                Call RefreshCategory()
            Case "Close"
                Call FormClose()
        End Select
    End Sub

    Private Sub Panel1_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel1.Resize
        'code added by dipak 20090902 for set flexGrid value when panel1 resize
        grdSIG.DataSource = objSIG.GetAllSIG
        CustomGridStyle()
        'end add by dipak
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'SHUBHANGI 200909390
        'Use Clear button to clear serach box 
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

End Class
