<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewCQMCriteriaReason
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewCQMCriteriaReason))
        Me.C1NotNumerator = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.C1Vitals = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.C1Medication = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.C1Immunization = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.C1General = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlToolstrip = New System.Windows.Forms.Panel()
        Me.tls_Main = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlsbtn_Cancel = New System.Windows.Forms.ToolStripButton()
        Me.pnlC1NotNumerator = New System.Windows.Forms.Panel()
        Me.pnlDarkHeader = New System.Windows.Forms.Panel()
        Me.pnlMid = New System.Windows.Forms.Panel()
        Me.Label107 = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lblGridBottom = New System.Windows.Forms.Label()
        Me.lblGridLeft = New System.Windows.Forms.Label()
        Me.lblGridRight = New System.Windows.Forms.Label()
        Me.lblGridTop = New System.Windows.Forms.Label()
        Me.sptpnlC1NotNumerator = New System.Windows.Forms.Splitter()
        Me.pnlC1Vitals = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pnlMainHeader = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblCriteriaReason = New System.Windows.Forms.Label()
        Me.pnlC1Medication = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.sptpnlC1Vitals = New System.Windows.Forms.Splitter()
        Me.pnlC1Immunization = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.sptpnlC1Medication = New System.Windows.Forms.Splitter()
        Me.sptpnlC1Immunization = New System.Windows.Forms.Splitter()
        Me.pnlC1General = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.pnlC1OrderResults = New System.Windows.Forms.Panel()
        Me.C1OrderResults = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.sptpnlC1General = New System.Windows.Forms.Splitter()
        Me.pnlPatientDetails = New System.Windows.Forms.Panel()
        Me.lblPatientDetails = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        CType(Me.C1NotNumerator, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Vitals, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Medication, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1Immunization, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1General, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlToolstrip.SuspendLayout()
        Me.tls_Main.SuspendLayout()
        Me.pnlC1NotNumerator.SuspendLayout()
        Me.pnlDarkHeader.SuspendLayout()
        Me.pnlMid.SuspendLayout()
        Me.pnlC1Vitals.SuspendLayout()
        Me.pnlMainHeader.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlC1Medication.SuspendLayout()
        Me.pnlC1Immunization.SuspendLayout()
        Me.pnlC1General.SuspendLayout()
        Me.pnlC1OrderResults.SuspendLayout()
        CType(Me.C1OrderResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPatientDetails.SuspendLayout()
        Me.SuspendLayout()
        '
        'C1NotNumerator
        '
        Me.C1NotNumerator.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1NotNumerator.AllowEditing = False
        Me.C1NotNumerator.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn
        Me.C1NotNumerator.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1NotNumerator.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1NotNumerator.ColumnInfo = "1,0,0,0,0,105,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCente" & _
    "r;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1NotNumerator.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1NotNumerator.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1NotNumerator.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1NotNumerator.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1NotNumerator.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1NotNumerator.Location = New System.Drawing.Point(4, 26)
        Me.C1NotNumerator.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1NotNumerator.Name = "C1NotNumerator"
        Me.C1NotNumerator.Rows.Count = 1
        Me.C1NotNumerator.Rows.DefaultSize = 19
        Me.C1NotNumerator.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1NotNumerator.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1NotNumerator.ShowCellLabels = True
        Me.C1NotNumerator.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1NotNumerator.Size = New System.Drawing.Size(924, 103)
        Me.C1NotNumerator.StyleInfo = resources.GetString("C1NotNumerator.StyleInfo")
        Me.C1NotNumerator.TabIndex = 12
        '
        'C1Vitals
        '
        Me.C1Vitals.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1Vitals.AllowEditing = False
        Me.C1Vitals.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn
        Me.C1Vitals.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Vitals.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Vitals.ColumnInfo = "1,0,0,0,0,105,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCente" & _
    "r;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1Vitals.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Vitals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Vitals.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Vitals.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Vitals.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Vitals.Location = New System.Drawing.Point(4, 1)
        Me.C1Vitals.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1Vitals.Name = "C1Vitals"
        Me.C1Vitals.Rows.Count = 1
        Me.C1Vitals.Rows.DefaultSize = 19
        Me.C1Vitals.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Vitals.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Vitals.ShowCellLabels = True
        Me.C1Vitals.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1Vitals.Size = New System.Drawing.Size(924, 128)
        Me.C1Vitals.StyleInfo = resources.GetString("C1Vitals.StyleInfo")
        Me.C1Vitals.TabIndex = 16
        '
        'C1Medication
        '
        Me.C1Medication.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1Medication.AllowEditing = False
        Me.C1Medication.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn
        Me.C1Medication.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Medication.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Medication.ColumnInfo = "1,0,0,0,0,105,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCente" & _
    "r;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1Medication.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Medication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Medication.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Medication.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Medication.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Medication.Location = New System.Drawing.Point(4, 1)
        Me.C1Medication.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1Medication.Name = "C1Medication"
        Me.C1Medication.Rows.Count = 1
        Me.C1Medication.Rows.DefaultSize = 19
        Me.C1Medication.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Medication.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Medication.ShowCellLabels = True
        Me.C1Medication.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1Medication.Size = New System.Drawing.Size(924, 128)
        Me.C1Medication.StyleInfo = resources.GetString("C1Medication.StyleInfo")
        Me.C1Medication.TabIndex = 17
        '
        'C1Immunization
        '
        Me.C1Immunization.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1Immunization.AllowEditing = False
        Me.C1Immunization.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn
        Me.C1Immunization.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Immunization.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Immunization.ColumnInfo = "1,0,0,0,0,105,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCente" & _
    "r;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1Immunization.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Immunization.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Immunization.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Immunization.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Immunization.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Immunization.Location = New System.Drawing.Point(4, 1)
        Me.C1Immunization.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1Immunization.Name = "C1Immunization"
        Me.C1Immunization.Rows.Count = 1
        Me.C1Immunization.Rows.DefaultSize = 19
        Me.C1Immunization.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Immunization.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Immunization.ShowCellLabels = True
        Me.C1Immunization.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1Immunization.Size = New System.Drawing.Size(924, 128)
        Me.C1Immunization.StyleInfo = resources.GetString("C1Immunization.StyleInfo")
        Me.C1Immunization.TabIndex = 18
        '
        'C1General
        '
        Me.C1General.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1General.AllowEditing = False
        Me.C1General.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn
        Me.C1General.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1General.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1General.ColumnInfo = "1,0,0,0,0,105,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCente" & _
    "r;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1General.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1General.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1General.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1General.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1General.Location = New System.Drawing.Point(4, 1)
        Me.C1General.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1General.Name = "C1General"
        Me.C1General.Rows.Count = 1
        Me.C1General.Rows.DefaultSize = 19
        Me.C1General.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1General.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1General.ShowCellLabels = True
        Me.C1General.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1General.Size = New System.Drawing.Size(924, 166)
        Me.C1General.StyleInfo = resources.GetString("C1General.StyleInfo")
        Me.C1General.TabIndex = 19
        '
        'pnlToolstrip
        '
        Me.pnlToolstrip.AutoSize = True
        Me.pnlToolstrip.Controls.Add(Me.tls_Main)
        Me.pnlToolstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlToolstrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlToolstrip.Name = "pnlToolstrip"
        Me.pnlToolstrip.Size = New System.Drawing.Size(932, 60)
        Me.pnlToolstrip.TabIndex = 28
        '
        'tls_Main
        '
        Me.tls_Main.AutoSize = False
        Me.tls_Main.BackgroundImage = CType(resources.GetObject("tls_Main.BackgroundImage"), System.Drawing.Image)
        Me.tls_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tls_Main.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_Main.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtn_Cancel})
        Me.tls_Main.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.tls_Main.Location = New System.Drawing.Point(0, 0)
        Me.tls_Main.Name = "tls_Main"
        Me.tls_Main.Size = New System.Drawing.Size(932, 60)
        Me.tls_Main.TabIndex = 3
        Me.tls_Main.Text = "ToolStrip1"
        '
        'tlsbtn_Cancel
        '
        Me.tlsbtn_Cancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtn_Cancel.Image = CType(resources.GetObject("tlsbtn_Cancel.Image"), System.Drawing.Image)
        Me.tlsbtn_Cancel.Name = "tlsbtn_Cancel"
        Me.tlsbtn_Cancel.Size = New System.Drawing.Size(43, 57)
        Me.tlsbtn_Cancel.Tag = "Close"
        Me.tlsbtn_Cancel.Text = "&Close"
        Me.tlsbtn_Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtn_Cancel.ToolTipText = "Close"
        '
        'pnlC1NotNumerator
        '
        Me.pnlC1NotNumerator.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlC1NotNumerator.Controls.Add(Me.C1NotNumerator)
        Me.pnlC1NotNumerator.Controls.Add(Me.pnlDarkHeader)
        Me.pnlC1NotNumerator.Controls.Add(Me.lblGridBottom)
        Me.pnlC1NotNumerator.Controls.Add(Me.lblGridLeft)
        Me.pnlC1NotNumerator.Controls.Add(Me.lblGridRight)
        Me.pnlC1NotNumerator.Controls.Add(Me.lblGridTop)
        Me.pnlC1NotNumerator.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlC1NotNumerator.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlC1NotNumerator.Location = New System.Drawing.Point(0, 95)
        Me.pnlC1NotNumerator.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlC1NotNumerator.Name = "pnlC1NotNumerator"
        Me.pnlC1NotNumerator.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.pnlC1NotNumerator.Size = New System.Drawing.Size(932, 130)
        Me.pnlC1NotNumerator.TabIndex = 29
        '
        'pnlDarkHeader
        '
        Me.pnlDarkHeader.Controls.Add(Me.pnlMid)
        Me.pnlDarkHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDarkHeader.Location = New System.Drawing.Point(4, 1)
        Me.pnlDarkHeader.Name = "pnlDarkHeader"
        Me.pnlDarkHeader.Size = New System.Drawing.Size(924, 25)
        Me.pnlDarkHeader.TabIndex = 230
        Me.pnlDarkHeader.TabStop = True
        '
        'pnlMid
        '
        Me.pnlMid.BackgroundImage = CType(resources.GetObject("pnlMid.BackgroundImage"), System.Drawing.Image)
        Me.pnlMid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMid.Controls.Add(Me.Label107)
        Me.pnlMid.Controls.Add(Me.lblHeader)
        Me.pnlMid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMid.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMid.Location = New System.Drawing.Point(0, 0)
        Me.pnlMid.Name = "pnlMid"
        Me.pnlMid.Size = New System.Drawing.Size(924, 25)
        Me.pnlMid.TabIndex = 44
        '
        'Label107
        '
        Me.Label107.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label107.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label107.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label107.Location = New System.Drawing.Point(0, 24)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(924, 1)
        Me.Label107.TabIndex = 13
        Me.Label107.Text = "label2"
        '
        'lblHeader
        '
        Me.lblHeader.BackColor = System.Drawing.Color.Transparent
        Me.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblHeader.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblHeader.Location = New System.Drawing.Point(0, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(924, 25)
        Me.lblHeader.TabIndex = 9
        Me.lblHeader.Text = "Patient lacking following conditions"
        Me.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblGridBottom
        '
        Me.lblGridBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGridBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblGridBottom.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblGridBottom.Location = New System.Drawing.Point(4, 129)
        Me.lblGridBottom.Name = "lblGridBottom"
        Me.lblGridBottom.Size = New System.Drawing.Size(924, 1)
        Me.lblGridBottom.TabIndex = 10
        Me.lblGridBottom.Text = "label2"
        '
        'lblGridLeft
        '
        Me.lblGridLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGridLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblGridLeft.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGridLeft.Location = New System.Drawing.Point(3, 1)
        Me.lblGridLeft.Name = "lblGridLeft"
        Me.lblGridLeft.Size = New System.Drawing.Size(1, 129)
        Me.lblGridLeft.TabIndex = 9
        Me.lblGridLeft.Text = "label4"
        '
        'lblGridRight
        '
        Me.lblGridRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGridRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblGridRight.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.lblGridRight.Location = New System.Drawing.Point(928, 1)
        Me.lblGridRight.Name = "lblGridRight"
        Me.lblGridRight.Size = New System.Drawing.Size(1, 129)
        Me.lblGridRight.TabIndex = 8
        Me.lblGridRight.Text = "label3"
        '
        'lblGridTop
        '
        Me.lblGridTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblGridTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblGridTop.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGridTop.Location = New System.Drawing.Point(3, 0)
        Me.lblGridTop.Name = "lblGridTop"
        Me.lblGridTop.Size = New System.Drawing.Size(926, 1)
        Me.lblGridTop.TabIndex = 7
        Me.lblGridTop.Text = "label1"
        '
        'sptpnlC1NotNumerator
        '
        Me.sptpnlC1NotNumerator.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.sptpnlC1NotNumerator.Dock = System.Windows.Forms.DockStyle.Top
        Me.sptpnlC1NotNumerator.Location = New System.Drawing.Point(0, 225)
        Me.sptpnlC1NotNumerator.Name = "sptpnlC1NotNumerator"
        Me.sptpnlC1NotNumerator.Size = New System.Drawing.Size(932, 3)
        Me.sptpnlC1NotNumerator.TabIndex = 30
        Me.sptpnlC1NotNumerator.TabStop = False
        '
        'pnlC1Vitals
        '
        Me.pnlC1Vitals.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlC1Vitals.Controls.Add(Me.C1Vitals)
        Me.pnlC1Vitals.Controls.Add(Me.Label3)
        Me.pnlC1Vitals.Controls.Add(Me.Label4)
        Me.pnlC1Vitals.Controls.Add(Me.Label5)
        Me.pnlC1Vitals.Controls.Add(Me.Label6)
        Me.pnlC1Vitals.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlC1Vitals.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlC1Vitals.Location = New System.Drawing.Point(0, 256)
        Me.pnlC1Vitals.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlC1Vitals.Name = "pnlC1Vitals"
        Me.pnlC1Vitals.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.pnlC1Vitals.Size = New System.Drawing.Size(932, 130)
        Me.pnlC1Vitals.TabIndex = 31
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label3.Location = New System.Drawing.Point(4, 129)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(924, 1)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "label2"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 129)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "label4"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label5.Location = New System.Drawing.Point(928, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 129)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "label3"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(926, 1)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "label1"
        '
        'pnlMainHeader
        '
        Me.pnlMainHeader.Controls.Add(Me.Panel3)
        Me.pnlMainHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMainHeader.Location = New System.Drawing.Point(0, 228)
        Me.pnlMainHeader.Name = "pnlMainHeader"
        Me.pnlMainHeader.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlMainHeader.Size = New System.Drawing.Size(932, 28)
        Me.pnlMainHeader.TabIndex = 230
        Me.pnlMainHeader.TabStop = True
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.Label20)
        Me.Panel3.Controls.Add(Me.Label19)
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.lblCriteriaReason)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Location = New System.Drawing.Point(3, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(926, 25)
        Me.Panel3.TabIndex = 44
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(1, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(924, 1)
        Me.Label20.TabIndex = 16
        Me.Label20.Text = "label1"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(925, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 24)
        Me.Label19.TabIndex = 15
        Me.Label19.Text = "label4"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(0, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 24)
        Me.Label18.TabIndex = 14
        Me.Label18.Text = "label4"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label1.Location = New System.Drawing.Point(0, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(926, 1)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "label2"
        '
        'lblCriteriaReason
        '
        Me.lblCriteriaReason.BackColor = System.Drawing.Color.Transparent
        Me.lblCriteriaReason.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCriteriaReason.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblCriteriaReason.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCriteriaReason.ForeColor = System.Drawing.Color.White
        Me.lblCriteriaReason.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblCriteriaReason.Location = New System.Drawing.Point(0, 0)
        Me.lblCriteriaReason.Name = "lblCriteriaReason"
        Me.lblCriteriaReason.Size = New System.Drawing.Size(926, 25)
        Me.lblCriteriaReason.TabIndex = 9
        Me.lblCriteriaReason.Text = "  Criteria"
        Me.lblCriteriaReason.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlC1Medication
        '
        Me.pnlC1Medication.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlC1Medication.Controls.Add(Me.C1Medication)
        Me.pnlC1Medication.Controls.Add(Me.Label8)
        Me.pnlC1Medication.Controls.Add(Me.Label9)
        Me.pnlC1Medication.Controls.Add(Me.Label10)
        Me.pnlC1Medication.Controls.Add(Me.Label11)
        Me.pnlC1Medication.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlC1Medication.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlC1Medication.Location = New System.Drawing.Point(0, 389)
        Me.pnlC1Medication.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlC1Medication.Name = "pnlC1Medication"
        Me.pnlC1Medication.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.pnlC1Medication.Size = New System.Drawing.Size(932, 130)
        Me.pnlC1Medication.TabIndex = 32
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(4, 129)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(924, 1)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "label2"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 129)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(928, 1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1, 129)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "label3"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(3, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(926, 1)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "label1"
        '
        'sptpnlC1Vitals
        '
        Me.sptpnlC1Vitals.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.sptpnlC1Vitals.Dock = System.Windows.Forms.DockStyle.Top
        Me.sptpnlC1Vitals.Location = New System.Drawing.Point(0, 386)
        Me.sptpnlC1Vitals.Name = "sptpnlC1Vitals"
        Me.sptpnlC1Vitals.Size = New System.Drawing.Size(932, 3)
        Me.sptpnlC1Vitals.TabIndex = 34
        Me.sptpnlC1Vitals.TabStop = False
        '
        'pnlC1Immunization
        '
        Me.pnlC1Immunization.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlC1Immunization.Controls.Add(Me.C1Immunization)
        Me.pnlC1Immunization.Controls.Add(Me.Label2)
        Me.pnlC1Immunization.Controls.Add(Me.Label7)
        Me.pnlC1Immunization.Controls.Add(Me.Label12)
        Me.pnlC1Immunization.Controls.Add(Me.Label13)
        Me.pnlC1Immunization.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlC1Immunization.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlC1Immunization.Location = New System.Drawing.Point(0, 522)
        Me.pnlC1Immunization.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlC1Immunization.Name = "pnlC1Immunization"
        Me.pnlC1Immunization.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.pnlC1Immunization.Size = New System.Drawing.Size(932, 130)
        Me.pnlC1Immunization.TabIndex = 35
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label2.Location = New System.Drawing.Point(4, 129)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(924, 1)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "label2"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 129)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "label4"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label12.Location = New System.Drawing.Point(928, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 129)
        Me.Label12.TabIndex = 8
        Me.Label12.Text = "label3"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(3, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(926, 1)
        Me.Label13.TabIndex = 7
        Me.Label13.Text = "label1"
        '
        'sptpnlC1Medication
        '
        Me.sptpnlC1Medication.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.sptpnlC1Medication.Dock = System.Windows.Forms.DockStyle.Top
        Me.sptpnlC1Medication.Location = New System.Drawing.Point(0, 519)
        Me.sptpnlC1Medication.Name = "sptpnlC1Medication"
        Me.sptpnlC1Medication.Size = New System.Drawing.Size(932, 3)
        Me.sptpnlC1Medication.TabIndex = 36
        Me.sptpnlC1Medication.TabStop = False
        '
        'sptpnlC1Immunization
        '
        Me.sptpnlC1Immunization.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.sptpnlC1Immunization.Dock = System.Windows.Forms.DockStyle.Top
        Me.sptpnlC1Immunization.Location = New System.Drawing.Point(0, 652)
        Me.sptpnlC1Immunization.Name = "sptpnlC1Immunization"
        Me.sptpnlC1Immunization.Size = New System.Drawing.Size(932, 3)
        Me.sptpnlC1Immunization.TabIndex = 37
        Me.sptpnlC1Immunization.TabStop = False
        '
        'pnlC1General
        '
        Me.pnlC1General.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlC1General.Controls.Add(Me.C1General)
        Me.pnlC1General.Controls.Add(Me.Label14)
        Me.pnlC1General.Controls.Add(Me.Label15)
        Me.pnlC1General.Controls.Add(Me.Label16)
        Me.pnlC1General.Controls.Add(Me.Label17)
        Me.pnlC1General.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlC1General.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlC1General.Location = New System.Drawing.Point(0, 655)
        Me.pnlC1General.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlC1General.Name = "pnlC1General"
        Me.pnlC1General.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.pnlC1General.Size = New System.Drawing.Size(932, 168)
        Me.pnlC1General.TabIndex = 38
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label14.Location = New System.Drawing.Point(4, 167)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(924, 1)
        Me.Label14.TabIndex = 10
        Me.Label14.Text = "label2"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(3, 1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 167)
        Me.Label15.TabIndex = 9
        Me.Label15.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(928, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 167)
        Me.Label16.TabIndex = 8
        Me.Label16.Text = "label3"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(3, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(926, 1)
        Me.Label17.TabIndex = 7
        Me.Label17.Text = "label1"
        '
        'pnlC1OrderResults
        '
        Me.pnlC1OrderResults.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlC1OrderResults.Controls.Add(Me.C1OrderResults)
        Me.pnlC1OrderResults.Controls.Add(Me.Label21)
        Me.pnlC1OrderResults.Controls.Add(Me.Label22)
        Me.pnlC1OrderResults.Controls.Add(Me.Label23)
        Me.pnlC1OrderResults.Controls.Add(Me.Label24)
        Me.pnlC1OrderResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlC1OrderResults.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlC1OrderResults.Location = New System.Drawing.Point(0, 826)
        Me.pnlC1OrderResults.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlC1OrderResults.Name = "pnlC1OrderResults"
        Me.pnlC1OrderResults.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlC1OrderResults.Size = New System.Drawing.Size(932, 83)
        Me.pnlC1OrderResults.TabIndex = 231
        '
        'C1OrderResults
        '
        Me.C1OrderResults.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1OrderResults.AllowEditing = False
        Me.C1OrderResults.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.MultiColumn
        Me.C1OrderResults.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1OrderResults.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1OrderResults.ColumnInfo = "1,0,0,0,0,105,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCente" & _
    "r;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1OrderResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1OrderResults.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1OrderResults.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1OrderResults.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1OrderResults.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1OrderResults.Location = New System.Drawing.Point(4, 1)
        Me.C1OrderResults.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1OrderResults.Name = "C1OrderResults"
        Me.C1OrderResults.Rows.Count = 1
        Me.C1OrderResults.Rows.DefaultSize = 19
        Me.C1OrderResults.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1OrderResults.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1OrderResults.ShowCellLabels = True
        Me.C1OrderResults.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1OrderResults.Size = New System.Drawing.Size(924, 78)
        Me.C1OrderResults.StyleInfo = resources.GetString("C1OrderResults.StyleInfo")
        Me.C1OrderResults.TabIndex = 18
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label21.Location = New System.Drawing.Point(4, 79)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(924, 1)
        Me.Label21.TabIndex = 10
        Me.Label21.Text = "label2"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(3, 1)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 79)
        Me.Label22.TabIndex = 9
        Me.Label22.Text = "label4"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(928, 1)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 79)
        Me.Label23.TabIndex = 8
        Me.Label23.Text = "label3"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(3, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(926, 1)
        Me.Label24.TabIndex = 7
        Me.Label24.Text = "label1"
        '
        'sptpnlC1General
        '
        Me.sptpnlC1General.BackColor = System.Drawing.Color.FromArgb(CType(CType(159, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.sptpnlC1General.Dock = System.Windows.Forms.DockStyle.Top
        Me.sptpnlC1General.Location = New System.Drawing.Point(0, 823)
        Me.sptpnlC1General.Name = "sptpnlC1General"
        Me.sptpnlC1General.Size = New System.Drawing.Size(932, 3)
        Me.sptpnlC1General.TabIndex = 232
        Me.sptpnlC1General.TabStop = False
        '
        'pnlPatientDetails
        '
        Me.pnlPatientDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlPatientDetails.Controls.Add(Me.lblPatientDetails)
        Me.pnlPatientDetails.Controls.Add(Me.Label25)
        Me.pnlPatientDetails.Controls.Add(Me.Label26)
        Me.pnlPatientDetails.Controls.Add(Me.Label27)
        Me.pnlPatientDetails.Controls.Add(Me.Label28)
        Me.pnlPatientDetails.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPatientDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlPatientDetails.Location = New System.Drawing.Point(0, 60)
        Me.pnlPatientDetails.Name = "pnlPatientDetails"
        Me.pnlPatientDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlPatientDetails.Size = New System.Drawing.Size(932, 35)
        Me.pnlPatientDetails.TabIndex = 233
        Me.pnlPatientDetails.Visible = False
        '
        'lblPatientDetails
        '
        Me.lblPatientDetails.AutoSize = True
        Me.lblPatientDetails.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientDetails.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblPatientDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPatientDetails.Location = New System.Drawing.Point(12, 10)
        Me.lblPatientDetails.Name = "lblPatientDetails"
        Me.lblPatientDetails.Size = New System.Drawing.Size(103, 16)
        Me.lblPatientDetails.TabIndex = 10
        Me.lblPatientDetails.Text = "Patient Details"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(4, 31)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(924, 1)
        Me.Label25.TabIndex = 7
        Me.Label25.Text = "label4"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(4, 3)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(924, 1)
        Me.Label26.TabIndex = 6
        Me.Label26.Text = "label4"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(928, 3)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 29)
        Me.Label27.TabIndex = 5
        Me.Label27.Text = "label4"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(3, 3)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 29)
        Me.Label28.TabIndex = 4
        Me.Label28.Text = "label4"
        '
        'frmViewCQMCriteriaReason
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(932, 909)
        Me.Controls.Add(Me.pnlC1OrderResults)
        Me.Controls.Add(Me.sptpnlC1General)
        Me.Controls.Add(Me.pnlC1General)
        Me.Controls.Add(Me.sptpnlC1Immunization)
        Me.Controls.Add(Me.pnlC1Immunization)
        Me.Controls.Add(Me.sptpnlC1Medication)
        Me.Controls.Add(Me.pnlC1Medication)
        Me.Controls.Add(Me.sptpnlC1Vitals)
        Me.Controls.Add(Me.pnlC1Vitals)
        Me.Controls.Add(Me.pnlMainHeader)
        Me.Controls.Add(Me.sptpnlC1NotNumerator)
        Me.Controls.Add(Me.pnlC1NotNumerator)
        Me.Controls.Add(Me.pnlPatientDetails)
        Me.Controls.Add(Me.pnlToolstrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmViewCQMCriteriaReason"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "CQM Details"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.C1NotNumerator, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Vitals, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Medication, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1Immunization, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1General, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlToolstrip.ResumeLayout(False)
        Me.tls_Main.ResumeLayout(False)
        Me.tls_Main.PerformLayout()
        Me.pnlC1NotNumerator.ResumeLayout(False)
        Me.pnlDarkHeader.ResumeLayout(False)
        Me.pnlMid.ResumeLayout(False)
        Me.pnlC1Vitals.ResumeLayout(False)
        Me.pnlMainHeader.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlC1Medication.ResumeLayout(False)
        Me.pnlC1Immunization.ResumeLayout(False)
        Me.pnlC1General.ResumeLayout(False)
        Me.pnlC1OrderResults.ResumeLayout(False)
        CType(Me.C1OrderResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPatientDetails.ResumeLayout(False)
        Me.pnlPatientDetails.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents C1NotNumerator As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents FlowLayoutPnlNumDeno As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents C1FlexGrid1 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents C1FlexGrid2 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents C1Vitals As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents C1Medication As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents C1Immunization As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents C1General As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlToolstrip As System.Windows.Forms.Panel
    Friend WithEvents tls_Main As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlsbtn_Cancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlC1NotNumerator As System.Windows.Forms.Panel
    Friend WithEvents pnlDarkHeader As System.Windows.Forms.Panel
    Friend WithEvents pnlMid As System.Windows.Forms.Panel
    Private WithEvents Label107 As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Private WithEvents lblGridBottom As System.Windows.Forms.Label
    Private WithEvents lblGridLeft As System.Windows.Forms.Label
    Private WithEvents lblGridRight As System.Windows.Forms.Label
    Private WithEvents lblGridTop As System.Windows.Forms.Label
    Friend WithEvents sptpnlC1NotNumerator As System.Windows.Forms.Splitter
    Friend WithEvents pnlC1Vitals As System.Windows.Forms.Panel
    Friend WithEvents pnlMainHeader As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblCriteriaReason As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents pnlC1Medication As System.Windows.Forms.Panel
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents sptpnlC1Vitals As System.Windows.Forms.Splitter
    Friend WithEvents pnlC1Immunization As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents sptpnlC1Medication As System.Windows.Forms.Splitter
    Friend WithEvents sptpnlC1Immunization As System.Windows.Forms.Splitter
    Friend WithEvents pnlC1General As System.Windows.Forms.Panel
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents pnlC1OrderResults As System.Windows.Forms.Panel
    Friend WithEvents C1OrderResults As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents sptpnlC1General As System.Windows.Forms.Splitter
    Friend WithEvents pnlPatientDetails As System.Windows.Forms.Panel
    Friend WithEvents lblPatientDetails As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
End Class
