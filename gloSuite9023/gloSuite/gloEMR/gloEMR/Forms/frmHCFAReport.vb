Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports gloEMRReports
Public Class frmHCFAReport
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal PatientId As Int64)
        MyBase.New()
        m_PatientId = PatientId
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtpfromDate, dtptoDate}
            Dim cntControls() As System.Windows.Forms.Control = {dtpfromDate, dtptoDate}
            If Not (components Is Nothing) Then
                components.Dispose()
            End If

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

            
            Try
                If (IsNothing(PrintDialog1) = False) Then
                    PrintDialog1.Dispose()
                    PrintDialog1 = Nothing
                End If
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
    Friend WithEvents cmbCondition As System.Windows.Forms.ComboBox
    Friend WithEvents dtpfromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtptoDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents flxHCFA As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents btnFax As System.Windows.Forms.Button
    Private WithEvents pnl_tls_HCFAReport As System.Windows.Forms.Panel
    Private WithEvents tls_ReportCriteria As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents btn_tls_PrintReport As System.Windows.Forms.ToolStripButton
    Private WithEvents btn_tls_ShowReport As System.Windows.Forms.ToolStripButton
    Private WithEvents pnl_Base As System.Windows.Forms.Panel
    Private WithEvents lbl_pnlBottom As System.Windows.Forms.Label
    Private WithEvents lbl_pnlLeft As System.Windows.Forms.Label
    Private WithEvents lbl_pnlRight As System.Windows.Forms.Label
    Private WithEvents lbl_pnlTop As System.Windows.Forms.Label
    Private WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtnSelectedProvider As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnselectedPatient As System.Windows.Forms.RadioButton
    Friend WithEvents cmbProviderName As System.Windows.Forms.ComboBox
    Friend WithEvents grpCriteria As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents lblTO As System.Windows.Forms.Label
    Friend WithEvents btn_tls_Selectall As System.Windows.Forms.ToolStripButton
    Private WithEvents btnShowReportList As System.Windows.Forms.ToolStripButton
    Friend WithEvents btn_tls_Close As System.Windows.Forms.ToolStripButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHCFAReport))
        Me.dtptoDate = New System.Windows.Forms.DateTimePicker
        Me.dtpfromDate = New System.Windows.Forms.DateTimePicker
        Me.cmbCondition = New System.Windows.Forms.ComboBox
        Me.btnFax = New System.Windows.Forms.Button
        Me.flxHCFA = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.pnl_tls_HCFAReport = New System.Windows.Forms.Panel
        Me.tls_ReportCriteria = New gloGlobal.gloToolStripIgnoreFocus
        Me.btn_tls_Selectall = New System.Windows.Forms.ToolStripButton
        Me.btn_tls_PrintReport = New System.Windows.Forms.ToolStripButton
        Me.btn_tls_ShowReport = New System.Windows.Forms.ToolStripButton
        Me.btnShowReportList = New System.Windows.Forms.ToolStripButton
        Me.btn_tls_Close = New System.Windows.Forms.ToolStripButton
        Me.pnl_Base = New System.Windows.Forms.Panel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rbtnSelectedProvider = New System.Windows.Forms.RadioButton
        Me.rbtnselectedPatient = New System.Windows.Forms.RadioButton
        Me.cmbProviderName = New System.Windows.Forms.ComboBox
        Me.grpCriteria = New System.Windows.Forms.GroupBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblFrom = New System.Windows.Forms.Label
        Me.lblTO = New System.Windows.Forms.Label
        Me.lbl_pnlBottom = New System.Windows.Forms.Label
        Me.lbl_pnlLeft = New System.Windows.Forms.Label
        Me.lbl_pnlRight = New System.Windows.Forms.Label
        Me.lbl_pnlTop = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        CType(Me.flxHCFA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_tls_HCFAReport.SuspendLayout()
        Me.tls_ReportCriteria.SuspendLayout()
        Me.pnl_Base.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpCriteria.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtptoDate
        '
        Me.dtptoDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtptoDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtptoDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtptoDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtptoDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtptoDate.CustomFormat = "MM/dd/yyyy"
        Me.dtptoDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtptoDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtptoDate.Location = New System.Drawing.Point(58, 79)
        Me.dtptoDate.Name = "dtptoDate"
        Me.dtptoDate.Size = New System.Drawing.Size(117, 22)
        Me.dtptoDate.TabIndex = 2
        '
        'dtpfromDate
        '
        Me.dtpfromDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpfromDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpfromDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpfromDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpfromDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpfromDate.CustomFormat = "MM/dd/yyyy"
        Me.dtpfromDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpfromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpfromDate.Location = New System.Drawing.Point(58, 48)
        Me.dtpfromDate.Name = "dtpfromDate"
        Me.dtpfromDate.Size = New System.Drawing.Size(117, 22)
        Me.dtpfromDate.TabIndex = 1
        '
        'cmbCondition
        '
        Me.cmbCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCondition.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCondition.Location = New System.Drawing.Point(58, 17)
        Me.cmbCondition.Name = "cmbCondition"
        Me.cmbCondition.Size = New System.Drawing.Size(117, 22)
        Me.cmbCondition.TabIndex = 0
        '
        'btnFax
        '
        Me.btnFax.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFax.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnFax.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFax.ForeColor = System.Drawing.Color.GhostWhite
        Me.btnFax.Location = New System.Drawing.Point(374, 14)
        Me.btnFax.Name = "btnFax"
        Me.btnFax.Size = New System.Drawing.Size(88, 24)
        Me.btnFax.TabIndex = 2
        Me.btnFax.Text = "&Fax Report"
        Me.btnFax.UseVisualStyleBackColor = False
        Me.btnFax.Visible = False
        '
        'flxHCFA
        '
        Me.flxHCFA.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.flxHCFA.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.flxHCFA.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.flxHCFA.ColumnInfo = "12,0,0,0,0,95,Columns:0{Style:""DataType:System.Boolean;ImageAlign:CenterCenter;"";" & _
            "}" & Global.Microsoft.VisualBasic.ChrW(9) & "1{StyleFixed:""TextAlign:CenterCenter;"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.flxHCFA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flxHCFA.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.flxHCFA.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.flxHCFA.Location = New System.Drawing.Point(4, 1)
        Me.flxHCFA.Name = "flxHCFA"
        Me.flxHCFA.Rows.DefaultSize = 19
        Me.flxHCFA.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.flxHCFA.Size = New System.Drawing.Size(532, 295)
        Me.flxHCFA.StyleInfo = resources.GetString("flxHCFA.StyleInfo")
        Me.flxHCFA.TabIndex = 0
        '
        'pnl_tls_HCFAReport
        '
        Me.pnl_tls_HCFAReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tls_HCFAReport.Controls.Add(Me.btnFax)
        Me.pnl_tls_HCFAReport.Controls.Add(Me.tls_ReportCriteria)
        Me.pnl_tls_HCFAReport.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tls_HCFAReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tls_HCFAReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tls_HCFAReport.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tls_HCFAReport.Name = "pnl_tls_HCFAReport"
        Me.pnl_tls_HCFAReport.Size = New System.Drawing.Size(540, 54)
        Me.pnl_tls_HCFAReport.TabIndex = 0
        '
        'tls_ReportCriteria
        '
        Me.tls_ReportCriteria.BackColor = System.Drawing.Color.Transparent
        Me.tls_ReportCriteria.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_ReportCriteria.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_ReportCriteria.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_ReportCriteria.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_ReportCriteria.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btn_tls_Selectall, Me.btn_tls_PrintReport, Me.btn_tls_ShowReport, Me.btnShowReportList, Me.btn_tls_Close})
        Me.tls_ReportCriteria.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_ReportCriteria.Location = New System.Drawing.Point(0, 0)
        Me.tls_ReportCriteria.Name = "tls_ReportCriteria"
        Me.tls_ReportCriteria.Size = New System.Drawing.Size(540, 53)
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
        Me.btnShowReportList.Tag = "ShowReport"
        Me.btnShowReportList.Text = "&Show Report"
        Me.btnShowReportList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        'pnl_Base
        '
        Me.pnl_Base.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_Base.Controls.Add(Me.GroupBox1)
        Me.pnl_Base.Controls.Add(Me.grpCriteria)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlBottom)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlLeft)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlRight)
        Me.pnl_Base.Controls.Add(Me.lbl_pnlTop)
        Me.pnl_Base.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_Base.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_Base.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_Base.Location = New System.Drawing.Point(0, 54)
        Me.pnl_Base.Name = "pnl_Base"
        Me.pnl_Base.Padding = New System.Windows.Forms.Padding(3)
        Me.pnl_Base.Size = New System.Drawing.Size(540, 136)
        Me.pnl_Base.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.GroupBox1.Controls.Add(Me.rbtnSelectedProvider)
        Me.GroupBox1.Controls.Add(Me.rbtnselectedPatient)
        Me.GroupBox1.Controls.Add(Me.cmbProviderName)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(225, 11)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(187, 113)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Other Criteria"
        '
        'rbtnSelectedProvider
        '
        Me.rbtnSelectedProvider.AutoSize = True
        Me.rbtnSelectedProvider.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtnSelectedProvider.Location = New System.Drawing.Point(21, 51)
        Me.rbtnSelectedProvider.Name = "rbtnSelectedProvider"
        Me.rbtnSelectedProvider.Size = New System.Drawing.Size(121, 18)
        Me.rbtnSelectedProvider.TabIndex = 1
        Me.rbtnSelectedProvider.Text = "Selected Provider"
        Me.rbtnSelectedProvider.UseVisualStyleBackColor = True
        '
        'rbtnselectedPatient
        '
        Me.rbtnselectedPatient.AutoSize = True
        Me.rbtnselectedPatient.Checked = True
        Me.rbtnselectedPatient.Location = New System.Drawing.Point(21, 24)
        Me.rbtnselectedPatient.Name = "rbtnselectedPatient"
        Me.rbtnselectedPatient.Size = New System.Drawing.Size(126, 18)
        Me.rbtnselectedPatient.TabIndex = 0
        Me.rbtnselectedPatient.TabStop = True
        Me.rbtnselectedPatient.Text = "Selected Patient"
        Me.rbtnselectedPatient.UseVisualStyleBackColor = True
        '
        'cmbProviderName
        '
        Me.cmbProviderName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProviderName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProviderName.ForeColor = System.Drawing.Color.Black
        Me.cmbProviderName.Location = New System.Drawing.Point(21, 78)
        Me.cmbProviderName.Name = "cmbProviderName"
        Me.cmbProviderName.Size = New System.Drawing.Size(143, 22)
        Me.cmbProviderName.TabIndex = 2
        Me.cmbProviderName.Visible = False
        '
        'grpCriteria
        '
        Me.grpCriteria.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.grpCriteria.Controls.Add(Me.Label6)
        Me.grpCriteria.Controls.Add(Me.lblFrom)
        Me.grpCriteria.Controls.Add(Me.lblTO)
        Me.grpCriteria.Controls.Add(Me.cmbCondition)
        Me.grpCriteria.Controls.Add(Me.dtpfromDate)
        Me.grpCriteria.Controls.Add(Me.dtptoDate)
        Me.grpCriteria.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpCriteria.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grpCriteria.Location = New System.Drawing.Point(18, 11)
        Me.grpCriteria.Name = "grpCriteria"
        Me.grpCriteria.Size = New System.Drawing.Size(193, 113)
        Me.grpCriteria.TabIndex = 10
        Me.grpCriteria.TabStop = False
        Me.grpCriteria.Text = "Date Criteria"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(14, 21)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 14)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Date :"
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblFrom.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrom.Location = New System.Drawing.Point(13, 52)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(42, 14)
        Me.lblFrom.TabIndex = 10
        Me.lblFrom.Text = "From :"
        Me.lblFrom.Visible = False
        '
        'lblTO
        '
        Me.lblTO.AutoSize = True
        Me.lblTO.BackColor = System.Drawing.Color.Transparent
        Me.lblTO.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTO.Location = New System.Drawing.Point(25, 83)
        Me.lblTO.Name = "lblTO"
        Me.lblTO.Size = New System.Drawing.Size(30, 14)
        Me.lblTO.TabIndex = 9
        Me.lblTO.Text = "To :"
        Me.lblTO.Visible = False
        '
        'lbl_pnlBottom
        '
        Me.lbl_pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lbl_pnlBottom.Location = New System.Drawing.Point(4, 132)
        Me.lbl_pnlBottom.Name = "lbl_pnlBottom"
        Me.lbl_pnlBottom.Size = New System.Drawing.Size(532, 1)
        Me.lbl_pnlBottom.TabIndex = 4
        Me.lbl_pnlBottom.Text = "label2"
        '
        'lbl_pnlLeft
        '
        Me.lbl_pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlLeft.Location = New System.Drawing.Point(3, 4)
        Me.lbl_pnlLeft.Name = "lbl_pnlLeft"
        Me.lbl_pnlLeft.Size = New System.Drawing.Size(1, 129)
        Me.lbl_pnlLeft.TabIndex = 3
        Me.lbl_pnlLeft.Text = "label4"
        '
        'lbl_pnlRight
        '
        Me.lbl_pnlRight.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlRight.Location = New System.Drawing.Point(536, 4)
        Me.lbl_pnlRight.Name = "lbl_pnlRight"
        Me.lbl_pnlRight.Size = New System.Drawing.Size(1, 129)
        Me.lbl_pnlRight.TabIndex = 2
        Me.lbl_pnlRight.Text = "label3"
        '
        'lbl_pnlTop
        '
        Me.lbl_pnlTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_pnlTop.Location = New System.Drawing.Point(3, 3)
        Me.lbl_pnlTop.Name = "lbl_pnlTop"
        Me.lbl_pnlTop.Size = New System.Drawing.Size(534, 1)
        Me.lbl_pnlTop.TabIndex = 0
        Me.lbl_pnlTop.Text = "label1"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel1.Controls.Add(Me.flxHCFA)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(0, 190)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel1.Size = New System.Drawing.Size(540, 300)
        Me.Panel1.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label1.Location = New System.Drawing.Point(4, 296)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(532, 1)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "label2"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1, 296)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label3.Location = New System.Drawing.Point(536, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 296)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "label3"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Location = New System.Drawing.Point(3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(534, 1)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "label1"
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmHCFAReport
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 15)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(540, 490)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnl_Base)
        Me.Controls.Add(Me.pnl_tls_HCFAReport)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHCFAReport"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HCFA Report "
        CType(Me.flxHCFA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_tls_HCFAReport.ResumeLayout(False)
        Me.pnl_tls_HCFAReport.PerformLayout()
        Me.tls_ReportCriteria.ResumeLayout(False)
        Me.tls_ReportCriteria.PerformLayout()
        Me.pnl_Base.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpCriteria.ResumeLayout(False)
        Me.grpCriteria.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private m_PatientId As Int64
    Private objHCFAReport As ClsHCFAReport
    Dim oRpt As ReportDocument
    Public m_Examid As Long
    'Added By Shweta 20091128  against case no :GLO2009 0003381
    Public m_ProviderName As String = ""
    Public m_ProviderID As Long = 0
    Dim arrlist As New ArrayList
    Dim IsselectAll As Boolean = False

    Private Sub frmHCFAReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            gloC1FlexStyle.Style(flxHCFA)

            cmbCondition.Items.Add("is equal to")
            cmbCondition.Items.Add("is between")
            'Changed By Shweta 20091128  against case no :GLO2009 0003381
            'lblRange.Visible = False
            lblFrom.Visible = True
            lblTO.Visible = False
            rbtnselectedPatient.Checked = True
            'End 20091128
            dtptoDate.Visible = False
            objHCFAReport = New ClsHCFAReport
            'Changed By Shweta 20091128 against case no :GLO2009 0003381
            'If objHCFAReport.GetExams(dtpfromDate.Value.Date, dtptoDate.Value.Date) Then
            If objHCFAReport.GetExams(dtpfromDate.Value.Date, dtptoDate.Value.Date, m_PatientId, 0) Then
                'End 20091128
                Fill_Data()
            End If
            cmbCondition.Text = "is equal to"
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, m_PatientId, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub cmbCondition_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCondition.SelectedValueChanged
        Try


            If cmbCondition.Text = "is between" Then
                'Changed By Shweta 20091128  against case no :GLO2009 0003381
                'lblRange.Visible = True
                lblTO.Visible = True
                'End 
                dtptoDate.Visible = True
                dtptoDate.Value = dtpfromDate.Value
            Else
                'Changed By Shweta 20091128  against case no :GLO2009 0003381
                'lblRange.Visible = False
                lblTO.Visible = False
                'End
                dtptoDate.Visible = False
            End If
            ''Commented By Shweta 20091128  against case no :GLO2009 0003381
            'If objHCFAReport.GetExams(dtpfromDate.Value.Date, dtptoDate.Value.Date) Then
            '    Fill_Data()
            'End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        End Try

    End Sub

    Private Sub ShowHCFAReport()
        Dim objClsHCFAReport As ClsHCFAReport = Nothing
        Dim oCPT As rpt_CptDriven = Nothing
        Dim frm As frmHCFAViewer = Nothing
        Dim oICD9 As Rpt_HCFA_ICD9Driven = Nothing
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim nCount As Int16
            '            Dim arrlist As New ArrayList
            arrlist.Clear()
            For nCount = 1 To flxHCFA.Rows.Count - 1
                If CBool(flxHCFA.GetData(nCount, 0)) = True Then
                    'MsgBox(flxHCFA.GetData(nCount, 1))
                    arrlist.Add(flxHCFA.GetData(nCount, 1))
                End If
            Next
            'Added by Shweta 20100106
            'Against the bug id:5658 
            'if the exam doesnt have diagnosis then dont load report viewer form
            Dim _ExamID As String = ""
            objClsHCFAReport = New ClsHCFAReport
            If arrlist.Count > 0 Then
                For i As Integer = 0 To arrlist.Count - 1
                    _ExamID = _ExamID & "'" & arrlist(i) & "',"
                Next

                If gblnICD9Driven Then
                    'Create report object to retrive the report 
                    oICD9 = New Rpt_HCFA_ICD9Driven()
                    _ExamID = _ExamID.Substring(0, _ExamID.Length - 1)
                    If _ExamID <> "" Then
                        oICD9 = objClsHCFAReport.CreateICD9Report(_ExamID)
                        If oICD9 IsNot Nothing Then
                            frm = New frmHCFAViewer(arrlist, oICD9)
                            frm.PatientID = m_PatientId
                            frm.FromDate = dtpfromDate.Value.Date
                            frm.ToDate = dtptoDate.Value.Date
                            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                        End If
                    End If
                Else
                    oCPT = New rpt_CptDriven()
                    _ExamID = _ExamID.Substring(0, _ExamID.Length - 1)
                    If _ExamID <> "" Then
                        oCPT = objClsHCFAReport.CreateReport(_ExamID)
                        If oCPT IsNot Nothing Then
                            frm = New frmHCFAViewer(arrlist, oCPT)
                            frm.PatientID = m_PatientId
                            frm.FromDate = dtpfromDate.Value.Date
                            frm.ToDate = dtptoDate.Value.Date
                            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                        End If
                    End If
                End If
            End If


            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to load Report", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oICD9) Then
                oICD9.Dispose()
                oICD9 = Nothing
            End If
            If Not IsNothing(frm) Then
                frm.Dispose()
                frm = Nothing
            End If
            If Not IsNothing(oCPT) Then
                oCPT.Dispose()
                oCPT = Nothing
            End If
            If Not IsNothing(objClsHCFAReport) Then
                objClsHCFAReport = Nothing
            End If
        End Try

    End Sub
    'Private Sub CustomGridStyle()
    '    Dim dv As DataView
    '    dv = objHCFAReport.DsDataview
    '    Dim ts As New clsDataGridTableStyle(dv.Table.TableName, False)

    '    'objHCFAReport.SortDataview(dv.Table.Columns(1).ColumnName)
    '    Dim IDCol As New DataGridTextBoxColumn
    '    IDCol.Width = 0
    '    IDCol.MappingName = dv.Table.Columns(0).ColumnName
    '    IDCol.HeaderText = "ID"

    '    Dim VisitCol As New DataGridTextBoxColumn
    '    With VisitCol
    '        .Width = 0
    '        .MappingName = dv.Table.Columns(1).ColumnName
    '        .HeaderText = "Visit ID"
    '        .NullText = ""
    '        .ReadOnly = True
    '    End With

    '    Dim DOSCol As New DataGridTextBoxColumn
    '    With DOSCol
    '        .Width = 150
    '        .MappingName = dv.Table.Columns(2).ColumnName
    '        .HeaderText = "Date of Service"
    '        .NullText = ""
    '        .ReadOnly = True
    '    End With

    '    Dim ExamCol As New DataGridTextBoxColumn
    '    With ExamCol
    '        .Width = 200
    '        .MappingName = dv.Table.Columns(3).ColumnName
    '        .HeaderText = "Exam Name"
    '        .NullText = ""
    '        .ReadOnly = True
    '    End With

    '    Dim SelectedCol As New DataGridBoolColumn
    '    With SelectedCol
    '        .Width = 100
    '        .MappingName = dv.Table.Columns(4).ColumnName
    '        .HeaderText = "Is Selected"
    '        .NullText = ""
    '        .ReadOnly = False
    '        .TrueValue = True
    '    End With

    '    ts.GridColumnStyles.AddRange(New DataGridColumnStyle() {IDCol, VisitCol, DOSCol, ExamCol, SelectedCol})
    '    grdHCFAReport.TableStyles.Clear()
    '    grdHCFAReport.TableStyles.Add(ts)

    'End Sub


    'Commented By Shweta 20091128  against case no :GLO2009 0003381
    'Private Sub dtpfromDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpfromDate.TextChanged
    '    If cmbCondition.Text = "is equal to" Then
    '        dtptoDate.Value = dtpfromDate.Value
    '    End If
    '    If objHCFAReport.GetExams(dtpfromDate.Value.Date, dtptoDate.Value.Date) Then
    '        Fill_Data()
    '    End If
    'End Sub
    'Private Sub dtptoDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtptoDate.TextChanged
    '    Try
    '        If objHCFAReport.GetExams(dtpfromDate.Value.Date, dtptoDate.Value.Date) Then
    '            Fill_Data()
    '        End If
    '    Catch ex As Exception

    '    End Try

    'End Sub
    'End Commenting By Shweta 20091128


    Private Sub Fill_Data()
        Try


            'Retrieve all Pending FAXes and store it in Table
            Dim dtData As New DataTable
            dtData = objHCFAReport.DsDataview.Table

            'Check TIFF File is generated or not
            'If the TIFF File is already generated then delete the pending fax from table.....as we required only those pending faxes whose TIFF File has not been generated
            Dim nCount As Int16

            'flxPendingFAXes.DataSource = dtFiles

            With flxHCFA
                .Cols.Count = 5
                .Rows.Count = 1
                .Rows.Fixed = 1
                'Changed By Shweta 20091128 against case no :GLO2009 0003381
                '.SetData(0, 0, "Is Selected")
                .SetData(0, 0, "Select")
                'End
                .SetData(0, 1, "Exam ID")
                .SetData(0, 2, "Visit ID")
                .SetData(0, 3, "Exam Name")
                .SetData(0, 4, "Date Of Service")

                .Cols(0).Width = 100 '70
                .Cols(1).Width = 0
                .Cols(2).Width = 0
                .Cols(3).Width = 400 '370
                .Cols(4).Width = 150 '100

                .Cols(0).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(1).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(2).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(3).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(4).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter

                .Cols(0).AllowEditing = True
                .Cols(1).AllowEditing = False
                .Cols(2).AllowEditing = False
                .Cols(3).AllowEditing = False
                .Cols(4).AllowEditing = False



                For nCount = 0 To dtData.Rows.Count - 1
                    .Rows.Add()
                    'Changed By Shweta 20091128 against case no :GLO2009 0003381
                    '.SetData(.Rows.Count - 1, 0, True)
                    .SetData(.Rows.Count - 1, 0, False)
                    'End
                    .SetData(.Rows.Count - 1, 1, dtData.Rows(nCount).Item(0))
                    .SetData(.Rows.Count - 1, 2, dtData.Rows(nCount).Item(1))
                    .SetData(.Rows.Count - 1, 3, dtData.Rows(nCount).Item(3))
                    .SetData(.Rows.Count - 1, 4, dtData.Rows(nCount).Item(2))

                Next
            End With
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub PrintHCFAReport()

        Try
            Me.Cursor = Cursors.WaitCursor

            Me.Cursor = Cursors.WaitCursor
            Dim nCount As Int16
            '            Dim arrlist As New ArrayList
            arrlist.Clear()
            For nCount = 1 To flxHCFA.Rows.Count - 1
                If CBool(flxHCFA.GetData(nCount, 0)) = True Then
                    'MsgBox(flxHCFA.GetData(nCount, 1))
                    arrlist.Add(flxHCFA.GetData(nCount, 1))
                End If
            Next
            If arrlist.Count > 0 Then
                FillReportdetails()
            End If

            'oRpt.PrintToPrinter(1, False, 0, 0)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Reports, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Unable to load Report", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FillReportdetails()


        Dim _ExamID As String = ""

        If arrlist.Count > 0 Then
            For i As Integer = 0 To arrlist.Count - 1
                _ExamID = _ExamID & "'" & arrlist(i) & "',"
            Next
        End If

        'Added By Shweta 20091205 
        'To print the report as per the setting in admin 
        If gblnICD9Driven = True Then
            '  Dim nCount As Int16
            Dim arrlist As New ArrayList

            'Commented By Shweta 20091231
            'As new report added in the gloEMRReport project for ICD9 Driven exam
            'Try

            '    arrlist.Clear()
            '    For nCount = 1 To flxHCFA.Rows.Count - 1
            '        If CBool(flxHCFA.GetData(nCount, 0)) = True Then
            '            arrlist.Add(flxHCFA.GetData(nCount, 1))
            '        End If
            '    Next

            '    Dim oRpt As ReportDocument
            '    Dim _strPath As String = gstrgloEMRStartupPath & "\Reports\rptHCFA.rpt"

            '    Try
            '        oRpt = New ReportDocument
            '        oRpt.Load(_strPath)
            '        Dim crtableLogoninfos As New TableLogOnInfos
            '        Dim crtableLogoninfo As New TableLogOnInfo
            '        Dim crConnectionInfo As New ConnectionInfo
            '        Dim CrTables As Tables
            '        Dim CrTable As Table
            '        Dim TableCounter

            '        With crConnectionInfo
            '            .AllowCustomConnection = True
            '            .ServerName = gstrSQLServerName
            '            'If you are connecting to Oracle there is no 
            '            'DatabaseName. Use an empty string. 
            '            'For example, .DatabaseName = "" 
            '            .DatabaseName = gstrDatabaseName
            '            '.UserID = "Your User ID"
            '            '.Password = "Your Password"
            '            .IntegratedSecurity = True
            '        End With

            '        'This code works for both user tables and stored 
            '        'procedures. Set the CrTables to the Tables collection 
            '        'of the report 

            '        CrTables = oRpt.Database.Tables

            '        'Loop through each table in the report and apply the 
            '        'LogonInfo information 

            '        For Each CrTable In CrTables
            '            crtableLogoninfo = CrTable.LogOnInfo
            '            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            '            CrTable.ApplyLogOnInfo(crtableLogoninfo)

            '            'If your DatabaseName is changing at runtime, specify 
            '            'the table location. 
            '            'For example, when you are reporting off of a 
            '            'Northwind database on SQL server you 
            '            'should have the following line of code: 

            '            CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name

            '        Next

            '        'oRpt.Load(_strPath)
            '        ''m_PatientId, m_VisitId, m_ExamId
            '        'oRpt.SetParameterValue("PatientID", m_PatientId.ToString)
            '        'oRpt.SetParameterValue("FromDate", CType(dtpfromDate.Value.Date, String))
            '        'oRpt.SetParameterValue("ToDate", CType(dtptoDate.Value.Date, String))
            '        ' ''
            '        'If blnPrint = True Then
            '        '    oRpt.PrintToPrinter(1, False, 0, 0)
            '        'Else
            '        '    oRpt.PrintToPrinter(1, False, 0, 0)
            '        'End If

            '        'If arrlist.Count > 0 Then
            '        ' Dim selectFormula As String = ""
            '        '    Dim i As Int16
            '        '    For i = 0 To arrlist.Count - 1

            '        '        If i = arrlist.Count - 1 Then
            '        '            'oRpt.DataDefinition.GroupSelectionFormula = Mid(selectFormula, 1, Len(selectFormula) - 4)

            '        '        End If
            '        '    Next
            '        'End If{HIPPAExamReport.PatientID}={?@PatientID}and
            '        '{HIPPAExamReport.DateofService}>={?@fromdate}and
            '        '{HIPPAExamReport.DateofService}<={?@todate}
            '        'oRpt.RecordSelectionFormula = Mid(selectFormula, 1, Len(selectFormula) - 4)
            '        _ExamID = _ExamID.Substring(0, _ExamID.Length - 1)
            '        ''Selection formula at runtime to select records for a report
            '        Dim selectFormula As String = ""
            '        'selectFormula = selectFormula & "{HIPPAExamReport.PatientID} = '" & m_PatientId.ToString & "' and " & "{HIPPAExamReport.DateofService} >= CDate ('" & dtpfromDate.Value.Date & "') and " & "{HIPPAExamReport.DateofService}<= CDate ('" & dtptoDate.Value.Date & "')"
            '        selectFormula = selectFormula & "{HIPPAExamReport.ExamID} in (" & _ExamID & "))" 'and " & "{HIPPAExamReport.DateofService} >= CDate ('" & dtpfromDate.Value.Date & "') and " & "{HIPPAExamReport.DateofService}<= CDate ('" & dtptoDate.Value.Date & "')"
            '        oRpt.RecordSelectionFormula = selectFormula
            '        ''Print report
            '        'oRpt.PrintToPrinter(1, False, 0, 0)

            '        'sarika Show Print Dialog 20090226
            '        '     oRpt.PrintToPrinter(1, False, 0, 0)


            '        If gblnUseDefaultPrinter = False Then
            '            'sarika Show Print Dialog 20080923
            '            'oRpt.PrintToPrinter(1, False, 0, 0)
            '            '   PrintDialog1.UseEXDialog = True
            '            PrintDialog1 = New PrintDialog()
            '            'PrintDialog1.ShowDialog()
            '            'If PrintDialog1.Then Then



            '            If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            '                oRpt.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName
            '                oRpt.Load(Application.StartupPath & "\Reports\rptHCFA.rpt")
            '                oRpt.PrintToPrinter(1, False, 0, 0)
            '            End If

            '            If Not IsNothing(oRpt) Then
            '                oRpt.Close()
            '            End If

            '            '----------------------
            '        Else
            '            oRpt.PrintToPrinter(1, False, 0, 0)
            '        End If


            '        ''
            '    Catch ex As Exception
            '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    End Try

            'Catch ex As Exception
            '    Throw ex
            'End Try
            'End Commenting By Shweta 20091231

            'Added By Shweta 20091231
            Try

                'Create report object to retrive the report 
                Dim oICD9 As Rpt_HCFA_ICD9Driven = New Rpt_HCFA_ICD9Driven()
                Dim objClsHCFAReport As ClsHCFAReport = New ClsHCFAReport
                If _ExamID <> "" Then
                    _ExamID = _ExamID.Substring(0, _ExamID.Length - 1)
                    'retrive related information to represent in report
                    oICD9 = objClsHCFAReport.CreateICD9Report(_ExamID)
                    If oICD9 IsNot Nothing Then
                        'To print the report 
                        If gblnUseDefaultPrinter = False Then
                            '   PrintDialog1 = New PrintDialog()
                            If PrintDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                                oICD9.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName
                                oICD9.PrintToPrinter(1, False, 0, 0)
                            End If
                            '  PrintDialog1.Dispose()
                        Else
                            oICD9.PrintToPrinter(1, False, 0, 0)
                        End If
                        ''
                    End If
                End If
                If Not IsNothing(oICD9) Then
                    oICD9.Dispose()
                    oICD9 = Nothing
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            'End 20091231
        Else
            Try
                'Added By Shweta 20091205
                'Create report object to retrive the report 
                Dim oCpt As rpt_CptDriven = New rpt_CptDriven()
                Dim objClsHCFAReport As ClsHCFAReport = New ClsHCFAReport
                If _ExamID <> "" Then
                    _ExamID = _ExamID.Substring(0, _ExamID.Length - 1)
                    'retrive related information to represent in report
                    oCpt = objClsHCFAReport.CreateReport(_ExamID)
                    If oCpt IsNot Nothing Then
                        'To print the report 
                        If gblnUseDefaultPrinter = False Then
                            ' PrintDialog1 = New PrintDialog()
                            If PrintDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm) = Windows.Forms.DialogResult.OK Then
                                oCpt.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName
                                oCpt.PrintToPrinter(1, False, 0, 0)
                            End If
                            'PrintDialog1.Dispose()
                        Else
                            oCpt.PrintToPrinter(1, False, 0, 0)
                        End If
                        ''
                    End If
                End If
                If Not IsNothing(oCpt) Then
                    oCpt.Dispose()
                    oCpt = Nothing
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            'End shweta
        End If
    End Sub

    'Dim prm As ParameterValues
    'Dim discreteval As ParameterDiscreteValue

    'prm = oRpt.DataDefinition.ParameterFields.Item(0).CurrentValues()
    'prm.Clear()
    'discreteval = New ParameterDiscreteValue
    'discreteval.Value = m_PatientId
    'prm.Add(discreteval)
    'oRpt.DataDefinition.ParameterFields.Item(0).ApplyCurrentValues(prm)

    'prm = oRpt.DataDefinition.ParameterFields.Item(1).CurrentValues()
    'prm.Clear()
    'discreteval = New ParameterDiscreteValue
    'discreteval.Value = CType(dtpfromDate.Value.Date, String)
    'prm.Add(discreteval)
    'oRpt.DataDefinition.ParameterFields.Item(1).ApplyCurrentValues(prm)

    'prm = oRpt.DataDefinition.ParameterFields.Item(2).CurrentValues()
    'prm.Clear()
    'discreteval = New ParameterDiscreteValue
    'discreteval.Value = CType(dtptoDate.Value.Date, String)
    'prm.Add(discreteval)
    'oRpt.DataDefinition.ParameterFields.Item(2).ApplyCurrentValues(prm)

    ''{sp_HCFAReport;1.ExamID}
    'If arrlist.Count > 0 Then
    '    Dim selectFormula As String = ""
    '    Dim i As Int16
    '    For i = 0 To arrlist.Count - 1
    '        selectFormula = selectFormula & "{sp_RptHIPAA;1.ExamID} = " & CType(arrlist.Item(i), Int64) & " or "
    '        If i = arrlist.Count - 1 Then
    '            oRpt.DataDefinition.GroupSelectionFormula = Mid(selectFormula, 1, Len(selectFormula) - 4)
    '        End If
    '    Next
    'End If
    'MapDatabaseInfo(oRpt)


    'Private Sub MapDatabaseInfo(ByVal rpt As ReportDocument)

    '    'Dim crConnectionInfo As New ConnectionInfo

    '    'With crConnectionInfo
    '    '    .ServerName = gstrSQLServerName

    '    '    'If you are connecting to Oracle there is no 
    '    '    'DatabaseName. Use an empty string. 
    '    '    'For example, .DatabaseName = "" 

    '    '    .DatabaseName = gstrDatabaseName

    '    '    .IntegratedSecurity = True

    '    '    '.UserID = "Your User ID"
    '    '    '.Password = "Your Password"
    '    'End With
    '    'MapTableInfo(crConnectionInfo, rpt)
    '    'Dim objsubrpt As SubreportObject
    '    'Dim objrpt As ReportDocument

    '    'objsubrpt = rpt.ReportDefinition.Sections.Item(3).ReportObjects(0)
    '    'objrpt = New ReportDocument
    '    'objrpt = rpt.OpenSubreport(objsubrpt.SubreportName)
    '    'MapTableInfo(crConnectionInfo, objrpt)

    'End Sub

    Private Sub MapTableInfo(ByVal crConnectionInfo As ConnectionInfo, ByVal rpt As ReportDocument)
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo

        Dim CrTables As Tables
        Dim CrTable As Table
        'Dim TableCounter
        'This code works for both user tables and stored 
        'procedures. Set the CrTables to the Tables collection 
        'of the report 
        Try


            CrTables = rpt.Database.Tables

            'Loop through each table in the report and apply the 
            'LogonInfo information 

            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)

                'If your DatabaseName is changing at runtime, specify 
                'the table location. 
                'For example, when you are reporting off of a 
                'Northwind database on SQL server you 
                'should have the following line of code: 

                CrTable.Location = gstrDatabaseName & ".dbo." & CrTable.Name
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnFax_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFax.Click
        'Try
        '    FillReportdetails()
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub tls_ReportCriteria_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_ReportCriteria.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "PrintReport"
                PrintHCFAReport()
                'Changed By Shweta 20091128 against case no :GLO2009 0003381
                'Case "ShowReport"
                '    ShowHCFAReport()
            Case "ViewReport"
                ShowHCFAReport()
                'End 20091128
            Case "ShowReport"
                ShowReport()
            Case "Close"
                Me.Close()

            Case "Selectall"                    ''Dhruv 20100201----------------------
                SelectAll()                     ''To Select all the data
            Case "Clearall"
                ClearAll()                      ''Dhruv---To Clear all the data----End

        End Select
    End Sub
    ''Dhruv 201000201
