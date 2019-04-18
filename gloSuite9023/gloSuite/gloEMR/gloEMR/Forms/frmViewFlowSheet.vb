Public Class frmViewFlowSheet
    Inherits System.Windows.Forms.Form
    Public Shared blnModify As Boolean
    Public CategoryID As Int64
    Friend CategoryType As String
    Dim objFlowSheet As New clsFlowSheet
    Dim _blnSearch As Boolean = True
    Dim i As Integer
    Dim sortOrder As String
    Dim strSearchstring As String
    Dim arrcolumnsort() As String
    Dim strcolumnName As String
    Dim strsortorder As String

#Region " Windows Controls "
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_ViewButtons As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents ts_btnAdd As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents ts_gloCommunityDownload As System.Windows.Forms.ToolStripButton
    Friend WithEvents panel4 As System.Windows.Forms.Panel
    Friend WithEvents label21 As System.Windows.Forms.Label
    Friend WithEvents label20 As System.Windows.Forms.Label
    Friend WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents label24 As System.Windows.Forms.Label
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
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
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Try

                If (IsNothing(grdFlowSheet) = False) Then
                    grdFlowSheet.TableStyles.Clear()
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(grdFlowSheet)
                    grdFlowSheet.Dispose()
                    grdFlowSheet = Nothing
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
    Friend WithEvents pnlGrid As System.Windows.Forms.Panel
    Friend WithEvents grdFlowSheet As System.Windows.Forms.DataGrid
    Friend WithEvents pnlTopRight1 As System.Windows.Forms.Panel
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewFlowSheet))
        Me.pnlTopRight1 = New System.Windows.Forms.Panel()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.lbl_BottomBrd = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.lbl_RightBrd = New System.Windows.Forms.Label()
        Me.lbl_TopBrd = New System.Windows.Forms.Label()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.grdFlowSheet = New System.Windows.Forms.DataGrid()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.ts_ViewButtons = New gloGlobal.gloToolStripIgnoreFocus()
        Me.ts_btnAdd = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnModify = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ts_gloCommunityDownload = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.label20 = New System.Windows.Forms.Label()
        Me.label21 = New System.Windows.Forms.Label()
        Me.label22 = New System.Windows.Forms.Label()
        Me.label23 = New System.Windows.Forms.Label()
        Me.label24 = New System.Windows.Forms.Label()
        Me.pnlTopRight1.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        CType(Me.grdFlowSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_ViewButtons.SuspendLayout()
        Me.pnlTopRight.SuspendLayout()
        Me.panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTopRight1
        '
        Me.pnlTopRight1.BackColor = System.Drawing.Color.Transparent
        Me.pnlTopRight1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight1.Controls.Add(Me.panel4)
        Me.pnlTopRight1.Controls.Add(Me.lblSearch)
        Me.pnlTopRight1.Controls.Add(Me.lbl_BottomBrd)
        Me.pnlTopRight1.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlTopRight1.Controls.Add(Me.lbl_RightBrd)
        Me.pnlTopRight1.Controls.Add(Me.lbl_TopBrd)
        Me.pnlTopRight1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight1.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight1.Name = "pnlTopRight1"
        Me.pnlTopRight1.Size = New System.Drawing.Size(914, 24)
        Me.pnlTopRight1.TabIndex = 1
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
        Me.btnClear.TabIndex = 47
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
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(64, 22)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 23)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(912, 1)
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
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 23)
        Me.lbl_LeftBrd.TabIndex = 7
        Me.lbl_LeftBrd.Text = "label4"
        '
        'lbl_RightBrd
        '
        Me.lbl_RightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_RightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_RightBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_RightBrd.Location = New System.Drawing.Point(913, 1)
        Me.lbl_RightBrd.Name = "lbl_RightBrd"
        Me.lbl_RightBrd.Size = New System.Drawing.Size(1, 23)
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
        Me.lbl_TopBrd.Size = New System.Drawing.Size(914, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'pnlGrid
        '
        Me.pnlGrid.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlGrid.Controls.Add(Me.grdFlowSheet)
        Me.pnlGrid.Controls.Add(Me.Label6)
        Me.pnlGrid.Controls.Add(Me.Label5)
        Me.pnlGrid.Controls.Add(Me.Label1)
        Me.pnlGrid.Controls.Add(Me.Label2)
        Me.pnlGrid.Controls.Add(Me.Label3)
        Me.pnlGrid.Controls.Add(Me.Label4)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.Location = New System.Drawing.Point(0, 83)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlGrid.Size = New System.Drawing.Size(920, 594)
        Me.pnlGrid.TabIndex = 11
        '
        'grdFlowSheet
        '
        Me.grdFlowSheet.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(231, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.grdFlowSheet.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grdFlowSheet.BackgroundColor = System.Drawing.Color.White
        Me.grdFlowSheet.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdFlowSheet.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdFlowSheet.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdFlowSheet.CaptionForeColor = System.Drawing.Color.White
        Me.grdFlowSheet.CaptionVisible = False
        Me.grdFlowSheet.DataMember = ""
        Me.grdFlowSheet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdFlowSheet.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdFlowSheet.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grdFlowSheet.GridLineColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.grdFlowSheet.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(86, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.grdFlowSheet.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdFlowSheet.HeaderForeColor = System.Drawing.Color.White
        Me.grdFlowSheet.LinkColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.grdFlowSheet.Location = New System.Drawing.Point(6, 3)
        Me.grdFlowSheet.Name = "grdFlowSheet"
        Me.grdFlowSheet.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.grdFlowSheet.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdFlowSheet.ReadOnly = True
        Me.grdFlowSheet.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.grdFlowSheet.SelectionForeColor = System.Drawing.Color.Black
        Me.grdFlowSheet.Size = New System.Drawing.Size(910, 587)
        Me.grdFlowSheet.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.White
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(4, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(2, 587)
        Me.Label6.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(4, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(912, 2)
        Me.Label5.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(4, 590)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(912, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 590)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(916, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 590)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(914, 1)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "label1"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.ts_ViewButtons)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(920, 53)
        Me.pnlToolStrip.TabIndex = 15
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
        Me.ts_ViewButtons.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnAdd, Me.ts_btnModify, Me.ts_btnDelete, Me.ts_gloCommunityDownload, Me.ts_btnRefresh, Me.ts_btnClose})
        Me.ts_ViewButtons.Location = New System.Drawing.Point(0, 0)
        Me.ts_ViewButtons.Name = "ts_ViewButtons"
        Me.ts_ViewButtons.Size = New System.Drawing.Size(920, 53)
        Me.ts_ViewButtons.TabIndex = 0
        Me.ts_ViewButtons.Text = "ToolStrip1"
        '
        'ts_btnAdd
        '
        Me.ts_btnAdd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.ts_btnModify.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.ts_btnDelete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnDelete.Image = CType(resources.GetObject("ts_btnDelete.Image"), System.Drawing.Image)
        Me.ts_btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnDelete.Name = "ts_btnDelete"
        Me.ts_btnDelete.Size = New System.Drawing.Size(50, 50)
        Me.ts_btnDelete.Tag = "Delete"
        Me.ts_btnDelete.Text = "&Delete"
        Me.ts_btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        'ts_btnRefresh
        '
        Me.ts_btnRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlTopRight
        '
        Me.pnlTopRight.Controls.Add(Me.pnlTopRight1)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTopRight.Location = New System.Drawing.Point(0, 53)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTopRight.Size = New System.Drawing.Size(920, 30)
        Me.pnlTopRight.TabIndex = 16
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
        'frmViewFlowSheet
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(920, 677)
        Me.Controls.Add(Me.pnlGrid)
        Me.Controls.Add(Me.pnlTopRight)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmViewFlowSheet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Flowsheet"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTopRight1.ResumeLayout(False)
        Me.pnlGrid.ResumeLayout(False)
        CType(Me.grdFlowSheet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_ViewButtons.ResumeLayout(False)
        Me.ts_ViewButtons.PerformLayout()
        Me.pnlTopRight.ResumeLayout(False)
        Me.panel4.ResumeLayout(False)
        Me.panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmFlowSheetView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            'Code Start added by kanchan on 20120102 for gloCommunity integration
            If gblnIsShareUserRights = True Then ''Added condition to fixed Bug # : 37984 on 20120927
                ts_gloCommunityDownload.Visible = gblngloCommunity
            End If
            'Code end added by kanchan on 20120102 for gloCommunity integration
            grdFlowSheet.DataSource = objFlowSheet.GetAllFlowSheet
            'Commented by shubhangi 20091202
            'It is called on Shown event
            'CustomGridStyle()
            If grdFlowSheet.VisibleRowCount > 0 Then
                grdFlowSheet.Select(0)
            End If
            'CustomTreeView()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
    Private Sub UpdateFlowSheet()
        Dim ID As Long
        Dim FlowSheetName As String
        Dim objfrmFlowSheet As frmMSTFlowSheet1 = Nothing
        Try
            If grdFlowSheet.VisibleRowCount >= 1 Then
                If grdFlowSheet.CurrentRowIndex >= 0 Then
                    blnModify = True
                    ID = grdFlowSheet.Item(grdFlowSheet.CurrentRowIndex, 0).ToString
                    FlowSheetName = grdFlowSheet.Item(grdFlowSheet.CurrentRowIndex, 1).ToString
                    Dim grdIndex As Integer = grdFlowSheet.CurrentRowIndex

                    '' Sudhir 20090226 '' NOT ALLOW USER TO MODIFY.. IF DATA AVAILABLE FOR CURRENT FLOWSHEET
                    'If objFlowSheet.CheckIsUsed(FlowSheetName) = True Then
                    '    MessageBox.Show("Data is already entered for Flow Sheet '" & FlowSheetName & "', you cannot modify it", "Flow Sheet", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                    '    Exit Sub
                    'End If
                    '' ''
                    Dim myDataView As DataView = CType(grdFlowSheet.DataSource, DataView)
                    ''''''''''Code is added by Anil on 01/11/2007
                    If (IsNothing(myDataView) = False) Then


                        sortOrder = CType(grdFlowSheet.DataSource, DataView).Sort
                        strSearchstring = txtSearch.Text.Trim
                        arrcolumnsort = Split(sortOrder, "]")
                        If arrcolumnsort.Length > 1 Then
                            strcolumnName = arrcolumnsort.GetValue(0)
                            strsortorder = arrcolumnsort.GetValue(1)
                        End If
                        '''''''''''
                    End If

                    ''''''''
                    Dim IsInUse As Boolean = False
                    'If objFlowSheet.CheckIsUsed(ID) = True Then   ''ORINGINAL COMMENT BY SUDHIR 20081212
                    If objFlowSheet.CheckIsUsed(FlowSheetName) = True Then
                        IsInUse = True
                    End If
                    ''''''''
                    objfrmFlowSheet = New frmMSTFlowSheet1(ID, IsInUse)
                    objfrmFlowSheet.Text = "Modify Flowsheet"
                    objfrmFlowSheet.ShowDialog(IIf(IsNothing(objfrmFlowSheet.Parent), Me, objfrmFlowSheet.Parent))
                    objfrmFlowSheet.BringToFront()


                    If objfrmFlowSheet._Cancel = False Then
                        grdFlowSheet.DataSource = objFlowSheet.GetAllFlowSheet
                        ''''Code line added by Anil on 01/11/2007
                        CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                        Dim myDatagView As DataView = CType(grdFlowSheet.DataSource, DataView)
                        If (IsNothing(myDatagView) = False) Then

                            For i = 0 To myDatagView.Table.Rows.Count - 1
                                '''' when ID Found select that matching Row
                                If ID = grdFlowSheet.Item(i, 0) Then
                                    grdFlowSheet.CurrentRowIndex = i
                                    grdFlowSheet.Select(i)
                                    Exit For
                                End If
                            Next
                        End If

                    End If

                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If Not IsNothing(objfrmFlowSheet) Then    'obj disposed by Mitesh
                objfrmFlowSheet.Dispose()
                objfrmFlowSheet = Nothing
            End If

        End Try
    End Sub
    Public Sub CustomGridStyle(Optional ByVal strcolumnName As String = "", Optional ByVal strSortBy As String = "", Optional ByVal strsearchtxt As String = "")
        Dim dv As DataView
        Try

            dv = objFlowSheet.GetDataview
            Dim ts As New clsDataGridTableStyle(dv.Table.TableName)

            Dim IDCol As New DataGridTextBoxColumn
            IDCol.Width = 0
            IDCol.MappingName = dv.Table.Columns(0).ColumnName
            IDCol.HeaderText = "ID"

            Dim FlowSheetCol As New DataGridTextBoxColumn
            With FlowSheetCol
                .Width = grdFlowSheet.Width
                .MappingName = dv.Table.Columns(1).ColumnName
                .HeaderText = "Flowsheet Name"
                .NullText = ""
            End With

            '''''Code modifications are done by Anil on 01/11/2007
            txtSearch.Text = ""
            txtSearch.Text = strsearchtxt
            If strcolumnName = "" Then
                objFlowSheet.SortDataview(dv.Table.Columns(1).ColumnName)
            Else
                Dim strColumn As String = Replace(strcolumnName, "[", "")
                objFlowSheet.SortDataview(strColumn, strSortBy)
            End If
            '''''''''''

            'Dim NoOfCol As New DataGridTextBoxColumn
            'With NoOfCol
            '    .Width = 150
            '    .MappingName = objFlowSheet.GetDataview.Table.Columns(2).ColumnName
            '    .HeaderText = "No Of Columns"
            '    .NullText = ""
            'End With

            ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {IDCol, FlowSheetCol})
            grdFlowSheet.TableStyles.Clear()
            grdFlowSheet.TableStyles.Add(ts)

        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'If Not IsNothing(dv) Then    'obj disposed by Mitesh
            '    dv.Dispose()
            '    dv = Nothing
            'End If
        End Try
    End Sub

    Private Sub grdFlowSheet_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdFlowSheet.CurrentCellChanged
        '''''This code is commented by Anil on 01/11/2007.
        'Try
        '    Select Case grdFlowSheet.CurrentCell.ColumnNumber
        '        Case 1
        '            lblSearch.Text = "Flow Sheet Name"
        '    End Select
        'Catch objErr As Exception
        '    MessageBox.Show(objErr.ToString, "Flow Sheet", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        ''''''This IF statement is added by Anil on 01/11/2007
        If _blnSearch = True Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim dvPatient As DataView
                dvPatient = CType(grdFlowSheet.DataSource(), DataView)
                If (IsNothing(dvPatient) = False) Then


                    grdFlowSheet.DataSource = dvPatient
                    Dim strPatientSearchDetails As String
                    If Trim(txtSearch.Text) <> "" Then
                        strPatientSearchDetails = Replace(txtSearch.Text, "'", "''")
                        ''''Code line below is added on 29/10/2007 by Anil, to resolve the bug, which was giving error for special characters in search.
                        strPatientSearchDetails = Replace(strPatientSearchDetails, "[", "") & ""
                        strPatientSearchDetails = mdlGeneral.ReplaceSpecialCharacters(strPatientSearchDetails)
                    Else
                        strPatientSearchDetails = ""
                    End If

                    Select Case Trim(lblSearch.Text)
                        Case "Search :" '' change for bugid 71306 search not working 
                            '''''Code Modified by Anil on 24/09/2007 at 4:05 p.m.
                            '''''This change is made to get In-String search i.e.,for any string which has the character/characters present in the search textbox.
                            '''''Previously the commented code was searching for the strings which are having first character same as that in search textbox 
                            '''''and also it was searching strings which are having the character in between the words but for that we had to use " % " or " * " sign before that character. But now it is not required to add % or * signs.

                            ''If strPatientSearchDetails.StartsWith("%") = True Or strPatientSearchDetails.StartsWith("*") = True Then
                            dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '%" & strPatientSearchDetails & "%'"

                            'Set focus on first record after filtering
                            If dvPatient.Count > 0 Then
                                grdFlowSheet.Select(0)
                            End If

                            ''Else
                            ''dvPatient.RowFilter = dvPatient.Table.Columns(1).ColumnName & " Like '" & strPatientSearchDetails & "%'"
                            ''End If
                            '''''Upto here the changes are made by Anil on 24/09/2007 at 4:05 p.m.
                    End Select
                End If

                Me.Cursor = Cursors.Default
            Catch objErr As Exception
                Me.Cursor = Cursors.Default
                MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Search, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        End If
    End Sub

    ''''''''This code/event is added by Anil on 01/11/2007, 

    Private Sub grdFlowSheet_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdFlowSheet.MouseDoubleClick
        Try
            Dim ptPoint As Point = New Point(e.X, e.Y)
            Dim htInfo As DataGrid.HitTestInfo = grdFlowSheet.HitTest(ptPoint)
            If htInfo.Type = DataGrid.HitTestType.ColumnHeader Then
                
                If txtSearch.Text = "" Then
                    _blnSearch = True
                End If
                'For Bug #88519: the txtSearch box was getting blank else code was not need when double clicked on grid Header.
                'Else
                '    _blnSearch = False
                '    'txtSearch.Text = ""
                '    _blnSearch = True

            ElseIf htInfo.Type = DataGrid.HitTestType.Cell Then
            _blnSearch = True
            UpdateFlowSheet()
            End If

        Catch objErr As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, objErr.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''''''''''''''

    Private Sub grdFlowSheet_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdFlowSheet.MouseUp
        Try
            If grdFlowSheet.CurrentRowIndex >= 0 Then
                grdFlowSheet.Select(grdFlowSheet.CurrentRowIndex)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End Try
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        Try
            If (e.KeyChar = ChrW(13)) Then
                If grdFlowSheet.CurrentRowIndex >= 0 Then
                    grdFlowSheet.Select(0)
                    grdFlowSheet.CurrentRowIndex = 0
                End If
            End If
            ''--Added by Anil on 20071213
            mdlGeneral.ValidateText(txtSearch.Text, e)
            ''----
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''Code Added by Shilpa for adding the new buttons on 13th Nov 2007
    Private Sub AddCategory()
        Dim objfrmFlowSheet As New frmMSTFlowSheet1

        Try
            blnModify = False
            objfrmFlowSheet.Text = "Add New Flowsheet"
            objfrmFlowSheet.ShowDialog(IIf(IsNothing(objfrmFlowSheet.Parent), Me, objfrmFlowSheet.Parent))
            grdFlowSheet.DataSource = objFlowSheet.GetAllFlowSheet
            CustomGridStyle()
            'grdFlowSheet.DataSource = objFlowSheet.GetAllFlowSheet
            '''' To Remember the Selection of Row 
            Dim myDataview As DataView = CType(grdFlowSheet.DataSource, DataView)
            If (IsNothing(myDataview) = False) Then


                Dim i As Integer
                For i = 0 To myDataview.Table.Rows.Count - 1
                    '''' when ID Found select that matching Row
                    If objfrmFlowSheet._FlowSheetName = grdFlowSheet.Item(i, 1) Then
                        grdFlowSheet.CurrentRowIndex = i
                        grdFlowSheet.Select(i)
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(objfrmFlowSheet) Then    'obj disposed by Mitesh
                objfrmFlowSheet.Dispose()
                objfrmFlowSheet = Nothing
            End If
        End Try
    End Sub
    Private Sub UpdateCategory()
        UpdateFlowSheet()
        
    End Sub
    Private Sub DeleteCategory()
        Dim ID As Long
        Dim FlowSheetName As String
        Try
            If grdFlowSheet.VisibleRowCount > 0 Then
                If grdFlowSheet.CurrentRowIndex >= 0 Then
                    'blnModify = True
                    ID = grdFlowSheet.Item(grdFlowSheet.CurrentRowIndex, 0).ToString
                    FlowSheetName = grdFlowSheet.Item(grdFlowSheet.CurrentRowIndex, 1).ToString

                    ''''''''
                    'If objFlowSheet.CheckIsUsed(ID) = True Then  ''ORIGINAL ''COMMENT BY SUDHIR 20081212
                    '' COMMENTED BY SUDHIR 20090408 - FOR DENORMALIZATION ''
                    'If objFlowSheet.CheckIsUsed(FlowSheetName) = True Then
                    '    MessageBox.Show("Data is already entered for Flow Sheet '" & FlowSheetName & "', you cannot delete it", "Flow Sheet", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
                    '    Exit Sub
                    'End If
                    ''''''''
                    If MessageBox.Show("Are you sure you want to delete flowsheet '" & FlowSheetName & "'" & "?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        objFlowSheet.DeleteFlowSheet(ID, FlowSheetName.Replace("'", "''"))
                        grdFlowSheet.DataSource = objFlowSheet.GetAllFlowSheet()
                        Dim myDataView As DataView = CType(grdFlowSheet.DataSource, DataView)
                        If (IsNothing(myDataView) = False) Then


                            ''''''''''Code is added by Anil on 01/11/2007
                            sortOrder = CType(grdFlowSheet.DataSource, DataView).Sort
                            strSearchstring = txtSearch.Text.Trim
                            Dim arrcolumnsort() As String = Split(sortOrder, "]")
                            If arrcolumnsort.Length > 1 Then
                                strcolumnName = arrcolumnsort.GetValue(0)
                                strsortorder = arrcolumnsort.GetValue(1)
                            End If
                            CustomGridStyle(strcolumnName, strsortorder, strSearchstring)
                        End If

                        '''''''''''
                    End If
                End If
            End If
            'If (grdFlowSheet.VisibleRowCount > 0) Then
            '    ts_btnDelete.Visible = True
            '    ts_btnModify.Visible = True
            'Else
            '    ts_btnDelete.Visible = False
            '    ts_btnModify.Visible = False
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Flowsheet, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub RefreshCategory()
        Try
            grdFlowSheet.DataSource = objFlowSheet.GetAllFlowSheet
            CustomGridStyle()
            '''''Following 3 code lines are addded by Anil 0n 26/09/07 at 10:15 a.m.
            '''''This code clears the search textbox and gets the focus on the first row of the grid on click of Refresh button.
            txtSearch.Clear()
            _blnSearch = True
            If grdFlowSheet.VisibleRowCount > 0 Then
                grdFlowSheet.CurrentRowIndex = 0
                grdFlowSheet.Select(grdFlowSheet.CurrentRowIndex)
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Contact, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Refresh, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                Call RefreshCategory()
            Case "Close"
                Call FormClose()
        End Select
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'SHUBHANGI 20090930
        'Use to clear search text box
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    'Shubhangi 20091202
    'Add this event coz this event is fire after load so at that time form's size is maximize so the column size become appropriate according to actual size of control
    Private Sub frmViewFlowSheet_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        CustomGridStyle()
    End Sub
    'Code Start added by kanchan on 20120102 for gloCommunity integration
    Private Sub ts_gloCommunityDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_gloCommunityDownload.Click
        If CheckUser() = False Then ''Added for fixed Bug # : 35658 on 20120904
            Dim FrmgloCommunityViewData As New gloCommunity.Forms.gloCommunityViewData("Flowsheet", "Download")
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
