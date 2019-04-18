<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIMTransactionList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIMTransactionList))
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tblStrip = New System.Windows.Forms.ToolStrip()
        Me.tblbtn_Add = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Modify = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Delete = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Refresh = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Consent = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_VaccineRecord = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_PrintSummary = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_PrintDue = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_GenCCD = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.C1PatientIM = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.pnlTopRight = New System.Windows.Forms.Panel()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.pnlTop.SuspendLayout()
        Me.tblStrip.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        CType(Me.C1PatientIM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearch.SuspendLayout()
        Me.pnlTopRight.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTop.Controls.Add(Me.tblStrip)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1213, 58)
        Me.pnlTop.TabIndex = 28
        '
        'tblStrip
        '
        Me.tblStrip.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblStrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtn_Add, Me.tblbtn_Modify, Me.tblbtn_Delete, Me.tblbtn_Refresh, Me.tblbtn_Consent, Me.tblbtn_VaccineRecord, Me.tblbtn_PrintSummary, Me.tblbtn_PrintDue, Me.tblbtn_GenCCD, Me.tblbtn_Close})
        Me.tblStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tblStrip.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip.Name = "tblStrip"
        Me.tblStrip.Size = New System.Drawing.Size(1213, 53)
        Me.tblStrip.TabIndex = 0
        Me.tblStrip.Text = "ToolStrip1"
        '
        'tblbtn_Add
        '
        Me.tblbtn_Add.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Add.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Add.Image = CType(resources.GetObject("tblbtn_Add.Image"), System.Drawing.Image)
        Me.tblbtn_Add.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Add.Name = "tblbtn_Add"
        Me.tblbtn_Add.Size = New System.Drawing.Size(37, 50)
        Me.tblbtn_Add.Tag = "Add"
        Me.tblbtn_Add.Text = "&New"
        Me.tblbtn_Add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Modify
        '
        Me.tblbtn_Modify.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Modify.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Modify.Image = CType(resources.GetObject("tblbtn_Modify.Image"), System.Drawing.Image)
        Me.tblbtn_Modify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Modify.Name = "tblbtn_Modify"
        Me.tblbtn_Modify.Size = New System.Drawing.Size(53, 50)
        Me.tblbtn_Modify.Tag = "Modify"
        Me.tblbtn_Modify.Text = "&Modify"
        Me.tblbtn_Modify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Delete
        '
        Me.tblbtn_Delete.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Delete.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Delete.Image = CType(resources.GetObject("tblbtn_Delete.Image"), System.Drawing.Image)
        Me.tblbtn_Delete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Delete.Name = "tblbtn_Delete"
        Me.tblbtn_Delete.Size = New System.Drawing.Size(50, 50)
        Me.tblbtn_Delete.Tag = "Delete"
        Me.tblbtn_Delete.Text = "&Delete"
        Me.tblbtn_Delete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Refresh
        '
        Me.tblbtn_Refresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Refresh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Refresh.Image = CType(resources.GetObject("tblbtn_Refresh.Image"), System.Drawing.Image)
        Me.tblbtn_Refresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Refresh.Name = "tblbtn_Refresh"
        Me.tblbtn_Refresh.Size = New System.Drawing.Size(58, 50)
        Me.tblbtn_Refresh.Tag = "Refresh"
        Me.tblbtn_Refresh.Text = "&Refresh"
        Me.tblbtn_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Consent
        '
        Me.tblbtn_Consent.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Consent.Image = CType(resources.GetObject("tblbtn_Consent.Image"), System.Drawing.Image)
        Me.tblbtn_Consent.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Consent.Name = "tblbtn_Consent"
        Me.tblbtn_Consent.Size = New System.Drawing.Size(62, 50)
        Me.tblbtn_Consent.Tag = "Save and Close"
        Me.tblbtn_Consent.Text = "&Consent"
        Me.tblbtn_Consent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Consent.ToolTipText = "Consent"
        '
        'tblbtn_VaccineRecord
        '
        Me.tblbtn_VaccineRecord.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tblbtn_VaccineRecord.Image = CType(resources.GetObject("tblbtn_VaccineRecord.Image"), System.Drawing.Image)
        Me.tblbtn_VaccineRecord.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_VaccineRecord.Name = "tblbtn_VaccineRecord"
        Me.tblbtn_VaccineRecord.Size = New System.Drawing.Size(103, 50)
        Me.tblbtn_VaccineRecord.Tag = "Vaccine Record"
        Me.tblbtn_VaccineRecord.Text = "Vaccine Record"
        Me.tblbtn_VaccineRecord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_PrintSummary
        '
        Me.tblbtn_PrintSummary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tblbtn_PrintSummary.Image = CType(resources.GetObject("tblbtn_PrintSummary.Image"), System.Drawing.Image)
        Me.tblbtn_PrintSummary.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_PrintSummary.Name = "tblbtn_PrintSummary"
        Me.tblbtn_PrintSummary.Size = New System.Drawing.Size(102, 50)
        Me.tblbtn_PrintSummary.Tag = "Print Summary"
        Me.tblbtn_PrintSummary.Text = "Print Summary"
        Me.tblbtn_PrintSummary.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_PrintDue
        '
        Me.tblbtn_PrintDue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tblbtn_PrintDue.Image = CType(resources.GetObject("tblbtn_PrintDue.Image"), System.Drawing.Image)
        Me.tblbtn_PrintDue.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_PrintDue.Name = "tblbtn_PrintDue"
        Me.tblbtn_PrintDue.Size = New System.Drawing.Size(69, 50)
        Me.tblbtn_PrintDue.Tag = "Print Due"
        Me.tblbtn_PrintDue.Text = "Print Due"
        Me.tblbtn_PrintDue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_GenCCD
        '
        Me.tblbtn_GenCCD.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.tblbtn_GenCCD.Image = CType(resources.GetObject("tblbtn_GenCCD.Image"), System.Drawing.Image)
        Me.tblbtn_GenCCD.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_GenCCD.Name = "tblbtn_GenCCD"
        Me.tblbtn_GenCCD.Size = New System.Drawing.Size(63, 50)
        Me.tblbtn_GenCCD.Tag = "Gen CCD"
        Me.tblbtn_GenCCD.Text = "Gen CCD"
        Me.tblbtn_GenCCD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tblbtn_Close
        '
        Me.tblbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Close.Image = CType(resources.GetObject("tblbtn_Close.Image"), System.Drawing.Image)
        Me.tblbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close.Name = "tblbtn_Close"
        Me.tblbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close.Tag = "Close"
        Me.tblbtn_Close.Text = "Close"
        Me.tblbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.Label7)
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Controls.Add(Me.C1PatientIM)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMain.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlMain.Location = New System.Drawing.Point(0, 85)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMain.Size = New System.Drawing.Size(1213, 508)
        Me.pnlMain.TabIndex = 29
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(4, 504)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1205, 1)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "label2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 501)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label7.Location = New System.Drawing.Point(1209, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 501)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "label3"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1207, 1)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "label1"
        '
        'C1PatientIM
        '
        Me.C1PatientIM.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1PatientIM.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1PatientIM.AutoGenerateColumns = False
        Me.C1PatientIM.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1PatientIM.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1PatientIM.ColumnInfo = resources.GetString("C1PatientIM.ColumnInfo")
        Me.C1PatientIM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1PatientIM.ExtendLastCol = True
        Me.C1PatientIM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1PatientIM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1PatientIM.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1PatientIM.Location = New System.Drawing.Point(3, 3)
        Me.C1PatientIM.Name = "C1PatientIM"
        Me.C1PatientIM.Rows.Count = 13
        Me.C1PatientIM.Rows.DefaultSize = 19
        Me.C1PatientIM.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1PatientIM.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1PatientIM.ShowCellLabels = True
        Me.C1PatientIM.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1PatientIM.Size = New System.Drawing.Size(1207, 502)
        Me.C1PatientIM.StyleInfo = resources.GetString("C1PatientIM.StyleInfo")
        Me.C1PatientIM.TabIndex = 14
        Me.C1PatientIM.Tree.NodeImageCollapsed = CType(resources.GetObject("C1PatientIM.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1PatientIM.Tree.NodeImageExpanded = CType(resources.GetObject("C1PatientIM.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearch.Controls.Add(Me.pnlTopRight)
        Me.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearch.Location = New System.Drawing.Point(0, 58)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Padding = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.pnlSearch.Size = New System.Drawing.Size(1213, 27)
        Me.pnlSearch.TabIndex = 30
        '
        'pnlTopRight
        '
        Me.pnlTopRight.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlTopRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlTopRight.Controls.Add(Me.btnClear)
        Me.pnlTopRight.Controls.Add(Me.Label1)
        Me.pnlTopRight.Controls.Add(Me.txtSearch)
        Me.pnlTopRight.Controls.Add(Me.lblSearch)
        Me.pnlTopRight.Controls.Add(Me.Label2)
        Me.pnlTopRight.Controls.Add(Me.Label3)
        Me.pnlTopRight.Controls.Add(Me.Label4)
        Me.pnlTopRight.Controls.Add(Me.Label9)
        Me.pnlTopRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlTopRight.Location = New System.Drawing.Point(3, 3)
        Me.pnlTopRight.Name = "pnlTopRight"
        Me.pnlTopRight.Size = New System.Drawing.Size(1207, 24)
        Me.pnlTopRight.TabIndex = 8
        '
        'btnClear
        '
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(428, 1)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 22)
        Me.btnClear.TabIndex = 48
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(424, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label1.Size = New System.Drawing.Size(4, 20)
        Me.Label1.TabIndex = 49
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSearch
        '
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.Color.Black
        Me.txtSearch.Location = New System.Drawing.Point(110, 1)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(314, 22)
        Me.txtSearch.TabIndex = 0
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.Transparent
        Me.lblSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearch.Location = New System.Drawing.Point(1, 1)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(109, 22)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "Search :"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.ForeColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(1, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1205, 1)
        Me.Label2.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 23)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(1206, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 23)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label9.ForeColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(0, 23)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1207, 1)
        Me.Label9.TabIndex = 8
        '
        'frmIMTransactionList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1213, 593)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlSearch)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmIMTransactionList"
        Me.Text = "View Patient Immunization"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.tblStrip.ResumeLayout(False)
        Me.tblStrip.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        CType(Me.C1PatientIM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlTopRight.ResumeLayout(False)
        Me.pnlTopRight.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents tblStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents tblbtn_Add As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Modify As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Delete As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Refresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents C1PatientIM As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents tblbtn_Consent As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_PrintSummary As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_PrintDue As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_VaccineRecord As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_GenCCD As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents pnlTopRight As System.Windows.Forms.Panel
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
End Class
