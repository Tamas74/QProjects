<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHxForecastReconcileList
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

                Dim dtpContextMenuStrip As ContextMenuStrip() = {cntListType}
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpContextMenuStrip)
                    gloGlobal.cEventHelper.DisposeAllControls(dtpContextMenuStrip)
                Catch ex As Exception

                End Try
                Try
                    If (IsNothing(gloUC_PatientStrip1) = False) Then
                        gloUC_PatientStrip1.Dispose()
                        gloUC_PatientStrip1 = Nothing
                    End If
                Catch ex As Exception

                End Try


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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHxForecastReconcileList))
        Me.panToolstrip = New System.Windows.Forms.Panel()
        Me.tsTop = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbbtn_Finalize = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cmbListType = New System.Windows.Forms.ComboBox()
        Me.lblListType = New System.Windows.Forms.Label()
        Me.pnlReconciliation = New System.Windows.Forms.Panel()
        Me.c1Reconciliation = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.chkHideCurrentData = New System.Windows.Forms.CheckBox()
        Me.lblReconciliationType = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblBottomNote = New System.Windows.Forms.Label()
        Me.pnlBottomNote = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.ImgGrid = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.pnlConsolidatedList = New System.Windows.Forms.Panel()
        Me.C1ConsolidatedList = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkSelectAll = New System.Windows.Forms.CheckBox()
        Me.lblConsolidated = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.cntListType = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuMarkasReady = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMarkedFinished = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectAllToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.panToolstrip.SuspendLayout()
        Me.tsTop.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlReconciliation.SuspendLayout()
        CType(Me.c1Reconciliation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.pnlBottomNote.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlConsolidatedList.SuspendLayout()
        CType(Me.C1ConsolidatedList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.cntListType.SuspendLayout()
        Me.SuspendLayout()
        '
        'panToolstrip
        '
        Me.panToolstrip.Controls.Add(Me.tsTop)
        Me.panToolstrip.Dock = System.Windows.Forms.DockStyle.Top
        Me.panToolstrip.Location = New System.Drawing.Point(0, 0)
        Me.panToolstrip.Name = "panToolstrip"
        Me.panToolstrip.Size = New System.Drawing.Size(1168, 56)
        Me.panToolstrip.TabIndex = 3
        '
        'tsTop
        '
        Me.tsTop.BackgroundImage = CType(resources.GetObject("tsTop.BackgroundImage"), System.Drawing.Image)
        Me.tsTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tsTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tsTop.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tsTop.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtn_Finalize, Me.tlbbtn_Close})
        Me.tsTop.Location = New System.Drawing.Point(0, 0)
        Me.tsTop.Name = "tsTop"
        Me.tsTop.Size = New System.Drawing.Size(1168, 53)
        Me.tsTop.TabIndex = 0
        Me.tsTop.TabStop = True
        Me.tsTop.Text = "ToolStrip1"
        '
        'tlbbtn_Finalize
        '
        Me.tlbbtn_Finalize.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Finalize.Image = CType(resources.GetObject("tlbbtn_Finalize.Image"), System.Drawing.Image)
        Me.tlbbtn_Finalize.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Finalize.Name = "tlbbtn_Finalize"
        Me.tlbbtn_Finalize.Size = New System.Drawing.Size(54, 50)
        Me.tlbbtn_Finalize.Tag = "Finalize"
        Me.tlbbtn_Finalize.Text = "&Finalize"
        Me.tlbbtn_Finalize.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_Close
        '
        Me.tlbbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Close.Image = CType(resources.GetObject("tlbbtn_Close.Image"), System.Drawing.Image)
        Me.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Close.Name = "tlbbtn_Close"
        Me.tlbbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tlbbtn_Close.Tag = "Close"
        Me.tlbbtn_Close.Text = "&Close"
        Me.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.Label19)
        Me.Panel3.Controls.Add(Me.Label22)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(4, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1160, 25)
        Me.Panel3.TabIndex = 1
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.White
        Me.Label19.Location = New System.Drawing.Point(0, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.Label19.Size = New System.Drawing.Size(1160, 24)
        Me.Label19.TabIndex = 43
        Me.Label19.Text = "1. Select Type of Information to Reconcile [Immunization History, Immunization Fo" & _
    "recast]"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label22.Location = New System.Drawing.Point(0, 24)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1160, 1)
        Me.Label22.TabIndex = 35
        Me.Label22.Text = "label1"
        '
        'cmbListType
        '
        Me.cmbListType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbListType.FormattingEnabled = True
        Me.cmbListType.Location = New System.Drawing.Point(127, 31)
        Me.cmbListType.Name = "cmbListType"
        Me.cmbListType.Size = New System.Drawing.Size(165, 22)
        Me.cmbListType.TabIndex = 1
        '
        'lblListType
        '
        Me.lblListType.AutoSize = True
        Me.lblListType.Location = New System.Drawing.Point(22, 35)
        Me.lblListType.Name = "lblListType"
        Me.lblListType.Size = New System.Drawing.Size(103, 14)
        Me.lblListType.TabIndex = 41
        Me.lblListType.Text = "Select List Type :"
        '
        'pnlReconciliation
        '
        Me.pnlReconciliation.Controls.Add(Me.c1Reconciliation)
        Me.pnlReconciliation.Controls.Add(Me.Panel5)
        Me.pnlReconciliation.Controls.Add(Me.Label2)
        Me.pnlReconciliation.Controls.Add(Me.Label6)
        Me.pnlReconciliation.Controls.Add(Me.Label7)
        Me.pnlReconciliation.Controls.Add(Me.Label8)
        Me.pnlReconciliation.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlReconciliation.Location = New System.Drawing.Point(0, 63)
        Me.pnlReconciliation.Name = "pnlReconciliation"
        Me.pnlReconciliation.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.pnlReconciliation.Size = New System.Drawing.Size(1168, 256)
        Me.pnlReconciliation.TabIndex = 2
        '
        'c1Reconciliation
        '
        Me.c1Reconciliation.AllowEditing = False
        Me.c1Reconciliation.BackColor = System.Drawing.Color.White
        Me.c1Reconciliation.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1Reconciliation.ColumnInfo = "0,0,0,0,0,105,Columns:"
        Me.c1Reconciliation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1Reconciliation.EditOptions = C1.Win.C1FlexGrid.EditFlags.None
        Me.c1Reconciliation.ExtendLastCol = True
        Me.c1Reconciliation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1Reconciliation.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus
        Me.c1Reconciliation.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.c1Reconciliation.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.c1Reconciliation.Location = New System.Drawing.Point(4, 26)
        Me.c1Reconciliation.Name = "c1Reconciliation"
        Me.c1Reconciliation.Rows.Count = 5
        Me.c1Reconciliation.Rows.DefaultSize = 21
        Me.c1Reconciliation.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1Reconciliation.ShowCellLabels = True
        Me.c1Reconciliation.Size = New System.Drawing.Size(1160, 229)
        Me.c1Reconciliation.StyleInfo = resources.GetString("c1Reconciliation.StyleInfo")
        Me.c1Reconciliation.TabIndex = 0
        Me.c1Reconciliation.Tag = "ReconcileList"
        '
        'Panel5
        '
        Me.Panel5.BackgroundImage = CType(resources.GetObject("Panel5.BackgroundImage"), System.Drawing.Image)
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.chkHideCurrentData)
        Me.Panel5.Controls.Add(Me.lblReconciliationType)
        Me.Panel5.Controls.Add(Me.Label16)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.ForeColor = System.Drawing.Color.White
        Me.Panel5.Location = New System.Drawing.Point(4, 1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1160, 25)
        Me.Panel5.TabIndex = 43
        '
        'chkHideCurrentData
        '
        Me.chkHideCurrentData.AutoSize = True
        Me.chkHideCurrentData.BackColor = System.Drawing.Color.Transparent
        Me.chkHideCurrentData.Dock = System.Windows.Forms.DockStyle.Right
        Me.chkHideCurrentData.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHideCurrentData.ForeColor = System.Drawing.Color.White
        Me.chkHideCurrentData.Location = New System.Drawing.Point(996, 0)
        Me.chkHideCurrentData.Name = "chkHideCurrentData"
        Me.chkHideCurrentData.Size = New System.Drawing.Size(164, 24)
        Me.chkHideCurrentData.TabIndex = 44
        Me.chkHideCurrentData.Text = "Hide Patient's Records"
        Me.chkHideCurrentData.UseVisualStyleBackColor = False
        '
        'lblReconciliationType
        '
        Me.lblReconciliationType.BackColor = System.Drawing.Color.Transparent
        Me.lblReconciliationType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblReconciliationType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReconciliationType.Location = New System.Drawing.Point(0, 0)
        Me.lblReconciliationType.Name = "lblReconciliationType"
        Me.lblReconciliationType.Padding = New System.Windows.Forms.Padding(4, 3, 0, 0)
        Me.lblReconciliationType.Size = New System.Drawing.Size(1160, 24)
        Me.lblReconciliationType.TabIndex = 43
        Me.lblReconciliationType.Text = "2. View the selected lists. Each separate list is designated by its name. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.lblReconciliationType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label16.Location = New System.Drawing.Point(0, 24)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1160, 1)
        Me.Label16.TabIndex = 35
        Me.Label16.Text = "label1"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label2.Location = New System.Drawing.Point(1164, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 254)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "label4"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Location = New System.Drawing.Point(3, 1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1, 254)
        Me.Label6.TabIndex = 40
        Me.Label6.Text = "label4"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Location = New System.Drawing.Point(3, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1162, 1)
        Me.Label7.TabIndex = 37
        Me.Label7.Text = "label1"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Location = New System.Drawing.Point(3, 255)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1162, 1)
        Me.Label8.TabIndex = 36
        Me.Label8.Text = "label1"
        '
        'lblBottomNote
        '
        Me.lblBottomNote.AutoSize = True
        Me.lblBottomNote.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.lblBottomNote.ForeColor = System.Drawing.Color.Red
        Me.lblBottomNote.Location = New System.Drawing.Point(304, 35)
        Me.lblBottomNote.Name = "lblBottomNote"
        Me.lblBottomNote.Size = New System.Drawing.Size(315, 14)
        Me.lblBottomNote.TabIndex = 41
        Me.lblBottomNote.Text = "Patient has Immunization History, Immunization Forecast"
        Me.lblBottomNote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlBottomNote
        '
        Me.pnlBottomNote.Controls.Add(Me.lblBottomNote)
        Me.pnlBottomNote.Controls.Add(Me.cmbListType)
        Me.pnlBottomNote.Controls.Add(Me.Panel3)
        Me.pnlBottomNote.Controls.Add(Me.lblListType)
        Me.pnlBottomNote.Controls.Add(Me.Label9)
        Me.pnlBottomNote.Controls.Add(Me.Label15)
        Me.pnlBottomNote.Controls.Add(Me.Label23)
        Me.pnlBottomNote.Controls.Add(Me.Label24)
        Me.pnlBottomNote.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlBottomNote.Location = New System.Drawing.Point(0, 0)
        Me.pnlBottomNote.Name = "pnlBottomNote"
        Me.pnlBottomNote.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlBottomNote.Size = New System.Drawing.Size(1168, 59)
        Me.pnlBottomNote.TabIndex = 0
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label9.Location = New System.Drawing.Point(1164, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 51)
        Me.Label9.TabIndex = 41
        Me.Label9.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Location = New System.Drawing.Point(3, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 51)
        Me.Label15.TabIndex = 40
        Me.Label15.Text = "label4"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label23.Location = New System.Drawing.Point(3, 3)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1162, 1)
        Me.Label23.TabIndex = 37
        Me.Label23.Text = "label1"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label24.Location = New System.Drawing.Point(3, 55)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1162, 1)
        Me.Label24.TabIndex = 36
        Me.Label24.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 59)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(1168, 4)
        Me.Splitter1.TabIndex = 4
        Me.Splitter1.TabStop = False
        '
        'ImgGrid
        '
        Me.ImgGrid.ImageStream = CType(resources.GetObject("ImgGrid.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgGrid.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgGrid.Images.SetKeyName(0, "Duplicate Records.ico")
        Me.ImgGrid.Images.SetKeyName(1, "Img_Status_Alert.ico")
        Me.ImgGrid.Images.SetKeyName(2, "Img_Status_Pending01.ico")
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlConsolidatedList)
        Me.pnlMain.Controls.Add(Me.Splitter2)
        Me.pnlMain.Controls.Add(Me.pnlReconciliation)
        Me.pnlMain.Controls.Add(Me.Splitter1)
        Me.pnlMain.Controls.Add(Me.pnlBottomNote)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 56)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(1168, 634)
        Me.pnlMain.TabIndex = 5
        '
        'pnlConsolidatedList
        '
        Me.pnlConsolidatedList.Controls.Add(Me.C1ConsolidatedList)
        Me.pnlConsolidatedList.Controls.Add(Me.Panel2)
        Me.pnlConsolidatedList.Controls.Add(Me.Label12)
        Me.pnlConsolidatedList.Controls.Add(Me.Label13)
        Me.pnlConsolidatedList.Controls.Add(Me.Label14)
        Me.pnlConsolidatedList.Controls.Add(Me.Label17)
        Me.pnlConsolidatedList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlConsolidatedList.Location = New System.Drawing.Point(0, 323)
        Me.pnlConsolidatedList.Name = "pnlConsolidatedList"
        Me.pnlConsolidatedList.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlConsolidatedList.Size = New System.Drawing.Size(1168, 311)
        Me.pnlConsolidatedList.TabIndex = 5
        '
        'C1ConsolidatedList
        '
        Me.C1ConsolidatedList.BackColor = System.Drawing.Color.White
        Me.C1ConsolidatedList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1ConsolidatedList.ColumnInfo = "0,0,0,0,0,105,Columns:"
        Me.C1ConsolidatedList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1ConsolidatedList.ExtendLastCol = True
        Me.C1ConsolidatedList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1ConsolidatedList.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1ConsolidatedList.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut
        Me.C1ConsolidatedList.Location = New System.Drawing.Point(4, 26)
        Me.C1ConsolidatedList.Name = "C1ConsolidatedList"
        Me.C1ConsolidatedList.Rows.Count = 5
        Me.C1ConsolidatedList.Rows.DefaultSize = 21
        Me.C1ConsolidatedList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1ConsolidatedList.ShowCellLabels = True
        Me.C1ConsolidatedList.Size = New System.Drawing.Size(1160, 281)
        Me.C1ConsolidatedList.StyleInfo = resources.GetString("C1ConsolidatedList.StyleInfo")
        Me.C1ConsolidatedList.TabIndex = 0
        Me.C1ConsolidatedList.Tag = "ConsolidatedList"
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.chkSelectAll)
        Me.Panel2.Controls.Add(Me.lblConsolidated)
        Me.Panel2.Controls.Add(Me.Label11)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.ForeColor = System.Drawing.Color.White
        Me.Panel2.Location = New System.Drawing.Point(4, 1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1160, 25)
        Me.Panel2.TabIndex = 43
        '
        'chkSelectAll
        '
        Me.chkSelectAll.BackColor = System.Drawing.Color.Transparent
        Me.chkSelectAll.Dock = System.Windows.Forms.DockStyle.Right
        Me.chkSelectAll.Location = New System.Drawing.Point(1142, 0)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(18, 24)
        Me.chkSelectAll.TabIndex = 93
        Me.SelectAllToolTip.SetToolTip(Me.chkSelectAll, "Select All / Deselect All")
        Me.chkSelectAll.UseVisualStyleBackColor = False
        '
        'lblConsolidated
        '
        Me.lblConsolidated.BackColor = System.Drawing.Color.Transparent
        Me.lblConsolidated.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblConsolidated.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConsolidated.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblConsolidated.Location = New System.Drawing.Point(0, 0)
        Me.lblConsolidated.Name = "lblConsolidated"
        Me.lblConsolidated.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.lblConsolidated.Size = New System.Drawing.Size(1160, 24)
        Me.lblConsolidated.TabIndex = 43
        Me.lblConsolidated.Text = "3. Review the consolidated display. Unselect any unneeded items. Then press ‘Fina" & _
    "lize’. The system has already removed obvious duplicates and warns you about sim" & _
    "ilar items to review."
        Me.lblConsolidated.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Location = New System.Drawing.Point(0, 24)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1160, 1)
        Me.Label11.TabIndex = 35
        Me.Label11.Text = "label1"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Location = New System.Drawing.Point(1164, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 306)
        Me.Label12.TabIndex = 41
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Location = New System.Drawing.Point(3, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 306)
        Me.Label13.TabIndex = 40
        Me.Label13.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Location = New System.Drawing.Point(3, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1162, 1)
        Me.Label14.TabIndex = 37
        Me.Label14.Text = "label1"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Location = New System.Drawing.Point(3, 307)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1162, 1)
        Me.Label17.TabIndex = 36
        Me.Label17.Text = "label1"
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(0, 319)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(1168, 4)
        Me.Splitter2.TabIndex = 6
        Me.Splitter2.TabStop = False
        '
        'cntListType
        '
        Me.cntListType.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMarkasReady, Me.mnuMarkedFinished})
        Me.cntListType.Name = "cntListType"
        Me.cntListType.Size = New System.Drawing.Size(165, 48)
        '
        'mnuMarkasReady
        '
        Me.mnuMarkasReady.Image = CType(resources.GetObject("mnuMarkasReady.Image"), System.Drawing.Image)
        Me.mnuMarkasReady.Name = "mnuMarkasReady"
        Me.mnuMarkasReady.Size = New System.Drawing.Size(164, 22)
        Me.mnuMarkasReady.Text = "Mark As Ready"
        '
        'mnuMarkedFinished
        '
        Me.mnuMarkedFinished.Image = CType(resources.GetObject("mnuMarkedFinished.Image"), System.Drawing.Image)
        Me.mnuMarkedFinished.Name = "mnuMarkedFinished"
        Me.mnuMarkedFinished.Size = New System.Drawing.Size(164, 22)
        Me.mnuMarkedFinished.Text = "Mark As Finished"
        '
        'frmHxForecastReconcileList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1168, 690)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.panToolstrip)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmHxForecastReconcileList"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reconcile List"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.panToolstrip.ResumeLayout(False)
        Me.panToolstrip.PerformLayout()
        Me.tsTop.ResumeLayout(False)
        Me.tsTop.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.pnlReconciliation.ResumeLayout(False)
        CType(Me.c1Reconciliation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.pnlBottomNote.ResumeLayout(False)
        Me.pnlBottomNote.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        Me.pnlConsolidatedList.ResumeLayout(False)
        CType(Me.C1ConsolidatedList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.cntListType.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panToolstrip As System.Windows.Forms.Panel
    Friend WithEvents tsTop As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbbtn_Finalize As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents pnlReconciliation As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbListType As System.Windows.Forms.ComboBox
    Friend WithEvents lblListType As System.Windows.Forms.Label
    Private WithEvents lblBottomNote As System.Windows.Forms.Label
    Friend WithEvents c1Reconciliation As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents pnlBottomNote As System.Windows.Forms.Panel
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lblReconciliationType As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents ImgGrid As System.Windows.Forms.ImageList
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents cntListType As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuMarkasReady As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMarkedFinished As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlConsolidatedList As System.Windows.Forms.Panel
    Friend WithEvents C1ConsolidatedList As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblConsolidated As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents chkSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents SelectAllToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents chkHideCurrentData As System.Windows.Forms.CheckBox
End Class
