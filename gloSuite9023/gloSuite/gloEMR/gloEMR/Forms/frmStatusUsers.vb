Imports System.Data.SqlClient

Public Class frmStatusUsers
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
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Dim dtpContextMenu As ContextMenu() = {cmnuDelete}
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpContextMenu)
                gloGlobal.cEventHelper.DisposeContextMenu(dtpContextMenu)
            Catch ex As Exception

            End Try
           
            Try
                If Not IsNothing(colordialogSatatus) Then
                    colordialogSatatus.Dispose()
                    colordialogSatatus = Nothing
                End If
            Catch ex As Exception

            End Try
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents pnlLocation As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents trvUsers As System.Windows.Forms.TreeView
    Friend WithEvents trvAssociation As System.Windows.Forms.TreeView
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents cmnuDelete As System.Windows.Forms.ContextMenu
    Friend WithEvents colordialogSatatus As System.Windows.Forms.ColorDialog
    Friend WithEvents mnuDelete As System.Windows.Forms.MenuItem
    Friend WithEvents mnuShowColor As System.Windows.Forms.MenuItem
    Friend WithEvents pnlToolStrip As System.Windows.Forms.Panel
    Friend WithEvents ts_StatusUsers As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents ts_btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ts_btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents lbl_BottomBrd As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_RightBrd As System.Windows.Forms.Label
    Private WithEvents lbl_TopBrd As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents mnuAddStatus As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStatusUsers))
        Me.pnlLocation = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.trvUsers = New System.Windows.Forms.TreeView
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.lbl_BottomBrd = New System.Windows.Forms.Label
        Me.lbl_LeftBrd = New System.Windows.Forms.Label
        Me.lbl_RightBrd = New System.Windows.Forms.Label
        Me.lbl_TopBrd = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Splitter1 = New System.Windows.Forms.Splitter
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.trvAssociation = New System.Windows.Forms.TreeView
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmnuDelete = New System.Windows.Forms.ContextMenu
        Me.mnuAddStatus = New System.Windows.Forms.MenuItem
        Me.mnuDelete = New System.Windows.Forms.MenuItem
        Me.mnuShowColor = New System.Windows.Forms.MenuItem
        Me.colordialogSatatus = New System.Windows.Forms.ColorDialog
        Me.pnlToolStrip = New System.Windows.Forms.Panel
        Me.ts_StatusUsers = New gloGlobal.gloToolStripIgnoreFocus
        Me.ts_btnSave = New System.Windows.Forms.ToolStripButton
        Me.ts_btnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlLocation.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlToolStrip.SuspendLayout()
        Me.ts_StatusUsers.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlLocation
        '
        Me.pnlLocation.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlLocation.Controls.Add(Me.Panel3)
        Me.pnlLocation.Controls.Add(Me.Panel6)
        Me.pnlLocation.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLocation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlLocation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlLocation.Location = New System.Drawing.Point(0, 54)
        Me.pnlLocation.Name = "pnlLocation"
        Me.pnlLocation.Size = New System.Drawing.Size(232, 456)
        Me.pnlLocation.TabIndex = 9
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.trvUsers)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.Label8)
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Controls.Add(Me.Label13)
        Me.Panel3.Controls.Add(Me.Label14)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 30)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel3.Size = New System.Drawing.Size(232, 426)
        Me.Panel3.TabIndex = 18
        '
        'trvUsers
        '
        Me.trvUsers.BackColor = System.Drawing.Color.White
        Me.trvUsers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvUsers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvUsers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvUsers.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.trvUsers.HideSelection = False
        Me.trvUsers.ImageIndex = 0
        Me.trvUsers.ImageList = Me.ImageList1
        Me.trvUsers.ItemHeight = 20
        Me.trvUsers.Location = New System.Drawing.Point(8, 5)
        Me.trvUsers.Name = "trvUsers"
        Me.trvUsers.SelectedImageIndex = 0
        Me.trvUsers.ShowLines = False
        Me.trvUsers.Size = New System.Drawing.Size(223, 417)
        Me.trvUsers.TabIndex = 13
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Bullet06.ico")
        Me.ImageList1.Images.SetKeyName(1, "Small Arrow.ico")
        Me.ImageList1.Images.SetKeyName(2, "Arrow_02.ico")
        Me.ImageList1.Images.SetKeyName(3, "bullet_01.ico")
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.White
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(4, 5)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(4, 417)
        Me.Label7.TabIndex = 19
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.White
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(227, 4)
        Me.Label8.TabIndex = 18
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label11.Location = New System.Drawing.Point(4, 422)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(227, 1)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "label2"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(3, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 422)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(231, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 422)
        Me.Label13.TabIndex = 15
        Me.Label13.Text = "label3"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(229, 1)
        Me.Label14.TabIndex = 14
        Me.Label14.Text = "label1"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel5)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.Panel6.Size = New System.Drawing.Size(232, 30)
        Me.Panel6.TabIndex = 19
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Transparent
        Me.Panel5.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.lbl_BottomBrd)
        Me.Panel5.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel5.Controls.Add(Me.lbl_RightBrd)
        Me.Panel5.Controls.Add(Me.lbl_TopBrd)
        Me.Panel5.Controls.Add(Me.Label2)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(3, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(229, 24)
        Me.Panel5.TabIndex = 1
        '
        'lbl_BottomBrd
        '
        Me.lbl_BottomBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_BottomBrd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_BottomBrd.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lbl_BottomBrd.Location = New System.Drawing.Point(1, 23)
        Me.lbl_BottomBrd.Name = "lbl_BottomBrd"
        Me.lbl_BottomBrd.Size = New System.Drawing.Size(227, 1)
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
        Me.lbl_RightBrd.Location = New System.Drawing.Point(228, 1)
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
        Me.lbl_TopBrd.Size = New System.Drawing.Size(229, 1)
        Me.lbl_TopBrd.TabIndex = 5
        Me.lbl_TopBrd.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(229, 24)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Users"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter1.Location = New System.Drawing.Point(232, 54)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 456)
        Me.Splitter1.TabIndex = 10
        Me.Splitter1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.Panel7)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(235, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(310, 456)
        Me.Panel1.TabIndex = 11
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.Controls.Add(Me.trvAssociation)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Controls.Add(Me.Label16)
        Me.Panel4.Controls.Add(Me.Label17)
        Me.Panel4.Controls.Add(Me.Label18)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel4.Location = New System.Drawing.Point(0, 30)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.Panel4.Size = New System.Drawing.Size(310, 426)
        Me.Panel4.TabIndex = 14
        '
        'trvAssociation
        '
        Me.trvAssociation.BackColor = System.Drawing.Color.White
        Me.trvAssociation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvAssociation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvAssociation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvAssociation.ForeColor = System.Drawing.Color.Black
        Me.trvAssociation.HideSelection = False
        Me.trvAssociation.ImageIndex = 0
        Me.trvAssociation.ImageList = Me.ImageList1
        Me.trvAssociation.ItemHeight = 20
        Me.trvAssociation.Location = New System.Drawing.Point(5, 5)
        Me.trvAssociation.Name = "trvAssociation"
        Me.trvAssociation.SelectedImageIndex = 0
        Me.trvAssociation.ShowLines = False
        Me.trvAssociation.Size = New System.Drawing.Size(301, 417)
        Me.trvAssociation.TabIndex = 13
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(1, 5)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(4, 417)
        Me.Label9.TabIndex = 21
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.White
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(1, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(305, 4)
        Me.Label10.TabIndex = 20
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(1, 422)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(305, 1)
        Me.Label15.TabIndex = 17
        Me.Label15.Text = "label2"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(0, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 422)
        Me.Label16.TabIndex = 16
        Me.Label16.Text = "label4"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(306, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 422)
        Me.Label17.TabIndex = 15
        Me.Label17.Text = "label3"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(0, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(307, 1)
        Me.Label18.TabIndex = 14
        Me.Label18.Text = "label1"
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Panel2)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.Panel7.Size = New System.Drawing.Size(310, 30)
        Me.Panel7.TabIndex = 15
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel2.Location = New System.Drawing.Point(0, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(307, 24)
        Me.Panel2.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(1, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(305, 1)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(0, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 23)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "label4"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(306, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 23)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "label3"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(307, 1)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "label1"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(307, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Status User Association"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmnuDelete
        '
        Me.cmnuDelete.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAddStatus, Me.mnuDelete, Me.mnuShowColor})
        '
        'mnuAddStatus
        '
        Me.mnuAddStatus.Index = 0
        Me.mnuAddStatus.Text = "Add Status"
        '
        'mnuDelete
        '
        Me.mnuDelete.Index = 1
        Me.mnuDelete.Text = "Delete"
        '
        'mnuShowColor
        '
        Me.mnuShowColor.Index = 2
        Me.mnuShowColor.Text = "Show Color DilogBox"
        '
        'pnlToolStrip
        '
        Me.pnlToolStrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlToolStrip.Controls.Add(Me.ts_StatusUsers)
        Me.pnlToolStrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolStrip.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlToolStrip.ForeColor = System.Drawing.Color.Black
        Me.pnlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolStrip.Name = "pnlToolStrip"
        Me.pnlToolStrip.Size = New System.Drawing.Size(545, 54)
        Me.pnlToolStrip.TabIndex = 12
        '
        'ts_StatusUsers
        '
        Me.ts_StatusUsers.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ts_StatusUsers.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ts_StatusUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ts_StatusUsers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_StatusUsers.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ts_StatusUsers.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ts_StatusUsers.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_btnSave, Me.ts_btnClose})
        Me.ts_StatusUsers.Location = New System.Drawing.Point(0, 0)
        Me.ts_StatusUsers.Name = "ts_StatusUsers"
        Me.ts_StatusUsers.Size = New System.Drawing.Size(545, 53)
        Me.ts_StatusUsers.TabIndex = 1
        Me.ts_StatusUsers.Text = "ToolStrip1"
        '
        'ts_btnSave
        '
        Me.ts_btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnSave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnSave.Image = CType(resources.GetObject("ts_btnSave.Image"), System.Drawing.Image)
        Me.ts_btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnSave.Name = "ts_btnSave"
        Me.ts_btnSave.Size = New System.Drawing.Size(66, 50)
        Me.ts_btnSave.Tag = "Save"
        Me.ts_btnSave.Text = "&Save&&Cls"
        Me.ts_btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ts_btnSave.ToolTipText = "Save and Close"
        '
        'ts_btnClose
        '
        Me.ts_btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ts_btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ts_btnClose.Image = CType(resources.GetObject("ts_btnClose.Image"), System.Drawing.Image)
        Me.ts_btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ts_btnClose.Name = "ts_btnClose"
        Me.ts_btnClose.Size = New System.Drawing.Size(43, 50)
        Me.ts_btnClose.Tag = "Close"
        Me.ts_btnClose.Text = "&Close"
        Me.ts_btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'frmStatusUsers
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(545, 510)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlLocation)
        Me.Controls.Add(Me.pnlToolStrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmStatusUsers"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Status Users Association"
        Me.pnlLocation.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlToolStrip.ResumeLayout(False)
        Me.pnlToolStrip.PerformLayout()
        Me.ts_StatusUsers.ResumeLayout(False)
        Me.ts_StatusUsers.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

   



    Private Sub frmStatusUsers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim clsDashBoard As clsDoctorsDashBoard
        Try


            clsDashBoard = New clsDoctorsDashBoard


            With trvUsers

                Dim dt As DataTable
                dt = clsDashBoard.GetUsers()
                ''nUserID, sLoginName, Name 
                Dim i As Integer
                If IsNothing(dt) = False Then
                    For i = 0 To dt.Rows.Count - 1
                        Dim node As New TreeNode


                        With node
                            .Text = dt.Rows(i)("sLoginName")
                            .Tag = dt.Rows(i)("nUserID")

                        End With
                        .Nodes.Add(node)
                    Next
                    '.SelectedIndex = 0
                    dt.Dispose()
                    dt = Nothing
                End If
                .ExpandAll()
            End With
            Call FillStatus()


        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            clsDashBoard = Nothing
        End Try
    End Sub

    Public Sub FillStatus()

        Dim clsDashBoard As clsDoctorsDashBoard



        clsDashBoard = New clsDoctorsDashBoard

        With trvAssociation


            Dim dt As DataTable
            dt = clsDashBoard.GetStatus()
            If IsNothing(dt) = False Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim strSelect As String = "SELECT nColorCode from StatusUsers where nStatusID = " & dt.Rows(i)("nCategoryID") & " "
                    Dim oDB As New gloStream.gloDataBase.gloDataBase
                    oDB.Connect(GetConnectionString)
                    Dim dtcolorcode As DataTable
                    dtcolorcode = oDB.ReadQueryDataTable(strSelect)
                    oDB.Disconnect()
                    oDB.Dispose()

                    Dim node As New TreeNode
                    With node
                        .Text = dt.Rows(i)("sDescription") '' Status
                        .Tag = dt.Rows(i)("nCategoryID") '' StatusID
                        If IsNothing(dtcolorcode) = False Then
                            If dtcolorcode.Rows.Count <> 0 Then
                                .ForeColor = Color.FromArgb(dtcolorcode.Rows(0)("nColorCode"))
                            End If
                            dtcolorcode.Dispose()
                            dtcolorcode = Nothing
                        End If

                    End With
                    .Nodes.Add(node)
                    Dim dtAssociate As DataTable
                    dtAssociate = clsDashBoard.GetAssociates(node.Tag)
                    '''''User_MST.nUserID, User_MST.sLoginName, Name  

                    If IsNothing(dtAssociate) = False Then
                        Dim myNode As TreeNode
                        For j As Integer = 0 To dtAssociate.Rows.Count - 1
                            'Dim forcolor As Long = dtAssociate.Rows(j)("nColorCode")

                            myNode = New TreeNode
                            With myNode
                                .Text = dtAssociate.Rows(j)("sLoginName")
                                .Tag = dtAssociate.Rows(j)("nUserID")
                                '.ForeColor = Color.FromArgb(dtAssociate.Rows(j)("nColorCode"))
                                .ImageIndex = 1
                                .SelectedImageIndex = 1
                            End With
                            node.Nodes.Add(myNode)
                        Next
                        '.SelectedIndex = 0
                        dtAssociate.Dispose()
                        dtAssociate = Nothing

                    End If

                Next
                '.SelectedIndex = 0
                dt.Dispose()
                dt = Nothing

            End If
            .ExpandAll()
        End With
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts_btnSave.Click
        Try
            Dim i, j As Integer

            Dim lst As myList
            Dim objCOL As New Collection

            With trvAssociation
                For i = 0 To .GetNodeCount(False) - 1
                    Dim StatusID As Long = 0
                    Dim UserID As Long = 0

                    StatusID = .Nodes(i).Tag
                    For j = 0 To .Nodes(i).GetNodeCount(False) - 1
                        UserID = .Nodes(i).Nodes(j).Tag
                        lst = New myList
                        lst.ID = StatusID
                        lst.Index = UserID
                        lst.ColorCode = trvAssociation.Nodes(i).ForeColor.ToArgb()
                        objCOL.Add(lst)
                    Next
                Next
            End With

            If Save_StatusUser(objCOL) = True Then
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Function Save_StatusUser(ByVal objCOL As Collection) As Boolean
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        oDB.Connect(GetConnectionString)
        With oDB
            .ExecuteNonQuery("gsp_DELETE_StatusUsers")

            Dim i As Integer
            Dim lst As myList

            For i = 1 To objCOL.Count
                lst = CType(objCOL(i), myList)
                '''''lst.ID '' StatusID
                '''''lst.Index '' UserID
                .DBParameters.Add("@StatusID", lst.ID, ParameterDirection.Input, SqlDbType.BigInt)
                .DBParameters.Add("@UserID", lst.Index, ParameterDirection.Input, SqlDbType.BigInt)
                .DBParameters.Add("@nColorCode", lst.ColorCode, ParameterDirection.Input, SqlDbType.BigInt)
                .ExecuteNonQuery("gsp_INSERT_StatusUsers")
                .DBParameters.Clear()
            Next
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "User Status Added", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "User Status Added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "User Status Added", gstrLoginName, gstrClientMachineName)
        End With
        oDB.Disconnect()
        oDB.Dispose()
        oDB = Nothing

        Return True

    End Function

    Private Sub trvUsers_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles trvUsers.DoubleClick
        Try
            Dim i As Integer
            Dim IsExists As Boolean = False

            'With trvUsers
            'If IsNothing(.SelectedNode) = False Then
            '    If IsNothing(trvAssociation.SelectedNode) = False Then
            '        'If IsNothing(trvAssociation.SelectedNode.Parent) = False Then
            '        For i = 0 To trvAssociation.SelectedNode.GetNodeCount(False) - 1
            '            If trvAssociation.SelectedNode.Nodes(i).Tag = .SelectedNode.Tag Then
            '                Exit Sub
            '            End If
            '        Next

            '        Dim node As New TreeNode
            '        node = .SelectedNode.Clone
            '        node.ImageIndex = 1
            '        node.SelectedImageIndex = 1
            '        trvAssociation.SelectedNode.Nodes.Add(node)
            '        'End If
            '    End If
            'End If
            'End With

            ''Sandip Darade 20090618
            ''cmmented code above and added code below to fix the issue that 
            ''not asociate user with an user but status
            With trvUsers
                If IsNothing(.SelectedNode) = False Then
                    If IsNothing(trvAssociation.SelectedNode) = False Then

                        For i = 0 To trvAssociation.SelectedNode.GetNodeCount(False) - 1
                            If trvAssociation.SelectedNode.Nodes(i).Tag = .SelectedNode.Tag Then
                                Exit Sub
                            End If
                        Next


                        Dim node As New TreeNode
                        node = .SelectedNode.Clone
                        node.ImageIndex = 1
                        node.SelectedImageIndex = 1

                        ''Sandip Darade 20090618
                        ''Associate user with status if the selected node in assocation tree is an user
                        If IsNothing(trvAssociation.SelectedNode.Parent) = False Then
                            Dim n As New TreeNode
                            For Each n In trvAssociation.SelectedNode.Parent.Nodes
                                If n.Tag = .SelectedNode.Tag Then
                                    Exit Sub
                                End If
                            Next
                            trvAssociation.SelectedNode.Parent.Nodes.Add(node)
                        Else
                            trvAssociation.SelectedNode.Nodes.Add(node)
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
        Try
            With trvAssociation
                If e.Button = Windows.Forms.MouseButtons.Right Then
                    Dim Node As TreeNode
                    Node = trvAssociation.GetNodeAt(e.X, e.Y)
                    If IsNothing(Node) = False Then
                        .SelectedNode = Node
                    Else
                        Exit Sub
                    End If
                    'Try
                    '    If (IsNothing(.ContextMenu) = False) Then
                    '        .ContextMenu.Dispose()
                    '        .ContextMenu = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    .ContextMenu = cmnuDelete
                    If IsNothing(.SelectedNode.Parent) = False Then
                        '.ContextMenu = cmnuDelete
                        mnuDelete.Visible = True
                        mnuShowColor.Visible = False

                    Else
                        '.ContextMenu = cmnuDelete
                        mnuDelete.Visible = False
                        mnuShowColor.Visible = True
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
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub mnuShowColor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuShowColor.Click
        Try
            Try
                colordialogSatatus.CustomColors = gloGlobal.gloCustomColor.customColor
            Catch ex As Exception

            End Try

            If colordialogSatatus.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                trvAssociation.SelectedNode.ForeColor = colordialogSatatus.Color
                Try
                    gloGlobal.gloCustomColor.customColor = colordialogSatatus.CustomColors
                Catch ex As Exception

                End Try

            End If

        Catch ex As Exception

        End Try

    End Sub
    Dim _Status As String = ""

    Private Sub mnuAddStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuAddStatus.Click
        

        Dim frm As New CategoryMaster
        Try
            frm.Text = "Add Status"
            frm.Isfromstatus = True
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            _Status = ""
            _Status = frm._CategoryName
            ''Sandip Darade 20100329 bug ID 6513
            ''Instead of getting all status get the newly added one only 
            If (_Status <> "") Then
                GetStatus()
            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, "Category", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            frm.Isfromstatus = False
            frm.Dispose()
            frm = Nothing
        End Try
    End Sub
    Private Sub GetStatus()

        ''Sandip Darade 20100329 bug ID 6513
        ''Instead of getting all status get the newly added one only 
        Dim dt As DataTable = Nothing
        Dim _strSQL As String = ""
        Dim cmd As SqlCommand = Nothing
        Dim conn As New SqlConnection(GetConnectionString())
        Dim da As SqlDataAdapter = Nothing

        Try
            dt = New DataTable
            _strSQL = "SELECT ISNULL(nCategoryID,0) AS nCategoryID  ,ISNULL(sDescription,'') AS sDescription FROM Category_MST WHERE sCategoryType='Status' AND  sDescription = '" & _Status.Replace("'", "''") & " '  Order By sDescription"

            cmd = New SqlCommand(_strSQL, conn)
            da = New SqlDataAdapter(cmd)
            da.Fill(dt)
            Dim node As New TreeNode
            With node
                If (dt.Rows.Count > 0) Then


                    .Text = dt.Rows(0)("sDescription")  '' Status
                    .Tag = dt.Rows(0)("nCategoryID") '' StatusID
                End If
            End With
            trvAssociation.Nodes.Add(node)
        Catch ex As Exception
            UpdateLog("clsDoctorsDashBoard -- GetStatus -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            conn.Dispose()
            conn = Nothing
            If (IsNothing(da) = False) Then
                da.Dispose()
                da = Nothing
            End If
            If (IsNothing(dt) = False) Then
                dt.Dispose()
                dt = Nothing
            End If

        End Try
    End Sub

    Private Sub trvUsers_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvUsers.MouseDown
        With trvUsers
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Dim Node As TreeNode
                Node = trvUsers.GetNodeAt(e.X, e.Y)
                If IsNothing(Node) = False Then
                    .SelectedNode = Node
                Else
                    Exit Sub
                End If
            End If
        End With
    End Sub
End Class
