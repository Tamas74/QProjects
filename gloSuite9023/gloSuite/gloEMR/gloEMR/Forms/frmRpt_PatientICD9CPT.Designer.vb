<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRpt_PatientICD9CPT
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
            

            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(Me)
            Catch
            End Try
            Dim dtpControls As DateTimePicker() = {dtpicFrom, dtpicTo}
            Try
                gloGlobal.cEventHelper.RemoveAllEventHandlers(dtpControls)
                gloGlobal.cEventHelper.DisposeAllControls(dtpControls)
            Catch ex As Exception

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRpt_PatientICD9CPT))
        Me.dtpicTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpicFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmbProvider = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbLocation = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtDiagnosis = New System.Windows.Forms.TextBox()
        Me.pnlInternalControl = New System.Windows.Forms.Panel()
        Me.txtToproc = New System.Windows.Forms.TextBox()
        Me.txtFrom = New System.Windows.Forms.TextBox()
        Me.txtProcedure = New System.Windows.Forms.TextBox()
        Me.pnl_tls_PtICD9CPTReport = New System.Windows.Forms.Panel()
        Me.tls_ReportCriteria = New gloGlobal.gloToolStripIgnoreFocus()
        Me.btn_tls_Selectall = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.btn_tls_PrintReport = New System.Windows.Forms.ToolStripButton()
        Me.btn_tls_ShowReport = New System.Windows.Forms.ToolStripButton()
        Me.btnShowReportList = New System.Windows.Forms.ToolStripButton()
        Me.tlbbtn_HL7 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.tlbbtnmenuItm_HL7_MU1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tlbbtnmenuItm_HL7_MU2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tlbbtnmenuItm_HL7_MU3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tlbmenuItm_HL7_MU3_A04 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tlbmenuItm_HL7_MU3_A08 = New System.Windows.Forms.ToolStripMenuItem()
        Me.btn_tls_Close = New System.Windows.Forms.ToolStripButton()
        Me.pnlICD9CPT = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.C1grdPatients = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.RbDrDxLocation = New System.Windows.Forms.RadioButton()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.RbDxbyProc = New System.Windows.Forms.RadioButton()
        Me.RbDrDxProc = New System.Windows.Forms.RadioButton()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.RbDrDx = New System.Windows.Forms.RadioButton()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.rbALL = New System.Windows.Forms.RadioButton()
        Me.rbICD10 = New System.Windows.Forms.RadioButton()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.btnClearAllSelectedDx = New System.Windows.Forms.Button()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.rbICD9 = New System.Windows.Forms.RadioButton()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblSelectedDrugs = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnAddToSearch = New System.Windows.Forms.Button()
        Me.trvselecteddia = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.trvDiagnosis = New System.Windows.Forms.TreeView()
        Me.txtSearchDiagnosis = New System.Windows.Forms.TextBox()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.trvLocation = New System.Windows.Forms.TreeView()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.btnbtnDeselectLocation = New System.Windows.Forms.Button()
        Me.btnSelectLocation = New System.Windows.Forms.Button()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.trvProvider = New System.Windows.Forms.TreeView()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.btnDeSelectProvider = New System.Windows.Forms.Button()
        Me.btnSelectProvider = New System.Windows.Forms.Button()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.chkDiagnosis = New System.Windows.Forms.CheckedListBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl_LeftBrd = New System.Windows.Forms.Label()
        Me.pnlProcedureControl = New System.Windows.Forms.Panel()
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.pnl_tls_PtICD9CPTReport.SuspendLayout()
        Me.tls_ReportCriteria.SuspendLayout()
        Me.pnlICD9CPT.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.C1grdPatients, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel10.SuspendLayout()
        Me.Panel17.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtpicTo
        '
        Me.dtpicTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpicTo.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpicTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpicTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpicTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpicTo.CustomFormat = "MM/dd/yyyy"
        Me.dtpicTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpicTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpicTo.Location = New System.Drawing.Point(225, 8)
        Me.dtpicTo.Name = "dtpicTo"
        Me.dtpicTo.Size = New System.Drawing.Size(92, 22)
        Me.dtpicTo.TabIndex = 4
        '
        'dtpicFrom
        '
        Me.dtpicFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpicFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpicFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpicFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpicFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpicFrom.CustomFormat = "MM/dd/yyyy"
        Me.dtpicFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpicFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpicFrom.Location = New System.Drawing.Point(103, 8)
        Me.dtpicFrom.Name = "dtpicFrom"
        Me.dtpicFrom.Size = New System.Drawing.Size(92, 22)
        Me.dtpicFrom.TabIndex = 3
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.BackColor = System.Drawing.Color.Transparent
        Me.lblTo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.Location = New System.Drawing.Point(199, 12)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(22, 14)
        Me.lblTo.TabIndex = 4
        Me.lblTo.Text = "To"
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrom.Location = New System.Drawing.Point(66, 12)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(34, 14)
        Me.lblFrom.TabIndex = 0
        Me.lblFrom.Text = "From"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox2.Controls.Add(Me.cmbProvider)
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(872, 84)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(18, 46)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Provider"
        Me.GroupBox2.Visible = False
        '
        'cmbProvider
        '
        Me.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProvider.ForeColor = System.Drawing.Color.Black
        Me.cmbProvider.FormattingEnabled = True
        Me.cmbProvider.Location = New System.Drawing.Point(7, 18)
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.Size = New System.Drawing.Size(174, 22)
        Me.cmbProvider.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox1.Controls.Add(Me.cmbLocation)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(872, 32)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(18, 46)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Location"
        Me.GroupBox1.Visible = False
        '
        'cmbLocation
        '
        Me.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLocation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLocation.ForeColor = System.Drawing.Color.Black
        Me.cmbLocation.FormattingEnabled = True
        Me.cmbLocation.Location = New System.Drawing.Point(7, 18)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Size = New System.Drawing.Size(174, 22)
        Me.cmbLocation.TabIndex = 2
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox3.Controls.Add(Me.txtDiagnosis)
        Me.GroupBox3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox3.Location = New System.Drawing.Point(872, 136)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(18, 46)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Diagnosis"
        Me.GroupBox3.Visible = False
        '
        'txtDiagnosis
        '
        Me.txtDiagnosis.Location = New System.Drawing.Point(7, 18)
        Me.txtDiagnosis.Name = "txtDiagnosis"
        Me.txtDiagnosis.Size = New System.Drawing.Size(94, 22)
        Me.txtDiagnosis.TabIndex = 0
        '
        'pnlInternalControl
        '
        Me.pnlInternalControl.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlInternalControl.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlInternalControl.Location = New System.Drawing.Point(872, 6)
        Me.pnlInternalControl.Name = "pnlInternalControl"
        Me.pnlInternalControl.Size = New System.Drawing.Size(18, 20)
        Me.pnlInternalControl.TabIndex = 10
        Me.pnlInternalControl.Visible = False
        '
        'txtToproc
        '
        Me.txtToproc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToproc.Location = New System.Drawing.Point(294, 61)
        Me.txtToproc.Name = "txtToproc"
        Me.txtToproc.Size = New System.Drawing.Size(60, 22)
        Me.txtToproc.TabIndex = 1
        '
        'txtFrom
        '
        Me.txtFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFrom.Location = New System.Drawing.Point(202, 61)
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(60, 22)
        Me.txtFrom.TabIndex = 0
        '
        'txtProcedure
        '
        Me.txtProcedure.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProcedure.Location = New System.Drawing.Point(496, 61)
        Me.txtProcedure.Name = "txtProcedure"
        Me.txtProcedure.Size = New System.Drawing.Size(60, 22)
        Me.txtProcedure.TabIndex = 9
        '
        'pnl_tls_PtICD9CPTReport
        '
        Me.pnl_tls_PtICD9CPTReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tls_PtICD9CPTReport.Controls.Add(Me.tls_ReportCriteria)
        Me.pnl_tls_PtICD9CPTReport.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tls_PtICD9CPTReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tls_PtICD9CPTReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tls_PtICD9CPTReport.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tls_PtICD9CPTReport.Name = "pnl_tls_PtICD9CPTReport"
        Me.pnl_tls_PtICD9CPTReport.Size = New System.Drawing.Size(1286, 54)
        Me.pnl_tls_PtICD9CPTReport.TabIndex = 11
        '
        'tls_ReportCriteria
        '
        Me.tls_ReportCriteria.BackColor = System.Drawing.Color.Transparent
        Me.tls_ReportCriteria.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_ReportCriteria.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_ReportCriteria.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_ReportCriteria.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_ReportCriteria.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_Selectall, Me.ToolStripButton1, Me.btn_tls_PrintReport, Me.btn_tls_ShowReport, Me.btnShowReportList, Me.tlbbtn_HL7, Me.btn_tls_Close})
        Me.tls_ReportCriteria.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_ReportCriteria.Location = New System.Drawing.Point(0, 0)
        Me.tls_ReportCriteria.Name = "tls_ReportCriteria"
        Me.tls_ReportCriteria.Size = New System.Drawing.Size(1286, 53)
        Me.tls_ReportCriteria.TabIndex = 0
        Me.tls_ReportCriteria.Text = "toolStrip1"
        '
        'btn_tls_Selectall
        '
        Me.btn_tls_Selectall.Image = CType(resources.GetObject("btn_tls_Selectall.Image"), System.Drawing.Image)
        Me.btn_tls_Selectall.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Selectall.Name = "btn_tls_Selectall"
        Me.btn_tls_Selectall.Size = New System.Drawing.Size(67, 50)
        Me.btn_tls_Selectall.Tag = "Selectall"
        Me.btn_tls_Selectall.Text = "Select &All"
        Me.btn_tls_Selectall.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btn_tls_Selectall.Visible = False
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(81, 50)
        Me.ToolStripButton1.Tag = "Selectall"
        Me.ToolStripButton1.Text = "&Deselect All"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolStripButton1.Visible = False
        '
        'btn_tls_PrintReport
        '
        Me.btn_tls_PrintReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_PrintReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_tls_PrintReport.Image = CType(resources.GetObject("btn_tls_PrintReport.Image"), System.Drawing.Image)
        Me.btn_tls_PrintReport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_PrintReport.Name = "btn_tls_PrintReport"
        Me.btn_tls_PrintReport.Size = New System.Drawing.Size(88, 50)
        Me.btn_tls_PrintReport.Tag = "PrintReport"
        Me.btn_tls_PrintReport.Text = "&Print Report"
        Me.btn_tls_PrintReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btn_tls_ShowReport
        '
        Me.btn_tls_ShowReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_ShowReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_tls_ShowReport.Image = CType(resources.GetObject("btn_tls_ShowReport.Image"), System.Drawing.Image)
        Me.btn_tls_ShowReport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_ShowReport.Name = "btn_tls_ShowReport"
        Me.btn_tls_ShowReport.Size = New System.Drawing.Size(87, 50)
        Me.btn_tls_ShowReport.Tag = "ViewReport"
        Me.btn_tls_ShowReport.Text = "&View Report"
        Me.btn_tls_ShowReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btnShowReportList
        '
        Me.btnShowReportList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShowReportList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btnShowReportList.Image = CType(resources.GetObject("btnShowReportList.Image"), System.Drawing.Image)
        Me.btnShowReportList.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnShowReportList.Name = "btnShowReportList"
        Me.btnShowReportList.Size = New System.Drawing.Size(93, 50)
        Me.btnShowReportList.Tag = "ShowReportList"
        Me.btnShowReportList.Text = "&Show Report"
        Me.btnShowReportList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlbbtn_HL7
        '
        Me.tlbbtn_HL7.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbbtnmenuItm_HL7_MU1, Me.tlbbtnmenuItm_HL7_MU2, Me.tlbbtnmenuItm_HL7_MU3})
        Me.tlbbtn_HL7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtn_HL7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtn_HL7.Image = CType(resources.GetObject("tlbbtn_HL7.Image"), System.Drawing.Image)
        Me.tlbbtn_HL7.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlbbtn_HL7.Name = "tlbbtn_HL7"
        Me.tlbbtn_HL7.Size = New System.Drawing.Size(119, 50)
        Me.tlbbtn_HL7.Text = "&Gen Surveillance"
        Me.tlbbtn_HL7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlbbtn_HL7.ToolTipText = "Generate Surveillance"
        '
        'tlbbtnmenuItm_HL7_MU1
        '
        Me.tlbbtnmenuItm_HL7_MU1.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtnmenuItm_HL7_MU1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnmenuItm_HL7_MU1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtnmenuItm_HL7_MU1.Image = CType(resources.GetObject("tlbbtnmenuItm_HL7_MU1.Image"), System.Drawing.Image)
        Me.tlbbtnmenuItm_HL7_MU1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tlbbtnmenuItm_HL7_MU1.Name = "tlbbtnmenuItm_HL7_MU1"
        Me.tlbbtnmenuItm_HL7_MU1.Size = New System.Drawing.Size(152, 22)
        Me.tlbbtnmenuItm_HL7_MU1.Text = "MU 1"
        '
        'tlbbtnmenuItm_HL7_MU2
        '
        Me.tlbbtnmenuItm_HL7_MU2.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtnmenuItm_HL7_MU2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnmenuItm_HL7_MU2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtnmenuItm_HL7_MU2.Image = CType(resources.GetObject("tlbbtnmenuItm_HL7_MU2.Image"), System.Drawing.Image)
        Me.tlbbtnmenuItm_HL7_MU2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tlbbtnmenuItm_HL7_MU2.Name = "tlbbtnmenuItm_HL7_MU2"
        Me.tlbbtnmenuItm_HL7_MU2.Size = New System.Drawing.Size(152, 22)
        Me.tlbbtnmenuItm_HL7_MU2.Text = "MU 2"
        '
        'tlbbtnmenuItm_HL7_MU3
        '
        Me.tlbbtnmenuItm_HL7_MU3.BackColor = System.Drawing.Color.Transparent
        Me.tlbbtnmenuItm_HL7_MU3.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbmenuItm_HL7_MU3_A04, Me.tlbmenuItm_HL7_MU3_A08})
        Me.tlbbtnmenuItm_HL7_MU3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlbbtnmenuItm_HL7_MU3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbbtnmenuItm_HL7_MU3.Image = CType(resources.GetObject("tlbbtnmenuItm_HL7_MU3.Image"), System.Drawing.Image)
        Me.tlbbtnmenuItm_HL7_MU3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tlbbtnmenuItm_HL7_MU3.Name = "tlbbtnmenuItm_HL7_MU3"
        Me.tlbbtnmenuItm_HL7_MU3.Size = New System.Drawing.Size(152, 22)
        Me.tlbbtnmenuItm_HL7_MU3.Text = "MU 3"
        '
        'tlbmenuItm_HL7_MU3_A04
        '
        Me.tlbmenuItm_HL7_MU3_A04.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbmenuItm_HL7_MU3_A04.Image = CType(resources.GetObject("tlbmenuItm_HL7_MU3_A04.Image"), System.Drawing.Image)
        Me.tlbmenuItm_HL7_MU3_A04.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tlbmenuItm_HL7_MU3_A04.Name = "tlbmenuItm_HL7_MU3_A04"
        Me.tlbmenuItm_HL7_MU3_A04.Size = New System.Drawing.Size(218, 22)
        Me.tlbmenuItm_HL7_MU3_A04.Text = "Register/Discharge Patient"
        '
        'tlbmenuItm_HL7_MU3_A08
        '
        Me.tlbmenuItm_HL7_MU3_A08.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.tlbmenuItm_HL7_MU3_A08.Image = CType(resources.GetObject("tlbmenuItm_HL7_MU3_A08.Image"), System.Drawing.Image)
        Me.tlbmenuItm_HL7_MU3_A08.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tlbmenuItm_HL7_MU3_A08.Name = "tlbmenuItm_HL7_MU3_A08"
        Me.tlbmenuItm_HL7_MU3_A08.Size = New System.Drawing.Size(226, 22)
        Me.tlbmenuItm_HL7_MU3_A08.Text = "Update Patient"
        '
        'btn_tls_Close
        '
        Me.btn_tls_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tls_Close.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.btn_tls_Close.Image = CType(resources.GetObject("btn_tls_Close.Image"), System.Drawing.Image)
        Me.btn_tls_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_tls_Close.Name = "btn_tls_Close"
        Me.btn_tls_Close.Size = New System.Drawing.Size(43, 50)
        Me.btn_tls_Close.Tag = "Close"
        Me.btn_tls_Close.Text = "&Close"
        Me.btn_tls_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'pnlICD9CPT
        '
        Me.pnlICD9CPT.Controls.Add(Me.Panel7)
        Me.pnlICD9CPT.Controls.Add(Me.Panel10)
        Me.pnlICD9CPT.Controls.Add(Me.Panel8)
        Me.pnlICD9CPT.Controls.Add(Me.chkDiagnosis)
        Me.pnlICD9CPT.Controls.Add(Me.Panel2)
        Me.pnlICD9CPT.Controls.Add(Me.pnlInternalControl)
        Me.pnlICD9CPT.Controls.Add(Me.GroupBox2)
        Me.pnlICD9CPT.Controls.Add(Me.GroupBox1)
        Me.pnlICD9CPT.Controls.Add(Me.GroupBox3)
        Me.pnlICD9CPT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlICD9CPT.Location = New System.Drawing.Point(0, 54)
        Me.pnlICD9CPT.Name = "pnlICD9CPT"
        Me.pnlICD9CPT.Size = New System.Drawing.Size(1286, 661)
        Me.pnlICD9CPT.TabIndex = 12
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.C1grdPatients)
        Me.Panel7.Controls.Add(Me.Label19)
        Me.Panel7.Controls.Add(Me.Label20)
        Me.Panel7.Controls.Add(Me.Label21)
        Me.Panel7.Controls.Add(Me.Label22)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel7.Location = New System.Drawing.Point(0, 378)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel7.Size = New System.Drawing.Size(1286, 283)
        Me.Panel7.TabIndex = 103
        '
        'C1grdPatients
        '
        Me.C1grdPatients.BackColor = System.Drawing.Color.GhostWhite
        Me.C1grdPatients.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1grdPatients.ColumnInfo = "1,0,0,0,0,105,Columns:"
        Me.C1grdPatients.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1grdPatients.ExtendLastCol = True
        Me.C1grdPatients.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1grdPatients.Location = New System.Drawing.Point(4, 4)
        Me.C1grdPatients.Name = "C1grdPatients"
        Me.C1grdPatients.Rows.Count = 1
        Me.C1grdPatients.Rows.DefaultSize = 21
        Me.C1grdPatients.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1grdPatients.Size = New System.Drawing.Size(1278, 275)
        Me.C1grdPatients.StyleInfo = resources.GetString("C1grdPatients.StyleInfo")
        Me.C1grdPatients.TabIndex = 104
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(4, 279)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1278, 1)
        Me.Label19.TabIndex = 7
        Me.Label19.Text = "label4"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(4, 3)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1278, 1)
        Me.Label20.TabIndex = 6
        Me.Label20.Text = "label4"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(1282, 3)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(1, 277)
        Me.Label21.TabIndex = 5
        Me.Label21.Text = "label4"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(3, 3)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(1, 277)
        Me.Label22.TabIndex = 4
        Me.Label22.Text = "label4"
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.Panel17)
        Me.Panel10.Controls.Add(Me.Panel6)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(0, 269)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(1286, 109)
        Me.Panel10.TabIndex = 105
        '
        'Panel17
        '
        Me.Panel17.Controls.Add(Me.RbDrDxLocation)
        Me.Panel17.Controls.Add(Me.Panel16)
        Me.Panel17.Controls.Add(Me.RbDxbyProc)
        Me.Panel17.Controls.Add(Me.RbDrDxProc)
        Me.Panel17.Controls.Add(Me.Label49)
        Me.Panel17.Controls.Add(Me.Label50)
        Me.Panel17.Controls.Add(Me.RbDrDx)
        Me.Panel17.Controls.Add(Me.Label51)
        Me.Panel17.Controls.Add(Me.Label52)
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel17.Location = New System.Drawing.Point(639, 0)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Panel17.Size = New System.Drawing.Size(647, 109)
        Me.Panel17.TabIndex = 103
        '
        'RbDrDxLocation
        '
        Me.RbDrDxLocation.AutoSize = True
        Me.RbDrDxLocation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbDrDxLocation.Location = New System.Drawing.Point(399, 71)
        Me.RbDrDxLocation.Name = "RbDrDxLocation"
        Me.RbDrDxLocation.Size = New System.Drawing.Size(188, 18)
        Me.RbDrDxLocation.TabIndex = 13
        Me.RbDrDxLocation.TabStop = True
        Me.RbDrDxLocation.Text = "By Provider by Dx by Location"
        Me.RbDrDxLocation.UseVisualStyleBackColor = True
        '
        'Panel16
        '
        Me.Panel16.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel16.Controls.Add(Me.Label2)
        Me.Panel16.Controls.Add(Me.Label23)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel16.Location = New System.Drawing.Point(4, 1)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(639, 28)
        Me.Panel16.TabIndex = 15
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(639, 1)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "label4"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Location = New System.Drawing.Point(0, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(639, 28)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "   Report Summary Criteria"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RbDxbyProc
        '
        Me.RbDxbyProc.AutoSize = True
        Me.RbDxbyProc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbDxbyProc.Location = New System.Drawing.Point(24, 46)
        Me.RbDxbyProc.Name = "RbDxbyProc"
        Me.RbDxbyProc.Size = New System.Drawing.Size(138, 18)
        Me.RbDxbyProc.TabIndex = 10
        Me.RbDxbyProc.TabStop = True
        Me.RbDxbyProc.Text = "By Dx by Procedures"
        Me.RbDxbyProc.UseVisualStyleBackColor = True
        '
        'RbDrDxProc
        '
        Me.RbDrDxProc.AutoSize = True
        Me.RbDrDxProc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbDrDxProc.Location = New System.Drawing.Point(24, 71)
        Me.RbDrDxProc.Name = "RbDrDxProc"
        Me.RbDrDxProc.Size = New System.Drawing.Size(203, 18)
        Me.RbDrDxProc.TabIndex = 11
        Me.RbDrDxProc.TabStop = True
        Me.RbDrDxProc.Text = "By Provider by Dx by Procedures"
        Me.RbDrDxProc.UseVisualStyleBackColor = True
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.Location = New System.Drawing.Point(4, 108)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(639, 1)
        Me.Label49.TabIndex = 7
        Me.Label49.Text = "label4"
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label50.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(4, 0)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(639, 1)
        Me.Label50.TabIndex = 6
        Me.Label50.Text = "label4"
        '
        'RbDrDx
        '
        Me.RbDrDx.AutoSize = True
        Me.RbDrDx.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbDrDx.Location = New System.Drawing.Point(399, 46)
        Me.RbDrDx.Name = "RbDrDx"
        Me.RbDrDx.Size = New System.Drawing.Size(121, 18)
        Me.RbDrDx.TabIndex = 12
        Me.RbDrDx.TabStop = True
        Me.RbDrDx.Text = "By Provider by Dx"
        Me.RbDrDx.UseVisualStyleBackColor = True
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label51.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.Location = New System.Drawing.Point(643, 0)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(1, 109)
        Me.Label51.TabIndex = 5
        Me.Label51.Text = "label4"
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.Location = New System.Drawing.Point(3, 0)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(1, 109)
        Me.Label52.TabIndex = 4
        Me.Label52.Text = "label4"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel15)
        Me.Panel6.Controls.Add(Me.txtFrom)
        Me.Panel6.Controls.Add(Me.txtProcedure)
        Me.Panel6.Controls.Add(Me.Label29)
        Me.Panel6.Controls.Add(Me.Label24)
        Me.Panel6.Controls.Add(Me.Label25)
        Me.Panel6.Controls.Add(Me.Label28)
        Me.Panel6.Controls.Add(Me.txtToproc)
        Me.Panel6.Controls.Add(Me.Label15)
        Me.Panel6.Controls.Add(Me.Label16)
        Me.Panel6.Controls.Add(Me.Label17)
        Me.Panel6.Controls.Add(Me.Label18)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Panel6.Size = New System.Drawing.Size(639, 109)
        Me.Panel6.TabIndex = 103
        '
        'Panel15
        '
        Me.Panel15.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel15.Controls.Add(Me.Label1)
        Me.Panel15.Controls.Add(Me.Label27)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel15.Location = New System.Drawing.Point(4, 1)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(634, 28)
        Me.Panel15.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(634, 1)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "label4"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Location = New System.Drawing.Point(0, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(634, 28)
        Me.Label27.TabIndex = 0
        Me.Label27.Text = "    Procedure "
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(39, 65)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(120, 14)
        Me.Label29.TabIndex = 1
        Me.Label29.Text = "Procedure Range :"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(165, 65)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(34, 14)
        Me.Label24.TabIndex = 1
        Me.Label24.Text = "From"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(269, 65)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(22, 14)
        Me.Label25.TabIndex = 1
        Me.Label25.Text = "To"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(376, 65)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(118, 14)
        Me.Label28.TabIndex = 1
        Me.Label28.Text = "Select Procedure :"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(4, 108)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(634, 1)
        Me.Label15.TabIndex = 7
        Me.Label15.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(634, 1)
        Me.Label16.TabIndex = 6
        Me.Label16.Text = "label4"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(638, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 109)
        Me.Label17.TabIndex = 5
        Me.Label17.Text = "label4"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(3, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 109)
        Me.Label18.TabIndex = 4
        Me.Label18.Text = "label4"
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.Panel5)
        Me.Panel8.Controls.Add(Me.Panel9)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel8.Location = New System.Drawing.Point(0, 40)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Padding = New System.Windows.Forms.Padding(3, 0, 0, 3)
        Me.Panel8.Size = New System.Drawing.Size(1286, 229)
        Me.Panel8.TabIndex = 104
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.rbALL)
        Me.Panel5.Controls.Add(Me.rbICD10)
        Me.Panel5.Controls.Add(Me.Panel13)
        Me.Panel5.Controls.Add(Me.rbICD9)
        Me.Panel5.Controls.Add(Me.Label11)
        Me.Panel5.Controls.Add(Me.lblSelectedDrugs)
        Me.Panel5.Controls.Add(Me.Label12)
        Me.Panel5.Controls.Add(Me.Label13)
        Me.Panel5.Controls.Add(Me.Label30)
        Me.Panel5.Controls.Add(Me.btnRemove)
        Me.Panel5.Controls.Add(Me.Label14)
        Me.Panel5.Controls.Add(Me.btnAddToSearch)
        Me.Panel5.Controls.Add(Me.trvselecteddia)
        Me.Panel5.Controls.Add(Me.trvDiagnosis)
        Me.Panel5.Controls.Add(Me.txtSearchDiagnosis)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(639, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.Panel5.Size = New System.Drawing.Size(647, 226)
        Me.Panel5.TabIndex = 103
        '
        'rbALL
        '
        Me.rbALL.AutoSize = True
        Me.rbALL.Checked = True
        Me.rbALL.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbALL.Location = New System.Drawing.Point(24, 32)
        Me.rbALL.Name = "rbALL"
        Me.rbALL.Size = New System.Drawing.Size(40, 18)
        Me.rbALL.TabIndex = 105
        Me.rbALL.TabStop = True
        Me.rbALL.Text = "All"
        Me.rbALL.UseVisualStyleBackColor = True
        '
        'rbICD10
        '
        Me.rbICD10.AutoSize = True
        Me.rbICD10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbICD10.Location = New System.Drawing.Point(201, 32)
        Me.rbICD10.Name = "rbICD10"
        Me.rbICD10.Size = New System.Drawing.Size(58, 18)
        Me.rbICD10.TabIndex = 102
        Me.rbICD10.Text = "ICD10"
        Me.rbICD10.UseVisualStyleBackColor = True
        '
        'Panel13
        '
        Me.Panel13.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel13.Controls.Add(Me.btnClearAllSelectedDx)
        Me.Panel13.Controls.Add(Me.Label33)
        Me.Panel13.Controls.Add(Me.Label34)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel13.Location = New System.Drawing.Point(4, 1)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(639, 25)
        Me.Panel13.TabIndex = 104
        '
        'btnClearAllSelectedDx
        '
        Me.btnClearAllSelectedDx.BackColor = System.Drawing.Color.Transparent
        Me.btnClearAllSelectedDx.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearAllSelectedDx.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearAllSelectedDx.FlatAppearance.BorderSize = 0
        Me.btnClearAllSelectedDx.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearAllSelectedDx.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearAllSelectedDx.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearAllSelectedDx.Image = CType(resources.GetObject("btnClearAllSelectedDx.Image"), System.Drawing.Image)
        Me.btnClearAllSelectedDx.Location = New System.Drawing.Point(608, 0)
        Me.btnClearAllSelectedDx.Name = "btnClearAllSelectedDx"
        Me.btnClearAllSelectedDx.Size = New System.Drawing.Size(31, 24)
        Me.btnClearAllSelectedDx.TabIndex = 100
        Me.btnClearAllSelectedDx.Tag = "Select"
        Me.C1SuperTooltip1.SetToolTip(Me.btnClearAllSelectedDx, "Clear All")
        Me.btnClearAllSelectedDx.UseVisualStyleBackColor = False
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.White
        Me.Label33.Location = New System.Drawing.Point(0, 0)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(639, 24)
        Me.Label33.TabIndex = 0
        Me.Label33.Text = "    Diagnosis"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(0, 24)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(639, 1)
        Me.Label34.TabIndex = 100
        Me.Label34.Text = "label4"
        '
        'rbICD9
        '
        Me.rbICD9.AutoSize = True
        Me.rbICD9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbICD9.Location = New System.Drawing.Point(105, 32)
        Me.rbICD9.Name = "rbICD9"
        Me.rbICD9.Size = New System.Drawing.Size(51, 18)
        Me.rbICD9.TabIndex = 101
        Me.rbICD9.Text = "ICD9"
        Me.rbICD9.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(4, 225)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(639, 1)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "label4"
        '
        'lblSelectedDrugs
        '
        Me.lblSelectedDrugs.AutoSize = True
        Me.lblSelectedDrugs.BackColor = System.Drawing.Color.Transparent
        Me.lblSelectedDrugs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectedDrugs.Location = New System.Drawing.Point(338, 62)
        Me.lblSelectedDrugs.Name = "lblSelectedDrugs"
        Me.lblSelectedDrugs.Size = New System.Drawing.Size(129, 14)
        Me.lblSelectedDrugs.TabIndex = 8
        Me.lblSelectedDrugs.Text = "Selected Diagnosis :"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(639, 1)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(643, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 226)
        Me.Label13.TabIndex = 5
        Me.Label13.Text = "label4"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Location = New System.Drawing.Point(19, 63)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(118, 14)
        Me.Label30.TabIndex = 0
        Me.Label30.Text = "Search Diagnosis :"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnRemove
        '
        Me.btnRemove.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnRemove.BackgroundImage = CType(resources.GetObject("btnRemove.BackgroundImage"), System.Drawing.Image)
        Me.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRemove.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRemove.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemove.Location = New System.Drawing.Point(284, 160)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(43, 24)
        Me.btnRemove.TabIndex = 9
        Me.btnRemove.Text = "<< Remove"
        Me.btnRemove.UseVisualStyleBackColor = False
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1, 226)
        Me.Label14.TabIndex = 4
        Me.Label14.Text = "label4"
        '
        'btnAddToSearch
        '
        Me.btnAddToSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnAddToSearch.BackgroundImage = CType(resources.GetObject("btnAddToSearch.BackgroundImage"), System.Drawing.Image)
        Me.btnAddToSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddToSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(176, Byte), Integer))
        Me.btnAddToSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.btnAddToSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddToSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddToSearch.Location = New System.Drawing.Point(284, 130)
        Me.btnAddToSearch.Name = "btnAddToSearch"
        Me.btnAddToSearch.Size = New System.Drawing.Size(43, 24)
        Me.btnAddToSearch.TabIndex = 8
        Me.btnAddToSearch.Text = " >>"
        Me.btnAddToSearch.UseVisualStyleBackColor = False
        '
        'trvselecteddia
        '
        Me.trvselecteddia.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvselecteddia.ForeColor = System.Drawing.Color.Black
        Me.trvselecteddia.ImageIndex = 1
        Me.trvselecteddia.ImageList = Me.ImageList1
        Me.trvselecteddia.Indent = 20
        Me.trvselecteddia.ItemHeight = 20
        Me.trvselecteddia.Location = New System.Drawing.Point(338, 90)
        Me.trvselecteddia.Name = "trvselecteddia"
        Me.trvselecteddia.SelectedImageKey = "Bullet06.ico"
        Me.trvselecteddia.ShowLines = False
        Me.trvselecteddia.Size = New System.Drawing.Size(252, 127)
        Me.trvselecteddia.TabIndex = 7
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "DX01.ico")
        Me.ImageList1.Images.SetKeyName(1, "Bullet06.ico")
        Me.ImageList1.Images.SetKeyName(2, "Small Arrow.ico")
        '
        'trvDiagnosis
        '
        Me.trvDiagnosis.CheckBoxes = True
        Me.trvDiagnosis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvDiagnosis.ForeColor = System.Drawing.Color.Black
        Me.trvDiagnosis.ImageIndex = 2
        Me.trvDiagnosis.ImageList = Me.ImageList1
        Me.trvDiagnosis.Indent = 20
        Me.trvDiagnosis.ItemHeight = 20
        Me.trvDiagnosis.Location = New System.Drawing.Point(22, 90)
        Me.trvDiagnosis.Name = "trvDiagnosis"
        Me.trvDiagnosis.SelectedImageIndex = 2
        Me.trvDiagnosis.ShowLines = False
        Me.trvDiagnosis.Size = New System.Drawing.Size(252, 127)
        Me.trvDiagnosis.TabIndex = 7
        '
        'txtSearchDiagnosis
        '
        Me.txtSearchDiagnosis.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchDiagnosis.Location = New System.Drawing.Point(139, 59)
        Me.txtSearchDiagnosis.Name = "txtSearchDiagnosis"
        Me.txtSearchDiagnosis.Size = New System.Drawing.Size(134, 22)
        Me.txtSearchDiagnosis.TabIndex = 6
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.Panel3)
        Me.Panel9.Controls.Add(Me.Panel12)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel9.Location = New System.Drawing.Point(3, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(636, 226)
        Me.Panel9.TabIndex = 105
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Location = New System.Drawing.Point(314, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Panel3.Size = New System.Drawing.Size(322, 226)
        Me.Panel3.TabIndex = 105
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Label32)
        Me.Panel4.Controls.Add(Me.trvLocation)
        Me.Panel4.Controls.Add(Me.Label6)
        Me.Panel4.Controls.Add(Me.Panel14)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(319, 226)
        Me.Panel4.TabIndex = 103
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(1, 225)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(317, 1)
        Me.Label32.TabIndex = 107
        Me.Label32.Text = "label4"
        '
        'trvLocation
        '
        Me.trvLocation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvLocation.CheckBoxes = True
        Me.trvLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvLocation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvLocation.Indent = 20
        Me.trvLocation.ItemHeight = 20
        Me.trvLocation.Location = New System.Drawing.Point(1, 27)
        Me.trvLocation.Name = "trvLocation"
        Me.trvLocation.Size = New System.Drawing.Size(317, 199)
        Me.trvLocation.TabIndex = 94
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(1, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(317, 1)
        Me.Label6.TabIndex = 104
        Me.Label6.Text = "label4"
        '
        'Panel14
        '
        Me.Panel14.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel14.Controls.Add(Me.btnbtnDeselectLocation)
        Me.Panel14.Controls.Add(Me.btnSelectLocation)
        Me.Panel14.Controls.Add(Me.Label46)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel14.Location = New System.Drawing.Point(1, 1)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(317, 25)
        Me.Panel14.TabIndex = 103
        '
        'btnbtnDeselectLocation
        '
        Me.btnbtnDeselectLocation.BackColor = System.Drawing.Color.Transparent
        Me.btnbtnDeselectLocation.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnbtnDeselectLocation.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnbtnDeselectLocation.FlatAppearance.BorderSize = 0
        Me.btnbtnDeselectLocation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnbtnDeselectLocation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnbtnDeselectLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnbtnDeselectLocation.Image = CType(resources.GetObject("btnbtnDeselectLocation.Image"), System.Drawing.Image)
        Me.btnbtnDeselectLocation.Location = New System.Drawing.Point(255, 0)
        Me.btnbtnDeselectLocation.Name = "btnbtnDeselectLocation"
        Me.btnbtnDeselectLocation.Size = New System.Drawing.Size(31, 25)
        Me.btnbtnDeselectLocation.TabIndex = 99
        Me.btnbtnDeselectLocation.Tag = "Select"
        Me.C1SuperTooltip1.SetToolTip(Me.btnbtnDeselectLocation, "Deselect All")
        Me.btnbtnDeselectLocation.UseVisualStyleBackColor = False
        Me.btnbtnDeselectLocation.Visible = False
        '
        'btnSelectLocation
        '
        Me.btnSelectLocation.BackColor = System.Drawing.Color.Transparent
        Me.btnSelectLocation.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSelectLocation.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSelectLocation.FlatAppearance.BorderSize = 0
        Me.btnSelectLocation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSelectLocation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSelectLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectLocation.Image = CType(resources.GetObject("btnSelectLocation.Image"), System.Drawing.Image)
        Me.btnSelectLocation.Location = New System.Drawing.Point(286, 0)
        Me.btnSelectLocation.Name = "btnSelectLocation"
        Me.btnSelectLocation.Size = New System.Drawing.Size(31, 25)
        Me.btnSelectLocation.TabIndex = 98
        Me.btnSelectLocation.Tag = "Select"
        Me.C1SuperTooltip1.SetToolTip(Me.btnSelectLocation, "Select All")
        Me.btnSelectLocation.UseVisualStyleBackColor = False
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.Transparent
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.Color.White
        Me.Label46.Location = New System.Drawing.Point(0, 0)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(317, 25)
        Me.Label46.TabIndex = 0
        Me.Label46.Text = "    Location"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(1, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(317, 1)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 226)
        Me.Label9.TabIndex = 105
        Me.Label9.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(318, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 226)
        Me.Label8.TabIndex = 106
        Me.Label8.Text = "label4"
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.Panel1)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel12.Location = New System.Drawing.Point(0, 0)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(314, 226)
        Me.Panel12.TabIndex = 105
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label31)
        Me.Panel1.Controls.Add(Me.trvProvider)
        Me.Panel1.Controls.Add(Me.Panel11)
        Me.Panel1.Controls.Add(Me.Label26)
        Me.Panel1.Controls.Add(Me.Label37)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(314, 226)
        Me.Panel1.TabIndex = 103
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(1, 225)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(312, 1)
        Me.Label31.TabIndex = 106
        Me.Label31.Text = "label4"
        '
        'trvProvider
        '
        Me.trvProvider.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvProvider.CheckBoxes = True
        Me.trvProvider.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.trvProvider.Indent = 20
        Me.trvProvider.ItemHeight = 20
        Me.trvProvider.Location = New System.Drawing.Point(1, 25)
        Me.trvProvider.Name = "trvProvider"
        Me.trvProvider.Size = New System.Drawing.Size(312, 201)
        Me.trvProvider.TabIndex = 13
        '
        'Panel11
        '
        Me.Panel11.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel11.Controls.Add(Me.btnDeSelectProvider)
        Me.Panel11.Controls.Add(Me.btnSelectProvider)
        Me.Panel11.Controls.Add(Me.Label35)
        Me.Panel11.Controls.Add(Me.Label36)
        Me.Panel11.Controls.Add(Me.Label39)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel11.Location = New System.Drawing.Point(1, 0)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(312, 25)
        Me.Panel11.TabIndex = 103
        '
        'btnDeSelectProvider
        '
        Me.btnDeSelectProvider.BackColor = System.Drawing.Color.Transparent
        Me.btnDeSelectProvider.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDeSelectProvider.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnDeSelectProvider.FlatAppearance.BorderSize = 0
        Me.btnDeSelectProvider.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnDeSelectProvider.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnDeSelectProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeSelectProvider.Image = CType(resources.GetObject("btnDeSelectProvider.Image"), System.Drawing.Image)
        Me.btnDeSelectProvider.Location = New System.Drawing.Point(250, 1)
        Me.btnDeSelectProvider.Name = "btnDeSelectProvider"
        Me.btnDeSelectProvider.Size = New System.Drawing.Size(31, 23)
        Me.btnDeSelectProvider.TabIndex = 99
        Me.btnDeSelectProvider.Tag = "Select"
        Me.C1SuperTooltip1.SetToolTip(Me.btnDeSelectProvider, "Deselect All")
        Me.btnDeSelectProvider.UseVisualStyleBackColor = False
        Me.btnDeSelectProvider.Visible = False
        '
        'btnSelectProvider
        '
        Me.btnSelectProvider.BackColor = System.Drawing.Color.Transparent
        Me.btnSelectProvider.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSelectProvider.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnSelectProvider.FlatAppearance.BorderSize = 0
        Me.btnSelectProvider.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSelectProvider.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSelectProvider.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectProvider.Image = CType(resources.GetObject("btnSelectProvider.Image"), System.Drawing.Image)
        Me.btnSelectProvider.Location = New System.Drawing.Point(281, 1)
        Me.btnSelectProvider.Name = "btnSelectProvider"
        Me.btnSelectProvider.Size = New System.Drawing.Size(31, 23)
        Me.btnSelectProvider.TabIndex = 98
        Me.btnSelectProvider.Tag = "Select"
        Me.C1SuperTooltip1.SetToolTip(Me.btnSelectProvider, "Select All")
        Me.btnSelectProvider.UseVisualStyleBackColor = False
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(0, 24)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(312, 1)
        Me.Label35.TabIndex = 7
        Me.Label35.Text = "label4"
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(0, 0)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(312, 1)
        Me.Label36.TabIndex = 6
        Me.Label36.Text = "label4"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.Transparent
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.Color.White
        Me.Label39.Location = New System.Drawing.Point(0, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(312, 25)
        Me.Label39.TabIndex = 0
        Me.Label39.Text = "    Provider "
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(0, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(1, 226)
        Me.Label26.TabIndex = 104
        Me.Label26.Text = "label4"
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(313, 0)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(1, 226)
        Me.Label37.TabIndex = 105
        Me.Label37.Text = "label4"
        '
        'chkDiagnosis
        '
        Me.chkDiagnosis.CheckOnClick = True
        Me.chkDiagnosis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDiagnosis.ForeColor = System.Drawing.Color.Black
        Me.chkDiagnosis.FormattingEnabled = True
        Me.chkDiagnosis.Location = New System.Drawing.Point(853, 384)
        Me.chkDiagnosis.Name = "chkDiagnosis"
        Me.chkDiagnosis.Size = New System.Drawing.Size(231, 123)
        Me.chkDiagnosis.TabIndex = 10
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.dtpicTo)
        Me.Panel2.Controls.Add(Me.dtpicFrom)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.lblTo)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.lblFrom)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.lbl_LeftBrd)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel2.Size = New System.Drawing.Size(1286, 40)
        Me.Panel2.TabIndex = 103
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(4, 36)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1278, 1)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "label4"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1278, 1)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "label4"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(19, 12)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(44, 14)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Date :"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(1282, 3)
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
        'pnlProcedureControl
        '
        Me.pnlProcedureControl.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlProcedureControl.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlProcedureControl.Location = New System.Drawing.Point(496, 407)
        Me.pnlProcedureControl.Name = "pnlProcedureControl"
        Me.pnlProcedureControl.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlProcedureControl.Size = New System.Drawing.Size(292, 130)
        Me.pnlProcedureControl.TabIndex = 11
        Me.pnlProcedureControl.Visible = False
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmRpt_PatientICD9CPT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1286, 715)
        Me.Controls.Add(Me.pnlProcedureControl)
        Me.Controls.Add(Me.pnlICD9CPT)
        Me.Controls.Add(Me.pnl_tls_PtICD9CPTReport)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRpt_PatientICD9CPT"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Patient ICD9/10-CPT"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.pnl_tls_PtICD9CPTReport.ResumeLayout(False)
        Me.pnl_tls_PtICD9CPTReport.PerformLayout()
        Me.tls_ReportCriteria.ResumeLayout(False)
        Me.tls_ReportCriteria.PerformLayout()
        Me.pnlICD9CPT.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        CType(Me.C1grdPatients, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel10.ResumeLayout(False)
        Me.Panel17.ResumeLayout(False)
        Me.Panel17.PerformLayout()
        Me.Panel16.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel15.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel13.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dtpicTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpicFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbLocation As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtDiagnosis As System.Windows.Forms.TextBox
    Friend WithEvents txtToproc As System.Windows.Forms.TextBox
    Friend WithEvents txtFrom As System.Windows.Forms.TextBox
    Friend WithEvents txtProcedure As System.Windows.Forms.TextBox
    Friend WithEvents pnlInternalControl As System.Windows.Forms.Panel
    Private WithEvents pnl_tls_PtICD9CPTReport As System.Windows.Forms.Panel
    Private WithEvents tls_ReportCriteria As gloGlobal.gloToolStripIgnoreFocus
    Friend WithEvents btn_tls_Selectall As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_PrintReport As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_ShowReport As System.Windows.Forms.ToolStripButton
    Private WithEvents btnShowReportList As System.Windows.Forms.ToolStripButton
    Friend WithEvents btn_tls_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlICD9CPT As System.Windows.Forms.Panel
    Friend WithEvents pnlProcedureControl As System.Windows.Forms.Panel
    Friend WithEvents lblSelectedDrugs As System.Windows.Forms.Label
    Friend WithEvents chkDiagnosis As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnAddToSearch As System.Windows.Forms.Button
    Friend WithEvents trvDiagnosis As System.Windows.Forms.TreeView
    Friend WithEvents txtSearchDiagnosis As System.Windows.Forms.TextBox
    Friend WithEvents trvProvider As System.Windows.Forms.TreeView
    Private WithEvents btnDeSelectProvider As System.Windows.Forms.Button
    Private WithEvents btnSelectProvider As System.Windows.Forms.Button
    Friend WithEvents trvLocation As System.Windows.Forms.TreeView
    Private WithEvents btnbtnDeselectLocation As System.Windows.Forms.Button
    Private WithEvents btnSelectLocation As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents lbl_LeftBrd As System.Windows.Forms.Label
    Friend WithEvents C1grdPatients As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Private WithEvents Label35 As System.Windows.Forms.Label
    Private WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents trvselecteddia As System.Windows.Forms.TreeView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents RbDrDx As System.Windows.Forms.RadioButton
    Friend WithEvents RbDrDxProc As System.Windows.Forms.RadioButton
    Friend WithEvents RbDxbyProc As System.Windows.Forms.RadioButton
    Friend WithEvents RbDrDxLocation As System.Windows.Forms.RadioButton
    Private WithEvents btnClearAllSelectedDx As System.Windows.Forms.Button
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Private WithEvents Label49 As System.Windows.Forms.Label
    Private WithEvents Label50 As System.Windows.Forms.Label
    Private WithEvents Label51 As System.Windows.Forms.Label
    Private WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Private WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Private WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents tlbbtnmenuItm_HL7_MU1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlbbtnmenuItm_HL7_MU2 As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Private WithEvents tlbbtn_HL7 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents rbICD10 As System.Windows.Forms.RadioButton
    Friend WithEvents rbICD9 As System.Windows.Forms.RadioButton
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents rbALL As System.Windows.Forms.RadioButton
    Friend WithEvents tlbbtnmenuItm_HL7_MU3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlbmenuItm_HL7_MU3_A04 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlbmenuItm_HL7_MU3_A08 As System.Windows.Forms.ToolStripMenuItem
End Class
