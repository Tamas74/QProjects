<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_MIPSQuality_Reports_2017
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then

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
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try
                components.Dispose()
                Try
                    If (IsNothing(SaveFileDialogXML) = False) Then
                        SaveFileDialogXML.Dispose()
                        SaveFileDialogXML = Nothing
                    End If
                Catch ex As Exception

                End Try
                Try
                    If (IsNothing(FolderBrowserDialog1) = False) Then
                        FolderBrowserDialog1.Dispose()
                        FolderBrowserDialog1 = Nothing
                    End If
                Catch ex As Exception

                End Try
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_MIPSQuality_Reports_2017))
        Me.C1QualityMeasures = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnl_tls_PtICD9CPTReport = New System.Windows.Forms.Panel()
        Me.lblQRDAMsg = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbbtn_SelectAll = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_DeelectAll = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Export = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtnShowReportList = New System.Windows.Forms.ToolStripButton()
        Me.tsp_QRDAIII = New System.Windows.Forms.ToolStripButton()
        Me.tsp_ExportQRDAI = New System.Windows.Forms.ToolStripButton()
        Me.Tblbtn_More = New System.Windows.Forms.ToolStripButton()
        Me.Tblbtn_Hide = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Print = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.pnlC1Grid = New System.Windows.Forms.Panel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.pnlLoadingLable = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.pnlDemoFilter = New System.Windows.Forms.Panel()
        Me.CmbProviderTaxanomy = New System.Windows.Forms.ComboBox()
        Me.btnClearProviderType = New System.Windows.Forms.Button()
        Me.btnClearFilters = New System.Windows.Forms.Button()
        Me.btnBrowseProviderType = New System.Windows.Forms.Button()
        Me.txtTIN = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtNPI = New System.Windows.Forms.TextBox()
        Me.pnlProviderAddress = New System.Windows.Forms.Panel()
        Me.chkPracticeAddress = New System.Windows.Forms.CheckBox()
        Me.pnlAge = New System.Windows.Forms.Panel()
        Me.cmbAgeCriteria = New System.Windows.Forms.ComboBox()
        Me.dtpicAge = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbAge = New System.Windows.Forms.ComboBox()
        Me.chkAge = New System.Windows.Forms.CheckBox()
        Me.btnClearNPIs = New System.Windows.Forms.Button()
        Me.btnBrowseNPIs = New System.Windows.Forms.Button()
        Me.label72 = New System.Windows.Forms.Label()
        Me.btnClearProblems = New System.Windows.Forms.Button()
        Me.btnBrowseProblems = New System.Windows.Forms.Button()
        Me.label64 = New System.Windows.Forms.Label()
        Me.cmbProblems = New System.Windows.Forms.ComboBox()
        Me.btnClearPayers = New System.Windows.Forms.Button()
        Me.btnBrowsePayers = New System.Windows.Forms.Button()
        Me.btnClearTINs = New System.Windows.Forms.Button()
        Me.btnBrowseTINs = New System.Windows.Forms.Button()
        Me.btnclearethnicity = New System.Windows.Forms.Button()
        Me.btnBrowseEthnicity = New System.Windows.Forms.Button()
        Me.btnCleaseRace = New System.Windows.Forms.Button()
        Me.btnBrowseRace = New System.Windows.Forms.Button()
        Me.btncleargender = New System.Windows.Forms.Button()
        Me.btnBrowseGender = New System.Windows.Forms.Button()
        Me.label51 = New System.Windows.Forms.Label()
        Me.cmbPayers = New System.Windows.Forms.ComboBox()
        Me.label50 = New System.Windows.Forms.Label()
        Me.label54 = New System.Windows.Forms.Label()
        Me.cmbethnicity = New System.Windows.Forms.ComboBox()
        Me.cmbGender = New System.Windows.Forms.ComboBox()
        Me.cmbRace = New System.Windows.Forms.ComboBox()
        Me.label55 = New System.Windows.Forms.Label()
        Me.label56 = New System.Windows.Forms.Label()
        Me.label57 = New System.Windows.Forms.Label()
        Me.label58 = New System.Windows.Forms.Label()
        Me.label59 = New System.Windows.Forms.Label()
        Me.label60 = New System.Windows.Forms.Label()
        Me.pnlMeasurementPeriod = New System.Windows.Forms.Panel()
        Me.btnClearProvider = New System.Windows.Forms.Button()
        Me.btnbrprov = New System.Windows.Forms.Button()
        Me.chkCQM2014 = New System.Windows.Forms.CheckBox()
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
        Me.SaveFileDialogXML = New System.Windows.Forms.SaveFileDialog()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.pnlcustomTask = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlPatientDetails = New System.Windows.Forms.Panel()
        Me.lblPatientDetails = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        CType(Me.C1QualityMeasures, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_tls_PtICD9CPTReport.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlC1Grid.SuspendLayout()
        Me.pnlLoadingLable.SuspendLayout()
        Me.pnlDemoFilter.SuspendLayout()
        Me.pnlAge.SuspendLayout()
        Me.pnlMeasurementPeriod.SuspendLayout()
        Me.pnlPatientDetails.SuspendLayout()
        Me.SuspendLayout()
        '
        'C1QualityMeasures
        '
        Me.C1QualityMeasures.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free
        Me.C1QualityMeasures.BackColor = System.Drawing.Color.GhostWhite
        Me.C1QualityMeasures.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1QualityMeasures.ColumnInfo = "1,0,0,0,0,105,Columns:"
        Me.C1QualityMeasures.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1QualityMeasures.ExtendLastCol = True
        Me.C1QualityMeasures.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1QualityMeasures.Location = New System.Drawing.Point(3, 0)
        Me.C1QualityMeasures.Name = "C1QualityMeasures"
        Me.C1QualityMeasures.Rows.Count = 1
        Me.C1QualityMeasures.Rows.DefaultSize = 21
        Me.C1QualityMeasures.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1QualityMeasures.Size = New System.Drawing.Size(1072, 258)
        Me.C1QualityMeasures.StyleInfo = resources.GetString("C1QualityMeasures.StyleInfo")
        Me.C1QualityMeasures.TabIndex = 105
        '
        'pnl_tls_PtICD9CPTReport
        '
        Me.pnl_tls_PtICD9CPTReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tls_PtICD9CPTReport.Controls.Add(Me.lblQRDAMsg)
        Me.pnl_tls_PtICD9CPTReport.Controls.Add(Me.ToolStrip1)
        Me.pnl_tls_PtICD9CPTReport.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tls_PtICD9CPTReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tls_PtICD9CPTReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tls_PtICD9CPTReport.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tls_PtICD9CPTReport.Name = "pnl_tls_PtICD9CPTReport"
        Me.pnl_tls_PtICD9CPTReport.Size = New System.Drawing.Size(1078, 54)
        Me.pnl_tls_PtICD9CPTReport.TabIndex = 107
        '
        'lblQRDAMsg
        '
        Me.lblQRDAMsg.AutoSize = True
        Me.lblQRDAMsg.BackColor = System.Drawing.Color.Transparent
        Me.lblQRDAMsg.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQRDAMsg.Location = New System.Drawing.Point(590, 25)
        Me.lblQRDAMsg.Name = "lblQRDAMsg"
        Me.lblQRDAMsg.Size = New System.Drawing.Size(0, 14)
        Me.lblQRDAMsg.TabIndex = 1
        Me.lblQRDAMsg.Visible = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStrip1.BackgroundImage = Global.gloMU.My.Resources.Resources.Img_Toolstrip
        Me.ToolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ToolStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtn_SelectAll, Me.tlbbtn_DeelectAll, Me.tlbbtn_Export, Me.tlsbtnShowReportList, Me.tsp_QRDAIII, Me.tsp_ExportQRDAI, Me.Tblbtn_More, Me.Tblbtn_Hide, Me.tlbbtn_Print, Me.tlbbtn_Close})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1078, 53)
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
        Me.tlbbtn_Export.Enabled = False
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
        'tsp_QRDAIII
        '
        Me.tsp_QRDAIII.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsp_QRDAIII.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsp_QRDAIII.Image = CType(resources.GetObject("tsp_QRDAIII.Image"), System.Drawing.Image)
        Me.tsp_QRDAIII.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsp_QRDAIII.Name = "tsp_QRDAIII"
        Me.tsp_QRDAIII.Size = New System.Drawing.Size(111, 50)
        Me.tsp_QRDAIII.Tag = "QRDAIII"
        Me.tsp_QRDAIII.Text = "&Export QRDA III"
        Me.tsp_QRDAIII.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsp_ExportQRDAI
        '
        Me.tsp_ExportQRDAI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsp_ExportQRDAI.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tsp_ExportQRDAI.Image = CType(resources.GetObject("tsp_ExportQRDAI.Image"), System.Drawing.Image)
        Me.tsp_ExportQRDAI.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsp_ExportQRDAI.Name = "tsp_ExportQRDAI"
        Me.tsp_ExportQRDAI.Size = New System.Drawing.Size(101, 50)
        Me.tsp_ExportQRDAI.Tag = "QRDAI"
        Me.tsp_ExportQRDAI.Text = "Export Q&RDA I"
        Me.tsp_ExportQRDAI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Tblbtn_More
        '
        Me.Tblbtn_More.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tblbtn_More.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Tblbtn_More.Image = CType(resources.GetObject("Tblbtn_More.Image"), System.Drawing.Image)
        Me.Tblbtn_More.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Tblbtn_More.Name = "Tblbtn_More"
        Me.Tblbtn_More.Size = New System.Drawing.Size(42, 50)
        Me.Tblbtn_More.Tag = "More "
        Me.Tblbtn_More.Text = "&More"
        Me.Tblbtn_More.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Tblbtn_More.ToolTipText = "More Options"
        '
        'Tblbtn_Hide
        '
        Me.Tblbtn_Hide.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tblbtn_Hide.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Tblbtn_Hide.Image = CType(resources.GetObject("Tblbtn_Hide.Image"), System.Drawing.Image)
        Me.Tblbtn_Hide.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Tblbtn_Hide.Name = "Tblbtn_Hide"
        Me.Tblbtn_Hide.Size = New System.Drawing.Size(38, 50)
        Me.Tblbtn_Hide.Tag = "Hide"
        Me.Tblbtn_Hide.Text = "&Hide"
        Me.Tblbtn_Hide.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Tblbtn_Hide.ToolTipText = "Hide Options"
        Me.Tblbtn_Hide.Visible = False
        '
        'tlbbtn_Print
        '
        Me.tlbbtn_Print.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_Print.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_Print.Image = CType(resources.GetObject("tlbbtn_Print.Image"), System.Drawing.Image)
        Me.tlbbtn_Print.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Print.Name = "tlbbtn_Print"
        Me.tlbbtn_Print.Size = New System.Drawing.Size(41, 50)
        Me.tlbbtn_Print.Tag = "Print"
        Me.tlbbtn_Print.Text = "&Print"
        Me.tlbbtn_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        Me.pnlC1Grid.Controls.Add(Me.Label17)
        Me.pnlC1Grid.Controls.Add(Me.Label16)
        Me.pnlC1Grid.Controls.Add(Me.Label15)
        Me.pnlC1Grid.Controls.Add(Me.Label9)
        Me.pnlC1Grid.Controls.Add(Me.C1QualityMeasures)
        Me.pnlC1Grid.Controls.Add(Me.pnlLoadingLable)
        Me.pnlC1Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlC1Grid.Location = New System.Drawing.Point(0, 301)
        Me.pnlC1Grid.Name = "pnlC1Grid"
        Me.pnlC1Grid.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlC1Grid.Size = New System.Drawing.Size(1078, 261)
        Me.pnlC1Grid.TabIndex = 108
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1070, 1)
        Me.Label17.TabIndex = 114
        Me.Label17.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(4, 257)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1070, 1)
        Me.Label16.TabIndex = 113
        Me.Label16.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(1074, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 258)
        Me.Label15.TabIndex = 112
        Me.Label15.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 258)
        Me.Label9.TabIndex = 111
        Me.Label9.Text = "label4"
        '
        'pnlLoadingLable
        '
        Me.pnlLoadingLable.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlLoadingLable.Controls.Add(Me.Label6)
        Me.pnlLoadingLable.Controls.Add(Me.Label8)
        Me.pnlLoadingLable.Controls.Add(Me.Label11)
        Me.pnlLoadingLable.Controls.Add(Me.Label13)
        Me.pnlLoadingLable.Controls.Add(Me.Label14)
        Me.pnlLoadingLable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLoadingLable.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlLoadingLable.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlLoadingLable.Location = New System.Drawing.Point(3, 0)
        Me.pnlLoadingLable.Name = "pnlLoadingLable"
        Me.pnlLoadingLable.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlLoadingLable.Size = New System.Drawing.Size(1072, 258)
        Me.pnlLoadingLable.TabIndex = 110
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(21, 13)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(224, 14)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Calculating measures... Please wait."
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 254)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1064, 1)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "label4"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(4, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1064, 1)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(1068, 3)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 252)
        Me.Label13.TabIndex = 5
        Me.Label13.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 3)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 252)
        Me.Label14.TabIndex = 4
        Me.Label14.Text = "label4"
        '
        'pnlDemoFilter
        '
        Me.pnlDemoFilter.Controls.Add(Me.CmbProviderTaxanomy)
        Me.pnlDemoFilter.Controls.Add(Me.btnClearProviderType)
        Me.pnlDemoFilter.Controls.Add(Me.btnClearFilters)
        Me.pnlDemoFilter.Controls.Add(Me.btnBrowseProviderType)
        Me.pnlDemoFilter.Controls.Add(Me.txtTIN)
        Me.pnlDemoFilter.Controls.Add(Me.Label7)
        Me.pnlDemoFilter.Controls.Add(Me.txtNPI)
        Me.pnlDemoFilter.Controls.Add(Me.pnlProviderAddress)
        Me.pnlDemoFilter.Controls.Add(Me.chkPracticeAddress)
        Me.pnlDemoFilter.Controls.Add(Me.pnlAge)
        Me.pnlDemoFilter.Controls.Add(Me.chkAge)
        Me.pnlDemoFilter.Controls.Add(Me.btnClearNPIs)
        Me.pnlDemoFilter.Controls.Add(Me.btnBrowseNPIs)
        Me.pnlDemoFilter.Controls.Add(Me.label72)
        Me.pnlDemoFilter.Controls.Add(Me.btnClearProblems)
        Me.pnlDemoFilter.Controls.Add(Me.btnBrowseProblems)
        Me.pnlDemoFilter.Controls.Add(Me.label64)
        Me.pnlDemoFilter.Controls.Add(Me.cmbProblems)
        Me.pnlDemoFilter.Controls.Add(Me.btnClearPayers)
        Me.pnlDemoFilter.Controls.Add(Me.btnBrowsePayers)
        Me.pnlDemoFilter.Controls.Add(Me.btnClearTINs)
        Me.pnlDemoFilter.Controls.Add(Me.btnBrowseTINs)
        Me.pnlDemoFilter.Controls.Add(Me.btnclearethnicity)
        Me.pnlDemoFilter.Controls.Add(Me.btnBrowseEthnicity)
        Me.pnlDemoFilter.Controls.Add(Me.btnCleaseRace)
        Me.pnlDemoFilter.Controls.Add(Me.btnBrowseRace)
        Me.pnlDemoFilter.Controls.Add(Me.btncleargender)
        Me.pnlDemoFilter.Controls.Add(Me.btnBrowseGender)
        Me.pnlDemoFilter.Controls.Add(Me.label51)
        Me.pnlDemoFilter.Controls.Add(Me.cmbPayers)
        Me.pnlDemoFilter.Controls.Add(Me.label50)
        Me.pnlDemoFilter.Controls.Add(Me.label54)
        Me.pnlDemoFilter.Controls.Add(Me.cmbethnicity)
        Me.pnlDemoFilter.Controls.Add(Me.cmbGender)
        Me.pnlDemoFilter.Controls.Add(Me.cmbRace)
        Me.pnlDemoFilter.Controls.Add(Me.label55)
        Me.pnlDemoFilter.Controls.Add(Me.label56)
        Me.pnlDemoFilter.Controls.Add(Me.label57)
        Me.pnlDemoFilter.Controls.Add(Me.label58)
        Me.pnlDemoFilter.Controls.Add(Me.label59)
        Me.pnlDemoFilter.Controls.Add(Me.label60)
        Me.pnlDemoFilter.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDemoFilter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlDemoFilter.Location = New System.Drawing.Point(0, 129)
        Me.pnlDemoFilter.Name = "pnlDemoFilter"
        Me.pnlDemoFilter.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlDemoFilter.Size = New System.Drawing.Size(1078, 172)
        Me.pnlDemoFilter.TabIndex = 112
        Me.pnlDemoFilter.Visible = False
        '
        'CmbProviderTaxanomy
        '
        Me.CmbProviderTaxanomy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbProviderTaxanomy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbProviderTaxanomy.FormattingEnabled = True
        Me.CmbProviderTaxanomy.Location = New System.Drawing.Point(102, 31)
        Me.CmbProviderTaxanomy.Name = "CmbProviderTaxanomy"
        Me.CmbProviderTaxanomy.Size = New System.Drawing.Size(174, 23)
        Me.CmbProviderTaxanomy.TabIndex = 232
        '
        'btnClearProviderType
        '
        Me.btnClearProviderType.BackColor = System.Drawing.Color.Transparent
        Me.btnClearProviderType.BackgroundImage = CType(resources.GetObject("btnClearProviderType.BackgroundImage"), System.Drawing.Image)
        Me.btnClearProviderType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearProviderType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearProviderType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearProviderType.Image = CType(resources.GetObject("btnClearProviderType.Image"), System.Drawing.Image)
        Me.btnClearProviderType.Location = New System.Drawing.Point(306, 32)
        Me.btnClearProviderType.Name = "btnClearProviderType"
        Me.btnClearProviderType.Size = New System.Drawing.Size(22, 22)
        Me.btnClearProviderType.TabIndex = 224
        Me.btnClearProviderType.UseVisualStyleBackColor = False
        '
        'btnClearFilters
        '
        Me.btnClearFilters.BackColor = System.Drawing.Color.Transparent
        Me.btnClearFilters.BackgroundImage = CType(resources.GetObject("btnClearFilters.BackgroundImage"), System.Drawing.Image)
        Me.btnClearFilters.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearFilters.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearFilters.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearFilters.Location = New System.Drawing.Point(468, 126)
        Me.btnClearFilters.Name = "btnClearFilters"
        Me.btnClearFilters.Size = New System.Drawing.Size(183, 24)
        Me.btnClearFilters.TabIndex = 231
        Me.btnClearFilters.Text = "Clear Filters"
        Me.btnClearFilters.UseVisualStyleBackColor = False
        '
        'btnBrowseProviderType
        '
        Me.btnBrowseProviderType.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowseProviderType.BackgroundImage = CType(resources.GetObject("btnBrowseProviderType.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseProviderType.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseProviderType.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseProviderType.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseProviderType.Image = CType(resources.GetObject("btnBrowseProviderType.Image"), System.Drawing.Image)
        Me.btnBrowseProviderType.Location = New System.Drawing.Point(280, 32)
        Me.btnBrowseProviderType.Name = "btnBrowseProviderType"
        Me.btnBrowseProviderType.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseProviderType.TabIndex = 223
        Me.btnBrowseProviderType.UseVisualStyleBackColor = False
        '
        'txtTIN
        '
        Me.txtTIN.Location = New System.Drawing.Point(448, 6)
        Me.txtTIN.MaxLength = 12
        Me.txtTIN.Name = "txtTIN"
        Me.txtTIN.Size = New System.Drawing.Size(202, 22)
        Me.txtTIN.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 36)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(91, 14)
        Me.Label7.TabIndex = 226
        Me.Label7.Text = "Provider Type :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNPI
        '
        Me.txtNPI.Location = New System.Drawing.Point(102, 6)
        Me.txtNPI.MaxLength = 12
        Me.txtNPI.Name = "txtNPI"
        Me.txtNPI.Size = New System.Drawing.Size(174, 22)
        Me.txtNPI.TabIndex = 3
        '
        'pnlProviderAddress
        '
        Me.pnlProviderAddress.Enabled = False
        Me.pnlProviderAddress.Location = New System.Drawing.Point(871, 6)
        Me.pnlProviderAddress.Name = "pnlProviderAddress"
        Me.pnlProviderAddress.Size = New System.Drawing.Size(338, 138)
        Me.pnlProviderAddress.TabIndex = 230
        '
        'chkPracticeAddress
        '
        Me.chkPracticeAddress.AutoSize = True
        Me.chkPracticeAddress.Location = New System.Drawing.Point(741, 8)
        Me.chkPracticeAddress.Name = "chkPracticeAddress"
        Me.chkPracticeAddress.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkPracticeAddress.Size = New System.Drawing.Size(124, 18)
        Me.chkPracticeAddress.TabIndex = 7
        Me.chkPracticeAddress.Text = ": Practice Address"
        Me.chkPracticeAddress.UseVisualStyleBackColor = True
        '
        'pnlAge
        '
        Me.pnlAge.Controls.Add(Me.cmbAgeCriteria)
        Me.pnlAge.Controls.Add(Me.dtpicAge)
        Me.pnlAge.Controls.Add(Me.Label12)
        Me.pnlAge.Controls.Add(Me.cmbAge)
        Me.pnlAge.Enabled = False
        Me.pnlAge.Location = New System.Drawing.Point(467, 61)
        Me.pnlAge.Name = "pnlAge"
        Me.pnlAge.Size = New System.Drawing.Size(198, 64)
        Me.pnlAge.TabIndex = 18
        '
        'cmbAgeCriteria
        '
        Me.cmbAgeCriteria.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbAgeCriteria.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAgeCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAgeCriteria.Enabled = False
        Me.cmbAgeCriteria.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAgeCriteria.ForeColor = System.Drawing.Color.Black
        Me.cmbAgeCriteria.FormattingEnabled = True
        Me.cmbAgeCriteria.Items.AddRange(New Object() {"Maximum", "Minimum", "As On"})
        Me.cmbAgeCriteria.Location = New System.Drawing.Point(1, 1)
        Me.cmbAgeCriteria.Name = "cmbAgeCriteria"
        Me.cmbAgeCriteria.Size = New System.Drawing.Size(183, 22)
        Me.cmbAgeCriteria.TabIndex = 0
        '
        'dtpicAge
        '
        Me.dtpicAge.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpicAge.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpicAge.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpicAge.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpicAge.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpicAge.CustomFormat = "MM/dd/yyyy"
        Me.dtpicAge.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpicAge.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpicAge.Location = New System.Drawing.Point(1, 25)
        Me.dtpicAge.Name = "dtpicAge"
        Me.dtpicAge.Size = New System.Drawing.Size(105, 22)
        Me.dtpicAge.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(139, 48)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(28, 11)
        Me.Label12.TabIndex = 229
        Me.Label12.Text = "(Age)"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbAge
        '
        Me.cmbAge.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbAge.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAge.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAge.FormattingEnabled = True
        Me.cmbAge.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "100", "101", "102", "103", "104", "105", "106", "107", "108", "109", "110"})
        Me.cmbAge.Location = New System.Drawing.Point(112, 25)
        Me.cmbAge.Name = "cmbAge"
        Me.cmbAge.Size = New System.Drawing.Size(72, 22)
        Me.cmbAge.TabIndex = 2
        '
        'chkAge
        '
        Me.chkAge.AutoSize = True
        Me.chkAge.Location = New System.Drawing.Point(407, 58)
        Me.chkAge.Name = "chkAge"
        Me.chkAge.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkAge.Size = New System.Drawing.Size(56, 18)
        Me.chkAge.TabIndex = 17
        Me.chkAge.Text = ": Age"
        Me.chkAge.UseVisualStyleBackColor = True
        '
        'btnClearNPIs
        '
        Me.btnClearNPIs.BackColor = System.Drawing.Color.Transparent
        Me.btnClearNPIs.BackgroundImage = CType(resources.GetObject("btnClearNPIs.BackgroundImage"), System.Drawing.Image)
        Me.btnClearNPIs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearNPIs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearNPIs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearNPIs.Image = CType(resources.GetObject("btnClearNPIs.Image"), System.Drawing.Image)
        Me.btnClearNPIs.Location = New System.Drawing.Point(306, 6)
        Me.btnClearNPIs.Name = "btnClearNPIs"
        Me.btnClearNPIs.Size = New System.Drawing.Size(22, 22)
        Me.btnClearNPIs.TabIndex = 2
        Me.btnClearNPIs.UseVisualStyleBackColor = False
        Me.btnClearNPIs.Visible = False
        '
        'btnBrowseNPIs
        '
        Me.btnBrowseNPIs.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowseNPIs.BackgroundImage = CType(resources.GetObject("btnBrowseNPIs.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseNPIs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseNPIs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseNPIs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseNPIs.Image = CType(resources.GetObject("btnBrowseNPIs.Image"), System.Drawing.Image)
        Me.btnBrowseNPIs.Location = New System.Drawing.Point(280, 6)
        Me.btnBrowseNPIs.Name = "btnBrowseNPIs"
        Me.btnBrowseNPIs.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseNPIs.TabIndex = 1
        Me.btnBrowseNPIs.UseVisualStyleBackColor = False
        Me.btnBrowseNPIs.Visible = False
        '
        'label72
        '
        Me.label72.AutoSize = True
        Me.label72.BackColor = System.Drawing.Color.Transparent
        Me.label72.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label72.Location = New System.Drawing.Point(65, 10)
        Me.label72.Name = "label72"
        Me.label72.Size = New System.Drawing.Size(34, 14)
        Me.label72.TabIndex = 222
        Me.label72.Text = "NPI :"
        Me.label72.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClearProblems
        '
        Me.btnClearProblems.BackColor = System.Drawing.Color.Transparent
        Me.btnClearProblems.BackgroundImage = CType(resources.GetObject("btnClearProblems.BackgroundImage"), System.Drawing.Image)
        Me.btnClearProblems.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearProblems.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearProblems.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearProblems.Image = CType(resources.GetObject("btnClearProblems.Image"), System.Drawing.Image)
        Me.btnClearProblems.Location = New System.Drawing.Point(306, 141)
        Me.btnClearProblems.Name = "btnClearProblems"
        Me.btnClearProblems.Size = New System.Drawing.Size(22, 22)
        Me.btnClearProblems.TabIndex = 23
        Me.btnClearProblems.UseVisualStyleBackColor = False
        '
        'btnBrowseProblems
        '
        Me.btnBrowseProblems.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowseProblems.BackgroundImage = CType(resources.GetObject("btnBrowseProblems.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseProblems.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseProblems.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseProblems.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseProblems.Image = CType(resources.GetObject("btnBrowseProblems.Image"), System.Drawing.Image)
        Me.btnBrowseProblems.Location = New System.Drawing.Point(280, 141)
        Me.btnBrowseProblems.Name = "btnBrowseProblems"
        Me.btnBrowseProblems.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseProblems.TabIndex = 22
        Me.btnBrowseProblems.UseVisualStyleBackColor = False
        '
        'label64
        '
        Me.label64.AutoSize = True
        Me.label64.BackColor = System.Drawing.Color.Transparent
        Me.label64.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label64.Location = New System.Drawing.Point(40, 145)
        Me.label64.Name = "label64"
        Me.label64.Size = New System.Drawing.Size(59, 14)
        Me.label64.TabIndex = 218
        Me.label64.Text = "Problem :"
        Me.label64.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbProblems
        '
        Me.cmbProblems.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbProblems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProblems.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProblems.FormattingEnabled = True
        Me.cmbProblems.Location = New System.Drawing.Point(102, 141)
        Me.cmbProblems.Name = "cmbProblems"
        Me.cmbProblems.Size = New System.Drawing.Size(174, 23)
        Me.cmbProblems.TabIndex = 24
        '
        'btnClearPayers
        '
        Me.btnClearPayers.BackColor = System.Drawing.Color.Transparent
        Me.btnClearPayers.BackgroundImage = CType(resources.GetObject("btnClearPayers.BackgroundImage"), System.Drawing.Image)
        Me.btnClearPayers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearPayers.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearPayers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearPayers.Image = CType(resources.GetObject("btnClearPayers.Image"), System.Drawing.Image)
        Me.btnClearPayers.Location = New System.Drawing.Point(306, 113)
        Me.btnClearPayers.Name = "btnClearPayers"
        Me.btnClearPayers.Size = New System.Drawing.Size(22, 22)
        Me.btnClearPayers.TabIndex = 20
        Me.btnClearPayers.UseVisualStyleBackColor = False
        '
        'btnBrowsePayers
        '
        Me.btnBrowsePayers.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowsePayers.BackgroundImage = CType(resources.GetObject("btnBrowsePayers.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowsePayers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowsePayers.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowsePayers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowsePayers.Image = CType(resources.GetObject("btnBrowsePayers.Image"), System.Drawing.Image)
        Me.btnBrowsePayers.Location = New System.Drawing.Point(280, 113)
        Me.btnBrowsePayers.Name = "btnBrowsePayers"
        Me.btnBrowsePayers.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowsePayers.TabIndex = 19
        Me.btnBrowsePayers.UseVisualStyleBackColor = False
        '
        'btnClearTINs
        '
        Me.btnClearTINs.BackColor = System.Drawing.Color.Transparent
        Me.btnClearTINs.BackgroundImage = CType(resources.GetObject("btnClearTINs.BackgroundImage"), System.Drawing.Image)
        Me.btnClearTINs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearTINs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearTINs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearTINs.Image = CType(resources.GetObject("btnClearTINs.Image"), System.Drawing.Image)
        Me.btnClearTINs.Location = New System.Drawing.Point(680, 6)
        Me.btnClearTINs.Name = "btnClearTINs"
        Me.btnClearTINs.Size = New System.Drawing.Size(22, 22)
        Me.btnClearTINs.TabIndex = 5
        Me.btnClearTINs.UseVisualStyleBackColor = False
        Me.btnClearTINs.Visible = False
        '
        'btnBrowseTINs
        '
        Me.btnBrowseTINs.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowseTINs.BackgroundImage = CType(resources.GetObject("btnBrowseTINs.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseTINs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseTINs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseTINs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseTINs.Image = CType(resources.GetObject("btnBrowseTINs.Image"), System.Drawing.Image)
        Me.btnBrowseTINs.Location = New System.Drawing.Point(654, 6)
        Me.btnBrowseTINs.Name = "btnBrowseTINs"
        Me.btnBrowseTINs.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseTINs.TabIndex = 4
        Me.btnBrowseTINs.UseVisualStyleBackColor = False
        Me.btnBrowseTINs.Visible = False
        '
        'btnclearethnicity
        '
        Me.btnclearethnicity.BackColor = System.Drawing.Color.Transparent
        Me.btnclearethnicity.BackgroundImage = CType(resources.GetObject("btnclearethnicity.BackgroundImage"), System.Drawing.Image)
        Me.btnclearethnicity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnclearethnicity.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnclearethnicity.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnclearethnicity.Image = CType(resources.GetObject("btnclearethnicity.Image"), System.Drawing.Image)
        Me.btnclearethnicity.Location = New System.Drawing.Point(680, 31)
        Me.btnclearethnicity.Name = "btnclearethnicity"
        Me.btnclearethnicity.Size = New System.Drawing.Size(22, 22)
        Me.btnclearethnicity.TabIndex = 12
        Me.btnclearethnicity.UseVisualStyleBackColor = False
        '
        'btnBrowseEthnicity
        '
        Me.btnBrowseEthnicity.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowseEthnicity.BackgroundImage = CType(resources.GetObject("btnBrowseEthnicity.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseEthnicity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseEthnicity.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseEthnicity.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseEthnicity.Image = CType(resources.GetObject("btnBrowseEthnicity.Image"), System.Drawing.Image)
        Me.btnBrowseEthnicity.Location = New System.Drawing.Point(654, 31)
        Me.btnBrowseEthnicity.Name = "btnBrowseEthnicity"
        Me.btnBrowseEthnicity.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseEthnicity.TabIndex = 11
        Me.btnBrowseEthnicity.UseVisualStyleBackColor = False
        '
        'btnCleaseRace
        '
        Me.btnCleaseRace.BackColor = System.Drawing.Color.Transparent
        Me.btnCleaseRace.BackgroundImage = CType(resources.GetObject("btnCleaseRace.BackgroundImage"), System.Drawing.Image)
        Me.btnCleaseRace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCleaseRace.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnCleaseRace.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCleaseRace.Image = CType(resources.GetObject("btnCleaseRace.Image"), System.Drawing.Image)
        Me.btnCleaseRace.Location = New System.Drawing.Point(306, 57)
        Me.btnCleaseRace.Name = "btnCleaseRace"
        Me.btnCleaseRace.Size = New System.Drawing.Size(22, 22)
        Me.btnCleaseRace.TabIndex = 9
        Me.btnCleaseRace.UseVisualStyleBackColor = False
        '
        'btnBrowseRace
        '
        Me.btnBrowseRace.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowseRace.BackgroundImage = CType(resources.GetObject("btnBrowseRace.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseRace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseRace.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseRace.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseRace.Image = CType(resources.GetObject("btnBrowseRace.Image"), System.Drawing.Image)
        Me.btnBrowseRace.Location = New System.Drawing.Point(280, 57)
        Me.btnBrowseRace.Name = "btnBrowseRace"
        Me.btnBrowseRace.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseRace.TabIndex = 8
        Me.btnBrowseRace.UseVisualStyleBackColor = False
        '
        'btncleargender
        '
        Me.btncleargender.BackColor = System.Drawing.Color.Transparent
        Me.btncleargender.BackgroundImage = CType(resources.GetObject("btncleargender.BackgroundImage"), System.Drawing.Image)
        Me.btncleargender.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btncleargender.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btncleargender.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncleargender.Image = CType(resources.GetObject("btncleargender.Image"), System.Drawing.Image)
        Me.btncleargender.Location = New System.Drawing.Point(306, 85)
        Me.btncleargender.Name = "btncleargender"
        Me.btncleargender.Size = New System.Drawing.Size(22, 22)
        Me.btncleargender.TabIndex = 15
        Me.btncleargender.UseVisualStyleBackColor = False
        '
        'btnBrowseGender
        '
        Me.btnBrowseGender.BackColor = System.Drawing.Color.Transparent
        Me.btnBrowseGender.BackgroundImage = CType(resources.GetObject("btnBrowseGender.BackgroundImage"), System.Drawing.Image)
        Me.btnBrowseGender.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBrowseGender.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnBrowseGender.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseGender.Image = CType(resources.GetObject("btnBrowseGender.Image"), System.Drawing.Image)
        Me.btnBrowseGender.Location = New System.Drawing.Point(280, 85)
        Me.btnBrowseGender.Name = "btnBrowseGender"
        Me.btnBrowseGender.Size = New System.Drawing.Size(22, 22)
        Me.btnBrowseGender.TabIndex = 14
        Me.btnBrowseGender.UseVisualStyleBackColor = False
        '
        'label51
        '
        Me.label51.AutoSize = True
        Me.label51.BackColor = System.Drawing.Color.Transparent
        Me.label51.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label51.Location = New System.Drawing.Point(54, 117)
        Me.label51.Name = "label51"
        Me.label51.Size = New System.Drawing.Size(45, 14)
        Me.label51.TabIndex = 20
        Me.label51.Text = "Payer :"
        Me.label51.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbPayers
        '
        Me.cmbPayers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbPayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPayers.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPayers.FormattingEnabled = True
        Me.cmbPayers.Location = New System.Drawing.Point(102, 113)
        Me.cmbPayers.Name = "cmbPayers"
        Me.cmbPayers.Size = New System.Drawing.Size(174, 23)
        Me.cmbPayers.TabIndex = 21
        '
        'label50
        '
        Me.label50.AutoSize = True
        Me.label50.BackColor = System.Drawing.Color.Transparent
        Me.label50.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label50.Location = New System.Drawing.Point(410, 10)
        Me.label50.Name = "label50"
        Me.label50.Size = New System.Drawing.Size(35, 14)
        Me.label50.TabIndex = 18
        Me.label50.Text = "TIN :"
        Me.label50.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label54
        '
        Me.label54.AutoSize = True
        Me.label54.BackColor = System.Drawing.Color.Transparent
        Me.label54.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label54.Location = New System.Drawing.Point(383, 35)
        Me.label54.Name = "label54"
        Me.label54.Size = New System.Drawing.Size(62, 14)
        Me.label54.TabIndex = 9
        Me.label54.Text = "Ethnicity :"
        Me.label54.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbethnicity
        '
        Me.cmbethnicity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbethnicity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbethnicity.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbethnicity.FormattingEnabled = True
        Me.cmbethnicity.Location = New System.Drawing.Point(448, 31)
        Me.cmbethnicity.Name = "cmbethnicity"
        Me.cmbethnicity.Size = New System.Drawing.Size(202, 23)
        Me.cmbethnicity.TabIndex = 13
        '
        'cmbGender
        '
        Me.cmbGender.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGender.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbGender.ForeColor = System.Drawing.Color.Black
        Me.cmbGender.FormattingEnabled = True
        Me.cmbGender.Location = New System.Drawing.Point(102, 85)
        Me.cmbGender.Name = "cmbGender"
        Me.cmbGender.Size = New System.Drawing.Size(174, 23)
        Me.cmbGender.TabIndex = 16
        '
        'cmbRace
        '
        Me.cmbRace.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRace.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRace.FormattingEnabled = True
        Me.cmbRace.Location = New System.Drawing.Point(102, 57)
        Me.cmbRace.Name = "cmbRace"
        Me.cmbRace.Size = New System.Drawing.Size(174, 23)
        Me.cmbRace.TabIndex = 10
        '
        'label55
        '
        Me.label55.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label55.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.label55.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label55.Location = New System.Drawing.Point(4, 168)
        Me.label55.Name = "label55"
        Me.label55.Size = New System.Drawing.Size(1070, 1)
        Me.label55.TabIndex = 16
        Me.label55.Text = "label1"
        '
        'label56
        '
        Me.label56.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label56.Dock = System.Windows.Forms.DockStyle.Top
        Me.label56.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label56.Location = New System.Drawing.Point(4, 0)
        Me.label56.Name = "label56"
        Me.label56.Size = New System.Drawing.Size(1070, 1)
        Me.label56.TabIndex = 15
        Me.label56.Text = "label1"
        '
        'label57
        '
        Me.label57.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label57.Dock = System.Windows.Forms.DockStyle.Right
        Me.label57.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label57.Location = New System.Drawing.Point(1074, 0)
        Me.label57.Name = "label57"
        Me.label57.Size = New System.Drawing.Size(1, 169)
        Me.label57.TabIndex = 14
        Me.label57.Text = "label4"
        '
        'label58
        '
        Me.label58.AutoSize = True
        Me.label58.BackColor = System.Drawing.Color.Transparent
        Me.label58.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label58.Location = New System.Drawing.Point(58, 61)
        Me.label58.Name = "label58"
        Me.label58.Size = New System.Drawing.Size(41, 14)
        Me.label58.TabIndex = 0
        Me.label58.Text = "Race :"
        '
        'label59
        '
        Me.label59.AutoSize = True
        Me.label59.BackColor = System.Drawing.Color.Transparent
        Me.label59.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label59.Location = New System.Drawing.Point(44, 89)
        Me.label59.Name = "label59"
        Me.label59.Size = New System.Drawing.Size(55, 14)
        Me.label59.TabIndex = 0
        Me.label59.Text = "Gender :"
        '
        'label60
        '
        Me.label60.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.label60.Dock = System.Windows.Forms.DockStyle.Left
        Me.label60.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label60.Location = New System.Drawing.Point(3, 0)
        Me.label60.Name = "label60"
        Me.label60.Size = New System.Drawing.Size(1, 169)
        Me.label60.TabIndex = 13
        Me.label60.Text = "label4"
        '
        'pnlMeasurementPeriod
        '
        Me.pnlMeasurementPeriod.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlMeasurementPeriod.Controls.Add(Me.btnClearProvider)
        Me.pnlMeasurementPeriod.Controls.Add(Me.btnbrprov)
        Me.pnlMeasurementPeriod.Controls.Add(Me.chkCQM2014)
        Me.pnlMeasurementPeriod.Controls.Add(Me.cmbProviders)
        Me.pnlMeasurementPeriod.Controls.Add(Me.Label2)
        Me.pnlMeasurementPeriod.Controls.Add(Me.Label1)
        Me.pnlMeasurementPeriod.Controls.Add(Me.dtpicEndDate)
        Me.pnlMeasurementPeriod.Controls.Add(Me.dtpicStartDate)
        Me.pnlMeasurementPeriod.Controls.Add(Me.Label5)
        Me.pnlMeasurementPeriod.Controls.Add(Me.lblTo)
        Me.pnlMeasurementPeriod.Controls.Add(Me.Label4)
        Me.pnlMeasurementPeriod.Controls.Add(Me.Label10)
        Me.pnlMeasurementPeriod.Controls.Add(Me.Label3)
        Me.pnlMeasurementPeriod.Controls.Add(Me.lbl_LeftBrd)
        Me.pnlMeasurementPeriod.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMeasurementPeriod.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlMeasurementPeriod.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlMeasurementPeriod.Location = New System.Drawing.Point(0, 54)
        Me.pnlMeasurementPeriod.Name = "pnlMeasurementPeriod"
        Me.pnlMeasurementPeriod.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlMeasurementPeriod.Size = New System.Drawing.Size(1078, 40)
        Me.pnlMeasurementPeriod.TabIndex = 109
        '
        'btnClearProvider
        '
        Me.btnClearProvider.BackColor = System.Drawing.Color.Transparent
        Me.btnClearProvider.BackgroundImage = CType(resources.GetObject("btnClearProvider.BackgroundImage"), System.Drawing.Image)
        Me.btnClearProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearProvider.Image = CType(resources.GetObject("btnClearProvider.Image"), System.Drawing.Image)
        Me.btnClearProvider.Location = New System.Drawing.Point(448, 8)
        Me.btnClearProvider.Name = "btnClearProvider"
        Me.btnClearProvider.Size = New System.Drawing.Size(22, 22)
        Me.btnClearProvider.TabIndex = 139
        Me.btnClearProvider.UseVisualStyleBackColor = False
        '
        'btnbrprov
        '
        Me.btnbrprov.BackColor = System.Drawing.Color.Transparent
        Me.btnbrprov.BackgroundImage = CType(resources.GetObject("btnbrprov.BackgroundImage"), System.Drawing.Image)
        Me.btnbrprov.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnbrprov.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnbrprov.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnbrprov.Image = CType(resources.GetObject("btnbrprov.Image"), System.Drawing.Image)
        Me.btnbrprov.Location = New System.Drawing.Point(420, 8)
        Me.btnbrprov.Name = "btnbrprov"
        Me.btnbrprov.Size = New System.Drawing.Size(22, 22)
        Me.btnbrprov.TabIndex = 138
        Me.btnbrprov.UseVisualStyleBackColor = False
        '
        'chkCQM2014
        '
        Me.chkCQM2014.AutoSize = True
        Me.chkCQM2014.Checked = True
        Me.chkCQM2014.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCQM2014.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.chkCQM2014.Location = New System.Drawing.Point(478, 12)
        Me.chkCQM2014.Name = "chkCQM2014"
        Me.chkCQM2014.Size = New System.Drawing.Size(100, 18)
        Me.chkCQM2014.TabIndex = 11
        Me.chkCQM2014.Text = "CQM 2014+"
        Me.chkCQM2014.UseVisualStyleBackColor = True
        '
        'cmbProviders
        '
        Me.cmbProviders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProviders.FormattingEnabled = True
        Me.cmbProviders.Location = New System.Drawing.Point(102, 8)
        Me.cmbProviders.Name = "cmbProviders"
        Me.cmbProviders.Size = New System.Drawing.Size(314, 22)
        Me.cmbProviders.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(27, 13)
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
        Me.Label1.Location = New System.Drawing.Point(721, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 14)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Start Date "
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
        Me.dtpicEndDate.Location = New System.Drawing.Point(968, 9)
        Me.dtpicEndDate.Name = "dtpicEndDate"
        Me.dtpicEndDate.Size = New System.Drawing.Size(101, 22)
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
        Me.dtpicStartDate.Location = New System.Drawing.Point(792, 9)
        Me.dtpicStartDate.Name = "dtpicStartDate"
        Me.dtpicStartDate.Size = New System.Drawing.Size(101, 22)
        Me.dtpicStartDate.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(4, 36)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1070, 1)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "label4"
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.BackColor = System.Drawing.Color.Transparent
        Me.lblTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.Location = New System.Drawing.Point(901, 13)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(62, 14)
        Me.lblTo.TabIndex = 4
        Me.lblTo.Text = "End Date "
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1070, 1)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label4"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(580, 13)
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
        Me.Label3.Location = New System.Drawing.Point(1074, 3)
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
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'pnlcustomTask
        '
        Me.pnlcustomTask.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlcustomTask.Location = New System.Drawing.Point(353, 169)
        Me.pnlcustomTask.Name = "pnlcustomTask"
        Me.pnlcustomTask.Size = New System.Drawing.Size(373, 225)
        Me.pnlcustomTask.TabIndex = 227
        Me.pnlcustomTask.Visible = False
        '
        'pnlPatientDetails
        '
        Me.pnlPatientDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlPatientDetails.Controls.Add(Me.lblPatientDetails)
        Me.pnlPatientDetails.Controls.Add(Me.Label20)
        Me.pnlPatientDetails.Controls.Add(Me.Label22)
        Me.pnlPatientDetails.Controls.Add(Me.Label24)
        Me.pnlPatientDetails.Controls.Add(Me.Label25)
        Me.pnlPatientDetails.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPatientDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlPatientDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlPatientDetails.Location = New System.Drawing.Point(0, 94)
        Me.pnlPatientDetails.Name = "pnlPatientDetails"
        Me.pnlPatientDetails.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlPatientDetails.Size = New System.Drawing.Size(1078, 35)
        Me.pnlPatientDetails.TabIndex = 228
        Me.pnlPatientDetails.Visible = False
        '
        'lblPatientDetails
        '
        Me.lblPatientDetails.AutoSize = True
        Me.lblPatientDetails.BackColor = System.Drawing.Color.Transparent
        Me.lblPatientDetails.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPatientDetails.Location = New System.Drawing.Point(25, 8)
        Me.lblPatientDetails.Name = "lblPatientDetails"
        Me.lblPatientDetails.Size = New System.Drawing.Size(103, 16)
        Me.lblPatientDetails.TabIndex = 10
        Me.lblPatientDetails.Text = "Patient Details"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(4, 31)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1070, 1)
        Me.Label20.TabIndex = 7
        Me.Label20.Text = "label4"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1070, 1)
        Me.Label22.TabIndex = 6
        Me.Label22.Text = "label4"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(1074, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 32)
        Me.Label24.TabIndex = 5
        Me.Label24.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(3, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 32)
        Me.Label25.TabIndex = 4
        Me.Label25.Text = "label4"
        '
        'frm_MIPSQuality_Reports_2017
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1078, 562)
        Me.Controls.Add(Me.pnlcustomTask)
        Me.Controls.Add(Me.pnlC1Grid)
        Me.Controls.Add(Me.pnlDemoFilter)
        Me.Controls.Add(Me.pnlPatientDetails)
        Me.Controls.Add(Me.pnlMeasurementPeriod)
        Me.Controls.Add(Me.pnl_tls_PtICD9CPTReport)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_MIPSQuality_Reports_2017"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "MIPS Quality Dashboard 2017"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.C1QualityMeasures, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_tls_PtICD9CPTReport.ResumeLayout(False)
        Me.pnl_tls_PtICD9CPTReport.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlC1Grid.ResumeLayout(False)
        Me.pnlLoadingLable.ResumeLayout(False)
        Me.pnlLoadingLable.PerformLayout()
        Me.pnlDemoFilter.ResumeLayout(False)
        Me.pnlDemoFilter.PerformLayout()
        Me.pnlAge.ResumeLayout(False)
        Me.pnlAge.PerformLayout()
        Me.pnlMeasurementPeriod.ResumeLayout(False)
        Me.pnlMeasurementPeriod.PerformLayout()
        Me.pnlPatientDetails.ResumeLayout(False)
        Me.pnlPatientDetails.PerformLayout()
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
    Friend WithEvents pnlMeasurementPeriod As System.Windows.Forms.Panel
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
    Friend WithEvents pnlLoadingLable As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents SaveFileDialogXML As System.Windows.Forms.SaveFileDialog
    Private WithEvents tlbbtn_Print As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkCQM2014 As System.Windows.Forms.CheckBox
    Private WithEvents tsp_QRDAIII As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblQRDAMsg As System.Windows.Forms.Label
    Private WithEvents tsp_ExportQRDAI As System.Windows.Forms.ToolStripButton
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents pnlDemoFilter As System.Windows.Forms.Panel
    Friend WithEvents CmbProviderTaxanomy As System.Windows.Forms.ComboBox
    Friend WithEvents btnClearProviderType As System.Windows.Forms.Button
    Friend WithEvents btnClearFilters As System.Windows.Forms.Button
    Friend WithEvents btnBrowseProviderType As System.Windows.Forms.Button
    Friend WithEvents txtTIN As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtNPI As System.Windows.Forms.TextBox
    Friend WithEvents pnlProviderAddress As System.Windows.Forms.Panel
    Friend WithEvents chkPracticeAddress As System.Windows.Forms.CheckBox
    Friend WithEvents pnlAge As System.Windows.Forms.Panel
    Friend WithEvents cmbAgeCriteria As System.Windows.Forms.ComboBox
    Friend WithEvents dtpicAge As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbAge As System.Windows.Forms.ComboBox
    Friend WithEvents chkAge As System.Windows.Forms.CheckBox
    Friend WithEvents btnClearNPIs As System.Windows.Forms.Button
    Friend WithEvents btnBrowseNPIs As System.Windows.Forms.Button
    Friend WithEvents label72 As System.Windows.Forms.Label
    Friend WithEvents btnClearProblems As System.Windows.Forms.Button
    Friend WithEvents btnBrowseProblems As System.Windows.Forms.Button
    Friend WithEvents label64 As System.Windows.Forms.Label
    Friend WithEvents cmbProblems As System.Windows.Forms.ComboBox
    Friend WithEvents btnClearPayers As System.Windows.Forms.Button
    Friend WithEvents btnBrowsePayers As System.Windows.Forms.Button
    Friend WithEvents btnClearTINs As System.Windows.Forms.Button
    Friend WithEvents btnBrowseTINs As System.Windows.Forms.Button
    Friend WithEvents btnclearethnicity As System.Windows.Forms.Button
    Friend WithEvents btnBrowseEthnicity As System.Windows.Forms.Button
    Friend WithEvents btnCleaseRace As System.Windows.Forms.Button
    Friend WithEvents btnBrowseRace As System.Windows.Forms.Button
    Friend WithEvents btncleargender As System.Windows.Forms.Button
    Friend WithEvents btnBrowseGender As System.Windows.Forms.Button
    Friend WithEvents label51 As System.Windows.Forms.Label
    Friend WithEvents cmbPayers As System.Windows.Forms.ComboBox
    Friend WithEvents label50 As System.Windows.Forms.Label
    Friend WithEvents label54 As System.Windows.Forms.Label
    Friend WithEvents cmbethnicity As System.Windows.Forms.ComboBox
    Friend WithEvents cmbGender As System.Windows.Forms.ComboBox
    Friend WithEvents cmbRace As System.Windows.Forms.ComboBox
    Private WithEvents label55 As System.Windows.Forms.Label
    Private WithEvents label56 As System.Windows.Forms.Label
    Private WithEvents label57 As System.Windows.Forms.Label
    Friend WithEvents label58 As System.Windows.Forms.Label
    Friend WithEvents label59 As System.Windows.Forms.Label
    Private WithEvents label60 As System.Windows.Forms.Label
    Private WithEvents pnlcustomTask As System.Windows.Forms.Panel
    Friend WithEvents Tblbtn_More As System.Windows.Forms.ToolStripButton
    Friend WithEvents Tblbtn_Hide As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents pnlPatientDetails As System.Windows.Forms.Panel
    Friend WithEvents lblPatientDetails As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents btnbrprov As System.Windows.Forms.Button
    Friend WithEvents btnClearProvider As System.Windows.Forms.Button
End Class
