Imports System.Data.SqlClient

Public Class frmVWCategory
    Inherits System.Windows.Forms.Form
    Private objDBLayer As New ClsDBLayer
    Private intCatID As Long
    Private strCatDesc As String
    Private strcatype As String
    Private strCode As String
    '' To Allow Sreach
    Private _blnSearch As Boolean = True
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    '  Dim objSmartOrderDBLayer As ClsSmartorderDBLayer
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents panel4 As System.Windows.Forms.Panel
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents label20 As System.Windows.Forms.Label
    Friend WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents label24 As System.Windows.Forms.Label
    Dim strsortorder As String

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

                If (IsNothing(dgCategoryList) = False) Then
                    dgCategoryList.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dgCategoryList)
                    dgCategoryList.Dispose()
                    dgCategoryList = Nothing
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
    Friend WithEvents dgCategoryList As System.Windows.Forms.DataGrid
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVWCategory))
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.label21 = New System.Windows.Forms.Label()
        Me.label20 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.label22 = New System.Windows.Forms.Label()
        Me.label23 = New System.Windows.Forms.Label()
        Me.label24 = New System.Windows.Forms.Label()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dgCategoryList = New System.Windows.Forms.DataGrid()
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
        Me.pnlTopRight.SuspendLayout()
        Me.panel4.SuspendLayout()
        CType(Me.dgCategoryList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
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
        Me.pnlTopRight.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(670, 24)
        Me.pnlTopRight.TabIndex = 0
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
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(5, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(214, 15)
        Me.txtSearch.TabIndex = 1
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
        'label20
        '
        Me.label20.BackColor = System.Drawing.Color.White
        Me.label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label20.Location = New System.Drawing.Point(5, 17)
        Me.label20.Name = "label20"
        Me.label20.Size = New System.Drawing.Size(214, 5)
        Me.label20.TabIndex = 43
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
        Me.btnClear.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.btnClear, "Clear search")
        Me.btnClear.UseVisualStyleBackColor = True
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
        Me.lblSearch.Text = "  Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(1, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(668, 1)
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
        Me.Label3.Location = New System.Drawing.Point(669, 1)
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
        Me.Label4.Image = CType(resources.GetObject("Label4.Image"), System.Drawing.Image)
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(670, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'dgCategoryList
        '
        Me.dgCategoryList.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.dgCategoryList.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgCategoryList.BackgroundColor = System.Drawing.Color.White
        Me.dgCategoryList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgCategoryList.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgCategoryList.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgCategoryList.CaptionForeColor = System.Drawing.Color.White
        Me.dgCategoryList.CaptionVisible = False
        Me.dgCategoryList.DataMember = ""
        Me.dgCategoryList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgCategoryList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgCategoryList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgCategoryList.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.dgCategoryList.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.dgCategoryList.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgCategoryList.HeaderForeColor = System.Drawing.Color.White
        Me.dgCategoryList.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgCategoryList.Location = New System.Drawing.Point(3, 1)
        Me.dgCategoryList.Name = "dgCategoryList"
        Me.dgCategoryList.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgCategoryList.ParentRowsForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.dgCategoryList.ReadOnly = True
        Me.dgCategoryList.RowHeadersVisible = False
        Me.dgCategoryList.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.dgCategoryList.SelectionForeColor = System.Drawing.Color.Black
        Me.dgCategoryList.Size = New System.Drawing.Size(670, 431)
        Me.dgCategoryList.TabIndex = 2
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(676, 53)
        Me.pnlToolStrip.TabIndex = 2
        '
        'ts_ViewButtons
        '
        Me.ts_ViewButtons.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ts_ViewButtons.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_ViewButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_ViewButtons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ts_ViewButtons.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_ViewButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_ViewButtons.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(676, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.TabStop = True
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnAdd
        '
        Me.ts_btnAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
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
        Me.ts_btnModify.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
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
        Me.ts_btnDelete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
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
        Me.ts_btnRefresh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
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
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
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
        Me.Panel1.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel1.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel1.Controls.Add(Me.lbl_RightBrd)
        Me.Panel1.Controls.Add(Me.lbl_TopBrd)
        Me.Panel1.Controls.Add(Me.dgCategoryList)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 83)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(676, 435)
        Me.Panel1.TabIndex = 1
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(4, 431)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(668, 1)
        Me.lbl_BottomBrd.TabIndex = 8
        Me.lbl_BottomBrd.Text = "label2"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 2)
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
        Me.lbl_RightBrd.Location = New System.Drawing.Point(672, 2)
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
        Me.lbl_TopBrd.Location = New System.Drawing.Point(3, 1)
        Me.lbl_TopBrd.Name = "lbl_TopBrd"
        Me.lbl_TopBrd.Size = New System.Drawing.Size(670, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pnlTopRight)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(676, 30)
        Me.Panel2.TabIndex = 0
        '
        'frmVWCategory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(676, 518)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmVWCategory"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Category"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTopRight.ResumeLayout(False)
        Me.panel4.ResumeLayout(False)
        Me.panel4.PerformLayout()
        CType(Me.dgCategoryList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmVWCategory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            dgCategoryList.AllowSorting = True
            Bindgrid()
            Me.Cursor = Cursors.Default
            txtSearch.Clear()
            _blnSearch = True
            If Not dgCategoryList.VisibleRowCount = 0 Then
                dgCategoryList.CurrentRowIndex = 0
                dgCategoryList.Select(dgCategoryList.CurrentRowIndex)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub Bindgrid(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        Try
            objDBLayer.FetchData()
            dgCategoryList.SetDataBinding(objDBLayer.DsDataview, "")
            txtSearch.Text = ""
            txtSearch.Text = strsearchtxt
            If strcolumnName = "" Then
                objDBLayer.SortDataview(objDBLayer.DsDataview.Table.Columns(2).ColumnName)
            Else
                Dim strColumn As String = Replace(strcolumnName, "[", "")

                objDBLayer.SortDataview(strColumn, strSortBy)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        'dgCategoryList.DataBindings = objDBLayer.DsDataSet
        'dgCategoryList.DataMember = "Category_Mst"
    End Sub
    Private Sub HideColumn()
        Dim ts As New clsDataGridTableStyle(objDBLayer.DsDataview.Table.TableName)

        Dim dgCategory As New DataGridTextBoxColumn

        With dgCategory
            .MappingName = objDBLayer.DsDataview.Table.Columns(0).ColumnName
            .Alignment = HorizontalAlignment.Center
            .NullText = ""
            .Width = 0
        End With
        Dim dgCode As New DataGridTextBoxColumn
        With dgCode
            .MappingName = objDBLayer.DsDataview.Table.Columns(1).ColumnName
            .HeaderText = "Code"
            .NullText = ""
            .Width = 0.2 * dgCategoryList.Width
        End With

        Dim dgDescription As New DataGridTextBoxColumn
        With dgDescription
            .MappingName = objDBLayer.DsDataview.Table.Columns(2).ColumnName
            .HeaderText = "Description"
            .NullText = ""
            .Width = 0.2 * dgCategoryList.Width
        End With

        Dim dgCategoryType As New DataGridTextBoxColumn
        With dgCategoryType
            .MappingName = objDBLayer.DsDataview.Table.Columns(3).ColumnName
            .HeaderText = "Category Type"
            .NullText = ""
            .Width = 0.2 * dgCategoryList.Width - 10
        End With

        Dim dgIsSystem As New DataGridTextBoxColumn
        With dgIsSystem
            .MappingName = objDBLayer.DsDataview.Table.Columns(4).ColumnName
            .HeaderText = "IsSystemDefined"
            .NullText = ""
            .Width = 0
        End With

        Dim dgParentCode As New DataGridTextBoxColumn
        With dgParentCode
            .MappingName = objDBLayer.DsDataview.Table.Columns(5).ColumnName
            .HeaderText = "Parent Code"
            .NullText = ""
            .Width = 0.2 * dgCategoryList.Width
        End With

        Dim dgParentCategory As New DataGridTextBoxColumn
        With dgParentCategory
            .MappingName = objDBLayer.DsDataview.Table.Columns(6).ColumnName
            .HeaderText = "Parent Category"
            .NullText = ""
            .Width = 0.2 * dgCategoryList.Width
        End With

        ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {dgCategory, dgCode, dgDescription, dgCategoryType, dgIsSystem, dgParentCode, dgParentCategory})
        dgCategoryList.TableStyles.Clear()
        dgCategoryList.TableStyles.Add(ts)
        If Not dgCategoryList.VisibleRowCount = 0 Then
            dgCategoryList.CurrentRowIndex = 0
            dgCategoryList.Select(dgCategoryList.CurrentRowIndex)
        End If
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            ''--Added by Anil on 20071213
            mdlGeneral.ValidateText(txtSearch.Text, e)
            ''----
        Catch ex As Exception

        End Try
    End Sub



    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim dvPatient As DataView
                dvPatient = CType(dgCategoryList.DataSource(), DataView)

                If IsNothing(dvPatient) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If

                ' dgCategoryList.DataSource = dvPatient
                Dim strPatientSearchDetails As String

                If Trim(txtSearch.Text) <> "" Then
                    strPatientSearchDetails = Replace(txtSearch.Text, "'", "''") & ""
                    ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                    strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                    strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
                Else
                    strPatientSearchDetails = ""
                End If

                'USE GENERAL SEARCH INSTEAD OF COLUMN WISE SEARCH
                dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                                    & dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%' OR " _
                                    & dvPatient.Table.Columns(3).ColumnName & " Like '%" & strPatientSearchDetails & "%' "

                'Shubhangi 20091202
                'To set focus on first line after filterate
                If dvPatient.Count > 0 Then
                    dgCategoryList.Select(0)
                End If



                'CODE COMMENTED BY SHUBHANGI 20091001 

                'If lblSearch.Text.Contains("Description") Then
                '    dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                'ElseIf lblSearch.Text.Contains("Category") Then
                '    dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                'End If


                '    Case "Description : "
                ''''''Code Modified by Anil on 24/09/2007 at 3:15 p.m.
                ''''''This change is made to get In-String search i.e.,for any string which has the character/characters present in the search textbox.
                ''''''Previously the commented code was searching for the strings which are having first character same as that in search textbox 
                ''''''and also it was searching strings which are having the character in between the words but for that we had to use " % " or " * " sign before that character. But now it is not required to add % or * signs.

                ' ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                'dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                ' '' Else
                ' ''dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                ' ''End If
                '    Case "Category Type : "
                ' ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                'dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '%" & strPatientSearchDetails & "%'"
                ' ''Else
                ' ''dvPatient.RowFilter = dvPatient.Table.Columns(2).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                ' ''End If
                ''''''Upto here the changes are made by Anil on 24/09/2007 at 3:15 p.m.
                'End Select
                Me.Cursor = Cursors.Default
            Catch objErr As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(objErr.ToString, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

    End Sub



    'Private Sub UpdateCategory()


    'End Sub


    Private Sub frmVWCategory_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try

        Catch ex As Exception
        End Try
    End Sub


    Private Sub dgCategoryList_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgCategoryList.MouseDoubleClick
        Try
            ' dgCategoryList.AllowSorting = False
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = dgCategoryList.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then

                'COMMENTED BY SHUBHANGI 20091001 
                'Select Case htInfo.Column ' dgCategoryList.CurrentCell.ColumnNumber
                '    Case 1
                '        lblSearch.Text = "  Description : "
                '    Case 2
                '        lblSearch.Text = "  Category Type : "
                'End Select

                If txtSearch.Text = "" Then
                    _blnSearch = True
                Else
                    _blnSearch = False
                    txtSearch.Text = ""
                    _blnSearch = True
                End If
            ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
                _blnSearch = True
                UpdateCategory()
            End If
            ' dgCategoryList.AllowSorting = True
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub AddCategory()
        Dim frm As New CategoryMaster

        Try

            frm.Text = "Add Category"
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            Bindgrid()
            Dim myDataView As DataView = CType(dgCategoryList.DataSource, DataView)
            If (IsNothing(myDataView) = False) Then


                Dim i As Integer
                For i = 0 To myDataView.Table.Rows.Count - 1
                    If frm._CategoryName = dgCategoryList.Item(i, 2) Then
                        dgCategoryList.CurrentRowIndex = i
                        dgCategoryList.Select(i)
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            frm.Dispose()
            frm = Nothing
        End Try
    End Sub
    Private Sub UpdateCategory()
        Dim EditFavorite As Boolean = False
        Dim _isSystemDefinedItem As Boolean = False

        If dgCategoryList.VisibleRowCount >= 1 Then
            strCatDesc = dgCategoryList.Item(dgCategoryList.CurrentRowIndex, 2)
            _isSystemDefinedItem = Convert.ToBoolean(dgCategoryList.Item(dgCategoryList.CurrentRowIndex, 4))
            collCategoryDesc = New Collection
            collCategoryDesc.Clear()
            FillCategoryDescription_Collection()
            strCatDesc = dgCategoryList.Item(dgCategoryList.CurrentRowIndex, 2)
            strcatype = dgCategoryList.Item(dgCategoryList.CurrentRowIndex, 3)
            strCode = dgCategoryList.Item(dgCategoryList.CurrentRowIndex, 1)
            If (strcatype.ToUpper() = "LANGUAGE") Or (strcatype.ToUpper() = "RACE") Or (strcatype.ToUpper() = "ETHNICITY") Then
                If (IsCategoryUsedInPatientDetails(strCatDesc, strcatype, strCode) = True) Then
                    If gloSettings.gloEMRAdminSettings.globlnEnableMultipleRaceFeatures Then
                        If (strcatype.ToUpper() = "LANGUAGE") Or (strcatype.ToUpper() = "RACE") Then
                            EditFavorite = True
                        Else
                            MessageBox.Show("Selected category is associated with patient(s),can not be modify.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        MessageBox.Show("Selected category is associated with patient(s),can not be modify.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If

                End If
            End If
            For j As Integer = 1 To collCategoryDesc.Count
                If collCategoryDesc.Item(j) = strCatDesc Then
                    MessageBox.Show("System defined categories can not be modified.  ", "Category", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            Next
            If (strCatDesc = "Federal" Or strCatDesc = "State" Or strCatDesc = "Private") And strcatype = "Funding Source" Then
                MessageBox.Show("System defined Records can not be modified.  ", "Category", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If ((strcatype.ToUpper() = "UNIT OF MEASURE CODES") Or (strcatype.ToUpper() = "PUBLICITY CODE") Or (strcatype.ToUpper() = "LANGUAGE") Or (strcatype.ToUpper() = "GENDER IDENTITY") Or (strcatype.ToUpper() = "SEXUAL ORIENTATION") Or (strcatype.ToUpper() = "RACE SPECIFICATION") Or (strcatype.ToUpper() = "ETHNICITY SPECIFICATION") Or (strcatype.ToUpper() = "GENDER")) And _isSystemDefinedItem = True Then

                MessageBox.Show("System defined Records can not be modified.", "Category", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub

            End If

            Dim grdIndex As Integer
            intCatID = Int64.Parse(dgCategoryList.Item(dgCategoryList.CurrentRowIndex, 0))
            grdIndex = dgCategoryList.CurrentRowIndex
            Dim myDataView As DataView = CType(dgCategoryList.DataSource, DataView)
            If (IsNothing(myDataView) = False) Then


                sortOrder = myDataView.Sort
                strSearchstring = txtSearch.Text.Trim
                arrcolumnsort = Split(sortOrder, "]")
                If arrcolumnsort.Length > 1 Then
                    strcolumnName = arrcolumnsort.GetValue(0)
                    strsortorder = arrcolumnsort.GetValue(1)
                End If
            End If
            Dim frm As CategoryMaster
            If EditFavorite Then
                frm = New CategoryMaster(intCatID, EditFavorite)
            Else
                frm = New CategoryMaster(intCatID)
            End If

            frm.Text = "Update Category"
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            Bindgrid(strcolumnName, strsortorder, strSearchstring)
            Dim myDatagView As DataView = CType(dgCategoryList.DataSource, DataView)
            If (IsNothing(myDatagView) = False) Then

                Dim i As Integer
                For i = 0 To CType(dgCategoryList.DataSource, DataView).Count - 1 ''  Table.Rows.Count - 1
                    If intCatID = dgCategoryList.Item(i, 0) Then
                        dgCategoryList.CurrentRowIndex = i
                        dgCategoryList.Select(i)
                        Exit For
                    End If
                Next
                frm.Dispose()
                frm = Nothing
            End If

        End If
    End Sub
    Private Sub DeleteCategory()
        Dim _isSystemDefinedItem As Boolean = False

        Try
            If dgCategoryList.VisibleRowCount >= 1 Then
                strCatDesc = dgCategoryList.Item(dgCategoryList.CurrentRowIndex, 2)
                strcatype = dgCategoryList.Item(dgCategoryList.CurrentRowIndex, 3)
                strCode = dgCategoryList.Item(dgCategoryList.CurrentRowIndex, 1)
                _isSystemDefinedItem = Convert.ToBoolean(dgCategoryList.Item(dgCategoryList.CurrentRowIndex, 4))


                collCategoryDesc = New Collection
                collCategoryDesc.Clear()
                FillCategoryDescription_Collection()

                '25-May-15 Aniket: Resolving Bug #83495: GloEMR->Edit Category->Ob Plan->Modify OB Plan first Trimester->system should not allow item to modify/delete
                If ((strcatype.ToUpper() = "UNIT OF MEASURE CODES") Or (strcatype.ToUpper() = "PUBLICITY CODE") Or (strcatype.ToUpper() = "LANGUAGE")) Or _isSystemDefinedItem = True Then

                    MessageBox.Show("System defined categories can not be deleted.", "Category", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub

                End If

                If ((strcatype.ToUpper() = "GENDER IDENTITY") Or (strcatype.ToUpper() = "SEXUAL ORIENTATION") Or (strcatype.ToUpper() = "RACE SPECIFICATION") Or (strcatype.ToUpper() = "ETHNICITY SPECIFICATION")) And _isSystemDefinedItem = True Then

                    MessageBox.Show("System defined categories can not be deleted.", "Category", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub

                End If

                If (strcatype.ToUpper() = "LANGUAGE") Or (strcatype.ToUpper() = "RACE") Or (strcatype.ToUpper = "ETHNICITY") Or (strcatype.ToUpper() = "GENDER IDENTITY") Or (strcatype.ToUpper() = "SEXUAL ORIENTATION") Or (strcatype.ToUpper() = "RACE SPECIFICATION") Or (strcatype.ToUpper() = "ETHNICITY SPECIFICATION") Or (strcatype.ToUpper() = "GENDER") Then
                    If (IsCategoryUsedInPatientDetails(strCatDesc, strcatype, strCode) = True) Then
                        MessageBox.Show("Selected category is associated with patient(s),can not be deleted.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If


                For i As Integer = 1 To collCategoryDesc.Count
                    '11-Nov-14 Aniket: Bug #75683: gloEMR - History - Default History category is not getting loaded after opening history,exception is coming.
                    If collCategoryDesc.Item(i) = strCatDesc Or strCatDesc = "Allergies" Then
                        MessageBox.Show("System defined categories can not be Deleted.", "Category", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                Next



                If strcatype.ToUpper() = "PUBLICITY CODE" And _isSystemDefinedItem = False Then

                    If IsNothing(objDBLayer) Then
                        objDBLayer = New ClsDBLayer
                    End If

                    If (objDBLayer.IsPublicityCodeUsedonIM(strCode) = True) Then
                        MessageBox.Show("Selected publicity code is associated with patient immunization(s),can not be deleted.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If

                End If


                If (strCatDesc = "Federal" Or strCatDesc = "State" Or strCatDesc = "Private") And strcatype = "Funding Source" Then
                    MessageBox.Show("System defined Records can not be modified.  ", "Category", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                ''20120831-Bug No 34235
                If strcatype = "Template Specialty" Then
                    If (IsspecialtyUsed(strCatDesc, strcatype, strCode) = True) Then
                        MessageBox.Show("Selected template specialty is associated with patient exam,can not be deleted.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If

                    '03-Mar-15 Aniket: Resolving Bug #80007 ( Modified): gloEMR: Patient Consent Tracking- Applicaion deletes in-use consent type
                ElseIf strcatype = "Consent Type" Then
                    If (ISPatientConsentUsed(Int64.Parse(dgCategoryList.Item(dgCategoryList.CurrentRowIndex, 0))) = True) Then
                        MessageBox.Show("Consent Type is used further and cannot be deleted.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If



                ElseIf strcatype = "CPT" Then
                    If strCatDesc <> "" Then
                        intCatID = Int64.Parse(dgCategoryList.Item(dgCategoryList.CurrentRowIndex, 0))
                        If objDBLayer.IsCPTCategoryInUse(intCatID, strCatDesc) Then
                            MessageBox.Show("CPT Category is in use. It cannot be deleted.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    End If
                End If





                Dim strMessage As String
                strMessage = ""
                If strcatype.ToUpper = "ORDERSET" Then
                    intCatID = Int64.Parse(dgCategoryList.Item(dgCategoryList.CurrentRowIndex, 0))
                    Dim dt As DataTable
                    Dim strQuery As String
                    Dim ODB As New gloStream.gloDataBase.gloDataBase
                    strQuery = "SELECT  Orderset.nOrderID " & _
                    " FROM Orderset INNER JOIN Category_MST ON Orderset.nOrderID = Category_MST.nCategoryID  " & _
                    " WHERE Orderset.nOrderID =" & intCatID & " "
                    ODB.Connect(GetConnectionString)
                    dt = ODB.ReadQueryDataTable(strQuery)
                    ODB.Disconnect()
                    ODB.Dispose()
                    ODB = Nothing

                    If (IsNothing(dt) = False) Then
                        If (dt.Rows.Count > 0) Then
                            strMessage = "Order Set Category """ & strCatDesc & """ has Order Groups and/or Tests assigned to it." & vbCrLf & "If you delete this Order Set Category, these items will also be deleted." & _
                                        "" & vbCrLf & vbCrLf & "Any existing patient orders will be preserved and will not be affected by deleting the Order Set Category." & vbCrLf & "Would you like to proceed and delete Order Set Category """ & strCatDesc & """?"
                        Else
                            strMessage = "Are you sure you want to delete this record?"
                        End If
                    Else
                        strMessage = "Are you sure you want to delete this record?"
                    End If

                Else
                    strMessage = "Are you sure you want to delete this record?"


                End If
                'dt = objSmartOrderDBLayer.FetchOrderforUpdate(intCatID)
                If MessageBox.Show(strMessage, gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                    intCatID = Int64.Parse(dgCategoryList.Item(dgCategoryList.CurrentRowIndex, 0))

                    Try
                        'Portal
                        If strcatype.ToUpper = "HISTORY" Or strcatype.ToUpper = "ROS" Then

                            If CheckIsCatagoryExistsinPortal(intCatID) Then
                                Exit Sub
                            End If

                        End If

                        objDBLayer.DeleteData(intCatID)
                        ''[Dhruv] Delte it from the Im_Manufacturers table if is delelted from the category master
                        If Not IsNothing(dgCategoryList) Then
                            Dim sCodeType As String = "Manufacturer"
                            Dim sDataGridCodeType As String = dgCategoryList.Item(dgCategoryList.CurrentRowIndex, 3)
                            Dim sDataGridManufacurerCode As String = dgCategoryList.Item(dgCategoryList.CurrentRowIndex, 1)
                            Dim sDataGridManufacturer As String = dgCategoryList.Item(dgCategoryList.CurrentRowIndex, 2)
                            If sDataGridCodeType = sCodeType Then
                                objDBLayer.DeleteData(sDataGridManufacurerCode, sDataGridManufacturer)
                            End If
                        End If
                        ''End dhruv -----------------------
                    Catch ex As SqlClient.SqlException
                        MessageBox.Show(ex.Message, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    Dim myDataView As DataView = CType(dgCategoryList.DataSource, DataView)
                    If (IsNothing(myDataView) = False) Then


                        sortOrder = myDataView.Sort
                        strSearchstring = txtSearch.Text.Trim
                        arrcolumnsort = Split(sortOrder, "]")
                        If arrcolumnsort.Length > 1 Then
                            strcolumnName = arrcolumnsort.GetValue(0)
                            strsortorder = arrcolumnsort.GetValue(1)
                        End If
                    End If

                    Bindgrid(strcolumnName, strsortorder, strSearchstring)
                    If Not dgCategoryList.VisibleRowCount = 0 Then
                        dgCategoryList.CurrentRowIndex = 0
                        dgCategoryList.Select(dgCategoryList.CurrentRowIndex)
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function CheckIsCatagoryExistsinPortal(ByVal CategoryID As Long) As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim _dt As DataTable = Nothing
        Dim _dt2 As DataTable = Nothing
        Try

            oParameters = New gloDatabaseLayer.DBParameters()
            oParameters.Add("@ncategoryid", CategoryID, ParameterDirection.Input, SqlDbType.BigInt)
            oParameters.Add("@IsDelete", False, ParameterDirection.Input, SqlDbType.Bit)
            oDB.Connect(False)
            oDB.Retrive("WS_IsHistorycategoryExistsinHealthform", oParameters, _dt)
            If _dt IsNot Nothing AndAlso _dt.Rows.Count > 0 Then

                If MessageBox.Show("Selected category is used in patient portal forms. Once this category is deleted then patients can no longer see this category in patient portal forms." + System.Environment.NewLine + System.Environment.NewLine + "Do you want to continue with the deletion?", gstrMessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
                    Try
                        ' Set IsRepublish Required to 1 & Delete Entry 
                        If oParameters IsNot Nothing Then

                            oParameters.Dispose()
                            oParameters = Nothing
                        End If
                        oParameters = New gloDatabaseLayer.DBParameters()
                        oParameters.Add("@ncategoryid", CategoryID, ParameterDirection.Input, SqlDbType.BigInt)
                        oParameters.Add("@IsDelete", True, ParameterDirection.Input, SqlDbType.Bit)
                        oDB.Connect(False)
                        oDB.Retrive("WS_IsHistorycategoryExistsinHealthform", oParameters, _dt2)
                    Catch ex As Exception
                    End Try
                    Return False
                Else
                    Return True
                End If
            Else
                Return False

            End If
        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            Throw dbEx
        Finally
            If oParameters IsNot Nothing Then

                oParameters.Dispose()
                oParameters = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(_dt) Then
                _dt.Dispose()
                _dt = Nothing
            End If

            If Not IsNothing(_dt2) Then
                _dt2.Dispose()
                _dt2 = Nothing
            End If
        End Try

        Return False

    End Function

    Private Function ValidateCPTCategoryData(CategoryID As Long, OriginalCategoryDescription As String) As Boolean
        Dim blnRetval As Boolean = False

        Try
            'validation to check whether the cpt category in use.
            If strCatDesc <> "" Then

                If objDBLayer.IsCPTCategoryInUse(CategoryID, OriginalCategoryDescription) Then
                    ' objDBLayer.Dispose() : SLR'Don't dispose, since it is used in many places>>>
                    Dim oDlgResult As DialogResult = DialogResult.None
                    oDlgResult = MessageBox.Show("CPT Category is in use. Do you want to modify and update it?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If oDlgResult = System.Windows.Forms.DialogResult.Yes Then


                        'user do not want to update hence keep it as it is
                        blnRetval = True
                    Else
                        'return false so that the main Category Master table will also get updated
                        blnRetval = False
                    End If

                    Return blnRetval
                Else
                    'if category not in use then user should be allowed to modify it.
                    blnRetval = True

                End If
            End If

            Return blnRetval
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Add, "Add Category", 0, intCatID, 0, gloAuditTrail.ActivityOutCome.Failure)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            If objDBLayer IsNot Nothing Then
                'objDBLayer.Dispose(): SLR'Don't dispose, since it is used in many places>>>
            End If

            Return blnRetval
        Finally
            If objDBLayer IsNot Nothing Then
                'objDBLayer.Dispose() : SLR'Don't dispose, since it is used in many places>>>
            End If
        End Try

    End Function


    Private Sub RefreshCategory()
        Me.Cursor = Cursors.WaitCursor
        Bindgrid()
        Me.Cursor = Cursors.Default
        '''''Following code lines are addded by Anil 0n 26/09/07 at 12:05 p.m.
        '''''This code clears the search textbox and gets the focus on the first row of the grid on click of Refresh button.
        txtSearch.Clear()
        _blnSearch = True
        If Not dgCategoryList.VisibleRowCount = 0 Then
            dgCategoryList.CurrentRowIndex = 0
            dgCategoryList.Select(dgCategoryList.CurrentRowIndex)
        End If
        '''''upto here code added
    End Sub
    Private Sub FormClose()

        Me.Close()
        objDBLayer.Dispose()
        objDBLayer = Nothing
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
                Call RefreshCategory()
            Case "Close"
                Call FormClose()
        End Select
    End Sub

    ''Event sdded by Sandip Darade 20090306 to select the entire row
    Private Sub dgCategoryList_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgCategoryList.KeyUp
        Try
            If dgCategoryList.CurrentRowIndex >= 0 Then
                dgCategoryList.Select(dgCategoryList.CurrentRowIndex)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''Event sdded by Sandip Darade 20090306 to select the entire row
    Private Sub dgCategoryList_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dgCategoryList.MouseUp
        Try
            If dgCategoryList.CurrentRowIndex >= 0 Then
                dgCategoryList.Select(dgCategoryList.CurrentRowIndex)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'SHUBHANGI 20090930
        'Use Clear Button to Clear Search text box.
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub
    'Shubhangi 20091202
    'Add this event coz this event is fire after load so at that time form's size is maximize so the column size become appropriate according to actual size of control
    Private Sub frmVWCategory_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        HideColumn()
    End Sub
    Public Function IsCategoryUsedInPatientDetails(ByVal categoryName As String, ByVal categoryType As String, ByVal sCode As String) As Boolean
        Try
            Dim oResult As Object
            oResult = "0"

            Dim con As New SqlConnection(GetConnectionString)
            Dim cmd As New SqlCommand()
            cmd.Connection = con
            If (categoryType = "Language" Or categoryType = "Gender") Then
                cmd.CommandText = "SELECT COUNT(*) FROM patient where sLang= '" & categoryName.Replace("'", "''") & "'"
            ElseIf (categoryType.ToUpper() = "ETHNICITY" Or categoryType.ToUpper() = "ETHNICITY SPECIFICATION") Then
                cmd.CommandText = "SELECT COUNT(*) FROM Patient_EthnicitySpecification where sEthnicitySpecificationDescription= '" & categoryName.Replace("'", "''") & "'"
            ElseIf (categoryType.ToUpper() = "RACE" Or categoryType.ToUpper() = "RACE SPECIFICATION") Then
                cmd.CommandText = "SELECT COUNT(*) FROM Patient_RaceSpecification where sRaceSpecificationDescription= '" & categoryName.Replace("'", "''") & "'"
            ElseIf (categoryType.ToUpper() = "SEXUAL ORIENTATION") Then
                cmd.CommandText = "SELECT COUNT(*) FROM dbo.Patient_OtherDetails AS POD WHERE ISNULL(POD.nSexualOrientationCategoryID,0)<>0 AND POD.sSexualOrientationDesc= '" & categoryName.Replace("'", "''") & "'"
            ElseIf (categoryType.ToUpper() = "GENDER IDENTITY") Then
                cmd.CommandText = "SELECT COUNT(*) FROM dbo.Patient_OtherDetails AS POD WHERE ISNULL(POD.nGenderIdentityCateroryID,0)<>0 AND POD.sGenderIdentityDesc= '" & categoryName.Replace("'", "''") & "'"
            End If

            con.Open()
            oResult = cmd.ExecuteScalar

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            con.Close()
            con.Dispose()
            con = Nothing

            'If (categoryType = "Language" Or categoryType = "Ethnicity" Or categoryType.ToUpper() = "RACE" Or categoryType.ToUpper() = "SEXUAL ORIENTATION" Or categoryType.ToUpper() = "GENDER IDENTITY" Or strcatype.ToUpper() = "RACE SPECIFICATION" Or strcatype.ToUpper() = "ETHNICITY SPECIFICATION") Then
            '    Dim con As New SqlConnection(GetConnectionString)
            '    Dim cmd As New SqlCommand()
            '    cmd.Connection = con
            '    If (categoryType = "Language") Then
            '        cmd.CommandText = "SELECT COUNT(*) FROM patient where sLang= '" & categoryName.Replace("'", "''") & "'"
            '    ElseIf (categoryType = "Ethnicity") Then
            '        cmd.CommandText = "SELECT COUNT(*) FROM patient where sEthn= '" & categoryName.Replace("'", "''") & "'"
            '    ElseIf (categoryType.ToUpper() = "SEXUAL ORIENTATION") Then
            '        cmd.CommandText = "SELECT COUNT(*) FROM dbo.Patient_OtherDetails AS POD WHERE ISNULL(POD.nSexualOrientationCategoryID,0)<>0 AND POD.sSexualOrientationDesc= '" & categoryName.Replace("'", "''") & "'"
            '    ElseIf (categoryType.ToUpper() = "GENDER IDENTITY") Then
            '        cmd.CommandText = "SELECT COUNT(*) FROM dbo.Patient_OtherDetails AS POD WHERE ISNULL(POD.nGenderIdentityCateroryID,0)<>0 AND POD.sGenderIdentityDesc= '" & categoryName.Replace("'", "''") & "'"
            '    End If

            '    con.Open()
            '    oResult = cmd.ExecuteScalar

            '    If cmd IsNot Nothing Then
            '        cmd.Parameters.Clear()
            '        cmd.Dispose()
            '        cmd = Nothing
            '    End If

            '    con.Close()
            '    con.Dispose()
            '    con = Nothing
            'ElseIf (categoryType = "Race") Then
            '    oResult = GetRaceUsedinPatientDetail(categoryName)
            'End If

            If CType(oResult, Int16) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return True
        End Try
    End Function

    Private Function GetRaceUsedinPatientDetail(sRace As String) As Integer

        Dim Con As SqlConnection
        Dim cmd As SqlCommand
        Dim count As Integer

        Try
            Con = New SqlConnection(GetConnectionString())
            cmd = New SqlCommand("GetRaceUsedinPatientDetail", Con)
            Dim objParam As SqlParameter

            objParam = cmd.Parameters.Add("@sRace", SqlDbType.Text)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sRace

            cmd.CommandType = CommandType.StoredProcedure

            Con.Open()
            count = cmd.ExecuteScalar
            Con.Close()

            If IsDBNull(count) = True Then
                count = 0
            End If

            If IsNothing(Con) = False Then
                Con.Dispose() : Con = Nothing
            End If

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose() : cmd = Nothing
            End If

            Return count

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function


    Public Function IsspecialtyUsed(ByVal categoryName As String, ByVal categoryType As String, ByVal sCode As String) As Boolean
        Try
            Dim con As New SqlConnection(GetConnectionString)
            Dim cmd As New SqlCommand()
            cmd.Connection = con
            cmd.CommandText = "select COUNT(*) from PatientExams where sTemplateSpecility= '" + categoryName + "'"
            Dim oResult As Object
            con.Open()
            oResult = cmd.ExecuteScalar
            con.Close()
            con.Dispose()
            con = Nothing

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If CType(oResult, Int32) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return True
        End Try
    End Function

    Public Function ISPatientConsentUsed(ByVal nConsentType As Long) As Boolean

        Dim Con As SqlConnection
        Dim cmd As SqlCommand
        Dim blnResult As Boolean

        Try
            Con = New SqlConnection(GetConnectionString())
            cmd = New SqlCommand("gsp_ISPatientConsentUsed", Con)
            Dim objParam As SqlParameter

            objParam = cmd.Parameters.Add("@nConsentType", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = nConsentType

            cmd.CommandType = CommandType.StoredProcedure

            Con.Open()
            blnResult = cmd.ExecuteScalar
            Con.Close()

            If IsNothing(Con) = False Then
                Con.Dispose() : Con = Nothing
            End If

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose() : cmd = Nothing
            End If

            Return blnResult

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Consent", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return True
        End Try

    End Function


End Class
