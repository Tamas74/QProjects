<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_MU_Dashboard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_MU_Dashboard))
        Me.tlb_MUDashboard = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbbtn_Print = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Save = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_SavenClose = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Settings = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtn_ShowReportList = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.pnlC1Grid = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.C1QualityMeasures = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnl = New System.Windows.Forms.Panel()
        Me.lbl_NPIValue = New System.Windows.Forms.Label()
        Me.lbl_CertificationValue = New System.Windows.Forms.Label()
        Me.lbl_Certification = New System.Windows.Forms.Label()
        Me.lbl_TaxIDValue = New System.Windows.Forms.Label()
        Me.lbl_TaxID = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtReportName = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlMeasurementPeriod = New System.Windows.Forms.Panel()
        Me.dtpicEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpicStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbl_PNPI = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chk_FirstYear = New System.Windows.Forms.CheckBox()
        Me.cmb_RptYear = New System.Windows.Forms.ComboBox()
        Me.cmb_Provider = New System.Windows.Forms.ComboBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.ImgFlag = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.tlb_MUDashboard.SuspendLayout()
        Me.pnlC1Grid.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.C1QualityMeasures, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl.SuspendLayout()
        Me.pnlMeasurementPeriod.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlb_MUDashboard
        '
        Me.tlb_MUDashboard.BackColor = System.Drawing.Color.Transparent
        Me.tlb_MUDashboard.BackgroundImage = CType(resources.GetObject("tlb_MUDashboard.BackgroundImage"), System.Drawing.Image)
        Me.tlb_MUDashboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlb_MUDashboard.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_MUDashboard.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlb_MUDashboard.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtn_Print, Me.tlbbtn_Save, Me.tlbbtn_SavenClose, Me.tlbbtn_Settings, Me.ToolStripButton1, Me.tlsbtn_ShowReportList, Me.tlbbtn_Close})
        Me.tlb_MUDashboard.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlb_MUDashboard.Location = New System.Drawing.Point(0, 0)
        Me.tlb_MUDashboard.Name = "tlb_MUDashboard"
        Me.tlb_MUDashboard.Size = New System.Drawing.Size(1001, 53)
        Me.tlb_MUDashboard.TabIndex = 1
        Me.tlb_MUDashboard.Text = "toolStrip1"
        '
        'tlbbtn_Print
        '
        Me.tlbbtn_Print.Image = CType(resources.GetObject("tlbbtn_Print.Image"), System.Drawing.Image)
        Me.tlbbtn_Print.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Print.Name = "tlbbtn_Print"
        Me.tlbbtn_Print.Size = New System.Drawing.Size(41, 50)
        Me.tlbbtn_Print.Tag = "Print"
        Me.tlbbtn_Print.Text = "&Print"
        Me.tlbbtn_Print.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_Save
        '
        Me.tlbbtn_Save.Image = CType(resources.GetObject("tlbbtn_Save.Image"), System.Drawing.Image)
        Me.tlbbtn_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Save.Name = "tlbbtn_Save"
        Me.tlbbtn_Save.Size = New System.Drawing.Size(40, 50)
        Me.tlbbtn_Save.Tag = "Save"
        Me.tlbbtn_Save.Text = "&Save"
        Me.tlbbtn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_SavenClose
        '
        Me.tlbbtn_SavenClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_SavenClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_SavenClose.Image = CType(resources.GetObject("tlbbtn_SavenClose.Image"), System.Drawing.Image)
        Me.tlbbtn_SavenClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_SavenClose.Name = "tlbbtn_SavenClose"
        Me.tlbbtn_SavenClose.Size = New System.Drawing.Size(66, 50)
        Me.tlbbtn_SavenClose.Tag = "Save&Close"
        Me.tlbbtn_SavenClose.Text = "Sa&ve&&Cls"
        Me.tlbbtn_SavenClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_SavenClose.ToolTipText = "Save and Close"
        '
        'tlbbtn_Settings
        '
        Me.tlbbtn_Settings.Image = CType(resources.GetObject("tlbbtn_Settings.Image"), System.Drawing.Image)
        Me.tlbbtn_Settings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_Settings.Name = "tlbbtn_Settings"
        Me.tlbbtn_Settings.Size = New System.Drawing.Size(63, 50)
        Me.tlbbtn_Settings.Tag = "Settings"
        Me.tlbbtn_Settings.Text = "Settings"
        Me.tlbbtn_Settings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripButton1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(39, 50)
        Me.ToolStripButton1.Tag = "CQM"
        Me.ToolStripButton1.Text = "&CQM"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton1.ToolTipText = "Clinical Quality Measures"
        '
        'tlsbtn_ShowReportList
        '
        Me.tlsbtn_ShowReportList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtn_ShowReportList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlsbtn_ShowReportList.Image = CType(resources.GetObject("tlsbtn_ShowReportList.Image"), System.Drawing.Image)
        Me.tlsbtn_ShowReportList.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtn_ShowReportList.Name = "tlsbtn_ShowReportList"
        Me.tlsbtn_ShowReportList.Size = New System.Drawing.Size(58, 50)
        Me.tlsbtn_ShowReportList.Tag = "RefreshReportList"
        Me.tlsbtn_ShowReportList.Text = "&Refresh"
        Me.tlsbtn_ShowReportList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        Me.pnlC1Grid.Controls.Add(Me.Panel2)
        Me.pnlC1Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlC1Grid.Location = New System.Drawing.Point(0, 54)
        Me.pnlC1Grid.Name = "pnlC1Grid"
        Me.pnlC1Grid.Size = New System.Drawing.Size(1001, 610)
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
        Me.Panel1.Location = New System.Drawing.Point(0, 150)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(1001, 460)
        Me.Panel1.TabIndex = 111
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Location = New System.Drawing.Point(997, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 455)
        Me.Label12.TabIndex = 116
        '
        'C1QualityMeasures
        '
        Me.C1QualityMeasures.BackColor = System.Drawing.Color.GhostWhite
        Me.C1QualityMeasures.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1QualityMeasures.ColumnInfo = "1,0,0,0,0,105,Columns:"
        Me.C1QualityMeasures.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1QualityMeasures.ExtendLastCol = True
        Me.C1QualityMeasures.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1QualityMeasures.Location = New System.Drawing.Point(4, 1)
        Me.C1QualityMeasures.Name = "C1QualityMeasures"
        Me.C1QualityMeasures.Rows.Count = 1
        Me.C1QualityMeasures.Rows.DefaultSize = 21
        Me.C1QualityMeasures.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1QualityMeasures.Size = New System.Drawing.Size(994, 455)
        Me.C1QualityMeasures.StyleInfo = resources.GetString("C1QualityMeasures.StyleInfo")
        Me.C1QualityMeasures.TabIndex = 105
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Location = New System.Drawing.Point(3, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 455)
        Me.Label11.TabIndex = 115
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Location = New System.Drawing.Point(3, 456)
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
        Me.pnl.Controls.Add(Me.lbl_NPIValue)
        Me.pnl.Controls.Add(Me.lbl_CertificationValue)
        Me.pnl.Controls.Add(Me.lbl_Certification)
        Me.pnl.Controls.Add(Me.lbl_TaxIDValue)
        Me.pnl.Controls.Add(Me.lbl_TaxID)
        Me.pnl.Controls.Add(Me.Label15)
        Me.pnl.Controls.Add(Me.txtReportName)
        Me.pnl.Controls.Add(Me.Label14)
        Me.pnl.Controls.Add(Me.Label13)
        Me.pnl.Controls.Add(Me.Label7)
        Me.pnl.Controls.Add(Me.Label6)
        Me.pnl.Controls.Add(Me.Label5)
        Me.pnl.Controls.Add(Me.pnlMeasurementPeriod)
        Me.pnl.Controls.Add(Me.Label4)
        Me.pnl.Controls.Add(Me.lbl_PNPI)
        Me.pnl.Controls.Add(Me.Label3)
        Me.pnl.Controls.Add(Me.chk_FirstYear)
        Me.pnl.Controls.Add(Me.cmb_RptYear)
        Me.pnl.Controls.Add(Me.cmb_Provider)
        Me.pnl.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl.Location = New System.Drawing.Point(0, 54)
        Me.pnl.Name = "pnl"
        Me.pnl.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnl.Size = New System.Drawing.Size(1001, 96)
        Me.pnl.TabIndex = 110
        '
        'lbl_NPIValue
        '
        Me.lbl_NPIValue.AutoSize = True
        Me.lbl_NPIValue.BackColor = System.Drawing.Color.Transparent
        Me.lbl_NPIValue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NPIValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_NPIValue.Location = New System.Drawing.Point(130, 40)
        Me.lbl_NPIValue.Name = "lbl_NPIValue"
        Me.lbl_NPIValue.Size = New System.Drawing.Size(0, 14)
        Me.lbl_NPIValue.TabIndex = 121
        '
        'lbl_CertificationValue
        '
        Me.lbl_CertificationValue.AutoSize = True
        Me.lbl_CertificationValue.BackColor = System.Drawing.Color.Transparent
        Me.lbl_CertificationValue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_CertificationValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_CertificationValue.Location = New System.Drawing.Point(790, 40)
        Me.lbl_CertificationValue.Name = "lbl_CertificationValue"
        Me.lbl_CertificationValue.Size = New System.Drawing.Size(0, 14)
        Me.lbl_CertificationValue.TabIndex = 120
        Me.lbl_CertificationValue.Visible = False
        '
        'lbl_Certification
        '
        Me.lbl_Certification.AutoSize = True
        Me.lbl_Certification.BackColor = System.Drawing.Color.Transparent
        Me.lbl_Certification.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Certification.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_Certification.Location = New System.Drawing.Point(555, 40)
        Me.lbl_Certification.Name = "lbl_Certification"
        Me.lbl_Certification.Size = New System.Drawing.Size(230, 14)
        Me.lbl_Certification.TabIndex = 120
        Me.lbl_Certification.Text = "gloEMR's CMS EHR Certification Number :"
        Me.lbl_Certification.Visible = False
        '
        'lbl_TaxIDValue
        '
        Me.lbl_TaxIDValue.AutoSize = True
        Me.lbl_TaxIDValue.BackColor = System.Drawing.Color.Transparent
        Me.lbl_TaxIDValue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TaxIDValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TaxIDValue.Location = New System.Drawing.Point(372, 40)
        Me.lbl_TaxIDValue.Name = "lbl_TaxIDValue"
        Me.lbl_TaxIDValue.Size = New System.Drawing.Size(0, 14)
        Me.lbl_TaxIDValue.TabIndex = 120
        '
        'lbl_TaxID
        '
        Me.lbl_TaxID.AutoSize = True
        Me.lbl_TaxID.BackColor = System.Drawing.Color.Transparent
        Me.lbl_TaxID.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TaxID.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TaxID.Location = New System.Drawing.Point(272, 40)
        Me.lbl_TaxID.Name = "lbl_TaxID"
        Me.lbl_TaxID.Size = New System.Drawing.Size(99, 14)
        Me.lbl_TaxID.TabIndex = 120
        Me.lbl_TaxID.Text = "Provider Tax ID :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(285, 10)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(87, 14)
        Me.Label15.TabIndex = 119
        Me.Label15.Text = "Report Name :"
        '
        'txtReportName
        '
        Me.txtReportName.Location = New System.Drawing.Point(376, 8)
        Me.txtReportName.MaxLength = 255
        Me.txtReportName.Name = "txtReportName"
        Me.txtReportName.Size = New System.Drawing.Size(279, 22)
        Me.txtReportName.TabIndex = 118
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Location = New System.Drawing.Point(997, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 91)
        Me.Label14.TabIndex = 117
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Location = New System.Drawing.Point(3, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 91)
        Me.Label13.TabIndex = 116
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Location = New System.Drawing.Point(3, 92)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(995, 1)
        Me.Label7.TabIndex = 113
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Location = New System.Drawing.Point(3, 0)
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
        Me.Label5.Location = New System.Drawing.Point(809, 64)
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
        Me.pnlMeasurementPeriod.Location = New System.Drawing.Point(366, 59)
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
        Me.Label1.Location = New System.Drawing.Point(10, 8)
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
        Me.Label4.Location = New System.Drawing.Point(28, 65)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 14)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Reporting Year :"
        '
        'lbl_PNPI
        '
        Me.lbl_PNPI.AutoSize = True
        Me.lbl_PNPI.BackColor = System.Drawing.Color.Transparent
        Me.lbl_PNPI.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PNPI.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_PNPI.Location = New System.Drawing.Point(44, 40)
        Me.lbl_PNPI.Name = "lbl_PNPI"
        Me.lbl_PNPI.Size = New System.Drawing.Size(82, 14)
        Me.lbl_PNPI.TabIndex = 10
        Me.lbl_PNPI.Text = "Provider NPI :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(66, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 14)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Provider :"
        '
        'chk_FirstYear
        '
        Me.chk_FirstYear.AutoSize = True
        Me.chk_FirstYear.BackColor = System.Drawing.Color.Transparent
        Me.chk_FirstYear.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_FirstYear.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.chk_FirstYear.Location = New System.Drawing.Point(286, 66)
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
        Me.cmb_RptYear.Location = New System.Drawing.Point(128, 63)
        Me.cmb_RptYear.Name = "cmb_RptYear"
        Me.cmb_RptYear.Size = New System.Drawing.Size(140, 22)
        Me.cmb_RptYear.TabIndex = 1
        '
        'cmb_Provider
        '
        Me.cmb_Provider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Provider.FormattingEnabled = True
        Me.cmb_Provider.Location = New System.Drawing.Point(128, 8)
        Me.cmb_Provider.Name = "cmb_Provider"
        Me.cmb_Provider.Size = New System.Drawing.Size(140, 22)
        Me.cmb_Provider.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label16)
        Me.Panel2.Controls.Add(Me.Label17)
        Me.Panel2.Controls.Add(Me.Label18)
        Me.Panel2.Controls.Add(Me.Label19)
        Me.Panel2.Controls.Add(Me.Label20)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(1001, 54)
        Me.Panel2.TabIndex = 111
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(12, 12)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(977, 30)
        Me.Label16.TabIndex = 118
        Me.Label16.Text = resources.GetString("Label16.Text")
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label17.Location = New System.Drawing.Point(997, 4)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 46)
        Me.Label17.TabIndex = 117
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Location = New System.Drawing.Point(3, 4)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 46)
        Me.Label18.TabIndex = 116
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Location = New System.Drawing.Point(3, 50)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(995, 1)
        Me.Label19.TabIndex = 113
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(3, 3)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(995, 1)
        Me.Label20.TabIndex = 112
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
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.tlb_MUDashboard)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1001, 54)
        Me.Panel3.TabIndex = 112
        '
        'frm_MU_Dashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1001, 664)
        Me.Controls.Add(Me.pnlC1Grid)
        Me.Controls.Add(Me.Panel3)
        Me.DoubleBuffered = True
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
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents tlb_MUDashboard As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tlbbtn_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Print As System.Windows.Forms.ToolStripButton
    Private WithEvents tlbbtn_SavenClose As System.Windows.Forms.ToolStripButton
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
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtReportName As System.Windows.Forms.TextBox
    Private WithEvents tlsbtn_ShowReportList As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlbbtn_Settings As System.Windows.Forms.ToolStripButton
    Friend WithEvents lbl_Certification As System.Windows.Forms.Label
    Friend WithEvents lbl_TaxID As System.Windows.Forms.Label
    Friend WithEvents lbl_PNPI As System.Windows.Forms.Label
    Friend WithEvents lbl_CertificationValue As System.Windows.Forms.Label
    Friend WithEvents lbl_TaxIDValue As System.Windows.Forms.Label
    Friend WithEvents lbl_NPIValue As System.Windows.Forms.Label

    
End Class
