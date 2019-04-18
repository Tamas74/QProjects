Imports System.Data.SqlClient

Public Class frmJrSrAssociation
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
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents trvJrDOC As System.Windows.Forms.TreeView
    Friend WithEvents trvAssociation As System.Windows.Forms.TreeView
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
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOk As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents mnuDelete As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
Me.components = New System.ComponentModel.Container
Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmJrSrAssociation))
Me.pnlLocation = New System.Windows.Forms.Panel
Me.trvJrDOC = New System.Windows.Forms.TreeView
Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label
Me.Label6 = New System.Windows.Forms.Label
Me.Label7 = New System.Windows.Forms.Label
Me.Label8 = New System.Windows.Forms.Label
Me.Label9 = New System.Windows.Forms.Label
Me.Label2 = New System.Windows.Forms.Label
Me.Splitter1 = New System.Windows.Forms.Splitter
Me.Panel1 = New System.Windows.Forms.Panel
Me.trvAssociation = New System.Windows.Forms.TreeView
Me.Label5 = New System.Windows.Forms.Label
Me.Label3 = New System.Windows.Forms.Label
Me.Label10 = New System.Windows.Forms.Label
Me.Label11 = New System.Windows.Forms.Label
Me.Label12 = New System.Windows.Forms.Label
Me.Label13 = New System.Windows.Forms.Label
Me.Label1 = New System.Windows.Forms.Label
Me.cmnuDelete = New System.Windows.Forms.ContextMenu
Me.mnuDelete = New System.Windows.Forms.MenuItem
Me.pnl_tlsp_Top = New System.Windows.Forms.Panel
Me.tstrip = New System.Windows.Forms.ToolStrip
Me.btnOk = New System.Windows.Forms.ToolStripButton
Me.btnCancel = New System.Windows.Forms.ToolStripButton
Me.Panel4 = New System.Windows.Forms.Panel
Me.Panel2 = New System.Windows.Forms.Panel
Me.Label14 = New System.Windows.Forms.Label
Me.Label15 = New System.Windows.Forms.Label
Me.Label16 = New System.Windows.Forms.Label
Me.Label17 = New System.Windows.Forms.Label
Me.Panel3 = New System.Windows.Forms.Panel
Me.Panel5 = New System.Windows.Forms.Panel
Me.Label18 = New System.Windows.Forms.Label
Me.Label19 = New System.Windows.Forms.Label
Me.Label20 = New System.Windows.Forms.Label
Me.Label21 = New System.Windows.Forms.Label
Me.Panel6 = New System.Windows.Forms.Panel
Me.Panel7 = New System.Windows.Forms.Panel
Me.pnlLocation.SuspendLayout
Me.Panel1.SuspendLayout
Me.pnl_tlsp_Top.SuspendLayout
Me.tstrip.SuspendLayout
Me.Panel4.SuspendLayout
Me.Panel2.SuspendLayout
Me.Panel3.SuspendLayout
Me.Panel5.SuspendLayout
Me.Panel6.SuspendLayout
Me.Panel7.SuspendLayout
Me.SuspendLayout
'
'pnlLocation
'
Me.pnlLocation.Controls.Add(Me.trvJrDOC)
Me.pnlLocation.Controls.Add(Me.lbl_WhiteSpaceTop)
Me.pnlLocation.Controls.Add(Me.Label6)
Me.pnlLocation.Controls.Add(Me.Label7)
Me.pnlLocation.Controls.Add(Me.Label8)
Me.pnlLocation.Controls.Add(Me.Label9)
Me.pnlLocation.Dock = System.Windows.Forms.DockStyle.Fill
Me.pnlLocation.Location = New System.Drawing.Point(0, 26)
Me.pnlLocation.Name = "pnlLocation"
Me.pnlLocation.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
Me.pnlLocation.Size = New System.Drawing.Size(303, 502)
Me.pnlLocation.TabIndex = 9
'
'trvJrDOC
'
Me.trvJrDOC.BackColor = System.Drawing.Color.White
Me.trvJrDOC.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.trvJrDOC.Dock = System.Windows.Forms.DockStyle.Fill
Me.trvJrDOC.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.trvJrDOC.ForeColor = System.Drawing.Color.Black
Me.trvJrDOC.HideSelection = false
Me.trvJrDOC.ImageIndex = 0
Me.trvJrDOC.ImageList = Me.ImageList1
Me.trvJrDOC.ItemHeight = 19
Me.trvJrDOC.Location = New System.Drawing.Point(4, 8)
Me.trvJrDOC.Name = "trvJrDOC"
Me.trvJrDOC.SelectedImageIndex = 0
Me.trvJrDOC.ShowLines = false
Me.trvJrDOC.Size = New System.Drawing.Size(295, 490)
Me.trvJrDOC.TabIndex = 13
'
'ImageList1
'
Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"),System.Windows.Forms.ImageListStreamer)
Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
Me.ImageList1.Images.SetKeyName(0, "Bullet06.ico")
Me.ImageList1.Images.SetKeyName(1, "Small Arrow.ico")
Me.ImageList1.Images.SetKeyName(2, "bullet.ico")
Me.ImageList1.Images.SetKeyName(3, "arrow_01.ico")
'
'lbl_WhiteSpaceTop
'
Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(4, 1)
Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(295, 7)
Me.lbl_WhiteSpaceTop.TabIndex = 38
'
'Label6
'
Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
Me.Label6.Font = New System.Drawing.Font("Tahoma", 9!)
Me.Label6.Location = New System.Drawing.Point(4, 498)
Me.Label6.Name = "Label6"
Me.Label6.Size = New System.Drawing.Size(295, 1)
Me.Label6.TabIndex = 43
Me.Label6.Text = "label2"
'
'Label7
'
Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
Me.Label7.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Label7.Location = New System.Drawing.Point(3, 1)
Me.Label7.Name = "Label7"
Me.Label7.Size = New System.Drawing.Size(1, 498)
Me.Label7.TabIndex = 42
Me.Label7.Text = "label4"
'
'Label8
'
Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
Me.Label8.Font = New System.Drawing.Font("Tahoma", 9!)
Me.Label8.Location = New System.Drawing.Point(299, 1)
Me.Label8.Name = "Label8"
Me.Label8.Size = New System.Drawing.Size(1, 498)
Me.Label8.TabIndex = 41
Me.Label8.Text = "label3"
'
'Label9
'
Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
Me.Label9.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Label9.Location = New System.Drawing.Point(3, 0)
Me.Label9.Name = "Label9"
Me.Label9.Size = New System.Drawing.Size(297, 1)
Me.Label9.TabIndex = 40
Me.Label9.Text = "label1"
'
'Label2
'
Me.Label2.BackColor = System.Drawing.Color.Transparent
Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
Me.Label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
Me.Label2.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Label2.ForeColor = System.Drawing.Color.White
Me.Label2.Location = New System.Drawing.Point(1, 1)
Me.Label2.Name = "Label2"
Me.Label2.Size = New System.Drawing.Size(295, 21)
Me.Label2.TabIndex = 0
Me.Label2.Text = "Senior Provider"
Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
'
'Splitter1
'
Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(112,Byte),Integer), CType(CType(168,Byte),Integer), CType(CType(205,Byte),Integer))
Me.Splitter1.Location = New System.Drawing.Point(0, 0)
Me.Splitter1.Name = "Splitter1"
Me.Splitter1.Size = New System.Drawing.Size(1, 584)
Me.Splitter1.TabIndex = 10
Me.Splitter1.TabStop = false
'
'Panel1
'
Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
Me.Panel1.Controls.Add(Me.trvAssociation)
Me.Panel1.Controls.Add(Me.Label5)
Me.Panel1.Controls.Add(Me.Label3)
Me.Panel1.Controls.Add(Me.Label10)
Me.Panel1.Controls.Add(Me.Label11)
Me.Panel1.Controls.Add(Me.Label12)
Me.Panel1.Controls.Add(Me.Label13)
Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Panel1.Location = New System.Drawing.Point(0, 26)
Me.Panel1.Name = "Panel1"
Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
Me.Panel1.Size = New System.Drawing.Size(280, 502)
Me.Panel1.TabIndex = 11
'
'trvAssociation
'
Me.trvAssociation.BackColor = System.Drawing.Color.White
Me.trvAssociation.BorderStyle = System.Windows.Forms.BorderStyle.None
Me.trvAssociation.Dock = System.Windows.Forms.DockStyle.Fill
Me.trvAssociation.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.trvAssociation.ForeColor = System.Drawing.Color.Black
Me.trvAssociation.HideSelection = false
Me.trvAssociation.ImageIndex = 0
Me.trvAssociation.ImageList = Me.ImageList1
Me.trvAssociation.ItemHeight = 19
Me.trvAssociation.Location = New System.Drawing.Point(7, 8)
Me.trvAssociation.Name = "trvAssociation"
Me.trvAssociation.SelectedImageIndex = 0
Me.trvAssociation.ShowLines = false
Me.trvAssociation.Size = New System.Drawing.Size(269, 490)
Me.trvAssociation.TabIndex = 13
'
'Label5
'
Me.Label5.BackColor = System.Drawing.Color.White
Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
Me.Label5.Location = New System.Drawing.Point(4, 8)
Me.Label5.Name = "Label5"
Me.Label5.Size = New System.Drawing.Size(3, 490)
Me.Label5.TabIndex = 40
'
'Label3
'
Me.Label3.BackColor = System.Drawing.Color.White
Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
Me.Label3.Location = New System.Drawing.Point(4, 1)
Me.Label3.Name = "Label3"
Me.Label3.Size = New System.Drawing.Size(272, 7)
Me.Label3.TabIndex = 38
'
'Label10
'
Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
Me.Label10.Font = New System.Drawing.Font("Tahoma", 9!)
Me.Label10.Location = New System.Drawing.Point(4, 498)
Me.Label10.Name = "Label10"
Me.Label10.Size = New System.Drawing.Size(272, 1)
Me.Label10.TabIndex = 44
Me.Label10.Text = "label2"
'
'Label11
'
Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
Me.Label11.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Label11.Location = New System.Drawing.Point(3, 1)
Me.Label11.Name = "Label11"
Me.Label11.Size = New System.Drawing.Size(1, 498)
Me.Label11.TabIndex = 43
Me.Label11.Text = "label4"
'
'Label12
'
Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
Me.Label12.Font = New System.Drawing.Font("Tahoma", 9!)
Me.Label12.Location = New System.Drawing.Point(276, 1)
Me.Label12.Name = "Label12"
Me.Label12.Size = New System.Drawing.Size(1, 498)
Me.Label12.TabIndex = 42
Me.Label12.Text = "label3"
'
'Label13
'
Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
Me.Label13.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Label13.Location = New System.Drawing.Point(3, 0)
Me.Label13.Name = "Label13"
Me.Label13.Size = New System.Drawing.Size(274, 1)
Me.Label13.TabIndex = 41
Me.Label13.Text = "label1"
'
'Label1
'
Me.Label1.BackColor = System.Drawing.Color.Transparent
Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
Me.Label1.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Label1.ForeColor = System.Drawing.Color.White
Me.Label1.Location = New System.Drawing.Point(0, 1)
Me.Label1.Name = "Label1"
Me.Label1.Size = New System.Drawing.Size(274, 22)
Me.Label1.TabIndex = 0
Me.Label1.Text = "Junior Senior Provider Association"
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
'pnl_tlsp_Top
'
Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
Me.pnl_tlsp_Top.Controls.Add(Me.tstrip)
Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.pnl_tlsp_Top.Location = New System.Drawing.Point(1, 0)
Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
Me.pnl_tlsp_Top.Size = New System.Drawing.Size(583, 56)
Me.pnl_tlsp_Top.TabIndex = 18
'
'tstrip
'
Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
Me.tstrip.BackgroundImage = CType(resources.GetObject("tstrip.BackgroundImage"),System.Drawing.Image)
Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
Me.tstrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOk, Me.btnCancel})
Me.tstrip.Location = New System.Drawing.Point(0, 0)
Me.tstrip.Name = "tstrip"
Me.tstrip.Size = New System.Drawing.Size(583, 53)
Me.tstrip.TabIndex = 0
Me.tstrip.Text = "ToolStrip1"
'
'btnOk
'
Me.btnOk.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.btnOk.Image = CType(resources.GetObject("btnOk.Image"),System.Drawing.Image)
Me.btnOk.ImageTransparentColor = System.Drawing.Color.Magenta
Me.btnOk.Name = "btnOk"
Me.btnOk.Size = New System.Drawing.Size(66, 50)
Me.btnOk.Text = "&Save&&Cls"
Me.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
Me.btnOk.ToolTipText = "Save and Close"
'
'btnCancel
'
Me.btnCancel.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"),System.Drawing.Image)
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
Me.Panel4.Size = New System.Drawing.Size(303, 26)
Me.Panel4.TabIndex = 21
'
'Panel2
'
Me.Panel2.BackColor = System.Drawing.Color.Transparent
Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"),System.Drawing.Image)
Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
Me.Panel2.Controls.Add(Me.Label2)
Me.Panel2.Controls.Add(Me.Label14)
Me.Panel2.Controls.Add(Me.Label15)
Me.Panel2.Controls.Add(Me.Label16)
Me.Panel2.Controls.Add(Me.Label17)
Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Panel2.Location = New System.Drawing.Point(3, 0)
Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
Me.Panel2.Name = "Panel2"
Me.Panel2.Size = New System.Drawing.Size(297, 23)
Me.Panel2.TabIndex = 19
'
'Label14
'
Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
Me.Label14.Font = New System.Drawing.Font("Tahoma", 9!)
Me.Label14.Location = New System.Drawing.Point(1, 22)
Me.Label14.Name = "Label14"
Me.Label14.Size = New System.Drawing.Size(295, 1)
Me.Label14.TabIndex = 8
Me.Label14.Text = "label2"
'
'Label15
'
Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
Me.Label15.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Label15.Location = New System.Drawing.Point(0, 1)
Me.Label15.Name = "Label15"
Me.Label15.Size = New System.Drawing.Size(1, 22)
Me.Label15.TabIndex = 7
Me.Label15.Text = "label4"
'
'Label16
'
Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
Me.Label16.Font = New System.Drawing.Font("Tahoma", 9!)
Me.Label16.Location = New System.Drawing.Point(296, 1)
Me.Label16.Name = "Label16"
Me.Label16.Size = New System.Drawing.Size(1, 22)
Me.Label16.TabIndex = 6
Me.Label16.Text = "label3"
'
'Label17
'
Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
Me.Label17.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Label17.Location = New System.Drawing.Point(0, 0)
Me.Label17.Name = "Label17"
Me.Label17.Size = New System.Drawing.Size(297, 1)
Me.Label17.TabIndex = 5
Me.Label17.Text = "label1"
'
'Panel3
'
Me.Panel3.Controls.Add(Me.Panel5)
Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
Me.Panel3.Location = New System.Drawing.Point(0, 0)
Me.Panel3.Name = "Panel3"
Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
Me.Panel3.Size = New System.Drawing.Size(280, 26)
Me.Panel3.TabIndex = 22
'
'Panel5
'
Me.Panel5.BackColor = System.Drawing.Color.Transparent
Me.Panel5.BackgroundImage = CType(resources.GetObject("Panel5.BackgroundImage"),System.Drawing.Image)
Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
Me.Panel5.Controls.Add(Me.Label18)
Me.Panel5.Controls.Add(Me.Label19)
Me.Panel5.Controls.Add(Me.Label20)
Me.Panel5.Controls.Add(Me.Label1)
Me.Panel5.Controls.Add(Me.Label21)
Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel5.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Panel5.Location = New System.Drawing.Point(3, 0)
Me.Panel5.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
Me.Panel5.Name = "Panel5"
Me.Panel5.Size = New System.Drawing.Size(274, 23)
Me.Panel5.TabIndex = 19
'
'Label18
'
Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Label18.Dock = System.Windows.Forms.DockStyle.Bottom
Me.Label18.Font = New System.Drawing.Font("Tahoma", 9!)
Me.Label18.Location = New System.Drawing.Point(1, 22)
Me.Label18.Name = "Label18"
Me.Label18.Size = New System.Drawing.Size(272, 1)
Me.Label18.TabIndex = 8
Me.Label18.Text = "label2"
'
'Label19
'
Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
Me.Label19.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Label19.Location = New System.Drawing.Point(0, 1)
Me.Label19.Name = "Label19"
Me.Label19.Size = New System.Drawing.Size(1, 22)
Me.Label19.TabIndex = 7
Me.Label19.Text = "label4"
'
'Label20
'
Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Label20.Dock = System.Windows.Forms.DockStyle.Right
Me.Label20.Font = New System.Drawing.Font("Tahoma", 9!)
Me.Label20.Location = New System.Drawing.Point(273, 1)
Me.Label20.Name = "Label20"
Me.Label20.Size = New System.Drawing.Size(1, 22)
Me.Label20.TabIndex = 6
Me.Label20.Text = "label3"
'
'Label21
'
Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Label21.Dock = System.Windows.Forms.DockStyle.Top
Me.Label21.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.Label21.Location = New System.Drawing.Point(0, 0)
Me.Label21.Name = "Label21"
Me.Label21.Size = New System.Drawing.Size(274, 1)
Me.Label21.TabIndex = 5
Me.Label21.Text = "label1"
'
'Panel6
'
Me.Panel6.Controls.Add(Me.pnlLocation)
Me.Panel6.Controls.Add(Me.Panel4)
Me.Panel6.Dock = System.Windows.Forms.DockStyle.Left
Me.Panel6.Location = New System.Drawing.Point(1, 56)
Me.Panel6.Name = "Panel6"
Me.Panel6.Size = New System.Drawing.Size(303, 528)
Me.Panel6.TabIndex = 23
'
'Panel7
'
Me.Panel7.Controls.Add(Me.Panel1)
Me.Panel7.Controls.Add(Me.Panel3)
Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
Me.Panel7.Location = New System.Drawing.Point(304, 56)
Me.Panel7.Name = "Panel7"
Me.Panel7.Size = New System.Drawing.Size(280, 528)
Me.Panel7.TabIndex = 24
'
'frmJrSrAssociation
'
Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207,Byte),Integer), CType(CType(224,Byte),Integer), CType(CType(248,Byte),Integer))
Me.ClientSize = New System.Drawing.Size(584, 584)
Me.Controls.Add(Me.Panel7)
Me.Controls.Add(Me.Panel6)
Me.Controls.Add(Me.pnl_tlsp_Top)
Me.Controls.Add(Me.Splitter1)
Me.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31,Byte),Integer), CType(CType(73,Byte),Integer), CType(CType(125,Byte),Integer))
Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
Me.MaximizeBox = false
Me.MinimizeBox = false
Me.Name = "frmJrSrAssociation"
Me.ShowInTaskbar = false
Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
Me.Text = "Junior Senior Provider Association"
Me.pnlLocation.ResumeLayout(false)
Me.Panel1.ResumeLayout(false)
Me.pnl_tlsp_Top.ResumeLayout(false)
Me.pnl_tlsp_Top.PerformLayout
Me.tstrip.ResumeLayout(false)
Me.tstrip.PerformLayout
Me.Panel4.ResumeLayout(false)
Me.Panel2.ResumeLayout(false)
Me.Panel3.ResumeLayout(false)
Me.Panel5.ResumeLayout(false)
Me.Panel6.ResumeLayout(false)
Me.Panel7.ResumeLayout(false)
Me.ResumeLayout(false)

