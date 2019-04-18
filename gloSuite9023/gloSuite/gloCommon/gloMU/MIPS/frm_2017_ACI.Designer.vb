<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_2017_ACI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_2017_ACI))
        Me.pnlC1Grid = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnlcustomTask = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.C1QualityMeasures = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnl = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpicStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dtpicEndDate = New System.Windows.Forms.DateTimePicker()
        Me.btnClearProvider = New System.Windows.Forms.Button()
        Me.btnbrprov = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbprovtaxid = New System.Windows.Forms.ComboBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lblACICatValue = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lbltotalValue = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.lblPerformanceScore = New System.Windows.Forms.Label()
        Me.lblPerformanceScoreData = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblBaseScore = New System.Windows.Forms.Label()
        Me.lblBaseScoreData = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lbl_NPIValue = New System.Windows.Forms.Label()
        Me.lbl_TaxIDValue = New System.Windows.Forms.Label()
        Me.lbl_TaxID = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtReportName = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbl_PNPI = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
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
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.C1SuperNPIToolTip = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.tlb_MUDashboard = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tlbbtn_Print = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Save = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_SavenClose = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Settings = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.tsp_QRDAIII = New System.Windows.Forms.ToolStripButton()
        Me.tlsbtn_ShowReportList = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_Close = New System.Windows.Forms.ToolStripButton()
        Me.pnlC1Grid.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.C1QualityMeasures, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.tlb_MUDashboard.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlC1Grid
        '
        Me.pnlC1Grid.Controls.Add(Me.Panel1)
        Me.pnlC1Grid.Controls.Add(Me.pnl)
        Me.pnlC1Grid.Controls.Add(Me.Panel2)
        Me.pnlC1Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlC1Grid.Location = New System.Drawing.Point(0, 54)
        Me.pnlC1Grid.Name = "pnlC1Grid"
        Me.pnlC1Grid.Size = New System.Drawing.Size(1189, 610)
        Me.pnlC1Grid.TabIndex = 109
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.pnlcustomTask)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.C1QualityMeasures)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 180)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(1189, 430)
        Me.Panel1.TabIndex = 111
        '
        'pnlcustomTask
        '
        Me.pnlcustomTask.Location = New System.Drawing.Point(299, 91)
        Me.pnlcustomTask.Name = "pnlcustomTask"
        Me.pnlcustomTask.Size = New System.Drawing.Size(590, 259)
        Me.pnlcustomTask.TabIndex = 118
        Me.pnlcustomTask.Visible = False
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label12.Location = New System.Drawing.Point(1185, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 425)
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
        Me.C1QualityMeasures.Size = New System.Drawing.Size(1182, 425)
        Me.C1QualityMeasures.StyleInfo = resources.GetString("C1QualityMeasures.StyleInfo")
        Me.C1QualityMeasures.TabIndex = 105
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Location = New System.Drawing.Point(3, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 425)
        Me.Label11.TabIndex = 115
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Location = New System.Drawing.Point(3, 426)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1183, 1)
        Me.Label9.TabIndex = 114
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label8.Location = New System.Drawing.Point(3, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1183, 1)
        Me.Label8.TabIndex = 113
        '
        'pnl
        '
        Me.pnl.Controls.Add(Me.Label1)
        Me.pnl.Controls.Add(Me.dtpicStartDate)
        Me.pnl.Controls.Add(Me.Label10)
        Me.pnl.Controls.Add(Me.dtpicEndDate)
        Me.pnl.Controls.Add(Me.btnClearProvider)
        Me.pnl.Controls.Add(Me.btnbrprov)
        Me.pnl.Controls.Add(Me.Label2)
        Me.pnl.Controls.Add(Me.cmbprovtaxid)
        Me.pnl.Controls.Add(Me.Panel4)
        Me.pnl.Controls.Add(Me.lbl_NPIValue)
        Me.pnl.Controls.Add(Me.lbl_TaxIDValue)
        Me.pnl.Controls.Add(Me.lbl_TaxID)
        Me.pnl.Controls.Add(Me.Label15)
        Me.pnl.Controls.Add(Me.txtReportName)
        Me.pnl.Controls.Add(Me.Label14)
        Me.pnl.Controls.Add(Me.Label13)
        Me.pnl.Controls.Add(Me.Label7)
        Me.pnl.Controls.Add(Me.Label6)
        Me.pnl.Controls.Add(Me.Label5)
        Me.pnl.Controls.Add(Me.Label4)
        Me.pnl.Controls.Add(Me.lbl_PNPI)
        Me.pnl.Controls.Add(Me.Label3)
        Me.pnl.Controls.Add(Me.cmb_RptYear)
        Me.pnl.Controls.Add(Me.cmb_Provider)
        Me.pnl.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl.Location = New System.Drawing.Point(0, 65)
        Me.pnl.Name = "pnl"
        Me.pnl.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnl.Size = New System.Drawing.Size(1189, 115)
        Me.pnl.TabIndex = 110
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(364, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 14)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Measurement Period :"
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
        Me.dtpicStartDate.Location = New System.Drawing.Point(536, 60)
        Me.dtpicStartDate.Name = "dtpicStartDate"
        Me.dtpicStartDate.Size = New System.Drawing.Size(98, 22)
        Me.dtpicStartDate.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(492, 68)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(38, 14)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "From "
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
        Me.dtpicEndDate.Location = New System.Drawing.Point(663, 60)
        Me.dtpicEndDate.Name = "dtpicEndDate"
        Me.dtpicEndDate.Size = New System.Drawing.Size(98, 22)
        Me.dtpicEndDate.TabIndex = 10
        '
        'btnClearProvider
        '
        Me.btnClearProvider.BackColor = System.Drawing.Color.Transparent
        Me.btnClearProvider.BackgroundImage = CType(resources.GetObject("btnClearProvider.BackgroundImage"), System.Drawing.Image)
        Me.btnClearProvider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearProvider.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnClearProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearProvider.Image = CType(resources.GetObject("btnClearProvider.Image"), System.Drawing.Image)
        Me.btnClearProvider.Location = New System.Drawing.Point(378, 11)
        Me.btnClearProvider.Name = "btnClearProvider"
        Me.btnClearProvider.Size = New System.Drawing.Size(22, 22)
        Me.btnClearProvider.TabIndex = 140
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
        Me.btnbrprov.Location = New System.Drawing.Point(353, 11)
        Me.btnbrprov.Name = "btnbrprov"
        Me.btnbrprov.Size = New System.Drawing.Size(22, 22)
        Me.btnbrprov.TabIndex = 139
        Me.btnbrprov.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(640, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 14)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "To "
        '
        'cmbprovtaxid
        '
        Me.cmbprovtaxid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbprovtaxid.FormattingEnabled = True
        Me.cmbprovtaxid.Location = New System.Drawing.Point(121, 38)
        Me.cmbprovtaxid.Name = "cmbprovtaxid"
        Me.cmbprovtaxid.Size = New System.Drawing.Size(226, 22)
        Me.cmbprovtaxid.TabIndex = 136
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.MintCream
        Me.Panel4.Controls.Add(Me.Label27)
        Me.Panel4.Controls.Add(Me.Panel7)
        Me.Panel4.Controls.Add(Me.Panel8)
        Me.Panel4.Controls.Add(Me.Panel6)
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Controls.Add(Me.Label28)
        Me.Panel4.Controls.Add(Me.Label29)
        Me.Panel4.Controls.Add(Me.Label30)
        Me.Panel4.Location = New System.Drawing.Point(960, 6)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(217, 100)
        Me.Panel4.TabIndex = 135
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.Green
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label27.Location = New System.Drawing.Point(1, 99)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(215, 1)
        Me.Label27.TabIndex = 137
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Label22)
        Me.Panel7.Controls.Add(Me.lblACICatValue)
        Me.Panel7.Controls.Add(Me.Label26)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(1, 73)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(215, 27)
        Me.Panel7.TabIndex = 136
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Green
        Me.Label22.Location = New System.Drawing.Point(0, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.Label22.Size = New System.Drawing.Size(173, 27)
        Me.Label22.TabIndex = 129
        Me.Label22.Text = "ACI Category :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblACICatValue
        '
        Me.lblACICatValue.AutoSize = True
        Me.lblACICatValue.BackColor = System.Drawing.Color.Transparent
        Me.lblACICatValue.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblACICatValue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblACICatValue.ForeColor = System.Drawing.Color.Green
        Me.lblACICatValue.Location = New System.Drawing.Point(173, 0)
        Me.lblACICatValue.Name = "lblACICatValue"
        Me.lblACICatValue.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.lblACICatValue.Size = New System.Drawing.Size(15, 21)
        Me.lblACICatValue.TabIndex = 132
        Me.lblACICatValue.Text = "0"
        '
        'Label26
        '
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Green
        Me.Label26.Location = New System.Drawing.Point(188, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(27, 27)
        Me.Label26.TabIndex = 134
        Me.Label26.Text = "pts"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label26.Visible = False
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.Label21)
        Me.Panel8.Controls.Add(Me.lbltotalValue)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(1, 49)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(215, 24)
        Me.Panel8.TabIndex = 136
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Green
        Me.Label21.Location = New System.Drawing.Point(0, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.Label21.Size = New System.Drawing.Size(186, 24)
        Me.Label21.TabIndex = 128
        Me.Label21.Text = "Total :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbltotalValue
        '
        Me.lbltotalValue.AutoSize = True
        Me.lbltotalValue.BackColor = System.Drawing.Color.Transparent
        Me.lbltotalValue.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbltotalValue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotalValue.ForeColor = System.Drawing.Color.Green
        Me.lbltotalValue.Location = New System.Drawing.Point(186, 0)
        Me.lbltotalValue.Name = "lbltotalValue"
        Me.lbltotalValue.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.lbltotalValue.Size = New System.Drawing.Size(29, 19)
        Me.lbltotalValue.TabIndex = 131
        Me.lbltotalValue.Text = "0%"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.lblPerformanceScore)
        Me.Panel6.Controls.Add(Me.lblPerformanceScoreData)
        Me.Panel6.Controls.Add(Me.Label25)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(1, 25)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(215, 24)
        Me.Panel6.TabIndex = 136
        '
        'lblPerformanceScore
        '
        Me.lblPerformanceScore.BackColor = System.Drawing.Color.Transparent
        Me.lblPerformanceScore.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPerformanceScore.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPerformanceScore.ForeColor = System.Drawing.Color.Green
        Me.lblPerformanceScore.Location = New System.Drawing.Point(0, 0)
        Me.lblPerformanceScore.Name = "lblPerformanceScore"
        Me.lblPerformanceScore.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.lblPerformanceScore.Size = New System.Drawing.Size(181, 24)
        Me.lblPerformanceScore.TabIndex = 127
        Me.lblPerformanceScore.Text = "Performance Score :"
        Me.lblPerformanceScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPerformanceScoreData
        '
        Me.lblPerformanceScoreData.AutoSize = True
        Me.lblPerformanceScoreData.BackColor = System.Drawing.Color.Transparent
        Me.lblPerformanceScoreData.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblPerformanceScoreData.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPerformanceScoreData.ForeColor = System.Drawing.Color.Green
        Me.lblPerformanceScoreData.Location = New System.Drawing.Point(181, 0)
        Me.lblPerformanceScoreData.Name = "lblPerformanceScoreData"
        Me.lblPerformanceScoreData.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.lblPerformanceScoreData.Size = New System.Drawing.Size(15, 19)
        Me.lblPerformanceScoreData.TabIndex = 125
        Me.lblPerformanceScoreData.Text = "0"
        '
        'Label25
        '
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Green
        Me.Label25.Location = New System.Drawing.Point(196, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(19, 24)
        Me.Label25.TabIndex = 133
        Me.Label25.Text = "%"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label25.Visible = False
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.lblBaseScore)
        Me.Panel5.Controls.Add(Me.lblBaseScoreData)
        Me.Panel5.Controls.Add(Me.Label24)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(1, 1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(215, 24)
        Me.Panel5.TabIndex = 136
        '
        'lblBaseScore
        '
        Me.lblBaseScore.BackColor = System.Drawing.Color.Transparent
        Me.lblBaseScore.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblBaseScore.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBaseScore.ForeColor = System.Drawing.Color.Green
        Me.lblBaseScore.Location = New System.Drawing.Point(0, 0)
        Me.lblBaseScore.Name = "lblBaseScore"
        Me.lblBaseScore.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.lblBaseScore.Size = New System.Drawing.Size(181, 24)
        Me.lblBaseScore.TabIndex = 126
        Me.lblBaseScore.Text = "Base Score :"
        Me.lblBaseScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBaseScoreData
        '
        Me.lblBaseScoreData.AutoSize = True
        Me.lblBaseScoreData.BackColor = System.Drawing.Color.Transparent
        Me.lblBaseScoreData.Dock = System.Windows.Forms.DockStyle.Right
        Me.lblBaseScoreData.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBaseScoreData.ForeColor = System.Drawing.Color.Green
        Me.lblBaseScoreData.Location = New System.Drawing.Point(181, 0)
        Me.lblBaseScoreData.Name = "lblBaseScoreData"
        Me.lblBaseScoreData.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.lblBaseScoreData.Size = New System.Drawing.Size(15, 19)
        Me.lblBaseScoreData.TabIndex = 123
        Me.lblBaseScoreData.Text = "0"
        Me.lblBaseScoreData.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label24
        '
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Green
        Me.Label24.Location = New System.Drawing.Point(196, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(19, 24)
        Me.Label24.TabIndex = 113
        Me.Label24.Text = "%"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label24.Visible = False
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.Green
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label28.Location = New System.Drawing.Point(1, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(215, 1)
        Me.Label28.TabIndex = 138
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.Green
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label29.Location = New System.Drawing.Point(0, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 100)
        Me.Label29.TabIndex = 139
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.Green
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label30.Location = New System.Drawing.Point(216, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(1, 100)
        Me.Label30.TabIndex = 140
        '
        'lbl_NPIValue
        '
        Me.lbl_NPIValue.AutoSize = True
        Me.lbl_NPIValue.BackColor = System.Drawing.Color.Transparent
        Me.lbl_NPIValue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NPIValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_NPIValue.Location = New System.Drawing.Point(492, 43)
        Me.lbl_NPIValue.MaximumSize = New System.Drawing.Size(550, 0)
        Me.lbl_NPIValue.Name = "lbl_NPIValue"
        Me.lbl_NPIValue.Size = New System.Drawing.Size(79, 14)
        Me.lbl_NPIValue.TabIndex = 121
        Me.lbl_NPIValue.Text = "123123123"
        '
        'lbl_TaxIDValue
        '
        Me.lbl_TaxIDValue.AutoSize = True
        Me.lbl_TaxIDValue.BackColor = System.Drawing.Color.Transparent
        Me.lbl_TaxIDValue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TaxIDValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_TaxIDValue.Location = New System.Drawing.Point(472, 92)
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
        Me.lbl_TaxID.Location = New System.Drawing.Point(16, 43)
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
        Me.Label15.Location = New System.Drawing.Point(404, 14)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(87, 14)
        Me.Label15.TabIndex = 119
        Me.Label15.Text = "Report Name :"
        '
        'txtReportName
        '
        Me.txtReportName.Location = New System.Drawing.Point(493, 10)
        Me.txtReportName.MaxLength = 255
        Me.txtReportName.Name = "txtReportName"
        Me.txtReportName.Size = New System.Drawing.Size(271, 22)
        Me.txtReportName.TabIndex = 118
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label14.Location = New System.Drawing.Point(1185, 1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 110)
        Me.Label14.TabIndex = 117
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Location = New System.Drawing.Point(3, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 110)
        Me.Label13.TabIndex = 116
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Location = New System.Drawing.Point(3, 111)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1183, 1)
        Me.Label7.TabIndex = 113
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Location = New System.Drawing.Point(3, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1183, 1)
        Me.Label6.TabIndex = 112
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(772, 68)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(186, 14)
        Me.Label5.TabIndex = 111
        Me.Label5.Text = "Reporting Period In Progress"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(18, 68)
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
        Me.lbl_PNPI.Location = New System.Drawing.Point(409, 43)
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
        Me.Label3.Location = New System.Drawing.Point(56, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 14)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Provider :"
        '
        'cmb_RptYear
        '
        Me.cmb_RptYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_RptYear.FormattingEnabled = True
        Me.cmb_RptYear.Location = New System.Drawing.Point(121, 65)
        Me.cmb_RptYear.Name = "cmb_RptYear"
        Me.cmb_RptYear.Size = New System.Drawing.Size(226, 22)
        Me.cmb_RptYear.TabIndex = 1
        '
        'cmb_Provider
        '
        Me.cmb_Provider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Provider.FormattingEnabled = True
        Me.cmb_Provider.Location = New System.Drawing.Point(121, 10)
        Me.cmb_Provider.Name = "cmb_Provider"
        Me.cmb_Provider.Size = New System.Drawing.Size(226, 22)
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
        Me.Panel2.Size = New System.Drawing.Size(1189, 65)
        Me.Panel2.TabIndex = 111
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(12, 12)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(977, 49)
        Me.Label16.TabIndex = 118
        Me.Label16.Text = resources.GetString("Label16.Text")
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label17.Location = New System.Drawing.Point(1185, 4)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 57)
        Me.Label17.TabIndex = 117
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Location = New System.Drawing.Point(3, 4)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 57)
        Me.Label18.TabIndex = 116
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Location = New System.Drawing.Point(3, 61)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1183, 1)
        Me.Label19.TabIndex = 113
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Location = New System.Drawing.Point(3, 3)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1183, 1)
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
        Me.ImgFlag.Images.SetKeyName(3, "Tick_Grid.ico")
        Me.ImgFlag.Images.SetKeyName(4, "Tick.png")
        Me.ImgFlag.Images.SetKeyName(5, "Close.png")
        Me.ImgFlag.Images.SetKeyName(6, "Tick_Grid_Red.ico")
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.tlb_MUDashboard)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1189, 54)
        Me.Panel3.TabIndex = 112
        '
        'C1SuperNPIToolTip
        '
        Me.C1SuperNPIToolTip.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperNPIToolTip.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'tlb_MUDashboard
        '
        Me.tlb_MUDashboard.BackColor = System.Drawing.Color.Transparent
        Me.tlb_MUDashboard.BackgroundImage = CType(resources.GetObject("tlb_MUDashboard.BackgroundImage"), System.Drawing.Image)
        Me.tlb_MUDashboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tlb_MUDashboard.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_MUDashboard.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tlb_MUDashboard.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtn_Print, Me.tlbbtn_Save, Me.tlbbtn_SavenClose, Me.tlbbtn_Settings, Me.ToolStripButton1, Me.tsp_QRDAIII, Me.tlsbtn_ShowReportList, Me.tlbbtn_Close})
        Me.tlb_MUDashboard.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tlb_MUDashboard.Location = New System.Drawing.Point(0, 0)
        Me.tlb_MUDashboard.Name = "tlb_MUDashboard"
        Me.tlb_MUDashboard.Size = New System.Drawing.Size(1189, 53)
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
        Me.ToolStripButton1.Text = "CQ&M"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton1.ToolTipText = "Clinical Quality Measures"
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
        'frm_2017_ACI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1189, 664)
        Me.Controls.Add(Me.pnlC1Grid)
        Me.Controls.Add(Me.Panel3)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_2017_ACI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PI (Promoting Interoperability) TRANSITION"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlC1Grid.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.C1QualityMeasures, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl.ResumeLayout(False)
        Me.pnl.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.tlb_MUDashboard.ResumeLayout(False)
        Me.tlb_MUDashboard.PerformLayout()
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
    Friend WithEvents cmb_RptYear As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
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
    Friend WithEvents lbl_PNPI As System.Windows.Forms.Label
    Friend WithEvents lbl_TaxIDValue As System.Windows.Forms.Label
    Friend WithEvents lbl_NPIValue As System.Windows.Forms.Label
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents lblPerformanceScoreData As System.Windows.Forms.Label
    Friend WithEvents lblBaseScoreData As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents lblPerformanceScore As System.Windows.Forms.Label
    Friend WithEvents lblBaseScore As System.Windows.Forms.Label
    Friend WithEvents lblACICatValue As System.Windows.Forms.Label
    Friend WithEvents lbltotalValue As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents btnClearProvider As System.Windows.Forms.Button
    Friend WithEvents btnbrprov As System.Windows.Forms.Button
    Friend WithEvents pnlcustomTask As System.Windows.Forms.Panel
    Friend WithEvents cmbprovtaxid As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_TaxID As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents C1SuperNPIToolTip As C1.Win.C1SuperTooltip.C1SuperTooltip
    Private WithEvents tsp_QRDAIII As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog


End Class
