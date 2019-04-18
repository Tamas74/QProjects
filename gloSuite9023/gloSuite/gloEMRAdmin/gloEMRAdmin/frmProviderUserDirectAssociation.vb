Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Collections.Generic
Imports System.Threading
Imports gloAuditTrail


Public Class frmProviderUserDirectAssociation
    Inherits System.Windows.Forms.Form

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
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlLocation As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlTreeView As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents trvProviders As System.Windows.Forms.TreeView
    Friend WithEvents trvUsers As System.Windows.Forms.TreeView
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents cmnuDelete As System.Windows.Forms.ContextMenu
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOk As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents pnlLabels As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents pnlProviders As System.Windows.Forms.Panel
    Friend WithEvents pnlAssociations As System.Windows.Forms.Panel
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents PicBx_Search As System.Windows.Forms.PictureBox
    Private WithEvents lbl_pnlSearchTopBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Friend WithEvents toolTip As System.Windows.Forms.ToolTip
    Private WithEvents lbl_pnlSearchBottomBrd As System.Windows.Forms.Label
    Private WithEvents lblWhiteBG As System.Windows.Forms.Label
    Friend WithEvents VerticalBar As System.Windows.Forms.Panel
    Friend WithEvents BottomPanel As System.Windows.Forms.Panel
    Friend WithEvents MainPanel As System.Windows.Forms.Panel
    Friend WithEvents TopPanel As System.Windows.Forms.Panel
    Friend WithEvents btnAddAssociation As System.Windows.Forms.Button
    Friend WithEvents btnRemoveAssociation As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnlInformation As System.Windows.Forms.Panel
    Friend WithEvents lblInfoII As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents mnuDelete As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProviderUserDirectAssociation))
        Me.pnlLocation = New System.Windows.Forms.Panel()
        Me.trvProviders = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlTreeView = New System.Windows.Forms.Panel()
        Me.trvUsers = New System.Windows.Forms.TreeView()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmnuDelete = New System.Windows.Forms.ContextMenu()
        Me.mnuDelete = New System.Windows.Forms.MenuItem()
        Me.pnlToolStrip = New System.Windows.Forms.Panel()
        Me.tstrip = New System.Windows.Forms.ToolStrip()
        Me.btnOk = New System.Windows.Forms.ToolStripButton()
        Me.btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.pnlLabels = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.pnlProviders = New System.Windows.Forms.Panel()
        Me.pnlAssociations = New System.Windows.Forms.Panel()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.PicBx_Search = New System.Windows.Forms.PictureBox()
        Me.lblWhiteBG = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchBottomBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchTopBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnRemoveAssociation = New System.Windows.Forms.Button()
        Me.btnAddAssociation = New System.Windows.Forms.Button()
        Me.VerticalBar = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.BottomPanel = New System.Windows.Forms.Panel()
        Me.TopPanel = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.MainPanel = New System.Windows.Forms.Panel()
        Me.pnlInformation = New System.Windows.Forms.Panel()
        Me.lblInfoII = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.pnlLocation.SuspendLayout()
        Me.pnlTreeView.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlLabels.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.pnlProviders.SuspendLayout()
        Me.pnlAssociations.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.VerticalBar.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.BottomPanel.SuspendLayout()
        Me.TopPanel.SuspendLayout()
        Me.MainPanel.SuspendLayout()
        Me.pnlInformation.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlLocation
        '
        Me.pnlLocation.Controls.Add(Me.trvProviders)
        Me.pnlLocation.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnlLocation.Controls.Add(Me.Label6)
        Me.pnlLocation.Controls.Add(Me.Label7)
        Me.pnlLocation.Controls.Add(Me.Label8)
        Me.pnlLocation.Controls.Add(Me.Label9)
        Me.pnlLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLocation.Location = New System.Drawing.Point(0, 26)
        Me.pnlLocation.Name = "pnlLocation"
        Me.pnlLocation.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlLocation.Size = New System.Drawing.Size(350, 568)
        Me.pnlLocation.TabIndex = 9
        '
        'trvProviders
        '
        Me.trvProviders.BackColor = System.Drawing.Color.White
        Me.trvProviders.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvProviders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvProviders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvProviders.ForeColor = System.Drawing.Color.Black
        Me.trvProviders.HideSelection = False
        Me.trvProviders.ImageIndex = 0
        Me.trvProviders.ImageList = Me.ImageList1
        Me.trvProviders.ItemHeight = 19
        Me.trvProviders.Location = New System.Drawing.Point(4, 8)
        Me.trvProviders.Name = "trvProviders"
        Me.trvProviders.SelectedImageIndex = 0
        Me.trvProviders.ShowLines = False
        Me.trvProviders.Size = New System.Drawing.Size(342, 556)
        Me.trvProviders.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Bullet06.ico")
        Me.ImageList1.Images.SetKeyName(1, "Small Arrow.ico")
        Me.ImageList1.Images.SetKeyName(2, "bullet.ico")
        Me.ImageList1.Images.SetKeyName(3, "arrow_01.ico")
        Me.ImageList1.Images.SetKeyName(4, "Tick.ico")
        Me.ImageList1.Images.SetKeyName(5, "Bullet04.ico")
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(4, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(342, 7)
        Me.lbl_WhiteSpaceTop.TabIndex = 38
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(4, 564)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(342, 1)
        Me.Label6.TabIndex = 43
        Me.Label6.Text = "label2"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 564)
        Me.Label7.TabIndex = 42
        Me.Label7.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(346, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 564)
        Me.Label8.TabIndex = 41
        Me.Label8.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(344, 1)
        Me.Label9.TabIndex = 40
        Me.Label9.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(1, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(342, 21)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Providers"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlTreeView
        '
        Me.pnlTreeView.AutoSize = True
        Me.pnlTreeView.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlTreeView.Controls.Add(Me.trvUsers)
        Me.pnlTreeView.Controls.Add(Me.Label5)
        Me.pnlTreeView.Controls.Add(Me.Label3)
        Me.pnlTreeView.Controls.Add(Me.Label10)
        Me.pnlTreeView.Controls.Add(Me.Label11)
        Me.pnlTreeView.Controls.Add(Me.Label12)
        Me.pnlTreeView.Controls.Add(Me.Label13)
        Me.pnlTreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTreeView.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlTreeView.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlTreeView.Location = New System.Drawing.Point(0, 56)
        Me.pnlTreeView.Name = "pnlTreeView"
        Me.pnlTreeView.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlTreeView.Size = New System.Drawing.Size(357, 538)
        Me.pnlTreeView.TabIndex = 11
        '
        'trvUsers
        '
        Me.trvUsers.BackColor = System.Drawing.Color.White
        Me.trvUsers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvUsers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvUsers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvUsers.ForeColor = System.Drawing.Color.Black
        Me.trvUsers.HideSelection = False
        Me.trvUsers.ImageIndex = 0
        Me.trvUsers.ImageList = Me.ImageList1
        Me.trvUsers.ItemHeight = 19
        Me.trvUsers.Location = New System.Drawing.Point(7, 8)
        Me.trvUsers.Name = "trvUsers"
        Me.trvUsers.SelectedImageIndex = 0
        Me.trvUsers.ShowLines = False
        Me.trvUsers.Size = New System.Drawing.Size(346, 526)
        Me.trvUsers.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.White
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Location = New System.Drawing.Point(4, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(3, 526)
        Me.Label5.TabIndex = 40
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Location = New System.Drawing.Point(4, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(349, 7)
        Me.Label3.TabIndex = 38
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(4, 534)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(349, 1)
        Me.Label10.TabIndex = 44
        Me.Label10.Text = "label2"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(3, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 534)
        Me.Label11.TabIndex = 43
        Me.Label11.Text = "label4"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(353, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 534)
        Me.Label12.TabIndex = 42
        Me.Label12.Text = "label3"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(3, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(351, 1)
        Me.Label13.TabIndex = 41
        Me.Label13.Text = "label1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(0, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(351, 22)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "User Provider Association"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmnuDelete
        '
        Me.cmnuDelete.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuDelete})
        '
        'mnuDelete
        '
        Me.mnuDelete.Index = 0
        Me.mnuDelete.Text = "Delete"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlToolStrip.Controls.Add(Me.tstrip)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlToolStrip.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(743, 56)
        Me.pnlToolStrip.TabIndex = 18
        '
        'tstrip
        '
        Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tstrip.BackgroundImage = CType(resources.GetObject("tstrip.BackgroundImage"), System.Drawing.Image)
        Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOk, Me.btnCancel})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(743, 53)
        Me.tstrip.TabIndex = 0
        Me.tstrip.Text = "ToolStrip1"
        '
        'btnOk
        '
        Me.btnOk.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOk.Image = CType(resources.GetObject("btnOk.Image"), System.Drawing.Image)
        Me.btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(66, 50)
        Me.btnOk.Text = "&Save&&Cls"
        Me.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOk.ToolTipText = "Save and Close"
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(43, 50)
        Me.btnCancel.Text = "&Close"
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnCancel.ToolTipText = "Close"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Panel2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel4.Size = New System.Drawing.Size(350, 26)
        Me.Panel4.TabIndex = 21
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label14)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(3, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(344, 23)
        Me.Panel2.TabIndex = 19
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(1, 22)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(342, 1)
        Me.Label14.TabIndex = 8
        Me.Label14.Text = "label2"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(0, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 22)
        Me.Label15.TabIndex = 7
        Me.Label15.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(343, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 22)
        Me.Label16.TabIndex = 6
        Me.Label16.Text = "label3"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(0, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(344, 1)
        Me.Label17.TabIndex = 5
        Me.Label17.Text = "label1"
        '
        'pnlLabels
        '
        Me.pnlLabels.Controls.Add(Me.Panel5)
        Me.pnlLabels.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLabels.Location = New System.Drawing.Point(0, 0)
        Me.pnlLabels.Name = "pnlLabels"
        Me.pnlLabels.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlLabels.Size = New System.Drawing.Size(357, 26)
        Me.pnlLabels.TabIndex = 22
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Transparent
        Me.Panel5.BackgroundImage = CType(resources.GetObject("Panel5.BackgroundImage"), System.Drawing.Image)
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.Label18)
        Me.Panel5.Controls.Add(Me.Label19)
        Me.Panel5.Controls.Add(Me.Label20)
        Me.Panel5.Controls.Add(Me.Label1)
        Me.Panel5.Controls.Add(Me.Label21)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel5.Location = New System.Drawing.Point(3, 0)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(351, 23)
        Me.Panel5.TabIndex = 19
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(1, 22)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(349, 1)
        Me.Label18.TabIndex = 8
        Me.Label18.Text = "label2"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(0, 1)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 22)
        Me.Label19.TabIndex = 7
        Me.Label19.Text = "label4"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label20.Location = New System.Drawing.Point(350, 1)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 22)
        Me.Label20.TabIndex = 6
        Me.Label20.Text = "label3"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(0, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(351, 1)
        Me.Label21.TabIndex = 5
        Me.Label21.Text = "label1"
        '
        'pnlProviders
        '
        Me.pnlProviders.Controls.Add(Me.pnlLocation)
        Me.pnlProviders.Controls.Add(Me.Panel4)
        Me.pnlProviders.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlProviders.Location = New System.Drawing.Point(0, 0)
        Me.pnlProviders.Name = "pnlProviders"
        Me.pnlProviders.Size = New System.Drawing.Size(350, 594)
        Me.pnlProviders.TabIndex = 23
        '
        'pnlAssociations
        '
        Me.pnlAssociations.Controls.Add(Me.pnlTreeView)
        Me.pnlAssociations.Controls.Add(Me.pnlSearch)
        Me.pnlAssociations.Controls.Add(Me.pnlLabels)
        Me.pnlAssociations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAssociations.Location = New System.Drawing.Point(386, 0)
        Me.pnlAssociations.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlAssociations.Name = "pnlAssociations"
        Me.pnlAssociations.Size = New System.Drawing.Size(357, 594)
        Me.pnlAssociations.TabIndex = 24
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearch.Controls.Add(Me.txtSearch)
        Me.pnlSearch.Controls.Add(Me.PicBx_Search)
        Me.pnlSearch.Controls.Add(Me.lblWhiteBG)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchBottomBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchTopBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlSearch.Location = New System.Drawing.Point(0, 26)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlSearch.Size = New System.Drawing.Size(357, 30)
        Me.pnlSearch.TabIndex = 24
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(38, 6)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(306, 15)
        Me.txtSearch.TabIndex = 2
        Me.toolTip.SetToolTip(Me.txtSearch, "Search in Users")
        '
        'PicBx_Search
        '
        Me.PicBx_Search.BackColor = System.Drawing.Color.White
        Me.PicBx_Search.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicBx_Search.Image = CType(resources.GetObject("PicBx_Search.Image"), System.Drawing.Image)
        Me.PicBx_Search.Location = New System.Drawing.Point(4, 1)
        Me.PicBx_Search.Name = "PicBx_Search"
        Me.PicBx_Search.Size = New System.Drawing.Size(28, 25)
        Me.PicBx_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PicBx_Search.TabIndex = 9
        Me.PicBx_Search.TabStop = False
        Me.toolTip.SetToolTip(Me.PicBx_Search, "Search in Users")
        '
        'lblWhiteBG
        '
        Me.lblWhiteBG.BackColor = System.Drawing.Color.White
        Me.lblWhiteBG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblWhiteBG.Location = New System.Drawing.Point(4, 1)
        Me.lblWhiteBG.Name = "lblWhiteBG"
        Me.lblWhiteBG.Size = New System.Drawing.Size(349, 25)
        Me.lblWhiteBG.TabIndex = 43
        '
        'lbl_pnlSearchBottomBrd
        '
        Me.lbl_pnlSearchBottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchBottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlSearchBottomBrd.Location = New System.Drawing.Point(4, 26)
        Me.lbl_pnlSearchBottomBrd.Name = "lbl_pnlSearchBottomBrd"
        Me.lbl_pnlSearchBottomBrd.Size = New System.Drawing.Size(349, 1)
        Me.lbl_pnlSearchBottomBrd.TabIndex = 35
        Me.lbl_pnlSearchBottomBrd.Text = "label1"
        '
        'lbl_pnlSearchTopBrd
        '
        Me.lbl_pnlSearchTopBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchTopBrd.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlSearchTopBrd.Location = New System.Drawing.Point(4, 0)
        Me.lbl_pnlSearchTopBrd.Name = "lbl_pnlSearchTopBrd"
        Me.lbl_pnlSearchTopBrd.Size = New System.Drawing.Size(349, 1)
        Me.lbl_pnlSearchTopBrd.TabIndex = 36
        Me.lbl_pnlSearchTopBrd.Text = "label1"
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(3, 0)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 27)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(353, 0)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 27)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'btnRemoveAssociation
        '
        Me.btnRemoveAssociation.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Close
        Me.btnRemoveAssociation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnRemoveAssociation.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnRemoveAssociation.FlatAppearance.BorderSize = 0
        Me.btnRemoveAssociation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveAssociation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnRemoveAssociation.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemoveAssociation.ForeColor = System.Drawing.Color.Transparent
        Me.btnRemoveAssociation.ImageKey = "Close.png"
        Me.btnRemoveAssociation.Location = New System.Drawing.Point(0, 0)
        Me.btnRemoveAssociation.Name = "btnRemoveAssociation"
        Me.btnRemoveAssociation.Size = New System.Drawing.Size(34, 40)
        Me.btnRemoveAssociation.TabIndex = 4
        Me.toolTip.SetToolTip(Me.btnRemoveAssociation, "Remove Provider from association")
        Me.btnRemoveAssociation.UseVisualStyleBackColor = True
        '
        'btnAddAssociation
        '
        Me.btnAddAssociation.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Right
        Me.btnAddAssociation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnAddAssociation.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnAddAssociation.FlatAppearance.BorderSize = 0
        Me.btnAddAssociation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnAddAssociation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnAddAssociation.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddAssociation.ForeColor = System.Drawing.Color.Transparent
        Me.btnAddAssociation.ImageKey = "(none)"
        Me.btnAddAssociation.Location = New System.Drawing.Point(0, 268)
        Me.btnAddAssociation.Name = "btnAddAssociation"
        Me.btnAddAssociation.Size = New System.Drawing.Size(34, 40)
        Me.btnAddAssociation.TabIndex = 1
        Me.toolTip.SetToolTip(Me.btnAddAssociation, "Add Provider to User")
        Me.btnAddAssociation.UseVisualStyleBackColor = True
        '
        'VerticalBar
        '
        Me.VerticalBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.VerticalBar.Controls.Add(Me.Panel1)
        Me.VerticalBar.Dock = System.Windows.Forms.DockStyle.Left
        Me.VerticalBar.Location = New System.Drawing.Point(350, 0)
        Me.VerticalBar.Name = "VerticalBar"
        Me.VerticalBar.Size = New System.Drawing.Size(36, 594)
        Me.VerticalBar.TabIndex = 25
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.BottomPanel)
        Me.Panel1.Controls.Add(Me.TopPanel)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label23)
        Me.Panel1.Controls.Add(Me.Label24)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel1.Size = New System.Drawing.Size(36, 594)
        Me.Panel1.TabIndex = 44
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label22.Location = New System.Drawing.Point(1, 590)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(34, 1)
        Me.Label22.TabIndex = 22
        Me.Label22.Text = "label2"
        '
        'BottomPanel
        '
        Me.BottomPanel.BackColor = System.Drawing.Color.Transparent
        Me.BottomPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BottomPanel.Controls.Add(Me.btnRemoveAssociation)
        Me.BottomPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.BottomPanel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BottomPanel.Location = New System.Drawing.Point(1, 309)
        Me.BottomPanel.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.BottomPanel.Name = "BottomPanel"
        Me.BottomPanel.Size = New System.Drawing.Size(34, 282)
        Me.BottomPanel.TabIndex = 19
        '
        'TopPanel
        '
        Me.TopPanel.BackColor = System.Drawing.Color.Transparent
        Me.TopPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TopPanel.Controls.Add(Me.btnAddAssociation)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopPanel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TopPanel.Location = New System.Drawing.Point(1, 1)
        Me.TopPanel.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Size = New System.Drawing.Size(34, 308)
        Me.TopPanel.TabIndex = 20
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(35, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 590)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "label1"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(1, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(35, 1)
        Me.Label23.TabIndex = 23
        Me.Label23.Text = "label1"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(0, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 591)
        Me.Label24.TabIndex = 24
        Me.Label24.Text = "label1"
        '
        'MainPanel
        '
        Me.MainPanel.Controls.Add(Me.pnlAssociations)
        Me.MainPanel.Controls.Add(Me.VerticalBar)
        Me.MainPanel.Controls.Add(Me.pnlProviders)
        Me.MainPanel.Controls.Add(Me.pnlInformation)
        Me.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainPanel.Location = New System.Drawing.Point(0, 56)
        Me.MainPanel.Name = "MainPanel"
        Me.MainPanel.Size = New System.Drawing.Size(743, 637)
        Me.MainPanel.TabIndex = 26
        '
        'pnlInformation
        '
        Me.pnlInformation.Controls.Add(Me.lblInfoII)
        Me.pnlInformation.Controls.Add(Me.Label25)
        Me.pnlInformation.Controls.Add(Me.Label26)
        Me.pnlInformation.Controls.Add(Me.Label27)
        Me.pnlInformation.Controls.Add(Me.Label28)
        Me.pnlInformation.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlInformation.Location = New System.Drawing.Point(0, 594)
        Me.pnlInformation.Name = "pnlInformation"
        Me.pnlInformation.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlInformation.Size = New System.Drawing.Size(743, 43)
        Me.pnlInformation.TabIndex = 27
        '
        'lblInfoII
        '
        Me.lblInfoII.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblInfoII.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfoII.Location = New System.Drawing.Point(4, 1)
        Me.lblInfoII.Name = "lblInfoII"
        Me.lblInfoII.Padding = New System.Windows.Forms.Padding(5)
        Me.lblInfoII.Size = New System.Drawing.Size(735, 38)
        Me.lblInfoII.TabIndex = 1
        Me.lblInfoII.Text = "Highlight a User and then select which Provider Inboxes the User may access.  Als" & _
    "o, highlight a Provider to see which Users have access. "
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(4, 39)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(735, 1)
        Me.Label25.TabIndex = 44
        Me.Label25.Text = "label2"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label26.Location = New System.Drawing.Point(4, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(735, 1)
        Me.Label26.TabIndex = 45
        Me.Label26.Text = "label2"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(3, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 40)
        Me.Label27.TabIndex = 46
        Me.Label27.Text = "label4"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(739, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 40)
        Me.Label28.TabIndex = 47
        Me.Label28.Text = "label4"
        '
        'frmProviderUserDirectAssociation
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(743, 693)
        Me.Controls.Add(Me.MainPanel)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProviderUserDirectAssociation"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Provider DIRECT Message – Assign Users to Provider Inboxes"
        Me.pnlLocation.ResumeLayout(False)
        Me.pnlTreeView.ResumeLayout(False)
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlLabels.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.pnlProviders.ResumeLayout(False)
        Me.pnlAssociations.ResumeLayout(False)
        Me.pnlAssociations.PerformLayout()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.PicBx_Search, System.ComponentModel.ISupportInitialize).EndInit()
        Me.VerticalBar.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.BottomPanel.ResumeLayout(False)
        Me.TopPanel.ResumeLayout(False)
        Me.MainPanel.ResumeLayout(False)
        Me.pnlInformation.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Class Attributes"

    Dim dsProviderUserList As DataSet = Nothing
    Dim dtProvidersList As DataTable = Nothing
    Dim dtUserList As DataTable = Nothing
    Dim dtAssociationList As DataTable = Nothing

    Dim dictionaryProvider As Dictionary(Of Long, ProviderDirectAssociation) = Nothing
    Dim dictionaryUser As Dictionary(Of Long, UserDirectAssociation) = Nothing

    Dim EnumProviders As EnumerableRowCollection(Of ProviderDirectAssociation) = Nothing
    Dim EnumUser As EnumerableRowCollection(Of UserDirectAssociation) = Nothing

    Dim EnumAssociations As EnumerableRowCollection(Of DirectAssociation) = Nothing
    Dim EnumMatchedUsers As EnumerableRowCollection(Of DirectAssociation) = Nothing
    Dim EnumMatchedAssociations As EnumerableRowCollection(Of DirectAssociation) = Nothing

    Private WithEvents Timer_Search As Windows.Forms.Timer = Nothing

    Dim DefaultTreeViewFont As New Font("Tahoma", 9, FontStyle.Regular)
    Dim BoldTreeViewFont As New Font("Tahoma", 8, FontStyle.Bold)

#End Region

#Region "Events"

#Region "Form Events"

    Private Sub FormClosingEvent(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If dtProvidersList IsNot Nothing Then
                dtProvidersList.Clear()
                dtProvidersList.Dispose()
                dtProvidersList = Nothing
            End If

            If dtUserList IsNot Nothing Then
                dtUserList.Clear()
                dtUserList.Dispose()
                dtUserList = Nothing
            End If

            If dtAssociationList IsNot Nothing Then
                dtAssociationList.Clear()
                dtAssociationList.Dispose()
                dtAssociationList = Nothing
            End If

            If dsProviderUserList IsNot Nothing Then
                dsProviderUserList.Clear()
                dsProviderUserList.Dispose()
                dsProviderUserList = Nothing
            End If

            If DefaultTreeViewFont IsNot Nothing Then
                DefaultTreeViewFont = Nothing
            End If

            If BoldTreeViewFont IsNot Nothing Then
                BoldTreeViewFont = Nothing
            End If

            If Timer_Search IsNot Nothing Then
                Timer_Search.Dispose()
                Timer_Search = Nothing
            End If

            If dictionaryProvider IsNot Nothing Then
                For Each ElementProvider As ProviderDirectAssociation In dictionaryProvider.Values
                    If ElementProvider IsNot Nothing Then
                        ElementProvider.Dispose()
                    End If
                Next
                dictionaryProvider.Clear()
                dictionaryProvider = Nothing
            End If

            If dictionaryUser IsNot Nothing Then
                For Each ElementUser As UserDirectAssociation In dictionaryUser.Values
                    If ElementUser IsNot Nothing Then
                        ElementUser.Dispose()
                    End If
                Next
                dictionaryUser.Clear()
                dictionaryUser = Nothing
            End If

            If EnumProviders IsNot Nothing Then
                EnumProviders = Nothing
            End If

            If EnumUser IsNot Nothing Then
                EnumUser = Nothing
            End If

            If EnumAssociations IsNot Nothing Then
                EnumAssociations = Nothing
            End If

            If EnumMatchedAssociations IsNot Nothing Then
                EnumMatchedAssociations = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
        End Try
    End Sub

    Private Sub FormLoadingEvent(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Using clsAssociationDBLayer As New clsProviderUserDirectAssociationDBLayer
                dsProviderUserList = clsAssociationDBLayer.GetProviderUserList
            End Using

            '20-Mar-14 Chetan: Resolving resolution issues code change for bugid 65029
            Dim myScreenWidth As Integer = System.Windows.SystemParameters.PrimaryScreenWidth
            Dim myScreenHeight As Integer = System.Windows.SystemParameters.PrimaryScreenHeight
            If Me.Width > myScreenWidth Or Me.Height > myScreenHeight Then
                Me.MaximumSize = New System.Drawing.Size(myScreenWidth, myScreenHeight)
                Me.AutoScroll = True
            End If
            dtProvidersList = dsProviderUserList.Tables(0)
            dtUserList = dsProviderUserList.Tables(1)
            dtAssociationList = dsProviderUserList.Tables(2)

            'Timer is used for search functionality
            Timer_Search = New Windows.Forms.Timer
            Timer_Search.Interval = 700

            '----------------------Fill up the base Enumerables-----------------------------------
            'Adds all Providers in EnumerableProviders
            EnumProviders = From ElementRow As DataRow In dtProvidersList
                                        Select New ProviderDirectAssociation(ElementRow)

            'Adds all Users in EnumerableUsers
            EnumUser = From ElementRow As DataRow In dtUserList
                                Select New UserDirectAssociation(ElementRow)

            'Adds all Associations in EnumAssociations
            EnumAssociations = From ElementRow As DataRow In dtAssociationList
                                        Select New DirectAssociation(ElementRow)
            '-------------------------------------------------------------------------------------

            '----------------------Base Dictionaries for Provider and Users-----------------------
            dictionaryProvider = New Dictionary(Of Long, ProviderDirectAssociation)
            dictionaryUser = New Dictionary(Of Long, UserDirectAssociation)
            '-------------------------------------------------------------------------------------

            '-------------------------Add all Providers in DictionaryProviders--------------------            
            For Each ElementProvider As ProviderDirectAssociation In EnumProviders
                If Not dictionaryProvider.ContainsKey(ElementProvider.ProviderID) Then
                    dictionaryProvider.Add(ElementProvider.ProviderID, ElementProvider)
                End If
            Next
            '-------------------------------------------------------------------------------------

            '----------------------------Code Logic-----------------------------------------------
            ' For each User in EnumUser
            '   Add that User in DictionaryUser
            '       Check if that User is present in Associations
            '               If present
            '                |   For each association
            '                |       Add each Provider in that User (User object inherits a Dictionary(of Long, Provider) object)

            Dim ClonedUser As UserDirectAssociation = Nothing

            For Each ElementUser As UserDirectAssociation In EnumUser

                Dim nProviderID As Long = Nothing
                Dim nUserID As Long = Nothing

                ClonedUser = UserDirectAssociation.Clone(ElementUser)

                If Not dictionaryUser.ContainsKey(ClonedUser.UserID) Then
                    nUserID = ClonedUser.UserID

                    dictionaryUser.Add(ClonedUser.UserID, ClonedUser)
                    EnumMatchedUsers = From ElementAssociation As DirectAssociation In EnumAssociations
                                       Where ElementAssociation.UserID = ClonedUser.UserID
                                       Select ElementAssociation

                    For Each MatchedAssociation As DirectAssociation In EnumMatchedUsers
                        If dictionaryUser.ContainsKey(MatchedAssociation.UserID) Then
                            '--If dictionaryUser contains the corresponding UserID
                            '----Add the Provider object in the corresponding User
                            If dictionaryProvider.ContainsKey(MatchedAssociation.ProviderID) Then
                                ClonedUser.Add(MatchedAssociation.ProviderID, ProviderDirectAssociation.Clone(dictionaryProvider(MatchedAssociation.ProviderID)))
                            End If
                        End If
                    Next
                End If

                nProviderID = Nothing
                nUserID = Nothing
                ClonedUser = Nothing
            Next

            AddTreeViewNodes()

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Tree View Events"

    Private Sub trvProviders_AfterSelect(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) Handles trvProviders.AfterSelect
        Try
            'Dim bAllowSort As Boolean = False
            'bAllowSort = True
            'If txtSearch.Text = String.Empty Then
            '    If String.IsNullOrWhiteSpace(txtSearch.Text) Then
            '        bAllowSort = True
            '    End If
            'End If

            If trvProviders.SelectedNode IsNot Nothing Then
                Dim SelectedUserNodeIndex As Int32 = 0
                Dim SelectedNode As TreeNode = trvProviders.SelectedNode

                If trvUsers.SelectedNode IsNot Nothing Then
                    SelectedUserNodeIndex = trvUsers.SelectedNode.Index
                End If

                trvUsers.BeginUpdate()

                'If bAllowProviderSort Then
                '    SearchProviders(SelectedNode.Text)
                '    WhereProviderNotInUser(SelectedNode.Text)
                'End If

                ResetTextFormattingOfUsersTreeView()
                trvUsers.EndUpdate()

                If trvUsers.Nodes.Count > 0 Then
                    trvUsers.SelectedNode = trvUsers.Nodes(SelectedUserNodeIndex)
                End If

                SelectedNode = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub trvUsers_AfterSelect(sender As Object, e As System.Windows.Forms.TreeViewEventArgs) Handles trvUsers.AfterSelect
        Try
            If trvUsers.SelectedNode IsNot Nothing Then
                Dim SelectedUser As TreeNode = trvUsers.SelectedNode
                Dim nUserID As Long = SelectedUser.Tag

                If dictionaryUser.ContainsKey(nUserID) And Not dictionaryProvider.ContainsKey(nUserID) Then
                    trvProviders.BeginUpdate()

                    For Each TreeNode As TreeNode In trvProviders.Nodes
                        With TreeNode
                            '.ImageIndex = 0
                            '.SelectedImageIndex = 0
                            .NodeFont = DefaultTreeViewFont
                        End With
                    Next

                    Dim EnumProviderList As IEnumerable(Of TreeNode) = Nothing
                    Dim SelectedTreeViewUser As UserDirectAssociation = dictionaryUser(nUserID)
                    Dim nProviderID As Long = Nothing

                    For Each Provider As ProviderDirectAssociation In SelectedTreeViewUser.Values
                        If Not Provider.IsDeleted Then
                            nProviderID = Provider.ProviderID

                            EnumProviderList = From ElementNode As TreeNode In trvProviders.Nodes
                                                    Where ElementNode.Tag = nProviderID
                                                        Select ElementNode

                            For Each TreeNode As TreeNode In EnumProviderList
                                With TreeNode
                                    '.ImageIndex = 1
                                    '.SelectedImageIndex = 1
                                    .NodeFont = BoldTreeViewFont
                                End With
                            Next
                        End If                        
                    Next
                    trvProviders.EndUpdate()
                    trvProviders.Refresh()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub trvUsers_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvUsers.MouseDown
        Dim Node As TreeNode = Nothing
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Node = trvUsers.GetNodeAt(e.X, e.Y)
                If IsNothing(Node) = False Then
                    trvUsers.SelectedNode = Node

                    If IsNothing(trvUsers.SelectedNode.Parent) = False Then
                        trvUsers.ContextMenu = cmnuDelete
                        trvUsers_AfterSelect(Me, New TreeViewEventArgs(trvUsers.SelectedNode))
                    Else
                        trvUsers.ContextMenu = Nothing
                    End If
                End If
            End If

            'If e.Button = Windows.Forms.MouseButtons.Left Then
            '    Node = trvUsers.GetNodeAt(e.X, e.Y)
            '    If IsNothing(Node) = False Then
            '        trvUsers.SelectedNode = Node

            '        If IsNothing(trvUsers.SelectedNode.Parent) = False Then                    
            '            trvUsers_AfterSelect(Me, New TreeViewEventArgs(trvUsers.SelectedNode))                    
            '        End If
            '    End If
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Node = Nothing
        End Try
    End Sub


#End Region

#Region "Timer Event"

    Private Sub TimerEventProcessor() Handles Timer_Search.Tick
        Dim sSearchString As String = Nothing
        Try
            Timer_Search.Stop()

            sSearchString = txtSearch.Text

            trvUsers.BeginUpdate()
            SearchUsers(sSearchString)
            trvUsers.EndUpdate()

            If trvUsers.Nodes.Count > 0 Then
                trvUsers.SelectedNode = trvUsers.Nodes(0)
            Else
                trvProviders.BeginUpdate()
                ResetTextFormattingOfProvidersTreeView()
                trvProviders.EndUpdate()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

#End Region

#Region "Textbox Event"

    Private Sub txtSearch_TextChanged(sender As Object, e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            If Not Timer_Search.Enabled Then
                Timer_Search.Start()
            Else
                Timer_Search.Stop()
                Timer_Search.Start()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

#End Region

#End Region

#Region "Functions and Procedures"

    Private Function GetName(ByVal FirstName As String, ByVal MiddleName As String, ByVal LastName As String) As String
        Dim sReturnedName As String = Nothing
        Try
            sReturnedName = FirstName

            If Not MiddleName = String.Empty And Not String.IsNullOrWhiteSpace(MiddleName) Then
                sReturnedName = sReturnedName + " " + MiddleName
            End If

            If Not LastName = String.Empty And Not String.IsNullOrWhiteSpace(LastName) Then
                sReturnedName = sReturnedName + " " + LastName
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return sReturnedName
    End Function

    Private Sub ResetTextFormattingOfUsersTreeView()
        Try
            For Each Node As TreeNode In trvUsers.Nodes
                For Each ElementNode As TreeNode In Node.Nodes
                    If ElementNode.NodeFont Is BoldTreeViewFont Then
                        With ElementNode
                            .ImageIndex = 1
                            .SelectedImageIndex = 1
                            .NodeFont = DefaultTreeViewFont
                            End With
                        End If
                Next
            Next

            If trvProviders.SelectedNode IsNot Nothing Then
                Dim nProviderID As Long = Convert.ToInt64(trvProviders.SelectedNode.Tag)

                Dim EnumUserWithProvider As Generic.IEnumerable(Of UserDirectAssociation) = Nothing
                Dim EnumNodeWithProvider As Generic.IEnumerable(Of TreeNode) = Nothing
                Dim localElementUser As UserDirectAssociation = Nothing

                EnumUserWithProvider = From ElementUser As UserDirectAssociation In dictionaryUser.Values
                                          Where ElementUser.ContainsKey(nProviderID)
                                             Select ElementUser


                For Each ElementUser As UserDirectAssociation In EnumUserWithProvider
                    localElementUser = ElementUser

                    EnumNodeWithProvider = From ElementNode As TreeNode In trvUsers.Nodes
                                                Where (ElementNode.Tag = localElementUser.UserID)
                                                    Select ElementNode

                    For Each ElementNode As TreeNode In EnumNodeWithProvider
                        For Each ProviderNode As TreeNode In ElementNode.Nodes
                            If ProviderNode.Tag = nProviderID Then
                                With ProviderNode
                                    .ImageIndex = 5
                                    .SelectedImageIndex = 5
                                    .NodeFont = BoldTreeViewFont
                                    End With
                                End If
                        Next
                    Next
                Next

                nProviderID = Nothing
                EnumUserWithProvider = Nothing
                EnumNodeWithProvider = Nothing
                localElementUser = Nothing

                End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub ResetTextFormattingOfProvidersTreeView()

        For Each ElementNode As TreeNode In trvProviders.Nodes
            With ElementNode
                '.ImageIndex = 1
                '.SelectedImageIndex = 1
                .NodeFont = DefaultTreeViewFont
            End With        
        Next
    End Sub

#End Region

#Region "Treeview Nodes Adding Functionality"

    Private Sub AddProviderNodes(ByVal dictionaryProviders As Dictionary(Of Long, ProviderDirectAssociation))
        Try
            '--Add Provider Nodes----------------------------------------------
            If dictionaryProviders.Count > 0 Then
                trvProviders.BeginUpdate()
                Dim sProviderFullName As String = Nothing
                Dim NodeToAdd As TreeNode = Nothing
                For Each ElementProvider As ProviderDirectAssociation In dictionaryProviders.Values
                    sProviderFullName = GetName(ElementProvider.FirstName, ElementProvider.MiddleName, ElementProvider.LastName)
                    NodeToAdd = New TreeNode()
                    With NodeToAdd
                        .Text = sProviderFullName
                        .Tag = ElementProvider.ProviderID
                    End With
                    trvProviders.Nodes.Add(NodeToAdd)
                    NodeToAdd = Nothing
                Next

            End If

            'If EnumerableBlockedProviders.Count > 0 Then
            '    Dim sProviderFullName As String = Nothing
            '    Dim NodeToAdd As TreeNode = Nothing

            '    NodeToAdd = New TreeNode()
            '    With NodeToAdd
            '        .Text = "Blocked Providers"
            '        .Tag = "BlockedProviders"
            '        .ImageIndex = 3
            '        .SelectedImageIndex = 3
            '    End With
            '    trvProviders.Nodes.Add(NodeToAdd)
            '    NodeToAdd = Nothing

            '    For Each ElementProvider As ProviderDirectAssociation In EnumerableBlockedProviders
            '        sProviderFullName = GetName(ElementProvider.FirstName, ElementProvider.MiddleName, ElementProvider.LastName)

            '        NodeToAdd = New TreeNode()
            '        With NodeToAdd
            '            .Text = sProviderFullName
            '            .Tag = ElementProvider.ProviderID
            '        End With

            '        trvProviders.Nodes(1).Nodes.Add(NodeToAdd)
            '        NodeToAdd = Nothing
            '    Next
            'End If

            trvProviders.ExpandAll()
            trvProviders.EndUpdate()
            If trvProviders.Nodes.Count > 0 Then
                trvProviders.SelectedNode = trvProviders.Nodes(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub AddUserNodes(ByVal DictionaryUser As Dictionary(Of Long, UserDirectAssociation))
        Try
            '--Add User Nodes-------------------------------------------------
            If DictionaryUser.Count > 0 Then

                'Dim NodeSorter As New DirectUserProviderNodeSorter

                Dim NodeToAdd As TreeNode = Nothing
                For Each ElementUserDirect As UserDirectAssociation In DictionaryUser.Values
                    Dim EnumProviderToAdd As Dictionary(Of Long, ProviderDirectAssociation).ValueCollection = Nothing
                    EnumProviderToAdd = ElementUserDirect.Values

                    NodeToAdd = New TreeNode()
                    With NodeToAdd
                        .Text = ElementUserDirect.LoginName
                        .Tag = ElementUserDirect.UserID
                    End With

                    If EnumProviderToAdd.Count > 0 Then
                        Dim ChildNode As TreeNode = Nothing
                        For Each ElementProvider As ProviderDirectAssociation In EnumProviderToAdd
                            ChildNode = New TreeNode()
                            With ChildNode
                                .Text = GetName(ElementProvider.FirstName, ElementProvider.MiddleName, ElementProvider.LastName)
                                .Tag = ElementProvider.ProviderID
                                .ImageIndex = 1
                                .SelectedImageIndex = 1
                            End With
                            NodeToAdd.Nodes.Add(ChildNode)
                            ChildNode = Nothing
                        Next
                    End If
                    trvUsers.Nodes.Add(NodeToAdd)
                    NodeToAdd = Nothing
                Next

                With trvUsers
                    '.TreeViewNodeSorter = NodeSorter
                    .Sort()
                    .ExpandAll()
                End With

                'NodeSorter = Nothing

                'If trvUsers.Nodes.Count > 0 Then
                '    trvUsers.SelectedNode = trvUsers.Nodes(0)
                'End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AddTreeViewNodes()

        trvUsers.BeginUpdate()
        AddUserNodes(dictionaryUser)
        trvUsers.EndUpdate()

        'trvProviders.BeginUpdate()
        AddProviderNodes(dictionaryProvider)
        'trvProviders.EndUpdate()

    End Sub

#End Region

#Region "Button Clicks and Context Menu"

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click

        Dim dtTVPAssociation As New DataTable("TVP_Associations")
        Dim dtTVPToDelete As New DataTable("TVP_ToDelete")

        Try
            Dim EnumerableUsers As Dictionary(Of Long, UserDirectAssociation).ValueCollection = dictionaryUser.Values

            With dtTVPAssociation
                .Columns.Add(New DataColumn("nAssociationID", System.Type.GetType("System.Int64")))
                .Columns.Add(New DataColumn("nProviderID", System.Type.GetType("System.Int64")))
                .Columns.Add(New DataColumn("nUserID", System.Type.GetType("System.Int64")))
            End With

            With dtTVPToDelete
                .Columns.Add(New DataColumn("nAssociationID", System.Type.GetType("System.Int64")))
                .Columns.Add(New DataColumn("nProviderID", System.Type.GetType("System.Int64")))
                .Columns.Add(New DataColumn("nUserID", System.Type.GetType("System.Int64")))
            End With

            Dim dataRow As DataRow = Nothing

            Dim EnumerableProviderList As Dictionary(Of Long, ProviderDirectAssociation).ValueCollection
            For Each ElementUser As UserDirectAssociation In EnumerableUsers
                EnumerableProviderList = ElementUser.Values

                For Each Provider As ProviderDirectAssociation In EnumerableProviderList
                    If Provider.IsNew And Not Provider.IsDeleted Then
                        dataRow = dtTVPAssociation.NewRow
                        dataRow("nAssociationID") = 0
                        dataRow("nProviderID") = Provider.ProviderID
                        dataRow("nUserID") = ElementUser.UserID
                        dtTVPAssociation.Rows.Add(dataRow)
                    ElseIf Not Provider.IsNew And Provider.IsDeleted Then
                        dataRow = dtTVPToDelete.NewRow
                        Dim n_UserID As Long = ElementUser.UserID
                        Dim n_ProviderID As Long = Provider.ProviderID

                        Dim EnumerableToDelete As EnumerableRowCollection(Of DirectAssociation) = Nothing

                        EnumerableToDelete = From ElementAssociation As DirectAssociation In EnumAssociations
                                                Where ElementAssociation.UserID = n_UserID And
                                                    ElementAssociation.ProviderID = n_ProviderID
                                                        Select ElementAssociation

                        For Each ElementAssociation As DirectAssociation In EnumerableToDelete
                            If ElementAssociation.UserID = ElementUser.UserID And ElementAssociation.ProviderID = Provider.ProviderID Then
                                dataRow("nAssociationID") = ElementAssociation.AssociationID
                            End If
                        Next
                        dataRow("nProviderID") = Provider.ProviderID
                        dataRow("nUserID") = ElementUser.UserID
                        dtTVPToDelete.Rows.Add(dataRow)
                        dataRow = Nothing
                        EnumerableToDelete = Nothing
                        EnumerableProviderList = Nothing

                    End If
                Next
            Next

            Dim bNewInsert As Boolean = False
            Dim bDelete As Boolean = False

            If dtTVPAssociation.Rows.Count > 0 Then
                bNewInsert = True
            End If

            If dtTVPToDelete.Rows.Count > 0 Then
                bDelete = True
            End If

            If bNewInsert Or bDelete Then
                Dim clsDBLayer As New clsProviderUserDirectAssociationDBLayer
                Dim nRowsInserted As Int32 = 0
                nRowsInserted = clsDBLayer.ModifyProviderUserAssociation(dtTVPAssociation, dtTVPToDelete)

                If nRowsInserted < 1 Then
                    MsgBox("Database insertion failed. Records not added.", MsgBoxStyle.Information)
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Error at database insertion in frmProviderUserDirectAssociation", False)
                Else
                    'Audit Logging here
                    If bNewInsert Then
                        If bDelete Then
                            'Audit Log
                            'Records added and deleted

                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.SecureMessage, ActivityCategory.Settings, ActivityType.Modify, "Multiple User-Provider associations added and removed", ActivityOutCome.Success)

                        Else
                            'Records added
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.SecureMessage, ActivityCategory.Settings, ActivityType.Modify, "User-Provider association(s) added", ActivityOutCome.Success)
                        End If
                    End If
                End If

                clsDBLayer.Dispose()
                clsDBLayer = Nothing
            End If
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            If dtTVPAssociation IsNot Nothing Then
                dtTVPAssociation.Clear()
                dtTVPAssociation.Dispose()
                dtTVPAssociation = Nothing
            End If

            If dtTVPToDelete IsNot Nothing Then
                dtTVPToDelete.Clear()
                dtTVPToDelete.Dispose()
                dtTVPToDelete = Nothing
            End If
        End Try
    End Sub

    Private Sub mnuDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDelete.Click, btnRemoveAssociation.Click
        Try
            Dim sInformationString As String = "Please select a Provider to remove from the association list."

            If IsNothing(trvUsers.SelectedNode) = False AndAlso trvUsers.SelectedNode.Parent IsNot Nothing Then
                Dim SelectedProvider As TreeNode = trvUsers.SelectedNode
                Dim nProviderID As Long = SelectedProvider.Tag

                'Dim nCurrentNodeID As Long = trvUsers.SelectedNode.Tag

                'If Not dictionaryUser.ContainsKey(nCurrentNodeID) Then
                Dim nUserID As Long = SelectedProvider.Parent.Tag

                If dictionaryUser.ContainsKey(nUserID) Then
                    Dim User As UserDirectAssociation = dictionaryUser(nUserID)

                    If User.ContainsKey(nProviderID) Then
                        If User(nProviderID).IsNew Then
                            User.Remove(nProviderID)
                        Else
                            User(nProviderID).IsDeleted = True
                        End If
                    End If
                    'End If

                    nUserID = Nothing
                    'nCurrentNodeID = Nothing

                    Dim ParentNode As TreeNode = trvUsers.SelectedNode.Parent

                    With trvUsers
                        .BeginUpdate()
                        .SelectedNode.Remove()
                        .EndUpdate()
                        .SelectedNode = ParentNode
                    End With

                    ParentNode = Nothing
                Else
                    MsgBox(sInformationString, MsgBoxStyle.Information)
                End If

            Else
                MsgBox(sInformationString, MsgBoxStyle.Information)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub AddAssociationOperation(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddAssociation.Click, trvProviders.DoubleClick
        If AddAssociation(sender, e) Then
            If trvUsers.SelectedNode IsNot Nothing Then
                trvUsers_AfterSelect(sender, New TreeViewEventArgs(trvUsers.SelectedNode))
            End If
        End If
    End Sub

    Private Function AddAssociation(ByVal sender As Object, ByVal e As System.EventArgs) As Boolean 'Handles btnAddAssociation.Click, trvProviders.DoubleClick
        Dim bNodeAdded As Boolean = False
        Try
            Dim bShouldExit As Boolean = True
            'If SelectedUser IsNot Nothing
            If IsNothing(trvUsers.SelectedNode) = False Then

                'If SelectedProvider IsNot Nothing
                If IsNothing(trvProviders.SelectedNode) = False Then

                    'If the Parent Node of the SelectedUser Is Nothing
                    'ie its the Parent Node
                    If IsNothing(trvUsers.SelectedNode.Parent) = True Then

                        'If the SelectedUser IsNot Nothing
                        If IsNothing(trvUsers.SelectedNode) = False Then

                            'If the Top Level Node is of type 'ActiveProvider'
                            'If trvProviders.SelectedNode.Tag.ToString <> "ActiveProviders" Then

                            'If trvProviders.SelectedNode.Tag.ToString <> "BlockedProviders" Then
                            'If trvProviders.SelectedNode.Parent.Tag <> "BlockedProviders" Then
                            bShouldExit = False
                            'End If
                            'End If

                            'End If
                        End If
                    Else
                        MsgBox("Providers can be associated only with Users.", MsgBoxStyle.Information)
                    End If
                Else
                    MsgBox("Please select a Provider from the Providers list.", MsgBoxStyle.Information)
                End If
            Else
                MsgBox("Please select a User from the Users list.", MsgBoxStyle.Information)
            End If


            If Not bShouldExit Then
                Dim nProviderID As Long = Convert.ToInt64(trvProviders.SelectedNode.Tag)
                Dim nUserID As Long = Convert.ToInt64(trvUsers.SelectedNode.Tag)

                If dictionaryProvider.ContainsKey(nProviderID) And dictionaryUser.ContainsKey(nUserID) Then

                    Dim SelectedProvider As ProviderDirectAssociation = dictionaryProvider(nProviderID)
                    Dim SelectedUser As UserDirectAssociation = dictionaryUser(nUserID)

                    If Not SelectedProvider.IsBlocked Then
                        'If Not SelectedProvider.UserID = SelectedUser.UserID And Not SelectedProvider.ContainsKey(nUserID) Then
                        If Not SelectedProvider.UserID = SelectedUser.UserID Then

                            If Not SelectedUser.ContainsKey(nProviderID) Then
                                Dim ProviderToAdd As New ProviderDirectAssociation(SelectedProvider)
                                ProviderToAdd.IsNew = True

                                'SelectedProvider.Add(UserToAdd.UserID, UserToAdd)
                                SelectedUser.Add(ProviderToAdd.ProviderID, ProviderToAdd)
                                ProviderToAdd = Nothing

                                Dim NodeToAdd As New TreeNode
                                NodeToAdd = trvProviders.SelectedNode.Clone
                                With NodeToAdd
                                    .ImageIndex = 1
                                    .SelectedImageIndex = 1
                                End With
                                trvUsers.SelectedNode.Nodes.Add(NodeToAdd)
                                bNodeAdded = True
                                NodeToAdd = Nothing
                                trvUsers.SelectedNode.Expand()
                            Else

                                If SelectedUser(nProviderID).IsDeleted = True Then
                                    SelectedUser(nProviderID).IsDeleted = False

                                    Dim NodeToAdd As New TreeNode
                                    NodeToAdd = trvProviders.SelectedNode.Clone
                                    With NodeToAdd
                                        .ImageIndex = 1
                                        .SelectedImageIndex = 1
                                    End With
                                    trvUsers.SelectedNode.Nodes.Add(NodeToAdd)
                                    bNodeAdded = True
                                    NodeToAdd = Nothing
                                Else
                                    MsgBox("The Provider that you are trying to add is already added.", MsgBoxStyle.Information)
                                End If
                                trvUsers.SelectedNode.Expand()
                            End If
                        Else
                            MessageBox.Show("The User and the Provider are same.", "Same Provider and User", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                        SelectedProvider = Nothing
                        SelectedUser = Nothing
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

        Return bNodeAdded
    End Function

    Private Sub btnAddAssociation_MouseHover(sender As Object, e As System.EventArgs) Handles btnAddAssociation.MouseHover
        btnAddAssociation.BackgroundImage = Global.gloEMRAdmin.My.Resources.YellowRight24
    End Sub

    Private Sub btnAddAssociation_MouseLeave(sender As Object, e As System.EventArgs) Handles btnAddAssociation.MouseLeave
        btnAddAssociation.BackgroundImage = Global.gloEMRAdmin.My.Resources.Right
    End Sub

    Private Sub btnRemoveAssociation_MouseHover(sender As Object, e As System.EventArgs) Handles btnRemoveAssociation.MouseHover
        btnRemoveAssociation.BackgroundImage = Global.gloEMRAdmin.My.Resources.YellowClose
    End Sub

    Private Sub btnRemoveAssociation_MouseLeave(sender As Object, e As System.EventArgs) Handles btnRemoveAssociation.MouseLeave
        btnRemoveAssociation.BackgroundImage = Global.gloEMRAdmin.My.Resources.Close
    End Sub

#End Region

#Region "Search Operations"

    Private Sub SearchUsers(ByVal SearchString As String)
        Try
            Dim EnumSearchUsers As EnumerableRowCollection(Of UserDirectAssociation) = Nothing

            EnumSearchUsers = From ElementUser As UserDirectAssociation In EnumUser
                              Where ElementUser.LoginName.ToLower.Contains(SearchString.ToLower)
                              Order By ElementUser.LoginName
                              Select ElementUser

            Dim dictionarySearchedUsers As New Dictionary(Of Long, UserDirectAssociation)
            For Each ElementUserDirect As UserDirectAssociation In EnumSearchUsers
                If dictionaryUser.ContainsKey(ElementUserDirect.UserID) Then
                    Dim SearchedUser As UserDirectAssociation = dictionaryUser(ElementUserDirect.UserID)
                    dictionarySearchedUsers.Add(ElementUserDirect.UserID, SearchedUser)
                End If
            Next

            EnumSearchUsers = Nothing

            trvUsers.Nodes.Clear()
            AddUserNodes(dictionarySearchedUsers)

            dictionarySearchedUsers.Clear()
            dictionarySearchedUsers = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub SearchProviders(ByVal SearchString As String)
        Try
            Dim EnumUserWithProvider As IEnumerable(Of UserDirectAssociation) = Nothing

            If trvProviders.SelectedNode IsNot Nothing Then
                Dim nProviderID As Long = Convert.ToInt64(trvProviders.SelectedNode.Tag)

                EnumUserWithProvider = From ElementUser As UserDirectAssociation In dictionaryUser.Values
                                         Where ElementUser.ContainsKey(nProviderID)
                                            Select ElementUser

                Dim dictionaryMatchedUsers As New Dictionary(Of Long, UserDirectAssociation)

                For Each ElementUserDirect As UserDirectAssociation In EnumUserWithProvider
                    If dictionaryUser.ContainsKey(ElementUserDirect.UserID) Then
                        Dim SearchedUser As UserDirectAssociation = dictionaryUser(ElementUserDirect.UserID)
                        dictionaryMatchedUsers.Add(ElementUserDirect.UserID, SearchedUser)
                    End If
                Next

                EnumUserWithProvider = Nothing
                AddUserNodes(dictionaryMatchedUsers)

                dictionaryMatchedUsers.Clear()
                dictionaryMatchedUsers = Nothing

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub WhereProviderNotInUser(ByVal Provider As String)
        Try
            Dim EnumUserWithProvider As IEnumerable(Of UserDirectAssociation) = Nothing
            If trvProviders.SelectedNode IsNot Nothing Then
                Dim nProviderID As Long = Convert.ToInt64(trvProviders.SelectedNode.Tag)

                EnumUserWithProvider = From ElementUser As UserDirectAssociation In dictionaryUser.Values
                                         Where Not ElementUser.ContainsKey(nProviderID)
                                            Select ElementUser

                Dim dictionaryMatchedUsers As New Dictionary(Of Long, UserDirectAssociation)

                For Each ElementUserDirect As UserDirectAssociation In EnumUserWithProvider
                    Dim SearchedUser As UserDirectAssociation = dictionaryUser(ElementUserDirect.UserID)
                    dictionaryMatchedUsers.Add(ElementUserDirect.UserID, SearchedUser)
                Next

                EnumUserWithProvider = Nothing
                AddUserNodes(dictionaryMatchedUsers)

                dictionaryMatchedUsers.Clear()
                dictionaryMatchedUsers = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

#End Region

End Class