#Region "toolstripbutton Select all and Clear all "
    ''' <summary>
    ''' To Select all the Unselected Data
    ''' Note over the single Physical button we are showing 2 button 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SelectAll()
        btn_tls_Selectall.Text = "Clear &All"                                   ''Givivng the name to the button
        btn_tls_Selectall.Tag = "Clearall"                                      ''Providing the aliase name to the button as an tag
        btn_tls_Selectall.Image = Global.gloEMR.My.Resources.Clear_All          ''taking the imgage from the resources location
        btn_tls_Selectall.ToolTipText = "Clear All"                             ''Providing the tooltip to that button
        IsselectAll = True
        For i As Integer = 1 To flxHCFA.Rows.Count - 1                          ''Checking into the C1 flex grid  to checked or unchecked.
            flxHCFA.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Checked)
        Next
    End Sub
    ''' <summary>
    ''' It is used for clearing all the Select Data 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearAll()
        btn_tls_Selectall.Text = "Select &All"                                  ''Givivng the name to the button
        btn_tls_Selectall.Tag = "Selectall"                                      ''Providing the aliase name to the button as an tag
        btn_tls_Selectall.Image = Global.gloEMR.My.Resources.Select_All1         ''taking the imgage from the resources location
        btn_tls_Selectall.ToolTipText = "Select All"                            ''Providing the tooltip to that button
        For i As Integer = 1 To flxHCFA.Rows.Count - 1                          ''Checking into the C1 flex grid  to checked or unchecked.
            flxHCFA.SetCellCheck(i, 0, C1.Win.C1FlexGrid.CheckEnum.Unchecked)
        Next
    End Sub
    ''-------------------------------------
#End Region

    Private Sub flxHCFA_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles flxHCFA.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
    'Added By Shweta 20091128 against case no :GLO2009 0003381
    Private Sub rbtnselectedPatient_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnselectedPatient.CheckedChanged
        If rbtnselectedPatient.Checked = True Then
            rbtnselectedPatient.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            cmbProviderName.Visible = False

            cmbProviderName.DataSource = Nothing
            cmbProviderName.Items.Clear()
        Else
            rbtnselectedPatient.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub

    Private Sub rbtnSelectedProvider_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSelectedProvider.CheckedChanged
        If rbtnSelectedProvider.Checked = True Then
            rbtnSelectedProvider.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
            cmbProviderName.Visible = True
            'Fill_Provider()
            Fill_Providers()
        Else
            rbtnSelectedProvider.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)
        End If
    End Sub
    'Fill Provider Combobox
    Private Sub Fill_Providers()
        With cmbProviderName
            .Items.Clear()
            .Items.Add("All")
            Dim clProviders As New Collection
            Dim objProvider As New ClsHCFAReport
            clProviders = objProvider.Fill_Providers
            objProvider = Nothing
            Dim nCount As Int16
            For nCount = 1 To clProviders.Count
                .Items.Add(clProviders.Item(nCount).ToString.Trim)
            Next
            'If Trim(gstrPatientProviderName) <> "" Then
            '    .Text = gstrPatientProviderName.Trim
            'Else
            '    gstrPatientProviderName = "All"
            .SelectedIndex = 0
            'End If
        End With
    End Sub

    Private Sub cmbProviderName_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProviderName.SelectionChangeCommitted
        Try
            'Retrive providerID for selected provider in Combobox
            m_ProviderName = cmbProviderName.Text
            Dim objProvider As New ClsHCFAReport
            m_ProviderID = objProvider.RetrieveProviderID(m_ProviderName)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ShowReport()
        Try
            ClearAll()                                              ''When clicked on these button "Report List" it loads the reports so before loading set the toolstripbuttton as select all
            'Get list of report for Selected patient on Dashboard
            If rbtnselectedPatient.Checked = True Then
                'Get list of report for Selected date
                If cmbCondition.Text = "is equal to" Then
                    dtptoDate.Value = dtpfromDate.Value
                    If objHCFAReport.GetExams(dtpfromDate.Value.Date, dtptoDate.Value.Date, m_PatientId, 0) Then
                        Fill_Data()
                    End If
                Else

                    If dtpfromDate.Value.Date > dtptoDate.Value.Date Then
                        MessageBox.Show("From date is greater than To date", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        dtpfromDate.Focus()
                        Return
                    Else
                        'Get list of report form Selected date to Selected date in Combobox
                        If objHCFAReport.GetExams(dtpfromDate.Value.Date, dtptoDate.Value.Date, m_PatientId, 0) Then
                            Fill_Data()
                        End If
                    End If
                End If
            Else
                'Get list of report for selected provider
                If cmbCondition.Text = "is equal to" Then
                    'Get list of report for Selected date
                    dtptoDate.Value = dtpfromDate.Value
                    If objHCFAReport.GetExams(dtpfromDate.Value.Date, dtptoDate.Value.Date, 0, m_ProviderID) Then
                        Fill_Data()
                    End If
                Else
                    If dtpfromDate.Value.Date > dtptoDate.Value.Date Then
                        MessageBox.Show("From date is greater than To date", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        dtpfromDate.Focus()
                        Return
                    Else
                        'Get list of report form Selected date to Selected date in Combobox
                        If objHCFAReport.GetExams(dtpfromDate.Value.Date, dtptoDate.Value.Date, 0, m_ProviderID) Then
                            Fill_Data()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub



    'Private Sub btnShowReportList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowReportList.Click
    '    'Try
    '    '    ClearAll()                                              ''When clicked on these button "Report List" it loads the reports so before loading set the toolstripbuttton as select all
    '    '    'Get list of report for Selected patient on Dashboard
    '    '    If rbtnselectedPatient.Checked = True Then
    '    '        'Get list of report for Selected date
    '    '        If cmbCondition.Text = "is equal to" Then
    '    '            dtptoDate.Value = dtpfromDate.Value
    '    '            If objHCFAReport.GetExams(dtpfromDate.Value.Date, dtptoDate.Value.Date, m_PatientId, 0) Then
    '    '                Fill_Data()
    '    '            End If
    '    '        Else

    '    '            If dtpfromDate.Value.Date > dtptoDate.Value.Date Then
    '    '                MessageBox.Show("From date is greater than To date", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    '                dtpfromDate.Focus()
    '    '                Return
    '    '            Else
    '    '                'Get list of report form Selected date to Selected date in Combobox
    '    '                If objHCFAReport.GetExams(dtpfromDate.Value.Date, dtptoDate.Value.Date, m_PatientId, 0) Then
    '    '                    Fill_Data()
    '    '                End If
    '    '            End If
    '    '        End If
    '    '    Else
    '    '        'Get list of report for selected provider
    '    '        If cmbCondition.Text = "is equal to" Then
    '    '            'Get list of report for Selected date
    '    '            dtptoDate.Value = dtpfromDate.Value
    '    '            If objHCFAReport.GetExams(dtpfromDate.Value.Date, dtptoDate.Value.Date, 0, m_ProviderID) Then
    '    '                Fill_Data()
    '    '            End If
    '    '        Else
    '    '            If dtpfromDate.Value.Date > dtptoDate.Value.Date Then
    '    '                MessageBox.Show("From date is greater than To date", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    '                dtpfromDate.Focus()
    '    '                Return
    '    '            Else
    '    '                'Get list of report form Selected date to Selected date in Combobox
    '    '                If objHCFAReport.GetExams(dtpfromDate.Value.Date, dtptoDate.Value.Date, 0, m_ProviderID) Then
    '    '                    Fill_Data()
    '    '                End If
    '    '            End If
    '    '        End If
    '    '    End If
    '    'Catch ex As Exception

    '    'End Try
    'End Sub
    'End 20091128 
End Class
