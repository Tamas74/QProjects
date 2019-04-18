<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class gloUCPatientSynopsis
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            oMDI = Nothing

            Dim dtpControls() As System.Windows.Forms.DateTimePicker = {dtpExamTo, dtpExamFrom, dtpToDate, dtpFromDate}
            Dim cntControls() As System.Windows.Forms.Control = {dtpExamTo, dtpExamFrom, dtpToDate, dtpFromDate}
            Dim CmpControls() As System.Windows.Forms.ContextMenuStrip = {cMnuPatient, C1MedicationDetails.ContextMenuStrip, C1Dignosis.ContextMenuStrip}

            If disposing AndAlso components IsNot Nothing Then
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



                If (IsNothing(CmpControls) = False) Then
                    If CmpControls.Length > 0 Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(CmpControls)
                    End If
                End If

                If (IsNothing(CmpControls) = False) Then
                    If CmpControls.Length > 0 Then
                        gloGlobal.cEventHelper.DisposeContextMenuStrip(CmpControls)
                    End If
                End If

                If (IsNothing(cMnuPatient.Items) = False) Then
                    cMnuPatient.Items.Clear()
                End If

                If (IsNothing(c2) = False) Then
                    c2.Dispose()
                    c2 = Nothing
                End If

                If IsNothing(dtStatusFillExamTypeCombo) = False Then
                    dtStatusFillExamTypeCombo.Dispose()
                    dtStatusFillExamTypeCombo = Nothing
                End If

                If (IsNothing(GloUC_TransactionHistory1) = False) Then
                    RemoveHandler GloUC_TransactionHistory1.gUC_ViewDocument, AddressOf gloLabUC_Transaction1_gUC_ViewDocument
                    Try
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(GloUC_TransactionHistory1)
                        GloUC_TransactionHistory1.Dispose()
                        GloUC_TransactionHistory1 = Nothing
                    Catch ex As Exception

                    End Try

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(gloUCPatientSynopsis))
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.tbSummary = New System.Windows.Forms.TabControl()
        Me.tbpSummary = New System.Windows.Forms.TabPage()
        Me.pnlSummary = New System.Windows.Forms.Panel()
        Me.pnlImplant = New System.Windows.Forms.Panel()
        Me.pnltrImplant = New System.Windows.Forms.Panel()
        Me.trImplant = New System.Windows.Forms.TreeView()
        Me.ImgPatientTab = New System.Windows.Forms.ImageList(Me.components)
        Me.Label98 = New System.Windows.Forms.Label()
        Me.Label99 = New System.Windows.Forms.Label()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.Label101 = New System.Windows.Forms.Label()
        Me.Label102 = New System.Windows.Forms.Label()
        Me.Label103 = New System.Windows.Forms.Label()
        Me.pnllblImplantHeader = New System.Windows.Forms.Panel()
        Me.pnllblImplant = New System.Windows.Forms.Panel()
        Me.btnImplantExpand = New System.Windows.Forms.Button()
        Me.Label104 = New System.Windows.Forms.Label()
        Me.Label105 = New System.Windows.Forms.Label()
        Me.lblImplant = New System.Windows.Forms.Label()
        Me.Label107 = New System.Windows.Forms.Label()
        Me.Label108 = New System.Windows.Forms.Label()
        Me.splImagingST = New System.Windows.Forms.Splitter()
        Me.pnlImagingST = New System.Windows.Forms.Panel()
        Me.pnltrImagingST = New System.Windows.Forms.Panel()
        Me.trimagingecho = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.trImagingST = New System.Windows.Forms.TreeView()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.pnllblImagingSTHeader = New System.Windows.Forms.Panel()
        Me.pnllblImagingST = New System.Windows.Forms.Panel()
        Me.btnImagSTExpand = New System.Windows.Forms.Button()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.lblImaging = New System.Windows.Forms.Label()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.Label97 = New System.Windows.Forms.Label()
        Me.spltLabs = New System.Windows.Forms.Splitter()
        Me.pnlLabs = New System.Windows.Forms.Panel()
        Me.pnltrLabs = New System.Windows.Forms.Panel()
        Me.trLabs = New System.Windows.Forms.TreeView()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.pnlLabTitle = New System.Windows.Forms.Panel()
        Me.pnllblLabs = New System.Windows.Forms.Panel()
        Me.btnLabExpand = New System.Windows.Forms.Button()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.lblLabs = New System.Windows.Forms.Label()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.spltProcedures = New System.Windows.Forms.Splitter()
        Me.pnlProcedures = New System.Windows.Forms.Panel()
        Me.pnltrProcedures = New System.Windows.Forms.Panel()
        Me.trProcedures = New System.Windows.Forms.TreeView()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.pnlprocedureTitle = New System.Windows.Forms.Panel()
        Me.pnllblProcedures = New System.Windows.Forms.Panel()
        Me.btnProcedExpand = New System.Windows.Forms.Button()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.lblProcedures = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.spltHistory = New System.Windows.Forms.Splitter()
        Me.pnlHistory = New System.Windows.Forms.Panel()
        Me.pnltrHistory = New System.Windows.Forms.Panel()
        Me.trHistory = New System.Windows.Forms.TreeView()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.pnlHistoryTitle = New System.Windows.Forms.Panel()
        Me.pnllblHistory = New System.Windows.Forms.Panel()
        Me.btnHistExpand = New System.Windows.Forms.Button()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.lblHistory = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.spltMedications = New System.Windows.Forms.Splitter()
        Me.pnlMedications = New System.Windows.Forms.Panel()
        Me.pnltrMedications = New System.Windows.Forms.Panel()
        Me.trMedications = New System.Windows.Forms.TreeView()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.pnlMedicationTitle = New System.Windows.Forms.Panel()
        Me.pnllblMedications = New System.Windows.Forms.Panel()
        Me.btnMedExpand = New System.Windows.Forms.Button()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.lblMedications = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.spltProblems = New System.Windows.Forms.Splitter()
        Me.pnlProblems = New System.Windows.Forms.Panel()
        Me.pnltrProblemList = New System.Windows.Forms.Panel()
        Me.trProblemList = New System.Windows.Forms.TreeView()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.pnlProblemsTitle = New System.Windows.Forms.Panel()
        Me.pnllblProblems = New System.Windows.Forms.Panel()
        Me.btnProbExpand = New System.Windows.Forms.Button()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.lblProblems = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.tbpProblem = New System.Windows.Forms.TabPage()
        Me.Panel28 = New System.Windows.Forms.Panel()
        Me.Label119 = New System.Windows.Forms.Label()
        Me.Label116 = New System.Windows.Forms.Label()
        Me.Label117 = New System.Windows.Forms.Label()
        Me.Label118 = New System.Windows.Forms.Label()
        Me.c1ProblemList = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlSearchproblemList = New System.Windows.Forms.Panel()
        Me.pnlsearchProblems = New System.Windows.Forms.Panel()
        Me.Label128 = New System.Windows.Forms.Label()
        Me.Label129 = New System.Windows.Forms.Label()
        Me.Label130 = New System.Windows.Forms.Label()
        Me.Label131 = New System.Windows.Forms.Label()
        Me.tbpMedications = New System.Windows.Forms.TabPage()
        Me.pnldgPatientDetails = New System.Windows.Forms.Panel()
        Me.C1MedicationDetails = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label144 = New System.Windows.Forms.Label()
        Me.Label145 = New System.Windows.Forms.Label()
        Me.Label146 = New System.Windows.Forms.Label()
        Me.Label147 = New System.Windows.Forms.Label()
        Me.pnlSearchMedications = New System.Windows.Forms.Panel()
        Me.pnlSearchMed = New System.Windows.Forms.Panel()
        Me.Label140 = New System.Windows.Forms.Label()
        Me.Label141 = New System.Windows.Forms.Label()
        Me.Label142 = New System.Windows.Forms.Label()
        Me.Label143 = New System.Windows.Forms.Label()
        Me.tbpAllergies = New System.Windows.Forms.TabPage()
        Me.pnlC1dgPatientDetails = New System.Windows.Forms.Panel()
        Me.Label120 = New System.Windows.Forms.Label()
        Me.Label121 = New System.Windows.Forms.Label()
        Me.Label122 = New System.Windows.Forms.Label()
        Me.Label123 = New System.Windows.Forms.Label()
        Me.C1dgPatientDetails = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlSearchAllergies = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.tbProcedures = New System.Windows.Forms.TabPage()
        Me.pnltrProcedureDetails = New System.Windows.Forms.Panel()
        Me.pnlNormalSearchProc = New System.Windows.Forms.Panel()
        Me.Label163 = New System.Windows.Forms.Label()
        Me.Label160 = New System.Windows.Forms.Label()
        Me.C1Dignosis = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.trProcedureDetails = New System.Windows.Forms.TreeView()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label124 = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceTop = New System.Windows.Forms.Label()
        Me.Label127 = New System.Windows.Forms.Label()
        Me.Label125 = New System.Windows.Forms.Label()
        Me.Label126 = New System.Windows.Forms.Label()
        Me.pnlSearchProcedures = New System.Windows.Forms.Panel()
        Me.pnlSearchProc = New System.Windows.Forms.Panel()
        Me.Label164 = New System.Windows.Forms.Label()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.txtsearchProcedures = New gloUserControlLibrary.gloSearchTextBox()
        Me.Label187 = New System.Windows.Forms.Label()
        Me.Label188 = New System.Windows.Forms.Label()
        Me.Label189 = New System.Windows.Forms.Label()
        Me.btnClearProcedures = New System.Windows.Forms.Button()
        Me.Label190 = New System.Windows.Forms.Label()
        Me.pnlDateSearch = New System.Windows.Forms.Panel()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.Label159 = New System.Windows.Forms.Label()
        Me.cmbCriteria = New System.Windows.Forms.ComboBox()
        Me.lblSearchBy = New System.Windows.Forms.Label()
        Me.Label132 = New System.Windows.Forms.Label()
        Me.Label133 = New System.Windows.Forms.Label()
        Me.Label134 = New System.Windows.Forms.Label()
        Me.Label135 = New System.Windows.Forms.Label()
        Me.tbpLabs = New System.Windows.Forms.TabPage()
        Me.pnlLabDetails = New System.Windows.Forms.Panel()
        Me.tbpImaging = New System.Windows.Forms.TabPage()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.Label165 = New System.Windows.Forms.Label()
        Me.Label162 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.C1OrderDetails = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.pnlSearchImaging = New System.Windows.Forms.Panel()
        Me.Label166 = New System.Windows.Forms.Label()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label180 = New System.Windows.Forms.Label()
        Me.Label181 = New System.Windows.Forms.Label()
        Me.Label182 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Label183 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.tbpImagingST = New System.Windows.Forms.TabPage()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label168 = New System.Windows.Forms.Label()
        Me.Label161 = New System.Windows.Forms.Label()
        Me.Label115 = New System.Windows.Forms.Label()
        Me.Label136 = New System.Windows.Forms.Label()
        Me.C1CV_StressTest = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label137 = New System.Windows.Forms.Label()
        Me.Label138 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label167 = New System.Windows.Forms.Label()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.txtImagingSearch = New System.Windows.Forms.TextBox()
        Me.Label172 = New System.Windows.Forms.Label()
        Me.Label174 = New System.Windows.Forms.Label()
        Me.Label175 = New System.Windows.Forms.Label()
        Me.btnClearImaging = New System.Windows.Forms.Button()
        Me.Label176 = New System.Windows.Forms.Label()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.Label106 = New System.Windows.Forms.Label()
        Me.Label109 = New System.Windows.Forms.Label()
        Me.Label110 = New System.Windows.Forms.Label()
        Me.tbpImplant = New System.Windows.Forms.TabPage()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label139 = New System.Windows.Forms.Label()
        Me.Label148 = New System.Windows.Forms.Label()
        Me.C1Cardiology = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label149 = New System.Windows.Forms.Label()
        Me.Label150 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.pnlSearchImplant = New System.Windows.Forms.Panel()
        Me.Label111 = New System.Windows.Forms.Label()
        Me.Label112 = New System.Windows.Forms.Label()
        Me.Label113 = New System.Windows.Forms.Label()
        Me.Label114 = New System.Windows.Forms.Label()
        Me.tbpEjectFrac = New System.Windows.Forms.TabPage()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Label155 = New System.Windows.Forms.Label()
        Me.Label156 = New System.Windows.Forms.Label()
        Me.cfgEjectionFraction = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.Label157 = New System.Windows.Forms.Label()
        Me.Label158 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.pnlSearchEjection = New System.Windows.Forms.Panel()
        Me.Label151 = New System.Windows.Forms.Label()
        Me.Label152 = New System.Windows.Forms.Label()
        Me.Label153 = New System.Windows.Forms.Label()
        Me.Label154 = New System.Windows.Forms.Label()
        Me.pnlImaging = New System.Windows.Forms.Panel()
        Me.splImaging = New System.Windows.Forms.Splitter()
        Me.pnltrImaging = New System.Windows.Forms.Panel()
        Me.trImaging = New System.Windows.Forms.TreeView()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.pnlImagingTitle = New System.Windows.Forms.Panel()
        Me.pnllblImaging = New System.Windows.Forms.Panel()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.lblOrders = New System.Windows.Forms.Label()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.pnlFill = New System.Windows.Forms.Panel()
        Me.tbExamDMS = New System.Windows.Forms.TabControl()
        Me.tbExams = New System.Windows.Forms.TabPage()
        Me.Panel22 = New System.Windows.Forms.Panel()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.Img_Reviwed = New System.Windows.Forms.PictureBox()
        Me.Img_Blanck = New System.Windows.Forms.PictureBox()
        Me.Img_Note = New System.Windows.Forms.PictureBox()
        Me.C1PatientExam = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlFilterExams = New System.Windows.Forms.Panel()
        Me.Panel25 = New System.Windows.Forms.Panel()
        Me.pnlClearSearch = New System.Windows.Forms.Panel()
        Me.txtExamName = New System.Windows.Forms.TextBox()
        Me.Label170 = New System.Windows.Forms.Label()
        Me.Label171 = New System.Windows.Forms.Label()
        Me.lbl_WhiteSpaceBottom = New System.Windows.Forms.Label()
        Me.btnExamNameClear = New System.Windows.Forms.Button()
        Me.lbl_pnlSearchLeftBrd = New System.Windows.Forms.Label()
        Me.lbl_pnlSearchRightBrd = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.cmbTemplateSpeciality = New System.Windows.Forms.ComboBox()
        Me.Label169 = New System.Windows.Forms.Label()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.cmbExamProvider = New System.Windows.Forms.ComboBox()
        Me.lblProvider = New System.Windows.Forms.Label()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.cmbExamStatus = New System.Windows.Forms.ComboBox()
        Me.lblcmbType = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.dtpExamTo = New System.Windows.Forms.DateTimePicker()
        Me.lblto = New System.Windows.Forms.Label()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.dtpExamFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.chkDateFilter = New System.Windows.Forms.CheckBox()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel21 = New System.Windows.Forms.Panel()
        Me.txtRefillRequest = New System.Windows.Forms.TextBox()
        Me.txtError = New System.Windows.Forms.TextBox()
        Me.Panel20 = New System.Windows.Forms.Panel()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.btnbrwError = New System.Windows.Forms.Button()
        Me.btnbrwRefill = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.tbDMS = New System.Windows.Forms.TabPage()
        Me.pnlDMSGrid = New System.Windows.Forms.Panel()
        Me.Label185 = New System.Windows.Forms.Label()
        Me.Label179 = New System.Windows.Forms.Label()
        Me.Label178 = New System.Windows.Forms.Label()
        Me.C1PatientDMS = New C1.Win.C1FlexGrid.C1FlexGrid()
        Me.pnlDMSSearch = New System.Windows.Forms.Panel()
        Me.Panel23 = New System.Windows.Forms.Panel()
        Me.Panel24 = New System.Windows.Forms.Panel()
        Me.txtSearchCriteria = New gloUserControlLibrary.gloSearchTextBox()
        Me.Label194 = New System.Windows.Forms.Label()
        Me.Label195 = New System.Windows.Forms.Label()
        Me.Label196 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label198 = New System.Windows.Forms.Label()
        Me.Label197 = New System.Windows.Forms.Label()
        Me.lblsearch = New System.Windows.Forms.Label()
        Me.cmbSearch = New System.Windows.Forms.ComboBox()
        Me.Label93 = New System.Windows.Forms.Label()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.Label91 = New System.Windows.Forms.Label()
        Me.Label92 = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlTop.SuspendLayout()
        Me.tbSummary.SuspendLayout()
        Me.tbpSummary.SuspendLayout()
        Me.pnlSummary.SuspendLayout()
        Me.pnlImplant.SuspendLayout()
        Me.pnltrImplant.SuspendLayout()
        Me.pnllblImplantHeader.SuspendLayout()
        Me.pnllblImplant.SuspendLayout()
        Me.pnlImagingST.SuspendLayout()
        Me.pnltrImagingST.SuspendLayout()
        Me.pnllblImagingSTHeader.SuspendLayout()
        Me.pnllblImagingST.SuspendLayout()
        Me.pnlLabs.SuspendLayout()
        Me.pnltrLabs.SuspendLayout()
        Me.pnlLabTitle.SuspendLayout()
        Me.pnllblLabs.SuspendLayout()
        Me.pnlProcedures.SuspendLayout()
        Me.pnltrProcedures.SuspendLayout()
        Me.pnlprocedureTitle.SuspendLayout()
        Me.pnllblProcedures.SuspendLayout()
        Me.pnlHistory.SuspendLayout()
        Me.pnltrHistory.SuspendLayout()
        Me.pnlHistoryTitle.SuspendLayout()
        Me.pnllblHistory.SuspendLayout()
        Me.pnlMedications.SuspendLayout()
        Me.pnltrMedications.SuspendLayout()
        Me.pnlMedicationTitle.SuspendLayout()
        Me.pnllblMedications.SuspendLayout()
        Me.pnlProblems.SuspendLayout()
        Me.pnltrProblemList.SuspendLayout()
        Me.pnlProblemsTitle.SuspendLayout()
        Me.pnllblProblems.SuspendLayout()
        Me.tbpProblem.SuspendLayout()
        Me.Panel28.SuspendLayout()
        CType(Me.c1ProblemList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearchproblemList.SuspendLayout()
        Me.pnlsearchProblems.SuspendLayout()
        Me.tbpMedications.SuspendLayout()
        Me.pnldgPatientDetails.SuspendLayout()
        CType(Me.C1MedicationDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearchMedications.SuspendLayout()
        Me.pnlSearchMed.SuspendLayout()
        Me.tbpAllergies.SuspendLayout()
        Me.pnlC1dgPatientDetails.SuspendLayout()
        CType(Me.C1dgPatientDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearchAllergies.SuspendLayout()
        Me.tbProcedures.SuspendLayout()
        Me.pnltrProcedureDetails.SuspendLayout()
        Me.pnlNormalSearchProc.SuspendLayout()
        CType(Me.C1Dignosis, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearchProcedures.SuspendLayout()
        Me.pnlSearchProc.SuspendLayout()
        Me.Panel19.SuspendLayout()
        Me.pnlDateSearch.SuspendLayout()
        Me.tbpLabs.SuspendLayout()
        Me.tbpImaging.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        CType(Me.C1OrderDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlSearchImaging.SuspendLayout()
        Me.Panel18.SuspendLayout()
        Me.tbpImagingST.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.C1CV_StressTest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.tbpImplant.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.C1Cardiology, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.tbpEjectFrac.SuspendLayout()
        Me.Panel10.SuspendLayout()
        CType(Me.cfgEjectionFraction, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        Me.pnlImaging.SuspendLayout()
        Me.pnltrImaging.SuspendLayout()
        Me.pnlImagingTitle.SuspendLayout()
        Me.pnllblImaging.SuspendLayout()
        Me.pnlFill.SuspendLayout()
        Me.tbExamDMS.SuspendLayout()
        Me.tbExams.SuspendLayout()
        Me.Panel22.SuspendLayout()
        CType(Me.Img_Reviwed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Img_Blanck, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Img_Note, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.C1PatientExam, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFilterExams.SuspendLayout()
        Me.Panel25.SuspendLayout()
        Me.pnlClearSearch.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.Panel17.SuspendLayout()
        Me.Panel21.SuspendLayout()
        Me.Panel20.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel12.SuspendLayout()
        Me.tbDMS.SuspendLayout()
        Me.pnlDMSGrid.SuspendLayout()
        CType(Me.C1PatientDMS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDMSSearch.SuspendLayout()
        Me.Panel23.SuspendLayout()
        Me.Panel24.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.tbSummary)
        Me.pnlTop.Controls.Add(Me.pnlImaging)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Padding = New System.Windows.Forms.Padding(3)
        Me.pnlTop.Size = New System.Drawing.Size(1024, 311)
        Me.pnlTop.TabIndex = 4
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
        Me.tbSummary.Controls.Add(Me.tbpImagingST)
        Me.tbSummary.Controls.Add(Me.tbpImplant)
        Me.tbSummary.Controls.Add(Me.tbpEjectFrac)
        Me.tbSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbSummary.ItemSize = New System.Drawing.Size(80, 22)
        Me.tbSummary.Location = New System.Drawing.Point(3, 3)
        Me.tbSummary.Margin = New System.Windows.Forms.Padding(4)
        Me.tbSummary.Name = "tbSummary"
        Me.tbSummary.SelectedIndex = 0
        Me.tbSummary.Size = New System.Drawing.Size(1018, 305)
        Me.tbSummary.TabIndex = 0
        '
        'tbpSummary
        '
        Me.tbpSummary.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpSummary.Controls.Add(Me.pnlSummary)
        Me.tbpSummary.ImageIndex = 0
        Me.tbpSummary.Location = New System.Drawing.Point(4, 26)
        Me.tbpSummary.Name = "tbpSummary"
        Me.tbpSummary.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.tbpSummary.Size = New System.Drawing.Size(1010, 275)
        Me.tbpSummary.TabIndex = 0
        Me.tbpSummary.Text = "Synopsis"
        Me.tbpSummary.UseVisualStyleBackColor = True
        '
        'pnlSummary
        '
        Me.pnlSummary.AutoScroll = True
        Me.pnlSummary.Controls.Add(Me.pnlImplant)
        Me.pnlSummary.Controls.Add(Me.splImagingST)
        Me.pnlSummary.Controls.Add(Me.pnlImagingST)
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
        Me.pnlSummary.Location = New System.Drawing.Point(0, 2)
        Me.pnlSummary.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlSummary.Name = "pnlSummary"
        Me.pnlSummary.Size = New System.Drawing.Size(1010, 273)
        Me.pnlSummary.TabIndex = 0
        '
        'pnlImplant
        '
        Me.pnlImplant.Controls.Add(Me.pnltrImplant)
        Me.pnlImplant.Controls.Add(Me.pnllblImplantHeader)
        Me.pnlImplant.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlImplant.Location = New System.Drawing.Point(810, 0)
        Me.pnlImplant.Name = "pnlImplant"
        Me.pnlImplant.Size = New System.Drawing.Size(132, 273)
        Me.pnlImplant.TabIndex = 11
        '
        'pnltrImplant
        '
        Me.pnltrImplant.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrImplant.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrImplant.Controls.Add(Me.trImplant)
        Me.pnltrImplant.Controls.Add(Me.Label98)
        Me.pnltrImplant.Controls.Add(Me.Label99)
        Me.pnltrImplant.Controls.Add(Me.Label100)
        Me.pnltrImplant.Controls.Add(Me.Label101)
        Me.pnltrImplant.Controls.Add(Me.Label102)
        Me.pnltrImplant.Controls.Add(Me.Label103)
        Me.pnltrImplant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrImplant.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrImplant.Location = New System.Drawing.Point(0, 30)
        Me.pnltrImplant.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnltrImplant.Name = "pnltrImplant"
        Me.pnltrImplant.Size = New System.Drawing.Size(132, 243)
        Me.pnltrImplant.TabIndex = 20
        '
        'trImplant
        '
        Me.trImplant.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trImplant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trImplant.ForeColor = System.Drawing.Color.Black
        Me.trImplant.HideSelection = False
        Me.trImplant.ImageIndex = 0
        Me.trImplant.ImageList = Me.ImgPatientTab
        Me.trImplant.ItemHeight = 19
        Me.trImplant.Location = New System.Drawing.Point(3, 3)
        Me.trImplant.Name = "trImplant"
        Me.trImplant.SelectedImageIndex = 0
        Me.trImplant.ShowLines = False
        Me.trImplant.Size = New System.Drawing.Size(128, 239)
        Me.trImplant.TabIndex = 3
        '
        'ImgPatientTab
        '
        Me.ImgPatientTab.ImageStream = CType(resources.GetObject("ImgPatientTab.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgPatientTab.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgPatientTab.Images.SetKeyName(0, "")
        Me.ImgPatientTab.Images.SetKeyName(1, "")
        Me.ImgPatientTab.Images.SetKeyName(2, "")
        Me.ImgPatientTab.Images.SetKeyName(3, "")
        Me.ImgPatientTab.Images.SetKeyName(4, "")
        Me.ImgPatientTab.Images.SetKeyName(5, "")
        Me.ImgPatientTab.Images.SetKeyName(6, "")
        Me.ImgPatientTab.Images.SetKeyName(7, "")
        Me.ImgPatientTab.Images.SetKeyName(8, "")
        Me.ImgPatientTab.Images.SetKeyName(9, "")
        Me.ImgPatientTab.Images.SetKeyName(10, "")
        Me.ImgPatientTab.Images.SetKeyName(11, "")
        Me.ImgPatientTab.Images.SetKeyName(12, "")
        Me.ImgPatientTab.Images.SetKeyName(13, "Procedure Date.ico")
        Me.ImgPatientTab.Images.SetKeyName(14, "CPT.ico")
        Me.ImgPatientTab.Images.SetKeyName(15, "Procedure.ico")
        Me.ImgPatientTab.Images.SetKeyName(16, "User.ico")
        Me.ImgPatientTab.Images.SetKeyName(17, "Ejection Fraction.ico")
        Me.ImgPatientTab.Images.SetKeyName(18, "")
        Me.ImgPatientTab.Images.SetKeyName(19, "")
        Me.ImgPatientTab.Images.SetKeyName(20, "")
        Me.ImgPatientTab.Images.SetKeyName(21, "")
        Me.ImgPatientTab.Images.SetKeyName(22, "")
        Me.ImgPatientTab.Images.SetKeyName(23, "")
        Me.ImgPatientTab.Images.SetKeyName(24, "")
        Me.ImgPatientTab.Images.SetKeyName(25, "")
        Me.ImgPatientTab.Images.SetKeyName(26, "")
        Me.ImgPatientTab.Images.SetKeyName(27, "")
        Me.ImgPatientTab.Images.SetKeyName(28, "Lead Location.ico")
        Me.ImgPatientTab.Images.SetKeyName(29, "Physical Location of Device Implant.ico")
        Me.ImgPatientTab.Images.SetKeyName(30, "Threshold Atrial.ico")
        Me.ImgPatientTab.Images.SetKeyName(31, "Threshold Ventricular.ico")
        Me.ImgPatientTab.Images.SetKeyName(32, "Sensing Atrial.ico")
        Me.ImgPatientTab.Images.SetKeyName(33, "Sensing Ventricular.ico")
        Me.ImgPatientTab.Images.SetKeyName(34, "Impedence Atrial.ico")
        Me.ImgPatientTab.Images.SetKeyName(35, "Impedence Ventricular.ico")
        '
        'Label98
        '
        Me.Label98.BackColor = System.Drawing.Color.White
        Me.Label98.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label98.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label98.Location = New System.Drawing.Point(3, 1)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(128, 2)
        Me.Label98.TabIndex = 12
        Me.Label98.Text = "label1"
        '
        'Label99
        '
        Me.Label99.BackColor = System.Drawing.Color.White
        Me.Label99.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label99.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label99.Location = New System.Drawing.Point(1, 1)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(2, 241)
        Me.Label99.TabIndex = 11
        '
        'Label100
        '
        Me.Label100.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label100.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label100.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label100.Location = New System.Drawing.Point(1, 242)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(130, 1)
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
        Me.Label101.Size = New System.Drawing.Size(1, 242)
        Me.Label101.TabIndex = 7
        Me.Label101.Text = "label4"
        '
        'Label102
        '
        Me.Label102.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label102.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label102.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label102.Location = New System.Drawing.Point(131, 1)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(1, 242)
        Me.Label102.TabIndex = 6
        Me.Label102.Text = "label3"
        '
        'Label103
        '
        Me.Label103.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label103.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label103.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label103.Location = New System.Drawing.Point(0, 0)
        Me.Label103.Name = "Label103"
        Me.Label103.Size = New System.Drawing.Size(132, 1)
        Me.Label103.TabIndex = 5
        Me.Label103.Text = "label1"
        '
        'pnllblImplantHeader
        '
        Me.pnllblImplantHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnllblImplantHeader.Controls.Add(Me.pnllblImplant)
        Me.pnllblImplantHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnllblImplantHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnllblImplantHeader.Name = "pnllblImplantHeader"
        Me.pnllblImplantHeader.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnllblImplantHeader.Size = New System.Drawing.Size(132, 30)
        Me.pnllblImplantHeader.TabIndex = 1
        '
        'pnllblImplant
        '
        Me.pnllblImplant.BackColor = System.Drawing.Color.Transparent
        Me.pnllblImplant.BackgroundImage = CType(resources.GetObject("pnllblImplant.BackgroundImage"), System.Drawing.Image)
        Me.pnllblImplant.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnllblImplant.Controls.Add(Me.btnImplantExpand)
        Me.pnllblImplant.Controls.Add(Me.Label104)
        Me.pnllblImplant.Controls.Add(Me.Label105)
        Me.pnllblImplant.Controls.Add(Me.lblImplant)
        Me.pnllblImplant.Controls.Add(Me.Label107)
        Me.pnllblImplant.Controls.Add(Me.Label108)
        Me.pnllblImplant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnllblImplant.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnllblImplant.Location = New System.Drawing.Point(0, 0)
        Me.pnllblImplant.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnllblImplant.Name = "pnllblImplant"
        Me.pnllblImplant.Size = New System.Drawing.Size(132, 27)
        Me.pnllblImplant.TabIndex = 20
        '
        'btnImplantExpand
        '
        Me.btnImplantExpand.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnImplantExpand.FlatAppearance.BorderSize = 0
        Me.btnImplantExpand.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnImplantExpand.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnImplantExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImplantExpand.Location = New System.Drawing.Point(107, 1)
        Me.btnImplantExpand.Name = "btnImplantExpand"
        Me.btnImplantExpand.Size = New System.Drawing.Size(24, 25)
        Me.btnImplantExpand.TabIndex = 11
        Me.btnImplantExpand.UseVisualStyleBackColor = True
        '
        'Label104
        '
        Me.Label104.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label104.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label104.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label104.Location = New System.Drawing.Point(1, 26)
        Me.Label104.Name = "Label104"
        Me.Label104.Size = New System.Drawing.Size(130, 1)
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
        Me.Label105.Size = New System.Drawing.Size(1, 26)
        Me.Label105.TabIndex = 7
        Me.Label105.Text = "label4"
        '
        'lblImplant
        '
        Me.lblImplant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblImplant.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblImplant.ForeColor = System.Drawing.Color.White
        Me.lblImplant.Location = New System.Drawing.Point(0, 1)
        Me.lblImplant.Name = "lblImplant"
        Me.lblImplant.Size = New System.Drawing.Size(131, 26)
        Me.lblImplant.TabIndex = 1
        Me.lblImplant.Text = "Implant"
        Me.lblImplant.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label107
        '
        Me.Label107.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label107.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label107.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label107.Location = New System.Drawing.Point(131, 1)
        Me.Label107.Name = "Label107"
        Me.Label107.Size = New System.Drawing.Size(1, 26)
        Me.Label107.TabIndex = 6
        Me.Label107.Text = "label3"
        '
        'Label108
        '
        Me.Label108.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label108.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label108.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label108.Location = New System.Drawing.Point(0, 0)
        Me.Label108.Name = "Label108"
        Me.Label108.Size = New System.Drawing.Size(132, 1)
        Me.Label108.TabIndex = 5
        Me.Label108.Text = "label1"
        '
        'splImagingST
        '
        Me.splImagingST.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.splImagingST.Location = New System.Drawing.Point(807, 0)
        Me.splImagingST.Name = "splImagingST"
        Me.splImagingST.Size = New System.Drawing.Size(3, 273)
        Me.splImagingST.TabIndex = 13
        Me.splImagingST.TabStop = False
        '
        'pnlImagingST
        '
        Me.pnlImagingST.Controls.Add(Me.pnltrImagingST)
        Me.pnlImagingST.Controls.Add(Me.pnllblImagingSTHeader)
        Me.pnlImagingST.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlImagingST.Location = New System.Drawing.Point(675, 0)
        Me.pnlImagingST.Name = "pnlImagingST"
        Me.pnlImagingST.Size = New System.Drawing.Size(132, 273)
        Me.pnlImagingST.TabIndex = 11
        '
        'pnltrImagingST
        '
        Me.pnltrImagingST.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrImagingST.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrImagingST.Controls.Add(Me.trimagingecho)
        Me.pnltrImagingST.Controls.Add(Me.Splitter2)
        Me.pnltrImagingST.Controls.Add(Me.trImagingST)
        Me.pnltrImagingST.Controls.Add(Me.Label78)
        Me.pnltrImagingST.Controls.Add(Me.Label79)
        Me.pnltrImagingST.Controls.Add(Me.Label80)
        Me.pnltrImagingST.Controls.Add(Me.Label81)
        Me.pnltrImagingST.Controls.Add(Me.Label82)
        Me.pnltrImagingST.Controls.Add(Me.Label83)
        Me.pnltrImagingST.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrImagingST.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrImagingST.Location = New System.Drawing.Point(0, 30)
        Me.pnltrImagingST.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnltrImagingST.Name = "pnltrImagingST"
        Me.pnltrImagingST.Size = New System.Drawing.Size(132, 243)
        Me.pnltrImagingST.TabIndex = 20
        '
        'trimagingecho
        '
        Me.trimagingecho.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trimagingecho.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trimagingecho.ForeColor = System.Drawing.Color.Black
        Me.trimagingecho.HideSelection = False
        Me.trimagingecho.ImageIndex = 0
        Me.trimagingecho.ImageList = Me.ImageList1
        Me.trimagingecho.ItemHeight = 19
        Me.trimagingecho.Location = New System.Drawing.Point(3, 113)
        Me.trimagingecho.Name = "trimagingecho"
        Me.trimagingecho.SelectedImageIndex = 0
        Me.trimagingecho.ShowLines = False
        Me.trimagingecho.Size = New System.Drawing.Size(128, 129)
        Me.trimagingecho.TabIndex = 14
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Procedure.ico")
        Me.ImageList1.Images.SetKeyName(1, "Procedure Date.ico")
        Me.ImageList1.Images.SetKeyName(2, "User.ico")
        Me.ImageList1.Images.SetKeyName(3, "Bullet06.ico")
        Me.ImageList1.Images.SetKeyName(4, "MV area.ico")
        Me.ImageList1.Images.SetKeyName(5, "AV area.ico")
        Me.ImageList1.Images.SetKeyName(6, "LA Area.ico")
        Me.ImageList1.Images.SetKeyName(7, "Saturation.ico")
        Me.ImageList1.Images.SetKeyName(8, "Narrative Summary.ico")
        Me.ImageList1.Images.SetKeyName(9, "LV(Left ventricular) Systolic.ico")
        Me.ImageList1.Images.SetKeyName(10, "LV.ico")
        Me.ImageList1.Images.SetKeyName(11, "LV Mass.ico")
        Me.ImageList1.Images.SetKeyName(12, "LV(Left ventricular) Diastolic.ico")
        Me.ImageList1.Images.SetKeyName(13, "Doppler Gradients.ico")
        Me.ImageList1.Images.SetKeyName(14, "M-Mode (Mitral Valve).ico")
        Me.ImageList1.Images.SetKeyName(15, "Intrevention Type.ico")
        Me.ImageList1.Images.SetKeyName(16, "CPT code.ico")
        Me.ImageList1.Images.SetKeyName(17, "CPT.ico")
        Me.ImageList1.Images.SetKeyName(18, "Physician.ico")
        Me.ImageList1.Images.SetKeyName(19, "Pressure.ico")
        Me.ImageList1.Images.SetKeyName(20, "Test Type.ico")
        Me.ImageList1.Images.SetKeyName(21, "CPTs.ico")
        Me.ImageList1.Images.SetKeyName(22, "EchoCardiogram.ico")
        '
        'Splitter2
        '
        Me.Splitter2.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter2.Location = New System.Drawing.Point(3, 109)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(128, 4)
        Me.Splitter2.TabIndex = 13
        Me.Splitter2.TabStop = False
        '
        'trImagingST
        '
        Me.trImagingST.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trImagingST.Dock = System.Windows.Forms.DockStyle.Top
        Me.trImagingST.ForeColor = System.Drawing.Color.Black
        Me.trImagingST.HideSelection = False
        Me.trImagingST.ImageIndex = 0
        Me.trImagingST.ImageList = Me.ImgPatientTab
        Me.trImagingST.ItemHeight = 19
        Me.trImagingST.Location = New System.Drawing.Point(3, 3)
        Me.trImagingST.Name = "trImagingST"
        Me.trImagingST.SelectedImageIndex = 0
        Me.trImagingST.ShowLines = False
        Me.trImagingST.Size = New System.Drawing.Size(128, 106)
        Me.trImagingST.TabIndex = 3
        '
        'Label78
        '
        Me.Label78.BackColor = System.Drawing.Color.White
        Me.Label78.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label78.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label78.Location = New System.Drawing.Point(3, 1)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(128, 2)
        Me.Label78.TabIndex = 12
        Me.Label78.Text = "label1"
        '
        'Label79
        '
        Me.Label79.BackColor = System.Drawing.Color.White
        Me.Label79.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label79.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label79.Location = New System.Drawing.Point(1, 1)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(2, 241)
        Me.Label79.TabIndex = 11
        Me.Label79.Visible = False
        '
        'Label80
        '
        Me.Label80.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label80.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label80.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label80.Location = New System.Drawing.Point(1, 242)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(130, 1)
        Me.Label80.TabIndex = 8
        Me.Label80.Text = "label2"
        '
        'Label81
        '
        Me.Label81.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label81.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label81.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label81.Location = New System.Drawing.Point(0, 1)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(1, 242)
        Me.Label81.TabIndex = 7
        Me.Label81.Text = "label4"
        '
        'Label82
        '
        Me.Label82.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label82.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label82.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label82.Location = New System.Drawing.Point(131, 1)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(1, 242)
        Me.Label82.TabIndex = 6
        Me.Label82.Text = "label3"
        '
        'Label83
        '
        Me.Label83.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label83.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label83.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label83.Location = New System.Drawing.Point(0, 0)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(132, 1)
        Me.Label83.TabIndex = 5
        Me.Label83.Text = "label1"
        '
        'pnllblImagingSTHeader
        '
        Me.pnllblImagingSTHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnllblImagingSTHeader.Controls.Add(Me.pnllblImagingST)
        Me.pnllblImagingSTHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnllblImagingSTHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnllblImagingSTHeader.Name = "pnllblImagingSTHeader"
        Me.pnllblImagingSTHeader.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnllblImagingSTHeader.Size = New System.Drawing.Size(132, 30)
        Me.pnllblImagingSTHeader.TabIndex = 1
        '
        'pnllblImagingST
        '
        Me.pnllblImagingST.BackColor = System.Drawing.Color.Transparent
        Me.pnllblImagingST.BackgroundImage = CType(resources.GetObject("pnllblImagingST.BackgroundImage"), System.Drawing.Image)
        Me.pnllblImagingST.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnllblImagingST.Controls.Add(Me.btnImagSTExpand)
        Me.pnllblImagingST.Controls.Add(Me.Label84)
        Me.pnllblImagingST.Controls.Add(Me.Label94)
        Me.pnllblImagingST.Controls.Add(Me.lblImaging)
        Me.pnllblImagingST.Controls.Add(Me.Label96)
        Me.pnllblImagingST.Controls.Add(Me.Label97)
        Me.pnllblImagingST.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnllblImagingST.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnllblImagingST.Location = New System.Drawing.Point(0, 0)
        Me.pnllblImagingST.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnllblImagingST.Name = "pnllblImagingST"
        Me.pnllblImagingST.Size = New System.Drawing.Size(132, 27)
        Me.pnllblImagingST.TabIndex = 20
        '
        'btnImagSTExpand
        '
        Me.btnImagSTExpand.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnImagSTExpand.FlatAppearance.BorderSize = 0
        Me.btnImagSTExpand.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnImagSTExpand.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnImagSTExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnImagSTExpand.Location = New System.Drawing.Point(107, 1)
        Me.btnImagSTExpand.Name = "btnImagSTExpand"
        Me.btnImagSTExpand.Size = New System.Drawing.Size(24, 25)
        Me.btnImagSTExpand.TabIndex = 10
        Me.btnImagSTExpand.UseVisualStyleBackColor = True
        '
        'Label84
        '
        Me.Label84.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label84.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label84.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label84.Location = New System.Drawing.Point(1, 26)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(130, 1)
        Me.Label84.TabIndex = 8
        Me.Label84.Text = "label2"
        '
        'Label94
        '
        Me.Label94.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label94.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label94.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label94.Location = New System.Drawing.Point(0, 1)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(1, 26)
        Me.Label94.TabIndex = 7
        Me.Label94.Text = "label4"
        '
        'lblImaging
        '
        Me.lblImaging.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblImaging.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblImaging.ForeColor = System.Drawing.Color.White
        Me.lblImaging.Location = New System.Drawing.Point(0, 1)
        Me.lblImaging.Name = "lblImaging"
        Me.lblImaging.Size = New System.Drawing.Size(131, 26)
        Me.lblImaging.TabIndex = 1
        Me.lblImaging.Text = "Imaging"
        Me.lblImaging.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label96
        '
        Me.Label96.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label96.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label96.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label96.Location = New System.Drawing.Point(131, 1)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(1, 26)
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
        Me.Label97.Size = New System.Drawing.Size(132, 1)
        Me.Label97.TabIndex = 5
        Me.Label97.Text = "label1"
        '
        'spltLabs
        '
        Me.spltLabs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.spltLabs.Location = New System.Drawing.Point(672, 0)
        Me.spltLabs.Name = "spltLabs"
        Me.spltLabs.Size = New System.Drawing.Size(3, 273)
        Me.spltLabs.TabIndex = 9
        Me.spltLabs.TabStop = False
        '
        'pnlLabs
        '
        Me.pnlLabs.Controls.Add(Me.pnltrLabs)
        Me.pnlLabs.Controls.Add(Me.pnlLabTitle)
        Me.pnlLabs.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLabs.Location = New System.Drawing.Point(540, 0)
        Me.pnlLabs.Name = "pnlLabs"
        Me.pnlLabs.Size = New System.Drawing.Size(132, 273)
        Me.pnlLabs.TabIndex = 8
        '
        'pnltrLabs
        '
        Me.pnltrLabs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrLabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrLabs.Controls.Add(Me.trLabs)
        Me.pnltrLabs.Controls.Add(Me.Label72)
        Me.pnltrLabs.Controls.Add(Me.Label73)
        Me.pnltrLabs.Controls.Add(Me.Label32)
        Me.pnltrLabs.Controls.Add(Me.Label33)
        Me.pnltrLabs.Controls.Add(Me.Label34)
        Me.pnltrLabs.Controls.Add(Me.Label35)
        Me.pnltrLabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrLabs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrLabs.Location = New System.Drawing.Point(0, 30)
        Me.pnltrLabs.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnltrLabs.Name = "pnltrLabs"
        Me.pnltrLabs.Size = New System.Drawing.Size(132, 243)
        Me.pnltrLabs.TabIndex = 20
        '
        'trLabs
        '
        Me.trLabs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trLabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trLabs.ForeColor = System.Drawing.Color.Black
        Me.trLabs.HideSelection = False
        Me.trLabs.ImageIndex = 0
        Me.trLabs.ImageList = Me.ImgPatientTab
        Me.trLabs.ItemHeight = 19
        Me.trLabs.Location = New System.Drawing.Point(3, 3)
        Me.trLabs.Name = "trLabs"
        Me.trLabs.SelectedImageIndex = 0
        Me.trLabs.ShowLines = False
        Me.trLabs.Size = New System.Drawing.Size(128, 239)
        Me.trLabs.TabIndex = 4
        '
        'Label72
        '
        Me.Label72.BackColor = System.Drawing.Color.White
        Me.Label72.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label72.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.Location = New System.Drawing.Point(3, 1)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(128, 2)
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
        Me.Label73.Size = New System.Drawing.Size(2, 241)
        Me.Label73.TabIndex = 11
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label32.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label32.Location = New System.Drawing.Point(1, 242)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(130, 1)
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
        Me.Label33.Size = New System.Drawing.Size(1, 242)
        Me.Label33.TabIndex = 7
        Me.Label33.Text = "label4"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label34.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label34.Location = New System.Drawing.Point(131, 1)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(1, 242)
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
        Me.Label35.Size = New System.Drawing.Size(132, 1)
        Me.Label35.TabIndex = 5
        Me.Label35.Text = "label1"
        '
        'pnlLabTitle
        '
        Me.pnlLabTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlLabTitle.Controls.Add(Me.pnllblLabs)
        Me.pnlLabTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLabTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlLabTitle.Name = "pnlLabTitle"
        Me.pnlLabTitle.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlLabTitle.Size = New System.Drawing.Size(132, 30)
        Me.pnlLabTitle.TabIndex = 1
        '
        'pnllblLabs
        '
        Me.pnllblLabs.BackColor = System.Drawing.Color.Transparent
        Me.pnllblLabs.BackgroundImage = CType(resources.GetObject("pnllblLabs.BackgroundImage"), System.Drawing.Image)
        Me.pnllblLabs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnllblLabs.Controls.Add(Me.btnLabExpand)
        Me.pnllblLabs.Controls.Add(Me.Label56)
        Me.pnllblLabs.Controls.Add(Me.Label57)
        Me.pnllblLabs.Controls.Add(Me.lblLabs)
        Me.pnllblLabs.Controls.Add(Me.Label58)
        Me.pnllblLabs.Controls.Add(Me.Label59)
        Me.pnllblLabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnllblLabs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnllblLabs.Location = New System.Drawing.Point(0, 0)
        Me.pnllblLabs.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnllblLabs.Name = "pnllblLabs"
        Me.pnllblLabs.Size = New System.Drawing.Size(132, 27)
        Me.pnllblLabs.TabIndex = 20
        '
        'btnLabExpand
        '
        Me.btnLabExpand.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnLabExpand.FlatAppearance.BorderSize = 0
        Me.btnLabExpand.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLabExpand.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLabExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLabExpand.Location = New System.Drawing.Point(107, 1)
        Me.btnLabExpand.Name = "btnLabExpand"
        Me.btnLabExpand.Size = New System.Drawing.Size(24, 25)
        Me.btnLabExpand.TabIndex = 10
        Me.btnLabExpand.UseVisualStyleBackColor = True
        '
        'Label56
        '
        Me.Label56.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label56.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label56.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label56.Location = New System.Drawing.Point(1, 26)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(130, 1)
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
        'lblLabs
        '
        Me.lblLabs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLabs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLabs.ForeColor = System.Drawing.Color.White
        Me.lblLabs.Location = New System.Drawing.Point(0, 1)
        Me.lblLabs.Name = "lblLabs"
        Me.lblLabs.Size = New System.Drawing.Size(131, 26)
        Me.lblLabs.TabIndex = 1
        Me.lblLabs.Text = "Orders && Results"
        Me.lblLabs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label58
        '
        Me.Label58.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label58.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label58.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label58.Location = New System.Drawing.Point(131, 1)
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
        Me.Label59.Size = New System.Drawing.Size(132, 1)
        Me.Label59.TabIndex = 5
        Me.Label59.Text = "label1"
        '
        'spltProcedures
        '
        Me.spltProcedures.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.spltProcedures.Location = New System.Drawing.Point(537, 0)
        Me.spltProcedures.Name = "spltProcedures"
        Me.spltProcedures.Size = New System.Drawing.Size(3, 273)
        Me.spltProcedures.TabIndex = 7
        Me.spltProcedures.TabStop = False
        '
        'pnlProcedures
        '
        Me.pnlProcedures.Controls.Add(Me.pnltrProcedures)
        Me.pnlProcedures.Controls.Add(Me.pnlprocedureTitle)
        Me.pnlProcedures.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlProcedures.Location = New System.Drawing.Point(405, 0)
        Me.pnlProcedures.Name = "pnlProcedures"
        Me.pnlProcedures.Size = New System.Drawing.Size(132, 273)
        Me.pnlProcedures.TabIndex = 6
        '
        'pnltrProcedures
        '
        Me.pnltrProcedures.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrProcedures.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrProcedures.Controls.Add(Me.trProcedures)
        Me.pnltrProcedures.Controls.Add(Me.Label70)
        Me.pnltrProcedures.Controls.Add(Me.Label71)
        Me.pnltrProcedures.Controls.Add(Me.Label27)
        Me.pnltrProcedures.Controls.Add(Me.Label28)
        Me.pnltrProcedures.Controls.Add(Me.Label29)
        Me.pnltrProcedures.Controls.Add(Me.Label31)
        Me.pnltrProcedures.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrProcedures.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrProcedures.Location = New System.Drawing.Point(0, 30)
        Me.pnltrProcedures.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnltrProcedures.Name = "pnltrProcedures"
        Me.pnltrProcedures.Size = New System.Drawing.Size(132, 243)
        Me.pnltrProcedures.TabIndex = 20
        '
        'trProcedures
        '
        Me.trProcedures.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trProcedures.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trProcedures.ForeColor = System.Drawing.Color.Black
        Me.trProcedures.HideSelection = False
        Me.trProcedures.ImageIndex = 0
        Me.trProcedures.ImageList = Me.ImgPatientTab
        Me.trProcedures.ItemHeight = 19
        Me.trProcedures.Location = New System.Drawing.Point(3, 3)
        Me.trProcedures.Name = "trProcedures"
        Me.trProcedures.SelectedImageIndex = 0
        Me.trProcedures.ShowLines = False
        Me.trProcedures.Size = New System.Drawing.Size(128, 239)
        Me.trProcedures.TabIndex = 2
        '
        'Label70
        '
        Me.Label70.BackColor = System.Drawing.Color.White
        Me.Label70.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label70.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.Location = New System.Drawing.Point(3, 1)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(128, 2)
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
        Me.Label71.Size = New System.Drawing.Size(2, 241)
        Me.Label71.TabIndex = 11
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label27.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label27.Location = New System.Drawing.Point(1, 242)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(130, 1)
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
        Me.Label28.Size = New System.Drawing.Size(1, 242)
        Me.Label28.TabIndex = 7
        Me.Label28.Text = "label4"
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label29.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label29.Location = New System.Drawing.Point(131, 1)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(1, 242)
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
        Me.Label31.Size = New System.Drawing.Size(132, 1)
        Me.Label31.TabIndex = 5
        Me.Label31.Text = "label1"
        '
        'pnlprocedureTitle
        '
        Me.pnlprocedureTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlprocedureTitle.Controls.Add(Me.pnllblProcedures)
        Me.pnlprocedureTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlprocedureTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlprocedureTitle.Name = "pnlprocedureTitle"
        Me.pnlprocedureTitle.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlprocedureTitle.Size = New System.Drawing.Size(132, 30)
        Me.pnlprocedureTitle.TabIndex = 1
        '
        'pnllblProcedures
        '
        Me.pnllblProcedures.BackColor = System.Drawing.Color.Transparent
        Me.pnllblProcedures.BackgroundImage = CType(resources.GetObject("pnllblProcedures.BackgroundImage"), System.Drawing.Image)
        Me.pnllblProcedures.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnllblProcedures.Controls.Add(Me.btnProcedExpand)
        Me.pnllblProcedures.Controls.Add(Me.Label52)
        Me.pnllblProcedures.Controls.Add(Me.Label53)
        Me.pnllblProcedures.Controls.Add(Me.Label54)
        Me.pnllblProcedures.Controls.Add(Me.lblProcedures)
        Me.pnllblProcedures.Controls.Add(Me.Label55)
        Me.pnllblProcedures.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnllblProcedures.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnllblProcedures.Location = New System.Drawing.Point(0, 0)
        Me.pnllblProcedures.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnllblProcedures.Name = "pnllblProcedures"
        Me.pnllblProcedures.Size = New System.Drawing.Size(132, 27)
        Me.pnllblProcedures.TabIndex = 20
        '
        'btnProcedExpand
        '
        Me.btnProcedExpand.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnProcedExpand.FlatAppearance.BorderSize = 0
        Me.btnProcedExpand.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnProcedExpand.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnProcedExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProcedExpand.Location = New System.Drawing.Point(107, 1)
        Me.btnProcedExpand.Name = "btnProcedExpand"
        Me.btnProcedExpand.Size = New System.Drawing.Size(24, 25)
        Me.btnProcedExpand.TabIndex = 10
        Me.btnProcedExpand.UseVisualStyleBackColor = True
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label52.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label52.Location = New System.Drawing.Point(1, 26)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(130, 1)
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
        Me.Label54.Location = New System.Drawing.Point(131, 1)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(1, 26)
        Me.Label54.TabIndex = 6
        Me.Label54.Text = "label3"
        '
        'lblProcedures
        '
        Me.lblProcedures.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblProcedures.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProcedures.ForeColor = System.Drawing.Color.White
        Me.lblProcedures.Location = New System.Drawing.Point(0, 1)
        Me.lblProcedures.Name = "lblProcedures"
        Me.lblProcedures.Size = New System.Drawing.Size(132, 26)
        Me.lblProcedures.TabIndex = 1
        Me.lblProcedures.Text = "Procedures"
        Me.lblProcedures.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label55
        '
        Me.Label55.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label55.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.Location = New System.Drawing.Point(0, 0)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(132, 1)
        Me.Label55.TabIndex = 5
        Me.Label55.Text = "label1"
        '
        'spltHistory
        '
        Me.spltHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.spltHistory.Location = New System.Drawing.Point(402, 0)
        Me.spltHistory.Name = "spltHistory"
        Me.spltHistory.Size = New System.Drawing.Size(3, 273)
        Me.spltHistory.TabIndex = 5
        Me.spltHistory.TabStop = False
        '
        'pnlHistory
        '
        Me.pnlHistory.Controls.Add(Me.pnltrHistory)
        Me.pnlHistory.Controls.Add(Me.pnlHistoryTitle)
        Me.pnlHistory.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlHistory.Location = New System.Drawing.Point(270, 0)
        Me.pnlHistory.Name = "pnlHistory"
        Me.pnlHistory.Size = New System.Drawing.Size(132, 273)
        Me.pnlHistory.TabIndex = 4
        '
        'pnltrHistory
        '
        Me.pnltrHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrHistory.Controls.Add(Me.trHistory)
        Me.pnltrHistory.Controls.Add(Me.Label68)
        Me.pnltrHistory.Controls.Add(Me.Label69)
        Me.pnltrHistory.Controls.Add(Me.Label23)
        Me.pnltrHistory.Controls.Add(Me.Label24)
        Me.pnltrHistory.Controls.Add(Me.Label25)
        Me.pnltrHistory.Controls.Add(Me.Label26)
        Me.pnltrHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrHistory.Location = New System.Drawing.Point(0, 30)
        Me.pnltrHistory.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnltrHistory.Name = "pnltrHistory"
        Me.pnltrHistory.Size = New System.Drawing.Size(132, 243)
        Me.pnltrHistory.TabIndex = 20
        '
        'trHistory
        '
        Me.trHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trHistory.ForeColor = System.Drawing.Color.Black
        Me.trHistory.HideSelection = False
        Me.trHistory.ImageIndex = 0
        Me.trHistory.ImageList = Me.ImgPatientTab
        Me.trHistory.ItemHeight = 19
        Me.trHistory.Location = New System.Drawing.Point(3, 3)
        Me.trHistory.Name = "trHistory"
        Me.trHistory.SelectedImageIndex = 0
        Me.trHistory.ShowLines = False
        Me.trHistory.Size = New System.Drawing.Size(128, 239)
        Me.trHistory.TabIndex = 2
        '
        'Label68
        '
        Me.Label68.BackColor = System.Drawing.Color.White
        Me.Label68.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label68.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label68.Location = New System.Drawing.Point(3, 1)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(128, 2)
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
        Me.Label69.Size = New System.Drawing.Size(2, 241)
        Me.Label69.TabIndex = 11
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label23.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label23.Location = New System.Drawing.Point(1, 242)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(130, 1)
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
        Me.Label24.Size = New System.Drawing.Size(1, 242)
        Me.Label24.TabIndex = 7
        Me.Label24.Text = "label4"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label25.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label25.Location = New System.Drawing.Point(131, 1)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(1, 242)
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
        Me.Label26.Size = New System.Drawing.Size(132, 1)
        Me.Label26.TabIndex = 5
        Me.Label26.Text = "label1"
        '
        'pnlHistoryTitle
        '
        Me.pnlHistoryTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlHistoryTitle.Controls.Add(Me.pnllblHistory)
        Me.pnlHistoryTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHistoryTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlHistoryTitle.Name = "pnlHistoryTitle"
        Me.pnlHistoryTitle.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlHistoryTitle.Size = New System.Drawing.Size(132, 30)
        Me.pnlHistoryTitle.TabIndex = 1
        '
        'pnllblHistory
        '
        Me.pnllblHistory.BackColor = System.Drawing.Color.Transparent
        Me.pnllblHistory.BackgroundImage = CType(resources.GetObject("pnllblHistory.BackgroundImage"), System.Drawing.Image)
        Me.pnllblHistory.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnllblHistory.Controls.Add(Me.btnHistExpand)
        Me.pnllblHistory.Controls.Add(Me.Label48)
        Me.pnllblHistory.Controls.Add(Me.Label49)
        Me.pnllblHistory.Controls.Add(Me.lblHistory)
        Me.pnllblHistory.Controls.Add(Me.Label50)
        Me.pnllblHistory.Controls.Add(Me.Label51)
        Me.pnllblHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnllblHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnllblHistory.Location = New System.Drawing.Point(0, 0)
        Me.pnllblHistory.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnllblHistory.Name = "pnllblHistory"
        Me.pnllblHistory.Size = New System.Drawing.Size(132, 27)
        Me.pnllblHistory.TabIndex = 20
        '
        'btnHistExpand
        '
        Me.btnHistExpand.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnHistExpand.FlatAppearance.BorderSize = 0
        Me.btnHistExpand.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnHistExpand.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnHistExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHistExpand.Location = New System.Drawing.Point(107, 1)
        Me.btnHistExpand.Name = "btnHistExpand"
        Me.btnHistExpand.Size = New System.Drawing.Size(24, 25)
        Me.btnHistExpand.TabIndex = 10
        Me.btnHistExpand.UseVisualStyleBackColor = True
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label48.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label48.Location = New System.Drawing.Point(1, 26)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(130, 1)
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
        'lblHistory
        '
        Me.lblHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHistory.ForeColor = System.Drawing.Color.White
        Me.lblHistory.Location = New System.Drawing.Point(0, 1)
        Me.lblHistory.Name = "lblHistory"
        Me.lblHistory.Size = New System.Drawing.Size(131, 26)
        Me.lblHistory.TabIndex = 1
        Me.lblHistory.Text = "History"
        Me.lblHistory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label50.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label50.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label50.Location = New System.Drawing.Point(131, 1)
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
        Me.Label51.Size = New System.Drawing.Size(132, 1)
        Me.Label51.TabIndex = 5
        Me.Label51.Text = "label1"
        '
        'spltMedications
        '
        Me.spltMedications.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.spltMedications.Location = New System.Drawing.Point(267, 0)
        Me.spltMedications.Name = "spltMedications"
        Me.spltMedications.Size = New System.Drawing.Size(3, 273)
        Me.spltMedications.TabIndex = 3
        Me.spltMedications.TabStop = False
        '
        'pnlMedications
        '
        Me.pnlMedications.Controls.Add(Me.pnltrMedications)
        Me.pnlMedications.Controls.Add(Me.pnlMedicationTitle)
        Me.pnlMedications.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlMedications.Location = New System.Drawing.Point(135, 0)
        Me.pnlMedications.Name = "pnlMedications"
        Me.pnlMedications.Size = New System.Drawing.Size(132, 273)
        Me.pnlMedications.TabIndex = 2
        '
        'pnltrMedications
        '
        Me.pnltrMedications.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrMedications.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrMedications.Controls.Add(Me.trMedications)
        Me.pnltrMedications.Controls.Add(Me.Label66)
        Me.pnltrMedications.Controls.Add(Me.Label67)
        Me.pnltrMedications.Controls.Add(Me.Label10)
        Me.pnltrMedications.Controls.Add(Me.Label11)
        Me.pnltrMedications.Controls.Add(Me.Label16)
        Me.pnltrMedications.Controls.Add(Me.Label22)
        Me.pnltrMedications.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrMedications.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrMedications.Location = New System.Drawing.Point(0, 30)
        Me.pnltrMedications.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnltrMedications.Name = "pnltrMedications"
        Me.pnltrMedications.Size = New System.Drawing.Size(132, 243)
        Me.pnltrMedications.TabIndex = 20
        '
        'trMedications
        '
        Me.trMedications.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trMedications.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trMedications.ForeColor = System.Drawing.Color.Black
        Me.trMedications.HideSelection = False
        Me.trMedications.ImageIndex = 0
        Me.trMedications.ImageList = Me.ImgPatientTab
        Me.trMedications.ItemHeight = 19
        Me.trMedications.Location = New System.Drawing.Point(3, 3)
        Me.trMedications.Name = "trMedications"
        Me.trMedications.SelectedImageIndex = 0
        Me.trMedications.ShowLines = False
        Me.trMedications.Size = New System.Drawing.Size(128, 239)
        Me.trMedications.TabIndex = 2
        '
        'Label66
        '
        Me.Label66.BackColor = System.Drawing.Color.White
        Me.Label66.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label66.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.Location = New System.Drawing.Point(3, 1)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(128, 2)
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
        Me.Label67.Size = New System.Drawing.Size(2, 241)
        Me.Label67.TabIndex = 11
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label10.Location = New System.Drawing.Point(1, 242)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(130, 1)
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
        Me.Label11.Size = New System.Drawing.Size(1, 242)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "label4"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label16.Location = New System.Drawing.Point(131, 1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(1, 242)
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
        Me.Label22.Size = New System.Drawing.Size(132, 1)
        Me.Label22.TabIndex = 5
        Me.Label22.Text = "label1"
        '
        'pnlMedicationTitle
        '
        Me.pnlMedicationTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMedicationTitle.Controls.Add(Me.pnllblMedications)
        Me.pnlMedicationTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlMedicationTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlMedicationTitle.Name = "pnlMedicationTitle"
        Me.pnlMedicationTitle.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlMedicationTitle.Size = New System.Drawing.Size(132, 30)
        Me.pnlMedicationTitle.TabIndex = 1
        '
        'pnllblMedications
        '
        Me.pnllblMedications.BackColor = System.Drawing.Color.Transparent
        Me.pnllblMedications.BackgroundImage = CType(resources.GetObject("pnllblMedications.BackgroundImage"), System.Drawing.Image)
        Me.pnllblMedications.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnllblMedications.Controls.Add(Me.btnMedExpand)
        Me.pnllblMedications.Controls.Add(Me.Label47)
        Me.pnllblMedications.Controls.Add(Me.Label44)
        Me.pnllblMedications.Controls.Add(Me.lblMedications)
        Me.pnllblMedications.Controls.Add(Me.Label45)
        Me.pnllblMedications.Controls.Add(Me.Label46)
        Me.pnllblMedications.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnllblMedications.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnllblMedications.Location = New System.Drawing.Point(0, 0)
        Me.pnllblMedications.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnllblMedications.Name = "pnllblMedications"
        Me.pnllblMedications.Size = New System.Drawing.Size(132, 27)
        Me.pnllblMedications.TabIndex = 20
        '
        'btnMedExpand
        '
        Me.btnMedExpand.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnMedExpand.FlatAppearance.BorderSize = 0
        Me.btnMedExpand.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnMedExpand.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnMedExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMedExpand.Location = New System.Drawing.Point(107, 1)
        Me.btnMedExpand.Name = "btnMedExpand"
        Me.btnMedExpand.Size = New System.Drawing.Size(24, 25)
        Me.btnMedExpand.TabIndex = 10
        Me.btnMedExpand.UseVisualStyleBackColor = True
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label47.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(1, 0)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(130, 1)
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
        Me.Label44.Size = New System.Drawing.Size(130, 1)
        Me.Label44.TabIndex = 8
        Me.Label44.Text = "label2"
        '
        'lblMedications
        '
        Me.lblMedications.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMedications.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMedications.ForeColor = System.Drawing.Color.White
        Me.lblMedications.Location = New System.Drawing.Point(1, 0)
        Me.lblMedications.Name = "lblMedications"
        Me.lblMedications.Size = New System.Drawing.Size(130, 27)
        Me.lblMedications.TabIndex = 1
        Me.lblMedications.Text = "Medications"
        Me.lblMedications.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.Label46.Location = New System.Drawing.Point(131, 0)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(1, 27)
        Me.Label46.TabIndex = 6
        Me.Label46.Text = "label3"
        '
        'spltProblems
        '
        Me.spltProblems.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.spltProblems.Location = New System.Drawing.Point(132, 0)
        Me.spltProblems.Name = "spltProblems"
        Me.spltProblems.Size = New System.Drawing.Size(3, 273)
        Me.spltProblems.TabIndex = 1
        Me.spltProblems.TabStop = False
        '
        'pnlProblems
        '
        Me.pnlProblems.Controls.Add(Me.pnltrProblemList)
        Me.pnlProblems.Controls.Add(Me.pnlProblemsTitle)
        Me.pnlProblems.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlProblems.Location = New System.Drawing.Point(0, 0)
        Me.pnlProblems.Name = "pnlProblems"
        Me.pnlProblems.Size = New System.Drawing.Size(132, 273)
        Me.pnlProblems.TabIndex = 0
        '
        'pnltrProblemList
        '
        Me.pnltrProblemList.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrProblemList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrProblemList.Controls.Add(Me.trProblemList)
        Me.pnltrProblemList.Controls.Add(Me.Label65)
        Me.pnltrProblemList.Controls.Add(Me.Label64)
        Me.pnltrProblemList.Controls.Add(Me.Label6)
        Me.pnltrProblemList.Controls.Add(Me.Label7)
        Me.pnltrProblemList.Controls.Add(Me.Label8)
        Me.pnltrProblemList.Controls.Add(Me.Label9)
        Me.pnltrProblemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrProblemList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrProblemList.Location = New System.Drawing.Point(0, 30)
        Me.pnltrProblemList.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnltrProblemList.Name = "pnltrProblemList"
        Me.pnltrProblemList.Size = New System.Drawing.Size(132, 243)
        Me.pnltrProblemList.TabIndex = 20
        '
        'trProblemList
        '
        Me.trProblemList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trProblemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trProblemList.ForeColor = System.Drawing.Color.Black
        Me.trProblemList.HideSelection = False
        Me.trProblemList.ImageIndex = 0
        Me.trProblemList.ImageList = Me.ImgPatientTab
        Me.trProblemList.ItemHeight = 19
        Me.trProblemList.Location = New System.Drawing.Point(3, 3)
        Me.trProblemList.Name = "trProblemList"
        Me.trProblemList.SelectedImageIndex = 0
        Me.trProblemList.ShowLines = False
        Me.trProblemList.Size = New System.Drawing.Size(128, 239)
        Me.trProblemList.TabIndex = 1
        '
        'Label65
        '
        Me.Label65.BackColor = System.Drawing.Color.White
        Me.Label65.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label65.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.Location = New System.Drawing.Point(3, 1)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(128, 2)
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
        Me.Label64.Size = New System.Drawing.Size(2, 241)
        Me.Label64.TabIndex = 9
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label6.Location = New System.Drawing.Point(1, 242)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(130, 1)
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
        Me.Label7.Size = New System.Drawing.Size(1, 242)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "label4"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label8.Location = New System.Drawing.Point(131, 1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 242)
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
        Me.Label9.Size = New System.Drawing.Size(132, 1)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "label1"
        '
        'pnlProblemsTitle
        '
        Me.pnlProblemsTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlProblemsTitle.Controls.Add(Me.pnllblProblems)
        Me.pnlProblemsTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlProblemsTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlProblemsTitle.Name = "pnlProblemsTitle"
        Me.pnlProblemsTitle.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlProblemsTitle.Size = New System.Drawing.Size(132, 30)
        Me.pnlProblemsTitle.TabIndex = 0
        '
        'pnllblProblems
        '
        Me.pnllblProblems.BackColor = System.Drawing.Color.Transparent
        Me.pnllblProblems.BackgroundImage = CType(resources.GetObject("pnllblProblems.BackgroundImage"), System.Drawing.Image)
        Me.pnllblProblems.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnllblProblems.Controls.Add(Me.btnProbExpand)
        Me.pnllblProblems.Controls.Add(Me.Label40)
        Me.pnllblProblems.Controls.Add(Me.Label41)
        Me.pnllblProblems.Controls.Add(Me.Label42)
        Me.pnllblProblems.Controls.Add(Me.lblProblems)
        Me.pnllblProblems.Controls.Add(Me.Label43)
        Me.pnllblProblems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnllblProblems.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnllblProblems.Location = New System.Drawing.Point(0, 0)
        Me.pnllblProblems.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnllblProblems.Name = "pnllblProblems"
        Me.pnllblProblems.Size = New System.Drawing.Size(132, 27)
        Me.pnllblProblems.TabIndex = 20
        '
        'btnProbExpand
        '
        Me.btnProbExpand.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnProbExpand.FlatAppearance.BorderSize = 0
        Me.btnProbExpand.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnProbExpand.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnProbExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProbExpand.Location = New System.Drawing.Point(107, 1)
        Me.btnProbExpand.Name = "btnProbExpand"
        Me.btnProbExpand.Size = New System.Drawing.Size(24, 25)
        Me.btnProbExpand.TabIndex = 9
        Me.btnProbExpand.UseVisualStyleBackColor = True
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label40.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label40.Location = New System.Drawing.Point(1, 26)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(130, 1)
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
        Me.Label42.Location = New System.Drawing.Point(131, 1)
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
        Me.lblProblems.Size = New System.Drawing.Size(132, 26)
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
        Me.Label43.Size = New System.Drawing.Size(132, 1)
        Me.Label43.TabIndex = 5
        Me.Label43.Text = "label1"
        '
        'tbpProblem
        '
        Me.tbpProblem.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpProblem.Controls.Add(Me.Panel28)
        Me.tbpProblem.Controls.Add(Me.pnlSearchproblemList)
        Me.tbpProblem.ImageIndex = 1
        Me.tbpProblem.Location = New System.Drawing.Point(4, 26)
        Me.tbpProblem.Name = "tbpProblem"
        Me.tbpProblem.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.tbpProblem.Size = New System.Drawing.Size(1010, 275)
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
        Me.Panel28.Location = New System.Drawing.Point(0, 26)
        Me.Panel28.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel28.Name = "Panel28"
        Me.Panel28.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel28.Size = New System.Drawing.Size(1010, 249)
        Me.Panel28.TabIndex = 22
        '
        'Label119
        '
        Me.Label119.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label119.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label119.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label119.Location = New System.Drawing.Point(1, 3)
        Me.Label119.Name = "Label119"
        Me.Label119.Size = New System.Drawing.Size(1008, 1)
        Me.Label119.TabIndex = 5
        Me.Label119.Text = "label1"
        '
        'Label116
        '
        Me.Label116.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label116.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label116.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label116.Location = New System.Drawing.Point(1, 248)
        Me.Label116.Name = "Label116"
        Me.Label116.Size = New System.Drawing.Size(1008, 1)
        Me.Label116.TabIndex = 8
        Me.Label116.Text = "label2"
        '
        'Label117
        '
        Me.Label117.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label117.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label117.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label117.Location = New System.Drawing.Point(0, 3)
        Me.Label117.Name = "Label117"
        Me.Label117.Size = New System.Drawing.Size(1, 246)
        Me.Label117.TabIndex = 7
        Me.Label117.Text = "label4"
        '
        'Label118
        '
        Me.Label118.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label118.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label118.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label118.Location = New System.Drawing.Point(1009, 3)
        Me.Label118.Name = "Label118"
        Me.Label118.Size = New System.Drawing.Size(1, 246)
        Me.Label118.TabIndex = 6
        Me.Label118.Text = "label3"
        '
        'c1ProblemList
        '
        Me.c1ProblemList.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.c1ProblemList.AllowEditing = False
        Me.c1ProblemList.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.c1ProblemList.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.c1ProblemList.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.c1ProblemList.ColumnInfo = "1,0,0,0,0,95,Columns:"
        Me.c1ProblemList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.c1ProblemList.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.c1ProblemList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.c1ProblemList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.c1ProblemList.Location = New System.Drawing.Point(0, 3)
        Me.c1ProblemList.Name = "c1ProblemList"
        Me.c1ProblemList.Rows.Count = 1
        Me.c1ProblemList.Rows.DefaultSize = 19
        Me.c1ProblemList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.c1ProblemList.Size = New System.Drawing.Size(1010, 246)
        Me.c1ProblemList.StyleInfo = resources.GetString("c1ProblemList.StyleInfo")
        Me.c1ProblemList.TabIndex = 16
        '
        'pnlSearchproblemList
        '
        Me.pnlSearchproblemList.Controls.Add(Me.pnlsearchProblems)
        Me.pnlSearchproblemList.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearchproblemList.Location = New System.Drawing.Point(0, 2)
        Me.pnlSearchproblemList.Name = "pnlSearchproblemList"
        Me.pnlSearchproblemList.Size = New System.Drawing.Size(1010, 24)
        Me.pnlSearchproblemList.TabIndex = 21
        '
        'pnlsearchProblems
        '
        Me.pnlsearchProblems.BackColor = System.Drawing.Color.Transparent
        Me.pnlsearchProblems.BackgroundImage = CType(resources.GetObject("pnlsearchProblems.BackgroundImage"), System.Drawing.Image)
        Me.pnlsearchProblems.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlsearchProblems.Controls.Add(Me.Label128)
        Me.pnlsearchProblems.Controls.Add(Me.Label129)
        Me.pnlsearchProblems.Controls.Add(Me.Label130)
        Me.pnlsearchProblems.Controls.Add(Me.Label131)
        Me.pnlsearchProblems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlsearchProblems.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlsearchProblems.Location = New System.Drawing.Point(0, 0)
        Me.pnlsearchProblems.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlsearchProblems.Name = "pnlsearchProblems"
        Me.pnlsearchProblems.Size = New System.Drawing.Size(1010, 24)
        Me.pnlsearchProblems.TabIndex = 19
        '
        'Label128
        '
        Me.Label128.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label128.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label128.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label128.Location = New System.Drawing.Point(1, 23)
        Me.Label128.Name = "Label128"
        Me.Label128.Size = New System.Drawing.Size(1008, 1)
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
        Me.Label129.Size = New System.Drawing.Size(1, 23)
        Me.Label129.TabIndex = 7
        Me.Label129.Text = "label4"
        '
        'Label130
        '
        Me.Label130.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label130.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label130.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label130.Location = New System.Drawing.Point(1009, 1)
        Me.Label130.Name = "Label130"
        Me.Label130.Size = New System.Drawing.Size(1, 23)
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
        Me.Label131.Size = New System.Drawing.Size(1010, 1)
        Me.Label131.TabIndex = 5
        Me.Label131.Text = "label1"
        '
        'tbpMedications
        '
        Me.tbpMedications.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpMedications.Controls.Add(Me.pnldgPatientDetails)
        Me.tbpMedications.Controls.Add(Me.pnlSearchMedications)
        Me.tbpMedications.ImageIndex = 2
        Me.tbpMedications.Location = New System.Drawing.Point(4, 26)
        Me.tbpMedications.Name = "tbpMedications"
        Me.tbpMedications.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.tbpMedications.Size = New System.Drawing.Size(1010, 275)
        Me.tbpMedications.TabIndex = 2
        Me.tbpMedications.Text = "Medications"
        Me.tbpMedications.UseVisualStyleBackColor = True
        '
        'pnldgPatientDetails
        '
        Me.pnldgPatientDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnldgPatientDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnldgPatientDetails.Controls.Add(Me.C1MedicationDetails)
        Me.pnldgPatientDetails.Controls.Add(Me.Label144)
        Me.pnldgPatientDetails.Controls.Add(Me.Label145)
        Me.pnldgPatientDetails.Controls.Add(Me.Label146)
        Me.pnldgPatientDetails.Controls.Add(Me.Label147)
        Me.pnldgPatientDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnldgPatientDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnldgPatientDetails.Location = New System.Drawing.Point(0, 26)
        Me.pnldgPatientDetails.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnldgPatientDetails.Name = "pnldgPatientDetails"
        Me.pnldgPatientDetails.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnldgPatientDetails.Size = New System.Drawing.Size(1010, 249)
        Me.pnldgPatientDetails.TabIndex = 23
        '
        'C1MedicationDetails
        '
        Me.C1MedicationDetails.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1MedicationDetails.AllowEditing = False
        Me.C1MedicationDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1MedicationDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1MedicationDetails.ColumnInfo = "1,0,0,0,0,95,Columns:"
        Me.C1MedicationDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1MedicationDetails.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1MedicationDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1MedicationDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1MedicationDetails.Location = New System.Drawing.Point(1, 4)
        Me.C1MedicationDetails.Name = "C1MedicationDetails"
        Me.C1MedicationDetails.Rows.Count = 1
        Me.C1MedicationDetails.Rows.DefaultSize = 19
        Me.C1MedicationDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1MedicationDetails.Size = New System.Drawing.Size(1008, 244)
        Me.C1MedicationDetails.StyleInfo = resources.GetString("C1MedicationDetails.StyleInfo")
        Me.C1MedicationDetails.TabIndex = 17
        '
        'Label144
        '
        Me.Label144.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label144.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label144.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label144.Location = New System.Drawing.Point(1, 248)
        Me.Label144.Name = "Label144"
        Me.Label144.Size = New System.Drawing.Size(1008, 1)
        Me.Label144.TabIndex = 8
        Me.Label144.Text = "label2"
        '
        'Label145
        '
        Me.Label145.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label145.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label145.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label145.Location = New System.Drawing.Point(0, 4)
        Me.Label145.Name = "Label145"
        Me.Label145.Size = New System.Drawing.Size(1, 245)
        Me.Label145.TabIndex = 7
        Me.Label145.Text = "label4"
        '
        'Label146
        '
        Me.Label146.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label146.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label146.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label146.Location = New System.Drawing.Point(1009, 4)
        Me.Label146.Name = "Label146"
        Me.Label146.Size = New System.Drawing.Size(1, 245)
        Me.Label146.TabIndex = 6
        Me.Label146.Text = "label3"
        '
        'Label147
        '
        Me.Label147.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label147.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label147.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label147.Location = New System.Drawing.Point(0, 3)
        Me.Label147.Name = "Label147"
        Me.Label147.Size = New System.Drawing.Size(1010, 1)
        Me.Label147.TabIndex = 5
        Me.Label147.Text = "label1"
        '
        'pnlSearchMedications
        '
        Me.pnlSearchMedications.Controls.Add(Me.pnlSearchMed)
        Me.pnlSearchMedications.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearchMedications.Location = New System.Drawing.Point(0, 2)
        Me.pnlSearchMedications.Name = "pnlSearchMedications"
        Me.pnlSearchMedications.Size = New System.Drawing.Size(1010, 24)
        Me.pnlSearchMedications.TabIndex = 21
        '
        'pnlSearchMed
        '
        Me.pnlSearchMed.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearchMed.BackgroundImage = CType(resources.GetObject("pnlSearchMed.BackgroundImage"), System.Drawing.Image)
        Me.pnlSearchMed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSearchMed.Controls.Add(Me.Label140)
        Me.pnlSearchMed.Controls.Add(Me.Label141)
        Me.pnlSearchMed.Controls.Add(Me.Label142)
        Me.pnlSearchMed.Controls.Add(Me.Label143)
        Me.pnlSearchMed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSearchMed.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearchMed.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearchMed.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlSearchMed.Name = "pnlSearchMed"
        Me.pnlSearchMed.Size = New System.Drawing.Size(1010, 24)
        Me.pnlSearchMed.TabIndex = 19
        '
        'Label140
        '
        Me.Label140.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label140.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label140.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label140.Location = New System.Drawing.Point(1, 23)
        Me.Label140.Name = "Label140"
        Me.Label140.Size = New System.Drawing.Size(1008, 1)
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
        Me.Label141.Size = New System.Drawing.Size(1, 23)
        Me.Label141.TabIndex = 7
        Me.Label141.Text = "label4"
        '
        'Label142
        '
        Me.Label142.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label142.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label142.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label142.Location = New System.Drawing.Point(1009, 1)
        Me.Label142.Name = "Label142"
        Me.Label142.Size = New System.Drawing.Size(1, 23)
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
        Me.Label143.Size = New System.Drawing.Size(1010, 1)
        Me.Label143.TabIndex = 5
        Me.Label143.Text = "label1"
        '
        'tbpAllergies
        '
        Me.tbpAllergies.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpAllergies.Controls.Add(Me.pnlC1dgPatientDetails)
        Me.tbpAllergies.Controls.Add(Me.pnlSearchAllergies)
        Me.tbpAllergies.ImageIndex = 3
        Me.tbpAllergies.Location = New System.Drawing.Point(4, 26)
        Me.tbpAllergies.Name = "tbpAllergies"
        Me.tbpAllergies.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.tbpAllergies.Size = New System.Drawing.Size(1010, 275)
        Me.tbpAllergies.TabIndex = 3
        Me.tbpAllergies.Text = "History"
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
        Me.pnlC1dgPatientDetails.Location = New System.Drawing.Point(0, 26)
        Me.pnlC1dgPatientDetails.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlC1dgPatientDetails.Name = "pnlC1dgPatientDetails"
        Me.pnlC1dgPatientDetails.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnlC1dgPatientDetails.Size = New System.Drawing.Size(1010, 249)
        Me.pnlC1dgPatientDetails.TabIndex = 21
        '
        'Label120
        '
        Me.Label120.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label120.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label120.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label120.Location = New System.Drawing.Point(1, 248)
        Me.Label120.Name = "Label120"
        Me.Label120.Size = New System.Drawing.Size(1008, 1)
        Me.Label120.TabIndex = 8
        Me.Label120.Text = "label2"
        '
        'Label121
        '
        Me.Label121.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label121.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label121.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label121.Location = New System.Drawing.Point(0, 4)
        Me.Label121.Name = "Label121"
        Me.Label121.Size = New System.Drawing.Size(1, 245)
        Me.Label121.TabIndex = 7
        Me.Label121.Text = "label4"
        '
        'Label122
        '
        Me.Label122.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label122.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label122.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label122.Location = New System.Drawing.Point(1009, 4)
        Me.Label122.Name = "Label122"
        Me.Label122.Size = New System.Drawing.Size(1, 245)
        Me.Label122.TabIndex = 6
        Me.Label122.Text = "label3"
        '
        'Label123
        '
        Me.Label123.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label123.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label123.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label123.Location = New System.Drawing.Point(0, 3)
        Me.Label123.Name = "Label123"
        Me.Label123.Size = New System.Drawing.Size(1010, 1)
        Me.Label123.TabIndex = 5
        Me.Label123.Text = "label1"
        '
        'C1dgPatientDetails
        '
        Me.C1dgPatientDetails.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1dgPatientDetails.AllowEditing = False
        Me.C1dgPatientDetails.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.None
        Me.C1dgPatientDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1dgPatientDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1dgPatientDetails.ColumnInfo = "1,0,0,0,0,95,Columns:"
        Me.C1dgPatientDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1dgPatientDetails.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None
        Me.C1dgPatientDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1dgPatientDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1dgPatientDetails.Location = New System.Drawing.Point(0, 3)
        Me.C1dgPatientDetails.Name = "C1dgPatientDetails"
        Me.C1dgPatientDetails.Rows.Count = 1
        Me.C1dgPatientDetails.Rows.DefaultSize = 19
        Me.C1dgPatientDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1dgPatientDetails.Size = New System.Drawing.Size(1010, 246)
        Me.C1dgPatientDetails.StyleInfo = resources.GetString("C1dgPatientDetails.StyleInfo")
        Me.C1dgPatientDetails.TabIndex = 16
        '
        'pnlSearchAllergies
        '
        Me.pnlSearchAllergies.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearchAllergies.BackgroundImage = CType(resources.GetObject("pnlSearchAllergies.BackgroundImage"), System.Drawing.Image)
        Me.pnlSearchAllergies.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSearchAllergies.Controls.Add(Me.Label4)
        Me.pnlSearchAllergies.Controls.Add(Me.Label5)
        Me.pnlSearchAllergies.Controls.Add(Me.Label13)
        Me.pnlSearchAllergies.Controls.Add(Me.Label14)
        Me.pnlSearchAllergies.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearchAllergies.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearchAllergies.Location = New System.Drawing.Point(0, 2)
        Me.pnlSearchAllergies.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlSearchAllergies.Name = "pnlSearchAllergies"
        Me.pnlSearchAllergies.Size = New System.Drawing.Size(1010, 24)
        Me.pnlSearchAllergies.TabIndex = 22
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label4.Location = New System.Drawing.Point(1, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1008, 1)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "label2"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(0, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 23)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "label4"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label13.Location = New System.Drawing.Point(1009, 1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(1, 23)
        Me.Label13.TabIndex = 6
        Me.Label13.Text = "label3"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(1010, 1)
        Me.Label14.TabIndex = 5
        Me.Label14.Text = "label1"
        '
        'tbProcedures
        '
        Me.tbProcedures.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbProcedures.Controls.Add(Me.pnltrProcedureDetails)
        Me.tbProcedures.Controls.Add(Me.pnlSearchProcedures)
        Me.tbProcedures.ImageIndex = 4
        Me.tbProcedures.Location = New System.Drawing.Point(4, 26)
        Me.tbProcedures.Name = "tbProcedures"
        Me.tbProcedures.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.tbProcedures.Size = New System.Drawing.Size(1010, 275)
        Me.tbProcedures.TabIndex = 6
        Me.tbProcedures.Text = "Procedures"
        Me.tbProcedures.UseVisualStyleBackColor = True
        '
        'pnltrProcedureDetails
        '
        Me.pnltrProcedureDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrProcedureDetails.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrProcedureDetails.Controls.Add(Me.pnlNormalSearchProc)
        Me.pnltrProcedureDetails.Controls.Add(Me.C1Dignosis)
        Me.pnltrProcedureDetails.Controls.Add(Me.trProcedureDetails)
        Me.pnltrProcedureDetails.Controls.Add(Me.Label21)
        Me.pnltrProcedureDetails.Controls.Add(Me.Label124)
        Me.pnltrProcedureDetails.Controls.Add(Me.lbl_WhiteSpaceTop)
        Me.pnltrProcedureDetails.Controls.Add(Me.Label127)
        Me.pnltrProcedureDetails.Controls.Add(Me.Label125)
        Me.pnltrProcedureDetails.Controls.Add(Me.Label126)
        Me.pnltrProcedureDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrProcedureDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrProcedureDetails.Location = New System.Drawing.Point(0, 26)
        Me.pnltrProcedureDetails.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnltrProcedureDetails.Name = "pnltrProcedureDetails"
        Me.pnltrProcedureDetails.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.pnltrProcedureDetails.Size = New System.Drawing.Size(1010, 249)
        Me.pnltrProcedureDetails.TabIndex = 40
        '
        'pnlNormalSearchProc
        '
        Me.pnlNormalSearchProc.Controls.Add(Me.Label163)
        Me.pnlNormalSearchProc.Controls.Add(Me.Label160)
        Me.pnlNormalSearchProc.Location = New System.Drawing.Point(861, 63)
        Me.pnlNormalSearchProc.Name = "pnlNormalSearchProc"
        Me.pnlNormalSearchProc.Size = New System.Drawing.Size(390, 22)
        Me.pnlNormalSearchProc.TabIndex = 10
        Me.pnlNormalSearchProc.Visible = False
        '
        'Label163
        '
        Me.Label163.BackColor = System.Drawing.Color.Transparent
        Me.Label163.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label163.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label163.Location = New System.Drawing.Point(329, 0)
        Me.Label163.Name = "Label163"
        Me.Label163.Size = New System.Drawing.Size(61, 22)
        Me.Label163.TabIndex = 50
        Me.Label163.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label160
        '
        Me.Label160.AutoSize = True
        Me.Label160.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label160.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label160.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label160.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label160.Location = New System.Drawing.Point(0, 0)
        Me.Label160.Name = "Label160"
        Me.Label160.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label160.Size = New System.Drawing.Size(4, 20)
        Me.Label160.TabIndex = 49
        Me.Label160.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'C1Dignosis
        '
        Me.C1Dignosis.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1Dignosis.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None
        Me.C1Dignosis.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Dignosis.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Dignosis.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1Dignosis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Dignosis.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Dignosis.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Dignosis.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Dignosis.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Dignosis.Location = New System.Drawing.Point(5, 8)
        Me.C1Dignosis.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.C1Dignosis.Name = "C1Dignosis"
        Me.C1Dignosis.Rows.Count = 1
        Me.C1Dignosis.Rows.DefaultSize = 19
        Me.C1Dignosis.Rows.Fixed = 0
        Me.C1Dignosis.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Dignosis.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Dignosis.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1Dignosis.Size = New System.Drawing.Size(1004, 240)
        Me.C1Dignosis.StyleInfo = resources.GetString("C1Dignosis.StyleInfo")
        Me.C1Dignosis.TabIndex = 40
        '
        'trProcedureDetails
        '
        Me.trProcedureDetails.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trProcedureDetails.ForeColor = System.Drawing.Color.Black
        Me.trProcedureDetails.ItemHeight = 19
        Me.trProcedureDetails.Location = New System.Drawing.Point(621, 125)
        Me.trProcedureDetails.Name = "trProcedureDetails"
        Me.trProcedureDetails.ShowLines = False
        Me.trProcedureDetails.ShowPlusMinus = False
        Me.trProcedureDetails.Size = New System.Drawing.Size(153, 87)
        Me.trProcedureDetails.TabIndex = 0
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.White
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label21.Location = New System.Drawing.Point(1, 8)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(4, 240)
        Me.Label21.TabIndex = 39
        Me.Label21.Visible = False
        '
        'Label124
        '
        Me.Label124.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label124.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label124.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label124.Location = New System.Drawing.Point(1, 248)
        Me.Label124.Name = "Label124"
        Me.Label124.Size = New System.Drawing.Size(1008, 1)
        Me.Label124.TabIndex = 8
        Me.Label124.Text = "label2"
        '
        'lbl_WhiteSpaceTop
        '
        Me.lbl_WhiteSpaceTop.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_WhiteSpaceTop.Location = New System.Drawing.Point(1, 4)
        Me.lbl_WhiteSpaceTop.Name = "lbl_WhiteSpaceTop"
        Me.lbl_WhiteSpaceTop.Size = New System.Drawing.Size(1008, 4)
        Me.lbl_WhiteSpaceTop.TabIndex = 38
        Me.lbl_WhiteSpaceTop.Visible = False
        '
        'Label127
        '
        Me.Label127.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label127.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label127.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label127.Location = New System.Drawing.Point(1, 3)
        Me.Label127.Name = "Label127"
        Me.Label127.Size = New System.Drawing.Size(1008, 1)
        Me.Label127.TabIndex = 5
        Me.Label127.Text = "label1"
        '
        'Label125
        '
        Me.Label125.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label125.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label125.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label125.Location = New System.Drawing.Point(0, 3)
        Me.Label125.Name = "Label125"
        Me.Label125.Size = New System.Drawing.Size(1, 246)
        Me.Label125.TabIndex = 7
        Me.Label125.Text = "label4"
        '
        'Label126
        '
        Me.Label126.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label126.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label126.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label126.Location = New System.Drawing.Point(1009, 3)
        Me.Label126.Name = "Label126"
        Me.Label126.Size = New System.Drawing.Size(1, 246)
        Me.Label126.TabIndex = 6
        Me.Label126.Text = "label3"
        '
        'pnlSearchProcedures
        '
        Me.pnlSearchProcedures.Controls.Add(Me.pnlSearchProc)
        Me.pnlSearchProcedures.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearchProcedures.Location = New System.Drawing.Point(0, 2)
        Me.pnlSearchProcedures.Name = "pnlSearchProcedures"
        Me.pnlSearchProcedures.Size = New System.Drawing.Size(1010, 24)
        Me.pnlSearchProcedures.TabIndex = 41
        '
        'pnlSearchProc
        '
        Me.pnlSearchProc.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearchProc.BackgroundImage = CType(resources.GetObject("pnlSearchProc.BackgroundImage"), System.Drawing.Image)
        Me.pnlSearchProc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSearchProc.Controls.Add(Me.Label164)
        Me.pnlSearchProc.Controls.Add(Me.Panel19)
        Me.pnlSearchProc.Controls.Add(Me.pnlDateSearch)
        Me.pnlSearchProc.Controls.Add(Me.cmbCriteria)
        Me.pnlSearchProc.Controls.Add(Me.lblSearchBy)
        Me.pnlSearchProc.Controls.Add(Me.Label132)
        Me.pnlSearchProc.Controls.Add(Me.Label133)
        Me.pnlSearchProc.Controls.Add(Me.Label134)
        Me.pnlSearchProc.Controls.Add(Me.Label135)
        Me.pnlSearchProc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSearchProc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearchProc.Location = New System.Drawing.Point(0, 0)
        Me.pnlSearchProc.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlSearchProc.Name = "pnlSearchProc"
        Me.pnlSearchProc.Size = New System.Drawing.Size(1010, 24)
        Me.pnlSearchProc.TabIndex = 19
        '
        'Label164
        '
        Me.Label164.AutoSize = True
        Me.Label164.BackColor = System.Drawing.Color.Transparent
        Me.Label164.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label164.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label164.Location = New System.Drawing.Point(704, 1)
        Me.Label164.Name = "Label164"
        Me.Label164.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Label164.Size = New System.Drawing.Size(64, 17)
        Me.Label164.TabIndex = 51
        Me.Label164.Text = "  Search : "
        Me.Label164.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel19
        '
        Me.Panel19.BackColor = System.Drawing.Color.Transparent
        Me.Panel19.Controls.Add(Me.txtsearchProcedures)
        Me.Panel19.Controls.Add(Me.Label187)
        Me.Panel19.Controls.Add(Me.Label188)
        Me.Panel19.Controls.Add(Me.Label189)
        Me.Panel19.Controls.Add(Me.btnClearProcedures)
        Me.Panel19.Controls.Add(Me.Label190)
        Me.Panel19.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel19.ForeColor = System.Drawing.Color.Black
        Me.Panel19.Location = New System.Drawing.Point(768, 1)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(241, 22)
        Me.Panel19.TabIndex = 44
        '
        'txtsearchProcedures
        '
        Me.txtsearchProcedures.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtsearchProcedures.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtsearchProcedures.ForeColor = System.Drawing.Color.Black
        Me.txtsearchProcedures.Location = New System.Drawing.Point(5, 3)
        Me.txtsearchProcedures.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.txtsearchProcedures.Name = "txtsearchProcedures"
        Me.txtsearchProcedures.Size = New System.Drawing.Size(215, 15)
        Me.txtsearchProcedures.TabIndex = 11
        '
        'Label187
        '
        Me.Label187.BackColor = System.Drawing.Color.White
        Me.Label187.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label187.Location = New System.Drawing.Point(5, 17)
        Me.Label187.Name = "Label187"
        Me.Label187.Size = New System.Drawing.Size(215, 5)
        Me.Label187.TabIndex = 43
        '
        'Label188
        '
        Me.Label188.BackColor = System.Drawing.Color.White
        Me.Label188.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label188.Location = New System.Drawing.Point(5, 0)
        Me.Label188.Name = "Label188"
        Me.Label188.Size = New System.Drawing.Size(215, 3)
        Me.Label188.TabIndex = 37
        '
        'Label189
        '
        Me.Label189.BackColor = System.Drawing.Color.White
        Me.Label189.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label189.Location = New System.Drawing.Point(1, 0)
        Me.Label189.Name = "Label189"
        Me.Label189.Size = New System.Drawing.Size(4, 22)
        Me.Label189.TabIndex = 38
        '
        'btnClearProcedures
        '
        Me.btnClearProcedures.BackgroundImage = CType(resources.GetObject("btnClearProcedures.BackgroundImage"), System.Drawing.Image)
        Me.btnClearProcedures.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearProcedures.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearProcedures.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearProcedures.FlatAppearance.BorderSize = 0
        Me.btnClearProcedures.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearProcedures.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearProcedures.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearProcedures.Image = CType(resources.GetObject("btnClearProcedures.Image"), System.Drawing.Image)
        Me.btnClearProcedures.Location = New System.Drawing.Point(220, 0)
        Me.btnClearProcedures.Name = "btnClearProcedures"
        Me.btnClearProcedures.Size = New System.Drawing.Size(21, 22)
        Me.btnClearProcedures.TabIndex = 41
        Me.btnClearProcedures.UseVisualStyleBackColor = True
        '
        'Label190
        '
        Me.Label190.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label190.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label190.Location = New System.Drawing.Point(0, 0)
        Me.Label190.Name = "Label190"
        Me.Label190.Size = New System.Drawing.Size(1, 22)
        Me.Label190.TabIndex = 39
        Me.Label190.Text = "label4"
        '
        'pnlDateSearch
        '
        Me.pnlDateSearch.Controls.Add(Me.dtpToDate)
        Me.pnlDateSearch.Controls.Add(Me.lblToDate)
        Me.pnlDateSearch.Controls.Add(Me.dtpFromDate)
        Me.pnlDateSearch.Controls.Add(Me.lblFromDate)
        Me.pnlDateSearch.Controls.Add(Me.Label159)
        Me.pnlDateSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlDateSearch.Location = New System.Drawing.Point(200, 1)
        Me.pnlDateSearch.Name = "pnlDateSearch"
        Me.pnlDateSearch.Size = New System.Drawing.Size(434, 22)
        Me.pnlDateSearch.TabIndex = 42
        '
        'dtpToDate
        '
        Me.dtpToDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpToDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpToDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpToDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpToDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpToDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpToDate.Location = New System.Drawing.Point(266, 0)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(98, 22)
        Me.dtpToDate.TabIndex = 40
        '
        'lblToDate
        '
        Me.lblToDate.BackColor = System.Drawing.Color.Transparent
        Me.lblToDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblToDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(198, 0)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(68, 22)
        Me.lblToDate.TabIndex = 39
        Me.lblToDate.Text = "To :"
        Me.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpFromDate.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpFromDate.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpFromDate.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpFromDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpFromDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFromDate.Location = New System.Drawing.Point(100, 0)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(98, 22)
        Me.dtpFromDate.TabIndex = 38
        '
        'lblFromDate
        '
        Me.lblFromDate.BackColor = System.Drawing.Color.Transparent
        Me.lblFromDate.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblFromDate.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.Location = New System.Drawing.Point(44, 0)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(56, 22)
        Me.lblFromDate.TabIndex = 37
        Me.lblFromDate.Text = "From :"
        Me.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label159
        '
        Me.Label159.BackColor = System.Drawing.Color.Transparent
        Me.Label159.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label159.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label159.Location = New System.Drawing.Point(0, 0)
        Me.Label159.Name = "Label159"
        Me.Label159.Size = New System.Drawing.Size(44, 22)
        Me.Label159.TabIndex = 41
        Me.Label159.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbCriteria
        '
        Me.cmbCriteria.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCriteria.ForeColor = System.Drawing.Color.Black
        Me.cmbCriteria.FormattingEnabled = True
        Me.cmbCriteria.Location = New System.Drawing.Point(92, 1)
        Me.cmbCriteria.Name = "cmbCriteria"
        Me.cmbCriteria.Size = New System.Drawing.Size(108, 22)
        Me.cmbCriteria.TabIndex = 41
        '
        'lblSearchBy
        '
        Me.lblSearchBy.BackColor = System.Drawing.Color.Transparent
        Me.lblSearchBy.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblSearchBy.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearchBy.Location = New System.Drawing.Point(1, 1)
        Me.lblSearchBy.Name = "lblSearchBy"
        Me.lblSearchBy.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.lblSearchBy.Size = New System.Drawing.Size(91, 22)
        Me.lblSearchBy.TabIndex = 36
        Me.lblSearchBy.Text = "  Search by : "
        Me.lblSearchBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label132
        '
        Me.Label132.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label132.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label132.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label132.Location = New System.Drawing.Point(1, 23)
        Me.Label132.Name = "Label132"
        Me.Label132.Size = New System.Drawing.Size(1008, 1)
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
        Me.Label133.Size = New System.Drawing.Size(1, 23)
        Me.Label133.TabIndex = 7
        Me.Label133.Text = "label4"
        '
        'Label134
        '
        Me.Label134.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label134.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label134.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label134.Location = New System.Drawing.Point(1009, 1)
        Me.Label134.Name = "Label134"
        Me.Label134.Size = New System.Drawing.Size(1, 23)
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
        Me.Label135.Size = New System.Drawing.Size(1010, 1)
        Me.Label135.TabIndex = 5
        Me.Label135.Text = "label1"
        '
        'tbpLabs
        '
        Me.tbpLabs.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpLabs.Controls.Add(Me.pnlLabDetails)
        Me.tbpLabs.ImageIndex = 5
        Me.tbpLabs.Location = New System.Drawing.Point(4, 26)
        Me.tbpLabs.Name = "tbpLabs"
        Me.tbpLabs.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.tbpLabs.Size = New System.Drawing.Size(1010, 275)
        Me.tbpLabs.TabIndex = 5
        Me.tbpLabs.Text = "Orders && Results"
        Me.tbpLabs.UseVisualStyleBackColor = True
        '
        'pnlLabDetails
        '
        Me.pnlLabDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLabDetails.Location = New System.Drawing.Point(0, 2)
        Me.pnlLabDetails.Name = "pnlLabDetails"
        Me.pnlLabDetails.Size = New System.Drawing.Size(1010, 273)
        Me.pnlLabDetails.TabIndex = 42
        '
        'tbpImaging
        '
        Me.tbpImaging.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpImaging.Controls.Add(Me.Panel3)
        Me.tbpImaging.Controls.Add(Me.pnlSearchImaging)
        Me.tbpImaging.ImageIndex = 6
        Me.tbpImaging.Location = New System.Drawing.Point(4, 26)
        Me.tbpImaging.Name = "tbpImaging"
        Me.tbpImaging.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.tbpImaging.Size = New System.Drawing.Size(1010, 275)
        Me.tbpImaging.TabIndex = 4
        Me.tbpImaging.Text = "Order Templates"
        Me.tbpImaging.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.pnlSearch)
        Me.Panel3.Controls.Add(Me.Label20)
        Me.Panel3.Controls.Add(Me.Label30)
        Me.Panel3.Controls.Add(Me.C1OrderDetails)
        Me.Panel3.Controls.Add(Me.Label76)
        Me.Panel3.Controls.Add(Me.Label77)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Location = New System.Drawing.Point(0, 26)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel3.Size = New System.Drawing.Size(1010, 249)
        Me.Panel3.TabIndex = 20
        '
        'pnlSearch
        '
        Me.pnlSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlSearch.Controls.Add(Me.Label165)
        Me.pnlSearch.Controls.Add(Me.Label162)
        Me.pnlSearch.Location = New System.Drawing.Point(627, 31)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(390, 22)
        Me.pnlSearch.TabIndex = 9
        Me.pnlSearch.Visible = False
        '
        'Label165
        '
        Me.Label165.BackColor = System.Drawing.Color.Transparent
        Me.Label165.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label165.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label165.Location = New System.Drawing.Point(321, 0)
        Me.Label165.Name = "Label165"
        Me.Label165.Size = New System.Drawing.Size(69, 22)
        Me.Label165.TabIndex = 51
        Me.Label165.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label162
        '
        Me.Label162.AutoSize = True
        Me.Label162.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label162.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label162.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label162.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label162.Location = New System.Drawing.Point(0, 0)
        Me.Label162.Name = "Label162"
        Me.Label162.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label162.Size = New System.Drawing.Size(4, 20)
        Me.Label162.TabIndex = 49
        Me.Label162.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label20.Location = New System.Drawing.Point(1, 248)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(1008, 1)
        Me.Label20.TabIndex = 8
        Me.Label20.Text = "label2"
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(0, 4)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(1, 245)
        Me.Label30.TabIndex = 7
        Me.Label30.Text = "label4"
        '
        'C1OrderDetails
        '
        Me.C1OrderDetails.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1OrderDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1OrderDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1OrderDetails.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1OrderDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1OrderDetails.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid
        Me.C1OrderDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1OrderDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1OrderDetails.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1OrderDetails.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1OrderDetails.Location = New System.Drawing.Point(0, 4)
        Me.C1OrderDetails.Name = "C1OrderDetails"
        Me.C1OrderDetails.Rows.Count = 1
        Me.C1OrderDetails.Rows.DefaultSize = 19
        Me.C1OrderDetails.Rows.Fixed = 0
        Me.C1OrderDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1OrderDetails.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1OrderDetails.Size = New System.Drawing.Size(1009, 245)
        Me.C1OrderDetails.StyleInfo = resources.GetString("C1OrderDetails.StyleInfo")
        Me.C1OrderDetails.TabIndex = 39
        Me.C1OrderDetails.Tree.NodeImageCollapsed = CType(resources.GetObject("C1OrderDetails.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1OrderDetails.Tree.NodeImageExpanded = CType(resources.GetObject("C1OrderDetails.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Label76
        '
        Me.Label76.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label76.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label76.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label76.Location = New System.Drawing.Point(1009, 4)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(1, 245)
        Me.Label76.TabIndex = 6
        Me.Label76.Text = "label3"
        '
        'Label77
        '
        Me.Label77.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label77.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label77.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label77.Location = New System.Drawing.Point(0, 3)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(1010, 1)
        Me.Label77.TabIndex = 5
        Me.Label77.Text = "label1"
        '
        'pnlSearchImaging
        '
        Me.pnlSearchImaging.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearchImaging.BackgroundImage = CType(resources.GetObject("pnlSearchImaging.BackgroundImage"), System.Drawing.Image)
        Me.pnlSearchImaging.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlSearchImaging.Controls.Add(Me.Label166)
        Me.pnlSearchImaging.Controls.Add(Me.Panel18)
        Me.pnlSearchImaging.Controls.Add(Me.Label15)
        Me.pnlSearchImaging.Controls.Add(Me.Label17)
        Me.pnlSearchImaging.Controls.Add(Me.Label18)
        Me.pnlSearchImaging.Controls.Add(Me.Label19)
        Me.pnlSearchImaging.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlSearchImaging.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlSearchImaging.Location = New System.Drawing.Point(0, 2)
        Me.pnlSearchImaging.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlSearchImaging.Name = "pnlSearchImaging"
        Me.pnlSearchImaging.Size = New System.Drawing.Size(1010, 24)
        Me.pnlSearchImaging.TabIndex = 40
        '
        'Label166
        '
        Me.Label166.AutoSize = True
        Me.Label166.BackColor = System.Drawing.Color.Transparent
        Me.Label166.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label166.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label166.Location = New System.Drawing.Point(704, 1)
        Me.Label166.Name = "Label166"
        Me.Label166.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Label166.Size = New System.Drawing.Size(64, 17)
        Me.Label166.TabIndex = 52
        Me.Label166.Text = "  Search : "
        Me.Label166.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel18
        '
        Me.Panel18.BackColor = System.Drawing.Color.Transparent
        Me.Panel18.Controls.Add(Me.txtSearch)
        Me.Panel18.Controls.Add(Me.Label180)
        Me.Panel18.Controls.Add(Me.Label181)
        Me.Panel18.Controls.Add(Me.Label182)
        Me.Panel18.Controls.Add(Me.btnClear)
        Me.Panel18.Controls.Add(Me.Label183)
        Me.Panel18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel18.ForeColor = System.Drawing.Color.Black
        Me.Panel18.Location = New System.Drawing.Point(768, 1)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(241, 22)
        Me.Panel18.TabIndex = 44
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearch.Location = New System.Drawing.Point(5, 3)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(215, 15)
        Me.txtSearch.TabIndex = 42
        '
        'Label180
        '
        Me.Label180.BackColor = System.Drawing.Color.White
        Me.Label180.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label180.Location = New System.Drawing.Point(5, 17)
        Me.Label180.Name = "Label180"
        Me.Label180.Size = New System.Drawing.Size(215, 5)
        Me.Label180.TabIndex = 43
        '
        'Label181
        '
        Me.Label181.BackColor = System.Drawing.Color.White
        Me.Label181.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label181.Location = New System.Drawing.Point(5, 0)
        Me.Label181.Name = "Label181"
        Me.Label181.Size = New System.Drawing.Size(215, 3)
        Me.Label181.TabIndex = 37
        '
        'Label182
        '
        Me.Label182.BackColor = System.Drawing.Color.White
        Me.Label182.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label182.Location = New System.Drawing.Point(1, 0)
        Me.Label182.Name = "Label182"
        Me.Label182.Size = New System.Drawing.Size(4, 22)
        Me.Label182.TabIndex = 38
        '
        'btnClear
        '
        Me.btnClear.BackgroundImage = CType(resources.GetObject("btnClear.BackgroundImage"), System.Drawing.Image)
        Me.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClear.FlatAppearance.BorderSize = 0
        Me.btnClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClear.Image = CType(resources.GetObject("btnClear.Image"), System.Drawing.Image)
        Me.btnClear.Location = New System.Drawing.Point(220, 0)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(21, 22)
        Me.btnClear.TabIndex = 41
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Label183
        '
        Me.Label183.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label183.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label183.Location = New System.Drawing.Point(0, 0)
        Me.Label183.Name = "Label183"
        Me.Label183.Size = New System.Drawing.Size(1, 22)
        Me.Label183.TabIndex = 39
        Me.Label183.Text = "label4"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label15.Location = New System.Drawing.Point(1, 23)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(1008, 1)
        Me.Label15.TabIndex = 8
        Me.Label15.Text = "label2"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(0, 1)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1, 23)
        Me.Label17.TabIndex = 7
        Me.Label17.Text = "label4"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label18.Location = New System.Drawing.Point(1009, 1)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(1, 23)
        Me.Label18.TabIndex = 6
        Me.Label18.Text = "label3"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(0, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(1010, 1)
        Me.Label19.TabIndex = 5
        Me.Label19.Text = "label1"
        '
        'tbpImagingST
        '
        Me.tbpImagingST.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpImagingST.Controls.Add(Me.Panel6)
        Me.tbpImagingST.Controls.Add(Me.Panel1)
        Me.tbpImagingST.ImageIndex = 12
        Me.tbpImagingST.Location = New System.Drawing.Point(4, 26)
        Me.tbpImagingST.Name = "tbpImagingST"
        Me.tbpImagingST.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.tbpImagingST.Size = New System.Drawing.Size(1010, 275)
        Me.tbpImagingST.TabIndex = 7
        Me.tbpImagingST.Text = "Imaging"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Transparent
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel6.Controls.Add(Me.Panel2)
        Me.Panel6.Controls.Add(Me.Label115)
        Me.Panel6.Controls.Add(Me.Label136)
        Me.Panel6.Controls.Add(Me.C1CV_StressTest)
        Me.Panel6.Controls.Add(Me.Label137)
        Me.Panel6.Controls.Add(Me.Label138)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel6.Location = New System.Drawing.Point(0, 26)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel6.Size = New System.Drawing.Size(1010, 249)
        Me.Panel6.TabIndex = 42
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label168)
        Me.Panel2.Controls.Add(Me.Label161)
        Me.Panel2.Location = New System.Drawing.Point(763, 46)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(496, 22)
        Me.Panel2.TabIndex = 9
        Me.Panel2.Visible = False
        '
        'Label168
        '
        Me.Label168.BackColor = System.Drawing.Color.Transparent
        Me.Label168.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label168.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label168.Location = New System.Drawing.Point(427, 0)
        Me.Label168.Name = "Label168"
        Me.Label168.Size = New System.Drawing.Size(69, 22)
        Me.Label168.TabIndex = 53
        Me.Label168.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label161
        '
        Me.Label161.AutoSize = True
        Me.Label161.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label161.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label161.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label161.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label161.Location = New System.Drawing.Point(0, 0)
        Me.Label161.Name = "Label161"
        Me.Label161.Padding = New System.Windows.Forms.Padding(2, 4, 2, 2)
        Me.Label161.Size = New System.Drawing.Size(4, 20)
        Me.Label161.TabIndex = 49
        Me.Label161.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label115
        '
        Me.Label115.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label115.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label115.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label115.Location = New System.Drawing.Point(1, 248)
        Me.Label115.Name = "Label115"
        Me.Label115.Size = New System.Drawing.Size(1008, 1)
        Me.Label115.TabIndex = 8
        Me.Label115.Text = "label2"
        '
        'Label136
        '
        Me.Label136.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label136.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label136.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label136.Location = New System.Drawing.Point(0, 4)
        Me.Label136.Name = "Label136"
        Me.Label136.Size = New System.Drawing.Size(1, 245)
        Me.Label136.TabIndex = 7
        Me.Label136.Text = "label4"
        '
        'C1CV_StressTest
        '
        Me.C1CV_StressTest.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1CV_StressTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1CV_StressTest.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1CV_StressTest.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1CV_StressTest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1CV_StressTest.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid
        Me.C1CV_StressTest.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1CV_StressTest.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1CV_StressTest.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1CV_StressTest.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1CV_StressTest.Location = New System.Drawing.Point(0, 4)
        Me.C1CV_StressTest.Name = "C1CV_StressTest"
        Me.C1CV_StressTest.Rows.Count = 1
        Me.C1CV_StressTest.Rows.DefaultSize = 19
        Me.C1CV_StressTest.Rows.Fixed = 0
        Me.C1CV_StressTest.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1CV_StressTest.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1CV_StressTest.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1CV_StressTest.Size = New System.Drawing.Size(1009, 245)
        Me.C1CV_StressTest.StyleInfo = resources.GetString("C1CV_StressTest.StyleInfo")
        Me.C1CV_StressTest.TabIndex = 39
        Me.C1CV_StressTest.Tree.NodeImageCollapsed = CType(resources.GetObject("C1CV_StressTest.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1CV_StressTest.Tree.NodeImageExpanded = CType(resources.GetObject("C1CV_StressTest.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Label137
        '
        Me.Label137.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label137.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label137.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label137.Location = New System.Drawing.Point(1009, 4)
        Me.Label137.Name = "Label137"
        Me.Label137.Size = New System.Drawing.Size(1, 245)
        Me.Label137.TabIndex = 6
        Me.Label137.Text = "label3"
        '
        'Label138
        '
        Me.Label138.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label138.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label138.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label138.Location = New System.Drawing.Point(0, 3)
        Me.Label138.Name = "Label138"
        Me.Label138.Size = New System.Drawing.Size(1010, 1)
        Me.Label138.TabIndex = 5
        Me.Label138.Text = "label1"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.Label167)
        Me.Panel1.Controls.Add(Me.Panel11)
        Me.Panel1.Controls.Add(Me.Label95)
        Me.Panel1.Controls.Add(Me.Label106)
        Me.Panel1.Controls.Add(Me.Label109)
        Me.Panel1.Controls.Add(Me.Label110)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 2)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1010, 24)
        Me.Panel1.TabIndex = 41
        '
        'Label167
        '
        Me.Label167.BackColor = System.Drawing.Color.Transparent
        Me.Label167.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label167.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label167.Location = New System.Drawing.Point(708, 1)
        Me.Label167.MaximumSize = New System.Drawing.Size(60, 18)
        Me.Label167.MinimumSize = New System.Drawing.Size(60, 18)
        Me.Label167.Name = "Label167"
        Me.Label167.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Label167.Size = New System.Drawing.Size(60, 18)
        Me.Label167.TabIndex = 54
        Me.Label167.Text = "Search : "
        Me.Label167.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.Transparent
        Me.Panel11.Controls.Add(Me.txtImagingSearch)
        Me.Panel11.Controls.Add(Me.Label172)
        Me.Panel11.Controls.Add(Me.Label174)
        Me.Panel11.Controls.Add(Me.Label175)
        Me.Panel11.Controls.Add(Me.btnClearImaging)
        Me.Panel11.Controls.Add(Me.Label176)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel11.ForeColor = System.Drawing.Color.Black
        Me.Panel11.Location = New System.Drawing.Point(768, 1)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(241, 22)
        Me.Panel11.TabIndex = 44
        '
        'txtImagingSearch
        '
        Me.txtImagingSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtImagingSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtImagingSearch.ForeColor = System.Drawing.Color.Black
        Me.txtImagingSearch.Location = New System.Drawing.Point(5, 3)
        Me.txtImagingSearch.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.txtImagingSearch.Name = "txtImagingSearch"
        Me.txtImagingSearch.Size = New System.Drawing.Size(215, 15)
        Me.txtImagingSearch.TabIndex = 11
        '
        'Label172
        '
        Me.Label172.BackColor = System.Drawing.Color.White
        Me.Label172.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label172.Location = New System.Drawing.Point(5, 17)
        Me.Label172.Name = "Label172"
        Me.Label172.Size = New System.Drawing.Size(215, 5)
        Me.Label172.TabIndex = 43
        '
        'Label174
        '
        Me.Label174.BackColor = System.Drawing.Color.White
        Me.Label174.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label174.Location = New System.Drawing.Point(5, 0)
        Me.Label174.Name = "Label174"
        Me.Label174.Size = New System.Drawing.Size(215, 3)
        Me.Label174.TabIndex = 37
        '
        'Label175
        '
        Me.Label175.BackColor = System.Drawing.Color.White
        Me.Label175.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label175.Location = New System.Drawing.Point(1, 0)
        Me.Label175.Name = "Label175"
        Me.Label175.Size = New System.Drawing.Size(4, 22)
        Me.Label175.TabIndex = 38
        '
        'btnClearImaging
        '
        Me.btnClearImaging.BackgroundImage = CType(resources.GetObject("btnClearImaging.BackgroundImage"), System.Drawing.Image)
        Me.btnClearImaging.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnClearImaging.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClearImaging.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClearImaging.FlatAppearance.BorderSize = 0
        Me.btnClearImaging.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnClearImaging.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClearImaging.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearImaging.Image = CType(resources.GetObject("btnClearImaging.Image"), System.Drawing.Image)
        Me.btnClearImaging.Location = New System.Drawing.Point(220, 0)
        Me.btnClearImaging.Name = "btnClearImaging"
        Me.btnClearImaging.Size = New System.Drawing.Size(21, 22)
        Me.btnClearImaging.TabIndex = 41
        Me.btnClearImaging.UseVisualStyleBackColor = True
        '
        'Label176
        '
        Me.Label176.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label176.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label176.Location = New System.Drawing.Point(0, 0)
        Me.Label176.Name = "Label176"
        Me.Label176.Size = New System.Drawing.Size(1, 22)
        Me.Label176.TabIndex = 39
        Me.Label176.Text = "label4"
        '
        'Label95
        '
        Me.Label95.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label95.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label95.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label95.Location = New System.Drawing.Point(1, 23)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(1008, 1)
        Me.Label95.TabIndex = 8
        Me.Label95.Text = "label2"
        '
        'Label106
        '
        Me.Label106.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label106.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label106.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label106.Location = New System.Drawing.Point(0, 1)
        Me.Label106.Name = "Label106"
        Me.Label106.Size = New System.Drawing.Size(1, 23)
        Me.Label106.TabIndex = 7
        Me.Label106.Text = "label4"
        '
        'Label109
        '
        Me.Label109.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label109.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label109.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label109.Location = New System.Drawing.Point(1009, 1)
        Me.Label109.Name = "Label109"
        Me.Label109.Size = New System.Drawing.Size(1, 23)
        Me.Label109.TabIndex = 6
        Me.Label109.Text = "label3"
        '
        'Label110
        '
        Me.Label110.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label110.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label110.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label110.Location = New System.Drawing.Point(0, 0)
        Me.Label110.Name = "Label110"
        Me.Label110.Size = New System.Drawing.Size(1010, 1)
        Me.Label110.TabIndex = 5
        Me.Label110.Text = "label1"
        '
        'tbpImplant
        '
        Me.tbpImplant.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpImplant.Controls.Add(Me.Panel7)
        Me.tbpImplant.Controls.Add(Me.Panel4)
        Me.tbpImplant.ImageIndex = 11
        Me.tbpImplant.Location = New System.Drawing.Point(4, 26)
        Me.tbpImplant.Name = "tbpImplant"
        Me.tbpImplant.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.tbpImplant.Size = New System.Drawing.Size(1010, 275)
        Me.tbpImplant.TabIndex = 8
        Me.tbpImplant.Text = "Implant"
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel7.Controls.Add(Me.Label139)
        Me.Panel7.Controls.Add(Me.Label148)
        Me.Panel7.Controls.Add(Me.C1Cardiology)
        Me.Panel7.Controls.Add(Me.Label149)
        Me.Panel7.Controls.Add(Me.Label150)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel7.Location = New System.Drawing.Point(0, 26)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel7.Size = New System.Drawing.Size(1010, 249)
        Me.Panel7.TabIndex = 42
        '
        'Label139
        '
        Me.Label139.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label139.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label139.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label139.Location = New System.Drawing.Point(1, 248)
        Me.Label139.Name = "Label139"
        Me.Label139.Size = New System.Drawing.Size(1008, 1)
        Me.Label139.TabIndex = 8
        Me.Label139.Text = "label2"
        '
        'Label148
        '
        Me.Label148.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label148.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label148.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label148.Location = New System.Drawing.Point(0, 4)
        Me.Label148.Name = "Label148"
        Me.Label148.Size = New System.Drawing.Size(1, 245)
        Me.Label148.TabIndex = 7
        Me.Label148.Text = "label4"
        '
        'C1Cardiology
        '
        Me.C1Cardiology.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.C1Cardiology.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.C1Cardiology.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1Cardiology.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.C1Cardiology.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1Cardiology.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid
        Me.C1Cardiology.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1Cardiology.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1Cardiology.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Cardiology.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcross
        Me.C1Cardiology.Location = New System.Drawing.Point(0, 4)
        Me.C1Cardiology.Name = "C1Cardiology"
        Me.C1Cardiology.Rows.Count = 1
        Me.C1Cardiology.Rows.DefaultSize = 19
        Me.C1Cardiology.Rows.Fixed = 0
        Me.C1Cardiology.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1Cardiology.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.C1Cardiology.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.C1Cardiology.Size = New System.Drawing.Size(1009, 245)
        Me.C1Cardiology.StyleInfo = resources.GetString("C1Cardiology.StyleInfo")
        Me.C1Cardiology.TabIndex = 39
        Me.C1Cardiology.Tree.NodeImageCollapsed = CType(resources.GetObject("C1Cardiology.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1Cardiology.Tree.NodeImageExpanded = CType(resources.GetObject("C1Cardiology.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Label149
        '
        Me.Label149.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label149.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label149.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label149.Location = New System.Drawing.Point(1009, 4)
        Me.Label149.Name = "Label149"
        Me.Label149.Size = New System.Drawing.Size(1, 245)
        Me.Label149.TabIndex = 6
        Me.Label149.Text = "label3"
        '
        'Label150
        '
        Me.Label150.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label150.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label150.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label150.Location = New System.Drawing.Point(0, 3)
        Me.Label150.Name = "Label150"
        Me.Label150.Size = New System.Drawing.Size(1010, 1)
        Me.Label150.TabIndex = 5
        Me.Label150.Text = "label1"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImage = CType(resources.GetObject("Panel4.BackgroundImage"), System.Drawing.Image)
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Controls.Add(Me.pnlSearchImplant)
        Me.Panel4.Controls.Add(Me.Label111)
        Me.Panel4.Controls.Add(Me.Label112)
        Me.Panel4.Controls.Add(Me.Label113)
        Me.Panel4.Controls.Add(Me.Label114)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.Location = New System.Drawing.Point(0, 2)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1010, 24)
        Me.Panel4.TabIndex = 41
        '
        'pnlSearchImplant
        '
        Me.pnlSearchImplant.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlSearchImplant.Location = New System.Drawing.Point(584, 1)
        Me.pnlSearchImplant.Name = "pnlSearchImplant"
        Me.pnlSearchImplant.Size = New System.Drawing.Size(425, 22)
        Me.pnlSearchImplant.TabIndex = 9
        '
        'Label111
        '
        Me.Label111.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label111.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label111.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label111.Location = New System.Drawing.Point(1, 23)
        Me.Label111.Name = "Label111"
        Me.Label111.Size = New System.Drawing.Size(1008, 1)
        Me.Label111.TabIndex = 8
        Me.Label111.Text = "label2"
        '
        'Label112
        '
        Me.Label112.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label112.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label112.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label112.Location = New System.Drawing.Point(0, 1)
        Me.Label112.Name = "Label112"
        Me.Label112.Size = New System.Drawing.Size(1, 23)
        Me.Label112.TabIndex = 7
        Me.Label112.Text = "label4"
        '
        'Label113
        '
        Me.Label113.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label113.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label113.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label113.Location = New System.Drawing.Point(1009, 1)
        Me.Label113.Name = "Label113"
        Me.Label113.Size = New System.Drawing.Size(1, 23)
        Me.Label113.TabIndex = 6
        Me.Label113.Text = "label3"
        '
        'Label114
        '
        Me.Label114.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label114.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label114.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label114.Location = New System.Drawing.Point(0, 0)
        Me.Label114.Name = "Label114"
        Me.Label114.Size = New System.Drawing.Size(1010, 1)
        Me.Label114.TabIndex = 5
        Me.Label114.Text = "label1"
        '
        'tbpEjectFrac
        '
        Me.tbpEjectFrac.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbpEjectFrac.Controls.Add(Me.Panel10)
        Me.tbpEjectFrac.Controls.Add(Me.Panel8)
        Me.tbpEjectFrac.ImageIndex = 17
        Me.tbpEjectFrac.Location = New System.Drawing.Point(4, 26)
        Me.tbpEjectFrac.Name = "tbpEjectFrac"
        Me.tbpEjectFrac.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.tbpEjectFrac.Size = New System.Drawing.Size(1010, 275)
        Me.tbpEjectFrac.TabIndex = 9
        Me.tbpEjectFrac.Text = "Ejection Fraction"
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.Transparent
        Me.Panel10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel10.Controls.Add(Me.Label155)
        Me.Panel10.Controls.Add(Me.Label156)
        Me.Panel10.Controls.Add(Me.cfgEjectionFraction)
        Me.Panel10.Controls.Add(Me.Label157)
        Me.Panel10.Controls.Add(Me.Label158)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel10.Location = New System.Drawing.Point(0, 26)
        Me.Panel10.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Panel10.Size = New System.Drawing.Size(1010, 249)
        Me.Panel10.TabIndex = 43
        '
        'Label155
        '
        Me.Label155.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label155.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label155.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label155.Location = New System.Drawing.Point(1, 248)
        Me.Label155.Name = "Label155"
        Me.Label155.Size = New System.Drawing.Size(1008, 1)
        Me.Label155.TabIndex = 8
        Me.Label155.Text = "label2"
        '
        'Label156
        '
        Me.Label156.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label156.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label156.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label156.Location = New System.Drawing.Point(0, 4)
        Me.Label156.Name = "Label156"
        Me.Label156.Size = New System.Drawing.Size(1, 245)
        Me.Label156.TabIndex = 7
        Me.Label156.Text = "label4"
        '
        'cfgEjectionFraction
        '
        Me.cfgEjectionFraction.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None
        Me.cfgEjectionFraction.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cfgEjectionFraction.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.cfgEjectionFraction.ColumnInfo = "1,0,0,0,0,95,Columns:0{StyleFixed:""TextAlign:CenterCenter;ImageAlign:CenterCenter" & _
    ";"";}" & Global.Microsoft.VisualBasic.ChrW(9)
        Me.cfgEjectionFraction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cfgEjectionFraction.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Solid
        Me.cfgEjectionFraction.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cfgEjectionFraction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.cfgEjectionFraction.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None
        Me.cfgEjectionFraction.Location = New System.Drawing.Point(0, 4)
        Me.cfgEjectionFraction.Name = "cfgEjectionFraction"
        Me.cfgEjectionFraction.Rows.Count = 1
        Me.cfgEjectionFraction.Rows.DefaultSize = 19
        Me.cfgEjectionFraction.Rows.Fixed = 0
        Me.cfgEjectionFraction.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.cfgEjectionFraction.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always
        Me.cfgEjectionFraction.ShowSortPosition = C1.Win.C1FlexGrid.ShowSortPositionEnum.None
        Me.cfgEjectionFraction.Size = New System.Drawing.Size(1009, 245)
        Me.cfgEjectionFraction.StyleInfo = resources.GetString("cfgEjectionFraction.StyleInfo")
        Me.cfgEjectionFraction.TabIndex = 39
        Me.cfgEjectionFraction.Tree.NodeImageCollapsed = CType(resources.GetObject("cfgEjectionFraction.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.cfgEjectionFraction.Tree.NodeImageExpanded = CType(resources.GetObject("cfgEjectionFraction.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'Label157
        '
        Me.Label157.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label157.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label157.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label157.Location = New System.Drawing.Point(1009, 4)
        Me.Label157.Name = "Label157"
        Me.Label157.Size = New System.Drawing.Size(1, 245)
        Me.Label157.TabIndex = 6
        Me.Label157.Text = "label3"
        '
        'Label158
        '
        Me.Label158.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label158.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label158.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label158.Location = New System.Drawing.Point(0, 3)
        Me.Label158.Name = "Label158"
        Me.Label158.Size = New System.Drawing.Size(1010, 1)
        Me.Label158.TabIndex = 5
        Me.Label158.Text = "label1"
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.Transparent
        Me.Panel8.BackgroundImage = CType(resources.GetObject("Panel8.BackgroundImage"), System.Drawing.Image)
        Me.Panel8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel8.Controls.Add(Me.pnlSearchEjection)
        Me.Panel8.Controls.Add(Me.Label151)
        Me.Panel8.Controls.Add(Me.Label152)
        Me.Panel8.Controls.Add(Me.Label153)
        Me.Panel8.Controls.Add(Me.Label154)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel8.Location = New System.Drawing.Point(0, 2)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(1010, 24)
        Me.Panel8.TabIndex = 42
        '
        'pnlSearchEjection
        '
        Me.pnlSearchEjection.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlSearchEjection.Location = New System.Drawing.Point(619, 1)
        Me.pnlSearchEjection.Name = "pnlSearchEjection"
        Me.pnlSearchEjection.Size = New System.Drawing.Size(390, 22)
        Me.pnlSearchEjection.TabIndex = 9
        '
        'Label151
        '
        Me.Label151.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label151.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label151.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label151.Location = New System.Drawing.Point(1, 23)
        Me.Label151.Name = "Label151"
        Me.Label151.Size = New System.Drawing.Size(1008, 1)
        Me.Label151.TabIndex = 8
        Me.Label151.Text = "label2"
        '
        'Label152
        '
        Me.Label152.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label152.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label152.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label152.Location = New System.Drawing.Point(0, 1)
        Me.Label152.Name = "Label152"
        Me.Label152.Size = New System.Drawing.Size(1, 23)
        Me.Label152.TabIndex = 7
        Me.Label152.Text = "label4"
        '
        'Label153
        '
        Me.Label153.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label153.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label153.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label153.Location = New System.Drawing.Point(1009, 1)
        Me.Label153.Name = "Label153"
        Me.Label153.Size = New System.Drawing.Size(1, 23)
        Me.Label153.TabIndex = 6
        Me.Label153.Text = "label3"
        '
        'Label154
        '
        Me.Label154.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label154.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label154.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label154.Location = New System.Drawing.Point(0, 0)
        Me.Label154.Name = "Label154"
        Me.Label154.Size = New System.Drawing.Size(1010, 1)
        Me.Label154.TabIndex = 5
        Me.Label154.Text = "label1"
        '
        'pnlImaging
        '
        Me.pnlImaging.Controls.Add(Me.splImaging)
        Me.pnlImaging.Controls.Add(Me.pnltrImaging)
        Me.pnlImaging.Controls.Add(Me.pnlImagingTitle)
        Me.pnlImaging.Location = New System.Drawing.Point(862, 0)
        Me.pnlImaging.Name = "pnlImaging"
        Me.pnlImaging.Size = New System.Drawing.Size(150, 252)
        Me.pnlImaging.TabIndex = 10
        Me.pnlImaging.Visible = False
        '
        'splImaging
        '
        Me.splImaging.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.splImaging.Location = New System.Drawing.Point(0, 30)
        Me.splImaging.Name = "splImaging"
        Me.splImaging.Size = New System.Drawing.Size(3, 222)
        Me.splImaging.TabIndex = 12
        Me.splImaging.TabStop = False
        Me.splImaging.Visible = False
        '
        'pnltrImaging
        '
        Me.pnltrImaging.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnltrImaging.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnltrImaging.Controls.Add(Me.trImaging)
        Me.pnltrImaging.Controls.Add(Me.Label74)
        Me.pnltrImaging.Controls.Add(Me.Label75)
        Me.pnltrImaging.Controls.Add(Me.Label36)
        Me.pnltrImaging.Controls.Add(Me.Label37)
        Me.pnltrImaging.Controls.Add(Me.Label38)
        Me.pnltrImaging.Controls.Add(Me.Label39)
        Me.pnltrImaging.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnltrImaging.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnltrImaging.Location = New System.Drawing.Point(0, 30)
        Me.pnltrImaging.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnltrImaging.Name = "pnltrImaging"
        Me.pnltrImaging.Size = New System.Drawing.Size(150, 222)
        Me.pnltrImaging.TabIndex = 20
        '
        'trImaging
        '
        Me.trImaging.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trImaging.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trImaging.ForeColor = System.Drawing.Color.Black
        Me.trImaging.HideSelection = False
        Me.trImaging.ItemHeight = 19
        Me.trImaging.Location = New System.Drawing.Point(3, 3)
        Me.trImaging.Name = "trImaging"
        Me.trImaging.ShowLines = False
        Me.trImaging.Size = New System.Drawing.Size(146, 218)
        Me.trImaging.TabIndex = 3
        '
        'Label74
        '
        Me.Label74.BackColor = System.Drawing.Color.White
        Me.Label74.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label74.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label74.Location = New System.Drawing.Point(3, 1)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(146, 2)
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
        Me.Label75.Size = New System.Drawing.Size(2, 220)
        Me.Label75.TabIndex = 11
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label36.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label36.Location = New System.Drawing.Point(1, 221)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(148, 1)
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
        Me.Label37.Size = New System.Drawing.Size(1, 221)
        Me.Label37.TabIndex = 7
        Me.Label37.Text = "label4"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label38.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label38.Location = New System.Drawing.Point(149, 1)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(1, 221)
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
        Me.Label39.Size = New System.Drawing.Size(150, 1)
        Me.Label39.TabIndex = 5
        Me.Label39.Text = "label1"
        '
        'pnlImagingTitle
        '
        Me.pnlImagingTitle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlImagingTitle.Controls.Add(Me.pnllblImaging)
        Me.pnlImagingTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlImagingTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlImagingTitle.Name = "pnlImagingTitle"
        Me.pnlImagingTitle.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.pnlImagingTitle.Size = New System.Drawing.Size(150, 30)
        Me.pnlImagingTitle.TabIndex = 1
        '
        'pnllblImaging
        '
        Me.pnllblImaging.BackColor = System.Drawing.Color.Transparent
        Me.pnllblImaging.BackgroundImage = CType(resources.GetObject("pnllblImaging.BackgroundImage"), System.Drawing.Image)
        Me.pnllblImaging.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnllblImaging.Controls.Add(Me.Label60)
        Me.pnllblImaging.Controls.Add(Me.Label61)
        Me.pnllblImaging.Controls.Add(Me.lblOrders)
        Me.pnllblImaging.Controls.Add(Me.Label62)
        Me.pnllblImaging.Controls.Add(Me.Label63)
        Me.pnllblImaging.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnllblImaging.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnllblImaging.Location = New System.Drawing.Point(0, 0)
        Me.pnllblImaging.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnllblImaging.Name = "pnllblImaging"
        Me.pnllblImaging.Size = New System.Drawing.Size(150, 27)
        Me.pnllblImaging.TabIndex = 20
        '
        'Label60
        '
        Me.Label60.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label60.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label60.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label60.Location = New System.Drawing.Point(1, 26)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(148, 1)
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
        'lblOrders
        '
        Me.lblOrders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOrders.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrders.ForeColor = System.Drawing.Color.White
        Me.lblOrders.Location = New System.Drawing.Point(0, 1)
        Me.lblOrders.Name = "lblOrders"
        Me.lblOrders.Size = New System.Drawing.Size(149, 26)
        Me.lblOrders.TabIndex = 1
        Me.lblOrders.Text = "Orders"
        Me.lblOrders.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label62
        '
        Me.Label62.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label62.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label62.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label62.Location = New System.Drawing.Point(149, 1)
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
        Me.Label63.Size = New System.Drawing.Size(150, 1)
        Me.Label63.TabIndex = 5
        Me.Label63.Text = "label1"
        '
        'pnlFill
        '
        Me.pnlFill.Controls.Add(Me.tbExamDMS)
        Me.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlFill.Location = New System.Drawing.Point(0, 314)
        Me.pnlFill.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pnlFill.Name = "pnlFill"
        Me.pnlFill.Size = New System.Drawing.Size(1024, 452)
        Me.pnlFill.TabIndex = 6
        '
        'tbExamDMS
        '
        Me.tbExamDMS.Controls.Add(Me.tbExams)
        Me.tbExamDMS.Controls.Add(Me.tbDMS)
        Me.tbExamDMS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbExamDMS.ItemSize = New System.Drawing.Size(68, 22)
        Me.tbExamDMS.Location = New System.Drawing.Point(0, 0)
        Me.tbExamDMS.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tbExamDMS.Name = "tbExamDMS"
        Me.tbExamDMS.SelectedIndex = 0
        Me.tbExamDMS.Size = New System.Drawing.Size(1024, 452)
        Me.tbExamDMS.TabIndex = 1
        '
        'tbExams
        '
        Me.tbExams.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbExams.Controls.Add(Me.Panel22)
        Me.tbExams.ImageIndex = 10
        Me.tbExams.Location = New System.Drawing.Point(4, 26)
        Me.tbExams.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tbExams.Name = "tbExams"
        Me.tbExams.Size = New System.Drawing.Size(1016, 422)
        Me.tbExams.TabIndex = 0
        Me.tbExams.Text = "Exams"
        '
        'Panel22
        '
        Me.Panel22.Controls.Add(Me.Label87)
        Me.Panel22.Controls.Add(Me.Img_Reviwed)
        Me.Panel22.Controls.Add(Me.Img_Blanck)
        Me.Panel22.Controls.Add(Me.Img_Note)
        Me.Panel22.Controls.Add(Me.C1PatientExam)
        Me.Panel22.Controls.Add(Me.pnlFilterExams)
        Me.Panel22.Controls.Add(Me.Label85)
        Me.Panel22.Controls.Add(Me.Label86)
        Me.Panel22.Controls.Add(Me.Label88)
        Me.Panel22.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel22.Location = New System.Drawing.Point(0, 0)
        Me.Panel22.Name = "Panel22"
        Me.Panel22.Size = New System.Drawing.Size(1016, 422)
        Me.Panel22.TabIndex = 25
        '
        'Label87
        '
        Me.Label87.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label87.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label87.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label87.Location = New System.Drawing.Point(1015, 26)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(1, 395)
        Me.Label87.TabIndex = 10
        Me.Label87.Text = "label3"
        '
        'Img_Reviwed
        '
        Me.Img_Reviwed.Image = CType(resources.GetObject("Img_Reviwed.Image"), System.Drawing.Image)
        Me.Img_Reviwed.Location = New System.Drawing.Point(586, 198)
        Me.Img_Reviwed.Name = "Img_Reviwed"
        Me.Img_Reviwed.Size = New System.Drawing.Size(110, 26)
        Me.Img_Reviwed.TabIndex = 25
        Me.Img_Reviwed.TabStop = False
        Me.Img_Reviwed.Visible = False
        '
        'Img_Blanck
        '
        Me.Img_Blanck.Location = New System.Drawing.Point(452, 198)
        Me.Img_Blanck.Name = "Img_Blanck"
        Me.Img_Blanck.Size = New System.Drawing.Size(110, 26)
        Me.Img_Blanck.TabIndex = 24
        Me.Img_Blanck.TabStop = False
        Me.Img_Blanck.Visible = False
        '
        'Img_Note
        '
        Me.Img_Note.Image = CType(resources.GetObject("Img_Note.Image"), System.Drawing.Image)
        Me.Img_Note.Location = New System.Drawing.Point(330, 198)
        Me.Img_Note.Name = "Img_Note"
        Me.Img_Note.Size = New System.Drawing.Size(110, 26)
        Me.Img_Note.TabIndex = 23
        Me.Img_Note.TabStop = False
        Me.Img_Note.Visible = False
        '
        'C1PatientExam
        '
        Me.C1PatientExam.BackColor = System.Drawing.Color.White
        Me.C1PatientExam.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1PatientExam.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.C1PatientExam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1PatientExam.EditOptions = C1.Win.C1FlexGrid.EditFlags.None
        Me.C1PatientExam.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1PatientExam.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.C1PatientExam.Location = New System.Drawing.Point(1, 26)
        Me.C1PatientExam.Name = "C1PatientExam"
        Me.C1PatientExam.Rows.Count = 1
        Me.C1PatientExam.Rows.DefaultSize = 19
        Me.C1PatientExam.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1PatientExam.Size = New System.Drawing.Size(1015, 395)
        Me.C1PatientExam.StyleInfo = resources.GetString("C1PatientExam.StyleInfo")
        Me.C1PatientExam.TabIndex = 20
        Me.C1PatientExam.Tree.NodeImageCollapsed = CType(resources.GetObject("C1PatientExam.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1PatientExam.Tree.NodeImageExpanded = CType(resources.GetObject("C1PatientExam.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'pnlFilterExams
        '
        Me.pnlFilterExams.BackColor = System.Drawing.Color.Transparent
        Me.pnlFilterExams.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.pnlFilterExams.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlFilterExams.Controls.Add(Me.Panel25)
        Me.pnlFilterExams.Controls.Add(Me.Panel5)
        Me.pnlFilterExams.Controls.Add(Me.Panel13)
        Me.pnlFilterExams.Controls.Add(Me.Panel14)
        Me.pnlFilterExams.Controls.Add(Me.Panel9)
        Me.pnlFilterExams.Controls.Add(Me.chkDateFilter)
        Me.pnlFilterExams.Controls.Add(Me.Panel17)
        Me.pnlFilterExams.Controls.Add(Me.Label3)
        Me.pnlFilterExams.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilterExams.Location = New System.Drawing.Point(1, 1)
        Me.pnlFilterExams.Name = "pnlFilterExams"
        Me.pnlFilterExams.Size = New System.Drawing.Size(1015, 25)
        Me.pnlFilterExams.TabIndex = 22
        '
        'Panel25
        '
        Me.Panel25.Controls.Add(Me.pnlClearSearch)
        Me.Panel25.Controls.Add(Me.Label2)
        Me.Panel25.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel25.Location = New System.Drawing.Point(898, 0)
        Me.Panel25.Name = "Panel25"
        Me.Panel25.Size = New System.Drawing.Size(117, 24)
        Me.Panel25.TabIndex = 19
        '
        'pnlClearSearch
        '
        Me.pnlClearSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlClearSearch.Controls.Add(Me.txtExamName)
        Me.pnlClearSearch.Controls.Add(Me.Label170)
        Me.pnlClearSearch.Controls.Add(Me.Label171)
        Me.pnlClearSearch.Controls.Add(Me.lbl_WhiteSpaceBottom)
        Me.pnlClearSearch.Controls.Add(Me.btnExamNameClear)
        Me.pnlClearSearch.Controls.Add(Me.lbl_pnlSearchLeftBrd)
        Me.pnlClearSearch.Controls.Add(Me.lbl_pnlSearchRightBrd)
        Me.pnlClearSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlClearSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlClearSearch.ForeColor = System.Drawing.Color.Black
        Me.pnlClearSearch.Location = New System.Drawing.Point(79, 0)
        Me.pnlClearSearch.Name = "pnlClearSearch"
        Me.pnlClearSearch.Size = New System.Drawing.Size(38, 24)
        Me.pnlClearSearch.TabIndex = 44
        '
        'txtExamName
        '
        Me.txtExamName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtExamName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtExamName.Location = New System.Drawing.Point(5, 3)
        Me.txtExamName.Name = "txtExamName"
        Me.txtExamName.Size = New System.Drawing.Size(11, 15)
        Me.txtExamName.TabIndex = 14
        '
        'Label170
        '
        Me.Label170.BackColor = System.Drawing.Color.White
        Me.Label170.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label170.Location = New System.Drawing.Point(5, 18)
        Me.Label170.Name = "Label170"
        Me.Label170.Size = New System.Drawing.Size(11, 6)
        Me.Label170.TabIndex = 43
        '
        'Label171
        '
        Me.Label171.BackColor = System.Drawing.Color.White
        Me.Label171.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label171.Location = New System.Drawing.Point(5, 0)
        Me.Label171.Name = "Label171"
        Me.Label171.Size = New System.Drawing.Size(11, 3)
        Me.Label171.TabIndex = 37
        '
        'lbl_WhiteSpaceBottom
        '
        Me.lbl_WhiteSpaceBottom.BackColor = System.Drawing.Color.White
        Me.lbl_WhiteSpaceBottom.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_WhiteSpaceBottom.Location = New System.Drawing.Point(1, 0)
        Me.lbl_WhiteSpaceBottom.Name = "lbl_WhiteSpaceBottom"
        Me.lbl_WhiteSpaceBottom.Size = New System.Drawing.Size(4, 24)
        Me.lbl_WhiteSpaceBottom.TabIndex = 38
        '
        'btnExamNameClear
        '
        Me.btnExamNameClear.BackgroundImage = CType(resources.GetObject("btnExamNameClear.BackgroundImage"), System.Drawing.Image)
        Me.btnExamNameClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExamNameClear.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnExamNameClear.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnExamNameClear.FlatAppearance.BorderSize = 0
        Me.btnExamNameClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnExamNameClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnExamNameClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExamNameClear.Image = CType(resources.GetObject("btnExamNameClear.Image"), System.Drawing.Image)
        Me.btnExamNameClear.Location = New System.Drawing.Point(16, 0)
        Me.btnExamNameClear.Name = "btnExamNameClear"
        Me.btnExamNameClear.Size = New System.Drawing.Size(21, 24)
        Me.btnExamNameClear.TabIndex = 41
        Me.btnExamNameClear.UseVisualStyleBackColor = True
        '
        'lbl_pnlSearchLeftBrd
        '
        Me.lbl_pnlSearchLeftBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchLeftBrd.Dock = System.Windows.Forms.DockStyle.Left
        Me.lbl_pnlSearchLeftBrd.Location = New System.Drawing.Point(0, 0)
        Me.lbl_pnlSearchLeftBrd.Name = "lbl_pnlSearchLeftBrd"
        Me.lbl_pnlSearchLeftBrd.Size = New System.Drawing.Size(1, 24)
        Me.lbl_pnlSearchLeftBrd.TabIndex = 39
        Me.lbl_pnlSearchLeftBrd.Text = "label4"
        '
        'lbl_pnlSearchRightBrd
        '
        Me.lbl_pnlSearchRightBrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lbl_pnlSearchRightBrd.Dock = System.Windows.Forms.DockStyle.Right
        Me.lbl_pnlSearchRightBrd.Location = New System.Drawing.Point(37, 0)
        Me.lbl_pnlSearchRightBrd.Name = "lbl_pnlSearchRightBrd"
        Me.lbl_pnlSearchRightBrd.Size = New System.Drawing.Size(1, 24)
        Me.lbl_pnlSearchRightBrd.TabIndex = 40
        Me.lbl_pnlSearchRightBrd.Text = "label4"
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 24)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Exam Name :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.cmbTemplateSpeciality)
        Me.Panel5.Controls.Add(Me.Label169)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel5.Location = New System.Drawing.Point(673, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(225, 24)
        Me.Panel5.TabIndex = 19
        '
        'cmbTemplateSpeciality
        '
        Me.cmbTemplateSpeciality.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbTemplateSpeciality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTemplateSpeciality.FormattingEnabled = True
        Me.cmbTemplateSpeciality.Location = New System.Drawing.Point(69, 0)
        Me.cmbTemplateSpeciality.Name = "cmbTemplateSpeciality"
        Me.cmbTemplateSpeciality.Size = New System.Drawing.Size(156, 22)
        Me.cmbTemplateSpeciality.TabIndex = 1
        '
        'Label169
        '
        Me.Label169.BackColor = System.Drawing.Color.Transparent
        Me.Label169.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label169.Location = New System.Drawing.Point(0, 0)
        Me.Label169.Name = "Label169"
        Me.Label169.Size = New System.Drawing.Size(69, 24)
        Me.Label169.TabIndex = 18
        Me.Label169.Text = "Specialty :"
        Me.Label169.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.cmbExamProvider)
        Me.Panel13.Controls.Add(Me.lblProvider)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel13.Location = New System.Drawing.Point(463, 0)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(210, 24)
        Me.Panel13.TabIndex = 13
        '
        'cmbExamProvider
        '
        Me.cmbExamProvider.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbExamProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbExamProvider.FormattingEnabled = True
        Me.cmbExamProvider.Location = New System.Drawing.Point(59, 0)
        Me.cmbExamProvider.Name = "cmbExamProvider"
        Me.cmbExamProvider.Size = New System.Drawing.Size(150, 22)
        Me.cmbExamProvider.TabIndex = 8
        '
        'lblProvider
        '
        Me.lblProvider.BackColor = System.Drawing.Color.Transparent
        Me.lblProvider.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblProvider.Location = New System.Drawing.Point(0, 0)
        Me.lblProvider.Name = "lblProvider"
        Me.lblProvider.Size = New System.Drawing.Size(59, 24)
        Me.lblProvider.TabIndex = 7
        Me.lblProvider.Text = "Provider :"
        Me.lblProvider.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.cmbExamStatus)
        Me.Panel14.Controls.Add(Me.lblcmbType)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel14.Location = New System.Drawing.Point(318, 0)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(145, 24)
        Me.Panel14.TabIndex = 12
        '
        'cmbExamStatus
        '
        Me.cmbExamStatus.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbExamStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbExamStatus.FormattingEnabled = True
        Me.cmbExamStatus.Location = New System.Drawing.Point(50, 0)
        Me.cmbExamStatus.Name = "cmbExamStatus"
        Me.cmbExamStatus.Size = New System.Drawing.Size(93, 22)
        Me.cmbExamStatus.TabIndex = 1
        '
        'lblcmbType
        '
        Me.lblcmbType.BackColor = System.Drawing.Color.Transparent
        Me.lblcmbType.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblcmbType.Location = New System.Drawing.Point(0, 0)
        Me.lblcmbType.Name = "lblcmbType"
        Me.lblcmbType.Size = New System.Drawing.Size(50, 24)
        Me.lblcmbType.TabIndex = 6
        Me.lblcmbType.Text = "Status :"
        Me.lblcmbType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel9
        '
        Me.Panel9.Controls.Add(Me.Panel15)
        Me.Panel9.Controls.Add(Me.Panel16)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel9.Location = New System.Drawing.Point(70, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(248, 24)
        Me.Panel9.TabIndex = 26
        '
        'Panel15
        '
        Me.Panel15.Controls.Add(Me.dtpExamTo)
        Me.Panel15.Controls.Add(Me.lblto)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel15.Location = New System.Drawing.Point(128, 0)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(120, 24)
        Me.Panel15.TabIndex = 10
        '
        'dtpExamTo
        '
        Me.dtpExamTo.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpExamTo.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpExamTo.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpExamTo.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpExamTo.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpExamTo.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtpExamTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpExamTo.Location = New System.Drawing.Point(30, 0)
        Me.dtpExamTo.Name = "dtpExamTo"
        Me.dtpExamTo.Size = New System.Drawing.Size(88, 22)
        Me.dtpExamTo.TabIndex = 5
        '
        'lblto
        '
        Me.lblto.BackColor = System.Drawing.Color.Transparent
        Me.lblto.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblto.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblto.Location = New System.Drawing.Point(0, 0)
        Me.lblto.Name = "lblto"
        Me.lblto.Size = New System.Drawing.Size(30, 24)
        Me.lblto.TabIndex = 2
        Me.lblto.Text = "To :"
        Me.lblto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel16
        '
        Me.Panel16.BackColor = System.Drawing.Color.Transparent
        Me.Panel16.Controls.Add(Me.dtpExamFrom)
        Me.Panel16.Controls.Add(Me.lblFrom)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel16.Location = New System.Drawing.Point(0, 0)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(128, 24)
        Me.Panel16.TabIndex = 11
        '
        'dtpExamFrom
        '
        Me.dtpExamFrom.CalendarForeColor = System.Drawing.Color.Maroon
        Me.dtpExamFrom.CalendarMonthBackground = System.Drawing.Color.White
        Me.dtpExamFrom.CalendarTitleBackColor = System.Drawing.Color.Orange
        Me.dtpExamFrom.CalendarTitleForeColor = System.Drawing.Color.Brown
        Me.dtpExamFrom.CalendarTrailingForeColor = System.Drawing.Color.Tomato
        Me.dtpExamFrom.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtpExamFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpExamFrom.Location = New System.Drawing.Point(42, 0)
        Me.dtpExamFrom.Name = "dtpExamFrom"
        Me.dtpExamFrom.Size = New System.Drawing.Size(88, 22)
        Me.dtpExamFrom.TabIndex = 3
        '
        'lblFrom
        '
        Me.lblFrom.BackColor = System.Drawing.Color.Transparent
        Me.lblFrom.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblFrom.Location = New System.Drawing.Point(0, 0)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(42, 24)
        Me.lblFrom.TabIndex = 4
        Me.lblFrom.Text = "From :"
        Me.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkDateFilter
        '
        Me.chkDateFilter.AutoSize = True
        Me.chkDateFilter.Dock = System.Windows.Forms.DockStyle.Left
        Me.chkDateFilter.Location = New System.Drawing.Point(55, 0)
        Me.chkDateFilter.Name = "chkDateFilter"
        Me.chkDateFilter.Size = New System.Drawing.Size(15, 24)
        Me.chkDateFilter.TabIndex = 1
        Me.chkDateFilter.UseVisualStyleBackColor = True
        '
        'Panel17
        '
        Me.Panel17.Controls.Add(Me.Label1)
        Me.Panel17.Controls.Add(Me.Panel21)
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel17.Location = New System.Drawing.Point(0, 0)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(55, 24)
        Me.Panel17.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 24)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Search :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel21
        '
        Me.Panel21.Controls.Add(Me.txtRefillRequest)
        Me.Panel21.Controls.Add(Me.txtError)
        Me.Panel21.Controls.Add(Me.Panel20)
        Me.Panel21.Controls.Add(Me.Panel12)
        Me.Panel21.Location = New System.Drawing.Point(0, 0)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Size = New System.Drawing.Size(417, 148)
        Me.Panel21.TabIndex = 25
        Me.Panel21.Visible = False
        '
        'txtRefillRequest
        '
        Me.txtRefillRequest.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.txtRefillRequest.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRefillRequest.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtRefillRequest.ForeColor = System.Drawing.Color.Black
        Me.txtRefillRequest.Location = New System.Drawing.Point(23, 0)
        Me.txtRefillRequest.Multiline = True
        Me.txtRefillRequest.Name = "txtRefillRequest"
        Me.txtRefillRequest.ReadOnly = True
        Me.txtRefillRequest.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRefillRequest.Size = New System.Drawing.Size(366, 58)
        Me.txtRefillRequest.TabIndex = 14
        Me.txtRefillRequest.TabStop = False
        '
        'txtError
        '
        Me.txtError.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.txtError.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtError.ForeColor = System.Drawing.Color.Black
        Me.txtError.Location = New System.Drawing.Point(40, 76)
        Me.txtError.Multiline = True
        Me.txtError.Name = "txtError"
        Me.txtError.ReadOnly = True
        Me.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtError.Size = New System.Drawing.Size(331, 66)
        Me.txtError.TabIndex = 11
        Me.txtError.TabStop = False
        '
        'Panel20
        '
        Me.Panel20.Controls.Add(Me.PictureBox2)
        Me.Panel20.Controls.Add(Me.PictureBox1)
        Me.Panel20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel20.Location = New System.Drawing.Point(0, 0)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Size = New System.Drawing.Size(23, 148)
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
        Me.PictureBox1.Size = New System.Drawing.Size(17, 17)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.btnbrwError)
        Me.Panel12.Controls.Add(Me.btnbrwRefill)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel12.Location = New System.Drawing.Point(389, 0)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(28, 148)
        Me.Panel12.TabIndex = 12
        '
        'btnbrwError
        '
        Me.btnbrwError.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnbrwError.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnbrwError.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnbrwError.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnbrwError.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnbrwError.Location = New System.Drawing.Point(1, 57)
        Me.btnbrwError.Name = "btnbrwError"
        Me.btnbrwError.Size = New System.Drawing.Size(30, 24)
        Me.btnbrwError.TabIndex = 0
        Me.btnbrwError.Text = "..."
        Me.btnbrwError.UseVisualStyleBackColor = True
        '
        'btnbrwRefill
        '
        Me.btnbrwRefill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnbrwRefill.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnbrwRefill.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnbrwRefill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnbrwRefill.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnbrwRefill.Location = New System.Drawing.Point(1, 0)
        Me.btnbrwRefill.Name = "btnbrwRefill"
        Me.btnbrwRefill.Size = New System.Drawing.Size(30, 24)
        Me.btnbrwRefill.TabIndex = 0
        Me.btnbrwRefill.Text = "..."
        Me.btnbrwRefill.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(0, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1015, 1)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "label1"
        '
        'Label85
        '
        Me.Label85.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label85.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label85.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label85.Location = New System.Drawing.Point(1, 421)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(1015, 1)
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
        Me.Label86.Size = New System.Drawing.Size(1, 421)
        Me.Label86.TabIndex = 11
        Me.Label86.Text = "label4"
        '
        'Label88
        '
        Me.Label88.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label88.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label88.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label88.Location = New System.Drawing.Point(0, 0)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(1016, 1)
        Me.Label88.TabIndex = 9
        Me.Label88.Text = "label1"
        '
        'tbDMS
        '
        Me.tbDMS.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.tbDMS.Controls.Add(Me.pnlDMSGrid)
        Me.tbDMS.Controls.Add(Me.pnlDMSSearch)
        Me.tbDMS.ImageIndex = 11
        Me.tbDMS.Location = New System.Drawing.Point(4, 26)
        Me.tbDMS.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.tbDMS.Name = "tbDMS"
        Me.tbDMS.Size = New System.Drawing.Size(1016, 422)
        Me.tbDMS.TabIndex = 1
        Me.tbDMS.Text = "DMS"
        '
        'pnlDMSGrid
        '
        Me.pnlDMSGrid.Controls.Add(Me.Label185)
        Me.pnlDMSGrid.Controls.Add(Me.Label179)
        Me.pnlDMSGrid.Controls.Add(Me.Label178)
        Me.pnlDMSGrid.Controls.Add(Me.C1PatientDMS)
        Me.pnlDMSGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDMSGrid.Location = New System.Drawing.Point(0, 25)
        Me.pnlDMSGrid.Name = "pnlDMSGrid"
        Me.pnlDMSGrid.Size = New System.Drawing.Size(1016, 397)
        Me.pnlDMSGrid.TabIndex = 1
        '
        'Label185
        '
        Me.Label185.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label185.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label185.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label185.Location = New System.Drawing.Point(1, 396)
        Me.Label185.Name = "Label185"
        Me.Label185.Size = New System.Drawing.Size(1014, 1)
        Me.Label185.TabIndex = 24
        Me.Label185.Text = "label4"
        '
        'Label179
        '
        Me.Label179.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label179.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label179.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label179.Location = New System.Drawing.Point(1015, 0)
        Me.Label179.Name = "Label179"
        Me.Label179.Size = New System.Drawing.Size(1, 397)
        Me.Label179.TabIndex = 23
        Me.Label179.Text = "label4"
        '
        'Label178
        '
        Me.Label178.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label178.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label178.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label178.Location = New System.Drawing.Point(0, 0)
        Me.Label178.Name = "Label178"
        Me.Label178.Size = New System.Drawing.Size(1, 397)
        Me.Label178.TabIndex = 22
        Me.Label178.Text = "label4"
        '
        'C1PatientDMS
        '
        Me.C1PatientDMS.BackColor = System.Drawing.Color.White
        Me.C1PatientDMS.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None
        Me.C1PatientDMS.ColumnInfo = "10,0,0,0,0,95,Columns:"
        Me.C1PatientDMS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.C1PatientDMS.EditOptions = C1.Win.C1FlexGrid.EditFlags.None
        Me.C1PatientDMS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1PatientDMS.Location = New System.Drawing.Point(0, 0)
        Me.C1PatientDMS.Name = "C1PatientDMS"
        Me.C1PatientDMS.Rows.Count = 1
        Me.C1PatientDMS.Rows.DefaultSize = 19
        Me.C1PatientDMS.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row
        Me.C1PatientDMS.Size = New System.Drawing.Size(1016, 397)
        Me.C1PatientDMS.StyleInfo = resources.GetString("C1PatientDMS.StyleInfo")
        Me.C1PatientDMS.TabIndex = 21
        Me.C1PatientDMS.Tree.NodeImageCollapsed = CType(resources.GetObject("C1PatientDMS.Tree.NodeImageCollapsed"), System.Drawing.Image)
        Me.C1PatientDMS.Tree.NodeImageExpanded = CType(resources.GetObject("C1PatientDMS.Tree.NodeImageExpanded"), System.Drawing.Image)
        '
        'pnlDMSSearch
        '
        Me.pnlDMSSearch.BackColor = System.Drawing.Color.Transparent
        Me.pnlDMSSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlDMSSearch.Controls.Add(Me.Panel23)
        Me.pnlDMSSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlDMSSearch.Location = New System.Drawing.Point(0, 0)
        Me.pnlDMSSearch.Name = "pnlDMSSearch"
        Me.pnlDMSSearch.Size = New System.Drawing.Size(1016, 25)
        Me.pnlDMSSearch.TabIndex = 0
        '
        'Panel23
        '
        Me.Panel23.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_Button
        Me.Panel23.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel23.Controls.Add(Me.Panel24)
        Me.Panel23.Controls.Add(Me.lblsearch)
        Me.Panel23.Controls.Add(Me.cmbSearch)
        Me.Panel23.Controls.Add(Me.Label93)
        Me.Panel23.Controls.Add(Me.Label89)
        Me.Panel23.Controls.Add(Me.Label90)
        Me.Panel23.Controls.Add(Me.Label91)
        Me.Panel23.Controls.Add(Me.Label92)
        Me.Panel23.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel23.Location = New System.Drawing.Point(0, 0)
        Me.Panel23.Name = "Panel23"
        Me.Panel23.Size = New System.Drawing.Size(1016, 25)
        Me.Panel23.TabIndex = 0
        '
        'Panel24
        '
        Me.Panel24.BackColor = System.Drawing.Color.Transparent
        Me.Panel24.Controls.Add(Me.txtSearchCriteria)
        Me.Panel24.Controls.Add(Me.Label194)
        Me.Panel24.Controls.Add(Me.Label195)
        Me.Panel24.Controls.Add(Me.Label196)
        Me.Panel24.Controls.Add(Me.Button1)
        Me.Panel24.Controls.Add(Me.Label198)
        Me.Panel24.Controls.Add(Me.Label197)
        Me.Panel24.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel24.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel24.ForeColor = System.Drawing.Color.Black
        Me.Panel24.Location = New System.Drawing.Point(355, 1)
        Me.Panel24.Name = "Panel24"
        Me.Panel24.Size = New System.Drawing.Size(241, 23)
        Me.Panel24.TabIndex = 45
        '
        'txtSearchCriteria
        '
        Me.txtSearchCriteria.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSearchCriteria.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSearchCriteria.Location = New System.Drawing.Point(5, 3)
        Me.txtSearchCriteria.Name = "txtSearchCriteria"
        Me.txtSearchCriteria.Size = New System.Drawing.Size(214, 15)
        Me.txtSearchCriteria.TabIndex = 14
        '
        'Label194
        '
        Me.Label194.BackColor = System.Drawing.Color.White
        Me.Label194.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label194.Location = New System.Drawing.Point(5, 17)
        Me.Label194.Name = "Label194"
        Me.Label194.Size = New System.Drawing.Size(214, 6)
        Me.Label194.TabIndex = 43
        '
        'Label195
        '
        Me.Label195.BackColor = System.Drawing.Color.White
        Me.Label195.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label195.Location = New System.Drawing.Point(5, 0)
        Me.Label195.Name = "Label195"
        Me.Label195.Size = New System.Drawing.Size(214, 3)
        Me.Label195.TabIndex = 37
        '
        'Label196
        '
        Me.Label196.BackColor = System.Drawing.Color.White
        Me.Label196.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label196.Location = New System.Drawing.Point(1, 0)
        Me.Label196.Name = "Label196"
        Me.Label196.Size = New System.Drawing.Size(4, 23)
        Me.Label196.TabIndex = 38
        '
        'Button1
        '
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(219, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(21, 23)
        Me.Button1.TabIndex = 41
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label198
        '
        Me.Label198.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label198.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label198.Location = New System.Drawing.Point(240, 0)
        Me.Label198.Name = "Label198"
        Me.Label198.Size = New System.Drawing.Size(1, 23)
        Me.Label198.TabIndex = 40
        Me.Label198.Text = "label4"
        '
        'Label197
        '
        Me.Label197.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label197.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label197.Location = New System.Drawing.Point(0, 0)
        Me.Label197.Name = "Label197"
        Me.Label197.Size = New System.Drawing.Size(1, 23)
        Me.Label197.TabIndex = 39
        Me.Label197.Text = "label4"
        '
        'lblsearch
        '
        Me.lblsearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblsearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.lblsearch.Location = New System.Drawing.Point(214, 1)
        Me.lblsearch.Name = "lblsearch"
        Me.lblsearch.Size = New System.Drawing.Size(141, 23)
        Me.lblsearch.TabIndex = 1
        Me.lblsearch.Text = "   Search Criteria :"
        Me.lblsearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbSearch
        '
        Me.cmbSearch.Dock = System.Windows.Forms.DockStyle.Left
        Me.cmbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearch.ForeColor = System.Drawing.Color.Black
        Me.cmbSearch.FormattingEnabled = True
        Me.cmbSearch.Location = New System.Drawing.Point(58, 1)
        Me.cmbSearch.Name = "cmbSearch"
        Me.cmbSearch.Size = New System.Drawing.Size(156, 22)
        Me.cmbSearch.TabIndex = 2
        '
        'Label93
        '
        Me.Label93.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label93.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label93.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label93.Location = New System.Drawing.Point(1, 1)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(57, 23)
        Me.Label93.TabIndex = 46
        Me.Label93.Text = "Search :"
        Me.Label93.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label89
        '
        Me.Label89.BackColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Label89.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label89.Font = New System.Drawing.Font("Tahoma", 9.0!)
        Me.Label89.Location = New System.Drawing.Point(1, 24)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(1014, 1)
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
        Me.Label91.Location = New System.Drawing.Point(1015, 1)
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
        Me.Label92.Size = New System.Drawing.Size(1016, 1)
        Me.Label92.TabIndex = 9
        Me.Label92.Text = "label1"
        '
        'Splitter1
        '
        Me.Splitter1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Splitter1.Location = New System.Drawing.Point(0, 311)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(1024, 3)
        Me.Splitter1.TabIndex = 11
        Me.Splitter1.TabStop = False
        '
        'gloUCPatientSynopsis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.Controls.Add(Me.pnlFill)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(31, Byte), Integer), CType(CType(73, Byte), Integer), CType(CType(125, Byte), Integer))
        Me.Name = "gloUCPatientSynopsis"
        Me.Size = New System.Drawing.Size(1024, 766)
        Me.pnlTop.ResumeLayout(False)
        Me.tbSummary.ResumeLayout(False)
        Me.tbpSummary.ResumeLayout(False)
        Me.pnlSummary.ResumeLayout(False)
        Me.pnlImplant.ResumeLayout(False)
        Me.pnltrImplant.ResumeLayout(False)
        Me.pnllblImplantHeader.ResumeLayout(False)
        Me.pnllblImplant.ResumeLayout(False)
        Me.pnlImagingST.ResumeLayout(False)
        Me.pnltrImagingST.ResumeLayout(False)
        Me.pnllblImagingSTHeader.ResumeLayout(False)
        Me.pnllblImagingST.ResumeLayout(False)
        Me.pnlLabs.ResumeLayout(False)
        Me.pnltrLabs.ResumeLayout(False)
        Me.pnlLabTitle.ResumeLayout(False)
        Me.pnllblLabs.ResumeLayout(False)
        Me.pnlProcedures.ResumeLayout(False)
        Me.pnltrProcedures.ResumeLayout(False)
        Me.pnlprocedureTitle.ResumeLayout(False)
        Me.pnllblProcedures.ResumeLayout(False)
        Me.pnlHistory.ResumeLayout(False)
        Me.pnltrHistory.ResumeLayout(False)
        Me.pnlHistoryTitle.ResumeLayout(False)
        Me.pnllblHistory.ResumeLayout(False)
        Me.pnlMedications.ResumeLayout(False)
        Me.pnltrMedications.ResumeLayout(False)
        Me.pnlMedicationTitle.ResumeLayout(False)
        Me.pnllblMedications.ResumeLayout(False)
        Me.pnlProblems.ResumeLayout(False)
        Me.pnltrProblemList.ResumeLayout(False)
        Me.pnlProblemsTitle.ResumeLayout(False)
        Me.pnllblProblems.ResumeLayout(False)
        Me.tbpProblem.ResumeLayout(False)
        Me.Panel28.ResumeLayout(False)
        CType(Me.c1ProblemList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearchproblemList.ResumeLayout(False)
        Me.pnlsearchProblems.ResumeLayout(False)
        Me.tbpMedications.ResumeLayout(False)
        Me.pnldgPatientDetails.ResumeLayout(False)
        CType(Me.C1MedicationDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearchMedications.ResumeLayout(False)
        Me.pnlSearchMed.ResumeLayout(False)
        Me.tbpAllergies.ResumeLayout(False)
        Me.pnlC1dgPatientDetails.ResumeLayout(False)
        CType(Me.C1dgPatientDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearchAllergies.ResumeLayout(False)
        Me.tbProcedures.ResumeLayout(False)
        Me.pnltrProcedureDetails.ResumeLayout(False)
        Me.pnlNormalSearchProc.ResumeLayout(False)
        Me.pnlNormalSearchProc.PerformLayout()
        CType(Me.C1Dignosis, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearchProcedures.ResumeLayout(False)
        Me.pnlSearchProc.ResumeLayout(False)
        Me.pnlSearchProc.PerformLayout()
        Me.Panel19.ResumeLayout(False)
        Me.Panel19.PerformLayout()
        Me.pnlDateSearch.ResumeLayout(False)
        Me.tbpLabs.ResumeLayout(False)
        Me.tbpImaging.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        CType(Me.C1OrderDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlSearchImaging.ResumeLayout(False)
        Me.pnlSearchImaging.PerformLayout()
        Me.Panel18.ResumeLayout(False)
        Me.Panel18.PerformLayout()
        Me.tbpImagingST.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.C1CV_StressTest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        Me.tbpImplant.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        CType(Me.C1Cardiology, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.tbpEjectFrac.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        CType(Me.cfgEjectionFraction, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        Me.pnlImaging.ResumeLayout(False)
        Me.pnltrImaging.ResumeLayout(False)
        Me.pnlImagingTitle.ResumeLayout(False)
        Me.pnllblImaging.ResumeLayout(False)
        Me.pnlFill.ResumeLayout(False)
        Me.tbExamDMS.ResumeLayout(False)
        Me.tbExams.ResumeLayout(False)
        Me.Panel22.ResumeLayout(False)
        CType(Me.Img_Reviwed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Img_Blanck, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Img_Note, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.C1PatientExam, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFilterExams.ResumeLayout(False)
        Me.pnlFilterExams.PerformLayout()
        Me.Panel25.ResumeLayout(False)
        Me.pnlClearSearch.ResumeLayout(False)
        Me.pnlClearSearch.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel13.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
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
        Me.pnlDMSGrid.ResumeLayout(False)
        CType(Me.C1PatientDMS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDMSSearch.ResumeLayout(False)
        Me.Panel23.ResumeLayout(False)
        Me.Panel24.ResumeLayout(False)
        Me.Panel24.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlFill As System.Windows.Forms.Panel
    Friend WithEvents tbExamDMS As System.Windows.Forms.TabControl
    Friend WithEvents tbExams As System.Windows.Forms.TabPage
    Friend WithEvents Panel22 As System.Windows.Forms.Panel
    Private WithEvents Label85 As System.Windows.Forms.Label
    Private WithEvents Label86 As System.Windows.Forms.Label
    Private WithEvents Label87 As System.Windows.Forms.Label
    Private WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents tbDMS As System.Windows.Forms.TabPage
    Friend WithEvents tbSummary As System.Windows.Forms.TabControl
    Friend WithEvents tbpSummary As System.Windows.Forms.TabPage
    Friend WithEvents pnlSummary As System.Windows.Forms.Panel
    Friend WithEvents pnlImaging As System.Windows.Forms.Panel
    Friend WithEvents pnltrImaging As System.Windows.Forms.Panel
    Friend WithEvents trImaging As System.Windows.Forms.TreeView
    Private WithEvents Label74 As System.Windows.Forms.Label
    Private WithEvents Label75 As System.Windows.Forms.Label
    Private WithEvents Label36 As System.Windows.Forms.Label
    Private WithEvents Label37 As System.Windows.Forms.Label
    Private WithEvents Label38 As System.Windows.Forms.Label
    Private WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents pnlImagingTitle As System.Windows.Forms.Panel
    Friend WithEvents pnllblImaging As System.Windows.Forms.Panel
    Private WithEvents Label60 As System.Windows.Forms.Label
    Private WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents lblOrders As System.Windows.Forms.Label
    Private WithEvents Label62 As System.Windows.Forms.Label
    Private WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents spltLabs As System.Windows.Forms.Splitter
    Friend WithEvents pnlLabs As System.Windows.Forms.Panel
    Friend WithEvents pnltrLabs As System.Windows.Forms.Panel
    Friend WithEvents trLabs As System.Windows.Forms.TreeView
    Private WithEvents Label72 As System.Windows.Forms.Label
    Private WithEvents Label73 As System.Windows.Forms.Label
    Private WithEvents Label32 As System.Windows.Forms.Label
    Private WithEvents Label33 As System.Windows.Forms.Label
    Private WithEvents Label34 As System.Windows.Forms.Label
    Private WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents pnlLabTitle As System.Windows.Forms.Panel
    Friend WithEvents pnllblLabs As System.Windows.Forms.Panel
    Private WithEvents Label56 As System.Windows.Forms.Label
    Private WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents lblLabs As System.Windows.Forms.Label
    Private WithEvents Label58 As System.Windows.Forms.Label
    Private WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents spltProcedures As System.Windows.Forms.Splitter
    Friend WithEvents pnlProcedures As System.Windows.Forms.Panel
    Friend WithEvents pnltrProcedures As System.Windows.Forms.Panel
    Friend WithEvents trProcedures As System.Windows.Forms.TreeView
    Private WithEvents Label70 As System.Windows.Forms.Label
    Private WithEvents Label71 As System.Windows.Forms.Label
    Private WithEvents Label27 As System.Windows.Forms.Label
    Private WithEvents Label28 As System.Windows.Forms.Label
    Private WithEvents Label29 As System.Windows.Forms.Label
    Private WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents pnlprocedureTitle As System.Windows.Forms.Panel
    Friend WithEvents pnllblProcedures As System.Windows.Forms.Panel
    Private WithEvents Label52 As System.Windows.Forms.Label
    Private WithEvents Label53 As System.Windows.Forms.Label
    Private WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents lblProcedures As System.Windows.Forms.Label
    Private WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents spltHistory As System.Windows.Forms.Splitter
    Friend WithEvents pnlHistory As System.Windows.Forms.Panel
    Friend WithEvents pnltrHistory As System.Windows.Forms.Panel
    Friend WithEvents trHistory As System.Windows.Forms.TreeView
    Private WithEvents Label68 As System.Windows.Forms.Label
    Private WithEvents Label69 As System.Windows.Forms.Label
    Private WithEvents Label23 As System.Windows.Forms.Label
    Private WithEvents Label24 As System.Windows.Forms.Label
    Private WithEvents Label25 As System.Windows.Forms.Label
    Private WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents pnlHistoryTitle As System.Windows.Forms.Panel
    Friend WithEvents pnllblHistory As System.Windows.Forms.Panel
    Private WithEvents Label48 As System.Windows.Forms.Label
    Private WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents lblHistory As System.Windows.Forms.Label
    Private WithEvents Label50 As System.Windows.Forms.Label
    Private WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents spltMedications As System.Windows.Forms.Splitter
    Friend WithEvents pnlMedications As System.Windows.Forms.Panel
    Friend WithEvents pnltrMedications As System.Windows.Forms.Panel
    Friend WithEvents trMedications As System.Windows.Forms.TreeView
    Private WithEvents Label66 As System.Windows.Forms.Label
    Private WithEvents Label67 As System.Windows.Forms.Label
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents Label16 As System.Windows.Forms.Label
    Private WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents pnlMedicationTitle As System.Windows.Forms.Panel
    Friend WithEvents pnllblMedications As System.Windows.Forms.Panel
    Private WithEvents Label47 As System.Windows.Forms.Label
    Private WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents lblMedications As System.Windows.Forms.Label
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents spltProblems As System.Windows.Forms.Splitter
    Friend WithEvents pnlProblems As System.Windows.Forms.Panel
    Friend WithEvents pnltrProblemList As System.Windows.Forms.Panel
    Friend WithEvents trProblemList As System.Windows.Forms.TreeView
    Private WithEvents Label65 As System.Windows.Forms.Label
    Private WithEvents Label64 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents pnlProblemsTitle As System.Windows.Forms.Panel
    Friend WithEvents pnllblProblems As System.Windows.Forms.Panel
    Private WithEvents Label40 As System.Windows.Forms.Label
    Private WithEvents Label41 As System.Windows.Forms.Label
    Private WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents lblProblems As System.Windows.Forms.Label
    Private WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents tbpProblem As System.Windows.Forms.TabPage
    Friend WithEvents pnlSearchproblemList As System.Windows.Forms.Panel
    Friend WithEvents pnlsearchProblems As System.Windows.Forms.Panel
    Private WithEvents Label128 As System.Windows.Forms.Label
    Private WithEvents Label129 As System.Windows.Forms.Label
    Private WithEvents Label130 As System.Windows.Forms.Label
    Private WithEvents Label131 As System.Windows.Forms.Label
    Friend WithEvents tbpMedications As System.Windows.Forms.TabPage
    Friend WithEvents pnlSearchMedications As System.Windows.Forms.Panel
    Friend WithEvents pnlSearchMed As System.Windows.Forms.Panel
    Private WithEvents Label140 As System.Windows.Forms.Label
    Private WithEvents Label141 As System.Windows.Forms.Label
    Private WithEvents Label142 As System.Windows.Forms.Label
    Private WithEvents Label143 As System.Windows.Forms.Label
    Friend WithEvents tbpAllergies As System.Windows.Forms.TabPage
    Friend WithEvents tbProcedures As System.Windows.Forms.TabPage
    Friend WithEvents pnltrProcedureDetails As System.Windows.Forms.Panel
    Friend WithEvents trProcedureDetails As System.Windows.Forms.TreeView
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Private WithEvents Label124 As System.Windows.Forms.Label
    Private WithEvents Label126 As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceTop As System.Windows.Forms.Label
    Private WithEvents Label127 As System.Windows.Forms.Label
    Private WithEvents Label125 As System.Windows.Forms.Label
    Friend WithEvents pnlSearchProcedures As System.Windows.Forms.Panel
    Friend WithEvents pnlSearchProc As System.Windows.Forms.Panel
    Private WithEvents Label132 As System.Windows.Forms.Label
    Private WithEvents Label133 As System.Windows.Forms.Label
    Private WithEvents Label134 As System.Windows.Forms.Label
    Private WithEvents Label135 As System.Windows.Forms.Label
    Friend WithEvents tbpLabs As System.Windows.Forms.TabPage
    Friend WithEvents tbpImaging As System.Windows.Forms.TabPage
    Friend WithEvents pnlLabDetails As System.Windows.Forms.Panel
    Friend WithEvents C1OrderDetails As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlC1dgPatientDetails As System.Windows.Forms.Panel
    Private WithEvents Label120 As System.Windows.Forms.Label
    Private WithEvents Label121 As System.Windows.Forms.Label
    Private WithEvents Label122 As System.Windows.Forms.Label
    Private WithEvents Label123 As System.Windows.Forms.Label
    Friend WithEvents C1dgPatientDetails As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnldgPatientDetails As System.Windows.Forms.Panel
    Private WithEvents Label144 As System.Windows.Forms.Label
    Private WithEvents Label145 As System.Windows.Forms.Label
    Private WithEvents Label146 As System.Windows.Forms.Label
    Private WithEvents Label147 As System.Windows.Forms.Label
    Friend WithEvents Panel28 As System.Windows.Forms.Panel
    Private WithEvents Label119 As System.Windows.Forms.Label
    Private WithEvents Label116 As System.Windows.Forms.Label
    Private WithEvents Label117 As System.Windows.Forms.Label
    Private WithEvents Label118 As System.Windows.Forms.Label
    Friend WithEvents c1ProblemList As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Private WithEvents C1PatientExam As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlFilterExams As System.Windows.Forms.Panel
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents cmbExamProvider As System.Windows.Forms.ComboBox
    Friend WithEvents lblProvider As System.Windows.Forms.Label
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents cmbExamStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblcmbType As System.Windows.Forms.Label
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents dtpExamTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblto As System.Windows.Forms.Label
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents dtpExamFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Friend WithEvents txtRefillRequest As System.Windows.Forms.TextBox
    Friend WithEvents txtError As System.Windows.Forms.TextBox
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents btnbrwError As System.Windows.Forms.Button
    Friend WithEvents btnbrwRefill As System.Windows.Forms.Button
    Public WithEvents Img_Reviwed As System.Windows.Forms.PictureBox
    Public WithEvents Img_Blanck As System.Windows.Forms.PictureBox
    Public WithEvents Img_Note As System.Windows.Forms.PictureBox
    Friend WithEvents pnlDMSGrid As System.Windows.Forms.Panel
    Public WithEvents C1PatientDMS As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents pnlDMSSearch As System.Windows.Forms.Panel
    Friend WithEvents Panel23 As System.Windows.Forms.Panel
    Friend WithEvents lblsearch As System.Windows.Forms.Label
    Friend WithEvents cmbSearch As System.Windows.Forms.ComboBox
    Private WithEvents Label89 As System.Windows.Forms.Label
    Private WithEvents Label90 As System.Windows.Forms.Label
    Private WithEvents Label91 As System.Windows.Forms.Label
    Private WithEvents Label92 As System.Windows.Forms.Label
    Friend WithEvents txtExamName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label

    Private Sub txtExamName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtExamName.TextChanged
        Try
            If txtExamName.Text.Trim.Length = 0 Then

                blnIsExam = True

                FillExamDMS()

            Else
                m_ExamFilter = True
                blnIsExam = True

                SearchExams()
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Searched Exams having substring " & txtExamName.Text.Trim, gstrLoginName, gstrClientMachineName, pID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SysnopsisScreen, gloAuditTrail.ActivityType.Query, "Searched Exams having substring " & txtExamName.Text.Trim, gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.Query, "Searched Exams having substring " & txtExamName.Text.Trim, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
                m_ExamFilter = False
            End If
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Could not search Exams having substring " & txtExamName.Text.Trim, gstrLoginName, gstrClientMachineName, pID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SysnopsisScreen, gloAuditTrail.ActivityType.Query, "Could not search Exams having substring " & txtExamName.Text.Trim, gloAuditTrail.ActivityOutCome.Failure)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.Query, "Could not search Exams having substring " & txtExamName.Text.Trim, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub
    Private WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pnlSearchAllergies As System.Windows.Forms.Panel
    Private WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents Label13 As System.Windows.Forms.Label
    Private WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Private WithEvents Label20 As System.Windows.Forms.Label
    Private WithEvents Label30 As System.Windows.Forms.Label
    Private WithEvents Label76 As System.Windows.Forms.Label
    Private WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents pnlSearchImaging As System.Windows.Forms.Panel
    Private WithEvents Label15 As System.Windows.Forms.Label
    Private WithEvents Label17 As System.Windows.Forms.Label
    Private WithEvents Label18 As System.Windows.Forms.Label
    Private WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents pnlSearch As System.Windows.Forms.Panel
    Friend WithEvents pnlNormalSearchProc As System.Windows.Forms.Panel
    Friend WithEvents txtsearchProcedures As gloUserControlLibrary.gloSearchTextBox
    Friend WithEvents C1Dignosis As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents tbpImagingST As System.Windows.Forms.TabPage
    Friend WithEvents tbpImplant As System.Windows.Forms.TabPage
    Friend WithEvents pnlImplant As System.Windows.Forms.Panel
    Friend WithEvents pnltrImplant As System.Windows.Forms.Panel
    Friend WithEvents trImplant As System.Windows.Forms.TreeView
    Private WithEvents Label98 As System.Windows.Forms.Label
    Private WithEvents Label99 As System.Windows.Forms.Label
    Private WithEvents Label100 As System.Windows.Forms.Label
    Private WithEvents Label101 As System.Windows.Forms.Label
    Private WithEvents Label102 As System.Windows.Forms.Label
    Private WithEvents Label103 As System.Windows.Forms.Label
    Friend WithEvents pnllblImplantHeader As System.Windows.Forms.Panel
    Friend WithEvents pnllblImplant As System.Windows.Forms.Panel
    Private WithEvents Label104 As System.Windows.Forms.Label
    Private WithEvents Label105 As System.Windows.Forms.Label
    Friend WithEvents lblImplant As System.Windows.Forms.Label
    Private WithEvents Label107 As System.Windows.Forms.Label
    Private WithEvents Label108 As System.Windows.Forms.Label
    Friend WithEvents splImagingST As System.Windows.Forms.Splitter
    Friend WithEvents pnlImagingST As System.Windows.Forms.Panel
    Friend WithEvents pnltrImagingST As System.Windows.Forms.Panel
    Friend WithEvents trImagingST As System.Windows.Forms.TreeView
    Private WithEvents Label78 As System.Windows.Forms.Label
    Private WithEvents Label79 As System.Windows.Forms.Label
    Private WithEvents Label80 As System.Windows.Forms.Label
    Private WithEvents Label81 As System.Windows.Forms.Label
    Private WithEvents Label82 As System.Windows.Forms.Label
    Private WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents pnllblImagingSTHeader As System.Windows.Forms.Panel
    Friend WithEvents pnllblImagingST As System.Windows.Forms.Panel
    Private WithEvents Label84 As System.Windows.Forms.Label
    Private WithEvents Label94 As System.Windows.Forms.Label
    Friend WithEvents lblImaging As System.Windows.Forms.Label
    Private WithEvents Label96 As System.Windows.Forms.Label
    Private WithEvents Label97 As System.Windows.Forms.Label
    Friend WithEvents splImaging As System.Windows.Forms.Splitter
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Private WithEvents Label115 As System.Windows.Forms.Label
    Private WithEvents Label136 As System.Windows.Forms.Label
    Friend WithEvents C1CV_StressTest As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label137 As System.Windows.Forms.Label
    Private WithEvents Label138 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtImagingSearch As System.Windows.Forms.TextBox
    Private WithEvents Label95 As System.Windows.Forms.Label
    Private WithEvents Label106 As System.Windows.Forms.Label
    Private WithEvents Label109 As System.Windows.Forms.Label
    Private WithEvents Label110 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Private WithEvents Label139 As System.Windows.Forms.Label
    Private WithEvents Label148 As System.Windows.Forms.Label
    Friend WithEvents C1Cardiology As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label149 As System.Windows.Forms.Label
    Private WithEvents Label150 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents pnlSearchImplant As System.Windows.Forms.Panel
    Private WithEvents Label111 As System.Windows.Forms.Label
    Private WithEvents Label112 As System.Windows.Forms.Label
    Private WithEvents Label113 As System.Windows.Forms.Label
    Private WithEvents Label114 As System.Windows.Forms.Label
    Friend WithEvents tbpEjectFrac As System.Windows.Forms.TabPage
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Private WithEvents Label155 As System.Windows.Forms.Label
    Private WithEvents Label156 As System.Windows.Forms.Label
    Friend WithEvents cfgEjectionFraction As C1.Win.C1FlexGrid.C1FlexGrid
    Private WithEvents Label157 As System.Windows.Forms.Label
    Private WithEvents Label158 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents pnlSearchEjection As System.Windows.Forms.Panel
    Private WithEvents Label151 As System.Windows.Forms.Label
    Private WithEvents Label152 As System.Windows.Forms.Label
    Private WithEvents Label153 As System.Windows.Forms.Label
    Private WithEvents Label154 As System.Windows.Forms.Label
    Public WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Public WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblSearchBy As System.Windows.Forms.Label
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Public WithEvents cmbCriteria As System.Windows.Forms.ComboBox
    Friend WithEvents pnlDateSearch As System.Windows.Forms.Panel
    Friend WithEvents Label159 As System.Windows.Forms.Label
    Friend WithEvents btnProbExpand As System.Windows.Forms.Button
    Friend WithEvents btnImagSTExpand As System.Windows.Forms.Button
    Friend WithEvents btnLabExpand As System.Windows.Forms.Button
    Friend WithEvents btnProcedExpand As System.Windows.Forms.Button
    Friend WithEvents btnHistExpand As System.Windows.Forms.Button
    Friend WithEvents btnMedExpand As System.Windows.Forms.Button
    Friend WithEvents btnImplantExpand As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents C1MedicationDetails As C1.Win.C1FlexGrid.C1FlexGrid
    Friend WithEvents Label160 As System.Windows.Forms.Label
    Friend WithEvents Label162 As System.Windows.Forms.Label
    Friend WithEvents Label161 As System.Windows.Forms.Label
    Friend WithEvents Label164 As System.Windows.Forms.Label
    Friend WithEvents Label163 As System.Windows.Forms.Label
    Friend WithEvents Label166 As System.Windows.Forms.Label
    Friend WithEvents Label165 As System.Windows.Forms.Label
    Friend WithEvents Label167 As System.Windows.Forms.Label
    Friend WithEvents Label168 As System.Windows.Forms.Label
    Friend WithEvents ImgPatientTab As System.Windows.Forms.ImageList
    Friend WithEvents trimagingecho As System.Windows.Forms.TreeView
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents cmbTemplateSpeciality As System.Windows.Forms.ComboBox
    Friend WithEvents Label169 As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents chkDateFilter As System.Windows.Forms.CheckBox
    Friend WithEvents txtSearchCriteria As gloUserControlLibrary.gloSearchTextBox
    Friend WithEvents pnlClearSearch As System.Windows.Forms.Panel
    Friend WithEvents Label170 As System.Windows.Forms.Label
    Friend WithEvents Label171 As System.Windows.Forms.Label
    Friend WithEvents lbl_WhiteSpaceBottom As System.Windows.Forms.Label
    Friend WithEvents btnExamNameClear As System.Windows.Forms.Button
    Private WithEvents lbl_pnlSearchLeftBrd As System.Windows.Forms.Label
    Private WithEvents lbl_pnlSearchRightBrd As System.Windows.Forms.Label
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Label172 As System.Windows.Forms.Label
    Friend WithEvents Label174 As System.Windows.Forms.Label
    Friend WithEvents Label175 As System.Windows.Forms.Label
    Friend WithEvents btnClearImaging As System.Windows.Forms.Button
    Private WithEvents Label176 As System.Windows.Forms.Label
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label180 As System.Windows.Forms.Label
    Friend WithEvents Label181 As System.Windows.Forms.Label
    Friend WithEvents Label182 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Private WithEvents Label183 As System.Windows.Forms.Label
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents Label187 As System.Windows.Forms.Label
    Friend WithEvents Label188 As System.Windows.Forms.Label
    Friend WithEvents Label189 As System.Windows.Forms.Label
    Friend WithEvents btnClearProcedures As System.Windows.Forms.Button
    Private WithEvents Label190 As System.Windows.Forms.Label
    Friend WithEvents Panel24 As System.Windows.Forms.Panel
    Friend WithEvents Label194 As System.Windows.Forms.Label
    Friend WithEvents Label195 As System.Windows.Forms.Label
    Friend WithEvents Label196 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Private WithEvents Label197 As System.Windows.Forms.Label
    Private WithEvents Label198 As System.Windows.Forms.Label
    Private WithEvents Label185 As System.Windows.Forms.Label
    Private WithEvents Label179 As System.Windows.Forms.Label
    Private WithEvents Label178 As System.Windows.Forms.Label
    Friend WithEvents Label93 As System.Windows.Forms.Label
    Friend WithEvents Panel25 As System.Windows.Forms.Panel
    ' Friend WithEvents ServiceController1 As System.ServiceProcess.ServiceController
End Class