End Sub

#End Region

    ''
    Private Sub frmJrSrAssociation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim dt As New DataTable
            Dim dtAssociate As DataTable
            '   Dim clsDashBoard As New clsDoctorsDashBoard
            Dim i, j As Integer

            With trvJrDOC
                dt = GetSrDoctors()
                ''nUserID, sLoginName, Name 
                If IsNothing(dt) = False Then
                    '******By Sandip Deshmukh 26 th Oct 2007 
                    '******to check for if no senior doctor information is present in the application
                    If dt.Rows.Count > 0 Then
                        '****** 26 th Oct 2007 
                        For i = 0 To dt.Rows.Count - 1
                            Dim node As New TreeNode
                            With node
                                .Text = dt.Rows(i)("Name")
                                .Tag = dt.Rows(i)("nProviderID")
                            End With
                            .Nodes.Add(node)
                        Next
                        '.SelectedIndex = 0

                        '******By Sandip Deshmukh 26 th Oct 2007 
                        '******info message if no senior doctor information is present in the application
                        '******and closes the dialog
                    Else
                        MessageBox.Show("No Senior Provider information available", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()
                        Exit Sub
                    End If
                    '******* 26 th Oct 2007 
                End If
                .ExpandAll()
            End With

            With trvAssociation
                dt = GetJrDoctors()
                If IsNothing(dt) = False Then

                    '******By Sandip Deshmukh 26 th Oct 2007 
                    '******to check for if no junior doctor information is present in the application
                    If dt.Rows.Count > 0 Then
                        '****** 26 th Oct 2007 
                        For i = 0 To dt.Rows.Count - 1
                            Dim node As New TreeNode
                            With node
                                .Text = dt.Rows(i)("Name") '' Status
                                .Tag = dt.Rows(i)("nProviderID") '' StatusID
                            End With
                            .Nodes.Add(node)

                            dtAssociate = GetJrSrDocAssociation(node.Tag)

                            ''User_MST.nUserID, User_MST.sLoginName, Name  
                            If IsNothing(dtAssociate) = False Then
                                Dim myNode As TreeNode
                                For j = 0 To dtAssociate.Rows.Count - 1
                                    myNode = New TreeNode
                                    With myNode
                                        .Text = dtAssociate.Rows(j)("Name")
                                        .Tag = dtAssociate.Rows(j)("nProviderID")
                                        .ImageIndex = 1
                                        .SelectedImageIndex = 1
                                    End With
                                    node.Nodes.Add(myNode)
                                Next
                                '.SelectedIndex = 0
                            End If
                        Next
                        '.SelectedIndex = 0

                        '******By Sandip Deshmukh 26 th Oct 2007 
                        '******info Message for if no junior doctor information is present in the application
                        '******and closes the dialog
                    Else
                        MessageBox.Show("No Junior Provider information available", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()
                        Exit Sub
                    End If
                    '******* 26 th Oct 2007 
                End If
                .ExpandAll()
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function GetSrDoctors() As DataTable
        Dim dt As New DataTable
        Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
        Dim da As SqlDataAdapter
        Dim _sqlstr As String = ""

        dt = New DataTable
        Try
            'Resolve case GLO2010-0004942
            _sqlstr = "select sFirstName + ' ' + CASE ISNULL(Provider_MST.sMiddleName,'') WHEN  '' THEN '' When Provider_MST.sMiddleName then Provider_MST.sMiddleName + ' ' END + sLastName as Name,nProviderID from Provider_MST where nProviderType=0"
            da = New SqlDataAdapter(_sqlstr, conn)
            da.Fill(dt)

            Return dt
        Catch ex As Exception
            Return dt
        End Try


    End Function

    Public Function GetJrSrDocAssociation(ByVal id As Long) As DataTable
        Dim dt As New DataTable
        Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
        Dim da As SqlDataAdapter
        Dim _sqlstr As String = ""

        dt = New DataTable
        Try
            'Resolve case GLO2010-0004942
            _sqlstr = " SELECT  Provider_MST.sFirstName + ' ' + CASE ISNULL(Provider_MST.sMiddleName,'') WHEN  '' THEN '' When Provider_MST.sMiddleName then  Provider_MST.sMiddleName + ' '  END   + Provider_MST.sLastName AS Name, Provider_MST.nProviderID " _
                    & " FROM Provider_MST INNER JOIN ProviderSettings ON Provider_MST.nProviderID = ProviderSettings.nOthersID  " _
                    & " WHERE (ProviderSettings.sSettingsType = 'ProviderSeniorAssignment') AND ProviderSettings.nProviderID =" & id


            da = New SqlDataAdapter(_sqlstr, conn)
            da.Fill(dt)

            Return dt
        Catch ex As Exception
            Return dt
        End Try
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Try
            Dim i, j As Integer

            Dim lst As myList
            Dim objCOL As New Collection

            With trvAssociation
                For i = 0 To .GetNodeCount(False) - 1
                    Dim JrDocID As Long = 0
                    Dim SrDocID As Long = 0

                    JrDocID = .Nodes(i).Tag
                    For j = 0 To .Nodes(i).GetNodeCount(False) - 1
                        SrDocID = .Nodes(i).Nodes(j).Tag
                        lst = New myList
                        lst.ID = JrDocID
                        lst.Index = SrDocID
                        objCOL.Add(lst)
                    Next
                Next
            End With

            If Save_JrSrAssociation(objCOL) = True Then
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Function Save_JrSrAssociation(ByVal objCOL As Collection) As Boolean
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        oDB.Connect(gloEMRAdmin.mdlGeneral.GetConnectionString)
        With oDB
            .ExecuteNonQuery("adm_DELETE_JrSrAssociation")

            Dim i As Integer
            Dim lst As myList

            For i = 1 To objCOL.Count
                lst = CType(objCOL(i), myList)
                ''lst.ID '' StatusID
                ''lst.Index '' UserID
                .DBParameters.Add("@JrDocID", lst.ID, ParameterDirection.Input, SqlDbType.BigInt)
                .DBParameters.Add("@SrDocID", lst.Index, ParameterDirection.Input, SqlDbType.BigInt)

                .ExecuteNonQuery("adm_INSERT_JrSrAssociation")
                .DBParameters.Clear()
            Next

            'Sarika 21st April 2007
            Dim objAudit As New clsAudit
            objAudit.CreateLog(clsAudit.enmActivityType.Add, "Junior-Senior Doctor Association created.", gstrLoginName, gstrClientMachineName)
            objAudit = Nothing
            '------------------

        End With
        oDB.Disconnect()
        oDB = Nothing

        Return True

    End Function

    Private Sub trvJrDOC_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvJrDOC.AfterSelect

    End Sub

    'Yatin 04/17/2012
    Public Function isBlockedProvider(ByVal SproviderId As Long, ByVal JproviderId As Long) As Boolean()
        Dim bProv(1) As Boolean
        Try
        Dim oPrd As New clsProvider()
        Dim dt As New DataTable()
            dt = oPrd.GetDataFromDb(SproviderId, JproviderId, 1)

            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)(0).ToString = SproviderId.ToString() Then
                        bProv(0) = dt.Rows(0)(1)
                        bProv(1) = dt.Rows(1)(1)
                    Else
                        bProv(0) = dt.Rows(1)(1)
                        bProv(1) = dt.Rows(0)(1)
                    End If
                End If
            End If
        Catch ex As Exception
        End Try
        Return bProv
    End Function

    Private Sub trvJrDOC_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvJrDOC.DoubleClick
        Try
            Dim i As Integer
            Dim IsExists As Boolean = False
            If IsNothing(trvAssociation.SelectedNode) = True Then
                Exit Sub
            End If

            With trvJrDOC
                If IsNothing(.SelectedNode) = False Then
                    If IsNothing(trvAssociation.SelectedNode.Parent) = True Then
                        If IsNothing(trvAssociation.SelectedNode) = False Then

                            Dim BlockedProvider() As Boolean
                            BlockedProvider = isBlockedProvider(.SelectedNode.Tag, trvAssociation.SelectedNode.Tag)
                            If BlockedProvider(0) Then
                                MessageBox.Show("This senior provider is blocked and cannot be associated.", "Blocked Senior Provider", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ElseIf BlockedProvider(1) Then
                                MessageBox.Show("This junior provider is blocked and cannot be associated.", "Blocked Junior Provider", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else

                                For i = 0 To trvAssociation.SelectedNode.GetNodeCount(False) - 1
                                    If trvAssociation.SelectedNode.Nodes(i).Tag = .SelectedNode.Tag Then
                                        Exit Sub
                                    End If
                                Next

                                Dim node As New TreeNode
                                node = .SelectedNode.Clone
                                node.ImageIndex = 1
                                node.SelectedImageIndex = 1
                                trvAssociation.SelectedNode.Nodes.Add(node)
                            End If
                        End If
                    End If
                End If
            End With
            trvAssociation.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub trvAssociation_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvAssociation.MouseDown
        Dim Node As TreeNode
        Try
            With trvAssociation
                If e.Button = Windows.Forms.MouseButtons.Right Then

                    Node = trvAssociation.GetNodeAt(e.X, e.Y)
                    If IsNothing(Node) = False Then
                        .SelectedNode = Node
                    Else
                        Exit Sub
                    End If

                    If IsNothing(.SelectedNode.Parent) = False Then
                        .ContextMenu = cmnuDelete
                    Else
                        .ContextMenu = Nothing
                    End If
                End If
            End With

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub mnuDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDelete.Click
        Try
            If IsNothing(trvAssociation.SelectedNode) = False Then
                trvAssociation.SelectedNode.Remove()

                ''Sarika 20070507
                Dim objAudit As New clsAudit
                objAudit.CreateLog(clsAudit.enmActivityType.Delete, "The Senior Doctor '" & trvAssociation.SelectedNode.Text & "' removed from the Junior doctor association.", gstrLoginName, gstrClientMachineName)
                objAudit = Nothing
                '------------------
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Public Function GetJrDoctors() As DataTable
        Dim dt As DataTable
        Dim conn As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString)
        Dim da As SqlDataAdapter
        Dim _sqlstr As String = ""

        dt = New DataTable
        Try
            'Resolve case GLO2010-0004942
            _sqlstr = "select sFirstName + ' ' + CASE ISNULL(Provider_MST.sMiddleName,'') WHEN  '' THEN '' When Provider_MST.sMiddleName then  Provider_MST.sMiddleName + ' '  END  + sLastName as Name,nProviderID from Provider_MST where nProviderType=1"
            da = New SqlDataAdapter(_sqlstr, conn)
            da.Fill(dt)

            Return dt
        Catch ex As Exception
            Return dt
        End Try
    End Function

End Class
