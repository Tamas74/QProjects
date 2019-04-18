<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_MU_Dashboard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtpicStartDate, dtpicEndDate}
            Dim cntControls() As System.Windows.Forms.Control = {dtpicStartDate, dtpicEndDate}

            components.Dispose()

            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try


            If (IsNothing(dtpControls) = False) Then
                If dtpControls.Length > 0 Then
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                End If
            End If


            If (IsNothing(cntControls) = False) Then
                If cntControls.Length > 0 Then
                    gloGlobal.cEventHelper.DisposeAllControls(cntControls)
                End If
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_MU_Dashboard))
        Me.tlb_MUDashboard = New System.Windows.Forms.ToolStrip
        Me.tlbbtn_DeelectAll = New System.Windows.Forms.ToolStripButton
        Me.tlbbtn_SelectAll = New System.Windows.Forms.ToolStripButton
        Me.tlbbtn_Export = New System.Windows.Forms.ToolStripButton
        Me.tlbbtn_Close = New System.Windows.Forms.ToolStripButton
        Me.pnlC1Grid = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label12 = New System.Windows.Forms.Label
        Me.C1QualityMeasures = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.pnl = New System.Windows.Forms.Panel
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.pnlMeasurementPeriod = New System.Windows.Forms.Panel
        Me.dtpicEndDate = New System.Windows.Forms.DateTimePicker
        Me.dtpicStartDate = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.chk_FirstYear = New System.Windows.Forms.CheckBox
        Me.cmb_RptYear = New System.Windows.Forms.ComboBox
        Me.cmb_Provider = New System.Windows.Forms.ComboBox
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.ImgFlag = New System.Windows.Forms.ImageList(Me.components)
        Me.tlb_MUDashboard.SuspendLayout()
        Me.pnlC1Grid.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.C1QualityMeasures, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl.SuspendLayout()
        Me.pnlMeasurementPeriod.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlb_MUDashboard
        '
        Me.tlb_MUDashboard.BackColor = System.Drawing.Color.Transparent
        Me.tlb_MUDashboard.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tlb_MUDashboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlb_MUDashboard.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_MUDashboard.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlb_MUDashboard.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtn_DeelectAll, Me.tlbbtn_SelectAll, Me.tlbbtn_Export, Me.tlbbtn_Close})
        Me.tlb_MUDashboard.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlb_MUDashboard.Location = New System.Drawing.Point(0, 0)
        Me.tlb_MUDashboard.Name = "tlb_MUDashboard"
        Me.tlb_MUDashboard.Size = New System.Drawing.Size(1001, 53)
        Me.tlb_MUDashboard.TabIndex = 1
        Me.tlb_MUDashboard.Text = "toolStrip1"
        '
        'tlbbtn_DeelectAll
        '
        Me.tlbbtn_DeelectAll.Image = CType(resources.GetObject("tlbbtn_DeelectAll.Image"), System.Drawing.Image)
        Me.tlbbtn_DeelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_DeelectAll.Name = "tlbbtn_DeelectAll"
        Me.tlbbtn_DeelectAll.Size = New System.Drawing.Size(41, 50)
        Me.tlbbtn_DeelectAll.Tag = "Print"
        Me.tlbbtn_DeelectAll.Text = "&Print"
        Me.tlbbtn_DeelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_SelectAll
        '
        Me.tlbbtn_SelectAll.Image = CType(resources.GetObject("tlbbtn_SelectAll.Image"), System.Drawing.Image)
        Me.tlbbtn_SelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_SelectAll.Name = "tlbbtn_SelectAll"
        Me.tlbbtn_SelectAll.Size = New System.Drawing.Size(40, 50)
        Me.tlbbtn_SelectAll.Tag = "Save"
        Me.tlbbtn_SelectAll.Text = "&Save"
        Me.tlbbtn_SelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_Export
        '
        Me.tlbbtn_Export.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Export.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_Export.Image = CType(resources.GetObject("tlbbtn_Export.Image"), System.Drawing.Image)
        Me.tlbbtn_Export.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Export.Name = "tlbbtn_Export"
        Me.tlbbtn_Export.Size = New System.Drawing.Size(66, 50)
        Me.tlbbtn_Export.Tag = "Save&Close"
        Me.tlbbtn_Export.Text = "Sa&ve&&Cls"
        Me.tlbbtn_Export.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_Export.ToolTipText = "Save and Close"
        '
        'tlbbtn_Close
        '
        Me.tlbbtn_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Close.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_Close.Image = CType(resources.GetObject("tlbbtn_Close.Image"), System.Drawing.Image)
        Me.tlbbtn_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Close.Name = "tlbbtn_Close"
        Me.tlbbtn_Close.Size = New System.Drawing.Size(43, 50)
        Me.tlbbtn_Close.Tag = "Close"
        Me.tlbbtn_Close.Text = "&Close"
        Me.tlbbtn_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlC1Grid
        '
        Me.pnlC1Grid.Controls.Add(Me.Panel1)
        Me.pnlC1Grid.Controls.Add(Me.pnl)
        Me.pnlC1Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlC1Grid.Location = New System.Drawing.Point(0, 53)
        Me.pnlC1Grid.Name = "pnlC1Grid"
        Me.pnlC1Grid.Size = New System.Drawing.Size(1001, 611)
        Me.pnlC1Grid.TabIndex = 109
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.C1QualityMeasures)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 67)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(1001, 544)
        Me.Panel1.TabIndex = 111
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Location = New System.Drawing.Point(997, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 539)
        Me.Label12.TabIndex = 116
        '
        'C1QualityMeasures
        '
        Me.C1QualityMeasures.BackColor = System.Drawing.Color.GhostWhite
        Me.C1QualityMeasures.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1QualityMeasures.ColumnInfo = "1,0,0,0,0,95,Columns:"
        Me.C1QualityMeasures.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1QualityMeasures.ExtendLastCol = True
        Me.C1QualityMeasures.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1QualityMeasures.Location = New System.Drawing.Point(4, 1)
        Me.C1QualityMeasures.Name = "C1QualityMeasures"
        Me.C1QualityMeasures.Rows.Count = 1
        Me.C1QualityMeasures.Rows.DefaultSize = 19
        Me.C1QualityMeasures.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1QualityMeasures.Size = New System.Drawing.Size(994, 539)
        Me.C1QualityMeasures.StyleInfo = resources.GetString("C1QualityMeasures.StyleInfo")
        Me.C1QualityMeasures.TabIndex = 105
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Location = New System.Drawing.Point(3, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 539)
        Me.Label11.TabIndex = 115
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Location = New System.Drawing.Point(3, 540)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(995, 1)
        Me.Label9.TabIndex = 114
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Location = New System.Drawing.Point(3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(995, 1)
        Me.Label8.TabIndex = 113
        '
        'pnl
        '
        Me.pnl.Controls.Add(Me.Label14)
        Me.pnl.Controls.Add(Me.Label13)
        Me.pnl.Controls.Add(Me.Label7)
        Me.pnl.Controls.Add(Me.Label6)
        Me.pnl.Controls.Add(Me.Label5)
        Me.pnl.Controls.Add(Me.pnlMeasurementPeriod)
        Me.pnl.Controls.Add(Me.Label4)
        Me.pnl.Controls.Add(Me.Label3)
        Me.pnl.Controls.Add(Me.chk_FirstYear)
        Me.pnl.Controls.Add(Me.cmb_RptYear)
        Me.pnl.Controls.Add(Me.cmb_Provider)
        Me.pnl.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl.Location = New System.Drawing.Point(0, 0)
        Me.pnl.Name = "pnl"
        Me.pnl.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl.Size = New System.Drawing.Size(1001, 67)
        Me.pnl.TabIndex = 110
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Location = New System.Drawing.Point(997, 4)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 59)
        Me.Label14.TabIndex = 117
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Location = New System.Drawing.Point(3, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 59)
        Me.Label13.TabIndex = 116
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Location = New System.Drawing.Point(3, 63)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(995, 1)
        Me.Label7.TabIndex = 113
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Location = New System.Drawing.Point(3, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(995, 1)
        Me.Label6.TabIndex = 112
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(809, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(186, 14)
        Me.Label5.TabIndex = 111
        Me.Label5.Text = "Reporting Period In Progress"
        '
        'pnlMeasurementPeriod
        '
        Me.pnlMeasurementPeriod.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMeasurementPeriod.Controls.Add(Me.dtpicEndDate)
        Me.pnlMeasurementPeriod.Controls.Add(Me.dtpicStartDate)
        Me.pnlMeasurementPeriod.Controls.Add(Me.Label1)
        Me.pnlMeasurementPeriod.Controls.Add(Me.Label10)
        Me.pnlMeasurementPeriod.Controls.Add(Me.Label2)
        Me.pnlMeasurementPeriod.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlMeasurementPeriod.Location = New System.Drawing.Point(366, 7)
        Me.pnlMeasurementPeriod.Name = "pnlMeasurementPeriod"
        Me.pnlMeasurementPeriod.Size = New System.Drawing.Size(427, 31)
        Me.pnlMeasurementPeriod.TabIndex = 110
        '
        'dtpicEndDate
        '
        Me.dtpicEndDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpicEndDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpicEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpicEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpicEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpicEndDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpicEndDate.Enabled = False
        Me.dtpicEndDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpicEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpicEndDate.Location = New System.Drawing.Point(322, 4)
        Me.dtpicEndDate.Name = "dtpicEndDate"
        Me.dtpicEndDate.Size = New System.Drawing.Size(98, 22)
        Me.dtpicEndDate.TabIndex = 10
        '
        'dtpicStartDate
        '
        Me.dtpicStartDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpicStartDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpicStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpicStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpicStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpicStartDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpicStartDate.Enabled = False
        Me.dtpicStartDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpicStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpicStartDate.Location = New System.Drawing.Point(191, 4)
        Me.dtpicStartDate.Name = "dtpicStartDate"
        Me.dtpicStartDate.Size = New System.Drawing.Size(98, 22)
        Me.dtpicStartDate.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(16, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 14)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Measurement Period :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(148, 8)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(38, 14)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "From "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(294, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 14)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "To "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(28, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 14)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Reporting Year :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(61, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 14)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Providers :"
        '
        'chk_FirstYear
        '
        Me.chk_FirstYear.AutoSize = True
        Me.chk_FirstYear.BackColor = System.Drawing.Color.Transparent
        Me.chk_FirstYear.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_FirstYear.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.chk_FirstYear.Location = New System.Drawing.Point(286, 14)
        Me.chk_FirstYear.Name = "chk_FirstYear"
        Me.chk_FirstYear.Size = New System.Drawing.Size(77, 18)
        Me.chk_FirstYear.TabIndex = 3
        Me.chk_FirstYear.Text = "First Year"
        Me.chk_FirstYear.UseVisualStyleBackColor = False
        '
        'cmb_RptYear
        '
        Me.cmb_RptYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_RptYear.FormattingEnabled = True
        Me.cmb_RptYear.Location = New System.Drawing.Point(128, 35)
        Me.cmb_RptYear.Name = "cmb_RptYear"
        Me.cmb_RptYear.Size = New System.Drawing.Size(140, 22)
        Me.cmb_RptYear.TabIndex = 1
        '
        'cmb_Provider
        '
        Me.cmb_Provider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Provider.FormattingEnabled = True
        Me.cmb_Provider.Location = New System.Drawing.Point(128, 10)
        Me.cmb_Provider.Name = "cmb_Provider"
        Me.cmb_Provider.Size = New System.Drawing.Size(140, 22)
        Me.cmb_Provider.TabIndex = 0
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'ImgFlag
        '
        Me.ImgFlag.ImageStream = CType(resources.GetObject("ImgFlag.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgFlag.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgFlag.Images.SetKeyName(0, "Exclamation.png")
        Me.ImgFlag.Images.SetKeyName(1, "Information.png")
        Me.ImgFlag.Images.SetKeyName(2, "FlagRed.png")
        '
        'frm_MU_Dashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1001, 664)
        Me.Controls.Add(Me.pnlC1Grid)
        Me.Controls.Add(Me.tlb_MUDashboard)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_MU_Dashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MU Dashboard"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tlb_MUDashboard.ResumeLayout(False)
        Me.tlb_MUDashboard.PerformLayout()
        Me.pnlC1Grid.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.C1QualityMeasures, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl.ResumeLayout(False)
        Me.pnl.PerformLayout()
        Me.pnlMeasurementPeriod.ResumeLayout(False)
        Me.pnlMeasurementPeriod.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents tlb_MUDashboard As System.Windows.Forms.ToolStrip
    Friend WithEvents tlbbtn_SelectAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_DeelectAll As System.Windows.Forms.ToolStripButton
    Private WithEvents tlbbtn_Export As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlC1Grid As System.Windows.Forms.Panel
    Friend WithEvents C1QualityMeasures As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnl As System.Windows.Forms.Panel
    Friend WithEvents cmb_Provider As System.Windows.Forms.ComboBox
    Friend WithEvents chk_FirstYear As System.Windows.Forms.CheckBox
    Friend WithEvents cmb_RptYear As System.Windows.Forms.ComboBox
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pnlMeasurementPeriod As System.Windows.Forms.Panel
    Friend WithEvents dtpicStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpicEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ImgFlag As System.Windows.Forms.ImageList

    
End Class
