<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAuditLogTampered
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAuditLogTampered))
        Me.MainPanel = New System.Windows.Forms.Panel()
        Me.pnlGridControl = New System.Windows.Forms.Panel()
        Me.flxData = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlLabelsPanel = New System.Windows.Forms.Panel()
        Me.pnlLabels = New System.Windows.Forms.SplitContainer()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupPanel = New System.Windows.Forms.Panel()
        Me.lblType = New System.Windows.Forms.Label()
        Me.lblModule = New System.Windows.Forms.Label()
        Me.lblOutcome = New System.Windows.Forms.Label()
        Me.lblSoftwareComponent = New System.Windows.Forms.Label()
        Me.lblMachineName = New System.Windows.Forms.Label()
        Me.lblDescp = New System.Windows.Forms.Label()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.lblActivityDateTime = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblTamperingDateTime = New System.Windows.Forms.Label()
        Me.lblActionUnderTaken = New System.Windows.Forms.Label()
        Me.lblTamperingUserName = New System.Windows.Forms.Label()
        Me.lblTamperingMachineName = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblsTamperingUserName = New System.Windows.Forms.Label()
        Me.pnlRecordDeleted = New System.Windows.Forms.Panel()
        Me.lblRowDeleted = New System.Windows.Forms.Label()
        Me.pnlAfterAlterationLabels = New System.Windows.Forms.Panel()
        Me.lblAfterType = New System.Windows.Forms.Label()
        Me.lblAfterModule = New System.Windows.Forms.Label()
        Me.lblAfterOutcome = New System.Windows.Forms.Label()
        Me.lblAfterSoftwareComponent = New System.Windows.Forms.Label()
        Me.lblAfterMachineName = New System.Windows.Forms.Label()
        Me.lblAfterDesc = New System.Windows.Forms.Label()
        Me.lblAfterCategory = New System.Windows.Forms.Label()
        Me.lblAfterActivity = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlToolstrip = New System.Windows.Forms.Panel()
        Me.tstrip = New System.Windows.Forms.ToolStrip()
        Me.btnClose = New System.Windows.Forms.ToolStripButton()
        Me.MainPanel.SuspendLayout()
        Me.pnlGridControl.SuspendLayout()
        CType(Me.flxData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLabelsPanel.SuspendLayout()
        CType(Me.pnlLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlLabels.Panel1.SuspendLayout()
        Me.pnlLabels.Panel2.SuspendLayout()
        Me.pnlLabels.SuspendLayout()
        Me.GroupPanel.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlRecordDeleted.SuspendLayout()
        Me.pnlAfterAlterationLabels.SuspendLayout()
        Me.pnlToolstrip.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainPanel
        '
        Me.MainPanel.BackColor = System.Drawing.Color.Transparent
        Me.MainPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MainPanel.Controls.Add(Me.pnlGridControl)
        Me.MainPanel.Controls.Add(Me.Label15)
        Me.MainPanel.Controls.Add(Me.Label14)
        Me.MainPanel.Controls.Add(Me.Label13)
        Me.MainPanel.Controls.Add(Me.Label7)
        Me.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainPanel.Location = New System.Drawing.Point(4, 59)
        Me.MainPanel.Name = "MainPanel"
        Me.MainPanel.Padding = New System.Windows.Forms.Padding(3)
        Me.MainPanel.Size = New System.Drawing.Size(746, 762)
        Me.MainPanel.TabIndex = 23
        '
        'pnlGridControl
        '
        Me.pnlGridControl.Controls.Add(Me.flxData)
        Me.pnlGridControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGridControl.Location = New System.Drawing.Point(4, 4)
        Me.pnlGridControl.Name = "pnlGridControl"
        Me.pnlGridControl.Size = New System.Drawing.Size(738, 754)
        Me.pnlGridControl.TabIndex = 13
        '
        'flxData
        '
        Me.flxData.AutoGenerateColumns = False
        Me.flxData.BackColor = System.Drawing.Color.GhostWhite
        Me.flxData.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.flxData.ColumnInfo = "0,0,0,0,0,105,Columns:"
        Me.flxData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flxData.ExtendLastCol = True
        Me.flxData.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.flxData.ForeColor = System.Drawing.Color.Black
        Me.flxData.Location = New System.Drawing.Point(0, 0)
        Me.flxData.Name = "flxData"
        Me.flxData.Rows.DefaultSize = 21
        Me.flxData.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.flxData.Size = New System.Drawing.Size(738, 754)
        Me.flxData.StyleInfo = resources.GetString("flxData.StyleInfo")
        Me.flxData.TabIndex = 3
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Location = New System.Drawing.Point(742, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Padding = New System.Windows.Forms.Padding(80, 3, 0, 0)
        Me.Label15.Size = New System.Drawing.Size(1, 754)
        Me.Label15.TabIndex = 29
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Location = New System.Drawing.Point(3, 4)
        Me.Label14.Name = "Label14"
        Me.Label14.Padding = New System.Windows.Forms.Padding(80, 3, 0, 0)
        Me.Label14.Size = New System.Drawing.Size(1, 754)
        Me.Label14.TabIndex = 28
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label13.Location = New System.Drawing.Point(3, 758)
        Me.Label13.Name = "Label13"
        Me.Label13.Padding = New System.Windows.Forms.Padding(80, 3, 0, 0)
        Me.Label13.Size = New System.Drawing.Size(740, 1)
        Me.Label13.TabIndex = 27
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Location = New System.Drawing.Point(3, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Padding = New System.Windows.Forms.Padding(80, 3, 0, 0)
        Me.Label7.Size = New System.Drawing.Size(740, 1)
        Me.Label7.TabIndex = 26
        '
        'pnlLabelsPanel
        '
        Me.pnlLabelsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.pnlLabelsPanel.BackColor = System.Drawing.Color.Transparent
        Me.pnlLabelsPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLabelsPanel.Controls.Add(Me.pnlLabels)
        Me.pnlLabelsPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLabelsPanel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlLabelsPanel.Location = New System.Drawing.Point(0, 53)
        Me.pnlLabelsPanel.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlLabelsPanel.Name = "pnlLabelsPanel"
        Me.pnlLabelsPanel.Size = New System.Drawing.Size(746, 2)
        Me.pnlLabelsPanel.TabIndex = 1
        '
        'pnlLabels
        '
        Me.pnlLabels.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLabels.IsSplitterFixed = True
        Me.pnlLabels.Location = New System.Drawing.Point(0, 0)
        Me.pnlLabels.Name = "pnlLabels"
        '
        'pnlLabels.Panel1
        '
        Me.pnlLabels.Panel1.Controls.Add(Me.btnBack)
        Me.pnlLabels.Panel1.Controls.Add(Me.Label8)
        Me.pnlLabels.Panel1.Controls.Add(Me.GroupPanel)
        '
        'pnlLabels.Panel2
        '
        Me.pnlLabels.Panel2.Controls.Add(Me.Label11)
        Me.pnlLabels.Panel2.Controls.Add(Me.Panel1)
        Me.pnlLabels.Size = New System.Drawing.Size(746, 2)
        Me.pnlLabels.SplitterDistance = 332
        Me.pnlLabels.TabIndex = 4
        '
        'btnBack
        '
        Me.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Location = New System.Drawing.Point(0, 0)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(33, 27)
        Me.btnBack.TabIndex = 34
        Me.ToolTip.SetToolTip(Me.btnBack, "Back")
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Padding = New System.Windows.Forms.Padding(80, 3, 0, 0)
        Me.Label8.Size = New System.Drawing.Size(220, 17)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Before Alteration Log"
        '
        'GroupPanel
        '
        Me.GroupPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupPanel.Controls.Add(Me.lblType)
        Me.GroupPanel.Controls.Add(Me.lblModule)
        Me.GroupPanel.Controls.Add(Me.lblOutcome)
        Me.GroupPanel.Controls.Add(Me.lblSoftwareComponent)
        Me.GroupPanel.Controls.Add(Me.lblMachineName)
        Me.GroupPanel.Controls.Add(Me.lblDescp)
        Me.GroupPanel.Controls.Add(Me.lblCategory)
        Me.GroupPanel.Controls.Add(Me.lblActivityDateTime)
        Me.GroupPanel.Controls.Add(Me.Label9)
        Me.GroupPanel.Controls.Add(Me.Label10)
        Me.GroupPanel.Controls.Add(Me.Label6)
        Me.GroupPanel.Controls.Add(Me.Label5)
        Me.GroupPanel.Controls.Add(Me.Label4)
        Me.GroupPanel.Controls.Add(Me.Label3)
        Me.GroupPanel.Controls.Add(Me.Label2)
        Me.GroupPanel.Controls.Add(Me.Label1)
        Me.GroupPanel.Location = New System.Drawing.Point(0, 20)
        Me.GroupPanel.Name = "GroupPanel"
        Me.GroupPanel.Size = New System.Drawing.Size(314, 2)
        Me.GroupPanel.TabIndex = 16
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.Location = New System.Drawing.Point(12, 397)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(91, 14)
        Me.lblType.TabIndex = 31
        Me.lblType.Text = "Activity Type:"
        '
        'lblModule
        '
        Me.lblModule.AutoSize = True
        Me.lblModule.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModule.Location = New System.Drawing.Point(12, 345)
        Me.lblModule.Name = "lblModule"
        Me.lblModule.Size = New System.Drawing.Size(111, 14)
        Me.lblModule.TabIndex = 30
        Me.lblModule.Text = "Activity  Module:"
        '
        'lblOutcome
        '
        Me.lblOutcome.AutoSize = True
        Me.lblOutcome.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOutcome.Location = New System.Drawing.Point(12, 293)
        Me.lblOutcome.Name = "lblOutcome"
        Me.lblOutcome.Size = New System.Drawing.Size(117, 14)
        Me.lblOutcome.TabIndex = 29
        Me.lblOutcome.Text = "Activity Outcome:"
        '
        'lblSoftwareComponent
        '
        Me.lblSoftwareComponent.AutoSize = True
        Me.lblSoftwareComponent.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSoftwareComponent.Location = New System.Drawing.Point(12, 241)
        Me.lblSoftwareComponent.Name = "lblSoftwareComponent"
        Me.lblSoftwareComponent.Size = New System.Drawing.Size(144, 14)
        Me.lblSoftwareComponent.TabIndex = 28
        Me.lblSoftwareComponent.Text = "Software Component:"
        '
        'lblMachineName
        '
        Me.lblMachineName.AutoSize = True
        Me.lblMachineName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMachineName.Location = New System.Drawing.Point(12, 189)
        Me.lblMachineName.Name = "lblMachineName"
        Me.lblMachineName.Size = New System.Drawing.Size(102, 14)
        Me.lblMachineName.TabIndex = 27
        Me.lblMachineName.Text = "Machine Name :"
        '
        'lblDescp
        '
        Me.lblDescp.AutoSize = True
        Me.lblDescp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescp.Location = New System.Drawing.Point(12, 137)
        Me.lblDescp.Name = "lblDescp"
        Me.lblDescp.Size = New System.Drawing.Size(135, 14)
        Me.lblDescp.TabIndex = 26
        Me.lblDescp.Text = "Activity Description :"
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategory.Location = New System.Drawing.Point(12, 85)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(122, 14)
        Me.lblCategory.TabIndex = 25
        Me.lblCategory.Text = "Activity Category :"
        '
        'lblActivityDateTime
        '
        Me.lblActivityDateTime.AutoSize = True
        Me.lblActivityDateTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActivityDateTime.Location = New System.Drawing.Point(12, 33)
        Me.lblActivityDateTime.Name = "lblActivityDateTime"
        Me.lblActivityDateTime.Size = New System.Drawing.Size(154, 14)
        Me.lblActivityDateTime.TabIndex = 24
        Me.lblActivityDateTime.Text = "Activity Date and Time :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 375)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(87, 14)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "Activity Type :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 323)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(98, 14)
        Me.Label10.TabIndex = 22
        Me.Label10.Text = "Activity Module :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 271)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(110, 14)
        Me.Label6.TabIndex = 21
        Me.Label6.Text = "Activity Outcome :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 219)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(133, 14)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Software Component :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 167)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 14)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Machine Name :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 115)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(119, 14)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Activity Description :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(108, 14)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Activity Category :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 14)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Activity Date and Time :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(0, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Padding = New System.Windows.Forms.Padding(100, 3, 0, 0)
        Me.Label11.Size = New System.Drawing.Size(232, 17)
        Me.Label11.TabIndex = 33
        Me.Label11.Text = "After Alteration Log"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.pnlRecordDeleted)
        Me.Panel1.Controls.Add(Me.pnlAfterAlterationLabels)
        Me.Panel1.Location = New System.Drawing.Point(2, 17)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(377, 0)
        Me.Panel1.TabIndex = 32
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Controls.Add(Me.lblTamperingDateTime)
        Me.Panel3.Controls.Add(Me.lblActionUnderTaken)
        Me.Panel3.Controls.Add(Me.lblTamperingUserName)
        Me.Panel3.Controls.Add(Me.lblTamperingMachineName)
        Me.Panel3.Controls.Add(Me.Label25)
        Me.Panel3.Controls.Add(Me.Label26)
        Me.Panel3.Controls.Add(Me.lblsTamperingUserName)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 452)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(377, 226)
        Me.Panel3.TabIndex = 49
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 153)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(156, 14)
        Me.Label12.TabIndex = 67
        Me.Label12.Text = "Tampering Machine Name :"
        '
        'lblTamperingDateTime
        '
        Me.lblTamperingDateTime.AutoSize = True
        Me.lblTamperingDateTime.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTamperingDateTime.Location = New System.Drawing.Point(6, 127)
        Me.lblTamperingDateTime.Name = "lblTamperingDateTime"
        Me.lblTamperingDateTime.Size = New System.Drawing.Size(131, 14)
        Me.lblTamperingDateTime.TabIndex = 66
        Me.lblTamperingDateTime.Text = "Activity Description:"
        '
        'lblActionUnderTaken
        '
        Me.lblActionUnderTaken.AutoSize = True
        Me.lblActionUnderTaken.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActionUnderTaken.Location = New System.Drawing.Point(6, 79)
        Me.lblActionUnderTaken.Name = "lblActionUnderTaken"
        Me.lblActionUnderTaken.Size = New System.Drawing.Size(118, 14)
        Me.lblActionUnderTaken.TabIndex = 65
        Me.lblActionUnderTaken.Text = "Activity Category:"
        '
        'lblTamperingUserName
        '
        Me.lblTamperingUserName.AutoSize = True
        Me.lblTamperingUserName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTamperingUserName.Location = New System.Drawing.Point(6, 31)
        Me.lblTamperingUserName.Name = "lblTamperingUserName"
        Me.lblTamperingUserName.Size = New System.Drawing.Size(142, 14)
        Me.lblTamperingUserName.TabIndex = 64
        Me.lblTamperingUserName.Text = "Tampering User Name:"
        '
        'lblTamperingMachineName
        '
        Me.lblTamperingMachineName.AutoSize = True
        Me.lblTamperingMachineName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTamperingMachineName.Location = New System.Drawing.Point(6, 175)
        Me.lblTamperingMachineName.Name = "lblTamperingMachineName"
        Me.lblTamperingMachineName.Size = New System.Drawing.Size(98, 14)
        Me.lblTamperingMachineName.TabIndex = 63
        Me.lblTamperingMachineName.Text = "Machine Name:"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(6, 105)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(158, 14)
        Me.Label25.TabIndex = 62
        Me.Label25.Text = "Tampering Date and Time :"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(6, 57)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(225, 14)
        Me.Label26.TabIndex = 61
        Me.Label26.Text = "Action Undertaken By Tampering User :"
        '
        'lblsTamperingUserName
        '
        Me.lblsTamperingUserName.AutoSize = True
        Me.lblsTamperingUserName.Location = New System.Drawing.Point(6, 9)
        Me.lblsTamperingUserName.Name = "lblsTamperingUserName"
        Me.lblsTamperingUserName.Size = New System.Drawing.Size(136, 14)
        Me.lblsTamperingUserName.TabIndex = 60
        Me.lblsTamperingUserName.Text = "Tampering User Name :"
        '
        'pnlRecordDeleted
        '
        Me.pnlRecordDeleted.Controls.Add(Me.lblRowDeleted)
        Me.pnlRecordDeleted.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRecordDeleted.Location = New System.Drawing.Point(0, 428)
        Me.pnlRecordDeleted.Name = "pnlRecordDeleted"
        Me.pnlRecordDeleted.Size = New System.Drawing.Size(377, 24)
        Me.pnlRecordDeleted.TabIndex = 50
        '
        'lblRowDeleted
        '
        Me.lblRowDeleted.AutoSize = True
        Me.lblRowDeleted.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblRowDeleted.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRowDeleted.ForeColor = System.Drawing.Color.Red
        Me.lblRowDeleted.Location = New System.Drawing.Point(0, 0)
        Me.lblRowDeleted.Name = "lblRowDeleted"
        Me.lblRowDeleted.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.lblRowDeleted.Size = New System.Drawing.Size(262, 18)
        Me.lblRowDeleted.TabIndex = 48
        Me.lblRowDeleted.Text = "This log entry was deleted from the Audit Log"
        '
        'pnlAfterAlterationLabels
        '
        Me.pnlAfterAlterationLabels.Controls.Add(Me.lblAfterType)
        Me.pnlAfterAlterationLabels.Controls.Add(Me.lblAfterModule)
        Me.pnlAfterAlterationLabels.Controls.Add(Me.lblAfterOutcome)
        Me.pnlAfterAlterationLabels.Controls.Add(Me.lblAfterSoftwareComponent)
        Me.pnlAfterAlterationLabels.Controls.Add(Me.lblAfterMachineName)
        Me.pnlAfterAlterationLabels.Controls.Add(Me.lblAfterDesc)
        Me.pnlAfterAlterationLabels.Controls.Add(Me.lblAfterCategory)
        Me.pnlAfterAlterationLabels.Controls.Add(Me.lblAfterActivity)
        Me.pnlAfterAlterationLabels.Controls.Add(Me.Label17)
        Me.pnlAfterAlterationLabels.Controls.Add(Me.Label18)
        Me.pnlAfterAlterationLabels.Controls.Add(Me.Label19)
        Me.pnlAfterAlterationLabels.Controls.Add(Me.Label20)
        Me.pnlAfterAlterationLabels.Controls.Add(Me.Label21)
        Me.pnlAfterAlterationLabels.Controls.Add(Me.Label22)
        Me.pnlAfterAlterationLabels.Controls.Add(Me.Label23)
        Me.pnlAfterAlterationLabels.Controls.Add(Me.Label24)
        Me.pnlAfterAlterationLabels.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlAfterAlterationLabels.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlAfterAlterationLabels.Location = New System.Drawing.Point(0, 0)
        Me.pnlAfterAlterationLabels.Name = "pnlAfterAlterationLabels"
        Me.pnlAfterAlterationLabels.Size = New System.Drawing.Size(377, 428)
        Me.pnlAfterAlterationLabels.TabIndex = 48
        '
        'lblAfterType
        '
        Me.lblAfterType.AutoSize = True
        Me.lblAfterType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAfterType.Location = New System.Drawing.Point(11, 399)
        Me.lblAfterType.Name = "lblAfterType"
        Me.lblAfterType.Size = New System.Drawing.Size(91, 14)
        Me.lblAfterType.TabIndex = 63
        Me.lblAfterType.Text = "Activity Type:"
        '
        'lblAfterModule
        '
        Me.lblAfterModule.AutoSize = True
        Me.lblAfterModule.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAfterModule.Location = New System.Drawing.Point(11, 347)
        Me.lblAfterModule.Name = "lblAfterModule"
        Me.lblAfterModule.Size = New System.Drawing.Size(111, 14)
        Me.lblAfterModule.TabIndex = 62
        Me.lblAfterModule.Text = "Activity  Module:"
        '
        'lblAfterOutcome
        '
        Me.lblAfterOutcome.AutoSize = True
        Me.lblAfterOutcome.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAfterOutcome.Location = New System.Drawing.Point(11, 295)
        Me.lblAfterOutcome.Name = "lblAfterOutcome"
        Me.lblAfterOutcome.Size = New System.Drawing.Size(117, 14)
        Me.lblAfterOutcome.TabIndex = 61
        Me.lblAfterOutcome.Text = "Activity Outcome:"
        '
        'lblAfterSoftwareComponent
        '
        Me.lblAfterSoftwareComponent.AutoSize = True
        Me.lblAfterSoftwareComponent.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAfterSoftwareComponent.Location = New System.Drawing.Point(11, 243)
        Me.lblAfterSoftwareComponent.Name = "lblAfterSoftwareComponent"
        Me.lblAfterSoftwareComponent.Size = New System.Drawing.Size(144, 14)
        Me.lblAfterSoftwareComponent.TabIndex = 60
        Me.lblAfterSoftwareComponent.Text = "Software Component:"
        '
        'lblAfterMachineName
        '
        Me.lblAfterMachineName.AutoSize = True
        Me.lblAfterMachineName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAfterMachineName.Location = New System.Drawing.Point(11, 191)
        Me.lblAfterMachineName.Name = "lblAfterMachineName"
        Me.lblAfterMachineName.Size = New System.Drawing.Size(98, 14)
        Me.lblAfterMachineName.TabIndex = 59
        Me.lblAfterMachineName.Text = "Machine Name:"
        '
        'lblAfterDesc
        '
        Me.lblAfterDesc.AutoSize = True
        Me.lblAfterDesc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAfterDesc.Location = New System.Drawing.Point(11, 139)
        Me.lblAfterDesc.Name = "lblAfterDesc"
        Me.lblAfterDesc.Size = New System.Drawing.Size(131, 14)
        Me.lblAfterDesc.TabIndex = 58
        Me.lblAfterDesc.Text = "Activity Description:"
        '
        'lblAfterCategory
        '
        Me.lblAfterCategory.AutoSize = True
        Me.lblAfterCategory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAfterCategory.Location = New System.Drawing.Point(11, 87)
        Me.lblAfterCategory.Name = "lblAfterCategory"
        Me.lblAfterCategory.Size = New System.Drawing.Size(118, 14)
        Me.lblAfterCategory.TabIndex = 57
        Me.lblAfterCategory.Text = "Activity Category:"
        '
        'lblAfterActivity
        '
        Me.lblAfterActivity.AutoSize = True
        Me.lblAfterActivity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAfterActivity.Location = New System.Drawing.Point(11, 35)
        Me.lblAfterActivity.Name = "lblAfterActivity"
        Me.lblAfterActivity.Size = New System.Drawing.Size(150, 14)
        Me.lblAfterActivity.TabIndex = 56
        Me.lblAfterActivity.Text = "Activity Date and Time:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(11, 377)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(87, 14)
        Me.Label17.TabIndex = 55
        Me.Label17.Text = "Activity Type :"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(11, 325)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(98, 14)
        Me.Label18.TabIndex = 54
        Me.Label18.Text = "Activity Module :"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(11, 273)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(110, 14)
        Me.Label19.TabIndex = 53
        Me.Label19.Text = "Activity Outcome :"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(11, 221)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(133, 14)
        Me.Label20.TabIndex = 52
        Me.Label20.Text = "Software Component :"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(11, 169)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(94, 14)
        Me.Label21.TabIndex = 51
        Me.Label21.Text = "Machine Name :"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(11, 117)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(119, 14)
        Me.Label22.TabIndex = 50
        Me.Label22.Text = "Activity Description :"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(11, 65)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(108, 14)
        Me.Label23.TabIndex = 49
        Me.Label23.Text = "Activity Category :"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(11, 13)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(140, 14)
        Me.Label24.TabIndex = 48
        Me.Label24.Text = "Activity Date and Time :"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "browse-details.ico")
        Me.ImageList1.Images.SetKeyName(1, "Browse-Web.png")
        Me.ImageList1.Images.SetKeyName(2, "web-browser.png")
        Me.ImageList1.Images.SetKeyName(3, "Right Arrow Red.ico")
        Me.ImageList1.Images.SetKeyName(4, "LeftArrow.ico")
        Me.ImageList1.Images.SetKeyName(5, "web-browser.png")
        Me.ImageList1.Images.SetKeyName(6, "Magnify.ico")
        '
        'pnlToolstrip
        '
        Me.pnlToolstrip.Controls.Add(Me.pnlLabelsPanel)
        Me.pnlToolstrip.Controls.Add(Me.tstrip)
        Me.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolstrip.Location = New System.Drawing.Point(4, 4)
        Me.pnlToolstrip.Name = "pnlToolstrip"
        Me.pnlToolstrip.Size = New System.Drawing.Size(746, 55)
        Me.pnlToolstrip.TabIndex = 25
        '
        'tstrip
        '
        Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tstrip.BackgroundImage = CType(resources.GetObject("tstrip.BackgroundImage"), System.Drawing.Image)
        Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(746, 53)
        Me.tstrip.TabIndex = 1
        Me.tstrip.Text = "ToolStrip1"
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(43, 50)
        Me.btnClose.Text = "&Close"
        Me.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnClose.ToolTipText = "Close"
        '
        'frmAuditLogTampered
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(754, 825)
        Me.Controls.Add(Me.MainPanel)
        Me.Controls.Add(Me.pnlToolstrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAuditLogTampered"
        Me.Padding = New System.Windows.Forms.Padding(4)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tampered Audit Log Details"
        Me.MainPanel.ResumeLayout(False)
        Me.pnlGridControl.ResumeLayout(False)
        CType(Me.flxData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLabelsPanel.ResumeLayout(False)
        Me.pnlLabels.Panel1.ResumeLayout(False)
        Me.pnlLabels.Panel1.PerformLayout()
        Me.pnlLabels.Panel2.ResumeLayout(False)
        Me.pnlLabels.Panel2.PerformLayout()
        CType(Me.pnlLabels, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlLabels.ResumeLayout(False)
        Me.GroupPanel.ResumeLayout(False)
        Me.GroupPanel.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.pnlRecordDeleted.ResumeLayout(False)
        Me.pnlRecordDeleted.PerformLayout()
        Me.pnlAfterAlterationLabels.ResumeLayout(False)
        Me.pnlAfterAlterationLabels.PerformLayout()
        Me.pnlToolstrip.ResumeLayout(False)
        Me.pnlToolstrip.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MainPanel As System.Windows.Forms.Panel
    Friend WithEvents pnlLabelsPanel As System.Windows.Forms.Panel
    Friend WithEvents pnlLabels As System.Windows.Forms.SplitContainer
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupPanel As System.Windows.Forms.Panel
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents lblModule As System.Windows.Forms.Label
    Friend WithEvents lblOutcome As System.Windows.Forms.Label
    Friend WithEvents lblSoftwareComponent As System.Windows.Forms.Label
    Friend WithEvents lblMachineName As System.Windows.Forms.Label
    Friend WithEvents lblDescp As System.Windows.Forms.Label
    Friend WithEvents lblCategory As System.Windows.Forms.Label
    Friend WithEvents lblActivityDateTime As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents pnlAfterAlterationLabels As System.Windows.Forms.Panel
    Friend WithEvents lblAfterType As System.Windows.Forms.Label
    Friend WithEvents lblAfterModule As System.Windows.Forms.Label
    Friend WithEvents lblAfterOutcome As System.Windows.Forms.Label
    Friend WithEvents lblAfterSoftwareComponent As System.Windows.Forms.Label
    Friend WithEvents lblAfterMachineName As System.Windows.Forms.Label
    Friend WithEvents lblAfterDesc As System.Windows.Forms.Label
    Friend WithEvents lblAfterCategory As System.Windows.Forms.Label
    Friend WithEvents lblAfterActivity As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblTamperingDateTime As System.Windows.Forms.Label
    Friend WithEvents lblActionUnderTaken As System.Windows.Forms.Label
    Friend WithEvents lblTamperingUserName As System.Windows.Forms.Label
    Friend WithEvents lblTamperingMachineName As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lblsTamperingUserName As System.Windows.Forms.Label
    Friend WithEvents pnlRecordDeleted As System.Windows.Forms.Panel
    Friend WithEvents lblRowDeleted As System.Windows.Forms.Label
    Friend WithEvents pnlGridControl As System.Windows.Forms.Panel
    Friend WithEvents flxData As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents pnlToolstrip As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnClose As System.Windows.Forms.ToolStripButton
End Class
