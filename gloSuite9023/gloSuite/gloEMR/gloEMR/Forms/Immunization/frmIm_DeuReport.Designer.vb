<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIm_DueReport
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
            Try
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
                Catch
                End Try

                Dim dtpControls As DateTimePicker() = {dtpVacTo, dtpVacFrom, dtFrom, dtReceiveTo, dtReceiveFrom}
                Try
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                    gloGlobal.cEventHelper.DisposeAllControls(dtpControls)
                Catch ex As Exception

                End Try
                
            Catch
            End Try


           

        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIm_DueReport))
        Me.btnViewReport = New System.Windows.Forms.Button()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.tblStrip_32 = New gloGlobal.gloToolStripIgnoreFocus()
        Me.tblbtnShowReport = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Print_32 = New System.Windows.Forms.ToolStripButton()
        Me.tblbtn_Close_32 = New System.Windows.Forms.ToolStripButton()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.C1FlexGrid1 = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlVacInventory = New System.Windows.Forms.Panel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.cmb_Location = New System.Windows.Forms.ComboBox()
        Me.rdbtnFundingSource = New System.Windows.Forms.RadioButton()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.ChkVaccineList = New System.Windows.Forms.CheckedListBox()
        Me.chkFundingSourceList = New System.Windows.Forms.CheckedListBox()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.rdbtnDateReceived = New System.Windows.Forms.RadioButton()
        Me.rdbtnVaccineGroup = New System.Windows.Forms.RadioButton()
        Me.rdbtnTrade = New System.Windows.Forms.RadioButton()
        Me.rdbtnVaccine = New System.Windows.Forms.RadioButton()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.chkDateRage = New System.Windows.Forms.CheckBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.dtReceiveTo = New System.Windows.Forms.DateTimePicker()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.dtReceiveFrom = New System.Windows.Forms.DateTimePicker()
        Me.pnlIm_Due = New System.Windows.Forms.Panel()
        Me.lbl_pnlBottom = New System.Windows.Forms.Label()
        Me.lbl_pnlLeft = New System.Windows.Forms.Label()
        Me.lbl_pnlRight = New System.Windows.Forms.Label()
        Me.lbl_pnlTop = New System.Windows.Forms.Label()
        Me.chkDueTemp = New System.Windows.Forms.CheckBox()
        Me.chkOverDueTemp = New System.Windows.Forms.CheckBox()
        Me.lblType = New System.Windows.Forms.Label()
        Me.lblIM = New System.Windows.Forms.Label()
        Me.chkMedicationList = New System.Windows.Forms.CheckedListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.pnlImDueReport = New System.Windows.Forms.Panel()
        Me.PnlVaccineReport = New System.Windows.Forms.Panel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cmbLocation = New System.Windows.Forms.ComboBox()
        Me.chkDateRange = New System.Windows.Forms.CheckBox()
        Me.rdb_AdminDate = New System.Windows.Forms.RadioButton()
        Me.rdb_VaccineGroup = New System.Windows.Forms.RadioButton()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dtpVacTo = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.dtpVacFrom = New System.Windows.Forms.DateTimePicker()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.cmb_Category = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.pnlBottom.SuspendLayout()
        Me.tblStrip_32.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlVacInventory.SuspendLayout()
        Me.pnlIm_Due.SuspendLayout()
        Me.PnlVaccineReport.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnViewReport
        '
        Me.btnViewReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnViewReport.BackgroundImage = CType(resources.GetObject("btnViewReport.BackgroundImage"), System.Drawing.Image)
        Me.btnViewReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnViewReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.btnViewReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnViewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewReport.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewReport.Location = New System.Drawing.Point(415, 9)
        Me.btnViewReport.Name = "btnViewReport"
        Me.btnViewReport.Size = New System.Drawing.Size(104, 23)
        Me.btnViewReport.TabIndex = 44
        Me.btnViewReport.Text = "Show Report"
        Me.btnViewReport.UseVisualStyleBackColor = True
        Me.btnViewReport.Visible = False
        '
        'pnlBottom
        '
        Me.pnlBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlBottom.Controls.Add(Me.btnViewReport)
        Me.pnlBottom.Controls.Add(Me.tblStrip_32)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlBottom.Location = New System.Drawing.Point(0, 0)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(887, 54)
        Me.pnlBottom.TabIndex = 1
        '
        'tblStrip_32
        '
        Me.tblStrip_32.BackColor = System.Drawing.Color.Transparent
        Me.tblStrip_32.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tblStrip_32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tblStrip_32.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tblStrip_32.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tblbtnShowReport, Me.tblbtn_Print_32, Me.tblbtn_Close_32})
        Me.tblStrip_32.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tblStrip_32.Location = New System.Drawing.Point(0, 0)
        Me.tblStrip_32.Name = "tblStrip_32"
        Me.tblStrip_32.Size = New System.Drawing.Size(887, 53)
        Me.tblStrip_32.TabIndex = 0
        Me.tblStrip_32.Text = "ToolStrip1"
        '
        'tblbtnShowReport
        '
        Me.tblbtnShowReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtnShowReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtnShowReport.Image = CType(resources.GetObject("tblbtnShowReport.Image"), System.Drawing.Image)
        Me.tblbtnShowReport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtnShowReport.Name = "tblbtnShowReport"
        Me.tblbtnShowReport.Size = New System.Drawing.Size(93, 50)
        Me.tblbtnShowReport.Text = "&Show Report"
        Me.tblbtnShowReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtnShowReport.Visible = False
        '
        'tblbtn_Print_32
        '
        Me.tblbtn_Print_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Print_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Print_32.Image = CType(resources.GetObject("tblbtn_Print_32.Image"), System.Drawing.Image)
        Me.tblbtn_Print_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Print_32.Name = "tblbtn_Print_32"
        Me.tblbtn_Print_32.Size = New System.Drawing.Size(41, 50)
        Me.tblbtn_Print_32.Text = "&Print"
        Me.tblbtn_Print_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tblbtn_Print_32.Visible = False
        '
        'tblbtn_Close_32
        '
        Me.tblbtn_Close_32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tblbtn_Close_32.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tblbtn_Close_32.Image = CType(resources.GetObject("tblbtn_Close_32.Image"), System.Drawing.Image)
        Me.tblbtn_Close_32.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tblbtn_Close_32.Name = "tblbtn_Close_32"
        Me.tblbtn_Close_32.Size = New System.Drawing.Size(43, 50)
        Me.tblbtn_Close_32.Text = "&Close"
        Me.tblbtn_Close_32.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.Label1)
        Me.pnlMain.Controls.Add(Me.Label3)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.ReportViewer1)
        Me.pnlMain.Controls.Add(Me.C1FlexGrid1)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 303)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlMain.Size = New System.Drawing.Size(887, 281)
        Me.pnlMain.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(4, 277)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(879, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label2"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label3.Location = New System.Drawing.Point(3, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 277)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label4.Location = New System.Drawing.Point(883, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 277)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label3"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label5.Location = New System.Drawing.Point(3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(881, 1)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "label1"
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportViewer1.Location = New System.Drawing.Point(3, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(881, 278)
        Me.ReportViewer1.TabIndex = 9
        '
        'C1FlexGrid1
        '
        Me.C1FlexGrid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1FlexGrid1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1FlexGrid1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1FlexGrid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1FlexGrid1.ColumnInfo = "10,1,0,0,0,95,Columns:"
        Me.C1FlexGrid1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1FlexGrid1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1FlexGrid1.Location = New System.Drawing.Point(200, 265)
        Me.C1FlexGrid1.Name = "C1FlexGrid1"
        Me.C1FlexGrid1.Rows.DefaultSize = 19
        Me.C1FlexGrid1.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.C1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1FlexGrid1.Size = New System.Drawing.Size(585, 92)
        Me.C1FlexGrid1.StyleInfo = resources.GetString("C1FlexGrid1.StyleInfo")
        Me.C1FlexGrid1.TabIndex = 0
        Me.C1FlexGrid1.Visible = False
        '
        'pnlVacInventory
        '
        Me.pnlVacInventory.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlVacInventory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlVacInventory.Controls.Add(Me.Label24)
        Me.pnlVacInventory.Controls.Add(Me.Label23)
        Me.pnlVacInventory.Controls.Add(Me.cmb_Category)
        Me.pnlVacInventory.Controls.Add(Me.cmb_Location)
        Me.pnlVacInventory.Controls.Add(Me.rdbtnFundingSource)
        Me.pnlVacInventory.Controls.Add(Me.Label21)
        Me.pnlVacInventory.Controls.Add(Me.Label20)
        Me.pnlVacInventory.Controls.Add(Me.ChkVaccineList)
        Me.pnlVacInventory.Controls.Add(Me.chkFundingSourceList)
        Me.pnlVacInventory.Controls.Add(Me.chkActive)
        Me.pnlVacInventory.Controls.Add(Me.rdbtnDateReceived)
        Me.pnlVacInventory.Controls.Add(Me.rdbtnVaccineGroup)
        Me.pnlVacInventory.Controls.Add(Me.rdbtnTrade)
        Me.pnlVacInventory.Controls.Add(Me.rdbtnVaccine)
        Me.pnlVacInventory.Controls.Add(Me.Label18)
        Me.pnlVacInventory.Controls.Add(Me.chkDateRage)
        Me.pnlVacInventory.Controls.Add(Me.Label13)
        Me.pnlVacInventory.Controls.Add(Me.dtReceiveTo)
        Me.pnlVacInventory.Controls.Add(Me.Label14)
        Me.pnlVacInventory.Controls.Add(Me.Label15)
        Me.pnlVacInventory.Controls.Add(Me.Label16)
        Me.pnlVacInventory.Controls.Add(Me.Label17)
        Me.pnlVacInventory.Controls.Add(Me.Label19)
        Me.pnlVacInventory.Controls.Add(Me.dtReceiveFrom)
        Me.pnlVacInventory.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlVacInventory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlVacInventory.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlVacInventory.Location = New System.Drawing.Point(0, 98)
        Me.pnlVacInventory.Name = "pnlVacInventory"
        Me.pnlVacInventory.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlVacInventory.Size = New System.Drawing.Size(887, 205)
        Me.pnlVacInventory.TabIndex = 11
        Me.pnlVacInventory.Visible = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Location = New System.Drawing.Point(73, 50)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(61, 14)
        Me.Label23.TabIndex = 71
        Me.Label23.Text = "Location :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_Location
        '
        Me.cmb_Location.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Location.FormattingEnabled = True
        Me.cmb_Location.Location = New System.Drawing.Point(135, 46)
        Me.cmb_Location.Name = "cmb_Location"
        Me.cmb_Location.Size = New System.Drawing.Size(155, 22)
        Me.cmb_Location.TabIndex = 70
        '
        'rdbtnFundingSource
        '
        Me.rdbtnFundingSource.AutoSize = True
        Me.rdbtnFundingSource.Location = New System.Drawing.Point(708, 41)
        Me.rdbtnFundingSource.Name = "rdbtnFundingSource"
        Me.rdbtnFundingSource.Size = New System.Drawing.Size(121, 18)
        Me.rdbtnFundingSource.TabIndex = 69
        Me.rdbtnFundingSource.TabStop = True
        Me.rdbtnFundingSource.Text = "Funding Received"
        Me.rdbtnFundingSource.UseVisualStyleBackColor = True
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Location = New System.Drawing.Point(34, 111)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(100, 14)
        Me.Label21.TabIndex = 68
        Me.Label21.Text = "Funding Source :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Location = New System.Drawing.Point(532, 108)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(57, 14)
        Me.Label20.TabIndex = 67
        Me.Label20.Text = "Vaccine :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ChkVaccineList
        '
        Me.ChkVaccineList.FormattingEnabled = True
        Me.ChkVaccineList.Location = New System.Drawing.Point(595, 106)
        Me.ChkVaccineList.Name = "ChkVaccineList"
        Me.ChkVaccineList.Size = New System.Drawing.Size(269, 89)
        Me.ChkVaccineList.TabIndex = 66
        '
        'chkFundingSourceList
        '
        Me.chkFundingSourceList.FormattingEnabled = True
        Me.chkFundingSourceList.Location = New System.Drawing.Point(135, 106)
        Me.chkFundingSourceList.Name = "chkFundingSourceList"
        Me.chkFundingSourceList.Size = New System.Drawing.Size(269, 89)
        Me.chkFundingSourceList.TabIndex = 65
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.Location = New System.Drawing.Point(135, 78)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(104, 18)
        Me.chkActive.TabIndex = 63
        Me.chkActive.Text = "Show Inactive"
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'rdbtnDateReceived
        '
        Me.rdbtnDateReceived.AutoSize = True
        Me.rdbtnDateReceived.Location = New System.Drawing.Point(596, 41)
        Me.rdbtnDateReceived.Name = "rdbtnDateReceived"
        Me.rdbtnDateReceived.Size = New System.Drawing.Size(109, 18)
        Me.rdbtnDateReceived.TabIndex = 62
        Me.rdbtnDateReceived.TabStop = True
        Me.rdbtnDateReceived.Text = "Date received  "
        Me.rdbtnDateReceived.UseVisualStyleBackColor = True
        '
        'rdbtnVaccineGroup
        '
        Me.rdbtnVaccineGroup.AutoSize = True
        Me.rdbtnVaccineGroup.Location = New System.Drawing.Point(771, 12)
        Me.rdbtnVaccineGroup.Name = "rdbtnVaccineGroup"
        Me.rdbtnVaccineGroup.Size = New System.Drawing.Size(108, 18)
        Me.rdbtnVaccineGroup.TabIndex = 61
        Me.rdbtnVaccineGroup.TabStop = True
        Me.rdbtnVaccineGroup.Text = "Vaccine Group "
        Me.rdbtnVaccineGroup.UseVisualStyleBackColor = True
        '
        'rdbtnTrade
        '
        Me.rdbtnTrade.AutoSize = True
        Me.rdbtnTrade.Location = New System.Drawing.Point(673, 13)
        Me.rdbtnTrade.Name = "rdbtnTrade"
        Me.rdbtnTrade.Size = New System.Drawing.Size(92, 18)
        Me.rdbtnTrade.TabIndex = 60
        Me.rdbtnTrade.TabStop = True
        Me.rdbtnTrade.Text = "Trade Name"
        Me.rdbtnTrade.UseVisualStyleBackColor = True
        '
        'rdbtnVaccine
        '
        Me.rdbtnVaccine.AutoSize = True
        Me.rdbtnVaccine.Location = New System.Drawing.Point(596, 13)
        Me.rdbtnVaccine.Name = "rdbtnVaccine"
        Me.rdbtnVaccine.Size = New System.Drawing.Size(67, 18)
        Me.rdbtnVaccine.TabIndex = 59
        Me.rdbtnVaccine.TabStop = True
        Me.rdbtnVaccine.Text = "Vaccine"
        Me.rdbtnVaccine.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(532, 15)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(55, 14)
        Me.Label18.TabIndex = 58
        Me.Label18.Text = "Sort by :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkDateRage
        '
        Me.chkDateRage.AutoSize = True
        Me.chkDateRage.Location = New System.Drawing.Point(135, 14)
        Me.chkDateRage.Name = "chkDateRage"
        Me.chkDateRage.Size = New System.Drawing.Size(15, 14)
        Me.chkDateRage.TabIndex = 57
        Me.chkDateRage.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(271, 14)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(113, 14)
        Me.Label13.TabIndex = 54
        Me.Label13.Text = "Date Received To :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtReceiveTo
        '
        Me.dtReceiveTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtReceiveTo.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtReceiveTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtReceiveTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtReceiveTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtReceiveTo.CustomFormat = "MM/dd/yyyy"
        Me.dtReceiveTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtReceiveTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtReceiveTo.Location = New System.Drawing.Point(386, 10)
        Me.dtReceiveTo.Name = "dtReceiveTo"
        Me.dtReceiveTo.Size = New System.Drawing.Size(98, 22)
        Me.dtReceiveTo.TabIndex = 53
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Location = New System.Drawing.Point(4, 201)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(879, 1)
        Me.Label14.TabIndex = 52
        Me.Label14.Text = "label2"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Location = New System.Drawing.Point(3, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 198)
        Me.Label15.TabIndex = 51
        Me.Label15.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Location = New System.Drawing.Point(883, 4)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 198)
        Me.Label16.TabIndex = 50
        Me.Label16.Text = "label3"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label17.Location = New System.Drawing.Point(3, 3)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(881, 1)
        Me.Label17.TabIndex = 49
        Me.Label17.Text = "label1"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Location = New System.Drawing.Point(9, 14)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(125, 14)
        Me.Label19.TabIndex = 41
        Me.Label19.Text = "Date Received From :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtReceiveFrom
        '
        Me.dtReceiveFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtReceiveFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtReceiveFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtReceiveFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtReceiveFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtReceiveFrom.CustomFormat = "MM/dd/yyyy"
        Me.dtReceiveFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtReceiveFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtReceiveFrom.Location = New System.Drawing.Point(153, 10)
        Me.dtReceiveFrom.Name = "dtReceiveFrom"
        Me.dtReceiveFrom.Size = New System.Drawing.Size(98, 22)
        Me.dtReceiveFrom.TabIndex = 38
        '
        'pnlIm_Due
        '
        Me.pnlIm_Due.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlIm_Due.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlIm_Due.Controls.Add(Me.lbl_pnlBottom)
        Me.pnlIm_Due.Controls.Add(Me.lbl_pnlLeft)
        Me.pnlIm_Due.Controls.Add(Me.lbl_pnlRight)
        Me.pnlIm_Due.Controls.Add(Me.lbl_pnlTop)
        Me.pnlIm_Due.Controls.Add(Me.chkDueTemp)
        Me.pnlIm_Due.Controls.Add(Me.chkOverDueTemp)
        Me.pnlIm_Due.Controls.Add(Me.lblType)
        Me.pnlIm_Due.Controls.Add(Me.lblIM)
        Me.pnlIm_Due.Controls.Add(Me.chkMedicationList)
        Me.pnlIm_Due.Controls.Add(Me.Label2)
        Me.pnlIm_Due.Controls.Add(Me.dtFrom)
        Me.pnlIm_Due.Controls.Add(Me.pnlImDueReport)
        Me.pnlIm_Due.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlIm_Due.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlIm_Due.Location = New System.Drawing.Point(53, 135)
        Me.pnlIm_Due.Name = "pnlIm_Due"
        Me.pnlIm_Due.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlIm_Due.Size = New System.Drawing.Size(783, 91)
        Me.pnlIm_Due.TabIndex = 0
        Me.pnlIm_Due.Visible = False
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 87)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(775, 1)
        Me.lbl_pnlBottom.TabIndex = 52
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 95)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 0)
        Me.lbl_pnlLeft.TabIndex = 51
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(779, 95)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 0)
        Me.lbl_pnlRight.TabIndex = 50
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 94)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(777, 1)
        Me.lbl_pnlTop.TabIndex = 49
        Me.lbl_pnlTop.Text = "label1"
        '
        'chkDueTemp
        '
        Me.chkDueTemp.AutoSize = True
        Me.chkDueTemp.BackColor = System.Drawing.Color.Transparent
        Me.chkDueTemp.Checked = True
        Me.chkDueTemp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDueTemp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDueTemp.Location = New System.Drawing.Point(99, 61)
        Me.chkDueTemp.Name = "chkDueTemp"
        Me.chkDueTemp.Size = New System.Drawing.Size(48, 18)
        Me.chkDueTemp.TabIndex = 48
        Me.chkDueTemp.Text = "Due"
        Me.chkDueTemp.UseVisualStyleBackColor = False
        '
        'chkOverDueTemp
        '
        Me.chkOverDueTemp.AutoSize = True
        Me.chkOverDueTemp.BackColor = System.Drawing.Color.Transparent
        Me.chkOverDueTemp.Checked = True
        Me.chkOverDueTemp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOverDueTemp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOverDueTemp.Location = New System.Drawing.Point(99, 38)
        Me.chkOverDueTemp.Name = "chkOverDueTemp"
        Me.chkOverDueTemp.Size = New System.Drawing.Size(73, 18)
        Me.chkOverDueTemp.TabIndex = 47
        Me.chkOverDueTemp.Text = "Overdue"
        Me.chkOverDueTemp.UseVisualStyleBackColor = False
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.BackColor = System.Drawing.Color.Transparent
        Me.lblType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblType.Location = New System.Drawing.Point(46, 40)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(50, 14)
        Me.lblType.TabIndex = 46
        Me.lblType.Text = "Status :"
        Me.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblIM
        '
        Me.lblIM.AutoSize = True
        Me.lblIM.BackColor = System.Drawing.Color.Transparent
        Me.lblIM.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblIM.Location = New System.Drawing.Point(235, 11)
        Me.lblIM.Name = "lblIM"
        Me.lblIM.Size = New System.Drawing.Size(87, 14)
        Me.lblIM.TabIndex = 45
        Me.lblIM.Text = "Immunization :"
        Me.lblIM.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkMedicationList
        '
        Me.chkMedicationList.CheckOnClick = True
        Me.chkMedicationList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMedicationList.ForeColor = System.Drawing.Color.Black
        Me.chkMedicationList.FormattingEnabled = True
        Me.chkMedicationList.HorizontalScrollbar = True
        Me.chkMedicationList.Location = New System.Drawing.Point(328, 11)
        Me.chkMedicationList.Name = "chkMedicationList"
        Me.chkMedicationList.Size = New System.Drawing.Size(363, 72)
        Me.chkMedicationList.TabIndex = 42
        Me.chkMedicationList.UseCompatibleTextRendering = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(34, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 14)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "For Date :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtFrom
        '
        Me.dtFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtFrom.CustomFormat = "MM/dd/yyyy"
        Me.dtFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtFrom.Location = New System.Drawing.Point(98, 11)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(98, 22)
        Me.dtFrom.TabIndex = 38
        '
        'pnlImDueReport
        '
        Me.pnlImDueReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlImDueReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlImDueReport.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlImDueReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlImDueReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnlImDueReport.Location = New System.Drawing.Point(3, 3)
        Me.pnlImDueReport.Name = "pnlImDueReport"
        Me.pnlImDueReport.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlImDueReport.Size = New System.Drawing.Size(777, 91)
        Me.pnlImDueReport.TabIndex = 53
        Me.pnlImDueReport.Visible = False
        '
        'PnlVaccineReport
        '
        Me.PnlVaccineReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.PnlVaccineReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PnlVaccineReport.Controls.Add(Me.Label22)
        Me.PnlVaccineReport.Controls.Add(Me.cmbLocation)
        Me.PnlVaccineReport.Controls.Add(Me.chkDateRange)
        Me.PnlVaccineReport.Controls.Add(Me.rdb_AdminDate)
        Me.PnlVaccineReport.Controls.Add(Me.rdb_VaccineGroup)
        Me.PnlVaccineReport.Controls.Add(Me.Label11)
        Me.PnlVaccineReport.Controls.Add(Me.dtpVacTo)
        Me.PnlVaccineReport.Controls.Add(Me.Label6)
        Me.PnlVaccineReport.Controls.Add(Me.Label7)
        Me.PnlVaccineReport.Controls.Add(Me.Label8)
        Me.PnlVaccineReport.Controls.Add(Me.Label9)
        Me.PnlVaccineReport.Controls.Add(Me.Label10)
        Me.PnlVaccineReport.Controls.Add(Me.Label12)
        Me.PnlVaccineReport.Controls.Add(Me.dtpVacFrom)
        Me.PnlVaccineReport.Dock = System.Windows.Forms.DockStyle.Top
        Me.PnlVaccineReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlVaccineReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.PnlVaccineReport.Location = New System.Drawing.Point(0, 54)
        Me.PnlVaccineReport.Name = "PnlVaccineReport"
        Me.PnlVaccineReport.Padding = New System.Windows.Forms.Padding(3)
        Me.PnlVaccineReport.Size = New System.Drawing.Size(887, 44)
        Me.PnlVaccineReport.TabIndex = 10
        Me.PnlVaccineReport.Visible = False
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Location = New System.Drawing.Point(342, 15)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(61, 14)
        Me.Label22.TabIndex = 60
        Me.Label22.Text = "Location :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbLocation
        '
        Me.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLocation.FormattingEnabled = True
        Me.cmbLocation.Location = New System.Drawing.Point(409, 12)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Size = New System.Drawing.Size(155, 22)
        Me.cmbLocation.TabIndex = 59
        '
        'chkDateRange
        '
        Me.chkDateRange.AutoSize = True
        Me.chkDateRange.Location = New System.Drawing.Point(58, 14)
        Me.chkDateRange.Name = "chkDateRange"
        Me.chkDateRange.Size = New System.Drawing.Size(15, 14)
        Me.chkDateRange.TabIndex = 57
        Me.chkDateRange.UseVisualStyleBackColor = True
        '
        'rdb_AdminDate
        '
        Me.rdb_AdminDate.AutoSize = True
        Me.rdb_AdminDate.Location = New System.Drawing.Point(746, 12)
        Me.rdb_AdminDate.Name = "rdb_AdminDate"
        Me.rdb_AdminDate.Size = New System.Drawing.Size(132, 18)
        Me.rdb_AdminDate.TabIndex = 56
        Me.rdb_AdminDate.TabStop = True
        Me.rdb_AdminDate.Text = "Administration Date"
        Me.rdb_AdminDate.UseVisualStyleBackColor = True
        '
        'rdb_VaccineGroup
        '
        Me.rdb_VaccineGroup.AutoSize = True
        Me.rdb_VaccineGroup.Location = New System.Drawing.Point(635, 12)
        Me.rdb_VaccineGroup.Name = "rdb_VaccineGroup"
        Me.rdb_VaccineGroup.Size = New System.Drawing.Size(108, 18)
        Me.rdb_VaccineGroup.TabIndex = 55
        Me.rdb_VaccineGroup.TabStop = True
        Me.rdb_VaccineGroup.Text = "Vaccine Group "
        Me.rdb_VaccineGroup.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(181, 14)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(30, 14)
        Me.Label11.TabIndex = 54
        Me.Label11.Text = "To :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpVacTo
        '
        Me.dtpVacTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpVacTo.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpVacTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpVacTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpVacTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpVacTo.CustomFormat = "MM/dd/yyyy"
        Me.dtpVacTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpVacTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpVacTo.Location = New System.Drawing.Point(214, 10)
        Me.dtpVacTo.Name = "dtpVacTo"
        Me.dtpVacTo.Size = New System.Drawing.Size(98, 22)
        Me.dtpVacTo.TabIndex = 53
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Location = New System.Drawing.Point(4, 40)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(879, 1)
        Me.Label6.TabIndex = 52
        Me.Label6.Text = "label2"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(3, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 37)
        Me.Label7.TabIndex = 51
        Me.Label7.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Location = New System.Drawing.Point(883, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 37)
        Me.Label8.TabIndex = 50
        Me.Label8.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Location = New System.Drawing.Point(3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(881, 1)
        Me.Label9.TabIndex = 49
        Me.Label9.Text = "label1"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(570, 14)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(55, 14)
        Me.Label10.TabIndex = 46
        Me.Label10.Text = "Sort by :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(10, 14)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(42, 14)
        Me.Label12.TabIndex = 41
        Me.Label12.Text = "From :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpVacFrom
        '
        Me.dtpVacFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpVacFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpVacFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpVacFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpVacFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpVacFrom.CustomFormat = "MM/dd/yyyy"
        Me.dtpVacFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpVacFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpVacFrom.Location = New System.Drawing.Point(79, 10)
        Me.dtpVacFrom.Name = "dtpVacFrom"
        Me.dtpVacFrom.Size = New System.Drawing.Size(98, 22)
        Me.dtpVacFrom.TabIndex = 38
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'cmb_Category
        '
        Me.cmb_Category.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Category.FormattingEnabled = True
        Me.cmb_Category.Location = New System.Drawing.Point(381, 46)
        Me.cmb_Category.Name = "cmb_Category"
        Me.cmb_Category.Size = New System.Drawing.Size(155, 22)
        Me.cmb_Category.TabIndex = 70
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Location = New System.Drawing.Point(314, 50)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(64, 14)
        Me.Label24.TabIndex = 71
        Me.Label24.Text = "Category :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmIm_DueReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(887, 584)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnlVacInventory)
        Me.Controls.Add(Me.PnlVaccineReport)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.pnlIm_Due)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmIm_DueReport"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Immunization Due Report"
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.tblStrip_32.ResumeLayout(False)
        Me.tblStrip_32.PerformLayout()
        Me.pnlMain.ResumeLayout(False)
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlVacInventory.ResumeLayout(False)
        Me.pnlVacInventory.PerformLayout()
        Me.pnlIm_Due.ResumeLayout(False)
        Me.pnlIm_Due.PerformLayout()
        Me.PnlVaccineReport.ResumeLayout(False)
        Me.PnlVaccineReport.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents C1FlexGrid1 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents btnViewReport As System.Windows.Forms.Button
    Friend WithEvents tblStrip_32 As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents tblbtn_Print_32 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tblbtn_Close_32 As System.Windows.Forms.ToolStripButton
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tblbtnShowReport As System.Windows.Forms.ToolStripButton
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip

    Public Sub New(ByVal PatientID As Long)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        _PatientID = PatientID
    End Sub
    Public Sub New(ByVal PatientID As Long, ByVal ReportName As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        _PatientID = PatientID
        _RptName = ReportName
    End Sub
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents PnlVaccineReport As System.Windows.Forms.Panel
    Friend WithEvents chkDateRange As System.Windows.Forms.CheckBox
    Friend WithEvents rdb_AdminDate As System.Windows.Forms.RadioButton
    Friend WithEvents rdb_VaccineGroup As System.Windows.Forms.RadioButton
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtpVacTo As System.Windows.Forms.DateTimePicker
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents dtpVacFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlIm_Due As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Friend WithEvents chkDueTemp As System.Windows.Forms.CheckBox
    Friend WithEvents chkOverDueTemp As System.Windows.Forms.CheckBox
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents lblIM As System.Windows.Forms.Label
    Friend WithEvents chkMedicationList As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlImDueReport As System.Windows.Forms.Panel
    Friend WithEvents pnlVacInventory As System.Windows.Forms.Panel
    Friend WithEvents rdbtnDateReceived As System.Windows.Forms.RadioButton
    Friend WithEvents rdbtnVaccineGroup As System.Windows.Forms.RadioButton
    Friend WithEvents rdbtnTrade As System.Windows.Forms.RadioButton
    Friend WithEvents rdbtnVaccine As System.Windows.Forms.RadioButton
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents chkDateRage As System.Windows.Forms.CheckBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents dtReceiveTo As System.Windows.Forms.DateTimePicker
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents dtReceiveFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents ChkVaccineList As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkFundingSourceList As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents rdbtnFundingSource As System.Windows.Forms.RadioButton
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents cmbLocation As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents cmb_Location As System.Windows.Forms.ComboBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents cmb_Category As System.Windows.Forms.ComboBox
End Class
