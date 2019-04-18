Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient

'***************************************************************************
'Developer: Mitesh Patel
'Date:20-Dec-2011'
'PRD: Lab Usability Admin Setting
'***************************************************************************
Public Class frmLabUserTask
    Inherits System.Windows.Forms.Form

    Public blnModify As Boolean
    Dim dtUser As New DataTable
    Dim dtclmnCheckBox As New DataColumn
    Dim dtclmnUserName As New DataColumn
    Dim dtclmnPassword As New DataColumn
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnSaveNclose As System.Windows.Forms.ToolStripButton
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
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    ''Added Rahul on 20101110
    Dim chkchild As Boolean = False ''It becomes True if child node is checked.
    Dim blncheckuncheck As Boolean = False
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents C1ProviderDetails As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents cmbAbnormalSendTask As System.Windows.Forms.ComboBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents btnABSelect_UnselectAll As System.Windows.Forms.Button
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Private WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents trvAbnormalGroups As System.Windows.Forms.TreeView
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label33 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents cmbNormalSendTask As System.Windows.Forms.ComboBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents btnSelect_UnselectAll As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lblProviderName As System.Windows.Forms.Label
    ''It becomes True if Parent node is checked or unchecked.
    ''End
    Dim clRights As New Collection
    Private Const COL_PROVIDERID As Byte = 0
    Private Const COL_PROVIDERNAME As Byte = 1
    Private Const COL_GENDER As Byte = 2
    Private Const COL_Users As Byte = 3
    Private Const COL_ImageUsers As Byte = 4

    Private bParentTrigger As Boolean = True
    Private bChildTrigger As Boolean = True
    Private bABParentTrigger As Boolean = True
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private bABChildTrigger As Boolean = True
    Private PreviousDt As DataTable
    Private bSavenClose As Boolean = False
    Friend WithEvents btnAbCollapse_ExpandAll As System.Windows.Forms.Button
    Friend WithEvents btnCollapse_ExpandAll As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Private nProviderID As Long
    Private isUserBlocked As Boolean = False
    Private Enum enumSendTask
        For_each_result = 1
        Upon_order_Completion = 2

    End Enum

    Dim _ToList As gloGeneralItem.gloItems
    Dim ofrmList As New frmUserList
    Dim oListUsers As gloListControl.gloListControl
    Dim nSelectedRow As Integer = 0

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
    Friend WithEvents trvNormalGroups As System.Windows.Forms.TreeView
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    ' System.Windows.Forms.DataGrid

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLabUserTask))
        Me.trvNormalGroups = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel()
        Me.tstrip = New System.Windows.Forms.ToolStrip()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnSaveNclose = New System.Windows.Forms.ToolStripButton()
        Me.btnClose = New System.Windows.Forms.ToolStripButton()
        Me.pnlWindowsGroupsUsers = New System.Windows.Forms.Panel()
        Me.C1ProviderDetails = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlGroupsUsers = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.btnAbCollapse_ExpandAll = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbAbnormalSendTask = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.btnABSelect_UnselectAll = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.trvAbnormalGroups = New System.Windows.Forms.TreeView()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.btnCollapse_ExpandAll = New System.Windows.Forms.Button()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.cmbNormalSendTask = New System.Windows.Forms.ComboBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.btnSelect_UnselectAll = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblProviderName = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.pnlWindowsGroupsUsers.SuspendLayout()
        CType(Me.C1ProviderDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlGroupsUsers.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'trvNormalGroups
        '
        Me.trvNormalGroups.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvNormalGroups.CheckBoxes = True
        Me.trvNormalGroups.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvNormalGroups.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.trvNormalGroups.ImageIndex = 0
        Me.trvNormalGroups.ImageList = Me.ImageList1
        Me.trvNormalGroups.Indent = 20
        Me.trvNormalGroups.ItemHeight = 20
        Me.trvNormalGroups.Location = New System.Drawing.Point(1, 6)
        Me.trvNormalGroups.Name = "trvNormalGroups"
        Me.trvNormalGroups.SelectedImageIndex = 0
        Me.trvNormalGroups.Size = New System.Drawing.Size(313, 303)
        Me.trvNormalGroups.TabIndex = 2
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
        Me.ImageList1.Images.SetKeyName(5, "Browse.ico")
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
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(709, 56)
        Me.pnl_tlsp_Top.TabIndex = 0
        '
        'tstrip
        '
        Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tstrip.BackgroundImage = CType(resources.GetObject("tstrip.BackgroundImage"), System.Drawing.Image)
        Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSave, Me.btnSaveNclose, Me.btnClose})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(709, 53)
        Me.tstrip.TabIndex = 0
        Me.tstrip.Text = "ToolStrip1"
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(40, 50)
        Me.btnSave.Text = "Sa&ve"
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnSave.ToolTipText = "Save"
        '
        'btnSaveNclose
        '
        Me.btnSaveNclose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveNclose.Image = CType(resources.GetObject("btnSaveNclose.Image"), System.Drawing.Image)
        Me.btnSaveNclose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSaveNclose.Name = "btnSaveNclose"
        Me.btnSaveNclose.Size = New System.Drawing.Size(66, 50)
        Me.btnSaveNclose.Text = "&Save&&Cls"
        Me.btnSaveNclose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnSaveNclose.ToolTipText = "Save and Close"
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
        Me.pnlWindowsGroupsUsers.Controls.Add(Me.C1ProviderDetails)
        Me.pnlWindowsGroupsUsers.Controls.Add(Me.Label5)
        Me.pnlWindowsGroupsUsers.Controls.Add(Me.Label6)
        Me.pnlWindowsGroupsUsers.Controls.Add(Me.Label7)
        Me.pnlWindowsGroupsUsers.Controls.Add(Me.Label8)
        Me.pnlWindowsGroupsUsers.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlWindowsGroupsUsers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlWindowsGroupsUsers.Location = New System.Drawing.Point(0, 56)
        Me.pnlWindowsGroupsUsers.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlWindowsGroupsUsers.Name = "pnlWindowsGroupsUsers"
        Me.pnlWindowsGroupsUsers.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlWindowsGroupsUsers.Size = New System.Drawing.Size(709, 197)
        Me.pnlWindowsGroupsUsers.TabIndex = 2
        '
        'C1ProviderDetails
        '
        Me.C1ProviderDetails.AllowDelete = True
        Me.C1ProviderDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1ProviderDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1ProviderDetails.ColumnInfo = "3,1,0,0,0,105,Columns:0{Visible:False;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1ProviderDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1ProviderDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1ProviderDetails.Location = New System.Drawing.Point(4, 4)
        Me.C1ProviderDetails.Name = "C1ProviderDetails"
        Me.C1ProviderDetails.Rows.DefaultSize = 21
        Me.C1ProviderDetails.Rows.Fixed = 0
        Me.C1ProviderDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1ProviderDetails.Size = New System.Drawing.Size(701, 189)
        Me.C1ProviderDetails.StyleInfo = resources.GetString("C1ProviderDetails.StyleInfo")
        Me.C1ProviderDetails.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 193)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(701, 1)
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
        Me.Label6.Size = New System.Drawing.Size(1, 190)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(705, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 190)
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
        Me.Label8.Size = New System.Drawing.Size(703, 1)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "label1"
        '
        'pnlGroupsUsers
        '
        Me.pnlGroupsUsers.AutoScroll = True
        Me.pnlGroupsUsers.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlGroupsUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlGroupsUsers.Controls.Add(Me.Panel3)
        Me.pnlGroupsUsers.Controls.Add(Me.Panel1)
        Me.pnlGroupsUsers.Controls.Add(Me.lblProviderName)
        Me.pnlGroupsUsers.Controls.Add(Me.Label4)
        Me.pnlGroupsUsers.Controls.Add(Me.Label9)
        Me.pnlGroupsUsers.Controls.Add(Me.Label10)
        Me.pnlGroupsUsers.Controls.Add(Me.Label2)
        Me.pnlGroupsUsers.Controls.Add(Me.Label11)
        Me.pnlGroupsUsers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGroupsUsers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlGroupsUsers.Location = New System.Drawing.Point(0, 253)
        Me.pnlGroupsUsers.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlGroupsUsers.Name = "pnlGroupsUsers"
        Me.pnlGroupsUsers.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlGroupsUsers.Size = New System.Drawing.Size(709, 492)
        Me.pnlGroupsUsers.TabIndex = 21
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label20)
        Me.Panel3.Controls.Add(Me.btnAbCollapse_ExpandAll)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.cmbAbnormalSendTask)
        Me.Panel3.Controls.Add(Me.Label21)
        Me.Panel3.Controls.Add(Me.btnABSelect_UnselectAll)
        Me.Panel3.Controls.Add(Me.Panel5)
        Me.Panel3.Controls.Add(Me.Label24)
        Me.Panel3.Controls.Add(Me.Label25)
        Me.Panel3.Controls.Add(Me.Panel6)
        Me.Panel3.Controls.Add(Me.Label34)
        Me.Panel3.Controls.Add(Me.Label35)
        Me.Panel3.Location = New System.Drawing.Point(358, 38)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(337, 442)
        Me.Panel3.TabIndex = 3
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Red
        Me.Label20.Location = New System.Drawing.Point(3, 38)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(14, 14)
        Me.Label20.TabIndex = 49
        Me.Label20.Text = "*"
        '
        'btnAbCollapse_ExpandAll
        '
        Me.btnAbCollapse_ExpandAll.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.btnAbCollapse_ExpandAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAbCollapse_ExpandAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnAbCollapse_ExpandAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnAbCollapse_ExpandAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnAbCollapse_ExpandAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAbCollapse_ExpandAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAbCollapse_ExpandAll.Location = New System.Drawing.Point(233, 412)
        Me.btnAbCollapse_ExpandAll.Name = "btnAbCollapse_ExpandAll"
        Me.btnAbCollapse_ExpandAll.Size = New System.Drawing.Size(92, 23)
        Me.btnAbCollapse_ExpandAll.TabIndex = 4
        Me.btnAbCollapse_ExpandAll.Text = "Collapse All"
        Me.btnAbCollapse_ExpandAll.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 77)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 14)
        Me.Label1.TabIndex = 48
        Me.Label1.Text = "Groups\Users :"
        '
        'cmbAbnormalSendTask
        '
        Me.cmbAbnormalSendTask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAbnormalSendTask.Location = New System.Drawing.Point(108, 38)
        Me.cmbAbnormalSendTask.Name = "cmbAbnormalSendTask"
        Me.cmbAbnormalSendTask.Size = New System.Drawing.Size(218, 22)
        Me.cmbAbnormalSendTask.TabIndex = 1
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(14, 42)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(95, 14)
        Me.Label21.TabIndex = 41
        Me.Label21.Text = "Generate Task :"
        '
        'btnABSelect_UnselectAll
        '
        Me.btnABSelect_UnselectAll.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.btnABSelect_UnselectAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnABSelect_UnselectAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnABSelect_UnselectAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnABSelect_UnselectAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnABSelect_UnselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnABSelect_UnselectAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnABSelect_UnselectAll.Location = New System.Drawing.Point(11, 412)
        Me.btnABSelect_UnselectAll.Name = "btnABSelect_UnselectAll"
        Me.btnABSelect_UnselectAll.Size = New System.Drawing.Size(75, 23)
        Me.btnABSelect_UnselectAll.TabIndex = 3
        Me.btnABSelect_UnselectAll.Text = "Select All"
        Me.btnABSelect_UnselectAll.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.Label22)
        Me.Panel5.Controls.Add(Me.Label23)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(1, 1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(335, 26)
        Me.Panel5.TabIndex = 46
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(6, 6)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(115, 14)
        Me.Label22.TabIndex = 23
        Me.Label22.Text = "Abnormal Results"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(0, 25)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(335, 1)
        Me.Label23.TabIndex = 40
        Me.Label23.Text = "label1"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(1, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(335, 1)
        Me.Label24.TabIndex = 45
        Me.Label24.Text = "label1"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(1, 441)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(335, 1)
        Me.Label25.TabIndex = 44
        Me.Label25.Text = "label1"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel6.Controls.Add(Me.Label26)
        Me.Panel6.Controls.Add(Me.trvAbnormalGroups)
        Me.Panel6.Controls.Add(Me.Label27)
        Me.Panel6.Controls.Add(Me.Label28)
        Me.Panel6.Controls.Add(Me.Label29)
        Me.Panel6.Controls.Add(Me.Label33)
        Me.Panel6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel6.Location = New System.Drawing.Point(11, 97)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(315, 309)
        Me.Panel6.TabIndex = 2
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(1, 308)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(313, 1)
        Me.Label26.TabIndex = 39
        Me.Label26.Text = "label1"
        '
        'trvAbnormalGroups
        '
        Me.trvAbnormalGroups.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvAbnormalGroups.CheckBoxes = True
        Me.trvAbnormalGroups.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvAbnormalGroups.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.trvAbnormalGroups.ImageIndex = 0
        Me.trvAbnormalGroups.ImageList = Me.ImageList1
        Me.trvAbnormalGroups.Indent = 20
        Me.trvAbnormalGroups.ItemHeight = 20
        Me.trvAbnormalGroups.Location = New System.Drawing.Point(1, 6)
        Me.trvAbnormalGroups.Name = "trvAbnormalGroups"
        Me.trvAbnormalGroups.SelectedImageIndex = 0
        Me.trvAbnormalGroups.Size = New System.Drawing.Size(313, 303)
        Me.trvAbnormalGroups.TabIndex = 2
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.White
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label27.Location = New System.Drawing.Point(1, 1)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(313, 5)
        Me.Label27.TabIndex = 38
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(1, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(313, 1)
        Me.Label28.TabIndex = 5
        Me.Label28.Text = "label1"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(0, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 309)
        Me.Label29.TabIndex = 40
        Me.Label29.Text = "label4"
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(314, 0)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(1, 309)
        Me.Label33.TabIndex = 41
        Me.Label33.Text = "label4"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(0, 0)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(1, 442)
        Me.Label34.TabIndex = 42
        Me.Label34.Text = "label4"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(336, 0)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(1, 442)
        Me.Label35.TabIndex = 43
        Me.Label35.Text = "label4"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label56)
        Me.Panel1.Controls.Add(Me.btnCollapse_ExpandAll)
        Me.Panel1.Controls.Add(Me.Label32)
        Me.Panel1.Controls.Add(Me.cmbNormalSendTask)
        Me.Panel1.Controls.Add(Me.Label31)
        Me.Panel1.Controls.Add(Me.btnSelect_UnselectAll)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Location = New System.Drawing.Point(15, 38)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(337, 442)
        Me.Panel1.TabIndex = 2
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.BackColor = System.Drawing.Color.Transparent
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.ForeColor = System.Drawing.Color.Red
        Me.Label56.Location = New System.Drawing.Point(3, 38)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(14, 14)
        Me.Label56.TabIndex = 49
        Me.Label56.Text = "*"
        '
        'btnCollapse_ExpandAll
        '
        Me.btnCollapse_ExpandAll.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.btnCollapse_ExpandAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCollapse_ExpandAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnCollapse_ExpandAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnCollapse_ExpandAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnCollapse_ExpandAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCollapse_ExpandAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCollapse_ExpandAll.Location = New System.Drawing.Point(233, 412)
        Me.btnCollapse_ExpandAll.Name = "btnCollapse_ExpandAll"
        Me.btnCollapse_ExpandAll.Size = New System.Drawing.Size(92, 23)
        Me.btnCollapse_ExpandAll.TabIndex = 4
        Me.btnCollapse_ExpandAll.Text = "Collapse All"
        Me.btnCollapse_ExpandAll.UseVisualStyleBackColor = True
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(12, 77)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(97, 14)
        Me.Label32.TabIndex = 48
        Me.Label32.Text = "Groups\Users :"
        '
        'cmbNormalSendTask
        '
        Me.cmbNormalSendTask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNormalSendTask.FormattingEnabled = True
        Me.cmbNormalSendTask.Location = New System.Drawing.Point(107, 38)
        Me.cmbNormalSendTask.Name = "cmbNormalSendTask"
        Me.cmbNormalSendTask.Size = New System.Drawing.Size(219, 22)
        Me.cmbNormalSendTask.TabIndex = 1
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(14, 42)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(95, 14)
        Me.Label31.TabIndex = 41
        Me.Label31.Text = "Generate Task :"
        '
        'btnSelect_UnselectAll
        '
        Me.btnSelect_UnselectAll.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.btnSelect_UnselectAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSelect_UnselectAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnSelect_UnselectAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSelect_UnselectAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSelect_UnselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelect_UnselectAll.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSelect_UnselectAll.Location = New System.Drawing.Point(11, 412)
        Me.btnSelect_UnselectAll.Name = "btnSelect_UnselectAll"
        Me.btnSelect_UnselectAll.Size = New System.Drawing.Size(75, 23)
        Me.btnSelect_UnselectAll.TabIndex = 3
        Me.btnSelect_UnselectAll.Text = "Select All"
        Me.btnSelect_UnselectAll.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = Global.gloEMRAdmin.My.Resources.Resources.Img_Button
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.Label30)
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(1, 1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(335, 26)
        Me.Panel2.TabIndex = 46
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(6, 6)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(98, 14)
        Me.Label30.TabIndex = 23
        Me.Label30.Text = "Normal Results"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(0, 25)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(335, 1)
        Me.Label19.TabIndex = 40
        Me.Label19.Text = "label1"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(1, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(335, 1)
        Me.Label18.TabIndex = 45
        Me.Label18.Text = "label1"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(1, 441)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(335, 1)
        Me.Label17.TabIndex = 44
        Me.Label17.Text = "label1"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Controls.Add(Me.trvNormalGroups)
        Me.Panel4.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.Panel4.Controls.Add(Me.Label16)
        Me.Panel4.Controls.Add(Me.Label12)
        Me.Panel4.Controls.Add(Me.Label13)
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.Location = New System.Drawing.Point(11, 97)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(315, 309)
        Me.Panel4.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(1, 308)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(313, 1)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "label1"
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(1, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(313, 5)
        Me.lbl_WhiteSpaceTop.TabIndex = 38
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(1, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(313, 1)
        Me.Label16.TabIndex = 5
        Me.Label16.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(0, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 309)
        Me.Label12.TabIndex = 40
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(314, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 309)
        Me.Label13.TabIndex = 41
        Me.Label13.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 442)
        Me.Label14.TabIndex = 42
        Me.Label14.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(336, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 442)
        Me.Label15.TabIndex = 43
        Me.Label15.Text = "label4"
        '
        'lblProviderName
        '
        Me.lblProviderName.AutoSize = True
        Me.lblProviderName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProviderName.Location = New System.Drawing.Point(266, 10)
        Me.lblProviderName.Name = "lblProviderName"
        Me.lblProviderName.Size = New System.Drawing.Size(0, 14)
        Me.lblProviderName.TabIndex = 21
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(4, 488)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(701, 1)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "label2"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 488)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(705, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 488)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "label3"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(250, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Showing Task Rights for the Provider : "
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(3, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(703, 1)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = "label1"
        '
        'frmLabUserTask
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(709, 745)
        Me.Controls.Add(Me.pnlGroupsUsers)
        Me.Controls.Add(Me.pnlWindowsGroupsUsers)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLabUserTask"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lab User Task"
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.pnlWindowsGroupsUsers.ResumeLayout(False)
        CType(Me.C1ProviderDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlGroupsUsers.ResumeLayout(False)
        Me.pnlGroupsUsers.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
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
        With trvNormalGroups
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
                trvNormalGroups.Nodes.Add(trvParentNodes)
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
                    trvNormalGroups.Nodes(nCount).Nodes.Add(trvChildNode)
                    trvChildNode = Nothing
                Next
                dtChild = Nothing
                trvParentNodes = Nothing
            Next
            trvNormalGroups.ExpandAll()
            dtRights = Nothing
            objRights = Nothing
            Dim i, j As Int16
            Dim blnSelectedAll As Boolean = True
            For i = 0 To trvNormalGroups.GetNodeCount(False) - 1
                For j = 0 To trvNormalGroups.Nodes(i).GetNodeCount(False) - 1
                    If trvNormalGroups.Nodes(i).Nodes(j).Checked = False Then
                        blnSelectedAll = False
                        Exit Sub
                    End If
                Next
            Next
            If blnSelectedAll = False Then
                ' '' ''btnSelectAll.Text = "Clear All"
            Else
                '  btnSelectAll.Text = "Select All"
            End If
        End With
    End Sub

    Private Sub frmLabUserTask_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            If Not IsNothing(PreviousDt) Then
                PreviousDt.Dispose()
                PreviousDt = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmLabUserTask_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If bSavenClose = False Then
            PreviousDt = Get_LabTaskRights(nProviderID)
            If Validate_DataChanged(PreviousDt, dtSaveData()) = False Then
                Dim oResult As DialogResult = MessageBox.Show("Do you want to save Lab task rights?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If oResult = Windows.Forms.DialogResult.Yes Then
                    If SaveData(nProviderID) = False Then
                        e.Cancel = True
                    End If
                ElseIf oResult = Windows.Forms.DialogResult.Cancel Then
                    e.Cancel = True
                End If

            End If
        End If

    End Sub

    Private Sub frmLabUserTask_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            dtUser = FillUser()
            Fill_Providers()
            fill_Genratetask()

            Load_tree()
            '20-Mar-14 Chetan: Resolving resolution issues made change for bugid 65030
            Dim myScreenWidth As Integer = System.Windows.SystemParameters.PrimaryScreenWidth
            Dim myScreenHeight As Integer = System.Windows.SystemParameters.PrimaryScreenHeight
            If Me.Width > myScreenWidth Or Me.Height > myScreenHeight Then
                Me.MaximumSize = New System.Drawing.Size(myScreenWidth, (myScreenHeight - 50))
                Me.AutoScroll = True
            End If
            ' Dim Sendtasks As enumSendTask
            'For Each Sendtasks In [Enum].GetValues(GetType(enumSendTask))

            'cmbNormalSendTask.DataSource = System.Enum.GetValues(GetType(enumSendTask))

            'oNode.Text = Replace(CCType.ToString(), "_", " ")
            'oNode.Tag = Replace(CCType.ToString(), "_", " ")
            'trvClinicalChart.Nodes.Add(oNode)
            'Next

            If C1ProviderDetails.RowSel > 0 Then
                C1ProviderDetails_Click(sender, e)
            End If
            Me.Cursor = Cursors.Default
        Catch objErr As Exception
            '  Me.Cursor = Cursors.Default
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Function FillUser() As DataTable
        Dim dt As DataTable = Nothing
        Dim oclsgloIntuit As clsgloIntuit = Nothing
        Try
            oclsgloIntuit = New clsgloIntuit()
            dt = oclsgloIntuit.GetUsers()
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return dt
        Finally
            If Not IsNothing(oclsgloIntuit) Then
                oclsgloIntuit.Dispose()
                oclsgloIntuit = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    Public Function FillMergeUsers() As DataTable
        Dim dt As DataTable = Nothing
        Dim objCon As SqlConnection = Nothing
        Dim objDA As SqlDataAdapter
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillMergeUsers"

            objCmd.Connection = objCon
            objCon.Open()

            objDA = New SqlDataAdapter(objCmd)
            dt = New DataTable
            objDA.Fill(dt)

            objCon.Close()


            If Not IsNothing(dt) Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objDA) Then
                objDA.Dispose()
                objDA = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    Private Sub fill_Genratetask()
        Dim TempDt As New DataTable()

        Dim colID As New DataColumn("nID")
        colID.DataType = GetType(Integer)
        Dim colDescription As New DataColumn("sDescription")
        colDescription.DataType = GetType(String)

        TempDt.Columns.Add(colID)
        TempDt.Columns.Add(colDescription)

        Dim Sendtasks As enumSendTask
        Dim i As Int16 = 0
        Dim dr As DataRow
        'dr = TempDt.NewRow()
        'dr.Item("nID") = i
        'dr.Item("sDescription") = ""
        'TempDt.Rows.Add(dr)
        For Each Sendtasks In [Enum].GetValues(GetType(enumSendTask))
            dr = TempDt.NewRow()
            i = i + 1
            dr.Item("nID") = i
            dr.Item("sDescription") = Replace(Sendtasks.ToString(), "_", " ")
            TempDt.Rows.Add(dr)

        Next

        cmbNormalSendTask.DisplayMember = "sDescription"
        cmbNormalSendTask.ValueMember = "nID"
        cmbNormalSendTask.DataSource = TempDt


        cmbAbnormalSendTask.DisplayMember = "sDescription"
        cmbAbnormalSendTask.ValueMember = "nID"
        cmbAbnormalSendTask.DataSource = New DataView(TempDt)

    End Sub
    Private Sub Fill_Providers()
        Dim dtProvider As DataTable = Nothing
        Try
            gloC1FlexStyle.Style(C1ProviderDetails)
            With C1ProviderDetails
                .Cols.Count = 5
                .Rows.Count = 1
                .Rows.Fixed = 1
                .Cols.Fixed = 0


                .Cols(COL_PROVIDERID).Width = 0
                .Cols(COL_PROVIDERNAME).Width = 300
                .Cols(COL_GENDER).Width = 0

                .Cols(COL_Users).Width = 300
                .Cols(COL_Users).ComboList = " |"

                .Cols(COL_ImageUsers).Width = 30


                .SetData(0, COL_PROVIDERID, "ProviderId")
                .SetData(0, COL_PROVIDERNAME, "Provider Name")
                .SetData(0, COL_GENDER, "Gender")
                .SetData(0, COL_Users, "Unmatched Results Task Users")



                .Cols(COL_PROVIDERNAME).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(COL_GENDER).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(COL_PROVIDERNAME).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                .Cols(COL_GENDER).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

                .Cols(COL_PROVIDERID).AllowEditing = False
                .Cols(COL_PROVIDERNAME).AllowEditing = False
                .Cols(COL_GENDER).AllowEditing = False

                .Cols(COL_PROVIDERID).Visible = False
                .Cols(COL_GENDER).Visible = False
            End With


            dtProvider = Get_ProviderList()

            Dim nCount As Int16
            If dtProvider.Rows.Count > 0 Then

                For nCount = 0 To dtProvider.Rows.Count - 1
                    With C1ProviderDetails
                        .Rows.Add()
                        .SetData(nCount + 1, COL_PROVIDERID, dtProvider.Rows(nCount)("nProviderID").ToString().Trim)
                        .SetData(nCount + 1, COL_PROVIDERNAME, dtProvider.Rows(nCount)("ProviderName").ToString().Trim)
                        .SetData(nCount + 1, COL_GENDER, dtProvider.Rows(nCount)("sGender").ToString().Trim)
                        '.Cols(COL_Users).ComboList = " |" & dtProvider.Rows(nCount)("UserList").ToString().Trim

                        If dtProvider.Rows(nCount)("UserList").ToString().Trim <> "" Then
                            Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                            Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
                            cstyle = C1ProviderDetails.Styles.Add("BubbleValues" & nCount + 1)
                            cstyle.ComboList = dtProvider.Rows(nCount)("UserList").ToString().Trim
                            C1ProviderDetails.SetData(nCount + 1, 3, "")
                            ocell = C1ProviderDetails.GetCellRange(nCount + 1, 3, nCount + 1, 3)
                            ocell.Style = cstyle
                            Dim splstruser As String() = dtProvider.Rows(nCount)("UserList").ToString().Trim.Split("|")
                            If splstruser.Length > 0 Then
                                ocell.Data = splstruser(0)
                            End If
                            splstruser = Nothing
                            'C1ProviderDetails.[Select](nCount + 1, 3)
                        End If



                        .SetCellImage(nCount + 1, COL_ImageUsers, ImageList1.Images(5))

                    End With
                Next
            Else
                Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
                cstyle = C1ProviderDetails.Styles.Add("BubbleValues" & nCount + 1)
                cstyle.ComboList = " "
                C1ProviderDetails.SetData(nCount + 1, 3, "")
                'C1ProviderDetails.[Select](nCount + 1, 3)
                ocell = C1ProviderDetails.GetCellRange(nCount + 1, 3, nCount + 1, 3)
                ocell.Style = cstyle

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(dtProvider) Then
                dtProvider.Dispose()
                dtProvider = Nothing
            End If
        End Try

    End Sub

    Public Function Get_ProviderList() As DataTable
        Dim dt As DataTable = Nothing
        Dim objCon As SqlConnection = Nothing
        Dim objDA As SqlDataAdapter
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCmd = New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetProviderLabUserMergeTask"

            objCmd.Connection = objCon
            objCon.Open()

            objDA = New SqlDataAdapter(objCmd)
            dt = New DataTable
            objDA.Fill(dt)

            objCon.Close()


            If Not IsNothing(dt) Then
                Return dt
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
            Return Nothing
        Finally
            If objCmd.Connection.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objDA) Then
                objDA.Dispose()
                objDA = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try

    End Function
    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Try
            SaveData(nProviderID)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub btnSaveNclose_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveNclose.Click
        Try
            If SaveData(nProviderID) = True Then
                bSavenClose = True
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub SaveUserList()
        Dim ostyle As C1.Win.C1FlexGrid.CellStyle
        Dim dtprv As New DataTable
        Dim strUserList As String = ""
        Dim blockedUsers As String = ""
        With C1ProviderDetails
            Dim nProviderID As New DataColumn("nProviderID")
            Dim nUserID As New DataColumn("nUserID")

            dtprv.Columns.Add(nProviderID)
            dtprv.Columns.Add(nUserID)

            For _row As Integer = 1 To C1ProviderDetails.Rows.Count - 1




                strUserList = ""
                If Not IsNothing(C1ProviderDetails.GetCellStyle(_row, 3)) Then
                    ostyle = C1ProviderDetails.GetCellStyle(_row, 3)
                    strUserList = ostyle.ComboList
                Else
                    If Not IsNothing(C1ProviderDetails.GetData(_row, 3)) Then
                        If C1ProviderDetails.GetData(_row, 3) <> "" Then
                            strUserList = C1ProviderDetails.GetData(_row, 3)
                        End If
                    End If
                End If
                ''
                If Trim(strUserList) <> "" Then
                    Dim splstruser As String() = strUserList.Split("|")

                    For i As Int16 = 0 To splstruser.Length - 1
                        Dim drTemp As DataRow = dtprv.NewRow()

                        Dim _dr() As DataRow
                        _dr = dtUser.[Select]("sLoginName = '" & Convert.ToString(splstruser(i).ToString()).Replace("'", "''") & "'")
                        If Not IsNothing(_dr) And _dr.Length > 0 Then
                            drTemp("nUserID") = Convert.ToInt64(_dr(0).Item("nUserID"))
                        End If

                        drTemp("nProviderID") = Convert.ToString(C1ProviderDetails.GetData(_row, COL_PROVIDERID))

                        ''Bug #91349: Problem : #00001029: Error when trying to modify Admin > Lab User Task for unmatched results
                        If IsDBNull(drTemp("nUserID")) Then
                            If blockedUsers.Length = 0 Then
                                blockedUsers = splstruser(i)
                            Else
                                blockedUsers = blockedUsers & " , " & splstruser(i)
                            End If
                        Else
                            dtprv.Rows.Add(drTemp)
                        End If
                        _dr = Nothing
                    Next
                End If
                'End If
            Next
        End With

        ''Bug #91349: Problem : #00001029: Error when trying to modify Admin > Lab User Task for unmatched results
        isUserBlocked = False
        If blockedUsers.Length > 0 Then
            MessageBox.Show(" Following user(s) are blocked. '" & vbNewLine & blockedUsers & vbNewLine & "' Please select different user(s).", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            isUserBlocked = True
        Else
            If Not IsNothing(dtprv) Then
                Insert_LabUserMergeTask(dtprv)
            End If
        End If
    End Sub
    Private Function SaveData(ByVal _nProviderID As Long) As Boolean
        Dim gDtData As DataTable
        Try
            If cmbNormalSendTask.Text = "" Then
                MessageBox.Show("Select Generate Task from Normal Results group.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbNormalSendTask.Focus()
                Return False
            End If

            If cmbAbnormalSendTask.Text = "" Then
                MessageBox.Show("Select Generate Task from Abnormal Results group.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbAbnormalSendTask.Focus()
                Return False
            End If

            If cmbNormalSendTask.SelectedValue.ToString() = "1" And cmbAbnormalSendTask.SelectedValue.ToString() = "2" Then
                Dim oResult As DialogResult = MessageBox.Show("Are you sure that you want to send Normal results sooner than Abnormal results?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                If oResult = Windows.Forms.DialogResult.No Then
                    cmbAbnormalSendTask.Focus()
                    Return False
                End If
            End If

            gDtData = dtSaveData()
            'Dim dv As DataView
            'dv = New DataView(gDtData, "nTaskTypeID = 1", "nTaskTypeID", DataViewRowState.CurrentRows)
            'If dv.Count = 0 Then
            '    MessageBox.Show("Select at least one user from Normal Results group.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Return False
            'End If

            'dv = New DataView(gDtData, "nTaskTypeID = 2", "nTaskTypeID", DataViewRowState.CurrentRows)
            'If dv.Count = 0 Then
            '    MessageBox.Show("Select at least one user from Abnormal Results group.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Return False
            'End If
            Insert_LabTaskRights(_nProviderID, cmbNormalSendTask.SelectedValue, cmbAbnormalSendTask.SelectedValue, gDtData)

            If IsNothing(PreviousDt) Or (PreviousDt.Rows.Count = 0) Then
                PreviousDt = Get_LabTaskRights(C1ProviderDetails.GetData(C1ProviderDetails.RowSel, COL_PROVIDERID))
            End If
            SaveUserList()
            If isUserBlocked Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(gDtData) Then
                gDtData.Dispose()
                gDtData = Nothing
            End If
        End Try

    End Function


    Private Sub SelectAll(ByVal Treeviewcntl As TreeView)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim nCount As Int16
            Dim nTotalNodes As Int16
            nTotalNodes = Treeviewcntl.GetNodeCount(False) - 1
            For nCount = 0 To nTotalNodes
                Treeviewcntl.Nodes(nCount).Checked = True
            Next
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearAll(ByVal Treeviewcntl As TreeView)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim nCount As Int16
            Dim nTotalNodes As Int16
            nTotalNodes = Treeviewcntl.GetNodeCount(False) - 1
            For nCount = 0 To nTotalNodes
                Treeviewcntl.Nodes(nCount).Checked = False
            Next
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Protected Function PDataset(select_statement As String) As DataSet
        Dim ds As New DataSet()
        Try
            Dim _databaseconnectionstring As String = gloEMRAdmin.mdlGeneral.GetConnectionString
            Dim _con As New SqlConnection(_databaseconnectionstring)
            Dim ad As New SqlDataAdapter(select_statement, _con)
            ad.Fill(ds)
            _con.Close()
            Return ds
        Catch ex As Exception
            Return Nothing
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Function


    Public Sub Load_tree()
        Dim strSql = "SELECT  nGroupID ,sGroupName  FROM    SEC_UserGroups_MST"

        Dim PrSet As DataSet = PDataset(strSql)
        Try
            trvNormalGroups.Nodes.Clear()
            trvAbnormalGroups.Nodes.Clear()
            Dim tnParent As TreeNode
            Dim tnAbparent As TreeNode
            For Each dr As DataRow In PrSet.Tables(0).Rows

                tnParent = New TreeNode
                tnParent.Text = dr("sGroupName").ToString()
                tnParent.Tag = dr("nGroupID").ToString()
                tnAbparent = tnParent.Clone()

                trvNormalGroups.Nodes.Add(tnParent)
                trvAbnormalGroups.Nodes.Add(tnAbparent)

                FillChild(tnParent, tnAbparent, dr("nGroupID").ToString())


            Next
            tnParent = New TreeNode
            tnParent.Text = "Other"
            tnParent.Tag = "0"
            tnAbparent = tnParent.Clone()

            trvNormalGroups.Nodes.Add(tnParent)
            trvAbnormalGroups.Nodes.Add(tnAbparent)

            FillChild(tnParent, tnAbparent, "0")

            trvNormalGroups.ExpandAll()
            trvAbnormalGroups.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(PrSet) Then
                PrSet.Dispose()
                PrSet = Nothing
            End If
        End Try
    End Sub

    Public Function FillChild(parent As TreeNode, AbParent As TreeNode, GID As String) As Integer
        Dim ds As DataSet
        Try

            Dim strsql = "SELECT    User_MST.nUserID, User_MST.sLoginName " & _
                "  FROM     SEC_UserGroups_MST INNER JOIN  SEC_UserGroups_DTL ON SEC_UserGroups_MST.nGroupID = SEC_UserGroups_DTL.nGroupID RIGHT OUTER JOIN " & _
                "  User_MST ON SEC_UserGroups_DTL.nUserID =User_MST.nUserID   where User_MST.nBlockstatus = 0 And ISNULL(SEC_UserGroups_MST.nGroupID,0)  = '" & GID & "' " & _
                 " ORDER BY sLoginName"

            ds = PDataset(strsql)
            If ds.Tables(0).Rows.Count > 0 Then

                For Each dr As DataRow In ds.Tables(0).Rows
                    Dim child As New TreeNode()
                    child.Text = dr("sLoginName").ToString().Trim()
                    child.Tag = dr("nUserID").ToString()
                    child.ImageIndex = 1
                    child.SelectedImageIndex = 1
                    child.Collapse()
                    parent.Nodes.Add(child)
                    AbParent.Nodes.Add(child.Clone())

                Next
                Return 1
            Else
                Return 0
            End If
        Catch ex As Exception
            Return Nothing
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Function

    Private Sub CheckAllChildren(ByVal tn As TreeNode, ByVal bCheck As [Boolean], ByVal sflag As String)
        If sflag = "trvNormal" Then

            bParentTrigger = False
            For Each ctn As TreeNode In tn.Nodes
                bChildTrigger = False
                ctn.Checked = bCheck
                bChildTrigger = True

                CheckAllChildren(ctn, bCheck, sflag)
            Next
            bParentTrigger = True
        Else
            bABParentTrigger = False
            For Each ctn As TreeNode In tn.Nodes
                bABChildTrigger = False
                ctn.Checked = bCheck
                bABChildTrigger = True

                CheckAllChildren(ctn, bCheck, "trvAbNormal")
            Next
            bABParentTrigger = True
        End If
    End Sub

    Private Sub CheckMyParent(ByVal tn As TreeNode, ByVal bCheck As [Boolean], ByVal sflag As String)
        If sflag = "trvNormal" Then


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

            CheckMyParent(tn.Parent, bCheck, sflag)
            bParentTrigger = True
            bChildTrigger = True
        Else

            If tn Is Nothing Then
                Exit Sub
            End If
            If tn.Parent Is Nothing Then
                Exit Sub
            End If

            bABChildTrigger = False
            bABParentTrigger = False

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

            CheckMyParent(tn.Parent, bCheck, "trvAbNormal")
            bABParentTrigger = True
            bABChildTrigger = True
        End If
    End Sub

    Private Sub trvNormalGroups_AfterCheck(sender As System.Object, e As System.Windows.Forms.TreeViewEventArgs) Handles trvNormalGroups.AfterCheck
        If bChildTrigger Then
            CheckAllChildren(e.Node, e.Node.Checked, "trvNormal")
        End If
        If bParentTrigger Then
            CheckMyParent(e.Node, e.Node.Checked, "trvNormal")
        End If
    End Sub

    Private Sub trvAbnormalGroups_AfterCheck(sender As System.Object, e As System.Windows.Forms.TreeViewEventArgs) Handles trvAbnormalGroups.AfterCheck
        If bABChildTrigger Then
            CheckAllChildren(e.Node, e.Node.Checked, "trvAbNormal")
        End If
        If bABParentTrigger Then
            CheckMyParent(e.Node, e.Node.Checked, "trvAbNormal")
        End If
    End Sub

    Private Function dtSaveData() As DataTable
        Dim TempDt As New DataTable()
        Try
            Dim colNormalSendTaskID As New DataColumn("nNormalSendTaskID")
            colNormalSendTaskID.DataType = GetType(Integer)
            Dim colAbnormalSendTaskID As New DataColumn("nAbnormalSendTaskID")
            colAbnormalSendTaskID.DataType = GetType(Integer)
            Dim colGroupID As New DataColumn("nGroupID")
            colGroupID.DataType = GetType(Long)
            Dim colUserID As New DataColumn("nUserID")
            colUserID.DataType = GetType(Long)
            Dim colTypeID As New DataColumn("nTaskTypeID")
            colTypeID.DataType = GetType(Integer)

            TempDt.Columns.Add(colNormalSendTaskID)
            TempDt.Columns.Add(colAbnormalSendTaskID)
            TempDt.Columns.Add(colGroupID)
            TempDt.Columns.Add(colUserID)
            TempDt.Columns.Add(colTypeID)

            Dim node As TreeNode
            If trvNormalGroups.Nodes.Count > 0 Then
                For Each node In trvNormalGroups.Nodes
                    Dim Childnode As TreeNode = Nothing
                    For Each Childnode In node.Nodes
                        If Childnode.Checked = True Then
                            Dim dr As DataRow = TempDt.NewRow()
                            dr.Item("nNormalSendTaskID") = IIf(IsNothing(cmbNormalSendTask.SelectedValue), 1, cmbNormalSendTask.SelectedValue)
                            dr.Item("nAbnormalSendTaskID") = IIf(IsNothing(cmbAbnormalSendTask.SelectedValue), 1, cmbAbnormalSendTask.SelectedValue)
                            dr.Item("nGroupID") = node.Tag
                            dr.Item("nUserID") = Childnode.Tag
                            dr.Item("nTaskTypeID") = 1
                            TempDt.Rows.Add(dr)
                        End If
                    Next
                Next
            End If
            node = Nothing

            'Abnormal result tree
            Dim Abnode As TreeNode
            If trvAbnormalGroups.Nodes.Count > 0 Then
                For Each Abnode In trvAbnormalGroups.Nodes
                    Dim Abchildnode As TreeNode = Nothing
                    For Each Abchildnode In Abnode.Nodes
                        If Abchildnode.Checked = True Then
                            Dim dr As DataRow = TempDt.NewRow()
                            dr.Item("nNormalSendTaskID") = IIf(IsNothing(cmbNormalSendTask.SelectedValue), 1, cmbNormalSendTask.SelectedValue)
                            dr.Item("nAbnormalSendTaskID") = IIf(IsNothing(cmbAbnormalSendTask.SelectedValue), 1, cmbAbnormalSendTask.SelectedValue)
                            dr.Item("nGroupID") = Abnode.Tag
                            dr.Item("nUserID") = Abchildnode.Tag
                            dr.Item("nTaskTypeID") = 2
                            TempDt.Rows.Add(dr)
                        End If
                    Next
                Next
            End If

            If TempDt.Rows.Count = 0 Then
                If IsNothing(cmbNormalSendTask.SelectedValue) = False And IsNothing(cmbAbnormalSendTask.SelectedValue) = False Then
                    ' If Not (cmbNormalSendTask.SelectedValue = 1 And cmbAbnormalSendTask.SelectedValue = 1) Then
                    Dim dr As DataRow = TempDt.NewRow()
                    dr.Item("nNormalSendTaskID") = IIf(IsNothing(cmbNormalSendTask.SelectedValue), 1, cmbNormalSendTask.SelectedValue)
                    dr.Item("nAbnormalSendTaskID") = IIf(IsNothing(cmbAbnormalSendTask.SelectedValue), 1, cmbAbnormalSendTask.SelectedValue)
                    dr.Item("nGroupID") = 0
                    dr.Item("nUserID") = 0
                    dr.Item("nTaskTypeID") = 0
                    TempDt.Rows.Add(dr)
                    'End If
                End If
            End If

            Return TempDt
        Catch objErr As Exception
            MessageBox.Show(objErr.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(TempDt) Then
                TempDt.Dispose()
                TempDt = Nothing
            End If
        End Try
    End Function

    Private Sub C1ProviderDetails_Click(sender As System.Object, e As System.EventArgs) Handles C1ProviderDetails.Click
        Dim Dt As DataTable
        Try

            nSelectedRow = C1ProviderDetails.RowSel
            If C1ProviderDetails.RowSel > 0 Then

                ' Dt = Get_LabTaskRights(C1ProviderDetails.GetData(C1ProviderDetails.RowSel, COL_PROVIDERID))
                If IsNothing(PreviousDt) Then
                    nProviderID = C1ProviderDetails.GetData(C1ProviderDetails.RowSel, COL_PROVIDERID)
                    Dt = Get_LabTaskRights(C1ProviderDetails.GetData(C1ProviderDetails.RowSel, COL_PROVIDERID))
                    PreviousDt = Dt
                Else
                    '' Dt = Get_LabTaskRights(nProviderID)
                    PreviousDt = Get_LabTaskRights(nProviderID)

                    If Validate_DataChanged(PreviousDt, dtSaveData()) = False Then
                        Dim oResult As DialogResult = MessageBox.Show("Do you want to save Lab task rights?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                        If oResult = Windows.Forms.DialogResult.Yes Then
                            If SaveData(nProviderID) = False Then
                                C1ProviderDetails.Row = CType(C1ProviderDetails.FindRow(CType(nProviderID, String), 0, 0, True), Integer)
                                Exit Sub
                            End If
                        ElseIf oResult = Windows.Forms.DialogResult.Cancel Then
                            C1ProviderDetails.Row = CType(C1ProviderDetails.FindRow(CType(nProviderID, String), 0, 0, True), Integer)
                            Exit Sub
                        End If

                    End If

                End If

                nProviderID = C1ProviderDetails.GetData(C1ProviderDetails.RowSel, COL_PROVIDERID)
                Dt = Get_LabTaskRights(nProviderID)

                lblProviderName.Text = C1ProviderDetails.GetData(C1ProviderDetails.RowSel, COL_PROVIDERNAME)

                cmbNormalSendTask.SelectedValue = 1
                cmbAbnormalSendTask.SelectedValue = 1
                ClearAll(trvNormalGroups)
                ClearAll(trvAbnormalGroups)
                If Dt.Rows.Count > 0 Then
                    Dim i As Integer
                    cmbNormalSendTask.SelectedValue = Dt.Rows(0).Item("nNormalSendTaskID")
                    cmbAbnormalSendTask.SelectedValue = Dt.Rows(0).Item("nAbnormalSendTaskID")

                    Dim node As TreeNode
                    For i = 0 To Dt.Rows.Count - 1
                        Select Case Dt.Rows(i).Item("nTaskTypeID").ToString()
                            Case "1"
                                For Each node In trvNormalGroups.Nodes
                                    Dim Childnode As TreeNode
                                    For Each Childnode In node.Nodes
                                        If Childnode.Tag.ToString() = Dt.Rows(i).Item("nUserID").ToString() And node.Tag.ToString() = Dt.Rows(i).Item("nGroupID").ToString() Then
                                            Childnode.Checked = True
                                            Exit For
                                        End If
                                    Next
                                Next

                            Case "2"
                                node = Nothing
                                For Each node In trvAbnormalGroups.Nodes
                                    Dim Childnode As TreeNode = Nothing
                                    For Each Childnode In node.Nodes
                                        If Childnode.Tag.ToString() = Dt.Rows(i).Item("nUserID").ToString() And node.Tag.ToString() = Dt.Rows(i).Item("nGroupID").ToString() Then
                                            Childnode.Checked = True
                                            Exit For
                                        End If
                                    Next
                                Next

                        End Select
                    Next

                End If

                Dim ostyle As C1.Win.C1FlexGrid.CellStyle

                Dim strUserList As String = ""
                With C1ProviderDetails
                    If C1ProviderDetails.RowSel >= 0 Then

                        If .ColSel = 4 Then

                            If Not IsNothing(C1ProviderDetails.GetCellStyle(nSelectedRow, 3)) Then
                                ostyle = C1ProviderDetails.GetCellStyle(nSelectedRow, 3)
                                strUserList = ostyle.ComboList
                            Else
                                If Not IsNothing(C1ProviderDetails.GetData(nSelectedRow, 3)) Then
                                    If C1ProviderDetails.GetData(nSelectedRow, 3) <> "" Then
                                        strUserList = C1ProviderDetails.GetData(nSelectedRow, 3)
                                    End If
                                End If
                            End If
                            ''
                            If Trim(strUserList) <> "" Then
                                Dim dtprv As New DataTable
                                Dim nUserID As New DataColumn("nUserID")
                                Dim Description As New DataColumn("Description")
                                dtprv.Columns.Add(nUserID)
                                dtprv.Columns.Add(Description)

                                Dim splstruser As String() = strUserList.Split("|")

                                For i As Int16 = 0 To splstruser.Length - 1
                                    Dim drTemp As DataRow = dtprv.NewRow()
                                    'drTemp("nUserID") = 216512308151873336

                                    Dim _dr() As DataRow
                                    _dr = dtUser.[Select]("sLoginName = '" & Convert.ToString(splstruser(i).ToString()).Replace("'", "''") & "'")
                                    If Not IsNothing(_dr) And _dr.Length > 0 Then
                                        drTemp("nUserID") = Convert.ToInt64(_dr(0).Item("nUserID"))
                                    End If
                                    _dr = Nothing
                                    drTemp("Description") = splstruser(i).ToString()
                                    dtprv.Rows.Add(drTemp)
                                Next

                                If dtprv.Rows.Count > 0 Then
                                    Dim ToItemp As gloGeneralItem.gloItem
                                    _ToList = New gloGeneralItem.gloItems
                                    For i As Int16 = 0 To dtprv.Rows.Count - 1
                                        ToItemp = New gloGeneralItem.gloItem()
                                        ''Bug #91349: Problem : #00001029: Error when trying to modify Admin > Lab User Task for unmatched results
                                        ''ToItemp.ID = dtprv.Rows(i)("nUserID")
                                        If Not IsDBNull(dtprv.Rows(i)("nUserID")) Then
                                            ToItemp.ID = dtprv.Rows(i)("nUserID")
                                        End If
                                        ToItemp.Description = dtprv.Rows(i)("Description")
                                        _ToList.Add(ToItemp)
                                        ToItemp = Nothing
                                    Next
                                End If
                            Else
                                If IsNothing(_ToList) = False Then
                                    _ToList.Clear()
                                End If
                            End If
                            ''
                            oListUsers = New gloListControl.gloListControl(gloEMRAdmin.mdlGeneral.GetConnectionString(), gloListControl.gloListControlType.Users, True, Me.Width)
                            oListUsers.ControlHeader = "Users"
                            AddHandler oListUsers.ItemSelectedClick, AddressOf olistUsers_ItemSelectedClick
                            AddHandler oListUsers.ItemClosedClick, AddressOf olistUsers_ItemClosedClick
                            'To Select already Added Users.
                            ' _ToList.Clear()
                            If IsNothing(_ToList) = False Then

                                For i As Integer = 0 To _ToList.Count - 1
                                    oListUsers.SelectedItems.Add(_ToList(i))
                                Next
                            End If
                            ofrmList.Controls.Add(oListUsers)
                            oListUsers.Dock = DockStyle.Fill
                            oListUsers.BringToFront()
                            oListUsers.ShowHeaderPanel(False)
                            oListUsers.OpenControl()
                            ofrmList.Text = "Users"
                            ofrmList.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                            ofrmList.ShowDialog()
                        End If
                    End If
                End With



            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(Dt) Then
                Dt.Dispose()
                Dt = Nothing
            End If
        End Try

    End Sub

    Private Sub olistUsers_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim dtUsers As New DataTable()
        Dim dcnIntuitStaffMappingDetailID As New DataColumn("nIntuitStaffMappingDetailID")
        Dim dcnIntuitStaffMappingID As New DataColumn("nIntuitStaffMappingID")
        Dim dcId As New DataColumn("nUserID")
        Dim dcDescription As New DataColumn("Description")
        dtUsers.Columns.Add(dcId)
        dtUsers.Columns.Add(dcDescription)
        _ToList = New gloGeneralItem.gloItems
        Dim ToItem As gloGeneralItem.gloItem

        If oListUsers.SelectedItems.Count > 0 Then
            For i As Int16 = 0 To oListUsers.SelectedItems.Count - 1
                Dim drTemp As DataRow = dtUsers.NewRow()
                drTemp("nUserID") = oListUsers.SelectedItems(i).ID
                drTemp("Description") = oListUsers.SelectedItems(i).Description

                ''Bug #91349: Problem : #00001029: Error when trying to modify Admin > Lab User Task for unmatched results
                If drTemp("nUserID") <> 0 Then
                    dtUsers.Rows.Add(drTemp)
                End If

                ToItem = New gloGeneralItem.gloItem()

                ToItem.ID = oListUsers.SelectedItems(i).ID
                ToItem.Description = oListUsers.SelectedItems(i).Description

                _ToList.Add(ToItem)
                '
                ToItem = Nothing
            Next
        End If
        RefreshUsers(dtUsers)
        ofrmList.Close()
    End Sub

    Public Sub RefreshUsers(ByVal dt As DataTable)
        Try
            Dim strUserList As String = ""
            Dim strUserID As String = ""

            For i As Int16 = 0 To dt.Rows.Count - 1
                If strUserList = "" Then
                    strUserList = dt.Rows(i)("Description")
                    strUserID = dt.Rows(i)("nUserID")
                Else
                    strUserList = strUserList + "|" + dt.Rows(i)("Description")
                    strUserID = strUserID + "|" + dt.Rows(i)("nUserID")
                End If
            Next

            If dt.Rows.Count > 0 Then
                Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
                cstyle = C1ProviderDetails.Styles.Add("BubbleValues" & nSelectedRow)
                cstyle.ComboList = strUserList
                C1ProviderDetails.SetData(nSelectedRow, 3, "")
                ocell = C1ProviderDetails.GetCellRange(nSelectedRow, 3, nSelectedRow, 3)
                ocell.Style = cstyle
                Dim splstruser As String() = strUserList.Split("|")
                If splstruser.Length > 0 Then
                    ocell.Data = splstruser(0)
                End If
                splstruser = Nothing
                C1ProviderDetails.[Select](nSelectedRow, 3)
            Else
                Dim cstyle As C1.Win.C1FlexGrid.CellStyle = Nothing
                Dim ocell As C1.Win.C1FlexGrid.CellRange = Nothing
                cstyle = C1ProviderDetails.Styles.Add("BubbleValues" & nSelectedRow)
                cstyle.ComboList = " "
                C1ProviderDetails.SetData(nSelectedRow, 3, "")
                C1ProviderDetails.[Select](nSelectedRow, 3)
                ocell = C1ProviderDetails.GetCellRange(nSelectedRow, 3, nSelectedRow, 3)
                ocell.Style = cstyle
            End If

            Dim ToItem As gloGeneralItem.gloItem
            _ToList = New gloGeneralItem.gloItems
            For i As Int16 = 0 To dt.Rows.Count - 1
                ToItem = New gloGeneralItem.gloItem()
                ToItem.ID = dt.Rows(i)("nUserID")
                ToItem.Description = dt.Rows(i)("Description")
                _ToList.Add(ToItem)
                ToItem = Nothing
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub olistUsers_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsNothing(ofrmList) Then
            ofrmList.Close()
        Else
            ofrmList = New frmUserList
            ofrmList.Close()
            ofrmList.Dispose()
            ofrmList = Nothing
        End If
    End Sub

    Private Sub Insert_LabTaskRights(ByVal nProviderID As Int64, ByVal nNormalSendTaskID As Int16, ByVal nAbnormalSendTaskID As Int16, ByVal dtLabtask As DataTable)
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Try
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_INUPLabUserTaskRights"
            Dim objPara_ProviderID As New SqlParameter
            With objPara_ProviderID
                .ParameterName = "@nProviderID"
                .Value = nProviderID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objPara_ProviderID)

            objPara_ProviderID = Nothing

            Dim objPara_NormalSendTaskID As New SqlParameter
            With objPara_NormalSendTaskID
                .ParameterName = "@nNormalSendTaskID"
                .Value = nNormalSendTaskID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.SmallInt
            End With
            objCmd.Parameters.Add(objPara_NormalSendTaskID)

            objPara_NormalSendTaskID = Nothing

            Dim objPara_AbnormalSendTaskID As New SqlParameter
            With objPara_AbnormalSendTaskID
                .ParameterName = "@nAbnormalSendTaskID"
                .Value = nAbnormalSendTaskID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.SmallInt
            End With
            objCmd.Parameters.Add(objPara_AbnormalSendTaskID)

            objPara_AbnormalSendTaskID = Nothing

            Dim objPara_LabUserRights As New SqlParameter
            With objPara_LabUserRights
                .ParameterName = "@TmpLabUserRights"
                .Value = dtLabtask
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Structured
            End With
            objCmd.Parameters.Add(objPara_LabUserRights)

            objPara_LabUserRights = Nothing

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()


        Catch ex As SqlException
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objCon.Close()
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If

        End Try
    End Sub

    Private Sub Insert_LabUserMergeTask(ByVal dtLabtask As DataTable)
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Try
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_SaveLabUserMergeTask"

            Dim objPara_LabUserRights As New SqlParameter
            With objPara_LabUserRights
                .ParameterName = "@TmpLabUserMerge"
                .Value = dtLabtask
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Structured
            End With
            objCmd.Parameters.Add(objPara_LabUserRights)

            objPara_LabUserRights = Nothing

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()


        Catch ex As SqlException
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objCon.Close()
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Dispose()
                objCmd = Nothing
            End If

        End Try
    End Sub

    Private Function Get_LabTaskRights(ByVal nProviderID As Long) As DataTable

        Dim oDataTable As DataTable
        Dim oDB As gloStream.gloDataBase.gloDataBase
        Try

            Dim _SQLQuery = "  SELECT   LabUserTask.nProviderID, LabUserTask.nNormalSendTaskID, LabUserTask.nAbnormalSendTaskID," & _
                        "  isnull(LabUserTaskDetail.nGroupID,0) as nGroupID , isnull(LabUserTaskDetail.nUserID,0) as nUserID, isnull(LabUserTaskDetail.nTaskTypeID,0) as nTaskTypeID " & _
                        "  FROM  LabUserTask LEFT OUTER JOIN   LabUserTaskDetail ON LabUserTask.nLabUserTaskID = LabUserTaskDetail.nLabUserTaskID AND  LabUserTask.nProviderID = LabUserTaskDetail.nProviderID  " & _
                        "  where  LabUserTask.nProviderID =" & nProviderID & "" & _
                        "  ORDER BY LabUserTask.nProviderID, LabUserTaskDetail.nTaskTypeID"

            oDB = New gloStream.gloDataBase.gloDataBase
            oDB.Connect(gloEMRAdmin.mdlGeneral.GetConnectionString)
            oDataTable = oDB.ReadQueryData(_SQLQuery)
            Return oDataTable
        Catch ex As Exception
            Return Nothing
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        If Not IsNothing(oDB) Then
            oDB = Nothing
        End If
        If Not IsNothing(oDataTable) Then
            oDataTable.Dispose()
            oDataTable = Nothing
        End If
    End Function

    Private Function Validate_DataChanged(ByVal sDt As DataTable, ByVal tDt As DataTable) As Boolean
        Dim sDv As DataView
        Dim tDv As DataView
        Dim sDTtemp As DataTable
        Dim tDTtemp As DataTable
        Try


            If sDt.Rows.Count > 0 Then

                If sDt.Rows.Count <> tDt.Rows.Count Then
                    Return False
                End If

                'If String.Compare(sDt.Rows(0).Item("nNormalSendTaskID").ToString(), cmbNormalSendTask.SelectedValue.ToString(), False) <> 0 Then
                '    Return False
                'End If

                'If String.Compare(sDt.Rows(0).Item("nAbnormalSendTaskID").ToString(), cmbAbnormalSendTask.SelectedValue.ToString(), False) <> 0 Then
                '    Return False
                'End If

                If String.Compare(sDt.Rows(0).Item("nNormalSendTaskID").ToString(), tDt.Rows(0).Item("nNormalSendTaskID").ToString(), False) <> 0 Then
                    Return False
                End If

                If String.Compare(sDt.Rows(0).Item("nAbnormalSendTaskID").ToString(), tDt.Rows(0).Item("nAbnormalSendTaskID").ToString(), False) <> 0 Then
                    Return False
                End If



                Dim i, j As Integer
                'sDv = New DataView(sDt, "nTaskTypeID <> 0", "nTaskTypeID,nUserID", DataViewRowState.CurrentRows)
                'tDv = New DataView(tDt, "nTaskTypeID <> 0", "nTaskTypeID,nUserID", DataViewRowState.CurrentRows)
                sDv = New DataView(sDt)
                tDv = New DataView(tDt)

                sDv.Sort = "nTaskTypeID,nUserID"
                tDv.Sort = "nTaskTypeID,nUserID"
                sDTtemp = sDv.ToTable()
                tDTtemp = tDv.ToTable()

                j = 0
                For i = 0 To tDTtemp.Rows.Count - 1

                    If String.Compare(tDTtemp.Rows(i).Item("nTaskTypeID"), sDTtemp.Rows(j).Item("nTaskTypeID")) = 0 Then
                        If String.Compare(tDTtemp.Rows(i).Item("nUserID").ToString(), sDTtemp.Rows(j).Item("nUserID").ToString()) <> 0 Then
                            Return False
                            Exit For
                        End If
                    Else
                        Return False
                        Exit For
                    End If
                    j = j + 1
                Next
            Else
                If tDt.Rows.Count > 0 Then
                    If tDt.Rows(0).Item("nNormalSendTaskID").ToString() = "1" And tDt.Rows(0).Item("nAbnormalSendTaskID").ToString() = "1" And tDt.Rows(0).Item("nTaskTypeID").ToString() = "0" Then
                        Return True
                    End If
                    If sDt.Rows.Count <> tDt.Rows.Count Then
                        Return False
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(tDTtemp) Then
                tDTtemp.Dispose()
                tDTtemp = Nothing
            End If
            If Not IsNothing(sDTtemp) Then
                sDTtemp.Dispose()
                sDTtemp = Nothing
            End If
            If Not IsNothing(tDv) Then
                tDv.Dispose()
                tDv = Nothing
            End If
            If Not IsNothing(sDv) Then
                sDv.Dispose()
                sDv = Nothing
            End If
        End Try
    End Function

    Private Sub btnSelect_UnselectAll_Click(sender As System.Object, e As System.EventArgs) Handles btnSelect_UnselectAll.Click
        Try
            If btnSelect_UnselectAll.Text = "Select All" Then
                SelectAll(trvNormalGroups)
                btnSelect_UnselectAll.Text = "Clear All"
            Else
                ClearAll(trvNormalGroups)
                btnSelect_UnselectAll.Text = "Select All"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnABSelect_UnselectAll_Click(sender As System.Object, e As System.EventArgs) Handles btnABSelect_UnselectAll.Click
        Try
            If btnABSelect_UnselectAll.Text = "Select All" Then
                SelectAll(trvAbnormalGroups)
                btnABSelect_UnselectAll.Text = "Clear All"
            Else
                ClearAll(trvAbnormalGroups)
                btnABSelect_UnselectAll.Text = "Select All"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Function Validate_DataChanged(ByVal sDt As DataSet, ByVal tDt As DataSet) As Boolean
    '    Try
    '        If String.Compare(sDt.Tables(0).Rows(0)("nNormalSendTaskID").ToString(), cmbNormalSendTask.SelectedValue.ToString(), False) <> 0 Then
    '            Return False
    '        End If

    '        If String.Compare(sDt..Tables(0).Rows(0).Item("nAbnormalSendTaskID").ToString(), cmbNormalSendTask.SelectedValue.ToString(), False) <> 0 Then
    '            Return False
    '        End If
    '        If sDt.Tables(0).Rows.Count <> tDt..Tables(0).Rows.Count Then
    '            Return False
    '        End If

    '        Dim drSource As DataRow
    '        Dim drTarget As DataRow
    '        Dim i, j As Integer
    '        'Dim sDv As New DataView(sDt, "nTaskTypeID <> 0", "nTaskTypeID,nUserID", DataViewRowState.CurrentRows)
    '        'Dim tDv As New DataView(tDt, "nTaskTypeID <> 0", "nTaskTypeID,nUserID", DataViewRowState.CurrentRows)

    '        'sDv.Sort = "nTaskTypeID,nUserID"
    '        'tDv.Sort = "nTaskTypeID,nUserID"
    '        'For Each drSource In sDv.Table.Rows
    '        '    For Each drTarget In tDv.Table.Rows
    '        '        If String.Compare(drTarget("nTaskTypeID"), drSource("nTaskTypeID")) = 0 Then
    '        '            If String.Compare(drTarget("nUserID").ToString(), drSource("nUserID").ToString()) <> 0 Then
    '        '                Return False
    '        '                Exit For
    '        '            End If
    '        '        End If
    '        '    Next
    '        'Next

    '        j = 0
    '        For i = 0 To tDt.Tables(0).Rows.Count - 1

    '            If String.Compare(tDt.Tables(0).Rows(i).Item("nTaskTypeID"), sDt.Tables(0).Rows(j).Item("nTaskTypeID")) = 0 Then
    '                If String.Compare(tDt.Tables(0).Rows(i).Item("nUserID").ToString(), sDt.table.Rows(j).Item("nUserID").ToString()) <> 0 Then
    '                    Return False
    '                    Exit For
    '                End If
    '            End If
    '            j = j + 1
    '        Next
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

    Private Sub btnCollapse_ExpandAll_Click(sender As System.Object, e As System.EventArgs) Handles btnCollapse_ExpandAll.Click
        Try
            If btnCollapse_ExpandAll.Text = "Collapse All" Then
                trvNormalGroups.CollapseAll()
                btnCollapse_ExpandAll.Text = "Expand All"
            Else
                trvNormalGroups.ExpandAll()
                btnCollapse_ExpandAll.Text = "Collapse All"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAbCollapse_ExpandAll_Click(sender As System.Object, e As System.EventArgs) Handles btnAbCollapse_ExpandAll.Click
        Try
            If btnAbCollapse_ExpandAll.Text = "Collapse All" Then
                trvAbnormalGroups.CollapseAll()
                btnAbCollapse_ExpandAll.Text = "Expand All"
            Else
                trvAbnormalGroups.ExpandAll()
                btnAbCollapse_ExpandAll.Text = "Collapse All"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
