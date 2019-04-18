<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPatientSummary
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPatientSummary))
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnlFill = New System.Windows.Forms.Panel
        Me.tbExamDMS = New System.Windows.Forms.TabControl
        Me.tbExams = New System.Windows.Forms.TabPage
        Me.Panel22 = New System.Windows.Forms.Panel
        Me.Label85 = New System.Windows.Forms.Label
        Me.Label86 = New System.Windows.Forms.Label
        Me.dgExams = New gloEMR.clsDataGrid
        Me.Label87 = New System.Windows.Forms.Label
        Me.Label88 = New System.Windows.Forms.Label
        Me.pnlFilterExams = New System.Windows.Forms.Panel
        Me.pnlFilterExamsBase = New System.Windows.Forms.Panel
        Me.btnsearch = New System.Windows.Forms.Button
        Me.Label84 = New System.Windows.Forms.Label
        Me.txtExamName = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Panel13 = New System.Windows.Forms.Panel
        Me.cmbExamProvider = New System.Windows.Forms.ComboBox
        Me.lblProvider = New System.Windows.Forms.Label
        Me.Panel14 = New System.Windows.Forms.Panel
        Me.cmbExamtype = New System.Windows.Forms.ComboBox
        Me.lblcmbType = New System.Windows.Forms.Label
        Me.Panel15 = New System.Windows.Forms.Panel
        Me.dtpTo = New System.Windows.Forms.DateTimePicker
        Me.lblto = New System.Windows.Forms.Label
        Me.Panel16 = New System.Windows.Forms.Panel
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker
        Me.lblFrom = New System.Windows.Forms.Label
        Me.Panel17 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.Panel21 = New System.Windows.Forms.Panel
        Me.txtRefillRequest = New System.Windows.Forms.TextBox
        Me.txtError = New System.Windows.Forms.TextBox
        Me.Panel20 = New System.Windows.Forms.Panel
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Panel12 = New System.Windows.Forms.Panel
        Me.btnbrwError = New System.Windows.Forms.Button
        Me.btnbrwRefill = New System.Windows.Forms.Button
        Me.Label80 = New System.Windows.Forms.Label
        Me.Label81 = New System.Windows.Forms.Label
        Me.Label82 = New System.Windows.Forms.Label
        Me.Label83 = New System.Windows.Forms.Label
        Me.Label78 = New System.Windows.Forms.Label
        Me.Label76 = New System.Windows.Forms.Label
        Me.Label77 = New System.Windows.Forms.Label
        Me.Label79 = New System.Windows.Forms.Label
        Me.tbDMS = New System.Windows.Forms.TabPage
        Me.pnlScannedDocs = New System.Windows.Forms.Panel
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.pnlDocument_CategorisedDocument = New System.Windows.Forms.Panel
        Me.Panel27 = New System.Windows.Forms.Panel
        Me.Label112 = New System.Windows.Forms.Label
        Me.txtPatientID = New System.Windows.Forms.TextBox
        Me.Label113 = New System.Windows.Forms.Label
        Me.c1CategorisedDocuments = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.Label114 = New System.Windows.Forms.Label
        Me.Label115 = New System.Windows.Forms.Label
        Me.pnlDocument_CategorisedDocument_MenuOption = New System.Windows.Forms.Panel
        Me.Panel24 = New System.Windows.Forms.Panel
        Me.chkCategorisedMenu_12Month = New System.Windows.Forms.CheckBox
        Me.Label99 = New System.Windows.Forms.Label
        Me.chkCategorisedMenu_Merge = New System.Windows.Forms.CheckBox
        Me.Label98 = New System.Windows.Forms.Label
        Me.Label94 = New System.Windows.Forms.Label
        Me.Label95 = New System.Windows.Forms.Label
        Me.ImgBtn_CategorisedDocument_MenuOptionOk = New System.Windows.Forms.PictureBox
        Me.Label96 = New System.Windows.Forms.Label
        Me.Label97 = New System.Windows.Forms.Label
        Me.pnlDocument_CategorisedDocument_MonthYear = New System.Windows.Forms.Panel
        Me.pnlDocument_CategorisedDocument_Month = New System.Windows.Forms.Panel
        Me.pnlDocument_CategorisedDocument_Cmd_YearRefresh = New System.Windows.Forms.Panel
        Me.ImgBtn_CategorisedDocument_YearRefresh = New System.Windows.Forms.PictureBox
        Me.txtCategorisedYear = New System.Windows.Forms.TextBox
        Me.pnlDocument_CategorisedDocument_Year = New System.Windows.Forms.Panel
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label108 = New System.Windows.Forms.Label
        Me.Label109 = New System.Windows.Forms.Label
        Me.Label110 = New System.Windows.Forms.Label
        Me.Label111 = New System.Windows.Forms.Label
        Me.pnlDocument_CategorisedDocument_HdrCmd = New System.Windows.Forms.Panel
        Me.Panel26 = New System.Windows.Forms.Panel
        Me.lblDocument_CategorisedDocument_Hdr = New System.Windows.Forms.Label
        Me.pnlDocument_CategorisedDocument_Cmd = New System.Windows.Forms.Panel
        Me.Label15 = New System.Windows.Forms.Label
        Me.pnlDocument_CategorisedDocument_Cmd_MenuOption = New System.Windows.Forms.Panel
        Me.ImgBtn_CategorisedDocument_MenuOption = New System.Windows.Forms.PictureBox
        Me.pnlDocument_CategorisedDocument_Cmd_Year = New System.Windows.Forms.Panel
        Me.ImgBtn_CategorisedDocument_Year = New System.Windows.Forms.PictureBox
        Me.pnlDocument_CategorisedDocument_Cmd_Scan = New System.Windows.Forms.Panel
        Me.ImgBtn_CategorisedDocument_Scan = New System.Windows.Forms.PictureBox
        Me.pnlDocument_CategorisedDocument_Cmd_Import = New System.Windows.Forms.Panel
        Me.ImgBtn_CategorisedDocument_Import = New System.Windows.Forms.PictureBox
        Me.pnlDocument_CategorisedDocument_Cmd_Category = New System.Windows.Forms.Panel
        Me.ImgBtn_CategorisedDocument_Category = New System.Windows.Forms.PictureBox
        Me.pnlDocument_CategorisedDocument_Cmd_Refresh = New System.Windows.Forms.Panel
        Me.ImgBtn_CategorisedDocument_Refresh = New System.Windows.Forms.PictureBox
        Me.Label104 = New System.Windows.Forms.Label
        Me.Label105 = New System.Windows.Forms.Label
        Me.Label106 = New System.Windows.Forms.Label
        Me.Label107 = New System.Windows.Forms.Label
        Me.trvscandoc = New System.Windows.Forms.TreeView
        Me.pnlScannedDocsHeader = New System.Windows.Forms.Panel
        Me.Panel25 = New System.Windows.Forms.Panel
        Me.lblScannedDocs = New System.Windows.Forms.Label
        Me.PictureBox6 = New System.Windows.Forms.PictureBox
        Me.Label102 = New System.Windows.Forms.Label
        Me.Label100 = New System.Windows.Forms.Label
        Me.Label101 = New System.Windows.Forms.Label
        Me.Label103 = New System.Windows.Forms.Label
        Me.pnlDMSSearch = New System.Windows.Forms.Panel
        Me.Panel23 = New System.Windows.Forms.Panel
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label93 = New System.Windows.Forms.Label
        Me.txtSearchCriteria = New System.Windows.Forms.TextBox
        Me.lblsearch = New System.Windows.Forms.Label
        Me.cmbSearch = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label89 = New System.Windows.Forms.Label
        Me.Label90 = New System.Windows.Forms.Label
        Me.Label91 = New System.Windows.Forms.Label
        Me.Label92 = New System.Windows.Forms.Label
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.tbSummary = New System.Windows.Forms.TabControl
        Me.tbpSummary = New System.Windows.Forms.TabPage
        Me.pnlSummary = New System.Windows.Forms.Panel
        Me.pnlImaging = New System.Windows.Forms.Panel
        Me.Panel7 = New System.Windows.Forms.Panel
        Me.trImaging = New System.Windows.Forms.TreeView
        Me.Label74 = New System.Windows.Forms.Label
        Me.Label75 = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.pnlImagingTitle = New System.Windows.Forms.Panel
        Me.Panel19 = New System.Windows.Forms.Panel
        Me.Label60 = New System.Windows.Forms.Label
        Me.Label61 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label62 = New System.Windows.Forms.Label
        Me.Label63 = New System.Windows.Forms.Label
        Me.spltLabs = New System.Windows.Forms.Splitter
        Me.pnlLabs = New System.Windows.Forms.Panel
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.trLabs = New System.Windows.Forms.TreeView
        Me.Label72 = New System.Windows.Forms.Label
        Me.Label73 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.Label35 = New System.Windows.Forms.Label
        Me.pnlLabTitle = New System.Windows.Forms.Panel
        Me.Panel18 = New System.Windows.Forms.Panel
        Me.Label56 = New System.Windows.Forms.Label
        Me.Label57 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label58 = New System.Windows.Forms.Label
        Me.Label59 = New System.Windows.Forms.Label
        Me.spltProcedures = New System.Windows.Forms.Splitter
        Me.pnlProcedures = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.trProcedures = New System.Windows.Forms.TreeView
        Me.Label70 = New System.Windows.Forms.Label
        Me.Label71 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label31 = New System.Windows.Forms.Label
        Me.pnlprocedureTitle = New System.Windows.Forms.Panel
        Me.Panel11 = New System.Windows.Forms.Panel
        Me.Label52 = New System.Windows.Forms.Label
        Me.Label53 = New System.Windows.Forms.Label
        Me.Label54 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label55 = New System.Windows.Forms.Label
        Me.spltHistory = New System.Windows.Forms.Splitter
        Me.pnlHistory = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.trHistory = New System.Windows.Forms.TreeView
        Me.Label68 = New System.Windows.Forms.Label
        Me.Label69 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.pnlHistoryTitle = New System.Windows.Forms.Panel
        Me.Panel10 = New System.Windows.Forms.Panel
        Me.Label48 = New System.Windows.Forms.Label
        Me.Label49 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label50 = New System.Windows.Forms.Label
        Me.Label51 = New System.Windows.Forms.Label
        Me.spltMedications = New System.Windows.Forms.Splitter
        Me.pnlMedications = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.trMedications = New System.Windows.Forms.TreeView
        Me.Label66 = New System.Windows.Forms.Label
        Me.Label67 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.pnlMedicationTitle = New System.Windows.Forms.Panel
        Me.Panel9 = New System.Windows.Forms.Panel
        Me.Label47 = New System.Windows.Forms.Label
        Me.Label44 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.Label46 = New System.Windows.Forms.Label
        Me.spltProblems = New System.Windows.Forms.Splitter
        Me.pnlProblems = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.trProblemList = New System.Windows.Forms.TreeView
        Me.Label65 = New System.Windows.Forms.Label
        Me.Label64 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.pnlProblemsTitle = New System.Windows.Forms.Panel
        Me.Panel8 = New System.Windows.Forms.Panel
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label41 = New System.Windows.Forms.Label
        Me.Label42 = New System.Windows.Forms.Label
        Me.lblProblems = New System.Windows.Forms.Label
        Me.Label43 = New System.Windows.Forms.Label
        Me.tbpProblem = New System.Windows.Forms.TabPage
        Me.Panel28 = New System.Windows.Forms.Panel
        Me.Label119 = New System.Windows.Forms.Label
        Me.Label116 = New System.Windows.Forms.Label
        Me.Label117 = New System.Windows.Forms.Label
        Me.Label118 = New System.Windows.Forms.Label
        Me.c1ProblemList = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.pnlSearchproblemList = New System.Windows.Forms.Panel
        Me.Panel32 = New System.Windows.Forms.Panel
        Me.Label128 = New System.Windows.Forms.Label
        Me.Label129 = New System.Windows.Forms.Label
        Me.Label130 = New System.Windows.Forms.Label
        Me.Label131 = New System.Windows.Forms.Label
        Me.tbpMedications = New System.Windows.Forms.TabPage
        Me.pnldgPatientDetails = New System.Windows.Forms.Panel
        Me.Label144 = New System.Windows.Forms.Label
        Me.Label145 = New System.Windows.Forms.Label
        Me.dgPatientDetails = New gloEMR.clsDataGrid
        Me.Label146 = New System.Windows.Forms.Label
        Me.Label147 = New System.Windows.Forms.Label
        Me.pnlSearchMedications = New System.Windows.Forms.Panel
        Me.Panel37 = New System.Windows.Forms.Panel
        Me.Label140 = New System.Windows.Forms.Label
        Me.Label141 = New System.Windows.Forms.Label
        Me.Label142 = New System.Windows.Forms.Label
        Me.Label143 = New System.Windows.Forms.Label
        Me.tbpAllergies = New System.Windows.Forms.TabPage
        Me.pnlC1dgPatientDetails = New System.Windows.Forms.Panel
        Me.Label120 = New System.Windows.Forms.Label
        Me.Label121 = New System.Windows.Forms.Label
        Me.Label122 = New System.Windows.Forms.Label
        Me.Label123 = New System.Windows.Forms.Label
        Me.C1dgPatientDetails = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.pnlSearchAllergies = New System.Windows.Forms.Panel
        Me.Panel35 = New System.Windows.Forms.Panel
        Me.Label136 = New System.Windows.Forms.Label
        Me.Label137 = New System.Windows.Forms.Label
        Me.Label138 = New System.Windows.Forms.Label
        Me.Label139 = New System.Windows.Forms.Label
        Me.tbProcedures = New System.Windows.Forms.TabPage
        Me.pnltrProcedureDetails = New System.Windows.Forms.Panel
        Me.trProcedureDetails = New System.Windows.Forms.TreeView
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label124 = New System.Windows.Forms.Label
        Me.Label126 = New System.Windows.Forms.Label
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label
        Me.Label127 = New System.Windows.Forms.Label
        Me.Label125 = New System.Windows.Forms.Label
        Me.pnlSearchProcedures = New System.Windows.Forms.Panel
        Me.Panel33 = New System.Windows.Forms.Panel
        Me.Label132 = New System.Windows.Forms.Label
        Me.Label133 = New System.Windows.Forms.Label
        Me.Label134 = New System.Windows.Forms.Label
        Me.Label135 = New System.Windows.Forms.Label
        Me.tbpLabs = New System.Windows.Forms.TabPage
        Me.GloUC_TransactionHistory1 = New gloUserControlLibrary.gloUC_TransactionHistory
        Me.tbpImaging = New System.Windows.Forms.TabPage
        Me.C1OrderDetails = New C1.Win.C1FlexGrid.C1FlexGrid
        Me.dgImaging = New gloEMR.clsDataGrid
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.pnlPatientStrip = New System.Windows.Forms.Panel
        Me.imgTreeView = New System.Windows.Forms.ImageList(Me.components)
        Me.pnl_tlsp_Top = New System.Windows.Forms.Panel
        Me.tstrip = New System.Windows.Forms.ToolStrip
        Me.tlsbtnClose = New System.Windows.Forms.ToolStripButton
        Me.pnlMain.SuspendLayout()
        Me.pnlFill.SuspendLayout()
        Me.tbExamDMS.SuspendLayout()
        Me.tbExams.SuspendLayout()
        Me.Panel22.SuspendLayout()
        CType(Me.dgExams, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFilterExams.SuspendLayout()
        Me.pnlFilterExamsBase.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.Panel17.SuspendLayout()
        Me.Panel21.SuspendLayout()
        Me.Panel20.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel12.SuspendLayout()
        Me.tbDMS.SuspendLayout()
        Me.pnlScannedDocs.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.pnlDocument_CategorisedDocument.SuspendLayout()
        Me.Panel27.SuspendLayout()
        CType(Me.c1CategorisedDocuments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDocument_CategorisedDocument_MenuOption.SuspendLayout()
        Me.Panel24.SuspendLayout()
        CType(Me.ImgBtn_CategorisedDocument_MenuOptionOk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDocument_CategorisedDocument_MonthYear.SuspendLayout()
        Me.pnlDocument_CategorisedDocument_Month.SuspendLayout()
        Me.pnlDocument_CategorisedDocument_Cmd_YearRefresh.SuspendLayout()
        CType(Me.ImgBtn_CategorisedDocument_YearRefresh, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDocument_CategorisedDocument_Year.SuspendLayout()
        Me.pnlDocument_CategorisedDocument_HdrCmd.SuspendLayout()
        Me.Panel26.SuspendLayout()
        Me.pnlDocument_CategorisedDocument_Cmd.SuspendLayout()
        Me.pnlDocument_CategorisedDocument_Cmd_MenuOption.SuspendLayout()
        CType(Me.ImgBtn_CategorisedDocument_MenuOption, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDocument_CategorisedDocument_Cmd_Year.SuspendLayout()
        CType(Me.ImgBtn_CategorisedDocument_Year, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDocument_CategorisedDocument_Cmd_Scan.SuspendLayout()
        CType(Me.ImgBtn_CategorisedDocument_Scan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDocument_CategorisedDocument_Cmd_Import.SuspendLayout()
        CType(Me.ImgBtn_CategorisedDocument_Import, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDocument_CategorisedDocument_Cmd_Category.SuspendLayout()
        CType(Me.ImgBtn_CategorisedDocument_Category, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDocument_CategorisedDocument_Cmd_Refresh.SuspendLayout()
        CType(Me.ImgBtn_CategorisedDocument_Refresh, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlScannedDocsHeader.SuspendLayout()
        Me.Panel25.SuspendLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDMSSearch.SuspendLayout()
        Me.Panel23.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.tbSummary.SuspendLayout()
        Me.tbpSummary.SuspendLayout()
        Me.pnlSummary.SuspendLayout()
        Me.pnlImaging.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.pnlImagingTitle.SuspendLayout()
        Me.Panel19.SuspendLayout()
        Me.pnlLabs.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.pnlLabTitle.SuspendLayout()
        Me.Panel18.SuspendLayout()
        Me.pnlProcedures.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlprocedureTitle.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.pnlHistory.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlHistoryTitle.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.pnlMedications.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlMedicationTitle.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.pnlProblems.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlProblemsTitle.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.tbpProblem.SuspendLayout()
        Me.Panel28.SuspendLayout()
        CType(Me.c1ProblemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearchproblemList.SuspendLayout()
        Me.Panel32.SuspendLayout()
        Me.tbpMedications.SuspendLayout()
        Me.pnldgPatientDetails.SuspendLayout()
        CType(Me.dgPatientDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearchMedications.SuspendLayout()
        Me.Panel37.SuspendLayout()
        Me.tbpAllergies.SuspendLayout()
        Me.pnlC1dgPatientDetails.SuspendLayout()
        CType(Me.C1dgPatientDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearchAllergies.SuspendLayout()
        Me.Panel35.SuspendLayout()
        Me.tbProcedures.SuspendLayout()
        Me.pnltrProcedureDetails.SuspendLayout()
        Me.pnlSearchProcedures.SuspendLayout()
        Me.Panel33.SuspendLayout()
        Me.tbpLabs.SuspendLayout()
        Me.tbpImaging.SuspendLayout()
        CType(Me.C1OrderDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgImaging, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnl_tlsp_Top.SuspendLayout()
        Me.tstrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlFill)
        Me.pnlMain.Controls.Add(Me.pnlTop)
        Me.pnlMain.Controls.Add(Me.pnlPatientStrip)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 55)
        Me.pnlMain.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlMain.Size = New System.Drawing.Size(1096, 785)
        Me.pnlMain.TabIndex = 0
        '
        'pnlFill
        '
        Me.pnlFill.Controls.Add(Me.tbExamDMS)
        Me.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFill.Location = New System.Drawing.Point(0, 403)
        Me.pnlFill.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlFill.Name = "pnlFill"
        Me.pnlFill.Size = New System.Drawing.Size(1096, 382)
        Me.pnlFill.TabIndex = 5
        '
        'tbExamDMS
        '
        Me.tbExamDMS.Controls.Add(Me.tbExams)
        Me.tbExamDMS.Controls.Add(Me.tbDMS)
        Me.tbExamDMS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbExamDMS.ImageList = Me.ImageList1
        Me.tbExamDMS.ItemSize = New System.Drawing.Size(68, 22)
        Me.tbExamDMS.Location = New System.Drawing.Point(0, 0)
        Me.tbExamDMS.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tbExamDMS.Name = "tbExamDMS"
        Me.tbExamDMS.SelectedIndex = 0
        Me.tbExamDMS.Size = New System.Drawing.Size(1096, 382)
        Me.tbExamDMS.TabIndex = 1
        '
        'tbExams
        '
        Me.tbExams.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbExams.Controls.Add(Me.Panel22)
        Me.tbExams.Controls.Add(Me.pnlFilterExams)
        Me.tbExams.ImageIndex = 10
        Me.tbExams.Location = New System.Drawing.Point(4, 26)
        Me.tbExams.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tbExams.Name = "tbExams"
        Me.tbExams.Size = New System.Drawing.Size(1088, 352)
        Me.tbExams.TabIndex = 0
        Me.tbExams.Text = "Exams"
        '
        'Panel22
        '
        Me.Panel22.Controls.Add(Me.Label85)
        Me.Panel22.Controls.Add(Me.Label86)
        Me.Panel22.Controls.Add(Me.dgExams)
        Me.Panel22.Controls.Add(Me.Label87)
        Me.Panel22.Controls.Add(Me.Label88)
        Me.Panel22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel22.Location = New System.Drawing.Point(0, 30)
        Me.Panel22.Name = "Panel22"
        Me.Panel22.Size = New System.Drawing.Size(1088, 322)
        Me.Panel22.TabIndex = 25
        '
        'Label85
        '
        Me.Label85.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label85.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label85.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label85.Location = New System.Drawing.Point(1, 321)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(1086, 1)
        Me.Label85.TabIndex = 12
        Me.Label85.Text = "label2"
        '
        'Label86
        '
        Me.Label86.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label86.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label86.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label86.Location = New System.Drawing.Point(0, 1)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(1, 321)
        Me.Label86.TabIndex = 11
        Me.Label86.Text = "label4"
        '
        'dgExams
        '
        Me.dgExams.BackColor = System.Drawing.Color.Black
        Me.dgExams.BackgroundColor = System.Drawing.Color.White
        Me.dgExams.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgExams.CaptionBackColor = System.Drawing.Color.Black
        Me.dgExams.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgExams.CaptionForeColor = System.Drawing.SystemColors.ControlDark
        Me.dgExams.CaptionVisible = False
        Me.dgExams.DataMember = ""
        Me.dgExams.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgExams.FlatMode = True
        Me.dgExams.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgExams.FullRowSelect = True
        Me.dgExams.GridLineColor = System.Drawing.SystemColors.ControlText
        Me.dgExams.HeaderBackColor = System.Drawing.SystemColors.ControlDark
        Me.dgExams.HeaderFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgExams.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgExams.Location = New System.Drawing.Point(0, 1)
        Me.dgExams.Name = "dgExams"
        Me.dgExams.ParentRowsBackColor = System.Drawing.SystemColors.ControlText
        Me.dgExams.ReadOnly = True
        Me.dgExams.SelectionBackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.dgExams.Size = New System.Drawing.Size(1087, 321)
        Me.dgExams.TabIndex = 22
        '
        'Label87
        '
        Me.Label87.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label87.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label87.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label87.Location = New System.Drawing.Point(1087, 1)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(1, 321)
        Me.Label87.TabIndex = 10
        Me.Label87.Text = "label3"
        '
        'Label88
        '
        Me.Label88.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label88.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label88.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label88.Location = New System.Drawing.Point(0, 0)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(1088, 1)
        Me.Label88.TabIndex = 9
        Me.Label88.Text = "label1"
        '
        'pnlFilterExams
        '
        Me.pnlFilterExams.Controls.Add(Me.pnlFilterExamsBase)
        Me.pnlFilterExams.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilterExams.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilterExams.Name = "pnlFilterExams"
        Me.pnlFilterExams.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlFilterExams.Size = New System.Drawing.Size(1088, 30)
        Me.pnlFilterExams.TabIndex = 24
        '
        'pnlFilterExamsBase
        '
        Me.pnlFilterExamsBase.BackColor = System.Drawing.Color.Transparent
        Me.pnlFilterExamsBase.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlFilterExamsBase.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlFilterExamsBase.Controls.Add(Me.btnsearch)
        Me.pnlFilterExamsBase.Controls.Add(Me.Label84)
        Me.pnlFilterExamsBase.Controls.Add(Me.txtExamName)
        Me.pnlFilterExamsBase.Controls.Add(Me.Label13)
        Me.pnlFilterExamsBase.Controls.Add(Me.Panel13)
        Me.pnlFilterExamsBase.Controls.Add(Me.lblProvider)
        Me.pnlFilterExamsBase.Controls.Add(Me.Panel14)
        Me.pnlFilterExamsBase.Controls.Add(Me.lblcmbType)
        Me.pnlFilterExamsBase.Controls.Add(Me.Panel15)
        Me.pnlFilterExamsBase.Controls.Add(Me.lblto)
        Me.pnlFilterExamsBase.Controls.Add(Me.Panel16)
        Me.pnlFilterExamsBase.Controls.Add(Me.lblFrom)
        Me.pnlFilterExamsBase.Controls.Add(Me.Panel17)
        Me.pnlFilterExamsBase.Controls.Add(Me.Label78)
        Me.pnlFilterExamsBase.Controls.Add(Me.Label76)
        Me.pnlFilterExamsBase.Controls.Add(Me.Label77)
        Me.pnlFilterExamsBase.Controls.Add(Me.Label79)
        Me.pnlFilterExamsBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFilterExamsBase.Location = New System.Drawing.Point(0, 0)
        Me.pnlFilterExamsBase.Name = "pnlFilterExamsBase"
        Me.pnlFilterExamsBase.Size = New System.Drawing.Size(1088, 27)
        Me.pnlFilterExamsBase.TabIndex = 21
        '
        'btnsearch
        '
        Me.btnsearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.btnsearch.FlatAppearance.BorderSize = 0
        Me.btnsearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnsearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsearch.Image = CType(resources.GetObject("btnsearch.Image"), System.Drawing.Image)
        Me.btnsearch.Location = New System.Drawing.Point(980, 1)
        Me.btnsearch.Name = "btnsearch"
        Me.btnsearch.Size = New System.Drawing.Size(24, 25)
        Me.btnsearch.TabIndex = 16
        Me.btnsearch.UseVisualStyleBackColor = True
        '
        'Label84
        '
        Me.Label84.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label84.Location = New System.Drawing.Point(970, 1)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(10, 25)
        Me.Label84.TabIndex = 17
        Me.Label84.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtExamName
        '
        Me.txtExamName.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtExamName.Location = New System.Drawing.Point(823, 1)
        Me.txtExamName.Name = "txtExamName"
        Me.txtExamName.Size = New System.Drawing.Size(147, 22)
        Me.txtExamName.TabIndex = 15
        '
        'Label13
        '
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label13.Location = New System.Drawing.Point(731, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(92, 25)
        Me.Label13.TabIndex = 14
        Me.Label13.Text = "  Exam Name :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.cmbExamProvider)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel13.Location = New System.Drawing.Point(595, 1)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(136, 25)
        Me.Panel13.TabIndex = 13
        '
        'cmbExamProvider
        '
        Me.cmbExamProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbExamProvider.ForeColor = System.Drawing.Color.Black
        Me.cmbExamProvider.FormattingEnabled = True
        Me.cmbExamProvider.Location = New System.Drawing.Point(4, 1)
        Me.cmbExamProvider.Name = "cmbExamProvider"
        Me.cmbExamProvider.Size = New System.Drawing.Size(132, 22)
        Me.cmbExamProvider.TabIndex = 8
        '
        'lblProvider
        '
        Me.lblProvider.BackColor = System.Drawing.Color.Transparent
        Me.lblProvider.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblProvider.Location = New System.Drawing.Point(521, 1)
        Me.lblProvider.Name = "lblProvider"
        Me.lblProvider.Size = New System.Drawing.Size(74, 25)
        Me.lblProvider.TabIndex = 7
        Me.lblProvider.Text = "  Provider :"
        Me.lblProvider.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.cmbExamtype)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel14.Location = New System.Drawing.Point(425, 1)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(96, 25)
        Me.Panel14.TabIndex = 12
        '
        'cmbExamtype
        '
        Me.cmbExamtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbExamtype.ForeColor = System.Drawing.Color.Black
        Me.cmbExamtype.FormattingEnabled = True
        Me.cmbExamtype.Location = New System.Drawing.Point(3, 1)
        Me.cmbExamtype.Name = "cmbExamtype"
        Me.cmbExamtype.Size = New System.Drawing.Size(88, 22)
        Me.cmbExamtype.TabIndex = 1
        '
        'lblcmbType
        '
        Me.lblcmbType.BackColor = System.Drawing.Color.Transparent
        Me.lblcmbType.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblcmbType.Location = New System.Drawing.Point(360, 1)
        Me.lblcmbType.Name = "lblcmbType"
        Me.lblcmbType.Size = New System.Drawing.Size(65, 25)
        Me.lblcmbType.TabIndex = 6
        Me.lblcmbType.Text = "  Status :"
        Me.lblcmbType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel15
        '
        Me.Panel15.Controls.Add(Me.dtpTo)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel15.Location = New System.Drawing.Point(263, 1)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(97, 25)
        Me.Panel15.TabIndex = 10
        '
        'dtpTo
        '
        Me.dtpTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpTo.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(3, 1)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(88, 22)
        Me.dtpTo.TabIndex = 5
        '
        'lblto
        '
        Me.lblto.BackColor = System.Drawing.Color.Transparent
        Me.lblto.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblto.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblto.Location = New System.Drawing.Point(223, 1)
        Me.lblto.Name = "lblto"
        Me.lblto.Size = New System.Drawing.Size(40, 25)
        Me.lblto.TabIndex = 2
        Me.lblto.Text = "  To :"
        Me.lblto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel16
        '
        Me.Panel16.Controls.Add(Me.dtpFrom)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel16.Location = New System.Drawing.Point(127, 1)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(96, 25)
        Me.Panel16.TabIndex = 11
        '
        'dtpFrom
        '
        Me.dtpFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(3, 1)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(88, 22)
        Me.dtpFrom.TabIndex = 3
        '
        'lblFrom
        '
        Me.lblFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblFrom.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblFrom.Location = New System.Drawing.Point(74, 1)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(53, 25)
        Me.lblFrom.TabIndex = 4
        Me.lblFrom.Text = "  From :"
        Me.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel17
        '
        Me.Panel17.Controls.Add(Me.Label30)
        Me.Panel17.Controls.Add(Me.Panel21)
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel17.Location = New System.Drawing.Point(1, 1)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(73, 25)
        Me.Panel17.TabIndex = 9
        '
        'Label30
        '
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label30.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label30.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(0, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(73, 25)
        Me.Label30.TabIndex = 0
        Me.Label30.Text = "Search"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel21
        '
        Me.Panel21.Controls.Add(Me.txtRefillRequest)
        Me.Panel21.Controls.Add(Me.txtError)
        Me.Panel21.Controls.Add(Me.Panel20)
        Me.Panel21.Controls.Add(Me.Panel12)
        Me.Panel21.Controls.Add(Me.Label80)
        Me.Panel21.Controls.Add(Me.Label81)
        Me.Panel21.Controls.Add(Me.Label82)
        Me.Panel21.Controls.Add(Me.Label83)
        Me.Panel21.Location = New System.Drawing.Point(13, 2)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Size = New System.Drawing.Size(365, 148)
        Me.Panel21.TabIndex = 25
        Me.Panel21.Visible = False
        '
        'txtRefillRequest
        '
        Me.txtRefillRequest.BackColor = System.Drawing.Color.White
        Me.txtRefillRequest.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRefillRequest.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtRefillRequest.ForeColor = System.Drawing.Color.Black
        Me.txtRefillRequest.Location = New System.Drawing.Point(21, 1)
        Me.txtRefillRequest.Multiline = True
        Me.txtRefillRequest.Name = "txtRefillRequest"
        Me.txtRefillRequest.ReadOnly = True
        Me.txtRefillRequest.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRefillRequest.Size = New System.Drawing.Size(319, 58)
        Me.txtRefillRequest.TabIndex = 14
        Me.txtRefillRequest.TabStop = False
        '
        'txtError
        '
        Me.txtError.BackColor = System.Drawing.Color.White
        Me.txtError.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtError.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtError.ForeColor = System.Drawing.Color.Black
        Me.txtError.Location = New System.Drawing.Point(21, 81)
        Me.txtError.Multiline = True
        Me.txtError.Name = "txtError"
        Me.txtError.ReadOnly = True
        Me.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtError.Size = New System.Drawing.Size(319, 66)
        Me.txtError.TabIndex = 11
        Me.txtError.TabStop = False
        '
        'Panel20
        '
        Me.Panel20.Controls.Add(Me.PictureBox2)
        Me.Panel20.Controls.Add(Me.PictureBox1)
        Me.Panel20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel20.Location = New System.Drawing.Point(1, 1)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Size = New System.Drawing.Size(20, 146)
        Me.Panel20.TabIndex = 13
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(3, 58)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox2.TabIndex = 1
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(15, 17)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.btnbrwError)
        Me.Panel12.Controls.Add(Me.btnbrwRefill)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel12.Location = New System.Drawing.Point(340, 1)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(24, 146)
        Me.Panel12.TabIndex = 12
        '
        'btnbrwError
        '
        Me.btnbrwError.BackgroundImage = CType(resources.GetObject("btnbrwError.BackgroundImage"), System.Drawing.Image)
        Me.btnbrwError.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnbrwError.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnbrwError.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnbrwError.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnbrwError.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnbrwError.Image = CType(resources.GetObject("btnbrwError.Image"), System.Drawing.Image)
        Me.btnbrwError.Location = New System.Drawing.Point(1, 57)
        Me.btnbrwError.Name = "btnbrwError"
        Me.btnbrwError.Size = New System.Drawing.Size(26, 24)
        Me.btnbrwError.TabIndex = 0
        Me.btnbrwError.UseVisualStyleBackColor = True
        '
        'btnbrwRefill
        '
        Me.btnbrwRefill.BackgroundImage = CType(resources.GetObject("btnbrwRefill.BackgroundImage"), System.Drawing.Image)
        Me.btnbrwRefill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnbrwRefill.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnbrwRefill.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnbrwRefill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnbrwRefill.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnbrwRefill.Image = CType(resources.GetObject("btnbrwRefill.Image"), System.Drawing.Image)
        Me.btnbrwRefill.Location = New System.Drawing.Point(1, 0)
        Me.btnbrwRefill.Name = "btnbrwRefill"
        Me.btnbrwRefill.Size = New System.Drawing.Size(26, 24)
        Me.btnbrwRefill.TabIndex = 0
        Me.btnbrwRefill.UseVisualStyleBackColor = True
        '
        'Label80
        '
        Me.Label80.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label80.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label80.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label80.Location = New System.Drawing.Point(1, 147)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(363, 1)
        Me.Label80.TabIndex = 18
        Me.Label80.Text = "label2"
        '
        'Label81
        '
        Me.Label81.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label81.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label81.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label81.Location = New System.Drawing.Point(0, 1)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(1, 147)
        Me.Label81.TabIndex = 17
        Me.Label81.Text = "label4"
        '
        'Label82
        '
        Me.Label82.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label82.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label82.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label82.Location = New System.Drawing.Point(364, 1)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(1, 147)
        Me.Label82.TabIndex = 16
        Me.Label82.Text = "label3"
        '
        'Label83
        '
        Me.Label83.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label83.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label83.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label83.Location = New System.Drawing.Point(0, 0)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(365, 1)
        Me.Label83.TabIndex = 15
        Me.Label83.Text = "label1"
        '
        'Label78
        '
        Me.Label78.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label78.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label78.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label78.Location = New System.Drawing.Point(1087, 1)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(1, 25)
        Me.Label78.TabIndex = 6
        Me.Label78.Text = "label3"
        '
        'Label76
        '
        Me.Label76.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label76.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label76.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label76.Location = New System.Drawing.Point(1, 26)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(1087, 1)
        Me.Label76.TabIndex = 8
        Me.Label76.Text = "label2"
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label77.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label77.Location = New System.Drawing.Point(0, 1)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(1, 26)
        Me.Label77.TabIndex = 7
        Me.Label77.Text = "label4"
        '
        'Label79
        '
        Me.Label79.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label79.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label79.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label79.Location = New System.Drawing.Point(0, 0)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(1088, 1)
        Me.Label79.TabIndex = 5
        Me.Label79.Text = "label1"
        '
        'tbDMS
        '
        Me.tbDMS.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbDMS.Controls.Add(Me.pnlScannedDocs)
        Me.tbDMS.Controls.Add(Me.pnlDMSSearch)
        Me.tbDMS.ImageIndex = 11
        Me.tbDMS.Location = New System.Drawing.Point(4, 26)
        Me.tbDMS.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tbDMS.Name = "tbDMS"
        Me.tbDMS.Size = New System.Drawing.Size(1088, 352)
        Me.tbDMS.TabIndex = 1
        Me.tbDMS.Text = "DMS"
        '
        'pnlScannedDocs
        '
        Me.pnlScannedDocs.BackColor = System.Drawing.Color.Transparent
        Me.pnlScannedDocs.Controls.Add(Me.Panel6)
        Me.pnlScannedDocs.Controls.Add(Me.pnlScannedDocsHeader)
        Me.pnlScannedDocs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlScannedDocs.Location = New System.Drawing.Point(0, 28)
        Me.pnlScannedDocs.Name = "pnlScannedDocs"
        Me.pnlScannedDocs.Size = New System.Drawing.Size(1088, 324)
        Me.pnlScannedDocs.TabIndex = 20
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Transparent
        Me.Panel6.Controls.Add(Me.pnlDocument_CategorisedDocument)
        Me.Panel6.Controls.Add(Me.trvscandoc)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(0, 27)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1088, 297)
        Me.Panel6.TabIndex = 9
        '
        'pnlDocument_CategorisedDocument
        '
        Me.pnlDocument_CategorisedDocument.BackColor = System.Drawing.Color.Transparent
        Me.pnlDocument_CategorisedDocument.Controls.Add(Me.Panel27)
        Me.pnlDocument_CategorisedDocument.Controls.Add(Me.pnlDocument_CategorisedDocument_MenuOption)
        Me.pnlDocument_CategorisedDocument.Controls.Add(Me.pnlDocument_CategorisedDocument_MonthYear)
        Me.pnlDocument_CategorisedDocument.Controls.Add(Me.pnlDocument_CategorisedDocument_HdrCmd)
        Me.pnlDocument_CategorisedDocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDocument_CategorisedDocument.Location = New System.Drawing.Point(0, 0)
        Me.pnlDocument_CategorisedDocument.Name = "pnlDocument_CategorisedDocument"
        Me.pnlDocument_CategorisedDocument.Size = New System.Drawing.Size(1088, 297)
        Me.pnlDocument_CategorisedDocument.TabIndex = 25
        '
        'Panel27
        '
        Me.Panel27.Controls.Add(Me.Label112)
        Me.Panel27.Controls.Add(Me.txtPatientID)
        Me.Panel27.Controls.Add(Me.Label113)
        Me.Panel27.Controls.Add(Me.c1CategorisedDocuments)
        Me.Panel27.Controls.Add(Me.Label114)
        Me.Panel27.Controls.Add(Me.Label115)
        Me.Panel27.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel27.Location = New System.Drawing.Point(0, 82)
        Me.Panel27.Name = "Panel27"
        Me.Panel27.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.Panel27.Size = New System.Drawing.Size(1088, 215)
        Me.Panel27.TabIndex = 25
        '
        'Label112
        '
        Me.Label112.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label112.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label112.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label112.Location = New System.Drawing.Point(4, 211)
        Me.Label112.Name = "Label112"
        Me.Label112.Size = New System.Drawing.Size(1080, 1)
        Me.Label112.TabIndex = 12
        Me.Label112.Text = "label2"
        '
        'txtPatientID
        '
        Me.txtPatientID.ForeColor = System.Drawing.Color.Black
        Me.txtPatientID.Location = New System.Drawing.Point(82, 53)
        Me.txtPatientID.Name = "txtPatientID"
        Me.txtPatientID.Size = New System.Drawing.Size(36, 22)
        Me.txtPatientID.TabIndex = 2
        Me.txtPatientID.Visible = False
        '
        'Label113
        '
        Me.Label113.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label113.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label113.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label113.Location = New System.Drawing.Point(3, 1)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(1, 211)
        Me.Label113.TabIndex = 11
        Me.Label113.Text = "label4"
        '
        'c1CategorisedDocuments
        '
        Me.c1CategorisedDocuments.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1CategorisedDocuments.AllowEditing = False
        Me.c1CategorisedDocuments.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1CategorisedDocuments.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1CategorisedDocuments.ColumnInfo = "2,0,0,0,0,95,Columns:0{Width:209;}" & Global.Microsoft.VisualBasic.ChrW(9) & "1{Width:15;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.c1CategorisedDocuments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1CategorisedDocuments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1CategorisedDocuments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1CategorisedDocuments.Location = New System.Drawing.Point(3, 1)
        Me.c1CategorisedDocuments.Name = "c1CategorisedDocuments"
        Me.c1CategorisedDocuments.Rows.DefaultSize = 19
        Me.c1CategorisedDocuments.Rows.Fixed = 0
        Me.c1CategorisedDocuments.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1CategorisedDocuments.Size = New System.Drawing.Size(1081, 211)
        Me.c1CategorisedDocuments.StyleInfo = resources.GetString("c1CategorisedDocuments.StyleInfo")
        Me.c1CategorisedDocuments.TabIndex = 24
        Me.c1CategorisedDocuments.Tree.Column = 2
        Me.c1CategorisedDocuments.Tree.Indent = 72
        Me.c1CategorisedDocuments.Tree.LineStyle = System.Drawing.Drawing2D.DashStyle.Custom
        Me.c1CategorisedDocuments.Tree.NodeImageCollapsed = CType(resources.GetObject("c1CategorisedDocuments.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.c1CategorisedDocuments.Tree.NodeImageExpanded = CType(resources.GetObject("c1CategorisedDocuments.Tree.NodeImageExpanded"), System.Drawing.Image)
        Me.c1CategorisedDocuments.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.None
        '
        'Label114
        '
        Me.Label114.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label114.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label114.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label114.Location = New System.Drawing.Point(1084, 1)
        Me.Label114.Name = "Label114"
        Me.Label114.Size = New System.Drawing.Size(1, 211)
        Me.Label114.TabIndex = 10
        Me.Label114.Text = "label3"
        '
        'Label115
        '
        Me.Label115.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label115.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label115.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label115.Location = New System.Drawing.Point(3, 0)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(1082, 1)
        Me.Label115.TabIndex = 9
        Me.Label115.Text = "label1"
        '
        'pnlDocument_CategorisedDocument_MenuOption
        '
        Me.pnlDocument_CategorisedDocument_MenuOption.BackColor = System.Drawing.Color.Transparent
        Me.pnlDocument_CategorisedDocument_MenuOption.Controls.Add(Me.Panel24)
        Me.pnlDocument_CategorisedDocument_MenuOption.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDocument_CategorisedDocument_MenuOption.Location = New System.Drawing.Point(0, 54)
        Me.pnlDocument_CategorisedDocument_MenuOption.Name = "pnlDocument_CategorisedDocument_MenuOption"
        Me.pnlDocument_CategorisedDocument_MenuOption.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlDocument_CategorisedDocument_MenuOption.Size = New System.Drawing.Size(1088, 28)
        Me.pnlDocument_CategorisedDocument_MenuOption.TabIndex = 22
        Me.pnlDocument_CategorisedDocument_MenuOption.Visible = False
        '
        'Panel24
        '
        Me.Panel24.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel24.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel24.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel24.Controls.Add(Me.chkCategorisedMenu_12Month)
        Me.Panel24.Controls.Add(Me.Label99)
        Me.Panel24.Controls.Add(Me.chkCategorisedMenu_Merge)
        Me.Panel24.Controls.Add(Me.Label98)
        Me.Panel24.Controls.Add(Me.Label94)
        Me.Panel24.Controls.Add(Me.Label95)
        Me.Panel24.Controls.Add(Me.ImgBtn_CategorisedDocument_MenuOptionOk)
        Me.Panel24.Controls.Add(Me.Label96)
        Me.Panel24.Controls.Add(Me.Label97)
        Me.Panel24.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel24.Location = New System.Drawing.Point(3, 0)
        Me.Panel24.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel24.Name = "Panel24"
        Me.Panel24.Size = New System.Drawing.Size(1082, 25)
        Me.Panel24.TabIndex = 21
        '
        'chkCategorisedMenu_12Month
        '
        Me.chkCategorisedMenu_12Month.BackColor = System.Drawing.Color.Transparent
        Me.chkCategorisedMenu_12Month.Dock = System.Windows.Forms.DockStyle.Left
        Me.chkCategorisedMenu_12Month.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkCategorisedMenu_12Month.Location = New System.Drawing.Point(67, 1)
        Me.chkCategorisedMenu_12Month.Name = "chkCategorisedMenu_12Month"
        Me.chkCategorisedMenu_12Month.Size = New System.Drawing.Size(121, 23)
        Me.chkCategorisedMenu_12Month.TabIndex = 20
        Me.chkCategorisedMenu_12Month.Text = "12 Month New Menu"
        Me.chkCategorisedMenu_12Month.UseVisualStyleBackColor = False
        '
        'Label99
        '
        Me.Label99.BackColor = System.Drawing.Color.Transparent
        Me.Label99.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label99.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label99.Location = New System.Drawing.Point(62, 1)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(5, 23)
        Me.Label99.TabIndex = 22
        '
        'chkCategorisedMenu_Merge
        '
        Me.chkCategorisedMenu_Merge.BackColor = System.Drawing.Color.Transparent
        Me.chkCategorisedMenu_Merge.Dock = System.Windows.Forms.DockStyle.Left
        Me.chkCategorisedMenu_Merge.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkCategorisedMenu_Merge.Location = New System.Drawing.Point(6, 1)
        Me.chkCategorisedMenu_Merge.Name = "chkCategorisedMenu_Merge"
        Me.chkCategorisedMenu_Merge.Size = New System.Drawing.Size(56, 23)
        Me.chkCategorisedMenu_Merge.TabIndex = 18
        Me.chkCategorisedMenu_Merge.Text = "Merge"
        Me.chkCategorisedMenu_Merge.UseVisualStyleBackColor = False
        '
        'Label98
        '
        Me.Label98.BackColor = System.Drawing.Color.Transparent
        Me.Label98.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label98.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label98.Location = New System.Drawing.Point(1, 1)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(5, 23)
        Me.Label98.TabIndex = 21
        '
        'Label94
        '
        Me.Label94.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label94.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label94.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label94.Location = New System.Drawing.Point(1, 24)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(1059, 1)
        Me.Label94.TabIndex = 8
        Me.Label94.Text = "label2"
        '
        'Label95
        '
        Me.Label95.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label95.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label95.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label95.Location = New System.Drawing.Point(0, 1)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(1, 24)
        Me.Label95.TabIndex = 7
        Me.Label95.Text = "label4"
        '
        'ImgBtn_CategorisedDocument_MenuOptionOk
        '
        Me.ImgBtn_CategorisedDocument_MenuOptionOk.BackColor = System.Drawing.Color.Transparent
        Me.ImgBtn_CategorisedDocument_MenuOptionOk.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ImgBtn_CategorisedDocument_MenuOptionOk.Dock = System.Windows.Forms.DockStyle.Right
        Me.ImgBtn_CategorisedDocument_MenuOptionOk.Image = CType(resources.GetObject("ImgBtn_CategorisedDocument_MenuOptionOk.Image"), System.Drawing.Image)
        Me.ImgBtn_CategorisedDocument_MenuOptionOk.Location = New System.Drawing.Point(1060, 1)
        Me.ImgBtn_CategorisedDocument_MenuOptionOk.Name = "ImgBtn_CategorisedDocument_MenuOptionOk"
        Me.ImgBtn_CategorisedDocument_MenuOptionOk.Size = New System.Drawing.Size(21, 24)
        Me.ImgBtn_CategorisedDocument_MenuOptionOk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ImgBtn_CategorisedDocument_MenuOptionOk.TabIndex = 19
        Me.ImgBtn_CategorisedDocument_MenuOptionOk.TabStop = False
        '
        'Label96
        '
        Me.Label96.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label96.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label96.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label96.Location = New System.Drawing.Point(1081, 1)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(1, 24)
        Me.Label96.TabIndex = 6
        Me.Label96.Text = "label3"
        '
        'Label97
        '
        Me.Label97.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label97.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label97.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label97.Location = New System.Drawing.Point(0, 0)
        Me.Label97.Name = "Label97"
        Me.Label97.Size = New System.Drawing.Size(1082, 1)
        Me.Label97.TabIndex = 5
        Me.Label97.Text = "label1"
        '
        'pnlDocument_CategorisedDocument_MonthYear
        '
        Me.pnlDocument_CategorisedDocument_MonthYear.Controls.Add(Me.pnlDocument_CategorisedDocument_Month)
        Me.pnlDocument_CategorisedDocument_MonthYear.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDocument_CategorisedDocument_MonthYear.Location = New System.Drawing.Point(0, 27)
        Me.pnlDocument_CategorisedDocument_MonthYear.Name = "pnlDocument_CategorisedDocument_MonthYear"
        Me.pnlDocument_CategorisedDocument_MonthYear.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlDocument_CategorisedDocument_MonthYear.Size = New System.Drawing.Size(1088, 27)
        Me.pnlDocument_CategorisedDocument_MonthYear.TabIndex = 19
        Me.pnlDocument_CategorisedDocument_MonthYear.Visible = False
        '
        'pnlDocument_CategorisedDocument_Month
        '
        Me.pnlDocument_CategorisedDocument_Month.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlDocument_CategorisedDocument_Month.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDocument_CategorisedDocument_Month.Controls.Add(Me.pnlDocument_CategorisedDocument_Cmd_YearRefresh)
        Me.pnlDocument_CategorisedDocument_Month.Controls.Add(Me.txtCategorisedYear)
        Me.pnlDocument_CategorisedDocument_Month.Controls.Add(Me.pnlDocument_CategorisedDocument_Year)
        Me.pnlDocument_CategorisedDocument_Month.Controls.Add(Me.Label108)
        Me.pnlDocument_CategorisedDocument_Month.Controls.Add(Me.Label109)
        Me.pnlDocument_CategorisedDocument_Month.Controls.Add(Me.Label110)
        Me.pnlDocument_CategorisedDocument_Month.Controls.Add(Me.Label111)
        Me.pnlDocument_CategorisedDocument_Month.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDocument_CategorisedDocument_Month.Location = New System.Drawing.Point(3, 0)
        Me.pnlDocument_CategorisedDocument_Month.Name = "pnlDocument_CategorisedDocument_Month"
        Me.pnlDocument_CategorisedDocument_Month.Size = New System.Drawing.Size(1082, 24)
        Me.pnlDocument_CategorisedDocument_Month.TabIndex = 19
        '
        'pnlDocument_CategorisedDocument_Cmd_YearRefresh
        '
        Me.pnlDocument_CategorisedDocument_Cmd_YearRefresh.Controls.Add(Me.ImgBtn_CategorisedDocument_YearRefresh)
        Me.pnlDocument_CategorisedDocument_Cmd_YearRefresh.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlDocument_CategorisedDocument_Cmd_YearRefresh.Location = New System.Drawing.Point(1062, 1)
        Me.pnlDocument_CategorisedDocument_Cmd_YearRefresh.Name = "pnlDocument_CategorisedDocument_Cmd_YearRefresh"
        Me.pnlDocument_CategorisedDocument_Cmd_YearRefresh.Size = New System.Drawing.Size(19, 22)
        Me.pnlDocument_CategorisedDocument_Cmd_YearRefresh.TabIndex = 1
        '
        'ImgBtn_CategorisedDocument_YearRefresh
        '
        Me.ImgBtn_CategorisedDocument_YearRefresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ImgBtn_CategorisedDocument_YearRefresh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ImgBtn_CategorisedDocument_YearRefresh.Image = CType(resources.GetObject("ImgBtn_CategorisedDocument_YearRefresh.Image"), System.Drawing.Image)
        Me.ImgBtn_CategorisedDocument_YearRefresh.Location = New System.Drawing.Point(0, 0)
        Me.ImgBtn_CategorisedDocument_YearRefresh.Name = "ImgBtn_CategorisedDocument_YearRefresh"
        Me.ImgBtn_CategorisedDocument_YearRefresh.Size = New System.Drawing.Size(19, 22)
        Me.ImgBtn_CategorisedDocument_YearRefresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ImgBtn_CategorisedDocument_YearRefresh.TabIndex = 1
        Me.ImgBtn_CategorisedDocument_YearRefresh.TabStop = False
        '
        'txtCategorisedYear
        '
        Me.txtCategorisedYear.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtCategorisedYear.ForeColor = System.Drawing.Color.Black
        Me.txtCategorisedYear.Location = New System.Drawing.Point(164, 1)
        Me.txtCategorisedYear.MaxLength = 4
        Me.txtCategorisedYear.Name = "txtCategorisedYear"
        Me.txtCategorisedYear.Size = New System.Drawing.Size(123, 22)
        Me.txtCategorisedYear.TabIndex = 0
        '
        'pnlDocument_CategorisedDocument_Year
        '
        Me.pnlDocument_CategorisedDocument_Year.Controls.Add(Me.Label14)
        Me.pnlDocument_CategorisedDocument_Year.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlDocument_CategorisedDocument_Year.Location = New System.Drawing.Point(1, 1)
        Me.pnlDocument_CategorisedDocument_Year.Name = "pnlDocument_CategorisedDocument_Year"
        Me.pnlDocument_CategorisedDocument_Year.Size = New System.Drawing.Size(163, 22)
        Me.pnlDocument_CategorisedDocument_Year.TabIndex = 17
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(163, 22)
        Me.Label14.TabIndex = 16
        Me.Label14.Text = " Enter Document Year :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label108
        '
        Me.Label108.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label108.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label108.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label108.Location = New System.Drawing.Point(1, 23)
        Me.Label108.Name = "Label108"
        Me.Label108.Size = New System.Drawing.Size(1080, 1)
        Me.Label108.TabIndex = 21
        Me.Label108.Text = "label2"
        '
        'Label109
        '
        Me.Label109.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label109.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label109.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label109.Location = New System.Drawing.Point(0, 1)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(1, 23)
        Me.Label109.TabIndex = 20
        Me.Label109.Text = "label4"
        '
        'Label110
        '
        Me.Label110.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label110.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label110.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label110.Location = New System.Drawing.Point(1081, 1)
        Me.Label110.Name = "Label110"
        Me.Label110.Size = New System.Drawing.Size(1, 23)
        Me.Label110.TabIndex = 19
        Me.Label110.Text = "label3"
        '
        'Label111
        '
        Me.Label111.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label111.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label111.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label111.Location = New System.Drawing.Point(0, 0)
        Me.Label111.Name = "Label111"
        Me.Label111.Size = New System.Drawing.Size(1082, 1)
        Me.Label111.TabIndex = 18
        Me.Label111.Text = "label1"
        '
        'pnlDocument_CategorisedDocument_HdrCmd
        '
        Me.pnlDocument_CategorisedDocument_HdrCmd.Controls.Add(Me.Panel26)
        Me.pnlDocument_CategorisedDocument_HdrCmd.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDocument_CategorisedDocument_HdrCmd.Location = New System.Drawing.Point(0, 0)
        Me.pnlDocument_CategorisedDocument_HdrCmd.Name = "pnlDocument_CategorisedDocument_HdrCmd"
        Me.pnlDocument_CategorisedDocument_HdrCmd.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlDocument_CategorisedDocument_HdrCmd.Size = New System.Drawing.Size(1088, 27)
        Me.pnlDocument_CategorisedDocument_HdrCmd.TabIndex = 14
        Me.pnlDocument_CategorisedDocument_HdrCmd.Visible = False
        '
        'Panel26
        '
        Me.Panel26.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel26.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel26.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel26.Controls.Add(Me.lblDocument_CategorisedDocument_Hdr)
        Me.Panel26.Controls.Add(Me.pnlDocument_CategorisedDocument_Cmd)
        Me.Panel26.Controls.Add(Me.Label104)
        Me.Panel26.Controls.Add(Me.Label105)
        Me.Panel26.Controls.Add(Me.Label106)
        Me.Panel26.Controls.Add(Me.Label107)
        Me.Panel26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel26.Location = New System.Drawing.Point(3, 0)
        Me.Panel26.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel26.Name = "Panel26"
        Me.Panel26.Size = New System.Drawing.Size(1082, 24)
        Me.Panel26.TabIndex = 20
        '
        'lblDocument_CategorisedDocument_Hdr
        '
        Me.lblDocument_CategorisedDocument_Hdr.BackColor = System.Drawing.Color.Transparent
        Me.lblDocument_CategorisedDocument_Hdr.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDocument_CategorisedDocument_Hdr.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocument_CategorisedDocument_Hdr.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblDocument_CategorisedDocument_Hdr.Location = New System.Drawing.Point(1, 1)
        Me.lblDocument_CategorisedDocument_Hdr.Name = "lblDocument_CategorisedDocument_Hdr"
        Me.lblDocument_CategorisedDocument_Hdr.Size = New System.Drawing.Size(943, 22)
        Me.lblDocument_CategorisedDocument_Hdr.TabIndex = 16
        Me.lblDocument_CategorisedDocument_Hdr.Text = " Document : 2006"
        Me.lblDocument_CategorisedDocument_Hdr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlDocument_CategorisedDocument_Cmd
        '
        Me.pnlDocument_CategorisedDocument_Cmd.BackColor = System.Drawing.Color.FromArgb(CType(CType(167, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.pnlDocument_CategorisedDocument_Cmd.Controls.Add(Me.Label15)
        Me.pnlDocument_CategorisedDocument_Cmd.Controls.Add(Me.pnlDocument_CategorisedDocument_Cmd_MenuOption)
        Me.pnlDocument_CategorisedDocument_Cmd.Controls.Add(Me.pnlDocument_CategorisedDocument_Cmd_Year)
        Me.pnlDocument_CategorisedDocument_Cmd.Controls.Add(Me.pnlDocument_CategorisedDocument_Cmd_Scan)
        Me.pnlDocument_CategorisedDocument_Cmd.Controls.Add(Me.pnlDocument_CategorisedDocument_Cmd_Import)
        Me.pnlDocument_CategorisedDocument_Cmd.Controls.Add(Me.pnlDocument_CategorisedDocument_Cmd_Category)
        Me.pnlDocument_CategorisedDocument_Cmd.Controls.Add(Me.pnlDocument_CategorisedDocument_Cmd_Refresh)
        Me.pnlDocument_CategorisedDocument_Cmd.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlDocument_CategorisedDocument_Cmd.Location = New System.Drawing.Point(944, 1)
        Me.pnlDocument_CategorisedDocument_Cmd.Name = "pnlDocument_CategorisedDocument_Cmd"
        Me.pnlDocument_CategorisedDocument_Cmd.Size = New System.Drawing.Size(137, 22)
        Me.pnlDocument_CategorisedDocument_Cmd.TabIndex = 0
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(167, Byte), Integer), CType(CType(197, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Navy
        Me.Label15.Image = CType(resources.GetObject("Label15.Image"), System.Drawing.Image)
        Me.Label15.Location = New System.Drawing.Point(0, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(23, 22)
        Me.Label15.TabIndex = 17
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlDocument_CategorisedDocument_Cmd_MenuOption
        '
        Me.pnlDocument_CategorisedDocument_Cmd_MenuOption.Controls.Add(Me.ImgBtn_CategorisedDocument_MenuOption)
        Me.pnlDocument_CategorisedDocument_Cmd_MenuOption.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlDocument_CategorisedDocument_Cmd_MenuOption.Location = New System.Drawing.Point(23, 0)
        Me.pnlDocument_CategorisedDocument_Cmd_MenuOption.Name = "pnlDocument_CategorisedDocument_Cmd_MenuOption"
        Me.pnlDocument_CategorisedDocument_Cmd_MenuOption.Size = New System.Drawing.Size(19, 22)
        Me.pnlDocument_CategorisedDocument_Cmd_MenuOption.TabIndex = 8
        Me.pnlDocument_CategorisedDocument_Cmd_MenuOption.Visible = False
        '
        'ImgBtn_CategorisedDocument_MenuOption
        '
        Me.ImgBtn_CategorisedDocument_MenuOption.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ImgBtn_CategorisedDocument_MenuOption.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ImgBtn_CategorisedDocument_MenuOption.Image = CType(resources.GetObject("ImgBtn_CategorisedDocument_MenuOption.Image"), System.Drawing.Image)
        Me.ImgBtn_CategorisedDocument_MenuOption.Location = New System.Drawing.Point(0, 0)
        Me.ImgBtn_CategorisedDocument_MenuOption.Name = "ImgBtn_CategorisedDocument_MenuOption"
        Me.ImgBtn_CategorisedDocument_MenuOption.Size = New System.Drawing.Size(19, 22)
        Me.ImgBtn_CategorisedDocument_MenuOption.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.ImgBtn_CategorisedDocument_MenuOption.TabIndex = 1
        Me.ImgBtn_CategorisedDocument_MenuOption.TabStop = False
        '
        'pnlDocument_CategorisedDocument_Cmd_Year
        '
        Me.pnlDocument_CategorisedDocument_Cmd_Year.Controls.Add(Me.ImgBtn_CategorisedDocument_Year)
        Me.pnlDocument_CategorisedDocument_Cmd_Year.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlDocument_CategorisedDocument_Cmd_Year.Location = New System.Drawing.Point(42, 0)
        Me.pnlDocument_CategorisedDocument_Cmd_Year.Name = "pnlDocument_CategorisedDocument_Cmd_Year"
        Me.pnlDocument_CategorisedDocument_Cmd_Year.Size = New System.Drawing.Size(19, 22)
        Me.pnlDocument_CategorisedDocument_Cmd_Year.TabIndex = 7
        Me.pnlDocument_CategorisedDocument_Cmd_Year.Visible = False
        '
        'ImgBtn_CategorisedDocument_Year
        '
        Me.ImgBtn_CategorisedDocument_Year.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ImgBtn_CategorisedDocument_Year.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ImgBtn_CategorisedDocument_Year.Image = CType(resources.GetObject("ImgBtn_CategorisedDocument_Year.Image"), System.Drawing.Image)
        Me.ImgBtn_CategorisedDocument_Year.Location = New System.Drawing.Point(0, 0)
        Me.ImgBtn_CategorisedDocument_Year.Name = "ImgBtn_CategorisedDocument_Year"
        Me.ImgBtn_CategorisedDocument_Year.Size = New System.Drawing.Size(19, 22)
        Me.ImgBtn_CategorisedDocument_Year.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.ImgBtn_CategorisedDocument_Year.TabIndex = 1
        Me.ImgBtn_CategorisedDocument_Year.TabStop = False
        '
        'pnlDocument_CategorisedDocument_Cmd_Scan
        '
        Me.pnlDocument_CategorisedDocument_Cmd_Scan.Controls.Add(Me.ImgBtn_CategorisedDocument_Scan)
        Me.pnlDocument_CategorisedDocument_Cmd_Scan.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlDocument_CategorisedDocument_Cmd_Scan.Location = New System.Drawing.Point(61, 0)
        Me.pnlDocument_CategorisedDocument_Cmd_Scan.Name = "pnlDocument_CategorisedDocument_Cmd_Scan"
        Me.pnlDocument_CategorisedDocument_Cmd_Scan.Size = New System.Drawing.Size(19, 22)
        Me.pnlDocument_CategorisedDocument_Cmd_Scan.TabIndex = 6
        '
        'ImgBtn_CategorisedDocument_Scan
        '
        Me.ImgBtn_CategorisedDocument_Scan.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ImgBtn_CategorisedDocument_Scan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ImgBtn_CategorisedDocument_Scan.Image = CType(resources.GetObject("ImgBtn_CategorisedDocument_Scan.Image"), System.Drawing.Image)
        Me.ImgBtn_CategorisedDocument_Scan.Location = New System.Drawing.Point(0, 0)
        Me.ImgBtn_CategorisedDocument_Scan.Name = "ImgBtn_CategorisedDocument_Scan"
        Me.ImgBtn_CategorisedDocument_Scan.Size = New System.Drawing.Size(19, 22)
        Me.ImgBtn_CategorisedDocument_Scan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.ImgBtn_CategorisedDocument_Scan.TabIndex = 1
        Me.ImgBtn_CategorisedDocument_Scan.TabStop = False
        '
        'pnlDocument_CategorisedDocument_Cmd_Import
        '
        Me.pnlDocument_CategorisedDocument_Cmd_Import.Controls.Add(Me.ImgBtn_CategorisedDocument_Import)
        Me.pnlDocument_CategorisedDocument_Cmd_Import.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlDocument_CategorisedDocument_Cmd_Import.Location = New System.Drawing.Point(80, 0)
        Me.pnlDocument_CategorisedDocument_Cmd_Import.Name = "pnlDocument_CategorisedDocument_Cmd_Import"
        Me.pnlDocument_CategorisedDocument_Cmd_Import.Size = New System.Drawing.Size(19, 22)
        Me.pnlDocument_CategorisedDocument_Cmd_Import.TabIndex = 5
        '
        'ImgBtn_CategorisedDocument_Import
        '
        Me.ImgBtn_CategorisedDocument_Import.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ImgBtn_CategorisedDocument_Import.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ImgBtn_CategorisedDocument_Import.Image = CType(resources.GetObject("ImgBtn_CategorisedDocument_Import.Image"), System.Drawing.Image)
        Me.ImgBtn_CategorisedDocument_Import.Location = New System.Drawing.Point(0, 0)
        Me.ImgBtn_CategorisedDocument_Import.Name = "ImgBtn_CategorisedDocument_Import"
        Me.ImgBtn_CategorisedDocument_Import.Size = New System.Drawing.Size(19, 22)
        Me.ImgBtn_CategorisedDocument_Import.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.ImgBtn_CategorisedDocument_Import.TabIndex = 1
        Me.ImgBtn_CategorisedDocument_Import.TabStop = False
        '
        'pnlDocument_CategorisedDocument_Cmd_Category
        '
        Me.pnlDocument_CategorisedDocument_Cmd_Category.Controls.Add(Me.ImgBtn_CategorisedDocument_Category)
        Me.pnlDocument_CategorisedDocument_Cmd_Category.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlDocument_CategorisedDocument_Cmd_Category.Location = New System.Drawing.Point(99, 0)
        Me.pnlDocument_CategorisedDocument_Cmd_Category.Name = "pnlDocument_CategorisedDocument_Cmd_Category"
        Me.pnlDocument_CategorisedDocument_Cmd_Category.Size = New System.Drawing.Size(19, 22)
        Me.pnlDocument_CategorisedDocument_Cmd_Category.TabIndex = 1
        '
        'ImgBtn_CategorisedDocument_Category
        '
        Me.ImgBtn_CategorisedDocument_Category.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ImgBtn_CategorisedDocument_Category.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ImgBtn_CategorisedDocument_Category.Image = CType(resources.GetObject("ImgBtn_CategorisedDocument_Category.Image"), System.Drawing.Image)
        Me.ImgBtn_CategorisedDocument_Category.Location = New System.Drawing.Point(0, 0)
        Me.ImgBtn_CategorisedDocument_Category.Name = "ImgBtn_CategorisedDocument_Category"
        Me.ImgBtn_CategorisedDocument_Category.Size = New System.Drawing.Size(19, 22)
        Me.ImgBtn_CategorisedDocument_Category.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.ImgBtn_CategorisedDocument_Category.TabIndex = 1
        Me.ImgBtn_CategorisedDocument_Category.TabStop = False
        '
        'pnlDocument_CategorisedDocument_Cmd_Refresh
        '
        Me.pnlDocument_CategorisedDocument_Cmd_Refresh.Controls.Add(Me.ImgBtn_CategorisedDocument_Refresh)
        Me.pnlDocument_CategorisedDocument_Cmd_Refresh.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlDocument_CategorisedDocument_Cmd_Refresh.Location = New System.Drawing.Point(118, 0)
        Me.pnlDocument_CategorisedDocument_Cmd_Refresh.Name = "pnlDocument_CategorisedDocument_Cmd_Refresh"
        Me.pnlDocument_CategorisedDocument_Cmd_Refresh.Size = New System.Drawing.Size(19, 22)
        Me.pnlDocument_CategorisedDocument_Cmd_Refresh.TabIndex = 0
        '
        'ImgBtn_CategorisedDocument_Refresh
        '
        Me.ImgBtn_CategorisedDocument_Refresh.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ImgBtn_CategorisedDocument_Refresh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ImgBtn_CategorisedDocument_Refresh.Image = CType(resources.GetObject("ImgBtn_CategorisedDocument_Refresh.Image"), System.Drawing.Image)
        Me.ImgBtn_CategorisedDocument_Refresh.Location = New System.Drawing.Point(0, 0)
        Me.ImgBtn_CategorisedDocument_Refresh.Name = "ImgBtn_CategorisedDocument_Refresh"
        Me.ImgBtn_CategorisedDocument_Refresh.Size = New System.Drawing.Size(19, 22)
        Me.ImgBtn_CategorisedDocument_Refresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ImgBtn_CategorisedDocument_Refresh.TabIndex = 1
        Me.ImgBtn_CategorisedDocument_Refresh.TabStop = False
        '
        'Label104
        '
        Me.Label104.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label104.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label104.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label104.Location = New System.Drawing.Point(1, 23)
        Me.Label104.Name = "Label104"
        Me.Label104.Size = New System.Drawing.Size(1080, 1)
        Me.Label104.TabIndex = 8
        Me.Label104.Text = "label2"
        '
        'Label105
        '
        Me.Label105.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label105.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label105.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label105.Location = New System.Drawing.Point(0, 1)
        Me.Label105.Name = "Label105"
        Me.Label105.Size = New System.Drawing.Size(1, 23)
        Me.Label105.TabIndex = 7
        Me.Label105.Text = "label4"
        '
        'Label106
        '
        Me.Label106.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label106.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label106.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label106.Location = New System.Drawing.Point(1081, 1)
        Me.Label106.Name = "Label106"
        Me.Label106.Size = New System.Drawing.Size(1, 23)
        Me.Label106.TabIndex = 6
        Me.Label106.Text = "label3"
        '
        'Label107
        '
        Me.Label107.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label107.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label107.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label107.Location = New System.Drawing.Point(0, 0)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(1082, 1)
        Me.Label107.TabIndex = 5
        Me.Label107.Text = "label1"
        '
        'trvscandoc
        '
        Me.trvscandoc.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvscandoc.ItemHeight = 18
        Me.trvscandoc.Location = New System.Drawing.Point(0, 272)
        Me.trvscandoc.Name = "trvscandoc"
        Me.trvscandoc.Size = New System.Drawing.Size(204, 112)
        Me.trvscandoc.TabIndex = 1
        '
        'pnlScannedDocsHeader
        '
        Me.pnlScannedDocsHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlScannedDocsHeader.Controls.Add(Me.Panel25)
        Me.pnlScannedDocsHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlScannedDocsHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlScannedDocsHeader.Name = "pnlScannedDocsHeader"
        Me.pnlScannedDocsHeader.Padding = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.pnlScannedDocsHeader.Size = New System.Drawing.Size(1088, 27)
        Me.pnlScannedDocsHeader.TabIndex = 6
        Me.pnlScannedDocsHeader.Visible = False
        '
        'Panel25
        '
        Me.Panel25.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel25.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel25.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel25.Controls.Add(Me.lblScannedDocs)
        Me.Panel25.Controls.Add(Me.PictureBox6)
        Me.Panel25.Controls.Add(Me.Label102)
        Me.Panel25.Controls.Add(Me.Label100)
        Me.Panel25.Controls.Add(Me.Label101)
        Me.Panel25.Controls.Add(Me.Label103)
        Me.Panel25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel25.Location = New System.Drawing.Point(3, 0)
        Me.Panel25.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel25.Name = "Panel25"
        Me.Panel25.Size = New System.Drawing.Size(1082, 24)
        Me.Panel25.TabIndex = 20
        '
        'lblScannedDocs
        '
        Me.lblScannedDocs.BackColor = System.Drawing.Color.Transparent
        Me.lblScannedDocs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblScannedDocs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScannedDocs.Location = New System.Drawing.Point(29, 1)
        Me.lblScannedDocs.Name = "lblScannedDocs"
        Me.lblScannedDocs.Size = New System.Drawing.Size(1052, 22)
        Me.lblScannedDocs.TabIndex = 0
        Me.lblScannedDocs.Text = "Scanned Documents"
        Me.lblScannedDocs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox6
        '
        Me.PictureBox6.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox6.Dock = System.Windows.Forms.DockStyle.Left
        Me.PictureBox6.Image = CType(resources.GetObject("PictureBox6.Image"), System.Drawing.Image)
        Me.PictureBox6.Location = New System.Drawing.Point(1, 1)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(28, 22)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox6.TabIndex = 1
        Me.PictureBox6.TabStop = False
        '
        'Label102
        '
        Me.Label102.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label102.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label102.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label102.Location = New System.Drawing.Point(1081, 1)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(1, 22)
        Me.Label102.TabIndex = 6
        Me.Label102.Text = "label3"
        '
        'Label100
        '
        Me.Label100.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label100.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label100.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label100.Location = New System.Drawing.Point(1, 23)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(1081, 1)
        Me.Label100.TabIndex = 8
        Me.Label100.Text = "label2"
        '
        'Label101
        '
        Me.Label101.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label101.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label101.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label101.Location = New System.Drawing.Point(0, 1)
        Me.Label101.Name = "Label101"
        Me.Label101.Size = New System.Drawing.Size(1, 23)
        Me.Label101.TabIndex = 7
        Me.Label101.Text = "label4"
        '
        'Label103
        '
        Me.Label103.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label103.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label103.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label103.Location = New System.Drawing.Point(0, 0)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(1082, 1)
        Me.Label103.TabIndex = 5
        Me.Label103.Text = "label1"
        '
        'pnlDMSSearch
        '
        Me.pnlDMSSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlDMSSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDMSSearch.Controls.Add(Me.Panel23)
        Me.pnlDMSSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDMSSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlDMSSearch.Name = "pnlDMSSearch"
        Me.pnlDMSSearch.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlDMSSearch.Size = New System.Drawing.Size(1088, 28)
        Me.pnlDMSSearch.TabIndex = 0
        '
        'Panel23
        '
        Me.Panel23.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel23.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel23.Controls.Add(Me.Button1)
        Me.Panel23.Controls.Add(Me.Label93)
        Me.Panel23.Controls.Add(Me.txtSearchCriteria)
        Me.Panel23.Controls.Add(Me.lblsearch)
        Me.Panel23.Controls.Add(Me.cmbSearch)
        Me.Panel23.Controls.Add(Me.Label12)
        Me.Panel23.Controls.Add(Me.Label89)
        Me.Panel23.Controls.Add(Me.Label90)
        Me.Panel23.Controls.Add(Me.Label91)
        Me.Panel23.Controls.Add(Me.Label92)
        Me.Panel23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel23.Location = New System.Drawing.Point(0, 0)
        Me.Panel23.Name = "Panel23"
        Me.Panel23.Size = New System.Drawing.Size(1088, 25)
        Me.Panel23.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(545, 1)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(24, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label93
        '
        Me.Label93.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label93.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label93.ForeColor = System.Drawing.Color.White
        Me.Label93.Location = New System.Drawing.Point(535, 1)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(10, 23)
        Me.Label93.TabIndex = 13
        Me.Label93.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSearchCriteria
        '
        Me.txtSearchCriteria.Dock = System.Windows.Forms.DockStyle.Left
        Me.txtSearchCriteria.ForeColor = System.Drawing.Color.Black
        Me.txtSearchCriteria.Location = New System.Drawing.Point(379, 1)
        Me.txtSearchCriteria.Name = "txtSearchCriteria"
        Me.txtSearchCriteria.Size = New System.Drawing.Size(156, 22)
        Me.txtSearchCriteria.TabIndex = 3
        '
        'lblsearch
        '
        Me.lblsearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblsearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsearch.ForeColor = System.Drawing.Color.White
        Me.lblsearch.Location = New System.Drawing.Point(248, 1)
        Me.lblsearch.Name = "lblsearch"
        Me.lblsearch.Size = New System.Drawing.Size(131, 23)
        Me.lblsearch.TabIndex = 1
        Me.lblsearch.Text = "   Search Criteria :"
        Me.lblsearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbSearch
        '
        Me.cmbSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbSearch.ForeColor = System.Drawing.Color.Black
        Me.cmbSearch.FormattingEnabled = True
        Me.cmbSearch.Location = New System.Drawing.Point(92, 1)
        Me.cmbSearch.Name = "cmbSearch"
        Me.cmbSearch.Size = New System.Drawing.Size(156, 22)
        Me.cmbSearch.TabIndex = 2
        '
        'Label12
        '
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(1, 1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(91, 23)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "  Search on :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label89
        '
        Me.Label89.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label89.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label89.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label89.Location = New System.Drawing.Point(1, 24)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(1086, 1)
        Me.Label89.TabIndex = 12
        Me.Label89.Text = "label2"
        '
        'Label90
        '
        Me.Label90.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label90.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label90.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label90.Location = New System.Drawing.Point(0, 1)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(1, 24)
        Me.Label90.TabIndex = 11
        Me.Label90.Text = "label4"
        '
        'Label91
        '
        Me.Label91.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label91.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label91.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label91.Location = New System.Drawing.Point(1087, 1)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(1, 24)
        Me.Label91.TabIndex = 10
        Me.Label91.Text = "label3"
        '
        'Label92
        '
        Me.Label92.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label92.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label92.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label92.Location = New System.Drawing.Point(0, 0)
        Me.Label92.Name = "Label92"
        Me.Label92.Size = New System.Drawing.Size(1088, 1)
        Me.Label92.TabIndex = 9
        Me.Label92.Text = "label1"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Problem List.ico")
        Me.ImageList1.Images.SetKeyName(1, "Medication.ico")
        Me.ImageList1.Images.SetKeyName(2, "History.ico")
        Me.ImageList1.Images.SetKeyName(3, "CPT.ico")
        Me.ImageList1.Images.SetKeyName(4, "Labs.ico")
        Me.ImageList1.Images.SetKeyName(5, "Radiology.ico")
        Me.ImageList1.Images.SetKeyName(6, "Bullet06.ico")
        Me.ImageList1.Images.SetKeyName(7, "ICD 9.ico")
        Me.ImageList1.Images.SetKeyName(8, "Modify.ico")
        Me.ImageList1.Images.SetKeyName(9, "Patient Synopsis.ico")
        Me.ImageList1.Images.SetKeyName(10, "Modify.ico")
        Me.ImageList1.Images.SetKeyName(11, "Category.ico")
        Me.ImageList1.Images.SetKeyName(12, "arrow_01.ico")
        Me.ImageList1.Images.SetKeyName(13, "Modifier_01.ico")
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.tbSummary)
        Me.pnlTop.Controls.Add(Me.Label17)
        Me.pnlTop.Controls.Add(Me.Label18)
        Me.pnlTop.Controls.Add(Me.Label19)
        Me.pnlTop.Controls.Add(Me.Label20)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 3)
        Me.pnlTop.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTop.Size = New System.Drawing.Size(1096, 400)
        Me.pnlTop.TabIndex = 3
        '
        'tbSummary
        '
        Me.tbSummary.Controls.Add(Me.tbpSummary)
        Me.tbSummary.Controls.Add(Me.tbpProblem)
        Me.tbSummary.Controls.Add(Me.tbpMedications)
        Me.tbSummary.Controls.Add(Me.tbpAllergies)
        Me.tbSummary.Controls.Add(Me.tbProcedures)
        Me.tbSummary.Controls.Add(Me.tbpLabs)
        Me.tbSummary.Controls.Add(Me.tbpImaging)
        Me.tbSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbSummary.ImageList = Me.ImageList1
        Me.tbSummary.ItemSize = New System.Drawing.Size(80, 22)
        Me.tbSummary.Location = New System.Drawing.Point(4, 4)
        Me.tbSummary.Margin = New System.Windows.Forms.Padding(4)
        Me.tbSummary.Name = "tbSummary"
        Me.tbSummary.SelectedIndex = 0
        Me.tbSummary.Size = New System.Drawing.Size(1088, 392)
        Me.tbSummary.TabIndex = 0
        '
        'tbpSummary
        '
        Me.tbpSummary.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpSummary.Controls.Add(Me.pnlSummary)
        Me.tbpSummary.ImageIndex = 9
        Me.tbpSummary.Location = New System.Drawing.Point(4, 26)
        Me.tbpSummary.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tbpSummary.Name = "tbpSummary"
        Me.tbpSummary.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tbpSummary.Size = New System.Drawing.Size(1080, 362)
        Me.tbpSummary.TabIndex = 0
        Me.tbpSummary.Text = "Synopsis"
        '
        'pnlSummary
        '
        Me.pnlSummary.AutoScroll = True
        Me.pnlSummary.Controls.Add(Me.pnlImaging)
        Me.pnlSummary.Controls.Add(Me.spltLabs)
        Me.pnlSummary.Controls.Add(Me.pnlLabs)
        Me.pnlSummary.Controls.Add(Me.spltProcedures)
        Me.pnlSummary.Controls.Add(Me.pnlProcedures)
        Me.pnlSummary.Controls.Add(Me.spltHistory)
        Me.pnlSummary.Controls.Add(Me.pnlHistory)
        Me.pnlSummary.Controls.Add(Me.spltMedications)
        Me.pnlSummary.Controls.Add(Me.pnlMedications)
        Me.pnlSummary.Controls.Add(Me.spltProblems)
        Me.pnlSummary.Controls.Add(Me.pnlProblems)
        Me.pnlSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSummary.Location = New System.Drawing.Point(4, 3)
        Me.pnlSummary.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlSummary.Name = "pnlSummary"
        Me.pnlSummary.Size = New System.Drawing.Size(1072, 356)
        Me.pnlSummary.TabIndex = 0
        '
        'pnlImaging
        '
        Me.pnlImaging.Controls.Add(Me.Panel7)
        Me.pnlImaging.Controls.Add(Me.pnlImagingTitle)
        Me.pnlImaging.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlImaging.Location = New System.Drawing.Point(905, 0)
        Me.pnlImaging.Name = "pnlImaging"
        Me.pnlImaging.Size = New System.Drawing.Size(167, 356)
        Me.pnlImaging.TabIndex = 10
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.trImaging)
        Me.Panel7.Controls.Add(Me.Label74)
        Me.Panel7.Controls.Add(Me.Label75)
        Me.Panel7.Controls.Add(Me.Label36)
        Me.Panel7.Controls.Add(Me.Label37)
        Me.Panel7.Controls.Add(Me.Label38)
        Me.Panel7.Controls.Add(Me.Label39)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel7.Location = New System.Drawing.Point(0, 30)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(167, 326)
        Me.Panel7.TabIndex = 20
        '
        'trImaging
        '
        Me.trImaging.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trImaging.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trImaging.ForeColor = System.Drawing.Color.Black
        Me.trImaging.ImageIndex = 0
        Me.trImaging.ImageList = Me.ImageList1
        Me.trImaging.ItemHeight = 19
        Me.trImaging.Location = New System.Drawing.Point(3, 3)
        Me.trImaging.Name = "trImaging"
        Me.trImaging.SelectedImageIndex = 0
        Me.trImaging.ShowLines = False
        Me.trImaging.Size = New System.Drawing.Size(163, 322)
        Me.trImaging.TabIndex = 3
        '
        'Label74
        '
        Me.Label74.BackColor = System.Drawing.Color.White
        Me.Label74.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label74.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label74.Location = New System.Drawing.Point(3, 1)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(163, 2)
        Me.Label74.TabIndex = 12
        Me.Label74.Text = "label1"
        '
        'Label75
        '
        Me.Label75.BackColor = System.Drawing.Color.White
        Me.Label75.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label75.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label75.Location = New System.Drawing.Point(1, 1)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(2, 324)
        Me.Label75.TabIndex = 11
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label36.Location = New System.Drawing.Point(1, 325)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(165, 1)
        Me.Label36.TabIndex = 8
        Me.Label36.Text = "label2"
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label37.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(0, 1)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(1, 325)
        Me.Label37.TabIndex = 7
        Me.Label37.Text = "label4"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label38.Location = New System.Drawing.Point(166, 1)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(1, 325)
        Me.Label38.TabIndex = 6
        Me.Label38.Text = "label3"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label39.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(0, 0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(167, 1)
        Me.Label39.TabIndex = 5
        Me.Label39.Text = "label1"
        '
        'pnlImagingTitle
        '
        Me.pnlImagingTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlImagingTitle.Controls.Add(Me.Panel19)
        Me.pnlImagingTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlImagingTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlImagingTitle.Name = "pnlImagingTitle"
        Me.pnlImagingTitle.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlImagingTitle.Size = New System.Drawing.Size(167, 30)
        Me.pnlImagingTitle.TabIndex = 1
        '
        'Panel19
        '
        Me.Panel19.BackColor = System.Drawing.Color.Transparent
        Me.Panel19.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel19.Controls.Add(Me.Label60)
        Me.Panel19.Controls.Add(Me.Label61)
        Me.Panel19.Controls.Add(Me.Label5)
        Me.Panel19.Controls.Add(Me.Label62)
        Me.Panel19.Controls.Add(Me.Label63)
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel19.Location = New System.Drawing.Point(0, 0)
        Me.Panel19.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(167, 27)
        Me.Panel19.TabIndex = 20
        '
        'Label60
        '
        Me.Label60.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label60.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label60.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label60.Location = New System.Drawing.Point(1, 26)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(165, 1)
        Me.Label60.TabIndex = 8
        Me.Label60.Text = "label2"
        '
        'Label61
        '
        Me.Label61.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label61.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label61.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.Location = New System.Drawing.Point(0, 1)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(1, 26)
        Me.Label61.TabIndex = 7
        Me.Label61.Text = "label4"
        '
        'Label5
        '
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(0, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(166, 26)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Imaging"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label62.Location = New System.Drawing.Point(166, 1)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(1, 26)
        Me.Label62.TabIndex = 6
        Me.Label62.Text = "label3"
        '
        'Label63
        '
        Me.Label63.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label63.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.Location = New System.Drawing.Point(0, 0)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(167, 1)
        Me.Label63.TabIndex = 5
        Me.Label63.Text = "label1"
        '
        'spltLabs
        '
        Me.spltLabs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.spltLabs.Location = New System.Drawing.Point(902, 0)
        Me.spltLabs.Name = "spltLabs"
        Me.spltLabs.Size = New System.Drawing.Size(3, 356)
        Me.spltLabs.TabIndex = 9
        Me.spltLabs.TabStop = False
        '
        'pnlLabs
        '
        Me.pnlLabs.Controls.Add(Me.Panel5)
        Me.pnlLabs.Controls.Add(Me.pnlLabTitle)
        Me.pnlLabs.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLabs.Location = New System.Drawing.Point(724, 0)
        Me.pnlLabs.Name = "pnlLabs"
        Me.pnlLabs.Size = New System.Drawing.Size(178, 356)
        Me.pnlLabs.TabIndex = 8
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Controls.Add(Me.trLabs)
        Me.Panel5.Controls.Add(Me.Label72)
        Me.Panel5.Controls.Add(Me.Label73)
        Me.Panel5.Controls.Add(Me.Label32)
        Me.Panel5.Controls.Add(Me.Label33)
        Me.Panel5.Controls.Add(Me.Label34)
        Me.Panel5.Controls.Add(Me.Label35)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel5.Location = New System.Drawing.Point(0, 30)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(178, 326)
        Me.Panel5.TabIndex = 20
        '
        'trLabs
        '
        Me.trLabs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trLabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trLabs.ForeColor = System.Drawing.Color.Black
        Me.trLabs.ImageIndex = 0
        Me.trLabs.ImageList = Me.ImageList1
        Me.trLabs.ItemHeight = 19
        Me.trLabs.Location = New System.Drawing.Point(3, 3)
        Me.trLabs.Name = "trLabs"
        Me.trLabs.SelectedImageIndex = 0
        Me.trLabs.ShowLines = False
        Me.trLabs.Size = New System.Drawing.Size(174, 322)
        Me.trLabs.TabIndex = 4
        '
        'Label72
        '
        Me.Label72.BackColor = System.Drawing.Color.White
        Me.Label72.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label72.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.Location = New System.Drawing.Point(3, 1)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(174, 2)
        Me.Label72.TabIndex = 12
        Me.Label72.Text = "label1"
        '
        'Label73
        '
        Me.Label73.BackColor = System.Drawing.Color.White
        Me.Label73.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label73.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.Location = New System.Drawing.Point(1, 1)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(2, 324)
        Me.Label73.TabIndex = 11
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label32.Location = New System.Drawing.Point(1, 325)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(176, 1)
        Me.Label32.TabIndex = 8
        Me.Label32.Text = "label2"
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label33.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(0, 1)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(1, 325)
        Me.Label33.TabIndex = 7
        Me.Label33.Text = "label4"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label34.Location = New System.Drawing.Point(177, 1)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(1, 325)
        Me.Label34.TabIndex = 6
        Me.Label34.Text = "label3"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label35.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(0, 0)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(178, 1)
        Me.Label35.TabIndex = 5
        Me.Label35.Text = "label1"
        '
        'pnlLabTitle
        '
        Me.pnlLabTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLabTitle.Controls.Add(Me.Panel18)
        Me.pnlLabTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLabTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlLabTitle.Name = "pnlLabTitle"
        Me.pnlLabTitle.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlLabTitle.Size = New System.Drawing.Size(178, 30)
        Me.pnlLabTitle.TabIndex = 1
        '
        'Panel18
        '
        Me.Panel18.BackColor = System.Drawing.Color.Transparent
        Me.Panel18.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel18.Controls.Add(Me.Label56)
        Me.Panel18.Controls.Add(Me.Label57)
        Me.Panel18.Controls.Add(Me.Label4)
        Me.Panel18.Controls.Add(Me.Label58)
        Me.Panel18.Controls.Add(Me.Label59)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel18.Location = New System.Drawing.Point(0, 0)
        Me.Panel18.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(178, 27)
        Me.Panel18.TabIndex = 20
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label56.Location = New System.Drawing.Point(1, 26)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(176, 1)
        Me.Label56.TabIndex = 8
        Me.Label56.Text = "label2"
        '
        'Label57
        '
        Me.Label57.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label57.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label57.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.Location = New System.Drawing.Point(0, 1)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(1, 26)
        Me.Label57.TabIndex = 7
        Me.Label57.Text = "label4"
        '
        'Label4
        '
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(0, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(177, 26)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Labs"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label58
        '
        Me.Label58.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label58.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label58.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label58.Location = New System.Drawing.Point(177, 1)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(1, 26)
        Me.Label58.TabIndex = 6
        Me.Label58.Text = "label3"
        '
        'Label59
        '
        Me.Label59.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label59.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label59.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.Location = New System.Drawing.Point(0, 0)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(178, 1)
        Me.Label59.TabIndex = 5
        Me.Label59.Text = "label1"
        '
        'spltProcedures
        '
        Me.spltProcedures.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.spltProcedures.Location = New System.Drawing.Point(721, 0)
        Me.spltProcedures.Name = "spltProcedures"
        Me.spltProcedures.Size = New System.Drawing.Size(3, 356)
        Me.spltProcedures.TabIndex = 7
        Me.spltProcedures.TabStop = False
        '
        'pnlProcedures
        '
        Me.pnlProcedures.Controls.Add(Me.Panel4)
        Me.pnlProcedures.Controls.Add(Me.pnlprocedureTitle)
        Me.pnlProcedures.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlProcedures.Location = New System.Drawing.Point(543, 0)
        Me.pnlProcedures.Name = "pnlProcedures"
        Me.pnlProcedures.Size = New System.Drawing.Size(178, 356)
        Me.pnlProcedures.TabIndex = 6
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.trProcedures)
        Me.Panel4.Controls.Add(Me.Label70)
        Me.Panel4.Controls.Add(Me.Label71)
        Me.Panel4.Controls.Add(Me.Label27)
        Me.Panel4.Controls.Add(Me.Label28)
        Me.Panel4.Controls.Add(Me.Label29)
        Me.Panel4.Controls.Add(Me.Label31)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.Location = New System.Drawing.Point(0, 30)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(178, 326)
        Me.Panel4.TabIndex = 20
        '
        'trProcedures
        '
        Me.trProcedures.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trProcedures.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trProcedures.ForeColor = System.Drawing.Color.Black
        Me.trProcedures.ImageIndex = 0
        Me.trProcedures.ImageList = Me.ImageList1
        Me.trProcedures.ItemHeight = 19
        Me.trProcedures.Location = New System.Drawing.Point(3, 3)
        Me.trProcedures.Name = "trProcedures"
        Me.trProcedures.SelectedImageIndex = 0
        Me.trProcedures.ShowLines = False
        Me.trProcedures.Size = New System.Drawing.Size(174, 322)
        Me.trProcedures.TabIndex = 2
        '
        'Label70
        '
        Me.Label70.BackColor = System.Drawing.Color.White
        Me.Label70.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label70.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.Location = New System.Drawing.Point(3, 1)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(174, 2)
        Me.Label70.TabIndex = 12
        Me.Label70.Text = "label1"
        '
        'Label71
        '
        Me.Label71.BackColor = System.Drawing.Color.White
        Me.Label71.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label71.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.Location = New System.Drawing.Point(1, 1)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(2, 324)
        Me.Label71.TabIndex = 11
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label27.Location = New System.Drawing.Point(1, 325)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(176, 1)
        Me.Label27.TabIndex = 8
        Me.Label27.Text = "label2"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label28.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(0, 1)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(1, 325)
        Me.Label28.TabIndex = 7
        Me.Label28.Text = "label4"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label29.Location = New System.Drawing.Point(177, 1)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 325)
        Me.Label29.TabIndex = 6
        Me.Label29.Text = "label3"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(0, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(178, 1)
        Me.Label31.TabIndex = 5
        Me.Label31.Text = "label1"
        '
        'pnlprocedureTitle
        '
        Me.pnlprocedureTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlprocedureTitle.Controls.Add(Me.Panel11)
        Me.pnlprocedureTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlprocedureTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlprocedureTitle.Name = "pnlprocedureTitle"
        Me.pnlprocedureTitle.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlprocedureTitle.Size = New System.Drawing.Size(178, 30)
        Me.pnlprocedureTitle.TabIndex = 1
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.Transparent
        Me.Panel11.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel11.Controls.Add(Me.Label52)
        Me.Panel11.Controls.Add(Me.Label53)
        Me.Panel11.Controls.Add(Me.Label54)
        Me.Panel11.Controls.Add(Me.Label3)
        Me.Panel11.Controls.Add(Me.Label55)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel11.Location = New System.Drawing.Point(0, 0)
        Me.Panel11.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(178, 27)
        Me.Panel11.TabIndex = 20
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label52.Location = New System.Drawing.Point(1, 26)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(176, 1)
        Me.Label52.TabIndex = 8
        Me.Label52.Text = "label2"
        '
        'Label53
        '
        Me.Label53.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label53.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label53.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.Location = New System.Drawing.Point(0, 1)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(1, 26)
        Me.Label53.TabIndex = 7
        Me.Label53.Text = "label4"
        '
        'Label54
        '
        Me.Label54.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label54.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label54.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label54.Location = New System.Drawing.Point(177, 1)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(1, 26)
        Me.Label54.TabIndex = 6
        Me.Label54.Text = "label3"
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(0, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(178, 26)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Procedures"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.Location = New System.Drawing.Point(0, 0)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(178, 1)
        Me.Label55.TabIndex = 5
        Me.Label55.Text = "label1"
        '
        'spltHistory
        '
        Me.spltHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.spltHistory.Location = New System.Drawing.Point(540, 0)
        Me.spltHistory.Name = "spltHistory"
        Me.spltHistory.Size = New System.Drawing.Size(3, 356)
        Me.spltHistory.TabIndex = 5
        Me.spltHistory.TabStop = False
        '
        'pnlHistory
        '
        Me.pnlHistory.Controls.Add(Me.Panel3)
        Me.pnlHistory.Controls.Add(Me.pnlHistoryTitle)
        Me.pnlHistory.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlHistory.Location = New System.Drawing.Point(362, 0)
        Me.pnlHistory.Name = "pnlHistory"
        Me.pnlHistory.Size = New System.Drawing.Size(178, 356)
        Me.pnlHistory.TabIndex = 4
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.trHistory)
        Me.Panel3.Controls.Add(Me.Label68)
        Me.Panel3.Controls.Add(Me.Label69)
        Me.Panel3.Controls.Add(Me.Label23)
        Me.Panel3.Controls.Add(Me.Label24)
        Me.Panel3.Controls.Add(Me.Label25)
        Me.Panel3.Controls.Add(Me.Label26)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Location = New System.Drawing.Point(0, 30)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(178, 326)
        Me.Panel3.TabIndex = 20
        '
        'trHistory
        '
        Me.trHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trHistory.ForeColor = System.Drawing.Color.Black
        Me.trHistory.ImageIndex = 0
        Me.trHistory.ImageList = Me.ImageList1
        Me.trHistory.ItemHeight = 19
        Me.trHistory.Location = New System.Drawing.Point(3, 3)
        Me.trHistory.Name = "trHistory"
        Me.trHistory.SelectedImageIndex = 0
        Me.trHistory.ShowLines = False
        Me.trHistory.Size = New System.Drawing.Size(174, 322)
        Me.trHistory.TabIndex = 2
        '
        'Label68
        '
        Me.Label68.BackColor = System.Drawing.Color.White
        Me.Label68.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label68.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label68.Location = New System.Drawing.Point(3, 1)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(174, 2)
        Me.Label68.TabIndex = 12
        Me.Label68.Text = "label1"
        '
        'Label69
        '
        Me.Label69.BackColor = System.Drawing.Color.White
        Me.Label69.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label69.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label69.Location = New System.Drawing.Point(1, 1)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(2, 324)
        Me.Label69.TabIndex = 11
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(1, 325)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(176, 1)
        Me.Label23.TabIndex = 8
        Me.Label23.Text = "label2"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(0, 1)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(1, 325)
        Me.Label24.TabIndex = 7
        Me.Label24.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(177, 1)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 325)
        Me.Label25.TabIndex = 6
        Me.Label25.Text = "label3"
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label26.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(0, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(178, 1)
        Me.Label26.TabIndex = 5
        Me.Label26.Text = "label1"
        '
        'pnlHistoryTitle
        '
        Me.pnlHistoryTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlHistoryTitle.Controls.Add(Me.Panel10)
        Me.pnlHistoryTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHistoryTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlHistoryTitle.Name = "pnlHistoryTitle"
        Me.pnlHistoryTitle.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlHistoryTitle.Size = New System.Drawing.Size(178, 30)
        Me.pnlHistoryTitle.TabIndex = 1
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.Transparent
        Me.Panel10.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel10.Controls.Add(Me.Label48)
        Me.Panel10.Controls.Add(Me.Label49)
        Me.Panel10.Controls.Add(Me.Label2)
        Me.Panel10.Controls.Add(Me.Label50)
        Me.Panel10.Controls.Add(Me.Label51)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel10.Location = New System.Drawing.Point(0, 0)
        Me.Panel10.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(178, 27)
        Me.Panel10.TabIndex = 20
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label48.Location = New System.Drawing.Point(1, 26)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(176, 1)
        Me.Label48.TabIndex = 8
        Me.Label48.Text = "label2"
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label49.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.Location = New System.Drawing.Point(0, 1)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(1, 26)
        Me.Label49.TabIndex = 7
        Me.Label49.Text = "label4"
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(177, 26)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "History"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label50.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label50.Location = New System.Drawing.Point(177, 1)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(1, 26)
        Me.Label50.TabIndex = 6
        Me.Label50.Text = "label3"
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label51.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label51.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.Location = New System.Drawing.Point(0, 0)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(178, 1)
        Me.Label51.TabIndex = 5
        Me.Label51.Text = "label1"
        '
        'spltMedications
        '
        Me.spltMedications.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.spltMedications.Location = New System.Drawing.Point(359, 0)
        Me.spltMedications.Name = "spltMedications"
        Me.spltMedications.Size = New System.Drawing.Size(3, 356)
        Me.spltMedications.TabIndex = 3
        Me.spltMedications.TabStop = False
        '
        'pnlMedications
        '
        Me.pnlMedications.Controls.Add(Me.Panel1)
        Me.pnlMedications.Controls.Add(Me.pnlMedicationTitle)
        Me.pnlMedications.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlMedications.Location = New System.Drawing.Point(181, 0)
        Me.pnlMedications.Name = "pnlMedications"
        Me.pnlMedications.Size = New System.Drawing.Size(178, 356)
        Me.pnlMedications.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.trMedications)
        Me.Panel1.Controls.Add(Me.Label66)
        Me.Panel1.Controls.Add(Me.Label67)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 30)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(178, 326)
        Me.Panel1.TabIndex = 20
        '
        'trMedications
        '
        Me.trMedications.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trMedications.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trMedications.ForeColor = System.Drawing.Color.Black
        Me.trMedications.ImageIndex = 0
        Me.trMedications.ImageList = Me.ImageList1
        Me.trMedications.ItemHeight = 19
        Me.trMedications.Location = New System.Drawing.Point(3, 3)
        Me.trMedications.Name = "trMedications"
        Me.trMedications.SelectedImageIndex = 0
        Me.trMedications.ShowLines = False
        Me.trMedications.Size = New System.Drawing.Size(174, 322)
        Me.trMedications.TabIndex = 2
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.White
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label66.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.Location = New System.Drawing.Point(3, 1)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(174, 2)
        Me.Label66.TabIndex = 12
        Me.Label66.Text = "label1"
        '
        'Label67
        '
        Me.Label67.BackColor = System.Drawing.Color.White
        Me.Label67.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label67.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.Location = New System.Drawing.Point(1, 1)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(2, 324)
        Me.Label67.TabIndex = 11
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(1, 325)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(176, 1)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "label2"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(0, 1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(1, 325)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(177, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 325)
        Me.Label16.TabIndex = 6
        Me.Label16.Text = "label3"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(0, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(178, 1)
        Me.Label22.TabIndex = 5
        Me.Label22.Text = "label1"
        '
        'pnlMedicationTitle
        '
        Me.pnlMedicationTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMedicationTitle.Controls.Add(Me.Panel9)
        Me.pnlMedicationTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMedicationTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlMedicationTitle.Name = "pnlMedicationTitle"
        Me.pnlMedicationTitle.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlMedicationTitle.Size = New System.Drawing.Size(178, 30)
        Me.pnlMedicationTitle.TabIndex = 1
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.Transparent
        Me.Panel9.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel9.Controls.Add(Me.Label47)
        Me.Panel9.Controls.Add(Me.Label44)
        Me.Panel9.Controls.Add(Me.Label1)
        Me.Panel9.Controls.Add(Me.Label45)
        Me.Panel9.Controls.Add(Me.Label46)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(178, 27)
        Me.Panel9.TabIndex = 20
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(1, 0)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(176, 1)
        Me.Label47.TabIndex = 9
        Me.Label47.Text = "label1"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label44.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label44.Location = New System.Drawing.Point(1, 26)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(176, 1)
        Me.Label44.TabIndex = 8
        Me.Label44.Text = "label2"
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(1, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(176, 27)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Medications"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label45.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(0, 0)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(1, 27)
        Me.Label45.TabIndex = 7
        Me.Label45.Text = "label4"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label46.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label46.Location = New System.Drawing.Point(177, 0)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(1, 27)
        Me.Label46.TabIndex = 6
        Me.Label46.Text = "label3"
        '
        'spltProblems
        '
        Me.spltProblems.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.spltProblems.Location = New System.Drawing.Point(178, 0)
        Me.spltProblems.Name = "spltProblems"
        Me.spltProblems.Size = New System.Drawing.Size(3, 356)
        Me.spltProblems.TabIndex = 1
        Me.spltProblems.TabStop = False
        '
        'pnlProblems
        '
        Me.pnlProblems.Controls.Add(Me.Panel2)
        Me.pnlProblems.Controls.Add(Me.pnlProblemsTitle)
        Me.pnlProblems.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlProblems.Location = New System.Drawing.Point(0, 0)
        Me.pnlProblems.Name = "pnlProblems"
        Me.pnlProblems.Size = New System.Drawing.Size(178, 356)
        Me.pnlProblems.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.trProblemList)
        Me.Panel2.Controls.Add(Me.Label65)
        Me.Panel2.Controls.Add(Me.Label64)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 30)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(178, 326)
        Me.Panel2.TabIndex = 20
        '
        'trProblemList
        '
        Me.trProblemList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trProblemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trProblemList.ForeColor = System.Drawing.Color.Black
        Me.trProblemList.ImageIndex = 0
        Me.trProblemList.ImageList = Me.ImageList1
        Me.trProblemList.ItemHeight = 19
        Me.trProblemList.Location = New System.Drawing.Point(3, 3)
        Me.trProblemList.Name = "trProblemList"
        Me.trProblemList.SelectedImageIndex = 0
        Me.trProblemList.ShowLines = False
        Me.trProblemList.Size = New System.Drawing.Size(174, 322)
        Me.trProblemList.TabIndex = 1
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.White
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.Location = New System.Drawing.Point(3, 1)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(174, 2)
        Me.Label65.TabIndex = 10
        Me.Label65.Text = "label1"
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.Color.White
        Me.Label64.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label64.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.Location = New System.Drawing.Point(1, 1)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(2, 324)
        Me.Label64.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(1, 325)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(176, 1)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "label2"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(0, 1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 325)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(177, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 325)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "label3"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(178, 1)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "label1"
        '
        'pnlProblemsTitle
        '
        Me.pnlProblemsTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlProblemsTitle.Controls.Add(Me.Panel8)
        Me.pnlProblemsTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlProblemsTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlProblemsTitle.Name = "pnlProblemsTitle"
        Me.pnlProblemsTitle.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlProblemsTitle.Size = New System.Drawing.Size(178, 30)
        Me.pnlProblemsTitle.TabIndex = 0
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Transparent
        Me.Panel8.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Blue2007
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.Label40)
        Me.Panel8.Controls.Add(Me.Label41)
        Me.Panel8.Controls.Add(Me.Label42)
        Me.Panel8.Controls.Add(Me.lblProblems)
        Me.Panel8.Controls.Add(Me.Label43)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(178, 27)
        Me.Panel8.TabIndex = 20
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label40.Location = New System.Drawing.Point(1, 26)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(176, 1)
        Me.Label40.TabIndex = 8
        Me.Label40.Text = "label2"
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label41.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(0, 1)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(1, 26)
        Me.Label41.TabIndex = 7
        Me.Label41.Text = "label4"
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label42.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label42.Location = New System.Drawing.Point(177, 1)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(1, 26)
        Me.Label42.TabIndex = 6
        Me.Label42.Text = "label3"
        '
        'lblProblems
        '
        Me.lblProblems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblProblems.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProblems.ForeColor = System.Drawing.Color.White
        Me.lblProblems.Location = New System.Drawing.Point(0, 1)
        Me.lblProblems.Name = "lblProblems"
        Me.lblProblems.Size = New System.Drawing.Size(178, 26)
        Me.lblProblems.TabIndex = 0
        Me.lblProblems.Text = "Problems "
        Me.lblProblems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label43.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(0, 0)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(178, 1)
        Me.Label43.TabIndex = 5
        Me.Label43.Text = "label1"
        '
        'tbpProblem
        '
        Me.tbpProblem.Controls.Add(Me.Panel28)
        Me.tbpProblem.Controls.Add(Me.pnlSearchproblemList)
        Me.tbpProblem.ImageIndex = 0
        Me.tbpProblem.Location = New System.Drawing.Point(4, 26)
        Me.tbpProblem.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tbpProblem.Name = "tbpProblem"
        Me.tbpProblem.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tbpProblem.Size = New System.Drawing.Size(1080, 362)
        Me.tbpProblem.TabIndex = 1
        Me.tbpProblem.Text = "Problem List"
        Me.tbpProblem.UseVisualStyleBackColor = True
        '
        'Panel28
        '
        Me.Panel28.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel28.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel28.Controls.Add(Me.Label119)
        Me.Panel28.Controls.Add(Me.Label116)
        Me.Panel28.Controls.Add(Me.Label117)
        Me.Panel28.Controls.Add(Me.Label118)
        Me.Panel28.Controls.Add(Me.c1ProblemList)
        Me.Panel28.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel28.Location = New System.Drawing.Point(4, 27)
        Me.Panel28.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel28.Name = "Panel28"
        Me.Panel28.Size = New System.Drawing.Size(1072, 332)
        Me.Panel28.TabIndex = 20
        '
        'Label119
        '
        Me.Label119.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label119.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label119.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label119.Location = New System.Drawing.Point(1, 0)
        Me.Label119.Name = "Label119"
        Me.Label119.Size = New System.Drawing.Size(1070, 1)
        Me.Label119.TabIndex = 5
        Me.Label119.Text = "label1"
        '
        'Label116
        '
        Me.Label116.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label116.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label116.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label116.Location = New System.Drawing.Point(1, 331)
        Me.Label116.Name = "Label116"
        Me.Label116.Size = New System.Drawing.Size(1070, 1)
        Me.Label116.TabIndex = 8
        Me.Label116.Text = "label2"
        '
        'Label117
        '
        Me.Label117.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label117.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label117.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label117.Location = New System.Drawing.Point(0, 0)
        Me.Label117.Name = "Label117"
        Me.Label117.Size = New System.Drawing.Size(1, 332)
        Me.Label117.TabIndex = 7
        Me.Label117.Text = "label4"
        '
        'Label118
        '
        Me.Label118.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label118.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label118.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label118.Location = New System.Drawing.Point(1071, 0)
        Me.Label118.Name = "Label118"
        Me.Label118.Size = New System.Drawing.Size(1, 332)
        Me.Label118.TabIndex = 6
        Me.Label118.Text = "label3"
        '
        'c1ProblemList
        '
        Me.c1ProblemList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1ProblemList.AllowEditing = False
        Me.c1ProblemList.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1ProblemList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1ProblemList.ColumnInfo = "1,0,0,0,0,95,Columns:0{TextAlign:LeftCenter;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.c1ProblemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1ProblemList.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.c1ProblemList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1ProblemList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1ProblemList.Location = New System.Drawing.Point(0, 0)
        Me.c1ProblemList.Name = "c1ProblemList"
        Me.c1ProblemList.Rows.Count = 1
        Me.c1ProblemList.Rows.DefaultSize = 19
        Me.c1ProblemList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1ProblemList.Size = New System.Drawing.Size(1072, 332)
        Me.c1ProblemList.StyleInfo = resources.GetString("c1ProblemList.StyleInfo")
        Me.c1ProblemList.TabIndex = 16
        '
        'pnlSearchproblemList
        '
        Me.pnlSearchproblemList.Controls.Add(Me.Panel32)
        Me.pnlSearchproblemList.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearchproblemList.Location = New System.Drawing.Point(4, 3)
        Me.pnlSearchproblemList.Name = "pnlSearchproblemList"
        Me.pnlSearchproblemList.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSearchproblemList.Size = New System.Drawing.Size(1072, 24)
        Me.pnlSearchproblemList.TabIndex = 21
        '
        'Panel32
        '
        Me.Panel32.BackColor = System.Drawing.Color.Transparent
        Me.Panel32.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel32.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel32.Controls.Add(Me.Label128)
        Me.Panel32.Controls.Add(Me.Label129)
        Me.Panel32.Controls.Add(Me.Label130)
        Me.Panel32.Controls.Add(Me.Label131)
        Me.Panel32.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel32.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel32.Location = New System.Drawing.Point(0, 0)
        Me.Panel32.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel32.Name = "Panel32"
        Me.Panel32.Size = New System.Drawing.Size(1072, 21)
        Me.Panel32.TabIndex = 19
        '
        'Label128
        '
        Me.Label128.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label128.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label128.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label128.Location = New System.Drawing.Point(1, 20)
        Me.Label128.Name = "Label128"
        Me.Label128.Size = New System.Drawing.Size(1070, 1)
        Me.Label128.TabIndex = 8
        Me.Label128.Text = "label2"
        '
        'Label129
        '
        Me.Label129.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label129.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label129.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label129.Location = New System.Drawing.Point(0, 1)
        Me.Label129.Name = "Label129"
        Me.Label129.Size = New System.Drawing.Size(1, 20)
        Me.Label129.TabIndex = 7
        Me.Label129.Text = "label4"
        '
        'Label130
        '
        Me.Label130.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label130.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label130.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label130.Location = New System.Drawing.Point(1071, 1)
        Me.Label130.Name = "Label130"
        Me.Label130.Size = New System.Drawing.Size(1, 20)
        Me.Label130.TabIndex = 6
        Me.Label130.Text = "label3"
        '
        'Label131
        '
        Me.Label131.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label131.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label131.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label131.Location = New System.Drawing.Point(0, 0)
        Me.Label131.Name = "Label131"
        Me.Label131.Size = New System.Drawing.Size(1072, 1)
        Me.Label131.TabIndex = 5
        Me.Label131.Text = "label1"
        '
        'tbpMedications
        '
        Me.tbpMedications.Controls.Add(Me.pnldgPatientDetails)
        Me.tbpMedications.Controls.Add(Me.pnlSearchMedications)
        Me.tbpMedications.ImageIndex = 1
        Me.tbpMedications.Location = New System.Drawing.Point(4, 26)
        Me.tbpMedications.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tbpMedications.Name = "tbpMedications"
        Me.tbpMedications.Size = New System.Drawing.Size(1080, 362)
        Me.tbpMedications.TabIndex = 2
        Me.tbpMedications.Text = "Medications"
        Me.tbpMedications.UseVisualStyleBackColor = True
        '
        'pnldgPatientDetails
        '
        Me.pnldgPatientDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnldgPatientDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnldgPatientDetails.Controls.Add(Me.Label144)
        Me.pnldgPatientDetails.Controls.Add(Me.Label145)
        Me.pnldgPatientDetails.Controls.Add(Me.dgPatientDetails)
        Me.pnldgPatientDetails.Controls.Add(Me.Label146)
        Me.pnldgPatientDetails.Controls.Add(Me.Label147)
        Me.pnldgPatientDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnldgPatientDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnldgPatientDetails.Location = New System.Drawing.Point(0, 24)
        Me.pnldgPatientDetails.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnldgPatientDetails.Name = "pnldgPatientDetails"
        Me.pnldgPatientDetails.Size = New System.Drawing.Size(1080, 338)
        Me.pnldgPatientDetails.TabIndex = 22
        '
        'Label144
        '
        Me.Label144.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label144.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label144.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label144.Location = New System.Drawing.Point(1, 337)
        Me.Label144.Name = "Label144"
        Me.Label144.Size = New System.Drawing.Size(1078, 1)
        Me.Label144.TabIndex = 8
        Me.Label144.Text = "label2"
        '
        'Label145
        '
        Me.Label145.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label145.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label145.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label145.Location = New System.Drawing.Point(0, 1)
        Me.Label145.Name = "Label145"
        Me.Label145.Size = New System.Drawing.Size(1, 337)
        Me.Label145.TabIndex = 7
        Me.Label145.Text = "label4"
        '
        'dgPatientDetails
        '
        Me.dgPatientDetails.BackColor = System.Drawing.Color.Black
        Me.dgPatientDetails.BackgroundColor = System.Drawing.Color.GhostWhite
        Me.dgPatientDetails.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgPatientDetails.CaptionBackColor = System.Drawing.Color.Black
        Me.dgPatientDetails.CaptionFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgPatientDetails.CaptionForeColor = System.Drawing.SystemColors.ControlDark
        Me.dgPatientDetails.CaptionVisible = False
        Me.dgPatientDetails.DataMember = ""
        Me.dgPatientDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgPatientDetails.FlatMode = True
        Me.dgPatientDetails.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgPatientDetails.FullRowSelect = True
        Me.dgPatientDetails.GridLineColor = System.Drawing.SystemColors.ControlText
        Me.dgPatientDetails.HeaderBackColor = System.Drawing.SystemColors.ControlDark
        Me.dgPatientDetails.HeaderFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgPatientDetails.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgPatientDetails.Location = New System.Drawing.Point(0, 1)
        Me.dgPatientDetails.Name = "dgPatientDetails"
        Me.dgPatientDetails.ParentRowsBackColor = System.Drawing.SystemColors.ControlText
        Me.dgPatientDetails.ReadOnly = True
        Me.dgPatientDetails.SelectionBackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.dgPatientDetails.Size = New System.Drawing.Size(1079, 337)
        Me.dgPatientDetails.TabIndex = 18
        '
        'Label146
        '
        Me.Label146.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label146.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label146.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label146.Location = New System.Drawing.Point(1079, 1)
        Me.Label146.Name = "Label146"
        Me.Label146.Size = New System.Drawing.Size(1, 337)
        Me.Label146.TabIndex = 6
        Me.Label146.Text = "label3"
        '
        'Label147
        '
        Me.Label147.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label147.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label147.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label147.Location = New System.Drawing.Point(0, 0)
        Me.Label147.Name = "Label147"
        Me.Label147.Size = New System.Drawing.Size(1080, 1)
        Me.Label147.TabIndex = 5
        Me.Label147.Text = "label1"
        '
        'pnlSearchMedications
        '
        Me.pnlSearchMedications.Controls.Add(Me.Panel37)
        Me.pnlSearchMedications.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearchMedications.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearchMedications.Name = "pnlSearchMedications"
        Me.pnlSearchMedications.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSearchMedications.Size = New System.Drawing.Size(1080, 24)
        Me.pnlSearchMedications.TabIndex = 21
        '
        'Panel37
        '
        Me.Panel37.BackColor = System.Drawing.Color.Transparent
        Me.Panel37.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel37.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel37.Controls.Add(Me.Label140)
        Me.Panel37.Controls.Add(Me.Label141)
        Me.Panel37.Controls.Add(Me.Label142)
        Me.Panel37.Controls.Add(Me.Label143)
        Me.Panel37.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel37.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel37.Location = New System.Drawing.Point(0, 0)
        Me.Panel37.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel37.Name = "Panel37"
        Me.Panel37.Size = New System.Drawing.Size(1080, 21)
        Me.Panel37.TabIndex = 19
        '
        'Label140
        '
        Me.Label140.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label140.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label140.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label140.Location = New System.Drawing.Point(1, 20)
        Me.Label140.Name = "Label140"
        Me.Label140.Size = New System.Drawing.Size(1078, 1)
        Me.Label140.TabIndex = 8
        Me.Label140.Text = "label2"
        '
        'Label141
        '
        Me.Label141.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label141.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label141.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label141.Location = New System.Drawing.Point(0, 1)
        Me.Label141.Name = "Label141"
        Me.Label141.Size = New System.Drawing.Size(1, 20)
        Me.Label141.TabIndex = 7
        Me.Label141.Text = "label4"
        '
        'Label142
        '
        Me.Label142.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label142.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label142.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label142.Location = New System.Drawing.Point(1079, 1)
        Me.Label142.Name = "Label142"
        Me.Label142.Size = New System.Drawing.Size(1, 20)
        Me.Label142.TabIndex = 6
        Me.Label142.Text = "label3"
        '
        'Label143
        '
        Me.Label143.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label143.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label143.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label143.Location = New System.Drawing.Point(0, 0)
        Me.Label143.Name = "Label143"
        Me.Label143.Size = New System.Drawing.Size(1080, 1)
        Me.Label143.TabIndex = 5
        Me.Label143.Text = "label1"
        '
        'tbpAllergies
        '
        Me.tbpAllergies.Controls.Add(Me.pnlC1dgPatientDetails)
        Me.tbpAllergies.Controls.Add(Me.pnlSearchAllergies)
        Me.tbpAllergies.ImageIndex = 2
        Me.tbpAllergies.Location = New System.Drawing.Point(4, 26)
        Me.tbpAllergies.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tbpAllergies.Name = "tbpAllergies"
        Me.tbpAllergies.Size = New System.Drawing.Size(1080, 362)
        Me.tbpAllergies.TabIndex = 3
        Me.tbpAllergies.Text = "Allergies"
        Me.tbpAllergies.UseVisualStyleBackColor = True
        '
        'pnlC1dgPatientDetails
        '
        Me.pnlC1dgPatientDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlC1dgPatientDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlC1dgPatientDetails.Controls.Add(Me.Label120)
        Me.pnlC1dgPatientDetails.Controls.Add(Me.Label121)
        Me.pnlC1dgPatientDetails.Controls.Add(Me.Label122)
        Me.pnlC1dgPatientDetails.Controls.Add(Me.Label123)
        Me.pnlC1dgPatientDetails.Controls.Add(Me.C1dgPatientDetails)
        Me.pnlC1dgPatientDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlC1dgPatientDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlC1dgPatientDetails.Location = New System.Drawing.Point(0, 24)
        Me.pnlC1dgPatientDetails.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlC1dgPatientDetails.Name = "pnlC1dgPatientDetails"
        Me.pnlC1dgPatientDetails.Size = New System.Drawing.Size(1080, 338)
        Me.pnlC1dgPatientDetails.TabIndex = 20
        '
        'Label120
        '
        Me.Label120.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label120.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label120.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label120.Location = New System.Drawing.Point(1, 337)
        Me.Label120.Name = "Label120"
        Me.Label120.Size = New System.Drawing.Size(1078, 1)
        Me.Label120.TabIndex = 8
        Me.Label120.Text = "label2"
        '
        'Label121
        '
        Me.Label121.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label121.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label121.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label121.Location = New System.Drawing.Point(0, 1)
        Me.Label121.Name = "Label121"
        Me.Label121.Size = New System.Drawing.Size(1, 337)
        Me.Label121.TabIndex = 7
        Me.Label121.Text = "label4"
        '
        'Label122
        '
        Me.Label122.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label122.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label122.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label122.Location = New System.Drawing.Point(1079, 1)
        Me.Label122.Name = "Label122"
        Me.Label122.Size = New System.Drawing.Size(1, 337)
        Me.Label122.TabIndex = 6
        Me.Label122.Text = "label3"
        '
        'Label123
        '
        Me.Label123.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label123.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label123.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label123.Location = New System.Drawing.Point(0, 0)
        Me.Label123.Name = "Label123"
        Me.Label123.Size = New System.Drawing.Size(1080, 1)
        Me.Label123.TabIndex = 5
        Me.Label123.Text = "label1"
        '
        'C1dgPatientDetails
        '
        Me.C1dgPatientDetails.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1dgPatientDetails.AllowEditing = False
        Me.C1dgPatientDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1dgPatientDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1dgPatientDetails.ColumnInfo = "1,0,0,0,0,95,Columns:0{TextAlign:LeftCenter;}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1dgPatientDetails.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1dgPatientDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1dgPatientDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1dgPatientDetails.Location = New System.Drawing.Point(0, 0)
        Me.C1dgPatientDetails.Name = "C1dgPatientDetails"
        Me.C1dgPatientDetails.Rows.Count = 1
        Me.C1dgPatientDetails.Rows.DefaultSize = 19
        Me.C1dgPatientDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1dgPatientDetails.Size = New System.Drawing.Size(1080, 460)
        Me.C1dgPatientDetails.StyleInfo = resources.GetString("C1dgPatientDetails.StyleInfo")
        Me.C1dgPatientDetails.TabIndex = 16
        '
        'pnlSearchAllergies
        '
        Me.pnlSearchAllergies.Controls.Add(Me.Panel35)
        Me.pnlSearchAllergies.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearchAllergies.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearchAllergies.Name = "pnlSearchAllergies"
        Me.pnlSearchAllergies.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSearchAllergies.Size = New System.Drawing.Size(1080, 24)
        Me.pnlSearchAllergies.TabIndex = 21
        '
        'Panel35
        '
        Me.Panel35.BackColor = System.Drawing.Color.Transparent
        Me.Panel35.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel35.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel35.Controls.Add(Me.Label136)
        Me.Panel35.Controls.Add(Me.Label137)
        Me.Panel35.Controls.Add(Me.Label138)
        Me.Panel35.Controls.Add(Me.Label139)
        Me.Panel35.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel35.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel35.Location = New System.Drawing.Point(0, 0)
        Me.Panel35.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel35.Name = "Panel35"
        Me.Panel35.Size = New System.Drawing.Size(1080, 21)
        Me.Panel35.TabIndex = 19
        '
        'Label136
        '
        Me.Label136.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label136.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label136.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label136.Location = New System.Drawing.Point(1, 20)
        Me.Label136.Name = "Label136"
        Me.Label136.Size = New System.Drawing.Size(1078, 1)
        Me.Label136.TabIndex = 8
        Me.Label136.Text = "label2"
        '
        'Label137
        '
        Me.Label137.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label137.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label137.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label137.Location = New System.Drawing.Point(0, 1)
        Me.Label137.Name = "Label137"
        Me.Label137.Size = New System.Drawing.Size(1, 20)
        Me.Label137.TabIndex = 7
        Me.Label137.Text = "label4"
        '
        'Label138
        '
        Me.Label138.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label138.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label138.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label138.Location = New System.Drawing.Point(1079, 1)
        Me.Label138.Name = "Label138"
        Me.Label138.Size = New System.Drawing.Size(1, 20)
        Me.Label138.TabIndex = 6
        Me.Label138.Text = "label3"
        '
        'Label139
        '
        Me.Label139.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label139.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label139.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label139.Location = New System.Drawing.Point(0, 0)
        Me.Label139.Name = "Label139"
        Me.Label139.Size = New System.Drawing.Size(1080, 1)
        Me.Label139.TabIndex = 5
        Me.Label139.Text = "label1"
        '
        'tbProcedures
        '
        Me.tbProcedures.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbProcedures.Controls.Add(Me.pnltrProcedureDetails)
        Me.tbProcedures.Controls.Add(Me.pnlSearchProcedures)
        Me.tbProcedures.ImageIndex = 3
        Me.tbProcedures.Location = New System.Drawing.Point(4, 26)
        Me.tbProcedures.Name = "tbProcedures"
        Me.tbProcedures.Size = New System.Drawing.Size(1080, 362)
        Me.tbProcedures.TabIndex = 6
        Me.tbProcedures.Text = "Procedures"
        '
        'pnltrProcedureDetails
        '
        Me.pnltrProcedureDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrProcedureDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrProcedureDetails.Controls.Add(Me.trProcedureDetails)
        Me.pnltrProcedureDetails.Controls.Add(Me.Label21)
        Me.pnltrProcedureDetails.Controls.Add(Me.Label124)
        Me.pnltrProcedureDetails.Controls.Add(Me.Label126)
        Me.pnltrProcedureDetails.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnltrProcedureDetails.Controls.Add(Me.Label127)
        Me.pnltrProcedureDetails.Controls.Add(Me.Label125)
        Me.pnltrProcedureDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrProcedureDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrProcedureDetails.Location = New System.Drawing.Point(0, 24)
        Me.pnltrProcedureDetails.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnltrProcedureDetails.Name = "pnltrProcedureDetails"
        Me.pnltrProcedureDetails.Size = New System.Drawing.Size(1080, 338)
        Me.pnltrProcedureDetails.TabIndex = 40
        '
        'trProcedureDetails
        '
        Me.trProcedureDetails.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trProcedureDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trProcedureDetails.ForeColor = System.Drawing.Color.Black
        Me.trProcedureDetails.ImageIndex = 0
        Me.trProcedureDetails.ImageList = Me.ImageList1
        Me.trProcedureDetails.ItemHeight = 19
        Me.trProcedureDetails.Location = New System.Drawing.Point(5, 5)
        Me.trProcedureDetails.Name = "trProcedureDetails"
        Me.trProcedureDetails.SelectedImageIndex = 0
        Me.trProcedureDetails.ShowLines = False
        Me.trProcedureDetails.ShowPlusMinus = False
        Me.trProcedureDetails.Size = New System.Drawing.Size(1074, 332)
        Me.trProcedureDetails.TabIndex = 0
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label21.Location = New System.Drawing.Point(1, 5)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(4, 332)
        Me.Label21.TabIndex = 39
        '
        'Label124
        '
        Me.Label124.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label124.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label124.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label124.Location = New System.Drawing.Point(1, 337)
        Me.Label124.Name = "Label124"
        Me.Label124.Size = New System.Drawing.Size(1078, 1)
        Me.Label124.TabIndex = 8
        Me.Label124.Text = "label2"
        '
        'Label126
        '
        Me.Label126.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label126.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label126.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label126.Location = New System.Drawing.Point(1079, 5)
        Me.Label126.Name = "Label126"
        Me.Label126.Size = New System.Drawing.Size(1, 333)
        Me.Label126.TabIndex = 6
        Me.Label126.Text = "label3"
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(1, 1)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(1079, 4)
        Me.lbl_WhiteSpaceTop.TabIndex = 38
        '
        'Label127
        '
        Me.Label127.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label127.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label127.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label127.Location = New System.Drawing.Point(1, 0)
        Me.Label127.Name = "Label127"
        Me.Label127.Size = New System.Drawing.Size(1079, 1)
        Me.Label127.TabIndex = 5
        Me.Label127.Text = "label1"
        '
        'Label125
        '
        Me.Label125.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label125.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label125.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label125.Location = New System.Drawing.Point(0, 0)
        Me.Label125.Name = "Label125"
        Me.Label125.Size = New System.Drawing.Size(1, 338)
        Me.Label125.TabIndex = 7
        Me.Label125.Text = "label4"
        '
        'pnlSearchProcedures
        '
        Me.pnlSearchProcedures.Controls.Add(Me.Panel33)
        Me.pnlSearchProcedures.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearchProcedures.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearchProcedures.Name = "pnlSearchProcedures"
        Me.pnlSearchProcedures.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlSearchProcedures.Size = New System.Drawing.Size(1080, 24)
        Me.pnlSearchProcedures.TabIndex = 41
        '
        'Panel33
        '
        Me.Panel33.BackColor = System.Drawing.Color.Transparent
        Me.Panel33.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel33.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel33.Controls.Add(Me.Label132)
        Me.Panel33.Controls.Add(Me.Label133)
        Me.Panel33.Controls.Add(Me.Label134)
        Me.Panel33.Controls.Add(Me.Label135)
        Me.Panel33.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel33.Location = New System.Drawing.Point(0, 0)
        Me.Panel33.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel33.Name = "Panel33"
        Me.Panel33.Size = New System.Drawing.Size(1080, 21)
        Me.Panel33.TabIndex = 19
        '
        'Label132
        '
        Me.Label132.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label132.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label132.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label132.Location = New System.Drawing.Point(1, 20)
        Me.Label132.Name = "Label132"
        Me.Label132.Size = New System.Drawing.Size(1078, 1)
        Me.Label132.TabIndex = 8
        Me.Label132.Text = "label2"
        '
        'Label133
        '
        Me.Label133.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label133.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label133.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label133.Location = New System.Drawing.Point(0, 1)
        Me.Label133.Name = "Label133"
        Me.Label133.Size = New System.Drawing.Size(1, 20)
        Me.Label133.TabIndex = 7
        Me.Label133.Text = "label4"
        '
        'Label134
        '
        Me.Label134.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label134.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label134.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label134.Location = New System.Drawing.Point(1079, 1)
        Me.Label134.Name = "Label134"
        Me.Label134.Size = New System.Drawing.Size(1, 20)
        Me.Label134.TabIndex = 6
        Me.Label134.Text = "label3"
        '
        'Label135
        '
        Me.Label135.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label135.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label135.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label135.Location = New System.Drawing.Point(0, 0)
        Me.Label135.Name = "Label135"
        Me.Label135.Size = New System.Drawing.Size(1080, 1)
        Me.Label135.TabIndex = 5
        Me.Label135.Text = "label1"
        '
        'tbpLabs
        '
        Me.tbpLabs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpLabs.Controls.Add(Me.GloUC_TransactionHistory1)
        Me.tbpLabs.ImageIndex = 4
        Me.tbpLabs.Location = New System.Drawing.Point(4, 26)
        Me.tbpLabs.Name = "tbpLabs"
        Me.tbpLabs.Size = New System.Drawing.Size(1080, 362)
        Me.tbpLabs.TabIndex = 5
        Me.tbpLabs.Text = "Labs"
        '
        'GloUC_TransactionHistory1
        '
        Me.GloUC_TransactionHistory1.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.GloUC_TransactionHistory1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.GloUC_TransactionHistory1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GloUC_TransactionHistory1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GloUC_TransactionHistory1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.GloUC_TransactionHistory1.Location = New System.Drawing.Point(0, 0)
        Me.GloUC_TransactionHistory1.Name = "GloUC_TransactionHistory1"
        Me.GloUC_TransactionHistory1.Padding = New System.Windows.Forms.Padding(3)
        Me.GloUC_TransactionHistory1.Size = New System.Drawing.Size(1080, 362)
        Me.GloUC_TransactionHistory1.TabIndex = 0
        '
        'tbpImaging
        '
        Me.tbpImaging.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpImaging.Controls.Add(Me.C1OrderDetails)
        Me.tbpImaging.Controls.Add(Me.dgImaging)
        Me.tbpImaging.ImageIndex = 5
        Me.tbpImaging.Location = New System.Drawing.Point(4, 26)
        Me.tbpImaging.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tbpImaging.Name = "tbpImaging"
        Me.tbpImaging.Size = New System.Drawing.Size(1080, 362)
        Me.tbpImaging.TabIndex = 4
        Me.tbpImaging.Text = "Imaging"
        '
        'C1OrderDetails
        '
        Me.C1OrderDetails.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1OrderDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1OrderDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1OrderDetails.ColumnInfo = "1,0,0,0,0,95,Columns:0{TextAlignFixed:CenterCenter;ImageAlignFixed:CenterCenter;}" & _
            "" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1OrderDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1OrderDetails.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid
        Me.C1OrderDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1OrderDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1OrderDetails.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1OrderDetails.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1OrderDetails.Location = New System.Drawing.Point(0, 0)
        Me.C1OrderDetails.Name = "C1OrderDetails"
        Me.C1OrderDetails.Rows.Count = 1
        Me.C1OrderDetails.Rows.DefaultSize = 19
        Me.C1OrderDetails.Rows.Fixed = 0
        Me.C1OrderDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1OrderDetails.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1OrderDetails.ShowSort = False
        Me.C1OrderDetails.Size = New System.Drawing.Size(1080, 362)
        Me.C1OrderDetails.StyleInfo = resources.GetString("C1OrderDetails.StyleInfo")
        Me.C1OrderDetails.TabIndex = 38
        Me.C1OrderDetails.Tree.NodeImageCollapsed = CType(resources.GetObject("C1OrderDetails.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1OrderDetails.Tree.NodeImageExpanded = CType(resources.GetObject("C1OrderDetails.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'dgImaging
        '
        Me.dgImaging.BackColor = System.Drawing.Color.Black
        Me.dgImaging.BackgroundColor = System.Drawing.Color.GhostWhite
        Me.dgImaging.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dgImaging.CaptionBackColor = System.Drawing.Color.Black
        Me.dgImaging.CaptionFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgImaging.CaptionForeColor = System.Drawing.SystemColors.ControlDark
        Me.dgImaging.CaptionVisible = False
        Me.dgImaging.DataMember = ""
        Me.dgImaging.FlatMode = True
        Me.dgImaging.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgImaging.FullRowSelect = True
        Me.dgImaging.GridLineColor = System.Drawing.SystemColors.ControlText
        Me.dgImaging.HeaderBackColor = System.Drawing.SystemColors.ControlDark
        Me.dgImaging.HeaderFont = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgImaging.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgImaging.Location = New System.Drawing.Point(0, 467)
        Me.dgImaging.Name = "dgImaging"
        Me.dgImaging.ParentRowsBackColor = System.Drawing.SystemColors.ControlText
        Me.dgImaging.ReadOnly = True
        Me.dgImaging.SelectionBackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.dgImaging.Size = New System.Drawing.Size(1088, 44)
        Me.dgImaging.TabIndex = 19
        Me.dgImaging.Visible = False
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label17.Location = New System.Drawing.Point(4, 396)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1088, 1)
        Me.Label17.TabIndex = 12
        Me.Label17.Text = "label2"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(3, 4)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 393)
        Me.Label18.TabIndex = 11
        Me.Label18.Text = "label4"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label19.Location = New System.Drawing.Point(1092, 4)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1, 393)
        Me.Label19.TabIndex = 10
        Me.Label19.Text = "label3"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(3, 3)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1090, 1)
        Me.Label20.TabIndex = 9
        Me.Label20.Text = "label1"
        '
        'pnlPatientStrip
        '
        Me.pnlPatientStrip.Location = New System.Drawing.Point(0, 3)
        Me.pnlPatientStrip.Name = "pnlPatientStrip"
        Me.pnlPatientStrip.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlPatientStrip.Size = New System.Drawing.Size(1096, 10)
        Me.pnlPatientStrip.TabIndex = 2
        Me.pnlPatientStrip.Visible = False
        '
        'imgTreeView
        '
        Me.imgTreeView.ImageStream = CType(resources.GetObject("imgTreeView.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgTreeView.TransparentColor = System.Drawing.Color.Transparent
        Me.imgTreeView.Images.SetKeyName(0, "Order Details.ico")
        Me.imgTreeView.Images.SetKeyName(1, "Drug_02.ico")
        Me.imgTreeView.Images.SetKeyName(2, "Radiology_01.ico")
        Me.imgTreeView.Images.SetKeyName(3, "arrow_01.ico")
        Me.imgTreeView.Images.SetKeyName(4, "")
        '
        'pnl_tlsp_Top
        '
        Me.pnl_tlsp_Top.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnl_tlsp_Top.Controls.Add(Me.tstrip)
        Me.pnl_tlsp_Top.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnl_tlsp_Top.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnl_tlsp_Top.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.pnl_tlsp_Top.Location = New System.Drawing.Point(0, 0)
        Me.pnl_tlsp_Top.Name = "pnl_tlsp_Top"
        Me.pnl_tlsp_Top.Size = New System.Drawing.Size(1096, 55)
        Me.pnl_tlsp_Top.TabIndex = 18
        '
        'tstrip
        '
        Me.tstrip.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tstrip.BackgroundImage = CType(resources.GetObject("tstrip.BackgroundImage"), System.Drawing.Image)
        Me.tstrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.tstrip.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.tstrip.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.tstrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlsbtnClose})
        Me.tstrip.Location = New System.Drawing.Point(0, 0)
        Me.tstrip.Name = "tstrip"
        Me.tstrip.Size = New System.Drawing.Size(1096, 53)
        Me.tstrip.TabIndex = 0
        Me.tstrip.Text = "ToolStrip1"
        '
        'tlsbtnClose
        '
        Me.tlsbtnClose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlsbtnClose.Image = CType(resources.GetObject("tlsbtnClose.Image"), System.Drawing.Image)
        Me.tlsbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tlsbtnClose.Name = "tlsbtnClose"
        Me.tlsbtnClose.Size = New System.Drawing.Size(43, 50)
        Me.tlsbtnClose.Text = "&Close"
        Me.tlsbtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.tlsbtnClose.ToolTipText = "close"
        '
        'frmPatientSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1096, 840)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.pnl_tlsp_Top)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "frmPatientSummary"
        Me.Text = "Patient Synopsis"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlFill.ResumeLayout(False)
        Me.tbExamDMS.ResumeLayout(False)
        Me.tbExams.ResumeLayout(False)
        Me.Panel22.ResumeLayout(False)
        CType(Me.dgExams, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFilterExams.ResumeLayout(False)
        Me.pnlFilterExamsBase.ResumeLayout(False)
        Me.pnlFilterExamsBase.PerformLayout()
        Me.Panel13.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.Panel16.ResumeLayout(False)
        Me.Panel17.ResumeLayout(False)
        Me.Panel21.ResumeLayout(False)
        Me.Panel21.PerformLayout()
        Me.Panel20.ResumeLayout(False)
        Me.Panel20.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel12.ResumeLayout(False)
        Me.tbDMS.ResumeLayout(False)
        Me.pnlScannedDocs.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.pnlDocument_CategorisedDocument.ResumeLayout(False)
        Me.Panel27.ResumeLayout(False)
        Me.Panel27.PerformLayout()
        CType(Me.c1CategorisedDocuments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDocument_CategorisedDocument_MenuOption.ResumeLayout(False)
        Me.Panel24.ResumeLayout(False)
        CType(Me.ImgBtn_CategorisedDocument_MenuOptionOk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDocument_CategorisedDocument_MonthYear.ResumeLayout(False)
        Me.pnlDocument_CategorisedDocument_Month.ResumeLayout(False)
        Me.pnlDocument_CategorisedDocument_Month.PerformLayout()
        Me.pnlDocument_CategorisedDocument_Cmd_YearRefresh.ResumeLayout(False)
        CType(Me.ImgBtn_CategorisedDocument_YearRefresh, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDocument_CategorisedDocument_Year.ResumeLayout(False)
        Me.pnlDocument_CategorisedDocument_HdrCmd.ResumeLayout(False)
        Me.Panel26.ResumeLayout(False)
        Me.pnlDocument_CategorisedDocument_Cmd.ResumeLayout(False)
        Me.pnlDocument_CategorisedDocument_Cmd_MenuOption.ResumeLayout(False)
        Me.pnlDocument_CategorisedDocument_Cmd_MenuOption.PerformLayout()
        CType(Me.ImgBtn_CategorisedDocument_MenuOption, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDocument_CategorisedDocument_Cmd_Year.ResumeLayout(False)
        Me.pnlDocument_CategorisedDocument_Cmd_Year.PerformLayout()
        CType(Me.ImgBtn_CategorisedDocument_Year, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDocument_CategorisedDocument_Cmd_Scan.ResumeLayout(False)
        Me.pnlDocument_CategorisedDocument_Cmd_Scan.PerformLayout()
        CType(Me.ImgBtn_CategorisedDocument_Scan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDocument_CategorisedDocument_Cmd_Import.ResumeLayout(False)
        Me.pnlDocument_CategorisedDocument_Cmd_Import.PerformLayout()
        CType(Me.ImgBtn_CategorisedDocument_Import, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDocument_CategorisedDocument_Cmd_Category.ResumeLayout(False)
        Me.pnlDocument_CategorisedDocument_Cmd_Category.PerformLayout()
        CType(Me.ImgBtn_CategorisedDocument_Category, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDocument_CategorisedDocument_Cmd_Refresh.ResumeLayout(False)
        CType(Me.ImgBtn_CategorisedDocument_Refresh, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlScannedDocsHeader.ResumeLayout(False)
        Me.Panel25.ResumeLayout(False)
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDMSSearch.ResumeLayout(False)
        Me.Panel23.ResumeLayout(False)
        Me.Panel23.PerformLayout()
        Me.pnlTop.ResumeLayout(False)
        Me.tbSummary.ResumeLayout(False)
        Me.tbpSummary.ResumeLayout(False)
        Me.pnlSummary.ResumeLayout(False)
        Me.pnlImaging.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.pnlImagingTitle.ResumeLayout(False)
        Me.Panel19.ResumeLayout(False)
        Me.pnlLabs.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.pnlLabTitle.ResumeLayout(False)
        Me.Panel18.ResumeLayout(False)
        Me.pnlProcedures.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.pnlprocedureTitle.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.pnlHistory.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlHistoryTitle.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.pnlMedications.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlMedicationTitle.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.pnlProblems.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlProblemsTitle.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.tbpProblem.ResumeLayout(False)
        Me.Panel28.ResumeLayout(False)
        CType(Me.c1ProblemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearchproblemList.ResumeLayout(False)
        Me.Panel32.ResumeLayout(False)
        Me.tbpMedications.ResumeLayout(False)
        Me.pnldgPatientDetails.ResumeLayout(False)
        CType(Me.dgPatientDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearchMedications.ResumeLayout(False)
        Me.Panel37.ResumeLayout(False)
        Me.tbpAllergies.ResumeLayout(False)
        Me.pnlC1dgPatientDetails.ResumeLayout(False)
        CType(Me.C1dgPatientDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearchAllergies.ResumeLayout(False)
        Me.Panel35.ResumeLayout(False)
        Me.tbProcedures.ResumeLayout(False)
        Me.pnltrProcedureDetails.ResumeLayout(False)
        Me.pnlSearchProcedures.ResumeLayout(False)
        Me.Panel33.ResumeLayout(False)
        Me.tbpLabs.ResumeLayout(False)
        Me.tbpImaging.ResumeLayout(False)
        CType(Me.C1OrderDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgImaging, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnl_tlsp_Top.ResumeLayout(False)
        Me.pnl_tlsp_Top.PerformLayout()
        Me.tstrip.ResumeLayout(False)
        Me.tstrip.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents imgTreeView As System.Windows.Forms.ImageList
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents tbSummary As System.Windows.Forms.TabControl
    Friend WithEvents tbpSummary As System.Windows.Forms.TabPage
    Friend WithEvents pnlSummary As System.Windows.Forms.Panel
    Friend WithEvents pnlImaging As System.Windows.Forms.Panel
    Friend WithEvents trImaging As System.Windows.Forms.TreeView
    Friend WithEvents pnlImagingTitle As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents spltLabs As System.Windows.Forms.Splitter
    Friend WithEvents pnlLabs As System.Windows.Forms.Panel
    Friend WithEvents trLabs As System.Windows.Forms.TreeView
    Friend WithEvents pnlLabTitle As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents spltProcedures As System.Windows.Forms.Splitter
    Friend WithEvents pnlProcedures As System.Windows.Forms.Panel
    Friend WithEvents trProcedures As System.Windows.Forms.TreeView
    Friend WithEvents pnlprocedureTitle As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents spltHistory As System.Windows.Forms.Splitter
    Friend WithEvents pnlHistory As System.Windows.Forms.Panel
    Friend WithEvents trHistory As System.Windows.Forms.TreeView
    Friend WithEvents pnlHistoryTitle As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents spltMedications As System.Windows.Forms.Splitter
    Friend WithEvents pnlMedications As System.Windows.Forms.Panel
    Friend WithEvents trMedications As System.Windows.Forms.TreeView
    Friend WithEvents pnlMedicationTitle As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents spltProblems As System.Windows.Forms.Splitter
    Friend WithEvents pnlProblems As System.Windows.Forms.Panel
    Friend WithEvents trProblemList As System.Windows.Forms.TreeView
    Friend WithEvents pnlProblemsTitle As System.Windows.Forms.Panel
    Friend WithEvents lblProblems As System.Windows.Forms.Label
    Friend WithEvents tbpProblem As System.Windows.Forms.TabPage
    Friend WithEvents c1ProblemList As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents tbpMedications As System.Windows.Forms.TabPage
    Friend WithEvents dgPatientDetails As gloEMR.clsDataGrid
    Friend WithEvents tbpAllergies As System.Windows.Forms.TabPage
    Friend WithEvents C1dgPatientDetails As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents tbProcedures As System.Windows.Forms.TabPage
    Friend WithEvents trProcedureDetails As System.Windows.Forms.TreeView
    Friend WithEvents tbpLabs As System.Windows.Forms.TabPage
    Friend WithEvents GloUC_TransactionHistory1 As gloUserControlLibrary.gloUC_TransactionHistory
    Friend WithEvents tbpImaging As System.Windows.Forms.TabPage
    Friend WithEvents C1OrderDetails As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents dgImaging As gloEMR.clsDataGrid
    Friend WithEvents pnlPatientStrip As System.Windows.Forms.Panel
    Friend WithEvents pnlFill As System.Windows.Forms.Panel
    Friend WithEvents tbExamDMS As System.Windows.Forms.TabControl
    Friend WithEvents tbExams As System.Windows.Forms.TabPage
    Friend WithEvents dgExams As gloEMR.clsDataGrid
    Friend WithEvents pnlFilterExamsBase As System.Windows.Forms.Panel
    Friend WithEvents btnsearch As System.Windows.Forms.Button
    Friend WithEvents txtExamName As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents cmbExamProvider As System.Windows.Forms.ComboBox
    Friend WithEvents lblProvider As System.Windows.Forms.Label
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents cmbExamtype As System.Windows.Forms.ComboBox
    Friend WithEvents lblcmbType As System.Windows.Forms.Label
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblto As System.Windows.Forms.Label
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Friend WithEvents txtRefillRequest As System.Windows.Forms.TextBox
    Friend WithEvents txtError As System.Windows.Forms.TextBox
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents btnbrwError As System.Windows.Forms.Button
    Friend WithEvents btnbrwRefill As System.Windows.Forms.Button
    Friend WithEvents tbDMS As System.Windows.Forms.TabPage
    Friend WithEvents pnlScannedDocs As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents pnlDocument_CategorisedDocument As System.Windows.Forms.Panel
    Friend WithEvents txtPatientID As System.Windows.Forms.TextBox
    Friend WithEvents c1CategorisedDocuments As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlDocument_CategorisedDocument_MenuOption As System.Windows.Forms.Panel
    Friend WithEvents chkCategorisedMenu_12Month As System.Windows.Forms.CheckBox
    Friend WithEvents ImgBtn_CategorisedDocument_MenuOptionOk As System.Windows.Forms.PictureBox
    Friend WithEvents chkCategorisedMenu_Merge As System.Windows.Forms.CheckBox
    Friend WithEvents pnlDocument_CategorisedDocument_MonthYear As System.Windows.Forms.Panel
    Friend WithEvents pnlDocument_CategorisedDocument_Month As System.Windows.Forms.Panel
    Friend WithEvents pnlDocument_CategorisedDocument_Cmd_YearRefresh As System.Windows.Forms.Panel
    Friend WithEvents ImgBtn_CategorisedDocument_YearRefresh As System.Windows.Forms.PictureBox
    Friend WithEvents txtCategorisedYear As System.Windows.Forms.TextBox
    Friend WithEvents pnlDocument_CategorisedDocument_Year As System.Windows.Forms.Panel
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents pnlDocument_CategorisedDocument_HdrCmd As System.Windows.Forms.Panel
    Friend WithEvents lblDocument_CategorisedDocument_Hdr As System.Windows.Forms.Label
    Friend WithEvents pnlDocument_CategorisedDocument_Cmd As System.Windows.Forms.Panel
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents pnlDocument_CategorisedDocument_Cmd_MenuOption As System.Windows.Forms.Panel
    Friend WithEvents ImgBtn_CategorisedDocument_MenuOption As System.Windows.Forms.PictureBox
    Friend WithEvents pnlDocument_CategorisedDocument_Cmd_Year As System.Windows.Forms.Panel
    Friend WithEvents ImgBtn_CategorisedDocument_Year As System.Windows.Forms.PictureBox
    Friend WithEvents pnlDocument_CategorisedDocument_Cmd_Scan As System.Windows.Forms.Panel
    Friend WithEvents ImgBtn_CategorisedDocument_Scan As System.Windows.Forms.PictureBox
    Friend WithEvents pnlDocument_CategorisedDocument_Cmd_Import As System.Windows.Forms.Panel
    Friend WithEvents ImgBtn_CategorisedDocument_Import As System.Windows.Forms.PictureBox
    Friend WithEvents pnlDocument_CategorisedDocument_Cmd_Category As System.Windows.Forms.Panel
    Friend WithEvents ImgBtn_CategorisedDocument_Category As System.Windows.Forms.PictureBox
    Friend WithEvents pnlDocument_CategorisedDocument_Cmd_Refresh As System.Windows.Forms.Panel
    Friend WithEvents ImgBtn_CategorisedDocument_Refresh As System.Windows.Forms.PictureBox
    Friend WithEvents trvscandoc As System.Windows.Forms.TreeView
    Friend WithEvents pnlScannedDocsHeader As System.Windows.Forms.Panel
    Friend WithEvents lblScannedDocs As System.Windows.Forms.Label
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents pnlDMSSearch As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtSearchCriteria As System.Windows.Forms.TextBox
    Friend WithEvents cmbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents lblsearch As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Private WithEvents pnl_tlsp_Top As System.Windows.Forms.Panel
    Friend WithEvents tstrip As System.Windows.Forms.ToolStrip
    Friend WithEvents tlsbtnClose As System.Windows.Forms.ToolStripButton
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Private WithEvents Label36 As System.Windows.Forms.Label
    Private WithEvents Label37 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Private WithEvents Label60 As System.Windows.Forms.Label
    Private WithEvents Label61 As System.Windows.Forms.Label
    Private WithEvents Label62 As System.Windows.Forms.Label
    Private WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label33 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Private WithEvents Label56 As System.Windows.Forms.Label
    Private WithEvents Label57 As System.Windows.Forms.Label
    Private WithEvents Label58 As System.Windows.Forms.Label
    Private WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Private WithEvents Label52 As System.Windows.Forms.Label
    Private WithEvents Label53 As System.Windows.Forms.Label
    Private WithEvents Label54 As System.Windows.Forms.Label
    Private WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Private WithEvents Label48 As System.Windows.Forms.Label
    Private WithEvents Label49 As System.Windows.Forms.Label
    Private WithEvents Label50 As System.Windows.Forms.Label
    Private WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Private WithEvents Label44 As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Private WithEvents Label47 As System.Windows.Forms.Label
    Private WithEvents Label74 As System.Windows.Forms.Label
    Private WithEvents Label75 As System.Windows.Forms.Label
    Private WithEvents Label72 As System.Windows.Forms.Label
    Private WithEvents Label73 As System.Windows.Forms.Label
    Private WithEvents Label70 As System.Windows.Forms.Label
    Private WithEvents Label71 As System.Windows.Forms.Label
    Private WithEvents Label68 As System.Windows.Forms.Label
    Private WithEvents Label69 As System.Windows.Forms.Label
    Private WithEvents Label66 As System.Windows.Forms.Label
    Private WithEvents Label67 As System.Windows.Forms.Label
    Private WithEvents Label65 As System.Windows.Forms.Label
    Private WithEvents Label64 As System.Windows.Forms.Label
    Private WithEvents Label76 As System.Windows.Forms.Label
    Private WithEvents Label77 As System.Windows.Forms.Label
    Private WithEvents Label78 As System.Windows.Forms.Label
    Private WithEvents Label79 As System.Windows.Forms.Label
    Private WithEvents Label80 As System.Windows.Forms.Label
    Private WithEvents Label81 As System.Windows.Forms.Label
    Private WithEvents Label82 As System.Windows.Forms.Label
    Private WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents pnlFilterExams As System.Windows.Forms.Panel
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents Panel22 As System.Windows.Forms.Panel
    Private WithEvents Label85 As System.Windows.Forms.Label
    Private WithEvents Label86 As System.Windows.Forms.Label
    Private WithEvents Label87 As System.Windows.Forms.Label
    Private WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents Panel23 As System.Windows.Forms.Panel
    Private WithEvents Label89 As System.Windows.Forms.Label
    Private WithEvents Label90 As System.Windows.Forms.Label
    Private WithEvents Label91 As System.Windows.Forms.Label
    Private WithEvents Label92 As System.Windows.Forms.Label
    Friend WithEvents Label93 As System.Windows.Forms.Label
    Friend WithEvents Panel24 As System.Windows.Forms.Panel
    Private WithEvents Label94 As System.Windows.Forms.Label
    Private WithEvents Label95 As System.Windows.Forms.Label
    Private WithEvents Label96 As System.Windows.Forms.Label
    Private WithEvents Label97 As System.Windows.Forms.Label
    Private WithEvents Label99 As System.Windows.Forms.Label
    Private WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents Panel26 As System.Windows.Forms.Panel
    Private WithEvents Label104 As System.Windows.Forms.Label
    Private WithEvents Label105 As System.Windows.Forms.Label
    Private WithEvents Label106 As System.Windows.Forms.Label
    Private WithEvents Label107 As System.Windows.Forms.Label
    Friend WithEvents Panel25 As System.Windows.Forms.Panel
    Private WithEvents Label102 As System.Windows.Forms.Label
    Private WithEvents Label100 As System.Windows.Forms.Label
    Private WithEvents Label101 As System.Windows.Forms.Label
    Private WithEvents Label103 As System.Windows.Forms.Label
    Friend WithEvents Panel27 As System.Windows.Forms.Panel
    Private WithEvents Label112 As System.Windows.Forms.Label
    Private WithEvents Label113 As System.Windows.Forms.Label
    Private WithEvents Label114 As System.Windows.Forms.Label
    Private WithEvents Label115 As System.Windows.Forms.Label
    Private WithEvents Label108 As System.Windows.Forms.Label
    Private WithEvents Label109 As System.Windows.Forms.Label
    Private WithEvents Label110 As System.Windows.Forms.Label
    Private WithEvents Label111 As System.Windows.Forms.Label
    Friend WithEvents Panel28 As System.Windows.Forms.Panel
    Private WithEvents Label119 As System.Windows.Forms.Label
    Private WithEvents Label116 As System.Windows.Forms.Label
    Private WithEvents Label117 As System.Windows.Forms.Label
    Private WithEvents Label118 As System.Windows.Forms.Label
    Friend WithEvents pnlC1dgPatientDetails As System.Windows.Forms.Panel
    Private WithEvents Label120 As System.Windows.Forms.Label
    Private WithEvents Label121 As System.Windows.Forms.Label
    Private WithEvents Label122 As System.Windows.Forms.Label
    Private WithEvents Label123 As System.Windows.Forms.Label
    Friend WithEvents pnltrProcedureDetails As System.Windows.Forms.Panel
    Private WithEvents Label124 As System.Windows.Forms.Label
    Private WithEvents Label126 As System.Windows.Forms.Label
    Private WithEvents Label127 As System.Windows.Forms.Label
    Private WithEvents Label125 As System.Windows.Forms.Label
    Friend WithEvents pnlSearchproblemList As System.Windows.Forms.Panel
    Friend WithEvents Panel32 As System.Windows.Forms.Panel
    Private WithEvents Label128 As System.Windows.Forms.Label
    Private WithEvents Label129 As System.Windows.Forms.Label
    Private WithEvents Label130 As System.Windows.Forms.Label
    Private WithEvents Label131 As System.Windows.Forms.Label
    Friend WithEvents pnldgPatientDetails As System.Windows.Forms.Panel
    Private WithEvents Label144 As System.Windows.Forms.Label
    Private WithEvents Label145 As System.Windows.Forms.Label
    Private WithEvents Label146 As System.Windows.Forms.Label
    Private WithEvents Label147 As System.Windows.Forms.Label
    Friend WithEvents pnlSearchMedications As System.Windows.Forms.Panel
    Friend WithEvents Panel37 As System.Windows.Forms.Panel
    Private WithEvents Label140 As System.Windows.Forms.Label
    Private WithEvents Label141 As System.Windows.Forms.Label
    Private WithEvents Label142 As System.Windows.Forms.Label
    Private WithEvents Label143 As System.Windows.Forms.Label
    Friend WithEvents pnlSearchAllergies As System.Windows.Forms.Panel
    Friend WithEvents Panel35 As System.Windows.Forms.Panel
    Private WithEvents Label136 As System.Windows.Forms.Label
    Private WithEvents Label137 As System.Windows.Forms.Label
    Private WithEvents Label138 As System.Windows.Forms.Label
    Private WithEvents Label139 As System.Windows.Forms.Label
    Friend WithEvents pnlSearchProcedures As System.Windows.Forms.Panel
    Friend WithEvents Panel33 As System.Windows.Forms.Panel
    Private WithEvents Label132 As System.Windows.Forms.Label
    Private WithEvents Label133 As System.Windows.Forms.Label
    Private WithEvents Label134 As System.Windows.Forms.Label
    Private WithEvents Label135 As System.Windows.Forms.Label
End Class
