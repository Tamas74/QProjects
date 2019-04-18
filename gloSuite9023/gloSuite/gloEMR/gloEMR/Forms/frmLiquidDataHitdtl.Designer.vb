<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLiquidDataHitdtl
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLiquidDataHitdtl))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.tb_Hitdetails = New System.Windows.Forms.TabControl
        Me.tbp_Diagnosis = New System.Windows.Forms.TabPage
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.grpEMExamType = New System.Windows.Forms.GroupBox
        Me.lblChangeEMExamType = New System.Windows.Forms.Label
        Me.cmbEMExamType = New System.Windows.Forms.ComboBox
        Me.grpCodeType = New System.Windows.Forms.GroupBox
        Me.pnlOthervisittype = New System.Windows.Forms.Panel
        Me.Label61 = New System.Windows.Forms.Label
        Me.cmbcodetype = New System.Windows.Forms.ComboBox
        Me.pnlfix = New System.Windows.Forms.Panel
        Me.lblVisitType = New System.Windows.Forms.Label
        Me.grpOtherDiagnostictest = New System.Windows.Forms.GroupBox
        Me.chkODTindependentvisu = New System.Windows.Forms.CheckBox
        Me.chkOTDTDiscussperf = New System.Windows.Forms.CheckBox
        Me.numUpdownOtherDiagnosistests = New System.Windows.Forms.NumericUpDown
        Me.lblOD = New System.Windows.Forms.Label
        Me.grpDiagnosis = New System.Windows.Forms.GroupBox
        Me.chkDiagnosis = New System.Windows.Forms.CheckBox
        Me.cmbDignosis3 = New System.Windows.Forms.ComboBox
        Me.cmbDignosis8 = New System.Windows.Forms.ComboBox
        Me.lblDignosis1 = New System.Windows.Forms.Label
        Me.cmbDignosis7 = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbDignosis6 = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbDignosis5 = New System.Windows.Forms.ComboBox
        Me.lblDignosis2 = New System.Windows.Forms.Label
        Me.cmbDignosis4 = New System.Windows.Forms.ComboBox
        Me.lblDignosis3 = New System.Windows.Forms.Label
        Me.lblDignosis4 = New System.Windows.Forms.Label
        Me.cmbDignosis2 = New System.Windows.Forms.ComboBox
        Me.lblDignosis5 = New System.Windows.Forms.Label
        Me.cmbDignosis1 = New System.Windows.Forms.ComboBox
        Me.lblDignosis6 = New System.Windows.Forms.Label
        Me.lblDignosis8 = New System.Windows.Forms.Label
        Me.lblDignosis7 = New System.Windows.Forms.Label
        Me.tbp_History = New System.Windows.Forms.TabPage
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.tb_History = New System.Windows.Forms.TabControl
        Me.tbpage_HPI = New System.Windows.Forms.TabPage
        Me.Panel15 = New System.Windows.Forms.Panel
        Me.Label47 = New System.Windows.Forms.Label
        Me.C1HPI = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label48 = New System.Windows.Forms.Label
        Me.Label49 = New System.Windows.Forms.Label
        Me.Label50 = New System.Windows.Forms.Label
        Me.tbPageROS = New System.Windows.Forms.TabPage
        Me.Panel16 = New System.Windows.Forms.Panel
        Me.C1ROS = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label56 = New System.Windows.Forms.Label
        Me.Label57 = New System.Windows.Forms.Label
        Me.Label58 = New System.Windows.Forms.Label
        Me.Label59 = New System.Windows.Forms.Label
        Me.tbP_PatientHistory = New System.Windows.Forms.TabPage
        Me.Panel14 = New System.Windows.Forms.Panel
        Me.C1Details = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.tbp_Physicalexam = New System.Windows.Forms.TabPage
        Me.Panel13 = New System.Windows.Forms.Panel
        Me.C1PhysicalExamination = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.tbp_Medicalcondition = New System.Windows.Forms.TabPage
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.tb_MedicalComplexity = New System.Windows.Forms.TabControl
        Me.tb_Managmentoption = New System.Windows.Forms.TabPage
        Me.pnlManagementoption = New System.Windows.Forms.Panel
        Me.nudMTimeSpent = New System.Windows.Forms.NumericUpDown
        Me.lblMTimeSpent = New System.Windows.Forms.Label
        Me.chkMDecisionofcase = New System.Windows.Forms.CheckBox
        Me.ckkMreviewandsummary = New System.Windows.Forms.CheckBox
        Me.chkMDecisiontoobtain = New System.Windows.Forms.CheckBox
        Me.chkMDecisionnottoresuscitate = New System.Windows.Forms.CheckBox
        Me.chkMMajoremergencySurgery = New System.Windows.Forms.CheckBox
        Me.chkMMajorSurgerywrisk = New System.Windows.Forms.CheckBox
        Me.chkMMajorsurgeryworisk = New System.Windows.Forms.CheckBox
        Me.chkMMinorSurgeryWrisk = New System.Windows.Forms.CheckBox
        Me.chkMminorsurgeryWOrisk = New System.Windows.Forms.CheckBox
        Me.chkMClosefx = New System.Windows.Forms.CheckBox
        Me.chkMPhysicalOccupationaltherapy = New System.Windows.Forms.CheckBox
        Me.chkMNuclearMedicine = New System.Windows.Forms.CheckBox
        Me.chkMRespiratoryTreatment = New System.Windows.Forms.CheckBox
        Me.chkMTelemetry = New System.Windows.Forms.CheckBox
        Me.chkMHighRiskmeds = New System.Windows.Forms.CheckBox
        Me.chkMIVmedswadditives = New System.Windows.Forms.CheckBox
        Me.chkMivmeds = New System.Windows.Forms.CheckBox
        Me.chkMPerscripmeds = New System.Windows.Forms.CheckBox
        Me.chkMOTCmeds = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.tb_Labs = New System.Windows.Forms.TabPage
        Me.pnlLabbottom = New System.Windows.Forms.Panel
        Me.nudLOtherLabs = New System.Windows.Forms.NumericUpDown
        Me.chkLIndependentVisualizationoftest = New System.Windows.Forms.CheckBox
        Me.chkLDiscussionwithperformingPhysician = New System.Windows.Forms.CheckBox
        Me.lblLOtherLabs = New System.Windows.Forms.Label
        Me.chkLDeepIncisionalBiopsy = New System.Windows.Forms.CheckBox
        Me.chkLSuperficialbiopsy = New System.Windows.Forms.CheckBox
        Me.chkLTypesandCrossmatch = New System.Windows.Forms.CheckBox
        Me.chkLPT = New System.Windows.Forms.CheckBox
        Me.chkLABGS = New System.Windows.Forms.CheckBox
        Me.chkLCardiacenzymes = New System.Windows.Forms.CheckBox
        Me.chkLChemicalProfile = New System.Windows.Forms.CheckBox
        Me.chkLETOH = New System.Windows.Forms.CheckBox
        Me.chkLElectrolytes = New System.Windows.Forms.CheckBox
        Me.chkLBun = New System.Windows.Forms.CheckBox
        Me.chkLAmylase = New System.Windows.Forms.CheckBox
        Me.chkLPregnancyTest = New System.Windows.Forms.CheckBox
        Me.chkLFlu = New System.Windows.Forms.CheckBox
        Me.chkLLCBC = New System.Windows.Forms.CheckBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.C1Labs = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label35 = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.tb_XrayRadio = New System.Windows.Forms.TabPage
        Me.pnlRadiology = New System.Windows.Forms.Panel
        Me.numupXOther = New System.Windows.Forms.NumericUpDown
        Me.lblXOtherXRay = New System.Windows.Forms.Label
        Me.Label40 = New System.Windows.Forms.Label
        Me.chkXIndepedent = New System.Windows.Forms.CheckBox
        Me.Label41 = New System.Windows.Forms.Label
        Me.chkXperformingPhy = New System.Windows.Forms.CheckBox
        Me.Label42 = New System.Windows.Forms.Label
        Me.chkXVascularStudieswrisk = New System.Windows.Forms.CheckBox
        Me.Label43 = New System.Windows.Forms.Label
        Me.chkXVascularStudies = New System.Windows.Forms.CheckBox
        Me.chkXDiscographt = New System.Windows.Forms.CheckBox
        Me.chkXMRI = New System.Windows.Forms.CheckBox
        Me.chkXChest = New System.Windows.Forms.CheckBox
        Me.chkXcatScan = New System.Windows.Forms.CheckBox
        Me.chkXExtrimities = New System.Windows.Forms.CheckBox
        Me.chkXIVP = New System.Windows.Forms.CheckBox
        Me.chkXAbdomen = New System.Windows.Forms.CheckBox
        Me.chkXGIGallablader = New System.Windows.Forms.CheckBox
        Me.chkXHipPelvis = New System.Windows.Forms.CheckBox
        Me.chkXTLSpire = New System.Windows.Forms.CheckBox
        Me.chkXCspine = New System.Windows.Forms.CheckBox
        Me.chkXDiagosticUltrasound = New System.Windows.Forms.CheckBox
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.C1Radiology = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.tb_OtherDxTests = New System.Windows.Forms.TabPage
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.nudDignosisstudies = New System.Windows.Forms.NumericUpDown
        Me.lblOtherAdditionalDiagnosticStudies = New System.Windows.Forms.Label
        Me.chkOIndependentVisualization = New System.Windows.Forms.CheckBox
        Me.chkODiscuswithPerfoming = New System.Windows.Forms.CheckBox
        Me.chkOEndoScopewRisk = New System.Windows.Forms.CheckBox
        Me.chkOEndoscopeworisk = New System.Windows.Forms.CheckBox
        Me.chkOCuldcentesis = New System.Windows.Forms.CheckBox
        Me.chkOThoracentesis = New System.Windows.Forms.CheckBox
        Me.chkOLumbarPunctor = New System.Windows.Forms.CheckBox
        Me.chkONuclearScan = New System.Windows.Forms.CheckBox
        Me.chkOPulmonary = New System.Windows.Forms.CheckBox
        Me.chkODopplerFlowStudies = New System.Windows.Forms.CheckBox
        Me.chkOVectorCardiogram = New System.Windows.Forms.CheckBox
        Me.chkOEEG = New System.Windows.Forms.CheckBox
        Me.chkOTreadmill = New System.Windows.Forms.CheckBox
        Me.chkOHolterMonitor = New System.Windows.Forms.CheckBox
        Me.chkOEKG = New System.Windows.Forms.CheckBox
        Me.Label44 = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.Label46 = New System.Windows.Forms.Label
        Me.Label51 = New System.Windows.Forms.Label
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.Label52 = New System.Windows.Forms.Label
        Me.Label53 = New System.Windows.Forms.Label
        Me.Label54 = New System.Windows.Forms.Label
        Me.Label55 = New System.Windows.Forms.Label
        Me.tb_OthermedicalCondition = New System.Windows.Forms.TabPage
        Me.Panel11 = New System.Windows.Forms.Panel
        Me.Label15 = New System.Windows.Forms.Label
        Me.C1MedicalCondition = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Panel12 = New System.Windows.Forms.Panel
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.C1FlexGrid1 = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.C1FlexGrid2 = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.C1FlexGrid3 = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.C1FlexGrid4 = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.C1FlexGrid5 = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.TabPage6 = New System.Windows.Forms.TabPage
        Me.C1FlexGrid6 = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.tls_Liquiddata = New gloGlobal.gloToolStripIgnoreFocus
        Me.tlb_genrateEMCode = New System.Windows.Forms.ToolStripButton
        Me.tlb_Close = New System.Windows.Forms.ToolStripButton
        Me.C1SuperTooltip1 = New C1.Win.C1SuperTooltip.C1SuperTooltip(Me.components)
        Me.Panel1.SuspendLayout()
        Me.tb_Hitdetails.SuspendLayout()
        Me.tbp_Diagnosis.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.grpEMExamType.SuspendLayout()
        Me.grpCodeType.SuspendLayout()
        Me.pnlOthervisittype.SuspendLayout()
        Me.pnlfix.SuspendLayout()
        Me.grpOtherDiagnostictest.SuspendLayout()
        CType(Me.numUpdownOtherDiagnosistests, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDiagnosis.SuspendLayout()
        Me.tbp_History.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.tb_History.SuspendLayout()
        Me.tbpage_HPI.SuspendLayout()
        Me.Panel15.SuspendLayout()
        CType(Me.C1HPI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbPageROS.SuspendLayout()
        Me.Panel16.SuspendLayout()
        CType(Me.C1ROS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbP_PatientHistory.SuspendLayout()
        Me.Panel14.SuspendLayout()
        CType(Me.C1Details, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbp_Physicalexam.SuspendLayout()
        Me.Panel13.SuspendLayout()
        CType(Me.C1PhysicalExamination, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbp_Medicalcondition.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.tb_MedicalComplexity.SuspendLayout()
        Me.tb_Managmentoption.SuspendLayout()
        Me.pnlManagementoption.SuspendLayout()
        CType(Me.nudMTimeSpent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel9.SuspendLayout()
        Me.tb_Labs.SuspendLayout()
        Me.pnlLabbottom.SuspendLayout()
        CType(Me.nudLOtherLabs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        CType(Me.C1Labs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tb_XrayRadio.SuspendLayout()
        Me.pnlRadiology.SuspendLayout()
        CType(Me.numupXOther, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel7.SuspendLayout()
        CType(Me.C1Radiology, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tb_OtherDxTests.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.nudDignosisstudies, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.tb_OthermedicalCondition.SuspendLayout()
        Me.Panel11.SuspendLayout()
        CType(Me.C1MedicalCondition, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel12.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.C1FlexGrid2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.C1FlexGrid3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.C1FlexGrid4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        CType(Me.C1FlexGrid5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage6.SuspendLayout()
        CType(Me.C1FlexGrid6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.tls_Liquiddata.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel1.Controls.Add(Me.tb_Hitdetails)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 54)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(723, 601)
        Me.Panel1.TabIndex = 1
        '
        'tb_Hitdetails
        '
        Me.tb_Hitdetails.Controls.Add(Me.tbp_Diagnosis)
        Me.tb_Hitdetails.Controls.Add(Me.tbp_History)
        Me.tb_Hitdetails.Controls.Add(Me.tbp_Physicalexam)
        Me.tb_Hitdetails.Controls.Add(Me.tbp_Medicalcondition)
        Me.tb_Hitdetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_Hitdetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tb_Hitdetails.Location = New System.Drawing.Point(0, 0)
        Me.tb_Hitdetails.Name = "tb_Hitdetails"
        Me.tb_Hitdetails.SelectedIndex = 0
        Me.tb_Hitdetails.Size = New System.Drawing.Size(723, 601)
        Me.tb_Hitdetails.TabIndex = 1
        '
        'tbp_Diagnosis
        '
        Me.tbp_Diagnosis.Controls.Add(Me.Panel10)
        Me.tbp_Diagnosis.Location = New System.Drawing.Point(4, 23)
        Me.tbp_Diagnosis.Name = "tbp_Diagnosis"
        Me.tbp_Diagnosis.Size = New System.Drawing.Size(715, 574)
        Me.tbp_Diagnosis.TabIndex = 3
        Me.tbp_Diagnosis.Text = "Diagnosis"
        Me.tbp_Diagnosis.UseVisualStyleBackColor = True
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel10.Controls.Add(Me.grpEMExamType)
        Me.Panel10.Controls.Add(Me.grpCodeType)
        Me.Panel10.Controls.Add(Me.grpOtherDiagnostictest)
        Me.Panel10.Controls.Add(Me.grpDiagnosis)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Padding = New System.Windows.Forms.Padding(200, 40, 200, 40)
        Me.Panel10.Size = New System.Drawing.Size(715, 574)
        Me.Panel10.TabIndex = 61
        '
        'grpEMExamType
        '
        Me.grpEMExamType.Controls.Add(Me.lblChangeEMExamType)
        Me.grpEMExamType.Controls.Add(Me.cmbEMExamType)
        Me.grpEMExamType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpEMExamType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grpEMExamType.Location = New System.Drawing.Point(8, 72)
        Me.grpEMExamType.Name = "grpEMExamType"
        Me.grpEMExamType.Size = New System.Drawing.Size(693, 59)
        Me.grpEMExamType.TabIndex = 33
        Me.grpEMExamType.TabStop = False
        Me.grpEMExamType.Text = "E/M Exam Type"
        '
        'lblChangeEMExamType
        '
        Me.lblChangeEMExamType.AutoSize = True
        Me.lblChangeEMExamType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChangeEMExamType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblChangeEMExamType.Location = New System.Drawing.Point(15, 27)
        Me.lblChangeEMExamType.Name = "lblChangeEMExamType"
        Me.lblChangeEMExamType.Size = New System.Drawing.Size(146, 14)
        Me.lblChangeEMExamType.TabIndex = 1
        Me.lblChangeEMExamType.Text = "Change E/M Exam Type :"
        '
        'cmbEMExamType
        '
        Me.cmbEMExamType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEMExamType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbEMExamType.FormattingEnabled = True
        Me.cmbEMExamType.Location = New System.Drawing.Point(163, 23)
        Me.cmbEMExamType.Name = "cmbEMExamType"
        Me.cmbEMExamType.Size = New System.Drawing.Size(349, 22)
        Me.cmbEMExamType.TabIndex = 0
        '
        'grpCodeType
        '
        Me.grpCodeType.Controls.Add(Me.pnlOthervisittype)
        Me.grpCodeType.Controls.Add(Me.pnlfix)
        Me.grpCodeType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpCodeType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grpCodeType.Location = New System.Drawing.Point(8, 6)
        Me.grpCodeType.Name = "grpCodeType"
        Me.grpCodeType.Size = New System.Drawing.Size(693, 64)
        Me.grpCodeType.TabIndex = 32
        Me.grpCodeType.TabStop = False
        Me.grpCodeType.Text = "E/M Visit Type"
        '
        'pnlOthervisittype
        '
        Me.pnlOthervisittype.Controls.Add(Me.Label61)
        Me.pnlOthervisittype.Controls.Add(Me.cmbcodetype)
        Me.pnlOthervisittype.Location = New System.Drawing.Point(13, 18)
        Me.pnlOthervisittype.Name = "pnlOthervisittype"
        Me.pnlOthervisittype.Size = New System.Drawing.Size(662, 36)
        Me.pnlOthervisittype.TabIndex = 1
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label61.Location = New System.Drawing.Point(10, 11)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(139, 14)
        Me.Label61.TabIndex = 1
        Me.Label61.Text = "Change E/M Visit Type :"
        '
        'cmbcodetype
        '
        Me.cmbcodetype.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcodetype.FormattingEnabled = True
        Me.cmbcodetype.Location = New System.Drawing.Point(151, 7)
        Me.cmbcodetype.Name = "cmbcodetype"
        Me.cmbcodetype.Size = New System.Drawing.Size(349, 22)
        Me.cmbcodetype.TabIndex = 0
        '
        'pnlfix
        '
        Me.pnlfix.Controls.Add(Me.lblVisitType)
        Me.pnlfix.Location = New System.Drawing.Point(13, 18)
        Me.pnlfix.Name = "pnlfix"
        Me.pnlfix.Size = New System.Drawing.Size(662, 36)
        Me.pnlfix.TabIndex = 0
        '
        'lblVisitType
        '
        Me.lblVisitType.AutoSize = True
        Me.lblVisitType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVisitType.Location = New System.Drawing.Point(15, 11)
        Me.lblVisitType.Name = "lblVisitType"
        Me.lblVisitType.Size = New System.Drawing.Size(72, 14)
        Me.lblVisitType.TabIndex = 0
        Me.lblVisitType.Text = "Code type :"
        '
        'grpOtherDiagnostictest
        '
        Me.grpOtherDiagnostictest.Controls.Add(Me.chkODTindependentvisu)
        Me.grpOtherDiagnostictest.Controls.Add(Me.chkOTDTDiscussperf)
        Me.grpOtherDiagnostictest.Controls.Add(Me.numUpdownOtherDiagnosistests)
        Me.grpOtherDiagnostictest.Controls.Add(Me.lblOD)
        Me.grpOtherDiagnostictest.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpOtherDiagnostictest.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grpOtherDiagnostictest.Location = New System.Drawing.Point(8, 486)
        Me.grpOtherDiagnostictest.Name = "grpOtherDiagnostictest"
        Me.grpOtherDiagnostictest.Size = New System.Drawing.Size(1026, 66)
        Me.grpOtherDiagnostictest.TabIndex = 30
        Me.grpOtherDiagnostictest.TabStop = False
        Me.grpOtherDiagnostictest.Text = "Other Diagnostic Tests"
        Me.grpOtherDiagnostictest.Visible = False
        '
        'chkODTindependentvisu
        '
        Me.chkODTindependentvisu.AutoSize = True
        Me.chkODTindependentvisu.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkODTindependentvisu.Location = New System.Drawing.Point(19, 19)
        Me.chkODTindependentvisu.Name = "chkODTindependentvisu"
        Me.chkODTindependentvisu.Size = New System.Drawing.Size(207, 18)
        Me.chkODTindependentvisu.TabIndex = 8
        Me.chkODTindependentvisu.Text = "Independent Visualization of test"
        Me.chkODTindependentvisu.UseVisualStyleBackColor = True
        '
        'chkOTDTDiscussperf
        '
        Me.chkOTDTDiscussperf.AutoSize = True
        Me.chkOTDTDiscussperf.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOTDTDiscussperf.Location = New System.Drawing.Point(242, 19)
        Me.chkOTDTDiscussperf.Name = "chkOTDTDiscussperf"
        Me.chkOTDTDiscussperf.Size = New System.Drawing.Size(223, 18)
        Me.chkOTDTDiscussperf.TabIndex = 7
        Me.chkOTDTDiscussperf.Text = "Discussion with performing physician"
        Me.chkOTDTDiscussperf.UseVisualStyleBackColor = True
        '
        'numUpdownOtherDiagnosistests
        '
        Me.numUpdownOtherDiagnosistests.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numUpdownOtherDiagnosistests.Location = New System.Drawing.Point(254, 38)
        Me.numUpdownOtherDiagnosistests.Maximum = New Decimal(New Integer() {9, 0, 0, 0})
        Me.numUpdownOtherDiagnosistests.Name = "numUpdownOtherDiagnosistests"
        Me.numUpdownOtherDiagnosistests.Size = New System.Drawing.Size(40, 22)
        Me.numUpdownOtherDiagnosistests.TabIndex = 6
        '
        'lblOD
        '
        Me.lblOD.AutoSize = True
        Me.lblOD.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOD.Location = New System.Drawing.Point(18, 42)
        Me.lblOD.Name = "lblOD"
        Me.lblOD.Size = New System.Drawing.Size(230, 14)
        Me.lblOD.TabIndex = 5
        Me.lblOD.Text = "Other additional diagnostic studies(0-9) :"
        '
        'grpDiagnosis
        '
        Me.grpDiagnosis.Controls.Add(Me.chkDiagnosis)
        Me.grpDiagnosis.Controls.Add(Me.cmbDignosis3)
        Me.grpDiagnosis.Controls.Add(Me.cmbDignosis8)
        Me.grpDiagnosis.Controls.Add(Me.lblDignosis1)
        Me.grpDiagnosis.Controls.Add(Me.cmbDignosis7)
        Me.grpDiagnosis.Controls.Add(Me.Label1)
        Me.grpDiagnosis.Controls.Add(Me.cmbDignosis6)
        Me.grpDiagnosis.Controls.Add(Me.Label2)
        Me.grpDiagnosis.Controls.Add(Me.cmbDignosis5)
        Me.grpDiagnosis.Controls.Add(Me.lblDignosis2)
        Me.grpDiagnosis.Controls.Add(Me.cmbDignosis4)
        Me.grpDiagnosis.Controls.Add(Me.lblDignosis3)
        Me.grpDiagnosis.Controls.Add(Me.lblDignosis4)
        Me.grpDiagnosis.Controls.Add(Me.cmbDignosis2)
        Me.grpDiagnosis.Controls.Add(Me.lblDignosis5)
        Me.grpDiagnosis.Controls.Add(Me.cmbDignosis1)
        Me.grpDiagnosis.Controls.Add(Me.lblDignosis6)
        Me.grpDiagnosis.Controls.Add(Me.lblDignosis8)
        Me.grpDiagnosis.Controls.Add(Me.lblDignosis7)
        Me.grpDiagnosis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpDiagnosis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.grpDiagnosis.Location = New System.Drawing.Point(8, 133)
        Me.grpDiagnosis.Name = "grpDiagnosis"
        Me.grpDiagnosis.Size = New System.Drawing.Size(693, 350)
        Me.grpDiagnosis.TabIndex = 28
        Me.grpDiagnosis.TabStop = False
        Me.grpDiagnosis.Text = "Diagnosis"
        '
        'chkDiagnosis
        '
        Me.chkDiagnosis.AutoSize = True
        Me.chkDiagnosis.Location = New System.Drawing.Point(29, 317)
        Me.chkDiagnosis.Name = "chkDiagnosis"
        Me.chkDiagnosis.Size = New System.Drawing.Size(606, 18)
        Me.chkDiagnosis.TabIndex = 59
        Me.chkDiagnosis.Text = "Are any of the above illnesses a severe exacerbation, progression, or side effect" & _
            " of treatment?"
        Me.chkDiagnosis.UseVisualStyleBackColor = True
        '
        'cmbDignosis3
        '
        Me.cmbDignosis3.Enabled = False
        Me.cmbDignosis3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDignosis3.ForeColor = System.Drawing.Color.Black
        Me.cmbDignosis3.FormattingEnabled = True
        Me.cmbDignosis3.Location = New System.Drawing.Point(237, 111)
        Me.cmbDignosis3.Name = "cmbDignosis3"
        Me.cmbDignosis3.Size = New System.Drawing.Size(234, 22)
        Me.cmbDignosis3.TabIndex = 26
        '
        'cmbDignosis8
        '
        Me.cmbDignosis8.Enabled = False
        Me.cmbDignosis8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDignosis8.ForeColor = System.Drawing.Color.Black
        Me.cmbDignosis8.FormattingEnabled = True
        Me.cmbDignosis8.Location = New System.Drawing.Point(237, 281)
        Me.cmbDignosis8.Name = "cmbDignosis8"
        Me.cmbDignosis8.Size = New System.Drawing.Size(234, 22)
        Me.cmbDignosis8.TabIndex = 19
        '
        'lblDignosis1
        '
        Me.lblDignosis1.AutoSize = True
        Me.lblDignosis1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDignosis1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDignosis1.Location = New System.Drawing.Point(169, 47)
        Me.lblDignosis1.Name = "lblDignosis1"
        Me.lblDignosis1.Size = New System.Drawing.Size(36, 14)
        Me.lblDignosis1.TabIndex = 11
        Me.lblDignosis1.Text = "None"
        Me.lblDignosis1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbDignosis7
        '
        Me.cmbDignosis7.Enabled = False
        Me.cmbDignosis7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDignosis7.ForeColor = System.Drawing.Color.Black
        Me.cmbDignosis7.FormattingEnabled = True
        Me.cmbDignosis7.Location = New System.Drawing.Point(237, 247)
        Me.cmbDignosis7.Name = "cmbDignosis7"
        Me.cmbDignosis7.Size = New System.Drawing.Size(234, 22)
        Me.cmbDignosis7.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(158, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 14)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Diagnosis "
        '
        'cmbDignosis6
        '
        Me.cmbDignosis6.Enabled = False
        Me.cmbDignosis6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDignosis6.ForeColor = System.Drawing.Color.Black
        Me.cmbDignosis6.FormattingEnabled = True
        Me.cmbDignosis6.Location = New System.Drawing.Point(237, 213)
        Me.cmbDignosis6.Name = "cmbDignosis6"
        Me.cmbDignosis6.Size = New System.Drawing.Size(234, 22)
        Me.cmbDignosis6.TabIndex = 21
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(300, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(107, 14)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Type of Problem"
        '
        'cmbDignosis5
        '
        Me.cmbDignosis5.Enabled = False
        Me.cmbDignosis5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDignosis5.ForeColor = System.Drawing.Color.Black
        Me.cmbDignosis5.FormattingEnabled = True
        Me.cmbDignosis5.Location = New System.Drawing.Point(237, 179)
        Me.cmbDignosis5.Name = "cmbDignosis5"
        Me.cmbDignosis5.Size = New System.Drawing.Size(234, 22)
        Me.cmbDignosis5.TabIndex = 20
        '
        'lblDignosis2
        '
        Me.lblDignosis2.AutoSize = True
        Me.lblDignosis2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDignosis2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDignosis2.Location = New System.Drawing.Point(169, 81)
        Me.lblDignosis2.Name = "lblDignosis2"
        Me.lblDignosis2.Size = New System.Drawing.Size(36, 14)
        Me.lblDignosis2.TabIndex = 12
        Me.lblDignosis2.Text = "None"
        Me.lblDignosis2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbDignosis4
        '
        Me.cmbDignosis4.Enabled = False
        Me.cmbDignosis4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDignosis4.ForeColor = System.Drawing.Color.Black
        Me.cmbDignosis4.FormattingEnabled = True
        Me.cmbDignosis4.Location = New System.Drawing.Point(237, 145)
        Me.cmbDignosis4.Name = "cmbDignosis4"
        Me.cmbDignosis4.Size = New System.Drawing.Size(234, 22)
        Me.cmbDignosis4.TabIndex = 23
        '
        'lblDignosis3
        '
        Me.lblDignosis3.AutoSize = True
        Me.lblDignosis3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDignosis3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDignosis3.Location = New System.Drawing.Point(169, 115)
        Me.lblDignosis3.Name = "lblDignosis3"
        Me.lblDignosis3.Size = New System.Drawing.Size(36, 14)
        Me.lblDignosis3.TabIndex = 13
        Me.lblDignosis3.Text = "None"
        Me.lblDignosis3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDignosis4
        '
        Me.lblDignosis4.AutoSize = True
        Me.lblDignosis4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDignosis4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDignosis4.Location = New System.Drawing.Point(169, 149)
        Me.lblDignosis4.Name = "lblDignosis4"
        Me.lblDignosis4.Size = New System.Drawing.Size(36, 14)
        Me.lblDignosis4.TabIndex = 14
        Me.lblDignosis4.Text = "None"
        Me.lblDignosis4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbDignosis2
        '
        Me.cmbDignosis2.Enabled = False
        Me.cmbDignosis2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDignosis2.ForeColor = System.Drawing.Color.Black
        Me.cmbDignosis2.FormattingEnabled = True
        Me.cmbDignosis2.Location = New System.Drawing.Point(237, 77)
        Me.cmbDignosis2.Name = "cmbDignosis2"
        Me.cmbDignosis2.Size = New System.Drawing.Size(234, 22)
        Me.cmbDignosis2.TabIndex = 25
        '
        'lblDignosis5
        '
        Me.lblDignosis5.AutoSize = True
        Me.lblDignosis5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDignosis5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDignosis5.Location = New System.Drawing.Point(169, 183)
        Me.lblDignosis5.Name = "lblDignosis5"
        Me.lblDignosis5.Size = New System.Drawing.Size(36, 14)
        Me.lblDignosis5.TabIndex = 15
        Me.lblDignosis5.Text = "None"
        Me.lblDignosis5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbDignosis1
        '
        Me.cmbDignosis1.Enabled = False
        Me.cmbDignosis1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDignosis1.ForeColor = System.Drawing.Color.Black
        Me.cmbDignosis1.FormattingEnabled = True
        Me.cmbDignosis1.Location = New System.Drawing.Point(237, 43)
        Me.cmbDignosis1.Name = "cmbDignosis1"
        Me.cmbDignosis1.Size = New System.Drawing.Size(234, 22)
        Me.cmbDignosis1.TabIndex = 24
        '
        'lblDignosis6
        '
        Me.lblDignosis6.AutoSize = True
        Me.lblDignosis6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDignosis6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDignosis6.Location = New System.Drawing.Point(169, 217)
        Me.lblDignosis6.Name = "lblDignosis6"
        Me.lblDignosis6.Size = New System.Drawing.Size(36, 14)
        Me.lblDignosis6.TabIndex = 16
        Me.lblDignosis6.Text = "None"
        Me.lblDignosis6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDignosis8
        '
        Me.lblDignosis8.AutoSize = True
        Me.lblDignosis8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDignosis8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDignosis8.Location = New System.Drawing.Point(169, 285)
        Me.lblDignosis8.Name = "lblDignosis8"
        Me.lblDignosis8.Size = New System.Drawing.Size(36, 14)
        Me.lblDignosis8.TabIndex = 18
        Me.lblDignosis8.Text = "None"
        Me.lblDignosis8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDignosis7
        '
        Me.lblDignosis7.AutoSize = True
        Me.lblDignosis7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDignosis7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDignosis7.Location = New System.Drawing.Point(169, 251)
        Me.lblDignosis7.Name = "lblDignosis7"
        Me.lblDignosis7.Size = New System.Drawing.Size(36, 14)
        Me.lblDignosis7.TabIndex = 17
        Me.lblDignosis7.Text = "None"
        Me.lblDignosis7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbp_History
        '
        Me.tbp_History.Controls.Add(Me.Panel4)
        Me.tbp_History.Location = New System.Drawing.Point(4, 23)
        Me.tbp_History.Name = "tbp_History"
        Me.tbp_History.Padding = New System.Windows.Forms.Padding(3)
        Me.tbp_History.Size = New System.Drawing.Size(715, 574)
        Me.tbp_History.TabIndex = 0
        Me.tbp_History.Text = "History"
        Me.tbp_History.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel4.Controls.Add(Me.tb_History)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Padding = New System.Windows.Forms.Padding(4)
        Me.Panel4.Size = New System.Drawing.Size(709, 568)
        Me.Panel4.TabIndex = 59
        '
        'tb_History
        '
        Me.tb_History.Controls.Add(Me.tbpage_HPI)
        Me.tb_History.Controls.Add(Me.tbPageROS)
        Me.tb_History.Controls.Add(Me.tbP_PatientHistory)
        Me.tb_History.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_History.Location = New System.Drawing.Point(4, 4)
        Me.tb_History.Name = "tb_History"
        Me.tb_History.SelectedIndex = 0
        Me.tb_History.Size = New System.Drawing.Size(701, 560)
        Me.tb_History.TabIndex = 2
        '
        'tbpage_HPI
        '
        Me.tbpage_HPI.BackColor = System.Drawing.Color.FromArgb(CType(CType(163, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.tbpage_HPI.Controls.Add(Me.Panel15)
        Me.tbpage_HPI.Location = New System.Drawing.Point(4, 23)
        Me.tbpage_HPI.Name = "tbpage_HPI"
        Me.tbpage_HPI.Size = New System.Drawing.Size(693, 533)
        Me.tbpage_HPI.TabIndex = 0
        Me.tbpage_HPI.Text = "HPI"
        Me.tbpage_HPI.UseVisualStyleBackColor = True
        '
        'Panel15
        '
        Me.Panel15.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel15.Controls.Add(Me.Label47)
        Me.Panel15.Controls.Add(Me.C1HPI)
        Me.Panel15.Controls.Add(Me.Label48)
        Me.Panel15.Controls.Add(Me.Label49)
        Me.Panel15.Controls.Add(Me.Label50)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel15.Location = New System.Drawing.Point(0, 0)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel15.Size = New System.Drawing.Size(693, 533)
        Me.Panel15.TabIndex = 67
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(3, 4)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(1, 525)
        Me.Label47.TabIndex = 59
        Me.Label47.Text = "label4"
        '
        'C1HPI
        '
        Me.C1HPI.BackColor = System.Drawing.Color.White
        Me.C1HPI.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1HPI.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1HPI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1HPI.ExtendLastCol = True
        Me.C1HPI.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1HPI.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1HPI.ForeColor = System.Drawing.SystemColors.ControlText
        Me.C1HPI.Location = New System.Drawing.Point(3, 4)
        Me.C1HPI.Name = "C1HPI"
        Me.C1HPI.Rows.Count = 0
        Me.C1HPI.Rows.DefaultSize = 19
        Me.C1HPI.Rows.Fixed = 0
        Me.C1HPI.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1HPI.Size = New System.Drawing.Size(686, 525)
        Me.C1HPI.StyleInfo = resources.GetString("C1HPI.StyleInfo")
        Me.C1HPI.TabIndex = 59
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(689, 4)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(1, 525)
        Me.Label48.TabIndex = 60
        Me.Label48.Text = "label4"
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.Location = New System.Drawing.Point(3, 529)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(687, 1)
        Me.Label49.TabIndex = 61
        Me.Label49.Text = "label4"
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label50.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(3, 3)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(687, 1)
        Me.Label50.TabIndex = 62
        Me.Label50.Text = "label4"
        '
        'tbPageROS
        '
        Me.tbPageROS.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbPageROS.Controls.Add(Me.Panel16)
        Me.tbPageROS.Location = New System.Drawing.Point(4, 23)
        Me.tbPageROS.Name = "tbPageROS"
        Me.tbPageROS.Size = New System.Drawing.Size(693, 533)
        Me.tbPageROS.TabIndex = 1
        Me.tbPageROS.Text = "ROS"
        Me.tbPageROS.UseVisualStyleBackColor = True
        '
        'Panel16
        '
        Me.Panel16.Controls.Add(Me.C1ROS)
        Me.Panel16.Controls.Add(Me.Label56)
        Me.Panel16.Controls.Add(Me.Label57)
        Me.Panel16.Controls.Add(Me.Label58)
        Me.Panel16.Controls.Add(Me.Label59)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel16.Location = New System.Drawing.Point(0, 0)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel16.Size = New System.Drawing.Size(693, 533)
        Me.Panel16.TabIndex = 67
        '
        'C1ROS
        '
        Me.C1ROS.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1ROS.AllowEditing = False
        Me.C1ROS.BackColor = System.Drawing.Color.White
        Me.C1ROS.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1ROS.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1ROS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1ROS.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1ROS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1ROS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1ROS.Location = New System.Drawing.Point(4, 4)
        Me.C1ROS.Name = "C1ROS"
        Me.C1ROS.Rows.Count = 0
        Me.C1ROS.Rows.DefaultSize = 19
        Me.C1ROS.Rows.Fixed = 0
        Me.C1ROS.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1ROS.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1ROS.Size = New System.Drawing.Size(685, 525)
        Me.C1ROS.StyleInfo = resources.GetString("C1ROS.StyleInfo")
        Me.C1ROS.TabIndex = 63
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.Location = New System.Drawing.Point(3, 4)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(1, 525)
        Me.Label56.TabIndex = 59
        Me.Label56.Text = "label4"
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label57.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.Location = New System.Drawing.Point(689, 4)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(1, 525)
        Me.Label57.TabIndex = 60
        Me.Label57.Text = "label4"
        '
        'Label58
        '
        Me.Label58.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label58.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label58.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.Location = New System.Drawing.Point(3, 529)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(687, 1)
        Me.Label58.TabIndex = 61
        Me.Label58.Text = "label4"
        '
        'Label59
        '
        Me.Label59.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label59.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label59.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.Location = New System.Drawing.Point(3, 3)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(687, 1)
        Me.Label59.TabIndex = 62
        Me.Label59.Text = "label4"
        '
        'tbP_PatientHistory
        '
        Me.tbP_PatientHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbP_PatientHistory.Controls.Add(Me.Panel14)
        Me.tbP_PatientHistory.Location = New System.Drawing.Point(4, 23)
        Me.tbP_PatientHistory.Name = "tbP_PatientHistory"
        Me.tbP_PatientHistory.Size = New System.Drawing.Size(693, 533)
        Me.tbP_PatientHistory.TabIndex = 5
        Me.tbP_PatientHistory.Text = "Other History"
        Me.tbP_PatientHistory.UseVisualStyleBackColor = True
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.C1Details)
        Me.Panel14.Controls.Add(Me.Label27)
        Me.Panel14.Controls.Add(Me.Label28)
        Me.Panel14.Controls.Add(Me.Label29)
        Me.Panel14.Controls.Add(Me.Label30)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel14.Location = New System.Drawing.Point(0, 0)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel14.Size = New System.Drawing.Size(693, 533)
        Me.Panel14.TabIndex = 67
        '
        'C1Details
        '
        Me.C1Details.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1Details.AllowEditing = False
        Me.C1Details.BackColor = System.Drawing.Color.White
        Me.C1Details.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Details.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1Details.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Details.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1Details.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Details.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Details.Location = New System.Drawing.Point(4, 4)
        Me.C1Details.Name = "C1Details"
        Me.C1Details.Rows.Count = 0
        Me.C1Details.Rows.DefaultSize = 19
        Me.C1Details.Rows.Fixed = 0
        Me.C1Details.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Details.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Details.Size = New System.Drawing.Size(685, 525)
        Me.C1Details.StyleInfo = resources.GetString("C1Details.StyleInfo")
        Me.C1Details.TabIndex = 59
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(3, 4)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(1, 525)
        Me.Label27.TabIndex = 59
        Me.Label27.Text = "label4"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(689, 4)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 525)
        Me.Label28.TabIndex = 60
        Me.Label28.Text = "label4"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(3, 529)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(687, 1)
        Me.Label29.TabIndex = 61
        Me.Label29.Text = "label4"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(3, 3)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(687, 1)
        Me.Label30.TabIndex = 62
        Me.Label30.Text = "label4"
        '
        'tbp_Physicalexam
        '
        Me.tbp_Physicalexam.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbp_Physicalexam.Controls.Add(Me.Panel13)
        Me.tbp_Physicalexam.Location = New System.Drawing.Point(4, 23)
        Me.tbp_Physicalexam.Name = "tbp_Physicalexam"
        Me.tbp_Physicalexam.Padding = New System.Windows.Forms.Padding(3)
        Me.tbp_Physicalexam.Size = New System.Drawing.Size(715, 574)
        Me.tbp_Physicalexam.TabIndex = 1
        Me.tbp_Physicalexam.Text = "Physical Examination"
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.C1PhysicalExamination)
        Me.Panel13.Controls.Add(Me.Label7)
        Me.Panel13.Controls.Add(Me.Label8)
        Me.Panel13.Controls.Add(Me.Label9)
        Me.Panel13.Controls.Add(Me.Label10)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel13.Location = New System.Drawing.Point(3, 3)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel13.Size = New System.Drawing.Size(709, 568)
        Me.Panel13.TabIndex = 67
        '
        'C1PhysicalExamination
        '
        Me.C1PhysicalExamination.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1PhysicalExamination.AllowEditing = False
        Me.C1PhysicalExamination.BackColor = System.Drawing.Color.White
        Me.C1PhysicalExamination.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1PhysicalExamination.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1PhysicalExamination.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1PhysicalExamination.ExtendLastCol = True
        Me.C1PhysicalExamination.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1PhysicalExamination.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1PhysicalExamination.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1PhysicalExamination.Location = New System.Drawing.Point(4, 4)
        Me.C1PhysicalExamination.Name = "C1PhysicalExamination"
        Me.C1PhysicalExamination.Rows.Count = 0
        Me.C1PhysicalExamination.Rows.DefaultSize = 19
        Me.C1PhysicalExamination.Rows.Fixed = 0
        Me.C1PhysicalExamination.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1PhysicalExamination.Size = New System.Drawing.Size(701, 560)
        Me.C1PhysicalExamination.StyleInfo = resources.GetString("C1PhysicalExamination.StyleInfo")
        Me.C1PhysicalExamination.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 560)
        Me.Label7.TabIndex = 59
        Me.Label7.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(705, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 560)
        Me.Label8.TabIndex = 60
        Me.Label8.Text = "label4"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 564)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(703, 1)
        Me.Label9.TabIndex = 61
        Me.Label9.Text = "label4"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(703, 1)
        Me.Label10.TabIndex = 62
        Me.Label10.Text = "label4"
        '
        'tbp_Medicalcondition
        '
        Me.tbp_Medicalcondition.Controls.Add(Me.Panel3)
        Me.tbp_Medicalcondition.Location = New System.Drawing.Point(4, 23)
        Me.tbp_Medicalcondition.Name = "tbp_Medicalcondition"
        Me.tbp_Medicalcondition.Size = New System.Drawing.Size(715, 574)
        Me.tbp_Medicalcondition.TabIndex = 2
        Me.tbp_Medicalcondition.Text = "Medical Complexity"
        Me.tbp_Medicalcondition.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel3.Controls.Add(Me.tb_MedicalComplexity)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(4)
        Me.Panel3.Size = New System.Drawing.Size(715, 574)
        Me.Panel3.TabIndex = 3
        '
        'tb_MedicalComplexity
        '
        Me.tb_MedicalComplexity.Controls.Add(Me.tb_Managmentoption)
        Me.tb_MedicalComplexity.Controls.Add(Me.tb_Labs)
        Me.tb_MedicalComplexity.Controls.Add(Me.tb_XrayRadio)
        Me.tb_MedicalComplexity.Controls.Add(Me.tb_OtherDxTests)
        Me.tb_MedicalComplexity.Controls.Add(Me.tb_OthermedicalCondition)
        Me.tb_MedicalComplexity.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_MedicalComplexity.Location = New System.Drawing.Point(4, 4)
        Me.tb_MedicalComplexity.Name = "tb_MedicalComplexity"
        Me.tb_MedicalComplexity.SelectedIndex = 0
        Me.tb_MedicalComplexity.Size = New System.Drawing.Size(707, 566)
        Me.tb_MedicalComplexity.TabIndex = 2
        '
        'tb_Managmentoption
        '
        Me.tb_Managmentoption.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tb_Managmentoption.Controls.Add(Me.pnlManagementoption)
        Me.tb_Managmentoption.Controls.Add(Me.Panel9)
        Me.tb_Managmentoption.Location = New System.Drawing.Point(4, 23)
        Me.tb_Managmentoption.Name = "tb_Managmentoption"
        Me.tb_Managmentoption.Size = New System.Drawing.Size(699, 539)
        Me.tb_Managmentoption.TabIndex = 1
        Me.tb_Managmentoption.Text = "Management Option"
        Me.tb_Managmentoption.UseVisualStyleBackColor = True
        '
        'pnlManagementoption
        '
        Me.pnlManagementoption.Controls.Add(Me.nudMTimeSpent)
        Me.pnlManagementoption.Controls.Add(Me.lblMTimeSpent)
        Me.pnlManagementoption.Controls.Add(Me.chkMDecisionofcase)
        Me.pnlManagementoption.Controls.Add(Me.ckkMreviewandsummary)
        Me.pnlManagementoption.Controls.Add(Me.chkMDecisiontoobtain)
        Me.pnlManagementoption.Controls.Add(Me.chkMDecisionnottoresuscitate)
        Me.pnlManagementoption.Controls.Add(Me.chkMMajoremergencySurgery)
        Me.pnlManagementoption.Controls.Add(Me.chkMMajorSurgerywrisk)
        Me.pnlManagementoption.Controls.Add(Me.chkMMajorsurgeryworisk)
        Me.pnlManagementoption.Controls.Add(Me.chkMMinorSurgeryWrisk)
        Me.pnlManagementoption.Controls.Add(Me.chkMminorsurgeryWOrisk)
        Me.pnlManagementoption.Controls.Add(Me.chkMClosefx)
        Me.pnlManagementoption.Controls.Add(Me.chkMPhysicalOccupationaltherapy)
        Me.pnlManagementoption.Controls.Add(Me.chkMNuclearMedicine)
        Me.pnlManagementoption.Controls.Add(Me.chkMRespiratoryTreatment)
        Me.pnlManagementoption.Controls.Add(Me.chkMTelemetry)
        Me.pnlManagementoption.Controls.Add(Me.chkMHighRiskmeds)
        Me.pnlManagementoption.Controls.Add(Me.chkMIVmedswadditives)
        Me.pnlManagementoption.Controls.Add(Me.chkMivmeds)
        Me.pnlManagementoption.Controls.Add(Me.chkMPerscripmeds)
        Me.pnlManagementoption.Controls.Add(Me.chkMOTCmeds)
        Me.pnlManagementoption.Controls.Add(Me.Label4)
        Me.pnlManagementoption.Controls.Add(Me.Label5)
        Me.pnlManagementoption.Controls.Add(Me.Label6)
        Me.pnlManagementoption.Controls.Add(Me.Label11)
        Me.pnlManagementoption.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlManagementoption.Location = New System.Drawing.Point(0, 0)
        Me.pnlManagementoption.Name = "pnlManagementoption"
        Me.pnlManagementoption.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlManagementoption.Size = New System.Drawing.Size(699, 539)
        Me.pnlManagementoption.TabIndex = 61
        '
        'nudMTimeSpent
        '
        Me.nudMTimeSpent.Location = New System.Drawing.Point(452, 386)
        Me.nudMTimeSpent.Name = "nudMTimeSpent"
        Me.nudMTimeSpent.Size = New System.Drawing.Size(57, 22)
        Me.nudMTimeSpent.TabIndex = 83
        Me.nudMTimeSpent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblMTimeSpent
        '
        Me.lblMTimeSpent.AutoSize = True
        Me.lblMTimeSpent.Location = New System.Drawing.Point(141, 390)
        Me.lblMTimeSpent.Name = "lblMTimeSpent"
        Me.lblMTimeSpent.Size = New System.Drawing.Size(307, 14)
        Me.lblMTimeSpent.TabIndex = 82
        Me.lblMTimeSpent.Text = "Time Spent in conference with patient or family(min) :"
        '
        'chkMDecisionofcase
        '
        Me.chkMDecisionofcase.AutoSize = True
        Me.chkMDecisionofcase.Location = New System.Drawing.Point(102, 353)
        Me.chkMDecisionofcase.Name = "chkMDecisionofcase"
        Me.chkMDecisionofcase.Size = New System.Drawing.Size(301, 18)
        Me.chkMDecisionofcase.TabIndex = 81
        Me.chkMDecisionofcase.Text = "Decision of case with another health care provider"
        Me.chkMDecisionofcase.UseVisualStyleBackColor = True
        '
        'ckkMreviewandsummary
        '
        Me.ckkMreviewandsummary.AutoSize = True
        Me.ckkMreviewandsummary.Location = New System.Drawing.Point(102, 321)
        Me.ckkMreviewandsummary.Name = "ckkMreviewandsummary"
        Me.ckkMreviewandsummary.Size = New System.Drawing.Size(494, 18)
        Me.ckkMreviewandsummary.TabIndex = 80
        Me.ckkMreviewandsummary.Text = "Review and summ. old med. records and/or history from someone other than patient"
        Me.ckkMreviewandsummary.UseVisualStyleBackColor = True
        '
        'chkMDecisiontoobtain
        '
        Me.chkMDecisiontoobtain.AutoSize = True
        Me.chkMDecisiontoobtain.Location = New System.Drawing.Point(102, 289)
        Me.chkMDecisiontoobtain.Name = "chkMDecisiontoobtain"
        Me.chkMDecisiontoobtain.Size = New System.Drawing.Size(493, 18)
        Me.chkMDecisiontoobtain.TabIndex = 79
        Me.chkMDecisiontoobtain.Text = "Decision to obtain old med. records and/ or history from someone other than patie" & _
            "nt"
        Me.chkMDecisiontoobtain.UseVisualStyleBackColor = True
        '
        'chkMDecisionnottoresuscitate
        '
        Me.chkMDecisionnottoresuscitate.AutoSize = True
        Me.chkMDecisionnottoresuscitate.Location = New System.Drawing.Point(276, 257)
        Me.chkMDecisionnottoresuscitate.Name = "chkMDecisionnottoresuscitate"
        Me.chkMDecisionnottoresuscitate.Size = New System.Drawing.Size(172, 18)
        Me.chkMDecisionnottoresuscitate.TabIndex = 78
        Me.chkMDecisionnottoresuscitate.Text = "Decision not to resuscitate"
        Me.chkMDecisionnottoresuscitate.UseVisualStyleBackColor = True
        '
        'chkMMajoremergencySurgery
        '
        Me.chkMMajoremergencySurgery.AutoSize = True
        Me.chkMMajoremergencySurgery.Location = New System.Drawing.Point(276, 225)
        Me.chkMMajoremergencySurgery.Name = "chkMMajoremergencySurgery"
        Me.chkMMajoremergencySurgery.Size = New System.Drawing.Size(164, 18)
        Me.chkMMajoremergencySurgery.TabIndex = 77
        Me.chkMMajoremergencySurgery.Text = "Major emergency surgery"
        Me.chkMMajoremergencySurgery.UseVisualStyleBackColor = True
        '
        'chkMMajorSurgerywrisk
        '
        Me.chkMMajorSurgerywrisk.AutoSize = True
        Me.chkMMajorSurgerywrisk.Location = New System.Drawing.Point(276, 193)
        Me.chkMMajorSurgerywrisk.Name = "chkMMajorSurgerywrisk"
        Me.chkMMajorSurgerywrisk.Size = New System.Drawing.Size(182, 18)
        Me.chkMMajorSurgerywrisk.TabIndex = 76
        Me.chkMMajorSurgerywrisk.Text = "Major Surgery w/ risk factors"
        Me.chkMMajorSurgerywrisk.UseVisualStyleBackColor = True
        '
        'chkMMajorsurgeryworisk
        '
        Me.chkMMajorsurgeryworisk.AutoSize = True
        Me.chkMMajorsurgeryworisk.Location = New System.Drawing.Point(276, 161)
        Me.chkMMajorsurgeryworisk.Name = "chkMMajorsurgeryworisk"
        Me.chkMMajorsurgeryworisk.Size = New System.Drawing.Size(189, 18)
        Me.chkMMajorsurgeryworisk.TabIndex = 75
        Me.chkMMajorsurgeryworisk.Text = "Major Surgery w/o risk factors"
        Me.chkMMajorsurgeryworisk.UseVisualStyleBackColor = True
        '
        'chkMMinorSurgeryWrisk
        '
        Me.chkMMinorSurgeryWrisk.AutoSize = True
        Me.chkMMinorSurgeryWrisk.Location = New System.Drawing.Point(276, 129)
        Me.chkMMinorSurgeryWrisk.Name = "chkMMinorSurgeryWrisk"
        Me.chkMMinorSurgeryWrisk.Size = New System.Drawing.Size(182, 18)
        Me.chkMMinorSurgeryWrisk.TabIndex = 74
        Me.chkMMinorSurgeryWrisk.Text = "Minor Surgery w/ risk factors"
        Me.chkMMinorSurgeryWrisk.UseVisualStyleBackColor = True
        '
        'chkMminorsurgeryWOrisk
        '
        Me.chkMminorsurgeryWOrisk.AutoSize = True
        Me.chkMminorsurgeryWOrisk.Location = New System.Drawing.Point(276, 97)
        Me.chkMminorsurgeryWOrisk.Name = "chkMminorsurgeryWOrisk"
        Me.chkMminorsurgeryWOrisk.Size = New System.Drawing.Size(184, 18)
        Me.chkMminorsurgeryWOrisk.TabIndex = 73
        Me.chkMminorsurgeryWOrisk.Text = "Minor Surgery w/o risk factor"
        Me.chkMminorsurgeryWOrisk.UseVisualStyleBackColor = True
        '
        'chkMClosefx
        '
        Me.chkMClosefx.AutoSize = True
        Me.chkMClosefx.Location = New System.Drawing.Point(276, 65)
        Me.chkMClosefx.Name = "chkMClosefx"
        Me.chkMClosefx.Size = New System.Drawing.Size(221, 18)
        Me.chkMClosefx.TabIndex = 72
        Me.chkMClosefx.Text = "Closed fx or disloc w/o manipulation"
        Me.chkMClosefx.UseVisualStyleBackColor = True
        '
        'chkMPhysicalOccupationaltherapy
        '
        Me.chkMPhysicalOccupationaltherapy.AutoSize = True
        Me.chkMPhysicalOccupationaltherapy.Location = New System.Drawing.Point(276, 33)
        Me.chkMPhysicalOccupationaltherapy.Name = "chkMPhysicalOccupationaltherapy"
        Me.chkMPhysicalOccupationaltherapy.Size = New System.Drawing.Size(188, 18)
        Me.chkMPhysicalOccupationaltherapy.TabIndex = 71
        Me.chkMPhysicalOccupationaltherapy.Text = "Physical/Occupational therapy"
        Me.chkMPhysicalOccupationaltherapy.UseVisualStyleBackColor = True
        '
        'chkMNuclearMedicine
        '
        Me.chkMNuclearMedicine.AutoSize = True
        Me.chkMNuclearMedicine.Location = New System.Drawing.Point(102, 257)
        Me.chkMNuclearMedicine.Name = "chkMNuclearMedicine"
        Me.chkMNuclearMedicine.Size = New System.Drawing.Size(117, 18)
        Me.chkMNuclearMedicine.TabIndex = 70
        Me.chkMNuclearMedicine.Text = "Nuclear Medicine"
        Me.chkMNuclearMedicine.UseVisualStyleBackColor = True
        '
        'chkMRespiratoryTreatment
        '
        Me.chkMRespiratoryTreatment.AutoSize = True
        Me.chkMRespiratoryTreatment.Location = New System.Drawing.Point(102, 225)
        Me.chkMRespiratoryTreatment.Name = "chkMRespiratoryTreatment"
        Me.chkMRespiratoryTreatment.Size = New System.Drawing.Size(146, 18)
        Me.chkMRespiratoryTreatment.TabIndex = 69
        Me.chkMRespiratoryTreatment.Text = "Respiratory treatment"
        Me.chkMRespiratoryTreatment.UseVisualStyleBackColor = True
        '
        'chkMTelemetry
        '
        Me.chkMTelemetry.AutoSize = True
        Me.chkMTelemetry.Location = New System.Drawing.Point(102, 193)
        Me.chkMTelemetry.Name = "chkMTelemetry"
        Me.chkMTelemetry.Size = New System.Drawing.Size(82, 18)
        Me.chkMTelemetry.TabIndex = 68
        Me.chkMTelemetry.Text = "Telemetry"
        Me.chkMTelemetry.UseVisualStyleBackColor = True
        '
        'chkMHighRiskmeds
        '
        Me.chkMHighRiskmeds.AutoSize = True
        Me.chkMHighRiskmeds.Location = New System.Drawing.Point(102, 161)
        Me.chkMHighRiskmeds.Name = "chkMHighRiskmeds"
        Me.chkMHighRiskmeds.Size = New System.Drawing.Size(107, 18)
        Me.chkMHighRiskmeds.TabIndex = 67
        Me.chkMHighRiskmeds.Text = "High Risk meds"
        Me.chkMHighRiskmeds.UseVisualStyleBackColor = True
        '
        'chkMIVmedswadditives
        '
        Me.chkMIVmedswadditives.AutoSize = True
        Me.chkMIVmedswadditives.Location = New System.Drawing.Point(102, 129)
        Me.chkMIVmedswadditives.Name = "chkMIVmedswadditives"
        Me.chkMIVmedswadditives.Size = New System.Drawing.Size(145, 18)
        Me.chkMIVmedswadditives.TabIndex = 66
        Me.chkMIVmedswadditives.Text = "I.V. meds/w additives"
        Me.chkMIVmedswadditives.UseVisualStyleBackColor = True
        '
        'chkMivmeds
        '
        Me.chkMivmeds.AutoSize = True
        Me.chkMivmeds.Location = New System.Drawing.Point(102, 97)
        Me.chkMivmeds.Name = "chkMivmeds"
        Me.chkMivmeds.Size = New System.Drawing.Size(79, 18)
        Me.chkMivmeds.TabIndex = 65
        Me.chkMivmeds.Text = "I.V. meds"
        Me.chkMivmeds.UseVisualStyleBackColor = True
        '
        'chkMPerscripmeds
        '
        Me.chkMPerscripmeds.AutoSize = True
        Me.chkMPerscripmeds.Location = New System.Drawing.Point(102, 65)
        Me.chkMPerscripmeds.Name = "chkMPerscripmeds"
        Me.chkMPerscripmeds.Size = New System.Drawing.Size(119, 18)
        Me.chkMPerscripmeds.TabIndex = 64
        Me.chkMPerscripmeds.Text = "Prescrip/IM meds"
        Me.chkMPerscripmeds.UseVisualStyleBackColor = True
        '
        'chkMOTCmeds
        '
        Me.chkMOTCmeds.AutoSize = True
        Me.chkMOTCmeds.Location = New System.Drawing.Point(102, 33)
        Me.chkMOTCmeds.Name = "chkMOTCmeds"
        Me.chkMOTCmeds.Size = New System.Drawing.Size(83, 18)
        Me.chkMOTCmeds.TabIndex = 63
        Me.chkMOTCmeds.Text = "OTC meds"
        Me.chkMOTCmeds.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 531)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "label4"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(695, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 531)
        Me.Label5.TabIndex = 60
        Me.Label5.Text = "label4"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 535)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(693, 1)
        Me.Label6.TabIndex = 61
        Me.Label6.Text = "label4"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(3, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(693, 1)
        Me.Label11.TabIndex = 62
        Me.Label11.Text = "label4"
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.Label23)
        Me.Panel9.Controls.Add(Me.Label24)
        Me.Panel9.Controls.Add(Me.Label25)
        Me.Panel9.Controls.Add(Me.Label26)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel9.Size = New System.Drawing.Size(699, 539)
        Me.Panel9.TabIndex = 67
        Me.Panel9.Visible = False
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(3, 4)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(1, 531)
        Me.Label23.TabIndex = 59
        Me.Label23.Text = "label4"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(695, 4)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 531)
        Me.Label24.TabIndex = 60
        Me.Label24.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(3, 535)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(693, 1)
        Me.Label25.TabIndex = 61
        Me.Label25.Text = "label4"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(3, 3)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(693, 1)
        Me.Label26.TabIndex = 62
        Me.Label26.Text = "label4"
        '
        'tb_Labs
        '
        Me.tb_Labs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tb_Labs.Controls.Add(Me.pnlLabbottom)
        Me.tb_Labs.Controls.Add(Me.Panel8)
        Me.tb_Labs.Location = New System.Drawing.Point(4, 23)
        Me.tb_Labs.Name = "tb_Labs"
        Me.tb_Labs.Size = New System.Drawing.Size(699, 539)
        Me.tb_Labs.TabIndex = 2
        Me.tb_Labs.Text = "Labs"
        Me.tb_Labs.UseVisualStyleBackColor = True
        '
        'pnlLabbottom
        '
        Me.pnlLabbottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlLabbottom.Controls.Add(Me.nudLOtherLabs)
        Me.pnlLabbottom.Controls.Add(Me.chkLIndependentVisualizationoftest)
        Me.pnlLabbottom.Controls.Add(Me.chkLDiscussionwithperformingPhysician)
        Me.pnlLabbottom.Controls.Add(Me.lblLOtherLabs)
        Me.pnlLabbottom.Controls.Add(Me.chkLDeepIncisionalBiopsy)
        Me.pnlLabbottom.Controls.Add(Me.chkLSuperficialbiopsy)
        Me.pnlLabbottom.Controls.Add(Me.chkLTypesandCrossmatch)
        Me.pnlLabbottom.Controls.Add(Me.chkLPT)
        Me.pnlLabbottom.Controls.Add(Me.chkLABGS)
        Me.pnlLabbottom.Controls.Add(Me.chkLCardiacenzymes)
        Me.pnlLabbottom.Controls.Add(Me.chkLChemicalProfile)
        Me.pnlLabbottom.Controls.Add(Me.chkLETOH)
        Me.pnlLabbottom.Controls.Add(Me.chkLElectrolytes)
        Me.pnlLabbottom.Controls.Add(Me.chkLBun)
        Me.pnlLabbottom.Controls.Add(Me.chkLAmylase)
        Me.pnlLabbottom.Controls.Add(Me.chkLPregnancyTest)
        Me.pnlLabbottom.Controls.Add(Me.chkLFlu)
        Me.pnlLabbottom.Controls.Add(Me.chkLLCBC)
        Me.pnlLabbottom.Controls.Add(Me.Label12)
        Me.pnlLabbottom.Controls.Add(Me.Label13)
        Me.pnlLabbottom.Controls.Add(Me.Label14)
        Me.pnlLabbottom.Controls.Add(Me.Label39)
        Me.pnlLabbottom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLabbottom.Location = New System.Drawing.Point(0, 0)
        Me.pnlLabbottom.Name = "pnlLabbottom"
        Me.pnlLabbottom.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlLabbottom.Size = New System.Drawing.Size(699, 539)
        Me.pnlLabbottom.TabIndex = 60
        '
        'nudLOtherLabs
        '
        Me.nudLOtherLabs.Location = New System.Drawing.Point(378, 366)
        Me.nudLOtherLabs.Name = "nudLOtherLabs"
        Me.nudLOtherLabs.Size = New System.Drawing.Size(54, 22)
        Me.nudLOtherLabs.TabIndex = 80
        Me.nudLOtherLabs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkLIndependentVisualizationoftest
        '
        Me.chkLIndependentVisualizationoftest.AutoSize = True
        Me.chkLIndependentVisualizationoftest.Location = New System.Drawing.Point(157, 282)
        Me.chkLIndependentVisualizationoftest.Name = "chkLIndependentVisualizationoftest"
        Me.chkLIndependentVisualizationoftest.Size = New System.Drawing.Size(205, 18)
        Me.chkLIndependentVisualizationoftest.TabIndex = 79
        Me.chkLIndependentVisualizationoftest.Text = "Independent visualization of test"
        Me.chkLIndependentVisualizationoftest.UseVisualStyleBackColor = True
        '
        'chkLDiscussionwithperformingPhysician
        '
        Me.chkLDiscussionwithperformingPhysician.AutoSize = True
        Me.chkLDiscussionwithperformingPhysician.Location = New System.Drawing.Point(157, 315)
        Me.chkLDiscussionwithperformingPhysician.Name = "chkLDiscussionwithperformingPhysician"
        Me.chkLDiscussionwithperformingPhysician.Size = New System.Drawing.Size(223, 18)
        Me.chkLDiscussionwithperformingPhysician.TabIndex = 78
        Me.chkLDiscussionwithperformingPhysician.Text = "Discussion with performing physician"
        Me.chkLDiscussionwithperformingPhysician.UseVisualStyleBackColor = True
        '
        'lblLOtherLabs
        '
        Me.lblLOtherLabs.AutoSize = True
        Me.lblLOtherLabs.Location = New System.Drawing.Point(273, 370)
        Me.lblLOtherLabs.Name = "lblLOtherLabs"
        Me.lblLOtherLabs.Size = New System.Drawing.Size(103, 14)
        Me.lblLOtherLabs.TabIndex = 77
        Me.lblLOtherLabs.Text = "Other Labs[0-9] :"
        '
        'chkLDeepIncisionalBiopsy
        '
        Me.chkLDeepIncisionalBiopsy.AutoSize = True
        Me.chkLDeepIncisionalBiopsy.Location = New System.Drawing.Point(389, 249)
        Me.chkLDeepIncisionalBiopsy.Name = "chkLDeepIncisionalBiopsy"
        Me.chkLDeepIncisionalBiopsy.Size = New System.Drawing.Size(146, 18)
        Me.chkLDeepIncisionalBiopsy.TabIndex = 76
        Me.chkLDeepIncisionalBiopsy.Text = "Deep/Incisional biopsy"
        Me.chkLDeepIncisionalBiopsy.UseVisualStyleBackColor = True
        '
        'chkLSuperficialbiopsy
        '
        Me.chkLSuperficialbiopsy.AutoSize = True
        Me.chkLSuperficialbiopsy.Location = New System.Drawing.Point(389, 216)
        Me.chkLSuperficialbiopsy.Name = "chkLSuperficialbiopsy"
        Me.chkLSuperficialbiopsy.Size = New System.Drawing.Size(118, 18)
        Me.chkLSuperficialbiopsy.TabIndex = 75
        Me.chkLSuperficialbiopsy.Text = "Superficial biopsy"
        Me.chkLSuperficialbiopsy.UseVisualStyleBackColor = True
        '
        'chkLTypesandCrossmatch
        '
        Me.chkLTypesandCrossmatch.AutoSize = True
        Me.chkLTypesandCrossmatch.Location = New System.Drawing.Point(389, 183)
        Me.chkLTypesandCrossmatch.Name = "chkLTypesandCrossmatch"
        Me.chkLTypesandCrossmatch.Size = New System.Drawing.Size(147, 18)
        Me.chkLTypesandCrossmatch.TabIndex = 74
        Me.chkLTypesandCrossmatch.Text = "Type and cross match"
        Me.chkLTypesandCrossmatch.UseVisualStyleBackColor = True
        '
        'chkLPT
        '
        Me.chkLPT.AutoSize = True
        Me.chkLPT.Location = New System.Drawing.Point(389, 150)
        Me.chkLPT.Name = "chkLPT"
        Me.chkLPT.Size = New System.Drawing.Size(69, 18)
        Me.chkLPT.TabIndex = 73
        Me.chkLPT.Text = "PT/PTT"
        Me.chkLPT.UseVisualStyleBackColor = True
        '
        'chkLABGS
        '
        Me.chkLABGS.AutoSize = True
        Me.chkLABGS.Location = New System.Drawing.Point(389, 117)
        Me.chkLABGS.Name = "chkLABGS"
        Me.chkLABGS.Size = New System.Drawing.Size(54, 18)
        Me.chkLABGS.TabIndex = 72
        Me.chkLABGS.Text = "ABGs"
        Me.chkLABGS.UseVisualStyleBackColor = True
        '
        'chkLCardiacenzymes
        '
        Me.chkLCardiacenzymes.AutoSize = True
        Me.chkLCardiacenzymes.Location = New System.Drawing.Point(389, 84)
        Me.chkLCardiacenzymes.Name = "chkLCardiacenzymes"
        Me.chkLCardiacenzymes.Size = New System.Drawing.Size(115, 18)
        Me.chkLCardiacenzymes.TabIndex = 71
        Me.chkLCardiacenzymes.Text = "Cardiac enzymes"
        Me.chkLCardiacenzymes.UseVisualStyleBackColor = True
        '
        'chkLChemicalProfile
        '
        Me.chkLChemicalProfile.AutoSize = True
        Me.chkLChemicalProfile.Location = New System.Drawing.Point(389, 51)
        Me.chkLChemicalProfile.Name = "chkLChemicalProfile"
        Me.chkLChemicalProfile.Size = New System.Drawing.Size(110, 18)
        Me.chkLChemicalProfile.TabIndex = 70
        Me.chkLChemicalProfile.Text = "Chemical profile"
        Me.chkLChemicalProfile.UseVisualStyleBackColor = True
        '
        'chkLETOH
        '
        Me.chkLETOH.AutoSize = True
        Me.chkLETOH.Location = New System.Drawing.Point(157, 249)
        Me.chkLETOH.Name = "chkLETOH"
        Me.chkLETOH.Size = New System.Drawing.Size(131, 18)
        Me.chkLETOH.TabIndex = 69
        Me.chkLETOH.Text = "ETOH/Drug Screen"
        Me.chkLETOH.UseVisualStyleBackColor = True
        '
        'chkLElectrolytes
        '
        Me.chkLElectrolytes.AutoSize = True
        Me.chkLElectrolytes.Location = New System.Drawing.Point(157, 216)
        Me.chkLElectrolytes.Name = "chkLElectrolytes"
        Me.chkLElectrolytes.Size = New System.Drawing.Size(89, 18)
        Me.chkLElectrolytes.TabIndex = 68
        Me.chkLElectrolytes.Text = "Electrolytes"
        Me.chkLElectrolytes.UseVisualStyleBackColor = True
        '
        'chkLBun
        '
        Me.chkLBun.AutoSize = True
        Me.chkLBun.Location = New System.Drawing.Point(157, 183)
        Me.chkLBun.Name = "chkLBun"
        Me.chkLBun.Size = New System.Drawing.Size(106, 18)
        Me.chkLBun.TabIndex = 67
        Me.chkLBun.Text = "Bun/Creatinine"
        Me.chkLBun.UseVisualStyleBackColor = True
        '
        'chkLAmylase
        '
        Me.chkLAmylase.AutoSize = True
        Me.chkLAmylase.Location = New System.Drawing.Point(157, 150)
        Me.chkLAmylase.Name = "chkLAmylase"
        Me.chkLAmylase.Size = New System.Drawing.Size(70, 18)
        Me.chkLAmylase.TabIndex = 66
        Me.chkLAmylase.Text = "Amylase"
        Me.chkLAmylase.UseVisualStyleBackColor = True
        '
        'chkLPregnancyTest
        '
        Me.chkLPregnancyTest.AutoSize = True
        Me.chkLPregnancyTest.Location = New System.Drawing.Point(157, 117)
        Me.chkLPregnancyTest.Name = "chkLPregnancyTest"
        Me.chkLPregnancyTest.Size = New System.Drawing.Size(112, 18)
        Me.chkLPregnancyTest.TabIndex = 65
        Me.chkLPregnancyTest.Text = "Pregnancy Test"
        Me.chkLPregnancyTest.UseVisualStyleBackColor = True
        '
        'chkLFlu
        '
        Me.chkLFlu.AutoSize = True
        Me.chkLFlu.Location = New System.Drawing.Point(157, 84)
        Me.chkLFlu.Name = "chkLFlu"
        Me.chkLFlu.Size = New System.Drawing.Size(139, 18)
        Me.chkLFlu.TabIndex = 64
        Me.chkLFlu.Text = "Flu/Strep/Mono spot"
        Me.chkLFlu.UseVisualStyleBackColor = True
        '
        'chkLLCBC
        '
        Me.chkLLCBC.AutoSize = True
        Me.chkLLCBC.Location = New System.Drawing.Point(157, 51)
        Me.chkLLCBC.Name = "chkLLCBC"
        Me.chkLLCBC.Size = New System.Drawing.Size(68, 18)
        Me.chkLLCBC.TabIndex = 63
        Me.chkLLCBC.Text = "CBC/UA"
        Me.chkLLCBC.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(3, 4)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1, 531)
        Me.Label12.TabIndex = 59
        Me.Label12.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(695, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 531)
        Me.Label13.TabIndex = 60
        Me.Label13.Text = "label4"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 535)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(693, 1)
        Me.Label14.TabIndex = 61
        Me.Label14.Text = "label4"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(3, 3)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(693, 1)
        Me.Label39.TabIndex = 62
        Me.Label39.Text = "label4"
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.C1Labs)
        Me.Panel8.Controls.Add(Me.Label35)
        Me.Panel8.Controls.Add(Me.Label36)
        Me.Panel8.Controls.Add(Me.Label37)
        Me.Panel8.Controls.Add(Me.Label38)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel8.Size = New System.Drawing.Size(699, 539)
        Me.Panel8.TabIndex = 67
        Me.Panel8.Visible = False
        '
        'C1Labs
        '
        Me.C1Labs.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1Labs.AllowEditing = False
        Me.C1Labs.BackColor = System.Drawing.Color.White
        Me.C1Labs.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Labs.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1Labs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Labs.ExtendLastCol = True
        Me.C1Labs.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1Labs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Labs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Labs.Location = New System.Drawing.Point(4, 4)
        Me.C1Labs.Name = "C1Labs"
        Me.C1Labs.Rows.Count = 0
        Me.C1Labs.Rows.DefaultSize = 19
        Me.C1Labs.Rows.Fixed = 0
        Me.C1Labs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Labs.Size = New System.Drawing.Size(691, 531)
        Me.C1Labs.StyleInfo = resources.GetString("C1Labs.StyleInfo")
        Me.C1Labs.TabIndex = 59
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(3, 4)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(1, 531)
        Me.Label35.TabIndex = 59
        Me.Label35.Text = "label4"
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(695, 4)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(1, 531)
        Me.Label36.TabIndex = 60
        Me.Label36.Text = "label4"
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(3, 535)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(693, 1)
        Me.Label37.TabIndex = 61
        Me.Label37.Text = "label4"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(3, 3)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(693, 1)
        Me.Label38.TabIndex = 62
        Me.Label38.Text = "label4"
        '
        'tb_XrayRadio
        '
        Me.tb_XrayRadio.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tb_XrayRadio.Controls.Add(Me.pnlRadiology)
        Me.tb_XrayRadio.Controls.Add(Me.Panel7)
        Me.tb_XrayRadio.Location = New System.Drawing.Point(4, 23)
        Me.tb_XrayRadio.Name = "tb_XrayRadio"
        Me.tb_XrayRadio.Size = New System.Drawing.Size(699, 539)
        Me.tb_XrayRadio.TabIndex = 3
        Me.tb_XrayRadio.Text = "Orders"
        Me.tb_XrayRadio.UseVisualStyleBackColor = True
        '
        'pnlRadiology
        '
        Me.pnlRadiology.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlRadiology.Controls.Add(Me.numupXOther)
        Me.pnlRadiology.Controls.Add(Me.lblXOtherXRay)
        Me.pnlRadiology.Controls.Add(Me.Label40)
        Me.pnlRadiology.Controls.Add(Me.chkXIndepedent)
        Me.pnlRadiology.Controls.Add(Me.Label41)
        Me.pnlRadiology.Controls.Add(Me.chkXperformingPhy)
        Me.pnlRadiology.Controls.Add(Me.Label42)
        Me.pnlRadiology.Controls.Add(Me.chkXVascularStudieswrisk)
        Me.pnlRadiology.Controls.Add(Me.Label43)
        Me.pnlRadiology.Controls.Add(Me.chkXVascularStudies)
        Me.pnlRadiology.Controls.Add(Me.chkXDiscographt)
        Me.pnlRadiology.Controls.Add(Me.chkXMRI)
        Me.pnlRadiology.Controls.Add(Me.chkXChest)
        Me.pnlRadiology.Controls.Add(Me.chkXcatScan)
        Me.pnlRadiology.Controls.Add(Me.chkXExtrimities)
        Me.pnlRadiology.Controls.Add(Me.chkXIVP)
        Me.pnlRadiology.Controls.Add(Me.chkXAbdomen)
        Me.pnlRadiology.Controls.Add(Me.chkXGIGallablader)
        Me.pnlRadiology.Controls.Add(Me.chkXHipPelvis)
        Me.pnlRadiology.Controls.Add(Me.chkXTLSpire)
        Me.pnlRadiology.Controls.Add(Me.chkXCspine)
        Me.pnlRadiology.Controls.Add(Me.chkXDiagosticUltrasound)
        Me.pnlRadiology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRadiology.Location = New System.Drawing.Point(0, 0)
        Me.pnlRadiology.Name = "pnlRadiology"
        Me.pnlRadiology.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlRadiology.Size = New System.Drawing.Size(699, 539)
        Me.pnlRadiology.TabIndex = 64
        '
        'numupXOther
        '
        Me.numupXOther.Location = New System.Drawing.Point(368, 378)
        Me.numupXOther.Name = "numupXOther"
        Me.numupXOther.Size = New System.Drawing.Size(52, 22)
        Me.numupXOther.TabIndex = 86
        Me.numupXOther.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblXOtherXRay
        '
        Me.lblXOtherXRay.AutoSize = True
        Me.lblXOtherXRay.Location = New System.Drawing.Point(251, 382)
        Me.lblXOtherXRay.Name = "lblXOtherXRay"
        Me.lblXOtherXRay.Size = New System.Drawing.Size(111, 14)
        Me.lblXOtherXRay.TabIndex = 85
        Me.lblXOtherXRay.Text = "Other X-rays[0-9] :"
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(3, 4)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(1, 531)
        Me.Label40.TabIndex = 59
        Me.Label40.Text = "label4"
        '
        'chkXIndepedent
        '
        Me.chkXIndepedent.AutoSize = True
        Me.chkXIndepedent.Location = New System.Drawing.Point(172, 299)
        Me.chkXIndepedent.Name = "chkXIndepedent"
        Me.chkXIndepedent.Size = New System.Drawing.Size(205, 18)
        Me.chkXIndepedent.TabIndex = 84
        Me.chkXIndepedent.Text = "Independent visualization of test"
        Me.chkXIndepedent.UseVisualStyleBackColor = True
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(695, 4)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(1, 531)
        Me.Label41.TabIndex = 60
        Me.Label41.Text = "label4"
        '
        'chkXperformingPhy
        '
        Me.chkXperformingPhy.AutoSize = True
        Me.chkXperformingPhy.Location = New System.Drawing.Point(172, 336)
        Me.chkXperformingPhy.Name = "chkXperformingPhy"
        Me.chkXperformingPhy.Size = New System.Drawing.Size(223, 18)
        Me.chkXperformingPhy.TabIndex = 83
        Me.chkXperformingPhy.Text = "Discussion with performing physician"
        Me.chkXperformingPhy.UseVisualStyleBackColor = True
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(3, 535)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(693, 1)
        Me.Label42.TabIndex = 61
        Me.Label42.Text = "label4"
        '
        'chkXVascularStudieswrisk
        '
        Me.chkXVascularStudieswrisk.AutoSize = True
        Me.chkXVascularStudieswrisk.Location = New System.Drawing.Point(368, 262)
        Me.chkXVascularStudieswrisk.Name = "chkXVascularStudieswrisk"
        Me.chkXVascularStudieswrisk.Size = New System.Drawing.Size(152, 18)
        Me.chkXVascularStudieswrisk.TabIndex = 82
        Me.chkXVascularStudieswrisk.Text = "Vascular studies w/ risk"
        Me.chkXVascularStudieswrisk.UseVisualStyleBackColor = True
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(3, 3)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(693, 1)
        Me.Label43.TabIndex = 62
        Me.Label43.Text = "label4"
        '
        'chkXVascularStudies
        '
        Me.chkXVascularStudies.AutoSize = True
        Me.chkXVascularStudies.Location = New System.Drawing.Point(368, 225)
        Me.chkXVascularStudies.Name = "chkXVascularStudies"
        Me.chkXVascularStudies.Size = New System.Drawing.Size(159, 18)
        Me.chkXVascularStudies.TabIndex = 81
        Me.chkXVascularStudies.Text = "Vascular studies w/o risk"
        Me.chkXVascularStudies.UseVisualStyleBackColor = True
        '
        'chkXDiscographt
        '
        Me.chkXDiscographt.AutoSize = True
        Me.chkXDiscographt.Location = New System.Drawing.Point(172, 262)
        Me.chkXDiscographt.Name = "chkXDiscographt"
        Me.chkXDiscographt.Size = New System.Drawing.Size(91, 18)
        Me.chkXDiscographt.TabIndex = 75
        Me.chkXDiscographt.Text = "Discography"
        Me.chkXDiscographt.UseVisualStyleBackColor = True
        '
        'chkXMRI
        '
        Me.chkXMRI.AutoSize = True
        Me.chkXMRI.Location = New System.Drawing.Point(368, 188)
        Me.chkXMRI.Name = "chkXMRI"
        Me.chkXMRI.Size = New System.Drawing.Size(54, 18)
        Me.chkXMRI.TabIndex = 80
        Me.chkXMRI.Text = "M.R.I"
        Me.chkXMRI.UseVisualStyleBackColor = True
        '
        'chkXChest
        '
        Me.chkXChest.AutoSize = True
        Me.chkXChest.Location = New System.Drawing.Point(172, 40)
        Me.chkXChest.Name = "chkXChest"
        Me.chkXChest.Size = New System.Drawing.Size(57, 18)
        Me.chkXChest.TabIndex = 69
        Me.chkXChest.Text = "Chest"
        Me.chkXChest.UseVisualStyleBackColor = True
        '
        'chkXcatScan
        '
        Me.chkXcatScan.AutoSize = True
        Me.chkXcatScan.Location = New System.Drawing.Point(368, 151)
        Me.chkXcatScan.Name = "chkXcatScan"
        Me.chkXcatScan.Size = New System.Drawing.Size(77, 18)
        Me.chkXcatScan.TabIndex = 79
        Me.chkXcatScan.Text = "CAT scan"
        Me.chkXcatScan.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.chkXcatScan.UseVisualStyleBackColor = True
        '
        'chkXExtrimities
        '
        Me.chkXExtrimities.AutoSize = True
        Me.chkXExtrimities.Location = New System.Drawing.Point(172, 77)
        Me.chkXExtrimities.Name = "chkXExtrimities"
        Me.chkXExtrimities.Size = New System.Drawing.Size(86, 18)
        Me.chkXExtrimities.TabIndex = 70
        Me.chkXExtrimities.Text = "Extremities"
        Me.chkXExtrimities.UseVisualStyleBackColor = True
        '
        'chkXIVP
        '
        Me.chkXIVP.AutoSize = True
        Me.chkXIVP.Location = New System.Drawing.Point(368, 114)
        Me.chkXIVP.Name = "chkXIVP"
        Me.chkXIVP.Size = New System.Drawing.Size(57, 18)
        Me.chkXIVP.TabIndex = 78
        Me.chkXIVP.Text = "I.V.P."
        Me.chkXIVP.UseVisualStyleBackColor = True
        '
        'chkXAbdomen
        '
        Me.chkXAbdomen.AutoSize = True
        Me.chkXAbdomen.Location = New System.Drawing.Point(172, 114)
        Me.chkXAbdomen.Name = "chkXAbdomen"
        Me.chkXAbdomen.Size = New System.Drawing.Size(79, 18)
        Me.chkXAbdomen.TabIndex = 71
        Me.chkXAbdomen.Text = "Abdomen"
        Me.chkXAbdomen.UseVisualStyleBackColor = True
        '
        'chkXGIGallablader
        '
        Me.chkXGIGallablader.AutoSize = True
        Me.chkXGIGallablader.Location = New System.Drawing.Point(368, 77)
        Me.chkXGIGallablader.Name = "chkXGIGallablader"
        Me.chkXGIGallablader.Size = New System.Drawing.Size(135, 18)
        Me.chkXGIGallablader.TabIndex = 77
        Me.chkXGIGallablader.Text = "GI/Gallbladder series"
        Me.chkXGIGallablader.UseVisualStyleBackColor = True
        '
        'chkXHipPelvis
        '
        Me.chkXHipPelvis.AutoSize = True
        Me.chkXHipPelvis.Location = New System.Drawing.Point(172, 151)
        Me.chkXHipPelvis.Name = "chkXHipPelvis"
        Me.chkXHipPelvis.Size = New System.Drawing.Size(77, 18)
        Me.chkXHipPelvis.TabIndex = 72
        Me.chkXHipPelvis.Text = "Hip/Pelvis"
        Me.chkXHipPelvis.UseVisualStyleBackColor = True
        '
        'chkXTLSpire
        '
        Me.chkXTLSpire.AutoSize = True
        Me.chkXTLSpire.Location = New System.Drawing.Point(368, 40)
        Me.chkXTLSpire.Name = "chkXTLSpire"
        Me.chkXTLSpire.Size = New System.Drawing.Size(79, 18)
        Me.chkXTLSpire.TabIndex = 76
        Me.chkXTLSpire.Text = "T/L Spine"
        Me.chkXTLSpire.UseVisualStyleBackColor = True
        '
        'chkXCspine
        '
        Me.chkXCspine.AutoSize = True
        Me.chkXCspine.Location = New System.Drawing.Point(172, 188)
        Me.chkXCspine.Name = "chkXCspine"
        Me.chkXCspine.Size = New System.Drawing.Size(65, 18)
        Me.chkXCspine.TabIndex = 73
        Me.chkXCspine.Text = "C-spine"
        Me.chkXCspine.UseVisualStyleBackColor = True
        '
        'chkXDiagosticUltrasound
        '
        Me.chkXDiagosticUltrasound.AutoSize = True
        Me.chkXDiagosticUltrasound.Location = New System.Drawing.Point(172, 225)
        Me.chkXDiagosticUltrasound.Name = "chkXDiagosticUltrasound"
        Me.chkXDiagosticUltrasound.Size = New System.Drawing.Size(143, 18)
        Me.chkXDiagosticUltrasound.TabIndex = 74
        Me.chkXDiagosticUltrasound.Text = "Diagnostic Ultrasound"
        Me.chkXDiagosticUltrasound.UseVisualStyleBackColor = True
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.C1Radiology)
        Me.Panel7.Controls.Add(Me.Label31)
        Me.Panel7.Controls.Add(Me.Label32)
        Me.Panel7.Controls.Add(Me.Label33)
        Me.Panel7.Controls.Add(Me.Label34)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel7.Size = New System.Drawing.Size(699, 539)
        Me.Panel7.TabIndex = 67
        Me.Panel7.Visible = False
        '
        'C1Radiology
        '
        Me.C1Radiology.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1Radiology.AllowEditing = False
        Me.C1Radiology.BackColor = System.Drawing.Color.White
        Me.C1Radiology.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Radiology.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1Radiology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Radiology.ExtendLastCol = True
        Me.C1Radiology.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1Radiology.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Radiology.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Radiology.Location = New System.Drawing.Point(4, 4)
        Me.C1Radiology.Name = "C1Radiology"
        Me.C1Radiology.Rows.Count = 0
        Me.C1Radiology.Rows.DefaultSize = 19
        Me.C1Radiology.Rows.Fixed = 0
        Me.C1Radiology.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Radiology.Size = New System.Drawing.Size(691, 531)
        Me.C1Radiology.StyleInfo = resources.GetString("C1Radiology.StyleInfo")
        Me.C1Radiology.TabIndex = 63
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(3, 4)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(1, 531)
        Me.Label31.TabIndex = 59
        Me.Label31.Text = "label4"
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(695, 4)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(1, 531)
        Me.Label32.TabIndex = 60
        Me.Label32.Text = "label4"
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(3, 535)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(693, 1)
        Me.Label33.TabIndex = 61
        Me.Label33.Text = "label4"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(3, 3)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(693, 1)
        Me.Label34.TabIndex = 62
        Me.Label34.Text = "label4"
        '
        'tb_OtherDxTests
        '
        Me.tb_OtherDxTests.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tb_OtherDxTests.Controls.Add(Me.Panel5)
        Me.tb_OtherDxTests.Controls.Add(Me.Panel6)
        Me.tb_OtherDxTests.Location = New System.Drawing.Point(4, 23)
        Me.tb_OtherDxTests.Name = "tb_OtherDxTests"
        Me.tb_OtherDxTests.Size = New System.Drawing.Size(699, 539)
        Me.tb_OtherDxTests.TabIndex = 4
        Me.tb_OtherDxTests.Text = "Other Diagnostic Tests"
        Me.tb_OtherDxTests.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.nudDignosisstudies)
        Me.Panel5.Controls.Add(Me.lblOtherAdditionalDiagnosticStudies)
        Me.Panel5.Controls.Add(Me.chkOIndependentVisualization)
        Me.Panel5.Controls.Add(Me.chkODiscuswithPerfoming)
        Me.Panel5.Controls.Add(Me.chkOEndoScopewRisk)
        Me.Panel5.Controls.Add(Me.chkOEndoscopeworisk)
        Me.Panel5.Controls.Add(Me.chkOCuldcentesis)
        Me.Panel5.Controls.Add(Me.chkOThoracentesis)
        Me.Panel5.Controls.Add(Me.chkOLumbarPunctor)
        Me.Panel5.Controls.Add(Me.chkONuclearScan)
        Me.Panel5.Controls.Add(Me.chkOPulmonary)
        Me.Panel5.Controls.Add(Me.chkODopplerFlowStudies)
        Me.Panel5.Controls.Add(Me.chkOVectorCardiogram)
        Me.Panel5.Controls.Add(Me.chkOEEG)
        Me.Panel5.Controls.Add(Me.chkOTreadmill)
        Me.Panel5.Controls.Add(Me.chkOHolterMonitor)
        Me.Panel5.Controls.Add(Me.chkOEKG)
        Me.Panel5.Controls.Add(Me.Label44)
        Me.Panel5.Controls.Add(Me.Label45)
        Me.Panel5.Controls.Add(Me.Label46)
        Me.Panel5.Controls.Add(Me.Label51)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel5.Size = New System.Drawing.Size(699, 539)
        Me.Panel5.TabIndex = 65
        '
        'nudDignosisstudies
        '
        Me.nudDignosisstudies.Location = New System.Drawing.Point(397, 383)
        Me.nudDignosisstudies.Name = "nudDignosisstudies"
        Me.nudDignosisstudies.Size = New System.Drawing.Size(60, 22)
        Me.nudDignosisstudies.TabIndex = 97
        Me.nudDignosisstudies.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblOtherAdditionalDiagnosticStudies
        '
        Me.lblOtherAdditionalDiagnosticStudies.AutoSize = True
        Me.lblOtherAdditionalDiagnosticStudies.Location = New System.Drawing.Point(159, 387)
        Me.lblOtherAdditionalDiagnosticStudies.Name = "lblOtherAdditionalDiagnosticStudies"
        Me.lblOtherAdditionalDiagnosticStudies.Size = New System.Drawing.Size(235, 14)
        Me.lblOtherAdditionalDiagnosticStudies.TabIndex = 96
        Me.lblOtherAdditionalDiagnosticStudies.Text = "Other Additional Diagnostic Studies[0-9] :"
        '
        'chkOIndependentVisualization
        '
        Me.chkOIndependentVisualization.AutoSize = True
        Me.chkOIndependentVisualization.Location = New System.Drawing.Point(160, 300)
        Me.chkOIndependentVisualization.Name = "chkOIndependentVisualization"
        Me.chkOIndependentVisualization.Size = New System.Drawing.Size(205, 18)
        Me.chkOIndependentVisualization.TabIndex = 95
        Me.chkOIndependentVisualization.Text = "Independent visualization of test"
        Me.chkOIndependentVisualization.UseVisualStyleBackColor = True
        '
        'chkODiscuswithPerfoming
        '
        Me.chkODiscuswithPerfoming.AutoSize = True
        Me.chkODiscuswithPerfoming.Location = New System.Drawing.Point(160, 338)
        Me.chkODiscuswithPerfoming.Name = "chkODiscuswithPerfoming"
        Me.chkODiscuswithPerfoming.Size = New System.Drawing.Size(223, 18)
        Me.chkODiscuswithPerfoming.TabIndex = 94
        Me.chkODiscuswithPerfoming.Text = "Discussion with performing physician"
        Me.chkODiscuswithPerfoming.UseVisualStyleBackColor = True
        '
        'chkOEndoScopewRisk
        '
        Me.chkOEndoScopewRisk.AutoSize = True
        Me.chkOEndoScopewRisk.Location = New System.Drawing.Point(337, 224)
        Me.chkOEndoScopewRisk.Name = "chkOEndoScopewRisk"
        Me.chkOEndoScopewRisk.Size = New System.Drawing.Size(128, 18)
        Me.chkOEndoScopewRisk.TabIndex = 93
        Me.chkOEndoScopewRisk.Text = "EndoScope w/ risk"
        Me.chkOEndoScopewRisk.UseVisualStyleBackColor = True
        '
        'chkOEndoscopeworisk
        '
        Me.chkOEndoscopeworisk.AutoSize = True
        Me.chkOEndoscopeworisk.Location = New System.Drawing.Point(337, 186)
        Me.chkOEndoscopeworisk.Name = "chkOEndoscopeworisk"
        Me.chkOEndoscopeworisk.Size = New System.Drawing.Size(133, 18)
        Me.chkOEndoscopeworisk.TabIndex = 92
        Me.chkOEndoscopeworisk.Text = "Endoscope w/o risk"
        Me.chkOEndoscopeworisk.UseVisualStyleBackColor = True
        '
        'chkOCuldcentesis
        '
        Me.chkOCuldcentesis.AutoSize = True
        Me.chkOCuldcentesis.Location = New System.Drawing.Point(337, 148)
        Me.chkOCuldcentesis.Name = "chkOCuldcentesis"
        Me.chkOCuldcentesis.Size = New System.Drawing.Size(100, 18)
        Me.chkOCuldcentesis.TabIndex = 91
        Me.chkOCuldcentesis.Text = "Culdocentesis"
        Me.chkOCuldcentesis.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.chkOCuldcentesis.UseVisualStyleBackColor = True
        '
        'chkOThoracentesis
        '
        Me.chkOThoracentesis.AutoSize = True
        Me.chkOThoracentesis.Location = New System.Drawing.Point(337, 110)
        Me.chkOThoracentesis.Name = "chkOThoracentesis"
        Me.chkOThoracentesis.Size = New System.Drawing.Size(102, 18)
        Me.chkOThoracentesis.TabIndex = 90
        Me.chkOThoracentesis.Text = "Thoracentesis"
        Me.chkOThoracentesis.UseVisualStyleBackColor = True
        '
        'chkOLumbarPunctor
        '
        Me.chkOLumbarPunctor.AutoSize = True
        Me.chkOLumbarPunctor.Location = New System.Drawing.Point(337, 72)
        Me.chkOLumbarPunctor.Name = "chkOLumbarPunctor"
        Me.chkOLumbarPunctor.Size = New System.Drawing.Size(120, 18)
        Me.chkOLumbarPunctor.TabIndex = 89
        Me.chkOLumbarPunctor.Text = "Lumbar puncture"
        Me.chkOLumbarPunctor.UseVisualStyleBackColor = True
        '
        'chkONuclearScan
        '
        Me.chkONuclearScan.AutoSize = True
        Me.chkONuclearScan.Location = New System.Drawing.Point(337, 34)
        Me.chkONuclearScan.Name = "chkONuclearScan"
        Me.chkONuclearScan.Size = New System.Drawing.Size(99, 18)
        Me.chkONuclearScan.TabIndex = 88
        Me.chkONuclearScan.Text = "Nuclear scans"
        Me.chkONuclearScan.UseVisualStyleBackColor = True
        '
        'chkOPulmonary
        '
        Me.chkOPulmonary.AutoSize = True
        Me.chkOPulmonary.Location = New System.Drawing.Point(160, 262)
        Me.chkOPulmonary.Name = "chkOPulmonary"
        Me.chkOPulmonary.Size = New System.Drawing.Size(124, 18)
        Me.chkOPulmonary.TabIndex = 87
        Me.chkOPulmonary.Text = "Pulmonary studies"
        Me.chkOPulmonary.UseVisualStyleBackColor = True
        '
        'chkODopplerFlowStudies
        '
        Me.chkODopplerFlowStudies.AutoSize = True
        Me.chkODopplerFlowStudies.Location = New System.Drawing.Point(160, 224)
        Me.chkODopplerFlowStudies.Name = "chkODopplerFlowStudies"
        Me.chkODopplerFlowStudies.Size = New System.Drawing.Size(137, 18)
        Me.chkODopplerFlowStudies.TabIndex = 86
        Me.chkODopplerFlowStudies.Text = "Doppler flow studies"
        Me.chkODopplerFlowStudies.UseVisualStyleBackColor = True
        '
        'chkOVectorCardiogram
        '
        Me.chkOVectorCardiogram.AutoSize = True
        Me.chkOVectorCardiogram.Location = New System.Drawing.Point(160, 186)
        Me.chkOVectorCardiogram.Name = "chkOVectorCardiogram"
        Me.chkOVectorCardiogram.Size = New System.Drawing.Size(122, 18)
        Me.chkOVectorCardiogram.TabIndex = 85
        Me.chkOVectorCardiogram.Text = "Vectorcardiogram"
        Me.chkOVectorCardiogram.UseVisualStyleBackColor = True
        '
        'chkOEEG
        '
        Me.chkOEEG.AutoSize = True
        Me.chkOEEG.Location = New System.Drawing.Point(160, 148)
        Me.chkOEEG.Name = "chkOEEG"
        Me.chkOEEG.Size = New System.Drawing.Size(77, 18)
        Me.chkOEEG.TabIndex = 84
        Me.chkOEEG.Text = "EEG/EMG"
        Me.chkOEEG.UseVisualStyleBackColor = True
        '
        'chkOTreadmill
        '
        Me.chkOTreadmill.AutoSize = True
        Me.chkOTreadmill.Location = New System.Drawing.Point(160, 110)
        Me.chkOTreadmill.Name = "chkOTreadmill"
        Me.chkOTreadmill.Size = New System.Drawing.Size(136, 18)
        Me.chkOTreadmill.TabIndex = 83
        Me.chkOTreadmill.Text = "Treadmill/stress test"
        Me.chkOTreadmill.UseVisualStyleBackColor = True
        '
        'chkOHolterMonitor
        '
        Me.chkOHolterMonitor.AutoSize = True
        Me.chkOHolterMonitor.Location = New System.Drawing.Point(160, 72)
        Me.chkOHolterMonitor.Name = "chkOHolterMonitor"
        Me.chkOHolterMonitor.Size = New System.Drawing.Size(105, 18)
        Me.chkOHolterMonitor.TabIndex = 82
        Me.chkOHolterMonitor.Text = "Holter monitor"
        Me.chkOHolterMonitor.UseVisualStyleBackColor = True
        '
        'chkOEKG
        '
        Me.chkOEKG.AutoSize = True
        Me.chkOEKG.Location = New System.Drawing.Point(160, 34)
        Me.chkOEKG.Name = "chkOEKG"
        Me.chkOEKG.Size = New System.Drawing.Size(48, 18)
        Me.chkOEKG.TabIndex = 81
        Me.chkOEKG.Text = "EKG"
        Me.chkOEKG.UseVisualStyleBackColor = True
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(3, 4)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(1, 531)
        Me.Label44.TabIndex = 59
        Me.Label44.Text = "label4"
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(695, 4)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(1, 531)
        Me.Label45.TabIndex = 60
        Me.Label45.Text = "label4"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(3, 535)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(693, 1)
        Me.Label46.TabIndex = 61
        Me.Label46.Text = "label4"
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label51.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.Location = New System.Drawing.Point(3, 3)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(693, 1)
        Me.Label51.TabIndex = 62
        Me.Label51.Text = "label4"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Label52)
        Me.Panel6.Controls.Add(Me.Label53)
        Me.Panel6.Controls.Add(Me.Label54)
        Me.Panel6.Controls.Add(Me.Label55)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel6.Size = New System.Drawing.Size(699, 539)
        Me.Panel6.TabIndex = 66
        Me.Panel6.Visible = False
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.Location = New System.Drawing.Point(3, 4)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(1, 531)
        Me.Label52.TabIndex = 59
        Me.Label52.Text = "label4"
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label53.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label53.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.Location = New System.Drawing.Point(695, 4)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(1, 531)
        Me.Label53.TabIndex = 60
        Me.Label53.Text = "label4"
        '
        'Label54
        '
        Me.Label54.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label54.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label54.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.Location = New System.Drawing.Point(3, 535)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(693, 1)
        Me.Label54.TabIndex = 61
        Me.Label54.Text = "label4"
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.Location = New System.Drawing.Point(3, 3)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(693, 1)
        Me.Label55.TabIndex = 62
        Me.Label55.Text = "label4"
        '
        'tb_OthermedicalCondition
        '
        Me.tb_OthermedicalCondition.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tb_OthermedicalCondition.Controls.Add(Me.Panel11)
        Me.tb_OthermedicalCondition.Controls.Add(Me.Panel12)
        Me.tb_OthermedicalCondition.Location = New System.Drawing.Point(4, 23)
        Me.tb_OthermedicalCondition.Name = "tb_OthermedicalCondition"
        Me.tb_OthermedicalCondition.Size = New System.Drawing.Size(699, 539)
        Me.tb_OthermedicalCondition.TabIndex = 5
        Me.tb_OthermedicalCondition.Text = "Other"
        Me.tb_OthermedicalCondition.UseVisualStyleBackColor = True
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.Label15)
        Me.Panel11.Controls.Add(Me.C1MedicalCondition)
        Me.Panel11.Controls.Add(Me.Label16)
        Me.Panel11.Controls.Add(Me.Label17)
        Me.Panel11.Controls.Add(Me.Label18)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel11.Location = New System.Drawing.Point(0, 0)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel11.Size = New System.Drawing.Size(699, 485)
        Me.Panel11.TabIndex = 67
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(3, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1, 477)
        Me.Label15.TabIndex = 59
        Me.Label15.Text = "label4"
        '
        'C1MedicalCondition
        '
        Me.C1MedicalCondition.BackColor = System.Drawing.Color.White
        Me.C1MedicalCondition.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1MedicalCondition.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1MedicalCondition.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1MedicalCondition.ExtendLastCol = True
        Me.C1MedicalCondition.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1MedicalCondition.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1MedicalCondition.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1MedicalCondition.Location = New System.Drawing.Point(3, 4)
        Me.C1MedicalCondition.Name = "C1MedicalCondition"
        Me.C1MedicalCondition.Rows.Count = 0
        Me.C1MedicalCondition.Rows.DefaultSize = 19
        Me.C1MedicalCondition.Rows.Fixed = 0
        Me.C1MedicalCondition.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1MedicalCondition.Size = New System.Drawing.Size(692, 477)
        Me.C1MedicalCondition.StyleInfo = resources.GetString("C1MedicalCondition.StyleInfo")
        Me.C1MedicalCondition.TabIndex = 1
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(695, 4)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 477)
        Me.Label16.TabIndex = 60
        Me.Label16.Text = "label4"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(3, 481)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(693, 1)
        Me.Label17.TabIndex = 61
        Me.Label17.Text = "label4"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(3, 3)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(693, 1)
        Me.Label18.TabIndex = 62
        Me.Label18.Text = "label4"
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.Label19)
        Me.Panel12.Controls.Add(Me.Label20)
        Me.Panel12.Controls.Add(Me.Label21)
        Me.Panel12.Controls.Add(Me.Label22)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel12.Location = New System.Drawing.Point(0, 485)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Padding = New System.Windows.Forms.Padding(3)
        Me.Panel12.Size = New System.Drawing.Size(699, 54)
        Me.Panel12.TabIndex = 68
        Me.Panel12.Visible = False
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(3, 4)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 46)
        Me.Label19.TabIndex = 59
        Me.Label19.Text = "label4"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(695, 4)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1, 46)
        Me.Label20.TabIndex = 60
        Me.Label20.Text = "label4"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(3, 50)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(693, 1)
        Me.Label21.TabIndex = 61
        Me.Label21.Text = "label4"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(3, 3)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(693, 1)
        Me.Label22.TabIndex = 62
        Me.Label22.Text = "label4"
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.C1FlexGrid1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(870, 479)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "History"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'C1FlexGrid1
        '
        Me.C1FlexGrid1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1FlexGrid1.AllowEditing = False
        Me.C1FlexGrid1.BackColor = System.Drawing.Color.White
        Me.C1FlexGrid1.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
        Me.C1FlexGrid1.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1FlexGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1FlexGrid1.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1FlexGrid1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1FlexGrid1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.C1FlexGrid1.Location = New System.Drawing.Point(3, 3)
        Me.C1FlexGrid1.Name = "C1FlexGrid1"
        Me.C1FlexGrid1.Rows.Count = 0
        Me.C1FlexGrid1.Rows.DefaultSize = 19
        Me.C1FlexGrid1.Rows.Fixed = 0
        Me.C1FlexGrid1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1FlexGrid1.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1FlexGrid1.Size = New System.Drawing.Size(864, 473)
        Me.C1FlexGrid1.StyleInfo = resources.GetString("C1FlexGrid1.StyleInfo")
        Me.C1FlexGrid1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.C1FlexGrid2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(870, 479)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Physical Examination"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'C1FlexGrid2
        '
        Me.C1FlexGrid2.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1FlexGrid2.AllowEditing = False
        Me.C1FlexGrid2.BackColor = System.Drawing.Color.White
        Me.C1FlexGrid2.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
        Me.C1FlexGrid2.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1FlexGrid2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1FlexGrid2.ExtendLastCol = True
        Me.C1FlexGrid2.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1FlexGrid2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1FlexGrid2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.C1FlexGrid2.Location = New System.Drawing.Point(3, 3)
        Me.C1FlexGrid2.Name = "C1FlexGrid2"
        Me.C1FlexGrid2.Rows.Count = 0
        Me.C1FlexGrid2.Rows.DefaultSize = 19
        Me.C1FlexGrid2.Rows.Fixed = 0
        Me.C1FlexGrid2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1FlexGrid2.Size = New System.Drawing.Size(864, 473)
        Me.C1FlexGrid2.StyleInfo = resources.GetString("C1FlexGrid2.StyleInfo")
        Me.C1FlexGrid2.TabIndex = 1
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.C1FlexGrid3)
        Me.TabPage3.Location = New System.Drawing.Point(4, 23)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(870, 479)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Medical Condition"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'C1FlexGrid3
        '
        Me.C1FlexGrid3.BackColor = System.Drawing.Color.White
        Me.C1FlexGrid3.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
        Me.C1FlexGrid3.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1FlexGrid3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1FlexGrid3.ExtendLastCol = True
        Me.C1FlexGrid3.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1FlexGrid3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1FlexGrid3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.C1FlexGrid3.Location = New System.Drawing.Point(0, 0)
        Me.C1FlexGrid3.Name = "C1FlexGrid3"
        Me.C1FlexGrid3.Rows.Count = 0
        Me.C1FlexGrid3.Rows.DefaultSize = 19
        Me.C1FlexGrid3.Rows.Fixed = 0
        Me.C1FlexGrid3.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1FlexGrid3.Size = New System.Drawing.Size(870, 479)
        Me.C1FlexGrid3.StyleInfo = resources.GetString("C1FlexGrid3.StyleInfo")
        Me.C1FlexGrid3.TabIndex = 1
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.C1FlexGrid4)
        Me.TabPage4.Location = New System.Drawing.Point(4, 23)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(870, 479)
        Me.TabPage4.TabIndex = 0
        Me.TabPage4.Text = "History"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'C1FlexGrid4
        '
        Me.C1FlexGrid4.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1FlexGrid4.AllowEditing = False
        Me.C1FlexGrid4.BackColor = System.Drawing.Color.White
        Me.C1FlexGrid4.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
        Me.C1FlexGrid4.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1FlexGrid4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1FlexGrid4.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1FlexGrid4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1FlexGrid4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.C1FlexGrid4.Location = New System.Drawing.Point(3, 3)
        Me.C1FlexGrid4.Name = "C1FlexGrid4"
        Me.C1FlexGrid4.Rows.Count = 0
        Me.C1FlexGrid4.Rows.DefaultSize = 19
        Me.C1FlexGrid4.Rows.Fixed = 0
        Me.C1FlexGrid4.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1FlexGrid4.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1FlexGrid4.Size = New System.Drawing.Size(864, 473)
        Me.C1FlexGrid4.StyleInfo = resources.GetString("C1FlexGrid4.StyleInfo")
        Me.C1FlexGrid4.TabIndex = 0
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.C1FlexGrid5)
        Me.TabPage5.Location = New System.Drawing.Point(4, 23)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(870, 479)
        Me.TabPage5.TabIndex = 1
        Me.TabPage5.Text = "Physical Examination"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'C1FlexGrid5
        '
        Me.C1FlexGrid5.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1FlexGrid5.AllowEditing = False
        Me.C1FlexGrid5.BackColor = System.Drawing.Color.White
        Me.C1FlexGrid5.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
        Me.C1FlexGrid5.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1FlexGrid5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1FlexGrid5.ExtendLastCol = True
        Me.C1FlexGrid5.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1FlexGrid5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1FlexGrid5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.C1FlexGrid5.Location = New System.Drawing.Point(3, 3)
        Me.C1FlexGrid5.Name = "C1FlexGrid5"
        Me.C1FlexGrid5.Rows.Count = 0
        Me.C1FlexGrid5.Rows.DefaultSize = 19
        Me.C1FlexGrid5.Rows.Fixed = 0
        Me.C1FlexGrid5.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1FlexGrid5.Size = New System.Drawing.Size(864, 473)
        Me.C1FlexGrid5.StyleInfo = resources.GetString("C1FlexGrid5.StyleInfo")
        Me.C1FlexGrid5.TabIndex = 1
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.C1FlexGrid6)
        Me.TabPage6.Location = New System.Drawing.Point(4, 23)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(870, 479)
        Me.TabPage6.TabIndex = 2
        Me.TabPage6.Text = "Medical Condition"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'C1FlexGrid6
        '
        Me.C1FlexGrid6.BackColor = System.Drawing.Color.White
        Me.C1FlexGrid6.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle
        Me.C1FlexGrid6.ColumnInfo = "0,0,0,0,0,95,Columns:"
        Me.C1FlexGrid6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1FlexGrid6.ExtendLastCol = True
        Me.C1FlexGrid6.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1FlexGrid6.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1FlexGrid6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.C1FlexGrid6.Location = New System.Drawing.Point(0, 0)
        Me.C1FlexGrid6.Name = "C1FlexGrid6"
        Me.C1FlexGrid6.Rows.Count = 0
        Me.C1FlexGrid6.Rows.DefaultSize = 19
        Me.C1FlexGrid6.Rows.Fixed = 0
        Me.C1FlexGrid6.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1FlexGrid6.Size = New System.Drawing.Size(870, 479)
        Me.C1FlexGrid6.StyleInfo = resources.GetString("C1FlexGrid6.StyleInfo")
        Me.C1FlexGrid6.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.tls_Liquiddata)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(723, 54)
        Me.Panel2.TabIndex = 2
        '
        'tls_Liquiddata
        '
        Me.tls_Liquiddata.BackColor = System.Drawing.Color.Transparent
        Me.tls_Liquiddata.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Toolstrip
        Me.tls_Liquiddata.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tls_Liquiddata.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tls_Liquiddata.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tls_Liquiddata.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlb_genrateEMCode, Me.tlb_Close})
        Me.tls_Liquiddata.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        Me.tls_Liquiddata.Location = New System.Drawing.Point(0, 0)
        Me.tls_Liquiddata.Name = "tls_Liquiddata"
        Me.tls_Liquiddata.Size = New System.Drawing.Size(723, 53)
        Me.tls_Liquiddata.TabIndex = 28
        Me.tls_Liquiddata.Text = "toolStrip1"
        '
        'tlb_genrateEMCode
        '
        Me.tlb_genrateEMCode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_genrateEMCode.Image = CType(resources.GetObject("tlb_genrateEMCode.Image"), System.Drawing.Image)
        Me.tlb_genrateEMCode.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_genrateEMCode.Name = "tlb_genrateEMCode"
        Me.tlb_genrateEMCode.Size = New System.Drawing.Size(101, 50)
        Me.tlb_genrateEMCode.Tag = "EMCode"
        Me.tlb_genrateEMCode.Text = "&Generate Code"
        Me.tlb_genrateEMCode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tlb_Close
        '
        Me.tlb_Close.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlb_Close.Image = CType(resources.GetObject("tlb_Close.Image"), System.Drawing.Image)
        Me.tlb_Close.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlb_Close.Name = "tlb_Close"
        Me.tlb_Close.Size = New System.Drawing.Size(43, 50)
        Me.tlb_Close.Tag = "Close"
        Me.tlb_Close.Text = "&Close"
        Me.tlb_Close.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'C1SuperTooltip1
        '
        Me.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None
        Me.C1SuperTooltip1.Font = New System.Drawing.Font("Tahoma", 8.0!)
        '
        'frmLiquidDataHitdtl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(723, 655)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmLiquidDataHitdtl"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "View EM Fields"
        Me.Panel1.ResumeLayout(False)
        Me.tb_Hitdetails.ResumeLayout(False)
        Me.tbp_Diagnosis.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.grpEMExamType.ResumeLayout(False)
        Me.grpEMExamType.PerformLayout()
        Me.grpCodeType.ResumeLayout(False)
        Me.pnlOthervisittype.ResumeLayout(False)
        Me.pnlOthervisittype.PerformLayout()
        Me.pnlfix.ResumeLayout(False)
        Me.pnlfix.PerformLayout()
        Me.grpOtherDiagnostictest.ResumeLayout(False)
        Me.grpOtherDiagnostictest.PerformLayout()
        CType(Me.numUpdownOtherDiagnosistests, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDiagnosis.ResumeLayout(False)
        Me.grpDiagnosis.PerformLayout()
        Me.tbp_History.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.tb_History.ResumeLayout(False)
        Me.tbpage_HPI.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        CType(Me.C1HPI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbPageROS.ResumeLayout(False)
        Me.Panel16.ResumeLayout(False)
        CType(Me.C1ROS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbP_PatientHistory.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        CType(Me.C1Details, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbp_Physicalexam.ResumeLayout(False)
        Me.Panel13.ResumeLayout(False)
        CType(Me.C1PhysicalExamination, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbp_Medicalcondition.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.tb_MedicalComplexity.ResumeLayout(False)
        Me.tb_Managmentoption.ResumeLayout(False)
        Me.pnlManagementoption.ResumeLayout(False)
        Me.pnlManagementoption.PerformLayout()
        CType(Me.nudMTimeSpent, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel9.ResumeLayout(False)
        Me.tb_Labs.ResumeLayout(False)
        Me.pnlLabbottom.ResumeLayout(False)
        Me.pnlLabbottom.PerformLayout()
        CType(Me.nudLOtherLabs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        CType(Me.C1Labs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tb_XrayRadio.ResumeLayout(False)
        Me.pnlRadiology.ResumeLayout(False)
        Me.pnlRadiology.PerformLayout()
        CType(Me.numupXOther, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel7.ResumeLayout(False)
        CType(Me.C1Radiology, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tb_OtherDxTests.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.nudDignosisstudies, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.tb_OthermedicalCondition.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        CType(Me.C1MedicalCondition, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel12.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.C1FlexGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.C1FlexGrid2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.C1FlexGrid3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        CType(Me.C1FlexGrid4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        CType(Me.C1FlexGrid5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage6.ResumeLayout(False)
        CType(Me.C1FlexGrid6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.tls_Liquiddata.ResumeLayout(False)
        Me.tls_Liquiddata.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents C1FlexGrid1 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents C1FlexGrid2 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents C1FlexGrid3 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents C1FlexGrid4 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents C1FlexGrid5 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents C1FlexGrid6 As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents tls_Liquiddata As gloGlobal.gloToolStripIgnoreFocus
    Private WithEvents tlb_genrateEMCode As System.Windows.Forms.ToolStripButton
    Friend WithEvents tlb_Close As System.Windows.Forms.ToolStripButton
    Friend WithEvents tb_Hitdetails As System.Windows.Forms.TabControl
    Friend WithEvents tbp_History As System.Windows.Forms.TabPage
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents tb_History As System.Windows.Forms.TabControl
    Friend WithEvents tbP_PatientHistory As System.Windows.Forms.TabPage
    Friend WithEvents C1Details As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents tbpage_HPI As System.Windows.Forms.TabPage
    Friend WithEvents C1HPI As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents tbPageROS As System.Windows.Forms.TabPage
    Friend WithEvents tbp_Physicalexam As System.Windows.Forms.TabPage
    Friend WithEvents C1PhysicalExamination As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents tbp_Medicalcondition As System.Windows.Forms.TabPage
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Private WithEvents Label47 As System.Windows.Forms.Label
    Private WithEvents Label48 As System.Windows.Forms.Label
    Private WithEvents Label49 As System.Windows.Forms.Label
    Private WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Private WithEvents Label56 As System.Windows.Forms.Label
    Private WithEvents Label57 As System.Windows.Forms.Label
    Private WithEvents Label58 As System.Windows.Forms.Label
    Private WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents tbp_Diagnosis As System.Windows.Forms.TabPage
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents grpDiagnosis As System.Windows.Forms.GroupBox
    Friend WithEvents chkDiagnosis As System.Windows.Forms.CheckBox
    Friend WithEvents cmbDignosis3 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbDignosis8 As System.Windows.Forms.ComboBox
    Friend WithEvents lblDignosis1 As System.Windows.Forms.Label
    Friend WithEvents cmbDignosis7 As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbDignosis6 As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbDignosis5 As System.Windows.Forms.ComboBox
    Friend WithEvents lblDignosis2 As System.Windows.Forms.Label
    Friend WithEvents cmbDignosis4 As System.Windows.Forms.ComboBox
    Friend WithEvents lblDignosis3 As System.Windows.Forms.Label
    Friend WithEvents lblDignosis4 As System.Windows.Forms.Label
    Friend WithEvents cmbDignosis2 As System.Windows.Forms.ComboBox
    Friend WithEvents lblDignosis5 As System.Windows.Forms.Label
    Friend WithEvents cmbDignosis1 As System.Windows.Forms.ComboBox
    Friend WithEvents lblDignosis6 As System.Windows.Forms.Label
    Friend WithEvents lblDignosis8 As System.Windows.Forms.Label
    Friend WithEvents lblDignosis7 As System.Windows.Forms.Label
    Friend WithEvents tb_MedicalComplexity As System.Windows.Forms.TabControl
    Friend WithEvents tb_OthermedicalCondition As System.Windows.Forms.TabPage
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents C1MedicalCondition As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents tb_Managmentoption As System.Windows.Forms.TabPage
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents pnlManagementoption As System.Windows.Forms.Panel
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents tb_Labs As System.Windows.Forms.TabPage
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents C1Labs As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label35 As System.Windows.Forms.Label
    Private WithEvents Label36 As System.Windows.Forms.Label
    Private WithEvents Label37 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents pnlLabbottom As System.Windows.Forms.Panel
    Private WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents tb_XrayRadio As System.Windows.Forms.TabPage
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents C1Radiology As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label31 As System.Windows.Forms.Label
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label33 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents pnlRadiology As System.Windows.Forms.Panel
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents tb_OtherDxTests As System.Windows.Forms.TabPage
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Private WithEvents Label52 As System.Windows.Forms.Label
    Private WithEvents Label53 As System.Windows.Forms.Label
    Private WithEvents Label54 As System.Windows.Forms.Label
    Private WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Private WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents grpOtherDiagnostictest As System.Windows.Forms.GroupBox
    Friend WithEvents chkODTindependentvisu As System.Windows.Forms.CheckBox
    Friend WithEvents chkOTDTDiscussperf As System.Windows.Forms.CheckBox
    Friend WithEvents numUpdownOtherDiagnosistests As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblOD As System.Windows.Forms.Label
    Friend WithEvents C1ROS As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents grpCodeType As System.Windows.Forms.GroupBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents cmbcodetype As System.Windows.Forms.ComboBox
    Friend WithEvents pnlfix As System.Windows.Forms.Panel
    Friend WithEvents lblVisitType As System.Windows.Forms.Label
    Friend WithEvents nudMTimeSpent As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMTimeSpent As System.Windows.Forms.Label
    Friend WithEvents chkMDecisionofcase As System.Windows.Forms.CheckBox
    Friend WithEvents ckkMreviewandsummary As System.Windows.Forms.CheckBox
    Friend WithEvents chkMDecisiontoobtain As System.Windows.Forms.CheckBox
    Friend WithEvents chkMDecisionnottoresuscitate As System.Windows.Forms.CheckBox
    Friend WithEvents chkMMajoremergencySurgery As System.Windows.Forms.CheckBox
    Friend WithEvents chkMMajorSurgerywrisk As System.Windows.Forms.CheckBox
    Friend WithEvents chkMMajorsurgeryworisk As System.Windows.Forms.CheckBox
    Friend WithEvents chkMMinorSurgeryWrisk As System.Windows.Forms.CheckBox
    Friend WithEvents chkMminorsurgeryWOrisk As System.Windows.Forms.CheckBox
    Friend WithEvents chkMClosefx As System.Windows.Forms.CheckBox
    Friend WithEvents chkMPhysicalOccupationaltherapy As System.Windows.Forms.CheckBox
    Friend WithEvents chkMNuclearMedicine As System.Windows.Forms.CheckBox
    Friend WithEvents chkMRespiratoryTreatment As System.Windows.Forms.CheckBox
    Friend WithEvents chkMTelemetry As System.Windows.Forms.CheckBox
    Friend WithEvents chkMHighRiskmeds As System.Windows.Forms.CheckBox
    Friend WithEvents chkMIVmedswadditives As System.Windows.Forms.CheckBox
    Friend WithEvents chkMivmeds As System.Windows.Forms.CheckBox
    Friend WithEvents chkMPerscripmeds As System.Windows.Forms.CheckBox
    Friend WithEvents chkMOTCmeds As System.Windows.Forms.CheckBox
    Friend WithEvents nudLOtherLabs As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkLIndependentVisualizationoftest As System.Windows.Forms.CheckBox
    Friend WithEvents chkLDiscussionwithperformingPhysician As System.Windows.Forms.CheckBox
    Friend WithEvents lblLOtherLabs As System.Windows.Forms.Label
    Friend WithEvents chkLDeepIncisionalBiopsy As System.Windows.Forms.CheckBox
    Friend WithEvents chkLSuperficialbiopsy As System.Windows.Forms.CheckBox
    Friend WithEvents chkLTypesandCrossmatch As System.Windows.Forms.CheckBox
    Friend WithEvents chkLPT As System.Windows.Forms.CheckBox
    Friend WithEvents chkLABGS As System.Windows.Forms.CheckBox
    Friend WithEvents chkLCardiacenzymes As System.Windows.Forms.CheckBox
    Friend WithEvents chkLChemicalProfile As System.Windows.Forms.CheckBox
    Friend WithEvents chkLETOH As System.Windows.Forms.CheckBox
    Friend WithEvents chkLElectrolytes As System.Windows.Forms.CheckBox
    Friend WithEvents chkLBun As System.Windows.Forms.CheckBox
    Friend WithEvents chkLAmylase As System.Windows.Forms.CheckBox
    Friend WithEvents chkLPregnancyTest As System.Windows.Forms.CheckBox
    Friend WithEvents chkLFlu As System.Windows.Forms.CheckBox
    Friend WithEvents chkLLCBC As System.Windows.Forms.CheckBox
    Friend WithEvents numupXOther As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblXOtherXRay As System.Windows.Forms.Label
    Friend WithEvents chkXIndepedent As System.Windows.Forms.CheckBox
    Friend WithEvents chkXperformingPhy As System.Windows.Forms.CheckBox
    Friend WithEvents chkXVascularStudieswrisk As System.Windows.Forms.CheckBox
    Friend WithEvents chkXVascularStudies As System.Windows.Forms.CheckBox
    Friend WithEvents chkXMRI As System.Windows.Forms.CheckBox
    Friend WithEvents chkXcatScan As System.Windows.Forms.CheckBox
    Friend WithEvents chkXIVP As System.Windows.Forms.CheckBox
    Friend WithEvents chkXGIGallablader As System.Windows.Forms.CheckBox
    Friend WithEvents chkXTLSpire As System.Windows.Forms.CheckBox
    Friend WithEvents chkXDiscographt As System.Windows.Forms.CheckBox
    Friend WithEvents chkXDiagosticUltrasound As System.Windows.Forms.CheckBox
    Friend WithEvents chkXCspine As System.Windows.Forms.CheckBox
    Friend WithEvents chkXHipPelvis As System.Windows.Forms.CheckBox
    Friend WithEvents chkXAbdomen As System.Windows.Forms.CheckBox
    Friend WithEvents chkXExtrimities As System.Windows.Forms.CheckBox
    Friend WithEvents chkXChest As System.Windows.Forms.CheckBox
    Friend WithEvents nudDignosisstudies As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblOtherAdditionalDiagnosticStudies As System.Windows.Forms.Label
    Friend WithEvents chkOIndependentVisualization As System.Windows.Forms.CheckBox
    Friend WithEvents chkODiscuswithPerfoming As System.Windows.Forms.CheckBox
    Friend WithEvents chkOEndoScopewRisk As System.Windows.Forms.CheckBox
    Friend WithEvents chkOEndoscopeworisk As System.Windows.Forms.CheckBox
    Friend WithEvents chkOCuldcentesis As System.Windows.Forms.CheckBox
    Friend WithEvents chkOThoracentesis As System.Windows.Forms.CheckBox
    Friend WithEvents chkOLumbarPunctor As System.Windows.Forms.CheckBox
    Friend WithEvents chkONuclearScan As System.Windows.Forms.CheckBox
    Friend WithEvents chkOPulmonary As System.Windows.Forms.CheckBox
    Friend WithEvents chkODopplerFlowStudies As System.Windows.Forms.CheckBox
    Friend WithEvents chkOVectorCardiogram As System.Windows.Forms.CheckBox
    Friend WithEvents chkOEEG As System.Windows.Forms.CheckBox
    Friend WithEvents chkOTreadmill As System.Windows.Forms.CheckBox
    Friend WithEvents chkOHolterMonitor As System.Windows.Forms.CheckBox
    Friend WithEvents chkOEKG As System.Windows.Forms.CheckBox
    Friend WithEvents C1SuperTooltip1 As C1.Win.C1SuperTooltip.C1SuperTooltip
    Friend WithEvents lblChangeEMExamType As System.Windows.Forms.Label
    Friend WithEvents cmbEMExamType As System.Windows.Forms.ComboBox
    Friend WithEvents grpEMExamType As System.Windows.Forms.GroupBox
    Friend WithEvents pnlOthervisittype As System.Windows.Forms.Panel
End Class
