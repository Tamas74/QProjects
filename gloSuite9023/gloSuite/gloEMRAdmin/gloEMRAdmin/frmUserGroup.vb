Imports System.Data.SqlClient

'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************
Public Class frmUserGroup
    Inherits System.Windows.Forms.Form

    Public blnModify As Boolean
    Dim dtUser As New DataTable
    Dim dtclmnCheckBox As New DataColumn
    Dim dtclmnUserName As New DataColumn
    Dim dtclmnPassword As New DataColumn
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOK As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlWindowsGroupsUsers As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents pnlGroupsUsers As System.Windows.Forms.Panel
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    ''Added Rahul on 20101110
    Dim chkchild As Boolean = False ''It becomes True if child node is checked.
    Dim blncheckuncheck As Boolean = False
    Friend WithEvents btnClear As System.Windows.Forms.Button ''It becomes True if Parent node is checked or unchecked.
    ''End
    Dim clRights As New Collection
    Private dtGroupRights As DataTable
    Private blnLoad As Boolean

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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtGroupName As System.Windows.Forms.TextBox
    Friend WithEvents trvRights As System.Windows.Forms.TreeView
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbWindowsGroups As System.Windows.Forms.ComboBox
    Friend WithEvents dgUsers As clsDataGrid ' System.Windows.Forms.DataGrid
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents btnExpandAll As System.Windows.Forms.Button
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents mnuSelectAll As System.Windows.Forms.MenuItem
    Friend WithEvents mnuClearAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuExpandAll As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCollapseAll As System.Windows.Forms.MenuItem

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUserGroup))
        Me.btnExpandAll = New System.Windows.Forms.Button
        Me.btnSelectAll = New System.Windows.Forms.Button
        Me.trvRights = New System.Windows.Forms.TreeView
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
        Me.mnuSelectAll = New System.Windows.Forms.MenuItem
        Me.mnuClearAll = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.mnuExpandAll = New System.Windows.Forms.MenuItem
        Me.mnuCollapseAll = New System.Windows.Forms.MenuItem
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.txtGroupName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.cmbWindowsGroups = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel
        Me.tstrip = New System.Windows.Forms.ToolStrip
        Me.btnOK = New System.Windows.Forms.ToolStripButton
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlWindowsGroupsUsers = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label15 = New System.Windows.Forms.Label
        Me.dgUsers = New gloEMRAdmin.clsDataGrid
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.pnlGroupsUsers = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.btnClear = New System.Windows.Forms.Button
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.pnlWindowsGroupsUsers.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.dgUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlGroupsUsers.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExpandAll
        '
        Me.btnExpandAll.BackgroundImage = CType(resources.GetObject("btnExpandAll.BackgroundImage"), System.Drawing.Image)
        Me.btnExpandAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExpandAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnExpandAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnExpandAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnExpandAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExpandAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExpandAll.Location = New System.Drawing.Point(295, 37)
        Me.btnExpandAll.Name = "btnExpandAll"
        Me.btnExpandAll.Size = New System.Drawing.Size(90, 24)
        Me.btnExpandAll.TabIndex = 6
        Me.btnExpandAll.Text = "Collapse All"
        '
        'btnSelectAll
        '
        Me.btnSelectAll.BackgroundImage = CType(resources.GetObject("btnSelectAll.BackgroundImage"), System.Drawing.Image)
        Me.btnSelectAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSelectAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnSelectAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnSelectAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelectAll.Location = New System.Drawing.Point(113, 37)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(80, 24)
        Me.btnSelectAll.TabIndex = 4
        Me.btnSelectAll.Text = "Select All"
        '
        'trvRights
        '
        Me.trvRights.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvRights.ContextMenu = Me.ContextMenu1
        Me.trvRights.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvRights.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.trvRights.ImageIndex = 0
        Me.trvRights.ImageList = Me.ImageList1
        Me.trvRights.Indent = 20
        Me.trvRights.ItemHeight = 20
        Me.trvRights.Location = New System.Drawing.Point(0, 6)
        Me.trvRights.Name = "trvRights"
        Me.trvRights.SelectedImageIndex = 0
        Me.trvRights.Size = New System.Drawing.Size(481, 432)
        Me.trvRights.TabIndex = 3
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuSelectAll, Me.mnuClearAll, Me.MenuItem3, Me.mnuExpandAll, Me.mnuCollapseAll})
        '
        'mnuSelectAll
        '
        Me.mnuSelectAll.Index = 0
        Me.mnuSelectAll.Text = "Select All"
        '
        'mnuClearAll
        '
        Me.mnuClearAll.Index = 1
        Me.mnuClearAll.Text = "Clear All"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.Text = "-"
        '
        'mnuExpandAll
        '
        Me.mnuExpandAll.Index = 3
        Me.mnuExpandAll.Text = "Expand All"
        '
        'mnuCollapseAll
        '
        Me.mnuCollapseAll.Index = 4
        Me.mnuCollapseAll.Text = "Collapse All"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "User Mang.ico")
        Me.ImageList1.Images.SetKeyName(1, "Bullet06.ico")
        Me.ImageList1.Images.SetKeyName(2, "Resources02.ico")
        Me.ImageList1.Images.SetKeyName(3, "")
        Me.ImageList1.Images.SetKeyName(4, "")
        '
        'txtGroupName
        '
        Me.txtGroupName.Location = New System.Drawing.Point(106, 9)
        Me.txtGroupName.MaxLength = 49
        Me.txtGroupName.Name = "txtGroupName"
        Me.txtGroupName.Size = New System.Drawing.Size(279, 22)
        Me.txtGroupName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Group Name :"
        '
        'btnBrowse
        '
        Me.btnBrowse.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Image = CType(resources.GetObject("btnBrowse.Image"), System.Drawing.Image)
        Me.btnBrowse.Location = New System.Drawing.Point(369, 30)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowse.TabIndex = 4
        '
        'cmbWindowsGroups
        '
        Me.cmbWindowsGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbWindowsGroups.Location = New System.Drawing.Point(128, 30)
        Me.cmbWindowsGroups.Name = "cmbWindowsGroups"
        Me.cmbWindowsGroups.Size = New System.Drawing.Size(235, 22)
        Me.cmbWindowsGroups.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(118, 14)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Windows Groups :"
        '
        'pnl_tlsp_Top
        '
        Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp_Top.Controls.Add(Me.tstrip)
        Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(489, 56)
        Me.pnl_tlsp_Top.TabIndex = 18
        '
        'tstrip
        '
        Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tstrip.BackgroundImage = CType(resources.GetObject("tstrip.BackgroundImage"), System.Drawing.Image)
        Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOK, Me.btnClose})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(489, 53)
        Me.tstrip.TabIndex = 0
        Me.tstrip.Text = "ToolStrip1"
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(66, 50)
        Me.btnOK.Text = "&Save&&Cls"
        Me.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnOK.ToolTipText = "Save and Close"
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(43, 50)
        Me.btnClose.Text = "&Close"
        Me.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnClose.ToolTipText = "Close"
        '
        'pnlWindowsGroupsUsers
        '
        Me.pnlWindowsGroupsUsers.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlWindowsGroupsUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlWindowsGroupsUsers.Controls.Add(Me.Panel3)
        Me.pnlWindowsGroupsUsers.Controls.Add(Me.btnBrowse)
        Me.pnlWindowsGroupsUsers.Controls.Add(Me.Label5)
        Me.pnlWindowsGroupsUsers.Controls.Add(Me.Label3)
        Me.pnlWindowsGroupsUsers.Controls.Add(Me.Label6)
        Me.pnlWindowsGroupsUsers.Controls.Add(Me.cmbWindowsGroups)
        Me.pnlWindowsGroupsUsers.Controls.Add(Me.Label7)
        Me.pnlWindowsGroupsUsers.Controls.Add(Me.Label8)
        Me.pnlWindowsGroupsUsers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlWindowsGroupsUsers.Location = New System.Drawing.Point(0, 56)
        Me.pnlWindowsGroupsUsers.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlWindowsGroupsUsers.Name = "pnlWindowsGroupsUsers"
        Me.pnlWindowsGroupsUsers.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlWindowsGroupsUsers.Size = New System.Drawing.Size(407, 518)
        Me.pnlWindowsGroupsUsers.TabIndex = 20
        Me.pnlWindowsGroupsUsers.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.Label15)
        Me.Panel3.Controls.Add(Me.dgUsers)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Location = New System.Drawing.Point(4, 88)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(399, 426)
        Me.Panel3.TabIndex = 20
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(0, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(399, 1)
        Me.Label15.TabIndex = 5
        Me.Label15.Text = "label1"
        '
        'dgUsers
        '
        Me.dgUsers.AlternatingBackColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.dgUsers.BackColor = System.Drawing.Color.GhostWhite
        Me.dgUsers.BackgroundColor = System.Drawing.Color.GhostWhite
        Me.dgUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dgUsers.CaptionBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgUsers.CaptionForeColor = System.Drawing.Color.White
        Me.dgUsers.DataMember = ""
        Me.dgUsers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgUsers.ForeColor = System.Drawing.Color.Black
        Me.dgUsers.FullRowSelect = True
        Me.dgUsers.GridLineColor = System.Drawing.Color.Black
        Me.dgUsers.HeaderBackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(162, Byte), Integer))
        Me.dgUsers.HeaderForeColor = System.Drawing.Color.White
        Me.dgUsers.LinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgUsers.Location = New System.Drawing.Point(0, 0)
        Me.dgUsers.Name = "dgUsers"
        Me.dgUsers.ParentRowsBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.dgUsers.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgUsers.RowHeadersVisible = False
        Me.dgUsers.RowHeaderWidth = 0
        Me.dgUsers.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.dgUsers.SelectionForeColor = System.Drawing.Color.Black
        Me.dgUsers.Size = New System.Drawing.Size(399, 426)
        Me.dgUsers.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 514)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(399, 1)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 511)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(403, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 511)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(401, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'pnlGroupsUsers
        '
        Me.pnlGroupsUsers.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlGroupsUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlGroupsUsers.Controls.Add(Me.btnClear)
        Me.pnlGroupsUsers.Controls.Add(Me.Panel4)
        Me.pnlGroupsUsers.Controls.Add(Me.btnExpandAll)
        Me.pnlGroupsUsers.Controls.Add(Me.Label4)
        Me.pnlGroupsUsers.Controls.Add(Me.btnSelectAll)
        Me.pnlGroupsUsers.Controls.Add(Me.Label9)
        Me.pnlGroupsUsers.Controls.Add(Me.txtGroupName)
        Me.pnlGroupsUsers.Controls.Add(Me.Label10)
        Me.pnlGroupsUsers.Controls.Add(Me.Label2)
        Me.pnlGroupsUsers.Controls.Add(Me.Label11)
        Me.pnlGroupsUsers.Controls.Add(Me.Label1)
        Me.pnlGroupsUsers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGroupsUsers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlGroupsUsers.Location = New System.Drawing.Point(0, 56)
        Me.pnlGroupsUsers.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlGroupsUsers.Name = "pnlGroupsUsers"
        Me.pnlGroupsUsers.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlGroupsUsers.Size = New System.Drawing.Size(489, 509)
        Me.pnlGroupsUsers.TabIndex = 21
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.trvRights)
        Me.Panel4.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.Panel4.Controls.Add(Me.Label16)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.Location = New System.Drawing.Point(4, 67)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(481, 438)
        Me.Panel4.TabIndex = 20
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(0, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(481, 5)
        Me.lbl_WhiteSpaceTop.TabIndex = 38
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(0, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(481, 1)
        Me.Label16.TabIndex = 5
        Me.Label16.Text = "label1"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(4, 505)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(481, 1)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "label2"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 502)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(485, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 502)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "label3"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 102)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "&Rights :"
        Me.Label2.Visible = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(3, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(483, 1)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = "label1"
        '
        'btnClear
        '
        Me.btnClear.BackgroundImage = CType(resources.GetObject("btnClear.BackgroundImage"), System.Drawing.Image)
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClear.Location = New System.Drawing.Point(199, 37)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(90, 24)
        Me.btnClear.TabIndex = 21
        Me.btnClear.Text = "Clear All"
        '
        'frmUserGroup
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(489, 565)
        Me.Controls.Add(Me.pnlGroupsUsers)
        Me.Controls.Add(Me.pnlWindowsGroupsUsers)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUserGroup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "User Group"
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.pnlWindowsGroupsUsers.ResumeLayout(False)
        Me.pnlWindowsGroupsUsers.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.dgUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlGroupsUsers.ResumeLayout(False)
        Me.pnlGroupsUsers.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Fill_UserRights()
        With trvRights
            .Nodes.Clear()
            .CheckBoxes = True
            Dim objRights As New clsRights
            Dim dtRights As New DataTable
            dtRights = objRights.ScanParentRights()

            Dim trvParentNodes As TreeNode
            Dim nCount As Integer
            For nCount = 0 To dtRights.Rows.Count - 1
                trvParentNodes = New TreeNode
                With trvParentNodes
                    .Text = dtRights.Rows(nCount).Item(0)
                    .Tag = dtRights.Rows(nCount).Item(1)
                    .ImageIndex = 0
                    .SelectedImageIndex = 0
                    .ForeColor = Color.Black
                End With
                trvRights.Nodes.Add(trvParentNodes)
                Dim dtChild As New DataTable
                dtChild = objRights.ScanChildRights(Trim(trvParentNodes.Text))
                Dim trvChildNode As TreeNode
                Dim nCountChild As Integer
                For nCountChild = 0 To dtChild.Rows.Count - 1
                    trvChildNode = New TreeNode
                    With trvChildNode
                        .Text = dtChild.Rows(nCountChild).Item(0)
                        .Tag = dtChild.Rows(nCountChild).Item(1)
                        .ImageIndex = 1
                        .SelectedImageIndex = 1
                        .ForeColor = Color.Blue
                    End With
                    trvRights.Nodes(nCount).Nodes.Add(trvChildNode)
                    trvChildNode = Nothing
                Next
                dtChild = Nothing
                trvParentNodes = Nothing
            Next
            trvRights.ExpandAll()
            dtRights = Nothing
            objRights = Nothing
            Dim i, j As Int16
            Dim blnSelectedAll As Boolean = True
            For i = 0 To trvRights.GetNodeCount(False) - 1
                For j = 0 To trvRights.Nodes(i).GetNodeCount(False) - 1
                    If trvRights.Nodes(i).Nodes(j).Checked = False Then
                        blnSelectedAll = False
                        Exit Sub
                    End If
                Next
            Next
            If blnSelectedAll = False Then
                ' '' ''btnSelectAll.Text = "Clear All"
            Else
                btnSelectAll.Text = "Select All"
            End If
        End With
    End Sub

    Private Sub frmUserGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Me.Cursor = Cursors.Default
            'Call Fill_UserRights()
            'Call Fill_WindowsGroups()

            dtGroupRights = New DataTable

            cmbWindowsGroups.Items.Clear()
            cmbWindowsGroups.Items.Add("Browse Windows Group")
            cmbWindowsGroups.SelectedIndex = 0

            With dtclmnCheckBox
                .ColumnName = "Select User"
                .DataType = System.Type.GetType("System.Boolean")
                .AllowDBNull = False
                .DefaultValue = False
            End With
            With dtclmnUserName
                .ColumnName = "User Name"
                .Caption = "User Name"
            End With
            With dtclmnPassword
                .ColumnName = "Password"
                .Caption = "Password"
            End With
            With dtUser
                .Columns.Add(dtclmnCheckBox)
                .Columns.Add(dtclmnUserName)
                .Columns.Add(dtclmnPassword)
            End With


            Dim grdTableStyle As New clsDataGridTableStyle(dtUser.TableName)

            With dgUsers
                .DataSource = dtUser
                .CaptionText = "Windows Groups Users"
            End With
            'Dim grdTableStyle As New DataGridTableStyle
            'With grdTableStyle
            '    .AlternatingBackColor = Color.GhostWhite
            '    .AllowSorting = True
            '    .ReadOnly = False
            '    .MappingName = dtUser.TableName
            '    .PreferredColumnWidth = 125
            '    .PreferredRowHeight = 15
            'End With

            Dim grdColStyleCheckBox As New DataGridBoolColumn
            With grdColStyleCheckBox
                .HeaderText = "Select"
                .AllowNull = False
                .NullValue = False
                .NullText = False
                .TrueValue = True
                .FalseValue = False
                .ReadOnly = False
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtUser.Columns(0).ColumnName
                .Width = 50
            End With

            Dim grdColStyleUserName As New DataGridTextBoxColumn
            With grdColStyleUserName
                .HeaderText = "User Name"
                .ReadOnly = True
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtUser.Columns(1).ColumnName
                .NullText = ""
                .Width = 100
            End With

            Dim grdColStylePassword As New DataGridPasswordcharTextBoxColumn
            With grdColStylePassword
                '.TextBox.Multiline = False
                '.TextBox.PasswordChar = "*"
                Dim myGridTextBox As DataGridTextBox
                myGridTextBox = CType(grdColStylePassword.TextBox, DataGridTextBox)
                myGridTextBox.Multiline = False
                myGridTextBox.PasswordChar = "*"


                .HeaderText = "Password"
                .TextBox.PasswordChar = "*"

                .ReadOnly = False
                .Alignment = HorizontalAlignment.Left
                .MappingName = dtUser.Columns(2).ColumnName
                .Width = 100
                .NullText = ""
            End With


            dtGroupRights.Columns.Add("RightsID", GetType(Int64))
            dtGroupRights.Columns.Add("GroupName", GetType(String))
            dtGroupRights.Columns.Add("GroupID", GetType(Int64))
            dtGroupRights.Columns.Add("RightsStatus", GetType(Boolean))

            grdTableStyle.GridColumnStyles.AddRange(New DataGridColumnStyle() {grdColStyleCheckBox, grdColStyleUserName, grdColStylePassword})
            dgUsers.TableStyles.Clear()
            dgUsers.TableStyles.Add(grdTableStyle)
            blnLoad = True

            Me.Cursor = Cursors.Default

        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            If Trim(txtGroupName.Text) = "" Then
                MessageBox.Show("Group name must be entered", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtGroupName.Focus()
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim nCount As Integer
            For nCount = 0 To CType(dgUsers.DataSource, DataTable).Rows.Count - 1
                If IsNothing(dgUsers.Item(nCount, 0)) = False Then
                    If dgUsers.Item(nCount, 0) = True Then
                        If IsDBNull(dgUsers.Item(nCount, 2)) = True Then
                            MessageBox.Show("Enter the password for " & Trim(dgUsers.Item(nCount, 1)) & " user", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            dgUsers.CurrentCell = New DataGridCell(nCount, 2)
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        ElseIf Trim(dgUsers.Item(nCount, 2)) = "" Then
                            MessageBox.Show("Enter the password for " & Trim(dgUsers.Item(nCount, 1)) & " user", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            dgUsers.CurrentCell = New DataGridCell(nCount, 2)
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        End If
                    End If
                End If
            Next
            Dim clUserRights As New Collection
            clUserRights = GetCheckedNodes()
            If clUserRights.Count = 0 Then
                MessageBox.Show("Select minimum one right from group", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                trvRights.Focus()
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
            Dim objUserGroup As New clsUserGroups
            'Check Group already exists or not
            If blnModify = True Then
                If objUserGroup.CheckGroupExists(Trim(txtGroupName.Text), txtGroupName.Tag) = True Then
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("Group already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    objUserGroup = Nothing
                    txtGroupName.Focus()
                    Exit Sub
                End If
            Else
                If objUserGroup.CheckGroupExists(Trim(txtGroupName.Text)) = True Then
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("Group already exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    objUserGroup = Nothing
                    txtGroupName.Focus()
                    Exit Sub
                End If
            End If

            objUserGroup.GroupName = Trim(txtGroupName.Text)
            objUserGroup.GroupRights = clUserRights
            'Add User & User Rights
            Dim clUsers As New Collection
            Dim clPassword As New Collection
            For nCount = 0 To CType(dgUsers.DataSource, DataTable).Rows.Count - 1
                If IsNothing(dgUsers.Item(nCount, 0)) = False Then
                    If dgUsers.Item(nCount, 0) = True Then
                        clUsers.Add(dgUsers.Item(nCount, 1))
                        clPassword.Add(dgUsers.Item(nCount, 2))
                    End If
                End If
            Next
            objUserGroup.Users = clUsers
            objUserGroup.UserPasswords = clPassword


            If blnModify = True Then

                Dim intMessageResult As Integer

                '17-Oct-13 Aniket: User Group permissions change as per UCD testing
                If dtGroupRights.Rows.Count > 0 Then

                    If objUserGroup.CheckUserGroups(txtGroupName.Tag) > 0 Then

                        intMessageResult = MsgBox("Do you want to update the rights for the existing users which are in this group?", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Question)

                        If intMessageResult = 2 Then
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        End If

                    End If

                End If

                If objUserGroup.EditUserGroup(txtGroupName.Tag) = True Then

                    If intMessageResult = 6 Then
                        objUserGroup.SaveUserGroups(dtGroupRights)
                        Dim DtUser As New DataTable
                        DtUser = getUserInGroups(txtGroupName.Tag)
                        Dim index As Integer
                        If Not DtUser Is Nothing AndAlso DtUser.Rows.Count > 0 Then
                            For index = 0 To DtUser.Rows.Count - 1
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ChangesToUserPrivileges, gloAuditTrail.ActivityType.Modify, "User Rights of User " & DtUser.Rows(index)("sLoginName") & " are modified ", 0, DtUser.Rows(index)("nUserID"), 0, gloAuditTrail.ActivityOutCome.Success)
                            Next
                        End If
                        DtUser = Nothing
                    End If


                    objUserGroup = Nothing
                    Me.DialogResult = DialogResult.OK
                    Me.Cursor = Cursors.Default
                    Me.Close()

                    Dim objAudit As New clsAudit
                    objAudit.CreateLog(clsAudit.enmActivityType.Modify, gstrLoginName & " user has modified the User Group : " & txtGroupName.Text, gstrLoginName, gstrClientMachineName, 0, True)
                    objAudit = Nothing

                    Exit Sub

                Else
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("Unable to update user group", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            Else
                If objUserGroup.AddUserGroups() = True Then
                    objUserGroup = Nothing
                    Me.DialogResult = DialogResult.OK
                    Me.Cursor = Cursors.Default
                    Me.Close()
                    'sarika  22nd feb
                    Dim objAudit As New clsAudit
                    objAudit.CreateLog(clsAudit.enmActivityType.Add, gstrLoginName & " user has added a new User Group : " & txtGroupName.Text, gstrLoginName, gstrClientMachineName, 0, True)
                    objAudit = Nothing
                    '-------------
                    Exit Sub
                Else
                    Me.Cursor = Cursors.Default
                    MessageBox.Show("Unable to add user group", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
            Me.Cursor = Cursors.Default
            objUserGroup = Nothing
            Me.DialogResult = DialogResult.None
        Catch objErr As Exception
            Me.DialogResult = DialogResult.None
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function getUserInGroups(ByVal nGroupID As Int64) As DataTable
        Dim objCmd As New SqlCommand
        Dim objSqlAdapter As New SqlDataAdapter
        Dim dt As New DataTable
        Dim objCon As New SqlConnection
        Try

            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetGroupUsers"
            Dim objParaUserName As New SqlParameter
            With objParaUserName
                .ParameterName = "@nGroupID"
                .Value = nGroupID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaUserName)
            objCmd.Connection = objCon
            objSqlAdapter.SelectCommand = objCmd
            objSqlAdapter.Fill(dt)


            Return dt
        Catch objErr As Exception
            Me.DialogResult = DialogResult.None
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            objCon = Nothing
            objCmd = Nothing
            objSqlAdapter = Nothing
            dt = Nothing
        End Try

    End Function

    Private Sub Fill_WindowsGroups()
        With cmbWindowsGroups
            .Items.Clear()
            .Items.Add("Select Windows Group")
            Dim objWindowsGroupsUsers As New clsWindowsGroupsUsers
            Dim arrGroups As New Collection
            arrGroups = objWindowsGroupsUsers.PopulateWindowsGroups()
            Dim nCount As Integer
            For nCount = 1 To arrGroups.Count
                .Items.Add(arrGroups.Item(nCount))
            Next
            Dim objGroups As New clsUserGroups
            Dim clGroups As New Collection
            clGroups = objGroups.PopulateUserGroups()
            For nCount = 1 To clGroups.Count
                .Items.Remove(clGroups.Item(nCount))
            Next
            If .Items.Count >= 1 Then .SelectedIndex = 0
        End With
    End Sub

    Private Sub cmbWindowsGroups_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbWindowsGroups.SelectedIndexChanged
        Try
            If cmbWindowsGroups.SelectedIndex <= 0 Or Trim(cmbWindowsGroups.SelectedItem) = "Select Windows Group" Then
                Exit Sub
            End If
            'Clear DataGrid
            CType(dgUsers.DataSource, DataTable).Rows.Clear()
            txtGroupName.Text = Trim(cmbWindowsGroups.SelectedItem)
            Dim objWindowsGroupsUsers As New clsWindowsGroupsUsers
            Dim arrUsers As New Collection
            arrUsers = objWindowsGroupsUsers.PopulateWindowsUsers(Trim(cmbWindowsGroups.SelectedItem))
            Dim nCount As Integer
            Dim dtUsers As New DataTable
            Dim objUser As New clsUsers
            dtUsers = objUser.PopulateUsers(clsUsers.enmUsersType.All)
            objUser = Nothing
            Dim nCount1 As Integer
            Dim blnCheck As Boolean
            For nCount = 1 To arrUsers.Count
                blnCheck = False
                For nCount1 = 0 To dtUsers.Rows.Count - 1
                    If UCase(Trim(arrUsers.Item(nCount))) = UCase(dtUsers.Rows(nCount1).Item(1)) Then
                        blnCheck = True
                    End If
                Next
                If blnCheck = False Then
                    Dim drRow As DataRow
                    drRow = dtUser.NewRow
                    drRow(dtclmnUserName) = arrUsers.Item(nCount)
                    dtUser.Rows.Add(drRow)
                    drRow = Nothing
                End If
            Next
            objWindowsGroupsUsers = Nothing


        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub



    Private Function GetCheckedNodes() As Collection
        Dim nCount As Integer
        For nCount = clRights.Count To 1 Step -1
            clRights.Remove(nCount)
        Next
        Dim trvnde As TreeNode
        For Each trvnde In trvRights.Nodes
            GetCheckedNodes(trvnde)
        Next
        Return clRights
    End Function
    Private Sub GetCheckedNodes(ByVal rootNode As TreeNode)
        If rootNode.Checked Then
            'clRights.Add(rootNode.Text)
            clRights.Add(rootNode.Tag)
        End If
        For Each childNode As TreeNode In rootNode.Nodes
            GetCheckedNodes(childNode)
        Next
    End Sub

    Private Sub CheckUncheckChildNodes(ByVal rootNode As TreeNode, ByVal blnCheck As Boolean)
        For Each childNode As TreeNode In rootNode.Nodes
            childNode.Checked = blnCheck
            CheckUncheckChildNodes(childNode, blnCheck)
        Next
    End Sub

    Private Sub trvRights_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvRights.AfterCheck
        Try

            Dim dvRights As New DataView

            If e.Node.Nodes.Count = 0 Then

                If blnLoad = True Then

                    dtGroupRights.DefaultView.RowFilter = "RightsID = " & e.Node.Tag

                    If dtGroupRights.DefaultView.Count > 0 Then
                        dtGroupRights.DefaultView.Item(0)("RightsStatus") = e.Node.Checked
                    Else

                        '24-Oct-13 Aniket: Resolving Exception which was thrown in new mode
                        If Not txtGroupName.Tag Is Nothing Then
                            dtGroupRights.Rows.Add()
                            dtGroupRights.Rows(dtGroupRights.Rows.Count - 1)("RightsID") = e.Node.Tag
                            dtGroupRights.Rows(dtGroupRights.Rows.Count - 1)("GroupName") = txtGroupName.Text
                            dtGroupRights.Rows(dtGroupRights.Rows.Count - 1)("GroupID") = txtGroupName.Tag
                            dtGroupRights.Rows(dtGroupRights.Rows.Count - 1)("RightsStatus") = e.Node.Checked
                        End If

                    End If

                    dtGroupRights.DefaultView.RowFilter = Nothing

                End If

            End If

            If e.Node.Nodes.Count = 0 And blncheckuncheck = False Then


                chkchild = True
                If e.Node.Checked = False Then
                    e.Node.Parent.Checked = False
                Else

                    Dim blnchkallchild As Boolean = True ''It is use to check whether all child node of parent are checked or not.
                    Dim node As TreeNode = e.Node.Parent
                    For Each childnode As TreeNode In node.Nodes
                        If childnode.Checked = False Then
                            blnchkallchild = False
                            Exit For
                        End If
                    Next
                    If blnchkallchild = True Then
                        e.Node.Parent.Checked = True
                    End If

                End If
            Else
                If chkchild = False Then
                    blncheckuncheck = True
                    CheckUncheckChildNodes(e.Node, e.Node.Checked)
                    blncheckuncheck = False
                    'chkchild = False
                End If
            End If
            chkchild = False
            '******By Sandip Deshmukh 17 Oct 07 Bug#346
            '******for user should not allow to associate multiple user groups
            Dim i, j As Int16
            Dim blnChecked As Boolean = True
            With trvRights
                For i = 0 To .GetNodeCount(False) - 1
                    For j = 0 To .Nodes(i).GetNodeCount(False) - 1
                        If .Nodes(i).Nodes(j).Checked = False Then
                            blnChecked = False
                            Exit For
                        End If
                    Next
                    If blnChecked = False Then
                        Exit For
                    End If
                Next

                If blnChecked = False Then
                    btnSelectAll.Text = "Select All"
                Else
                    ' '' ''btnSelectAll.Text = "Clear All"
                End If
            End With
            '******17 Oct 07 Bug#346
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Call Fill_WindowsGroups()
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnExpandAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpandAll.Click
        Try
            If btnExpandAll.Text = "Expand All" Then
                trvRights.ExpandAll()
                btnExpandAll.Text = "Collapse All"
            ElseIf btnExpandAll.Text = "Collapse All" Then
                trvRights.CollapseAll()
                btnExpandAll.Text = "Expand All"
            End If
            ' trvRights.ExpandAll()
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCollapseAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            trvRights.CollapseAll()
            btnExpandAll.Text = "Expand All"
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        Try
            'Me.Cursor = Cursors.WaitCursor
            'Dim nCount As Int16
            'Dim nTotalNodes As Int16
            'nTotalNodes = trvRights.GetNodeCount(False) - 1
            'For nCount = 0 To nTotalNodes
            '    trvRights.Nodes(nCount).Checked = True
            'Next
            'Me.Cursor = Cursors.Default

            If btnSelectAll.Text = "Select All" Then
                SelectAll()
                ' '' ''    btnSelectAll.Text = "Clear All"
                ' '' ''ElseIf btnSelectAll.Text = "Clear All" Then
                ' '' ''    ClearAll()
                ' '' ''    btnSelectAll.Text = "Select All"

            End If
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub SelectAll()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim nCount As Int16
            Dim nTotalNodes As Int16
            nTotalNodes = trvRights.GetNodeCount(False) - 1
            For nCount = 0 To nTotalNodes
                trvRights.Nodes(nCount).Checked = True
            Next
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearAll()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim nCount As Int16
            Dim nTotalNodes As Int16
            nTotalNodes = trvRights.GetNodeCount(False) - 1
            For nCount = 0 To nTotalNodes
                trvRights.Nodes(nCount).Checked = False
            Next
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim nCount As Int16
            Dim nTotalNodes As Int16
            nTotalNodes = trvRights.GetNodeCount(False) - 1
            For nCount = 0 To nTotalNodes
                trvRights.Nodes(nCount).Checked = False
            Next
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub mnuSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSelectAll.Click
        Try
            btnSelectAll_Click(sender, e)
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClearAll.Click
        Try
            btnClearAll_Click(sender, e)
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuExpandAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExpandAll.Click
        Try
            btnExpandAll.Text = "Expand All"
            btnExpandAll_Click(sender, e)
            'If trvRights.ExpandAll = True Then
            btnExpandAll.Text = "Collapse All"
            'End If
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuCollapseAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCollapseAll.Click
        Try
            btnExpandAll.Text = "Collapse All"
            btnCollapseAll_Click(sender, e)
            btnExpandAll.Text = "Expand All"
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tstrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tstrip.ItemClicked

    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlWindowsGroupsUsers.Paint

    End Sub

    Private Sub pnlGroupsUsers_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlGroupsUsers.Paint

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub
End Class
