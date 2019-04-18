<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_MU_Reports
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                Try
                    If (IsNothing(dtpicStartDate) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpicStartDate)
                        Catch ex As Exception

                        End Try


                        dtpicStartDate.Dispose()
                        dtpicStartDate = Nothing
                    End If
                Catch
                End Try
                Try
                    If (IsNothing(dtpicEndDate) = False) Then
                        Try
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpicEndDate)
                        Catch ex As Exception

                        End Try


                        dtpicEndDate.Dispose()
                        dtpicEndDate = Nothing
                    End If
                Catch
                End Try
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

                components.Dispose()
                Try
                    If (IsNothing(HistoryItemList) = False) Then
                        HistoryItemList.Dispose()
                        HistoryItemList = Nothing
                    End If
                Catch ex As Exception

                End Try
                Try
                    If (IsNothing(NQF0028bHistoryItemList) = False) Then
                        NQF0028bHistoryItemList.Dispose()
                        NQF0028bHistoryItemList = Nothing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_MU_Reports))
        Me.C1QualityMeasures = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnl_tls_PtICD9CPTReport = New System.Windows.Forms.Panel()
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbbtn_SelectAll = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_DeelectAll = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Export = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnShowReportList = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.pnlC1Grid = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.cmbProviders = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpicEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpicStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.pnl = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        CType(Me.C1QualityMeasures, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_tls_PtICD9CPTReport.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlC1Grid.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnl.SuspendLayout()
        Me.SuspendLayout()
        '
        'C1QualityMeasures
        '
        Me.C1QualityMeasures.BackColor = System.Drawing.Color.GhostWhite
        Me.C1QualityMeasures.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1QualityMeasures.ColumnInfo = "1,0,0,0,0,105,Columns:"
        Me.C1QualityMeasures.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1QualityMeasures.ExtendLastCol = True
        Me.C1QualityMeasures.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1QualityMeasures.Location = New System.Drawing.Point(4, 0)
        Me.C1QualityMeasures.Name = "C1QualityMeasures"
        Me.C1QualityMeasures.Rows.Count = 1
        Me.C1QualityMeasures.Rows.DefaultSize = 21
        Me.C1QualityMeasures.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1QualityMeasures.Size = New System.Drawing.Size(914, 465)
        Me.C1QualityMeasures.StyleInfo = resources.GetString("C1QualityMeasures.StyleInfo")
        Me.C1QualityMeasures.TabIndex = 105
        '
        'pnl_tls_PtICD9CPTReport
        '
        Me.pnl_tls_PtICD9CPTReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tls_PtICD9CPTReport.Controls.Add(Me.ToolStrip1)
        Me.pnl_tls_PtICD9CPTReport.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tls_PtICD9CPTReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tls_PtICD9CPTReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tls_PtICD9CPTReport.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tls_PtICD9CPTReport.Name = "pnl_tls_PtICD9CPTReport"
        Me.pnl_tls_PtICD9CPTReport.Size = New System.Drawing.Size(922, 54)
        Me.pnl_tls_PtICD9CPTReport.TabIndex = 107
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStrip1.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtn_SelectAll, Me.tlbbtn_DeelectAll, Me.tlbbtn_Export, Me.tlsbtnShowReportList, Me.tlbbtn_Close})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(922, 53)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "toolStrip1"
        '
        'tlbbtn_SelectAll
        '
        Me.tlbbtn_SelectAll.Image = CType(resources.GetObject("tlbbtn_SelectAll.Image"), System.Drawing.Image)
        Me.tlbbtn_SelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_SelectAll.Name = "tlbbtn_SelectAll"
        Me.tlbbtn_SelectAll.Size = New System.Drawing.Size(67, 50)
        Me.tlbbtn_SelectAll.Tag = "Selectall"
        Me.tlbbtn_SelectAll.Text = "Select &All"
        Me.tlbbtn_SelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_DeelectAll
        '
        Me.tlbbtn_DeelectAll.Image = CType(resources.GetObject("tlbbtn_DeelectAll.Image"), System.Drawing.Image)
        Me.tlbbtn_DeelectAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_DeelectAll.Name = "tlbbtn_DeelectAll"
        Me.tlbbtn_DeelectAll.Size = New System.Drawing.Size(81, 50)
        Me.tlbbtn_DeelectAll.Tag = "Selectall"
        Me.tlbbtn_DeelectAll.Text = "&Deselect All"
        Me.tlbbtn_DeelectAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_Export
        '
        Me.tlbbtn_Export.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Export.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_Export.Image = CType(resources.GetObject("tlbbtn_Export.Image"), System.Drawing.Image)
        Me.tlbbtn_Export.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Export.Name = "tlbbtn_Export"
        Me.tlbbtn_Export.Size = New System.Drawing.Size(99, 50)
        Me.tlbbtn_Export.Tag = "ExpoeReport"
        Me.tlbbtn_Export.Text = "&Export Report"
        Me.tlbbtn_Export.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlsbtnShowReportList
        '
        Me.tlsbtnShowReportList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnShowReportList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsbtnShowReportList.Image = CType(resources.GetObject("tlsbtnShowReportList.Image"), System.Drawing.Image)
        Me.tlsbtnShowReportList.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnShowReportList.Name = "tlsbtnShowReportList"
        Me.tlsbtnShowReportList.Size = New System.Drawing.Size(93, 50)
        Me.tlsbtnShowReportList.Tag = "ShowReportList"
        Me.tlsbtnShowReportList.Text = "&Show Report"
        Me.tlsbtnShowReportList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        Me.pnlC1Grid.Controls.Add(Me.pnl)
        Me.pnlC1Grid.Controls.Add(Me.Panel2)
        Me.pnlC1Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlC1Grid.Location = New System.Drawing.Point(0, 54)
        Me.pnlC1Grid.Name = "pnlC1Grid"
        Me.pnlC1Grid.Size = New System.Drawing.Size(922, 508)
        Me.pnlC1Grid.TabIndex = 108
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel2.Controls.Add(Me.cmbProviders)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.dtpicEndDate)
        Me.Panel2.Controls.Add(Me.dtpicStartDate)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.lblTo)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(922, 40)
        Me.Panel2.TabIndex = 109
        '
        'cmbProviders
        '
        Me.cmbProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProviders.FormattingEnabled = True
        Me.cmbProviders.Location = New System.Drawing.Point(90, 9)
        Me.cmbProviders.Name = "cmbProviders"
        Me.cmbProviders.Size = New System.Drawing.Size(160, 22)
        Me.cmbProviders.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 14)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Providers :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(432, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(146, 14)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Measurement Start Date "
        '
        'dtpicEndDate
        '
        Me.dtpicEndDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpicEndDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpicEndDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpicEndDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpicEndDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpicEndDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpicEndDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpicEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpicEndDate.Location = New System.Drawing.Point(818, 8)
        Me.dtpicEndDate.Name = "dtpicEndDate"
        Me.dtpicEndDate.Size = New System.Drawing.Size(92, 22)
        Me.dtpicEndDate.TabIndex = 4
        '
        'dtpicStartDate
        '
        Me.dtpicStartDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpicStartDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpicStartDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpicStartDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpicStartDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpicStartDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpicStartDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpicStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpicStartDate.Location = New System.Drawing.Point(579, 8)
        Me.dtpicStartDate.Name = "dtpicStartDate"
        Me.dtpicStartDate.Size = New System.Drawing.Size(92, 22)
        Me.dtpicStartDate.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(4, 36)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(914, 1)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "label4"
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.BackColor = System.Drawing.Color.Transparent
        Me.lblTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.Location = New System.Drawing.Point(677, 12)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(140, 14)
        Me.lblTo.TabIndex = 4
        Me.lblTo.Text = "Measurement End Date "
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(914, 1)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label4"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(285, 12)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(141, 14)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Measurement Period :"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(918, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 34)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "label4"
        '
        'lbl_LeftBrd
        '
        Me.lbl_LeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_LeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_LeftBrd.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LeftBrd.Location = New System.Drawing.Point(3, 3)
        Me.lbl_LeftBrd.Name = "lbl_LeftBrd"
        Me.lbl_LeftBrd.Size = New System.Drawing.Size(1, 34)
        Me.lbl_LeftBrd.TabIndex = 4
        Me.lbl_LeftBrd.Text = "label4"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Comment.ico")
        '
        'pnl
        '
        Me.pnl.Controls.Add(Me.Label6)
        Me.pnl.Controls.Add(Me.Label7)
        Me.pnl.Controls.Add(Me.C1QualityMeasures)
        Me.pnl.Controls.Add(Me.Label8)
        Me.pnl.Controls.Add(Me.Label29)
        Me.pnl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl.Location = New System.Drawing.Point(0, 40)
        Me.pnl.Name = "pnl"
        Me.pnl.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnl.Size = New System.Drawing.Size(922, 468)
        Me.pnl.TabIndex = 110
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(4, 464)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(914, 1)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "label1"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(914, 1)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "label1"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 465)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "label1"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(918, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 465)
        Me.Label29.TabIndex = 9
        Me.Label29.Text = "label1"
        '
        'frm_MU_Reports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(922, 562)
        Me.Controls.Add(Me.pnlC1Grid)
        Me.Controls.Add(Me.pnl_tls_PtICD9CPTReport)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_MU_Reports"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Quality Measurments"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.C1QualityMeasures, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_tls_PtICD9CPTReport.ResumeLayout(False)
        Me.pnl_tls_PtICD9CPTReport.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlC1Grid.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnl.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents C1QualityMeasures As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents pnl_tls_PtICD9CPTReport As System.Windows.Forms.Panel
    Private WithEvents ToolStrip1 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbbtn_SelectAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_DeelectAll As System.Windows.Forms.ToolStripButton
    Private WithEvents tlbbtn_Export As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlC1Grid As System.Windows.Forms.Panel
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents dtpicEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpicStartDate As System.Windows.Forms.DateTimePicker
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbProviders As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents tlsbtnShowReportList As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnl As System.Windows.Forms.Panel
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
End Class
